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
            .AddTransient<IFraudService, FraudService>()
            .AddTransient<TransactionsController>()
            .AddTransient<TransactionsApplication>()
            )
    .Build();




using (var scope = host.Services.CreateScope())
{
    scope.ServiceProvider.GetService<TransactionsApplication>().Run();
}

host.Run();
