using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Images.Image.ReadDto;


/// <summary>
/// Дто для чтения данных изображения
/// </summary>
public class ImageInfoReadDto
{
    /// <summary>
    /// Идентификатор файла.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя файла.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Размер файла.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Тип контента
    /// </summary>
    public string ContentType { get; set; }
}
