using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe.EmrCommon.OperatingNotes
{
    public partial class OperatingNotesTemplateCopy : BasePageDialog
    {
        private string FormType
        {
            get { return this.Request.QueryString["tp"]; }
        }

        protected string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = FormType == "opr" ? "Operating Notes Template Copy" : (FormType == "ans" ? "Anesthetist Notes Template Copy" : "Post Surgery Instructions Template Copy");
                Session.Remove("oprnotes_" + FormType + ParamedicID);
                txTemplateText.Text = string.Empty;
            }

        }        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (grdList.SelectedValues != null)
            {
                return string.Format("oWnd.argument.notes = '{0}'", HttpContext.Current.Server.UrlEncode(txTemplateText.Text));
            }
            return "oWnd.argument.notes = ''";
        }

        private DataTable OperationNotesTemplates()
        {
            if ( Session["oprnotes_" + FormType + ParamedicID] == null)
            {
                var qr = new OperationNotesTemplateQuery();
                qr.Select(qr.TemplateID, qr.TemplateName);
                qr.Where(qr.Or(qr.ParamedicID.IsNull(), qr.ParamedicID == ParamedicID));
                if (FormType == "opr" || FormType == "ans")
                    qr.Where(qr.Or(qr.IsPostOp == false, qr.IsPostOp.IsNull()));
                else
                    qr.Where(qr.IsPostOp == true);

                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtSearch.Text);
                    qr.Where(qr.TemplateName.Like(searchTextContain));
                }

                Session["oprnotes_" + FormType + ParamedicID] = qr.LoadDataTable();
            }
            return (DataTable)Session["oprnotes_" + FormType + ParamedicID];
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dtb = OperationNotesTemplates();
            grdList.DataSource = dtb;
            if (dtb.Rows.Count == 1)
            {
                var id = dtb.Rows[0]["TemplateID"].ToInt();
                PopulateTemplateText(id);
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        public override bool OnButtonOkClicked()
        {

            if (string.IsNullOrEmpty(txTemplateText.Text)) return false;
            Session.Remove("oprnotes_" + FormType + ParamedicID);
            return true;
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            txTemplateText.Text = string.Empty;
            Session["oprnotes_" + FormType + ParamedicID] = null;
            grdList.Rebind();
        }

        protected void grdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = grdList.SelectedValue;
            PopulateTemplateText(id);
        }

        private void PopulateTemplateText(object id)
        {
            var ent = new OperationNotesTemplate();
            ent.LoadByPrimaryKey(id.ToInt());
            txTemplateText.Text = ent.TemplateText;
        }

        protected void grdList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    var item = e.Item as GridDataItem;
                    if (item == null) return;
                    var ent = new OperationNotesTemplate();
                    var id = item.OwnerTableView.DataKeyValues[item.ItemIndex]["TemplateID"];
                    ent.LoadByPrimaryKey(id.ToInt());
                    ent.MarkAsDeleted();
                    ent.Save();

                    Session["oprnotes_" + FormType + ParamedicID] = null;
                    grdList.Rebind();
                    break;
            }
        }
    }
}