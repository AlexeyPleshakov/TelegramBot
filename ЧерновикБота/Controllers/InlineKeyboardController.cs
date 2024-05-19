using MyTelegramBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTelegramBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage storage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = storage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery == null)
                return;
            _memoryStorage.GetSession(callbackQuery.From.Id).UserAction = callbackQuery.Data;

            string choice = callbackQuery.Data switch
            {
                "sum" => $"Выбран режим <b>сложения чисел</b>!",
                "count" => $"Выбран режим <b>подсчёта количества символов</b>!",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(
                callbackQuery.From.Id,
                choice,
                cancellationToken: ct,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}
