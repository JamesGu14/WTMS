using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace WTMS.Common
{
    public static class LogHelper
    {

        public static void Log(Type t, string msg)
        {
            ILog log = LogManager.GetLogger(t);
            log.Info(msg);
        }

        public static void Log(Type t, Exception exc)
        {
            ILog log = LogManager.GetLogger(t);
            log.Error("Error", exc);
        }
    }
}
