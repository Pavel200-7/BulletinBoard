using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Images.Image.CreateDto;

/// <summary>
/// Дто для загрузки изображения из временного хранилища
/// </summary>
public class ImageCreateDto
{
    /// <summary>
    /// Идентификатор файла.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Контент
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Тип контента
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Размер файла
    /// </summary>
    public int Length { get; set; }
}
