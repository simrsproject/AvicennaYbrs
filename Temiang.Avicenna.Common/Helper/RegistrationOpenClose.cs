using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public class RegistrationOpenClose
        {
            public static void SetVoid(Registration entity, string programName)
            {
                var serverDate = (new DateTime()).NowAtSqlServer();
                var regs = new RegistrationCollection();
                if (AppSession.Parameter.IsIndependentVoidRegistration)
                {
                    regs.Query.Where(regs.Query.RegistrationNo.In(entity.RegistrationNo));

                    var mrgs = new BusinessObject.MergeBillingCollection();
                    mrgs.Query.Where(mrgs.Query.FromRegistrationNo == entity.RegistrationNo);
                    mrgs.Query.OrderBy(mrgs.Query.RegistrationNo.Ascending);
                    mrgs.LoadAll();

                    var idx = 0;
                    var regNo = string.Empty;
                    foreach (var mrg in mrgs)
                    {
                        if (idx == 0)
                        {
                            mrg.FromRegistrationNo = string.Empty;
                            regNo = mrg.RegistrationNo;
                        }
                        else
                            mrg.FromRegistrationNo = regNo;
                        mrg.LastUpdateDateTime = serverDate;
                        mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        idx++;
                    }

                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(regNo))
                    {
                        reg.IsConsul = false;
                        reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        reg.LastUpdateDateTime = serverDate;

                        reg.Save();
                    }

                    mrgs.Save();
                }
                else
                    regs.Query.Where(regs.Query.RegistrationNo.In(MergeBilling.GetMergeRegistration(entity.RegistrationNo)));
                regs.LoadAll();

                var historys = new RegistrationCloseOpenHistoryCollection();
                
                foreach (var re in regs)
                {
                    re.IsClosed = true;
                    re.IsVoid = true;
                    re.VoidByUserID = AppSession.UserLogin.UserID;
                    re.VoidDate = serverDate;
                    re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    re.LastUpdateDateTime = serverDate;

                    var hist = historys.AddNew();
                    hist.RegistrationNo = re.RegistrationNo;
                    hist.StatusId = "C";
                    hist.IsTrue = re.IsClosed;
                    hist.Notes = programName;
                    hist.LastUpdateDateTime = serverDate;
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    switch (re.SRRegistrationType)
                    {
                        case AppConstant.RegistrationType.ClusterPatient:
                        case AppConstant.RegistrationType.OutPatient:
                            var ques = new ServiceUnitQueCollection();
                            ques.Query.Where(ques.Query.RegistrationNo == re.RegistrationNo);
                            ques.LoadAll();

                            ques.MarkAllAsDeleted();
                            ques.Save();

                            if (!string.IsNullOrEmpty(re.VisiteRegistrationNo))
                            {
                                var payment = new TransPayment();
                                payment.Query.Where(payment.Query.RegistrationNo == re.RegistrationNo);
                                payment.Query.Load();

                                payment.IsVoid = true;
                                payment.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                payment.LastUpdateDateTime = serverDate;
                                payment.Save();

                                var visites = new TransPaymentItemVisiteCollection();
                                visites.Query.Where(visites.Query.PaymentNo == re.VisiteRegistrationNo);
                                visites.Query.Load();

                                foreach (var visite in visites)
                                {
                                    visite.RealizationQty--;
                                    visite.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    visite.LastUpdateDateTime = serverDate;
                                }

                                visites.Save();
                            }
                            break;
                        case AppConstant.RegistrationType.InPatient:
                            if (!string.IsNullOrEmpty(re.AppointmentNo))
                            {
                                var appt = new Reservation();
                                if (appt.LoadByPrimaryKey(re.AppointmentNo))
                                {
                                    appt.SRReservationStatus = AppSession.Parameter.AppointmentStatusOpen;
                                    appt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    appt.LastUpdateDateTime = serverDate;
                                    appt.Save();
                                }
                            }

                            if (re.IsRoomIn ?? false)
                            {
                                var br = new BedRoomIn();
                                br.Query.Where(br.RegistrationNo == re.RegistrationNo);
                                if (br.Query.Load())
                                {
                                    br.DateOfExit = re.VoidDate.Value.Date;
                                    br.TimeOfExit = re.VoidDate.Value.ToString("HH:mm");
                                    br.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    br.LastUpdateDateTime = serverDate;
                                }
                            }

                            var b = new Bed();
                            if (b.LoadByPrimaryKey(re.str.BedID))
                            {
                                b.RegistrationNo = string.Empty;
                                b.SRBedStatus = AppSession.Parameter.BedStatusUnoccupied;
                                b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                b.LastUpdateDateTime = serverDate;
                                b.IsRoomIn = false;

                                b.Save();
                            }
                            break;
                    }
                }

                regs.Save();
                historys.Save();
            }

            public static void SetVoid(Registration entity, string voidReason, string notes, string programName)
            {
                var serverDate = (new DateTime()).NowAtSqlServer();
                var regs = new RegistrationCollection();

                var historys = new RegistrationCloseOpenHistoryCollection();

                if (AppSession.Parameter.IsIndependentVoidRegistration)
                {
                    regs.Query.Where(regs.Query.RegistrationNo == entity.RegistrationNo);

                    var mrgs = new BusinessObject.MergeBillingCollection();
                    mrgs.Query.Where(mrgs.Query.FromRegistrationNo == entity.RegistrationNo);
                    mrgs.Query.OrderBy(mrgs.Query.RegistrationNo.Ascending);
                    mrgs.LoadAll();

                    var idx = 0;
                    var regNo = string.Empty;
                    foreach (var mrg in mrgs)
                    {
                        if (idx == 0)
                        {
                            mrg.FromRegistrationNo = string.Empty;
                            regNo = mrg.RegistrationNo;
                        }
                        else
                            mrg.FromRegistrationNo = regNo;
                        mrg.LastUpdateDateTime = serverDate;
                        mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        idx++;
                    }

                    if (AppParameter.IsYes(AppParameter.ParameterItem.IsAllowVoidRegistrationOnTransfer))
                    {
                        var transferColl = new PatientTransferCollection();
                        transferColl.Query.Where(transferColl.Query.RegistrationNo == entity.RegistrationNo,
                                                 transferColl.Query.IsVoid == false);
                        transferColl.LoadAll();
                        foreach (var tf in transferColl) {
                            tf.IsVoid = true;
                            tf.IsApprove = false;
                            tf.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            tf.LastUpdateDateTime = serverDate;
                        }

                        transferColl.Save();
                    }

                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(regNo))
                    {
                        reg.IsConsul = false;
                        reg.IsClosed = false;
                        reg.IsTransferedToInpatient = false;
                        reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        reg.LastUpdateDateTime = serverDate;

                        reg.Save();

                        var hist = historys.AddNew();
                        hist.RegistrationNo = reg.RegistrationNo; 
                        hist.StatusId = "C";
                        hist.IsTrue = reg.IsClosed;
                        hist.Notes = programName;
                        hist.LastUpdateDateTime = serverDate;
                        hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    mrgs.Save();
                }
                else
                    regs.Query.Where(regs.Query.RegistrationNo.In(MergeBilling.GetMergeRegistration(entity.RegistrationNo)));
                regs.LoadAll();

                foreach (var re in regs)
                {
                    re.IsClosed = true;
                    re.IsVoid = true;
                    re.SRVoidReason = voidReason;
                    re.VoidNotes = notes;
                    re.VoidByUserID = AppSession.UserLogin.UserID;
                    re.VoidDate = serverDate;
                    re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    re.LastUpdateDateTime = serverDate;

                    switch (re.SRRegistrationType)
                    {
                        case AppConstant.RegistrationType.ClusterPatient:
                        case AppConstant.RegistrationType.OutPatient:
                            // void registrasi tidak perlu balikin status appointmentnya, kalau mau dibalikin confirm bangte dl
                            //if (!string.IsNullOrEmpty(re.AppointmentNo))
                            //{
                            //    var appt = new BusinessObject.Appointment();
                            //    if (appt.LoadByPrimaryKey(re.AppointmentNo))
                            //    {
                            //        appt.SRAppointmentStatus = AppSession.Parameter.AppointmentStatusConfirmed;
                            //        appt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            //        appt.LastUpdateDateTime = serverDate;
                            //        appt.Save();
                            //    }
                            //}

                            var ques = new ServiceUnitQueCollection();
                            ques.Query.Where(ques.Query.RegistrationNo == re.RegistrationNo);
                            ques.LoadAll();

                            ques.MarkAllAsDeleted();
                            ques.Save();

                            if (!string.IsNullOrEmpty(re.VisiteRegistrationNo))
                            {
                                var payment = new TransPayment();
                                payment.Query.Where(payment.Query.RegistrationNo == re.RegistrationNo);
                                payment.Query.Load();

                                payment.IsVoid = true;
                                payment.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                payment.LastUpdateDateTime = serverDate;
                                payment.Save();

                                var visites = new TransPaymentItemVisiteCollection();
                                visites.Query.Where(visites.Query.PaymentNo == re.VisiteRegistrationNo);
                                visites.Query.Load();

                                foreach (var visite in visites)
                                {
                                    visite.RealizationQty--;
                                    visite.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    visite.LastUpdateDateTime = serverDate;
                                }

                                visites.Save();
                            }
                            break;
                        case AppConstant.RegistrationType.InPatient:
                            if (!string.IsNullOrEmpty(re.AppointmentNo))
                            {
                                var appt = new Reservation();
                                if (appt.LoadByPrimaryKey(re.AppointmentNo))
                                {
                                    appt.SRReservationStatus = AppSession.Parameter.AppointmentStatusOpen;
                                    appt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    appt.LastUpdateDateTime = serverDate;
                                    appt.Save();
                                }
                            }

                            if (re.IsRoomIn ?? false)
                            {
                                var br = new BedRoomIn();
                                br.Query.Where(br.RegistrationNo == re.RegistrationNo);
                                if (br.Query.Load())
                                {
                                    br.DateOfExit = re.VoidDate.Value.Date;
                                    br.TimeOfExit = re.VoidDate.Value.ToString("HH:mm");
                                    br.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    br.LastUpdateDateTime = serverDate;
                                }
                            }

                            var b = new Bed();
                            if (b.LoadByPrimaryKey(re.str.BedID))
                            {
                                b.RegistrationNo = string.Empty;
                                b.SRBedStatus = AppSession.Parameter.BedStatusUnoccupied;
                                b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                b.LastUpdateDateTime = serverDate;
                                b.IsRoomIn = false;

                                b.Save();
                            }

                            var h = new PatientTransferHistoryCollection();
                            h.Query.Where(h.Query.RegistrationNo == re.RegistrationNo);
                            h.Query.Load();

                            if (h.Any())
                            {
                                h.MarkAllAsDeleted();
                                h.Save();
                            }

                            break;
                    }

                    var que = new ServiceUnitQueCollection();
                    que.Query.Where(que.Query.RegistrationNo == re.RegistrationNo);
                    que.LoadAll();
                    if (que.Any())
                    {
                        que.MarkAllAsDeleted();
                        que.Save();
                    }

                    // Kurangi Visite Order Realization
                    var visiteReals = new TransChargesVisiteItemRealizationCollection();
                    visiteReals.Query.Where(visiteReals.Query.RegistrationNo == re.RegistrationNo);
                    visiteReals.LoadAll();
                    foreach (var real in visiteReals)
                    {
                        var visiteOrder = new TransChargesVisiteItem();
                        if (visiteOrder.LoadByPrimaryKey(real.TransactionNo, real.ItemID))
                        {
                            if (visiteOrder.RealizationQty == visiteOrder.VisiteQty && visiteOrder.IsClosed ==true)
                                visiteOrder.IsClosed = false; // Open

                            visiteOrder.RealizationQty = visiteOrder.RealizationQty - 1;
                            visiteOrder.Save();
                        }
                    }

                    var hist = historys.AddNew();
                    hist.RegistrationNo = re.RegistrationNo;
                    hist.StatusId = "C";
                    hist.IsTrue = re.IsClosed;
                    hist.Notes = programName;
                    hist.LastUpdateDateTime = serverDate;
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                var c = new Patient();
                if (c.LoadByPrimaryKey(entity.PatientID))
                {
                    c.NumberOfVisit = (byte)(c.NumberOfVisit - 1);
                    c.Save();
                }

                var m = new MergeBillingCollection();
                m.Query.Where(m.Query.RegistrationNo == entity.RegistrationNo);
                m.LoadAll();
                foreach (var item in m)
                {
                    item.FromRegistrationNo = string.Empty;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = serverDate;
                }
                m.Save();

                // gak usah hapus MedicalRecordFileStatus
                //var mrf = new MedicalRecordFileStatus();
                //if (mrf.LoadByPrimaryKey(entity.RegistrationNo))
                //{
                //    mrf.MarkAsDeleted();
                //    mrf.Save();
                //}

                regs.Save();
                historys.Save();
            }

            public static void SetClosed(string registrationNo, bool isClosed, string programName)
            {
                var serverDate = (new DateTime()).NowAtSqlServer();
                var entity = new Registration();
                entity.LoadByPrimaryKey(registrationNo);

                entity.IsClosed = isClosed;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = serverDate;

                var historys = new RegistrationCloseOpenHistoryCollection();
                var hist = historys.AddNew();
                hist.RegistrationNo = entity.RegistrationNo;
                hist.StatusId = "C";
                hist.IsTrue = entity.IsClosed;
                hist.Notes = programName;
                hist.LastUpdateDateTime = serverDate;
                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;

                if (entity.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient ||
                    entity.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                {
                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)));
                    regs.LoadAll();

                    foreach (var re in regs.Where(re => re.SRRegistrationType != AppConstant.RegistrationType.InPatient))
                    {
                        re.IsClosed = isClosed;
                        re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        re.LastUpdateDateTime = serverDate;

                        if (re.RegistrationNo != entity.RegistrationNo)
                        {
                            var hist2 = historys.AddNew();
                            hist2.RegistrationNo = re.RegistrationNo;
                            hist2.StatusId = "C";
                            hist2.IsTrue = re.IsClosed;
                            hist2.Notes = programName;
                            hist2.LastUpdateDateTime = serverDate;
                            hist2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                    }

                    var ques = new ServiceUnitQueCollection();
                    ques.Query.Where(ques.Query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)));
                    ques.LoadAll();

                    foreach (var que in ques)
                    {
                        que.IsClosed = isClosed;
                        que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        que.LastUpdateDateTime = serverDate;
                    }

                    using (var trans = new esTransactionScope())
                    {
                        if (regs.Count > 0)
                            regs.Save();

                        if (ques.Count > 0)
                            ques.Save();

                        if (historys.Count > 0)
                            historys.Save();

                        trans.Complete();
                    }
                }
                else if (entity.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    if (!entity.DischargeDate.HasValue && entity.IsFromDispensary == false)
                        return;

                    var mrg = new BusinessObject.MergeBilling();
                    mrg.LoadByPrimaryKey(entity.RegistrationNo);

                    if (mrg.FromRegistrationNo == string.Empty)
                    {
                        using (var trans = new esTransactionScope())
                        {
                            entity.Save();

                            trans.Complete();
                        }
                    }
                    else
                    {
                        var coll = new MergeBillingCollection();
                        coll.Query.Where(coll.Query.FromRegistrationNo == mrg.FromRegistrationNo);
                        coll.LoadAll();

                        var reg = new string[coll.Count + 1];
                        var idx = 1;

                        reg.SetValue(mrg.FromRegistrationNo, 0);

                        foreach (var bill in coll)
                        {
                            reg.SetValue(bill.RegistrationNo, idx);
                            idx++;
                        }

                        var regs = new RegistrationCollection();
                        regs.Query.Where(regs.Query.RegistrationNo.In(reg));
                        regs.LoadAll();

                        foreach (var re in regs)
                        {
                            re.IsClosed = isClosed;
                            re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            re.LastUpdateDateTime = serverDate;

                            if (re.RegistrationNo != entity.RegistrationNo)
                            {
                                var hist2 = historys.AddNew();
                                hist2.RegistrationNo = re.RegistrationNo;
                                hist2.StatusId = "C";
                                hist2.IsTrue = re.IsClosed;
                                hist2.Notes = programName;
                                hist2.LastUpdateDateTime = serverDate;
                                hist2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }

                        using (var trans = new esTransactionScope())
                        {
                            if (regs.Count > 0)
                                regs.Save();

                            if (historys.Count > 0)
                                historys.Save();

                            trans.Complete();
                        }
                    }
                }
                else if (entity.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient ||
                         entity.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp)
                {
                    using (var trans = new esTransactionScope())
                    {
                        entity.Save();
                        historys.Save();

                        trans.Complete();
                    }
                }
            }

            public static void SetClosed(string registrationNo, string programName)
            {
                var serverDate = (new DateTime()).NowAtSqlServer();
                var entity = new Registration();
                entity.LoadByPrimaryKey(registrationNo);

                entity.IsClosed = !entity.IsClosed;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = serverDate;

                var historys = new RegistrationCloseOpenHistoryCollection();
                var hist = historys.AddNew();
                hist.RegistrationNo = registrationNo;
                hist.StatusId = "C";
                hist.IsTrue = entity.IsClosed;
                hist.Notes = programName;
                hist.LastUpdateDateTime = serverDate;
                hist.LastUpdateByUserID = AppSession.UserLogin.UserID;

                if (entity.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient ||
                    entity.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                {
                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)));
                    regs.LoadAll();

                    foreach (var re in regs.Where(re => re.SRRegistrationType != AppConstant.RegistrationType.InPatient))
                    {
                        re.IsClosed = entity.IsClosed;
                        re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        re.LastUpdateDateTime = serverDate;

                        if (re.RegistrationNo != registrationNo)
                        {
                            var hist2 = historys.AddNew();
                            hist2.RegistrationNo = re.RegistrationNo;
                            hist2.StatusId = "C";
                            hist2.IsTrue = entity.IsClosed;
                            hist2.Notes = programName;
                            hist2.LastUpdateDateTime = serverDate;
                            hist2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                    }

                    var ques = new ServiceUnitQueCollection();
                    ques.Query.Where(ques.Query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(registrationNo)));
                    ques.LoadAll();

                    foreach (var que in ques)
                    {
                        que.IsClosed = entity.IsClosed;
                        que.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        que.LastUpdateDateTime = serverDate;
                    }

                    using (var trans = new esTransactionScope())
                    {
                        if (regs.Count > 0)
                            regs.Save();

                        if (ques.Count > 0)
                            ques.Save();

                        if (historys.Count > 0)
                            historys.Save();

                        trans.Complete();
                    }
                }
                else if (entity.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    var mrg = new BusinessObject.MergeBilling();
                    mrg.LoadByPrimaryKey(entity.RegistrationNo);

                    if (mrg.FromRegistrationNo == string.Empty)
                    {
                        var coll = new MergeBillingCollection();
                        coll.Query.Where(coll.Query.FromRegistrationNo == mrg.RegistrationNo);
                        coll.LoadAll();

                        var reg = new string[coll.Count + 1];
                        var idx = 1;

                        reg.SetValue(mrg.RegistrationNo, 0);

                        foreach (var bill in coll)
                        {
                            reg.SetValue(bill.RegistrationNo, idx);
                            idx++;
                        }

                        var regs = new RegistrationCollection();
                        regs.Query.Where(regs.Query.RegistrationNo.In(reg));
                        regs.LoadAll();

                        foreach (var re in regs)
                        {
                            re.IsClosed = entity.IsClosed;
                            re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            re.LastUpdateDateTime = serverDate;

                            if (re.RegistrationNo != registrationNo)
                            {
                                var hist2 = historys.AddNew();
                                hist2.RegistrationNo = re.RegistrationNo;
                                hist2.StatusId = "C";
                                hist2.IsTrue = entity.IsClosed;
                                hist2.Notes = programName;
                                hist2.LastUpdateDateTime = serverDate;
                                hist2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }

                        using (var trans = new esTransactionScope())
                        {
                            entity.Save();
                            regs.Save();
                            historys.Save();

                            trans.Complete();
                        }
                    }
                    else
                    {
                        var coll = new MergeBillingCollection();
                        coll.Query.Where(coll.Query.FromRegistrationNo == mrg.FromRegistrationNo);
                        coll.LoadAll();

                        var reg = new string[coll.Count + 1];
                        var idx = 1;

                        reg.SetValue(mrg.FromRegistrationNo, 0);

                        foreach (var bill in coll)
                        {
                            reg.SetValue(bill.RegistrationNo, idx);
                            idx++;
                        }

                        var regs = new RegistrationCollection();
                        regs.Query.Where(regs.Query.RegistrationNo.In(reg));
                        regs.LoadAll();

                        foreach (var re in regs)
                        {
                            re.IsClosed = entity.IsClosed;
                            re.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            re.LastUpdateDateTime = serverDate;

                            if (re.RegistrationNo != registrationNo)
                            {
                                var hist2 = historys.AddNew();
                                hist2.RegistrationNo = re.RegistrationNo;
                                hist2.StatusId = "C";
                                hist2.IsTrue = entity.IsClosed;
                                hist2.Notes = programName;
                                hist2.LastUpdateDateTime = serverDate;
                                hist2.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }

                        using (var trans = new esTransactionScope())
                        {
                            if (regs.Count > 0)
                                regs.Save();

                            if (historys.Count > 0)
                                historys.Save();

                            trans.Complete();
                        }
                    }
                }
                else if (entity.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient ||
                         entity.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp)
                {
                    using (var trans = new esTransactionScope())
                    {
                        entity.Save();
                        historys.Save();

                        trans.Complete();
                    }
                }
            }

            public static void EditPhysician(Registration reg, string paramedicId, string paramedicName, string queId, string queText, string physicianSender, string shiftId)
            {
                using (var trans = new esTransactionScope())
                {
                    var charges = new TransCharges();
                    var chargesQr = new TransChargesQuery();
                    chargesQr.Where(chargesQr.RegistrationNo == reg.RegistrationNo, chargesQr.IsAutoBillTransaction == true);
                    chargesQr.es.Top = 1;
                    if (chargesQr.LoadDataTable().Rows.Count > 0)
                    {
                        charges.Load(chargesQr);
                        var tciccoll = new TransChargesItemCompCollection();
                        if (!string.IsNullOrEmpty(reg.ParamedicID))
                        {
                            tciccoll.Query.Where(
                            tciccoll.Query.TransactionNo == charges.TransactionNo,
                            tciccoll.Query.ParamedicID == reg.ParamedicID
                            );
                            tciccoll.LoadAll();

                            foreach (var tcic in tciccoll)
                            {
                                tcic.ParamedicID = paramedicId;

                                var tci = new TransChargesItem();
                                tci.LoadByPrimaryKey(tcic.TransactionNo, tcic.SequenceNo);
                                tci.ParamedicCollectionName = paramedicName;
                                tci.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                tci.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                tci.Save();
                            }
                            tciccoll.Save();
                        }
                        else
                        {
                            tciccoll.Query.Where(
                                    tciccoll.Query.TransactionNo == charges.TransactionNo,
                                    tciccoll.Query.Or(tciccoll.Query.ParamedicID == string.Empty, tciccoll.Query.ParamedicID.IsNull())
                                    );
                            tciccoll.LoadAll();

                            foreach (var tcic in tciccoll)
                            {
                                tcic.ParamedicID = paramedicId;

                                var tci = new TransChargesItem();
                                tci.LoadByPrimaryKey(tcic.TransactionNo, tcic.SequenceNo);
                                tci.ParamedicCollectionName = paramedicName;
                                tci.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                tci.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                                tci.Save();
                            }
                            tciccoll.Save();
                        }

                        if (AppSession.Parameter.IsPhysicianFeeBasedOnPayment)
                        {
                            var x = ParamedicFeeTransChargesItemCompSettled.UpdateSettled(charges, tciccoll, AppSession.UserLogin.UserID);

                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetFeeByTCIC(tciccoll, AppSession.UserLogin.UserID);
                            feeColl.Save();
                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                            //feeColl.Save();
                        }
                    }

                    if (reg.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient ||
                        reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                    {
                        var query = new ServiceUnitQueQuery();
                        if (string.IsNullOrEmpty(reg.ParamedicID))
                        {
                            query.Where(
                            query.ServiceUnitID == reg.ServiceUnitID,
                            query.ParamedicID == string.Empty,
                            query.RegistrationNo == reg.RegistrationNo
                            );
                        }
                        else
                            query.Where(
                            query.ServiceUnitID == reg.ServiceUnitID,
                            query.ParamedicID == reg.ParamedicID,
                            query.RegistrationNo == reg.RegistrationNo
                            );

                        var q = new ServiceUnitQue();
                        if (q.Load(query))
                        {
                            q.MarkAsDeleted();
                            q.Save();

                            q = new ServiceUnitQue();
                            q.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                            q.RegistrationNo = reg.RegistrationNo;
                            q.ParamedicID = paramedicId;
                            q.ServiceUnitID = reg.ServiceUnitID;

                            var sch = new ParamedicScheduleDate();
                            if (sch.LoadByPrimaryKey(reg.ServiceUnitID, paramedicId,
                                                     reg.RegistrationDate.Value.Year.ToString(), reg.RegistrationDate.Value.Date))
                            {
                                var sp = new ServiceUnitParamedic();
                                if (sp.LoadByPrimaryKey(reg.ServiceUnitID, paramedicId))
                                {
                                    q.QueNo = !string.IsNullOrEmpty(queId) ? int.Parse(queText.Split('-')[0].Trim()) :
                                        ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, paramedicId, reg.RegistrationDate ?? (new DateTime()).NowAtSqlServer().Date);

                                    if (!string.IsNullOrEmpty(queId))
                                    {
                                        string value = queText.Split('-')[1].Substring(1);
                                        DateTime dt;
                                        DateTime.TryParse(value, out dt);
                                        reg.RegistrationTime = dt.ToString("HH:mm");

                                        q.QueDate = reg.RegistrationDate.Value + TimeSpan.Parse(reg.RegistrationTime);
                                    }
                                }
                                else
                                    q.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, paramedicId, reg.RegistrationDate.Value.Date);
                            }
                            else
                                q.QueNo = ServiceUnitQue.GetNewQueNo(reg.ServiceUnitID, paramedicId, reg.RegistrationDate.Value.Date);

                            q.ServiceRoomID = reg.RoomID;
                            q.IsFromReferProcess = false;
                            q.StartTime = q.QueDate;
                            q.IsStopped = false;
                            q.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            q.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            q.Save();
                        }

                        reg.RegistrationQue = q.QueNo;
                    }

                    reg.ParamedicID = paramedicId;
                    reg.PhysicianSenders = physicianSender;

                    if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                        reg.SRShift = shiftId;

                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    reg.Save();

                    if (GuarantorBPJS.Contains(reg.GuarantorID))
                    {
                        var bpjs = new BpjsSEP();
                        bpjs.Query.Where(bpjs.Query.NoSEP == reg.BpjsSepNo);
                        if (bpjs.Query.Load())
                        {
                            var pb = new ParamedicBridging();
                            pb.Query.Where(pb.Query.ParamedicID == reg.ParamedicID && pb.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(), AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                            if (pb.Query.Load())
                            {
                                if (bpjs.KodeDpjpPelayanan != pb.BridgingID)
                                {
                                    bpjs.KodeDpjpPelayanan = pb.BridgingID;
                                    bpjs.Save();
                                }
                            }
                        }
                    }

                    trans.Complete();
                }
            }

            private static string[] GuarantorBPJS
            {
                get
                {
                    var grr = new GuarantorBridgingCollection();
                    grr.Query.es.Distinct = true;
                    grr.Query.Where(grr.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(), AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString()));
                    if (grr.Query.Load()) return grr.Select(g => g.GuarantorID).ToArray();
                    else return new string[] { string.Empty };
                }
            }

            #region Automatic Closed from payment process
            public static bool GetStatusClosed(Registration registration, string[] registrationNo, decimal patPaymentAmount, decimal guarPaymentAmount)
            {
                var guarantor = new Guarantor();
                guarantor.LoadByPrimaryKey(registration.GuarantorID);

                var patientParam = new string[2];
                patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
                patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

                decimal tpatient, tguarantor;
                CostCalculation.GetBillingTotal(registrationNo, registration.SRBussinesMethod, (registration.PlavonAmount ?? 0), out tpatient, out tguarantor,
                                                       guarantor, registration.IsGlobalPlafond ?? false);

                decimal treturn = Payment.GetTotalPayment(registrationNo, false);
                decimal tPatientPaymentAmt = Payment.GetTotalPayment(registrationNo, true, patientParam) + patPaymentAmount;
                decimal tGuarantorPaymentAmt = Payment.GetTotalPayment(registrationNo, true, AppSession.Parameter.PaymentTypeCorporateAR) + guarPaymentAmount;

                var discPatient = Payment.GetPaymentDiscount(registrationNo, false);
                var discGuarantor = Payment.GetPaymentDiscount(registrationNo, true);

                var patTotalPayment = tPatientPaymentAmt + treturn;
                var guarTotalPayment = tGuarantorPaymentAmt;

                var patRemaining = tpatient - patTotalPayment - discPatient;
                var guarRemaining = tguarantor - guarTotalPayment - discGuarantor;

                return patRemaining + guarRemaining <= 0;
            }

            #endregion

            #region Automatic Set Discharge Date For Reg RI on payment process
            /// <summary>
            /// Automatic Set Discharge Date On Payment Of Inpatient When AppParameter AutoCheckOutOnPayment is Yes
            /// </summary>
            /// <param name="reg">Registration Number</param>
            /// <returns>True if RI and Set/Unset Discharge Date Successful, Else False</returns>
            public static bool SetDischargeDate(Registration reg) {
                // jika bukan RI abaikan
                if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient) return false;

                var serverDate = (new DateTime()).NowAtSqlServer();

                // cek parameter, apakah auto checkout Yes or No
                if (AppSession.Parameter.AutoCheckOutOnPayment.ToUpper() == "Yes".ToUpper()) {
                    decimal BillAmount = 0, PaymentAmount = 0;
                    var regs = MergeBilling.GetFullMergeRegistration(reg.RegistrationNo);
                    BillAmount = CostCalculation.GetBillingTotalAllTransactionInclAdm(regs, true);
                    PaymentAmount = Payment.GetTotalPaymentAll(regs);

                    if (PaymentAmount >= BillAmount)
                    {
                        // set discharge date here
                        if (!reg.DischargeDate.HasValue)
                        {
                            reg.DischargeDate = serverDate.Date;
                            reg.DischargeTime = serverDate.ToString("HH:mm");
                            reg.Save();

                            // update tanggal pulang di jasa medis
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetDischargeDate(reg, AppSession.Parameter.IsFeeCalculatedOnTransaction);
                            feeColl.Save();

                            return true;
                        }
                    }
                    else { 
                        // set null lagi discharge datenya jika sebelumnya sudah pernah diisi 
                        // tapi discharge method kosong, artinya ada pembatalan kuitansi atau ar receive
                        if (reg.DischargeDate.HasValue && string.IsNullOrEmpty(reg.SRDischargeMethod)) {
                            reg.DischargeDate = (DateTime?)null;
                            reg.str.DischargeTime = string.Empty;
                            reg.Save();
                            return true;
                        }
                    }
                }
                return false;
            }
            #endregion
        }
    }
}
