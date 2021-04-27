using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication3.Pages
{
    public class apiModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            WebApplication3.ThreadApi obj = new WebApplication3.ThreadApi();

            String city = Request.Form["city"];
            obj.weather_thread(city);
        }

    }


}
