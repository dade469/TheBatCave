using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Timers;
using PuppeteerSharp.Helpers;
using TheBatCoreWebScrapper.Core.Models;
using TheBatCoreWebScrapper.Core.Models.Clients;
using TheBatCoreWebScrapper.Core.Models.Results;
using TheBatCoreWebScrapper.Notifier;

namespace TheBatCoreWebScrapper.TheBatConsole
{
    class Program
    {
        private static Timer _timer = new Timer();
        private static MessageSender _messageSender = new MessageSender();

        private static int _numberOfCycle = 60;

        private static int _parallelLimits = 5;

        private static List<AdvancedScrapperResult> _scrappingResult = new List<AdvancedScrapperResult>();

        private static string _testUrl = @"https://www.notre-shop.com/collections/footwear/mens";

        static void Main(string[] args)
        {
            #region Timer implementation

            // Console.WriteLine("Begin get request");
            // _timer.Elapsed += TimerOnElapsed;
            //
            // _timer.Interval = 50;
            //
            // _timer.Start();
            //
            // Console.WriteLine("Result returned");

            #endregion

            #region Multi thread implementation

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // TaskGenerator(_testUrl);
            // stopwatch.Stop();
            // Console.WriteLine("Normal way");
            // Console.WriteLine($"Elapsed time:{stopwatch.ElapsedMilliseconds}");
            // Console.WriteLine($"Total number of cycle:{_numberOfCycle}");
            // Console.WriteLine($"Total number of failed cycle:{_scrappingResult.Count(item => !item.IsSucces)}");
            // _scrappingResult = new List<AdvancedScrapperResult>();
            //stopwatch.Restart();
            TaskParallelBecero();
            stopwatch.Stop();
            Console.WriteLine("Piemped way");
            Console.WriteLine($"Elapsed time:{stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"Total number of cycle:{_numberOfCycle}");
            Console.WriteLine($"Total number of failed cycle:{_scrappingResult.Count(item => !item.IsSucces)}");

            #endregion

            //Notifier.MessageSender k = new MessageSender();

            // k.SendMessage();
            _messageSender.SendMessage("Lettura finita");
            Console.ReadLine();
        }

        /// <summary>
        /// Execute scrapping task in parallels
        /// </summary>
        static void TaskParallel()
        {
            List<string> urlList = new List<string>();
            for (int i = 0; i < _numberOfCycle; i++)
            {
                urlList.Add(_testUrl);
            }

            var splittedList = splitList(urlList, _parallelLimits);

            foreach (var task in splittedList.AsParallel())
            {
                TaskGenerator(task);
            }
        }

        /// <summary>
        /// Execute scrapping task in parallels
        /// </summary>
        static void TaskParallelBecero()
        {
            List<string> urlList = new List<string>();
            for (int i = 0; i < _numberOfCycle; i++)
            {
                urlList.Add(_testUrl);
            }
            
            //var splittedList = splitList(urlList, _parallelLimits);
            
            urlList.AsParallel().ForAll(item=>SingleTaskGenerator(item));
            
        }
        /// <summary>
        /// Dynamic number of call
        /// </summary>
        /// <param name="url"></param>
        static void TaskGenerator(List<string> urls)
        {
            List<Task> taskList = new List<Task>();
            foreach (var url in urls)
            {

                taskList.Add(Task.Factory.StartNew(() => ScrapperTester(url)));

            }

            Task.WaitAll(taskList.ToArray());
        }

        // /// <summary>
        // /// Dynamic number of call
        // /// </summary>
        // /// <param name="url"></param>
        // static void TaskGenerator(List<string> url)
        // {
        //     Task[] taskArray = new Task[url.Count];
        //     for (int i = 0; i < url.Count; i++)
        //     {
        //         Console.WriteLine(i);
        //
        //         taskArray[i] = Task.Factory.StartNew(() => ScrapperTester(url.ElementAt(i)));
        //         Console.WriteLine(i);
        //     }
        //
        //     Task.WaitAll(taskArray);
        // }

        /// <summary>
        /// Fixed number of call
        /// </summary>
        /// <param name="url"></param>
        static void SingleTaskGenerator(string url)
        {
            Task.Run(() => ScrapperTester(url));
        }
        
        /// <summary>
        /// Fixed number of call
        /// </summary>
        /// <param name="url"></param>
        static void TaskGenerator(string url)
        {
            Task[] taskArray = new Task[_numberOfCycle];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew(() => ScrapperTester(url));
            }

            Task.WaitAll(taskArray);
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            ScrapperTester(_testUrl);

            if (_numberOfCycle > 0)
            {
                _timer.Start();
                _numberOfCycle -= 1;
            }
            else
            {
                _messageSender.SendMessage("Operation finished;");
            }
        }

        static void ScrapperTester(string url)
        {
            var httpClient = new AdvancedScrapperClient();
            _scrappingResult.Add(httpClient.GetContent(url).Result);

            Console.WriteLine("Finito");
            Console.WriteLine(_scrappingResult.Last().IsSucces);
        }

        static IEnumerable<List<T>> splitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}