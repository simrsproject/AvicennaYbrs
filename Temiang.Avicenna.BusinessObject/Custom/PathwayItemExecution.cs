using System;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class PathwayItemExecution
    {
        public string AssesmentHeaderName
        {
            get { return GetColumn("refToPathwayItem_AssesmentHeaderName").ToString(); }
            set { SetColumn("refToPathwayItem_AssesmentHeaderName", value); }
        }

        public string AssesmentGroupName
        {
            get { return GetColumn("refToPathwayItem_AssesmentGroupName").ToString(); }
            set { SetColumn("refToPathwayItem_AssesmentGroupName", value); }
        }

        public string AssesmentName
        {
            get { return GetColumn("refToPathwayItem_AssesmentName").ToString(); }
            set { SetColumn("refToPathwayItem_AssesmentName", value); }
        }
    }


    public partial class RegistrationPathwayItemExecution
    {
        public string SRPathwayExecutionType
        {
            get { return GetColumn("refToPathwayItemExecution_SRPathwayExecutionType").ToString(); }
            set { SetColumn("refToPathwayItemExecution_SRPathwayExecutionType", value); }
        }

    }
}
