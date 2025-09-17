using BulletinBoard.AppServices.Contexts.Bulletin.Builder.IBuilders;
using BulletinBoard.AppServices.Contexts.Bulletin.Mapping.MappingServices.IMappingServices;
using BulletinBoard.AppServices.Contexts.Bulletin.Repository;
using BulletinBoard.AppServices.Contexts.Bulletin.Services;
using BulletinBoard.AppServices.Contexts.Bulletin.Validators.BulletinCategoryValidator.IValidators;
using BulletinBoard.Contracts.Bulletin.BulletinCategory;
using BulletinBoard.Contracts.Errors.Exeptions;
using FluentValidation.Results;
using Moq;


namespace BulletinBoard.Tests.Application.BulletinBoard.AppServices.Contexts.Bulletin.Services;

public class BulletinCategoryServiceTests
{
    private readonly Mock<IBulletinCategoryRepository> _mockRepo;
    private readonly Mock<IBulletinCategoryDtoValidatorFacade> _mockValidator;
    private readonly Mock<IBulletinCategorySpecificationBuilder> _mockSpecBuilder;
    private readonly Mock<IBulletinCategoryMappingService> _mockMapper;

    private readonly BulletinCategoryService _service;

    public BulletinCategoryServiceTests()
    {
        _mockRepo = new Mock<IBulletinCategoryRepository>();
        _mockValidator = new Mock<IBulletinCategoryDtoValidatorFacade>();
        _mockSpecBuilder = new Mock<IBulletinCategorySpecificationBuilder>();
        _mockMapper = new Mock<IBulletinCategoryMappingService>();


        _service = new BulletinCategoryService(
            _mockRepo.Object,
            _mockValidator.Object,
            _mockSpecBuilder.Object,
            _mockMapper.Object);
    }

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsCreatedCategory()
    {
        // Arrange
        var inputDto = new BulletinCategoryCreateDto { ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };
        var expectedOutput = new BulletinCategoryDto { Id = Guid.NewGuid(), ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.CreateAsync(inputDto))
                .ReturnsAsync(expectedOutput);

        // Act
        var result = await _service.CreateAsync(inputDto);

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
        var inputDto = new BulletinCategoryCreateDto { ParentCategoryId = null, CategoryName = "", IsLeafy = true };
        var validationErrors = new ValidationResult(new[]
        {
            new ValidationFailure("CategoryName", "Name is required")
        });

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(validationErrors);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationExeption>(() =>
            _service.CreateAsync(inputDto));
    }

    [Fact]
    public async Task CreateAsync_RepositoryThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var inputDto = new BulletinCategoryCreateDto { ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.CreateAsync(inputDto))
                .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.CreateAsync(inputDto));
    }

    [Fact]
    public async Task UpdateAsync_ValidDto_ReturnsUpdatedCategory()
    {
        // Arrange
        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = "Test Category"};
        var expectedOutput = new BulletinCategoryDto { Id = id, ParentCategoryId = null, CategoryName = "Test Category", IsLeafy = true };

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.UpdateAsync(id, inputDto))
                .ReturnsAsync(expectedOutput);

        // Act
        var result = await _service.UpdateAsync(id, inputDto);

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
        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = ""};
        var validationErrors = new ValidationResult(new[]
        {
            new ValidationFailure("CategoryName", "Name is required")
        });

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(validationErrors);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationExeption>(() =>
            _service.UpdateAsync(id, inputDto));
    }

    [Fact]
    public async Task UpdateAsync_NotFindExeption()
    {
        //Arrange
        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = "Test Category" };
        BulletinCategoryDto? expected = null;

        _mockRepo.Setup(r => r.UpdateAsync(id, inputDto))
            .ReturnsAsync(expected);

        //Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateAsync(id, inputDto));

        //await Assert.ThrowsAsync<Exception>(() =>
        //    _service.UpdateAsync(id, inputDto));
        //await Assert.ThrowsAsync<NotFoundException>(() =>
        //    _service.UpdateAsync(id, inputDto));
    }


    [Fact]
    public async Task UpdateAsync_RepositoryThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var id = Guid.NewGuid();
        var inputDto = new BulletinCategoryUpdateDto { ParentCategoryId = null, CategoryName = "Test Category"};

        _mockValidator.Setup(v => v.ValidateAsync(inputDto))
                     .ReturnsAsync(new ValidationResult());

        _mockRepo.Setup(r => r.UpdateAsync(id, inputDto))
                .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.UpdateAsync(id, inputDto));
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        //Arrange
        var id = Guid.NewGuid();
        var expected = true;

        _mockRepo.Setup(r => r.DeleteAsync(id))
            .ReturnsAsync(expected);

        //Act
        var result = await _service.DeleteAsync(id);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task DeleteAsync_NotFindExeption()
    {
        //Arrange
        var id = Guid.NewGuid();
        var expected = false;

        _mockRepo.Setup(r => r.DeleteAsync(id))
            .ReturnsAsync(expected);

        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.DeleteAsync(id));
    }

    [Fact]
    public async Task DeleteAsync_RepositoryThrowsException_ExceptionIsPropagated()
    {
        // Arrange
        var id = Guid.NewGuid();
        //var expected = true; ;

        _mockRepo.Setup(r => r.DeleteAsync(id))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _service.DeleteAsync(id));
    }
}
