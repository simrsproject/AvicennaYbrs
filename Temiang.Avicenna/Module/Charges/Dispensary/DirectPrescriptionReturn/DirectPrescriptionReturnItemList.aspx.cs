using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class DirectPrescriptionReturnItemList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.DirectPrescriptionReturn;

            if (!IsPostBack)
            {
                ViewState["TransactionNo" + Request.UserHostName] = string.Empty;
                txtPrescriptionDate.SelectedDate = DateTime.Now.Date;
            }
        }

        private DataTable TransPrescriptionItems
        {
            get
            {
                var query = new TransPrescriptionItemQuery("a");
                var item1 = new ItemQuery("b");
                var item2 = new ItemQuery("c");
                var header = new TransPrescriptionQuery("d");

                query.Select
                    (
                        query,
                        "<((a.ResultQty * a.Price) - a.DiscountAmount) + a.EmbalaceAmount + a.SweetenerAmount AS Total>",
                        "<CASE WHEN c.ItemName IS NOT NULL THEN c.ItemName ELSE b.ItemName END AS ItemName>",
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>"
                    );
                query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
                query.InnerJoin(item1).On(query.ItemID == item1.ItemID);
                query.LeftJoin(item2).On(query.ItemInterventionID == item2.ItemID);
                query.Where
                    (
                        header.PrescriptionNo == ViewState["TransactionNo" + Request.UserHostName].ToString(),
                        query.IsCompound == false,
                        query.IsApprove == true
                    );
                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                DataTable tbl1 = query.LoadDataTable();

                // update pending delivery
                //foreach (DataRow row in tbl1.Rows)
                //{
                //    if ((row["IsPendingDelivery"]) is DBNull ? false : (bool)(row["IsPendingDelivery"]))
                //    {
                //        row["ResultQty"] = (row["DeliveryQty"] is DBNull) ? 0 : ((decimal)row["DeliveryQty"]);
                //    }
                //}

                header = new TransPrescriptionQuery("d");
                query = new TransPrescriptionItemQuery("a");

                query.Select(query);
                query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
                query.Where
                    (
                        header.ReferenceNo == ViewState["TransactionNo" + Request.UserHostName].ToString(),
                        header.IsPrescriptionReturn == true,
                        header.IsApproval == true,
                        query.IsApprove == true
                    );

                DataTable tbl2 = query.LoadDataTable();

                foreach (DataRow row2 in tbl2.Rows)
                {
                    foreach (DataRow row1 in tbl1.Rows.Cast<DataRow>().Where(row1 => row1["SequenceNo"].Equals(row2["SequenceNo"]) &&
                                                                                     row1["ItemID"].Equals(row2["ItemID"])))
                    {
                        row1["PrescriptionQty"] = (decimal)row1["PrescriptionQty"] + (decimal)row2["PrescriptionQty"];
                        row1["TakenQty"] = (decimal)row1["TakenQty"] + (decimal)row2["TakenQty"];
                        row1["ResultQty"] = (decimal)row1["ResultQty"] + (decimal)row2["ResultQty"];
                    }
                }

                DataView view = tbl1.DefaultView;
                view.RowFilter = "ResultQty > 0";
                tbl1 = view.ToTable();
                view.Dispose();

                tbl1.AcceptChanges();

                return tbl1;
            }
        }

        protected void grdTransPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransPrescriptionItem.DataSource = TransPrescriptionItems;
        }

        protected void grdTransPrescriptionItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                var chk = (CheckBox)dataItem.FindControl("chkIsUsingAdmin");
                if (chk != null)
                {
                    chk.Checked = (AppSession.Parameter.IsPrescriptionReturnAdminChecked);
                }
            }
        }

        private DataTable TransPrescription
        {
            get
            {
                var presc = new TransPrescriptionQuery("a");
                var unit = new ServiceUnitQuery("b");
                var pat = new PatientQuery("c");
                var reg = new RegistrationQuery("d");
                var tp = new TransPaymentQuery("e");
                var tpi = new TransPaymentItemQuery("f");
                var guar = new GuarantorQuery("g");

                presc.Select
                    (
                        reg.RegistrationNo,
                        unit.ServiceUnitName,
                        pat.MedicalNo,
                        pat.PatientName,
                        presc.PrescriptionNo,
                        presc.PrescriptionDate,
                        reg.IsClosed,
                        guar.GuarantorName
                    );
                presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);
                presc.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                presc.LeftJoin(pat).On(reg.PatientID == pat.PatientID);
                presc.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);

                if (txtPrescriptionDate.SelectedDate != null)
                    presc.Where(presc.PrescriptionDate == txtPrescriptionDate.SelectedDate);
                if (txtPrescriptionNo.Text != string.Empty)
                    presc.Where(presc.PrescriptionNo == txtPrescriptionNo.Text);
                if (txtRegistrationNo.Text != string.Empty)
                    presc.Where(reg.RegistrationNo == txtRegistrationNo.Text);
                if (txtPatientName.Text != string.Empty)
                {
                    string searchTextContain = string.Format("%{0}%", txtPatientName.Text);
                    presc.Where
                        (
                            presc.Or
                                (
                                    pat.MedicalNo == txtPatientName.Text,
                                    pat.OldMedicalNo == txtPatientName.Text,
                                    pat.FirstName.Like(searchTextContain),
                                    pat.MiddleName.Like(searchTextContain),
                                    pat.LastName.Like(searchTextContain)
                                )
                        );
                }

                presc.Where
                    (
                        reg.IsClosed == true,
                        presc.IsApproval == true,
                        presc.IsPrescriptionReturn == false,
                        reg.RegistrationNo.NotIn(tp.Select(tp.RegistrationNo)
                                                   .InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo)
                                                   .Where(
                                                        tp.RegistrationNo == reg.RegistrationNo,
                                                        tp.IsApproved == true,
                                                        tpi.SRPaymentType == AppSession.Parameter.PaymentTypePersonalAR)
                                                    )

                    );
                presc.OrderBy(presc.PrescriptionDate.Ascending);

                return presc.LoadDataTable();
            }
        }

        protected void grdTransPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdTransPrescription.DataSource = TransPrescription;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdTransPrescriptionItem":
                    ViewState["TransactionNo" + Request.UserHostName] = eventArgument;
                    grdTransPrescriptionItem.Rebind();
                    break;
            }
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
            return "oWnd.argument.refno = '" + grdTransPrescription.SelectedValue + "'";
        }

        public override bool OnButtonOkClicked()
        {
            var presc = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName];
            presc.MarkAllAsDeleted();
            presc.AcceptChanges();

            foreach (GridDataItem dataItem in grdTransPrescriptionItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    if (((RadNumericTextBox)dataItem.FindControl("txtQty")).Value == 0)
                    {
                        ShowInformationHeader("Qty Return for item: " + ((Label)dataItem.FindControl("lblItemName")).Text + " is zero (0). Return Proceed is failed.");
                        return false;
                    }

                    var entity = new TransPrescriptionItem();
                    entity.LoadByPrimaryKey(dataItem["PrescriptionNo"].Text, dataItem["SequenceNo"].Text);
                    entity.MarkAllColumnsAsDirty(DataRowState.Added);

                    decimal qty = 0 - Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value);

                    entity.PrescriptionQty = qty;
                    entity.TakenQty = qty;
                    entity.DiscountAmount = (Math.Abs(qty) / entity.ResultQty) * entity.DiscountAmount;

                    if (AppSession.Parameter.PrescriptionReturnRecipeAmountReturned == "No")
                        entity.RecipeAmount = 0;

                    if (Math.Abs(qty) < entity.ResultQty)
                        entity.RecipeAmount = 0;

                    var isUsingAdmin = ((CheckBox)dataItem.FindControl("chkIsUsingAdmin")).Checked;
                    if (isUsingAdmin)
                    {
                        entity.Price = entity.Price - (((AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100) * entity.Price);
                        entity.IsUsingAdminReturn = (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) > 0;
                    }

                    if (Math.Abs(qty) < entity.ResultQty) entity.RecipeAmount = 0;

                    entity.ResultQty = qty;

                    var lineAmt = (((Math.Abs(entity.ResultQty ?? 0) * entity.Price) - entity.DiscountAmount) + entity.RecipeAmount);
                    entity.LineAmount = 0 - (Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription));

                    entity.IsApprove = false;
                    entity.IsVoid = false;
                    entity.IsBillProceed = false;

                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    entity.CreatedByUserID = AppSession.UserLogin.UserID;

                    presc.AttachEntity(entity);
                }
            }

            return true;
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["TransactionNo" + Request.UserHostName] = string.Empty;
            grdTransPrescription.Rebind();
            grdTransPrescriptionItem.Rebind();
        }
    }
}
