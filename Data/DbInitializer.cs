using RazorCountry.Models;

namespace RazorCountry.Data;

public  static class DbInitializer
{
    public static void Initialize(CountryContext context)
    {
        context.Database.EnsureCreated();
        
        //Look for any continents.
        if (context.Continents.Any())
        {
            return; //DB has been seeded
        }

        var Continents = new Continent[]
        {
            new Continent{ID = "NA", Name = "North America"},
            new Continent{ID="SA",Name="South America"},
            new Continent{ID="EU",Name="Europe"},
            new Continent{ID="AS",Name="Asia"},
            new Continent{ID="AF",Name="Africa"},
            new Continent{ID="AN",Name="Antarctica"},
        };

        foreach (Continent c in Continents)
        {
            context.Continents.Add(c);
        }

        context.SaveChanges();

        var Countries = new Country[]
        {
            new Country
            {
                ID = "RW", Name = "Rwanda", ContinentID = "N/A", Population = 13950000,
                UnitedNationsDate = new DateTime(1945, 10, 24)
            },
            new Country{ID="CHN",Name="China",ContinentID="AS",Population=1400737880,UnitedNationsDate=new DateTime(1945,10,24)},
            new Country{ID="SDN",Name="Sudan",ContinentID="AF",Population=42158625,UnitedNationsDate=new DateTime(1956,12,11)},
            new Country{ID="USA",Name="United States",ContinentID="NA",Population=330529481,UnitedNationsDate=new DateTime(1945,10,24)},
            new Country{ID="BRA",Name="Brazil",ContinentID="SA",Population=210938214,UnitedNationsDate=new DateTime(1945,10,24)},
            new Country{ID="FRA",Name="France",ContinentID="EU",Population=67081000,UnitedNationsDate=new DateTime(1945,10,24)},
        };

        foreach (var country in Countries)
        {
            context.Countries.Add(country);
        }

        context.SaveChanges();
    }
}