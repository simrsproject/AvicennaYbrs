using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.Design;
using Telerik.Web.UI;
using Telerik.Web.UI.Editor.DialogControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.MedicationStatus
{
    public partial class MedicationStatusCtl : System.Web.UI.UserControl
    {
        private bool _isCurrentDayFinish = false;

        //private DateTime _currentDate = (new DateTime()).NowAtSqlServer();

        //private DateTime SetupStartDate
        //{
        //    get
        //    {
        //        if (ViewState["stdt"] == null)
        //        {
        //            ViewState["stdt"] = _currentDate;
        //        }
        //        return (DateTime)ViewState["stdt"];
        //    }
        //    set { ViewState["stdt"] = value; }
        //}

        private DateTime SetupStartDate
        {
            get
            {
                if (ViewState["stdt"] == null)
                {
                    ViewState["stdt"] = CurrentDate;
                }
                return (DateTime)ViewState["stdt"];
            }
            set { ViewState["stdt"] = value; }
        }

        protected string ProgramID
        {
            get
            {
                if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                    return AppConstant.Program.PharmaceuticalCare;
                else
                    return AppConstant.Program.ElectronicMedicalRecord;
            }
        }

        public string MedicationStep
        {
            get
            {
                return (string)ViewState["step"];
            }
            set { ViewState["step"] = value; }
        }
        public DateTime CurrentDate
        {
            get
            {
                return (DateTime)ViewState["cdate"];
            }
            set { ViewState["cdate"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Delete detil via linkbutton tidak bisa menjalankan even grdMedicationStatus_DeleteCommand
                if (Request.Form["__EVENTTARGET"].Contains("grdMedicationStatus"))
                {
                    var arg = Request.Form["__EVENTARGUMENT"];
                    if (!string.IsNullOrWhiteSpace(arg) && arg.Contains("del|"))
                    {
                        var args = arg.Split('|');
                        // Delete setup

                        var nmd = new MedicationReceiveUsed();
                        if (nmd.LoadByPrimaryKey(args[2].ToInt(), args[3].ToInt()))
                        {
                            switch (args[1])
                            {
                                case "S":
                                    {
                                        //Kembalikan BalanceQty
                                        var mrec = new MedicationReceive();
                                        mrec.LoadByPrimaryKey(args[2].ToInt());
                                        mrec.BalanceQty = mrec.BalanceQty + nmd.Qty;
                                        mrec.Save();

                                        nmd.MarkAsDeleted();
                                        break;
                                    }
                                case "H":
                                    {
                                        nmd.str.HandoversByUserID = string.Empty;
                                        nmd.str.HandoversToUserID = string.Empty;
                                        nmd.str.HandoversDateTime = string.Empty;
                                        break;
                                    }
                                case "V":
                                    {
                                        nmd.str.VerificationByUserID = string.Empty;
                                        nmd.str.VerificationDateTime = string.Empty;
                                        break;
                                    }
                                case "R":
                                    {
                                        nmd.str.RealizedByUserID = string.Empty;
                                        nmd.str.RealizedDateTime = string.Empty;

                                        //Kembalikan BalanceRealQty
                                        var mrec = new MedicationReceive();
                                        mrec.LoadByPrimaryKey(args[2].ToInt());
                                        mrec.BalanceRealQty = mrec.BalanceRealQty + nmd.Qty;
                                        mrec.Save();
                                        break;
                                    }
                            }
                            nmd.Save();
                            grdMedicationStatus.MasterTableView.DetailTables[0].Rebind();
                        }
                    }
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #region MedicationReceive
        protected void grdMedicationStatus_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "Rebind")
            {
            }
        }



        //protected void grdMedicationStatus_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //var item = e.Item as GridDataItem;
        //if (item == null) return;

        //// Delete detail
        //if (e.Item.OwnerTableView.Name.Equals("grdMedicationStatusUsed"))
        //{
        //    var medicationReceiveNo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["MedicationReceiveNo"].ToInt();
        //    var sequenceNo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"].ToInt();

        //    using (var tr = new esTransactionScope())
        //    {
        //        var nmd = new MedicationReceiveUsed();
        //        if (nmd.LoadByPrimaryKey(medicationReceiveNo, sequenceNo))
        //        {
        //            switch (MedicationStep)
        //            {
        //                case "S":
        //                    nmd.MarkAsDeleted();
        //                    break;
        //                case "V":
        //                    nmd.str.VerificationByUserID = string.Empty;
        //                    nmd.str.VerificationDateTime = string.Empty;
        //                    break;
        //                case "R":
        //                    nmd.str.RealizedByUserID = string.Empty;
        //                    nmd.str.RealizedDateTime = string.Empty;
        //                    break;
        //            }
        //            nmd.Save();
        //        }
        //        tr.Complete();
        //    }

        //    grdMedicationStatus.MasterTableView.DetailTables[0].Rebind();
        //}
        //}

        public void Rebind(List<string> mergeRegistrations, string serviceUnitID, bool isIncludeFinished, bool isIncludeStopped, DateTime? setupStartDate, string drugSource)
        {
            // drugSource -> string.empty:All, P:From Patient, H: From Hospital
            if (setupStartDate == null)
                setupStartDate = CurrentDate;

            // Header Text
            if (CurrentDate == setupStartDate)
            {
                grdMedicationStatus.Columns.FindByUniqueName("Day0").HeaderText = string.Format("This Day ({0})",
                    SetupStartDate.ToString(AppConstant.DisplayFormat.Date));
                grdMedicationStatus.Columns.FindByUniqueName("Day1").HeaderText = string.Format("This Day+1 ({0})",
                    SetupStartDate.AddDays(1).ToString(AppConstant.DisplayFormat.Date));
                grdMedicationStatus.Columns.FindByUniqueName("Day2").HeaderText = string.Format("This Day+2 ({0})",
                    SetupStartDate.AddDays(2).ToString(AppConstant.DisplayFormat.Date));
            }
            else
            {
                grdMedicationStatus.Columns.FindByUniqueName("Day0").HeaderText = string.Format("({0})",
                    SetupStartDate.ToString(AppConstant.DisplayFormat.Date));
                grdMedicationStatus.Columns.FindByUniqueName("Day1").HeaderText = string.Format("Date+1 ({0})",
                    SetupStartDate.AddDays(1).ToString(AppConstant.DisplayFormat.Date));
                grdMedicationStatus.Columns.FindByUniqueName("Day2").HeaderText = string.Format("Date+2 ({0})",
                    SetupStartDate.AddDays(2).ToString(AppConstant.DisplayFormat.Date));
            }

            SetupStartDate = (DateTime)setupStartDate;
            grdMedicationStatus.DataSource = MedicationReceiveDataTable(mergeRegistrations, serviceUnitID, isIncludeFinished, isIncludeStopped, drugSource);
        }
        private DataTable MedicationReceiveDataTable(List<string> mergeRegistrations, string serviceUnitID, bool isIncludeFinished, bool isIncludeStopped, string drugSource)
        {
            // serviceUnitID -> For filter mapping ItemID in main location
            // drugSource -> string.empty:All, P:From Patient, H: From Hospital

            var dtbMedRec = new DataTable();
            using (var tr = new esTransactionScope())
            {
                var query = new MedicationReceiveQuery("a");

                var cm = new ConsumeMethodQuery("cm");
                query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

                var mc = new AppStandardReferenceItemQuery("mc");
                query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                                      mc.StandardReferenceID == "MedicationConsume");

                var tp = new TransPrescriptionQuery("tp");
                query.LeftJoin(tp).On(query.RefTransactionNo == tp.PrescriptionNo);

                var reg = new RegistrationQuery("reg");
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

                var ipm = new ItemProductMedicQuery("ipm");
                query.LeftJoin(ipm).On(query.ItemID == ipm.ItemID);

                query.Select
                (
                    query,
                    cm.SRConsumeMethodName,
                    reg.PatientID,
                    mc.ItemName.As("SRMedicationConsumeName"),
                    tp.RasproSeqNo.Coalesce("0").As("RasproSeqNo"),
                    ipm.IsAntibiotic.Coalesce("1").As("IsAntibiotic")
                );


                if (!string.IsNullOrEmpty(serviceUnitID))
                {
                    var locID = ServiceUnit.MainLocationID(serviceUnitID);
                    var locItem = new ItemBalanceQuery("li");
                    query.InnerJoin(locItem).On(query.ItemID == locItem.ItemID & locItem.LocationID == locID);
                }

                if (!string.IsNullOrEmpty(drugSource))
                {
                    if ("P".Equals(drugSource)) // From Patient
                        query.Where(query.Or(query.RefTransactionNo.IsNull(), query.RefTransactionNo == string.Empty));
                    else // From Hospital
                        query.Where(query.RefTransactionNo > string.Empty);
                }

                if (!isIncludeFinished)
                    query.Where(query.BalanceRealQty > 0); //Balance berdasarkan Receive - Realisasi

                if (!isIncludeStopped)
                    query.Where(query.IsContinue == true);

                // Obat yg dibawa oleh pasien hanya yg sudah recon admisi dan diconfirm approve oleh dokter yg bisa lanjut (2022 06 RSI)
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsUddSetupMustReconFirst))
                    query.Where(query.Or(query.RefTransactionNo.IsNotNull(), query.IsContinue == true));

                query.Where(query.RegistrationNo.In(mergeRegistrations), query.IsVoid != true);

                switch (MedicationStep)
                {

                    case "V":
                    case "H":
                        // Bisa Verifikasi dan Handovers jika sudah disetup (Handono 230906)
                        var setuped = new MedicationReceiveUsedQuery("stp");
                        setuped.Select(setuped.MedicationReceiveNo);
                        setuped.Where(setuped.MedicationReceiveNo == query.MedicationReceiveNo, setuped.SetupByUserID.IsNotNull(), setuped.SetupByUserID > string.Empty);
                        query.Where(query.MedicationReceiveNo.In(setuped));
                        break;
                    case "R":
                        var verified = new MedicationReceiveUsedQuery("ver");
                        verified.Select(verified.MedicationReceiveNo);
                        verified.Where(verified.MedicationReceiveNo == query.MedicationReceiveNo, verified.VerificationByUserID.IsNotNull(), verified.VerificationByUserID > string.Empty);
                        query.Where(query.MedicationReceiveNo.In(verified));
                        break;
                }

                query.OrderBy(query.ItemDescription.Ascending, query.ReceiveDateTime.Ascending);

                dtbMedRec = query.LoadDataTable();

                // Add raspro info
                dtbMedRec.Columns.Add("SRRaspro", typeof(string));

                // Add Entry Menu Day 1,2, & 3
                dtbMedRec.Columns.Add("ScheduleDay0", typeof(string));
                dtbMedRec.Columns.Add("ScheduleDay1", typeof(string));
                dtbMedRec.Columns.Add("ScheduleDay2", typeof(string));

                foreach (DataRow row in dtbMedRec.Rows)
                {
                    row["ItemDescription"] = row["ItemDescription"].ToString().Replace(Environment.NewLine, "<br>");

                    // AB Raspro Info
                    if (row["RasproSeqNo"].ToInt() > 0)
                    {
                        var rr = new RegistrationRaspro();
                        rr.LoadByPrimaryKey(row["RegistrationNo"].ToString(), row["RasproSeqNo"].ToInt());
                        row["SRRaspro"] = rr.SRRaspro;
                    }
                    else
                    {
                        row["RasproSeqNo"] = 0;
                        row["SRRaspro"] = string.Empty;
                    }

                    // Entry Menu Day 1,2, & 3
                    row["ScheduleDay0"] = MedicationScheduleHtml(row, 0);
                    row["ScheduleDay1"] = MedicationScheduleHtml(row, 1);
                    row["ScheduleDay2"] = MedicationScheduleHtml(row, 2);
                }

                dtbMedRec.Columns.Add("IsCurrentDayFinish", typeof(bool));
            }
            return dtbMedRec;
        }

        #endregion


        protected void grdMedicationStatus_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string medicationReceiveNo = dataItem.GetDataKeyValue("MedicationReceiveNo").ToString();

            if (e.DetailTableView.Name.Equals("grdMedicationStatusUsed"))
            {
                var query = new MedicationReceiveUsedQuery("a");

                var setup = new AppUserQuery("u1");
                query.LeftJoin(setup).On(query.SetupByUserID == setup.UserID);
                var verif = new AppUserQuery("u2");
                query.LeftJoin(verif).On(query.VerificationByUserID == verif.UserID);
                var realize = new AppUserQuery("u3");
                query.LeftJoin(realize).On(query.RealizedByUserID == realize.UserID);

                var hoBy = new AppUserQuery("hoby");
                query.LeftJoin(hoBy).On(query.HandoversByUserID == hoBy.UserID);

                var hoTo = new AppUserQuery("hoto");
                query.LeftJoin(hoTo).On(query.HandoversToUserID == hoTo.UserID);

                query.Select(query, setup.UserName.As("SetupByUserName"),
                    verif.UserName.As("VerificationByUserName"),
                    realize.UserName.As("RealizedByUserName"),
                    hoBy.UserName.As("HandoversByUserName"),
                    hoTo.UserName.As("HandoversToUserName")
                    );
                query.Where(query.MedicationReceiveNo == medicationReceiveNo); //, query.ScheduleDateTime.IsNotNull());
                query.OrderBy(query.ScheduleDateTime.Descending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }

        protected string MedicationScheduleHtml(object medrecno, object conMethod, object isContinue, object isVoid, object oBalanceQty,
            object oConsumeQty, int dayFromCurrentDay, string patientID, object oIsAntibioic, object startDateTime, object srMedicationConsume, object balanceQty)
        {
            return MedicationScheduleHtml(medrecno.ToInt(), conMethod.ToString(), true.Equals(isContinue), true.Equals(isVoid),
                oBalanceQty.ToDecimal(), oConsumeQty.ToDecimal(), dayFromCurrentDay.ToInt(), patientID, Convert.ToDateTime(startDateTime),
                srMedicationConsume == DBNull.Value ? string.Empty : srMedicationConsume.ToString(), true.Equals(oIsAntibioic),
                Convert.ToDecimal(balanceQty ?? 0));
        }

        private decimal BalanceQtyForSchedule(decimal receiveQty, decimal consumeQty, int medrecno, int dayFromCurrentDay, decimal iterationQty, DateTime startDateTime, ConsumeMethod cm)
        {
            decimal balanceQty = receiveQty;
            //var setupCount = 0;
            //var skippedSchedule = 0;
            //var additionalSetupCount = 0;

            // Hitung balance di awal tgl yg akan di setup 
            var qr = new MedicationReceiveUsedQuery();
            qr.Where(qr.MedicationReceiveNo == medrecno, qr.ScheduleDateTime < SetupStartDate.AddDays(dayFromCurrentDay));
            qr.Where(qr.Or(qr.IsReSchedule.IsNull(), qr.IsReSchedule == false), qr.Or(qr.IsVoidSchedule.IsNull(), qr.IsVoidSchedule == false));
            qr.Select(qr.Qty.Sum().As("Qty"));

            var dtb = qr.LoadDataTable();
            if (dtb.Rows[0] != null)
                balanceQty = receiveQty - dtb.Rows[0][0].ToDecimal(); // Initial Balance tgl yg diseting

            return balanceQty;

            //if (balanceQty == 0)
            //    return 0;

            //if (dayFromCurrentDay > 0)
            //{

            //    //// Ambil jumlah yg sudah disetup di hari sebelumnya 0 to dayFromCurrentDay
            //    //qr = new MedicationReceiveUsedQuery();
            //    //qr.Where(qr.MedicationReceiveNo == medrecno, qr.ScheduleDateTime >= SetupStartDate,
            //    //    qr.ScheduleDateTime < SetupStartDate.AddDays(dayFromCurrentDay), qr.IsAdditionalSchedule == false);
            //    //qr.Select(qr.ScheduleDateTime.Count().As("SetupCount"));
            //    //dtb = qr.LoadDataTable();
            //    //if (dtb.Rows[0] != null)
            //    //    setupCount = dtb.Rows[0][0].ToInt();

            //    //// Ambil jumlah setup Additional di hari 0 atau pertama
            //    //var additionalSetupCount = 0;
            //    //qr = new MedicationReceiveUsedQuery();
            //    //qr.Where(qr.MedicationReceiveNo == medrecno, qr.ScheduleDateTime >= SetupStartDate,
            //    //    qr.ScheduleDateTime >= startDateTime.Date, qr.ScheduleDateTime >= startDateTime.Date.AddDays(1), qr.IsAdditionalSchedule == true);
            //    //qr.Select(qr.ScheduleDateTime.Count().As("SetupCount"));
            //    //dtb = qr.LoadDataTable();
            //    //if (dtb.Rows[0] != null)
            //    //    additionalSetupCount = dtb.Rows[0][0].ToInt();


            //    // Ambil jumlah yg sudah disetup di tgl yg akan diseting
            //    qr = new MedicationReceiveUsedQuery();
            //    qr.Where(qr.MedicationReceiveNo == medrecno, qr.ScheduleDateTime >= SetupStartDate.AddDays(dayFromCurrentDay),
            //        qr.ScheduleDateTime < SetupStartDate.AddDays(dayFromCurrentDay+1));
            //    qr.Select(qr.ScheduleDateTime.Count().As("SetupCount"));
            //    dtb = qr.LoadDataTable();
            //    if (dtb.Rows[0] != null)
            //        setupCount = dtb.Rows[0][0].ToInt();

            //    //// Hitung schedule di hari pertama yg kurang dari startDateTime // Schedule bisa dimundurkan
            //    //if (cm.IterationQty != null || cm.IterationQty > 0)
            //    //{
            //    //    for (int i = 0; i < cm.IterationQty; i++)
            //    //    {
            //    //        var timeScheduleDisplay = cm.GetColumn(string.Format("Time{0:00}", i + 1)).ToString();
            //    //        if (string.IsNullOrEmpty(timeScheduleDisplay))
            //    //            timeScheduleDisplay = "00:00"; //default saja
            //    //        var timeScheduleDisplays = timeScheduleDisplay.Split(':');
            //    //        var scheduleDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day,
            //    //            timeScheduleDisplays[0].ToInt(), timeScheduleDisplays[1].ToInt(), 0);

            //    //        if (startDateTime > scheduleDateTime) skippedSchedule = skippedSchedule + 1;
            //    //    }

            //    //}

            //    skippedSchedule = skippedSchedule - additionalSetupCount;
            //}

            //// Jumlah setup seharusnya di hari sebelumnya
            //var setupScheduleCount = (iterationQty * dayFromCurrentDay);
            //var notYetSetup = setupScheduleCount - setupCount;

            //// Kurangi lagi balance dg schedule yg belum disetup jika dayFromCurrentDay>0
            //if (dayFromCurrentDay > 0)
            //    balanceQty = balanceQty - ((notYetSetup - skippedSchedule) * consumeQty);

            //return balanceQty;
        }

        #region Schedule Setup
        protected string MedicationScheduleSetupHtml(GridItem container)
        {
            int medrecno = Convert.ToInt32(DataBinder.Eval(container.DataItem, "MedicationReceiveNo"));
            var srConsumeMethod = Convert.ToString(DataBinder.Eval(container.DataItem, "SRConsumeMethod"));
            var patientID = Convert.ToString(DataBinder.Eval(container.DataItem, "PatientID"));
            var srMedicationConsume = Convert.ToString(DataBinder.Eval(container.DataItem, "SRMedicationConsume"));
            var consumeQty = Convert.ToDecimal(DataBinder.Eval(container.DataItem, "ConsumeQty"));
            return MedicationScheduleSetupHtml(medrecno, srConsumeMethod, srMedicationConsume, consumeQty);
        }
        private string MedicationScheduleSetupHtml(int medrecno, string srConsumeMethod, string srMedicationConsume, decimal consumeQty)
        {
            var scriptForScheduleDate = DateTime.Today;
            var strb = new StringBuilder();

            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(srConsumeMethod);

            var scheduleNo = 0;
            for (int i = 0; i < cm.IterationQty; i++)
            {
                var timeScheduleDisplay = "00:00";
                var timeScheduleDisplays = timeScheduleDisplay.Split(':');
                scheduleNo = i + 1;

                // Check custom schedule
                var qrSchedule = new MedicationScheduleQuery();
                qrSchedule.Where(qrSchedule.MedicationReceiveNo == medrecno, qrSchedule.ScheduleStartDate <= scriptForScheduleDate, qrSchedule.ScheduleNo == scheduleNo);
                qrSchedule.OrderBy(qrSchedule.ScheduleStartDate.Descending);
                qrSchedule.es.Top = 1;

                var histSch = new MedicationSchedule();
                if (histSch.Load(qrSchedule))
                {
                    // Jika ada history setup maka munculkan jam dari history
                    consumeQty = histSch.Qty ?? 0;
                    var scheduleTime = histSch.ScheduleTime.Value;

                    timeScheduleDisplay =
                        string.Format("{0:00}:{1:00}", scheduleTime.Hour, scheduleTime.Minute);
                    timeScheduleDisplays = timeScheduleDisplay.Split(':');
                }
                else
                {
                    // Ambil dari master ConsumeMethod
                    timeScheduleDisplay = cm.GetColumn(string.Format("Time{0:00}", scheduleNo)).ToString();
                    timeScheduleDisplays = timeScheduleDisplay.Split(':');

                    if (srMedicationConsume.Equals("AC"))
                    {
                        // Sebelum makan maka kurangi 30menit
                        timeScheduleDisplays = timeScheduleDisplay.Split(':');
                        var minute = (timeScheduleDisplays[0].ToInt() * 60) + timeScheduleDisplays[1].ToInt();
                        minute = minute - 30;
                        timeScheduleDisplay = string.Format("{0}:{1}",
                            string.Format("{0:00}", Math.Floor((decimal)(minute / 60))),
                            string.Format("{0:00}", minute % 60));
                        timeScheduleDisplays = timeScheduleDisplay.Split(':');
                    }
                }

                if (timeScheduleDisplays.Length < 2) continue;

                // Tampilkan menu entry schedule
                strb.AppendFormat(
    "&nbsp;<a  href=\"#\" onclick=\"javascript:entryMedicationScheduleSetup('{0}', '{1}', '{2}','{3}'); return false;\"><img style='vertical-align: text-bottom;' src=\"{4}/Images/Toolbar/edit16.png\"  alt=\"New\" />&nbsp;{3}({5})</a>&nbsp;|",
    medrecno, scriptForScheduleDate, scheduleNo, timeScheduleDisplay, Helper.UrlRoot(), consumeQty);

            }

            var retval = strb.ToString();
            return !string.IsNullOrEmpty(retval) ? retval.Substring(0, retval.Length - 2) : string.Empty;
        }

        #endregion
        protected string MedicationScheduleHtml(GridItem container, int dayFromCurrentDay)
        {
            return MedicationScheduleHtml(DataBinder.Eval(container.DataItem, "MedicationReceiveNo"),
                DataBinder.Eval(container.DataItem, "SRConsumeMethod"), DataBinder.Eval(container.DataItem, "IsContinue"),
                DataBinder.Eval(container.DataItem, "IsVoid"), DataBinder.Eval(container.DataItem, "ReceiveQty"),
                DataBinder.Eval(container.DataItem, "ConsumeQty"), dayFromCurrentDay, DataBinder.Eval(container.DataItem, "PatientID").ToString(),
                DataBinder.Eval(container.DataItem, "IsAntibiotic"), DataBinder.Eval(container.DataItem, "StartDateTime"),
                DataBinder.Eval(container.DataItem, "SRMedicationConsume"), DataBinder.Eval(container.DataItem, "BalanceQty"));
        }
        protected string MedicationScheduleHtml(DataRow row, int dayFromCurrentDay)
        {
            return MedicationScheduleHtml(row["MedicationReceiveNo"],
                row["SRConsumeMethod"], row["IsContinue"],
                row["IsVoid"], row["ReceiveQty"],
                row["ConsumeQty"], dayFromCurrentDay, row["PatientID"].ToString(),
                row["IsAntibiotic"], row["StartDateTime"],
                row["SRMedicationConsume"], row["BalanceQty"]);
        }

        private string MedicationScheduleHtml(int medrecno, string conMethod, bool isContinue, bool isVoid, decimal receiveQty, decimal consumeQty, int dayFromCurrentDay, string patientID, DateTime startDateTime, string srMedicationConsume, bool isAntibiotic, decimal balanceQty)
        {
            if (isVoid || !isContinue) return string.Empty;

            var consumeMethodId = (string)conMethod;
            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(consumeMethodId);

            // Hitung sisa obat pada awal dayFromCurrentDay
            decimal initialBalQtyCurrentDay = BalanceQtyForSchedule(receiveQty, consumeQty, medrecno, dayFromCurrentDay, cm.IterationQty.ToDecimal(), startDateTime, cm);

            if (cm.IterationQty == null || cm.IterationQty == 0)
            {
                // Tidak diketahui aturan pakainya
                return MedicationOutOffScheduleHtml(medrecno, dayFromCurrentDay, patientID, initialBalQtyCurrentDay);
            }

            return MedicationScheduleHtml(medrecno, consumeQty, dayFromCurrentDay, patientID, startDateTime, srMedicationConsume, isAntibiotic, cm, initialBalQtyCurrentDay, balanceQty);
        }

        // --- ANTIBIOTIC MENGGUNAKAN METODE JAM KONSUMSI YG BISA DITENTUKAN SETELAH KONSUMSI KE 2 ---//
        //private string MedicationScheduleHtml(int medrecno, decimal consumeQty, int dayFromCurrentDay, string patientID,
        //    DateTime startDateTime, string srMedicationConsume, bool isAntibiotic, ConsumeMethod cm, decimal balanceQty)
        //{

        //    var scriptForScheduleDate = SetupStartDate.AddDays(dayFromCurrentDay);
        //    var strb = new StringBuilder();

        //    DateTime lastAntibioticScheduled = new DateTime(1900, 1, 1);
        //    int antibioticScheduledCount = 0; // Untuk keperluan jika sudah 2 kali baru generate schedule berikutnya
        //    int antibioticMinuteRange = 0;
        //    var qrUsed = new MedicationReceiveUsedQuery();
        //    var usedColl = new MedicationReceiveUsedCollection();

        //    if (isAntibiotic)
        //    {
        //        // Jarak waktu konsumsi antibiotic adalah = 24jam/brp kali konsumsi
        //        antibioticMinuteRange = (1440 / cm.IterationQty).ToInt();

        //        // Ambil semua termasuk additional schedule
        //        qrUsed.es.Top = 2;
        //        qrUsed.OrderBy(qrUsed.RealizedDateTime.Descending);
        //        qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.ScheduleDateTime.IsNotNull());

        //        if (usedColl.Load(qrUsed) && usedColl.Count > 0)
        //        {
        //            antibioticScheduledCount = usedColl.Count;
        //            lastAntibioticScheduled = usedColl[0].ScheduleDateTime.Value;
        //        }
        //    }


        //    // Ambil histori  MedicationReceiveUsed utk tgl bersangkutan
        //    qrUsed = new MedicationReceiveUsedQuery();
        //    usedColl = new MedicationReceiveUsedCollection();
        //    qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.ScheduleDateTime >= scriptForScheduleDate,
        //        qrUsed.ScheduleDateTime < scriptForScheduleDate.AddDays(1));
        //    qrUsed.OrderBy(qrUsed.ScheduleDateTime.Ascending);
        //    usedColl.Load(qrUsed);

        //    var histCount = usedColl.Count;

        //    var additionalSchedule = usedColl.Count(used => used.IsAdditionalSchedule == true);
        //    var max = cm.IterationQty + additionalSchedule > histCount ? cm.IterationQty + additionalSchedule : histCount;

        //    var medRecUsed = new MedicationReceiveUsed();
        //    medRecUsed.MedicationReceiveNo = medrecno.ToInt();

        //    var entryMenuCreated = false; // Untuk flag hanya 1 script entry dibuat

        //    var scheduleNo = 0;
        //    var skippedSchedule = 0;
        //    for (int i = 0; i < max; i++)
        //    {
        //        var timeScheduleDisplay = "00:00";
        //        var timeScheduleDisplays = timeScheduleDisplay.Split(':');

        //        if (histCount > i)
        //        {
        //            // Jika ada history setup maka munculkan jam dari history
        //            medRecUsed = usedColl[i];
        //            var scheduleTime = medRecUsed.ScheduleDateTime.Value;

        //            timeScheduleDisplay =
        //                string.Format("{0:00}:{1:00}", scheduleTime.Hour, scheduleTime.Minute);
        //            timeScheduleDisplays = timeScheduleDisplay.Split(':');

        //            if (isAntibiotic)
        //                lastAntibioticScheduled = medRecUsed.ScheduleDateTime.Value;

        //            if (medRecUsed.IsAdditionalSchedule == false)
        //                scheduleNo = scheduleNo + 1;
        //        }
        //        else
        //        {
        //            scheduleNo = scheduleNo + 1;

        //            // Untuk keperluan pembuatan script edit
        //            medRecUsed = new MedicationReceiveUsed();
        //            medRecUsed.MedicationReceiveNo = medrecno.ToInt();

        //            // Jika belum pernah setup maka munculkan jam dari schedule
        //            timeScheduleDisplay = cm.GetColumn(string.Format("Time{0:00}", scheduleNo)).ToString();
        //            timeScheduleDisplays = timeScheduleDisplay.Split(':');

        //            if (isAntibiotic)
        //            {
        //                if (lastAntibioticScheduled.Year == 1900 || antibioticScheduledCount < 2)
        //                {
        //                    // Display undefined schedule, antibiotic baru digenerate schedulenya setelah konsumsi ke 2
        //                    // dg jarak waktu = 24jam/brp kali konsumsi
        //                    timeScheduleDisplay = "--:--";
        //                }
        //                else
        //                {
        //                    // Next schedule
        //                    lastAntibioticScheduled = lastAntibioticScheduled.AddMinutes(antibioticMinuteRange);

        //                    //Skip jika timeScheduleDisplay sudah melewati currentDate untuk kolom current Date
        //                    if (lastAntibioticScheduled.Date > scriptForScheduleDate.Date)
        //                        continue;

        //                    // Check tgl schedule
        //                    while (scriptForScheduleDate.Date > lastAntibioticScheduled.Date)
        //                    {
        //                        lastAntibioticScheduled = lastAntibioticScheduled.AddMinutes(antibioticMinuteRange);
        //                    }

        //                    timeScheduleDisplay = string.Format("{0:00}:{1:00}", lastAntibioticScheduled.Hour,
        //                        lastAntibioticScheduled.Minute);
        //                    timeScheduleDisplays = timeScheduleDisplay.Split(':');
        //                }
        //            }
        //            else
        //            {
        //                if (srMedicationConsume.Equals("AC"))
        //                {
        //                    // Sebelum makan maka kurangi 30menit
        //                    timeScheduleDisplays = timeScheduleDisplay.Split(':');
        //                    var minute = (timeScheduleDisplays[0].ToInt() * 60) + timeScheduleDisplays[1].ToInt();
        //                    minute = minute - 30;
        //                    timeScheduleDisplay = string.Format("{0}:{1}",
        //                        string.Format("{0:00}", Math.Floor((decimal)(minute / 60))),
        //                        string.Format("{0:00}", minute % 60));
        //                    timeScheduleDisplays = timeScheduleDisplay.Split(':');
        //                }
        //            }
        //        }

        //        if (timeScheduleDisplays.Length < 2) continue;

        //        var scheduleDateTime = new DateTime(scriptForScheduleDate.Year, scriptForScheduleDate.Month,
        //            scriptForScheduleDate.Day,
        //            timeScheduleDisplays[0].ToInt(), timeScheduleDisplays[1].ToInt(), 0);

        //        // Skip jika jam lebih kecil dari start schedule untuk schedule yg belum di setup
        //        if (medRecUsed.SetupDateTime == null && startDateTime > scheduleDateTime)
        //        {
        //            skippedSchedule = skippedSchedule + 1;
        //            continue;
        //        }

        //        // Tambah script entry
        //        entryMenuCreated = PopulateMedicationUsedEntryScript(strb, medRecUsed, dayFromCurrentDay, patientID,
        //            timeScheduleDisplay, scriptForScheduleDate, entryMenuCreated);

        //        balanceQty = balanceQty - (medRecUsed.SetupDateTime != null ? medRecUsed.Qty.ToDecimal() : consumeQty);
        //        if (balanceQty <= 0)
        //        {
        //            break;
        //        }
        //    }

        //    // Tambah tombol Extra order atau pemberian obat diluar schedule
        //    if (MedicationStep == "S" && dayFromCurrentDay == 0 && balanceQty > 0 && !entryMenuCreated)
        //    {
        //        strb.AppendFormat(
        //            "&nbsp;<a style='vertical-align: text-bottom;' href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('new', '{0}', 0,'','{1}','{2}','1'); return false;\"><img src=\"../../../Images/Toolbar/insert16.png\"  alt=\"New\" /></a>&nbsp;|",
        //            medrecno, patientID, dayFromCurrentDay);
        //    }

        //    _isCurrentDayFinish = histCount >= (cm.IterationQty - skippedSchedule);

        //    var retval = strb.ToString();
        //    return !string.IsNullOrEmpty(retval) ? retval.Substring(0, retval.Length - 2) : string.Empty;
        //}
        // --- END METODE


        MedicationReceiveUsedCollection _previouseMedicationReceiveUsed;
        int lastMedrecno = 0;
        private MedicationReceiveUsedCollection PreviouseMedicationReceiveUsed(int medrecno)
        {
            // Previouse MedicationReceiveUsed untuk seting default schedule time mulai tanggal sekarang
            if (lastMedrecno == medrecno)
                return _previouseMedicationReceiveUsed;

            lastMedrecno = medrecno;
            var qrPrevUsed = new MedicationReceiveUsedQuery();
            qrPrevUsed.Where(qrPrevUsed.MedicationReceiveNo == medrecno, qrPrevUsed.ScheduleDateTime < CurrentDate.Date);
            qrPrevUsed.OrderBy(qrPrevUsed.ScheduleDateTime.Descending);
            qrPrevUsed.es.Top = 1;
            var prevUsed = new MedicationReceiveUsed();
            if (prevUsed.Load(qrPrevUsed))
            {
                var startDate = prevUsed.ScheduleDateTime.Value.Date;
                var toDate = startDate.AddDays(1);
                qrPrevUsed = new MedicationReceiveUsedQuery();
                qrPrevUsed.Where(qrPrevUsed.MedicationReceiveNo == medrecno, qrPrevUsed.ScheduleDateTime > startDate, qrPrevUsed.ScheduleDateTime < toDate);
                qrPrevUsed.OrderBy(qrPrevUsed.ScheduleDateTime.Descending);

                _previouseMedicationReceiveUsed = new MedicationReceiveUsedCollection();
                _previouseMedicationReceiveUsed.Load(qrPrevUsed);

            }
            if (_previouseMedicationReceiveUsed == null)
                _previouseMedicationReceiveUsed = new MedicationReceiveUsedCollection();
            return _previouseMedicationReceiveUsed;
        }
        private string MedicationScheduleHtml(int medrecno, decimal consumeQty, int dayFromCurrentDay, string patientID,
    DateTime startDateTime, string srMedicationConsume, bool isAntibiotic, ConsumeMethod cm, decimal initialBalQtyCurrentDay, decimal balanceQty)
        {

            var scriptForScheduleDate = SetupStartDate.AddDays(dayFromCurrentDay);
            var strb = new StringBuilder();

            // Ambil histori  MedicationReceiveUsed utk tgl sebelumnya untuk default schedule time
            //var prevUsedColl = new MedicationReceiveUsedCollection();
            //var qrPrevUsed = new MedicationReceiveUsedQuery();
            //qrPrevUsed.Where(qrPrevUsed.MedicationReceiveNo == medrecno, qrPrevUsed.ScheduleDateTime < scriptForScheduleDate);
            //qrPrevUsed.OrderBy(qrPrevUsed.ScheduleDateTime.Descending);
            //if (cm.IterationQty > 0)
            //    qrPrevUsed.es.Top = cm.IterationQty;
            //else
            //    qrPrevUsed.es.Top = 1;

            //prevUsedColl.Load(qrPrevUsed);

            var prevUsedColl = PreviouseMedicationReceiveUsed(medrecno);

            // Ambil histori  MedicationReceiveUsed utk tgl bersangkutan
            var qrUsed = new MedicationReceiveUsedQuery();
            var usedColl = new MedicationReceiveUsedCollection();
            qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.ScheduleDateTime >= scriptForScheduleDate,
                qrUsed.ScheduleDateTime < scriptForScheduleDate.AddDays(1));
            qrUsed.OrderBy(qrUsed.ScheduleDateTime.Ascending);
            usedColl.Load(qrUsed);

            var histCount = usedColl.Count;

            var additionalSchedule = usedColl.Count(used => used.IsAdditionalSchedule == true);
            var max = cm.IterationQty + additionalSchedule > histCount ? cm.IterationQty + additionalSchedule : histCount;

            var medRecUsed = new MedicationReceiveUsed();
            medRecUsed.MedicationReceiveNo = medrecno.ToInt();

            var entryMenuCreated = false; // Untuk flag hanya boleh 1 script entry per hari

            var scheduleNo = 0;
            var skippedSchedule = 0;
            for (int i = 0; i < max; i++)
            {
                var timeScheduleDisplay = "00:00";
                var timeScheduleDisplays = timeScheduleDisplay.Split(':');

                if (histCount > i)
                {
                    // Jika ada history setup maka munculkan jam dari history
                    medRecUsed = usedColl[i];
                    var scheduleTime = medRecUsed.ScheduleDateTime.Value;

                    timeScheduleDisplay =
                        string.Format("{0:00}:{1:00}", scheduleTime.Hour, scheduleTime.Minute);
                    timeScheduleDisplays = timeScheduleDisplay.Split(':');

                    if (medRecUsed.IsAdditionalSchedule == false)
                        scheduleNo = scheduleNo + 1;
                }
                else
                {
                    scheduleNo = scheduleNo + 1;

                    // Untuk keperluan pembuatan script edit
                    medRecUsed = new MedicationReceiveUsed();
                    medRecUsed.MedicationReceiveNo = medrecno.ToInt();

                    // Check Custom schedule
                    var qrSchedule = new MedicationScheduleQuery();
                    qrSchedule.Where(qrSchedule.MedicationReceiveNo == medrecno, qrSchedule.ScheduleStartDate <= scriptForScheduleDate, qrSchedule.ScheduleNo == scheduleNo);
                    qrSchedule.OrderBy(qrSchedule.ScheduleStartDate.Descending);
                    qrSchedule.es.Top = 1;
                    var histSch = new MedicationSchedule();
                    if (histSch.Load(qrSchedule) && histSch.ScheduleTime != null)
                    {
                        var scheduleTime = histSch.ScheduleTime.Value;
                        timeScheduleDisplay =
                            string.Format("{0:00}:{1:00}", scheduleTime.Hour, scheduleTime.Minute);
                    }
                    else
                    {
                        // Check history schedule time sebelumnya
                        if (prevUsedColl.Count > i)
                        {
                            var prevSetupTime = prevUsedColl[(prevUsedColl.Count - i) - 1].ScheduleDateTime.Value.ToString("HH:mm");

                            // Cek jika time lebih besar dari schedule time berikutnya maka gunakan time default pada ConsumeMethod
                            var nextSchedule = cm.GetColumn(string.Format("Time{0:00}", scheduleNo + 1)).ToString();
                            if (string.Compare(prevSetupTime, nextSchedule) < 0)
                            {
                                timeScheduleDisplay = prevSetupTime;
                            }
                        }
                    }

                    // Jika belum pernah setup maka munculkan jam dari schedule master
                    if (timeScheduleDisplay.Equals("00:00"))
                    {
                        timeScheduleDisplay = cm.GetColumn(string.Format("Time{0:00}", scheduleNo)).ToString();
                    }

                    timeScheduleDisplays = timeScheduleDisplay.Split(':');

                    if (srMedicationConsume.Equals("AC") && timeScheduleDisplays.Length > 1)
                    {
                        // Sebelum makan maka kurangi 30menit
                        timeScheduleDisplays = timeScheduleDisplay.Split(':');
                        var minute = (timeScheduleDisplays[0].ToInt() * 60) + timeScheduleDisplays[1].ToInt();
                        minute = minute - 30;
                        timeScheduleDisplay = string.Format("{0}:{1}",
                            string.Format("{0:00}", Math.Floor((decimal)(minute / 60))),
                            string.Format("{0:00}", minute % 60));
                        timeScheduleDisplays = timeScheduleDisplay.Split(':');
                    }
                }

                if (timeScheduleDisplays.Length < 2) continue;

                var scheduleDateTime = new DateTime(scriptForScheduleDate.Year, scriptForScheduleDate.Month,
                    scriptForScheduleDate.Day,
                    timeScheduleDisplays[0].ToInt(), timeScheduleDisplays[1].ToInt(), 0);

                // Skip jika jam lebih kecil dari start schedule untuk schedule yg belum di setup
                if (medRecUsed.SetupDateTime == null && startDateTime > scheduleDateTime)
                {
                    skippedSchedule = skippedSchedule + 1;
                    continue;
                }

                // Tambah script entry
                entryMenuCreated = PopulateMedicationUsedEntryScript(strb, medRecUsed, dayFromCurrentDay, patientID,
                    timeScheduleDisplay, scriptForScheduleDate, entryMenuCreated, balanceQty, scheduleNo);

                initialBalQtyCurrentDay = initialBalQtyCurrentDay - (medRecUsed.SetupDateTime != null ? medRecUsed.Qty.ToDecimal() : consumeQty);
                if (initialBalQtyCurrentDay <= 0)
                {
                    break;
                }
            }

            // Tambah tombol Extra order atau pemberian obat diluar schedule untuk setup
            if (MedicationStep == "S")
            {
                if (dayFromCurrentDay == 0 && initialBalQtyCurrentDay > 0 && !entryMenuCreated)
                {
                    strb.AppendFormat(
                        "&nbsp;<a style='vertical-align: text-bottom;' href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('new', '{0}', 0,'','{1}','{2}','1'); return false;\"><img src=\"../../../Images/Toolbar/insert16.png\"  alt=\"New\" /></a>&nbsp;|",
                        medrecno, patientID, dayFromCurrentDay);
                }
            }

            _isCurrentDayFinish = histCount >= (cm.IterationQty - skippedSchedule);

            var retval = strb.ToString();
            return !string.IsNullOrEmpty(retval) ? retval.Substring(0, retval.Length - 2) : string.Empty;
        }



        private string MedicationOutOffScheduleHtml(int medrecno, int dayFromCurrentDay, string patientID, decimal initialBalQtyCurrentDay)
        {
            var isEntryMenuHasCreated = false; // Untuk flag hanya 1 script entry dibuat
            var strb = new StringBuilder();

            // Cari yg dari hasil schedule manual entry
            var qrUsed = new MedicationReceiveUsedQuery();
            var usedColls = new MedicationReceiveUsedCollection();

            qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno);

            var scriptForScheduleDate = SetupStartDate.AddDays(dayFromCurrentDay);

            qrUsed.Where(qrUsed.ScheduleDateTime >= scriptForScheduleDate,
                qrUsed.ScheduleDateTime < scriptForScheduleDate.AddDays(1));

            qrUsed.OrderBy(qrUsed.ScheduleDateTime.Ascending);
            if (usedColls.Load(qrUsed))
            {
                foreach (var used in usedColls)
                {
                    var time = Convert.ToDateTime(used.ScheduleDateTime);




                    //    if (isEntryMenuHasCreated || (MedicationStep == "S")
                    //    || (MedicationStep == "V" && (used.SetupDateTime == null || used.VerificationDateTime != null))
                    //    || (MedicationStep == "R" && (used.VerificationDateTime == null || used.RealizedDateTime != null)))
                    //{
                    //    // Display as icon disable
                    //    var color = "black";
                    //    var img = string.Format("{0}/Images/Toolbar/post16_d.png", Helper.UrlRoot());
                    //    if (used.SetupDateTime != null)
                    //        color = "red";
                    //    if (used.VerificationDateTime != null)
                    //        color = "darkorange";
                    //    if (used.RealizedDateTime != null)
                    //    {
                    //        color = "green";
                    //        img = string.Format("{0}/Images/Verified16.png", Helper.UrlRoot());
                    //    }

                    //    strb.AppendFormat(
                    //        "&nbsp;<a style=\"color: {2};pointer-events: none;cursor: default;\"><img style='vertical-align: text-bottom;' src=\"{3}\"  alt=\"New\" />&nbsp;{0:00}:{1:00}</a>&nbsp;|",
                    //        time.Hour, time.Minute, color, img);
                    //}
                    //else
                    //{
                    //    // Display as icon edit
                    //    var medicationMode = MedicationStep == "S" ? "new" : "edit";
                    //    strb.AppendFormat(
                    //        "&nbsp;<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('{3}', '{0}', '{1}', '{2}', '{4}','1'); return false;\"><img style='vertical-align: text-bottom;' src=\"{5}/Images/Toolbar/post16.png\"  alt=\"New\" />&nbsp;{2}</a>&nbsp;|",
                    //        medrecno, used.SequenceNo ?? 0, string.Format("{0:00}:{1:00}", time.Hour, time.Minute),
                    //        medicationMode,
                    //        dayFromCurrentDay, Helper.UrlRoot());

                    //    isEntryMenuHasCreated = true;
                    //}

                    // Hanya bisa 1 schedule yg dientry
                    if (!isEntryMenuHasCreated && used.IsVoidSchedule != true &&
                        ((MedicationStep == "S" && used.SetupDateTime == null && initialBalQtyCurrentDay > 0)
                         || ((MedicationStep == "H" && used.SetupDateTime != null && used.HandoversDateTime == null))
                         || ((MedicationStep == "V" && used.SetupDateTime != null && used.VerificationDateTime == null))
                         || ((MedicationStep == "R" && scriptForScheduleDate <= DateTime.Today && used.VerificationDateTime != null && used.RealizedDateTime == null))
                        ))
                    {
                        // Display as icon edit
                        var medicationMode = MedicationStep == "S" ? "new" : "edit";
                        strb.AppendFormat(
                            "&nbsp;<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('{3}', '{0}', '{1}', '{2}', '{4}','1','0'); return false;\"><img style='vertical-align: text-bottom;' src=\"{5}/Images/Toolbar/post16.png\"  alt=\"New\" />&nbsp;{2}</a>&nbsp;|",
                            medrecno, used.SequenceNo ?? 0, string.Format("{0:00}:{1:00}", time.Hour, time.Minute),
                            medicationMode,
                            dayFromCurrentDay, Helper.UrlRoot());

                        isEntryMenuHasCreated = true;
                    }
                    else
                    {
                        // Display as icon disable
                        var color = "black";
                        var img = string.Format("{0}/Images/Toolbar/post16_d.png", Helper.UrlRoot());
                        if (used.SetupDateTime != null)
                            color = "red";
                        if (used.VerificationDateTime != null)
                            color = "darkorange";
                        if (used.RealizedDateTime != null)
                        {
                            color = "green";
                            img = string.Format("{0}/Images/Verified16.png", Helper.UrlRoot());
                        }

                        strb.AppendFormat(
                            "&nbsp;<a style=\"color: {2};pointer-events: none;cursor: default;\"><img style='vertical-align: text-bottom;' src=\"{3}\"  alt=\"New\" />&nbsp;{0:00}:{1:00}</a>&nbsp;|",
                            time.Hour, time.Minute, color, img);
                    }
                    initialBalQtyCurrentDay = initialBalQtyCurrentDay - (used.SetupDateTime != null ? used.Qty.ToDecimal() : 0);
                }
            }

            if (initialBalQtyCurrentDay > 0)
                // Untuk pemberian obat diluar schedule
                if (MedicationStep == "S" && dayFromCurrentDay < 2)
                    strb.AppendFormat(
                        "&nbsp;&nbsp;<a style='vertical-align: text-bottom;' href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('new', '{0}', 0,'','{1}','{2}','1'); return false;\"><img src=\"../../../Images/Toolbar/insert16.png\"  alt=\"New\" /></a>",
                        medrecno, patientID, dayFromCurrentDay);

            var addScript = strb.ToString();
            return string.IsNullOrEmpty(addScript)
                ? string.Empty
                : addScript.Substring(0, addScript.Length - 2);
        }

        private bool PopulateMedicationUsedEntryScript(StringBuilder strb, MedicationReceiveUsed medRecUsed, int dayFromCurrentDay, string patientID, string timeScheduleDisplay, DateTime dateAfterAdded, bool isEntryMenuHasCreated, decimal balanceQty, int scheduleNo)
        {
            // Schedule untuk hari ini dan besok
            if (dayFromCurrentDay == 0 || (dayFromCurrentDay == 1 && _isCurrentDayFinish))
            {
                // Hanya bisa 1 schedule yg dientry
                if (!isEntryMenuHasCreated && medRecUsed.IsVoidSchedule != true &&
                    ((MedicationStep == "S" && medRecUsed.SetupDateTime == null && balanceQty > 0)
                     || ((MedicationStep == "H" && medRecUsed.SetupDateTime != null && medRecUsed.HandoversDateTime == null))
                     || ((MedicationStep == "V" && medRecUsed.SetupDateTime != null && medRecUsed.VerificationDateTime == null))
                     || ((MedicationStep == "R" && dateAfterAdded <= DateTime.Today && medRecUsed.VerificationDateTime != null && medRecUsed.RealizedDateTime == null))
                    ))
                {
                    isEntryMenuHasCreated = true; // Beri flag untuk mencegah script entry dibawah dibuat > 1x
                    var medicationMode = MedicationStep == "S" ? "new" : "edit";
                    strb.AppendFormat(
                        "&nbsp;<a  href=\"#\"  onclick=\"javascript:entryMedicationReceiveUsed('{3}', '{0}', '{1}', '{2}','{4}','{5}','0','{6}'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/post16.png\"  alt=\"New\" />&nbsp;{2}</a>&nbsp;|",
                        medRecUsed.MedicationReceiveNo, medRecUsed.SequenceNo ?? 0, timeScheduleDisplay, medicationMode, patientID, dayFromCurrentDay, scheduleNo);
                }
                else
                {
                    var color = "black";
                    var img = string.Format("{0}/Images/Toolbar/post16_d.png", Helper.UrlRoot());

                    if (medRecUsed.SetupDateTime != null)
                        color = "red";
                    if (medRecUsed.VerificationDateTime != null)
                        color = "darkorange";
                    if (medRecUsed.RealizedDateTime != null)
                    {
                        color = "green";
                        img = string.Format("{0}/Images/Verified16.png", Helper.UrlRoot());
                    }
                    strb.Append("&nbsp;");
                    if (medRecUsed.IsVoidSchedule == true)
                        strb.Append("<del>");

                    strb.AppendFormat(
                        "<a style=\"color: {1};pointer-events: none;cursor: default;\"><img style='vertical-align: text-bottom;' src=\"{2}\"  alt=\"New\" />&nbsp;{0}</a>&nbsp;|",
                        timeScheduleDisplay, color, img);

                    if (medRecUsed.IsVoidSchedule == true)
                        strb.Append("</del>");
                }
            }
            else
            {
                strb.AppendFormat("&nbsp;{0}&nbsp;|", timeScheduleDisplay);
            }

            return isEntryMenuHasCreated;
        }


        protected void grdMedicationStatus_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                if (true.Equals(item.GetDataKeyValue("IsAntibiotic")))
                {
                    //item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    var cell = item["ItemDescription"];
                    cell.Style.Add(HtmlTextWriterStyle.Color, "blue");
                }

                if (false.Equals(item.GetDataKeyValue("IsContinue")))
                {
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }

        }



        public RadGrid GridMedicationStatus
        {
            get { return grdMedicationStatus; }
        }

        protected string MedicationChangeConsumeMethodHtml(object medNo, object conmtd, object patientID, object balanceQty, object isAntibiotic)
        {
            return String.Empty; // entryMedicationChangeConsumeMethod harus diseusaikan dulu untuk per terapi 
            if (MedicationStep != "S") return string.Empty;
            if (balanceQty.ToDecimal() == 0) return string.Empty;
            if (AppSession.Parameter.IsRasproEnable && true.Equals(isAntibiotic)) return string.Empty; // harus melalui form RASPRO
            return string.Format(
                "<a  href=\"#\" onclick=\"javascript:entryMedicationChangeConsumeMethod('{0}','{1}','{2}'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",
                medNo, conmtd, patientID);
        }
        protected string MedicationEditHtml(object medNo, object patientID)
        {
            // Hanya bisa edit di setup
            if (MedicationStep != "S") return string.Empty;

            // Hanya bisa edit jika belum ada setup
            var qr = new MedicationReceiveUsedQuery();
            qr.Where(qr.MedicationReceiveNo == medNo.ToString());
            qr.es.Top = 1;
            var medUsed = new MedicationReceiveUsed();
            if (medUsed.Load(qr)) return string.Empty;

            return string.Format(
                "<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveEdit('{0}','{1}'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",
                medNo, patientID);
        }
        protected string MedicationUsedEditHtml(object medNo, object seqNo, object scheduleDateTime, object handoversDateTime, object verificationDateTime, object realizationDateTime)
        {
            var isEditable = scheduleDateTime != DBNull.Value
                && ((MedicationStep == "S" && verificationDateTime == DBNull.Value)
                     || (MedicationStep == "H" && handoversDateTime != DBNull.Value && verificationDateTime == DBNull.Value)
                     || (MedicationStep == "V" && verificationDateTime != DBNull.Value && realizationDateTime == DBNull.Value)
                     || MedicationStep == "R" && realizationDateTime != DBNull.Value);
            return isEditable ? string.Format(
                "<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('edit', '{0}', '{1}', '','','','0'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",
                medNo, seqNo) : string.Empty;
        }

        protected object MedicationScheduleGridDetailHtml(GridItem container, string realizationType)
        {
            if (realizationType == "H")
            {
                if (DataBinder.Eval(container.DataItem, "HandoversDateTime") != DBNull.Value)
                    return Convert.ToDateTime(DataBinder.Eval(container.DataItem, "HandoversDateTime"))
                        .ToString(AppConstant.DisplayFormat.DateHourMinute);

                if (DataBinder.Eval(container.DataItem, "SetupDateTime") != DBNull.Value)
                    return MedicationReceiveUsedEntryScript(container);
            }
            else if (realizationType == "V")
            {
                if (DataBinder.Eval(container.DataItem, "VerificationDateTime") != DBNull.Value)
                    return Convert.ToDateTime(DataBinder.Eval(container.DataItem, "VerificationDateTime"))
                        .ToString(AppConstant.DisplayFormat.DateHourMinute);

                //if (MedicationStep == "V")
                if (DataBinder.Eval(container.DataItem, "SetupDateTime") != DBNull.Value)
                    return MedicationReceiveUsedEntryScript(container);
            }
            else if (realizationType == "R")
            {
                if (DataBinder.Eval(container.DataItem, "RealizedDateTime") != DBNull.Value)
                    return Convert.ToDateTime(DataBinder.Eval(container.DataItem, "RealizedDateTime"))
                        .ToString(AppConstant.DisplayFormat.DateHourMinute);

                if (MedicationStep == "R" && DataBinder.Eval(container.DataItem, "VerificationDateTime") != DBNull.Value)
                    return MedicationReceiveUsedEntryScript(container);
            }

            return string.Empty;
        }

        private string MedicationReceiveUsedEntryScript(GridItem container)
        {
            if (DataBinder.Eval(container.DataItem, "ScheduleDateTime") == DBNull.Value) return string.Empty;

            var scheduleTime = Convert.ToDateTime(DataBinder.Eval(container.DataItem, "ScheduleDateTime"));
            var timeScheduleDisplay = string.Format("{0:00}:{1:00}", scheduleTime.Hour, scheduleTime.Minute);
            var dayNo = (scheduleTime - SetupStartDate).Days;
            var script = string.Format(
                "&nbsp;<a  href=\"#\"  onclick=\"javascript:entryMedicationReceiveUsed('edit', '{0}', '{1}', '{2}','{3}','{4}','0'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/post16.png\"  alt=\"New\" />&nbsp;{2}</a>",
                DataBinder.Eval(container.DataItem, "MedicationReceiveNo"), DataBinder.Eval(container.DataItem, "SequenceNo"),
                timeScheduleDisplay, string.Empty, dayNo);
            return script;
        }

    }
}