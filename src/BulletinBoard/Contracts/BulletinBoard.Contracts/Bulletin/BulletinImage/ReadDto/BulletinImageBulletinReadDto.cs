using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Bulletin.BulletinImage.ReadDto;

/// <summary>
/// Формат двнных изображения, используемый при чтении информации объявления.
/// </summary>
public class BulletinImageBulletinReadDto
{
    /// <summary>
    /// Id изображения.
    /// Является копией id изображения из другого домена,
    /// который предназначен для храния изображений в BLOB полях БД.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Является ли изображение титульным 
    /// </summary>
    public bool IsMain { get; set; }
}
