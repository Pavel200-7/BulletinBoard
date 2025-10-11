using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Bulletin;

public class DbInitializer : IAsyncInitializer
{
    private readonly BulletinContext _bulletinContext;


    public DbInitializer
        (
        BulletinContext bulletinContext
        )
    {
        _bulletinContext = bulletinContext;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await _bulletinContext.Database.MigrateAsync(cancellationToken);
        //await FillDatabase(_bulletinContext); // Для ручных тестов
    }


    static async Task FillDatabase(BulletinContext context)
    {
        var random = new Random();

        // 1. Создаем пользователей
        var users = new List<BulletinUser>();
        for (int i = 0; i < 50; i++)
        {
            users.Add(new BulletinUser
            {
                Id = Guid.NewGuid(),
                FullName = GetRandomFullName(random),
                Phone = $"+7{random.Next(900000000, 999999999)}",
                Latitude = 55.75 + random.NextDouble() * 0.1,
                Longitude = 37.61 + random.NextDouble() * 0.1,
                FormattedAddress = GetRandomAddress(random),
                Blocked = random.Next(10) == 0 // 10% заблокированы
            });
        }
        await context.BulletinUser.AddRangeAsync(users);
        await context.SaveChangesAsync();

        // 2. Создаем категории (иерархическая структура)
        var categories = new List<BulletinCategory>();

        // Родительские категории
        var parentCategories = new[]
        {
            "Электроника", "Недвижимость", "Транспорт", "Работа", "Услуги"
        };

        foreach (var parentName in parentCategories)
        {
            var parentCategory = new BulletinCategory
            {
                Id = Guid.NewGuid(),
                CategoryName = parentName,
                IsLeafy = false
            };
            categories.Add(parentCategory);

            // Дочерние категории
            var childNames = GetChildCategories(parentName);
            foreach (var childName in childNames)
            {
                categories.Add(new BulletinCategory
                {
                    Id = Guid.NewGuid(),
                    ParentCategoryId = parentCategory.Id,
                    CategoryName = childName,
                    IsLeafy = true
                });
            }
        }
        await context.BulletinCategory.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // 3. Создаем характеристики для категорий
        var characteristics = new List<BulletinCharacteristic>();
        var characteristicValues = new List<BulletinCharacteristicValue>();

        var leafyCategories = categories.Where(c => c.IsLeafy).ToList();
        foreach (var category in leafyCategories)
        {
            var categoryCharacteristics = GetCategoryCharacteristics(category.CategoryName);
            foreach (var charName in categoryCharacteristics)
            {
                var characteristic = new BulletinCharacteristic
                {
                    Id = Guid.NewGuid(),
                    Name = charName,
                    CategoryId = category.Id
                };
                characteristics.Add(characteristic);

                // Добавляем значения характеристик
                var values = GetCharacteristicValues(charName);
                foreach (var value in values)
                {
                    characteristicValues.Add(new BulletinCharacteristicValue
                    {
                        Id = Guid.NewGuid(),
                        CharacteristicId = characteristic.Id,
                        Value = value
                    });
                }
            }
        }
        await context.BulletinCharacteristic.AddRangeAsync(characteristics);
        await context.SaveChangesAsync();
        await context.BulletinCharacteristicValue.AddRangeAsync(characteristicValues);
        await context.SaveChangesAsync();

        // 4. Создаем объявления
        var bulletins = new List<BulletinMain>();
        var bulletinCharacteristics = new List<BulletinCharacteristicComparison>();
        var bulletinImages = new List<BulletinImage>();
        var viewsCounts = new List<BulletinViewsCount>();

        for (int i = 0; i < 200; i++)
        {
            var user = users[random.Next(users.Count)];
            var category = leafyCategories[random.Next(leafyCategories.Count)];
            var createdAt = DateTime.Now.AddDays(-random.Next(365));

            var bulletin = new BulletinMain
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CategoryId = category.Id,
                Title = GetRandomTitle(category.CategoryName, random),
                Description = GetRandomDescription(category.CategoryName, random),
                Price = random.Next(100, 100000),
                Hidden = random.Next(20) == 0, // 5% скрыты
                Closed = random.Next(15) == 0, // ~7% завершены
                Blocked = random.Next(25) == 0 // 4% заблокированы
            };
            bulletins.Add(bulletin);

            // Добавляем просмотры
            viewsCounts.Add(new BulletinViewsCount
            {
                Id = Guid.NewGuid(),
                BulletinId = bulletin.Id,
                ViewsCount = random.Next(0, 5000)
            });

            // Добавляем изображения
            var imageCount = random.Next(1, 6);
            for (int j = 0; j < imageCount; j++)
            {
                bulletinImages.Add(new BulletinImage
                {
                    Id = Guid.NewGuid(),
                    BulletinId = bulletin.Id,
                    IsMain = j == 0,
                });
            }

            // Добавляем характеристики
            var categoryChars = characteristics.Where(c => c.CategoryId == category.Id).ToList();
            foreach (var characteristic in categoryChars.Take(random.Next(1, categoryChars.Count + 1)))
            {
                var charValues = characteristicValues.Where(v => v.CharacteristicId == characteristic.Id).ToList();
                if (charValues.Any())
                {
                    var selectedValue = charValues[random.Next(charValues.Count)];
                    bulletinCharacteristics.Add(new BulletinCharacteristicComparison
                    {
                        Id = Guid.NewGuid(),
                        BulletinId = bulletin.Id,
                        CharacteristicId = characteristic.Id,
                        CharacteristicValueId = selectedValue.Id
                    });
                }
            }
        }

        await context.BulletinMain.AddRangeAsync(bulletins);
        await context.SaveChangesAsync();
        await context.BulletinViewsCount.AddRangeAsync(viewsCounts);
        await context.SaveChangesAsync();
        await context.BulletinImage.AddRangeAsync(bulletinImages);
        await context.SaveChangesAsync();
        await context.BulletinCharacteristicСomparison.AddRangeAsync(bulletinCharacteristics);
        await context.SaveChangesAsync();

        // 5. Создаем рейтинги
        var ratings = new List<BulletinRating>();
        foreach (var bulletin in bulletins.Where(b => !b.Blocked && !b.Closed))
        {
            var ratingCount = random.Next(0, 15);
            for (int i = 0; i < ratingCount; i++)
            {
                var user = users[random.Next(users.Count)];
                ratings.Add(new BulletinRating
                {
                    Id = Guid.NewGuid(),
                    BulletinId = bulletin.Id,
                    UserId = user.Id,
                    Rating = random.Next(1, 6),
                });
            }
        }
        await context.BulletinRating.AddRangeAsync(ratings);
        await context.SaveChangesAsync();
    }

    // Вспомогательные методы для генерации данных
    static string GetRandomFullName(Random random)
    {
        var firstNames = new[] { "Александр", "Мария", "Дмитрий", "Елена", "Сергей", "Ольга", "Андрей", "Наталья", "Алексей", "Ирина" };
        var lastNames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Васильев", "Соколов", "Михайлов", "Новиков" };
        return $"{lastNames[random.Next(lastNames.Length)]} {firstNames[random.Next(firstNames.Length)]}";
    }

    static string GetRandomAddress(Random random)
    {
        var streets = new[] { "Ленина", "Пушкина", "Гагарина", "Советская", "Мира", "Кирова", "Садовоя", "Центральная", "Молодежная", "Школьная" };
        return $"ул. {streets[random.Next(streets.Length)]}, д. {random.Next(1, 100)}";
    }

    static string[] GetChildCategories(string parentCategory)
    {
        return parentCategory switch
        {
            "Электроника" => new[] { "Смартфоны", "Ноутбуки", "Телевизоры", "Наушники", "Планшеты" },
            "Недвижимость" => new[] { "Квартиры", "Дома", "Комнаты", "Офисы", "Гаражи" },
            "Транспорт" => new[] { "Автомобили", "Мотоциклы", "Велосипеды", "Самокаты", "Запчасти" },
            "Работа" => new[] { "IT", "Строительство", "Образование", "Медицина", "Торговля" },
            "Услуги" => new[] { "Ремонт", "Красота", "Образование", "Перевозки", "Уборка" },
            _ => new[] { "Другое" }
        };
    }

    static string[] GetCategoryCharacteristics(string categoryName)
    {
        return categoryName switch
        {
            "Смартфоны" => new[] { "Бренд", "Память", "Цвет", "Состояние" },
            "Ноутбуки" => new[] { "Бренд", "Процессор", "Оперативная память", "Диагональ" },
            "Квартиры" => new[] { "Количество комнат", "Этаж", "Ремонт", "Площадь" },
            "Автомобили" => new[] { "Марка", "Год выпуска", "Пробег", "Тип топлива" },
            _ => new[] { "Состояние", "Тип" }
        };
    }

    static string[] GetCharacteristicValues(string characteristicName)
    {
        return characteristicName switch
        {
            "Бренд" => new[] { "Samsung", "Apple", "Xiaomi", "Huawei", "Nokia" },
            "Память" => new[] { "64 ГБ", "128 ГБ", "256 ГБ", "512 ГБ" },
            "Цвет" => new[] { "Черный", "Белый", "Синий", "Красный", "Серебристый" },
            "Состояние" => new[] { "Новое", "Б/у", "На запчасти" },
            "Количество комнат" => new[] { "1", "2", "3", "4", "5" },
            "Марка" => new[] { "Toyota", "BMW", "Mercedes", "Audi", "Volkswagen" },
            _ => new[] { "Отличное", "Хорошее", "Удовлетворительное" }
        };
    }

    static string GetRandomTitle(string category, Random random)
    {
        var adjectives = new[] { "Отличный", "Новый", "Б/у", "Качественный", "Срочно", "Дешевый" };
        return $"{adjectives[random.Next(adjectives.Length)]} {category}";
    }

    static string GetRandomDescription(string category, Random random)
    {
        var descriptions = new[]
        {
            "В отличном состоянии, почти не использовался.",
            "Срочная продажа в связи с переездом.",
            "Качественный товар по доступной цене.",
            "Полный комплект, все документы в наличии.",
            "Идеальное состояние, гарантия качества."
        };
        return $"Продается {category}. {descriptions[random.Next(descriptions.Length)]}";
    }
}