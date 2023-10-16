using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TimerTest
{
    internal class DaemonTimer
    {
        Timer watchDogTimer = new Timer();

        private DateTime _startTime;

        public void Start()
        {
            watchDogTimer = new Timer()
            {
                Interval = 4000,
                AutoReset = false
            };
            watchDogTimer.Elapsed += WatchDogTimerElapsedEventHandler;
            watchDogTimer.Start();
            _startTime = DateTime.Now;
        }

        /// <summary>
        /// 看门狗计时器计满触发事件
        /// </summary>
        /// <param name="sender">计时器</param>
        /// <param name="e">参数</param>
        private void WatchDogTimerElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now} 看门狗计时器触发 {_startTime} 相差{(DateTime.Now - _startTime).TotalSeconds}");
        }

    }
}
