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


        CreateRapport();

    }


    public void CreateRapport()
    {
        string[] Countries = { "Sweden", "Norway", "Denmark", "Finland" };

        DateTime date = DateTime.Today;
        var dateOnly = date.ToString("dd/MM/yyyy");
         

        foreach (var country in Countries)
        {
            string path = $"../../../Reports/bankReport -{country}-{dateOnly}.txt";
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine("");
            writer.WriteLine(country);

            var susTransactions = _transactionsController.TransactionsPerCountry(country);

            writer.WriteLine(" ");
            writer.WriteLine("Single Transactions over 15000:");
            foreach (var item in susTransactions.SusSingle)
            {
                writer.WriteLine($"Transaction-ID: {item.TransactionId}");
                writer.WriteLine($"Account-ID: {item.AccountId}");
                writer.WriteLine($"Amount: {item.Amount}");
                writer.WriteLine($"Date: {item.Date}");
                writer.WriteLine(" ");
            }

            Console.WriteLine($"Report for {country} created succelfully");
            writer.Close();
        }
    }


}




