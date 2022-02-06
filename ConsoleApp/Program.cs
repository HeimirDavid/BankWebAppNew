// See https://aka.ms/new-console-template for more information
using BankWebApp.Models;
using ConsoleApp.Controllers;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
        services

        // Add Bank Db and Services to the container.
        .AddDbContext<BankContext>(options =>
        options.UseSqlServer(
        context.Configuration.GetConnectionString("DefaultConnection")))
            .AddTransient<ITransactionService, TransactionService>()
            .AddTransient<TransactionsController>()
            .AddTransient<TransactionsApplication>()
            )
    .Build();




using (var scope = host.Services.CreateScope())
{
    scope.ServiceProvider.GetService<TransactionsApplication>().Run();
}

host.Run();





//string connectionString = "Server=.;Database=BankNewUsers;Trusted_Connection=True;MultipleActiveResultSets=true";

//var builder = new ConfigurationBuilder()
//    .AddJsonFile($"appsettings.json", true, true);
//var config = builder.Build();

//var options = new DbContextOptionsBuilder<BankContext>();
////NOT BEING USED ATM
//string s =
//    config.GetConnectionString("DefaultConnection");
//options.UseSqlServer(connectionString);

////using (var context = new BankContext(options.Options))
////{
////    var dataInitializer = new DataInitializer();
////    dataInitializer.MigrateAndSeed(context);
////}



//var app = new TransactionsApplication();
//app.Run();












//using IHost host = Host.CreateDefaultBuilder(args)

//    .ConfigureServices((_, services) =>
//    {
//        services.AddDbContext<DbContext>(options =>
//               options.UseSqlServer(connectionString)
//               );
//        //services.AddTransient<ITransactionService, TransactionService>();
//        //services.AddTransient<TransactionsController>();


//    }).Build();


//public class MainApplication
//{
//    private readonly BankContext _context;

//    public MainApplication(BankContext context)
//    {
//        _context = context;
//    }


//    public void Run()
//    {
//        var transactions = _context.Transactions.AsQueryable();

//        foreach (var transaction in transactions)
//        {
//            Console.WriteLine(transaction);
//        }
//    }

//}

//var app = new MainApplication(context);


//var services = new ServiceCollection();
//ConfigureServices(services);
////services
////    .Add


//static void ConfigureServices(IServiceCollection services)
//{
//    services
//        .AddTransient<ITransactionService, TransactionService>();
//}


//var builder = new ConfigurationBuilder()
//    .AddJsonFile($"appsettings.json", true, true);


//var config = builder.Build();

//var options = new DbContextOptionsBuilder<BankContext>();
//string s =
//    config.GetConnectionString("DefaultConnection");
//options.UseSqlServer(connectionString);

//using (var context = new BankContext(options.Options))
//{
//    var dataInitializer = new DataInitializer();
//    dataInitializer.MigrateAndSeed(context);
//}



//var app = new TransactionsApplication();
//app.Run();

//Console.WriteLine("Hejhopp");