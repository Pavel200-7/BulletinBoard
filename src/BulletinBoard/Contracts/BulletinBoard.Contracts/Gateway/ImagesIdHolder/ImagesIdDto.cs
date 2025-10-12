using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.DTO.Gateway.ImagesIdHolder;

/// <summary>
/// Структура данных, для хранения id изображения
/// </summary>
public class ImagesIdDto
{
    /// <summary>
    /// Id изображения
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Является ли изображение главным.
    /// </summary>
    public  bool IsMain { get; set; }

    /// <summary>
    /// Клиентский id, при помощи которого изображение можо удалить во время создания объявления.
    /// </summary>
    public string clientImageId { get; set; }
}
