using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorCountry.Data;
using RazorCountry.Models;

namespace RazorCountry.Pages.Continents;

public class Edit : PageModel
{
    private readonly CountryContext _context;
    
    [BindProperty]
    public Continent Continent { get; set; }
    
    public Edit(CountryContext context)
    {
        _context = context;
    }
    public async Task<IActionResult>OnGetAsync(string id){
        if(id==null){
            Continent = new Continent();
        }else{
            Continent  = await _context.Continents.FindAsync(id);
            if(Continent == null){
                return NotFound();
            }
        }
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
            _context.Continents.Add(Continent);
        }
        else
        {
            _context.Attach(Continent).State = EntityState.Modified;
        }
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}