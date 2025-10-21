using BulletinBoard.Domain.Entities.Bulletin;
using BulletinBoard.Infrastructure.DataAccess.Contexts.Bulletin;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.EntityFrameworkCore;
using System;


namespace BulletinBoard.Infrastructure.ComponentRegistrar.Registrar.Bulletin;

/// <summary>
/// Проводит миграциб БД
/// </summary>
public class DbInitializer : IAsyncInitializer
{
    private readonly BulletinContext _bulletinContext;
    private readonly Random _random = new();


    public DbInitializer(BulletinContext bulletinContext)
    {
        _bulletinContext = bulletinContext;
    }

    /// <summary>
    /// Проводит миграциб БД
    /// </summary>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await _bulletinContext.Database.MigrateAsync(cancellationToken);
        await FillDB();
    }

    /// <summary>
    /// Заполняет БД.
    /// </summary>
    public async Task FillDB()
    {
        //  Нужно ли заполнить БД
        bool YOU_WANT_FILL_DATABASE = false;

        // Проверяем, есть ли уже данные
        if (YOU_WANT_FILL_DATABASE != true && await _bulletinContext.BulletinCategory.AnyAsync())
            return;

        var categories = await CreateCategories();
        var characteristics = await CreateCharacteristics(categories);
        var characteristicValues = await CreateCharacteristicValues(characteristics);
        var users = await CreateUsers();
        var bulletins = await CreateBulletins(users, categories);
        await CreateBulletinCharacteristics(bulletins, characteristics, characteristicValues);
        await CreateBulletinImages(bulletins);
        await CreateBulletinViews(bulletins);
        await CreateRatings(bulletins, users);

        await _bulletinContext.SaveChangesAsync();
    }

    private async Task<List<BulletinCategory>> CreateCategories()
    {
        var parentCategories = new List<BulletinCategory>
    {
        new() { Id = Guid.NewGuid(), CategoryName = "Электроника", IsLeafy = false },
        new() { Id = Guid.NewGuid(), CategoryName = "Недвижимость", IsLeafy = false },
        new() { Id = Guid.NewGuid(), CategoryName = "Транспорт", IsLeafy = false },
        new() { Id = Guid.NewGuid(), CategoryName = "Работа", IsLeafy = false },
        new() { Id = Guid.NewGuid(), CategoryName = "Услуги", IsLeafy = false },
        new() { Id = Guid.NewGuid(), CategoryName = "Личные вещи", IsLeafy = false },
        new() { Id = Guid.NewGuid(), CategoryName = "Для дома и дачи", IsLeafy = false },
        new() { Id = Guid.NewGuid(), CategoryName = "Хобби и отдых", IsLeafy = false }
    };

        await _bulletinContext.BulletinCategory.AddRangeAsync(parentCategories);
        await _bulletinContext.SaveChangesAsync();

        var electronics = parentCategories[0];
        var realEstate = parentCategories[1];
        var transport = parentCategories[2];
        var work = parentCategories[3];
        var services = parentCategories[4];
        var personal = parentCategories[5];
        var home = parentCategories[6];
        var hobby = parentCategories[7];

        var childCategories = new List<BulletinCategory>()
        {

        new() { CategoryName = "Телефоны и аксессуары", IsLeafy = false, ParentCategoryId = electronics.Id },
        new() { CategoryName = "Смартфоны", IsLeafy = true, ParentCategoryId = electronics.Id },
        new() { CategoryName = "Наушники", IsLeafy = true, ParentCategoryId = electronics.Id },
        new() { CategoryName = "Ноутбуки", IsLeafy = true, ParentCategoryId = electronics.Id },
        new() { CategoryName = "Планшеты", IsLeafy = true, ParentCategoryId = electronics.Id },
        
        new() { CategoryName = "Квартиры", IsLeafy = true, ParentCategoryId = realEstate.Id },
        new() { CategoryName = "Дома", IsLeafy = true, ParentCategoryId = realEstate.Id },
        new() { CategoryName = "Комнаты", IsLeafy = true, ParentCategoryId = realEstate.Id },
        new() { CategoryName = "Офисы", IsLeafy = true, ParentCategoryId = realEstate.Id },
        
        };

        await _bulletinContext.BulletinCategory.AddRangeAsync(childCategories);
        await _bulletinContext.SaveChangesAsync();

        return parentCategories.Concat(childCategories).Where(c => c.IsLeafy).ToList();
    }

    private async Task<List<BulletinCharacteristic>> CreateCharacteristics(List<BulletinCategory> categories)
    {
        var characteristics = new List<BulletinCharacteristic>();

        foreach (var category in categories)
        {
            var categoryChars = GetCharacteristicsForCategory(category.CategoryName);
            characteristics.AddRange(categoryChars.Select(ch => new BulletinCharacteristic
            {
                Name = ch,
                CategoryId = category.Id
            }));
        }

        await _bulletinContext.BulletinCharacteristic.AddRangeAsync(characteristics);
        await _bulletinContext.SaveChangesAsync();
        return characteristics;
    }

    private async Task<List<BulletinCharacteristicValue>> CreateCharacteristicValues(List<BulletinCharacteristic> characteristics)
    {
        var values = new List<BulletinCharacteristicValue>();

        foreach (var characteristic in characteristics)
        {
            var possibleValues = GetPossibleValuesForCharacteristic(characteristic.Name);
            values.AddRange(possibleValues.Select(v => new BulletinCharacteristicValue
            {
                CharacteristicId = characteristic.Id,
                Value = v
            }));
        }

        await _bulletinContext.BulletinCharacteristicValue.AddRangeAsync(values);
        await _bulletinContext.SaveChangesAsync();
        return values;
    }

    private async Task<List<BulletinUser>> CreateUsers()
    {
        var users = new List<BulletinUser>();
        var cities = new[]
        {
            new { City = "Москва", Lat = 55.7558, Lon = 37.6173 },
            new { City = "Санкт-Петербург", Lat = 59.9343, Lon = 30.3351 },
            new { City = "Новосибирск", Lat = 55.0084, Lon = 82.9357 },
            new { City = "Екатеринбург", Lat = 56.8389, Lon = 60.6057 },
            new { City = "Казань", Lat = 55.7963, Lon = 49.1083 },
            new { City = "Нижний Новгород", Lat = 56.3269, Lon = 44.0059 },
            new { City = "Челябинск", Lat = 55.1644, Lon = 61.4368 },
            new { City = "Самара", Lat = 53.1959, Lon = 50.1002 },
            new { City = "Омск", Lat = 54.9885, Lon = 73.3242 },
            new { City = "Ростов-на-Дону", Lat = 47.2224, Lon = 39.7185 }
        };

        var firstNames = new[] { "Александр", "Алексей", "Андрей", "Артем", "Борис", "Вадим", "Василий", "Виктор", "Владимир", "Дмитрий", "Евгений", "Иван", "Игорь", "Кирилл", "Максим", "Михаил", "Николай", "Олег", "Павел", "Роман", "Сергей", "Юрий" };
        var lastNames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Лебедев", "Козлов", "Новиков", "Морозов", "Волков", "Соловьев", "Васильев", "Зайцев", "Павлов" };

        for (int i = 0; i < 5000; i++)
        {
            var city = cities[_random.Next(cities.Length)];
            users.Add(new BulletinUser
            {
                FullName = $"{lastNames[_random.Next(lastNames.Length)]} {firstNames[_random.Next(firstNames.Length)]}",
                Phone = $"+79{_random.Next(100000000, 999999999)}",
                Latitude = city.Lat + (_random.NextDouble() - 0.5) * 0.1,
                Longitude = city.Lon + (_random.NextDouble() - 0.5) * 0.1,
                FormattedAddress = $"г. {city.City}",
                Blocked = _random.Next(100) < 2 
            });
        }

        await _bulletinContext.BulletinUser.AddRangeAsync(users);
        await _bulletinContext.SaveChangesAsync();
        return users;
    }

    private async Task<List<BulletinMain>> CreateBulletins(List<BulletinUser> users, List<BulletinCategory> categories)
    {
        var bulletins = new List<BulletinMain>();
        var now = DateTime.UtcNow;

        for (int i = 0; i < 140000; i++)
        {
            var user = users[_random.Next(users.Count)];
            var category = categories[_random.Next(categories.Count)];
            var daysAgo = _random.Next(365);
            var createdAt = now.AddDays(-daysAgo);

            var bulletin = new BulletinMain
            {
                UserId = user.Id,
                CategoryId = category.Id,
                Title = GenerateTitle(category.CategoryName),
                Description = GenerateDescription(category.CategoryName),
                Price = GeneratePrice(category.CategoryName),
                CreatedAt = createdAt,
                Hidden = _random.Next(100) < 5, // 
                Closed = _random.Next(100) < 3, // 
                Blocked = _random.Next(100) < 1 // 
            };

            bulletins.Add(bulletin);

            if (bulletins.Count % 1000 == 0)
            {
                await _bulletinContext.BulletinMain.AddRangeAsync(bulletins);
                await _bulletinContext.SaveChangesAsync();
                bulletins.Clear();
                Console.WriteLine($"Создано {i + 1} объявлений...");
            }
        }

        if (bulletins.Any())
        {
            await _bulletinContext.BulletinMain.AddRangeAsync(bulletins);
            await _bulletinContext.SaveChangesAsync();
        }

        return await _bulletinContext.BulletinMain.ToListAsync();
    }

    private async Task CreateBulletinCharacteristics(List<BulletinMain> bulletins,
        List<BulletinCharacteristic> characteristics,
        List<BulletinCharacteristicValue> characteristicValues)
    {
        var comparisons = new List<BulletinCharacteristicComparison>();

        foreach (var bulletin in bulletins)
        {
            var categoryCharacteristics = characteristics
                .Where(c => c.CategoryId == bulletin.CategoryId)
                .ToList();

            var charsToAdd = categoryCharacteristics
                .OrderBy(x => _random.Next())
                .Take(_random.Next(1, 4))
                .ToList();

            foreach (var characteristic in charsToAdd)
            {
                var possibleValues = characteristicValues
                    .Where(cv => cv.CharacteristicId == characteristic.Id)
                    .ToList();

                if (possibleValues.Any())
                {
                    var value = possibleValues[_random.Next(possibleValues.Count)];

                    comparisons.Add(new BulletinCharacteristicComparison
                    {
                        BulletinId = bulletin.Id,
                        CharacteristicId = characteristic.Id,
                        CharacteristicValueId = value.Id
                    });
                }
            }

            if (comparisons.Count >= 1000)
            {
                await _bulletinContext.BulletinCharacteristicСomparison.AddRangeAsync(comparisons);
                await _bulletinContext.SaveChangesAsync();
                comparisons.Clear();
            }
        }

        if (comparisons.Any())
        {
            await _bulletinContext.BulletinCharacteristicСomparison.AddRangeAsync(comparisons);
            await _bulletinContext.SaveChangesAsync();
        }
    }

    private async Task CreateBulletinImages(List<BulletinMain> bulletins)
    {
        var images = new List<BulletinImage>();

        foreach (var bulletin in bulletins)
        {
            var imageCount = _random.Next(1, 6);

            for (int i = 0; i < imageCount; i++)
            {
                images.Add(new BulletinImage
                {
                    BulletinId = bulletin.Id,
                    IsMain = i == 0, 
                    CreatedAt = bulletin.CreatedAt.AddHours(i)
                });
            }

            if (images.Count >= 1000)
            {
                await _bulletinContext.BulletinImage.AddRangeAsync(images);
                await _bulletinContext.SaveChangesAsync();
                images.Clear();
            }
        }

        if (images.Any())
        {
            await _bulletinContext.BulletinImage.AddRangeAsync(images);
            await _bulletinContext.SaveChangesAsync();
        }
    }

    private async Task CreateBulletinViews(List<BulletinMain> bulletins)
    {
        var views = new List<BulletinViewsCount>();

        foreach (var bulletin in bulletins)
        {
            views.Add(new BulletinViewsCount
            {
                BulletinId = bulletin.Id,
                ViewsCount = _random.Next(0, 1000) 
            });

            if (views.Count >= 1000)
            {
                await _bulletinContext.BulletinViewsCount.AddRangeAsync(views);
                await _bulletinContext.SaveChangesAsync();
                views.Clear();
            }
        }

        if (views.Any())
        {
            await _bulletinContext.BulletinViewsCount.AddRangeAsync(views);
            await _bulletinContext.SaveChangesAsync();
        }
    }

    private async Task CreateRatings(List<BulletinMain> bulletins, List<BulletinUser> users)
    {
        var ratings = new List<BulletinRating>();

        foreach (var bulletin in bulletins)
        {
            var ratingCount = _random.Next(0, 3);

            for (int i = 0; i < ratingCount; i++)
            {
                var ratingUser = users[_random.Next(users.Count)];

                ratings.Add(new BulletinRating
                {
                    BulletinId = bulletin.Id,
                    UserId = ratingUser.Id,
                    Rating = _random.Next(1, 6), 
                    CreatedAt = bulletin.CreatedAt.AddDays(_random.Next(1, 30))
                });
            }

            if (ratings.Count >= 1000)
            {
                await _bulletinContext.BulletinRating.AddRangeAsync(ratings);
                await _bulletinContext.SaveChangesAsync();
                ratings.Clear();
            }
        }

        if (ratings.Any())
        {
            await _bulletinContext.BulletinRating.AddRangeAsync(ratings);
            await _bulletinContext.SaveChangesAsync();
        }
    }

    private string[] GetCharacteristicsForCategory(string categoryName)
    {
        return categoryName switch
        {
            "Смартфоны" => new[] { "Бренд", "Память", "Цвет", "Состояние", "Диагональ экрана" },
            "Наушники" => new[] { "Тип", "Бренд", "Цвет", "Состояние", "Беспроводные" },
            "Ноутбуки" => new[] { "Бренд", "Процессор", "Оперативная память", "Диагональ", "Видеокарта" },
            "Квартиры" => new[] { "Количество комнат", "Площадь", "Этаж", "Ремонт", "Мебель" },
            "Автомобили" => new[] { "Марка", "Год выпуска", "Пробег", "Тип кузова", "Двигатель" },
            "IT" => new[] { "Специализация", "Опыт работы", "Занятость", "Уровень английского" },
            "Одежда" => new[] { "Размер", "Бренд", "Состояние", "Цвет", "Материал" },
            "Мебель" => new[] { "Материал", "Состояние", "Стиль", "Цвет", "Размеры" },
            _ => new[] { "Состояние", "Бренд", "Цвет", "Размер" }
        };
    }

    private string[] GetPossibleValuesForCharacteristic(string characteristicName)
    {
        return characteristicName switch
        {
            "Бренд" => new[] { "Apple", "Samsung", "Xiaomi", "Huawei", "Sony", "LG" },
            "Память" => new[] { "64 ГБ", "128 ГБ", "256 ГБ", "512 ГБ", "1 ТБ" },
            "Цвет" => new[] { "Черный", "Белый", "Серый", "Синий", "Красный", "Золотой" },
            "Состояние" => new[] { "Новое", "Отличное", "Хорошее", "Удовлетворительное" },
            "Тип" => new[] { "Накладные", "Вкладыши", "Полноразмерные", "True Wireless" },
            "Количество комнат" => new[] { "1", "2", "3", "4", "5" },
            "Марка" => new[] { "Toyota", "Honda", "BMW", "Mercedes", "Audi", "Volkswagen" },
            "Специализация" => new[] { "Backend", "Frontend", "Mobile", "DevOps", "Data Science" },
            _ => new[] { "Да", "Нет", "Стандартный", "Премиум" }
        };
    }

    private string GenerateTitle(string categoryName)
    {
        var titles = new Dictionary<string, string[]>
        {
            ["Смартфоны"] = new[] { "Смартфон {0}", "Телефон {0} в отличном состоянии", "{0} - новое состояние" },
            ["Квартиры"] = new[] { "Сдам квартиру {0}", "Квартира в {0}", "Уютная квартира {0}" },
            ["Автомобили"] = new[] { "Продам автомобиль {0}", "{0} в хорошем состоянии", "Автомобиль {0}" },
            ["IT"] = new[] { "Требуется {0}", "Вакансия {0}", "Работа {0}" },
            ["Одежда"] = new[] { "{0} - новое", "Брендовая {0}", "Стильная {0}" }
        };

        var template = titles.ContainsKey(categoryName)
            ? titles[categoryName][_random.Next(titles[categoryName].Length)]
            : "{0}";

        return string.Format(template, categoryName);
    }

    private string GenerateDescription(string categoryName)
    {
        return $"Отличное предложение по категории {categoryName}. Качественный товар, хорошее состояние. Подробности при личном осмотре. Торг уместен.";
    }

    private decimal GeneratePrice(string categoryName)
    {
        return categoryName switch
        {
            "Смартфоны" => _random.Next(5000, 100000),
            "Квартиры" => _random.Next(15000, 200000),
            "Автомобили" => _random.Next(100000, 3000000),
            "IT" => _random.Next(30000, 300000),
            "Одежда" => _random.Next(500, 20000),
            _ => _random.Next(1000, 50000)
        };
    }


}