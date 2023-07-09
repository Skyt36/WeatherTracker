using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherTracker.Services;

namespace WeatherTrackerService
{
    public partial class WeatherTrackerService : ServiceBase
    {
        public WeatherTrackerService()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override async void OnStart(string[] args)
        {
            while (true)
            {
                DB_address.UpdateWeather();
                await Task.Delay(3600000);
            }
        }

        protected override void OnStop()
        {
            Thread.Sleep(1000);
        }
    }
}
