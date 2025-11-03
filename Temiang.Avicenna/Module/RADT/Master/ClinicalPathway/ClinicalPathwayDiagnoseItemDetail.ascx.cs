using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClinicalPathwayDiagnoseItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            var diag = new DiagnoseQuery();
            diag.Select(diag.DiagnoseID, diag.DiagnoseName);
            diag.Where(diag.DiagnoseID == (String)DataBinder.Eval(DataItem, PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID));

            cboDiagnoseID.DataSource = diag.LoadDataTable();
            cboDiagnoseID.DataBind();
            cboDiagnoseID.SelectedValue = (String)DataBinder.Eval(DataItem, PathwayDiagnoseItemMetadata.ColumnNames.DiagnoseID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string sessName = "collPathwayDiagnoseItemCollection";
                var coll = (PathwayDiagnoseItemCollection)Session[sessName];

                bool isExist = coll.Any(c => c.DiagnoseID == cboDiagnoseID.SelectedValue);
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Diagnose Name : {0} already exist", cboDiagnoseID.SelectedValue);
                    return;
                }

                //var txtPathwayID = Helper.FindControlRecursive(this.Page, "txtPathwayID") as RadTextBox;

                //var pdi = new PathwayDiagnoseItemQuery("a");
                //var pw = new PathwayQuery("b");

                //pdi.es.Top = 1;
                //pdi.Select(pdi);
                //pdi.InnerJoin(pw).On(pdi.PathwayID == pw.PathwayID);
                //pdi.Where(pw.PathwayID != txtPathwayID.Text, pw.IsActive == true, pdi.DiagnoseID == cboDiagnoseID.SelectedValue);

                //var eps = new PathwayDiagnoseItem();
                //if (eps.Load(pdi))
                //{
                //    var path = new Pathway();
                //    path.LoadByPrimaryKey(eps.PathwayID);

                //    args.IsValid = false;
                //    ((CustomValidator)source).ErrorMessage = string.Format("Diagnose Name : {0} already used on another clinical pathway : {1}", 
                //        cboDiagnoseID.SelectedValue, path.PathwayName);
                //    return;
                //}
            }
        }

        protected void cboDiagnoseID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = string.Format("({0}) {1}", ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString(),
                ((DataRowView)e.Item.DataItem)["DiagnoseName"].ToString());
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DiagnoseID"].ToString();
        }

        protected void cboDiagnoseID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var diag = new DiagnoseQuery();
            diag.es.Top = 20;
            diag.Select(diag.DiagnoseID, diag.DiagnoseName);
            diag.Where(
                diag.Or
                    (
                        diag.DiagnoseID.Like(searchTextContain),
                        diag.DiagnoseName.Like(searchTextContain)
                    )
                );
            diag.OrderBy(diag.DiagnoseName.Ascending);

            cboDiagnoseID.DataSource = diag.LoadDataTable();
            cboDiagnoseID.DataBind();
        }

        public String DiagnoseID
        {
            get { return cboDiagnoseID.SelectedValue; }
        }

        public String DiagnoseName
        {
            get { return cboDiagnoseID.Text; }
        }
    }
}