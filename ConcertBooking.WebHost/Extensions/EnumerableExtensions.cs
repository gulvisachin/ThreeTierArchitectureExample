using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking.WebHost.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null && !enumerable.Any();
        }

        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> Item)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem sli = new SelectListItem()
            {
                Text = "---Select---",
                Value = "0"
            };
            list.Add(sli);
            if (Item.Any())
            {
                foreach (var item in Item)
                {
                    sli = new SelectListItem
                    {
                        Text = item.GetPropertyValue("Name"),
                        Value = item.GetPropertyValue("Id"),
                    };
                    list.Add(sli);
                }
            }

            return list;

        }
    }
}
