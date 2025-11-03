using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ItemGroup
    {
        private AdjustedDisc _adjustedDisc = new AdjustedDisc();
        private bool _autoAdjust = false;

        public AdjustedDisc AdjustedDisc
        {
            get { return _adjustedDisc; }
            set { _adjustedDisc = value; }
        }

        public bool AutoAdjust {
            get { return _autoAdjust; }
            set { _autoAdjust = value; }
        }
    }
}