using System;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PathwayDiagnoseItem
    {
        public string DiagnoseName
        {
            get { return GetColumn("refToDiagnose_DiagnoseName").ToString(); }
            set { SetColumn("refToDiagnose_DiagnoseName", value); }
        }
    }
}
