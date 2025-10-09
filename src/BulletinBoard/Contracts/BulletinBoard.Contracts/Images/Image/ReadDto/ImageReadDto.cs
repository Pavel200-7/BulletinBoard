using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Images.Image.ReadDto;

/// <summary>
/// Дто для чтения изображения и передачи его на клиент.
/// </summary>
public class ImageReadDto
{
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
}


