using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceService.Web.Helpers
{
    public class PageSizeList
    {
        public static List<SelectListItem> List()
        {
            var SelectItems = new List<SelectListItem> {
                 new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "15", Value = "15" },
                new SelectListItem { Text = "25", Value = "25" }
             };
            return SelectItems;
        }
    }
}

