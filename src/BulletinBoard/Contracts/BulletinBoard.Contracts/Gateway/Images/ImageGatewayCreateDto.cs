using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.Gateway.Images;

/// <summary>
///  Dto добавления изображения объявления
/// </summary>
public class ImageGatewayCreateDto
{
    /// <summary>
    /// Id объявления.
    /// </summary>
    public Guid BulletinId { set; get; }

    /// <summary>
    /// Является ли изображение титульным
    /// </summary>
    public bool IsMain {  set; get; }
}
