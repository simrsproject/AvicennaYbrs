using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationReceiveFromServiceUnit : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        private DataTable ServiceUnitTransactions
        {
            get
            {
                var query = new TransChargesQuery("a");
                var su = new ServiceUnitQuery("b");
                query.InnerJoin(su).On(su.ServiceUnitID == query.FromServiceUnitID);

                query.Select
                    (
                        query.TransactionNo,
                        query.TransactionDate,
                        su.ServiceUnitID
                    );

                query.Where
                    (
                        query.RegistrationNo == RegistrationNo, query.IsApproved == true
                    );

                // Yg sudah pernah diimport jangan ditampilkan
                var received = new MedicationReceiveQuery("mr");
                received.Select(received.RefTransactionNo);
                received.Where(received.RefTransactionNo == query.TransactionNo);

                query.Where(query.TransactionNo.NotIn(received));

                query.OrderBy(query.TransactionNo.Descending);
                var dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var timeNow = (new DateTime()).NowAtSqlServer();
                txtReceiveDateTime.SelectedDate = timeNow.Date;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = ServiceUnitTransactions;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["transdt" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["transdt" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            DataTable dtb = (DataTable)ViewState["transdt" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                double receiveQty = ((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0;
                var startDateTime = ((RadDatePicker)dataItem.FindControl("txtStartDateTime")).SelectedDate;
                var consumeMethod = ((RadComboBox)dataItem.FindControl("cboSRConsumeMethod")).SelectedValue;
                var consumeUnit = ((RadComboBox)dataItem.FindControl("cboSRConsumeUnit")).SelectedValue;
                double consumeQty = ((RadNumericTextBox)dataItem.FindControl("txtConsumeQty")).Value ?? 0;
                var srMedicationConsume = ((RadComboBox)dataItem.FindControl("cboSRMedicationConsume")).SelectedValue;

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        row["QtyInput"] = receiveQty;
                        row["StartDateTime"] = startDateTime;
                        row["SRConsumeMethod"] = consumeMethod;
                        row["SRConsumeUnit"] = consumeUnit;
                        row["ConsumeQty"] = consumeQty;
                        row["SRMedicationConsume"] = srMedicationConsume;
                        break;
                    }
                }

                ViewState["transdt" + Request.UserHostName] = dtb;
            }
        }
        private void InitializeDataDetail(string transactionNo)
        {
            var query = new TransChargesItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");

            query.Select
            (
                query,
                query.ChargeQuantity.As("QtyInput"),
                "<'' as SRConsumeMethod>",
                "<0 as ConsumeQty>",
                "<'' as SRConsumeUnit>",
                "<GETDATE() as StartDateTime>",
                qItem.ItemName
            );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID && qItemMedic.IsMedication == true);
            query.Where(query.TransactionNo == transactionNo);

            var dtb = query.LoadDataTable();
            dtb.Columns.Add("SRMedicationConsume", typeof(string));

            ViewState["transdt" + Request.UserHostName] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string prescNo = pars[0].Split(':')[1];
                    InitializeDataDetail(prescNo);
                    break;
            }
        }
        public override bool OnButtonOkClicked()
        {
            UpdateDataSourceDetail();
            var dtb = (DataTable)ViewState["transdt" + Request.UserHostName];
            foreach (DataRow row in dtb.Rows)
            {
                if (string.IsNullOrEmpty(row["ParentNo"].ToString()) && Convert.ToDecimal(row["QtyInput"]) > 0)
                    Save(row);
            }

            return true;
        }

        private void Save(DataRow row)
        {
            var transNo = row["TransactionNo"].ToString();
            var seqNo = row["SequenceNo"].ToString();

            // Check has register
            var qr = new MedicationReceiveQuery("q");
            qr.Where(qr.RefTransactionNo == transNo, qr.RefSequenceNo == seqNo);
            qr.es.Top = 1;
            var ent = new MedicationReceive();
            if (ent.Load(qr))
                return;


            throw new Exception("UNDER CONSTRUCTION, Rubah MedicationReceive menjadi per Therapy");
            return;
            // TODO: MedicationReceive dirubah menjadi per Therapy
            
            ent = new MedicationReceive();

            ent.RegistrationNo = RegistrationNo;
            ent.MedicationReceiveNo = NewMedicationReceiveNo();
            ent.RefTransactionNo = transNo;
            ent.BalanceQty = Convert.ToDecimal(row["QtyInput"]);
            ent.StartDateTime = Convert.ToDateTime(row["StartDateTime"]);
            ent.ReceiveDateTime = txtReceiveDateTime.SelectedDate;

            if (row["ItemInterventionID"] != DBNull.Value &&
                !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
            {
                ent.ItemID = row["ItemInterventionID"].ToString();
                ent.ItemDescription = row["ItemInterventionName"].ToString();
            }
            else
            {
                ent.ItemID = row["ItemID"].ToString();
                ent.ItemDescription = row["ItemName"].ToString();
            }

            ent.ReceiveQty = Convert.ToDecimal(row["QtyInput"]);
            ent.ConsumeQty = Convert.ToDecimal(row["ConsumeQty"]);
            ent.ConsumeQtyInString = Convert.ToString(row["ConsumeQty"]);
            ent.SRConsumeUnit = row["SRConsumeUnit"].ToString();
            ent.SRConsumeMethod = row["SRConsumeMethod"].ToString();
            ent.SRMedicationConsume = row["SRMedicationConsume"].ToString();
            ent.IsVoid = false;
            ent.IsContinue = true;
            ent.Save();

        }
        private int NewMedicationReceiveNo()
        {
            var qr = new MedicationReceiveQuery("a");
            var fb = new MedicationReceive();
            qr.es.Top = 1;
            qr.OrderBy(qr.MedicationReceiveNo.Descending);

            if (fb.Load(qr))
            {
                return fb.MedicationReceiveNo.ToInt() + 1;
            }
            return 1;
        }
        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
            InitializeDataDetail(string.Empty);
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            return "&nbsp;&nbsp;&nbsp;" + itemName;
        }


        protected void grdDetail_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                var cbo = (RadComboBox)e.Item.FindControl("cboSRMedicationConsume");
                StandardReference.InitializeIncludeSpace(cbo, AppEnum.StandardReference.MedicationConsume);
                ComboBox.SelectedValue(cbo, dataItem["SRMedicationConsume"].Text);
            }
        }
    }
}