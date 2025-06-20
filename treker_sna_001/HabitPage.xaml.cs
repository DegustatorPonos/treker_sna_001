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
    /// Логика взаимодействия для HabitPage.xaml
    /// </summary>
    public partial class HabitPage : Page
    {
        
        public HabitPage()
        {
            InitializeComponent();
            List<Journal> list = App.db.Journals.Where(x => x.UserIdUser == GlobalData.user.IdUser).ToList();
            //Всего записей 
            int JornalCount = list.Count;

            if(JornalCount == 0)
            {
                MessageBox.Show("В журнале нет записей. Статистика недоступна.");
                statsStackPanel.Visibility = Visibility.Collapsed;
                return;
            }

            //Записей с типом сна глубокий
            int depthCount = 0;
            //Записей с ощущением бодрости
            int cheerfulnessCount = 0;
            //Записей с количеством пробуждений больше 1
            int WakeOneCount = 0;
            //Счетчик общего количества пробуждений
            int GlobalWakeCount = 0;
            //Счетчик высокой температуры
            int temperatureJarCount = 0;
            //Счетчик нормальной температуры
            int temperatureNormCount = 0;
            //Счетчик низкой температуры
            int temperatureColdCount = 0;
            //Счетчик записей с отметкой СТРЕСС
            int stressCount = 0;
            //Счетчик записий с отметкой о физической актимвности
            int phisCount = 0;
            //Сумма времени засыпания
            List<DateTime> DTlist = new List<DateTime>();
            //Сумма времени подъема
            List<DateTime> DTlistWake = new List<DateTime>();
            //Сумма времени сна
            List<TimeSpan> TotalSumList = new List<TimeSpan>();
            //подсчет баллов
            double ba;

            foreach (Journal journal in list)
            {
                if (journal.TypeDream == "Глубокий") depthCount++;
                if (journal.Feelings == "Бодрость") cheerfulnessCount++;
                if (journal.WakeUpCount > 1) WakeOneCount++;
                GlobalWakeCount += journal.WakeUpCount;
                if (journal.Temperature == "Жара")
                {
                    temperatureJarCount++;
                }
                else if (journal.Temperature == "Нормальная")
                {
                    temperatureNormCount++;
                }
                else
                {
                    temperatureColdCount++;
                }

                if(journal.Stress == "ДА") stressCount++;
                if(journal.Phisical == "ДА") phisCount++;
                DTlist.Add(journal.TimeDown);
                DTlistWake.Add(journal.TimeWakeUp);
                TotalSumList.Add(journal.SleepDuration);
            }

            //АнАлИз типа сна
            #region
            if (list.Count - depthCount > depthCount && depthCount != 0)
            {
                txt1.Text = "Вы плохо спите. Ваш сон нечасто бывает глубоким";
            }
            else if(depthCount == 0)
            {
                txt1.Text = "Вы плохо спите. Ваш сон не бывает глубоким";
            }
            else
            {
                txt1.Text = "Вы спите нормально";
            }
            #endregion
            //АнАлИз ощущений после пробуждения
            #region
            if (JornalCount - cheerfulnessCount > cheerfulnessCount)
            {
                txt2.Text = "Ваш сон некачественный";
            }
            else if(cheerfulnessCount == 0)
            {
                txt2.Text = "Ваш сон очень плох. Вы не отдыхаете во сне. Обратитесь к специалисту";
            }
            else
            {
                txt2.Text = "Вы хорошо отдыхаете в большинстве случаев";
            }
            #endregion
            //АнАлИз количества пробуждений
            #region
            
            double srWake = GlobalWakeCount / JornalCount; 
            if (srWake <= 2)
            {
                txt3.Text = $"Количество пробуждений в норме. Среднее количество пробуждений: {srWake}";
            }
            else
            {
                txt3.Text = "Количество пробуждений не в норме. Обратитесь к специалисту";
            }
            #endregion
            //Анализ времени отбоя
            #region
            TimeSpan averageTime = CalculateAverageTime(DTlist);
            txt4.Text =$"Среднее время отбоя: {averageTime.ToString(@"hh\:mm")}";
            #endregion
            //Анализ времени подъема
            #region
            TimeSpan averageTimeWake = CalculateAverageTime(DTlistWake);
            txt5.Text = $"Среднее время подъема: {averageTimeWake.ToString(@"hh\:mm")}";
            #endregion
            TimeSpan sum = TimeSpan.FromSeconds(0);
            //Подсчет среднего времени сна
            foreach (TimeSpan sleep in TotalSumList)
            {
                sum += sleep;
            }
            //будем считать, что 8 это нормальное время
            TimeSpan ts7 = TimeSpan.FromHours(7);
            TimeSpan ts9 = TimeSpan.FromHours(9);
            //подсчет среднего времени
            TimeSpan srTime = TimeSpan.FromTicks(sum.Ticks / TotalSumList.Count);
           
            if(srTime >= ts7 && srTime <= ts9)
            {
                txt6.Text = $"Средняя продолжительность сна в норме и равна: {srTime.ToString("hh\\:mm")}";
            }
            else if (srTime > ts9)
            {
                txt6.Text = $"Средняя продолжительность сна превышает норму на {(srTime - ts9).ToString("hh\\:mm")} и равна: {srTime.ToString("hh\\:mm")}";
            }
            else
            {
                txt6.Text = $"Средняя продолжительность сна меньше нормы на {(ts7 - srTime).ToString("hh\\:mm")} и равна: {srTime.ToString("hh\\:mm")}";
            }
            txt7.Text = $"Всего записей: {list.Count}";
        }
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

    }
}
