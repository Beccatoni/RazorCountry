using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Data;
using RazorCountry.Models;

namespace RazorCountry.Pages.Continents;

public class Detail : PageModel
{
    public readonly CountryContext _context;

    public Detail(CountryContext context)
    {
        _context = context;
    }
    public Continent Continent { get; set; }
    public async Task<IActionResult> OnGetAsync(string id)
    {
        Continent = await _context.Continents
             .Include(c => c.Countries)
             .AsNoTracking()
             .FirstOrDefaultAsync(m=> m.ID == id);

        if (Continent == null)
        {
            return NotFound();
        }

        return Page();

    }
   
}