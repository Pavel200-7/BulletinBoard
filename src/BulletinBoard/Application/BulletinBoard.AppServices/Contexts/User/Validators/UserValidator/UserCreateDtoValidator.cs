using BulletinBoard.AppServices.Contexts.User.Validators.UserValidator.IValidators;
using BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BulletinBoard.AppServices.Contexts.User.Validators.UserValidator;

/// <inheritdoc/>
public class UserCreateDtoValidator : AbstractValidator<ApplicationUserCreateDto>, IUserCreateDtoValidator
{
    /// <inheritdoc/>
    public UserCreateDtoValidator()
    {
        RuleFor(createDto => createDto.UserName)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 50).WithMessage("The user name must contain from 3 to 50 characters.")
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ0-9_.\-]+$").WithMessage("The user's name can contain only letters (Russian/English), numbers, dots, hyphens, and underscores.")
            .Must(NotContainOnlyNumbers).WithMessage("The user name should not contain only numbers.")
            .Must(NotContainOnlySpecialCharacters).WithMessage("The user name should not contain only special characters.");

        RuleFor(createDto => createDto.PhoneNumber)
            .NotEmpty().WithMessage("The phone number is required")
            .Matches(@"^[\+]?[0-9\-\s\(\)]{10,15}$").WithMessage("The phone number must be in the international format: +7XXX... or 8XXX...")
            .Must(BeValidPhoneNumber).WithMessage("Incorrect phone number format");

        RuleFor(createDto => createDto.Latitude)
            .InclusiveBetween(-90.0, 90.0).WithMessage("The latitude should be in the range from -90 to 90 degrees.");  

        RuleFor(createDto => createDto.Longitude)
            .InclusiveBetween(-180.0, 180.0).WithMessage("The longitude should be in the range from -180 to 180 degrees."); 

        RuleFor(createDto => createDto.FormattedAddress)
            .MaximumLength(500).WithMessage("The address must not exceed 500 characters")
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ0-9\s\.,\-\(\)\/\\№#:;""'&]+$").WithMessage("The address contains invalid characters");
    }

    /// <summary>
    /// Проверяет, что username не состоит только из цифр
    /// </summary>
    private bool NotContainOnlyNumbers(string username)
    {
        return !Regex.IsMatch(username, @"^[0-9]+$");
    }

    /// <summary>
    /// Проверяет, что username не состоит только из спецсимволов
    /// </summary>
    private bool NotContainOnlySpecialCharacters(string username)
    {
        return !Regex.IsMatch(username, @"^[_.\-]+$");
    }

    /// <summary>
    /// Дополнительная валидация телефонного номера
    /// </summary>
    private bool BeValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Убираем все нецифровые символы для проверки длины
        var digitsOnly = Regex.Replace(phoneNumber, @"[^\d]", "");

        // Международный формат: от 10 до 15 цифр (включая код страны)
        return digitsOnly.Length >= 10 && digitsOnly.Length <= 15;
    }
}