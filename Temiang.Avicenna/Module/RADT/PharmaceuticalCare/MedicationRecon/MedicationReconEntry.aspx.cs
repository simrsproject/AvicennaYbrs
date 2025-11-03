using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class MedicationReconEntry : BasePageDialogEntry
    {
        private Registration _regCurr;
        private new Registration RegistrationCurrent
        {
            get
            {
                if (_regCurr == null)
                {
                    _regCurr = new Registration();
                    _regCurr.LoadByPrimaryKey(RegistrationNo);
                }
                return _regCurr;
            }
        }
        protected string FromRegistrationNo
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["fregno"]))
                {
                    if (ViewState["fregno"] == null || string.IsNullOrEmpty(ViewState["fregno"].ToString()))
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(RegistrationNo);
                        ViewState["fregno"] = reg.FromRegistrationNo;
                    }

                    return ViewState["fregno"].ToString();
                }
                else
                {
                    return Request.QueryString["fregno"];
                }
            }
        }

        private string ReconType => Request.QueryString["rectype"].ToUpper();

        protected void Page_Init(object sender, EventArgs e)
        {
            // Program Fiture
            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = true;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            divPatSign.Visible = !ReconType.Equals("TRF"); //ttd di recon Transfer Pasien tidak perlu (RSI)
            // divParSign.Visible = !ReconType.Equals("DCG"); //ttd di recon discharge DPJP tidak perlu (RSI)

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    patientTransferSelection.Visible = false;
                    var title = string.Empty;
                    switch (ReconType)
                    {
                        case "ADM":
                            title = "Admission Reconciliation";

                            tabStrip.Tabs[0].Text = "Drug Item From Patient";
                            tabStrip.Tabs[1].Visible = false;

                            // Additional Toolbar item 
                            var tbiSplit = new RadToolBarButton();
                            tbiSplit.IsSeparator = true;
                            ToolBarMenuData.Items.Add(tbiSplit);

                            var tbi = new RadToolBarButton();
                            tbi.Text = "Drug Item From Patient";
                            tbi.ImageUrl = "~/Images/Toolbar/drugs16.png";
                            tbi.Value = "drugfrompatient";
                            ToolBarMenuData.Items.Add(tbi);

                            // Additional Toolbar item 
                            var tbiSplit2 = new RadToolBarButton();
                            tbiSplit2.IsSeparator = true;
                            ToolBarMenuData.Items.Add(tbiSplit2);

                            var tbi2 = new RadToolBarButton();
                            tbi2.Text = "Education";
                            tbi2.ImageUrl = "~/Images/Toolbar/drugs16.png";
                            tbi2.Value = "education";
                            ToolBarMenuData.Items.Add(tbi2);

                            break;
                        case "TRF":
                            title = "Transfer Reconciliation";

                            patientTransferSelection.Visible = true;

                            //Populate cboPatientTransfer
                            var pt = new PatientTransferQuery("a");
                            var fsu = new ServiceUnitQuery("fs");
                            pt.InnerJoin(fsu).On(pt.FromServiceUnitID == fsu.ServiceUnitID);

                            var tsu = new ServiceUnitQuery("ts");
                            pt.InnerJoin(tsu).On(pt.ToServiceUnitID == tsu.ServiceUnitID);

                            pt.Select(pt.TransferNo, pt.TransferDate, pt.FromServiceUnitID, pt.ToServiceUnitID,
                                fsu.ServiceUnitName.As("FromServiceUnitName"),
                                tsu.ServiceUnitName.As("ToServiceUnitName")
                                );
                            pt.Where(pt.RegistrationNo == RegistrationNo, pt.FromServiceUnitID != pt.ToServiceUnitID);
                            pt.OrderBy(pt.TransferNo.Descending);
                            var dtb = pt.LoadDataTable();
                            //Insert FromRegistratonNo
                            if (!string.IsNullOrWhiteSpace(RegistrationCurrent.FromRegistrationNo))
                            {
                                var fromReg = new Registration();
                                if (fromReg.LoadByPrimaryKey(RegistrationCurrent.FromRegistrationNo))
                                {
                                    var fromSu = new ServiceUnit();
                                    if (fromSu.LoadByPrimaryKey(fromReg.ServiceUnitID))
                                    {
                                        //Insert to first line
                                        var firstRow = dtb.NewRow();
                                        firstRow["TransferNo"] = "FROMREG";
                                        firstRow["TransferDate"] = RegistrationCurrent.RegistrationDate;
                                        firstRow["FromServiceUnitID"] = fromSu.ServiceUnitID;
                                        firstRow["FromServiceUnitName"] = fromSu.ServiceUnitName;
                                        firstRow["ToServiceUnitID"] = RegistrationCurrent.ServiceUnitID;
                                        var toSu = new ServiceUnit();
                                        toSu.LoadByPrimaryKey(RegistrationCurrent.ServiceUnitID);
                                        firstRow["ToServiceUnitName"] = toSu.ServiceUnitName;
                                        dtb.Rows.InsertAt(firstRow, 0);
                                    }
                                }
                            }

                            cboPatientTransfer.DataSource = dtb;
                            cboPatientTransfer.DataBind();
                            break;
                        case "DCG":
                            title = "Discharge Reconciliation";
                            break;
                    }

                    this.Title = string.Format("{0} : {1}  (MRN: {2})", title, pat.PatientName, pat.MedicalNo);

                }
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Last Recon Status
                var lastRecon = new MedicationRecon();
                lastRecon.Query.Where(lastRecon.Query.RegistrationNo == RegistrationNo, lastRecon.Query.ReconType == ReconType);
                lastRecon.Query.es.Top = 1;
                lastRecon.Query.OrderBy(lastRecon.Query.ReconSeqNo.Descending);
                if (lastRecon.Query.Load())
                {
                    PopulateReconInf(lastRecon);

                    if (ReconType.Equals("TRF"))
                    {
                        cboPatientTransfer.Value = lastRecon.TransferNo;
                    }
                }
                else
                {
                    if (ReconType.Equals("TRF"))
                    {
                        hdnReconSeqNo.Value = "0"; //New Transfer
                    }
                }
            }

            if (Request.Form["__EVENTARGUMENT"] == "refresh")
            {
                Session["dtbrecon_" + grdMedicationReceive] = null;
                grdMedicationReceive.DataSource = null;
                grdMedicationReceive.Rebind();

                Session["dtbrecon_" + grdStoppedMed] = null;
                grdStoppedMed.DataSource = null;
                grdStoppedMed.Rebind();
            }
        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            if (!IsPostBack) return; // SUdah diload pada event page load

            // Saat posback hanya refersh TTD nya saja
            if (!string.IsNullOrWhiteSpace(hdnReconSeqNo.Value) && hdnReconSeqNo.Value != "0")
            {
                var recon = new MedicationRecon();
                recon.LoadByPrimaryKey(RegistrationNo, hdnReconSeqNo.Value.ToInt());

                PopulateSignImage(rbiSign, hdnSign, recon.SignImg);
                PopulateSignImage(rbiPatSign, hdnPatSign, recon.PatientSignImg);

                if (divParSign.Visible)
                    PopulateSignImage(rbiParSign, hdnParSign, recon.ParamedicSignImg);

            }
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (oldVal == AppEnum.DataMode.Read && newVal != AppEnum.DataMode.Read)
            {
                Session.Remove("dtbrecon_" + grdMedicationReceive.ID);
                grdMedicationReceive.DataSource = null; //Must set null for fire NeedDatasource at Rebind

                Session.Remove("dtbrecon_" + grdStoppedMed.ID);
                grdStoppedMed.DataSource = null; //Must set null for fire NeedDatasource at Rebind

            }

            if (patientTransferSelection.Visible)
            {
                cboPatientTransfer.Enable = (newVal == AppEnum.DataMode.Read);
            }

            grdMedicationReceive.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            grdMedicationReceive.Columns.FindByUniqueName("MenuApprove").Visible = (newVal == AppEnum.DataMode.Read);
            grdMedicationReceive.Rebind();

            grdStoppedMed.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            grdStoppedMed.Rebind();

            //Sign
            var isEnable = newVal != AppEnum.DataMode.Read;
            btnSign.Enabled = isEnable;
            btnParSign.Enabled = isEnable;
            btnPatSign.Enabled = isEnable;
        }
        protected override void OnMenuNewClick()
        {
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args);
            //if (args.IsCancel==false)
            //{
            //    // Popup Sign Entry
            //    ClientScript.RegisterStartupScript(this.Page.GetType(), "showsign", "Sys.Application.add_load(ShowSignEntry);", true);
            //}
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "savesign")
            {
                ValidateArgs args = new ValidateArgs();
                //SaveSign(args);
                ClientScript.RegisterStartupScript(this.Page.GetType(), "ok", "alert('ok save sign');", true);
            }
        }

        private bool Save(ValidateArgs args)
        {
            var isNew = false;
            using (var trans = new esTransactionScope())
            {
                var newReconSeqNo = 1;

                MedicationRecon mr;

                if (string.IsNullOrWhiteSpace(hdnReconSeqNo.Value) || hdnReconSeqNo.Value == "0")
                {
                    // Create new SeqNO
                    // New ReconSeqNo
                    var qr = new MedicationReconQuery("a");
                    var ent = new MedicationRecon();
                    qr.es.Top = 1;
                    qr.Where(qr.RegistrationNo == RegistrationNo);
                    qr.OrderBy(qr.ReconSeqNo.Descending);

                    if (ent.Load(qr))
                    {
                        newReconSeqNo = ent.ReconSeqNo.ToInt() + 1;
                    }

                    // Create New
                    mr = new MedicationRecon();
                    mr.RegistrationNo = RegistrationNo;
                    mr.ReconSeqNo = newReconSeqNo;
                    mr.ReconType = ReconType;
                    if ("TRF".Equals(ReconType))
                    {
                        mr.TransferNo = cboPatientTransfer.Value;
                    }
                    mr.IsFinish = chkIsFinish.Checked;
                    mr.BodyWeight = VitalSign.LastVitalSign(MergeRegistrations, VitalSign.VitalSignEnum.BodyWeight, DateTime.Now).ToDecimal();

                    isNew = true;
                }
                else
                {
                    // Update MedicationRecon
                    mr = new MedicationRecon();
                    mr.Query.Where(mr.Query.RegistrationNo == RegistrationNo, mr.Query.ReconSeqNo == hdnReconSeqNo.Value.ToInt());
                    mr.Query.OrderBy(mr.Query.ReconSeqNo.Descending);
                    mr.Query.Load();
                }

                var imgHelper = new ImageHelper();
                if (!string.IsNullOrWhiteSpace(hdnSign.Value))
                {
                    var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnSign.Value), new System.Drawing.Size(332, 185));
                    mr.SignImg = imgHelper.ToByteArray(resized, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                    mr.SignImg = null;

                if (divParSign.Visible && !string.IsNullOrWhiteSpace(hdnParSign.Value))
                {
                    var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnParSign.Value), new System.Drawing.Size(332, 185));
                    mr.ParamedicSignImg = imgHelper.ToByteArray(resized, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                    mr.ParamedicSignImg = null;

                if (!string.IsNullOrWhiteSpace(hdnPatSign.Value))
                {
                    var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnPatSign.Value), new System.Drawing.Size(332, 185));
                    mr.PatientSignImg = imgHelper.ToByteArray(resized, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                    mr.PatientSignImg = null;


                mr.ParamedicID = RegistrationCurrent.ParamedicID;
                mr.Save();

                // Insert Detail
                // For Continued Medication
                SaveMedicationReconLine(grdMedicationReceive.ID, mr.ReconSeqNo ?? 0);

                if (!"ADM".Equals(ReconType)) // Admisi tidak difilter status stop continue nya
                {
                    // For Stopped Medication
                    SaveMedicationReconLine(grdStoppedMed.ID, mr.ReconSeqNo ?? 0);
                }

                //Commit if success, Rollback if failed
                trans.Complete();

                if (isNew)
                    hdnReconSeqNo.Value = newReconSeqNo.ToString();
            }
            return true;
        }

        private void SaveMedicationReconLine(string forGridId, int reconSeqNo)
        {
            var medicationReconDataSource = MedicationReconDataSource(forGridId);
            foreach (DataRow row in medicationReconDataSource.Rows)
            {
                var mrno = row["MedicationReceiveNo"].ToInt();

                // Update MedicationReconLine
                var mrl = new MedicationReconLine();
                mrl.Query.Where(mrl.Query.RegistrationNo == RegistrationNo, mrl.Query.ReconSeqNo == reconSeqNo, mrl.Query.MedicationReceiveNo == mrno);
                if (!mrl.Query.Load())
                {
                    if (row["ReconStatus"] == DBNull.Value) continue;

                    //New
                    mrl = new MedicationReconLine();
                    mrl.RegistrationNo = RegistrationNo;
                    mrl.ReconSeqNo = reconSeqNo;
                    mrl.MedicationReceiveNo = mrno;
                }

                if (row["ReconStatus"] == DBNull.Value)
                {
                    mrl.MarkAsDeleted(); // Delete
                }
                else
                {
                    mrl.ReconStatus = row["ReconStatus"].ToString();
                    if ("CC".Equals(mrl.ReconStatus)) //Contnue Change (Continue with consume method changed)
                    {
                        mrl.SRConsumeMethod = row["NewSRConsumeMethod"].ToString();
                        mrl.ConsumeQty = row["NewConsumeQty"].ToString();
                        mrl.SRConsumeUnit = row["NewSRConsumeUnit"].ToString();
                        mrl.SRMedicationConsume = row["NewSRMedicationConsume"].ToString();

                        // Create New Medication Receive for new Therapy
                        // Untuk Recon Adm saat di approve oleh dokter 
                        if (!"ADM".Equals(ReconType))
                        {
                            var mrSource = new MedicationReceive();
                            mrSource.LoadByPrimaryKey(mrno);
                            mrSource.IsContinue = !"ST".Equals(mrl.ReconStatus);
                            mrSource.Save();

                            // Rekon oleh Farmasi tidak mengintervensi Kardex
                            //CreateNewMedicationReceive(mrSource, mrl.ReconStatus, mrl.SRConsumeMethod, mrl.ConsumeQty, mrl.SRConsumeUnit, mrl.SRMedicationConsume);
                        }
                    }
                    else
                    {
                        mrl.str.SRConsumeMethod = String.Empty;
                        mrl.str.ConsumeQty = String.Empty;
                        mrl.str.SRConsumeUnit = String.Empty;
                    }
                }

                mrl.Save();

            }
        }

        private int NewMedicationReceiveUsedSequenceNo(long medicationReceiveNo)
        {
            var qr = new MedicationReceiveUsedQuery("a");
            var fb = new MedicationReceiveUsed();
            qr.es.Top = 1;
            qr.Where(qr.MedicationReceiveNo == medicationReceiveNo);
            qr.OrderBy(qr.SequenceNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SequenceNo.ToInt() + 1;
            }
            return 1;
        }


        private void CreateNewMedicationReceive(MedicationReceive mrSource, string reconStatus, string newSRConsumeMethod, string newConsumeQty, string newSRConsumeUnit, string newSRMedicationConsume)
        {
            // Hanya untuk obat yg dibawa pasien dari rumah yg ada di recon Admisi
            if (!"ADM".Equals(ReconType)) return;

            // Hanay untuk tipe Change Consume Method
            if (!"CC".Equals(reconStatus)) return;

            // Create Used untuk menghabiskan stok di item asal
            var mrUsed = new MedicationReceiveUsed();
            mrUsed.MedicationReceiveNo = mrSource.MedicationReceiveNo;
            mrUsed.SequenceNo = NewMedicationReceiveUsedSequenceNo(mrSource.MedicationReceiveNo ?? 0);
            mrUsed.Qty = mrSource.BalanceRealQty; // Habiskan balance

            var date = DateTime.Now;
            mrUsed.RealizedDateTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
            mrUsed.RealizedByUserID = AppSession.UserLogin.UserID;

            mrUsed.SetupDateTime = mrUsed.RealizedDateTime;
            mrUsed.SetupByUserID = mrUsed.RealizedByUserID;

            mrUsed.VerificationDateTime = mrUsed.RealizedDateTime;
            mrUsed.VerificationByUserID = mrUsed.RealizedByUserID;

            mrUsed.ScheduleDateTime = mrUsed.RealizedDateTime;

            mrUsed.Note = "Generate from Recon with status Continue with consume method not changed";
            mrUsed.IsNotConsume = true;
            mrUsed.Save();

            var newMedRec = new MedicationReceive
            {
                RegistrationNo = mrSource.RegistrationNo,
                MedicationReceiveNo = NewMedicationReceiveNo(),
                BalanceQty = mrSource.BalanceRealQty,
                BalanceRealQty = mrSource.BalanceRealQty,
                ReceiveDateTime = DateTime.Now,
                StartDateTime = DateTime.Now,
                ItemID = mrSource.ItemID,
                ItemDescription = mrSource.ItemDescription,
                ReceiveQty = mrSource.BalanceRealQty,

                ConsumeQty = Convert.ToDecimal(new Temiang.Avicenna.BusinessObject.Common.Fraction(newConsumeQty)),
                ConsumeQtyInString = newConsumeQty,
                SRConsumeUnit = newSRConsumeUnit,
                SRConsumeMethod = newSRConsumeMethod,
                SRMedicationConsume = newSRMedicationConsume,

                // Tukar posisi krn ternyata mrSource.MedicationReceiveNo.ToString() bisa lebih dari 3 karakter (Handono 230906)
                //RefTransactionNo = "CC", // From Change Consume Method
                //RefSequenceNo = mrSource.MedicationReceiveNo.ToString(),

                RefTransactionNo = mrSource.MedicationReceiveNo.ToString(),
                RefSequenceNo = "CC", // From Change Consume Method

                IsVoid = false,
                IsContinue = true
            };
            newMedRec.Save();

            mrSource.IsContinue = false; // Supaya tidak lanjut ke recon selanjutnya
            mrSource.Save();
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




        #region drugName
        private string DrugName(string presNo, string seqNo, string itemdesc)
        {

            if (!string.IsNullOrEmpty(presNo))
            {
                var prescItem = new TransPrescriptionItem();
                if (prescItem.LoadByPrimaryKey(presNo, seqNo))
                {
                    if (prescItem.IsCompound ?? false)
                    {
                        return PrescriptionDrugNameCompound(prescItem);
                    }
                    else
                    {
                        return string.Format("{0} {1}", ItemName(prescItem), prescItem.Notes);
                    }
                }
            }

            return itemdesc;
        }

        private string PrescriptionDrugNameCompound(TransPrescriptionItem prescItem)
        {
            // Obat Racikan
            var sbItem = new StringBuilder();

            var emb = new Embalace();
            emb.LoadByPrimaryKey(prescItem.EmbalaceID);
            sbItem.AppendFormat("<span>{0} {1}</span>",
                ItemName(prescItem), prescItem.Notes);

            // Detil racikan
            var coll = new TransPrescriptionItemCollection();
            coll.Query.Where(coll.Query.PrescriptionNo == prescItem.PrescriptionNo, coll.Query.ParentNo == prescItem.SequenceNo);
            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
            coll.LoadAll();

            foreach (var entChild in coll)
            {
                sbItem.AppendFormat("<br/><span style=\"padding-left:10px;\">• {0}</span>", ItemName(entChild));
            }
            return sbItem.ToString();
        }
        private static string ItemName(TransPrescriptionItem prescItem)
        {
            var item = new Item();
            string itemID;
            if (prescItem.ItemInterventionID != null && !string.IsNullOrEmpty(prescItem.ItemInterventionID.ToString()) && !prescItem.ItemID.Equals(prescItem.ItemInterventionID))
            {
                itemID = prescItem.ItemInterventionID;
            }
            else
            {
                itemID = prescItem.ItemID;
            }

            item.LoadByPrimaryKey(itemID);
            return item.ItemName;
        }

        #endregion

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args);
            //if (args.IsCancel == false)
            //{
            //    // Popup Sign Entry
            //    ClientScript.RegisterStartupScript(this.Page.GetType(), "showsign", "Sys.Application.add_load(ShowSignEntry);", true);
            //}
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            //if (programID == "XML.PC.0005")
            //{
            //    printJobParameters.AddNew("p_RegistrationNo", RegistrationNo);
            //}
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if ("TRF".Equals(ReconType) && string.IsNullOrWhiteSpace(cboPatientTransfer.Value))
            {
                args.IsCancel = true;
                args.MessageText = "Please select transfer patient first";
                cboPatientTransfer.Focus();
            }
        }

        protected override void OnMenuEditClick()
        {
            if (string.IsNullOrWhiteSpace(hdnReconSeqNo.Value) || "0".Equals(hdnReconSeqNo.Value))
            {
                lblCreateByUserName.Text = AppSession.UserLogin.UserName;
                lblCreateDateTime.Text = DateTime.Now.ToString(AppConstant.DisplayFormat.DateShortMonth);
                chkIsFinish.Checked = false;
                lblBodyWeight.Text = VitalSign.LastVitalSignWithUnit(MergeRegistrations, VitalSign.VitalSignEnum.BodyWeight, DateTime.Now);
            }
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            base.OnInitializeAjaxManagerSettingsCollection(ajax);
            ajax.AddAjaxSetting(grdMedicationReceive, grdMedicationReceive);
            ajax.AddAjaxSetting(grdStoppedMed, grdStoppedMed);

            if (ReconType == "TRF")
            {
                ajax.AddAjaxSetting(cboPatientTransfer, grdStoppedMed);
                ajax.AddAjaxSetting(cboPatientTransfer, grdMedicationReceive);
                ajax.AddAjaxSetting(cboPatientTransfer, hdnReconSeqNo);
                ajax.AddAjaxSetting(cboPatientTransfer, lblToServiceUnit);
                ajax.AddAjaxSetting(cboPatientTransfer, lblTransferDate);

                ajax.AddAjaxSetting(cboPatientTransfer, lblBodyWeight);
                ajax.AddAjaxSetting(cboPatientTransfer, lblCreateByUserName);
                ajax.AddAjaxSetting(cboPatientTransfer, lblCreateDateTime);
                ajax.AddAjaxSetting(cboPatientTransfer, chkIsFinish);
            }
        }
        #endregion

        #region Medication Reconciliaion
        protected void grdMedicationReceive_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grid = ((RadGrid)source);
            grid.DataSource = MedicationReconDataSource(grid.ID);
        }


        private DataTable MedicationReconDataSource(string forGridId)
        {

            if (Page.IsPostBack && Session["dtbrecon_" + forGridId] != null && RegistrationNo.Equals(Session["reconregno"]) && ReconType.Equals(Session["recontype"]))
                return (DataTable)Session["dtbrecon_" + forGridId];

            var query = new MedicationReceiveQuery("a");
            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var patrec = new MedicationReceiveFromPatientQuery("b");
            if (ReconType.Equals("ADM")) // hanya obat dari luar RS saja yg perlu direcon Admisi
                query.InnerJoin(patrec).On(query.MedicationReceiveNo == patrec.MedicationReceiveNo);
            else
                query.LeftJoin(patrec).On(query.MedicationReceiveNo == patrec.MedicationReceiveNo);

            query.Select(query, patrec.Condition, patrec.ExpireDate,
                patrec.ApprovedByParamedicID, patrec.LastConsumeDateTime,
                cm.SRConsumeMethodName,
                "<CONVERT(BIT,1) as IsMenuReconVisible>");

            switch (ReconType)
            {
                case "ADM": // Recon obat patient baru masuk
                    query.Where(query.Or(query.RefTransactionNo.IsNull(), query.RefTransactionNo == string.Empty)); // Hanya obat yg dibawa pasien atau bukan hasil generate perubahan consume method
                    query.Select("<'' as LastPrescriptionNo>");
                    break;
                case "TRF":
                case "DCG":
                    // Recon obat transfer dan  patient pulang
                    // Hanya yg sudah di recon admisi dan di confirm oleh dokter
                    // Last Recon Status

                    var reconAdm = new MedicationRecon();
                    reconAdm.Query.Where(reconAdm.Query.RegistrationNo == RegistrationNo, reconAdm.Query.ReconType == "ADM");
                    reconAdm.Query.es.Top = 1;
                    reconAdm.Query.OrderBy(reconAdm.Query.ReconSeqNo.Descending);
                    if (reconAdm.Query.Load())
                    {
                        var reconAdmLine = new MedicationReconLineQuery("mrl");
                        reconAdmLine.Select(reconAdmLine.MedicationReceiveNo);
                        reconAdmLine.Where(reconAdmLine.RegistrationNo == RegistrationNo,
                        reconAdmLine.ReconSeqNo == reconAdm.ReconSeqNo,
                        reconAdmLine.MedicationReceiveNo == query.MedicationReceiveNo,
                        reconAdmLine.IsApprove == true
                        );

                        query.Where(query.Or(query.RefTransactionNo.IsNotNull(), query.And(query.MedicationReceiveNo.In(reconAdmLine))));
                    }


                    ////if ("DCG".Equals(ReconType)) // Discharge Recon
                    ////{
                    ////// Recon Discharge sumber list obat nya dari :
                    //////      - x obat yg 24 jam terakhir dikonsumsi->diganti obat tidak stop(Kardex version)(Balance 0 tetap muncul untuk jaga2 dokter terlewat buat rsep baru)
                    //////      - x Prescription baru Tipe Home Presc
                    //////      - V Add Obat yg di continue lagi(ada semacam menu untuk continue obat yg stop)

                    //////// Filter last realization (24H)
                    //////var used = new MedicationReceiveUsedQuery("usd");
                    //////used.Select(used.MedicationReceiveNo);
                    //////used.Where(used.MedicationReceiveNo == query.MedicationReceiveNo, used.RealizedDateTime >= DateTime.Now.AddHours(-24));

                    //////var homPresc = new TransPrescriptionItemQuery("tpi");
                    //////var tp = new TransPrescriptionQuery("tp");
                    //////homPresc.InnerJoin(tp).On(homPresc.PrescriptionNo == tp.PrescriptionNo);
                    //////homPresc.Where(tp.IsApproval == true, homPresc.IsVoid != true, tp.IsForTakeItHome == true);
                    //////homPresc.Select(homPresc.ItemID, homPresc.ItemInterventionID, homPresc.SRConsumeMethod, homPresc.ConsumeQty, homPresc.SRConsumeUnit);
                    //////var dtbHomPresc = homPresc.LoadDataTable();
                    //////query.Where(query.Or(query.IsBroughtHome == true, query.MedicationReceiveNo.In(used)));
                    ////}

                    var prescOrderDateFrom = DateTime.Now.AddHours(-24);
                    var prescOrderDateTo = DateTime.Now;
                    if ("TRF".Equals(ReconType))
                    {
                        //untuk pasien transfer antar ruangan, diambil data obat dari ruangan sebelumnya  (Handono 231204 req by RSI)
                        var transferNo = cboPatientTransfer.Value == "FROMREG" ? "" : cboPatientTransfer.Value;

                        prescOrderDateFrom = RegistrationCurrent.RegistrationDate.Value; // Biar kosong
                        var ptrfTo = new PatientTransferHistory();
                        if (ptrfTo.LoadByPrimaryKey(RegistrationNo, transferNo))
                        {
                            var dt = ptrfTo.DateOfEntry.Value;
                            var tms = ptrfTo.TimeOfEntry.Split(':');
                            prescOrderDateTo = new DateTime(dt.Year, dt.Month, dt.Day, tms[0].ToInt(), tms[1].ToInt(), 0);

                            //// prescOrderDateFrom, Datetime  ServiceUnit
                            //var ptrfFrom = new PatientTransferHistory();
                            //ptrfFrom.Query.Where(ptrfFrom.Query.RegistrationNo == RegistrationNo, ptrfFrom.Query.ServiceUnitID != ptrfTo.ServiceUnitID);
                            //ptrfFrom.Query.OrderBy(ptrfFrom.Query.TransferNo.Ascending);
                            //ptrfFrom.Query.es.Top = 1;
                            //if (ptrfFrom.Query.Load())
                            //{
                            //    dt = ptrfFrom.DateOfEntry.Value;
                            //    tms = ptrfFrom.TimeOfEntry.Split(':');
                            //    prescOrderDateTo = new DateTime(dt.Year, dt.Month, dt.Day, tms[0].ToInt(), tms[1].ToInt(), 0);
                            //}
                        }
                        else
                            prescOrderDateTo = RegistrationCurrent.RegistrationDate.Value; // Biar kosong
                    }
                    else
                    {
                        // Recon discharge semua obat yang diorder  < 24 jam saat pasien pulang (Handono 231204 req by RSI)
                        if (!string.IsNullOrWhiteSpace(hdnReconSeqNo.Value) && hdnReconSeqNo.Value != "0")
                        {
                            var recon = new MedicationRecon();
                            if (recon.LoadByPrimaryKey(RegistrationNo, hdnReconSeqNo.Value.ToInt()))
                                prescOrderDateFrom = recon.CreateDateTime.Value.AddHours(-24);
                        }
                    }

                    // Subquery cek history order
                    var prescLess24 = new MedicationReceivePrescLogQuery("pl");
                    prescLess24.Select(prescLess24.MedicationReceiveNo);
                    prescLess24.es.Top = 1;
                    prescLess24.Where(prescLess24.MedicationReceiveNo == query.MedicationReceiveNo, prescLess24.CreatedDateTime >= prescOrderDateFrom, prescLess24.CreatedDateTime < prescOrderDateTo);

                    // Pakai sebagai filter
                    query.Where(query.MedicationReceiveNo.In(prescLess24));


                    if (forGridId.Equals(grdMedicationReceive.ID))
                        query.Where(query.Or(query.IsContinue == true, query.IsContinue.IsNull()));
                    else
                        query.Where(query.IsContinue == false);


                    // LastPrescriptionNo
                    var lsatPresc = new MedicationReceivePrescLogQuery("lp");
                    lsatPresc.Where(lsatPresc.MedicationReceiveNo == query.MedicationReceiveNo);
                    lsatPresc.Select(lsatPresc.PrescriptionNo);
                    lsatPresc.OrderBy(lsatPresc.CreatedDateTime.Descending);
                    lsatPresc.es.Top = 1;

                    query.Select(lsatPresc.As("LastPrescriptionNo"));

                    break;
            }

            if ("TRF".Equals(ReconType))
            {
                if ("FROMREG".Equals(cboPatientTransfer.Value)) // Transfer dari RJ -> RI
                    query.Where(query.RegistrationNo == FromRegistrationNo);
                else
                    query.Where(query.RegistrationNo.In(MergeRegistrations));
            }
            else
                query.Where(query.RegistrationNo.In(MergeRegistrations));

            query.OrderBy(query.MedicationReceiveNo.Descending);

            var dtb = query.LoadDataTable();

            // Add Information
            dtb.Columns.Add("ReconStatusName", typeof(string));
            dtb.Columns.Add("ReconStatus", typeof(string));
            dtb.Columns.Add("NewConsumeMethodName", typeof(string));
            dtb.Columns.Add("NewSRConsumeMethod", typeof(string));
            dtb.Columns.Add("NewConsumeQty", typeof(string));
            dtb.Columns.Add("NewSRConsumeUnit", typeof(string));
            dtb.Columns.Add("NewSRMedicationConsume", typeof(string));
            dtb.Columns.Add("IsApprove", typeof(bool));
            dtb.Columns.Add("ApproveDateTime", typeof(DateTime));
            dtb.Columns.Add("ApproveByParamedicID", typeof(string));

            if (dtb.Rows.Count > 0)
            {
                // Set Recon Status
                dtb.PrimaryKey = new DataColumn[] { dtb.Columns["MedicationReceiveNo"] };

                if (!string.IsNullOrEmpty(hdnReconSeqNo.Value) && hdnReconSeqNo.Value != "0")
                {
                    // History Recon
                    var reconLine = new MedicationReconLineQuery("mrcnl");
                    var newcm = new ConsumeMethodQuery("ncm");
                    reconLine.LeftJoin(newcm).On(reconLine.SRConsumeMethod == newcm.SRConsumeMethod);
                    reconLine.Select(reconLine, newcm.SRConsumeMethodName);
                    reconLine.Where(reconLine.RegistrationNo == RegistrationNo, reconLine.ReconSeqNo == hdnReconSeqNo.Value);

                    if (!"ADM".Equals(ReconType)) // Recon Admisi belum diset status continue nya
                    {
                        var medrec = new MedicationReceiveQuery("mr");
                        reconLine.InnerJoin(medrec).On(reconLine.MedicationReceiveNo == medrec.MedicationReceiveNo);

                        if (forGridId.Equals(grdMedicationReceive.ID))
                            reconLine.Where(query.Or(medrec.IsContinue == true, medrec.IsContinue.IsNull()));
                        else
                            reconLine.Where(medrec.IsContinue == false);
                    }

                    var dtbReconLine = reconLine.LoadDataTable();

                    foreach (DataRow rowRecon in dtbReconLine.Rows)
                    {
                        var row = dtb.Rows.Find(rowRecon["MedicationReceiveNo"]);
                        if (row == null) continue;

                        if ("ADM".Equals(ReconType))
                        {
                            row["IsMenuReconVisible"] = rowRecon["IsApprove"] == DBNull.Value; // Bisa diedit selama belum diconfirm approve / unapprove oleh dokter
                        }

                        if (rowRecon["ReconStatus"] != DBNull.Value)
                        {
                            row["ReconStatusName"] = ReconStatus(rowRecon["ReconStatus"].ToString());
                            row["ReconStatus"] = rowRecon["ReconStatus"];
                        }

                        if (rowRecon["SRConsumeMethodName"] != DBNull.Value)
                        {
                            row["NewConsumeMethodName"] = string.Format("{0} @{1} {2}", rowRecon["SRConsumeMethodName"], rowRecon["ConsumeQty"], rowRecon["SRConsumeUnit"]); ;
                            row["NewSRConsumeMethod"] = rowRecon["SRConsumeMethod"];
                            row["NewConsumeQty"] = rowRecon["ConsumeQty"];
                            row["NewSRConsumeMethod"] = rowRecon["SRConsumeUnit"];
                        }

                        row["IsApprove"] = rowRecon["IsApprove"];
                        row["ApproveDateTime"] = rowRecon["ApproveDateTime"];
                    }
                }
            }
            Session["dtbrecon_" + forGridId] = dtb;
            Session["reconregno"] = RegistrationNo;
            Session["recontype"] = ReconType;

            return (DataTable)Session["dtbrecon_" + forGridId];
        }

        private void PopulateReconInf(MedicationRecon recon)
        {
            hdnReconSeqNo.Value = recon.ReconSeqNo.ToString();
            var usr = new AppUser();
            usr.LoadByPrimaryKey(recon.CreateByUserID);
            lblCreateByUserName.Text = usr.UserName;

            lblCreateDateTime.Text = recon.CreateDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonth);
            chkIsFinish.Checked = recon.IsFinish ?? false;

            lblBodyWeight.Text = string.Format("{0:N2} KG", recon.BodyWeight);

            PopulateSignImage(rbiSign, hdnSign, recon.SignImg);
            PopulateSignImage(rbiPatSign, hdnPatSign, recon.PatientSignImg);

            if (divParSign.Visible)
                PopulateSignImage(rbiParSign, hdnParSign, recon.ParamedicSignImg);


            if (recon.ReconType == "TRF")
            {
                lblToServiceUnit.Text = String.Empty;
                lblTransferDate.Text = String.Empty;

                if (!string.IsNullOrWhiteSpace(recon.TransferNo))
                {
                    if (recon.TransferNo == "FROMREG")
                    {
                        var toSu = new ServiceUnit();
                        toSu.LoadByPrimaryKey(RegistrationCurrent.ServiceUnitID);
                        lblToServiceUnit.Text = toSu.ServiceUnitName;
                        lblTransferDate.Text = RegistrationCurrent.RegistrationDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth);
                    }
                    else
                    {
                        var pt = new PatientTransfer();
                        if (pt.LoadByPrimaryKey(recon.TransferNo))
                        {
                            var su = new ServiceUnit();
                            su.LoadByPrimaryKey(pt.ToServiceUnitID);
                            lblToServiceUnit.Text = su.ServiceUnitName;

                            lblTransferDate.Text = pt.TransferDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth);
                        }
                    }
                }
            }
        }

        private string ReconStatus(string statCode)
        {
            switch (statCode)
            {
                case "CN":
                    return "Continue with consume method not changed";
                case "CC":
                    return "Continue with consume method changed";
                case "ST":
                    return "Stop";
                case "NT":
                    return "New Therapies";
            }
            return string.Empty;
        }
        protected void grdMedicationReceive_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            //"Continue with consume method not changed", "CN"
            //"Continue with consume method changed", "CC"
            //"Stop", "ST"
            //"New Therapies", "NT"

            if (e.Item is GridDataItem)
            {
                var color = "black";
                GridDataItem item = (GridDataItem)e.Item;
                if (true.Equals(item.GetDataKeyValue("IsVoid")))
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    color = "gray";
                }
                else
                {
                    switch (item.GetDataKeyValue("ReconStatus"))
                    {
                        case "CC":
                            color = "blue";
                            break;
                        case "CN":
                            color = "green";
                            break;
                        case "ST":
                            color = "gray";
                            break;
                        case "NT":
                            color = "orange";
                            break;
                    }
                }
                item.Style.Add(HtmlTextWriterStyle.Color, color);
            }
        }

        protected void grdMedicationReceive_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Approve":
                case "UnApprove":
                    {
                        // ---- Hanya untuk Recon Admisi ----///
                        if ("ADM".Equals(ReconType))
                        {
                            var isApprove = e.CommandName.Equals("Approve");
                            var medicationReceiveNo = Convert.ToDecimal(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MedicationReceiveNo"]).ToInt();

                            //MedicationRecon
                            var mr = new MedicationRecon();
                            mr.Query.Where(mr.Query.RegistrationNo == RegistrationNo, mr.Query.ReconType == ReconType);
                            mr.Query.OrderBy(mr.Query.ReconSeqNo.Descending);
                            mr.Query.Load();

                            var mrl = new MedicationReconLine();
                            mrl.Query.Where(mrl.Query.RegistrationNo == RegistrationNo, mrl.Query.ReconSeqNo == mr.ReconSeqNo, mrl.Query.MedicationReceiveNo == medicationReceiveNo);
                            mrl.Query.Load();
                            mrl.IsApprove = isApprove;
                            if (isApprove)
                            {
                                mrl.ApproveByParamedicID = AppSession.UserLogin.ParamedicID;
                                mrl.ApproveDateTime = DateTime.Now;
                            }
                            else
                            {
                                mrl.str.ApproveByParamedicID = String.Empty; //set null
                                mrl.str.ApproveDateTime = String.Empty;
                            }
                            mrl.Save();


                            var med = new MedicationReceive();
                            if (med.LoadByPrimaryKey(medicationReceiveNo))
                            {
                                // Continue stat
                                med.IsContinue = isApprove && !"ST".Equals(mrl.ReconStatus);
                                med.IsClosed = !isApprove || ("ST".Equals(mrl.ReconStatus));
                                med.Save();

                                // Tambah history stop
                                if ("ST".Equals(mrl.ReconStatus)) // Stop
                                {
                                    var stat = new MedicationReceiveStatus();
                                    stat.MedicationReceiveNo = medicationReceiveNo;
                                    stat.StatusDateTime = DateTime.Now;
                                    stat.IsMedicationStop = !isApprove;
                                    stat.MedicationReason = String.Concat("Recon admision confirm", (isApprove ? String.Empty : " un"), "approve by paramedic");
                                    stat.Save();
                                }

                                // Efek ganti consume method Recon Adm saat di approve oleh dokter, transfer dan discharge saat save
                                if ("ADM".Equals(ReconType))
                                    CreateNewMedicationReceive(med, mrl.ReconStatus, mrl.SRConsumeMethod, mrl.ConsumeQty, mrl.SRConsumeUnit, mrl.SRMedicationConsume);

                                // Update datasource grid
                                var grid = (RadGrid)source;
                                var medicationReconDataSource = MedicationReconDataSource(grid.ID);
                                var row = medicationReconDataSource.Rows.Find(medicationReceiveNo);
                                row["IsApprove"] = isApprove;
                                if (isApprove)
                                {
                                    row["ApproveByParamedicID"] = AppSession.UserLogin.ParamedicID;
                                    row["ApproveDateTime"] = DateTime.Now;
                                }
                                else
                                {
                                    row["ApproveByParamedicID"] = String.Empty;
                                    row["ApproveDateTime"] = String.Empty;
                                }

                                grid.Rebind();
                            }
                        }
                        break;
                    }
            }


        }



        #endregion


        #region Additional Toolbar menu item
        public override string OnGetAdditionalMenuScript()
        {
            var script = @"
case ""drugfrompatient"":
    openMedicationReceiveFromPatient();
    args.set_cancel(true);
    break; 
case ""education"":
    openEducation();
    args.set_cancel(true);
    break;
";

            return script;
        }
        protected override void OnToolBarMenuDataAdditionalButtonClick(ValidateArgs args, string value)
        {
        }
        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if ("ADM".Equals(ReconType))
            {
                // Disable tombol status
                ToolBarMenuData.FindItemByValue("drugfrompatient").Enabled = !ToolBarMenuSave.Visible;



                // Cek mdp.IsManagedByPatient 
                var mdpq = new MedicationReceiveFromPatientQuery("mdp");
                var mrq = new MedicationReceiveQuery("mr");
                mdpq.InnerJoin(mrq).On(mrq.MedicationReceiveNo == mdpq.MedicationReceiveNo);
                mdpq.Where(mrq.RegistrationNo == RegistrationNo, mdpq.IsManagedByPatient == true);
                mdpq.es.Top = 1;
                mdpq.Select(mdpq.MedicationReceiveNo);

                var mdp = new MedicationReceiveFromPatient();
                var isExist = mdp.Load(mdpq);

                var tbEdu = ToolBarMenuData.FindItemByValue("education");
                tbEdu.Enabled = !ToolBarMenuSave.Visible && isExist;

            }
        }

        protected void cboPatientTransfer_SelectedIndexChanged(object sender, RadMultiColumnComboBoxSelectedIndexChangedEventArgs e)
        {
            var mr = new MedicationRecon();
            mr.Query.es.Top = 1;
            // Filter regnonya krn transfer dari RJ ke RI memakai TransferNo yg sama yaitu FROMREG
            mr.Query.Where(mr.Query.RegistrationNo == RegistrationNo, mr.Query.TransferNo == cboPatientTransfer.Value);
            if (mr.Query.Load())
                PopulateReconInf(mr);
            else
            {
                hdnReconSeqNo.Value = "0";
                lblCreateDateTime.Text = string.Empty;
                lblCreateByUserName.Text = string.Empty;
                chkIsFinish.Checked = false;
            }

            //Override
            lblToServiceUnit.Text = String.Empty;
            lblTransferDate.Text = String.Empty;

            if (cboPatientTransfer.Value == "FROMREG")
            {
                var toSu = new ServiceUnit();
                toSu.LoadByPrimaryKey(RegistrationCurrent.ServiceUnitID);
                lblToServiceUnit.Text = toSu.ServiceUnitName;
                lblTransferDate.Text = RegistrationCurrent.RegistrationDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth);
            }
            else
            {
                var pt = new PatientTransfer();
                if (pt.LoadByPrimaryKey(cboPatientTransfer.Value))
                {
                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(pt.ToServiceUnitID);
                    lblToServiceUnit.Text = su.ServiceUnitName;

                    lblTransferDate.Text = pt.TransferDate.Value.ToString(AppConstant.DisplayFormat.DateShortMonth);
                }
            }

            // Refresh list obat
            Session.Remove("dtbrecon_" + grdMedicationReceive);
            grdMedicationReceive.DataSource = null;
            grdMedicationReceive.Rebind();

            Session.Remove("dtbrecon_" + grdStoppedMed);
            grdStoppedMed.DataSource = null;
            grdStoppedMed.Rebind();
        }


        private void PopulateSignImage(RadBinaryImage rbImage, HiddenField hdnImage, Byte[] val)
        {
            rbImage.DataValue = val;
            if (val == null)
                hdnImage.Value = String.Empty;
            else
            {
                var mstream = new System.IO.MemoryStream(val);
                var img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                var imgHelper = new ImageHelper();
                hdnImage.Value = imgHelper.ToBase64String(img.Image, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

    }
}
