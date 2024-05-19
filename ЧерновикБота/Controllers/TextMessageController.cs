using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using MyTelegramBot.Services;
using System.Threading;
using MyTelegramBot.Utilities;

namespace MyTelegramBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _storage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage storage)
        {
            _telegramClient = telegramBotClient;
            _storage = storage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчёт суммы чисел",$"sum"),
                        InlineKeyboardButton.WithCallbackData($"Подсчёт символов", $"count")
                    });
                    await _telegramClient.SendTextMessageAsync(
                        message.Chat.Id,
                        $"<b> Бот может подсчитать символы в тексте или сумму чисел.</b> " +
                        $"{Environment.NewLine}" +
                        $"{Environment.NewLine}" +
                        $"Выберите желаемое." +
                        $"{Environment.NewLine}",
                        cancellationToken: ct,
                        parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:

                    string result = string.Empty;
                    int res = 0;
                    switch (_storage.GetSession(message.Chat.Id).UserAction)
                    {
                        case "sum":
                            res = Calculator.Sum(message.Text);
                            if (res == 0) result = "Error";
                            if ( res != 0) result = $"Сумма чисел равна {res}";
                            break;
                        case "count":
                            res = Count.GetCount(message.Text);
                            result = $"В сообщении {res} символов";
                            break;
                        default:
                            result = "Выберите режим бота";
                            break;
                    }

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
                    break;
            }
        }
    }
}
