using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Data;

public static class DataContextSeed
{
    public static async Task SeedAsync(DataContext context)
    {
        try
        {
            context.Database.EnsureCreated();

            if (context.University.Any())
            {
                return;
            }

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
        catch
        {
            throw;
        }
    }
}
