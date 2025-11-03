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
using System.Collections.Generic;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting
{
    public partial class PaymentTypeCoaSetting : BasePageDialog
    {
        private JournalTransactions JournalTrans {
            get {
                if (ViewState["JournalTransactions"] == null)
                {
                    var journalId = Request.QueryString["ivd"];
                    var journal = new JournalTransactions();
                    journal.LoadByPrimaryKey(Convert.ToInt32(journalId));

                    ViewState["JournalTransactions"] = journal;
                }
                return (JournalTransactions)ViewState["JournalTransactions"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["src"]))
                {
                    ((Button)Helper.FindControlRecursive(Page, "btnOk")).Visible = false;
                    ((Button)Helper.FindControlRecursive(Page, "btnCancel")).Visible = false;
                }

                ViewState["PaymentNo"] = string.Empty;

                if (JournalTrans.JournalType == "35")
                {
                    tabDetail.Tabs[0].Visible = false;
                    tabDetail.SelectedIndex = 1;
                    mpagDetail.SelectedIndex = 1;
                }
            }
        }

        private DataTable TransPaymentItems
        {
            get
            {
                if (ViewState["TransPaymentItems"] != null)
                    return (DataTable)ViewState["TransPaymentItems"];

                var query = new TransPaymentItemQuery("a");
                var qpm = new PaymentMethodQuery("b");
                var qheader = new TransPaymentQuery("c");
                var qjournal = new JournalTransactionsQuery("d");
                var qpt = new PaymentTypeQuery("e");
                var qedc = new EDCMachineQuery("f");
                var qcard = new AppStandardReferenceItemQuery("g");
                var qbank = new BankQuery("h");

                var journalId = Request.QueryString["ivd"];

                query.Select
                    (
                        query.PaymentNo.As("TransactionNo"),
                        query.SequenceNo,
                        query.SRPaymentType,
                        qpt.PaymentTypeName.As("PaymentType"),
                        query.SRPaymentMethod,
                        qpm.PaymentMethodName.As("PaymentMethod"),
                        "<ISNULL(a.SRCardProvider,'-') AS SRCardProvider>",
                        "<ISNULL(g.itemName,'-') AS CardProvider>",
                        "<ISNULL(a.SRCardType,'-') AS SRCardType>",
                        "<ISNULL(a.EDCMachineID,'-') AS EDCMachineID>",
                        "<ISNULL(f.EDCMachineName,'-') AS EDCMachine>",
                        "<ISNULL(a.BankID,'-') AS BankID>",
                        "<ISNULL(h.BankName,'-') AS BankName>",
                        query.Amount,
                        qjournal.RefferenceNumber,
                        query.IsFromDownPayment,
                        "<'' AS dAccCode>",
                        "<'' AS dAccName>",
                        "<'' AS dSublName>"
                    );

                query.InnerJoin(qheader).On(query.PaymentNo == qheader.PaymentNo);
                query.LeftJoin(qpm).On
                    (
                        query.SRPaymentMethod == qpm.SRPaymentMethodID &
                        query.SRPaymentType == qpm.SRPaymentTypeID
                    );
                query.InnerJoin(qjournal).On(query.PaymentNo == qjournal.RefferenceNumber);
                query.InnerJoin(qpt).On(query.SRPaymentType == qpt.SRPaymentTypeID);
                query.LeftJoin(qedc).On
                    (
                        query.EDCMachineID == qedc.EDCMachineID &
                        query.SRCardProvider == qedc.SRCardProvider
                    );
                query.LeftJoin(qcard).On
                    (
                        query.SRCardProvider == qcard.ItemID &
                        qcard.StandardReferenceID == "CardProvider"
                    );
                query.LeftJoin(qbank).On
                    (
                        query.BankID == qbank.BankID
                    );

                query.Where
                    (
                        qjournal.JournalId == journalId
                    );

                query.OrderBy
                    (
                        query.PaymentNo.Ascending,
                        query.SequenceNo.Ascending
                    );

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    int dAccId = 0;
                    int dSublId = 0;

                    if (row["SRPaymentType"].ToString().Equals("PaymentType-001") || row["SRPaymentType"].ToString().Equals("PaymentType-002") || row["SRPaymentType"].ToString().Equals("PaymentType-005"))
                    {
                        var pm = new PaymentMethod();
                        if (pm.LoadByPrimaryKey(row["SRPaymentType"].ToString(), row["SRPaymentMethod"].ToString()))
                        {
                            dAccId = pm.ChartOfAccountID ?? 0;
                            dSublId = pm.SubledgerID ?? 0;

                            //transfer
                            if (row["SRPaymentMethod"].ToString().Equals("PaymentMethod-004"))
                            {
                                var bank = new Bank();
                                if (bank.LoadByPrimaryKey(row["BankID"].ToString()))
                                {
                                    dAccId = bank.ChartOfAccountId ?? 0;
                                    dSublId = bank.SubledgerId ?? 0;
                                }
                            }

                        }

                        if (row["SRPaymentType"].ToString().Equals("PaymentType-001") || row["SRPaymentType"].ToString().Equals("PaymentType-002"))
                        {
                            // Payment Credit Card || Payment Debit Card 
                            if (row["SRPaymentMethod"].ToString().Equals("PaymentMethod-002") || row["SRPaymentMethod"].ToString().Equals("PaymentMethod-003"))
                            {
                                var edc = new EDCMachine();
                                if (edc.LoadByPrimaryKey(row["EDCMachineID"].ToString()))
                                {
                                    dAccId = edc.ChartOfAccountID ?? 0;
                                    dSublId = edc.SubledgerID ?? 0;
                                }
                            }
                        }

                        if (dAccId == 0)
                        {
                            var pt = new PaymentType();
                            if (pt.LoadByPrimaryKey(row["SRPaymentType"].ToString()))
                            {
                                dAccId = pt.ChartOfAccountID ?? 0;
                                dSublId = pt.SubledgerID ?? 0;
                            }
                        }
                    }

                    var acc = new ChartOfAccounts();
                    if (acc.LoadByPrimaryKey(dAccId))
                    {
                        row["dAccCode"] = acc.ChartOfAccountCode;
                        row["dAccName"] = acc.ChartOfAccountName;
                    }

                    var subl = new SubLedgers();
                    if (subl.LoadByPrimaryKey(dSublId))
                        row["dSublName"] = subl.Description;
                }
                tbl.AcceptChanges();

                ViewState["TransPaymentItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["TransPaymentItems"] = value; }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = TransPaymentItems;
        }

        private DataTable TransactionItems
        {
            get
            {
                if (ViewState["TransactionItems"] != null)
                    return (DataTable)ViewState["TransactionItems"];

                //var journalId = Request.QueryString["ivd"];
                //var journal = new JournalTransactions();
                //journal.LoadByPrimaryKey(Convert.ToInt32(journalId));

                DataTable tbl;

                if (JournalTrans.JournalType == "35" || (JournalTrans.JournalType == "02" && JournalTrans.RefferenceNumber.ToUpper().Contains("REG")))
                {
                    tbl = PatientReceivable(); // settingan jurnal recal billing untuk accrual sama dengan piutang dalam perawatan cash basis adalah sama
                }else{
                    var py = new TransPayment();
                    py.LoadByPrimaryKey(JournalTrans.RefferenceNumber);

                    switch (py.TransactionCode)
                    {
                        case TransactionCode.Payment:
                            tbl = TransactionItemPayments(JournalTrans.RefferenceNumber);
                            break;

                        case TransactionCode.PaymentReturn:
                            tbl = TransactionItemPaymentReturns(JournalTrans.RefferenceNumber);
                            break;
                        case TransactionCode.DownPayment:
                            tbl = TransactionItemDownPayments(JournalTrans.RefferenceNumber);
                            break;
                        default:
                            tbl = TransactionItem(JournalTrans.RefferenceNumber);
                            break;
                    }
                }

                ViewState["TransactionItems"] = tbl;
                return tbl;
            }
            set
            { ViewState["TransactionItems"] = value; }
        }

        private DataTable TransactionItem(string paymentNo)
        {
            DataTable tbl;

            var qtcic = new TransChargesItemCompQuery("b");
            var qtc = new TransChargesQuery("c");
            var qitem = new ItemQuery("d");
            var qsu = new ServiceUnitQuery("e");



            var qcoa = new ChartOfAccountsQuery("g");
            var qcoa2 = new ChartOfAccountsQuery("h");
            var qsubl = new SubLedgersQuery("i");
            var qsubl2 = new SubLedgersQuery("j");
            var qtariffcomp = new TariffComponentQuery("k");
            var reg = new RegistrationQuery("z");
            var qtci = new TransChargesItemQuery("t");

            qtcic.InnerJoin(qtci).On(qtcic.TransactionNo == qtci.TransactionNo && qtcic.SequenceNo == qtci.SequenceNo);
            qtcic.InnerJoin(qtariffcomp).On(qtcic.TariffComponentID == qtariffcomp.TariffComponentID);
            qtcic.InnerJoin(qtc).On(qtcic.TransactionNo == qtc.TransactionNo);
            qtcic.InnerJoin(reg).On(qtc.RegistrationNo == reg.RegistrationNo);
            qtcic.InnerJoin(qitem).On(qtci.ItemID == qitem.ItemID);
            qtcic.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.CoaUsingClass) == "0")
            {
                var qunitcoa = new ServiceUnitItemServiceCompMappingQuery("f");

                qtcic.LeftJoin(qunitcoa).On(
                    qtc.ToServiceUnitID == qunitcoa.ServiceUnitID &&
                    qtcic.TariffComponentID == qunitcoa.TariffComponentID &&
                    qtci.ItemID == qunitcoa.ItemID &&
                    qsu.SRRegistrationType == qunitcoa.SRRegistrationType
                );
                qtcic.LeftJoin(qcoa).On(qunitcoa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                qtcic.LeftJoin(qcoa2).On(qunitcoa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                qtcic.LeftJoin(qsubl).On(qunitcoa.SubledgerIdIncome == qsubl.SubLedgerId);
                qtcic.LeftJoin(qsubl2).On(qunitcoa.SubledgerIdDiscount == qsubl2.SubLedgerId);
            }
            else
            {
                var qunitcoa2 = new ServiceUnitItemServiceClassQuery("f");

                qtcic.LeftJoin(qunitcoa2).On(
                    qtc.ToServiceUnitID == qunitcoa2.ServiceUnitID &&
                    qtcic.TariffComponentID == qunitcoa2.TariffComponentID &&
                    qtci.ItemID == qunitcoa2.ItemID &&
                    qtc.ClassID == qunitcoa2.ClassID
                );
                qtcic.LeftJoin(qcoa).On(qunitcoa2.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                qtcic.LeftJoin(qcoa2).On(qunitcoa2.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                qtcic.LeftJoin(qsubl).On(qunitcoa2.SubledgerIdIncome == qsubl.SubLedgerId);
                qtcic.LeftJoin(qsubl2).On(qunitcoa2.SubledgerIdDiscount == qsubl2.SubLedgerId);

            }

            qtcic.Select
                (
                qitem.SRItemType,
                qsu.ServiceUnitID,
                qsu.ServiceUnitName,
                qtci.ItemID,
                qitem.ItemName,
                qtariffcomp.TariffComponentID,
                qtariffcomp.TariffComponentName,
                qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                qcoa.ChartOfAccountName.As("CoaNameIncome"),
                qsubl.Description.As("SublIncome"),
                qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                qsubl2.Description.As("SublDisc")
                );
            qtcic.Where(qtcic.TransactionNo == paymentNo, qitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical));
            qtcic.es.Distinct = true;
            tbl = qtcic.LoadDataTable();

            qtci = new TransChargesItemQuery("t");
            qtc = new TransChargesQuery("c");
            qitem = new ItemQuery("d");
            qsu = new ServiceUnitQuery("e");

            var pa = new ProductAccountQuery("x");

            var qpacoa = new ServiceUnitProductAccountMappingQuery("f");
            qcoa = new ChartOfAccountsQuery("g");
            qcoa2 = new ChartOfAccountsQuery("h");
            qsubl = new SubLedgersQuery("i");
            qsubl2 = new SubLedgersQuery("j");
            var qreg = new RegistrationQuery("k");

            qtci.InnerJoin(qtc).On(qtci.TransactionNo == qtc.TransactionNo);
            qtci.InnerJoin(qitem).On(qtci.ItemID == qitem.ItemID);
            qtci.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
            qtci.InnerJoin(qreg).On(qtc.RegistrationNo == qreg.RegistrationNo);

            qtci.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

            //query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
            //query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
            //query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
            //query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

            qtci.Select
                (
                qitem.SRItemType,
                qsu.ServiceUnitID,
                qsu.ServiceUnitName,
                qtci.ItemID,
                qitem.ItemName,
                "<'-' AS TariffComponentID>",
                "<'-' AS TariffComponentName>",

                @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaNameIncome>",
                @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                             ELSE x.SubledgerIdIncomeIGD
                        END)) AS SublIncome>",

                @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeDisc>",
                @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                             ELSE x.ChartOfAccountIdDiscountIGD
                        END)) AS CoaNameDisc>",
                @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                             WHEN k.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                             ELSE x.SubledgerIdDiscountIGD
                        END)) AS SublDisc>"
                );
            qtci.Where(qtci.TransactionNo == paymentNo, qitem.SRItemType.In(ItemType.Medical, ItemType.NonMedical));
            qtci.es.Distinct = true;
            DataTable tbl2 = qtci.LoadDataTable();

            tbl.Merge(tbl2);

            var qtpi = new TransPrescriptionItemQuery("t");
            var qtp = new TransPrescriptionQuery("c");
            qitem = new ItemQuery("d");
            qsu = new ServiceUnitQuery("e");

            pa = new ProductAccountQuery("x");

            qpacoa = new ServiceUnitProductAccountMappingQuery("f");
            qcoa = new ChartOfAccountsQuery("g");
            qcoa2 = new ChartOfAccountsQuery("h");
            qsubl = new SubLedgersQuery("i");
            qsubl2 = new SubLedgersQuery("j");
            qreg = new RegistrationQuery("k");

            qtpi.InnerJoin(qtp).On(qtpi.PrescriptionNo == qtp.PrescriptionNo);
            qtpi.InnerJoin(qitem).On(qtpi.ItemID == qitem.ItemID);
            qtpi.InnerJoin(qsu).On(qtp.ServiceUnitID == qsu.ServiceUnitID);
            qtpi.InnerJoin(qreg).On(qtp.RegistrationNo == qreg.RegistrationNo);

            qtpi.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

            qtpi.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
            qtpi.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
            qtpi.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
            qtpi.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

            qtpi.Select
                (
                qitem.SRItemType,
                qsu.ServiceUnitID,
                qsu.ServiceUnitName,
                qtpi.ItemID,
                qitem.ItemName,
                "<'-' AS TariffComponentID>",
                "<'-' AS TariffComponentName>",
                qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                qcoa.ChartOfAccountName.As("CoaNameIncome"),
                qsubl.Description.As("SublIncome"),
                qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                qsubl2.Description.As("SublDisc")
                );
            qtpi.Where(qtpi.PrescriptionNo == paymentNo);
            qtpi.es.Distinct = true;
            DataTable tbl3 = qtpi.LoadDataTable();

            tbl.Merge(tbl3);


            return tbl;
        }

        private DataTable PatientReceivable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("SRItemType",typeof(string));
            tbl.Columns.Add("ServiceUnitID",typeof(string));
            tbl.Columns.Add("ServiceUnitName",typeof(string));
            tbl.Columns.Add("ItemID",typeof(string));
            tbl.Columns.Add("ItemName",typeof(string));
            tbl.Columns.Add("TariffComponentID",typeof(string));
            tbl.Columns.Add("TariffComponentName",typeof(string));
            tbl.Columns.Add("CoaCodeIncome",typeof(string));
            tbl.Columns.Add("CoaNameIncome",typeof(string));
            tbl.Columns.Add("SublIncome",typeof(string));
            tbl.Columns.Add("CoaCodeDisc",typeof(string));
            tbl.Columns.Add("CoaNameDisc",typeof(string));
            tbl.Columns.Add("SublDisc",typeof(string));

            // get data from message
            var msgColl = new JournalMessageCollection();
            msgColl.Query.Where(msgColl.Query.JournalID == JournalTrans.JournalId);
            if (msgColl.LoadAll())
            {
                var revs = JsonConvert
                .DeserializeObject<List<JournalTransactions.CoaMapping>>(msgColl[0].AdditionalData);

                var suColl = new ServiceUnitCollection();
                var itmColl = new ItemCollection();
                var tcColl = new TariffComponentCollection();
                tcColl.LoadAll();

                List<string> keys = new List<string>();
                foreach (var x in revs.Distinct()) {
                    var r = tbl.NewRow();
                    // service unit
                    ServiceUnit su = suColl.Where(y => y.ServiceUnitID == x.ServiceUnitID).FirstOrDefault();
                    if (su == null)
                    {
                        su = new ServiceUnit();
                        if (su.LoadByPrimaryKey(x.ServiceUnitID))
                        {
                            suColl.AttachEntity(su);
                        }
                    }
                    r["ServiceUnitID"] = x.ServiceUnitID;
                    r["ServiceUnitName"] = su.ServiceUnitName;
                    // item
                    Item itm = itmColl.Where(y => y.ItemID == x.ItemID).FirstOrDefault();
                    if (itm == null)
                    {
                        itm = new Item();
                        if (itm.LoadByPrimaryKey(x.ItemID))
                        {
                            itmColl.AttachEntity(itm);
                        }
                    }
                    r["ItemID"] = x.ItemID;
                    r["ItemName"] = itm.ItemName;
                    r["SRItemType"] = itm.SRItemType;
                    if (!string.IsNullOrEmpty(x.TariffComponentID))
                    {
                        // tarif component
                        var tc = tcColl.Where(y => y.TariffComponentID == x.TariffComponentID).First();
                        r["TariffComponentID"] = x.TariffComponentID;
                        r["TariffComponentName"] = tc.TariffComponentName;
                        // COA
                        var coaMapQ = new ServiceUnitItemServiceCompMappingQuery("coaMap");
                        var coaR = new ChartOfAccountsQuery("coaR");
                        var coaD = new ChartOfAccountsQuery("coaD");
                        var slR = new SubLedgersQuery("slR");
                        var slD = new SubLedgersQuery("slD");

                        coaMapQ.LeftJoin(coaR).On(coaMapQ.ChartOfAccountIdIncome == coaR.ChartOfAccountId)
                            .LeftJoin(coaD).On(coaMapQ.ChartOfAccountIdDiscount == coaD.ChartOfAccountId)
                            .LeftJoin(slR).On(coaMapQ.SubledgerIdIncome == slR.SubLedgerId)
                            .LeftJoin(slD).On(coaMapQ.SubledgerIdDiscount == slD.SubLedgerId)
                            .Where(
                                coaMapQ.ServiceUnitID == x.ServiceUnitID,
                                coaMapQ.ItemID == x.ItemID,
                                coaMapQ.TariffComponentID == x.TariffComponentID,
                                coaMapQ.SRRegistrationType == x.SRRegistrationType
                            )
                            .Select(
                                coaR.ChartOfAccountCode.As("CoaCodeIncome"),
                                coaR.ChartOfAccountName.As("CoaNameIncome"),
                                slR.Description.As("SublIncome"),
                                coaD.ChartOfAccountCode.As("CoaCodeDisc"),
                                coaD.ChartOfAccountName.As("CoaNameDisc"),
                                slD.Description.As("SublDisc")
                            );
                        var dtMap = coaMapQ.LoadDataTable();
                        if (dtMap.Rows.Count > 0)
                        {
                            r["CoaCodeIncome"] = dtMap.Rows[0]["CoaCodeIncome"];
                            r["CoaNameIncome"] = dtMap.Rows[0]["CoaNameIncome"];
                            r["SublIncome"] = dtMap.Rows[0]["SublIncome"];
                            r["CoaCodeDisc"] = dtMap.Rows[0]["CoaCodeDisc"];
                            r["CoaNameDisc"] = dtMap.Rows[0]["CoaNameDisc"];
                            r["SublDisc"] = dtMap.Rows[0]["SublDisc"];
                        }
                        tbl.Rows.Add(r);
                    }
                    else
                    {
                        r["TariffComponentID"] = "-";
                        r["TariffComponentName"] = "-";

                        // item product
                        var pa = new ProductAccountQuery("pa");
                        var pag = new ProductAccountGuarantorGroupQuery("pag");
                        var i = new ItemQuery("i");
                        var coaR = new ChartOfAccountsQuery("coaR");
                        var coaD = new ChartOfAccountsQuery("coaD");
                        var slR = new SubLedgersQuery("slR");
                        var slD = new SubLedgersQuery("slD");

                        pa.InnerJoin(pag).On(pa.ProductAccountID == pag.ProductAccountID)
                            .InnerJoin(i).On(pag.ProductAccountID == i.ProductAccountID)
                            .LeftJoin(coaR).On(pa.ChartOfAccountIdIncomeIP == coaR.ChartOfAccountId)
                            .LeftJoin(coaD).On(pag.ChartOfAccountIdDiscountIP == coaD.ChartOfAccountId)
                            .LeftJoin(slR).On(pag.SubledgerIdIncomeIP == slR.SubLedgerId)
                            .LeftJoin(slD).On(pag.SubledgerIdDiscountIP == slD.SubLedgerId)
                            .Where(
                                i.ItemID == x.ItemID
                            )
                            .Select(
                                coaR.ChartOfAccountCode.As("CoaCodeIncome"),
                                coaR.ChartOfAccountName.As("CoaNameIncome"),
                                slR.Description.As("SublIncome"),
                                coaD.ChartOfAccountCode.As("CoaCodeDisc"),
                                coaD.ChartOfAccountName.As("CoaNameDisc"),
                                slD.Description.As("SublDisc")
                            );
                        var dtMap = pa.LoadDataTable();
                        if (dtMap.Rows.Count > 0)
                        {
                            r["CoaCodeIncome"] = dtMap.Rows[0]["CoaCodeIncome"];
                            r["CoaNameIncome"] = dtMap.Rows[0]["CoaNameIncome"];
                            r["SublIncome"] = dtMap.Rows[0]["SublIncome"];
                            r["CoaCodeDisc"] = dtMap.Rows[0]["CoaCodeDisc"];
                            r["CoaNameDisc"] = dtMap.Rows[0]["CoaNameDisc"];
                            r["SublDisc"] = dtMap.Rows[0]["SublDisc"];
                        }
                        tbl.Rows.Add(r);
                    }
                }
            }
            tbl.AcceptChanges();
            return tbl;
        }

        private DataTable TransactionItemPayments(string paymentNo)
        {
            DataTable tbl;

            var items = new TransPaymentItemOrderCollection();
            items.Query.Where(items.Query.PaymentNo == paymentNo);
            items.LoadAll();
            if (items.Count > 0)
            {
                var query = new TransPaymentItemOrderQuery("a");
                var qtcic = new TransChargesItemCompQuery("b");
                var qtc = new TransChargesQuery("c");
                var qitem = new ItemQuery("d");
                var qsu = new ServiceUnitQuery("e");
                var qunitcoa = new ServiceUnitItemServiceCompMappingQuery("f");
                var qcoa = new ChartOfAccountsQuery("g");
                var qcoa2 = new ChartOfAccountsQuery("h");
                var qsubl = new SubLedgersQuery("i");
                var qsubl2 = new SubLedgersQuery("j");
                var qtariffcomp = new TariffComponentQuery("k");
                var reg = new vwRegistrationForMappingCOAQuery("z");

                query.InnerJoin(qtcic).On(query.TransactionNo == qtcic.TransactionNo && query.SequenceNo == qtcic.SequenceNo);
                query.InnerJoin(qtariffcomp).On(qtcic.TariffComponentID == qtariffcomp.TariffComponentID);
                query.InnerJoin(qtc).On(query.TransactionNo == qtc.TransactionNo);
                query.InnerJoin(reg).On(qtc.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                query.LeftJoin(qunitcoa).On(
                    qtc.ToServiceUnitID == qunitcoa.ServiceUnitID &&
                    qtcic.TariffComponentID == qunitcoa.TariffComponentID &&
                    query.ItemID == qunitcoa.ItemID &&
                    reg.SRRegistrationType == qunitcoa.SRRegistrationType &&
                    reg.SRGuarantorIncomeGroup == qunitcoa.SRGuarantorIncomeGroup
                    );
                query.LeftJoin(qcoa).On(qunitcoa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                query.LeftJoin(qcoa2).On(qunitcoa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                query.LeftJoin(qsubl).On(qunitcoa.SubledgerIdIncome == qsubl.SubLedgerId);
                query.LeftJoin(qsubl2).On(qunitcoa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qitem.SRItemType,
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    query.ItemID,
                    qitem.ItemName,
                    qtariffcomp.TariffComponentID,
                    qtariffcomp.TariffComponentName,
                    qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    qsubl.Description.As("SublIncome"),
                    qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    qsubl2.Description.As("SublDisc"),
                    reg.SRRegistrationType
                    );
                query.Where(
                    query.PaymentNo == paymentNo,
                    qitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Package));
                query.es.Distinct = true;
                tbl = query.LoadDataTable();

                ///
                var pquery = new TransPaymentItemOrderQuery("a");
                var pqtcic = new TransChargesItemCompQuery("b");
                var pqtc = new TransChargesQuery("c");
                var pqitem = new ItemQuery("d");
                var pqsu = new ServiceUnitQuery("e");
                var pqunitcoa = new ServiceUnitItemServiceCompMappingQuery("f");
                var pqcoa = new ChartOfAccountsQuery("g");
                var pqcoa2 = new ChartOfAccountsQuery("h");
                var pqsubl = new SubLedgersQuery("i");
                var pqsubl2 = new SubLedgersQuery("j");
                var pqtariffcomp = new TariffComponentQuery("k");
                var pkqtc = new TransChargesQuery("l");
                var pkqitem = new ItemQuery("m");
                var pqtci = new TransChargesItemQuery("n");
                var preg = new vwRegistrationForMappingCOAQuery("z");

                pquery.InnerJoin(pkqtc).On(pquery.TransactionNo == pkqtc.TransactionNo)
                    .InnerJoin(pqtc).On(pkqtc.TransactionNo == pqtc.PackageReferenceNo)
                    .InnerJoin(pqtci).On(pqtc.TransactionNo == pqtci.TransactionNo)
                    .InnerJoin(pqtcic).On(pqtci.TransactionNo == pqtcic.TransactionNo && pqtci.SequenceNo == pqtcic.SequenceNo)
                    .InnerJoin(pqtariffcomp).On(pqtcic.TariffComponentID == pqtariffcomp.TariffComponentID)
                    .InnerJoin(preg).On(pqtc.RegistrationNo == preg.RegistrationNo)
                    .InnerJoin(pkqitem).On(pkqitem.ItemID == pquery.ItemID)
                    .InnerJoin(pqitem).On(pqtci.ItemID == pqitem.ItemID)
                    .InnerJoin(pqsu).On(pqtc.ToServiceUnitID == pqsu.ServiceUnitID)
                    .LeftJoin(pqunitcoa).On(
                        pqtc.ToServiceUnitID == pqunitcoa.ServiceUnitID &&
                        pqtcic.TariffComponentID == pqunitcoa.TariffComponentID &&
                        pqtci.ItemID == pqunitcoa.ItemID &&
                        preg.SRRegistrationType == pqunitcoa.SRRegistrationType &&
                        preg.SRGuarantorIncomeGroup == pqunitcoa.SRGuarantorIncomeGroup
                    )
                    .LeftJoin(pqcoa).On(pqunitcoa.ChartOfAccountIdIncome == pqcoa.ChartOfAccountId)
                    .LeftJoin(pqcoa2).On(pqunitcoa.ChartOfAccountIdDiscount == pqcoa2.ChartOfAccountId)
                    .LeftJoin(pqsubl).On(pqunitcoa.SubledgerIdIncome == pqsubl.SubLedgerId)
                    .LeftJoin(pqsubl2).On(pqunitcoa.SubledgerIdDiscount == pqsubl2.SubLedgerId);

                pquery.Select
                    (
                    pqitem.SRItemType,
                    pqsu.ServiceUnitID,
                    pqsu.ServiceUnitName,
                    pqtci.ItemID,
                    pqitem.ItemName,
                    pqtariffcomp.TariffComponentID,
                    pqtariffcomp.TariffComponentName,
                    pqcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    pqcoa.ChartOfAccountName.As("CoaNameIncome"),
                    pqsubl.Description.As("SublIncome"),
                    pqcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    pqcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    pqsubl2.Description.As("SublDisc"),
                    preg.SRRegistrationType
                    );
                pquery.Where(
                    pquery.PaymentNo == paymentNo,
                    pkqitem.SRItemType.In(ItemType.Package));
                pquery.es.Distinct = true;
                var mtbl = pquery.LoadDataTable();


                tbl.Merge(mtbl);
                ///

                query = new TransPaymentItemOrderQuery("a");
                qtc = new TransChargesQuery("c");
                qitem = new ItemQuery("d");
                qsu = new ServiceUnitQuery("e");

                var pa = new ProductAccountQuery("x");

                var qpacoa = new ServiceUnitProductAccountMappingQuery("f");
                qcoa = new ChartOfAccountsQuery("g");
                qcoa2 = new ChartOfAccountsQuery("h");
                qsubl = new SubLedgersQuery("i");
                qsubl2 = new SubLedgersQuery("j");
                var qreg = new RegistrationQuery("k");

                query.InnerJoin(qtc).On(query.TransactionNo == qtc.TransactionNo);
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                query.InnerJoin(qreg).On(qtc.RegistrationNo == qreg.RegistrationNo);

                query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                //query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                //query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                //query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                //query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qitem.SRItemType,
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    query.ItemID,
                    qitem.ItemName,
                    "<'-' AS TariffComponentID>",
                    "<'-' AS TariffComponentName>",

                    @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                    @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaNameIncome>",
                    @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                             ELSE x.SubledgerIdIncomeIGD
                        END)) AS SublIncome>",

                    //qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    //qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    //qsubl.Description.As("SublIncome"),

                    @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeDisc>",
                    @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                             ELSE x.ChartOfAccountIdDiscountIGD
                        END)) AS CoaNameDisc>",
                    @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                             WHEN k.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                             ELSE x.SubledgerIdDiscountIGD
                        END)) AS SublDisc>",
                    //,

                    //qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    //qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    //qsubl2.Description.As("SublDisc")
                    qreg.SRRegistrationType
                    );
                query.Where(query.PaymentNo == paymentNo, qitem.SRItemType.In(ItemType.Medical, ItemType.NonMedical));
                query.es.Distinct = true;
                DataTable tbl2 = query.LoadDataTable();

                tbl.Merge(tbl2);

                query = new TransPaymentItemOrderQuery("a");
                var qtp = new TransPrescriptionQuery("c");
                qitem = new ItemQuery("d");
                qsu = new ServiceUnitQuery("e");

                pa = new ProductAccountQuery("x");

                qpacoa = new ServiceUnitProductAccountMappingQuery("f");
                qcoa = new ChartOfAccountsQuery("g");
                qcoa2 = new ChartOfAccountsQuery("h");
                qsubl = new SubLedgersQuery("i");
                qsubl2 = new SubLedgersQuery("j");
                qreg = new RegistrationQuery("k");

                query.InnerJoin(qtp).On(query.TransactionNo == qtp.PrescriptionNo);
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtp.ServiceUnitID == qsu.ServiceUnitID);
                query.InnerJoin(qreg).On(qtp.RegistrationNo == qreg.RegistrationNo);

                query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qitem.SRItemType,
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    query.ItemID,
                    qitem.ItemName,
                    "<'-' AS TariffComponentID>",
                    "<'-' AS TariffComponentName>",
                    qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    qsubl.Description.As("SublIncome"),
                    qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    qsubl2.Description.As("SublDisc"),
                    qreg.SRRegistrationType
                    );
                query.Where(query.PaymentNo == paymentNo);
                query.es.Distinct = true;
                DataTable tbl3 = query.LoadDataTable();

                tbl.Merge(tbl3);
            }
            else
            {
                var ibs = new TransPaymentItemIntermBillCollection();
                ibs.Query.Where(ibs.Query.PaymentNo == paymentNo);
                ibs.LoadAll();
                if (ibs.Count > 0)
                {
                    var query = new TransPaymentItemIntermBillQuery("a");
                    var qcc = new CostCalculationQuery("b");
                    var qtcic = new TransChargesItemCompQuery("c");
                    var qtc = new TransChargesQuery("d");
                    var qitem = new ItemQuery("e");
                    var qsu = new ServiceUnitQuery("f");
                    var qunitcoa = new ServiceUnitItemServiceCompMappingQuery("g");
                    var qcoa = new ChartOfAccountsQuery("h");
                    var qcoa2 = new ChartOfAccountsQuery("i");
                    var qsubl = new SubLedgersQuery("j");
                    var qsubl2 = new SubLedgersQuery("k");
                    var qtariffcomp = new TariffComponentQuery("l");
                    var reg = new vwRegistrationForMappingCOAQuery("z");

                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qtcic).On(qcc.TransactionNo == qtcic.TransactionNo && qcc.SequenceNo == qtcic.SequenceNo);
                    query.InnerJoin(qtariffcomp).On(qtcic.TariffComponentID == qtariffcomp.TariffComponentID);
                    query.InnerJoin(qtc).On(qcc.TransactionNo == qtc.TransactionNo);
                    query.InnerJoin(reg).On(qtc.RegistrationNo == reg.RegistrationNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                    query.LeftJoin(qunitcoa).On(
                        qtc.ToServiceUnitID == qunitcoa.ServiceUnitID &&
                        qtcic.TariffComponentID == qunitcoa.TariffComponentID &&
                        qcc.ItemID == qunitcoa.ItemID &&
                        reg.SRRegistrationType == qunitcoa.SRRegistrationType &&
                        reg.SRGuarantorIncomeGroup == qunitcoa.SRGuarantorIncomeGroup
                        );
                    query.LeftJoin(qcoa).On(qunitcoa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                    query.LeftJoin(qcoa2).On(qunitcoa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                    query.LeftJoin(qsubl).On(qunitcoa.SubledgerIdIncome == qsubl.SubLedgerId);
                    query.LeftJoin(qsubl2).On(qunitcoa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                    query.Select
                        (
                        qitem.SRItemType,
                        qsu.ServiceUnitID,
                        qsu.ServiceUnitName,
                        qcc.ItemID,
                        qitem.ItemName,
                        qtariffcomp.TariffComponentID,
                        qtariffcomp.TariffComponentName,
                        qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                        qcoa.ChartOfAccountName.As("CoaNameIncome"),
                        qsubl.Description.As("SublIncome"),
                        qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                        qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                        qsubl2.Description.As("SublDisc"),
                        reg.SRRegistrationType
                        );
                    query.Where(
                        query.PaymentNo == paymentNo, 
                        qitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Package));
                    query.es.Distinct = true;
                    tbl = query.LoadDataTable();

                    ///
                    var mquery = new TransPaymentItemIntermBillQuery("a");
                    var mqcc = new CostCalculationQuery("b");
                    var mqtcic = new TransChargesItemCompQuery("c");
                    var mqtc = new TransChargesQuery("d");
                    var mqitem = new ItemQuery("e");
                    var mqsu = new ServiceUnitQuery("f");
                    var mqunitcoa = new ServiceUnitItemServiceCompMappingQuery("g");
                    var mqcoa = new ChartOfAccountsQuery("h");
                    var mqcoa2 = new ChartOfAccountsQuery("i");
                    var mqsubl = new SubLedgersQuery("j");
                    var mqsubl2 = new SubLedgersQuery("k");
                    var mqtariffcomp = new TariffComponentQuery("l");
                    var mreg = new vwRegistrationForMappingCOAQuery("z");
                    var mpqtc = new TransChargesQuery("cp");
                    var mqtci = new TransChargesItemQuery("m");
                    var mpqitem = new ItemQuery("ep");

                    mquery.InnerJoin(mqcc).On(mquery.IntermBillNo == mqcc.IntermBillNo)
                        .InnerJoin(mpqtc).On(mqcc.TransactionNo == mpqtc.TransactionNo)
                        .InnerJoin(mqtc).On(mpqtc.TransactionNo == mqtc.PackageReferenceNo)
                        .InnerJoin(mqtci).On(mqtc.TransactionNo == mqtci.TransactionNo)
                        .InnerJoin(mqtcic).On(mqtci.TransactionNo == mqtcic.TransactionNo && mqtci.SequenceNo == mqtcic.SequenceNo)
                        .InnerJoin(mqtariffcomp).On(mqtcic.TariffComponentID == mqtariffcomp.TariffComponentID)
                        .InnerJoin(mreg).On(mqtc.RegistrationNo == mreg.RegistrationNo)
                        .InnerJoin(mpqitem).On(mqcc.ItemID == mpqitem.ItemID)
                        .InnerJoin(mqitem).On(mqtci.ItemID == mqitem.ItemID)
                        .InnerJoin(mqsu).On(mqtc.ToServiceUnitID == mqsu.ServiceUnitID)
                        .LeftJoin(mqunitcoa).On(
                            mqtc.ToServiceUnitID == mqunitcoa.ServiceUnitID &&
                            mqtcic.TariffComponentID == mqunitcoa.TariffComponentID &&
                            mqtci.ItemID == mqunitcoa.ItemID &&
                            mreg.SRRegistrationType == mqunitcoa.SRRegistrationType &&
                            mreg.SRGuarantorIncomeGroup == mqunitcoa.SRGuarantorIncomeGroup
                        ).LeftJoin(mqcoa).On(mqunitcoa.ChartOfAccountIdIncome == mqcoa.ChartOfAccountId)
                        .LeftJoin(mqcoa2).On(mqunitcoa.ChartOfAccountIdDiscount == mqcoa2.ChartOfAccountId)
                        .LeftJoin(mqsubl).On(mqunitcoa.SubledgerIdIncome == mqsubl.SubLedgerId)
                        .LeftJoin(mqsubl2).On(mqunitcoa.SubledgerIdDiscount == mqsubl2.SubLedgerId);

                    mquery.Select
                        (
                        mqitem.SRItemType,
                        mqsu.ServiceUnitID,
                        mqsu.ServiceUnitName,
                        mqtci.ItemID,
                        mqitem.ItemName,
                        mqtariffcomp.TariffComponentID,
                        mqtariffcomp.TariffComponentName,
                        mqcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                        mqcoa.ChartOfAccountName.As("CoaNameIncome"),
                        mqsubl.Description.As("SublIncome"),
                        mqcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                        mqcoa2.ChartOfAccountName.As("CoaNameDisc"),
                        mqsubl2.Description.As("SublDisc"),
                        mreg.SRRegistrationType
                        );
                    mquery.Where(
                        mquery.PaymentNo == paymentNo,
                        mpqitem.SRItemType.In(ItemType.Package));
                    mquery.es.Distinct = true;
                    var mtbl = mquery.LoadDataTable();

                    tbl.Merge(mtbl);
                    ///

                    query = new TransPaymentItemIntermBillQuery("a");
                    qcc = new CostCalculationQuery("b");
                    qtc = new TransChargesQuery("d");
                    qitem = new ItemQuery("e");
                    qsu = new ServiceUnitQuery("f");

                    var pa = new ProductAccountQuery("x");

                    var qpacoa = new ServiceUnitProductAccountMappingQuery("g");
                    qcoa = new ChartOfAccountsQuery("h");
                    qcoa2 = new ChartOfAccountsQuery("i");
                    qsubl = new SubLedgersQuery("j");
                    qsubl2 = new SubLedgersQuery("k");
                    var qreg = new RegistrationQuery("l");

                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qtc).On(qcc.TransactionNo == qtc.TransactionNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                    query.InnerJoin(qreg).On(qtc.RegistrationNo == qreg.RegistrationNo);

                    query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                    //query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                    //query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                    //query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                    //query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);


                    query.Select
                        (
                        qitem.SRItemType,
                        qsu.ServiceUnitID,
                        qsu.ServiceUnitName,
                        qcc.ItemID,
                        qitem.ItemName,
                        "<'-' AS TariffComponentID>",
                        "<'-' AS TariffComponentName>",

                        @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaNameIncome>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                                 ELSE x.SubledgerIdIncomeIGD
                            END)) AS SublIncome>",

                        //qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                        //qcoa.ChartOfAccountName.As("CoaNameIncome"),
                        //qsubl.Description.As("SublIncome"),

                        @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaCodeDisc>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                                 ELSE x.ChartOfAccountIdDiscountIGD
                            END)) AS CoaNameDisc>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                                 ELSE x.SubledgerIdDiscountIGD
                            END)) AS SublDisc>",
                        //,

                        //qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                        //qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                        //qsubl2.Description.As("SublDisc")
                        qreg.SRRegistrationType
                        );
                    query.Where(query.PaymentNo == paymentNo, qitem.SRItemType.In(ItemType.Medical, ItemType.NonMedical));
                    query.es.Distinct = true;
                    DataTable tbl2 = query.LoadDataTable();

                    tbl.Merge(tbl2);

                    query = new TransPaymentItemIntermBillQuery("a");
                    qcc = new CostCalculationQuery("b");
                    var qtp = new TransPrescriptionQuery("d");
                    qitem = new ItemQuery("e");
                    qsu = new ServiceUnitQuery("f");

                    pa = new ProductAccountQuery("x");

                    qpacoa = new ServiceUnitProductAccountMappingQuery("g");
                    qcoa = new ChartOfAccountsQuery("h");
                    qcoa2 = new ChartOfAccountsQuery("i");
                    qsubl = new SubLedgersQuery("j");
                    qsubl2 = new SubLedgersQuery("k");
                    qreg = new RegistrationQuery("l");

                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qtp).On(qcc.TransactionNo == qtp.PrescriptionNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qsu).On(qtp.ServiceUnitID == qsu.ServiceUnitID);
                    query.InnerJoin(qreg).On(qtp.RegistrationNo == qreg.RegistrationNo);

                    query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                    query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                    query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                    query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                    query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                    query.Select
                        (
                        qitem.SRItemType,
                        qsu.ServiceUnitID,
                        qsu.ServiceUnitName,
                        qcc.ItemID,
                        qitem.ItemName,
                        "<'-' AS TariffComponentID>",
                        "<'-' AS TariffComponentName>",
                        qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                        qcoa.ChartOfAccountName.As("CoaNameIncome"),
                        qsubl.Description.As("SublIncome"),
                        qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                        qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                        qsubl2.Description.As("SublDisc"),
                        qreg.SRRegistrationType
                        );
                    query.Where(query.PaymentNo == paymentNo);
                    query.es.Distinct = true;
                    DataTable tbl3 = query.LoadDataTable();

                    tbl.Merge(tbl3);
                }
                else
                {
                    var query = new TransPaymentItemIntermBillGuarantorQuery("a");
                    var qcc = new CostCalculationQuery("b");
                    var qtcic = new TransChargesItemCompQuery("c");
                    var qtc = new TransChargesQuery("d");
                    var qitem = new ItemQuery("e");
                    var qsu = new ServiceUnitQuery("f");
                    var qunitcoa = new ServiceUnitItemServiceCompMappingQuery("g");
                    var qcoa = new ChartOfAccountsQuery("h");
                    var qcoa2 = new ChartOfAccountsQuery("i");
                    var qsubl = new SubLedgersQuery("j");
                    var qsubl2 = new SubLedgersQuery("k");
                    var qtariffcomp = new TariffComponentQuery("l");
                    var reg = new vwRegistrationForMappingCOAQuery("z");

                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qtcic).On(qcc.TransactionNo == qtcic.TransactionNo && qcc.SequenceNo == qtcic.SequenceNo);
                    query.InnerJoin(qtariffcomp).On(qtcic.TariffComponentID == qtariffcomp.TariffComponentID);
                    query.InnerJoin(qtc).On(qcc.TransactionNo == qtc.TransactionNo);
                    query.InnerJoin(reg).On(qtc.RegistrationNo == reg.RegistrationNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                    query.LeftJoin(qunitcoa).On(
                        qtc.ToServiceUnitID == qunitcoa.ServiceUnitID &&
                        qtcic.TariffComponentID == qunitcoa.TariffComponentID &&
                        qcc.ItemID == qunitcoa.ItemID &&
                        reg.SRRegistrationType == qunitcoa.SRRegistrationType &&
                        reg.SRGuarantorIncomeGroup == qunitcoa.SRGuarantorIncomeGroup
                        );
                    query.LeftJoin(qcoa).On(qunitcoa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                    query.LeftJoin(qcoa2).On(qunitcoa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                    query.LeftJoin(qsubl).On(qunitcoa.SubledgerIdIncome == qsubl.SubLedgerId);
                    query.LeftJoin(qsubl2).On(qunitcoa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                    query.Select
                        (
                        qitem.SRItemType,
                        qsu.ServiceUnitID,
                        qsu.ServiceUnitName,
                        qcc.ItemID,
                        qitem.ItemName,
                        qtariffcomp.TariffComponentID,
                        qtariffcomp.TariffComponentName,
                        qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                        qcoa.ChartOfAccountName.As("CoaNameIncome"),
                        qsubl.Description.As("SublIncome"),
                        qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                        qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                        qsubl2.Description.As("SublDisc"),
                        reg.SRRegistrationType
                        );
                    query.Where(
                        query.PaymentNo == paymentNo, 
                        qitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Package));
                    query.es.Distinct = true;
                    tbl = query.LoadDataTable();

                    ///
                    var mquery = new TransPaymentItemIntermBillGuarantorQuery("a");
                    var mqcc = new CostCalculationQuery("b");
                    var mqtcic = new TransChargesItemCompQuery("c");
                    var mqtc = new TransChargesQuery("d");
                    var mqitem = new ItemQuery("e");
                    var mqsu = new ServiceUnitQuery("f");
                    var mqunitcoa = new ServiceUnitItemServiceCompMappingQuery("g");
                    var mqcoa = new ChartOfAccountsQuery("h");
                    var mqcoa2 = new ChartOfAccountsQuery("i");
                    var mqsubl = new SubLedgersQuery("j");
                    var mqsubl2 = new SubLedgersQuery("k");
                    var mqtariffcomp = new TariffComponentQuery("l");
                    var mreg = new vwRegistrationForMappingCOAQuery("z");
                    var mpqtc = new TransChargesQuery("cp");
                    var mqtci = new TransChargesItemQuery("m");
                    var mpqitem = new ItemQuery("ep");

                    mquery.InnerJoin(mqcc).On(mquery.IntermBillNo == mqcc.IntermBillNo)
                        .InnerJoin(mpqtc).On(mqcc.TransactionNo == mpqtc.TransactionNo)
                        .InnerJoin(mqtc).On(mpqtc.TransactionNo == mqtc.PackageReferenceNo)
                        .InnerJoin(mqtci).On(mqtc.TransactionNo == mqtci.TransactionNo)
                        .InnerJoin(mqtcic).On(mqtci.TransactionNo == mqtcic.TransactionNo && mqtci.SequenceNo == mqtcic.SequenceNo)
                        .InnerJoin(mqtariffcomp).On(mqtcic.TariffComponentID == mqtariffcomp.TariffComponentID)
                        .InnerJoin(mreg).On(mqtc.RegistrationNo == mreg.RegistrationNo)
                        .InnerJoin(mpqitem).On(mqcc.ItemID == mpqitem.ItemID)
                        .InnerJoin(mqitem).On(mqtci.ItemID == mqitem.ItemID)
                        .InnerJoin(mqsu).On(mqtc.ToServiceUnitID == mqsu.ServiceUnitID)
                        .LeftJoin(mqunitcoa).On(
                            mqtc.ToServiceUnitID == mqunitcoa.ServiceUnitID &&
                            mqtcic.TariffComponentID == mqunitcoa.TariffComponentID &&
                            mqtci.ItemID == mqunitcoa.ItemID &&
                            mreg.SRRegistrationType == mqunitcoa.SRRegistrationType &&
                            mreg.SRGuarantorIncomeGroup == mqunitcoa.SRGuarantorIncomeGroup
                        ).LeftJoin(mqcoa).On(mqunitcoa.ChartOfAccountIdIncome == mqcoa.ChartOfAccountId)
                        .LeftJoin(mqcoa2).On(mqunitcoa.ChartOfAccountIdDiscount == mqcoa2.ChartOfAccountId)
                        .LeftJoin(mqsubl).On(mqunitcoa.SubledgerIdIncome == mqsubl.SubLedgerId)
                        .LeftJoin(mqsubl2).On(mqunitcoa.SubledgerIdDiscount == mqsubl2.SubLedgerId);

                    mquery.Select
                        (
                        mqitem.SRItemType,
                        mqsu.ServiceUnitID,
                        mqsu.ServiceUnitName,
                        mqtci.ItemID,
                        mqitem.ItemName,
                        mqtariffcomp.TariffComponentID,
                        mqtariffcomp.TariffComponentName,
                        mqcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                        mqcoa.ChartOfAccountName.As("CoaNameIncome"),
                        mqsubl.Description.As("SublIncome"),
                        mqcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                        mqcoa2.ChartOfAccountName.As("CoaNameDisc"),
                        mqsubl2.Description.As("SublDisc"),
                        mreg.SRRegistrationType
                        );
                    mquery.Where(
                        mquery.PaymentNo == paymentNo,
                        mpqitem.SRItemType.In(ItemType.Package));
                    mquery.es.Distinct = true;
                    var mtbl = mquery.LoadDataTable();
                    tbl.Merge(mtbl);
                    ///

                    query = new TransPaymentItemIntermBillGuarantorQuery("a");
                    qcc = new CostCalculationQuery("b");
                    qtc = new TransChargesQuery("d");
                    qitem = new ItemQuery("e");
                    qsu = new ServiceUnitQuery("f");

                    var pa = new ProductAccountQuery("x");
                    var pag = new ProductAccountGuarantorGroupQuery("pag");

                    var qpacoa = new ServiceUnitProductAccountMappingQuery("g");
                    qcoa = new ChartOfAccountsQuery("h");
                    qcoa2 = new ChartOfAccountsQuery("i");
                    qsubl = new SubLedgersQuery("j");
                    qsubl2 = new SubLedgersQuery("k");
                    var qreg = new RegistrationQuery("l");

                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qtc).On(qcc.TransactionNo == qtc.TransactionNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                    query.InnerJoin(qreg).On(qtc.RegistrationNo == qreg.RegistrationNo);

                    query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                    query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                    query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                    query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                    query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                    query.Select
                        (
                        qitem.SRItemType,
                        qsu.ServiceUnitID,
                        qsu.ServiceUnitName,
                        qcc.ItemID,
                        qitem.ItemName,
                        "<'-' AS TariffComponentID>",
                        "<'-' AS TariffComponentName>",

                        @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaNameIncome>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                                 ELSE x.SubledgerIdIncomeIGD
                            END)) AS SublIncome>",

                        //qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                        //qcoa.ChartOfAccountName.As("CoaNameIncome"),
                        //qsubl.Description.As("SublIncome"),

                        @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaCodeDisc>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                                 ELSE x.ChartOfAccountIdDiscountIGD
                            END)) AS CoaNameDisc>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                                 ELSE x.SubledgerIdDiscountIGD
                            END)) AS SublDisc>",
                        //,

                        //qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                        //qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                        //qsubl2.Description.As("SublDisc")
                        qreg.SRRegistrationType
                        );
                    query.Where(query.PaymentNo == paymentNo, qitem.SRItemType.In(ItemType.Medical, ItemType.NonMedical));
                    query.es.Distinct = true;
                    DataTable tbl2 = query.LoadDataTable();

                    tbl.Merge(tbl2);

                    query = new TransPaymentItemIntermBillGuarantorQuery("a");
                    qcc = new CostCalculationQuery("b");
                    var qtp = new TransPrescriptionQuery("d");
                    qitem = new ItemQuery("e");
                    qsu = new ServiceUnitQuery("f");

                    //pa = new ProductAccountQuery("x");
                    pag = new ProductAccountGuarantorGroupQuery("pag");

                    qpacoa = new ServiceUnitProductAccountMappingQuery("g");
                    qcoa = new ChartOfAccountsQuery("h");
                    qcoa2 = new ChartOfAccountsQuery("i");
                    qsubl = new SubLedgersQuery("j");
                    qsubl2 = new SubLedgersQuery("k");
                    //qreg = new RegistrationQuery("l");
                    var qcoaIPR = new ChartOfAccountsQuery("qcoaIPR");
                    var qcoaIPR2 = new ChartOfAccountsQuery("qcoaIPR2");
                    var qsublIPR = new SubLedgersQuery("qsublIPR");
                    var qsublIPR2 = new SubLedgersQuery("qsublIPR2");
                    var qcoaEMR = new ChartOfAccountsQuery("qcoaEMR");
                    var qcoaEMR2 = new ChartOfAccountsQuery("qcoaEMR2");
                    var qsublEMR = new SubLedgersQuery("qsublEMR");
                    var qsublEMR2 = new SubLedgersQuery("qsublEMR2");

                    query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                    query.InnerJoin(qtp).On(qcc.TransactionNo == qtp.PrescriptionNo);
                    query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                    query.InnerJoin(qsu).On(qtp.ServiceUnitID == qsu.ServiceUnitID);
                    query.InnerJoin(reg).On(qtp.RegistrationNo == reg.RegistrationNo);

                    //query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);
                    query.LeftJoin(pag).On(qitem.ProductAccountID == pag.ProductAccountID &
                        pag.SRGuarantorIncomeGroup == reg.SRGuarantorIncomeGroup);

                    query.LeftJoin(qcoa).On(pag.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                    query.LeftJoin(qcoa2).On(pag.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                    query.LeftJoin(qsubl).On(pag.SubledgerIdIncome == qsubl.SubLedgerId);
                    query.LeftJoin(qsubl2).On(pag.SubledgerIdDiscount == qsubl2.SubLedgerId);

                    query.LeftJoin(qcoaIPR).On(pag.ChartOfAccountIdIncomeIP == qcoaIPR.ChartOfAccountId);
                    query.LeftJoin(qcoaIPR2).On(pag.ChartOfAccountIdDiscountIP == qcoaIPR2.ChartOfAccountId);
                    query.LeftJoin(qsublIPR).On(pag.SubledgerIdIncomeIP == qsublIPR.SubLedgerId);
                    query.LeftJoin(qsublIPR2).On(pag.SubledgerIdDiscountIP == qsublIPR2.SubLedgerId);

                    query.LeftJoin(qcoaEMR).On(pag.ChartOfAccountIdIncomeIGD == qcoaEMR.ChartOfAccountId);
                    query.LeftJoin(qcoaEMR2).On(pag.ChartOfAccountIdDiscountIGD == qcoaEMR2.ChartOfAccountId);
                    query.LeftJoin(qsublEMR).On(pag.SubledgerIdIncomeIGD == qsublEMR.SubLedgerId);
                    query.LeftJoin(qsublEMR2).On(pag.SubledgerIdDiscountIGD == qsublEMR2.SubLedgerId);

                    query.Select
                        (
                        qitem.SRItemType,
                        qsu.ServiceUnitID,
                        qsu.ServiceUnitName,
                        qcc.ItemID,
                        qitem.ItemName,
                        "<'-' AS TariffComponentID>",
                        "<'-' AS TariffComponentName>",
                        "<case (z.SRRegistrationType) when 'EMR' THEN qcoaEMR.ChartOfAccountCode WHEN 'IPR' THEN qcoaIPR.ChartOfAccountCode ELSE h.ChartOfAccountCode END CoaCodeIncome>",
                        "<case (z.SRRegistrationType) when 'EMR' THEN qcoaEMR.ChartOfAccountName WHEN 'IPR' THEN qcoaIPR.ChartOfAccountName ELSE h.ChartOfAccountName END CoaNameIncome>",
                        "<case (z.SRRegistrationType) when 'EMR' THEN qsublEMR.Description WHEN 'IPR' THEN qsublIPR.Description ELSE j.Description END SublIncome>",
                        "<case (z.SRRegistrationType) when 'EMR' THEN qcoaEMR2.ChartOfAccountCode WHEN 'IPR' THEN qcoaIPR2.ChartOfAccountCode ELSE i.ChartOfAccountCode END CoaCodeDisc>",
                        "<case (z.SRRegistrationType) when 'EMR' THEN qcoaEMR2.ChartOfAccountName WHEN 'IPR' THEN qcoaIPR2.ChartOfAccountName ELSE i.ChartOfAccountName END CoaNameDisc>",
                        "<case (z.SRRegistrationType) when 'EMR' THEN qsublEMR2.Description WHEN 'IPR' THEN qsublIPR2.Description ELSE j.Description END SublDisc>",
                        reg.SRRegistrationType
                        );
                    query.Where(query.PaymentNo == paymentNo);
                    query.es.Distinct = true;
                    DataTable tbl3 = query.LoadDataTable();

                    tbl.Merge(tbl3);
                }
            }

            return tbl;
        }

        private DataTable TransactionItemPaymentReturns(string paymentNo)
        {
            DataTable tbl;

            var items = new TransPaymentItemOrderCollection();
            items.Query.Where(items.Query.PaymentNo == paymentNo);
            items.LoadAll();
            if (items.Count > 0)
            {
                var query = new TransPaymentItemOrderQuery("a");
                var qtcic = new TransChargesItemCompQuery("b");
                var qtc = new TransChargesQuery("c");
                var qitem = new ItemQuery("d");
                var qsu = new ServiceUnitQuery("e");
                var qunitcoa = new ServiceUnitItemServiceCompMappingQuery("f");
                var qcoa = new ChartOfAccountsQuery("g");
                var qcoa2 = new ChartOfAccountsQuery("h");
                var qsubl = new SubLedgersQuery("i");
                var qsubl2 = new SubLedgersQuery("j");
                var qtariffcomp = new TariffComponentQuery("k");
                var reg = new RegistrationQuery("z");

                query.InnerJoin(qtcic).On(query.TransactionNo == qtcic.TransactionNo && query.SequenceNo == qtcic.SequenceNo);
                query.InnerJoin(qtariffcomp).On(qtcic.TariffComponentID == qtariffcomp.TariffComponentID);
                query.InnerJoin(qtc).On(query.TransactionNo == qtc.TransactionNo);
                query.InnerJoin(reg).On(qtc.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                query.LeftJoin(qunitcoa).On(
                    qtc.ToServiceUnitID == qunitcoa.ServiceUnitID &&
                    qtcic.TariffComponentID == qunitcoa.TariffComponentID &&
                    query.ItemID == qunitcoa.ItemID &&
                    reg.SRRegistrationType == qunitcoa.SRRegistrationType
                    );
                query.LeftJoin(qcoa).On(qunitcoa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                query.LeftJoin(qcoa2).On(qunitcoa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                query.LeftJoin(qsubl).On(qunitcoa.SubledgerIdIncome == qsubl.SubLedgerId);
                query.LeftJoin(qsubl2).On(qunitcoa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    query.ItemID,
                    qitem.ItemName,
                    qtariffcomp.TariffComponentID,
                    qtariffcomp.TariffComponentName,
                    qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    qsubl.Description.As("SublIncome"),
                    qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    qsubl2.Description.As("SublDisc")
                    );
                query.Where(
                    query.PaymentNo == paymentNo, 
                    qitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical),
                    reg.SRRegistrationType != "MCU");
                query.es.Distinct = true;
                tbl = query.LoadDataTable();

                ///
                var mquery = new TransPaymentItemOrderQuery("a");
                var mqtcic = new TransChargesItemCompQuery("b");
                var mqtc = new TransChargesQuery("c");
                var mqitem = new ItemQuery("d");
                var mqsu = new ServiceUnitQuery("e");
                var mqunitcoa = new ServiceUnitItemServiceCompMappingQuery("f");
                var mqcoa = new ChartOfAccountsQuery("g");
                var mqcoa2 = new ChartOfAccountsQuery("h");
                var mqsubl = new SubLedgersQuery("i");
                var mqsubl2 = new SubLedgersQuery("j");
                var mqtariffcomp = new TariffComponentQuery("k");
                var mreg = new RegistrationQuery("z");

                mquery.InnerJoin(mqtcic).On(mquery.TransactionNo == mqtcic.TransactionNo && mquery.SequenceNo == mqtcic.SequenceNo);
                mquery.InnerJoin(mqtariffcomp).On(mqtcic.TariffComponentID == mqtariffcomp.TariffComponentID);
                mquery.InnerJoin(mqtc).On(mquery.TransactionNo == mqtc.TransactionNo);
                mquery.InnerJoin(mreg).On(mqtc.RegistrationNo == mreg.RegistrationNo);
                mquery.InnerJoin(mqitem).On(mquery.ItemID == mqitem.ItemID);
                mquery.InnerJoin(mqsu).On(mqtc.ToServiceUnitID == mqsu.ServiceUnitID);
                mquery.LeftJoin(mqunitcoa).On(
                    mqtc.ToServiceUnitID == mqunitcoa.ServiceUnitID &&
                    mqtcic.TariffComponentID == mqunitcoa.TariffComponentID &&
                    mquery.ItemID == mqunitcoa.ItemID &&
                    "OPR" == mqunitcoa.SRRegistrationType
                    );
                mquery.LeftJoin(mqcoa).On(mqunitcoa.ChartOfAccountIdIncome == mqcoa.ChartOfAccountId);
                mquery.LeftJoin(mqcoa2).On(mqunitcoa.ChartOfAccountIdDiscount == mqcoa2.ChartOfAccountId);
                mquery.LeftJoin(mqsubl).On(mqunitcoa.SubledgerIdIncome == mqsubl.SubLedgerId);
                mquery.LeftJoin(mqsubl2).On(mqunitcoa.SubledgerIdDiscount == mqsubl2.SubLedgerId);

                mquery.Select
                    (
                    mqsu.ServiceUnitID,
                    mqsu.ServiceUnitName,
                    mquery.ItemID,
                    mqitem.ItemName,
                    mqtariffcomp.TariffComponentID,
                    mqtariffcomp.TariffComponentName,
                    mqcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    mqcoa.ChartOfAccountName.As("CoaNameIncome"),
                    mqsubl.Description.As("SublIncome"),
                    mqcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    mqcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    mqsubl2.Description.As("SublDisc")
                    );
                mquery.Where(
                    mquery.PaymentNo == paymentNo,
                    mqitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical),
                    mreg.SRRegistrationType == "MCU");
                mquery.es.Distinct = true;
                var mtbl = mquery.LoadDataTable();

                tbl.Merge(mtbl);
                ///

                query = new TransPaymentItemOrderQuery("a");
                qtc = new TransChargesQuery("c");
                qitem = new ItemQuery("d");
                qsu = new ServiceUnitQuery("e");

                var pa = new ProductAccountQuery("x");

                var qpacoa = new ServiceUnitProductAccountMappingQuery("f");
                qcoa = new ChartOfAccountsQuery("g");
                qcoa2 = new ChartOfAccountsQuery("h");
                qsubl = new SubLedgersQuery("i");
                qsubl2 = new SubLedgersQuery("j");
                var qreg = new RegistrationQuery("k");

                query.InnerJoin(qtc).On(query.TransactionNo == qtc.TransactionNo);
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                query.InnerJoin(qreg).On(qtc.RegistrationNo == qreg.RegistrationNo);

                query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                //query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                //query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                //query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                //query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    query.ItemID,
                    qitem.ItemName,
                    "<'-' AS TariffComponentID>",
                    "<'-' AS TariffComponentName>",

                    @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                    @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaNameIncome>",
                    @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                             ELSE x.SubledgerIdIncomeIGD
                        END)) AS SublIncome>",

                    //qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    //qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    //qsubl.Description.As("SublIncome"),

                    @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeDisc>",
                    @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                             ELSE x.ChartOfAccountIdDiscountIGD
                        END)) AS CoaNameDisc>",
                    @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN e.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                             WHEN e.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                             ELSE x.SubledgerIdDiscountIGD
                        END)) AS SublDisc>"
                    //,

                    //qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    //qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    //qsubl2.Description.As("SublDisc")
                    );
                query.Where(query.PaymentNo == paymentNo, qitem.SRItemType.In(ItemType.Medical, ItemType.NonMedical));
                query.es.Distinct = true;
                DataTable tbl2 = query.LoadDataTable();

                tbl.Merge(tbl2);

                query = new TransPaymentItemOrderQuery("a");
                var qtp = new TransPrescriptionQuery("c");
                qitem = new ItemQuery("d");
                qsu = new ServiceUnitQuery("e");

                pa = new ProductAccountQuery("x");

                qpacoa = new ServiceUnitProductAccountMappingQuery("f");
                qcoa = new ChartOfAccountsQuery("g");
                qcoa2 = new ChartOfAccountsQuery("h");
                qsubl = new SubLedgersQuery("i");
                qsubl2 = new SubLedgersQuery("j");
                qreg = new RegistrationQuery("k");

                query.InnerJoin(qtp).On(query.TransactionNo == qtp.PrescriptionNo);
                query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtp.ServiceUnitID == qsu.ServiceUnitID);
                query.InnerJoin(qreg).On(qtp.RegistrationNo == qreg.RegistrationNo);

                query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                //query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                //query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                //query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                //query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    query.ItemID,
                    qitem.ItemName,
                    "<'-' AS TariffComponentID>",
                    "<'-' AS TariffComponentName>",

                    @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                    @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaNameIncome>",
                    @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                             ELSE x.SubledgerIdIncomeIGD
                        END)) AS SublIncome>",

                    //qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    //qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    //qsubl.Description.As("SublIncome"),

                    @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeDisc>",
                    @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN k.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                             ELSE x.ChartOfAccountIdDiscountIGD
                        END)) AS CoaNameDisc>",
                    @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                        (CASE WHEN k.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                             WHEN k.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                             ELSE x.SubledgerIdDiscountIGD
                        END)) AS SublDisc>"
                    //,

                    //qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    //qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    //qsubl2.Description.As("SublDisc")
                    );
                query.Where(query.PaymentNo == paymentNo);
                query.es.Distinct = true;
                DataTable tbl3 = query.LoadDataTable();

                tbl.Merge(tbl3);
            }
            else
            {
                var py = new TransPayment();
                py.LoadByPrimaryKey(paymentNo);

                var query = new TransPaymentItemIntermBillQuery("a");
                var qcc = new CostCalculationQuery("b");
                var qtcic = new TransChargesItemCompQuery("c");
                var qtc = new TransChargesQuery("d");
                var qitem = new ItemQuery("e");
                var qsu = new ServiceUnitQuery("f");
                var qunitcoa = new ServiceUnitItemServiceCompMappingQuery("g");
                var qcoa = new ChartOfAccountsQuery("h");
                var qcoa2 = new ChartOfAccountsQuery("i");
                var qsubl = new SubLedgersQuery("j");
                var qsubl2 = new SubLedgersQuery("k");
                var qtariffcomp = new TariffComponentQuery("l");
                var reg = new RegistrationQuery("z");

                query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                query.InnerJoin(qtcic).On(qcc.TransactionNo == qtcic.TransactionNo && qcc.SequenceNo == qtcic.SequenceNo);
                query.InnerJoin(qtariffcomp).On(qtcic.TariffComponentID == qtariffcomp.TariffComponentID);
                query.InnerJoin(qtc).On(qcc.TransactionNo == qtc.TransactionNo);
                query.InnerJoin(reg).On(qtc.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                query.LeftJoin(qunitcoa).On(
                    qtc.ToServiceUnitID == qunitcoa.ServiceUnitID &&
                    qtcic.TariffComponentID == qunitcoa.TariffComponentID &&
                    qcc.ItemID == qunitcoa.ItemID &&
                    reg.SRRegistrationType == qunitcoa.SRRegistrationType
                    );
                query.LeftJoin(qcoa).On(qunitcoa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                query.LeftJoin(qcoa2).On(qunitcoa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                query.LeftJoin(qsubl).On(qunitcoa.SubledgerIdIncome == qsubl.SubLedgerId);
                query.LeftJoin(qsubl2).On(qunitcoa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    qcc.ItemID,
                    qitem.ItemName,
                    qtariffcomp.TariffComponentID,
                    qtariffcomp.TariffComponentName,
                    qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    qsubl.Description.As("SublIncome"),
                    qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    qsubl2.Description.As("SublDisc")
                    );
                query.Where(
                    query.PaymentNo == py.PaymentReferenceNo, 
                    qitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical),
                    reg.SRRegistrationType != "MCU");
                query.es.Distinct = true;
                tbl = query.LoadDataTable();

                ///
                var mquery = new TransPaymentItemIntermBillQuery("a");
                var mqcc = new CostCalculationQuery("b");
                var mqtcic = new TransChargesItemCompQuery("c");
                var mqtc = new TransChargesQuery("d");
                var mqitem = new ItemQuery("e");
                var mqsu = new ServiceUnitQuery("f");
                var mqunitcoa = new ServiceUnitItemServiceCompMappingQuery("g");
                var mqcoa = new ChartOfAccountsQuery("h");
                var mqcoa2 = new ChartOfAccountsQuery("i");
                var mqsubl = new SubLedgersQuery("j");
                var mqsubl2 = new SubLedgersQuery("k");
                var mqtariffcomp = new TariffComponentQuery("l");
                var mreg = new RegistrationQuery("z");

                mquery.InnerJoin(mqcc).On(mquery.IntermBillNo == mqcc.IntermBillNo);
                mquery.InnerJoin(mqtcic).On(mqcc.TransactionNo == mqtcic.TransactionNo && mqcc.SequenceNo == mqtcic.SequenceNo);
                mquery.InnerJoin(mqtariffcomp).On(mqtcic.TariffComponentID == mqtariffcomp.TariffComponentID);
                mquery.InnerJoin(mqtc).On(mqcc.TransactionNo == mqtc.TransactionNo);
                mquery.InnerJoin(mreg).On(mqtc.RegistrationNo == mreg.RegistrationNo);
                mquery.InnerJoin(mqitem).On(mqcc.ItemID == mqitem.ItemID);
                mquery.InnerJoin(mqsu).On(mqtc.ToServiceUnitID == mqsu.ServiceUnitID);
                mquery.LeftJoin(mqunitcoa).On(
                    mqtc.ToServiceUnitID == mqunitcoa.ServiceUnitID &&
                    mqtcic.TariffComponentID == mqunitcoa.TariffComponentID &&
                    mqcc.ItemID == mqunitcoa.ItemID &&
                    "OPR" == mqunitcoa.SRRegistrationType
                    );
                mquery.LeftJoin(mqcoa).On(mqunitcoa.ChartOfAccountIdIncome == mqcoa.ChartOfAccountId);
                mquery.LeftJoin(mqcoa2).On(mqunitcoa.ChartOfAccountIdDiscount == mqcoa2.ChartOfAccountId);
                mquery.LeftJoin(mqsubl).On(mqunitcoa.SubledgerIdIncome == mqsubl.SubLedgerId);
                mquery.LeftJoin(mqsubl2).On(mqunitcoa.SubledgerIdDiscount == mqsubl2.SubLedgerId);

                mquery.Select
                    (
                    mqsu.ServiceUnitID,
                    mqsu.ServiceUnitName,
                    mqcc.ItemID,
                    mqitem.ItemName,
                    mqtariffcomp.TariffComponentID,
                    mqtariffcomp.TariffComponentName,
                    mqcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    mqcoa.ChartOfAccountName.As("CoaNameIncome"),
                    mqsubl.Description.As("SublIncome"),
                    mqcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    mqcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    mqsubl2.Description.As("SublDisc")
                    );
                mquery.Where(
                    mquery.PaymentNo == py.PaymentReferenceNo,
                    mqitem.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical),
                    mreg.SRRegistrationType == "MCU");
                mquery.es.Distinct = true;
                var mtbl = mquery.LoadDataTable();

                tbl.Merge(mtbl);
                ///

                query = new TransPaymentItemIntermBillQuery("a");
                qcc = new CostCalculationQuery("b");
                qtc = new TransChargesQuery("d");
                qitem = new ItemQuery("e");
                qsu = new ServiceUnitQuery("f");

                var pa = new ProductAccountQuery("x");

                var qpacoa = new ServiceUnitProductAccountMappingQuery("g");
                qcoa = new ChartOfAccountsQuery("h");
                qcoa2 = new ChartOfAccountsQuery("i");
                qsubl = new SubLedgersQuery("j");
                qsubl2 = new SubLedgersQuery("k");
                var qreg = new RegistrationQuery("l");

                query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                query.InnerJoin(qtc).On(qcc.TransactionNo == qtc.TransactionNo);
                query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
                query.InnerJoin(qreg).On(qtc.RegistrationNo == qreg.RegistrationNo);

                query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                //query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                //query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                //query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                //query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    qcc.ItemID,
                    qitem.ItemName,
                    "<'-' AS TariffComponentID>",
                    "<'-' AS TariffComponentName>",

                        @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaNameIncome>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                                 ELSE x.SubledgerIdIncomeIGD
                            END)) AS SublIncome>",

                        //qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    //qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    //qsubl.Description.As("SublIncome"),

                        @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaCodeDisc>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                                 ELSE x.ChartOfAccountIdDiscountIGD
                            END)) AS CoaNameDisc>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                                 ELSE x.SubledgerIdDiscountIGD
                            END)) AS SublDisc>"
                    //,

                        //qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    //qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    //qsubl2.Description.As("SublDisc")
                    );
                query.Where(query.PaymentNo == py.PaymentReferenceNo, qitem.SRItemType.In(ItemType.Medical, ItemType.NonMedical));
                query.es.Distinct = true;
                DataTable tbl2 = query.LoadDataTable();

                tbl.Merge(tbl2);

                query = new TransPaymentItemIntermBillQuery("a");
                qcc = new CostCalculationQuery("b");
                var qtp = new TransPrescriptionQuery("d");
                qitem = new ItemQuery("e");
                qsu = new ServiceUnitQuery("f");

                pa = new ProductAccountQuery("x");

                qpacoa = new ServiceUnitProductAccountMappingQuery("g");
                qcoa = new ChartOfAccountsQuery("h");
                qcoa2 = new ChartOfAccountsQuery("i");
                qsubl = new SubLedgersQuery("j");
                qsubl2 = new SubLedgersQuery("k");
                qreg = new RegistrationQuery("l");

                query.InnerJoin(qcc).On(query.IntermBillNo == qcc.IntermBillNo);
                query.InnerJoin(qtp).On(qcc.TransactionNo == qtp.PrescriptionNo);
                query.InnerJoin(qitem).On(qcc.ItemID == qitem.ItemID);
                query.InnerJoin(qsu).On(qtp.ServiceUnitID == qsu.ServiceUnitID);
                query.InnerJoin(qreg).On(qtp.RegistrationNo == qreg.RegistrationNo);

                query.LeftJoin(pa).On(qitem.ProductAccountID == pa.ProductAccountID);

                query.LeftJoin(qcoa).On(pa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
                query.LeftJoin(qcoa2).On(pa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
                query.LeftJoin(qsubl).On(pa.SubledgerIdIncome == qsubl.SubLedgerId);
                query.LeftJoin(qsubl2).On(pa.SubledgerIdDiscount == qsubl2.SubLedgerId);

                query.Select
                    (
                    qsu.ServiceUnitID,
                    qsu.ServiceUnitName,
                    qcc.ItemID,
                    qitem.ItemName,
                    "<'-' AS TariffComponentID>",
                    "<'-' AS TariffComponentName>",

                    @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                        (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                             WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                             ELSE x.ChartOfAccountIdIncomeIGD
                        END)) AS CoaCodeIncome>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaNameIncome>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdIncomeIP
                                 ELSE x.SubledgerIdIncomeIGD
                            END)) AS SublIncome>",

                        //qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                    //qcoa.ChartOfAccountName.As("CoaNameIncome"),
                    //qsubl.Description.As("SublIncome"),

                        @"<(SELECT c.ChartOfAccountCode FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdIncomeIP
                                 ELSE x.ChartOfAccountIdIncomeIGD
                            END)) AS CoaCodeDisc>",
                        @"<(SELECT c.ChartOfAccountName FROM ChartOfAccounts c WHERE c.ChartOfAccountId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.ChartOfAccountIdIncome
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.ChartOfAccountIdDiscountIP
                                 ELSE x.ChartOfAccountIdDiscountIGD
                            END)) AS CoaNameDisc>",
                        @"<(SELECT c.[Description] FROM SubLedgers c WHERE c.SubLedgerId = 
                            (CASE WHEN l.SRRegistrationType = 'OPR' THEN x.SubledgerIdDiscount
                                 WHEN l.SRRegistrationType = 'IPR' THEN x.SubledgerIdDiscountIP
                                 ELSE x.SubledgerIdDiscountIGD
                            END)) AS SublDisc>"
                    //,

                        //qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                    //qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                    //qsubl2.Description.As("SublDisc")
                    );
                query.Where(query.PaymentNo == py.PaymentReferenceNo);
                query.es.Distinct = true;
                DataTable tbl3 = query.LoadDataTable();

                tbl.Merge(tbl3);
            }

            return tbl;
        }

        private DataTable TransactionItemDownPayments(string paymentNo)
        {
            var query = new TransPaymentItemOrderQuery("a");
            var qtcic = new TransChargesItemCompQuery("b");
            var qtc = new TransChargesQuery("c");
            var qitem = new ItemQuery("d");
            var qsu = new ServiceUnitQuery("e");
            var qunitcoa = new ServiceUnitItemServiceCompMappingQuery("f");
            var qcoa = new ChartOfAccountsQuery("g");
            var qcoa2 = new ChartOfAccountsQuery("h");
            var qsubl = new SubLedgersQuery("i");
            var qsubl2 = new SubLedgersQuery("j");
            var qtariffcomp = new TariffComponentQuery("k");
            var reg = new RegistrationQuery("z");

            query.InnerJoin(qtcic).On(query.TransactionNo == qtcic.TransactionNo && query.SequenceNo == qtcic.SequenceNo);
            query.InnerJoin(qtariffcomp).On(qtcic.TariffComponentID == qtariffcomp.TariffComponentID);
            query.InnerJoin(qtc).On(query.TransactionNo == qtc.TransactionNo);
            query.InnerJoin(reg).On(qtc.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(qitem).On(query.ItemID == qitem.ItemID);
            query.InnerJoin(qsu).On(qtc.ToServiceUnitID == qsu.ServiceUnitID);
            query.InnerJoin(qunitcoa).On(
                qtc.ToServiceUnitID == qunitcoa.ServiceUnitID &&
                qtcic.TariffComponentID == qunitcoa.TariffComponentID &&
                query.ItemID == qunitcoa.ItemID
                );
            query.LeftJoin(qcoa).On(qunitcoa.ChartOfAccountIdIncome == qcoa.ChartOfAccountId);
            query.LeftJoin(qcoa2).On(qunitcoa.ChartOfAccountIdDiscount == qcoa2.ChartOfAccountId);
            query.LeftJoin(qsubl).On(qunitcoa.SubledgerIdIncome == qsubl.SubLedgerId);
            query.LeftJoin(qsubl2).On(qunitcoa.SubledgerIdDiscount == qsubl2.SubLedgerId);

            query.Select
                (
                qsu.ServiceUnitID,
                qsu.ServiceUnitName,
                query.ItemID,
                qitem.ItemName,
                qtariffcomp.TariffComponentID,
                qtariffcomp.TariffComponentName,
                qcoa.ChartOfAccountCode.As("CoaCodeIncome"),
                qcoa.ChartOfAccountName.As("CoaNameIncome"),
                qsubl.Description.As("SublIncome"),
                qcoa2.ChartOfAccountCode.As("CoaCodeDisc"),
                qcoa2.ChartOfAccountName.As("CoaNameDisc"),
                qsubl2.Description.As("SublDisc")
                );
            query.Where(query.PaymentNo == paymentNo);
            query.es.Distinct = true;
            DataTable tbl = query.LoadDataTable();

            return tbl;
        }

        protected void grdTransItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransItem.DataSource = TransactionItems;
        }

        protected void cboSettingStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "0") TransactionItems = null;
            else
            {
                var table = TransactionItems.AsEnumerable().Where(t => string.IsNullOrEmpty(t.Field<string>("CoaCodeIncome")));
                if (table.Any()) TransactionItems = table.CopyToDataTable();
                else TransactionItems.Rows.Clear();
            }
            grdTransItem.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind")
            {
                TransactionItems = null;
                grdTransItem.Rebind();
            }
        }
    }
}
