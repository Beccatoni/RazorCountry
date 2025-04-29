using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Models;
using RazorCountry.Data;

namespace RazorCountry.Pages;


public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    private readonly CountryContext _context;

    public IndexModel(ILogger<IndexModel> logger, CountryContext context)
    {
        _logger = logger;
        _context = context;
    }

    public List<Continent> Continents { get; set; }
    public List<Country> Countries { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Continents = await _context.Continents.ToListAsync();
        Countries = await _context.Countries.ToListAsync();
      return Page();
    }
}
