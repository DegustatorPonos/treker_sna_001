using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace treker_sna_001
{
    /// <summary>
    /// Логика взаимодействия для Test1.xaml
    /// </summary>
    public partial class Test1 : Page
    {
        public Test1()
        {
            InitializeComponent();
        }
        public class TimeAverager
        {
            public static TimeSpan CalculateAverageTime(List<DateTime> dateTimes)
            {
                if (dateTimes == null || dateTimes.Count == 0)
                {
                    throw new ArgumentException("Список DateTime не может быть null или пустым.");
                }

                // Преобразуем все DateTime в TimeSpan относительно начала дня.
                List<TimeSpan> timeSpans = dateTimes.Select(dt => dt.TimeOfDay).ToList();

                // Вычисляем среднее значение как сумму TimeSpan деленную на количество.
                TimeSpan totalTime = TimeSpan.Zero;
                foreach (TimeSpan timeSpan in timeSpans)
                {
                    totalTime += timeSpan;
                }

                //Чтобы избежать деления на 0
                return TimeSpan.FromTicks(totalTime.Ticks / timeSpans.Count);
            }

            /*public static void Main(string[] args)
            {
                // Пример использования:
                List<DateTime> dateTimes = new List<DateTime>
        {
            DateTime.Now.Date.AddHours(8).AddMinutes(30), // 8:30 AM
            DateTime.Now.Date.AddHours(12).AddMinutes(0),  // 12:00 PM
            DateTime.Now.Date.AddHours(16).AddMinutes(15), // 4:15 PM
            DateTime.Now.Date.AddHours(20).AddMinutes(0)   // 8:00 PM
        };

                try
                {
                    TimeSpan averageTime = CalculateAverageTime(dateTimes);
                    Console.WriteLine("Среднее время: " + averageTime); // Вывод в формате HH:mm:ss
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }

                // Пример с другим списком DateTime:
                List<DateTime> anotherDateTimes = new List<DateTime>
        {
            DateTime.Now.Date.AddHours(9),
            DateTime.Now.Date.AddHours(10),
            DateTime.Now.Date.AddHours(11)
        };

                try
                {
                    TimeSpan averageTime = CalculateAverageTime(anotherDateTimes);
                    Console.WriteLine("Среднее время: " + averageTime);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }

                // Пример с пустым списком:
                List<DateTime> emptyList = new List<DateTime>();

                try
                {
                    TimeSpan averageTime = CalculateAverageTime(emptyList);
                    Console.WriteLine("Среднее время: " + averageTime);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message); // Ожидаем исключение ArgumentException
                }
            }*/
        }
    }
}