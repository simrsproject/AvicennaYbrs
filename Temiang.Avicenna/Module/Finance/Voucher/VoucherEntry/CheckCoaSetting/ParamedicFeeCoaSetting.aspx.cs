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
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting
{
    public partial class ParamedicFeeCoaSetting : BasePageDialog
    {
        private JournalTransactions JournalTrans
        {
            get
            {
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

                ViewState["TransactionNo"] = string.Empty;
            }
        }

        private DataTable ParamedicFeePayable() {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("SRRegistrationType", typeof(string));
            tbl.Columns.Add("ServiceUnitID", typeof(string));
            tbl.Columns.Add("ServiceUnitName", typeof(string));
            tbl.Columns.Add("ItemID", typeof(string));
            tbl.Columns.Add("ItemName", typeof(string));
            tbl.Columns.Add("TariffComponentID", typeof(string));
            tbl.Columns.Add("TariffComponentName", typeof(string));
            tbl.Columns.Add("ChartOfAccountCode", typeof(string));
            tbl.Columns.Add("ChartOfAccountName", typeof(string));
            tbl.Columns.Add("SubLedgerId", typeof(string));
            tbl.Columns.Add("SubLedgerName", typeof(string));

            // get data from message
            var msgColl = new JournalMessageCollection();
            msgColl.Query.Where(msgColl.Query.JournalID == JournalTrans.JournalId);
            if (msgColl.LoadAll())
            {
                if (!string.IsNullOrEmpty(msgColl[0].AdditionalData)) {
                    var revs = JsonConvert
                    .DeserializeObject<List<JournalTransactions.CoaMapping>>(msgColl[0].AdditionalData);

                    var suColl = new ServiceUnitCollection();
                    var itmColl = new ItemCollection();
                    var tcColl = new TariffComponentCollection();
                    tcColl.LoadAll();

                    List<string> keys = new List<string>();
                    foreach (var x in revs.Distinct())
                    {
                        var r = tbl.NewRow();
                        r["SRRegistrationType"] = x.SRRegistrationType;
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
                        if (!string.IsNullOrEmpty(x.TariffComponentID))
                        {
                            // tarif component
                            var tc = tcColl.Where(y => y.TariffComponentID == x.TariffComponentID).First();
                            r["TariffComponentID"] = x.TariffComponentID;
                            r["TariffComponentName"] = tc.TariffComponentName;
                            // COA
                            var coaMapQ = new ServiceUnitItemServiceCompMappingQuery("coaMap");
                            var coaC = new ChartOfAccountsQuery("coaC");
                            var slC = new SubLedgersQuery("slC");

                            coaMapQ.LeftJoin(coaC).On(coaMapQ.ChartOfAccountIdCost == coaC.ChartOfAccountId)
                                .LeftJoin(slC).On(coaMapQ.SubledgerIdCost == slC.SubLedgerId)
                                .Where(
                                    coaMapQ.ServiceUnitID == x.ServiceUnitID,
                                    coaMapQ.ItemID == x.ItemID,
                                    coaMapQ.TariffComponentID == x.TariffComponentID,
                                    coaMapQ.SRRegistrationType == x.SRRegistrationType
                                )
                                .Select(
                                    coaC.ChartOfAccountCode.As("ChartOfAccountCode"),
                                    coaC.ChartOfAccountName.As("ChartOfAccountName"),
                                    slC.SubLedgerId.As("SubLedgerId"),
                                    slC.Description.As("SubLedgerName")
                                );
                            var dtMap = coaMapQ.LoadDataTable();
                            if (dtMap.Rows.Count > 0)
                            {
                                r["ChartOfAccountCode"] = dtMap.Rows[0]["ChartOfAccountCode"];
                                r["ChartOfAccountName"] = dtMap.Rows[0]["ChartOfAccountName"];
                                r["SubLedgerId"] = dtMap.Rows[0]["SubLedgerId"];
                                r["SubLedgerName"] = dtMap.Rows[0]["SubLedgerName"];
                            }
                            tbl.Rows.Add(r);
                        }
                    }
                }
            }
            tbl.AcceptChanges();
            return tbl;
        }

        private DataTable ParamedicFees
        {
            get
            {
                if (ViewState["ParamedicFee"] != null)
                    return (DataTable)ViewState["ParamedicFee"];

                DataTable tbl = null;

                if (JournalTrans.JournalType == "48")
                {
                    tbl = ParamedicFeePayable(); // settingan jurnal recal billing untuk accrual sama dengan piutang dalam perawatan cash basis adalah sama
                }
                else
                {
                    var jt = new JournalTransactionsQuery("jt");
                    var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                    var r = new vwRegistrationForMappingCOAQuery("r");
                    var i = new ItemQuery("i");
                    var tc2 = new TariffComponentQuery("tc2");
                    var tc = new TransChargesQuery("tc");
                    var su = new ServiceUnitQuery("su");
                    var suiscm = new ServiceUnitItemServiceCompMappingQuery("suiscm");
                    var coa = new ChartOfAccountsQuery("coa");
                    var sl = new SubLedgersQuery("sl");

                    var journalId = Request.QueryString["ivd"];

                    jt.InnerJoin(fee).On(jt.RefferenceNumber == fee.VerificationNo)
                        .InnerJoin(r).On(fee.RegistrationNo == r.RegistrationNo)
                        .InnerJoin(i).On(fee.ItemID == i.ItemID)
                        .InnerJoin(tc2).On(fee.TariffComponentID == tc2.TariffComponentID)
                        .InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                        .InnerJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                        .LeftJoin(suiscm).On(
                            fee.ItemID == suiscm.ItemID &&
                            fee.TariffComponentID == suiscm.TariffComponentID &&
                            tc.ToServiceUnitID == suiscm.ServiceUnitID &&
                            r.SRRegistrationType == suiscm.SRRegistrationType &&
                            r.SRGuarantorIncomeGroup == suiscm.SRGuarantorIncomeGroup)
                        .LeftJoin(coa).On(suiscm.ChartOfAccountIdCost == coa.ChartOfAccountId)
                        .LeftJoin(sl).On(suiscm.SubledgerIdCost == sl.SubLedgerId)
                        .Where(jt.JournalId == journalId)
                        .Select(
                            r.SRRegistrationType,
                            su.ServiceUnitID,
                            su.ServiceUnitName,
                            i.ItemID,
                            i.ItemName,
                            tc2.TariffComponentID, tc2.TariffComponentName,
                            coa.ChartOfAccountCode,
                            coa.ChartOfAccountName,
                            sl.SubLedgerId,
                            sl.SubLedgerName
                        );

                    jt.es.Distinct = true;

                    tbl = jt.LoadDataTable();
                }    

                ViewState["ParamedicFee"] = tbl;
                return tbl;
            }
            set
            { ViewState["ParamedicFee"] = value; }
        }

        protected void grdDebit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDebit.DataSource = ParamedicFees;
        }

        private DataTable ParamedicFeeAP
        {
            get
            {
                if (ViewState["ParamedicFeeAP"] != null)
                    return (DataTable)ViewState["ParamedicFeeAP"];

                var jt = new JournalTransactionsQuery("jt");
                var pfv = new ParamedicFeeVerificationQuery("pfv");
                var p = new ParamedicQuery("p");
                var coa = new ChartOfAccountsQuery("coa");

                var journalId = Request.QueryString["ivd"];

                jt.InnerJoin(pfv).On(jt.RefferenceNumber == pfv.VerificationNo)
                    .InnerJoin(p).On(pfv.ParamedicID == p.ParamedicID)
                    .InnerJoin(coa).On(p.ChartOfAccountIdAPParamedicFee == coa.ChartOfAccountId)
                    .Where(jt.JournalId == journalId)
                    .Select(
                        pfv.ParamedicID, p.ParamedicName,
                        coa.ChartOfAccountCode, coa.ChartOfAccountName
                    );

                jt.es.Distinct = true;

                DataTable tbl = jt.LoadDataTable();

                ViewState["ParamedicFeeAP"] = tbl;
                return tbl;
            }
            set
            { ViewState["ParamedicFeeAP"] = value; }
        }

        protected void grdCredit_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCredit.DataSource = ParamedicFeeAP;
        }
        protected void cboSettingStatus_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "0") ParamedicFees = null;
            else
            {
                var table = ParamedicFees.AsEnumerable().Where(t => string.IsNullOrEmpty(t.Field<string>("ChartOfAccountCode")));
                if (table.Any()) ParamedicFees = table.CopyToDataTable();
                else ParamedicFees.Rows.Clear();
            }
            grdDebit.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadGrid && eventArgument == "rebind")
            {
                ParamedicFees = null;
                grdDebit.Rebind();
            }
        }
    }
}
