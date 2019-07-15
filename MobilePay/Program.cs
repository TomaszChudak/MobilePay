using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobilePay.Configuration;
using MobilePay.Domain;
using MobilePay.Formatters;
using MobilePay.IO;
using MobilePay.UserStories;
using MobilePay.UserStories.UserStory2;
using MobilePay.UserStories.UserStory3;
using MobilePay.UserStories.UserStory4;
using MobilePay.UserStories.UserStory5;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("MobilePay.Tests")]

namespace MobilePay
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = GetServiceCollection();

            SetConfiguration(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var transactionSelector = serviceProvider.GetService<ITransactionSelector>();

            transactionSelector.Proceed();

            Console.ReadKey();
        }

        private static void SetConfiguration(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration);
        }

        private static IServiceCollection GetServiceCollection()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<ITransactionSelector, TransactionSelector>();
            serviceCollection.AddScoped<ITransactionCharger, TransactionCharger>();
            serviceCollection.AddScoped<ITransactionReader, TransactionReader>();
            serviceCollection.AddScoped<IFileReader, FileReader>();
            serviceCollection.AddScoped<ITransactionFeeWriter, TransactionFeeWriter>();
            serviceCollection.AddScoped<IConsoleWriter, ConsoleWriter>();
            serviceCollection.AddScoped<ITransactionFormatter, TransactionFormatter>();
            serviceCollection.AddScoped<ITransactionFeeFormatter, TransactionFeeFormatter>();
            serviceCollection.AddScoped<IStreamReaderWrapper, StreamReaderWrapper>();
            serviceCollection.AddScoped<IUserStoryFactory, UserStoryFactory>();
            serviceCollection.AddScoped<IMonthlyCharger, MonthlyCharger>();

            serviceCollection.AddScoped<IUserStoryMobilePay, UserStoryMobilePay2>();
            serviceCollection.AddScoped<IUserStoryMobilePay, UserStoryMobilePay3>();
            serviceCollection.AddScoped<IUserStoryMobilePay, UserStoryMobilePay4>();
            serviceCollection.AddScoped<IUserStoryMobilePay, UserStoryMobilePay5>();

            return serviceCollection;
        }
    }
}