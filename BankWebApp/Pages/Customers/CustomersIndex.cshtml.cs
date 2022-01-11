using BankWebApp.Services;
using BankWebApp.ViewModels;
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
        public List<CustomerList> Items { get; set; }
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }
        public string CustomerName { get; set; }
        public string SearchWord { get; set; }

        public void OnGet(string sortColumn, string sortOrder, int pageno, string searchWord)
        {
            SortOrder = sortOrder;
            SortColumn = sortColumn;
            SearchWord = searchWord;

            if (pageno == 0)
            {
                pageno = 1;
            }
            CurrentPage = pageno;
            var pageResult = _customerService.GetAll(CurrentPage, sortColumn, sortOrder, searchWord);

            PageCount = pageResult.PageCount;


            Items = pageResult.Results.Select(i => new CustomerList
            {
                Id = i.CustomerId,
                Name = i.Givenname,
                Address = i.Streetaddress,
                City = i.City,
                SSN = i.NationalId,
            }).ToList();
        }
    }
}
