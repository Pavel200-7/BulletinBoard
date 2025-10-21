using AutoMapper;
using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.CreateDto;
using BulletinBoard.Contracts.Bulletin.BulletinCategory.UpdateDto;
using BulletinBoard.Contracts.Errors.Exeptions;
using FluentValidation.Results;
using Moq;
using System.Threading;


namespace BulletinBoard.Tests.Application.BulletinBoard.AppServices.Contexts.Bulletin.Services;

public class BulletinCategoryServiceTests
{
    private readonly Mock<IBulletinCategoryRepository> _mockRepo;
    private readonly Mock<IBulletinCategoryDtoValidatorFacade> _mockValidator;
    private readonly Mock<IMapper> _mockAutoMapper;
    private readonly Mock<IBulletinCategorySpecificationBuilder> _mockSpecBuilder;
    private readonly Mock<IBulletinCategoryMappingService> _mockMapper;

    private readonly BulletinCategoryService _service;

    public BulletinCategoryServiceTests()
    {
        _mockRepo = new Mock<IBulletinCategoryRepository>();
        _mockValidator = new Mock<IBulletinCategoryDtoValidatorFacade>();
        _mockAutoMapper = new Mock<IMapper>();
        _mockSpecBuilder = new Mock<IBulletinCategorySpecificationBuilder>();
        _mockMapper = new Mock<IBulletinCategoryMappingService>();

        _service = new BulletinCategoryService
            (
            _mockRepo.Object,
            _mockValidator.Object,
            _mockAutoMapper.Object,
            _mockSpecBuilder.Object,
            _mockMapper.Object
            );
    }

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsCreatedCategory()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var inputDto = new BulletinCategoryCreateDto { ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };
        var expectedOutput = new BulletinCategoryDto { Id = Guid.NewGuid(), ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.CreateAsync(inputDto, cancellationToken))
                .ReturnsAsync(expectedOutput);

        // Act
        var result = await _service.CreateAsync(inputDto, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedOutput.Id, result.Id);
        Assert.Equal(expectedOutput.CategoryName, result.CategoryName);
        Assert.Equal(expectedOutput.ParentCategoryId, result.ParentCategoryId);
        Assert.Equal(expectedOutput.IsLeafy, result.IsLeafy);
    }

    [Fact]
    public async Task CreateAsync_InvalidDto_ThrowsValidationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var inputDto = new BulletinCategoryCreateDto { ParentCategoryId = null, CategoryName = "", IsLeafy = true };
        var validationErrors = new Dictionary<string, string[]>()
        {
            { "CategoryName", ["Name is required"] }
        };

        _mockValidator.Setup(v => v.ValidateThrowValidationExeptionAsync(inputDto))
                     .Throws(new ValidationExeption(validationErrors));

        _mockRepo.Setup(r => r.CreateAsync(inputDto, cancellationToken))
                .ReturnsAsync(new BulletinCategoryDto());

        // Act & Assert
        await Assert.ThrowsAsync<ValidationExeption>(() =>
            _service.CreateAsync(inputDto, cancellationToken));
    }

    [Fact]
    public async Task CreateAsync_RepositoryThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var inputDto = new BulletinCategoryCreateDto { ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.CreateAsync(inputDto, cancellationToken))
                .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.CreateAsync(inputDto, cancellationToken));
    }

    [Fact]
    public async Task UpdateAsync_ValidDto_ReturnsUpdatedCategory()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = "Test Category" };

        var inputDtoForValidating = new BulletinCategoryUpdateDtoForValidating()
        {
            ParentCategoryId = inputDto.ParentCategoryId,
            CategoryName = inputDto.CategoryName
        };

        var expectedOutput = new BulletinCategoryDto { Id = id, ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };

        _mockValidator.Setup(v => v.ValidateAsync(inputDtoForValidating))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.UpdateAsync(id, inputDto, cancellationToken))
                .ReturnsAsync(expectedOutput);

        // Act
        var result = await _service.UpdateAsync(id, inputDto, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedOutput.Id, result.Id);
        Assert.Equal(expectedOutput.CategoryName, result.CategoryName);
        Assert.Equal(expectedOutput.ParentCategoryId, result.ParentCategoryId);
        Assert.Equal(expectedOutput.IsLeafy, result.IsLeafy);
    }

    [Fact]
    public async Task UpdateAsync_InvalidDto_ThrowsValidationException()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = "" };

        var validationErrors = new Dictionary<string, string[]>()
        {
            { "CategoryName", ["Name is required"] }
        };

        _mockValidator.Setup(v => v.ValidateThrowValidationExeptionAsync(It.IsAny<BulletinCategoryUpdateDtoForValidating>()))
                     .Throws(new ValidationExeption(validationErrors));

        // Act & Assert
        await Assert.ThrowsAsync<ValidationExeption>(() =>
            _service.UpdateAsync(id, inputDto, cancellationToken));
    }

    [Fact]
    public async Task UpdateAsync_NotFindExeption()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = "Test Category" };

        var inputDtoForValidating = new BulletinCategoryUpdateDtoForValidating()
        {
            ParentCategoryId = inputDto.ParentCategoryId,
            CategoryName = inputDto.CategoryName
        };

        BulletinCategoryDto? expected = null;

        _mockRepo.Setup(r => r.UpdateAsync(id, inputDto, cancellationToken))
            .ReturnsAsync(expected);

        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.UpdateAsync(id, inputDto, cancellationToken));
    }


    [Fact]
    public async Task UpdateAsync_RepositoryThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = "Test Category" };

        var inputDtoForValidating = new BulletinCategoryUpdateDtoForValidating()
        {
            ParentCategoryId = inputDto.ParentCategoryId,
            CategoryName = inputDto.CategoryName
        };

        _mockValidator.Setup(v => v.ValidateAsync(inputDtoForValidating))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.UpdateAsync(id, inputDto, cancellationToken))
                .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateAsync(id, inputDto, cancellationToken));
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();
        var expected = true;

        _mockRepo.Setup(r => r.DeleteAsync(id, cancellationToken))
            .ReturnsAsync(expected);

        //Act
        var result = await _service.DeleteAsync(id, cancellationToken);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task DeleteAsync_NotFindExeption()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();
        var expected = false;

        _mockRepo.Setup(r => r.DeleteAsync(id, cancellationToken))
            .ReturnsAsync(expected);

        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.DeleteAsync(id, cancellationToken));
    }

    [Fact]
    public async Task DeleteAsync_RepositoryThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();

        _mockRepo.Setup(r => r.DeleteAsync(id, cancellationToken))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.DeleteAsync(id, cancellationToken));
    }
}
