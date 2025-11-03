using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class PrescriptionList : BasePageDialog
    {
        private bool IsNew {
            get {
                return Request.QueryString["mode"] == "new";
            }
        }
        private bool IsEdit
        {
            get
            {
                return Request.QueryString["mode"] == "edit";
            }
        }
        public bool IsView
        {
            get
            {
                return Request.QueryString["mode"] == "view";
            }
        }
        private string OrderNo
        {
            get
            {
                return Request.QueryString["ono"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PrescriptionReturnOrder;

            if (IsPostBack) return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);

            txtRegistrationNo.Text = reg.RegistrationNo;
            txtMedicalNo.Text = pat.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = pat.PatientName;
            txtServiceUnitID.Text = reg.BedID == string.Empty ? unit.ServiceUnitName : unit.ServiceUnitName + " [Bed No: " + reg.BedID + "]";
            txtGender.Text = pat.Sex;

            txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtAgeDay.Value = Helper.GetAgeInDay(pat.DateOfBirth.Value);
            txtAgeMonth.Value = Helper.GetAgeInMonth(pat.DateOfBirth.Value);
            txtAgeYear.Value = Helper.GetAgeInYear(pat.DateOfBirth.Value);

            grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 1].Visible = (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) > 0;

            if (IsNew)
            {
                AppAutoNumberLast _orderNo;
                _orderNo = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PrescriptionOrder);
                txtOrderNo.Text = _orderNo.LastCompleteNumber;
            }
            else
            {
                // load existing
            }
            if (IsNew || IsEdit)
            {
                (Helper.FindControlRecursive(Page, "btnOk") as Button).Attributes["onClick"] = "if (!confirm('Would you approval this transaction?')) return false;";
                (Helper.FindControlRecursive(Page, "btnOk") as Button).Text = "Approve";
            }
            else {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
            if (!IsNew) {
                var ho = new TransPrescriptionOrder();
                if (ho.LoadByPrimaryKey(OrderNo)) {
                    txtOrderNo.Text = ho.OrderNo;
                }
            }
        }

        private DataTable TransPrescriptionItems
        {
            get
            {
                var query = new TransPrescriptionItemQuery("a");
                var item1 = new ItemQuery("b");
                var item2 = new ItemQuery("c");

                var hd = new TransPrescriptionQuery("d");

                query.Select
                    (
                        hd.ApprovalDateTime,
                        hd.RegistrationNo,
                        query,
                        query.LineAmount.As("Total"),
                        "<CASE ISNULL(c.ItemName, '') WHEN '' THEN b.ItemName ELSE c.ItemName END AS ItemName>",
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        "<CAST(0 as decimal(10,2)) as OrderQty>"
                    );
                query.InnerJoin(hd).On(query.PrescriptionNo == hd.PrescriptionNo);
                query.InnerJoin(item1).On(query.ItemID == item1.ItemID);
                query.LeftJoin(item2).On(query.ItemInterventionID == item2.ItemID);
                query.Where
                    (
                        hd.IsPrescriptionReturn == false,
                        query.IsCompound == false,
                        query.IsApprove == true,
                        query.IsVoid == false
                    );
                if (AppSession.Parameter.IsAllowPrescriptionReturnForMultipleRegistration)
                    query.Where(hd.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"])));
                else
                    query.Where(hd.RegistrationNo == Request.QueryString["regno"]);

                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                DataTable tbl1 = query.LoadDataTable();

                if (tbl1.Rows.Count == 0) return tbl1;

                // update pending delivery
                //foreach(DataRow row in tbl1.Rows){
                //    if ((row["IsPendingDelivery"]) is DBNull ? false : (bool)(row["IsPendingDelivery"]))
                //    {
                //        row["ResultQty"] = (row["DeliveryQty"] is DBNull) ? 0 : ((decimal)row["DeliveryQty"]);
                //    }
                //}

                // returned
                var header = new TransPrescriptionQuery("c");
                query = new TransPrescriptionItemQuery("a");

                query.Select(query, header.ReferenceNo.As("ReferenceNoHeader"));
                query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
                query.Where
                    (
                        header.IsPrescriptionReturn == true,
                        header.IsApproval == true,
                        header.ReferenceNo.In(tbl1.AsEnumerable().Select(t => t.Field<string>("PrescriptionNo")).Distinct().ToList()),
                        query.IsApprove == true,
                        query.IsVoid == false
                    );

                DataTable tbl2 = query.LoadDataTable();

                foreach (DataRow row2 in tbl2.Rows)
                {
                    foreach (DataRow row1 in tbl1.Rows.Cast<DataRow>().Where(row1 => row1["PrescriptionNo"].Equals(row2["ReferenceNoHeader"]) && row1["SequenceNo"].Equals(row2["SequenceNo"]) && row1["ItemID"].Equals(row2["ItemID"])))
                    {
                        row1["PrescriptionQty"] = (decimal)row1["PrescriptionQty"] + (decimal)row2["PrescriptionQty"];
                        row1["TakenQty"] = (decimal)row1["TakenQty"] + (decimal)row2["TakenQty"];
                        row1["ResultQty"] = (decimal)row1["ResultQty"] + (decimal)row2["ResultQty"];
                    }
                }

                // pending order
                var dOrderColl = new TransPrescriptionOrderDetailCollection();
                var hOrder = new TransPrescriptionOrderQuery("ho");
                var dOrder = new TransPrescriptionOrderDetailQuery("do");

                dOrder.Select(dOrder);
                dOrder.InnerJoin(hOrder).On(dOrder.OrderNo == hOrder.OrderNo);

                if (IsView)
                {
                    dOrder.Where
                     (
                         //hOrder.IsApproval == true,
                         dOrder.PrescriptionNo.In(tbl1.AsEnumerable().Select(t => t.Field<string>("PrescriptionNo")).Distinct().ToList())//,
                         //hOrder.IsVoid == false,
                         //hOrder.IsClosed == false
                     );
                }
                else {
                    dOrder.Where
                       (
                           hOrder.IsApproval == true,
                           dOrder.PrescriptionNo.In(tbl1.AsEnumerable().Select(t => t.Field<string>("PrescriptionNo")).Distinct().ToList()),
                           hOrder.IsVoid == false,
                           hOrder.IsClosed == false
                       );
                }

                dOrderColl.Load(dOrder);

                foreach (var dor in dOrderColl)
                {
                    foreach (DataRow row1 in tbl1.Rows.Cast<DataRow>().Where(row1 => row1["PrescriptionNo"].Equals(dor.PrescriptionNo) && row1["SequenceNo"].Equals(dor.SequenceNo)))
                    {
                        if (dor.OrderNo == txtOrderNo.Text)
                        {
                            row1["OrderQty"] = (dor.Qty ?? 0);
                        }
                        else
                        {
                            row1["PrescriptionQty"] = (decimal)row1["PrescriptionQty"] - (dor.Qty ?? 0);
                            row1["TakenQty"] = (decimal)row1["TakenQty"] - (dor.Qty ?? 0);
                            row1["ResultQty"] = (decimal)row1["ResultQty"] - (dor.Qty ?? 0);
                        }
                    }
                }

                tbl1.AcceptChanges();

                DataView view = tbl1.DefaultView;
                if (!IsView)
                {
                    view.RowFilter = "ResultQty > 0";
                }

                return view.ToTable();
            }
        }

        protected void grdTransPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransPrescriptionItem.DataSource = TransPrescriptionItems;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdTransPrescriptionItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var reg = new Registration();
            var mb = new MergeBilling();
            if (mb.LoadByPrimaryKey(Request.QueryString["regno"]) && !string.IsNullOrEmpty(mb.FromRegistrationNo))
            {
                reg.LoadByPrimaryKey(mb.FromRegistrationNo);
            }
            else
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            if (reg.IsHoldTransactionEntry ?? false)
            {
                this.ShowInformationHeader("Registration locked.");
                return false;
            }
            if (reg.IsClosed ?? false)
            {
                this.ShowInformationHeader("Registration closed.");
                return false;
            }

            var dtAva = TransPrescriptionItems;
            // validate
            var iCount = 0;
            foreach (GridDataItem item in grdTransPrescriptionItem.Items.Cast<GridDataItem>().Where(dataItem => ((RadNumericTextBox)dataItem.FindControl("txtQty")).Value > 0))
            {
                var qty = Convert.ToDecimal((item.FindControl("txtQty") as RadNumericTextBox).Value);
                if (qty < 0)
                {
                    //s show error, entry minus is not allowed
                    this.ShowInformationHeader("Error: entry minus is not allowed!!");
                    return false;
                }
                var qtyAva = dtAva.AsEnumerable().Where(x => x["PrescriptionNo"].ToString() == item["PrescriptionNo"].Text && x["SequenceNo"].ToString() == item["SequenceNo"].Text)
                    .Sum(x => (decimal)x["ResultQty"]);
                if (qtyAva < qty)
                {
                    this.ShowInformationHeader("Error: available qty is less than return qty for item " + ((Label)item.FindControl("lblItemName")).Text);
                    return false;
                }

                iCount++;
            }
            if (iCount == 0)
            {
                this.ShowInformationHeader("Error: Nothing to be processed, please click cancel instead!");
                return false;
            }

            var dNow = (new DateTime()).NowAtSqlServer().Date;

            var header = new TransPrescriptionOrder();
            var details = new TransPrescriptionOrderDetailCollection();
            if (!IsNew)
            {
                if (!header.LoadByPrimaryKey(txtOrderNo.Text))
                {
                    this.ShowInformationHeader(AppConstant.Message.RecordNotExist);
                    return false;
                }
                details.Query.Where(details.Query.OrderNo == header.OrderNo);
                details.LoadAll();
            }
            else {
                header.AddNew();
                header.OrderDate = DateTime.Now.Date;
                header.RegistrationNo = RegistrationNo;
                header.IsVoid = false;
                header.CreateDateTime = dNow;
                header.CreateBy = AppSession.UserLogin.UserID;
            }
            header.Notes = txtNotes.Text;
            header.IsApproval = true;
            header.ApprovalDate = dNow;
            header.ApprovalBy = AppSession.UserLogin.UserID;
            header.IsClosed = false;
            header.LastUpdateDateTime = dNow;
            header.LastUpdateBy = AppSession.UserLogin.UserID;
            
            foreach (GridDataItem item in grdTransPrescriptionItem.Items.Cast<GridDataItem>().Where(dataItem => ((RadNumericTextBox)dataItem.FindControl("txtQty")).Value > 0))
            {
                var detail = details.Where(d => d.PrescriptionNo == item["PrescriptionNo"].Text &&
                d.SequenceNo == item["SequenceNo"].Text).FirstOrDefault();
                if (detail == null) {
                    // create new one
                    detail = details.AddNew();
                    detail.CreateDateTime = dNow;
                    detail.CreateBy = AppSession.UserLogin.UserID;
                }
                detail.PrescriptionNo = item["PrescriptionNo"].Text;
                detail.SequenceNo = item["SequenceNo"].Text;
                detail.Qty = Convert.ToDecimal((item.FindControl("txtQty") as RadNumericTextBox).Value);
                detail.SRItemUnit = item["SRItemUnit"].Text;
                detail.LastUpdateDateTime = dNow;
                detail.LastUpdateBy = AppSession.UserLogin.UserID;
            }

            using (var trans = new esTransactionScope())
            {
                if (IsNew)
                {
                    AppAutoNumberLast _orderNo;
                    _orderNo = Helper.GetNewAutoNumber(dNow, AppEnum.AutoNumber.PrescriptionOrder);
                    txtOrderNo.Text = _orderNo.LastCompleteNumber;
                    _orderNo.Save();

                    header.OrderNo = txtOrderNo.Text;
                }
                foreach (var d in details) {
                    d.OrderNo = header.OrderNo;
                }
                header.Save();
                details.Save();

                trans.Complete();
            }

            return true;
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }
    }
}
