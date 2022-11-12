using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SEQ
{
    public class Program
    {   
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            decimal course = 60;
            decimal com1 = 8.0m;
            decimal com2 = 0.0037m;
            decimal value = 0.0m; ;
            bool check = false; ;
            var log = new LoggerConfiguration()
             .MinimumLevel.Information()
             .Enrich.WithProperty("Курс: ", course)
             .WriteTo.Seq("http://localhost:5341", apiKey: "MoVnCg2plWLB4CtILRoU")
             .CreateLogger();

            while (value < 1 || !check)
            {
                Console.Write("Введите число: ");
                log.Information("Пользователь вводит значение");
                check = decimal.TryParse(Console.ReadLine(), out value);
                   
                if (value < 1)
                {
                    log.Error("Пользователь ввёл неверное значение");
                }
            }
            
            log.Information($"Пользователь ввёл корректное значение: {value} $");
            Console.WriteLine("Курс доллара: {0:C2}", course);         
            Console.WriteLine("Сумма в рублях: {0:C2}", value * course);

            if (value < 500)
            {
                Console.WriteLine("Итоговая сумма в рублях (с учётом комиссии {0:C2}): {1:C2}", com1, (value * course - com1));
                log.Information("Обмен средств прошёл успешно, получено: {0:f} ₽", value * course - com1);
            }
            else
            {
                Console.WriteLine("Комиссия за услуги (0,37%): {0:C2}", + (value * course) * (com2));
                Console.WriteLine("Итоговая сумма в рублях (с учётом комиссии 0,37%): {0:C2} ", (value * course - ((value * course) * (com2))));
                log.Information("Обмен средств прошёл успешно, получено: {0:f} ₽", value * course - ((value * course) * (com2)));
            }
            Log.CloseAndFlush();
            Console.ReadKey();
        }
    }
}
