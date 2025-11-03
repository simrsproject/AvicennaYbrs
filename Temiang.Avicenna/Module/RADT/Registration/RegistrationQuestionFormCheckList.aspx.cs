using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class RegistrationQuestionFormCheckList : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var regno = Request.QueryString["regNo"];
                var suid = Request.QueryString["unitID"];

                var qQF = new QuestionFormQuery("a");
                var qQFU = new QuestionFormInServiceUnitQuery("b");
                var qRQF = new RegistrationQuestionFormCheckListQuery("c");

                qRQF.Where(qRQF.RegistrationNo == regno);

                qQF.InnerJoin(qQFU).On(qQF.QuestionFormID == qQFU.QuestionFormID)
                    .LeftJoin(qRQF).On(qQFU.QuestionFormID == qRQF.QuestionFormID)
                    .Where(qQFU.ServiceUnitID == suid, qQF.IsActive == true)
                    .Select(
                        qQF.QuestionFormID,
                        qQF.QuestionFormName,
                        @"<CASE WHEN  c.RegistrationNo IS NULL THEN 0 ELSE 1 END IsAttached>"
                    );

                grdList.DataSource = qQF.LoadDataTable();
                grdList.DataBind();
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (grdList.SelectedValue != null)
            {
                return "oWnd.argument.print = '" + Page.Request.QueryString["regno"] + "|" + grdList.SelectedValue +
                       "'";
            }
            return string.Empty;
        }
        public override bool OnButtonOkClicked()
        {
            // 
            var regno = Request.QueryString["regNo"];
            var suid = Request.QueryString["unitID"];

            var odt = new RegistrationQuestionFormCheckListCollection();
            odt.Query.Where(odt.Query.RegistrationNo == regno);
            odt.LoadAll();



            // find checked
            List<string> docSelected = new List<string>();
            List<string> docRemoved = new List<string>();
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("IsAttached")).Checked)
                {
                    docSelected.Add(dataItem["QuestionFormID"].Text);
                }
                else
                {
                    docRemoved.Add(dataItem["QuestionFormID"].Text);
                }
            }

            // tambahkan yang selected
            foreach (var s in docSelected)
            {
                if ((from o in odt where o.QuestionFormID == s select o).Count() > 0)
                {
                    // jika sudah ada maka biarkan saja
                }
                else
                {
                    // jika belum ada tambahkan yang baru
                    var newo = odt.AddNew();
                    newo.QuestionFormID = s;
                    newo.RegistrationNo = regno;
                    newo.LastUpdateDateTime = DateTime.Now;
                    newo.LasuUpdateByUserID = AppSession.UserLogin.UserID;
                }
            }

            // buang yang gak dipilih
            foreach (var s in docRemoved)
            {
                if ((from o in odt where o.QuestionFormID == s select o).Count() > 0)
                {
                    // remove yang ini
                    var odel = (from o in odt where o.QuestionFormID == s select o).First();
                    odel.MarkAsDeleted();
                }
                else
                {
                    // 
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                odt.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        protected void grdList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is Telerik.Web.UI.GridDataItem)
            {
                Telerik.Web.UI.GridDataItem item = e.Item as Telerik.Web.UI.GridDataItem;
                System.Data.DataRowView dr = item.DataItem as System.Data.DataRowView; // Convert DataItem into Your Assigned Object
                (item.FindControl("IsAttached") as CheckBox).Checked = GetBoolValueFromString(Convert.ToString(dr["IsAttached"]));
            }
        }

        protected bool GetBoolValueFromString(string strFlag)
        {
            return strFlag.Trim() == "1";
        }
    }
}
