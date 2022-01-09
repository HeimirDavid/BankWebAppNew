using BankWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public List<Item> Items { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public void OnGet(int pageno)
        {
            if (pageno == 0)
            {
                pageno = 1;
            }
            CurrentPage = pageno;
            var pageResult = _customerService.GetAll(CurrentPage);

            PageCount = pageResult.PageCount;


            Items = pageResult.Results.Select(i => new Item
            {
                Id = i.CustomerId,
                Name = i.Givenname,
            }).ToList();
        }
    }
}
