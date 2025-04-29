using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Data;
using RazorCountry.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorCountry.Pages.Countries;

public class Edit : PageModel
{
    private readonly CountryContext _context;

    public Edit(CountryContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Country Country { get; set; }
    
    public SelectList Continents { get; set; }
    
    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            Country = new Country();
        }
        else
        {
            Country = await _context.Countries.FindAsync(id);
            if (Country == null)
            {
                return NotFound();
            }
            
            
        }
        
        Continents = new SelectList(_context.Continents, nameof(Continent.ID), nameof(Continent.Name));

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (id == null)
        {
            _context.Countries.Add(Country);
        }
        else
        {
            _context.Attach(Country).State = EntityState.Modified;
        }
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}