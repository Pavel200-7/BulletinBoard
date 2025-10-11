using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Images;

/// <summary>
/// Информация об изображении.
/// </summary>
public class ImageMetadata : EntityBase
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Тип контента.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Размер файла.
    /// </summary>
    public int Length { get; set; }
}