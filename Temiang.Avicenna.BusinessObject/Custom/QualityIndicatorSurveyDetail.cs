using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class QualityIndicatorSurveyCollection {
        public DataTable ExecuteQuery(string sQuery)
        {
            return FillDataTable(esQueryType.Text, sQuery);
        }
    }
    public partial class QualityIndicatorSurveyDetail
    {
        public string ItemName
        {
            get { return GetColumn("refToAppStandardReferenceItem_ItemName").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ItemName", value); }
        }

        public string Note
        {
            get { return GetColumn("refToAppStandardReferenceItem_Note").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_Note", value); }
        }

        public int LineNumber
        {
            get { return System.Convert.ToInt32(GetColumn("refToAppStandardReferenceItem_LineNumber")); }
            set { SetColumn("refToAppStandardReferenceItem_LineNumber", value); }
        }
    }
}
