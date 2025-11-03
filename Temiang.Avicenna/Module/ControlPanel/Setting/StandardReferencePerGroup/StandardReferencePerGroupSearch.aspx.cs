using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.ControlPanel.Setting
{
    public partial class StandardReferencePerGroupSearch : BasePageDialog
    {
        private string StdGroup
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["gr"]) ? string.Empty : Request.QueryString["gr"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            switch (StdGroup)
            {
                case "PINCIDENT":
                    ProgramID = AppConstant.Program.IncidentOtherMaster;
                    break;
                case "BLOODBANK":
                    ProgramID = AppConstant.Program.BloodBankStandardReference;
                    break;
                case "CSSD":
                    ProgramID = AppConstant.Program.CssdStandardReference;
                    break;
                case "PMKP":
                    ProgramID = AppConstant.Program.PmkpStandardReference;
                    break;
                case "ASSET":
                    ProgramID = AppConstant.Program.AssetStandardReference;
                    break;
                case "KEPK":
                    ProgramID = AppConstant.Program.KEPK_StandardReference;
                    break;
                case "LAUNDRY":
                    ProgramID = AppConstant.Program.LaundryStandardReference;
                    break;
                case "INV":
                    ProgramID = AppConstant.Program.InventoryStandardReference;
                    break;
                default:
                    ProgramID = AppConstant.Program.StandardReference;
                    break;
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
            var query = new AppStandardReferenceQuery();
            if (!string.IsNullOrEmpty(txtStandardReferenceID.Text))
            {
                if (cboFilterStandardReferenceID.SelectedIndex == 1)
                    query.Where(query.StandardReferenceID == txtStandardReferenceID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStandardReferenceID.Text);
                    query.Where(query.StandardReferenceID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtStandardReferenceName.Text))
            {
                if (cboFilterStandardReferenceName.SelectedIndex == 1)
                    query.Where(query.StandardReferenceName == txtStandardReferenceName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtStandardReferenceName.Text);
                    query.Where(query.StandardReferenceName.Like(searchTextContain));
                }
            }
            query.Where(query.StandardReferenceGroup == StdGroup, query.IsActive == true);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
