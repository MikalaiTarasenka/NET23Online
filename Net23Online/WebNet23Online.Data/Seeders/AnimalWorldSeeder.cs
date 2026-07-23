using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebNet23Online.Data.Enums;
using WebNet23Online.Data.Models;
using WebNet23Online.Data.Models.AnimalWorld;

namespace WebNet23Online.Data.Seeders
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(WebContext context)
        {
            // Проверяем, есть ли уже данные (чтобы не дублировать)
            if (await context.Users.AnyAsync())
                return;

            Console.WriteLine("🌱 Запуск заполнения базы данных (Seeding)...");

            // 1. Создаем пользователя admin/admin с ролью Admin
            var admin = new UserData
            {
                Name = "admin",
                Password = "odmi", // В реальном проекте обязательно хешируйте пароли!
                Role = UserRole.Admin,
                Language = Language.Russian,
                FirstName = "Админ",
                LastName = "Главный",
                Mobilephone = "+1234567890"
            };
            context.Users.Add(admin);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Администратор создан: Id={admin.Id}");

            // 2. Создаем РОДА животных (в вашей модели это AnimalFamilies)
            var bears = new AnimalFamilyData { AnimalFamilyName = "Медведи", Description = "Род крупных хищных млекопитающих", UserId = admin.Id };
            var panthers = new AnimalFamilyData { AnimalFamilyName = "Пантеры", Description = "Род крупных животных семейства кошачьих (львы, тигры, леопарды)", UserId = admin.Id };
            var lynxes = new AnimalFamilyData { AnimalFamilyName = "Рыси", Description = "Род хищных млекопитающих средней величины из семейства кошачьих", UserId = admin.Id };
            var canis = new AnimalFamilyData { AnimalFamilyName = "Волки (Псовые)", Description = "Род хищных млекопитающих, включающий волков, шакалов и койотов", UserId = admin.Id };
            var eagles = new AnimalFamilyData { AnimalFamilyName = "Орлы", Description = "Род крупных хищных птиц семейства ястребиных", UserId = admin.Id };

            context.AnimalFamilies.AddRange(bears, panthers, lynxes, canis, eagles);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Рода животных успешно созданы");

            // 3. Создаем ВИДЫ животных (AnimalSpeciesData), строго привязанные к своим родам
            var species = new List<AnimalSpeciesData>
            {
                // Род: Медведи (bears)
                new() { AnimalSpeciesName = "Бурый медведь", AnimalSpeciesUrl = "https://example.com", NativeRange = "Евразия, Северная Америка", Description = "Один из самых крупных наземных хищников.", AnimalFamilyId = bears.Id, UserId = admin.Id },
                new() { AnimalSpeciesName = "Белый медведь", AnimalSpeciesUrl = "https://example.com", NativeRange = "Арктика", Description = "Крупнейший сухопутный представитель млекопитающих отряда хищных.", AnimalFamilyId = bears.Id, UserId = admin.Id },
                new() { AnimalSpeciesName = "Гималайский медведь", AnimalSpeciesUrl = "https://example.com", NativeRange = "Азия", Description = "Отличается белым пятном на груди в форме полумесяца.", AnimalFamilyId = bears.Id, UserId = admin.Id },
                
                // Род: Пантеры (panthers)
                new() { AnimalSpeciesName = "Леопард", AnimalSpeciesUrl = "https://example.com", NativeRange = "Африка, Азия", Description = "Крупная кошка с характерным пятнистым окрасом.", AnimalFamilyId = panthers.Id, UserId = admin.Id },
                new() { AnimalSpeciesName = "Тигр", AnimalSpeciesUrl = "https://example.com", NativeRange = "Азия", Description = "Один из крупнейших наземных хищников с полосатым окрасом.", AnimalFamilyId = panthers.Id, UserId = admin.Id },
                new() { AnimalSpeciesName = "Лев", AnimalSpeciesUrl = "https://example.com", NativeRange = "Африка, Индия", Description = "Социальные хищники, живущие в прайдах.", AnimalFamilyId = panthers.Id, UserId = admin.Id },
                
                // Род: Рыси (lynxes)
                new() { AnimalSpeciesName = "Обыкновенная рысь", AnimalSpeciesUrl = "https://example.com", NativeRange = "Евразия", Description = "Хищник с характерными кисточками на ушах.", AnimalFamilyId = lynxes.Id, UserId = admin.Id },
                new() { AnimalSpeciesName = "Канадская рысь", AnimalSpeciesUrl = "https://example.com", NativeRange = "Северная Америка", Description = "Плотный мех и широкие лапы для хождения по снегу.", AnimalFamilyId = lynxes.Id, UserId = admin.Id },
                
                // Род: Волки / Псовые (canis)
                new() { AnimalSpeciesName = "Койот", AnimalSpeciesUrl = "https://example.com", NativeRange = "Северная Америка", Description = "Хищник, известный своей адаптивностью и воем.", AnimalFamilyId = canis.Id, UserId = admin.Id },
                new() { AnimalSpeciesName = "Серый волк", AnimalSpeciesUrl = "https://example.com", NativeRange = "Евразия, Северная Америка", Description = "Прямой предок домашней собаки, стайный охотник.", AnimalFamilyId = canis.Id, UserId = admin.Id },
                
                // Род: Орлы (eagles)
                new() { AnimalSpeciesName = "Беркут", AnimalSpeciesUrl = "https://example.com", NativeRange = "Северное полушарие", Description = "Один из самых известных и крупных хищных птиц.", AnimalFamilyId = eagles.Id, UserId = admin.Id },
                new() { AnimalSpeciesName = "Могильник", AnimalSpeciesUrl = "https://example.com", NativeRange = "Евразия", Description = "Крупный орел, гнездящийся преимущественно в лесостепной зоне.", AnimalFamilyId = eagles.Id, UserId = admin.Id }
            };

            context.AnimalSpecies.AddRange(species);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Создано видов животных: {species.Count}");

            // 4. Создаем зоопарки (на русском языке)
            var zoos = new List<ZooData>
            {
                new() { ZooName = "Московский зоопарк", Address = "ул. Большая Грузинская, 1, Москва, Россия", Description = "Один из старейших и крупнейших зоопарков России.", UserId = admin.Id },
                new() { ZooName = "Лондонский зоопарк", Address = "Риджентс-парк, Лондон, Великобритания", Description = "Старейший научный зоопарк в мире.", UserId = admin.Id },
                new() { ZooName = "Зоопарк Сан-Диего", Address = "Уэй-Драйв, Сан-Диего, Калифорния, США", Description = "Знаменит своими огромными природными вольерами.", UserId = admin.Id },
                new() { ZooName = "Берлинский зоопарк", Address = "Харденбергплац 8, Берлин, Германия", Description = "Самый посещаемый зоопарк в Европе.", UserId = admin.Id },
                new() { ZooName = "Сингапурский зоопарк", Address = "Мандай Лейк Роуд, Сингапур", Description = "Известен своей концепцией открытого пространства без видимых барьеров.", UserId = admin.Id }
            };

            context.Zoos.AddRange(zoos);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Создано зоопарков: {zoos.Count}");

            // 5. Привязываем виды животных к зоопаркам случайным образом
            var random = new Random();
            foreach (var zoo in zoos)
            {
                zoo.AnimalSpecies ??= new List<AnimalSpeciesData>();

                // Каждый зоопарк получает от 4 до 7 случайных видов
                var speciesCount = random.Next(4, 8);
                var selectedSpecies = species.OrderBy(_ => random.Next()).Take(speciesCount).ToList();

                foreach (var sp in selectedSpecies)
                {
                    zoo.AnimalSpecies.Add(sp);
                }
            }
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Виды животных успешно распределены по зоопаркам");

            // 6. Создаем промоакции (на русском языке)
            var promotions = new List<PromotionData>
            {
                new()
                {
                    PromotionName = "Летняя распродажа",
                    Description = "Скидка 20% на все входные билеты.",
                    EndDate = DateTime.UtcNow.AddMonths(3),
                    VenueId = zoos[0].Id, // Московский зоопарк
                    CreatorId = admin.Id
                },
                new()
                {
                    PromotionName = "Семейные выходные",
                    Description = "При покупке двух взрослых билетов — детский в подарок.",
                    EndDate = DateTime.UtcNow.AddMonths(2),
                    VenueId = zoos[1].Id, // Лондонский зоопарк
                    CreatorId = admin.Id
                }
            };

            context.Promotions.AddRange(promotions);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Создано промоакций: {promotions.Count}");

            Console.WriteLine("🎉 База данных успешно заполнена тестовыми данными!");
        }
    }
}
