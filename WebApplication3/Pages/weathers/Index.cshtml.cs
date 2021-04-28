using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Pages.weathers
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication3.Data.WebApplication3Context _context;

        public IndexModel(WebApplication3.Data.WebApplication3Context context)
        {
            _context = context;
        }

        public IList<weather> weather { get;set; }

        public async Task OnGetAsync(string sortOrder = "date_desc", string searchString = "")
        {

            ViewData["NameSortParm"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["DateSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["TempSortParm"] = sortOrder == "temp_asc" ? "temp_desc" : "temp_asc";
            ViewData["searchString"] = searchString;

            var weathers = from s in _context.weather
                           select s;

            if (searchString != "")
            {
                weathers = weathers.Where(w => w.name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_asc":
                    weathers = weathers.OrderBy(w => w.name);
                    break;
                case "date_asc":
                    weathers = weathers.OrderBy(w => w.datatime);
                    break;
                case "temp_asc":
                    weathers = weathers.OrderBy(w => w.temp);
                    break;
                case "temp_desc":
                    weathers = weathers.OrderByDescending(w => w.temp);
                    break;
                case "date_desc":
                    weathers = weathers.OrderByDescending(w => w.datatime);
                    break;
                case "name_desc":
                    weathers = weathers.OrderByDescending(w => w.name);
                    break;
            }

            weather = await weathers.ToListAsync();
        }
    }
}
