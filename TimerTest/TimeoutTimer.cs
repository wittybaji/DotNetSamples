using System;
using System.Timers;

namespace TimerTest
{
    /// <summary>
    /// 超时计时器
    /// </summary>
    internal class TimeoutTimer : Timer
    {
        private readonly int _timeout;

        private readonly Action _timeoutCallback;

        public TimeoutTimer(int timeout, Action timeoutCallback) : base(timeout)
        {
            _timeout = timeout;
            _timeoutCallback = timeoutCallback;
            Elapsed += (s, e) => OnElapsed(e);
            AutoReset = false;
            Enabled = true;
        }

        public virtual void OnElapsed(ElapsedEventArgs e)
        {
            _timeoutCallback?.Invoke();
            Enabled = false;
        }

        public void Reset(double interval = 0)
        {
            Interval = interval == 0 ? _timeout : interval > 1000 ? interval : 1000;
            Enabled = true;
        }
    }
}
