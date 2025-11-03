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
using DocumentFormat.OpenXml.Drawing;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeePaymentV3
{
    public partial class ParamedicFeePaymentList : BasePage
    {
        private AppAutoNumberLast _autoNumber;

        public bool IsUnapprovable()
        {
            UserAccess access = this.UserAccess;
            if (access.IsExist)
            {
                return access.IsUnApprovalAble;
            }
            return false;
        }
        public bool IsVoidable()
        {
            UserAccess access = this.UserAccess;
            if (access.IsExist)
            {
                return access.IsVoidAble;
            }
            return false;
        }

        public bool IsFeeCalculatedOnTransaction()
        {
            return ParamedicFee.V2.ParamedicFeeVerificationByDischargeDateList.IsFeeCalculatedOnTransaction();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicFeePayment;

            // calculation will take a long time, 
            var ajxmgr = (RadScriptManager)Common.Helper.FindControlRecursive(this, "fw_RadScriptManager");
            ajxmgr.AsyncPostBackTimeout = 600;

            if (!IsPostBack)
            {
                SelectedKeys = null;

                // guarantor type
                StandardReference.InitializeIncludeSpace(cboGuarantorType, AppEnum.StandardReference.GuarantorType);

                RadToolBar2.Items[1].Visible = AppSession.Parameter.IsParamedicFeePaymentEnableDraft;
                //trDraftNo.Style.Clear();
                //if (!AppSession.Parameter.IsParamedicFeePaymentEnableDraft) {
                //    trDraftNo.Style.Add(HtmlTextWriterStyle.Display, "none");
                //}

                //grdListVGN.Columns.FindByUniqueName("IsDraft").Visible = AppSession.Parameter.IsParamedicFeePaymentEnableDraft;

                //tdGuaranteeFee.Visible = AppSession.Parameter.IsParamedicFeePaymentEnableGuaranteeFee;
                var enableGFee = AppSession.Parameter.IsParamedicFeePaymentEnableGuaranteeFee;
                if(!enableGFee) tdGuaranteeFee.Attributes.Add("style", "display:none;");
                grdList.Columns.FindByUniqueName("FeeGuarantee").Visible = enableGFee;
                chkEnableGuaranteeFee.Checked = enableGFee;
                grdListVGN.MasterTableView.DetailTables[0].GetColumn("AmountGuarantee").Visible = enableGFee;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
            grdListVGN.Rebind();
        }

        protected void ToggleSelectedPaidState(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdList.MasterTableView.Items)
            {
                var chk = (CheckBox)i.FindControl("detailChkbox");
                var chkPaid = (CheckBox)i.FindControl("paidChkbox");

                chk.Checked = chkPaid.Checked && ((CheckBox)sender).Checked;
            }
        }

        public List<string> SetSelectedByParamedicID(string ParamedicID, DataTable dtfee, DataTable dtdeduc, bool IsChecked)
        {
            Dictionary<string, string> NewSelected = new Dictionary<string, string>();
            foreach (System.Data.DataRow row in dtfee.AsEnumerable().Where(r => r["ParamedicID"].ToString() == ParamedicID))
            {
                if (IsChecked)
                {
                    NewSelected.Add(row["Id"].ToString(), ParamedicID);
                }
            }
            foreach (System.Data.DataRow row in dtdeduc.AsEnumerable().Where(r => r["ParamedicID"].ToString() == ParamedicID))
            {
                if (IsChecked)
                {
                    NewSelected.Add(row["Id"].ToString(), ParamedicID);
                }
            }

            Dictionary<string, string> ls = SelectedKeys;

            // remove unselected
            var lx = ls.Where(x => !NewSelected.Select(y => y.Key).Contains(x.Key) &&
                x.Value == ParamedicID);
            for (var i = lx.Count() - 1; i >= 0; i--)
            {
                ls.Remove(lx.ElementAt(i).Key);
            }

            // add new
            foreach (var n in NewSelected)
            {
                if (!ls.ContainsKey(n.Key))
                {
                    ls.Add(n.Key, n.Value);
                }
            }

            Session["SelectedFeeTransPayment"] = ls;

            return ls.Select(x => x.Key).ToList();
        }

        public Dictionary<string, string> SelectedKeys
        {
            get {
                Dictionary<string, string> ls = new Dictionary<string, string>();
                if (Session["SelectedFeeTransPayment"] != null)
                {
                    return (Dictionary<string, string>)(Session["SelectedFeeTransPayment"]);
                }
                else {
                    return new Dictionary<string, string>();
                }
            }
            set {
                Session["SelectedFeeTransPayment"] = value;
            }
        }

        public DataTable GetDataSelected(DataTable dtfee, DataTable dtdeduc) {
            // bikin summary untuk tampilan
            DataTable ds = new DataTable();
            ds.Columns.Add("ParamedicID", typeof(string));
            ds.Columns.Add("ParamedicName", typeof(string));
            ds.Columns.Add("Fee4Service", typeof(decimal));
            ds.Columns.Add("FeeAddDec", typeof(decimal));
            ds.Columns.Add("Fee4ServiceSelected", typeof(decimal));
            ds.Columns.Add("FeeAddDecSelected", typeof(decimal));
            ds.Columns.Add("FeeGuarantee", typeof(decimal));
            ds.Columns.Add("FeePaidInGuaranteePeriod", typeof(decimal));


            var parColl = new ParamedicCollection();
            parColl.LoadAll();

            foreach (DataRow row in dtfee.Rows)
            {
                var r = ds.AsEnumerable().Where(x => x["ParamedicID"].ToString() == row["ParamedicID"].ToString()).FirstOrDefault();
                if (r == null)
                {
                    r = ds.NewRow();
                    r["ParamedicID"] = row["ParamedicID"];
                    r["ParamedicName"] = row["ParamedicName"];
                    r["Fee4Service"] = (decimal)row["FeeAmount"];
                    r["FeeAddDec"] = 0;
                    r["Fee4ServiceSelected"] = 0;
                    r["FeeAddDecSelected"] = 0;
                    r["FeeGuarantee"] = 0;
                    r["FeePaidInGuaranteePeriod"] = 0;
                    ds.Rows.Add(r);
                }
                else
                {
                    r["Fee4Service"] = (decimal)r["Fee4Service"] + (decimal)row["FeeAmount"];
                }

                if (SelectedKeys.ContainsKey(row["Id"].ToString()))
                {
                    r["Fee4ServiceSelected"] = (decimal)r["Fee4ServiceSelected"] + (decimal)row["FeeAmount"];
                }

                // guarantee fee
                if ((decimal)r["Fee4ServiceSelected"] != 0) {
                    if (chkEnableGuaranteeFee.Checked)
                    {
                        if (rdpGFeeFrom.SelectedDate.HasValue && rdpGFeeTo.SelectedDate.HasValue)
                        {
                            var par = parColl.Where(p => p.ParamedicID == r["ParamedicID"].ToString()).First();
                            if ((par.GuaranteeFee ?? 0) > 0 && ((decimal)r["Fee4ServiceSelected"] < par.GuaranteeFee.Value))
                            {
                                r["FeeGuarantee"] = par.GuaranteeFee.Value - (decimal)r["Fee4ServiceSelected"];

                                var fpgdColl = new ParamedicFeePaymentGroupDetailCollection();
                                var fpgd = new ParamedicFeePaymentGroupDetailQuery("pfgd");
                                var fpg = new ParamedicFeePaymentGroupQuery("fpg");
                                fpgd.InnerJoin(fpg).On(fpgd.PaymentGroupNo == fpg.PaymentGroupNo)
                                    .Where(
                                        fpg.PaymentDate.Between(rdpGFeeFrom.SelectedDate.Value, rdpGFeeTo.SelectedDate.Value),
                                        fpg.IsVoid == false,
                                        fpgd.ParamedicID == par.ParamedicID);
                                if (!string.IsNullOrEmpty(txtPaymentGroupNo.Text))
                                {
                                    fpgd.Where(fpg.PaymentGroupNo != txtPaymentGroupNo.Text);
                                }
                                if (fpgdColl.Load(fpgd))
                                {
                                    r["FeePaidInGuaranteePeriod"] = fpgdColl.Sum(f => f.AmountFee4Service + f.AmountGuarantee);
                                }
                                if ((decimal)r["FeeGuarantee"] > 0)
                                {
                                    var gf = (decimal)r["FeeGuarantee"] - (decimal)r["FeePaidInGuaranteePeriod"];
                                    if (gf < 0) { gf = 0; }
                                    r["FeeGuarantee"] = gf;
                                }
                            }
                        }
                    }
                }
            }
            foreach (DataRow row in dtdeduc.Rows)
            {
                var r = ds.AsEnumerable().Where(x => x["ParamedicID"].ToString() == row["ParamedicID"].ToString()).FirstOrDefault();
                if (r == null)
                {
                    r = ds.NewRow();
                    r["ParamedicID"] = row["ParamedicID"];
                    r["ParamedicName"] = row["ParamedicName"];
                    r["Fee4Service"] = 0;
                    r["FeeAddDec"] = (decimal)row["FeeAmount"];
                    r["Fee4ServiceSelected"] = 0;
                    r["FeeAddDecSelected"] = 0;
                    r["FeeGuarantee"] = 0;
                    ds.Rows.Add(r);
                }
                else
                {
                    r["FeeAddDec"] = (decimal)r["FeeAddDec"] + (decimal)row["FeeAmount"];
                }

                if (SelectedKeys.ContainsKey(row["Id"].ToString()))
                {
                    r["FeeAddDecSelected"] = (decimal)r["FeeAddDecSelected"] + (decimal)row["FeeAmount"];
                }
            }

            ds.DefaultView.Sort = "ParamedicID asc";
            ds = ds.DefaultView.ToTable();
            return ds;
        }

        public DataTable GetDataSelected() {
            DataTable dtfee;
            DataTable dtdeduc;
            OutstandingPayment(out dtfee, out dtdeduc);
            return GetDataSelected(dtfee, dtdeduc);
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) {
                var chk = e.Item.FindControl("detailChkbox") as CheckBox;
                var r = e.Item.DataItem as DataRowView;
                if (chk != null)
                {
                    var outs = Convert.ToDecimal(r["Fee4Service"]) + Convert.ToDecimal(r["FeeAddDec"]);
                    var seld = Convert.ToDecimal(r["Fee4ServiceSelected"]) + Convert.ToDecimal(r["FeeAddDecSelected"]);
                    chk.Checked = outs == seld && outs != 0;
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dt = GetDataSelected();
            grdList.DataSource = dt;

            txtPaymentAmount.Value = System.Convert.ToDouble( 
                dt.AsEnumerable().Sum(x => x.Field<decimal>("Fee4ServiceSelected") + x.Field<decimal>("FeeAddDecSelected") + x.Field<decimal>("FeeGuarantee")));
        }
        private void OutstandingPayment(out DataTable dtfee, out DataTable dtdeduc)
        {
            (new ParamedicFeeTransChargesItemCompByDischargeDateCollection()).GetParamedicFeeProrataBayar(
                out dtfee, out dtdeduc, txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate, cboParamedicID.SelectedValue,
                txtDischargeDateFrom.SelectedDate, txtDischargeDateTo.SelectedDate, txtPlanningPaymentDate.SelectedDate,
                txtRegistrationNo.Text, txtMedicalNo.Text, txtPatientName.Text, cboGuarantorID.SelectedValue, cboGuarantorType.SelectedValue,
                txtPaymentGroupNo.Text, txtVerificationNo.Text);
        }

        private DataTable OutstandingPaymentGroupByParamedic() {
            return (new ParamedicFeeTransChargesItemCompByDischargeDateCollection()).GetParamedicFeeProrataBayarGroupByParamedic(

                txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate, cboParamedicID.SelectedValue, 
                txtDischargeDateFrom.SelectedDate, txtDischargeDateTo.SelectedDate, 
                txtRegistrationNo.Text, txtMedicalNo.Text, txtPatientName.Text);
        }

        protected void grdListVGN_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListVGN.DataSource = ParamedicFeePaidGroupN;
        }

        protected void grdListVGN_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detailPaymentPhysician":
                    {
                        //gridListLoadDetail(e);
                        var pno = e.DetailTableView.ParentItem.GetDataKeyValue("PaymentGroupNo").ToString();
                        var pfgq = new ParamedicFeePaymentGroupDetailQuery("pfg");
                        var pftq = new ParamedicFeeTaxCalculationQuery("pft");
                        var parq = new ParamedicQuery("parq");

                        pfgq.InnerJoin(parq).On(pfgq.ParamedicID == parq.ParamedicID)
                            .LeftJoin(pftq).On(pfgq.PaymentGroupNo == pftq.PaymentGroupNo && pfgq.ParamedicID == pftq.ParamedicID)
                            .Select(pfgq.PaymentGroupNo, pfgq.ParamedicID, parq.ParamedicName, 
                                pfgq.AmountFee4Service, pfgq.AmountAddDec, pfgq.AmountGuarantee, //.As("FeeAmount"),
                                pftq.TaxAmount.Sum().As("TaxAmount")
                            ).GroupBy(pfgq.PaymentGroupNo, pfgq.ParamedicID, parq.ParamedicName,
                                pfgq.AmountFee4Service, pfgq.AmountAddDec, pfgq.AmountGuarantee)
                            .Where(pfgq.PaymentGroupNo == pno);

                        e.DetailTableView.DataSource = pfgq.LoadDataTable();
                        break;
                    }
                case "detailPaymentDetail":
                    {
                        var pno = e.DetailTableView.ParentItem.GetDataKeyValue("PaymentGroupNo").ToString();
                        var parid = e.DetailTableView.ParentItem.GetDataKeyValue("ParamedicID").ToString();

                        var pfpq = new ParamedicFeeTransPaymentQuery("pfp");
                        var feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                        var regq = new RegistrationQuery("reg");
                        var patq = new PatientQuery("pat");
                        var guarq = new GuarantorQuery("guar");
                        var iq = new ItemQuery("i");

                        pfpq.InnerJoin(feeq).On(pfpq.TransactionNo == feeq.TransactionNo &&
                            pfpq.SequenceNo == feeq.SequenceNo &&
                            pfpq.TariffComponentID == feeq.TariffComponentID)
                            .InnerJoin(regq).On(feeq.RegistrationNo == regq.RegistrationNo)
                            .InnerJoin(patq).On(regq.PatientID == patq.PatientID)
                            .InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID)
                            .InnerJoin(iq).On(feeq.ItemID == iq.ItemID)
                            .Where(pfpq.PaymentGroupNo == pno, feeq.ParamedicID == parid)
                            .Select(
                                "<CAST(pfp.Id as varchar(10)) Id>", feeq.DischargeDate, feeq.RegistrationNo,
                                patq.MedicalNo, patq.PatientName, guarq.GuarantorName, iq.ItemName, 
                                pfpq.Amount
                            );

                        // fee by team
                        var feebtq = new ParamedicFeeTransChargesItemCompByTeamQuery("feebtq");
                        //feeq = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                        regq = new RegistrationQuery("reg");
                        patq = new PatientQuery("pat");
                        guarq = new GuarantorQuery("guar");
                        iq = new ItemQuery("i");

                        feebtq
                            //.InnerJoin(feeq).On(feebtq.TransactionNo == feeq.TransactionNo &&
                            //    feebtq.SequenceNo == feeq.SequenceNo &&
                            //    feebtq.TariffComponentID == feeq.TariffComponentID)
                            .InnerJoin(regq).On(feebtq.RegistrationNo == regq.RegistrationNo)
                            .InnerJoin(patq).On(regq.PatientID == patq.PatientID)
                            .InnerJoin(guarq).On(regq.GuarantorID == guarq.GuarantorID)
                            .InnerJoin(iq).On(feebtq.ItemID == iq.ItemID)
                            .Where(feebtq.PaymentGroupNo == pno, feebtq.ParamedicID == parid)
                            .Select(
                                "<feebtq.TransactionNo + '|' + feebtq.SequenceNo + '|' + feebtq.TariffComponentID + '|' + feebtq.ParamedicID as Id>",
                                feebtq.DischargeDate, feebtq.RegistrationNo,
                                patq.MedicalNo, patq.PatientName, guarq.GuarantorName, iq.ItemName,
                                feebtq.FeeAmount.As("Amount")
                            );

                        var dtb = pfpq.LoadDataTable();
                        dtb.Merge(feebtq.LoadDataTable());

                        e.DetailTableView.DataSource = dtb;
                        break;
                    }
            }
        }

        private DataTable ParamedicFeePaidGroupN
        {
            get
            {
                var payG = new ParamedicFeePaymentGroupQuery("a");
                var b = new BankQuery("b");
                var pm = new PaymentMethodQuery("pm");
                //var pt = new PaymentTypeQuery("pt");

                payG.LeftJoin(b).On(payG.BankID == b.BankID)
                    .LeftJoin(pm).On(payG.PaymentMethodID == pm.SRPaymentMethodID
                        && pm.SRPaymentTypeID == "PaymentType-007")
                    .Select(
                        "<'./ParamedicFeePaymentDetail.aspx?md=view&id=' + a.PaymentGroupNo + '&v=2' as VerUrl>",
                        payG.PaymentGroupNo,
                        payG.PaymentDate,
                        payG.PaymentAmount,
                        payG.FeeAmountBeforeTax,
                        payG.TaxOnPaymentAmount,
                        payG.IsApprove,
                        payG.ApproveDateTime,
                        payG.IsVoid,
                        payG.IsDraft,
                        b.BankName,
                        pm.PaymentMethodName
                     ).Where(payG.IsDetail == 2);

                if (chkUnapprovedOnlyN.Checked)
                {
                    payG.Where(payG.IsApprove == false, payG.IsVoid == false);
                }

                if (!string.IsNullOrEmpty(txtPaymentGroupNoN.Text.Trim()))
                {
                    string searchTextContain = string.Format("%{0}%", txtPaymentGroupNoN.Text.Trim());
                    payG.Where(payG.PaymentGroupNo.Like(searchTextContain));
                }
                if (rdpPaidDate.SelectedDate.HasValue) {
                    payG.Where(payG.PaymentDate == rdpPaidDate.SelectedDate.Value);
                }

                payG.OrderBy(payG.PaymentGroupNo.Descending);

                //payG.OrderBy("<LEN(a.PaymentGroupNo) DESC>");
                payG.es.Top = 500;
                //payG.es.Distinct = true;

                if (!string.IsNullOrEmpty(txtRegistrationNo.Text) ||
                    //!string.IsNullOrEmpty(cboParamedicID.SelectedValue) || 
                    !string.IsNullOrEmpty(txtMedicalNo.Text) ||
                    !string.IsNullOrEmpty(txtPatientName.Text) ||
                    !string.IsNullOrEmpty(cboGuarantorID.SelectedValue) ||
                    !string.IsNullOrEmpty(cboGuarantorType.SelectedValue))
                {

                    var ptp = new ParamedicFeeTransPaymentQuery("ptp");
                    var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");

                    payG.LeftJoin(ptp).On(payG.PaymentGroupNo == ptp.PaymentGroupNo)
                        .LeftJoin(fee).On(ptp.TransactionNo == fee.TransactionNo && ptp.SequenceNo == fee.SequenceNo && ptp.TariffComponentID == fee.TariffComponentID);

                    var feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
                    payG.LeftJoin(feeBt).On(payG.PaymentGroupNo == feeBt.PaymentGroupNo);


                    if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
                    {
                        payG.Where(payG.Or(
                            fee.RegistrationNo == txtRegistrationNo.Text,
                            feeBt.RegistrationNo == txtRegistrationNo.Text));
                    }
                    if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    {
                        payG.Where(payG.Or(
                            fee.ParamedicID == cboParamedicID.SelectedValue,
                            feeBt.ParamedicID == cboParamedicID.SelectedValue));
                    }
                    if (!string.IsNullOrEmpty(txtMedicalNo.Text) ||
                        !string.IsNullOrEmpty(txtPatientName.Text) ||
                        !string.IsNullOrEmpty(cboGuarantorID.SelectedValue) ||
                        !string.IsNullOrEmpty(cboGuarantorType.SelectedValue))
                    {
                        var reg = new RegistrationQuery("reg");
                        payG.LeftJoin(reg).On(fee.RegistrationNo == reg.RegistrationNo);

                        var regBt = new RegistrationQuery("regBt");
                        payG.LeftJoin(regBt).On(feeBt.RegistrationNo == regBt.RegistrationNo);

                        if (!string.IsNullOrEmpty(txtMedicalNo.Text) ||
                        !string.IsNullOrEmpty(txtPatientName.Text))
                        {
                            var pat = new PatientQuery("pat");
                            var patBt = new PatientQuery("patBt");

                            payG.LeftJoin(pat).On(reg.PatientID == pat.PatientID);
                            payG.LeftJoin(patBt).On(regBt.PatientID == patBt.PatientID);
                            if (!string.IsNullOrEmpty(txtMedicalNo.Text))
                            {
                                payG.Where(payG.Or(
                                    pat.MedicalNo == txtMedicalNo.Text,
                                    patBt.MedicalNo == txtMedicalNo.Text));
                            }
                            if (!string.IsNullOrEmpty(txtPatientName.Text))
                            {
                                //payG.Where("<RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%" + txtPatientName.Text + "%'>");
                                payG.Where(
                                    "< (RTRIM(RTRIM(pat.FirstName + ' ' + pat.MiddleName) + ' ' + pat.LastName) like '%" + txtPatientName.Text + "%' OR RTRIM(RTRIM(patBt.FirstName + ' ' + patBt.MiddleName) + ' ' + patBt.LastName) like '%" + txtPatientName.Text + "%') >");
                            }
                        }
                        if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                        {
                            //payG.Where(reg.GuarantorID == cboGuarantorID.SelectedValue);
                            payG.Where(payG.Or(
                                    reg.GuarantorID == cboGuarantorID.SelectedValue,
                                    regBt.GuarantorID == cboGuarantorID.SelectedValue));
                        }
                        if (!string.IsNullOrEmpty(cboGuarantorType.SelectedValue))
                        {
                            var guar = new GuarantorQuery("guar");
                            var guarBt = new GuarantorQuery("guarBt");

                            payG.LeftJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                                .LeftJoin(guarBt).On(regBt.GuarantorID == guarBt.GuarantorID)
                                //.Where(guar.SRGuarantorType == cboGuarantorType.SelectedValue)
                                .Where(payG.Or(
                                    guar.SRGuarantorType == cboGuarantorType.SelectedValue,
                                    guarBt.SRGuarantorType == cboGuarantorType.SelectedValue));
                        }
                    }

                    payG.es.Distinct = true;
                }
                else if(!string.IsNullOrEmpty(cboParamedicID.SelectedValue)) {
                    var payGDt = new ParamedicFeePaymentGroupDetailQuery("payGDt");
                    payG.InnerJoin(payGDt).On(payG.PaymentGroupNo == payGDt.PaymentGroupNo)
                        .Where(payGDt.ParamedicID == cboParamedicID.SelectedValue);

                    payG.es.Distinct = true;
                }

                DataTable dtb = payG.LoadDataTable();
                return dtb;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            DataTable dtfee;
            DataTable dtdeduc;
            OutstandingPayment(out dtfee, out dtdeduc);

            var chkH = ((CheckBox)sender);

            var gtv = ((GridTableView)(((CheckBox)sender).Parent.Parent.Parent.Parent.Parent));
            foreach (CheckBox chk in gtv.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chk.Checked = chkH.Checked;
                var gdi_i = (GridDataItem)chk.NamingContainer;
                SetSelectedByParamedicID(gdi_i.GetDataKeyValue("ParamedicID").ToString(), dtfee, dtdeduc, chk.Checked);
            }

            GetDataSelected(dtfee, dtdeduc);

            var chkState = chkH.Checked;

            GridRebindKeepChecked();

            var chkHN = grdList.MasterTableView.GetItems(GridItemType.Header).First().FindControl("headerChkbox") as CheckBox;
            if (chkHN != null) {
                chkHN.Checked = chkState;
            }
            //chkH.Checked = chkState;
        }

        protected void ChkChanged(object sender, EventArgs e)
        {
            DataTable dtfee;
            DataTable dtdeduc;
            OutstandingPayment(out dtfee, out dtdeduc);

            var chk = (CheckBox)sender;
            var gdi_i = (GridDataItem)chk.NamingContainer;
            SetSelectedByParamedicID(gdi_i.GetDataKeyValue("ParamedicID").ToString(), dtfee, dtdeduc, chk.Checked);

            GetDataSelected(dtfee, dtdeduc);

            GridRebindKeepChecked();
        }

        private void GridRebindKeepChecked() {
            // keep selected
            //var chkh = grdList.FindControl("headerChkbox") as CheckBox;
            //bool isChkhChecked = chkh.Checked;

            //List<string> SelectedParid = new List<string>();
            //foreach (GridDataItem gdi in grdList.MasterTableView.Items.Cast<GridDataItem>())
            //{
            //    var chkBox = ((CheckBox)gdi.FindControl("detailChkbox"));
            //    if (chkBox != null)
            //    {
            //        if (chkBox.Checked) SelectedParid.Add(gdi.GetDataKeyValue("ParamedicID").ToString());
            //    }
            //}

            grdList.Rebind();

            //chkh = grdList.FindControl("headerChkbox") as CheckBox;
            //chkh.Checked = isChkhChecked;

            //foreach (GridDataItem gdi in grdList.MasterTableView.Items.Cast<GridDataItem>())
            //{
            //    var chkBox = ((CheckBox)gdi.FindControl("detailChkbox"));
            //    if (chkBox != null)
            //    {
            //        if (SelectedParid.Contains(gdi.GetDataKeyValue("ParamedicID").ToString()))
            //            chkBox.Checked = true;
            //    }
            //}

            //foreach (GridDataItem gdi in grdList.MasterTableView.Items.Cast<GridDataItem>())
            //{
            //    var chkBox = ((CheckBox)gdi.FindControl("detailChkbox"));
            //    if (chkBox != null)
            //    {
            //        chkBox.Checked =
            //            (Convert.ToDecimal(gdi["Fee4Service"].Text) == Convert.ToDecimal(gdi["Fee4ServiceSelected"].Text)) &&
            //            (Convert.ToDecimal(gdi["FeeAddDec"].Text) == Convert.ToDecimal(gdi["FeeAddDecSelected"].Text));
            //    }
            //}
        }

        private bool ValidateTaxUndo(string PaymentGroupNo, ParamedicFeeTaxCalculationCollection ptaxColl) {
            // undo progressive tax
            var pgdColl = new ParamedicFeePaymentGroupDetailCollection();
            pgdColl.Query.Where(pgdColl.Query.PaymentGroupNo == PaymentGroupNo);
            if (!pgdColl.LoadAll())
            {
                // entah lah
            }

            ptaxColl.Query.Where(ptaxColl.Query.PaymentGroupNo == PaymentGroupNo);
            ptaxColl.LoadAll();

            foreach (var pgd in pgdColl)
            {
                long? LastID = ptaxColl.Where(x => x.ParamedicID == pgd.ParamedicID).OrderByDescending(x => x.id)
                    .Select(x => x.id).FirstOrDefault();

                if (LastID.HasValue)
                {
                    var ptaxNextColl = new ParamedicFeeTaxCalculationCollection();
                    ptaxNextColl.Query.Where(
                        ptaxNextColl.Query.ParamedicID == pgd.ParamedicID,
                        ptaxNextColl.Query.id > LastID
                        );
                    if (ptaxNextColl.LoadAll())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            pnlInfo.Visible = false;

            if(sourceControl is ImageButton){
                var btn = sourceControl as ImageButton;
                switch (btn.ID) {
                    case "iBtnApprovePaymentGroup":
                        {
                            switch (btn.CommandName)
                            {
                                case "Approval":
                                    {
                                        // looping grid
                                        var gdi = (GridDataItem)btn.NamingContainer;
                                        string PayGNo = gdi.GetDataKeyValue("PaymentGroupNo").ToString();
                                        if (!string.IsNullOrEmpty(PayGNo))
                                        {
                                            (new ParamedicFeePaymentHd()).Approv(PayGNo, AppSession.UserLogin.UserID);
                                            ((GridDataItem)btn.NamingContainer).OwnerTableView.Rebind();
                                        }
                                        else {
                                            // approve old data
                                            foreach (GridDataItem r in gdi.ChildItem.NestedTableViews[0].Items)
                                            {
                                                CheckBox chk = (r["IsApproved"].Controls[0] as CheckBox);
                                                if (!chk.Checked)
                                                {
                                                    string PaymentNo = r["PaymentNo"].Text;
                                                    (new ParamedicFeePaymentHd()).Approv(PaymentNo, AppSession.UserLogin.UserID);
                                                }
                                            }

                                            gdi.ChildItem.NestedTableViews[0].Rebind();
                                        }
                                        break;
                                    }
                                case "Unapproval": 
                                    {
                                        var gdi = (GridDataItem)btn.NamingContainer;
                                        string PayGNo = gdi.GetDataKeyValue("PaymentGroupNo").ToString();
                                        if (!string.IsNullOrEmpty(PayGNo))
                                        {

                                            var ptaxColl = new ParamedicFeeTaxCalculationCollection();
                                            if (!ValidateTaxUndo(PayGNo, ptaxColl))
                                            {
                                                pnlInfo.Visible = true;
                                                lblInfo.Text = "ERROR! Process failed due to progressive tax conflicts!";
                                                return;
                                            }

                                            (new ParamedicFeePaymentHd()).UnApprov(PayGNo, AppSession.UserLogin.UserID);
                                            ((GridDataItem)btn.NamingContainer).OwnerTableView.Rebind();
                                        }

                                        ((GridDataItem)btn.NamingContainer).OwnerTableView.Rebind();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "iBtnVoidPaymentGroup": {
                            var gdi = (GridDataItem)btn.NamingContainer;
                            string PayGNo = gdi.GetDataKeyValue("PaymentGroupNo").ToString();
                            if (!string.IsNullOrEmpty(PayGNo))
                            {
                                var pg = new ParamedicFeePaymentGroup();
                                if (!pg.LoadByPrimaryKey(PayGNo))
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Record not found!";
                                    return;
                                }
                                if (pg.IsVoid ?? false)
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Record has been voided!";
                                    return;
                                }

                                var ptaxColl = new ParamedicFeeTaxCalculationCollection();
                                if (!ValidateTaxUndo(pg.PaymentGroupNo, ptaxColl)) {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Process failed due to progressive tax conflicts!";
                                    return;
                                }

                                pg.IsVoid = true;
                                pg.VoidByUserID = AppSession.UserLogin.UserID;
                                pg.VoidDateTime = DateTime.Now;

                                //ptaxColl.MarkAllAsDeleted();

                                var feepColl = new ParamedicFeeTransPaymentCollection();
                                feepColl.Query.Where(feepColl.Query.PaymentGroupNo == pg.PaymentGroupNo);
                                feepColl.LoadAll();
                                foreach (var feep in feepColl)
                                {
                                    feep.str.PaymentGroupNo = string.Empty;
                                }

                                var feeBtColl = new ParamedicFeeTransChargesItemCompByTeamCollection();
                                feeBtColl.Query.Where(feeBtColl.Query.PaymentGroupNo == pg.PaymentGroupNo);
                                feeBtColl.LoadAll();
                                foreach (var feeBt in feeBtColl)
                                {
                                    feeBt.str.PaymentGroupNo = string.Empty;
                                }

                                var adDec = new ParamedicFeeAddDeducCollection();
                                adDec.Query.Where(adDec.Query.PaymentGroupNo == pg.PaymentGroupNo);
                                adDec.LoadAll();
                                foreach (var ad in adDec) {
                                    ad.str.PaymentGroupNo = string.Empty;
                                }

                                using (var trans = new esTransactionScope())
                                {
                                    pg.Save();
                                    //ptaxColl.Save();
                                    ptaxColl.DeleteByPaymentGroupNo(pg.PaymentGroupNo);
                                    feepColl.Save();
                                    feeBtColl.Save();
                                    adDec.Save();

                                    trans.Complete();
                                }

                                grdListVGN.Rebind();
                                grdList.Rebind();
                            }
                            else
                            {
                                pnlInfo.Visible = true;
                                lblInfo.Text = "ERROR! Data not found!";
                                return;
                            }
                            break;
                        }
                    case "iBtnUndoToDraft": {
                            var gdi = (GridDataItem)btn.NamingContainer;
                            string PayGNo = gdi.GetDataKeyValue("PaymentGroupNo").ToString();
                            if (!string.IsNullOrEmpty(PayGNo))
                            {
                                var pg = new ParamedicFeePaymentGroup();
                                if (!pg.LoadByPrimaryKey(PayGNo))
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Record not found!";
                                    return;
                                }
                                if (pg.IsVoid ?? false)
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Record has been voided!";
                                    return;
                                }
                                if (pg.IsDraft ?? false)
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Record is a draft!";
                                    return;
                                }
                                if (pg.IsApprove ?? false)
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Record has been approved!";
                                    return;
                                }
                                var ptaxColl = new ParamedicFeeTaxCalculationCollection();
                                if (!ValidateTaxUndo(pg.PaymentGroupNo, ptaxColl))
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = "ERROR! Process failed due to progressive tax conflicts!";
                                    return;
                                }

                                //ptaxColl.MarkAllAsDeleted();

                                pg.IsDraft = true;
                                pg.TaxOnPaymentAmount = 0;

                                using (var trans = new esTransactionScope())
                                {
                                    pg.Save();
                                    //ptaxColl.Save();
                                    ptaxColl.DeleteByPaymentGroupNo(pg.PaymentGroupNo);

                                    trans.Complete();
                                }

                                grdListVGN.Rebind();
                                grdList.Rebind();
                            }
                            else
                            {
                                pnlInfo.Visible = true;
                                lblInfo.Text = "ERROR! Data not found!";
                                return;
                            }
                            break;
                        }
                    case "iBtnCalculateTax": {
                            var gdi = (GridDataItem)btn.NamingContainer;
                            string PayGNo = gdi.GetDataKeyValue("PaymentGroupNo").ToString();
                            using (var trans = new esTransactionScope())
                            {
                                try
                                {
                                    DraftToPayment(PayGNo);
                                }
                                catch (Exception ex)
                                {
                                    pnlInfo.Visible = true;
                                    lblInfo.Text = ex.Message;
                                    return;
                                }
                                trans.Complete();
                            }
                                
                            grdListVGN.Rebind();
                            grdList.Rebind();
                            break;
                        }
                    case "iBtnEditDraft": {
                            var gdi = (GridDataItem)btn.NamingContainer;
                            string PayGNo = gdi.GetDataKeyValue("PaymentGroupNo").ToString();

                            LoadDraft(PayGNo);

                            tabStrip.Tabs[0].Selected = true;
                            //tabStrip.Tabs[1].Selected = false;
                            multiPage.PageViews[0].Selected = true;
                            ///multiPage.PageViews[1].Selected = false;

                            //tabst
                            
                            break;
                        }
                }
            }

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "saveDraft")
            {
                pnlInfo.Visible = false;

                var msg = ValidateSave();
                if (!string.IsNullOrEmpty(msg)) {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                    return;
                }

                string transNo = string.Empty;
                using (var trans = new esTransactionScope())
                {
                    try
                    {
                        transNo = SaveToDraft();
                    }
                    catch (Exception ex)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = ex.Message;
                        return;
                    }

                    trans.Complete();
                }

                pnlInfo.Visible = true;
                lblInfo.Text = "Draft Succeed with No. : " + transNo;

                //SelectedKeys = null;

                grdList.Rebind();
                grdListVGN.Rebind();
            }
            else if (eventArgument == "generate")
            {
                pnlInfo.Visible = false;

                var msg = ValidateSave();
                if (!string.IsNullOrEmpty(msg))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                    return;
                }

                string transNo = string.Empty;
                using (var trans = new esTransactionScope())
                {
                    //try diremark karena ada error tapi tidak ada tracenya
                    //{
                        transNo = SaveToDraft();
                        DraftToPayment(transNo);
                    //}
                    //catch (Exception ex)
                    //{
                    //    pnlInfo.Visible = true;
                    //    lblInfo.Text = ex.Message;
                    //    return;
                    //}

                        trans.Complete();
                }

                pnlInfo.Visible = true;
                lblInfo.Text = "Fee Payment Succeed with No. : " + transNo;

                SelectedKeys = null;

                grdList.Rebind();
                grdListVGN.Rebind();
            }
            else if (eventArgument.Contains("deleteFee"))
            {
                var param = eventArgument.Split('|');
                {
                    var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                    fee.Query.Where(fee.Query.TransactionNo == param[1] && fee.Query.SequenceNo == param[2] && fee.Query.TariffComponentID == param[3]);
                    if (fee.Load(fee.Query))
                    {
                        if (fee.VerificationNo == null)
                        {
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
            else if (eventArgument.Contains("print"))
            {
                var param = eventArgument.Split('|');

                var jobParameters = new PrintJobParameterCollection();

                var parPaymentGroupNo = jobParameters.AddNew();
                parPaymentGroupNo.Name = "p_PaymentGroupNo";
                parPaymentGroupNo.ValueString = param[1];

                if (param.Count() == 3)
                {
                    var parParamedicID = jobParameters.AddNew();
                    parParamedicID.Name = "p_ParamedicID";
                    parParamedicID.ValueString = param[2];

                    AppSession.PrintJobParameters = jobParameters;
                    AppSession.PrintJobReportID = AppConstant.Report.PhysicianFeePaymentPerGroup;
                }
                else {
                    AppSession.PrintJobParameters = jobParameters;
                    AppSession.PrintJobReportID = AppConstant.Report.PhysicianFeePaymentPerGroupHeader;
                }

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        private string ValidateSave() {
            bool isValid = true;

            if ((txtPaymentAmount.Value ?? 0) < 0) {
                return "ERROR! The amount of payment must not be minus.";
            }

            if (SelectedKeys.Count == 0) isValid = false;

            if (!isValid)
            {
                return "ERROR! Please select at least one row";
            }

            if (txtPaymentDate.SelectedDate == null)
            {
                return "ERROR! Please select payment date";
            }

            if (cboPaymentMethodID.SelectedValue == string.Empty)
            {
                return "ERROR! Please select payment method";
            }

            // validasi bank hanya jika payment method tidak ada setting coanya
            var pmColl = new PaymentMethodCollection();
            var query = new PaymentMethodQuery("a");
            var ptQuery = new PaymentTypeQuery("b");
            var coa = new ChartOfAccountsQuery("c");
            query.InnerJoin(ptQuery).On(query.SRPaymentTypeID == ptQuery.SRPaymentTypeID)
                .InnerJoin(coa).On(query.ChartOfAccountID == coa.ChartOfAccountId)
                .Select(query)
                .Where(
                    query.SRPaymentMethodID == cboPaymentMethodID.SelectedValue,
                    ptQuery.IsFeePayment == true
                );
            if (!pmColl.Load(query))
            {
                if (cboBankID.SelectedValue == string.Empty)
                {
                    return "ERROR! Please select bank";
                }
            }

            return string.Empty;
        }

        private string SaveToDraft() {
            var transNos = string.Empty;
            var dafugs = grdList.MasterTableView.Items.Cast<GridDataItem>()
                    .Where(dataItem =>
                        (Math.Abs(Convert.ToDecimal(dataItem["Fee4ServiceSelected"].Text)) +
                        Math.Abs(Convert.ToDecimal(dataItem["FeeAddDecSelected"].Text)) +
                        Math.Abs(Convert.ToDecimal(dataItem["FeeGuarantee"].Text))) != 0
                    ).Select(dataItem => new
                    {
                        ParamedicID = dataItem.GetDataKeyValue("ParamedicID").ToString(),
                        Fee4Service = Convert.ToDecimal(dataItem["Fee4ServiceSelected"].Text),
                        FeeAddDec = Convert.ToDecimal(dataItem["FeeAddDecSelected"].Text),
                        FeeGuarantee = Convert.ToDecimal(dataItem["FeeGuarantee"].Text)
                    }).ToArray(); // linq bankai

            int c = dafugs.Count();
            if (c == 0)
            {
                throw new Exception("No data selected");
            }

            var isNew = false;

            // payment group
            var nDate = (new DateTime()).NowAtSqlServer();
            var fpg = new ParamedicFeePaymentGroup();
            if (string.IsNullOrEmpty(txtPaymentGroupNo.Text))
            {
                AppAutoNumberLast _autoNumber;
                _autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.FeePaymentGroupNo);
                txtPaymentGroupNo.Text = _autoNumber.LastCompleteNumber;

                fpg.AddNew();
                fpg.PaymentGroupNo = txtPaymentGroupNo.Text;
                _autoNumber.Save();
                isNew = true;
            }
            else
            {
                if (!fpg.LoadByPrimaryKey(txtPaymentGroupNo.Text))
                {
                    throw new Exception("Payment not found");
                }
                if (!(fpg.IsDraft ?? false))
                {
                    throw new Exception("Non draft payment cannot be saved");
                }
            }

            fpg.PaymentDate = txtPaymentDate.SelectedDate.Value;
            fpg.PaymentMethodID = cboPaymentMethodID.SelectedValue;
            fpg.BankID = cboBankID.SelectedValue;
            fpg.FeeAmountBeforeTax = System.Convert.ToDecimal(txtPaymentAmount.Value);
            fpg.PaymentAmount = System.Convert.ToDecimal(txtPaymentAmount.Value);
            fpg.IsApprove = false;
            fpg.IsVoid = false;
            fpg.IsDraft = true;
            if (fpg.es.IsAdded) {
                fpg.CreatedByUserID = AppSession.UserLogin.UserID;
                fpg.CreatedDateTime = nDate;
            }
            fpg.LastUpdateByUserID = fpg.CreatedByUserID;
            fpg.LastUpdateDateTime = fpg.CreatedDateTime;
            fpg.IsDetail = 2;

            fpg.Save();

            var fpgdColl = new ParamedicFeePaymentGroupDetailCollection();
            if (!isNew)
            {
                fpgdColl.Query.Where(fpgdColl.Query.PaymentGroupNo == fpg.PaymentGroupNo);
                fpgdColl.LoadAll();
                fpgdColl.MarkAllAsDeleted();

                // hapus paymentgroup no
                var ftpColl = new ParamedicFeeTransPaymentCollection();
                //ftpColl.Query.Where(ftpColl.Query.PaymentGroupNo == fpg.PaymentGroupNo);
                //ftpColl.LoadAll();
                //foreach (var ftp in ftpColl) {
                //    ftp.str.PaymentGroupNo = string.Empty;
                //}
                //ftpColl.Save();
                ftpColl.ResetPaymentGroupNo(fpg.PaymentGroupNo);

                //
                var feeBtColl = new ParamedicFeeTransChargesItemCompByTeamCollection();
                feeBtColl.Query.Where(feeBtColl.Query.PaymentGroupNo == fpg.PaymentGroupNo);
                feeBtColl.LoadAll();
                foreach (var feeBt in feeBtColl)
                {
                    feeBt.str.PaymentGroupNo = string.Empty;
                }
                feeBtColl.Save();

                var addDecColl = new ParamedicFeeAddDeducCollection();
                addDecColl.Query.Where(addDecColl.Query.PaymentGroupNo == fpg.PaymentGroupNo);
                addDecColl.LoadAll();
                foreach (var addDec in addDecColl)
                {
                    addDec.str.PaymentGroupNo = string.Empty;
                }
                addDecColl.Save();
            }

            foreach (var item in dafugs)
            {
                if (item.Fee4Service == 0 && item.FeeAddDec == 0) continue;

                var fpgd = fpgdColl.AddNew();
                fpgd.AddNew();
                fpgd.PaymentGroupNo = fpg.PaymentGroupNo;
                fpgd.ParamedicID = item.ParamedicID;
                fpgd.CreateByUserID = AppSession.UserLogin.UserID;
                fpgd.CreateDateTime = nDate;
                fpgd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                fpgd.LastUpdateDateTime = nDate;

                // set detail here
                var feePayColl = new ParamedicFeeTransPaymentCollection();
                var feeBtColl = new ParamedicFeeTransChargesItemCompByTeamCollection();
                var addDecColl = new ParamedicFeeAddDeducCollection();

                feePayColl.GetReadyToBePaid(
                    txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate, item.ParamedicID,
                    txtDischargeDateFrom.SelectedDate, txtDischargeDateTo.SelectedDate, txtPlanningPaymentDate.SelectedDate,
                    txtRegistrationNo.Text, txtMedicalNo.Text, txtPatientName.Text, cboGuarantorID.SelectedValue, cboGuarantorType.SelectedValue,
                    txtPaymentGroupNo.Text);

                
                var ssk = SelectedKeys.Where(s => s.Value == item.ParamedicID);
                var feeToPayColl = from pfc in feePayColl
                                    join sk in ssk on pfc.Id.ToString() equals sk.Key
                                    select pfc;

                decimal Total = 0;

                foreach (var feePay in feeToPayColl)
                {
                    Total += feePay.Amount.Value;
                    feePay.PaymentGroupNo = fpg.PaymentGroupNo;
                }

                // fee by team
                feeBtColl.GetReadyToBePaid(
                    txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate, item.ParamedicID,
                    txtDischargeDateFrom.SelectedDate, txtDischargeDateTo.SelectedDate, txtPlanningPaymentDate.SelectedDate,
                    txtRegistrationNo.Text, txtMedicalNo.Text, txtPatientName.Text, cboGuarantorID.SelectedValue, cboGuarantorType.SelectedValue,
                    txtPaymentGroupNo.Text);


                //var ssk = SelectedKeys.Where(s => s.Value == item.ParamedicID);
                var feeBtToPayColl = from fee in feeBtColl
                                   join sk in ssk on string.Format("{0}|{1}|{2}|{3}", fee.TransactionNo, fee.SequenceNo, fee.TariffComponentID, fee.ParamedicID) equals sk.Key
                                   select fee;

                //decimal Total = 0;

                foreach (var feeBtPay in feeBtToPayColl)
                {
                    Total += feeBtPay.FeeAmount.Value;
                    feeBtPay.PaymentGroupNo = fpg.PaymentGroupNo;
                }

                if (Total != item.Fee4Service)
                {
                    throw new Exception("Invalid amount");
                }

                feePayColl.Save();
                feeBtColl.Save();
                fpgd.AmountFee4Service = Total;

                addDecColl.GetReadyToBePaid(
                    txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate, item.ParamedicID,
                    txtDischargeDateFrom.SelectedDate, txtDischargeDateTo.SelectedDate,
                    txtRegistrationNo.Text, txtMedicalNo.Text, txtPatientName.Text,
                    txtPaymentGroupNo.Text);

                var addDecToPayColl = addDecColl
                    .Where(x => SelectedKeys.Where(k => k.Value == item.ParamedicID).Select(k => k.Key).Contains(x.TransactionNo));
                Total = addDecToPayColl.Sum(x => (x.SRParamedicFeeAdjustType == "01" ? x.Amount.Value : (-x.Amount.Value)));
                if (Total != item.FeeAddDec)
                {
                    throw new Exception("Invalid amount");
                }

                foreach (var addDec in addDecToPayColl)
                {
                    addDec.PaymentGroupNo = fpg.PaymentGroupNo;
                }
                addDecColl.Save();
                fpgd.AmountAddDec = Total;

                fpgd.AmountGuarantee = item.FeeGuarantee;
            }
            if (rdpGFeeFrom.SelectedDate.HasValue) {
                fpg.GuaranteeFeeDateFrom = rdpGFeeFrom.SelectedDate;
            }
            if (rdpGFeeTo.SelectedDate.HasValue)
            {
                fpg.GuaranteeFeeDateTo = rdpGFeeTo.SelectedDate;
            }

            fpg.TaxOnPaymentAmount = 0;
            fpg.PaymentAmount = fpg.FeeAmountBeforeTax - fpg.TaxOnPaymentAmount;
            fpg.Save();
            fpgdColl.Save();

            return fpg.PaymentGroupNo;
        }
        private void DraftToPayment(string PaymentGroupNo) {

            var fpg = new ParamedicFeePaymentGroup();
            if (fpg.LoadByPrimaryKey(PaymentGroupNo))
            {
                if (!(fpg.IsDraft ?? false)) {
                    throw new Exception("Cannot set from draft to payment");
                }
                if (fpg.IsApprove ?? false) {
                    throw new Exception("Cannot set from draft to payment, data has been approved");
                }
                fpg.IsDraft = false;
                fpg.LastUpdateByUserID = fpg.CreatedByUserID;
                fpg.LastUpdateDateTime = fpg.CreatedDateTime;

                var fpgdColl = new ParamedicFeePaymentGroupDetailCollection();
                fpgdColl.Query.Where(fpgdColl.Query.PaymentGroupNo == PaymentGroupNo);
                fpgdColl.LoadAll();

                var feeToPayColl = new ParamedicFeeTransPaymentCollection();
                var ftp = new ParamedicFeeTransPaymentQuery("ftp");
                var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                //var fset = new ParamedicFeeByFee4ServiceSettingQuery("fset");
                var reg = new RegistrationQuery("reg");
                var guar = new GuarantorQuery("guar");

                ftp.InnerJoin(fee).On(ftp.TransactionNo == fee.TransactionNo && ftp.SequenceNo == fee.SequenceNo && ftp.TariffComponentID == fee.TariffComponentID)
                    //.LeftJoin(fset).On(fee.ParamedicFeeByServiceSettingID == fset.Id)
                    .InnerJoin(reg).On(fee.RegistrationNoMergeTo == reg.RegistrationNo)
                    .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .Where(ftp.PaymentGroupNo == PaymentGroupNo)
                    .Select(ftp, 
                        fee.ParamedicID.As("refTo_ParamedicID"), 
                        fee.Price.As("refTo_Price"),
                        fee.Qty.As("refTo_Qty"),
                        fee.Discount.As("refTo_Discount"),
                        fee.FeeAmountBruto.As("refTo_FeeAmountBruto"),
                        fee.SRPhysicianFeeCategory.As("refTo_SRPhysicianFeeCategory"), 
                        fee.VerificationNo.As("refTo_VerificationNo")
                        //fset.IsUsingFormula.Coalesce("CAST(0 AS BIT)").As("refTo_IsUsingFormula")
                        );

                feeToPayColl.Load(ftp);

                // fee by team
                var feeBtColl = new ParamedicFeeTransChargesItemCompByTeamCollection();
                var feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
                //fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                //var fset = new ParamedicFeeByFee4ServiceSettingQuery("fset");
                //reg = new RegistrationQuery("reg");
                //guar = new GuarantorQuery("guar");

                feeBt//.InnerJoin(fee).On(ftp.TransactionNo == fee.TransactionNo && ftp.SequenceNo == fee.SequenceNo && ftp.TariffComponentID == fee.TariffComponentID)
                    //.LeftJoin(fset).On(fee.ParamedicFeeByServiceSettingID == fset.Id)
                    //.InnerJoin(reg).On(feeBt.RegistrationNoMergeTo == reg.RegistrationNo)
                    //.InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID)
                    .Where(feeBt.PaymentGroupNo == PaymentGroupNo)
                    .Select(feeBt);

                feeBtColl.Load(feeBt);

                var addDecToPayColl = new ParamedicFeeAddDeducCollection();
                addDecToPayColl.Query.Where(addDecToPayColl.Query.PaymentGroupNo == PaymentGroupNo);
                addDecToPayColl.LoadAll();

                var tax = new ParamedicFeeTaxCalculationCollection();
                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPhysicianFeeUsingTaxCalculation) == "2")
                    ParamedicFeeVerification.PrepareFeeTaxCalculationByDischargeDateOnPayment(
                        fpg, fpgdColl, feeToPayColl, feeBtColl, addDecToPayColl,
                        tax, AppSession.UserLogin.UserID, true, AppSession.Parameter.IsFeeEnableDualBruto,
                        AppSession.Parameter.IsFeeTaxProgressiveMonthly);


                tax.Save();

                fpg.TaxOnPaymentAmount = tax.Sum(t => t.TaxAmount.Value);
                fpg.PaymentAmount = fpg.FeeAmountBeforeTax - fpg.TaxOnPaymentAmount;
                fpg.Save();
            }
            else {
                throw new Exception("Paymment not found");
            }
        }

        private void LoadDraft(string PaymentGroupNo) {
            var fpg = new ParamedicFeePaymentGroup();
            if (fpg.LoadByPrimaryKey(PaymentGroupNo))
            {
                if (!fpg.IsDraft ?? false) {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Can not load non draft payment";
                    return;
                }
                txtPaymentGroupNo.Text = fpg.PaymentGroupNo;
                txtPaymentDate.SelectedDate = fpg.PaymentDate;
                cboPaymentMethodID_ItemsRequested(cboPaymentMethodID, new RadComboBoxItemsRequestedEventArgs() { Text = fpg.PaymentMethodID });
                if (cboPaymentMethodID.Items.FindItemByValue(fpg.PaymentMethodID) != null)
                {
                    cboPaymentMethodID.SelectedValue = fpg.PaymentMethodID;
                }
                cboBankID_ItemsRequested(cboBankID, new RadComboBoxItemsRequestedEventArgs() { Text = fpg.BankID });
                if (cboBankID.Items.FindItemByValue(fpg.BankID) != null)
                {
                    cboBankID.SelectedValue = fpg.BankID;
                }

                if (fpg.GuaranteeFeeDateFrom.HasValue) rdpGFeeFrom.SelectedDate = fpg.GuaranteeFeeDateFrom.Value;
                if (fpg.GuaranteeFeeDateTo.HasValue) rdpGFeeTo.SelectedDate = fpg.GuaranteeFeeDateTo.Value;

                var ftp = new ParamedicFeeTransPaymentQuery("ftp");
                var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
                ftp.InnerJoin(fee).On(ftp.TransactionNo == fee.TransactionNo && ftp.SequenceNo == fee.SequenceNo && ftp.TariffComponentID == fee.TariffComponentID)
                    .Where(ftp.PaymentGroupNo == fpg.PaymentGroupNo)
                    .Select(ftp.Id.ToString(), fee.ParamedicID);
                var dtFee = ftp.LoadDataTable();

                var feeBt = new ParamedicFeeTransChargesItemCompByTeamQuery("feeBt");
                feeBt.Where(feeBt.PaymentGroupNo == fpg.PaymentGroupNo)
                    .Select(
                        "<feeBt.TransactionNo + '|' + feeBt.SequenceNo + '|' + feeBt.TariffComponentID + '|' + feeBt.ParamedicID as Id>",
                        feeBt.ParamedicID);
                var dtFeeBt = feeBt.LoadDataTable();

                var addec = new ParamedicFeeAddDeducQuery("addec");
                addec.Where(addec.PaymentGroupNo == fpg.PaymentGroupNo)
                    .Select(addec.TransactionNo.As("Id"), addec.ParamedicID);
                var dtAddDec = addec.LoadDataTable();

                Dictionary<string, string> NewSelected = new Dictionary<string, string>();
                foreach (System.Data.DataRow row in dtFee.Rows)
                {
                    NewSelected.Add(row["Id"].ToString(), row["ParamedicID"].ToString());
                }
                foreach (System.Data.DataRow row in dtFeeBt.Rows)
                {
                    NewSelected.Add(row["Id"].ToString(), row["ParamedicID"].ToString());
                }
                foreach (System.Data.DataRow row in dtAddDec.Rows)
                {
                    NewSelected.Add(row["Id"].ToString(), row["ParamedicID"].ToString());
                }
                SelectedKeys = NewSelected;
                grdList.Rebind();
            }
            else {
                pnlInfo.Visible = true;
                lblInfo.Text = "Payment not found";
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }


        protected void cboBankID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BankName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BankID"].ToString();
        }

        protected void cboBankID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BankQuery();
            query.es.Top = 20;
            query.Select(
                query.BankID,
                query.BankName
                );
            query.Where(
                query.IsFeePayment == true,
                query.IsActive == true,
                query.Or(query.BankID == e.Text, query.BankName.Like(searchTextContain))
                );
            query.OrderBy(query.BankName.Ascending);
            cboBankID.DataSource = query.LoadDataTable();
            cboBankID.DataBind();
        }

        protected void cboPaymentMethodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PaymentMethodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SRPaymentMethodID"].ToString();
        }

        protected void cboPaymentMethodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PaymentMethodQuery("a");
            var ptQuery = new PaymentTypeQuery("b");
            query.InnerJoin(ptQuery).On(query.SRPaymentTypeID == ptQuery.SRPaymentTypeID);
            query.es.Top = 20;
            query.Select(
                query.SRPaymentMethodID,
                query.PaymentMethodName
                );
            query.Where(
                query.Or(query.SRPaymentMethodID == e.Text, query.PaymentMethodName.Like(searchTextContain)),
                ptQuery.IsFeePayment == true
                );
            cboPaymentMethodID.DataSource = query.LoadDataTable();
            cboPaymentMethodID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.Select(
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

        protected void btnCalcGuaranteeFee_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();

            //var dataItems = grdList.MasterTableView.Items.Cast<GridDataItem>();
            //            //.Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked);
            //var parColl = new ParamedicCollection();
            //parColl.Query.Where(parColl.Query.ParamedicFee == true);
            //parColl.LoadAll();
            //foreach (var item in dataItems) {
            //    var ParamedicID = item.GetDataKeyValue("ParamedicID").ToString();
            //    var Fee4Service = Convert.ToDecimal(item["Fee4Service"].Text);
            //    var FeeAddDec = Convert.ToDecimal(item["FeeAddDec"].Text);

            //    var par = parColl.Where(p => p.ParamedicID == ParamedicID).FirstOrDefault();
            //    if (par != null) {
            //        if ((par.GuaranteeFee ?? 0) > 0) {
            //            var gFee = (par.GuaranteeFee ?? 0) - (Fee4Service + FeeAddDec);
            //            if (gFee > 0) {
            //                item["FeeGuarantee"].Text = gFee.ToString("N");
            //            }
            //        }
            //    }
            //}
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var guar = new GuarantorQuery("a");
            guar.es.Top = 20;
            guar.Where(
                guar.GuarantorName.Like(searchTextContain),
                guar.IsActive == true
                );

            cboGuarantorID.DataSource = guar.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void btnReset_Click(object sender, ImageClickEventArgs e)
        {
            txtPaymentGroupNo.Text = string.Empty;
            txtPaymentDate.SelectedDate = null;
            //cboPaymentMethodID.SelectedValue = "";
            //cboBankID.SelectedValue = "";
            SelectedKeys = null;

            grdList.Rebind();
        }
    }
}
