using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class LabResultAnalysisList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LabTestResultAnalysis;

            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.ClusterPatient,
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

                txtOrderDate1.SelectedDate = DateTime.Today;
                txtOrderDate2.SelectedDate = DateTime.Today;
            }
        }
        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            {
                ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible = !e.Item.Expanded;
            }
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            var gridNestedViewItem = e.Item as GridNestedViewItem;
            if (gridNestedViewItem != null)
            {
                e.Item.FindControl("InnerContainer").Visible = (gridNestedViewItem).ParentItem.Expanded;
            }
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TransCharges;
        }

        private DataTable TransCharges
        {
            get
            {
                var query = new TransChargesItemQuery("a");
                var tcq = new TransChargesQuery("aa");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var fromUnit = new ServiceUnitQuery("e");
                var room = new ServiceRoomQuery("f");
                var item = new ItemQuery("g");
                var guar = new GuarantorQuery("h");
                var sumInfo = new RegistrationInfoSumaryQuery("sum");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        query.TransactionNo,
                        query.ReferenceNo,
                        tcq.TransactionDate,
                        tcq.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        fromUnit.ServiceUnitName.As("ClusterName"),
                        room.RoomName,
                        tcq.BedID,
                        @"<CAST(CONVERT(VARCHAR(10), aa.TransactionDate, 101) AS DATETIME) AS [Group]>",
                        @"<'' AS [PaymentNo]>",
                        @"<CONVERT(VARCHAR(5), aa.TransactionDate, 114) AS [TransactionTime]>",
                        tcq.ToServiceUnitID.As("ServiceUnitID"),
                        reg.PatientID,
                        guar.GuarantorName,
                        sumInfo.NoteCount,
                        @"<CAST(CONVERT(VARCHAR(10), aa.ExecutionDate, 101) AS DATETIME) AS [ExecutionDate]>",
                        "<case when (select count(*) from TransChargesItem x where x.TransactionNo = a.TransactionNo and x.IsOrderRealization = 0 and x.IsVoid = 0 and x.IsCito = 1) > 0 then 1 else 0 end as IsCitoAvailable>",
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(tcq).On(query.TransactionNo == tcq.TransactionNo);
                query.InnerJoin(reg).On(tcq.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                query.InnerJoin(fromUnit).On(tcq.FromServiceUnitID == fromUnit.ServiceUnitID);
                query.LeftJoin(room).On(tcq.RoomID == room.RoomID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
                query.LeftJoin(sumInfo).On(tcq.RegistrationNo == sumInfo.RegistrationNo & sumInfo.NoteCount > 0);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                query.Where(tcq.ToServiceUnitID ==
                            AppSession.Parameter.ServiceUnitLaboratoryID); // Lab Order

                if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                    query.Where(tcq.TransactionDate.Between(txtOrderDate1.SelectedDate, txtOrderDate2.SelectedDate.Value.AddDays(1).AddMinutes(-1)));
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(tcq.ToServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    query.Where(
                        query.Or(
                            reg.RegistrationNo == searchReg,
                            patient.MedicalNo == searchReg,
                            patient.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }

                if (txtTransactionNo.Text != string.Empty)
                {
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtExecutionDate.IsEmpty)
                    query.Where(tcq.ExecutionDate.Date() == txtExecutionDate.SelectedDate.Value.Date);

                query.Where
                    (
                        query.IsVoid == false,
                        tcq.IsOrder == true,
                        tcq.IsApproved == true,
                        query.Or(query.IsSelectedExtraItem == true, query.IsSelectedExtraItem.IsNull())
                    );
                query.Where(query.ParentNo == string.Empty);

                query.es.Distinct = true;

                query.OrderBy(tcq.TransactionDate.Descending);

                var tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "rebind")
                grdList.Rebind();
        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }

        protected string LabResult(string transactionNo)
        {
            var query = new TransChargesItemQuery("a");
            var tcq = new TransChargesQuery("b");
            var item = new ItemQuery("c");
            var reg = new RegistrationQuery("f");
            var unit = new ServiceUnitQuery("x");

            query.Select(
                query.TransactionNo,
                query.SequenceNo,
                tcq.TransactionDate,
                query.ItemID,
                item.ItemName,
                query.ChargeQuantity,
                query.Price,
                query.SRItemUnit,
                query.IsApprove,
                query.IsOrderRealization,
                query.IsVoid,
                query.IsBillProceed,
                unit.ServiceUnitName,
                unit.ServiceUnitID,
                tcq.PhysicianSenders,
                tcq.IsApproved.As("IsHdApproved"),
                tcq.IsVoid.As("IsHdVoid"),
                reg.PatientID, reg.RegistrationNo
                );

            query.InnerJoin(tcq).On(query.TransactionNo == tcq.TransactionNo);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(reg).On(tcq.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(unit).On(tcq.ToServiceUnitID == unit.ServiceUnitID);


            query.Where(
                query.TransactionNo == transactionNo,
                tcq.IsOrder == true,
                tcq.IsVoid == false
                );


            query.OrderBy(
                tcq.TransactionNo.Descending
                );


            var table = query.LoadDataTable();

            int i = 0;
            double total = 0;
            var orderContent = new StringBuilder();
            //orderContent.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'><tr><td align='left'>");
            var menuEditRadilogyResult = string.Empty;
            foreach (DataRow orderItem in table.Rows)
            {
                //if (i == 0)
                //{
                //    var menuView =
                //        string.Format(
                //            "<a href=\"#\" onclick=\"javascript:examOrderResult('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../Images/Toolbar/views16.png\"  alt=\"View\" /></a>",
                //            orderItem["PatientID"], orderItem["RegistrationNo"], orderItem["ServiceUnitID"], orderItem["TransactionNo"]);
                //    orderContent.AppendLine("<tr><td style='font-weight: bold'>");
                //    orderContent.AppendFormat("{0} - {1} - {2} {3}<br />", orderItem["TransactionNo"], Convert.ToDateTime(orderItem["TransactionDate"]).ToShortDateString(), orderItem["ServiceUnitName"], menuView);
                //    orderContent.AppendLine("</td></tr><tr><td align='left' style='font-style: italic'>");



                //}
                //i++;

                orderContent.AppendFormat("{0} {1} {2} <img src='../../../Images/Toolbar/{3}' />&nbsp;&nbsp;{4}<br />", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                    (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), menuEditRadilogyResult);

            }


            //orderContent.Append("</td></tr></table>");
            orderContent.Append("<br />");
            return orderContent.ToString();
        }

    }
}
