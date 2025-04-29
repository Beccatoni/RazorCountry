using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Data;
using RazorCountry.Models;

namespace RazorCountry.Pages.Countries;

public class Detail : PageModel
{
    private readonly CountryContext _context;

    public Detail(CountryContext context)
    {
        _context = context;
    }
    
    public CountryStat Country { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var country  = from c in _context.Countries where c.ID == id select new CountryStat{
            ID = c.ID, ContinentID = c.ContinentID, Name = c.Name, Population = c.Population, Area = c.Area, UnitedNationsDate = c.UnitedNationsDate, Density = c.Population / c.Area
        };
        Country = await country.SingleOrDefaultAsync();

        if (Country == null)
        {
            return NotFound();
        }
        return Page();
    }
  
}