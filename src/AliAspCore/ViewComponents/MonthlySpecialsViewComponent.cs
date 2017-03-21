using AliAspCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AliAspCore.ViewComponents
{
    [ViewComponent]
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        private readonly SpeciaDataContext _specials;
        public MonthlySpecialsViewComponent(SpeciaDataContext specials)
        {
            _specials = specials; 

        }
        public IViewComponentResult Invoke()
        {
            var specials = _specials.GetMonthlySpecials();
            return View(specials);
        }
    }
}
