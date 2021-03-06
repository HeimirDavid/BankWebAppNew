using BankWebApp.Services;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<UsersViewModel> Items { get; set; }
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }
        public string CustomerName { get; set; }
        public string SearchWord { get; set; }

        public void OnGet(string sortColumn, string sortOrder, int pageno, string searchWord, bool edit)
        {
            SortOrder = sortOrder;
            SortColumn = sortColumn;
            SearchWord = searchWord;

            if (pageno == 0)
            {
                pageno = 1;
            }
            CurrentPage = pageno;
            //var pageResult = _userService.GetAll(CurrentPage, sortColumn, sortOrder, searchWord);
            Items = _userService.GetAllUsersWithRoles(CurrentPage, sortColumn, sortOrder, searchWord);


            PageCount = Items.FirstOrDefault().PageCount;
            
        }
    }
}
