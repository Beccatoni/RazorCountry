using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Data;
using RazorCountry.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    [BindProperty(SupportsGet = true)] 
    public string SortField { get; set; } = "Name";
    
    public SelectList Continents { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string SelectedContinent { get; set; }
    
    public async Task OnGetAsync()
    {
        var countries = from c in _context.Countries select c;
        if (!string.IsNullOrEmpty(SearchString))
        {
            countries = countries.Where(c => c.Name.Contains(SearchString));
        }

        switch (SortField)
        {
            case "ID":
                countries = countries.OrderBy(c => c.ID);
                break;
            case "Name":
                countries = countries.OrderBy(c => c.Name);
                break;
            case "ContinentID": 
                countries = countries.OrderBy(c => c.ContinentID);
                break;
            
        }

        if (!string.IsNullOrEmpty(SelectedContinent))
        {
            countries = countries.Where(c => c.ContinentID == SelectedContinent);
        }
        IQueryable<string> continentQuery = from c in _context.Continents orderby c.ID select c.ID;
        Continents = new SelectList(await continentQuery.ToListAsync());
        Countries = await countries.ToListAsync();
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
            _context.Countries.Remove(country);
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}