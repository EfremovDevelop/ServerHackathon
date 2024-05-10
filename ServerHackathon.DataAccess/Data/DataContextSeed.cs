using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Data;

public static class DataContextSeed
{
    public static async Task SeedAsync(DataContext context)
    {
        try
        {
            context.Database.EnsureCreated();

            if (!context.University.Any())
            {
                var universities = new University[]
                {
                    new University{
                        Name = "Ивановский Государственный Энергетический университет им В.И. Ленина",
                        Initials = "ИГЭУ",
                        Description = "Лучший вуз"
                    },
                    new University{
                        Name = "Ивановский Государственный химико-технологический университет",
                        Initials = "ИГХТУ"
                    },
                    new University{
                        Name = "Ивановский Государственный университет",
                        Initials = "ИВГУ"
                    }
                };
                foreach (University u in universities)
                {
                    context.University.Add(u);
                }
                await context.SaveChangesAsync();
            }
            if (!context.Place.Any())
            {
                var places = new Place[]
                {
                    new Place
                    {
                        Name = "Сектор C",
                        Adress = "ИГЭУ, А корпус, 2 этаж, Сектор С",
                        Location = "153003, Иваново, ул. Рабфаковская, д. 34",
                        UniversityId = 1,
                        Capacity = 15,
                        Description = "Комфортное место для небольших мероприятий и коворкингов"
                    },
                    new Place
                    {
                        Name = "Аудитория Б303",
                        Adress = "ИГЭУ, Б корпус, 3 этаж, ауд. Б303",
                        Location = "153003, Иваново, ул. Рабфаковская, д. 34",
                        UniversityId = 1,
                        Capacity = 20,
                        Description = "Аудитория с компьютерами"
                    }
                };
                foreach (Place place in places)
                {
                    context.Place.Add(place);
                }
                await context.SaveChangesAsync();

                var placeTypeList = new PlaceTypeList[]
                {
                    new PlaceTypeList
                    {
                        PlaceId = 1,
                        PlaceTypeId = 1
                    },
                    new PlaceTypeList
                    {
                        PlaceId = 1,
                        PlaceTypeId = 2
                    },
                    new PlaceTypeList
                    {
                        PlaceId = 2,
                        PlaceTypeId = 1
                    }
                };
                foreach (PlaceTypeList place in placeTypeList)
                    context.PlaceTypeList.Add(place);
                await context.SaveChangesAsync();
            }
        }
        catch
        {
            throw;
        }
    }
}
