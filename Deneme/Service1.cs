using log4net;
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

namespace Deneme
{
    public partial class Service1 : ServiceBase
    {
        static ILog _LogDosyasi = LogManager.GetLogger(typeof(Service1));

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _LogDosyasi.Info("Servis başladı");
            ThreadPool.QueueUserWorkItem(new cIslemReader_0010().fn_BaslaINSERT);
           

        }

        protected override void OnStop()
        {
        }
    }
}
