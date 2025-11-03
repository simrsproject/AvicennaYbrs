using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Telerik.Windows.Documents.Spreadsheet.Model.DataValidation;
using Telerik.Reporting;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeRemunDetail : BasePageDetail
    {
        private string[] smfToHide
        {
            get
            {
                return new string[] { "", "umum" };
            }
        }
        private DataTable feeRemunDetailTransaction
        {
            get
            {
                return (DataTable)Session["feeRemunDetailTransaction"];
            }
            set
            {
                Session["feeRemunDetailTransaction"] = value;
            }
        }

        private DataTable feeRemunDetail
        {
            get
            {
                return (DataTable)Session["feeRemunDetail"];
            }
            set
            {
                Session["feeRemunDetail"] = value;
            }
        }

        private AppAutoNumberLast GetAutoNumber()
        {
            if (!txtDateStart.SelectedDate.HasValue) return null;
            if (!txtDateEnd.SelectedDate.HasValue) return null;

            var transCode = new AppAutoNumberTransactionCode();
            if (!transCode.LoadByPrimaryKey(BusinessObject.Reference.TransactionCode.RemunerationByIdi))
                return null;

            return Common.Helper.GetNewAutoNumber(txtDateStart.SelectedDate.Value, transCode.SRAutoNumber);
        }
        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ParamedicFeeRemunSearch.aspx";
            UrlPageList = string.Format("ParamedicFeeRemunList.aspx");

            IsUsingBeforeVoidValidation = true;

            ProgramID = AppConstant.Program.ParamedicFeeRemunerationByIDI;

            //StandardReference Initialize
            if (!IsPostBack)
            {

            }

            // calculation will take a long time, 
            var ajxmgr = (RadScriptManager)Common.Helper.FindControlRecursive(this, "fw_RadScriptManager");
            ajxmgr.AsyncPostBackTimeout = 600;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            //ajax.AddAjaxSetting(cboFromServiceUnitID, txtTransactionNo);
            //ajax.AddAjaxSetting(cboFromServiceUnitID, cboSRItemType);

            //ajax.AddAjaxSetting(grdItemTransactionItem, grdItemTransactionItem);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboFromServiceUnitID);
            //ajax.AddAjaxSetting(grdItemTransactionItem, cboSRItemType);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {

        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var rem = new BusinessObject.ParamedicFeeRemunByIdi();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (rem.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (rem.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }

            rem.IsApproved = true;
            rem.ApprovedDateTime = DateTime.Now;
            rem.ApprovedByUserID = AppSession.UserLogin.UserID;
            rem.LastUpdateDateTime = DateTime.Now;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;
            using (var trans = new esTransactionScope())
            {
                rem.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var rem = new BusinessObject.ParamedicFeeRemunByIdi();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(rem.IsApproved ?? false))
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }
            if (rem.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }


            rem.IsApproved = false;
            rem.LastUpdateDateTime = DateTime.Now;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                rem.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            // cek budgetnya sudah approve atau belum
            var rem = new BusinessObject.ParamedicFeeRemunByIdi();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (rem.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            rem.IsVoid = true;
            rem.VoidDateTime = DateTime.Now;
            rem.VoidByUserID = AppSession.UserLogin.UserID;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;
            rem.LastUpdateDateTime = DateTime.Now;

            var fees = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            fees.Query.Where(fees.Query.RemunByIdiID == rem.RemunID);
            fees.LoadAll();
            foreach (var fee in fees)
            {
                // reset
                fee.RemunByIdiID = null;
            }

            using (var trans = new esTransactionScope())
            {
                fees.Save();

                rem.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            // gak bisa lagi unvoid karena link ke tabel jasmed sudah dikosongkan -->  fee.RemunByIdiID = null;
            args.MessageText = "Unvoid is disabled";
            args.IsCancel = true;
            return;

            // cek budgetnya sudah approve atau belum
            var rem = new BusinessObject.ParamedicFeeRemunByIdi();
            if (!rem.LoadByRemunNo(txtRemunNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (!(rem.IsVoid ?? false))
            {
                args.MessageText = "This data has not been voided";
                args.IsCancel = true;
                return;
            }

            var dtime = DateTime.Now;

            rem.IsVoid = false;
            rem.LastUpdateDateTime = dtime;
            rem.LastUpdateByUserID = AppSession.UserLogin.UserID;

            using (var trans = new esTransactionScope())
            {
                rem.Save();

                trans.Complete();
            }
        }

        private bool IsApprovedOrVoid(ItemTransaction entity, ValidateArgs args)
        {
            if (entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ParamedicFeeRemunByIdi());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ParamedicFeeRemunByIdi entity = new ParamedicFeeRemunByIdi();
            if (entity.LoadByRemunNo(txtRemunNo.Text))
            {
                if (entity.IsApproved ?? false)
                {
                    args.MessageText = AppConstant.Message.RecordHasApproved;
                    args.IsCancel = true;
                }
                else
                {
                    var fees = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    fees.Query.Where(fees.Query.RemunByIdiID == entity.RemunID);
                    fees.LoadAll();
                    foreach (var fee in fees)
                    {
                        fee.RemunByIdiID = null;
                    }

                    var dti = new ParamedicFeeRemunByIdiDetailCollection();
                    dti.Query.Where(dti.Query.RemunID == entity.RemunID);
                    dti.LoadAll();

                    entity.MarkAsDeleted();
                    dti.MarkAllAsDeleted();

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        dti.Save();
                        entity.Save();
                        fees.Save();

                        trans.Complete();
                    }
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            // validate save
            if (!txtDateStart.SelectedDate.HasValue)
            {
                args.MessageText = "Invalid date start";
                args.IsCancel = true;
                return;
            }
            if (!txtDateEnd.SelectedDate.HasValue)
            {
                args.MessageText = "Invalid date end";
                args.IsCancel = true;
                return;
            }
            if (feeRemunDetail.Rows.Count == 0)
            {
                args.MessageText = "Detail can not be empty";
                args.IsCancel = true;
                return;
            }
            if (feeRemunDetailTransaction.Rows.Count == 0)
            {
                args.MessageText = "Detail transsaction can not be empty";
                args.IsCancel = true;
                return;
            }

            string ret = SaveEntity(true);
            if (ret != string.Empty)
            {
                args.MessageText = ret;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            // validate save
            if (!txtDateStart.SelectedDate.HasValue)
            {
                args.MessageText = "Invalid date start";
                args.IsCancel = true;
                return;
            }
            if (!txtDateEnd.SelectedDate.HasValue)
            {
                args.MessageText = "Invalid date end";
                args.IsCancel = true;
                return;
            }
            if (feeRemunDetail.Rows.Count == 0)
            {
                args.MessageText = "Detail can not be empty";
                args.IsCancel = true;
                return;
            }

            string ret = SaveEntity(false);
            if (ret != string.Empty)
            {
                args.MessageText = ret;
                args.IsCancel = true;
                return;
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("RemunNo='{0}'", txtRemunNo.Text.Trim());
            auditLogFilter.TableName = "ParamedicFeeRemunByIdi";
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_RemunNo", txtRemunNo.Text);
        }

        #endregion

        #region ToolBar Menu Support

        public override bool OnGetStatusMenuEdit()
        {
            return txtRemunNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //grdRemun.Rebind();
            btnCalculate.Enabled = (newVal != AppEnum.DataMode.Read);
            btnExportExcel.Enabled = (newVal == AppEnum.DataMode.Read);
            btnExportExcelDetail.Enabled = (newVal == AppEnum.DataMode.Read);
            //RefreshCommandItemGrid(oldVal, newVal);

            //if (oldVal == DataMode.New && newVal == DataMode.Read)
            //{ 
            //    // redirect to list
            //    string url = string.Format("BudgetPlanApprovalList.aspx");
            //    Page.Response.Redirect(url, true);
            //}
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            BusinessObject.ParamedicFeeRemunByIdi entity = new BusinessObject.ParamedicFeeRemunByIdi();
            if (parameters.Length > 0)
            {
                String RemunNo = (String)parameters[0];

                entity.LoadByRemunNo(RemunNo);
            }
            else
            {
                if (!txtRemunNo.Text.Equals(string.Empty))
                {
                    entity.LoadByRemunNo(txtRemunNo.Text);
                }
                else
                {
                    entity.LoadByRemunNo(txtRemunNo.Text);
                }
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            BusinessObject.ParamedicFeeRemunByIdi rem = (BusinessObject.ParamedicFeeRemunByIdi)entity;

            txtRemunNo.Text = rem.RemunNo;

            txtDateStart.SelectedDate = rem.DateStart;
            txtDateEnd.SelectedDate = rem.DateEnd;

            chkIsVoid.Checked = rem.IsVoid ?? false;
            chkIsApproved.Checked = rem.IsApproved ?? false;
            txtNotes.Text = rem.Notes;
            txtFundAllocProcedure.Value = System.Convert.ToDouble(rem.FundAllocProcedure ?? 0);
            txtKursPosition.Value = System.Convert.ToDouble(rem.KursPosition ?? 0);
            txtKursInsentif.Value = System.Convert.ToDouble(rem.KursInsentif ?? 0);
            txtAdjustmentFactor.Value = System.Convert.ToDouble(rem.AdjustmentFactor ?? 0);

            //Display Data Detail
            var dtFee = GetRemunDetails(rem.RemunID ?? 0);
            //grdRemun.DataSource = null;
            //grdRemun.DataSource = dt;
            //grdRemun.DataBind();

            RefreshGridSummary(dtFee, rem.RemunID);
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(BusinessObject.ParamedicFeeRemunByIdi entity)
        {
            entity.RemunNo = txtRemunNo.Text;

            entity.DateStart = txtDateStart.SelectedDate;
            entity.DateEnd = txtDateEnd.SelectedDate;

            entity.FundAllocProcedure = System.Convert.ToDecimal(txtFundAllocProcedure.Value);
            entity.KursPosition = System.Convert.ToDecimal(txtKursPosition.Value);
            entity.KursInsentif = System.Convert.ToDecimal(txtKursInsentif.Value);
            entity.AdjustmentFactor = System.Convert.ToDecimal(txtAdjustmentFactor.Value);
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;// DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
            }
            //Last Update Status
            if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;// DateTime.Now;
            }
        }

        private ParamedicFeeTransChargesItemCompByDischargeDateQuery GetQueryFee(int flag)
        {
            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var tc = new TransChargesQuery("tc");
            //var suit = new ServiceUnitItemServiceQuery("suit");
            var i = new ItemQuery("i");
            var par = new ParamedicQuery("par");
            var smf = new SmfQuery("smf");
            var idismf = new ItemIdiItemSmfQuery("idismf");
            var idic = new ItemIdiQuery("idic");
            var reg = new RegistrationQuery("reg");
            var guar = new GuarantorQuery("guar");
            var su = new ServiceUnitQuery("su");
            var res = new TestResultQuery("res");

            var tpio = new TransPaymentItemOrderQuery("tpio");
            var tpiop = new TransPaymentQuery("tpiop");

            var cc = new CostCalculationQuery("cc");

            var tpib = new TransPaymentItemIntermBillQuery("tpib");
            var tpibp = new TransPaymentQuery("tpibp");

            var tpibg = new TransPaymentItemIntermBillGuarantorQuery("tpibg");
            var tpibgp = new TransPaymentQuery("tpibgp");


            fee.InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                .InnerJoin(i).On(fee.ItemID == i.ItemID)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .InnerJoin(smf).On(par.SRParamedicRL1 == smf.SmfID)
                .InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID && guar.IsParamedicFeeRemun == true) //guar.SRGuarantorType == "09"/*BPJS*/)
                                                                                                             //.LeftJoin(suit).On(fee.ItemID == suit.ItemID && tc.ToServiceUnitID == suit.ServiceUnitID)
                .LeftJoin(idismf).On(idismf.ItemID == fee.ItemID && idismf.SmfID == par.SRParamedicRL1)
                .LeftJoin(idic).On(idismf.IdiCode == idic.IdiCode)
                .InnerJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                .LeftJoin(res).On(fee.TransactionNo == res.TransactionNo && fee.ItemID == res.ItemID)

                .LeftJoin(tpio).On(fee.TransactionNo == tpio.TransactionNo && fee.SequenceNo == tpio.SequenceNo && tpio.IsPaymentProceed == true && tpio.IsPaymentReturned == false)
                .LeftJoin(tpiop).On(tpio.PaymentNo == tpiop.PaymentNo)

                .LeftJoin(cc).On(fee.TransactionNo == cc.TransactionNo && fee.SequenceNo == cc.SequenceNo)

                .LeftJoin(tpib).On(cc.IntermBillNo == tpib.IntermBillNo && tpib.IsPaymentProceed == true && tpib.IsPaymentReturned == false)
                .LeftJoin(tpibp).On(tpib.PaymentNo == tpibp.PaymentNo)

                .LeftJoin(tpibg).On(cc.IntermBillNo == tpibg.IntermBillNo && tpibg.IsPaymentProceed == true && tpibg.IsPaymentReturned == false)
                .LeftJoin(tpibgp).On(tpibg.PaymentNo == tpibgp.PaymentNo);

            if (flag == 0)
            {
                fee.Select(fee.TransactionNo, fee.SequenceNo, fee.TariffComponentID, fee.ParamedicID, fee.ItemID, i.ItemGroupID,
                    par.ParamedicName, par.SRParamedicRL1.As("SmfID"), i.ItemName,
                    tc.ToServiceUnitID.As("ServiceUnitID"), idismf.IdiCode.Coalesce("''"), idic.IdiName.Coalesce("''"),
                    fee.Qty, idic.F1.Coalesce("0.00").As("F_1"),
                    idic.F21.Coalesce("0.00").As("F_2_1"),
                    idic.F22.Coalesce("0.00").As("F_2_2"),
                    idic.F23.Coalesce("0.00").As("F_2_3"),
                    idic.F3.Coalesce("0.00").As("F_3"),
                    idic.F4.Coalesce("0.00").As("F_4"),
                    idic.Rvu.Coalesce("0.00").As("Rvu"));
            }
            else if (flag == 1)
            {
                fee.Select(fee);
            }

            fee.Where(
                    fee.DischargeDateMergeTo.IsNotNull(),
                    "<ISNULL(ISNULL(tpibgp.PaymentDate, tpibp.PaymentDate),tpiop.PaymentDate) between '" +
                    txtDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and '" + txtDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'>",
                    "<ISNULL(fee.IsWriteOff, 0) = 0>")
                .Where(
                    fee.Or(
                        fee.And(
                            //su.IsUsingJobOrder == true, 
                            i.SRItemType == "41",
                            res.ItemID.IsNotNull()),
                        //su.IsUsingJobOrder == false
                        i.SRItemType != "41"
                        )) // hanya yang sudah ada hasil
                .Where(smf.SmfName.NotIn(smfToHide)); //tambah filter dokter spesialis

            return fee;
        }

        private string SaveEntity(bool IsNew)
        {
            ParamedicFeeRemunByIdi entity = new ParamedicFeeRemunByIdi();
            ParamedicFeeRemunByIdiDetailCollection itemColl = new ParamedicFeeRemunByIdiDetailCollection();
            ParamedicFeeRemunByIdiSummaryCollection sumColl = new ParamedicFeeRemunByIdiSummaryCollection();
            var fees = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();

            if (IsNew)
            {
                entity.AddNew();

                if (feeRemunDetailTransaction.Rows.Count > 0)
                {
                    //fees.Query.Where(fees.Query.TransactionNo.In(feeRemunDetailTransaction.AsEnumerable().Select(x => x["TransactionNo"].ToString())));
                    //fees.LoadAll();
                    fees.Load(GetQueryFee(1));
                }
            }
            else
            {
                entity.LoadByRemunNo(txtRemunNo.Text);
                itemColl.Query.Where(itemColl.Query.RemunID == entity.RemunID);
                itemColl.LoadAll();

                fees.Query.Where(fees.Query.RemunByIdiID == entity.RemunID);
                fees.LoadAll();
                foreach (var fee in fees)
                {
                    // reset
                    fee.RemunByIdiID = null;
                }

                var newTransNo = new List<string>();

                if (feeRemunDetailTransaction != null)
                {
                    foreach (System.Data.DataRow r in feeRemunDetailTransaction.Rows)
                    {
                        if (!fees.Where(f => f.TransactionNo == r["TransactionNo"].ToString() && f.SequenceNo == r["SequenceNo"].ToString()).Any())
                        {
                            newTransNo.Add(r["TransactionNo"].ToString());
                        }
                    }
                }
                newTransNo = newTransNo.Distinct().ToList();

                if (newTransNo.Count() > 0)
                {
                    var fees2 = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    fees2.Query.Where(fees2.Query.TransactionNo.In(newTransNo));
                    fees2.LoadAll();
                    foreach (var fee in fees2)
                    {
                        if (!fees.Where(f =>
                             f.TransactionNo == fee.TransactionNo &&
                             f.SequenceNo == fee.SequenceNo &&
                             f.TariffComponentID == fee.TariffComponentID).Any())
                        {
                            fees.AttachEntity(fee);
                        }
                    }
                }
            }

            SetEntityValue(entity);

            if (!IsNew)
            {
                itemColl.MarkAllAsDeleted();
            }

            foreach (DataRow row in feeRemunDetail.Rows)
            {
                var ic = itemColl.AddNew();
                ic.ParamedicID = row["ParamedicID"].ToString();
                ic.ItemID = row["ItemID"].ToString();
                ic.ServiceUnitID = row["ServiceUnitID"].ToString();
                ic.IdiCode = row["IdiCode"].ToString();
                ic.Qty = System.Convert.ToDecimal(row["Qty"]);
                ic.Score = System.Convert.ToDecimal(row["Score"]);
                ic.Rvu = System.Convert.ToDecimal(row["Rvu"]);
                ic.RvuConversion = System.Convert.ToDecimal(row["RvuConversion"]);
                ic.Coefficient = System.Convert.ToDecimal(row["Coefficient"]);

                ic.SmfID = row["SmfID"].ToString();
                ic.ItemGroupID = row["ItemGroupID"].ToString();
                ic.SettingID = (int)row["SettingID"];
                ic.Multiplier = (decimal)row["Multiplier"];

                ic.CreateByUserID = AppSession.UserLogin.UserID;
                ic.CreateDateTime = DateTime.Now;
                ic.LastUpdateByUserID = AppSession.UserLogin.UserID;
                ic.LastUpdateDateTime = DateTime.Now;
            }

            if (!IsNew)
            {
                sumColl.Query.Where(sumColl.Query.RemunID == entity.RemunID);
                sumColl.LoadAll();
                sumColl.MarkAllAsDeleted();
            }
            foreach (GridDataItem gdi in grdSummary.MasterTableView.Items)
            {
                var sumd = sumColl.AddNew();
                sumd.ParamedicID = gdi.GetDataKeyValue("ParamedicID").ToString();
                sumd.CoorporateGradeLevel = System.Convert.ToInt32(gdi["CoorporateGradeLevel"].Text);
                sumd.CoorporateGradeValue = System.Convert.ToInt32((gdi.FindControl("txtCoorporateGradeValue") as RadNumericTextBox).Value);
                sumd.PositionFeeValue = System.Convert.ToDecimal((gdi.FindControl("txtPositionFeeValue") as RadNumericTextBox).Value);
                sumd.InsentifFeeValue = System.Convert.ToDecimal((gdi.FindControl("txtInsentifFeeValue") as RadNumericTextBox).Value);
                sumd.CoefficientSummary = System.Convert.ToDecimal(gdi["CoefficientSummary"].Text);
                sumd.ProcedureFeeValue = sumd.CoorporateGradeValue * sumd.CoefficientSummary * entity.KursPosition; // System.Convert.ToDecimal(gdi["ProcedureFeeValue"].Text);

                sumd.CreateByUserID = AppSession.UserLogin.UserID;
                sumd.CreateDateTime = DateTime.Now;
                sumd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                sumd.LastUpdateDateTime = DateTime.Now;
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (txtRemunNo.Text == string.Empty)
                {
                    var autono = GetAutoNumber();
                    if (autono == null)
                    {
                        return "Generating transaction number failed";
                    }
                    txtRemunNo.Text = autono.LastCompleteNumber;
                    entity.RemunNo = txtRemunNo.Text;
                    autono.Save();
                }

                entity.Save();
                foreach (var ic in itemColl)
                {
                    ic.RemunID = entity.RemunID;
                }
                itemColl.Save();
                foreach (var sc in sumColl)
                {
                    sc.RemunID = entity.RemunID;
                }
                sumColl.Save();

                if (feeRemunDetailTransaction != null)
                {
                    //foreach (var fee in fees)
                    //{
                    //    if (feeRemunDetailTransaction.AsEnumerable()
                    //        .Where(x =>
                    //            x["TransactionNo"].ToString() == fee.TransactionNo &&
                    //            x["SequenceNo"].ToString() == fee.SequenceNo &&
                    //            x["TariffComponentID"].ToString() == fee.TariffComponentID).Any())
                    //    {
                    //        fee.RemunByIdiID = entity.RemunID;
                    //    }
                    //}

                    var feexx = from f in fees
                                join x in feeRemunDetailTransaction.AsEnumerable()
                                on new { f.TransactionNo, f.SequenceNo, f.TariffComponentID } equals
                                new
                                {
                                    TransactionNo = x["TransactionNo"].ToString(),
                                    SequenceNo = x["SequenceNo"].ToString(),
                                    TariffComponentID = x["TariffComponentID"].ToString()
                                }
                                select f;

                    foreach (var fee in feexx)
                    {
                        fee.RemunByIdiID = entity.RemunID;
                    }

                    fees.Save();
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return string.Empty;
        }

        private void MoveRecord(bool isNextRecord)
        {
            BusinessObject.ParamedicFeeRemunByIdiQuery que = new BusinessObject.ParamedicFeeRemunByIdiQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.RemunNo > txtRemunNo.Text
                    );
                que.OrderBy(que.RemunNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.RemunNo < txtRemunNo.Text
                    );
                que.OrderBy(que.RemunNo.Descending);
            }

            BusinessObject.ParamedicFeeRemunByIdi entity = new BusinessObject.ParamedicFeeRemunByIdi();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function
        private DataTable GetRemunSum(int RemunID)
        {
            var rsum = new ParamedicFeeRemunByIdiSummaryQuery("rsum");
            var par = new ParamedicQuery("par");
            rsum.InnerJoin(par).On(rsum.ParamedicID == par.ParamedicID)
                .Where(rsum.RemunID == RemunID)
                .Select(
                    rsum.ParamedicID,
                    par.ParamedicName,
                    rsum.CoorporateGradeLevel,
                    rsum.CoorporateGradeValue,
                    rsum.PositionFeeValue,
                    rsum.InsentifFeeValue,
                    rsum.CoefficientSummary,
                    rsum.ProcedureFeeValue
                );
            return rsum.LoadDataTable();
        }
        private DataTable GetRemunDetails(int RemunID)
        {
            var prd = new ParamedicFeeRemunByIdiDetailQuery("prd");
            var par = new ParamedicQuery("par");
            var i = new ItemQuery("i");
            var idi = new ItemIdiQuery("idi");
            prd.InnerJoin(par).On(prd.ParamedicID == par.ParamedicID)
                .InnerJoin(i).On(prd.ItemID == i.ItemID)
                .LeftJoin(idi).On(prd.IdiCode == idi.IdiCode)
                .Select(prd.ParamedicID, par.ParamedicName, prd.SmfID.Coalesce("par.SRParamedicRL1"),
                    prd.ItemID, i.ItemName, prd.ItemGroupID.Coalesce("i.ItemGroupID"),
                    prd.ServiceUnitID, prd.IdiCode, idi.IdiName,
                    prd.Qty, prd.Score, prd.Rvu, prd.RvuConversion, prd.SettingID.Coalesce("0"), prd.Multiplier.Coalesce("1"), prd.Coefficient
                )
                .Where(prd.RemunID == RemunID)
                .OrderBy(prd.ParamedicID.Ascending, prd.ItemID.Ascending);
            feeRemunDetail = prd.LoadDataTable();

            feeRemunDetailTransaction = null;

            return feeRemunDetail;
        }

        private void RefreshGridSummary(DataTable dtFee, int? RemunID)
        {
            //var parColl = new ParamedicCollection();
            //parColl.Query.Where(parColl.Query.IsActive == true,
            //    parColl.Query.SRParamedicType.In("ParamedicType-002", "ParamedicType-003", "ParamedicType-010", "ParamedicType-011"));
            //parColl.LoadAll();
            //Dictionary<string, string> parDict;
            //if (!RemunID.HasValue)
            //{
            //    parDict = parColl.ToDictionary(p => p.ParamedicID, p => p.ParamedicName);
            //}
            //else {
            //    parDict = dtFee.AsEnumerable().ToDictionary(p => p["ParamedicID"].ToString(), p => p["ParamedicName"].ToString());
            //}

            var dtSum = GetRemunSum(RemunID ?? 0);

            if (RemunID == null)
            {
                var ndt = dtFee.AsEnumerable()
                    .GroupBy(x => new { ParamedicID = x.Field<string>("ParamedicID"), ParamedicName = x.Field<string>("ParamedicName") })
                    .Select(x => new {
                        ParamedicID = x.Key.ParamedicID,
                        ParamedicName = x.Key.ParamedicName,
                        CoefficientSummary = x.Sum(y => y.Field<decimal>("Coefficient")),
                        empt = string.Empty
                    });

                // coorporate grade
                var parColl = new ParamedicCollection();
                parColl.Query.Where(parColl.Query.IsActive == true);
                parColl.Query.Select(parColl.Query.ParamedicID,
                    parColl.Query.ParamedicName,
                    parColl.Query.CoorporateGradeID,
                    parColl.Query.CoorporateGradeValue
                    );
                parColl.LoadAll();
                var cpColl = new CoorporateGradeCollection();
                cpColl.LoadAll();

                foreach (var dt in ndt)
                {
                    DataRow d = dtSum.AsEnumerable().Where(x => x["ParamedicID"].ToString() == dt.ParamedicID).FirstOrDefault();
                    if (d == null)
                    {
                        d = dtSum.NewRow();
                        d["ParamedicID"] = dt.ParamedicID;
                        d["ParamedicName"] = dt.ParamedicName;
                        dtSum.Rows.Add(d);
                    }

                    var cLevel = cpColl.Where(x => x.CoorporateGradeID == (
                        parColl.Where(p => p.ParamedicID == dt.ParamedicID)
                        .Select(p => p.CoorporateGradeID ?? 0).FirstOrDefault())).Select(x => x.CoorporateGradeLevel ?? 0).FirstOrDefault();
                    d["CoorporateGradeLevel"] = System.Convert.ToInt32(cLevel);

                    d["CoorporateGradeValue"] = (parColl.Where(p => p.ParamedicID == dt.ParamedicID).Select(p => p.CoorporateGradeValue).FirstOrDefault() ?? 0);
                    d["PositionFeeValue"] = 0;
                    d["InsentifFeeValue"] = 0;
                    d["CoefficientSummary"] = dt.CoefficientSummary;
                    d["ProcedureFeeValue"] = System.Convert.ToDecimal(d["CoorporateGradeValue"]) * System.Convert.ToDecimal(d["CoefficientSummary"]) * System.Convert.ToDecimal(txtKursPosition.Value);
                }
            }

            var c = dtSum.Columns.Add("SMFName", typeof(string));
            var pColl = new ParamedicCollection();
            var pidList = dtSum.AsEnumerable().Select(p => p["ParamedicID"].ToString()).Distinct().ToList();
            if (pidList.Count > 0)
            {
                pColl.Query.Where(pColl.Query.ParamedicID.In(pidList));
                pColl.LoadAll();

                var smfColl = new SmfCollection();
                smfColl.LoadAll();
                foreach (System.Data.DataRow row in dtSum.Rows)
                {
                    var par = pColl.Where(p => p.ParamedicID == row["ParamedicID"].ToString()).FirstOrDefault();
                    if (par != null)
                    {
                        var smf = smfColl.Where(s => s.SmfID == par.SRParamedicRL1).FirstOrDefault();
                        if (smf != null)
                        {
                            row["SMFName"] = smf.SmfName;
                        }
                    }
                }
            }

            grdSummary.DataSource = null;
            var dtSumFiltered = dtSum.AsEnumerable().Where(r => !smfToHide.Contains(r["SMFName"].ToString().ToLower()) && (decimal)r["CoefficientSummary"] != 0);
            if (dtSumFiltered.Count() > 0)
            {
                dtSum = dtSumFiltered.CopyToDataTable();
            }
            grdSummary.DataSource = dtSum;
            grdSummary.DataBind();

            decimal fundAllocation = txtFundAllocProcedure.Value.ToDecimal();
            lblFundAllocation.Text = fundAllocation.ToString("n2");
            decimal tAllocated = dtSum.AsEnumerable().Sum(r => System.Convert.ToDecimal(r["ProcedureFeeValue"]));
            lblTotalAllocated.Text = tAllocated.ToString("n2");
            lblDifference.Text = (fundAllocation - tAllocated).ToString("n2");
        }
        #endregion

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!txtDateStart.SelectedDate.HasValue)
            {
                ShowInformationHeader("Invalid date start");
                return;
            }
            if (!txtDateEnd.SelectedDate.HasValue)
            {
                ShowInformationHeader("Invalid date end");
                return;
            }

            if ((txtAdjustmentFactor.Value ?? 0) == 0)
            {
                ShowInformationHeader("Invalid adjustment factor");
                return;
            }

            var remColl = new ParamedicFeeRemunByIdiCollection();
            remColl.Query.Where(
                remColl.Query.Or(
                    remColl.Query.DateStart.Between(txtDateStart.SelectedDate, txtDateEnd.SelectedDate),
                    remColl.Query.DateEnd.Between(txtDateStart.SelectedDate, txtDateEnd.SelectedDate),
                    remColl.Query.And(
                        txtDateStart.SelectedDate <= remColl.Query.DateStart,
                        txtDateEnd.SelectedDate >= remColl.Query.DateStart
                        )
                    ),
                remColl.Query.RemunNo != txtRemunNo.Text,
                remColl.Query.IsVoid == false
                );
            if (remColl.LoadAll())
            {
                ShowInformationHeader("Selected period has exist");
                return;
            }

            ////var dt = GetRemunDetails(0);
            //var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            //var tc = new TransChargesQuery("tc");
            ////var suit = new ServiceUnitItemServiceQuery("suit");
            //var i = new ItemQuery("i");
            //var par = new ParamedicQuery("par");
            //var smf = new SmfQuery("smf");
            //var idismf = new ItemIdiItemSmfQuery("idismf");
            //var idic = new ItemIdiQuery("idic");
            //var reg = new RegistrationQuery("reg");
            //var guar = new GuarantorQuery("guar");
            //var su = new ServiceUnitQuery("su");
            //var res = new TestResultQuery("res");

            //var tpio = new TransPaymentItemOrderQuery("tpio");
            //var tpiop = new TransPaymentQuery("tpiop");

            //var cc = new CostCalculationQuery("cc");

            //var tpib = new TransPaymentItemIntermBillQuery("tpib");
            //var tpibp = new TransPaymentQuery("tpibp");

            //var tpibg = new TransPaymentItemIntermBillGuarantorQuery("tpibg");
            //var tpibgp = new TransPaymentQuery("tpibgp");


            //fee.InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
            //    .InnerJoin(i).On(fee.ItemID == i.ItemID)
            //    .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
            //    .InnerJoin(smf).On(par.SRParamedicRL1 == smf.SmfID)
            //    .InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo)
            //    .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID && guar.IsParamedicFeeRemun == true) //guar.SRGuarantorType == "09"/*BPJS*/)
            //                                                                                                 //.LeftJoin(suit).On(fee.ItemID == suit.ItemID && tc.ToServiceUnitID == suit.ServiceUnitID)
            //    .LeftJoin(idismf).On(idismf.ItemID == fee.ItemID && idismf.SmfID == par.SRParamedicRL1)
            //    .LeftJoin(idic).On(idismf.IdiCode == idic.IdiCode)
            //    .InnerJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
            //    .LeftJoin(res).On(fee.TransactionNo == res.TransactionNo && fee.ItemID == res.ItemID)

            //    .LeftJoin(tpio).On(fee.TransactionNo == tpio.TransactionNo && fee.SequenceNo == tpio.SequenceNo && tpio.IsPaymentProceed == true && tpio.IsPaymentReturned == false)
            //    .LeftJoin(tpiop).On(tpio.PaymentNo == tpiop.PaymentNo)

            //    .LeftJoin(cc).On(fee.TransactionNo == cc.TransactionNo && fee.SequenceNo == cc.SequenceNo)

            //    .LeftJoin(tpib).On(cc.IntermBillNo == tpib.IntermBillNo && tpib.IsPaymentProceed == true && tpib.IsPaymentReturned == false)
            //    .LeftJoin(tpibp).On(tpib.PaymentNo == tpibp.PaymentNo)

            //    .LeftJoin(tpibg).On(cc.IntermBillNo == tpibg.IntermBillNo && tpibg.IsPaymentProceed == true && tpibg.IsPaymentReturned == false)
            //    .LeftJoin(tpibgp).On(tpibg.PaymentNo == tpibgp.PaymentNo)

            //    .Select(fee.TransactionNo, fee.SequenceNo, fee.TariffComponentID, fee.ParamedicID, fee.ItemID, i.ItemGroupID,
            //        par.ParamedicName, par.SRParamedicRL1.As("SmfID"), i.ItemName,
            //        tc.ToServiceUnitID.As("ServiceUnitID"), idismf.IdiCode.Coalesce("''"), idic.IdiName.Coalesce("''"),
            //        fee.Qty, idic.F1.Coalesce("0.00").As("F_1"),
            //        idic.F21.Coalesce("0.00").As("F_2_1"),
            //        idic.F22.Coalesce("0.00").As("F_2_2"),
            //        idic.F23.Coalesce("0.00").As("F_2_3"),
            //        idic.F3.Coalesce("0.00").As("F_3"),
            //        idic.F4.Coalesce("0.00").As("F_4"),
            //        idic.Rvu.Coalesce("0.00").As("Rvu"))

            //    .Where(
            //        fee.DischargeDateMergeTo.IsNotNull(),
            //        "<ISNULL(ISNULL(tpibgp.PaymentDate, tpibp.PaymentDate),tpiop.PaymentDate) between '" +
            //        txtDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and '" + txtDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'>",
            //        "<ISNULL(fee.IsWriteOff, 0) = 0>")
            //    .Where(
            //        fee.Or(
            //            fee.And(
            //                //su.IsUsingJobOrder == true, 
            //                i.SRItemType == "41",
            //                res.ItemID.IsNotNull()),
            //            //su.IsUsingJobOrder == false
            //            i.SRItemType != "41"
            //            )) // hanya yang sudah ada hasil
            //    .Where(smf.SmfName.NotIn(smfToHide)); //tambah filter dokter spesialis

            //.Select(fee.ParamedicID, par.ParamedicName, fee.ItemID, i.ItemName, tc.ToServiceUnitID.As("ServiceUnitID"), suit.IdiCode, idic.IdiName,
            //    fee.Qty.Sum(), (idic.F1 + idic.F21 + idic.F22 + idic.F23 + idic.F3 + idic.F4).Coalesce("0").As("Score"),
            //    idic.Rvu.Coalesce("0").As("Rvu"))

            //.GroupBy(fee.ParamedicID, par.ParamedicName, fee.ItemID, i.ItemName, tc.ToServiceUnitID, suit.IdiCode, idic.IdiName,
            //    idic.F1, idic.F21, idic.F22, idic.F23, idic.F3, idic.F4, idic.Rvu)

            //.Having(fee.Qty.Sum() != 0);
            //;

            var fee = GetQueryFee(0);
            var dtFee = fee.LoadDataTable();

            // set dtFee sebagai remun unapprove 

            var oRemuns = dtFee.AsEnumerable().GroupBy(r => new
            {
                ParamedicID = r["ParamedicID"].ToString(),
                ParamedicName = r["ParamedicName"].ToString(),
                SmfID = r["SmfID"].ToString(),
                ItemID = r["ItemID"].ToString(),
                ItemName = r["ItemName"].ToString(),
                ItemGroupID = r["ItemGroupID"].ToString(),
                ServiceUnitID = r["ServiceUnitID"].ToString(),
                IdiCode = r["IdiCode"].ToString(),
                IdiName = r["IdiName"].ToString(),
                F1 = System.Convert.ToDecimal(r["F_1"]),
                F21 = System.Convert.ToDecimal(r["F_2_1"]),
                F22 = System.Convert.ToDecimal(r["F_2_2"]),
                F23 = System.Convert.ToDecimal(r["F_2_3"]),
                F3 = System.Convert.ToDecimal(r["F_3"]),
                F4 = System.Convert.ToDecimal(r["F_4"]),
                Rvu = System.Convert.ToDecimal(r["Rvu"])
            })
            .Select(r => new {
                ParamedicID = r.Key.ParamedicID,
                ParamedicName = r.Key.ParamedicName,
                SmfID = r.Key.SmfID,
                ItemID = r.Key.ItemID,
                ItemName = r.Key.ItemName,
                ItemGroupID = r.Key.ItemGroupID,
                ServiceUnitID = r.Key.ServiceUnitID,
                IdiCode = r.Key.IdiCode,
                IdiName = r.Key.IdiName,
                Qty = r.Sum(s => System.Convert.ToDecimal(s["Qty"])),
                F1 = System.Convert.ToDecimal(r.Key.F1),
                F21 = System.Convert.ToDecimal(r.Key.F21),
                F22 = System.Convert.ToDecimal(r.Key.F22),
                F23 = System.Convert.ToDecimal(r.Key.F23),
                F3 = System.Convert.ToDecimal(r.Key.F3),
                F4 = System.Convert.ToDecimal(r.Key.F4),
                Rvu = System.Convert.ToDecimal(r.Key.Rvu)
            });

            var dtRemun = new DataTable();
            dtRemun.Columns.Add("ParamedicID", typeof(string));
            dtRemun.Columns.Add("ParamedicName", typeof(string));
            dtRemun.Columns.Add("SmfID", typeof(string));
            dtRemun.Columns.Add("ItemID", typeof(string));
            dtRemun.Columns.Add("ItemName", typeof(string));
            dtRemun.Columns.Add("ItemGroupID", typeof(string));
            dtRemun.Columns.Add("ServiceUnitID", typeof(string));
            dtRemun.Columns.Add("IdiCode", typeof(string));
            dtRemun.Columns.Add("IdiName", typeof(string));
            dtRemun.Columns.Add("Qty", typeof(decimal));
            dtRemun.Columns.Add("Score", typeof(decimal));
            dtRemun.Columns.Add("Rvu", typeof(decimal));

            foreach (var oRem in oRemuns)
            {
                var r = dtRemun.NewRow();
                r["ParamedicID"] = oRem.ParamedicID;
                r["ParamedicName"] = oRem.ParamedicName;
                r["SmfID"] = oRem.SmfID;
                r["ItemID"] = oRem.ItemID;
                r["ItemName"] = oRem.ItemName;
                r["ItemGroupID"] = oRem.ItemGroupID;
                r["ServiceUnitID"] = oRem.ServiceUnitID;
                r["IdiCode"] = oRem.IdiCode;
                r["IdiName"] = oRem.IdiName;
                r["Qty"] = oRem.Qty;
                r["Score"] = oRem.F1 + oRem.F21 + oRem.F22 + oRem.F23 + oRem.F3 + oRem.F4;
                r["Rvu"] = oRem.Rvu;
                dtRemun.Rows.Add(r);
            }

            dtRemun.Columns.Add("RvuConversion", typeof(decimal));
            dtRemun.Columns.Add("SettingID", typeof(int));
            dtRemun.Columns.Add("Multiplier", typeof(decimal));
            dtRemun.Columns.Add("Coefficient", typeof(decimal));

            var rSettings = new ParamedicFeeRemunByIdiSettingsCollection();
            rSettings.LoadAll();

            foreach (DataRow dr in dtRemun.Rows)
            {
                dr["RvuConversion"] = System.Convert.ToDecimal(dr["Rvu"]) / System.Convert.ToDecimal(txtAdjustmentFactor.Value);
                dr["Coefficient"] = System.Convert.ToDecimal(dr["Qty"]) * System.Convert.ToDecimal(dr["Score"]) * System.Convert.ToDecimal(dr["RvuConversion"]);

                dr["SettingID"] = (int)0;
                dr["Multiplier"] = (decimal)1;
                var rSet = FindMatchFeeRemunSetting(rSettings, dr);
                if (rSet != null)
                {
                    // what to do??
                }

                dr["Coefficient"] = System.Convert.ToDecimal(dr["Coefficient"]) * System.Convert.ToDecimal(dr["Multiplier"]);
            }
            //dtFee.AcceptChanges();
            //grdRemun.DataSource = null;
            //grdRemun.DataSource = dtFee;
            //grdRemun.DataBind();
            feeRemunDetail = dtRemun;

            // optimasi memory
            while (dtFee.Columns.Count > 5)
            {
                dtFee.Columns.RemoveAt(dtFee.Columns.Count - 1);
            }
            feeRemunDetailTransaction = dtFee;

            RefreshGridSummary(dtRemun, new int?());

            ShowInformationHeader("Calculate complete");
        }

        private ParamedicFeeRemunByIdiSettings FindMatchFeeRemunSetting(ParamedicFeeRemunByIdiSettingsCollection rSetting, DataRow dr)
        {
            var r = rSetting.Where(x =>
                    (x.SmfID.Equals(dr["SmfID"].ToString()) || x.SmfID.Equals(string.Empty)) &&
                    (x.ParamedicID.Equals(dr["ParamedicID"].ToString()) || x.ParamedicID.Equals(string.Empty)) &&
                    (x.ItemGroupID.Equals(dr["ItemGroupID"].ToString()) || x.ItemGroupID.Equals(string.Empty)) &&
                    (x.ItemID.Equals(dr["ItemID"].ToString()) || x.ItemID.Equals(string.Empty)))

                    .OrderByDescending(x => x.SmfID)
                    .OrderByDescending(x => x.ParamedicID)
                    .OrderByDescending(x => x.ItemGroupID)
                    .OrderByDescending(x => x.ItemID)
                    .FirstOrDefault();
            if (r != null)
            {
                dr["SettingID"] = r.SettingID ?? 0;
                dr["Multiplier"] = r.MultiplierValue ?? 0;
            }

            return r;
        }

        protected void grdSummary_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                item["CoefficientSummary"].ToolTip = "Summary of coefficient (click on Physician name to show detail coefficient)";
                item["ProcedureFeeValue"].ToolTip = "Coorporate Grade Value * Coefficient * P1";
                item["Total"].ToolTip = "Position Fee + Insentif Fee + Procedure Fee";
            }
        }
    }
}