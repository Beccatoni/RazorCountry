using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Models;
using RazorCountry.Data;

namespace RazorCountry.Pages.Continents
{
    public class DeleteModel : PageModel
    {
        private readonly CountryContext _context;

        public DeleteModel(CountryContext context)
        {
            _context = context;
        }

        public Continent Continent { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Continent = await _context.Continents
                .Include(c => c.Countries)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Continent == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}