using Core;
using System;

namespace Stats
{
    public class Timer : IDirty, IResetable
    {
        private int secondsPerDay = 100;

        private ITimeProvider timeProvider;
        private DateTime time;
        private long milliSecondAccum;
        private bool isDayStarted;
        private int currentDay;
        private int elapsed;

        private bool dirty = false;

        private int daySecondsCounter = 0;

        public int SecondsPerDay { get => secondsPerDay; }

        public bool IsDayStarted
        {
            get => isDayStarted;
            set
            {
                if (value)
                {
                    isDayStarted = true;
                    HandleDayStarted();
                }
                else
                {
                    isDayStarted = false;
                }
            }
        }

        public Timer(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
            Reset();
        }

        public int CurrentDay { get => currentDay; }

        public int Elapsed { 
            get => elapsed;
            private set {
                elapsed = value;
                dirty = true;
            }
        }

        public void Tick()
        {
            if (!isDayStarted)
            {
                return;
            }

            DateTime curr = timeProvider.UtcNow();

            if (time != DateTime.MinValue)
            {
                TimeSpan delta = curr.Subtract(time);
                Elapsed += delta.Milliseconds;

                HandleSecondPassed(delta);
                HandleDayPassed();
            }

            time = curr;
        }

        private void HandleSecondPassed(TimeSpan delta)
        {
            milliSecondAccum += delta.Milliseconds;

            if (milliSecondAccum >= 1000)
            {
                SecondPassed?.Invoke(this, EventArgs.Empty);
                milliSecondAccum = milliSecondAccum - 1000;

                if (isDayStarted)
                {
                    daySecondsCounter++;
                }
            }
        }

        private void HandleDayPassed()
        {
            if (daySecondsCounter >= SecondsPerDay)
            {
                currentDay++;
                dirty = true;
                IsDayStarted = false;
                DayPassed?.Invoke(this, EventArgs.Empty);
            }
        }

        private void HandleDayStarted()
        {
            Elapsed = 0;
            daySecondsCounter = 0;
            DayStarted?.Invoke(this, EventArgs.Empty);
        }

        public float GetDayPercentage()
        {
            return Elapsed / 1000f / SecondsPerDay;
        }

        public bool IsDirty()
        {
            return dirty;
        }

        public void ClearDirty()
        {
            dirty = false;
        }

        public void Reset()
        {
            time = DateTime.MinValue;
            milliSecondAccum = 0;
            isDayStarted = false;
            currentDay = 1;
            Elapsed = 0;
        }

        public event EventHandler SecondPassed;
        public event EventHandler DayPassed;
        public event EventHandler DayStarted;
    }
}