using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeVerificationByDischargeDateList : BasePage
    {
        private AppAutoNumberLast _autoNumber;

        public bool IsUnapprovable() {
            UserAccess access = this.UserAccess;
            if (access.IsExist)
            {
                return access.IsUnApprovalAble;
            }
            return false;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicFeeVerification;

            if (!IsPostBack)
            {
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdListV.Rebind();
        }

        private DataTable ParamedicFeeTransChargesItemCompByDischargeDateNoMergeBillingWithCorrection()
        {
            var dtb = new DataTable();
           
            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var txhQRef = new TransChargesQuery("b");
            var tc = new TransChargesQuery("tc");
            var regQRef = new RegistrationQuery("c");
            var patQ = new PatientQuery("d");
            var iQ = new ItemQuery("e");
            var guarQ = new GuarantorQuery("f");
            var parQ = new ParamedicQuery("par");
            var tci = new TransChargesItemQuery("tci");

            query.Select(
                query.VerificationNo,
                query.TransactionNo,
                query.SequenceNo,
                query.TariffComponentID,
                query.FeeAmount,
                query.DischargeDate,
                query.ItemID,
                iQ.ItemName,
                query.Qty,
                txhQRef.RegistrationNo,
                tc.ExecutionDate,//txhQRef.ExecutionDate,
                patQ.MedicalNo,
                patQ.PatientName,
                @"<a.TransactionNo + '_' + a.SequenceNo + '_' + a.TariffComponentID AS 'KeyField'>",
                query.LastCalculatedDateTime,
                query.LastCalculatedByUserID,
                guarQ.GuarantorName,
                "<'' AS PaymentMethod>",
                @"<CAST(1 AS BIT) AS 'IsPaidOff'>",
                query.ParamedicID,
                parQ.ParamedicName,
                query.DeductionAmount,
                query.Price,
                query.CalculatedAmount,
                query.Discount,
                query.PriceItem,
                query.DiscountItem,
                query.IsModified,
	            tci.ReferenceNo,tci.ReferenceSequenceNo,
                "<CAST((case WHEN tci.ReferenceNo = '' THEN 0 ELSE 1 END) as bit) Corrected>"
                );
            query.InnerJoin(txhQRef).On(query.TransactionNoRef == txhQRef.TransactionNo);
            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
            query.InnerJoin(regQRef).On(txhQRef.RegistrationNo == regQRef.RegistrationNo);
            query.InnerJoin(patQ).On(regQRef.PatientID == patQ.PatientID);
            query.InnerJoin(iQ).On(query.ItemID == iQ.ItemID);
            query.InnerJoin(guarQ).On(regQRef.GuarantorID == guarQ.GuarantorID);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.InnerJoin(tci).On(query.TransactionNo == tci.TransactionNo && query.SequenceNo == tci.SequenceNo);
            query.Where(
                query.LastCalculatedDateTime.IsNotNull(),
                query.VerificationNo.IsNull(),
                query.IsFree == false
            );

            if (txtPeriodDate.SelectedDate.HasValue && txtPeriodDate2.SelectedDate.HasValue) {
                    query.Where(query.DischargeDate >= txtPeriodDate.SelectedDate);
                    query.Where(query.DischargeDate <= txtPeriodDate2.SelectedDate);
            }

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                query.Where(txhQRef.RegistrationNo == txtRegistrationNo.Text);
            }

            if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue)) {
                query.Where(txhQRef.ToServiceUnitID == cboServiceUnit.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            {
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtNamaLayanan.Text))
            {
                query.Where(iQ.ItemName.Like("%" + txtNamaLayanan.Text + "%"));
            }

            List<string> ArrType = new List<string>();
            if (cblRegType.Items[0].Selected) ArrType.Add(cblRegType.Items[0].Value);
            if (cblRegType.Items[1].Selected) ArrType.Add(cblRegType.Items[1].Value);
            if (cblRegType.Items[1].Selected) ArrType.Add("EMR");
            if (cblRegType.Items[1].Selected) ArrType.Add("MCU");

            if (ArrType.Count == 0) ArrType.Add("-");
            if (ArrType.Count > 0) {
                //var su = new ServiceUnitQuery("su");
                //query.InnerJoin(su).On(txhQ.ToServiceUnitID == su.ServiceUnitID)
                //    .Where(su.SRRegistrationType.In(ArrType.ToArray()));
                query.Where(regQRef.SRRegistrationType.In(ArrType.ToArray()));
            }

            query.OrderBy(
                query.IsModified.Descending,
                query.DischargeDate.Ascending,
                txhQRef.RegistrationNo.Ascending,
                query.ParamedicID.Ascending,
                iQ.ItemName.Ascending,
                "<case tci.ReferenceNo WHEN '' THEN tci.TransactionNo ELSE tci.ReferenceNo END>",
                "<case tci.ReferenceSequenceNo WHEN '' THEN tci.SequenceNo ELSE tci.ReferenceSequenceNo END>",
                query.TariffComponentID.Ascending
            );

            dtb = query.LoadDataTable();

            var corrected = from d in dtb.AsEnumerable() where d.Field<string>("ReferenceNo") != string.Empty select d;

            foreach (DataRow d in dtb.Rows) {
                if (((bool)d["Corrected"])) continue;
                var x = from c in corrected
                        where c.Field<string>("ReferenceNo") == d.Field<string>("TransactionNo")
                            && c.Field<string>("ReferenceSequenceNo") == d.Field<string>("SequenceNo")
                        select c;
                d["Corrected"] = (x.Count() > 0);
            }

            foreach (DataRow d in dtb.Rows)
            {
                var table = new ParamedicFeeTransChargesItemCompByDischargeDateCollection().GetPaymentType(d["TransactionNo"].ToString(), d["SequenceNo"].ToString());
                if (table.AsEnumerable().Any())
                {
                    var payment = table.AsEnumerable().Aggregate(string.Empty, (current, t) => current + (t["PaymentMethodName"].ToString() + ", "));
                    if (!string.IsNullOrEmpty(payment))
                        d["PaymentMethod"] = payment;
                }
                if (d["PaymentMethod"].ToString() == "")
                    d["IsPaidOff"] = false;
            }

            dtb.AcceptChanges();

            return dtb;
        }

        private DataTable ParamedicFeeTransChargesItemCompByDischargeDateMergeBilling()
        {
            var dtb = new DataTable();

            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var txhQ = new TransChargesQuery("b");
            var tc = new TransChargesQuery("tc");
            var regQ = new RegistrationQuery("c");
            var patQ = new PatientQuery("d");
            var iQ = new ItemQuery("e");
            var guarQ = new GuarantorQuery("f");
            var parQ = new ParamedicQuery("par");
            var tci = new TransChargesItemQuery("tci");
            var regIndxQ = new RegistrationQuery("rig");
            var cls = new ClassQuery("cls");

            query.Select(
                query.VerificationNo,
                query.TransactionNo,
                query.SequenceNo,
                query.TariffComponentID,
                query.FeeAmount,
                query.DischargeDate,
                query.ItemID,
                query.IsCalculatedInPercent,
                iQ.ItemName,
                query.Qty,
                txhQ.RegistrationNo,
                tc.ExecutionDate,//txhQRef.ExecutionDate,
                patQ.MedicalNo,
                patQ.PatientName,
                @"<a.TransactionNo + '_' + a.SequenceNo + '_' + a.TariffComponentID AS 'KeyField'>",
                query.LastCalculatedDateTime,
                query.LastCalculatedByUserID,
                guarQ.GuarantorName,
                "<'' AS PaymentMethod>",
                @"<CAST(1 AS BIT) AS 'IsPaidOff'>",
                query.ParamedicID,
                parQ.ParamedicName,
                query.DeductionAmount,
                query.Price,
                query.CalculatedAmount,
                query.Discount,
                query.PriceItem,
                query.DiscountItem,
                query.IsModified,
                tci.ReferenceNo, tci.ReferenceSequenceNo,
                "<CAST((case WHEN tci.ReferenceNo = '' THEN 0 ELSE 1 END) as bit) Corrected>",
                cls.ClassName,
                "<ISNULL(a.Notes,'') Notes>",
                query.PaymentMethodName
                );
            query.InnerJoin(txhQ).On(query.TransactionNo == txhQ.TransactionNo);
            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
            query.InnerJoin(regQ).On(txhQ.RegistrationNo == regQ.RegistrationNo);
            query.InnerJoin(patQ).On(regQ.PatientID == patQ.PatientID);
            query.InnerJoin(iQ).On(query.ItemID == iQ.ItemID);
            query.InnerJoin(guarQ).On(regQ.GuarantorID == guarQ.GuarantorID);
            query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);
            query.InnerJoin(tci).On(query.TransactionNo == tci.TransactionNo && query.SequenceNo == tci.SequenceNo);
            query.LeftJoin(cls).On(tci.ChargeClassID == cls.ClassID);
            query.InnerJoin(regIndxQ).On(query.RegistrationNoMergeTo == regIndxQ.RegistrationNo);
            query.Where(
                query.LastCalculatedDateTime.IsNotNull(),
                query.VerificationNo.IsNull(),
                query.IsFree == false,
                iQ.SRItemType != "11" /*exlude bug emc obat alkes sering masuk jasmed*/
            );

            if (txtPeriodDate.SelectedDate.HasValue && txtPeriodDate2.SelectedDate.HasValue)
            {
                query.Where(query.DischargeDateMergeTo >= txtPeriodDate.SelectedDate);
                query.Where(query.DischargeDateMergeTo <= txtPeriodDate2.SelectedDate);
            }

            if (txtTransactionDate1.SelectedDate.HasValue && txtTransactionDate2.SelectedDate.HasValue)
            {
                query.Where(tc.TransactionDate.Date() >= txtTransactionDate1.SelectedDate);
                query.Where(tc.TransactionDate.Date() <= txtTransactionDate2.SelectedDate);
            }

            if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                query.Where(
                    query.Or(
                        query.RegistrationNoMergeTo == txtRegistrationNo.Text,
                        query.RegistrationNo == txtRegistrationNo.Text
                    )
                );
            }

            if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
            {
                query.Where(txhQ.ToServiceUnitID == cboServiceUnit.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            {
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtNamaLayanan.Text))
            {
                query.Where(iQ.ItemName.Like("%" + txtNamaLayanan.Text + "%"));
            }

            List<string> ArrType = new List<string>();
            if (cblRegType.Items[0].Selected) ArrType.Add(cblRegType.Items[0].Value);
            if (cblRegType.Items[1].Selected) ArrType.Add(cblRegType.Items[1].Value);
            if (cblRegType.Items[1].Selected) ArrType.Add("EMR");
            if (cblRegType.Items[1].Selected) ArrType.Add("MCU");

            if (ArrType.Count == 0) ArrType.Add("-");
            if (ArrType.Count > 0)
            {
                //var su = new ServiceUnitQuery("su");
                //query.InnerJoin(su).On(txhQ.ToServiceUnitID == su.ServiceUnitID)
                //    .Where(su.SRRegistrationType.In(ArrType.ToArray()));
                query.Where(regIndxQ.SRRegistrationType.In(ArrType.ToArray()));
            }

            query.OrderBy(
                query.IsModified.Descending,
                query.DischargeDateMergeTo.Ascending,
                query.RegistrationNoMergeTo.Ascending,
                query.ParamedicID.Ascending,
                iQ.ItemName.Ascending,
                "<case tci.ReferenceNo WHEN '' THEN tci.TransactionNo ELSE tci.ReferenceNo END>",
                "<case tci.ReferenceSequenceNo WHEN '' THEN tci.SequenceNo ELSE tci.ReferenceSequenceNo END>",
                query.TariffComponentID.Ascending
            );

            dtb = query.LoadDataTable();

            var corrected = from d in dtb.AsEnumerable() where d.Field<string>("ReferenceNo") != string.Empty select d;

            foreach (DataRow d in dtb.Rows)
            {
                if (((bool)d["Corrected"])) continue;
                var x = from c in corrected
                        where c.Field<string>("ReferenceNo") == d.Field<string>("TransactionNo")
                            && c.Field<string>("ReferenceSequenceNo") == d.Field<string>("SequenceNo")
                        select c;
                d["Corrected"] = (x.Count() > 0);
            }

            //foreach (DataRow d in dtb.Rows)
            //{
            //    var table = new ParamedicFeeTransChargesItemCompByDischargeDateCollection().GetPaymentType(d["TransactionNo"].ToString(), d["SequenceNo"].ToString());
            //    if (table.AsEnumerable().Any())
            //    {
            //        var payment = table.AsEnumerable().Aggregate(string.Empty, (current, t) => current + (t["PaymentMethodName"].ToString() + ", "));
            //        if (!string.IsNullOrEmpty(payment))
            //            d["PaymentMethod"] = payment;
            //    }
            //    if (d["PaymentMethod"].ToString() == "")
            //        d["IsPaidOff"] = false;
            //}

            dtb.AcceptChanges();

            return dtb;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                CheckBox chkCorrected = item["Corrected"].Controls[0] as CheckBox;
                if (chkCorrected.Checked)
                {
                    item.ForeColor = Color.LimeGreen;
                    item.Font.Bold = true;
                    var chkd = item.FindControl("detailChkbox") as CheckBox;
                    if (chkd != null)
                    {
                        chkd.Checked = true;
                    }
                }

                CheckBox chk = item["IsModified"].Controls[0] as CheckBox;
                if (chk.Checked)
                {
                    item.ForeColor = Color.Red;
                    item.Font.Bold = true;
                    //celltoVerify1.Font.Bold = true;
                    //celltoVerify1.BackColor = Color.Yellow;
                }

                // test dynamic formating for share
                //Check the formatting condition
                if (item["IsCalculatedInPercent"].Text == true.ToString())
                {
                    item["CalculatedAmount"].Text = item["CalculatedAmount"].Text + "%";
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (txtPeriodDate.SelectedDate.HasValue && txtPeriodDate2.SelectedDate.HasValue)
            {
                //
            } else if (string.IsNullOrEmpty(txtRegistrationNo.Text.Trim()))
            {
                return;
            }

            grdList.DataSource = ParamedicFeeTransChargesItemCompByDischargeDateMergeBilling();
        }

        protected void grdListV_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListV.DataSource = ParamedicFeeVerifications;
        }

        private DataTable ParamedicFeeVerifications
        {
            get
            {
                var query = new ParamedicFeeVerificationQuery("a");
                var parQ = new ParamedicQuery("b");


                query.InnerJoin(parQ).On(query.ParamedicID == parQ.ParamedicID);

                query.Select(
                       "<'../ParamedicFeeVerificationByDischargeDate/ParamedicFeeVerificationByDischargeDateDetail.aspx?md=view&id=' + a.VerificationNo + '&type=4' as VerUrl>",
                       query.VerificationNo,
                       query.VerificationDate,
                       query.ApprovedDate,
                       parQ.ParamedicName,
                       query.VerificationAmount,
                       query.TaxAmount,
                       query.IsApproved,
                       "<LEN(a.VerificationNo) pjng>"
                   );
                //query.Where(query.Or(query.RegistrationNo == txtRegistrationNo.Text,
                //                     query.PaymentNo == txtPaymentNo.Text));

                //if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                //{
                //    query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                //}
                //if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                //{
                //    query.Where(query.PaymentNo == txtPaymentNo.Text);
                //}
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                {
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtRegistrationNo.Text)) {
                    var pmbydc = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("c");
                    query.InnerJoin(pmbydc).On(query.VerificationNo == pmbydc.VerificationNo);
                    query.Where(pmbydc.RegistrationNo == txtRegistrationNo.Text);

                    query.es.Distinct = true;
                }
                if (chkUnapprovedOnly.Checked) {
                    query.Where(query.IsApproved == false);
                }
                if (!string.IsNullOrEmpty(txtVerificationNo.Text.Trim())) { 
                    query.Where(query.VerificationNo.Like(string.Format("%{0}%", txtVerificationNo.Text.Trim())));
                }

                query.OrderBy("<LEN(a.VerificationNo) DESC>", query.VerificationNo.Descending);
                query.es.Top = 500;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            pnlInfo.Visible = false;

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "generate")
            {
                pnlInfo.Visible = false;
                //bool isValid = true;
                //if (string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPaymentNo.Text))
                //{
                //    pnlInfo.Visible = true;
                //    lblInfo.Text = "Registration or Payment No required.";
                //    isValid = false;
                //}

                //if (!isValid) return;

                /*(0:calculated fee, 1:tariff component fee)*/
                string BaseCalculateTax = AppParameter.GetParameterValue(AppParameter.ParameterItem.pphFeeBase);

                var transNos = string.Empty;

                using (var trans = new esTransactionScope())
                {
                    var xxx = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked);
                    var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      ParamedicID = dataItem["ParamedicID"].Text,
                                                                                      //DischargeDate = Convert.ToDateTime(dataItem["DischargeDate"].Text),
                                                                                      DischargeDate = DateTime.ParseExact(dataItem["DischargeDate"].Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                                                                                      TransactionNo = dataItem["TransactionNo"].Text,
                                                                                      SequenceNo = dataItem["SequenceNo"].Text,
                                                                                      TariffComponentID = dataItem["TariffComponentID"].Text
                                                                                  });
                    foreach (var group in (from g in items
                                           group g by new
                                           {
                                               g.ParamedicID
                                           }
                                               into grp
                                               orderby grp.Key.ParamedicID
                                               select new
                                               {
                                                   ParamedicID = grp.Key.ParamedicID
                                               }))
                    {
                        _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.FeeVerificationNo);
                        decimal verificationAmt = 0;
                        decimal verificationTaxAmt = 0;

                        var entity = new ParamedicFeeVerification();

                        #region header

                        entity.VerificationNo = _autoNumber.LastCompleteNumber;
                        entity.VerificationDate = DateTime.Now.Date;
                        entity.ParamedicID = group.ParamedicID;
                        
                        entity.TaxAmount = 0;
                        entity.IsVoid = false;
                        entity.IsApproved = false;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;
                        //entity.RegistrationNo = txtRegistrationNo.Text;
                        //entity.PaymentNo = txtPaymentNo.Text;

                        #endregion

                        DateTime startDate = DateTime.Now.Date;
                        DateTime endDate = DateTime.Now.Date.AddDays(-100);

                        foreach (var i in items.Where(i => i.ParamedicID == group.ParamedicID))
                        {
                            #region detail

                            if (i.DischargeDate < startDate)
                                startDate = i.DischargeDate;
                            if (i.DischargeDate > endDate)
                                endDate = i.DischargeDate;

                            var c = new ParamedicFeeTransChargesItemCompByDischargeDate();
                            c.Query.Where(c.Query.TransactionNo.Equal(i.TransactionNo),
                                c.Query.SequenceNo.Equal(i.SequenceNo),
                                c.Query.TariffComponentID.Equal(i.TariffComponentID));
                            c.Load(c.Query);
                            //c.LoadByPrimaryKey(i.SequenceNo, i.TariffComponentID, i.TransactionNo);

                            if (!string.IsNullOrEmpty(c.VerificationNo)) continue;

                            c.VerificationNo = entity.VerificationNo;
                            c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            c.LastUpdateDateTime = DateTime.Now;
                            
                            c.Save();

                            #endregion

                            verificationAmt += (c.FeeAmount ?? 0);

                            var tc = new TariffComponent();
                            tc.LoadByPrimaryKey(i.TariffComponentID);
                            if (tc.IsIncludeInTaxCalc == true)
                                verificationTaxAmt += (BaseCalculateTax == "1" && (c.SRPhysicianFeeCategory == "01" || c.SRPhysicianFeeCategory == "04")) ? (c.Price ?? 0) :(c.FeeAmount ?? 0);
                        }

                        entity.StartDate = startDate;
                        entity.EndDate = endDate;
                        entity.TaxPeriod = Convert.ToInt16(endDate.Year.ToString());
                        entity.VerificationAmount = verificationAmt;
                        entity.VerificationTaxAmount = verificationTaxAmt;

                        _autoNumber.Save();
                        entity.Save();

                        transNos = string.IsNullOrEmpty(transNos) ? entity.VerificationNo : transNos + ", " + entity.VerificationNo;
                    }

                    trans.Complete();
                }

                pnlInfo.Visible = true;
                lblInfo.Text = "Fee Verification Succeed with No. : " + transNos;

                grdList.Rebind();
                grdListV.Rebind();
            }
            else if (eventArgument.Contains("approved"))
            {
                var param = eventArgument.Split('|');
                switch (param[0]) {
                    case "approved": {
                        if (param[1].Equals("page"))
                        {
                            // looping grid
                            foreach (GridDataItem r in grdListV.MasterTableView.Items)
                            {
                                CheckBox chk = (r["IsApproved"].Controls[0] as CheckBox);
                                if (!chk.Checked)
                                {
                                    HyperLink ln = (r["VerificationNo"].Controls[0] as HyperLink);
                                    string verNo = ln.Text;
                                    (new ParamedicFeeVerification()).Approv(verNo, AppSession.UserLogin.UserID);
                                }
                            }
                        }
                        else
                        {
                            var msg = (new ParamedicFeeVerification()).Approv(param[1], AppSession.UserLogin.UserID);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                pnlInfo.Visible = true;
                                lblInfo.Text = msg;
                            }
                        }
                        break;
                    }
                    case "unapproved": {
                        var msg = (new ParamedicFeeVerification()).UnApprov(param[1], AppSession.UserLogin.UserID);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            pnlInfo.Visible = true;
                            lblInfo.Text = msg;
                        }
                        break;
                    }
                }
                
                grdListV.Rebind();
            }
            else if (eventArgument.Contains("deleteFee"))
            {
                var param = eventArgument.Split('|');
                {
                    var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                    fee.Query.Where(fee.Query.TransactionNo == param[1] && fee.Query.SequenceNo == param[2] && fee.Query.TariffComponentID == param[3]);
                    if (fee.Load(fee.Query)) {
                        if (fee.VerificationNo == null) {
                            fee.MarkAsDeleted();
                            fee.Save();
                            grdList.Rebind();
                        }
                    }
                }
            }
            else if (eventArgument == "rebind")
            {
                grdList.Rebind();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ParamedicQuery();
            query.Select(
                query.ParamedicID,
                (query.ParamedicName + " [" + query.ParamedicID + "]").As("ParamedicName")
                );
            query.Where
                (
                    query.Or
                    (
                       query.ParamedicID.Like(string.Format("%{0}%", e.Text)),
                       query.ParamedicName.Like(string.Format("%{0}%", e.Text))
                    ),
                    query.IsActive == true
                );
            query.OrderBy(query.ParamedicID.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ServiceUnitQuery();
            query.Select(
                query.ServiceUnitID,
                query.ServiceUnitName
            );
            query.Where
                (
                    query.Or
                    (
                       query.ServiceUnitID.Like(string.Format("%{0}%", e.Text)),
                       query.ServiceUnitName.Like(string.Format("%{0}%", e.Text))
                    ),
                    query.IsActive == true,
                    query.SRRegistrationType.In(
                    AppConstant.RegistrationType.ClusterPatient,
                    AppConstant.RegistrationType.EmergencyPatient,
                    AppConstant.RegistrationType.InPatient,
                    AppConstant.RegistrationType.OutPatient,
                    AppConstant.RegistrationType.MedicalCheckUp
                    )

                );
            query.OrderBy(query.ServiceUnitName.Ascending);

            cboServiceUnit.DataSource = query.LoadDataTable();
            cboServiceUnit.DataBind();
        }
    }
}
