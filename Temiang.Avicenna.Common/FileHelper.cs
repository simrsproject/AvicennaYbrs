using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common
{
    public class FileHelper
    {
        private static System.Threading.Thread _thread;
        private static string _file = string.Empty;
        private static bool _isExist = false;

        /// <summary>
        /// Check file exist using time out setting
        /// </summary>
        /// <param name="file">file name or path</param>
        /// <param name="timeOut">Milisecond</param>
        /// <returns></returns>
        public static bool FileExists(string file, int timeOut)
        {
            _file = file;
            _isExist = false;
            _thread = new System.Threading.Thread(CallFileExists);
            _thread.Start();
            _thread.Join(timeOut);
            _thread.Abort();
            return _isExist;
        }

        private static void CallFileExists()
        {
            _isExist = System.IO.File.Exists(_file);
        }


    }
}
