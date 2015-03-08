﻿using DDnsSharp.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DDnsSharp.Service
{
    partial class DDnsSharpService : ServiceBase
    {
        private const int REGULAR_INTERVAL = 18000;
        private const int ERROR_INTERVAL = 300000;
        public DDnsSharpService()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            InitializeComponent();
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            System.IO.Directory.SetCurrentDirectory(path);
        }

        private Logger logger;

        private Timer timer;

        protected override void OnStart(string[] args)
        {
            logger = LogManager.GetCurrentClassLogger();

            timer = new Timer(REGULAR_INTERVAL);
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            OnJob();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.FatalException("服务出现致命异常.", e.ExceptionObject as Exception);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnJob();
        }

        private async void OnJob()
        {
            try
            {
                DDnsSharpRuntime.LoadAppConfig();
                await DDNS.Start(DDnsSharpRuntime.AppConfig.UpdateList);
                DDnsSharpRuntime.SaveAppConfig();
                if (timer.Interval > REGULAR_INTERVAL)
                    timer.Interval = REGULAR_INTERVAL;
            }
            catch (Exception ex)
            {
                logger.ErrorException("更新记录时出现意外错误", ex);
                timer.Interval = ERROR_INTERVAL;
            }
        }
    }
}
