using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Pages.weathers
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication3.Data.WebApplication3Context _context;

        public CreateModel(WebApplication3.Data.WebApplication3Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public weather weather { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.weather.Add(weather);
            await _context.SaveChangesAsync();

            return RedirectToRoute("weathers");
        }
    }
}
