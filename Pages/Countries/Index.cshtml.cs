using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Data;
using RazorCountry.Models;

namespace RazorCountry.Pages.Countries;

public class Index : PageModel
{
    private readonly CountryContext _context;

    public Index(CountryContext context)
    {
        _context = context;
    }
    
    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }
    
    public List<Country> Countries { get; set; }
    
    public async Task OnGetAsync()
    {
        var countries = from c in _context.Countries select c;
        if (!string.IsNullOrEmpty(SearchString))
        {
            countries = countries.Where(c => c.Name.Contains(SearchString));
        }
        Countries = await _context.Countries.ToListAsync();
    }

    public async Task<IActionResult> OnPostAddCountryAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        Country country = await _context.Countries.FindAsync(id);
        if (country != null)
        {
            _context.Countries.Add(country);
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}