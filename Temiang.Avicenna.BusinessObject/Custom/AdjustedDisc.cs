using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    public class AdjustedDisc
    {
        private decimal? _adjustedDiscAmount;
        private int? _adjustedDiscSelection;

        public decimal? AdjustedDiscAmount
        {
            get { return _adjustedDiscAmount; }
            set { _adjustedDiscAmount = value; }
        }

        public int? AdjustedDiscSelection
        {
            get { return _adjustedDiscSelection; }
            set { _adjustedDiscSelection = value; }
        }

        public void Reset()
        {
            AdjustedDiscAmount = null;
            AdjustedDiscSelection = null;
        }
    }
}