using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientChargesDetail : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.MedicalRecordHistory;

            //service unit
            var unit = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");
            query.Where(
                query.SRRegistrationType.In(
                    AppConstant.RegistrationType.EmergencyPatient,
                    AppConstant.RegistrationType.InPatient,
                    AppConstant.RegistrationType.OutPatient,
                    AppConstant.RegistrationType.MedicalCheckUp
                    ),
                query.IsActive == true
                );

            query.OrderBy(unit.Query.ServiceUnitName.Ascending);
            unit.Load(query);

            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit entity in unit)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
            }

            ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
            ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = TransChargess;
        }

        protected void grdList_PreRender(object sender, System.EventArgs e)
        {
            var grid = (RadGrid)sender;
            // grid target
            GridItem[] nestedViewItems = grid.MasterTableView.GetItems(GridItemType.NestedView);
            foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
            {
                foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
                {
                    foreach (GridDataItem x in nestedView.Items)
                    {
                        System.Web.UI.WebControls.CheckBox chkPkg = x["IsPackage"].Controls[0] as System.Web.UI.WebControls.CheckBox;
                        if (!chkPkg.Checked)
                        {
                            x.Cells[0].Text = "&nbsp;";
                            //x.Cells[1].Text = "01";
                            //x.Cells[2].Text = "02";
                            //x.Cells[3].Text = "03";
                            //x.Cells[4].Text = "04";
                        }
                    }
                }
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {

            switch (e.DetailTableView.Name)
            {
                case "grdDetail":
                    {
                        TransChargesDetail(e);
                        break;
                    }
                case "grdDetailPackage":
                    {
                        gridListLoadDetailPackage(e);
                        break;
                    }
            }
        }

        private void TransChargesDetail(GridDetailTableDataBindEventArgs e)
        {
            var query = new TransChargesItemQuery("a");
            var iq = new ItemQuery("b");
            var pq = new ParamedicQuery("c");

            query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(pq).On(query.ParamedicID == pq.ParamedicID);
            query.Where
                (
                    query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString()
                 );
            query.OrderBy(query.SequenceNo.Ascending);

            query.Select
                (
                    query.TransactionNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.ChargeQuantity,
                    query.Price,
                    query.DiscountAmount,
                    query.CitoAmount,
                    query.SequenceNo,
                    query.IsVoid,
                    query.LastUpdateByUserID,
                    iq.ItemName.As("ItemName"),
                    pq.ParamedicName.As("ParamedicName"),
                    "<(a.ChargeQuantity * a.Price) - a.DiscountAmount + a.CitoAmount AS 'Total'>",
                    query.IsOrderRealization,
                    query.IsApprove,
                    query.IsVoid,
                    query.ParamedicCollectionName,
                    query.IsPackage
                );

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        private void gridListLoadDetailPackage(GridDetailTableDataBindEventArgs e)
        {
            var tc = new TransChargesQuery("a");
            var tci = new TransChargesItemQuery("b");
            var iq = new ItemQuery("c");
            var pq = new ParamedicQuery("d");

            tc.InnerJoin(tci).On(tc.TransactionNo.Equal(tci.TransactionNo));
            tc.InnerJoin(iq).On(tci.ItemID == iq.ItemID);
            tc.LeftJoin(pq).On(tci.ParamedicID == pq.ParamedicID);
            tc.Where
                (
                    tc.PackageReferenceNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString()
                );

            tc.OrderBy(tci.SequenceNo.Ascending);

            tc.Select
                (
                    tci.TransactionNo,
                    tci.ItemID,
                    tci.SRItemUnit,
                    tci.ChargeQuantity,
                    tci.Price,
                    tci.DiscountAmount,
                    tci.CitoAmount,
                    tci.SequenceNo,
                    tci.IsVoid,
                    tci.LastUpdateByUserID,
                    iq.ItemName.As("ItemName"),
                    pq.ParamedicName.As("ParamedicName"),
                    "<(b.ChargeQuantity * b.Price) - b.DiscountAmount + b.CitoAmount AS 'Total'>",
                    tci.IsOrderRealization,
                    tci.IsApprove,
                    tci.IsVoid,
                    tci.ParamedicCollectionName,
                    tci.IsPackage
                );

            e.DetailTableView.DataSource = tci.LoadDataTable();
        }

        private DataTable TransChargess
        {
            get
            {
                string patientID = Request.QueryString["patientID"].ToString();

                DataTable dtdTransCharges = (new TransChargesCollection()).TransChargesHistory(patientID, txtRegistrationNo.Text, cboServiceUnitID.SelectedValue);

                return dtdTransCharges;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == null) return;

            if (eventArgument.Contains("|"))
            {
                var args = eventArgument.Split(new char[] { '|' });

                if (sourceControl is RadGrid)
                {
                    switch (args[0])
                    {
                        case "printJoNotes":
                            {
                                var jobParameters = new PrintJobParameterCollection();
                                var jParam1 = jobParameters.AddNew();
                                jParam1.Name = "TransactionNo";
                                jParam1.ValueString = args[1];

                                AppSession.PrintJobParameters = jobParameters;
                                AppSession.PrintJobReportID = AppConstant.Report.JobOrderNotes;

                                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                                "oWnd.Show();" +
                                                "oWnd.Maximize();";
                                RadAjaxPanel1.ResponseScripts.Add(script);
                                break;
                            }
                    }
                }
            }
        }
    }
}