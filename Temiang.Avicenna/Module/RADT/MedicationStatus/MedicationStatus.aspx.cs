using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using System.Text;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Telerik.Charting;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Medication
{

    public partial class MedicationStatus : BasePage
    {
        private DateTime _currentDate = (new DateTime()).NowAtSqlServer();

        protected string MedicationStep
        {
            get
            {
                return Request.QueryString["stat"];
            }
        }
        private DateTime SetupStartDate
        {
            get
            {
                if (!txtStartDate.IsEmpty)
                {
                    if (txtStartDate.SelectedDate != null)
                        return txtStartDate.SelectedDate.Value;
                }
                return _currentDate;
            }
        }
        private bool _isCurrentDayFinish = false;
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            switch (MedicationStep)
            {
                case "S":
                    this.Title = "Medication Setup Entry";
                    break;
                case "V":
                    this.Title = "Medication Verification Entry";
                    break;
                case "R":
                    this.Title = "Medication Realization Entry";
                    break;
            }


            if (!IsPostBack)
            {
                //service unit
                var units = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );
                query.OrderBy(units.Query.ServiceUnitName.Ascending);
                units.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in units)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

            }


        }
        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
        }
        protected void cboRoomID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceRoomQuery("a");
            query.Select
            (
                query.RoomID,
                query.RoomName
            );
            query.es.Distinct = true;
            query.OrderBy(query.RoomID.Ascending);
            query.Where
            (
                query.RoomName.Like(searchTextContain), query.IsActive == true
            );

            if (!(string.IsNullOrEmpty(cboServiceUnitID.SelectedValue)))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                RestoreValueFromCookie();

                txtStartDate.SelectedDate = _currentDate;

                if (!string.IsNullOrEmpty(Request.QueryString["mn"]))
                {
                    txtMedRegNo.Text = Request.QueryString["mn"];
                }
            }

            // Header Text
            if (_currentDate == SetupStartDate)
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



        protected void grdMedicationStatus_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            // Delete detail
            if (e.Item.OwnerTableView.Name.Equals("grdMedicationStatusUsed"))
            {
                var medicationReceiveNo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["MedicationReceiveNo"].ToInt();
                var sequenceNo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"].ToInt();

                using (var tr = new esTransactionScope())
                {
                    var nmd = new MedicationReceiveUsed();
                    if (nmd.LoadByPrimaryKey(medicationReceiveNo, sequenceNo))
                    {
                        switch (MedicationStep)
                        {
                            case "S":
                                nmd.MarkAsDeleted();
                                break;
                            case "V":
                                nmd.str.VerificationByUserID = string.Empty;
                                nmd.str.VerificationDateTime = string.Empty;
                                break;
                            case "R":
                                nmd.str.RealizedByUserID = string.Empty;
                                nmd.str.RealizedDateTime = string.Empty;
                                break;
                        }
                        nmd.Save();
                    }
                    tr.Complete();
                }

                grdMedicationStatus.MasterTableView.DetailTables[0].Rebind();
            }
        }

        private DataTable MedicationReceiveDataTable()
        {
            var query = new MedicationReceiveQuery("a");

            //// Hanya registrasi RI yg masih ada di Bed yg ditampilkan berikut From Registrasi nya
            //var bed = new BedQuery("bd");
            //query.InnerJoin(bed).On(query.RegistrationNo == bed.RegistrationNo);

            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var reg = new RegistrationQuery("r");
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);

            var pat = new PatientQuery("p");
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);

            var grr = new GuarantorQuery("g");
            query.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);

            var unit = new ServiceUnitQuery("b");
            query.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);

            var room = new ServiceRoomQuery("c");
            query.InnerJoin(room).On(reg.RoomID == room.RoomID);

            var item = new ItemProductMedicQuery("im");
            query.LeftJoin(item).On(query.ItemID == item.ItemID);

            var mc = new AppStandardReferenceItemQuery("mc");
            query.LeftJoin(mc).On(query.SRMedicationConsume == mc.ItemID &&
                                  mc.StandardReferenceID == "MedicationConsume");

            var title = new AppStandardReferenceItemQuery("ttl");
            query.LeftJoin(title).On(pat.SRSalutation == title.ItemID &&
                                     title.StandardReferenceID == "Title");


            query.Select
            (
                query,
                cm.SRConsumeMethodName,
                room.RoomName,
                reg.PatientID,
                reg.RegistrationDate,
                reg.RegistrationQue,
                unit.ServiceUnitName,
                pat.MedicalNo,
                title.ItemName.As("SRSalutationName"),
                pat.PatientName,
                pat.Sex,
                grr.GuarantorName,
                reg.BedID,
                item.IsAntibiotic,
                reg.FromRegistrationNo,
                pat.DateOfBirth,
                mc.ItemName.As("SRMedicationConsumeName")
            );

            query.Where(query.Or(query.IsVoid.IsNull(), query.IsVoid == false));

            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);

            if (!string.IsNullOrEmpty(cboRoomID.SelectedValue))
                query.Where(reg.RoomID == cboRoomID.SelectedValue);

            if (!string.IsNullOrEmpty(txtMedRegNo.Text))
            {
                if (txtMedRegNo.Text.Contains("REG"))
                    query.Where(query.RegistrationNo == txtMedRegNo.Text);
                else
                    query.Where(pat.MedicalNo == txtMedRegNo.Text);
            }

            if (txtPatientName.Text != string.Empty)
            {
                var searchPatient = "%" + txtPatientName.Text + "%";
                query.Where(string.Format("<RTRIM(p.FirstName+' '+p.MiddleName)+' '+p.LastName LIKE '{0}'>", searchPatient));
            }

            if (!chkIsIncludeFinished.Checked)
                query.Where(query.BalanceRealQty > 0); //Balance berdasarkan Receive - Realisasi

            if (!chkIsIncludeStopped.Checked)
                query.Where(query.IsContinue == true);

            //var used = new MedicationReceiveUsedQuery("used");
            //switch (ProgramID)
            //{
            //    case AppConstant.Program.MedicationVerificationStatus:
            //        query.InnerJoin(used).On(query.MedicationReceiveNo == used.MedicationReceiveNo);
            //        query.Where(used.SetupByUserID.IsNotNull(), used.SetupByUserID > string.Empty);
            //        query.es.Distinct = true;
            //        break;
            //    case AppConstant.Program.MedicationRealizedStatus:
            //        query.InnerJoin(used).On(query.MedicationReceiveNo == used.MedicationReceiveNo);
            //        query.Where(used.VerificationByUserID.IsNotNull(), used.VerificationByUserID > string.Empty);
            //        query.es.Distinct = true;
            //        break;
            //}

            switch (MedicationStep)
            {
                case "V":
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

            // Order By dirubah berdasarkan Nama Obat untuk menghindari 2x setup (2019 09 25 Bu Yulis atas persetujuan Bu Wiwin)
            // query.OrderBy(unit.ServiceUnitName.Ascending, room.RoomName.Ascending, pat.PatientName.Ascending, query.RefTransactionNo.Ascending, query.RefSequenceNo.Ascending, query.ItemDescription.Ascending, query.ReceiveDateTime.Ascending);
            query.OrderBy(unit.ServiceUnitName.Ascending, room.RoomName.Ascending, pat.PatientName.Ascending, query.ItemDescription.Ascending, query.ReceiveDateTime.Ascending);


            var medicationReceiveDataTable = query.LoadDataTable();

            // Untuk pemberian warna berbeda tiap PatientID
            medicationReceiveDataTable.Columns.Add(new DataColumn("IsOddBackground", typeof(bool)));

            //Umur Pasien
            medicationReceiveDataTable.Columns.Add(new DataColumn("Age", typeof(string)));
            var patientID = string.Empty;
            var isOdd = false;
            var age = string.Empty;
            foreach (DataRow row in medicationReceiveDataTable.Rows)
            {
                if (!patientID.Equals(row["PatientID"]))
                {
                    // Age
                    if (row["DateOfBirth"] != DBNull.Value)
                    {
                        var birthDate = Convert.ToDateTime(row["DateOfBirth"]);

                        age = string.Format("{0}Y, {1}M",
                            Helper.GetAgeInYear(birthDate, SetupStartDate), Helper.GetAgeInMonth(birthDate, SetupStartDate));
                    }
                    patientID = row["PatientID"].ToString();
                    isOdd = !isOdd;
                }
                row["Age"] = age;
                row["IsOddBackground"] = isOdd;
                row["ItemDescription"] = row["ItemDescription"].ToString().Replace(Environment.NewLine, "<br>");
            }

            medicationReceiveDataTable.Columns.Add("IsCurrentDayFinish", typeof(bool));
            return medicationReceiveDataTable;
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

                query.Select(query, setup.UserName.As("SetupByUserName"), verif.UserName.As("VerificationByUserName"), realize.UserName.As("RealizedByUserName"));
                query.Where(query.MedicationReceiveNo == medicationReceiveNo); //, query.ScheduleDateTime.IsNotNull());
                query.OrderBy(query.SequenceNo.Descending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }

        protected string MedicationScheduleHtml(object medrecno, object conMethod, object isContinue, object isVoid, object oBalanceQty,
            object oConsumeQty, int dayFromCurrentDay, string patientID, object oIsAntibioic, object startDateTime, object srMedicationConsume)
        {
            return MedicationScheduleHtml(medrecno.ToInt(), conMethod.ToString(), true.Equals(isContinue), true.Equals(isVoid),
                oBalanceQty.ToDecimal(), oConsumeQty.ToDecimal(), dayFromCurrentDay.ToInt(), patientID, Convert.ToDateTime(startDateTime), srMedicationConsume == DBNull.Value ? string.Empty : srMedicationConsume.ToString(), true.Equals(oIsAntibioic));
        }

        private decimal BalanceQtyForSchedule(decimal receiveQty, decimal consumeQty, int medrecno, int dayFromCurrentDay, decimal iterationQty, DateTime startDateTime, ConsumeMethod cm)
        {
            decimal balanceQty = receiveQty;
            var setupCount = 0;
            var skippedSchedule = 0;

            // Ambil jumlah yg sudah disetup 
            var qr = new MedicationReceiveUsedQuery();
            qr.Where(qr.MedicationReceiveNo == medrecno,
                qr.ScheduleDateTime < SetupStartDate.AddDays(dayFromCurrentDay));
            qr.Where(qr.Or(qr.IsReSchedule.IsNull(), qr.IsReSchedule == false), qr.Or(qr.IsVoidSchedule.IsNull(), qr.IsVoidSchedule == false));
            qr.Select(qr.Qty.Sum().As("Qty"));

            var dtb = qr.LoadDataTable();
            if (dtb.Rows[0] != null)
                balanceQty = receiveQty - dtb.Rows[0][0].ToDecimal();

            if (balanceQty == 0)
                return 0;

            if (dayFromCurrentDay > 0)
            {
                // Ambil jumlah yg sudah disetup di hari sebelumnya 0 to dayFromCurrentDay
                qr = new MedicationReceiveUsedQuery();
                qr.Where(qr.MedicationReceiveNo == medrecno, qr.ScheduleDateTime >= SetupStartDate,
                    qr.ScheduleDateTime < SetupStartDate.AddDays(dayFromCurrentDay), qr.IsAdditionalSchedule == false);
                qr.Select(qr.ScheduleDateTime.Count().As("SetupCount"));
                dtb = qr.LoadDataTable();
                if (dtb.Rows[0] != null)
                    setupCount = dtb.Rows[0][0].ToInt();


                // Ambil jumlah setup Additional di hari 0 atau pertama
                var additionalSetupCount = 0;
                qr = new MedicationReceiveUsedQuery();
                qr.Where(qr.MedicationReceiveNo == medrecno, qr.ScheduleDateTime >= SetupStartDate,
                    qr.ScheduleDateTime >= startDateTime.Date, qr.ScheduleDateTime >= startDateTime.Date.AddDays(1), qr.IsAdditionalSchedule == true);
                qr.Select(qr.ScheduleDateTime.Count().As("SetupCount"));
                dtb = qr.LoadDataTable();
                if (dtb.Rows[0] != null)
                    additionalSetupCount = dtb.Rows[0][0].ToInt();

                // Hitung schedule di hari pertama yg kurang dari startDateTime
                if (cm.IterationQty != null || cm.IterationQty > 0)
                {
                    for (int i = 0; i < cm.IterationQty; i++)
                    {
                        var timeScheduleDisplay = cm.GetColumn(string.Format("Time{0:00}", i + 1)).ToString();
                        var timeScheduleDisplays = timeScheduleDisplay.Split(':');

                        var scheduleDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day,
                            timeScheduleDisplays[0].ToInt(), timeScheduleDisplays[1].ToInt(), 0);

                        if (startDateTime > scheduleDateTime) skippedSchedule = skippedSchedule + 1;
                    }

                }

                skippedSchedule = skippedSchedule - additionalSetupCount;
            }

            // Jumlah setup seharusnya di hari sebelumnya
            var setupScheduleCount = (iterationQty * dayFromCurrentDay);
            var notYetSetup = setupScheduleCount - setupCount;

            // Kurangi lagi balance dg schedule yg belum disetup jika dayFromCurrentDay>0
            if (dayFromCurrentDay > 0)
                balanceQty = balanceQty - ((notYetSetup - skippedSchedule) * consumeQty);

            return balanceQty;
        }

        protected string MedicationScheduleHtml(GridItem container, int dayFromCurrentDay)
        {
            return MedicationScheduleHtml(DataBinder.Eval(container.DataItem, "MedicationReceiveNo"),
                DataBinder.Eval(container.DataItem, "SRConsumeMethod"), DataBinder.Eval(container.DataItem, "IsContinue"),
                DataBinder.Eval(container.DataItem, "IsVoid"), DataBinder.Eval(container.DataItem, "ReceiveQty"),
                DataBinder.Eval(container.DataItem, "ConsumeQty"), dayFromCurrentDay, DataBinder.Eval(container.DataItem, "PatientID").ToString(),
                DataBinder.Eval(container.DataItem, "IsAntibiotic"), DataBinder.Eval(container.DataItem, "StartDateTime"), DataBinder.Eval(container.DataItem, "SRMedicationConsume"));
        }

        private string MedicationScheduleHtml(int medrecno, string conMethod, bool isContinue, bool isVoid, decimal receiveQty, decimal consumeQty, int dayFromCurrentDay, string patientID, DateTime startDateTime, string srMedicationConsume, bool isAntibiotic)
        {
            if (isVoid || !isContinue) return string.Empty;

            var consumeMethodId = (string)conMethod;
            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(consumeMethodId);

            decimal balanceQty = receiveQty;
            // Hitung sisa obat pada awal dayFromCurrentDay
            balanceQty = BalanceQtyForSchedule(receiveQty, consumeQty, medrecno, dayFromCurrentDay, cm.IterationQty.ToDecimal(), startDateTime, cm);
            if (balanceQty <= 0) return string.Empty;


            if (cm.IterationQty == null || cm.IterationQty < 0)
            {
                // Tidak diketahui aturan pakainya
                return MedicationOutOffScheduleHtml(medrecno, dayFromCurrentDay, patientID, balanceQty);
            }

            return MedicationScheduleHtml(medrecno, consumeQty, dayFromCurrentDay, patientID, startDateTime, srMedicationConsume, isAntibiotic, cm, balanceQty);
        }

        private string MedicationScheduleHtml(int medrecno, decimal consumeQty, int dayFromCurrentDay, string patientID,
            DateTime startDateTime, string srMedicationConsume, bool isAntibiotic, ConsumeMethod cm, decimal balanceQty)
        {
            var scriptForScheduleDate = SetupStartDate.AddDays(dayFromCurrentDay);
            var strb = new StringBuilder();

            DateTime lastAntibioticScheduled = new DateTime(1900, 1, 1);
            int antibioticScheduledCount = 0; // Untuk keperluan jika sudah 2 kali baru generate schedule berikutnya
            int antibioticMinuteRange = 0;
            var qrUsed = new MedicationReceiveUsedQuery();
            var usedColl = new MedicationReceiveUsedCollection();
            if (isAntibiotic)
            {
                // Jarak waktu konsumsi antibiotic adalah = 24jam/brp kali konsumsi
                antibioticMinuteRange = (1440 / cm.IterationQty).ToInt();

                // Ambil semua termasuk additional schedule
                qrUsed.es.Top = 2;
                qrUsed.OrderBy(qrUsed.RealizedDateTime.Descending);
                qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.ScheduleDateTime.IsNotNull());

                if (usedColl.Load(qrUsed) && usedColl.Count > 0)
                {
                    antibioticScheduledCount = usedColl.Count;
                    lastAntibioticScheduled = usedColl[0].ScheduleDateTime.Value;
                }
            }


            // Ambil histori  MedicationReceiveUsed utk tgl bersangkutan
            qrUsed = new MedicationReceiveUsedQuery();
            usedColl = new MedicationReceiveUsedCollection();
            qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.ScheduleDateTime >= scriptForScheduleDate,
                qrUsed.ScheduleDateTime < scriptForScheduleDate.AddDays(1));
            qrUsed.OrderBy(qrUsed.ScheduleDateTime.Ascending);
            usedColl.Load(qrUsed);

            var histCount = usedColl.Count;

            var additionalSchedule = usedColl.Count(used => used.IsAdditionalSchedule == true);
            var max = cm.IterationQty + additionalSchedule > histCount ? cm.IterationQty + additionalSchedule : histCount;

            var medRecUsed = new MedicationReceiveUsed();
            medRecUsed.MedicationReceiveNo = medrecno.ToInt();

            var entryMenuCreated = false; // Untuk flag hanya 1 script entry dibuat

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

                    if (isAntibiotic)
                        lastAntibioticScheduled = medRecUsed.ScheduleDateTime.Value;

                    if (medRecUsed.IsAdditionalSchedule == false)
                        scheduleNo = scheduleNo + 1;
                }
                else
                {
                    scheduleNo = scheduleNo + 1;

                    // Untuk keperluan pembuatan script edit
                    medRecUsed = new MedicationReceiveUsed();
                    medRecUsed.MedicationReceiveNo = medrecno.ToInt();

                    // Jika belum pernah setup maka munculkan jam dari schedule
                    timeScheduleDisplay = cm.GetColumn(string.Format("Time{0:00}", scheduleNo)).ToString();
                    timeScheduleDisplays = timeScheduleDisplay.Split(':');

                    if (isAntibiotic)
                    {
                        if (lastAntibioticScheduled.Year == 1900 || antibioticScheduledCount < 2)
                        {
                            // Display undefined schedule, antibiotic baru digenerate schedulenya setelah konsumsi ke 2
                            // dg jarak waktu = 24jam/brp kali konsumsi
                            timeScheduleDisplay = "--:--";
                        }
                        else
                        {
                            // Next schedule
                            lastAntibioticScheduled = lastAntibioticScheduled.AddMinutes(antibioticMinuteRange);

                            //Skip jika timeScheduleDisplay sudah melewati currentDate untuk kolom current Date
                            if (lastAntibioticScheduled.Date > scriptForScheduleDate.Date)
                                continue;

                            // Check tgl schedule
                            while (scriptForScheduleDate.Date > lastAntibioticScheduled.Date)
                            {
                                lastAntibioticScheduled = lastAntibioticScheduled.AddMinutes(antibioticMinuteRange);
                            }

                            timeScheduleDisplay = string.Format("{0:00}:{1:00}", lastAntibioticScheduled.Hour,
                                lastAntibioticScheduled.Minute);
                            timeScheduleDisplays = timeScheduleDisplay.Split(':');
                        }
                    }
                    else
                    {
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
                    timeScheduleDisplay, scriptForScheduleDate, entryMenuCreated);

                balanceQty = balanceQty - (medRecUsed.SetupDateTime != null ? medRecUsed.Qty.ToDecimal() : consumeQty);
                if (balanceQty <= 0)
                {
                    break;
                }
            }

            // Untuk Extra order atau pemberian obat diluar schedule
            if (MedicationStep == "S" && dayFromCurrentDay == 0 && balanceQty > 0)
            {
                strb.AppendFormat(
                    "&nbsp;<a style='vertical-align: text-bottom;' href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('new', '{0}', 0,'','{1}','{2}','1'); return false;\"><img src=\"../../../Images/Toolbar/insert16.png\"  alt=\"New\" /></a>&nbsp;|",
                    medrecno, patientID, dayFromCurrentDay);
            }

            _isCurrentDayFinish = histCount >= (cm.IterationQty - skippedSchedule);

            var retval = strb.ToString();
            return !string.IsNullOrEmpty(retval) ? retval.Substring(0, retval.Length - 2) : string.Empty;
        }

        private string MedicationOutOffScheduleHtml(int medrecno, int dayFromCurrentDay, string patientID, decimal balanceQty)
        {
            var strb = new StringBuilder();

            // Cari yg dari hasil schedule manual entry
            var qrUsed = new MedicationReceiveUsedQuery();
            var usedColls = new MedicationReceiveUsedCollection();

            if (MedicationStep == "S")
                qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.SetupDateTime.IsNotNull());
            else if (MedicationStep == "V")
                qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.SetupDateTime.IsNotNull(),
                    qrUsed.VerificationDateTime.IsNull());
            else
                qrUsed.Where(qrUsed.MedicationReceiveNo == medrecno, qrUsed.SetupDateTime.IsNotNull(), qrUsed.VerificationDateTime.IsNotNull(),
                    qrUsed.RealizedDateTime.IsNull());

            var scriptForScheduleDate = SetupStartDate.AddDays(dayFromCurrentDay);

            qrUsed.Where(qrUsed.ScheduleDateTime >= scriptForScheduleDate,
                qrUsed.ScheduleDateTime < scriptForScheduleDate.AddDays(1));

            qrUsed.OrderBy(qrUsed.ScheduleDateTime.Ascending);
            if (usedColls.Load(qrUsed))
            {
                foreach (var used in usedColls)
                {
                    var time = Convert.ToDateTime(used.ScheduleDateTime);
                    if (MedicationStep == "S")
                    {
                        var color = "black";

                        if (used.SetupDateTime != null)
                            color = "red";
                        if (used.VerificationDateTime != null)
                            color = "darkorange";
                        if (used.RealizedDateTime != null)
                            color = "green";
                        strb.AppendFormat(
                            "&nbsp;<a style=\"color: {2};pointer-events: none;cursor: default;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/post16_d.png\"  alt=\"New\" />&nbsp;{0:00}:{1:00}</a>&nbsp;|",
                            time.Hour, time.Minute, color);
                    }
                    else
                    {
                        var medicationMode = MedicationStep == "S" ? "new" : "edit";
                        strb.AppendFormat(
                            "&nbsp;<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('{3}', '{0}', '{1}', '{2}', '{4}','1'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/post16.png\"  alt=\"New\" />&nbsp;{2}</a>&nbsp;|",
                            medrecno, used.SequenceNo ?? 0, string.Format("{0:00}:{1:00}", time.Hour, time.Minute),
                            medicationMode,
                            dayFromCurrentDay);
                    }
                    balanceQty = balanceQty - (used.SetupDateTime != null ? used.Qty.ToDecimal() : 0);
                }
            }

            if (balanceQty > 0)
                // Untuk pemberian obat diluar schedule
                if (MedicationStep == "S" && dayFromCurrentDay < 2)
                    strb.Insert(0, string.Format(
                        "<a style='vertical-align: text-bottom;' href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('new', '{0}', 0,'','{1}','{2}','1'); return false;\"><img src=\"../../../Images/Toolbar/insert16.png\"  alt=\"New\" /></a>&nbsp;|",
                        medrecno, patientID, dayFromCurrentDay));

            var addScript = strb.ToString();
            return string.IsNullOrEmpty(addScript)
                ? string.Empty
                : addScript.Substring(0, addScript.Length - 2);
        }

        private bool PopulateMedicationUsedEntryScript(StringBuilder strb, MedicationReceiveUsed medRecUsed, int dayFromCurrentDay, string patientID, string timeScheduleDisplay, DateTime dateAfterAdded, bool isEntryMenuHasCreated)
        {
            // Schedule untuk hari ini dan besok
            if (dayFromCurrentDay == 0 || (dayFromCurrentDay == 1 && _isCurrentDayFinish))
            {
                // Hanya bisa 1 schedule yg dientry
                if (!isEntryMenuHasCreated && medRecUsed.IsVoidSchedule != true &&
                    ((MedicationStep == "S" && medRecUsed.SetupDateTime == null)
                     || ((MedicationStep == "V" && medRecUsed.SetupDateTime != null)
                         && (MedicationStep == "V" &&
                             medRecUsed.VerificationDateTime == null))
                     || ((MedicationStep == "R" && medRecUsed.VerificationDateTime != null)
                         && (MedicationStep == "R" && medRecUsed.RealizedDateTime == null))
                    ))
                {
                    isEntryMenuHasCreated = true; // Beri flag untuk mencegah script entry dibawah dibuat > 1x
                    var medicationMode = MedicationStep == "S" ? "new" : "edit";
                    strb.AppendFormat(
                        "&nbsp;<a  href=\"#\"  onclick=\"javascript:entryMedicationReceiveUsed('{3}', '{0}', '{1}', '{2}','{4}','{5}','0'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/post16.png\"  alt=\"New\" />&nbsp;{2}</a>&nbsp;|",
                        medRecUsed.MedicationReceiveNo, medRecUsed.SequenceNo ?? 0, timeScheduleDisplay, medicationMode, patientID, dayFromCurrentDay);
                }
                else
                {
                    var color = "black";
                    if (medRecUsed.SetupDateTime != null)
                        color = "red";
                    if (medRecUsed.VerificationDateTime != null)
                        color = "darkorange";
                    if (medRecUsed.RealizedDateTime != null)
                        color = "green";

                    strb.Append("&nbsp;");
                    if (medRecUsed.IsVoidSchedule == true)
                        strb.Append("<del>");

                    strb.AppendFormat(
                        "<a style=\"color: {1};pointer-events: none;cursor: default;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/post16_d.png\"  alt=\"New\" />&nbsp;{0}</a>&nbsp;|",
                        timeScheduleDisplay, color);

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
                GridDataItem item = (GridDataItem)e.Item;
                if (item.GetDataKeyValue("IsOddBackground") == DBNull.Value) return;
                var isOdd = Convert.ToBoolean(item.GetDataKeyValue("IsOddBackground"));
                item.CssClass = isOdd ? "rgOdd" : "rgEven";

                if (true.Equals(item.GetDataKeyValue("IsAntibiotic")))
                {
                    //item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    var cell = item["ItemDescription"];
                    cell.Style.Add(HtmlTextWriterStyle.Color, "red");
                }

                if (false.Equals(item.GetDataKeyValue("IsContinue")))
                {
                    item.Style.Add(HtmlTextWriterStyle.Color, "gray");
                }
            }

        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie(txtPatientName, cboServiceUnitID, cboRoomID, chkIsIncludeFinished);

            grdMedicationStatus.Columns[1].Visible = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboRoomID.SelectedValue);
            grdMedicationStatus.Columns[2].Visible = string.IsNullOrEmpty(cboRoomID.SelectedValue);

            grdMedicationStatus.Rebind();
        }

        protected string MedicationChangeConsumeMethodHtml(object medNo, object conmtd, object patientID, object balanceQty)
        {
            if (MedicationStep != "S") return string.Empty;
            if (balanceQty.ToDecimal() == 0) return string.Empty;
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
        protected string MedicationUsedEditHtml(object medNo, object seqNo, object scheduleDateTime, object verificationDateTime, object realizationDateTime)
        {
            var isEditable = scheduleDateTime != DBNull.Value && ((MedicationStep == "S" && verificationDateTime == DBNull.Value)
                                                                                                  || (MedicationStep == "V" && verificationDateTime != DBNull.Value && realizationDateTime == DBNull.Value)
                                                                                                  || MedicationStep == "R" && realizationDateTime != DBNull.Value);
            return isEditable ? string.Format(
                "<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveUsed('edit', '{0}', '{1}', '','','','0'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",
                medNo, seqNo) : string.Empty;
        }

        protected object MedicationScheduleGridDetailHtml(GridItem container, string realizationType)
        {
            if (realizationType == "V")
            {
                if (DataBinder.Eval(container.DataItem, "VerificationDateTime") != DBNull.Value)
                    return Convert.ToDateTime(DataBinder.Eval(container.DataItem, "VerificationDateTime"))
                        .ToString(AppConstant.DisplayFormat.DateHourMinute);
                if (MedicationStep == "V")
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

        protected void grdMedicationStatus_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdMedicationStatus.DataSource = MedicationReceiveDataTable();
        }
    }
}
