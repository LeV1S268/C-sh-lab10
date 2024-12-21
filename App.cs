using Functions;
using marketContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    internal class App
    {
        static async Task Main(string[] args)
        {
            using (var db = new MarketContext())
            {
                var marketFuncs = new MarketFunctions(db);

                // Читаем тикеры из файла "ticker.txt"
                var tickerFilePath = "C:\\Users\\aleks\\OneDrive\\Рабочий стол\\Прога\\СЕМ 3\\lab10\\ticker.txt"; // Путь к файлу с тикерами

                if (!File.Exists(tickerFilePath))
                {
                    Console.WriteLine($"Файл с тикерами {tickerFilePath} не найден.");
                    return;
                }

                var tickers = File.ReadAllLines(tickerFilePath).ToList();

                foreach (var ticker in tickers)
                {
                    string startDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    string endDate = DateTime.Now.ToString("yyyy-MM-dd");

                    Console.WriteLine($"Ожидание данных для тикера {ticker} с {startDate} по {endDate}...");
                    await marketFuncs.GetDataAndSaveAsync(ticker, startDate, endDate);
                }

                Console.WriteLine("Анализируем акции...");
                await marketFuncs.AnalyzeData();

                // Выводим результаты анализа
                foreach (var ticker in tickers)
                {
                    var condition = db.todaysConditions
                        .FirstOrDefault(tc => tc.tickerSymConditions.tickerSym == ticker);

                    if (condition != null)
                    {
                        Console.WriteLine($"Состояние акции {ticker}: {condition.state}");
                    }
                    else
                    {
                        Console.WriteLine($"Недостаточно данных для тикера {ticker}");
                    }
                }
            }
        }
    }
}
