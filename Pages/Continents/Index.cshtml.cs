using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Data;
using RazorCountry.Models;

namespace RazorCountry.Pages.Continents;

public class Index : PageModel
{
    private readonly CountryContext _context;

    public Index(CountryContext context)
    {
        _context = context;
    }
    
    public List<Continent> Continents { get; set; }
    
    public async Task OnGetAsync()
    {
        Continents = await _context.Continents.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return RedirectToPage("./Index");
    }
}