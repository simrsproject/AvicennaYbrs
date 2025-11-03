using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common
{
        public class EntryControl
        {
            private readonly string _controlID;
            private readonly string _controlType;

            public EntryControl(string controlID, string controlType)
            {
                _controlType = controlType;
                _controlID = controlID;
            }

            public string ControlID
            {
                get { return _controlID; }
            }

            public string ControlType
            {
                get { return _controlType; }
            }
        }

}
