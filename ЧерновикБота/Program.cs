using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using MyTelegramBot.Controllers;
using MyTelegramBot.Services;
using MyTelegramBot.Configuration;
using Newtonsoft.Json;
using System.Reflection;

namespace MyTelegramBot
{
    public class Program
    {
        public static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) 
                .UseConsoleLifetime() 
                .Build(); 

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();

            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<IStorage, MemoryStorage>();
        }

        static AppSettings? BuildAppSettings()
        {
            AppSettings settings = new AppSettings() 
            {
                BotToken = "6917814669:AAH8S89u8qUwb7toCUfeywZ0Fp1sh8PY96A" 
            };
            return settings;
        }
    }
}