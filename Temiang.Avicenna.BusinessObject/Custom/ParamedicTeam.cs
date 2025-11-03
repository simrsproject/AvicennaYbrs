using System;
using System.Collections.Generic;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class ParamedicTeam
    {
        public string ParamedicName
        {
            get { return GetColumn("refToParamedic_ParamedicName").ToString(); }
            set { SetColumn("refToParamedic_ParamedicName", value); }
        }

        public string ParamedicTeamStatus
        {
            get { return GetColumn("refToAppStandardReferenceItem_ParamedicTeamStatus").ToString(); }
            set { SetColumn("refToAppStandardReferenceItem_ParamedicTeamStatus", value); }
        }

        /// <summary>
        /// Terdiri dari:
        /// 1. Dokter di registrasi
        /// 2. Dokter di ParamedicTeam
        /// 3. Dokter di unit tipe Emergency jika patient diregistrasi tipe Emergency
        /// 4. Dokter di ServiceUnitBooking (Kamar Bedah) jika patient ServiceUnitBooking
        /// </summary>
        /// <param name="paramedicID"></param>
        /// <param name="registrationNo"></param>
        /// <returns></returns>
        public static bool IsParamedicInTeam(string paramedicID, string registrationNo, List<string> mergeRegistrations, string serviceUnitID, string registrationType)
        {
            if (string.IsNullOrWhiteSpace(paramedicID) || string.IsNullOrWhiteSpace(registrationNo)) return false;

            // 1. Cek dokter di registrasi
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(registrationNo))
            {
                if (reg.ParamedicID == paramedicID) return true;
            }

            // 2. Cek dokter di ParamedicTeam
            // ParamedicTeam nya pada satu episode
            var qrPt = new ParamedicTeamQuery("pt");
            if (mergeRegistrations == null)
                qrPt.Where(qrPt.RegistrationNo == registrationNo);
            else
                qrPt.Where(qrPt.RegistrationNo.In(mergeRegistrations));

            qrPt.Where(qrPt.ParamedicID == paramedicID, qrPt.Or(qrPt.EndDate.IsNull(), qrPt.EndDate >= DateTime.Today));
            var dtbPt = qrPt.LoadDataTable();
            if (dtbPt != null && dtbPt.Rows.Count > 0) return true;

            // 3. Cek dokter di unit IGD
            // Untuk pasien unit IGD / Emergency bisa diakses oleh semua dokter yg terdaftar di unit tsb (Handono & Hermawan 2018-08-10)
            var su = new ServiceUnit();
            su.LoadByPrimaryKey(reg.ServiceUnitID);
            if (su.SRRegistrationType == "EMR")
            {
                var qr = new ServiceUnitParamedicQuery();
                qr.Where(qr.ServiceUnitID == reg.ServiceUnitID && qr.ParamedicID == paramedicID);
                var dtb = qr.LoadDataTable();
                if (dtb != null && dtb.Rows.Count > 0) return true;
            }

            //4. Cek status dokter jaga
            if (IsAllowAccessPatientWithServiceUnitParamedic(serviceUnitID, paramedicID))
                return true;

            // Check dokter MCU yg diset di transChargesItemComp
            if (registrationType == "MCU")
            {
                var tcic = new TransChargesItemCompQuery("tcic");
                var tc = new TransChargesQuery("tc");
                tcic.InnerJoin(tc).On(tcic.TransactionNo == tc.TransactionNo);
                tcic.Where(tc.RegistrationNo == registrationNo, tcic.ParamedicID == paramedicID);
                tcic.es.Top = 1;
                tcic.Select(tcic.TransactionNo);
                var dtb = tcic.LoadDataTable();
                if (dtb != null && dtb.Rows.Count > 0)
                    return true;
            }

            //// 5. Cek dokter di ServiceUnitBooking (Kamar Bedah) 
            //var sub = new ServiceUnitBooking();
            //sub.Query.Where(sub.Query.RegistrationNo == registrationNo, sub.Query.ParamedicID == paramedicID, sub.Query.Or(sub.Query.IsVoid.IsNull(), sub.Query.IsVoid == false));
            //sub.Query.es.Top = 1;
            //if (sub.Query.Load()) return true;

            //sub = new ServiceUnitBooking();
            //sub.Query.es.Top = 1;
            //sub.Query.Where(sub.Query.RegistrationNo == registrationNo, sub.Query.ParamedicID2 == paramedicID, sub.Query.Or(sub.Query.IsVoid.IsNull(), sub.Query.IsVoid == false));
            //if (sub.Query.Load()) return true;

            //sub = new ServiceUnitBooking();
            //sub.Query.es.Top = 1;
            //sub.Query.Where(sub.Query.RegistrationNo == registrationNo, sub.Query.ParamedicID3 == paramedicID, sub.Query.Or(sub.Query.IsVoid.IsNull(), sub.Query.IsVoid == false));
            //if (sub.Query.Load()) return true;

            //sub = new ServiceUnitBooking();
            //sub.Query.es.Top = 1;
            //sub.Query.Where(sub.Query.RegistrationNo == registrationNo, sub.Query.ParamedicID4 == paramedicID, sub.Query.Or(sub.Query.IsVoid.IsNull(), sub.Query.IsVoid == false));
            //if (sub.Query.Load()) return true;

            //sub = new ServiceUnitBooking();
            //sub.Query.es.Top = 1;
            //sub.Query.Where(sub.Query.RegistrationNo == registrationNo, sub.Query.ParamedicIDAnestesi == paramedicID, sub.Query.Or(sub.Query.IsVoid.IsNull(), sub.Query.IsVoid == false));
            //if (sub.Query.Load()) return true;

            // 5.Cek dokter di ServiceUnitBooking(Kamar Bedah), di RSI dokter anestesi dibolehkan untuk membuat resep (Handono 220816)
            var sub = new ServiceUnitBooking();
            sub.Query.Where(sub.Query.RegistrationNo == registrationNo, sub.Query.Or(sub.Query.IsVoid.IsNull(), sub.Query.IsVoid == false));
            sub.Query.es.Top = 1;
            if (sub.Query.Load() && 
                (sub.ParamedicID == paramedicID 
                || sub.ParamedicID2 == paramedicID 
                || sub.ParamedicID3 == paramedicID 
                || sub.ParamedicID4 == paramedicID 
                || sub.ParamedicIDAnestesi == paramedicID))
            {
                return true;
            }

            return false;
        }

        public static bool IsAllowAccessPatientWithServiceUnitParamedic(string serviceUnitID, string paramedicID)
        {
            var su = new ServiceUnit();
            if (su.LoadByPrimaryKey(serviceUnitID))
            {
                if (su.IsAllowAccessPatientWithServiceUnitParamedic != true)
                    return false;
            } 

            var supar = new ServiceUnitParamedic();
            if (!supar.LoadByPrimaryKey(su.ServiceUnitID, paramedicID))
                return false;

            return true;
        }
        public static bool IsParamedicTeamStatusDpjpOrSharing(string registrationNo, string paramedicID)
        {
            if (string.IsNullOrWhiteSpace(paramedicID) || string.IsNullOrWhiteSpace(registrationNo)) return false;

            bool? status = null;
            var pt = new ParamedicTeam();
            var qr = new ParamedicTeamQuery();
            var statusDpjpId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID);
            var statusSharingId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusSharingID);

            qr.Where(qr.RegistrationNo == registrationNo, qr.ParamedicID == paramedicID,
                qr.Or(qr.SRParamedicTeamStatus == statusDpjpId, qr.SRParamedicTeamStatus == statusSharingId),
                qr.Or(qr.EndDate.IsNull(), qr.EndDate >= DateTime.Now));

            qr.es.Top = 1;
            qr.OrderBy(qr.StartDate.Descending);
            if (pt.Load(qr))
                status = !string.IsNullOrEmpty(pt.ParamedicID);

            // Ambil dari registrasi krn rawat jalan saat code ini dibuat tidak masuk ke ParamedicTeam
            if (!(status ?? false))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);
                status = !string.IsNullOrEmpty(reg.ParamedicID) && paramedicID == reg.ParamedicID;
            }

            return status ?? false;
        }


        public static bool IsParamedicTeamStatusDpjp(string registrationNo, string paramedicID)
        {
            return IsParamedicTeamStatusDpjp(registrationNo, paramedicID, DateTime.Now);
        }

        public static bool IsParamedicTeamStatusDpjp(string registrationNo, string paramedicID, DateTime dateTimeStatus)
        {
            if (string.IsNullOrWhiteSpace(paramedicID) || string.IsNullOrWhiteSpace(registrationNo)) return false;

            var pt = new ParamedicTeam();
            var qr = new ParamedicTeamQuery();
            var statusDpjpId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID);
            qr.Where(qr.RegistrationNo == registrationNo, qr.ParamedicID == paramedicID, qr.SRParamedicTeamStatus == statusDpjpId, qr.Or(qr.EndDate.IsNull(), qr.EndDate >= dateTimeStatus));
            qr.es.Top = 1;
            qr.OrderBy(qr.StartDate.Descending);
            if (pt.Load(qr))
                return !string.IsNullOrEmpty(pt.ParamedicID);

            // Ambil dari registrasi
            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);
            return !string.IsNullOrEmpty(reg.ParamedicID) && paramedicID == reg.ParamedicID;
        }

        public static Paramedic DPJP(string regNo)
        {
            var ptq = new ParamedicTeamQuery();
            ptq.Where(ptq.RegistrationNo == regNo,
                ptq.SRParamedicTeamStatus ==
                AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID));

            ptq.OrderBy(ptq.StartDate.Descending);
            ptq.es.Top = 1;

            var dpjp = new ParamedicTeam();
            if (dpjp.Load(ptq))
            {
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(dpjp.ParamedicID))
                    return par;
            }
            else
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(regNo))
                {
                    var par = new Paramedic();
                    if (par.LoadByPrimaryKey(reg.ParamedicID))
                    {
                        return par;
                    }
                }

            }

            return new Paramedic();
        }
    }
}
