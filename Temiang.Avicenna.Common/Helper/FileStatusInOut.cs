using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public partial class Helper
    {
        public class FileStatusInOut
        {
            public static void FileOut(string patientID)
            {
                var entity = new MedicalFileStatus();
                entity.LoadByPrimaryKey(patientID);
                entity.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusConfirm;
                entity.SRMedicalFileStatusCategory = AppSession.Parameter.MedicalFileCategoryOut;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                using (var trans = new esTransactionScope())
                {
                    entity.Save();

                    trans.Complete();
                }
            }

            public static void FileIn(string patientID)
            {
                var entity = new MedicalFileStatus();
                entity.LoadByPrimaryKey(patientID);
                entity.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusConfirm;
                entity.SRMedicalFileStatusCategory = AppSession.Parameter.MedicalFileCategoryIn;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;

                using (var trans = new esTransactionScope())
                {
                    entity.Save();

                    trans.Complete();
                }
            }

            public static void FileInOutConfirmed(string patientId, bool isIn, DateTime dateIn)
            {
                var coll = new MedicalRecordFileStatusCollection();
                var query = new MedicalRecordFileStatusQuery("a");
                var reg = new RegistrationQuery("b");
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.Where(reg.PatientID == patientId,
                            query.SRMedicalFileStatus == AppSession.Parameter.MedicalFileStatusRequest);
                coll.Load(query);

                foreach (var c in coll)
                {
                    c.SRMedicalFileStatus = AppSession.Parameter.MedicalFileStatusConfirm;

                    if (isIn)
                    {
                        c.FileInDate = dateIn;
                        c.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryIn;
                        c.ReceiptByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        if (c.FileOutDate >= dateIn && c.FileOutDate < dateIn.AddDays(1))
                        {
                            c.FileOutDateConfirmed = DateTime.Now;
                            c.FileOutUserIDComfirmed = AppSession.UserLogin.UserID;
                        }
                    }
                    c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    c.LastUpdateDateTime = DateTime.Now;
                }

                using (var trans = new esTransactionScope())
                {
                    coll.Save();

                    trans.Complete();
                }
            }

            public static void FileInConfirmedByPatientID(string PatientID, DateTime dateIn)
            {
                var coll = new MedicalRecordFileStatusCollection();
                var query = new MedicalRecordFileStatusQuery("a");
                var reg = new RegistrationQuery("b");
                query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.Where(reg.PatientID == PatientID,
                            query.SRMedicalFileCategory == AppSession.Parameter.MedicalFileCategoryOut);
                coll.Load(query);

                foreach (var fs in coll)
                {
                    fs.FileInDate = dateIn.Date;
                    fs.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryIn;
                    fs.ReceiptByUserID = AppSession.UserLogin.UserID;
                    fs.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    fs.LastUpdateDateTime = DateTime.Now;
                }
                using (var trans = new esTransactionScope())
                {
                    coll.Save();

                    trans.Complete();
                }
            }

            public static void FileInConfirmed(string regNo, DateTime dateIn)
            {
                using (var trans = new esTransactionScope())
                {
                    var fs = new MedicalRecordFileStatus();
                    if (fs.LoadByPrimaryKey(regNo))
                    {
                        fs.FileInDate = dateIn.Date;
                        fs.SRMedicalFileCategory = AppSession.Parameter.MedicalFileCategoryIn;
                        fs.ReceiptByUserID = AppSession.UserLogin.UserID;
                        fs.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        fs.LastUpdateDateTime = DateTime.Now;
                        fs.Save();
                    }

                    trans.Complete();
                }
            }

            public static void FileBorrowedReceive(string patientId, DateTime dateIn)
            {
                var coll = new MedicalRecordFileBorrowedCollection();
                var query = new MedicalRecordFileBorrowedQuery("a");
                query.Where(query.PatientID == patientId, query.DateOfReturn.IsNull());
                coll.Load(query);

                foreach (var c in coll)
                {
                    c.DateOfReturn = dateIn.Date;
                    c.NameOfRecipientID = AppSession.UserLogin.UserID;
                    c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    c.LastUpdateDateTime = DateTime.Now;
                }

                using (var trans = new esTransactionScope())
                {
                    coll.Save();

                    trans.Complete();
                }
            }
        }
    }
}
