// See https://aka.ms/new-console-template for more information
using ConsoleApp.Controllers;


internal class TransactionsApplication
{
    
    private readonly TransactionsController _transactionsController;


    public TransactionsApplication(TransactionsController transactionsController)
    {
        _transactionsController = transactionsController;
    }

    public void Run()
    {
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
            StreamWriter writer = new StreamWriter(path, false);
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

            Console.WriteLine($"Report for {country} created successfully");
            writer.Close();
        }
    }


}




