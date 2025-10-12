using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.DTO.Gateway.ImagesIdHolder;

/// <summary>
/// Структура данных, используемая на уровне APIGateway
/// для временного сохранения списака идентификаторов
/// для реализации use-case создания объявления,
/// а конкретно для передачи в домен объявлений id
/// изображений, загруженных пользователем в процессе 
/// создания объявления.
/// </summary>
public class ImagesIdHolderDto
{
    /// <summary>
    /// Список id.
    /// </summary>
    public List<ImagesIdDto> imagesIds { get; set; } = new List<ImagesIdDto>();
}
