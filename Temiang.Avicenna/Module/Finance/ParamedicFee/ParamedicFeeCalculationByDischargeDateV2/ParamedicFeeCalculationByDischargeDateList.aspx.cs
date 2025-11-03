using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class ParamedicFeeCalculationByDischargeDateList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.ParamedicFeeCalculation;

            // calculation will take a long time, 
            var ajxmgr = (RadScriptManager)Common.Helper.FindControlRecursive(this, "fw_RadScriptManager");
            ajxmgr.AsyncPostBackTimeout = 600;

            txtDatePeriode1.SelectedDate = DateTime.Now;
            txtDatePeriode2.SelectedDate = DateTime.Now;
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");

            query.es.Top = 15;
            query.Select
                (
                    query.ParamedicID,
                    (query.ParamedicName + " [" + query.ParamedicID + "]").As("ParamedicName")
                );
            query.Where
                (
                    query.Or
                    (
                       query.ParamedicID.Like(searchTextContain),
                       query.ParamedicName.Like(searchTextContain)
                    ),
                    query.IsActive == true

                );
            query.OrderBy(query.ParamedicID.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                CheckBox chk = item["IsModified"].Controls[0] as CheckBox;
                if (chk.Checked)
                {
                    item.ForeColor = Color.Red;
                    item.Font.Bold = true;
                    //item.BackColor = Color.Red;
                    //celltoVerify1.Font.Bold = true;
                    //celltoVerify1.BackColor = Color.Yellow;
                }
                item.ToolTip = ((DataRowView)item.DataItem)["refToGuarantor_GuarantorName"].ToString();
                decimal edisc = (decimal)(((DataRowView)item.DataItem)["DiscountExtra"]);
                item["FeeAmountTemplate"].ToolTip = edisc != 0 ? ("Discount: " + edisc.ToString("###.###.###")) : "";
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ParamedicFee();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable ParamedicFee()
        {
            var isEmptyFilter = txtDatePeriode1.IsEmpty && txtDatePeriode2.IsEmpty && string.IsNullOrEmpty(cboParamedicID.SelectedValue);
            if (!ValidateSearch(isEmptyFilter, "Physician Service Fee Calculation")) return null;

            try
            {
                //ExtractByDateRangeAndParamedicWithNoMergeBillingWithCorrection();
                ExtractByDateRangeAndParamedicWithMergeBilling();

                pnlInfo.Visible = false;
            }
            catch (Exception e)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = e.Message;
                return null;
            }

            #region Fee4Service
            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var transH = new TransChargesQuery("b");
            var reg = new RegistrationQuery("c");
            var item = new ItemQuery("d");
            var medic = new ParamedicQuery("e");
            var patient = new PatientQuery("f");
            var unit = new ServiceUnitQuery("g");
            var toUnit = new ServiceUnitQuery("h");
            //var guar = new GuarantorQuery("i");
            var guarCOB = new vwRegistrationGuarantorCOBQuery("j");

                query.Select(
                    transH.RegistrationNo,
                    query.RegistrationNoMergeTo,
                    transH.TransactionDate,
                    query.ParamedicID,
                    medic.ParamedicName,
                    "<ISNULL(e.IsPhysicianTeam, 0) IsPhysicianTeam>",//medic.IsPhysicianTeam,
                    "<CAST(0 as bit) IsPhysicianMember>",
                    patient.MedicalNo,
                    patient.PatientName,
                    query.DischargeDate,
                    //transH.ToServiceUnitID,
                    unit.ServiceUnitName,
                    transH.TransactionNo,
                    query.SequenceNo,
                    transH.ToServiceUnitID,
                    "<h.ServiceUnitName AS ToServiceUnitName>",
                    query.TariffComponentID,
                    query.SRPhysicianFeeCategory,
                    query.ItemID,
                    item.ItemName,
                    guarCOB.GuarantorName.As("refToGuarantor_GuarantorName"),
                    query.PriceItem,
                    query.Price,
                    query.Discount,
                    query.Qty,
                    "<(a.Price - a.Discount) * a.Qty AS ParamedicFee>",
                    query.FeeAmount,
                    "<ISNULL(a.DiscountExtra, 0) DiscountExtra>",
                    query.DeductionAmount,
                    query.LastCalculatedDateTime,
                    query.IsModified,
                    query.PaymentMethodName,
                    query.SumDeductionAmount,
                    //guar.GuarantorName.As("refToGuarantor_GuarantorName"),
                    "<CASE WHEN c.SRRegistrationType = 'IPR' THEN DATEDIFF(d,c.RegistrationDate, c.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>"
                    );
            query.InnerJoin(transH).On(transH.TransactionNo == query.TransactionNo);
            query.InnerJoin(reg).On(reg.RegistrationNo == transH.RegistrationNo);
            query.InnerJoin(item).On(item.ItemID == query.ItemID);
            query.InnerJoin(medic).On(medic.ParamedicID == query.ParamedicID);
            query.InnerJoin(patient).On(patient.PatientID == reg.PatientID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
            query.InnerJoin(toUnit).On(toUnit.ServiceUnitID == transH.ToServiceUnitID);
            //query.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID);
            query.InnerJoin(guarCOB).On(reg.RegistrationNo == guarCOB.RegistrationNo);

            query.Where(query.DischargeDateMergeTo.Between(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate),
                        //query.VerificationNo.IsNull(), 
                        query.SRPhysicianFeeCategory != "03"/*remun*/); //.In(new string[]{"01","04"})/*fee 4 service v1 dan v2*/);
            // filter hanya yang bisa proses ke jasmed saja
            query.Where(medic.ParamedicFee == true);

            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            // exclude
            var aCollGuar = new AppStandardReferenceItemCollection();
            aCollGuar.Query.Where(aCollGuar.Query.StandardReferenceID == "PhysFeeCalcGuarExcl");
            aCollGuar.LoadAll();
            if (aCollGuar.Count > 0)
            {
                query.Where(reg.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }

            query.OrderBy(query.IsModified.Descending, transH.RegistrationNo.Ascending);

            var res = query.LoadDataTable();

            ParamedicFeeTransChargesItemCompByDischargeDateCollection.UpdateMoreInfo(res,
                AppSession.Parameter.IsPhysicianFeeShowProcedureNote);

            #endregion

            #region FeeByTeam
            var feeTeams = res.AsEnumerable().Where(r => ((!(r["IsPhysicianTeam"] is DBNull)) && (bool)r["IsPhysicianTeam"]));
            if (feeTeams.Any()) {
                var qMember = new ParamedicFeeTransChargesItemCompByTeamQuery("qMember");
                var parMember = new ParamedicQuery("parMember");

                qMember.InnerJoin(parMember).On(qMember.ParamedicID == parMember.ParamedicID)
                    .Select(
                        qMember.RegistrationNo,
                        qMember.RegistrationNoMergeTo,
                        "<getdate() as TransactionDate>", //qMember.TransactionDate,
                        qMember.ParamedicID,
                        parMember.ParamedicName,
                        "<ISNULL(parMember.IsPhysicianTeam, 0) IsPhysicianTeam>", //parMember.IsPhysicianTeam,
                        "<CAST(1 as bit) IsPhysicianMember>",
                        "<'' MedicalNo>",//qMember.MedicalNo,
                        "<'' PatientName>",//qMember.PatientName,
                        qMember.DischargeDate,
                        //"<'' ToServiceUnitID>",//qMember.ToServiceUnitID,
                        "<'' ServiceUnitName>",//qMember.ServiceUnitName,
                        qMember.TransactionNo,
                        qMember.SequenceNo,
                        "<'' ToServiceUnitID>",//qMember.ToServiceUnitID,
                        "<'' ToServiceUnitName>",
                        qMember.TariffComponentID,
                        "<'' SRPhysicianFeeCategory>",//qMember.SRPhysicianFeeCategory,
                        qMember.ItemID,
                        "<'' ItemName>",//qMember.ItemName,
                        "<'' refToGuarantor_GuarantorName>",//qMember.GuarantorName.As("refToGuarantor_GuarantorName"),
                        qMember.PriceItem,
                        qMember.Price,
                        qMember.Discount,
                        qMember.Qty,
                        "<(qMember.Price - qMember.Discount) * qMember.Qty AS ParamedicFee>",
                        qMember.FeeAmount,
                        "<cast(0 as decimal(18,2)) DiscountExtra>",
                        "<cast(0 as decimal(18,2)) DeductionAmount>",//qMember.DeductionAmount,
                        qMember.LastCalculatedDateTime,
                        "<cast(0 as bit) IsModified>",//qMember.IsModified,
                        "<'' PaymentMethodName>",//qMember.PaymentMethodName,
                        "<cast(0 as decimal(18,2)) SumDeductionAmount>",//qMember.SumDeductionAmount,
                        "<cast(0 as int) refToRegistration_LOS>"
                );
                qMember.Where(qMember.TransactionNo.In(feeTeams.Select(f => f["TransactionNo"].ToString()).Distinct()));
                var resMember = qMember.LoadDataTable();
                if (resMember.Rows.Count > 0) {
                    foreach (System.Data.DataRow rMember in resMember.Rows) {
                        var feeTeam = feeTeams.Where(f =>
                            f["TransactionNo"].ToString() == rMember["TransactionNo"].ToString() &&
                            f["SequenceNo"].ToString() == rMember["SequenceNo"].ToString() &&
                            f["TariffComponentID"].ToString() == rMember["TariffComponentID"].ToString()).First();

                        rMember["TransactionDate"] = feeTeam["TransactionDate"];
                        rMember["MedicalNo"] = feeTeam["MedicalNo"];
                        rMember["PatientName"] = feeTeam["PatientName"];
                        rMember["ServiceUnitName"] = feeTeam["ServiceUnitName"];
                        rMember["ToServiceUnitID"] = feeTeam["ToServiceUnitID"];
                        rMember["ToServiceUnitName"] = feeTeam["ToServiceUnitName"];
                        rMember["SRPhysicianFeeCategory"] = feeTeam["SRPhysicianFeeCategory"];
                        rMember["ItemName"] = feeTeam["ItemName"];
                        rMember["refToGuarantor_GuarantorName"] = feeTeam["refToGuarantor_GuarantorName"];
                        rMember["DiscountExtra"] = feeTeam["DiscountExtra"];
                        rMember["PaymentMethodName"] = feeTeam["PaymentMethodName"];
                        rMember["refToRegistration_LOS"] = feeTeam["refToRegistration_LOS"];
                    }
                    resMember.AcceptChanges();

                    //hapus yang team
                    var feeTeamToRemoves = from t in res.AsEnumerable()
                             join m in resMember.AsEnumerable()
                             on new
                             {
                                 TransactionNo = t["TransactionNo"].ToString(),
                                 SequenceNo = t["SequenceNo"].ToString(),
                                 TariffComponentID = t["TariffComponentID"].ToString()
                             } equals new {
                                 TransactionNo = m["TransactionNo"].ToString(),
                                 SequenceNo = m["SequenceNo"].ToString(),
                                 TariffComponentID = m["TariffComponentID"].ToString()
                             }
                             select t;

                    foreach (var feeTeamToRemove in feeTeamToRemoves) {
                        feeTeamToRemove.Delete();
                    }
                    res.AcceptChanges();

                    res.Merge(resMember);
                }
            }
            #endregion

            #region FeeByAR
            var queryAR = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var guarAR = new GuarantorQuery("b");
            var regAR = new RegistrationQuery("c");
            var medicAR = new ParamedicQuery("e");
            var patientAR = new PatientQuery("f");
            var unitAR = new ServiceUnitQuery("g");
            var vwGuarCOB = new vwRegistrationGuarantorCOBQuery("cob");

                queryAR.Select(
                queryAR.RegistrationNo,
                queryAR.RegistrationNoMergeTo,
                queryAR.DischargeDate.As("TransactionDate"),
                queryAR.ParamedicID,
                medicAR.ParamedicName,
                "<ISNULL(e.IsPhysicianTeam, 0) IsPhysicianTeam>", //medicAR.IsPhysicianTeam,
                "<CAST(0 as bit) IsPhysicianMember>",
                patientAR.MedicalNo,
                patientAR.PatientName,
                queryAR.DischargeDate,
                //unitAR.ServiceUnitID.As("ToServiceUnitID"),
                unitAR.ServiceUnitName,
                queryAR.TransactionNo,
                queryAR.SequenceNo,
                unitAR.ServiceUnitID.As("ToServiceUnitID"),
                "<g.ServiceUnitName AS ToServiceUnitName>",
                queryAR.TariffComponentID,
                queryAR.SRPhysicianFeeCategory,
                queryAR.ItemID,
                //guarAR.GuarantorName.As("ItemName"),
                vwGuarCOB.GuarantorName.As("ItemName"),
                queryAR.PriceItem,
                queryAR.Price,
                queryAR.Discount,
                queryAR.Qty,
                "<(a.Price - a.Discount) * a.Qty AS ParamedicFee>",
                
                queryAR.FeeAmount,
                "<ISNULL(a.DiscountExtra, 0) DiscountExtra>",
                queryAR.DeductionAmount,
                queryAR.LastCalculatedDateTime,
                queryAR.IsModified,
                queryAR.PaymentMethodName,
                queryAR.SumDeductionAmount,
                //guarAR.GuarantorName.As("refToGuarantor_GuarantorName"),
                vwGuarCOB.GuarantorName.As("refToGuarantor_GuarantorName"),
                "<CASE WHEN c.SRRegistrationType = 'IPR' THEN DATEDIFF(d,c.RegistrationDate, c.DischargeDate) + 1 ELSE 1 END as refToRegistration_LOS>"
                );

            queryAR.InnerJoin(guarAR).On(queryAR.ItemID.Equal(guarAR.GuarantorID));
            queryAR.InnerJoin(regAR).On(regAR.RegistrationNo == queryAR.RegistrationNo);
            queryAR.InnerJoin(medicAR).On(medicAR.ParamedicID == queryAR.ParamedicID);
            queryAR.InnerJoin(patientAR).On(patientAR.PatientID == regAR.PatientID);
            queryAR.InnerJoin(unitAR).On(unitAR.ServiceUnitID == regAR.ServiceUnitID);
            queryAR.InnerJoin(vwGuarCOB).On(queryAR.RegistrationNo == vwGuarCOB.RegistrationNo);

            queryAR.Where(queryAR.DischargeDateMergeTo.Between(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate),
                queryAR.VerificationNo.IsNull(), queryAR.SRPhysicianFeeCategory == "02" /*Fee by AR*/,
                queryAR.TariffComponentID == string.Empty);

            // filter hanya yang bisa proses ke jasmed saja
            query.Where(medicAR.ParamedicFee == true);

            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                queryAR.Where(queryAR.ParamedicID == cboParamedicID.SelectedValue);

            // exclude
            if (aCollGuar.Count > 0)
            {
                queryAR.Where(regAR.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }

            queryAR.OrderBy(queryAR.IsModified.Descending, queryAR.RegistrationNo.Ascending);

            var resAR = queryAR.LoadDataTable();

            //khusus rsmp nilai dasar perhitungan jasa dari nilai perhitungan bpjs eklaim
            if (AppSession.Parameter.HealthcareInitial == "RSMP")
            {
                foreach (DataRow row in resAR.Rows)
                {
                    var ncc = new NccInacbg();
                    if (ncc.LoadByPrimaryKey(row["RegistrationNo"].ToString()))
                    {
                        row["PriceItem"] = ncc.CoverageAmount ?? 0;
                        row["Price"] = ncc.CoverageAmount ?? 0;
                        row["ParamedicFee"] = Convert.ToDecimal(row["Price"]) - Convert.ToDecimal(row["Discount"]);

                        var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                        fee.Query.Where(fee.Query.TransactionNo == row["RegistrationNo"].ToString(), 
                            fee.Query.ParamedicID == row["ParamedicID"].ToString(),
                            fee.Query.SequenceNo == row["SequenceNo"].ToString());
                        if (fee.Query.Load())
                        {
                            fee.Price = ncc.CoverageAmount ?? 0;
                            fee.PriceItem = ncc.CoverageAmount ?? 0;
                        }
                        fee.Save();
                    }
                    else //reload ulang dari eklaim jika tidak ada
                    {
                        var regs = new Registration();
                        if (!regs.LoadByPrimaryKey(row["RegistrationNo"].ToString())) continue;
                        if (string.IsNullOrEmpty(regs.BpjsSepNo)) continue;

                        var service = new Common.Inacbg.v51.Service();
                        var response = service.GetDetail(new Common.Inacbg.v51.Claim.Get.GetDetail.Data() { nomor_sep = reg.BpjsSepNo });
                        if (response.Metadata.IsValid)
                        {
                            var entity = new NccInacbg();
                            entity.RegistrationNo = row["RegistrationNo"].ToString();

                            entity.PatientId = response.DataResponse.Data.PatientId.ToInt();
                            entity.AdmissionId = response.DataResponse.Data.AdmissionId.ToInt();
                            entity.HospitalAdmissionId = response.DataResponse.Data.HospitalAdmissionId.ToInt();

                            entity.LastUpdateDateTime = DateTime.Now;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            entity.AddPaymentAmt = Convert.ToDecimal(string.IsNullOrEmpty(response.DataResponse.Data.AddPaymentAmt) ? 0 : Convert.ToDouble(response.DataResponse.Data.AddPaymentAmt));

                            var grouper = response.DataResponse.Data.Grouper;
                            if (grouper.Response != null) entity.CoverageAmount = Convert.ToDecimal(grouper.Response.Cbg.Tariff);
                            else entity.CoverageAmount = 0;

                            entity.Save();

                            row["PriceItem"] = entity.CoverageAmount ?? 0;
                            row["Price"] = entity.CoverageAmount ?? 0;
                            row["ParamedicFee"] = Convert.ToDecimal(row["Price"]) - Convert.ToDecimal(row["Discount"]);

                            var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                            fee.Query.Where(fee.Query.TransactionNo == row["RegistrationNo"].ToString(),
                                fee.Query.SequenceNo == row["SequenceNo"].ToString());
                            if (fee.Query.Load())
                            {
                                fee.Price = ncc.CoverageAmount ?? 0;
                                fee.PriceItem = ncc.CoverageAmount ?? 0;
                            }
                            fee.Save();
                        }
                    }
                }
            }
            #endregion

            res.Merge(resAR);

            return res;
        }

        private void ExtractByDateRangeAndParamedicWithNoMergeBillingWithCorrection()
        {
            ParamedicFeeTransChargesItemCompByDischargeDate.ExtractByDateRangeAndParamedicWithNoMergeBillingWithCorrection(
                txtDatePeriode1.SelectedDate.Value.Date, txtDatePeriode2.SelectedDate.Value.Date,
                cboParamedicID.SelectedValue, AppSession.UserLogin.UserID);
        }

        private void ExtractByDateRangeAndParamedicWithMergeBilling()
        {
            ParamedicFeeTransChargesItemCompByDischargeDate.ExtractByDateRangeAndParamedicWithMergeBilling(string.Empty,
                txtDatePeriode1.SelectedDate.Value.Date, txtDatePeriode2.SelectedDate.Value.Date,
                cboParamedicID.SelectedValue, AppSession.UserLogin.UserID);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            switch (eventArgument)
            {
                case "rebind":
                    //CalculateWithNoMergeBillingWithCorrection();
                    CalculateWithMergeBilling();
                    grdList.Rebind();
                    break;
                case "refresh":
                    grdList.Rebind();
                    break;
                case "print":
                    Print();
                    break;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var tbl = ParamedicFee();
            
            if (AppSession.Parameter.HealthcareInitial == "RSISB")
            {
                //untuk menghilangkan kolom yang visible = false di grid display
                //var cNames = new List<string>();
                //foreach (GridColumn col in grdList.Columns) {
                //    //if (col.Visible) cNames.Add(col.UniqueName); 
                //}
                var cNames = new List<string>();
                foreach (DataColumn col in tbl.Columns) {
                    if (!cNames.Contains(col.ColumnName))
                    {
                        cNames.Add(col.ColumnName);
                    }
                }
                foreach (string n in cNames)
                {
                    var a = tbl.Columns[n];
                    //if (a != null)
                    //{
                    //    tbl.Columns.Remove(a);
                    //}l.Columns.Remove("DischargeDate");
                    //}
                    if (tbl.Columns.Contains("DischargeDate"))
                        tbl.Columns.Remove("DischargeDate");
                    if (tbl.Columns.Contains("ToServiceUnitName"))
                        tbl.Columns.Remove("ToServiceUnitName");
                    if (tbl.Columns.Contains("ToServiceUnitID"))
                        tbl.Columns.Remove("ToServiceUnitID");
                    if (tbl.Columns.Contains("ToServiceUnitID1"))
                        tbl.Columns.Remove("ToServiceUnitID1");
                    //if (tbl.Columns.Contains("PaymentMethodName"))
                    //    tbl.Columns.Remove("PaymentMethodName");
                }
            }
            Common.CreateExcelFile.CreateExcelDocument(tbl, "ParamedicFee.xls", this.Response);
        }

        private int UpdateData(DateTime? d1, DateTime? d2, string ParamedicID, bool UseMergeBilling)
        {
            var o = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            return o.UpdateDataDiscount() + o.UpdateDataParamedic(d1, d2, ParamedicID, UseMergeBilling);
        }

        /// <summary>
        /// SUDAH TIDAK DIPAKAI LAGI, TAPI SAYANG DIBUANG, TAKUT NANTI MAU DIPAKAI LAGI
        /// </summary>
        private void CalculateWithNoMergeBillingWithCorrection()
        {
            UpdateData(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate, cboParamedicID.SelectedValue, false);

            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var transH = new TransChargesQuery("b");
            var regis = new RegistrationQuery("c");
            var item = new ItemQuery("d");
            var paramedic = new ParamedicQuery("e");
            var patient = new PatientQuery("f");
            var unit = new ServiceUnitQuery("g");
            var toUnit = new ServiceUnitQuery("h");
            var refferal = new ReferralQuery("i");

            query.Select(
                query,
                transH.RegistrationNo,
                refferal.ParamedicID.As("ParamedicIDReferral")
                );
            query.InnerJoin(transH).On(transH.TransactionNo == query.TransactionNo);
            query.InnerJoin(regis).On(regis.RegistrationNo == transH.RegistrationNo);
            query.InnerJoin(item).On(item.ItemID == query.ItemID);
            query.InnerJoin(paramedic).On(paramedic.ParamedicID == query.ParamedicID);
            query.InnerJoin(patient).On(patient.PatientID == regis.PatientID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == regis.ServiceUnitID);
            query.InnerJoin(toUnit).On(toUnit.ServiceUnitID == transH.ToServiceUnitID);
            query.LeftJoin(refferal).On(refferal.ReferralID == regis.ReferralID);

            query.Where(query.DischargeDate.Between(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate),
                        query.VerificationNo.IsNull());

            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            // exclude
            var aCollGuar = new AppStandardReferenceItemCollection();
            aCollGuar.Query.Where(aCollGuar.Query.StandardReferenceID == "PhysFeeCalcGuarExcl");
            aCollGuar.LoadAll();
            if (aCollGuar.Count > 0)
            {
                query.Where(regis.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }

            // filter hanya yang bisa diproses ke jasmed saja
            query.Where(paramedic.ParamedicFee == true);

            var app = new AppParameter();
            if (app.LoadByPrimaryKey("acc_IsAutoJournalPhysicianFeeBeforeVerification"))
            {
                if (app.ParameterValue == "Yes")
                {
                    query.Where(query.JournalId.IsNull());
                }
            }

            var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            coll.Load(query);

            //DoCalculateFee4Service(coll);
        }

        private void CalculateWithMergeBilling()
        {
            var o = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            o.UpdateDataDiscount();

            //var isDebugRSI = true;// (AppSession.Parameter.HealthcareInitial == "RSI" && AppSession.UserLogin.UserID == "sci");

            for (DateTime currDate = txtDatePeriode1.SelectedDate.Value;
                currDate <= txtDatePeriode2.SelectedDate.Value;
                currDate = currDate.AddDays(1)) {

                //    UpdateData(currDate, currDate, cboParamedicID.SelectedValue, true);
                o.UpdateDataParamedic(currDate, currDate, cboParamedicID.SelectedValue, true);

                try
                {
                    ParamedicFeeTransChargesItemCompByDischargeDate
                        .ExtractByDateRangeAndParamedicWithMergeBilling(string.Empty,
                        currDate, currDate, cboParamedicID.SelectedValue, AppSession.UserLogin.UserID);
                    pnlInfo.Visible = false;
                }
                catch (Exception e)
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = e.Message;
                    return;
                }
                
                //// hanya dipakai untuk perbaiki data jasmed RSI
                //if (isDebugRSI)
                //{
                //    var collx = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                //    var queryx = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("pfee");
                //    queryx.Where(
                //        queryx.DischargeDateMergeTo.Between(currDate, currDate),
                //        queryx.VerificationNo.IsNull()
                //        );
                //    if (collx.Load(queryx))
                //    {
                //        var regs = collx.Select(x => x.RegistrationNoMergeTo).Distinct().ToArray();

                //        foreach (var r in regs)
                //        {
                //            //nnn

                //            //var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                //            //feeColl.RecalculateForFeeByPlafonGuarantor(entity, TransPaymentItems, AppSession.UserLogin.UserID);
                //            //feeColl.SetPayment(entity, AppSession.UserLogin.UserID);

                //            if (r != "REG/IP/230718-0011") continue;

                //            // rekal untuk prorata ???
                //                var ba = new BillingAdjustment(r, true);
                //            var msg = ba.CalculateAndSaveProrata_NoTransactionScope(AppSession.UserLogin.UserID);
                //            if (!string.IsNullOrEmpty(msg))
                //            {
                //                //ShowInformationHeader(msg);
                //                //return false;
                //                //throw new Exception(msg);
                //            }
                //        }
                //    }
                //}

                var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                var query = coll.MainQuery();
                query.Where(
                    query.DischargeDateMergeTo.Between(currDate, currDate),
                    query.VerificationNo.IsNull()
                    );

                //if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue)) <-- tidak boleh difilter per dokter, harus dihitung per registrasi
                //    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                if (coll.Load(query))
                {
                    coll.CalculateGrossFee(AppSession.UserLogin.UserID);
                }

                //if (isDebugRSI)
                //{
                //    coll.Save();
                //}
                //else {
                    #region Deduction
                    var decColl = new ParamedicFeeDeductionsCollection();
                    var decQuery = new ParamedicFeeDeductionsQuery("a");
                    var feeQuery = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("b");
                    decQuery.InnerJoin(feeQuery).On(
                        decQuery.TransactionNo.Equal(feeQuery.TransactionNo) &&
                        decQuery.SequenceNo.Equal(feeQuery.SequenceNo) &&
                        decQuery.TariffComponentID.Equal(feeQuery.TariffComponentID))
                        .Where(
                            feeQuery.DischargeDateMergeTo.Between(currDate, currDate)
                            ,
                            feeQuery.VerificationNo.IsNull()
                        )
                        .Select(
                            decQuery
                        );
                    decColl.Load(decQuery);

                    // Erase prev
                    decColl.MarkAllAsDeleted();
                    decColl.Save();

                    coll.CalculateDeductionBeforeTax(decColl, AppSession.UserLogin.UserID);

                    using (var trans = new esTransactionScope())
                    {
                        coll.Save();

                        //coll.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                        //coll.Save();

                        decColl.Save();
                        trans.Complete();
                    }
                    #endregion
                //}
            }

            //if (isDebugRSI)
            //{
            //    var collAll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            //    var queryAll = collAll.MainQuery();
            //    queryAll.Where(
            //        queryAll.DischargeDateMergeTo.Between(txtDatePeriode1.SelectedDate.Value, txtDatePeriode2.SelectedDate.Value),
            //        queryAll.VerificationNo.IsNull()
            //        );
            //    collAll.Load(queryAll);

            //    collAll.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
            //    collAll.Save();
            //}

            // try to get payment status
        }

        private void Print()
        {
            var jobParameters = new PrintJobParameterCollection();

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "p_FromDate";
            parDate1.ValueDateTime = txtDatePeriode1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "p_ToDate";
            parDate2.ValueDateTime = txtDatePeriode2.SelectedDate ?? DateTime.Now.AddDays(10);

            var parPhysician = jobParameters.AddNew();
            parPhysician.Name = "p_ParamedicID";
            parPhysician.ValueString = cboParamedicID.SelectedValue;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.PhysicianFeeCalculationDraftSlip;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }
    }
}
