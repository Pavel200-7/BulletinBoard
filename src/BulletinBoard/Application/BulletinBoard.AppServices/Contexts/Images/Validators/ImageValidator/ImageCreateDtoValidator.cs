using BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator.IValidators;
using BulletinBoard.Contracts.Images.Image.CreateDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.AppServices.Contexts.Images.Validators.ImageValidator;

/// <inheritdoc/>
public class ImageCreateDtoValidator : AbstractValidator<ImageCreateDto>, IImageCreateDtoValidator
{
    /// <inheritdoc/>
    public ImageCreateDtoValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .WithMessage("The file name cannot be empty.")
            .MaximumLength(255)
            .WithMessage("The file name must not exceed 255 characters in length.")
            .Must(BeAValidFileName)
            .WithMessage("Invalid file name.");

        RuleFor(i => i.Content)
            .NotEmpty()
            .WithMessage("The contents of the file cannot be empty.")
            .Must(BeAReasonableSize)
            .WithMessage("The file size exceeds the allowed limit (5MB).")
            .Must(BeValidImageData)
            .WithMessage("The file is not a valid image.");

        RuleFor(i => i.ContentType)
            .NotEmpty()
            .WithMessage("The content type cannot be empty.")
            .Must(BeAValidImageContentType)
            .WithMessage("Unsupported image type. Allowed: JPEG, PNG, GIF, BMP, WEBP.");

        RuleFor(i => i.Length)
            .GreaterThan(0)
            .WithMessage("Image can not be empty")
            .LessThanOrEqualTo(5 * 1024 * 1024) // 5MB
            .WithMessage("The image can not be bigger then 5 MB.")
            .Must((dto, length) => length == dto.Content?.Length)
            .WithMessage("The stated size differs from the actual one.");
    }

    /// <summary>
    /// Проверяет, является ли имя файла допустимым.
    /// </summary>
    private bool BeAValidFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        // Проверка на запрещенные символы в именах файлов
        var invalidChars = Path.GetInvalidFileNameChars();
        if (fileName.Any(c => invalidChars.Contains(c)))
            return false;

        // Проверка на резервные имена в Windows
        var reservedNames = new[]
        {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        if (reservedNames.Contains(fileNameWithoutExtension?.ToUpperInvariant()))
            return false;

        return true;
    }

    /// <summary>
    /// Проверяет, является ли размер файла разумным.
    /// </summary>
    private bool BeAReasonableSize(byte[] content)
    {
        if (content == null)
            return false;

        int fiveMegabytes = 5 * 1024 * 1024;
        return content.Length <= fiveMegabytes; 
    }

    /// <summary>
    /// Проверяет, является ли файл валидным изображением по сигнатурам (магическим числам).
    /// </summary>
    private bool BeValidImageData(byte[] content)
    {
        if (content == null || content.Length < 8)
            return false;

        return IsJpeg(content) || IsPng(content) || IsGif(content) || IsBmp(content) || IsWebP(content);
    }

    /// <summary>
    /// Проверяет JPEG сигнатуру.
    /// </summary>
    private bool IsJpeg(byte[] bytes)
    {
        return bytes.Length > 2 && bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF;
    }

    /// <summary>
    /// Проверяет PNG сигнатуру.
    /// </summary>
    private bool IsPng(byte[] bytes)
    {
        return bytes.Length > 8 &&
               bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47 &&
               bytes[4] == 0x0D && bytes[5] == 0x0A && bytes[6] == 0x1A && bytes[7] == 0x0A;
    }

    /// <summary>
    /// Проверяет GIF сигнатуру.
    /// </summary>
    private bool IsGif(byte[] bytes)
    {
        return bytes.Length > 6 &&
               bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46 &&
               bytes[3] == 0x38 && (bytes[4] == 0x37 || bytes[4] == 0x39) && bytes[5] == 0x61;
    }

    /// <summary>
    /// Проверяет BMP сигнатуру.
    /// </summary>
    private bool IsBmp(byte[] bytes)
    {
        return bytes.Length > 2 && bytes[0] == 0x42 && bytes[1] == 0x4D;
    }

    /// <summary>
    /// Проверяет WEBP сигнатуру.
    /// </summary>
    private bool IsWebP(byte[] bytes)
    {
        return bytes.Length > 12 &&
               bytes[0] == 0x52 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x46 &&
               bytes[8] == 0x57 && bytes[9] == 0x45 && bytes[10] == 0x42 && bytes[11] == 0x50;
    }

    /// <summary>
    /// Проверяет, является ли тип контента допустимым для изображений.
    /// </summary>
    private bool BeAValidImageContentType(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            return false;

        var validContentTypes = new[]
        {
            "image/jpeg",
            "image/jpg",
            "image/png",
            "image/gif",
            "image/bmp",
            "image/webp",
            "image/x-png",
            "image/svg+xml"
        };

        return validContentTypes.Contains(contentType.ToLowerInvariant());
    }
}