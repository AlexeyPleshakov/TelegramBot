using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegramBot.Utilities
{
    public class Calculator
    {
        public static int Sum(string str)
        {
            int sum = 0;
            try
            {
                string[] strings = str.Split(' ');
                foreach (string s in strings)
                {
                    sum += Convert.ToInt32(s);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message + "\nВведите числа");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sum;
        }
    }
}
