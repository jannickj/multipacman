using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using GooseEngine.GameManagement.Actions;

namespace GooseEngine.GameManagement
{
    public class GameTimer
    {
        private Action action;
        private Timer timer = new Timer();
        private bool single;
        private ActionManager actman;
        private DateTime stopped;

        public GameTimer(ActionManager actman, Action action)
        {
            this.actman = actman;
            this.action = action;
            timer.AutoReset = false;
            
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!single)
                this.timer.Start();

            actman.Queue(new SimpleAction(sa => action()));
        }

        private void start(double m)
        {
            timer.Interval = m;
            timer.Start();
        }

        public void StartSingle(double milisec)
        {
            single = true;
            start(milisec);
            

        }

        public void StartPeriodic(double milisec)
        {
            single = false;
            start(milisec);
        }

        public void Stop()
        {
            this.stopped = DateTime.Now;   
            this.timer.Stop();
        }

    }
}
