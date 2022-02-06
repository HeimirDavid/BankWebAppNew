// See https://aka.ms/new-console-template for more information
using ConsoleApp.Controllers;
using ConsoleApp.Services;

internal class TransactionsApplication
{

    private readonly TransactionsController _transactionsController;
    private readonly IFraudService _transactionService;

    public TransactionsApplication(TransactionsController transactionsController, IFraudService transactionService)
    {
        _transactionsController = transactionsController;
        _transactionService = transactionService;
    }

    public void Run()
    {
        CreateRapport();
        _transactionService.WriteDateToTextFile();

        Environment.Exit(0);

    }


    public void CreateRapport()
    {
        string[] Countries = { "Sweden", "Norway", "Denmark", "Finland" };

        DateTime date = DateTime.Today;
        var dateOnly = date.ToString("dd/MM/yyyy");


        foreach (var country in Countries)
        {
            string path = $"../../../Reports/bankReport -{country}-{dateOnly}.txt";
            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine("");
            writer.WriteLine(country);


            var susTransactions = _transactionsController.TransactionsPerCountry(country);

            //Create repports for single transactions over 15000
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


            //Create repports for three days transactions over 23000
            writer.WriteLine("-----------------------------------------");
            writer.WriteLine(" ");
            writer.WriteLine("Multiple transactions over 23000, or single over 23000:");
            writer.WriteLine(" ");

            foreach (var transactions in susTransactions.SusMany)
            {
                writer.WriteLine("-----------------------------------------");
                writer.WriteLine("Suspicious transaction(s)");
                writer.WriteLine("-----------------------------------------");
                writer.WriteLine(" ");


                foreach (var item in transactions.Transactions)
                {
                    writer.WriteLine($"Transaction-ID: {item.TransactionId}");
                    writer.WriteLine($"Account-ID: {item.AccountId}");
                    writer.WriteLine($"Amount: {item.Amount}");
                    writer.WriteLine($"Date: {item.Date}");
                    writer.WriteLine(" ");
                }
            }



            Console.WriteLine($"Report for {country} created successfully");
            writer.Close();
        }
        Console.WriteLine("");
        Console.WriteLine("All reports created");
    }


}




