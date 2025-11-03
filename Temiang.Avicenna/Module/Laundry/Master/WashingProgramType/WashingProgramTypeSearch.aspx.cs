using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class WashingProgramTypeSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.LaundryWashingProgramType;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRLaundryProcessType, AppEnum.StandardReference.LaundryProcessType);
                StandardReference.InitializeIncludeSpace(cboSRLaundryProgram, AppEnum.StandardReference.LaundryProgram);
                StandardReference.InitializeIncludeSpace(cboSRLaundryType, AppEnum.StandardReference.LaundryType);
            }

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new LaundryWashingProgramTypeQuery("a");
            var pt = new AppStandardReferenceItemQuery("b");
            var p = new AppStandardReferenceItemQuery("c");
            var t = new AppStandardReferenceItemQuery("d");

            query.InnerJoin(pt).On(pt.StandardReferenceID == "LaundryProcessType" && pt.ItemID == query.SRLaundryProcessType);
            query.InnerJoin(p).On(p.StandardReferenceID == "LaundryProgram" && p.ItemID == query.SRLaundryProgram);
            query.InnerJoin(t).On(t.StandardReferenceID == "LaundryType" && t.ItemID == query.SRLaundryType);

            query.Select(
                query.LaundryProgramTypeID,
                query.SRLaundryProcessType,
                pt.ItemName.As("LaundryProcessTypeName"),
                query.SRLaundryProgram,
                p.ItemName.As("LaundryProgramName"),
                query.SRLaundryType,
                t.ItemName.As("LaundryTypeName"),
                query.Weight);
            query.OrderBy(query.SRLaundryProcessType.Ascending, query.SRLaundryProgram.Ascending, query.SRLaundryType.Ascending);

            var isEsTop = true;

            if (!string.IsNullOrEmpty(cboSRLaundryProcessType.SelectedValue))
            {
                query.Where(query.SRLaundryProcessType == cboSRLaundryProcessType.SelectedValue);
                isEsTop = false;
            }
            if (!string.IsNullOrEmpty(cboSRLaundryProgram.SelectedValue))
            {
                query.Where(query.SRLaundryProgram == cboSRLaundryProgram.SelectedValue);
                isEsTop = false;
            }
            if (!string.IsNullOrEmpty(cboSRLaundryType.SelectedValue))
            {
                query.Where(query.SRLaundryType == cboSRLaundryType.SelectedValue);
                isEsTop = false;
            }

            if (isEsTop)
                query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();
            return true;
        }
    }
}