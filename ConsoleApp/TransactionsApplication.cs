// See https://aka.ms/new-console-template for more information
using BankWebApp.Models;
using ConsoleApp.Controllers;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;

internal class TransactionsApplication
{
    //private readonly string _connectionString;
    //private readonly BankContext _context;
    //private readonly ITransactionsController _transactionsController;
    //private readonly ITransactionService _transactionService;


    //public TransactionsApplication()
    //{

    //}

    //public TransactionsApplication(string connectionString)
    //{


    //    //_connectionString = connectionString;
    //    var options = new DbContextOptionsBuilder<BankContext>();
    //    options.UseSqlServer("Server=.;Database=BankNewUsers;Trusted_Connection=True;MultipleActiveResultSets=true");

    //    _context = new BankContext(options.Options);

    //    _transactionsController = new TransactionsController(_transactionService);

    //}

    //public void Run()
    //{
    //    TransactionsController _transactionsController = new TransactionsController();
    //    _transactionsController.ListTransactions();
    //}

    //private readonly string _connectionString;
    private readonly BankContext _context;
    private readonly TransactionsController _transactionsController;

    public TransactionsApplication(string connectionString)
    {
        //_connectionString = connectionString;
        var options = new DbContextOptionsBuilder<BankContext>();
        options.UseSqlServer("Server=.;Database=BankNewUsers;Trusted_Connection=True;MultipleActiveResultSets=true");

        _context = new BankContext(options.Options);

        _transactionsController = new TransactionsController(_context);
    }

    public void Run()
    {
         //_transactionsController.ListTransactions();

        string[] Countries = { "Sweden", "Norway", "Denmark", "Finland" };
       

        foreach (var country in Countries)
        {
            _transactionsController.TransactionsPerCountry(country);
        }

        


    }

}

