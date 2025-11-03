using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI.Calendar;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class NumberOfBedDetail : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.NumberOfBed;

            if (IsPostBack) return;

            var query = new ClassQuery("a");
            query.Where(
                query.IsInPatientClass == true,
                query.IsActive == true
            );
            query.OrderBy(query.ClassID.Ascending);

            var cls = new ClassCollection();
            cls.Load(query);

            cboClassID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (Class entity in cls)
            {
                cboClassID.Items.Add(new RadComboBoxItem(entity.ClassName, entity.ClassID));
            }

            txtStartingDate.SelectedDate = string.IsNullOrEmpty(Request.QueryString["sdate"]) ? DateTime.Now : Convert.ToDateTime(Request.QueryString["sdate"]);
            cboClassID.SelectedValue =
                string.IsNullOrEmpty(Request.QueryString["sclass"]) ? string.Empty : Request.QueryString["sclass"];
            txtStartingDate.Enabled = Request.QueryString["md"] == "new";
        }

        private DataTable NumberOfBeds
        {
            get
            {
                var su = new ServiceUnitQuery("c");
                su.Select(
                    string.Format("<'{0}' AS StartingDate>", txtStartingDate.SelectedDate.Value.ToShortDateString()),
                    string.Format("<'{0}' AS ClassID>", cboClassID.SelectedValue),
                    su.ServiceUnitID,
                    su.ServiceUnitName,
                    string.Format(@"<CAST(ISNULL((SELECT cb.NumberOfBed FROM NumberOfBed AS cb WHERE cb.StartingDate = '{0}' AND cb.ServiceUnitID = c.ServiceUnitID AND cb.ClassID = '{1}'), 0) AS VARCHAR(MAX)) AS NumberOfBed>",
                        txtStartingDate.SelectedDate.Value.ToShortDateString(), cboClassID.SelectedValue)

                );
                su.Where(su.IsActive == true, su.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                su.OrderBy(su.ServiceUnitID.Ascending);

                var tab1 = su.LoadDataTable();
                return tab1;
            }
        }

        private DataTable NumberOfBedSmfs
        {
            get
            {
                var smf = new SmfQuery("c");
                smf.Select(
                    string.Format("<'{0}' AS StartingDate>", txtStartingDate.SelectedDate.Value.ToShortDateString()),
                    string.Format("<'{0}' AS ClassID>", cboClassID.SelectedValue),
                    smf.SmfID,
                    smf.SmfName,
                    string.Format(@"<CAST(ISNULL((SELECT cb.NumberOfBed FROM NumberOfBedSmf AS cb WHERE cb.StartingDate = '{0}' AND cb.ClassID = '{1}' AND cb.SmfID = c.SmfID ), 0) AS VARCHAR(MAX)) AS NumberOfBed>",
                        txtStartingDate.SelectedDate.Value.ToShortDateString(), cboClassID.SelectedValue)

                );
                smf.OrderBy(smf.SmfID.Ascending);

                var tab1 = smf.LoadDataTable();
                return tab1;
            }
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            using (var trans = new esTransactionScope())
            {
                var nobs = new NumberOfBedCollection();
                nobs.Query.Where(
                    nobs.Query.StartingDate == txtStartingDate.SelectedDate.Value.Date,
                    nobs.Query.ClassID == cboClassID.SelectedValue
                );
                nobs.Query.Load();

                nobs.MarkAllAsDeleted();
                nobs.Save();

                foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => !string.IsNullOrEmpty((dataItem.FindControl("txtNumberOfBed") as RadTextBox).Text)))
                {
                    var nob = new NumberOfBed();
                    if (!nob.LoadByPrimaryKey(txtStartingDate.SelectedDate.Value.Date, dataItem["ServiceUnitID"].Text, cboClassID.SelectedValue))
                    {
                        nob = new NumberOfBed();
                        nob.StartingDate = txtStartingDate.SelectedDate.Value.Date;
                        nob.ServiceUnitID = dataItem["ServiceUnitID"].Text;
                        nob.ClassID = cboClassID.SelectedValue;
                    }
                    nob.NumberOfBed = int.Parse(string.IsNullOrEmpty((dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text) ? "0" : (dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text);
                    nob.LastUpdateDateTime = DateTime.Now;
                    nob.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    nob.Save();
                }

                var nob2s = new NumberOfBedSmfCollection();
                nob2s.Query.Where(
                    nob2s.Query.StartingDate == txtStartingDate.SelectedDate.Value.Date,
                    nob2s.Query.ClassID == cboClassID.SelectedValue
                );
                nob2s.Query.Load();

                nob2s.MarkAllAsDeleted();
                nob2s.Save();

                foreach (GridDataItem dataItem in grdListSmf.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => !string.IsNullOrEmpty((dataItem.FindControl("txtNumberOfBed") as RadTextBox).Text)))
                {
                    var nob2 = new NumberOfBedSmf();
                    if (!nob2.LoadByPrimaryKey(txtStartingDate.SelectedDate.Value.Date, cboClassID.SelectedValue, dataItem["SmfID"].Text))
                    {
                        nob2 = new NumberOfBedSmf();
                        nob2.StartingDate = txtStartingDate.SelectedDate.Value.Date;
                        nob2.SmfID = dataItem["SmfID"].Text;
                        nob2.ClassID = cboClassID.SelectedValue;
                    }
                    nob2.NumberOfBed = int.Parse(string.IsNullOrEmpty((dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text) ? "0" : (dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text);
                    nob2.LastUpdateDateTime = DateTime.Now;
                    nob2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    nob2.Save();
                }

                trans.Complete();
            }

            grdList.Rebind();
            grdListSmf.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = NumberOfBeds;
        }

        protected void grdListSmf_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListSmf.DataSource = NumberOfBedSmfs;
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
                ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible = !e.Item.Expanded;
        }

        protected void grdListSmf_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
                ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible = !e.Item.Expanded;
        }

        protected void txtStartingDate_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            grdList.Rebind();
            grdListSmf.Rebind();
        }

        protected void cboClassID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
            grdListSmf.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (eventArgument)
            {
                case "new":
                    txtStartingDate.Enabled = true;
                    txtStartingDate.SelectedDate = DateTime.Now;
                    cboClassID.SelectedValue = string.Empty;
                    cboClassID.Text = string.Empty;

                    grdList.Rebind();
                    grdListSmf.Rebind();
                    break;
                case "save":
                    pnlInfo.Visible = false;
                    if (!string.IsNullOrEmpty(cboClassID.SelectedValue))
                    {
                        var i = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => !string.IsNullOrEmpty((dataItem.FindControl("txtNumberOfBed") as RadTextBox).Text)).Sum(dataItem => int.Parse(string.IsNullOrEmpty((dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text) ? "0" : (dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text));
                        var i2 = grdListSmf.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => !string.IsNullOrEmpty((dataItem.FindControl("txtNumberOfBed") as RadTextBox).Text)).Sum(dataItem => int.Parse(string.IsNullOrEmpty((dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text) ? "0" : (dataItem["NumberOfBed"].FindControl("txtNumberOfBed") as RadTextBox).Text));
                        if (i == i2)
                        {
                            btnProcess_Click(null, null);
                            txtStartingDate.Enabled = false;
                        }
                        else
                        {
                            pnlInfo.Visible = true;
                            lblInfo.Text = "There are differences in the number of beds between per Unit (" + i.ToString() + ") and per SMF (" + i2.ToString() + ").";
                            grdList.Rebind();
                            grdListSmf.Rebind();
                        }
                    }
                    else
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Class is required.";
                        grdList.Rebind();
                        grdListSmf.Rebind();
                    }
                    break;
                case "rebind":
                    grdList.Rebind();
                    grdListSmf.Rebind();
                    break;
            }
        }
    }
}
