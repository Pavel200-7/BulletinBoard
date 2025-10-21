using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.CreateDto;
using BulletinBoard.Contracts.Bulletin.Aggregates.Bulletin.FilterDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinValidator;

/// <inheritdoc/>
public class BulletinPaginationRequestDtoValidator : AbstractValidator<BulletinPaginationRequestDto>, IBulletinPaginationRequestDtoValidator
{
    /// <inheritdoc/>
    public BulletinPaginationRequestDtoValidator()
    {
        RuleFor(requestDto => requestDto.Limit)
            .InclusiveBetween(5, 50)
                .WithMessage("Must be between 5 and 50.");

        RuleFor(requestDto => requestDto.SortBy)
            .NotEmpty()
                .WithMessage("The field is requered.")
            .Must(SortBy => new string[] { "date", "title", "price" }.Contains(SortBy?.ToLower()))
                .WithMessage("Sort by expression can have only one of this values: \"date\" / \"title\" / \"price\"");

        RuleFor(requestDto => requestDto.SortOrder)
            .NotEmpty()
                .WithMessage("SortOrder is required")
            .Must(sortOrder => new[] { "asc", "desc" }.Contains(sortOrder?.ToLower()))
                .WithMessage("SortOrder must be 'asc' or 'desc'");

        RuleFor(requestDto => requestDto.MinPrice)
           .GreaterThanOrEqualTo(0).WithMessage("Must be not negative number");

        RuleFor(requestDto => requestDto.MaxPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Must be not negative number");

        // Price validation
        RuleFor(requestDto => requestDto)
            .Must(requestDto => requestDto.MaxPrice >= requestDto.MinPrice)
                .WithMessage("Max price must be greater or equat to min price.")
                .When(requestDto => (requestDto.MinPrice.HasValue && requestDto.MaxPrice.HasValue));
        
        RuleFor(requestDto => requestDto.SearchText)
            .MaximumLength(100)
            .WithMessage("SearchText cannot exceed 100 characters")
            .When(requestDto => !string.IsNullOrEmpty(requestDto.SearchText));

        // pagination data validation
        RuleFor(requestDto => requestDto)
            .Must(requestDto => !(requestDto.SortBy == "date" && requestDto.LastId.HasValue) || requestDto.LastDate.HasValue)
                .WithMessage("This page require last date")
            .Must(requestDto => !(requestDto.SortBy == "price" && requestDto.LastId.HasValue) || requestDto.LastPrice.HasValue)
                .WithMessage("This page require last price")
            .Must(requestDto => !(requestDto.SortBy == "title" && requestDto.LastId.HasValue) || requestDto.LastTitle is not null)
                .WithMessage("This page require last title");
    }
}
