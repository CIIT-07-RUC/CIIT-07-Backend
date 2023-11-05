using System;
namespace WebAPI.Controllers
{
	public class Helpers
	{

        public object Pagination<T>(List<T> items, int page, int visibleItems = 5)
		{
			var total = items.Count;
            int startIndex = (page - 1) * visibleItems;

            if (startIndex > total)
            {
                startIndex = total - visibleItems;
            }

            var selectedObjects = items.Skip(startIndex).Take(visibleItems).ToList();

            return new
            {
                Items = selectedObjects, 
                TotalItems = total 
            };
        }
	}
}

