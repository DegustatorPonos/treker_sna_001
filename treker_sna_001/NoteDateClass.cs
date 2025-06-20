using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treker_sna_001
{
    public partial class Journal
    {
        public string formatStartDate
        {
            get
            {
                string sd = TimeDown.ToString("HH:mm - dd MMMM");
                return sd;
            }
        }

        public string formatEndtDate
        {
            get
            {
                string sd = TimeWakeUp.ToString("HH:mm - dd MMMM");
                return sd;
            }
        }

        public TimeSpan SleepDuration
        {
            get
            {
                // Случай, когда EndSleep позже StartSleep (обычный случай)
                if (TimeWakeUp > TimeDown)
                {
                    return TimeWakeUp - TimeDown;
                }
                // Случай, когда EndSleep раньше StartSleep (переход через полночь)
                else
                {
                    // Вычисляем длительность до конца дня
                    TimeSpan durationToMidnight = DateTime.Today.AddDays(1) - TimeDown;

                    // Вычисляем длительность от начала следующего дня до EndSleep
                    TimeSpan durationFromMidnight = TimeWakeUp - DateTime.Today;

                    // Возвращаем общую продолжительность сна
                    return durationToMidnight + durationFromMidnight;
                }
            }
        }
    }
}
