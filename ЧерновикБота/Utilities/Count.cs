using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegramBot.Utilities
{
    public static class Count
    {
        public static int GetCount(string str)
        {
            try
            {
                return str.Length;
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Неверные входящие данные");
            }
        }
    }
}
