using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using DataRow = System.Data.DataRow;

namespace Temiang.Avicenna.Module.RADT.Emr.MainContent
{
    public partial class HealthRecordHistCtl : BaseMainContentCtl
    {

        public string GridPhrClientID
        {
            get { return grdPhr.ClientID; }
        }

        bool? _isFirstRegistrationInServiceUnit = null;
        protected bool IsFirstRegistrationInServiceUnit
        {
            get
            {
                if (_isFirstRegistrationInServiceUnit == null)
                {
                    _isFirstRegistrationInServiceUnit = true;

                    // Check status new patient at clinic
                    // Lebih tepat jika PatientAssessment baru 1 di Service Unit terpilih tapi akan bermasalah jika assessment dihapus
                    var regColl = new RegistrationCollection();
                    var regQuery = new RegistrationQuery();
                    regQuery.Where(regQuery.PatientID == PatientID, regQuery.ServiceUnitID == ServiceUnitID,
                        regQuery.RegistrationNo < RegistrationNo, regQuery.IsVoid == false);
                    regQuery.es.Top = 1;
                    regQuery.Select(regQuery.RegistrationNo);
                    if (regColl.Load(regQuery))
                    {
                        _isFirstRegistrationInServiceUnit = (regColl.Count == 0);
                    }
                }
                return _isFirstRegistrationInServiceUnit??true;
            }
        }


        internal static DataTable QuestionFormDatatable(string serviceUnitID, string patientID, string registrationNo)
        {
            var isNewPatient = true;
            // Check status new patient at clinic
            // Lebih tepat jika PatientAssessment baru 1 di Service Unit terpilih tapi akan bermasalah jika assessment dihapus
            var regColl = new RegistrationCollection();
            var regQuery = new RegistrationQuery();
            regQuery.Where(regQuery.PatientID == patientID, regQuery.ServiceUnitID == serviceUnitID,
                regQuery.RegistrationNo < registrationNo, regQuery.IsVoid == false);
            regQuery.es.Top = 1;
            regQuery.Select(regQuery.RegistrationNo);
            if (regColl.Load(regQuery))
            {
                isNewPatient = regColl.Count == 0;
            }

            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");

            query.InnerJoin(suQr)
                .On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);
            if (isNewPatient)
                query.Where(query.IsActive == true && query.IsInitialAssessment == true);
            else
                query.Where(query.IsActive == true && query.IsContinuedAssessment == true);

            // Berdasarkan Form Type
            query.Where(query.Or(query.SRQuestionFormType.IsNull(), query.SRQuestionFormType == string.Empty,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.PatientTransfer,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.PraRegistration,
                query.SRQuestionFormType == QuestionForm.QuestionFormType.General));

            // Berdasarkan tipe user
            query.Where(query.Or(query.RestrictionUserType.IsNull(), query.RestrictionUserType == string.Empty,
                query.RestrictionUserType.Like("%" + AppSession.UserLogin.SRUserType + "%")));

            query.Select(string.Format("<'{0}' as registrationNo>", registrationNo),
                query.QuestionFormID,
                query.QuestionFormName,
                query.IsSingleEntry.Coalesce("0").As("IsSingleEntry"), @"<CAST(1 AS BIT) AS IsNewEnable>");

            query.OrderBy(query.QuestionFormName.Ascending);

            var dtb = query.LoadDataTable();

            // Tune Up (Handono 230326)
            var singleEntries = (from DataRow row in dtb.Rows where Convert.ToBoolean(row["IsSingleEntry"]) select row["QuestionFormID"].ToString()).ToList();

            if (singleEntries.Count > 0)
            {
                var qr = new PatientHealthRecordQuery("phr");
                qr.Select(qr.QuestionFormID); // Just for check
                qr.es.Distinct = true;
                qr.Where(qr.RegistrationNo == registrationNo, qr.QuestionFormID.In(singleEntries));
                qr.OrderBy(qr.QuestionFormID.Ascending);
                var dtbExistForm = qr.LoadDataTable();
                if (dtbExistForm.Rows.Count > 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (Convert.ToBoolean(row["IsSingleEntry"]))
                        {
                            foreach (DataRow rowEf in dtbExistForm.Rows)
                            {
                                if (rowEf["QuestionFormID"].Equals(row["QuestionFormID"]))
                                {
                                    row["IsNewEnable"] = false;
                                    break;
                                }
                            }

                        }
                    }
                }
            }

            dtb.AcceptChanges();

            return dtb;
        }

        private void PopulatePhrMenuAdd()
        {
            return;
            var tbarItemAdd = (RadToolBarDropDown)tbarPhr.Items[0];
            tbarItemAdd.Buttons.Clear();

            if (!IsUserAddAble)
            {
                tbarItemAdd.Enabled = false;
                return;
            }

            var dtbQuestionForm = QuestionFormDatatable(ServiceUnitID, PatientID, RegistrationNo); ;
            if (dtbQuestionForm.Rows.Count > 0)
            {
                tbarItemAdd.Enabled = true;
                foreach (DataRow row in dtbQuestionForm.Rows)
                {
                    var btn = new RadToolBarButton(row["QuestionFormName"].ToString())
                    {
                        Value = string.Format("addphr_{0}", row["QuestionFormID"])
                    };
                    btn.Enabled = true.Equals(row["IsNewEnable"]);
                    tbarItemAdd.Buttons.Add(btn);
                }
            }
            else
                tbarItemAdd.Enabled = false;
        }
        private DataTable PatientHeathRecordDataTable(List<string> registrationNoList)
        {
            // Display semua PHR krn untuk keperluan list history
            var query = new PatientHealthRecordQuery("phr");
            var form = new QuestionFormQuery("f");
            var userQr = new AppUserQuery("usr");
            var reg = new RegistrationQuery("x");
            var par = new ParamedicQuery("y");
            var unit = new ServiceUnitQuery("z");

            query.InnerJoin(form).On(query.QuestionFormID == form.QuestionFormID);
            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(par).On(reg.ParamedicID == par.ParamedicID);
            query.LeftJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(userQr).On(query.CreateByUserID == userQr.UserID);

            if (registrationNoList.Count > 1)
                query.Where(query.RegistrationNo.In(registrationNoList));
            else
                query.Where(query.RegistrationNo == registrationNoList[0]);

            // Form Physiotherapy dan PatientLetter jangan dimunculkan
            query.Where(query.Or(form.SRQuestionFormType.IsNull(), form.SRQuestionFormType != QuestionForm.QuestionFormType.PatientLetter, form.SRQuestionFormType != QuestionForm.QuestionFormType.Physiotherapy));
            query.Select(
                query.TransactionNo,
                reg.RegistrationNo,
                par.ParamedicName,
                unit.ServiceUnitName,
                unit.ServiceUnitID, // Untuk keperluan hak akses
                query.QuestionFormID,
                form.QuestionFormName,
                @"<CAST(CONVERT(VARCHAR(10), phr.RecordDate, 112) + ' ' + phr.RecordTime AS DATETIME) AS RecordDateTime>",
                userQr.UserName.As("CreatedByUserName"),
                query.IsComplete,
                query.ReferenceNo,
                form.RmNO,
                query.CreateByUserID,
                query.IsApproved,
                form.IsSharingEdit,
                form.RestrictionUserType,
                form.IsSingleEntry
                );

            return query.LoadDataTable();
        }

        protected void grdPhr_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PopulatePhrMenuAdd();
            grdPhr.DataSource = PatientHeathRecordDataTable(MergeRegistrations);
        }
        protected object PhrEditLink(GridItem container)
        {
            var isEditAble = this.IsUserEditAble && (Eval("IsApproved") == DBNull.Value || false.Equals(Eval("IsApproved"))) && (Eval("IsSharingEdit") != DBNull.Value && true.Equals(Eval("IsSharingEdit")) || AppSession.UserLogin.UserID.Equals(Eval("CreateByUserID")));
            return string.Format(
                "<a href=\"#\" onclick=\"entryPhr('{4}', '{0}','{1}','{2}','{3}'); return false;\"><img src=\"{6}/Images/Toolbar/{5}16.png\" border=\"0\" /></a>",
                Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"), Eval("ServiceUnitID"),
                isEditAble ? "edit" : "view",
                isEditAble ? "edit" : "views", Helper.UrlRoot());
        }

        protected string PhrViewLink(GridItem container)
        {
            var retval =
                string.Format(
                    "<a href=\"#\" onclick=\"entryPhr('view', '{0}','{1}','{2}','{3}'); return false;\">{0}</a>",
                    Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"), Eval("ServiceUnitID"));
            return retval;
        }

        protected string AddPhrLink(GridItem container)
        {
            //TODO: Hati2 kalau mau dipakai dg form yg harus dipilih dahulu ReferenceNo nya sebelum New
            if (!this.IsUserAddAble) return string.Empty;
            if (Eval("IsSingleEntry") != DBNull.Value && true.Equals(Eval("IsSingleEntry"))) return string.Empty;
            if (!Eval("RestrictionUserType").ToString().Contains(AppSession.UserLogin.SRUserType)) return string.Empty;

            var newLink = string.Format(
                "<a href=\"#\" onclick=\"entryPhr('new','','{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/insert16.png\" border=\"0\" /></a>",
                RegistrationNo, Eval("QuestionFormID"), ServiceUnitID, Helper.UrlRoot());

            return newLink;
        }


    }
}