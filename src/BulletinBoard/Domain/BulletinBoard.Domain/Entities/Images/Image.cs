using BulletinBoard.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Domain.Entities.Images;

/// <summary>
/// Файл изображения.
/// </summary>
public class Image : EntityBase
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Контент.
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// Тип контента.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Размер файла.
    /// </summary>
    public int Length { get; set; }
}
