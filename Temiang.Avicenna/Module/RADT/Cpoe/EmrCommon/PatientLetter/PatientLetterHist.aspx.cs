using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PatientLetterHist : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            // Selalu Refresh
            PopulateMenuAdd();

            if (!IsPostBack)
            {

                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Letter of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
        }

        #region PHR
        private DataTable PatientHeathRecordDataTable()
        {
            var query = new QuestionFormQuery("a");
            var phrQr = new PatientHealthRecordQuery("b");
            var reg = new RegistrationQuery("x");
            var usr = new AppUserQuery("y");

            query.InnerJoin(phrQr).On(phrQr.QuestionFormID == query.QuestionFormID);
            query.InnerJoin(reg).On(phrQr.RegistrationNo == reg.RegistrationNo);
            query.LeftJoin(usr).On(phrQr.CreateByUserID == usr.UserID);

            if (string.IsNullOrEmpty(FromRegistrationNo))
                query.Where(reg.RegistrationNo == RegistrationNo);
            else
                query.Where(query.Or(reg.RegistrationNo == FromRegistrationNo, reg.RegistrationNo == RegistrationNo));

            query.Where(query.SRQuestionFormType == QuestionForm.QuestionFormType.PatientLetter);

            query.Select(
                phrQr.TransactionNo,
                phrQr.IsApproved,
                phrQr.CreateByUserID,
                query.IsSharingEdit,
                reg.RegistrationNo,
                usr.UserName,
                query.QuestionFormID,
                query.QuestionFormName,
                @"<CAST(CONVERT(VARCHAR(10), b.RecordDate, 112) + ' ' + b.RecordTime AS DATETIME) AS RecordDate>"
                );

            return query.LoadDataTable();
        }


        protected void grdPhr_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdPhr.DataSource = PatientHeathRecordDataTable();
        }
        #endregion

        private DataTable QuestionFormLetterDatatable()
        {
            var query = new QuestionFormQuery("a");
            query.Where(query.IsActive == true && query.SRQuestionFormType == QuestionForm.QuestionFormType.PatientLetter);

            query.Select(query.QuestionFormID,
                query.QuestionFormName,
                query.IsSingleEntry.Coalesce("0").As("IsSingleEntry"),
                @"<CAST(1 AS BIT) AS IsNewEnable>");

            var dtb = query.LoadDataTable();

            // Update info single entry
            foreach (DataRow row in dtb.Rows)
            {
                if (Convert.ToBoolean(row["IsSingleEntry"]))
                {
                    var phr = new PatientHealthRecordCollection();
                    phr.Query.Where(phr.Query.RegistrationNo == RegistrationNo,
                                    phr.Query.QuestionFormID == row["QuestionFormID"].ToString());
                    phr.LoadAll();
                    row["IsNewEnable"] = phr.Count == 0;
                }
            }
            dtb.AcceptChanges();

            return dtb;
        }

        protected object PatientLetterEditLink(GridItem container)
        {
            var isEditAble = this.IsUserEditAble && (Eval("IsApproved") == DBNull.Value || false.Equals(Eval("IsApproved"))) && (Eval("IsSharingEdit") != DBNull.Value && true.Equals(Eval("IsSharingEdit")) || AppSession.UserLogin.UserID.Equals(Eval("CreateByUserID")));

            return string.Format("<a href=\"#\" onclick=\"entryPatientLetter('{4}', '{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/{5}16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"),
                Eval("QuestionFormID"), Helper.UrlRoot(), isEditAble ? "edit" : "view", isEditAble ? "edit" : "views");

        }

        private void PopulateMenuAdd()
        {
            var tbarItemAdd = (RadToolBarDropDown)tbarPhr.Items[0];
            tbarItemAdd.Buttons.Clear();

            if (!IsUserAddAble)
            {
                tbarItemAdd.Enabled = false;
                return;
            }

            var dtbQuestionForm = QuestionFormLetterDatatable();

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
    }
}
