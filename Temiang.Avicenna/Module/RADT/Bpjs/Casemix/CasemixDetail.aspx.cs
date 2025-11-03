using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Streaming;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model.Resources;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class CasemixDetail : BasePage
    {
        private string _regType=null;
        public override string RegistrationType
        {
            get
            {
                if (_regType == null)
                {
                    var reg = new Registration();
                    reg.Query.Select(reg.Query.SRRegistrationType);
                    reg.Query.Where(reg.Query.RegistrationNo == Request.QueryString["regno"]);
                    if (reg.Query.Load())
                        _regType = reg.SRRegistrationType;
                    else
                        _regType = String.Empty;
                }
                return _regType;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CasemixApproval;

            if (!IsCallback)
            {
                //For Grid Detail
                PopUpSearch.RegisterClientScript(AppEnum.PopUpSearch.Item, Page);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.DataBind();

            txtRegistrationNo.Text = Request.QueryString["regno"];
            cboFilterByApprovalStatus.SelectedIndex = Request.QueryString["appst"] == string.Empty ? 0 : 1;
            cboFilterApprovalBloodRequest.SelectedIndex = Request.QueryString["appst"] == string.Empty ? 0 : 1;
            string tab = Request.QueryString["tab"];

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                txtNoBpjs.Text = reg.GuarantorCardNo;
                txtSepNo.Text = reg.BpjsSepNo;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtPlaceDOB.Text = pat.CityOfBirth;
                txtDateOfBirth.SelectedDate = pat.DateOfBirth;
                txtGender.Text = pat.Sex;
                txtAgeInYear.Text = Convert.ToString(reg.AgeInYear);
                txtAgeInMonth.Text = Convert.ToString(reg.AgeInMonth);
                txtAgeInDay.Text = Convert.ToString(reg.AgeInDay);

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnitID.Text = unit.ServiceUnitID;
                lblServiceUnitName.Text = unit.ServiceUnitName;

                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                txtRoomID.Text = room.RoomID;
                lblRoomName.Text = room.RoomName;

                var cls = new Class();
                cls.LoadByPrimaryKey(reg.ChargeClassID);
                txtClassID.Text = cls.ClassID;
                lblClassName.Text = cls.ClassName;

                cls = new Class();
                cls.LoadByPrimaryKey(reg.CoverageClassID);
                txtCoverageClass.Text = cls.ClassID;
                lblCoverageClassName.Text = cls.ClassName;

                txtBedID.Text = reg.BedID;

                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtParamedicID.Text = par.ParamedicID;
                lblParamedicName.Text = par.ParamedicName;

                var guar = new Guarantor();
                guar.LoadByPrimaryKey((string.IsNullOrEmpty(pat.str.MemberID) ? reg.GuarantorID : pat.MemberID));
                txtGuarantorID.Text = guar.GuarantorID;
                lblGuarantorName.Text = guar.GuarantorName;

                var x = reg.DischargeDate != null ? reg.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                var y = reg.RegistrationDate.Value.Date;
                txtLengthOfStay.Value = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;

                PopulatePatientImage(pat.PatientID);

                var ncc = new NccInacbg();
                if (ncc.LoadByPrimaryKey(txtRegistrationNo.Text))
                {
                    lblCoverageEklaim.Text = (ncc.CoverageAmount ?? 0).ToString("N2");
                    if (Helper.IsInacbgIntegration && AppSession.Parameter.IsiDRGIntegration)
                    {
                        var svc = new Common.Inacbg.v510.Service();
                        var response = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data() { nomor_sep = txtSepNo.Text });
                        if (response.Metadata.IsValid)
                        {
                            lblStatusEklaim.Text = response.DataResponse?.Data?.KlaimStatusCd ?? string.Empty;
                            lblStatusKemenkes.Text = response.DataResponse?.Data?.KemenkesDcStatusCd ?? string.Empty;
                            lblCoverageEklaim.Text = (ncc?.CoverageAmount ?? 0).ToString("N2");
                            lblGroupCode.Text = response?.DataResponse?.Data?.Grouper?.ResponseInacbg?.Cbg?.Code ?? string.Empty;
                            lblGroupDesc.Text = response?.DataResponse?.Data?.Grouper?.ResponseInacbg?.Cbg?.Description ?? string.Empty;
                            var culture = new System.Globalization.CultureInfo("id-ID");
                            if (decimal.TryParse(response?.DataResponse?.Data?.Grouper?.ResponseInacbg?.Tariff, out decimal tarif))
                            {
                                lblGroupTarif.Text = $"Rp {tarif.ToString("N0", culture)}";
                            }
                            else
                            {
                                lblGroupTarif.Text = "Rp 0";
                            }

                            lblMdc.Text = $"{response?.DataResponse?.Data?.Grouper?.ResponseIdrg?.mdc_description} - " + $"{response?.DataResponse?.Data?.Grouper?.ResponseIdrg?.mdc_number}";
                            lblDrg.Text = $"{response?.DataResponse?.Data?.Grouper?.ResponseIdrg?.drg_description} - " + $"{response?.DataResponse?.Data?.Grouper?.ResponseIdrg?.drg_code}";
                            lblIdrgStatus.Text = response?.DataResponse?.Data?.KlaimStatusCd ?? string.Empty;
                        }
                    }
                }
                else
                {
                    lblCoverageEklaim.Text = "0.00";
                    lblStatusEklaim.Text = "N/A";
                    lblStatusKemenkes.Text = "N/A";
                }
            }

            var iic = new InvoicesItemCollection();
            iic.Query.Where(iic.Query.RegistrationNo == txtRegistrationNo.Text);
            iic.Query.OrderBy(iic.Query.InvoiceNo.Ascending);
            if (iic.Query.Load())
            {
                foreach (var ii in iic)
                {
                    var i = new Invoices();
                    i.LoadByPrimaryKey(ii.InvoiceNo);
                    if (i.IsInvoicePayment ?? false) lblPaymentStatus.Text = "PAID";
                    else lblPaymentStatus.Text = "INVOICED";
                }
            }
            else lblPaymentStatus.Text = "N/A";

            if (tab == "pgDocument" && (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsNewDisplayCasemixCenter) == "Yes"))
            {
                RadTabStrip1.SelectedIndex = 6; 
                RadMultiPage1.SelectedIndex = 6; 
            }

            PopulatePlafonBar();

            InitializeCboFilterByServiceUnitID();
            CalculateDetailTransactionByGroup();
            PopulateEpisodeDiagnoseGrid();
            PopulateEpisodeProcedureGrid();
            PopulatePathwayItemGrid();
            PopulateRegistrationGuarantorGrid();
            PopulateCasemixCoveredRegistrationRuleItemGrid();
        }

        private void CalculateDetailTransactionByGroup()
        {
            var ccq = new CostCalculationQuery("a");
            var iq = new ItemQuery("b");

            ccq.Select(iq.SREklaimTariffGroup, (ccq.PatientAmount.Sum() + ccq.GuarantorAmount.Sum()).As("Amount"));
            ccq.InnerJoin(iq).On(ccq.ItemID == iq.ItemID);
            ccq.Where(ccq.RegistrationNo.In(txtRegistrationNo.Text));
            ccq.GroupBy(iq.SREklaimTariffGroup);

            var tbl = ccq.LoadDataTable();
            if (tbl.Rows.Count > 0)
            {
                var pnb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "01").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtProsedurNonBedah.Value = pnb != null ? Convert.ToDouble(pnb) : 0;
                var ta = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "04").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtTenagaAhli.Value = ta != null ? Convert.ToDouble(ta) : 0;
                var rd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "07").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtRadiologi.Value = rd != null ? Convert.ToDouble(rd) : 0;
                var rh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "10").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtRehabilitasi.Value = rh != null ? Convert.ToDouble(rh) : 0;
                var ob = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "13").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtObat.Value = ob != null ? Convert.ToDouble(ob) : 0;
                var sa = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "16").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtSewaAlat.Value = sa != null ? Convert.ToDouble(sa) : 0;
                var pb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "02").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtProsedurBedah.Value = pb != null ? Convert.ToDouble(pb) : 0;
                var kp = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "05").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtKeperawatan.Value = kp != null ? Convert.ToDouble(kp) : 0;
                var lb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "08").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtLaboratorium.Value = lb != null ? Convert.ToDouble(lb) : 0;
                var ko = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "11").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtKamarAkomodasi.Value = ko != null ? Convert.ToDouble(ko) : 0;
                var al = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "14").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtAlkes.Value = al != null ? Convert.ToDouble(al) : 0;
                var kn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "03").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtKonsultasi.Value = kn != null ? Convert.ToDouble(kn) : 0;
                var pn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "06").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtPenunjang.Value = pn != null ? Convert.ToDouble(pn) : 0;
                var pd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "09").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtPelayananDarah.Value = pd != null ? Convert.ToDouble(pd) : 0;
                var ri = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "12").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtRawatIntensifTarif.Value = ri != null ? Convert.ToDouble(ri) : 0;
                var bh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "15").Select(t => t.Field<decimal>("Amount")).SingleOrDefault();
                txtBMHP.Value = bh != null ? Convert.ToDouble(bh) : 0;
            }
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] == null)
                ViewState["BillingVerification:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            return (string[])ViewState["BillingVerification:MergeRegistration" + Request.UserHostName];
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = Transactions;
        }

        private DataTable Transactions
        {
            get
            {
                DataTable tbl = TransChargesItems;
                tbl.Merge(TransPrescriptionItems);

                var dv = tbl.DefaultView;
                dv.Sort = "ServiceUnitName ASC, TransactionNo ASC, SequenceNo ASC";

                return dv.ToTable();
            }
        }

        private DataTable TransPrescriptionItems
        {
            get
            {
                var query = new TransPrescriptionItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransPrescriptionQuery("c");
                var unit = new ServiceUnitQuery("d");
                var cost = new CostCalculationQuery("e");
                var reg = new RegistrationQuery("f");
                //var pay = new TransPaymentItemOrderQuery("g");
                //var payib = new TransPaymentItemIntermBillQuery("h");

                //var payReff = new TransPaymentItemOrderQuery("i");
                //var payibReff = new TransPaymentItemIntermBillQuery("j");
                var queryReff = new TransPrescriptionItemQuery("k");
                var costReff = new CostCalculationQuery("l");

                //var view = new VwTransactionQuery("x");
                var cls = new ClassQuery("cls");
                var tpReff = new TransPrescriptionQuery("tpReff");

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = header.PrescriptionDate.Cast(esCastType.String);

                var std = new AppStandardReferenceItemQuery("z");

                query.Select
                    (
                        query.CasemixNotes.As("Notes"),
                        header.RegistrationNo,
                        query.PrescriptionNo.As("TransactionNo"),
                        query.SequenceNo,
                        "<CASE WHEN a.IsApprove = 1 AND c.IsApproval = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsApprove>",
                        //query.IsApprove,
                        query.IsVoid,
                        "<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBillProceed>",
                        //query.IsBillProceed,
                        "<CAST(0 AS BIT) AS IsOrderRealization>",
                        "<CASE WHEN a.ItemInterventionID = '' THEN a.ItemID ELSE a.ItemInterventionID END AS ItemID>",
                        query.ResultQty.As("ChargeQuantity"),
                        query.SRItemUnit,
                        query.Price,
                        query.DiscountAmount,
                        query.LastUpdateByUserID,
                        "<CAST(0 AS NUMERIC(18, 2)) AS CitoAmount>",
                        @"<CASE WHEN e.LastUpdateDateTime = '01/01/1900 00:00:00.000' THEN '' 
                            ELSE e.LastUpdateDateTime END AS 'LastUpdateDateTime'>",
                        @"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN a.LineAmount
                            ELSE 0 END AS Total>",
                        //@"<CASE WHEN a.IsApprove = 1 AND a.IsBillProceed = 1 THEN a.LineAmount
                        // ELSE 0 END AS Total>",
                        @"<CASE WHEN a.ItemInterventionID = '' THEN b.ItemName 
                            ELSE (SELECT ItemName FROM Item WHERE ItemID = a.ItemInterventionID) END AS ItemName>",
                        header.PrescriptionDate.As("TransactionDate"),
                        header.ServiceUnitID,
                        header.ClassID,
                        unit.ServiceUnitName,
                        "<CAST(0 AS BIT) AS IsOrder>",
                        group.As("Group"),
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        "<'2' AS TYPE>",
                        reg.IsHoldTransactionEntry,
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL OR h.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaymentProceed>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceed>",
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL THEN '[' + g.PaymentNo + ']' ELSE CASE WHEN h.PaymentNo IS NOT NULL THEN '[' + h.PaymentNo + ']' ELSE '' END END AS PaymentNo>",
                        @"<'' AS PaymentNo>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsIntermBillProceed>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN '' ELSE ' - ' + e.IntermBillNo END AS IntermBillNo>",
                        //@"<CASE WHEN i.PaymentNo IS NOT NULL OR j.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaymentProceedReff>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceedReff>",
                        "<'' AS PatientName>",
                        //view.IsCorrection,
                        //view.OrderDate,
                        //view.OrderTransNo,
                        header.IsPrescriptionReturn.As("IsCorrection"),
                        @"<ISNULL(tpReff.PrescriptionDate, c.PrescriptionDate) AS OrderDate>",
                        @"<ISNULL(tpReff.PrescriptionNo, c.PrescriptionNo) AS OrderTransNo>",
                        header.PrescriptionDate.As("ExecutionDate"),
                        cls.ClassName,
                        query.IsReturned.As("IsCorrected"),
                        //"<CAST(0 AS BIT) AS IsCorrected>"
                        std.ItemName.Coalesce("''").As("DiscountReason"),
                        @"<ISNULL(e.IntermBillNo, '') AS CcIntermBillNo>",
                        @"<ISNULL(l.IntermBillNo, '') AS CcRefIntermBillNo>",
                        header.ReferenceNo,
                        //@"<ISNULL(a.IsCasemixApproved, CAST(0 AS BIT)) AS IsCasemixApproved>",
                        query.IsCasemixApproved,
                        query.CasemixApprovedDateTime,
                        query.CasemixApprovedByUserID,
                        header.IsUnitDosePrescription,
                        header.IsFromSOAP,
                        header.CreatedDateTime
                    );

                query.LeftJoin(std).On(query.SRDiscountReason == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString());

                query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
                query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(cls).On(header.ClassID == cls.ClassID);
                //if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                //{
                //    if (cboFilterByItemType.SelectedValue == "1")
                //        query.Where(item.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID != "0199");
                //    else
                //        query.Where(query.Or(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID == "0199"));
                //}

                query.InnerJoin(unit).On(header.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(cost).On(
                        query.PrescriptionNo == cost.TransactionNo &&
                        query.SequenceNo == cost.SequenceNo
                    );
                //query.LeftJoin(pay).On(
                //    query.PrescriptionNo == pay.TransactionNo &&
                //    query.SequenceNo == pay.SequenceNo &&
                //    pay.IsPaymentProceed == true &&
                //    pay.IsPaymentReturned == false
                //    );
                //query.LeftJoin(payib).On(
                //    cost.IntermBillNo == payib.IntermBillNo &&
                //    payib.IsPaymentProceed == true &&
                //    payib.IsPaymentReturned == false
                //    );

                //------------------------
                query.LeftJoin(queryReff).On(header.ReferenceNo == queryReff.PrescriptionNo &&
                                             query.SequenceNo == queryReff.SequenceNo);
                query.LeftJoin(costReff).On(header.RegistrationNo == costReff.RegistrationNo &&
                                            queryReff.PrescriptionNo == costReff.TransactionNo &&
                                            query.SequenceNo == costReff.SequenceNo);
                //query.LeftJoin(payReff).On(
                //    header.ReferenceNo == payReff.TransactionNo &&
                //    query.SequenceNo == payReff.SequenceNo &&
                //    payReff.IsPaymentProceed == true &&
                //    payReff.IsPaymentReturned == false
                //    );
                //query.LeftJoin(payibReff).On(
                //    costReff.IntermBillNo == payibReff.IntermBillNo &&
                //    payibReff.IsPaymentProceed == true &&
                //    payibReff.IsPaymentReturned == false
                //    );
                //------------------------------
                //query.InnerJoin(view).On(query.PrescriptionNo == view.TransactionNo);
                query.LeftJoin(tpReff).On(header.ReferenceNo == tpReff.PrescriptionNo);


                query.Where(
                        header.RegistrationNo.In(MergeRegistrationList()),
                        header.IsVoid == false,
                        query.Or(query.IsVoid == false, query.CasemixApprovedByUserID.IsNotNull())
                    //query.IsVoid == false
                    );

                if (!(string.IsNullOrEmpty(cboFilterByServiceUnitID.SelectedValue)))
                    query.Where(header.ServiceUnitID == cboFilterByServiceUnitID.SelectedValue);
                if (!(string.IsNullOrEmpty(cboFilterByApprovalStatus.SelectedValue)))
                {
                    query.Where("<ISNULL(a.IsCasemixApproved, CAST(0 AS BIT)) = 0>");
                    query.Where(query.CasemixApprovedByUserID.IsNull(), query.CasemixApprovedDateTime.IsNull());
                }
                //if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                //{
                //    if (cboFilterByPaymentStatus.SelectedValue == "1")
                //        query.Where(query.Or(pay.PaymentNo.IsNotNull(), payib.PaymentNo.IsNotNull()));
                //    else
                //        query.Where(pay.PaymentNo.IsNull(), payib.PaymentNo.IsNull());
                //}
                //if (!(string.IsNullOrEmpty(cboFilterByIntermBillStatus.SelectedValue)))
                //    query.Where(cboFilterByIntermBillStatus.SelectedValue == "1" ? cost.IntermBillNo.IsNotNull() : cost.IntermBillNo.IsNull());
                //if (!string.IsNullOrEmpty(cboFilterByCheckedStatus.SelectedValue))
                //{
                //    if (cboFilterByCheckedStatus.SelectedValue == "1")
                //        query.Where(cost.IsChecked == true);
                //    else
                //        query.Where(query.Or(cost.IsChecked.IsNull(), cost.IsChecked == false));
                //}
                //if (!(string.IsNullOrEmpty(cboFilterByItemGroupID.SelectedValue)))
                //    query.Where(item.ItemGroupID == cboFilterByItemGroupID.SelectedValue);
                //if (!txtTransDate1.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtTransDate2.SelectedDate.ToString().Trim().Equals(string.Empty))
                //    query.Where(view.FilterDate.Date().Between(txtTransDate1.SelectedDate.Value.Date, txtTransDate2.SelectedDate.Value.Date));
                //if (!txtTransDate1.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtTransDate2.SelectedDate.ToString().Trim().Equals(string.Empty))
                //    query.Where(header.PrescriptionDate.Date().Between(txtTransDate1.SelectedDate.Value.Date, txtTransDate2.SelectedDate.Value.Date));

                query.OrderBy(
                    query.PrescriptionNo.Ascending,
                    //view.OrderTransNo.Ascending,
                    query.SequenceNo.Ascending
                    );

                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var presc = new TransPrescriptionItemCollection();
                    presc.Query.Where(presc.Query.PrescriptionNo == row["TransactionNo"], presc.Query.IsApprove == true,
                                      presc.Query.IsBillProceed == true);
                    presc.LoadAll();
                    decimal subTotal = presc.Sum(x => (x.LineAmount ?? 0));

                    //--cek payment
                    var listPaymentNo = string.Empty;

                    var tpioColl = new TransPaymentItemOrderCollection();
                    tpioColl.Query.Where(tpioColl.Query.TransactionNo == row["TransactionNo"].ToString(),
                                         tpioColl.Query.SequenceNo == row["SequenceNo"].ToString(),
                                         tpioColl.Query.IsPaymentProceed == true,
                                         tpioColl.Query.IsPaymentReturned == false);
                    tpioColl.LoadAll();
                    if (tpioColl.Count > 0)
                    {
                        foreach (var tpio in tpioColl)
                        {
                            if (string.IsNullOrEmpty(listPaymentNo))
                                listPaymentNo = tpio.PaymentNo;
                            else listPaymentNo = listPaymentNo + ", " + tpio.PaymentNo;
                        }
                    }
                    else
                    {
                        var ibColl = new TransPaymentItemIntermBillCollection();
                        ibColl.Query.Where(ibColl.Query.IntermBillNo == row["CcIntermBillNo"].ToString(), ibColl.Query.IsPaymentProceed == true,
                                 ibColl.Query.IsPaymentReturned == false);
                        ibColl.LoadAll();
                        if (ibColl.Count > 0)
                        {
                            foreach (var ib in ibColl)
                            {
                                if (string.IsNullOrEmpty(listPaymentNo))
                                    listPaymentNo = ib.PaymentNo;
                                else listPaymentNo = listPaymentNo + ", " + ib.PaymentNo;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(listPaymentNo))
                    {
                        row["IsPaymentProceed"] = true;
                        row["PaymentNo"] = " [" + listPaymentNo + "]";
                    }

                    //--cek payment reference
                    var tpioRefColl = new TransPaymentItemOrderCollection();
                    tpioRefColl.Query.Where(tpioRefColl.Query.TransactionNo == row["ReferenceNo"].ToString(),
                                         tpioRefColl.Query.SequenceNo == row["SequenceNo"].ToString(),
                                         tpioRefColl.Query.IsPaymentProceed == true,
                                         tpioRefColl.Query.IsPaymentReturned == false);
                    tpioRefColl.LoadAll();

                    if (tpioRefColl.Count > 0)
                    {
                        row["IsPaymentProceedReff"] = true;
                    }
                    else
                    {
                        var ibRefColl = new TransPaymentItemIntermBillCollection();
                        ibRefColl.Query.Where(ibRefColl.Query.IntermBillNo == row["CcRefIntermBillNo"].ToString(),
                                              ibRefColl.Query.IsPaymentProceed == true,
                                              ibRefColl.Query.IsPaymentReturned == false);
                        ibRefColl.LoadAll();
                        if (ibRefColl.Count > 0)
                        {
                            row["IsPaymentProceedReff"] = true;
                        }
                    }

                    row["Group"] = row["ServiceUnitName"].ToString() + " - " +
                                   Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date) + " - " +
                                   row["TransactionNo"].ToString() + (Convert.ToBoolean(row["IsUnitDosePrescription"]).Equals(true) ? " [ UDD ]" : (Convert.ToBoolean(row["IsFromSOAP"]).Equals(true) ? " [ ORDER ]" : " [ SALES ]")) +
                                   " - " + row["ClassName"].ToString() + " (Rp. " + string.Format("{0:n2}", (subTotal)) + ")" + row["IntermBillNo"].ToString() + row["PaymentNo"].ToString();


                    //if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                    //{
                    //    if (cboFilterByPaymentStatus.SelectedValue == "1")
                    //    {
                    //        if (string.IsNullOrEmpty(listPaymentNo))
                    //            row.Delete();
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(listPaymentNo))
                    //            row.Delete();
                    //    }
                    //}
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        private DataTable TransChargesItems
        {
            get
            {
                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var header = new TransChargesQuery("c");
                var unit = new ServiceUnitQuery("d");
                var cost = new CostCalculationQuery("e");
                var reg = new RegistrationQuery("f");
                //var pay = new TransPaymentItemOrderQuery("g");
                //var payib = new TransPaymentItemIntermBillQuery("h");
                //var view = new VwTransactionQuery("x");
                var pat = new PatientQuery("i");
                var cls = new ClassQuery("cls");
                var tcReff = new TransChargesQuery("tcReff");


                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = header.TransactionDate.Cast(esCastType.String);

                var std = new AppStandardReferenceItemQuery("z");

                query.Select
                    (
                        query.CasemixNotes.As("Notes"),
                        header.RegistrationNo,
                        query.TransactionNo,
                        query.SequenceNo,
                        //query.IsApprove,
                        @"<CASE WHEN a.IsApprove = 1 AND c.IsApproved = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsApprove>",
                        query.IsVoid,
                        //query.IsBillProceed,
                        @"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBillProceed>",
                        query.IsOrderRealization,
                        query.ItemID,
                        query.ChargeQuantity,
                        query.SRItemUnit,
                        query.Price,
                        query.DiscountAmount,
                        query.CitoAmount,
                        header.LastUpdateByUserID,
                        @"<CASE WHEN e.LastUpdateDateTime = '01/01/1900 00:00:00.000' THEN '' 
                                ELSE e.LastUpdateDateTime END AS 'LastUpdateDateTime'>",
                        //                        @"<CASE WHEN a.IsApprove = 1 AND a.IsBillProceed = 1 THEN 
                        //                                CASE WHEN c.IsCorrection = 1 
                        //                                    THEN 0 - (((ABS(a.ChargeQuantity) * a.Price) - a.DiscountAmount) + a.CitoAmount)
                        //                                    ELSE ((a.ChargeQuantity * a.Price) - a.DiscountAmount) + a.CitoAmount
                        //                                END
                        //                            ELSE 0 END AS Total>",
                        @"<CASE WHEN a.IsBillProceed = 1 AND c.IsBillProceed = 1 THEN 
                                CASE WHEN c.IsCorrection = 1 
                                    THEN 0 - (((ABS(a.ChargeQuantity) * a.Price) - a.DiscountAmount) + a.CitoAmount)
                                    ELSE ((a.ChargeQuantity * a.Price) - a.DiscountAmount) + a.CitoAmount
                                END
                            ELSE 0 END AS Total>",
                        @"<b.[ItemName] + CASE WHEN (
                                     SELECT TOP 1 p.ParamedicName
                                     FROM   TransChargesItemComp tcic
                                            INNER JOIN TariffComponent tc
                                                 ON  tc.TariffComponentID = tcic.TariffComponentID
                                                 AND tc.IsTariffParamedic = 1
                                            LEFT JOIN Paramedic p
                                                 ON  p.ParamedicID = tcic.ParamedicID
                                     WHERE  tcic.TransactionNo = a.TransactionNo
                                            AND tcic.SequenceNo = a.SequenceNo
                                     ORDER BY
                                            tc.TariffComponentID DESC
                                 ) IS NULL THEN ''
                            ELSE ' (' + (
                                     SELECT TOP 1 p.ParamedicName
                                     FROM   TransChargesItemComp tcic
                                            INNER JOIN TariffComponent tc
                                                 ON  tc.TariffComponentID = tcic.TariffComponentID
                                                 AND tc.IsTariffParamedic = 1
                                            LEFT JOIN Paramedic p
                                                 ON  p.ParamedicID = tcic.ParamedicID
                                     WHERE  tcic.TransactionNo = a.TransactionNo
                                            AND tcic.SequenceNo = a.SequenceNo
                                     ORDER BY
                                            tc.TariffComponentID DESC
                            ) + ')'
                        END AS ItemName>",
                        header.TransactionDate,
                        header.ToServiceUnitID.As("ServiceUnitID"),
                        header.ClassID,
                        unit.ServiceUnitName,
                        header.IsOrder,
                        group.As("Group"),
                        "<'' AS ORDERKEY>",
                        "<'1' AS TYPE>",
                        reg.IsHoldTransactionEntry,
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL OR h.PaymentNo IS NOT NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsPaymentProceed>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceed>",
                        //@"<CASE WHEN g.PaymentNo IS NOT NULL THEN '[' + g.PaymentNo + ']' ELSE CASE WHEN h.PaymentNo IS NOT NULL THEN '[' + h.PaymentNo + ']' ELSE '' END END AS PaymentNo>",
                        @"<'' AS PaymentNo>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsIntermBillProceed>",
                        @"<CASE WHEN e.IntermBillNo IS NULL THEN '' ELSE ' - ' + e.IntermBillNo END AS IntermBillNo>",
                        @"<CAST(0 AS BIT) AS IsPaymentProceedReff>",
                        pat.PatientName,
                        //view.IsCorrection,
                        //view.OrderDate,
                        //view.OrderTransNo,
                        header.IsCorrection,
                        @"<ISNULL(tcReff.TransactionDate, c.TransactionDate) AS OrderDate>",
                        @"<ISNULL(tcReff.TransactionNo, c.TransactionNo) AS OrderTransNo>",
                        header.ExecutionDate,
                        cls.ClassName,
                        query.IsCorrection.As("IsCorrected"),
                        //@"<CAST(0 AS BIT) AS IsCorrected>",
                        std.ItemName.Coalesce("''").As("DiscountReason"),
                        @"<ISNULL(e.IntermBillNo, '') AS CcIntermBillNo>",
                        //@"<ISNULL(a.IsCasemixApproved, CAST(0 AS BIT)) AS IsCasemixApproved>",
                        query.IsCasemixApproved,
                        query.CasemixApprovedDateTime,
                        query.CasemixApprovedByUserID,
                        header.CreatedDateTime
                    );

                query.LeftJoin(std).On(query.SRDiscountReason == std.ItemID && std.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString());

                query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
                query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(cls).On(header.ClassID == cls.ClassID);
                //if (!string.IsNullOrEmpty(cboFilterByItemType.SelectedValue))
                //{
                //    if (cboFilterByItemType.SelectedValue == "1")
                //        query.Where(item.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID != "0199");
                //    else
                //        query.Where(query.Or(item.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen), item.ItemGroupID == "0199"));
                //}

                query.InnerJoin(unit).On(header.ToServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(cost).On(
                        query.TransactionNo == cost.TransactionNo &&
                        query.SequenceNo == cost.SequenceNo
                    );
                //query.LeftJoin(pay).On(
                //    query.TransactionNo == pay.TransactionNo &&
                //    query.SequenceNo == pay.SequenceNo &&
                //    pay.IsPaymentProceed == true &&
                //    pay.IsPaymentReturned == false
                //    );
                //query.LeftJoin(payib).On(
                //    cost.IntermBillNo == payib.IntermBillNo &&
                //    payib.IsPaymentProceed == true &&
                //    payib.IsPaymentReturned == false
                //    );
                //query.InnerJoin(view).On(query.TransactionNo == view.TransactionNo);
                query.LeftJoin(tcReff).On(header.ReferenceNo == tcReff.TransactionNo);

                query.Where(
                        header.RegistrationNo.In(MergeRegistrationList()),
                         query.Or(
                            header.PackageReferenceNo == string.Empty,
                            header.PackageReferenceNo.IsNull()
                            ),
                        header.IsVoid == false,
                        query.Or(query.IsVoid == false, query.CasemixApprovedByUserID.IsNotNull()),
                        //query.IsVoid == false,
                        query.Or(
                        query.ParentNo == string.Empty,
                        query.ParentNo.IsNull()
                        )
                    );

                if (!(string.IsNullOrEmpty(cboFilterByServiceUnitID.SelectedValue)))
                    query.Where(header.ToServiceUnitID == cboFilterByServiceUnitID.SelectedValue);
                if (!(string.IsNullOrEmpty(cboFilterByApprovalStatus.SelectedValue)))
                {
                    query.Where("<ISNULL(a.IsCasemixApproved, CAST(0 AS BIT)) = 0>");
                    query.Where(query.CasemixApprovedByUserID.IsNull(), query.CasemixApprovedDateTime.IsNull());
                }
                //if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                //{
                //    if (cboFilterByPaymentStatus.SelectedValue == "1")
                //        query.Where(query.Or(pay.PaymentNo.IsNotNull(), payib.PaymentNo.IsNotNull()));
                //    else
                //        query.Where(pay.PaymentNo.IsNull(), payib.PaymentNo.IsNull());
                //}
                //if (!(string.IsNullOrEmpty(cboFilterByIntermBillStatus.SelectedValue)))
                //{
                //    if (cboFilterByIntermBillStatus.SelectedValue == "1")
                //        query.Where(cost.IntermBillNo.IsNotNull());
                //    else
                //        query.Where(cost.IntermBillNo.IsNull());
                //}
                //if (!string.IsNullOrEmpty(cboFilterByCheckedStatus.SelectedValue))
                //{
                //    if (cboFilterByCheckedStatus.SelectedValue == "1")
                //        query.Where(cost.IsChecked == true);
                //    else
                //        query.Where(query.Or(cost.IsChecked.IsNull(), cost.IsChecked == false));
                //}
                //if (!(string.IsNullOrEmpty(cboFilterByItemGroupID.SelectedValue)))
                //    query.Where(item.ItemGroupID == cboFilterByItemGroupID.SelectedValue);
                //if (!txtTransDate1.IsEmpty && !txtTransDate2.IsEmpty)
                //    query.Where(view.FilterDate.Date().Between(txtTransDate1.SelectedDate.Value.Date, txtTransDate2.SelectedDate.Value.Date));

                //if (!txtTransDate1.IsEmpty && !txtTransDate2.IsEmpty)
                //    query.Where(@"<ISNULL(tcReff.ExecutionDate, c.ExecutionDate) >= '" + txtTransDate1.SelectedDate.Value.Date + "' AND ISNULL(tcReff.ExecutionDate, c.ExecutionDate) <= '" + txtTransDate2.SelectedDate.Value.Date + "'>");

                query.OrderBy
                    (
                        query.TransactionNo.Ascending,
                        //view.OrderTransNo.Ascending,
                        query.SequenceNo.Ascending
                    );

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var charges = new TransChargesItemCollection();
                    charges.Query.Where(charges.Query.TransactionNo == row["TransactionNo"], charges.Query.IsVoid == false);
                    charges.LoadAll();
                    decimal subTotal = 0;
                    foreach (var x in charges)
                    {
                        if ((bool)row["IsApprove"] & (bool)row["IsBillProceed"])
                        {
                            if ((bool)row["IsCorrection"])
                                subTotal += (0 -
                                             ((Math.Abs(x.ChargeQuantity ?? 0) * (x.Price ?? 0)) - (x.DiscountAmount ?? 0) +
                                              (x.CitoAmount ?? 0)));
                            else
                                subTotal += (((x.ChargeQuantity ?? 0) * (x.Price ?? 0)) - (x.DiscountAmount ?? 0) + (x.CitoAmount ?? 0));
                        }
                    }


                    //--cek payment
                    var listPaymentNo = string.Empty;

                    var tpioColl = new TransPaymentItemOrderCollection();
                    tpioColl.Query.Where(tpioColl.Query.TransactionNo == row["TransactionNo"].ToString(),
                                         tpioColl.Query.SequenceNo == row["SequenceNo"].ToString(),
                                         tpioColl.Query.IsPaymentProceed == true,
                                         tpioColl.Query.IsPaymentReturned == false);
                    tpioColl.LoadAll();
                    if (tpioColl.Count > 0)
                    {
                        foreach (var tpio in tpioColl)
                        {
                            if (string.IsNullOrEmpty(listPaymentNo))
                                listPaymentNo = tpio.PaymentNo;
                            else listPaymentNo = listPaymentNo + ", " + tpio.PaymentNo;
                        }
                    }
                    else
                    {
                        var ibColl = new TransPaymentItemIntermBillCollection();
                        ibColl.Query.Where(ibColl.Query.IntermBillNo == row["CcIntermBillNo"].ToString(), ibColl.Query.IsPaymentProceed == true,
                                 ibColl.Query.IsPaymentReturned == false);
                        ibColl.LoadAll();
                        if (ibColl.Count > 0)
                        {
                            foreach (var ib in ibColl)
                            {
                                if (string.IsNullOrEmpty(listPaymentNo))
                                    listPaymentNo = ib.PaymentNo;
                                else listPaymentNo = listPaymentNo + ", " + ib.PaymentNo;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(listPaymentNo))
                    {
                        row["IsPaymentProceed"] = true;
                        row["PaymentNo"] = " [" + listPaymentNo + "]";
                    }

                    row["Group"] = row["ServiceUnitName"] + " - " +
                                  Convert.ToDateTime(row["Group"]).ToString(AppConstant.DisplayFormat.Date) + " - " +
                                  row["TransactionNo"] + " - " + row["ClassName"] + " (Rp. " + string.Format("{0:n2}", (subTotal)) + ")" + row["IntermBillNo"] + row["PaymentNo"];
                    if ((bool)row["IsOrder"] && !(bool)row["IsOrderRealization"])
                        row["Total"] = 0D;

                    //if (!(string.IsNullOrEmpty(cboFilterByPaymentStatus.SelectedValue)))
                    //{
                    //    if (cboFilterByPaymentStatus.SelectedValue == "1")
                    //    {
                    //        if (string.IsNullOrEmpty(listPaymentNo))
                    //            row.Delete();
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(listPaymentNo))
                    //            row.Delete();
                    //    }
                    //}
                }

                tbl.AcceptChanges();

                var chargeBedItems = new ServiceRoomCollection();
                chargeBedItems.Query.Where(chargeBedItems.Query.ItemID != string.Empty);
                chargeBedItems.LoadAll();

                var chargeBeds = tbl.AsEnumerable().Where(t => chargeBedItems.Select(c => c.ItemID).Contains(t.Field<string>("ItemID")));
                if (chargeBedItems != null)
                {
                    foreach (DataRow chargeBed in chargeBeds)
                    {
                        chargeBed["ItemName"] = chargeBed["ItemName"].ToString() + " (" + chargeBed["PatientName"].ToString() + ")";
                    }
                }

                return tbl;
            }
        }

        protected string GetStatus(object isOrder, object IsOrderRealization, object IsApprove)
        {
            if (IsApprove.Equals(false))
                return "<img src=\"../../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
            else
            {
                if (isOrder.Equals(false))
                    return "<img src=\"../../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                else
                {
                    if (IsOrderRealization.Equals(false))
                        return "<img src=\"../../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
                    else
                        return "<img src=\"../../../../../Images/Toolbar/post16.png\" border=\"0\" />";
                }
            }
        }

        protected void cboFilterByServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdTransChargesItem.Rebind();
        }

        private void InitializeCboFilterByServiceUnitID()
        {
            string[] regList = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            var suQuery = new ServiceUnitQuery("a");
            var trQuery = new TransChargesQuery("b");
            suQuery.InnerJoin(trQuery).On(suQuery.ServiceUnitID == trQuery.ToServiceUnitID &&
                                            trQuery.RegistrationNo.In(regList));
            suQuery.es.Distinct = true;
            suQuery.Select(suQuery.ServiceUnitID, suQuery.ServiceUnitName);
            suQuery.Where
                (
                    suQuery.SRRegistrationType.In(
                        AppConstant.RegistrationType.ClusterPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.OutPatient
                    ),
                    suQuery.IsActive == true
                );
            DataTable dtb1 = suQuery.LoadDataTable();

            var suQuery2 = new ServiceUnitQuery("a");
            var trQuery2 = new TransPrescriptionQuery("b");
            suQuery2.InnerJoin(trQuery2).On(suQuery2.ServiceUnitID == trQuery2.ServiceUnitID &&
                                          trQuery2.RegistrationNo.In(regList));
            suQuery2.es.Distinct = true;
            suQuery2.Select(suQuery2.ServiceUnitID, suQuery2.ServiceUnitName);
            suQuery2.Where(suQuery2.IsActive == true);
            DataTable dtb2 = suQuery2.LoadDataTable();

            DataTable tbl = dtb1;
            tbl.Merge(dtb2);

            cboFilterByServiceUnitID.Items.Add(new RadComboBoxItem("--All--", string.Empty));
            foreach (DataRow row in tbl.Rows)
            {
                cboFilterByServiceUnitID.Items.Add(new RadComboBoxItem(row["ServiceUnitName"].ToString(), row["ServiceUnitID"].ToString()));
            }
        }

        #region Blood Transfusion Request

        protected void cboFilterApprovalBloodRequest_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdBloodRequest.Rebind();
        }

        protected void grdBloodRequest_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBloodRequest.DataSource = BloodBankTransactions;
        }

        private DataTable BloodBankTransactions
        {
            get
            {
                var query = new BloodBankTransactionQuery("a");
                var reg = new RegistrationQuery("b");
                var pat = new PatientQuery("c");
                var btype = new AppStandardReferenceItemQuery("d");
                var bgroup = new AppStandardReferenceItemQuery("e");
                var usr = new AppUserQuery("f");

                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
                query.LeftJoin(btype).On(pat.SRBloodType == btype.ItemID &&
                                         btype.StandardReferenceID == AppEnum.StandardReference.BloodType);
                query.InnerJoin(bgroup).On(query.SRBloodGroupRequest == bgroup.ItemID &&
                                          bgroup.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.InnerJoin(usr).On(query.OfficerByUserID == usr.UserID);
                query.Select
                    (
                        query.RegistrationNo,
                        query.TransactionNo,
                        query.TransactionDate,
                        query.RequestDate,
                        query.RequestTime,
                        query.BloodBankNo,
                        query.PdutNo,
                        btype.ItemName.As("BloodType"),
                        pat.BloodRhesus,
                        bgroup.ItemName.As("BloodGroup"),
                        query.QtyBagRequest,
                        query.VolumeBag,
                        usr.UserName.As("Officer"),
                        @"<ISNULL(a.IsValidatedByCasemix, 1) AS IsValidatedByCasemix>",
                        query.ValidatedByCasemixDateTime,
                        query.ValidatedByCasemixUserID,
                        query.CasemixNotes,
                        query.IsApproved,
                        query.IsVoid,
                        query.QtyBagCasemixAppr,
                        @"<CASE WHEN (ISNULL(a.IsValidatedByCasemix, 1) = 1 OR (ISNULL(a.IsValidatedByCasemix, 1) = 0 AND a.ValidatedByCasemixUserID IS NOT NULL)) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS 'IsVisible' >"
                    );

                query.Where(query.RegistrationNo.In(MergeRegistrationList()), query.IsApproved == true);
                if (!(string.IsNullOrEmpty(cboFilterApprovalBloodRequest.SelectedValue)))
                {
                    query.Where("<ISNULL(a.IsValidatedByCasemix, CAST(1 AS BIT)) = 0>");
                    query.Where(query.ValidatedByCasemixUserID.IsNull(), query.ValidatedByCasemixDateTime.IsNull());
                }

                query.OrderBy(query.TransactionNo.Descending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }
        #endregion

        #region Plafond Balance

        //private void ResetSessionPlafond()
        //{
        //    Session["reg"] = null;
        //    Session["regnos"] = null;
        //    Session["cobPlafond"] = null;
        //    Session["patientamt"] = null;
        //    Session["guarantoramt"] = null;
        //    Session["guarantortot"] = null;
        //    Session["patienttot"] = null;
        //}

        protected decimal PlafondValueUsedInPercent()
        {
            decimal totalPlafond = TotalPlafond;
            if (totalPlafond == 0)
                totalPlafond = 1;

            var plafonUsedPercent = (TotalGuarantorAndRemainingPatientAmount / totalPlafond) * (decimal)100;
            return plafonUsedPercent;
        }

        private Registration Registration
        {
            get
            {
                var reg = new Registration();
                //if (Session["reg"] != null)
                //{
                //    reg = (Registration)Session["reg"];
                //    if (reg.RegistrationNo == txtRegistrationNo.Text)
                //        return reg;
                //}

                reg.LoadByPrimaryKey(txtRegistrationNo.Text);
                //Session["reg"] = reg;
                return reg;
            }
        }

        private string[] MergeRegistrationNos
        {
            get
            {
                //var regnos = new string[0];
                //if (Session["regnos"] != null)
                //{
                //    regnos = (string[])Session["regnos"];
                //    if (Registration.RegistrationNo == txtRegistrationNo.Text)
                //        return regnos;
                //}
                //regnos = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                //Session["regnos"] = regnos;
                return Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
            }
        }

        protected decimal TotalPlafond
        {
            get
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                return reg.PlavonAmount ?? 0;
            }
        }

        private decimal AdditionalPlafond
        {
            get
            {
                decimal cobPlafond = 0;
                //if (Session["cobPlafond"] != null)
                //{
                //    if (Registration.RegistrationNo == txtRegistrationNo.Text)
                //    {
                //        cobPlafond = (decimal)Session["cobPlafond"];
                //        return cobPlafond;
                //    }
                //}
                var cob = new RegistrationGuarantorCollection();
                cob.Query.Where(cob.Query.RegistrationNo == txtRegistrationNo.Text);
                cob.LoadAll();
                cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
                //Session["cobPlafond"] = cobPlafond;
                return cobPlafond;
            }
        }

        protected decimal TotalGuarantorAndRemainingPatientAmount
        {
            get
            {
                return Helper.CostCalculation.GetBillingTotalAllTransaction(MergeRegistrationNos);
            }
        }

        #endregion

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            //// Patient Photo
            //imgPatientPhoto.ImageUrl = string.Empty;

            //// Load from database
            //var patientImg = new PatientImage();
            //if (patientImg.LoadByPrimaryKey(patientID))
            //{
            //    // Show Image
            //    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
            //        Convert.ToBase64String(patientImg.Photo));
            //}

            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = txtGender.Text == "M" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }
        #endregion

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdTransChargesItem.MasterTableView.Items)
            {
                var chk = ((CheckBox)dataItem.FindControl("detailChkbox"));
                if (!chk.Visible) continue;
                chk.Checked = selected;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (string.IsNullOrEmpty(eventArgument)) return;

            RegistrationPathwayCollection rpc;

            var args = eventArgument.Split('|').Count() > 0 ? eventArgument.Split('|')[0] : eventArgument;
            switch (args)
            {
                case "mergeDocuments":
                    MergeFiles();
                    grdDocument.Rebind();
                    break;
                case "rebindDiagnose":
                    PopulateEpisodeDiagnoseGrid();
                    PopulateEpisodeProcedureGrid();
                    break;
                case "approve":
                case "reject":
                    ApproveRejectTransaction(eventArgument);
                    break;
                case "approve2":
                case "reject2":
                    ApproveRejectBloodRequest(eventArgument);
                    break;
                case "rebindDocument":
                    grdDocument.Rebind();
                    break;
                case "rebindBilling":
                    grdTransChargesItem.Rebind();
                    break;
                case "clearPathway":
                    using (var trans = new esTransactionScope())
                    {
                        rpc = new RegistrationPathwayCollection();
                        rpc.Query.Where(rpc.Query.RegistrationNo == txtRegistrationNo.Text);
                        if (rpc.Query.Load())
                        {
                            foreach (var rp in rpc)
                            {
                                var rpic = new RegistrationPathwayItemCollection();
                                rpic.Query.Where(rpic.Query.RegistrationNo == txtRegistrationNo.Text, rpic.Query.PathwayID == rp.PathwayID);
                                rpic.Query.Load();
                                foreach (var rpi in rpic)
                                {
                                    var rpiec = new RegistrationPathwayItemExecutionCollection();
                                    rpiec.Query.Where(rpiec.Query.RegistrationNo == txtRegistrationNo.Text, rpiec.Query.PathwayID == rpi.PathwayID);
                                    rpiec.Query.Load();
                                    rpiec.MarkAllAsDeleted();
                                    rpiec.Save();
                                }
                                rpic.MarkAllAsDeleted();
                                rpic.Save();
                            }
                            rpc.MarkAllAsDeleted();
                            rpc.Save();
                        }

                        trans.Complete();
                    }

                    PopulatePathwayItemGrid();
                    break;
                case "rebindPathway":
                    rpc = new RegistrationPathwayCollection();
                    rpc.Query.Where(rpc.Query.RegistrationNo == txtRegistrationNo.Text);
                    if (rpc.Query.Load())
                    {
                        foreach (var rp in rpc)
                        {
                            var rpic = new RegistrationPathwayItemCollection();
                            rpic.Query.Where(rpic.Query.RegistrationNo == txtRegistrationNo.Text, rpic.Query.PathwayID == rp.PathwayID);
                            if (!rpic.Query.Load()) continue;

                            var pic = new PathwayItemCollection();
                            pic.Query.Where(pic.Query.PathwayID == rp.PathwayID && pic.Query.PathwayItemSeqNo.NotIn(rpic.Select(r => r.PathwayItemSeqNo)));
                            if (pic.Query.Load())
                            {
                                var rpiec = new RegistrationPathwayItemExecutionCollection();

                                foreach (var entity in pic)
                                {
                                    var rpi = rpic.AddNew();
                                    rpi.RegistrationNo = rp.RegistrationNo;
                                    rpi.PathwayID = rp.PathwayID;
                                    rpi.PathwayItemSeqNo = entity.PathwayItemSeqNo;
                                    rpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    rpi.LastUpdateDateTime = DateTime.Now;

                                    var piec = new PathwayItemExecutionCollection();
                                    piec.Query.Where(piec.Query.PathwayID == rp.PathwayID, piec.Query.PathwayItemSeqNo == entity.PathwayItemSeqNo);
                                    piec.Query.Load();

                                    foreach (var entity2 in piec)
                                    {
                                        var rpie = rpiec.AddNew();
                                        rpie.RegistrationNo = rp.RegistrationNo;
                                        rpie.PathwayID = rp.PathwayID;
                                        rpie.PathwayItemSeqNo = entity.PathwayItemSeqNo;
                                        rpie.DayNo = entity2.DayNo;
                                        rpie.IsApprove = false;
                                        rpie.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        rpie.LastUpdateDateTime = DateTime.Now;
                                    }
                                }
                                using (var trans = new esTransactionScope())
                                {
                                    rp.Save();
                                    rpic.Save();
                                    rpiec.Save();

                                    trans.Complete();
                                }
                            }
                        }

                        PopulatePathwayItemGrid();
                    }
                    break;
                case "rebindAll":
                    PopulatePlafonBar();
                    InitializeCboFilterByServiceUnitID();
                    CalculateDetailTransactionByGroup();
                    grdTransChargesItem.Rebind();
                    grdBloodRequest.Rebind();
                    PopulateEpisodeDiagnoseGrid();
                    PopulateEpisodeProcedureGrid();
                    PopulatePathwayItemGrid();
                    grdDocument.Rebind();
                    PopulateRegistrationGuarantorGrid();
                    break;
            }
        }

        private void ApproveRejectTransaction(string command)
        {
            var param = command.Split('|');
            if (param[1] == "1")
            {
                var tci = new TransChargesItem();
                tci.LoadByPrimaryKey(param[2], param[3]);
                tci.IsCasemixApproved = param[0] == "approve" ? true : false;
                tci.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                tci.CasemixApprovedDateTime = DateTime.Now;
                tci.CasemixNotes = param[4];
                tci.IsVoid = param[0] == "approve" ? false : true;
                tci.Save();
            }
            else
            {
                var tpi = new TransPrescriptionItem();
                tpi.LoadByPrimaryKey(param[2], param[3]);
                tpi.IsCasemixApproved = param[0] == "approve" ? true : false; ;
                tpi.CasemixApprovedByUserID = AppSession.UserLogin.UserID;
                tpi.CasemixApprovedDateTime = DateTime.Now;
                tpi.CasemixNotes = param[4];
                //db: 20231204 - rejcet tidak auto void, supaya tetap muncul di list (main di qty taken di-nol - kan)
                //tpi.IsVoid = param[0] == "approve" ? false : true; 
                tpi.Save();
            }

            grdTransChargesItem.Rebind();
        }

        private void ApproveRejectBloodRequest(string command)
        {
            var param = command.Split('|');
            var bb = new BloodBankTransaction();
            bb.LoadByPrimaryKey(param[1]);
            bb.IsValidatedByCasemix = param[0] == "approve2" ? true : false;
            bb.ValidatedByCasemixUserID = AppSession.UserLogin.UserID;
            bb.ValidatedByCasemixDateTime = DateTime.Now;
            bb.CasemixNotes = param[2];
            bb.Save();

            grdBloodRequest.Rebind();
        }

        #region Record Detail Method Function Episode Diagnose

        private EpisodeDiagnoseCollection EpisodeDiagnoses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeDiagnose"];
                    if (obj != null)
                        return ((EpisodeDiagnoseCollection)(obj));
                }

                var coll = new EpisodeDiagnoseCollection();
                var query = new EpisodeDiagnoseQuery("a");
                var diag = new DiagnoseQuery("b");
                var item = new AppStandardReferenceItemQuery("e");
                var morph = new MorphologyQuery("c");
                var param = new ParamedicQuery("d");

                query.LeftJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
                query.InnerJoin(item).On(query.SRDiagnoseType == item.ItemID);
                query.LeftJoin(morph).On(query.MorphologyID == morph.MorphologyID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);

                query.Select
                    (
                        query.SelectAll(),
                        diag.DiagnoseName.As("refToDiagnose_DiagnoseName"),
                        item.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"),
                        morph.MorphologyName.As("refToMorphology_MorphologyName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName")
                    );

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(query.SRDiagnoseType.Ascending, query.DiagnoseID.Ascending);

                coll.Load(query);

                Session["collEpisodeDiagnose"] = coll;
                return coll;
            }
            set { Session["collEpisodeDiagnose"] = value; }
        }

        private void PopulateEpisodeDiagnoseGrid()
        {
            EpisodeDiagnoses = null; //Reset Record Detail
            grdEpisodeDiagnose.DataSource = EpisodeDiagnoses;
            grdEpisodeDiagnose.MasterTableView.IsItemInserted = false;
            grdEpisodeDiagnose.MasterTableView.ClearEditItems();
            grdEpisodeDiagnose.DataBind();
        }

        protected void grdEpisodeDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeDiagnose.DataSource = EpisodeDiagnoses;
        }

        protected void grdEpisodeDiagnose_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EpisodeDiagnoseMetadata.ColumnNames.SequenceNo]);
            EpisodeDiagnose entity = FindEpisodeDiagnose(sequenceNo);
            if (entity != null)
            {
                SetEntityValueDiagnose(entity, e);
                EpisodeDiagnoses.Save();
            }
        }

        protected void grdEpisodeDiagnose_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EpisodeDiagnoseMetadata.ColumnNames.SequenceNo]);
            EpisodeDiagnose entity = FindEpisodeDiagnose(sequenceNo);
            if (entity != null)
            {
                entity.IsVoid = true;
                EpisodeDiagnoses.Save();
            }
        }

        protected void grdEpisodeDiagnose_InsertCommand(object source, GridCommandEventArgs e)
        {
            EpisodeDiagnose entity = EpisodeDiagnoses.AddNew();
            SetEntityValueDiagnose(entity, e);
            EpisodeDiagnoses.Save();

            e.Canceled = true;
            grdEpisodeDiagnose.Rebind();
        }

        private EpisodeDiagnose FindEpisodeDiagnose(String sequenceNo)
        {
            EpisodeDiagnoseCollection coll = EpisodeDiagnoses;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValueDiagnose(EpisodeDiagnose entity, GridCommandEventArgs e)
        {
            var userControl = (Temiang.Avicenna.Module.RADT.MedicalRecord.EpisodeDiagDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.DiagnoseID = userControl.DiagnoseID;
                entity.DiagnoseName = userControl.DiagnoseName;
                entity.SRDiagnoseType = userControl.SRDiagnoseType;
                entity.DiagnoseType = userControl.DiagnoseType;
                entity.DiagnosisText = userControl.DiagnosisText;
                entity.MorphologyID = userControl.MorphologyID;
                entity.MorphologyName = userControl.MorphologyName;
                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.IsAcuteDisease = userControl.IsAcuteDisease;
                entity.IsChronicDisease = userControl.IsChronicDisease;
                entity.IsOldCase = userControl.IsOldCase;
                entity.IsConfirmed = userControl.IsConfirmed;
                entity.IsVoid = userControl.IsVoid;
                entity.Notes = userControl.Notes;
                entity.ExternalCauseID = userControl.ExternalCauseID;

                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
                }
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        protected void grdEpisodeDiagnose_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                EpisodeDiagnose item = EpisodeDiagnoses[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region Record Detail Method Function Episode Procedure

        private EpisodeProcedureCollection EpisodeProcedures
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeProcedure"];
                    if (obj != null)
                        return ((EpisodeProcedureCollection)(obj));
                }

                var coll = new EpisodeProcedureCollection();
                var query = new EpisodeProcedureQuery("a");
                var param = new ParamedicQuery("b");
                var proc = new ProcedureQuery("c");

                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);

                query.Select
                     (
                         query.SelectAll(),
                         param.ParamedicName.As("refToParamedic_ParamedicName")
                     );

                query.Where(query.RegistrationNo == txtRegistrationNo.Text, query.IsFromOperatingRoom == true);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEpisodeProcedure"] = coll;
                return coll;
            }
            set { Session["collEpisodeProcedure"] = value; }
        }

        private void PopulateEpisodeProcedureGrid()
        {
            EpisodeProcedures = null; //Reset Record Detail
            grdEpisodeProcedure.DataSource = EpisodeProcedures;
            grdEpisodeProcedure.MasterTableView.IsItemInserted = false;
            grdEpisodeProcedure.MasterTableView.ClearEditItems();
            grdEpisodeProcedure.DataBind();
        }

        protected void grdEpisodeProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeProcedure.DataSource = EpisodeProcedures;
        }

        protected void grdEpisodeProcedure_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedure(sequenceNo);
            if (entity != null)
            {
                SetEntityValueEpisodeProcedure(entity, e);
                EpisodeProcedures.Save();
            }
        }

        protected void grdEpisodeProcedure_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][EpisodeProcedureMetadata.ColumnNames.SequenceNo]);
            EpisodeProcedure entity = FindEpisodeProcedure(sequenceNo);
            if (entity != null)
            {
                entity.IsVoid = true;
                EpisodeProcedures.Save();
            }
        }

        protected void grdEpisodeProcedure_InsertCommand(object source, GridCommandEventArgs e)
        {
            EpisodeProcedure entity = EpisodeProcedures.AddNew();
            SetEntityValueEpisodeProcedure(entity, e);
            EpisodeProcedures.Save();

            e.Canceled = true;
            grdEpisodeProcedure.Rebind();
        }

        protected void grdEpisodeProcedure_EditCommand(object source, GridCommandEventArgs e)
        {
            //EpisodeProcedure entity = EpisodeProcedures.AddNew();
            //SetEntityValueEpisodeProcedure(entity, e);

            e.Canceled = true;
            //grdEpisodeProcedure.Rebind();
        }

        private EpisodeProcedure FindEpisodeProcedure(String sequenceNo)
        {
            EpisodeProcedureCollection coll = EpisodeProcedures;
            return coll.FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private void SetEntityValueEpisodeProcedure(EpisodeProcedure entity, GridCommandEventArgs e)
        {
            var userControl = (Temiang.Avicenna.Module.RADT.MedicalRecord.EpisodeProcDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.SequenceNo = userControl.SequenceNo;
                entity.ProcedureDate = userControl.ProcedureDate;
                entity.ProcedureTime = userControl.ProcedureTime;
                entity.ProcedureDate2 = userControl.ProcedureDate2;
                entity.ProcedureTime2 = userControl.ProcedureTime2;
                entity.ParamedicID = userControl.ParamedicID;
                entity.ParamedicName = userControl.ParamedicName;
                entity.ParamedicID2 = userControl.ParamedicID2;
                entity.ProcedureID = userControl.ProcedureID;
                entity.ProcedureName = userControl.ProcedureName;
                entity.SRProcedureCategory = userControl.SRProcedureCategory;
                entity.SRAnestesi = userControl.SRAnestesi;
                entity.RoomID = userControl.RoomID;
                entity.IsCito = userControl.IsCito;
                entity.IsVoid = userControl.IsVoid;
                entity.AssistantID1 = userControl.AssistantID1;
                entity.AssistantID2 = userControl.AssistantID2;
                entity.BookingNo = userControl.BookingNo;
                entity.ParamedicID2a = userControl.ParamedicID2a;
                entity.ParamedicID3a = userControl.ParamedicID3a;
                entity.ParamedicID4a = userControl.ParamedicID4a;
                entity.IsFromOperatingRoom = true;
                entity.OpNotesSeqNo = userControl.OpNotesSeqNo;

                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = (new DateTime()).NowAtSqlServer();
                }
                //Last Update Status
                if (entity.es.IsAdded || entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }
            }
        }

        protected void grdEpisodeProcedure_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                EpisodeProcedure item = EpisodeProcedures[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid ?? false)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        #endregion

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (EpisodeDiagnoses.Any(d => d.SRDiagnoseType == "DiagnoseType-001" && (d.IsConfirmed ?? false) && !(d.IsVoid ?? false))) 
            grdList.DataSource = PathwayItems;
            //else grdList.DataSource = null;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                for (int i = 1; i <= 7; i++)
                {
                    var col = dataBoundItem["col_" + i.ToString()];
                    var lbtn = col.FindControl("lbtn_" + i.ToString()) as LinkButton;
                    if (lbtn == null) continue;
                    switch (lbtn.CommandName)
                    {
                        case "01":
                            col.ForeColor = Color.Red;
                            col.BackColor = Color.Red;
                            break;
                        case "02":
                            col.ForeColor = Color.Yellow;
                            col.BackColor = Color.Yellow;
                            break;
                    }
                }

                //var col_1 = dataBoundItem["col_1"];
                //var lbtn_1 = col_1.FindControl("lbtn_1") as LinkButton;

                //if (lbtn_1.CommandName == "01")
                //{
                //    dataBoundItem["col_1"].ForeColor = Color.Red;
                //    dataBoundItem["col_1"].BackColor = Color.Red;
                //}
                //else if (lbtn_1.CommandName == "02")
                //{
                //    dataBoundItem["col_1"].ForeColor = Color.Yellow;
                //    dataBoundItem["col_1"].BackColor = Color.Yellow;
                //}

                //if (dataBoundItem["col_2"].Text == "01")
                //{
                //    dataBoundItem["col_2"].ForeColor = Color.Red;
                //    dataBoundItem["col_2"].BackColor = Color.Red;
                //}
                //else if (dataBoundItem["col_2"].Text == "02")
                //{
                //    dataBoundItem["col_2"].ForeColor = Color.Yellow;
                //    dataBoundItem["col_2"].BackColor = Color.Yellow;
                //}

                //if (dataBoundItem["col_3"].Text == "01")
                //{
                //    dataBoundItem["col_3"].ForeColor = Color.Red;
                //    dataBoundItem["col_3"].BackColor = Color.Red;
                //}
                //else if (dataBoundItem["col_3"].Text == "02")
                //{
                //    dataBoundItem["col_3"].ForeColor = Color.Yellow;
                //    dataBoundItem["col_3"].BackColor = Color.Yellow;
                //}

                //if (dataBoundItem["col_4"].Text == "01")
                //{
                //    dataBoundItem["col_4"].ForeColor = Color.Red;
                //    dataBoundItem["col_4"].BackColor = Color.Red;
                //}
                //else if (dataBoundItem["col_4"].Text == "02")
                //{
                //    dataBoundItem["col_4"].ForeColor = Color.Yellow;
                //    dataBoundItem["col_4"].BackColor = Color.Yellow;
                //}

                //if (dataBoundItem["col_5"].Text == "01")
                //{
                //    dataBoundItem["col_5"].ForeColor = Color.Red;
                //    dataBoundItem["col_5"].BackColor = Color.Red;
                //}
                //else if (dataBoundItem["col_5"].Text == "02")
                //{
                //    dataBoundItem["col_5"].ForeColor = Color.Yellow;
                //    dataBoundItem["col_5"].BackColor = Color.Yellow;
                //}

                //if (dataBoundItem["col_6"].Text == "01")
                //{
                //    dataBoundItem["col_6"].ForeColor = Color.Red;
                //    dataBoundItem["col_6"].BackColor = Color.Red;
                //}
                //else if (dataBoundItem["col_6"].Text == "02")
                //{
                //    dataBoundItem["col_6"].ForeColor = Color.Yellow;
                //    dataBoundItem["col_6"].BackColor = Color.Yellow;
                //}

                //if (dataBoundItem["col_7"].Text == "01")
                //{
                //    dataBoundItem["col_7"].ForeColor = Color.Red;
                //    dataBoundItem["col_7"].BackColor = Color.Red;
                //}
                //else if (dataBoundItem["col_7"].Text == "02")
                //{
                //    dataBoundItem["col_7"].ForeColor = Color.Yellow;
                //    dataBoundItem["col_7"].BackColor = Color.Yellow;
                //}
            }
        }

        protected void grdBloodRequest_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "approve")
            {
                GridItem item = e.Item as GridItem;
                if (item == null)
                    return;

                double qtyAppr = ((RadNumericTextBox)item.FindControl("txtQtyBagCasemixAppr")).Value ?? 0;

                if (qtyAppr == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validate", "alert('Qty Approve is required');", true);
                    return;
                }

                var transactionNo =
                    Convert.ToString(
                        item.OwnerTableView.DataKeyValues[item.ItemIndex][BloodBankTransactionMetadata.ColumnNames.TransactionNo]);

                var bb = new BloodBankTransaction();
                if (bb.LoadByPrimaryKey(transactionNo))
                {
                    bb.IsValidatedByCasemix = true;
                    bb.ValidatedByCasemixUserID = AppSession.UserLogin.UserID;
                    bb.ValidatedByCasemixDateTime = DateTime.Now;
                    bb.CasemixNotes = string.Empty;
                    bb.QtyBagCasemixAppr = Convert.ToInt16(qtyAppr);
                    bb.Save();
                }

                grdBloodRequest.Rebind();
            }
        }

        private DataTable PathwayItems
        {
            get
            {
                //string sessName = "collPathwayItemCollection";
                //if (IsPostBack)
                //{
                //    object obj = Session[sessName];
                //    if (obj != null) return ((RegistrationPathwayItemCollection)(obj));
                //}

                //var coll = new PathwayItemCollection();

                var pathway = new PathwayQuery("b");
                var item = new PathwayItemQuery("a");
                //var diag = new PathwayDiagnoseItemQuery("c");

                //var rpic = new RegistrationPathwayItemCollection();

                var rpq = new RegistrationPathwayQuery("d");
                var rpiq = new RegistrationPathwayItemQuery("e");

                rpiq.InnerJoin(rpq).On(rpiq.PathwayID == rpq.PathwayID && rpiq.RegistrationNo == rpq.RegistrationNo);
                rpiq.InnerJoin(pathway).On(rpq.PathwayID == pathway.PathwayID);
                rpiq.InnerJoin(item).On(rpiq.PathwayID == item.PathwayID && rpiq.PathwayItemSeqNo == item.PathwayItemSeqNo);
                rpiq.Where(rpq.RegistrationNo == txtRegistrationNo.Text, rpq.PathwayID != string.Empty);
                rpiq.Select(
                    pathway.PathwayName,
                    rpiq,
                    item.AssesmentName,
                    item.AssesmentGroupName,
                    "<'' AS col_1>",
                    "<'' AS col_2>",
                    "<'' AS col_3>",
                    "<'' AS col_4>",
                    "<'' AS col_5>",
                    "<'' AS col_6>",
                    "<'' AS col_7>",
                    "<CAST(0 AS BIT) AS chk_1>",
                    "<CAST(0 AS BIT) AS chk_2>",
                    "<CAST(0 AS BIT) AS chk_3>",
                    "<CAST(0 AS BIT) AS chk_4>",
                    "<CAST(0 AS BIT) AS chk_5>",
                    "<CAST(0 AS BIT) AS chk_6>",
                    "<CAST(0 AS BIT) AS chk_7>"
                    );
                //item.InnerJoin(pathway).On(item.PathwayID == pathway.PathwayID);

                //item.InnerJoin(diag).On(pathway.PathwayID == diag.PathwayID);
                //item.Where(diag.DiagnoseID == EpisodeDiagnoses.Where(e => e.SRDiagnoseType == "DiagnoseType-001" && (e.IsConfirmed ?? false) && !(e.IsVoid ?? false)).Select(e => e.DiagnoseID).Take(1).Single());

                //rpic.Load(rpiq);
                var rpic = rpiq.LoadDataTable();

                foreach (DataRow entity in rpic.Rows)
                {

                    foreach (var exec in PathwayItemExecutions(entity["RegistrationNo"].ToString(), entity["PathwayID"].ToString(), entity["PathwayItemSeqNo"].ToInt()).OrderBy(p => p.DayNo))
                    {
                        if (exec.DayNo == 1)
                        {
                            entity["col_1"] = exec.SRPathwayExecutionType;
                            entity["chk_1"] = exec.IsApprove ?? false;
                        }
                        else if (exec.DayNo == 2)
                        {
                            entity["col_2"] = exec.SRPathwayExecutionType;
                            entity["chk_2"] = exec.IsApprove ?? false;
                        }
                        else if (exec.DayNo == 3)
                        {
                            entity["col_3"] = exec.SRPathwayExecutionType;
                            entity["chk_3"] = exec.IsApprove ?? false;
                        }
                        else if (exec.DayNo == 4)
                        {
                            entity["col_4"] = exec.SRPathwayExecutionType;
                            entity["chk_4"] = exec.IsApprove ?? false;
                        }
                        else if (exec.DayNo == 5)
                        {
                            entity["col_5"] = exec.SRPathwayExecutionType;
                            entity["chk_5"] = exec.IsApprove ?? false;
                        }
                        else if (exec.DayNo == 6)
                        {
                            entity["col_6"] = exec.SRPathwayExecutionType;
                            entity["chk_6"] = exec.IsApprove ?? false;
                        }
                        else if (exec.DayNo == 7)
                        {
                            entity["col_7"] = exec.SRPathwayExecutionType;
                            entity["chk_7"] = exec.IsApprove ?? false;
                        }
                    }
                }
                rpic.AcceptChanges();
                //Session[sessName] = rpic;
                return rpic;
            }
            //set
            //{
            //    string sessName = "collPathwayItemCollection";
            //    Session[sessName] = value;
            //}
        }

        private RegistrationPathwayItemExecutionCollection PathwayItemExecutions(string registrationNo, string pathwayID, int pathwayItemSeqNo)
        {
            var coll = new RegistrationPathwayItemExecutionCollection();

            var query = new RegistrationPathwayItemExecutionQuery("a");
            var pieq = new PathwayItemExecutionQuery("b");

            query.Select(query, pieq.SRPathwayExecutionType.As("refToPathwayItemExecution_SRPathwayExecutionType"));
            query.InnerJoin(pieq).On(query.PathwayID == pieq.PathwayID && query.PathwayItemSeqNo == pieq.PathwayItemSeqNo && query.DayNo == pieq.DayNo);
            query.Where(query.RegistrationNo == registrationNo, query.PathwayID == pathwayID, query.PathwayItemSeqNo == pathwayItemSeqNo);
            coll.Load(query);

            return coll;
        }

        private void PopulatePathwayItemGrid()
        {
            //Display Data Detail
            //PathwayItems = null; //Reset Record Detail
            grdList.DataSource = PathwayItems; //Requery
            grdList.MasterTableView.IsItemInserted = false;
            grdList.MasterTableView.ClearEditItems();
            grdList.DataBind();
        }

        protected void cboPathwayName_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var rp = new RegistrationPathway();
            if (rp.LoadByPrimaryKey(e.Value, txtRegistrationNo.Text)) return;

            rp = new RegistrationPathway();
            rp.RegistrationNo = txtRegistrationNo.Text;
            rp.PathwayID = e.Value;
            rp.LastUpdateByUserID = AppSession.UserLogin.UserID;
            rp.LastUpdateDateTime = DateTime.Now;

            var pic = new PathwayItemCollection();
            pic.Query.Where(pic.Query.PathwayID == rp.PathwayID);
            pic.Query.Load();

            var rpic = new RegistrationPathwayItemCollection();
            var rpiec = new RegistrationPathwayItemExecutionCollection();

            foreach (var entity in pic)
            {
                var rpi = rpic.AddNew();
                rpi.RegistrationNo = rp.RegistrationNo;
                rpi.PathwayID = rp.PathwayID;
                rpi.PathwayItemSeqNo = entity.PathwayItemSeqNo;
                rpi.LastUpdateByUserID = AppSession.UserLogin.UserID;
                rpi.LastUpdateDateTime = DateTime.Now;

                var piec = new PathwayItemExecutionCollection();
                piec.Query.Where(piec.Query.PathwayID == rp.PathwayID, piec.Query.PathwayItemSeqNo == entity.PathwayItemSeqNo);
                piec.Query.Load();

                foreach (var entity2 in piec)
                {
                    var rpie = rpiec.AddNew();
                    rpie.RegistrationNo = rp.RegistrationNo;
                    rpie.PathwayID = rp.PathwayID;
                    rpie.PathwayItemSeqNo = entity.PathwayItemSeqNo;
                    rpie.DayNo = entity2.DayNo;
                    rpie.IsApprove = false;
                    rpie.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    rpie.LastUpdateDateTime = DateTime.Now;
                }
            }
            using (var trans = new esTransactionScope())
            {
                rp.Save();
                rpic.Save();
                rpiec.Save();

                trans.Complete();
            }

            PopulatePathwayItemGrid();
        }

        protected void cboPathwayName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PathwayName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PathwayID"].ToString();
        }

        protected void cboPathwayName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //if (EpisodeDiagnoses.Any(d => d.SRDiagnoseType == "DiagnoseType-001" && (d.IsConfirmed ?? false) && !(d.IsVoid ?? false)))
            {
                var rpc = new RegistrationPathwayCollection();
                rpc.Query.Where(rpc.Query.RegistrationNo == txtRegistrationNo.Text);

                var pq = new PathwayQuery("a");
                //var pdq = new PathwayDiagnoseItemQuery("b");

                pq.es.Top = 20;
                pq.es.Distinct = true;
                pq.Select(
                    pq.PathwayID,
                    pq.PathwayName
                    );
                //pq.InnerJoin(pdq).On(pq.PathwayID == pdq.PathwayID);
                //pq.Where(pdq.DiagnoseID == EpisodeDiagnoses.Where(ed => ed.SRDiagnoseType == "DiagnoseType-001" && (ed.IsConfirmed ?? false) && !(ed.IsVoid ?? false)).Select(ed => ed.DiagnoseID).Take(1).Single());
                if (rpc.Query.Load()) pq.Where(pq.PathwayID.NotIn(rpc.Select(r => r.PathwayID)));

                (o as RadComboBox).DataSource = pq.LoadDataTable();
            }
            //else (o as RadComboBox).DataSource = null;
            (o as RadComboBox).DataBind();
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            var cmd = e.CommandArgument.ToString().Split('|');
            if (cmd[0] == "SetValue")
            {
                var entity = new RegistrationPathwayItemExecution();
                entity.LoadByPrimaryKey(cmd[1].ToInt(), (e.Item as GridDataItem).GetDataKeyValue("PathwayID").ToString(), (e.Item as GridDataItem).GetDataKeyValue("PathwayItemSeqNo").ToInt(), txtRegistrationNo.Text);
                entity.IsApprove = !(entity.IsApprove ?? false);
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.Save();

                grdList.Rebind();
            }
        }

        private void PopulateDocuments()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var gdcc = new GuarantorDocumentChecklistCollection();
            gdcc.Query.Where(gdcc.Query.GuarantorID == txtGuarantorID.Text, gdcc.Query.SRRegistrationType == reg.SRRegistrationType);
            if (gdcc.Query.Load())
            {
                foreach (var gdc in gdcc)
                {
                    var dcdc = new BusinessObject.DocumentChecklistDefinitionCollection();
                    dcdc.Query.Where(dcdc.Query.SRDocumentChecklist == gdc.SRDocumentChecklist);
                    if (!dcdc.Query.Load()) continue;
                    foreach (var dcd in dcdc)
                    {
                        var rdcl = new BusinessObject.RegistrationDocumentCheckList();
                        if (!rdcl.LoadByPrimaryKey(txtRegistrationNo.Text, dcd.DocumentFilesID ?? 0))
                        {
                            rdcl = new BusinessObject.RegistrationDocumentCheckList();
                            rdcl.RegistrationNo = txtRegistrationNo.Text;
                            rdcl.DocumentFilesID = dcd.DocumentFilesID;
                            rdcl.str.LastUpdateDateTime = string.Empty;
                            rdcl.LastUpdateByUserID = string.Empty;
                            rdcl.FileName = string.Empty;

                            rdcl.Save();
                        }
                    }
                }
            }
        }

        protected void ToggleSelectedStateDocuments(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDocument.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void grdDocument_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //PopulateDocuments();

            //var gdc = new GuarantorDocumentChecklistQuery("gdc");
            //var dcd = new DocumentChecklistDefinitionQuery("dcd");
            //var asri = new AppStandardReferenceItemQuery("asri");
            //var df = new DocumentFilesQuery("df");
            //var rdcl = new RegistrationDocumentCheckListQuery("rdcl");

            //gdc.Select(rdcl.RegistrationNo, rdcl.DocumentFilesID, asri.ItemName, df.DocumentName, rdcl.FileName, rdcl.LastUpdateDateTime, rdcl.LastUpdateByUserID);
            //gdc.InnerJoin(dcd).On(dcd.SRDocumentChecklist == gdc.SRDocumentChecklist);
            //gdc.InnerJoin(asri).On(asri.StandardReferenceID == AppEnum.StandardReference.DocumentChecklist && dcd.SRDocumentChecklist == asri.ItemID);
            //gdc.InnerJoin(df).On(df.DocumentFilesID == dcd.DocumentFilesID && df.IsActive == true);
            //gdc.InnerJoin(rdcl).On(rdcl.DocumentFilesID == df.DocumentFilesID);
            //gdc.Where(rdcl.RegistrationNo == txtRegistrationNo.Text);

            //grdDocument.DataSource = gdc.LoadDataTable();

            //var path = "D:\\testdoc";

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var bpjs = new BusinessObject.BpjsSEP();
            bpjs.LoadByPrimaryKey(reg.BpjsSepNo);

            var grr = new Guarantor();
            grr.Query.Where(grr.Query.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjs, grr.Query.GuarantorHeaderID > string.Empty);
            grr.Query.es.Top = 1;
            grr.Query.Load();

            var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (bpjs.NoSEP ?? (reg.BpjsSepNo ?? txtRegistrationNo.Text)) : txtRegistrationNo.Text;
            endFolderName = endFolderName.Trim().Replace("/", "-");

            var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
            if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

            //sepFolder = "\\\\26.32.31.198\\DOCUMENT_AVICENNA";

            string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);

            var list = new List<DirFileModel>();

            if (Directory.Exists(path))
            {
                var dirListModel = MapDirs(path);
                var fileListModel = MapFiles(path);
                var explorerModel = new ExplorerModel(dirListModel, fileListModel);

                list.AddRange(explorerModel.FileModelList.Where(d => d.FileName != "Thumbs.db").Select(d => new DirFileModel()
                {
                    Type = 2,
                    Url = string.Format("{0}/{1:0000}/{2:00}/{3:00}/{5:00}/{4}/{6}", ConfigurationManager.AppSettings["CasemixDocumentVirtualUrl"], reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day, d.FileName),
                    //Url = ConfigurationManager.AppSettings["CasemixDocumentVirtualUrl"] + "//" + d.FileName,
                    Path = path + "\\" + d.FileName,
                    Name = d.FileName,
                    Size = d.FileSizeText,
                    Accessed = d.FileAccessed
                }));
            }

            grdDocument.DataSource = list;
        }

        protected void btnReloadEklaim_Click(object sender, ImageClickEventArgs e)
        {
            if (!Helper.IsInacbgIntegration) return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var patientId = 0;
            var admissionId = 0;
            var hospitalAdmissionId = 0;

            var svc = new Common.Inacbg.v510.Service();
            var response1 = svc.Insert(new Common.Inacbg.v510.Claim.Create.Data()
            {
                nomor_kartu = txtNoBpjs.Text,
                nomor_sep = txtSepNo.Text,
                nomor_rm = txtMedicalNo.Text.Replace("-", string.Empty),
                nama_pasien = txtPatientName.Text,
                tgl_lahir = txtDateOfBirth.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                gender = (txtGender.Text == "M" ? "1" : "2")
            });
            if (response1.Metadata.IsDuplicate)
            {
                svc = new Common.Inacbg.v510.Service();
                var response2 = svc.Update(new Common.Inacbg.v510.Patient.Update.Data()
                {
                    nomor_kartu = txtNoBpjs.Text,
                    nomor_rm = txtMedicalNo.Text.Replace("-", string.Empty),
                    nama_pasien = txtPatientName.Text,
                    tgl_lahir = txtDateOfBirth.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    gender = (txtGender.Text == "M" ? "1" : "2")
                });
                if (response2.Metadata.IsValid)
                {
                    svc = new Common.Inacbg.v510.Service();
                    var response = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data() { nomor_sep = txtSepNo.Text });
                    if (response.Metadata.IsValid)
                    {
                        patientId = response.DataResponse.Data.PatientId.ToInt();
                        admissionId = response.DataResponse.Data.AdmissionId.ToInt();
                        hospitalAdmissionId = response.DataResponse.Data.HospitalAdmissionId.ToInt();
                    }
                }
            }
            else if (response1.Metadata.IsValid)
            {
                patientId = response1.Response.PatientId;
                admissionId = response1.Response.AdmissionId;
                hospitalAdmissionId = response1.Response.HospitalAdmissionId;
            }

            var ncc = new NccInacbg();
            if (!ncc.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                ncc = new NccInacbg();
                ncc.CoverageAmount = 0;
                ncc.AddPaymentAmt = 0;
            }
            ncc.RegistrationNo = txtRegistrationNo.Text;
            ncc.PatientId = patientId;
            ncc.AdmissionId = admissionId;
            ncc.HospitalAdmissionId = hospitalAdmissionId;
            ncc.LastUpdateDateTime = DateTime.Now;
            ncc.LastUpdateByUserID = AppSession.UserLogin.UserID;
            ncc.Save();

            var diag = string.Empty;
            foreach (var d in (EpisodeDiagnoses as EpisodeDiagnoseCollection))
            {
                diag += d.DiagnoseID + "#";
            }
            if (string.IsNullOrEmpty(diag)) diag = "#";

            var proc = string.Empty;
            foreach (var d in (EpisodeProcedures as EpisodeProcedureCollection))
            {
                proc += d.ProcedureID + "#";
            }
            if (string.IsNullOrEmpty(proc)) proc = "#";

            svc = new Common.Inacbg.v510.Service();
            var repsonse3 = svc.Insert(new Common.Inacbg.v510.Detail.Datass()
            {
                nomor_sep = txtSepNo.Text,
                nomor_kartu = txtNoBpjs.Text,
                tgl_masuk = reg.RegistrationDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                tgl_pulang = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                jenis_rawat = "1",
                kelas_rawat = "1",
                adl_sub_acute = string.Empty,
                adl_chronic = string.Empty,
                icu_indikator = "0",
                icu_los = "0",
                ventilator_hour = "0",
                upgrade_class_ind = "0",
                upgrade_class_class = string.Empty,
                upgrade_class_los = "0",
                add_payment_pct = "0",
                birth_weight = "0",
                discharge_status = "1",
                tarif_rs = new Common.Inacbg.v510.Detail.TarifRss()
                {
                    prosedur_non_bedah = txtProsedurNonBedah.Value.ToInt().ToString(),
                    prosedur_bedah = txtProsedurBedah.Value.ToInt().ToString(),
                    konsultasi = txtKonsultasi.Value.ToInt().ToString(),
                    tenaga_ahli = txtTenagaAhli.Value.ToInt().ToString(),
                    keperawatan = txtKeperawatan.Value.ToInt().ToString(),
                    penunjang = txtPenunjang.Value.ToInt().ToString(),
                    radiologi = txtRadiologi.Value.ToInt().ToString(),
                    laboratorium = txtLaboratorium.Value.ToInt().ToString(),
                    pelayanan_darah = txtPelayananDarah.Value.ToInt().ToString(),
                    rehabilitasi = txtRehabilitasi.Value.ToInt().ToString(),
                    kamar = txtKamarAkomodasi.Value.ToInt().ToString(),
                    rawat_intensif = txtRawatIntensifTarif.Value.ToInt().ToString(),
                    obat = txtObat.Value.ToInt().ToString(),
                    alkes = txtAlkes.Value.ToInt().ToString(),
                    bmhp = txtBMHP.Value.ToInt().ToString(),
                    sewa_alat = txtSewaAlat.Value.ToInt().ToString()
                },
                tarif_poli_eks = "0",
                nama_dokter = lblParamedicName.Text,
                kode_tarif = "AP",
                payor_id = "3",
                payor_cd = "JKN",
                cob_cd = string.Empty,
                coder_nik = "123123123123"
            });
            //if (repsonse3.Metadata.IsValid)
            //{
            //    svc = new Common.Inacbg.v51.Service();
            //    var response4 = svc.Grouper1(new Common.Inacbg.v51.Grouper.Grouper1.Data() { nomor_sep = txtSepNo.Text });
            //    if (response4.Metadata.IsValid)
            //    {
            //        var data = response4.Response;

            //        decimal coverage = 0;
            //        var cbg = data.Cbg;
            //        if (cbg != null) coverage += Convert.ToDecimal(cbg.Tariff);
            //        var chronic = data.Chronic;
            //        if (chronic != null) coverage += Convert.ToDecimal(chronic.Tariff);
            //        var acute = data.SubAcute;
            //        if (acute != null) coverage += Convert.ToDecimal(acute.Tariff);

            //        var add = string.IsNullOrEmpty(data.AddPaymentAmt) ? 0 : Convert.ToDecimal(data.AddPaymentAmt);

            //        ncc.AddPaymentAmt = add;
            //        ncc.CoverageAmount = coverage;
            //        ncc.Save();

            //        if (TotalGuarantorAndRemainingPatientAmount < coverage) reg.PlavonAmount = TotalGuarantorAndRemainingPatientAmount;
            //        else reg.PlavonAmount = coverage;
            //        reg.Save();
            //    }
            //}

            svc = new Common.Inacbg.v510.Service();
            var response5 = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data() { nomor_sep = txtSepNo.Text });
            if (response5.Metadata.IsValid)
            {
                lblCoverageEklaim.Text = (ncc.CoverageAmount ?? 0).ToString("N2");
                lblStatusEklaim.Text = response5.DataResponse.Data.KlaimStatusCd;
                lblStatusKemenkes.Text = response5.DataResponse.Data.KemenkesDcStatusCd;
                lblGroupCode.Text = response5.DataResponse.Data.Grouper.ResponseInacbg.Cbg.Code;
                lblGroupDesc.Text = response5.DataResponse.Data.Grouper.ResponseInacbg.Cbg.Description;
                lblGroupTarif.Text = response5.DataResponse.Data.Grouper.ResponseInacbg.Tariff;
                lblMdc.Text = $"{response5?.DataResponse?.Data?.Grouper?.ResponseIdrg?.mdc_description} - " +
                              $"{response5?.DataResponse?.Data?.Grouper?.ResponseIdrg?.mdc_number}";
                lblDrg.Text = $"{response5?.DataResponse?.Data?.Grouper?.ResponseIdrg?.drg_description} - " +
                              $"{response5?.DataResponse?.Data?.Grouper?.ResponseIdrg?.drg_code}";
                lblCostWeight.Text = response5?.DataResponse?.Data?.Grouper?.ResponseIdrg?.cost_weight ?? string.Empty;
                lblNBR.Text = response5?.DataResponse?.Data?.Grouper?.ResponseIdrg?.nbr ?? string.Empty;
                lblIdrgStatus.Text = response5?.DataResponse?.Data?.KlaimStatusCd ?? string.Empty;
            }
        }

        protected void lbtnSaveSep_Click(object sender, ImageClickEventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            reg.GuarantorCardNo = txtNoBpjs.Text;
            reg.BpjsSepNo = txtSepNo.Text;
            reg.Save();
        }

        private void PopulatePlafonBar()
        {
            txtEstimatedPlafon.Value = Convert.ToDouble(TotalPlafond);
            lblPercentagePlafon.Text = PlafondValueUsedInPercent().ToString("N2") + "%" + (PlafondValueUsedInPercent() > 100 ? string.Format(" ({0:n2})", TotalGuarantorAndRemainingPatientAmount - TotalPlafond) : string.Empty);
            lblEstimatedPlafon2.Text = string.Format("100% ({0})", TotalPlafond.ToString("N2"));
            lblCoverageCob.Text = AdditionalPlafond.ToString("N2");

            lblBillingTotal.Text = TotalGuarantorAndRemainingPatientAmount.ToString("N2");

            usedplafond.Style.Add("background", PlafondValueUsedInPercent() > 100 ? "red" : PlafondValueUsedInPercent() > 75 ? "yellow" : "green");
            usedplafond.Style.Add("width", PlafondValueUsedInPercent() > 100 ? "100%" : PlafondValueUsedInPercent().ToString("N2") + "%");
            var width2 = PlafondValueUsedInPercent() > 100 ? 100 / (PlafondValueUsedInPercent() / (decimal)100) : 0;
            usedPlafond2.Style.Add("width", PlafondValueUsedInPercent() < 100 ? "100%" : width2.ToString("N2") + "%");
        }

        protected void lbtnSaveEstimaedPlafon_Click(object sender, ImageClickEventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);
            reg.PlavonAmount = Convert.ToDecimal(txtEstimatedPlafon.Value ?? 0);
            reg.Save();

            PopulatePlafonBar();
        }

        #region Record Detail Method Function - Registration Guarantor

        private void PopulateRegistrationGuarantorGrid()
        {
            RegistrationGuarantors = null; //Reset Record Detail
            grdRegistrationGuarantor.DataSource = RegistrationGuarantors;
            grdRegistrationGuarantor.MasterTableView.IsItemInserted = false;
            grdRegistrationGuarantor.MasterTableView.ClearEditItems();
            grdRegistrationGuarantor.DataBind();
        }

        private RegistrationGuarantorCollection RegistrationGuarantors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRegistrationGuarantor" + Request.UserHostName];
                    if (obj != null)
                        return ((RegistrationGuarantorCollection)(obj));
                }

                var coll = new RegistrationGuarantorCollection();
                var query = new RegistrationGuarantorQuery("a");
                var gq = new GuarantorQuery("b");

                query.Select
                    (
                        query,
                        gq.GuarantorName.As("refToGuarantor_GuarantorName")
                    );

                query.InnerJoin(gq).On(query.GuarantorID == gq.GuarantorID);
                query.Where(query.RegistrationNo == txtRegistrationNo.Text);

                query.OrderBy(query.GuarantorID, esOrderByDirection.Ascending);

                coll.Load(query);

                Session["collRegistrationGuarantor" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collRegistrationGuarantor" + Request.UserHostName] = value; }
        }

        protected void grdRegistrationGuarantor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationGuarantor.DataSource = RegistrationGuarantors;
        }

        protected void grdRegistrationGuarantor_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);

            var tp = new TransPaymentCollection();
            tp.Query.Where(tp.Query.RegistrationNo == txtRegistrationNo.Text, tp.Query.GuarantorID == id, tp.Query.IsVoid == false);
            tp.LoadAll();
            if (tp.Count > 0) return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
            {
                SetEntityValue(entity, e);
                RegistrationGuarantors.Save();
                PopulatePlafonBar();
            }
        }

        protected void grdRegistrationGuarantor_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);
            var tp = new TransPaymentCollection();
            tp.Query.Where(tp.Query.RegistrationNo == txtRegistrationNo.Text, tp.Query.GuarantorID == id, tp.Query.IsVoid == false);
            tp.LoadAll();
            if (tp.Count > 0) return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                RegistrationGuarantors.Save();
                PopulatePlafonBar();
            }
        }

        protected void grdRegistrationGuarantor_InsertCommand(object source, GridCommandEventArgs e)
        {
            RegistrationGuarantor entity = RegistrationGuarantors.AddNew();
            SetEntityValue(entity, e);
            RegistrationGuarantors.Save();

            //e.Canceled = true;
            grdRegistrationGuarantor.Rebind();
            PopulatePlafonBar();
        }

        private void SetEntityValue(RegistrationGuarantor entity, GridCommandEventArgs e)
        {
            var userControl = (RegistrationGuarantorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.GuarantorID = userControl.GuarantorID;
                entity.GuarantorName = userControl.GuarantorName;
                entity.PlafondAmount = userControl.PlafondAmount;
                entity.Notes = userControl.Notes;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private RegistrationGuarantor FindItemGuarantorGrid(string guarantorId)
        {
            RegistrationGuarantorCollection coll = RegistrationGuarantors;
            RegistrationGuarantor retval = null;
            foreach (RegistrationGuarantor rec in coll)
            {
                if (rec.GuarantorID.Equals(guarantorId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdRegistrationRule.CurrentPageIndex = 0;
            grdRegistrationRule.Rebind();
        }

        #endregion

        #region CasemixCoveredRegistrationRule
        private CasemixCoveredRegistrationRuleCollection CasemixCoveredRegistrationRules
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCasemixCoveredRegistrationRule"];
                    if (obj != null) return ((CasemixCoveredRegistrationRuleCollection)(obj));
                }

                var coll = new CasemixCoveredRegistrationRuleCollection();

                var query = new CasemixCoveredRegistrationRuleQuery("a");
                var itm = new ItemQuery("b");

                query.Select(query, itm.ItemName.As("refToItem_ItemName"));
                query.InnerJoin(itm).On(query.ItemID == itm.ItemID);

                query.Where(query.RegistrationNo == txtRegistrationNo.Text);
                query.OrderBy(itm.ItemName.Ascending);
                coll.Load(query);

                Session["collCasemixCoveredRegistrationRule"] = coll;
                return coll;
            }
            set
            {
                Session["collCasemixCoveredRegistrationRule"] = value;
            }
        }

        private void PopulateCasemixCoveredRegistrationRuleItemGrid()
        {
            //Display Data Detail
            CasemixCoveredRegistrationRules = null; //Reset Record Detail
            grdRegistrationRule.DataSource = CasemixCoveredRegistrationRules; //Requery
            grdRegistrationRule.MasterTableView.IsItemInserted = false;
            grdRegistrationRule.MasterTableView.ClearEditItems();
            grdRegistrationRule.DataBind();
        }

        protected void grdRegistrationRule_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (txtFilterItemService.Text.Trim() != string.Empty)
            {
                var ds = from d in CasemixCoveredRegistrationRules
                         where d.ItemName.ToLower().Contains(txtFilterItemService.Text.ToLower()) || d.ItemID.ToLower().Contains(txtFilterItemService.Text.ToLower())
                         select d;
                grdRegistrationRule.DataSource = ds;
            }
            else
            {
                grdRegistrationRule.DataSource = CasemixCoveredRegistrationRules;
            }
        }

        protected void grdRegistrationRule_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var entity = CasemixCoveredRegistrationRules.AddNew();
            SetEntityValueCoveredItem(entity, e);
            CasemixCoveredRegistrationRules.Save();

            //Stay in insert mode
            e.Canceled = true;
            grdRegistrationRule.Rebind();
        }

        protected void grdRegistrationRule_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            string itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CasemixCoveredRegistrationRuleMetadata.ColumnNames.ItemID]);

            var entity = FindCasemixCoveredDetail(itemID);
            if (entity != null)
            {
                SetEntityValueCoveredItem(entity, e);
                CasemixCoveredRegistrationRules.Save();
            }
        }

        protected void grdRegistrationRule_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            string itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CasemixCoveredRegistrationRuleMetadata.ColumnNames.ItemID]);

            var entity = FindCasemixCoveredDetail(itemID);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                CasemixCoveredRegistrationRules.Save();
            }
        }

        private CasemixCoveredRegistrationRule FindCasemixCoveredDetail(string itemID)
        {
            var coll = CasemixCoveredRegistrationRules;
            return coll.FirstOrDefault(rec => rec.ItemID.Equals(itemID));
        }

        private void SetEntityValueCoveredItem(CasemixCoveredRegistrationRule entity, GridCommandEventArgs e)
        {
            var userControl = (CasemixExceptionRegistrationRuleItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = Convert.ToDecimal(userControl.Qty);
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }
        #endregion

        protected bool IsEklaimProcessed
        {
            get
            {
                var ncc = new NccInacbg();
                return ncc.LoadByPrimaryKey(txtRegistrationNo.Text);
            }
        }

        public class DirModel
        {
            public string DirName { get; set; }
            public DateTime DirAccessed { get; set; }
        }

        public class FileModel
        {
            public string FileName { get; set; }
            public string FileSizeText { get; set; }
            public DateTime FileAccessed { get; set; }
        }

        public class ExplorerModel
        {
            public List<DirModel> DirModelList;
            public List<FileModel> FileModelList;

            public string FolderName;
            public string ParentFolderName;
            public string URL;

            public ExplorerModel(List<DirModel> dirModelList, List<FileModel> fileModelList)
            {
                DirModelList = dirModelList;
                FileModelList = fileModelList;
            }
        }

        private List<DirModel> MapDirs(string realPath)
        {
            List<DirModel> dirListModel = new List<DirModel>();

            IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
            foreach (string dir in dirList)
            {
                DirectoryInfo d = new DirectoryInfo(dir);

                DirModel dirModel = new DirModel
                {
                    DirName = Path.GetFileName(dir),
                    DirAccessed = d.LastAccessTime
                };

                dirListModel.Add(dirModel);
            }

            return dirListModel;
        }

        private List<FileModel> MapFiles(string realPath)
        {
            List<FileModel> fileListModel = new List<FileModel>();

            IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
            foreach (string file in fileList)
            {
                FileInfo f = new FileInfo(file);

                FileModel fileModel = new FileModel();

                //if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
                //    && f.Extension.ToLower() != "asp" && f.Extension.ToLower() != "exe")
                {
                    fileModel.FileName = Path.GetFileName(file);
                    fileModel.FileAccessed = f.LastAccessTime;
                    fileModel.FileSizeText = (f.Length < 1024) ?
                                    f.Length.ToString() + " B" : f.Length / 1024 + " KB";

                    fileListModel.Add(fileModel);
                }
            }

            return fileListModel;
        }

        private class DirFileModel
        {
            public short Type { get; set; }
            public string Path { get; set; }
            public string Name { get; set; }
            public string Size { get; set; }
            public string Url { get; set; }
            public DateTime Accessed { get; set; }
        }

        protected void grdDocument_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            string name = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Name"]);

            //var realPath = "D:\\";

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var bpjs = new BusinessObject.BpjsSEP();
            bpjs.LoadByPrimaryKey(reg.BpjsSepNo);

            var grr = new Guarantor();
            grr.Query.Where(grr.Query.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjs, grr.Query.GuarantorHeaderID > string.Empty);
            grr.Query.es.Top = 1;
            grr.Query.Load();

            var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (bpjs.NoSEP ?? txtRegistrationNo.Text) : txtRegistrationNo.Text;
            endFolderName = endFolderName.Trim().Replace("/", "-");

            var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
            if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

            string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);

            if (File.Exists(path + "\\" + name))
            {
                File.Delete(path + "\\" + name);

                grdDocument.Rebind();
            }
        }

        private void MergeFiles()
        {
            var documentsToMerge = new List<string>();

            foreach (GridDataItem item in grdDocument.MasterTableView.Items)
            {
                if (!((CheckBox)item.FindControl("detailChkbox")).Checked) continue;
                var order = ((RadNumericTextBox)item.FindControl("orderTextBox")).Value ?? 0;
                documentsToMerge.Add($"{order}#{item["Path"].Text}");
            }

            if (!documentsToMerge.Any()) return;

            var document = new RadFixedDocument();
            var provider = new PdfFormatProvider();
            foreach (string documentName in documentsToMerge.OrderBy(t => t))
            {
                using (Stream input = File.OpenRead(documentName.Split('#')[1]))
                {
                    if (documentName.Contains(".pdf")) document.Merge(provider.Import(input));
                    else if (documentName.Contains(".jpg") || documentName.Contains(".jpeg") || documentName.Contains(".png") || documentName.Contains(".bmp"))
                    {
                        var imageSource = new ImageSource(input);
                        var lastPage = document.Pages.AddPage();
                        var editor = new FixedContentEditor(lastPage);

                        editor.Position.Translate(offsetX: 50, offsetY: 50);
                        editor.DrawImage(imageSource);
                    }
                }
            }

            //var path = "D:\\testdoc\\";

            var reg = new Registration();
            reg.LoadByPrimaryKey(txtRegistrationNo.Text);

            var bpjs = new BusinessObject.BpjsSEP();
            bpjs.LoadByPrimaryKey(reg.BpjsSepNo);

            var grr = new Guarantor();
            grr.Query.Where(grr.Query.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjs, grr.Query.GuarantorHeaderID > string.Empty);
            grr.Query.es.Top = 1;
            grr.Query.Load();

            var endFolderName = grr.LoadByPrimaryKey(reg.GuarantorID) ? (bpjs.NoSEP ?? (reg.BpjsSepNo ?? txtRegistrationNo.Text)) : txtRegistrationNo.Text;
            endFolderName = endFolderName.Trim().Replace("/", "-");

            var sepFolder = AppParameter.GetParameterValue(AppParameter.ParameterItem.SepFolder);
            if (string.IsNullOrWhiteSpace(sepFolder)) sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

            string path = string.Format("{0}\\{1:0000}\\{2:00}\\{3:00}\\{5:00}\\{4}\\", sepFolder, reg.RegistrationDate?.Year, reg.RegistrationDate?.Month, grr.GuarantorHeaderID, endFolderName, reg.RegistrationDate?.Day);
            var exportedDocument = $"{path}{txtSepNo.Text}_{DateTime.Now.Date:yyyyMMdd}_Merged.pdf";

            //if (string.IsNullOrWhiteSpace(sepFolder))
            //    sepFolder = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "BpjsSepDocument");

            // Create Directory
            //var path = Path.GetDirectoryName(exportedDocument);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            // Delete
            if (File.Exists(exportedDocument)) File.Delete(exportedDocument);

            //Create File
            using (Stream output = File.OpenWrite(exportedDocument))
            {
                provider.Export(document, output);
            }

            //Helper.DownloadFile(Response, exportedDocument);
        }
    }
}
