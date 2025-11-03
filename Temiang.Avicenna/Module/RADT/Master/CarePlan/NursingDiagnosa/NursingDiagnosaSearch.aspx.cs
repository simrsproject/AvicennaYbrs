using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingDiagnosaSearch : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return Request.QueryString["ndl"];
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            switch (getPageID)
            {
                case "00":
                    {
                        ProgramID = AppConstant.Program.NursingDomain;
                        break;
                    }
                case "10":
                    {
                        ProgramID = AppConstant.Program.NursingDiag;
                        break;
                    }
                case "11":
                    {
                        ProgramID = AppConstant.Program.NursingProblem;
                        break;
                    }
                case "20":
                    {
                        ProgramID = AppConstant.Program.NursingNOC;
                        break;
                    }
                case "21":
                    {
                        ProgramID = AppConstant.Program.NursingNOCObjcetive;
                        break;
                    }
                case "30":
                    {
                        ProgramID = AppConstant.Program.NursingNIC;
                        break;
                    }
                case "31":
                    {
                        ProgramID = AppConstant.Program.NursingNICImplementation;
                        break;
                    }
                default:
                    {
                        ProgramID = AppConstant.Program.NursingDiagnosa;
                        break;
                    }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey("NursingDiagnosaLevel", getPageID)) {
                    lblNursingDiagnosaID.Text = std.ItemName + " ID";
                    lblNursingDiagnosaName.Text = std.ItemName + " Name";
                }

                ComboBox.StandardReferenceItem(cboDiagType, AppEnum.StandardReference.NsDiagnosaType.ToString(), true);
            }
        }
        public override bool OnButtonOkClicked()
        {

            var query = new NursingDiagnosaQuery("a");
            NursingDiagnosaQuery parent = new NursingDiagnosaQuery("p");
            AppStandardReferenceItemQuery level = new AppStandardReferenceItemQuery("b");

            query.LeftJoin(parent).On(query.NursingDiagnosaParentID == parent.NursingDiagnosaID);
            query.InnerJoin(level).On(query.SRNursingDiagnosaLevel == level.ItemID & level.StandardReferenceID == "NursingDiagnosaLevel");
            query.Select
                (
                    query.NursingDiagnosaID,
                    query.NursingDiagnosaName,
                    query.NursingDiagnosaParentID,
                    parent.NursingDiagnosaCode.As("NursingDiagnosaParentCode"),
                    parent.NursingDiagnosaName.As("NursingDiagnosaParentName"),
                    query.SRNursingDiagnosaLevel,
                    level.ItemName,
                    query.IsActive
                );


            if (!string.IsNullOrEmpty(txtNursingDiagnosaID.Text))
            {
                if (cboFilterNursingDiagnosaID.SelectedIndex == 1)
                    query.Where(query.NursingDiagnosaID == txtNursingDiagnosaID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtNursingDiagnosaID.Text);
                    query.Where(query.NursingDiagnosaID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtNursingDiagnosaName.Text))
            {
                if (cboFilterNursingDiagnosaName.SelectedIndex == 1)
                    query.Where(query.NursingDiagnosaName == txtNursingDiagnosaName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtNursingDiagnosaName.Text);
                    query.Where(query.NursingDiagnosaName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboNursingParent.SelectedValue))
            {
                query.Where(query.NursingDiagnosaParentID == cboNursingParent.SelectedValue);
            }
            if (Request.QueryString["ndl"] != null)
            {
                query.Where(query.SRNursingDiagnosaLevel == getPageID);
            }
            if (!string.IsNullOrEmpty(cboDiagType.SelectedValue)) {
                query.Where(query.SRNsDiagnosaType == cboDiagType.SelectedValue);
            }
            query.OrderBy(query.NursingDiagnosaID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }

        protected void cboNursingParent_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var obj = new NursingDiagnosaQuery("a");
            var prn = new NursingDiagnosaQuery("b");

            obj.es.Top = 20;
            obj.es.Distinct = true;
            obj.InnerJoin(prn).On(obj.NursingDiagnosaParentID == prn.NursingDiagnosaID)
                .Where(
                obj.Or(
                    prn.NursingDiagnosaID.Like(searchTextContain),
                    prn.NursingDiagnosaName.Like(searchTextContain)
                ),
                obj.IsActive == true,
                prn.IsActive == true,
                obj.SRNursingDiagnosaLevel == getPageID
            ).Select(prn);
            cboNursingParent.DataSource = obj.LoadDataTable();
            cboNursingParent.DataBind();
        }

        protected void cboNursingParent_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["NursingDiagnosaID"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["NursingDiagnosaName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["NursingDiagnosaID"].ToString();
        }
    }
}