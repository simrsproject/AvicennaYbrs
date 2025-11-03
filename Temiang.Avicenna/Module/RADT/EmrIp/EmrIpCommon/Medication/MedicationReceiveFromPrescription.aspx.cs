using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationReceiveFromPrescription : BasePageDialog
    {
        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        private string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        private DataTable Prescriptions
        {
            get
            {

                var query = new TransPrescriptionQuery("a");
                var medic = new ParamedicQuery("b");
                query.InnerJoin(medic).On(medic.ParamedicID == query.ParamedicID);

                query.Select
                    (
                        query.PrescriptionNo,
                        query.PrescriptionDate,
                        medic.ParamedicName
                    );

                query.Where
                    (
                    query.Or(query.RegistrationNo == RegistrationNo, query.RegistrationNo == FromRegistrationNo), query.IsPrescriptionReturn == false, query.IsApproval == true
                    );

                //// Yg sudah pernah diimport jangan ditampilkan
                //var received = new MedicationReceiveQuery("mr");
                //received.Select(received.RefTransactionNo);
                //received.Where(received.RefTransactionNo == query.PrescriptionNo);

                //query.Where(query.PrescriptionNo.NotIn(received));

                query.OrderBy(query.PrescriptionNo.Descending);
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
            btnReImport.Visible = Request.QueryString["reimport"] == "1";
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = Prescriptions;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["prescdt" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["prescdt" + Request.UserHostName];
        }


        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            DataTable dtb = (DataTable)ViewState["prescdt" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                double qty = ((RadNumericTextBox)dataItem.FindControl("txtQtyInput")).Value ?? 0;
                var startDateTime = ((RadDateTimePicker)dataItem.FindControl("txtStartDateTime")).SelectedDate;
                var srMedicationConsume = ((RadComboBox)dataItem.FindControl("cboSRMedicationConsume")).SelectedValue;


                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        row["QtyInput"] = qty;
                        row["StartDateTime"] = startDateTime;
                        row["SRMedicationConsume"] = srMedicationConsume;
                        break;
                    }
                }

                ViewState["prescdt" + Request.UserHostName] = dtb;
            }
        }


        #region old code InitializeDataDetail
        private void InitializeDataDetail(string prescriptionNo)
        {
            var presc = InitilaizePrescriptionItemForMedication(prescriptionNo);
            ViewState["prescdt" + Request.UserHostName] = presc;
            grdDetail.DataSource = presc;
            grdDetail.DataBind();
        }

        #endregion

        private static string FieldStringText(DataRow row, string oriFieldName, string interventionFieldName)
        {
            var interventionFormat = "<del style='color:red;'>{0}</del> {1} ";
            return (row[oriFieldName] != DBNull.Value && !string.IsNullOrEmpty(row[oriFieldName].ToString())
             && !row[interventionFieldName].Equals(row[oriFieldName]))
                ? string.Format(interventionFormat, row[oriFieldName], row[interventionFieldName])
                : row[interventionFieldName].ToString();
        }
        private static string FieldNumericText(DataRow row, string oriFieldName, string interventionFieldName, string numericFormat = "{0}")
        {
            var interventionFormat = "<del style='color:red;'>" + numericFormat + "</del> {1} ";
            return (row[oriFieldName] != DBNull.Value && row[oriFieldName].ToInt() > 0
                    && !row[interventionFieldName].Equals(row[oriFieldName]))
                ? string.Format(interventionFormat, row[oriFieldName], row[interventionFieldName])
                : row[interventionFieldName].ToString();
        }
        private static DataTable InitilaizePrescriptionItemForMedication(string prescriptionNo)
        {

            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");
            var qItemIntervention = new ItemQuery("c");
            var cons = new ConsumeMethodQuery("d");
            var consOri = new ConsumeMethodQuery("cono");
            var emb = new EmbalaceQuery("e");


            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID && qItemMedic.IsMedication == true);
            query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.LeftJoin(consOri).On(query.OriSRConsumeMethod == consOri.SRConsumeMethod);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);


            query.Select
            (
                query,
                query.TakenQty.As("QtyInput"),
                query.ResultQty.As("RefQty"),
                qItem.ItemName,
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                cons.SRConsumeMethodName,
                emb.EmbalaceLabel,
                qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention"),
                consOri.SRConsumeMethodName.Coalesce("''").As("SRConsumeMethodNameOri"),
                qItem.ItemName.As("ItemNameOri")

            );

            query.Where(query.PrescriptionNo == prescriptionNo);
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var dtbSource = query.LoadDataTable();

            // Racikan dibuat 1 row
            var presc = dtbSource.Clone();
            var sbItem = new StringBuilder();
            var interventionFormat = "<del style='color:red;'>{0}</del> {1}";
            foreach (DataRow rowSource in dtbSource.Rows)
            {
                var isItemIntervention = (rowSource["ItemInterventionID"] != DBNull.Value &&
                                          !string.IsNullOrEmpty(rowSource["ItemInterventionID"].ToString()) && !rowSource["ItemID"].Equals(rowSource["ItemInterventionID"]));
                if (isItemIntervention)
                {
                    if (rowSource["ItemInterventionID"] != DBNull.Value &&
                         !string.IsNullOrEmpty(rowSource["ItemInterventionID"].ToString()))
                    {
                        rowSource["ItemID"] = rowSource["ItemInterventionID"].ToString();
                        rowSource["ItemName"] = rowSource["ItemNameIntervention"];
                    }
                }

                var itemName = isItemIntervention
                    ? string.Format("<del style='color:red;'>{0}</del> {1} ", rowSource["ItemNameOri"], rowSource["ItemName"])
                    : rowSource["ItemName"].ToString();

                var resultQty = FieldNumericText(rowSource, "OriResultQty", "ResultQty");
                var itemUnit = FieldStringText(rowSource, "OriSRItemUnit", "SRItemUnit");
                var dosageQty = FieldNumericText(rowSource, "OriDosageQty", "DosageQty");
                var dosageUnit = FieldStringText(rowSource, "OriSRDosageUnit", "SRDosageUnit");

                if (Convert.ToBoolean(rowSource["IsCompound"]))
                {

                    // Racikan
                    if (rowSource["ParentNo"] == DBNull.Value || string.IsNullOrEmpty(rowSource["ParentNo"].ToString()))
                    {
                        //Header Racikan
                        sbItem = new StringBuilder();

                        var destRow = presc.NewRow();
                        destRow.ItemArray = rowSource.ItemArray.Clone() as object[];

                        sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} {6}<br />",
                            Convert.ToBoolean(rowSource["IsRFlag"])
                                ? "R/"
                                : "&nbsp;&nbsp;",
                            itemName, rowSource["EmbalaceQty"], rowSource["EmbalaceLabel"], dosageQty,
                            dosageUnit, rowSource["Notes"]);

                        destRow["ItemName"] = sbItem.ToString();
                        destRow["QtyInput"] = rowSource["EmbalaceQty"];
                        destRow["RefQty"] = rowSource["EmbalaceQty"];
                        presc.Rows.Add(destRow);
                    }
                    else
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5}<br />",
                            Convert.ToBoolean(rowSource["IsRFlag"])
                                ? "R/&nbsp;&nbsp;&nbsp;"
                                : "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;",
                            itemName, rowSource["EmbalaceQty"], rowSource["EmbalaceLabel"], dosageQty,
                            dosageUnit);

                        if (rowSource["SRConsumeMethodNameOri"] != DBNull.Value && !string.IsNullOrEmpty(rowSource["SRConsumeMethodNameOri"].ToString())
                              && !rowSource["SRConsumeMethodName"].Equals(rowSource["SRConsumeMethodNameOri"]))
                        {
                            sbItem.AppendFormat("<br /><del style='color:red;'>{0}</del> {1} ", rowSource["SRConsumeMethodNameOri"],
                                rowSource["SRConsumeMethodName"]);
                        }

                        presc.Rows[presc.Rows.Count - 1]["ItemName"] = sbItem.ToString();
                    }
                }
                else
                {
                    // Obat paten
                    // Conversi dari Item Unit ke ConsumeUnit (sama dg DOsage Unit) jika unitnya berbeda
                    if (!rowSource["SRItemUnit"].Equals(rowSource["SRConsumeUnit"]))
                    {
                        // Cek dosage di master
                        var med = new ItemProductMedic();
                        if (med.LoadByPrimaryKey(rowSource["ItemID"].ToString()) && med.Dosage > 0 && rowSource["SRConsumeUnit"].Equals(med.SRDosageUnit))
                        {
                            rowSource["QtyInput"] = rowSource["QtyInput"].ToDecimal() * med.Dosage;

                        }
                        else
                        {
                            // cek conversion di matrix
                            var conversion = new ItemProductConsumeUnitMatrix();
                            if (conversion.LoadByPrimaryKey(rowSource["ItemID"].ToString(), rowSource["SRItemUnit"].ToString(),
                                rowSource["SRConsumeUnit"].ToString()))
                            {
                                rowSource["QtyInput"] = rowSource["QtyInput"].ToDecimal() * conversion.ConversionFactor;
                            }
                        }
                    }

                    sbItem = new StringBuilder();
                    sbItem.AppendFormat("{0} {1} {2} {3} {4}<br />",
                        Convert.ToBoolean(rowSource["IsRFlag"])
                            ? "R/"
                            : "     ",
                        itemName, resultQty, itemUnit, rowSource["Notes"]);

                    if (rowSource["SRConsumeMethodNameOri"] != DBNull.Value && !string.IsNullOrEmpty(rowSource["SRConsumeMethodNameOri"].ToString())
                        && !rowSource["SRConsumeMethodName"].Equals(rowSource["SRConsumeMethodNameOri"]))
                    {
                        sbItem.AppendFormat("<br /><del style='color:red;'>{0}</del> {1} ", rowSource["SRConsumeMethodNameOri"],
                            rowSource["SRConsumeMethodName"]);
                    }
                    var destRow = presc.NewRow();
                    destRow.ItemArray = rowSource.ItemArray.Clone() as object[];
                    destRow["ItemName"] = sbItem.ToString();
                    if (destRow["StartDateTime"] == DBNull.Value)
                        destRow["StartDateTime"] = DateTime.Now;
                    presc.Rows.Add(destRow);
                }
            }
            presc.Columns.Add("SRMedicationConsume", typeof(string));

            return presc;
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
            var dtb = (DataTable)ViewState["prescdt" + Request.UserHostName];
            foreach (DataRow row in dtb.Rows)
            {
                if (string.IsNullOrEmpty(row["ParentNo"].ToString()) && Convert.ToDecimal(row["QtyInput"]) > 0)
                    MedicationReceive.InsertMedicationReceive(row, RegistrationNo, txtReceiveDateTime.SelectedDate.Value);
            }

            return true;
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

        protected void btnReImport_Click(object sender, EventArgs e)
        {
            var dtb = (DataTable)ViewState["prescdt" + Request.UserHostName];
            if (dtb.Rows.Count > 0)
            {
                var row = dtb.Rows[0];
                MedicationReceive.ImportFromPrescriptionBaseOnTherapy(row["PrescriptionNo"].ToString(), RegistrationNo, txtReceiveDateTime.SelectedDate.Value);
            }
        }
    }
}