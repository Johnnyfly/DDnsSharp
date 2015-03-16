﻿using DDnsSharp.Monitor.Core;
using DDnsSharp.Monitor.Models;
using DDnsSharp.Monitor.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDnsSharp.Monitor.NinjectModules
{
    public class RuntimeNinjectModule:NinjectModule
    {
        public override void Load()
        {
            Bind<MonitorRuntime>().ToSelf().InSingletonScope();
            Bind<TempStorage>().ToSelf().InSingletonScope();
            Bind<DomainsCache>().ToSelf().InSingletonScope();
            Bind<MonitorConfig>().ToSelf().InSingletonScope();

            Bind<LoginWindowViewModel>().ToSelf();
            Bind<DDNSMonitorWindowViewModel>().ToSelf();
            Bind<RecordManageWindowViewModel>().ToSelf();
            Bind<SettingsWindowViewModel>().ToSelf();
        }
    }
}
