using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Contracts.User.ApplicationUserDto.CreateDto;

/// <summary>
/// Дто с результатом попытки создания пользователя.
/// </summary>
public class ApplicationUserCreateResponseDto
{
    /// <summary>
    /// Id пользователя.
    /// </summary>
    public string UserId {  get; set; }

    /// <summary>
    /// Прошло ли создание успешно.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Список ошибок.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; set; }

    /// <summary>
    /// Конструктор, неожиданно, да?
    /// </summary>
    public ApplicationUserCreateResponseDto(string userId, bool succeeded, IDictionary<string, string[]> errors)
    {
        UserId = userId;
        Succeeded = succeeded;
        Errors = errors;
    }
}
