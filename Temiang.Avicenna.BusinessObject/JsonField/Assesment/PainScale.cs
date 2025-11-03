using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    /// <summary>
    /// Glasgow Coma Scale
    /// </summary>
    public class PainScale
    {
        private string _painScaleType;
        public string PainScaleType
        {
            get { return _painScaleType; }
            set { _painScaleType = value; }
        }

        private int _painScaleValue;
        public int PainScaleValue
        {
            get { return _painScaleValue; }
            set { _painScaleValue = value; }

        }



    }
}
