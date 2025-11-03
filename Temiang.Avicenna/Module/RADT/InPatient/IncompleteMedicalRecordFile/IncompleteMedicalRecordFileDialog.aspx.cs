using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Configuration;
using System.Data;
using System.Linq;
using DevExpress.Data.Mask;
using Temiang.Avicenna.WebService;
using System.Drawing;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class IncompleteMedicalRecordFileDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.IncompleteMedicalRecordFile;

            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRegistrationNo.Text = Request.QueryString["regno"];
                var reg = new Registration();
                reg.LoadByPrimaryKey(txtRegistrationNo.Text);

                txtDischargeDate.SelectedDate = reg.DischargeDate ?? (new DateTime()).NowAtSqlServer().Date;
                txtDischargeTime.Text = string.IsNullOrEmpty(reg.DischargeTime) ? (new DateTime()).NowAtSqlServer().ToString("HH:mm") : reg.DischargeTime;
                
                txtRegistrationDate.SelectedDate = reg.RegistrationDate;
                txtRegistrationTime.Text = reg.RegistrationTime;
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);
                txtMedicalNo.Text = pat.MedicalNo;
                hdnPatientID.Value = reg.PatientID;

                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
                txtPatientName.Text = pat.PatientName;
                txtGender.Text = pat.Sex;
                txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);
                txtServiceUnit.Text = su.ServiceUnitName;
                var par = new Paramedic();
                par.LoadByPrimaryKey(reg.ParamedicID);
                txtParamedic.Text = par.ParamedicName;


                var hist = new MedicalRecordFileCompletenessHistory();
                if (hist.LoadByPrimaryKey(Convert.ToInt64(Request.QueryString["id"])))
                {
                    txtSubmitDate.SelectedDate = hist.SubmitDate;
                    var usr = new AppUserQuery();
                    usr.Where(usr.UserID == hist.SubmitByUserID);
                    cboSubmitByUserID.DataSource = usr.LoadDataTable();
                    cboSubmitByUserID.DataBind();
                    cboSubmitByUserID.SelectedValue = hist.SubmitByUserID;

                    txtSubmitNotes.Text = hist.SubmitNotes;

                    txtReturnDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                    usr = new AppUserQuery();
                    usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                    cboReturnByUserID.DataSource = usr.LoadDataTable();
                    cboReturnByUserID.DataBind();
                    cboReturnByUserID.SelectedValue = AppSession.UserLogin.UserID;
                }
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new MedicalRecordFileCompletenessHistoryItemQuery("a");
            var qdoc = new DocumentFilesQuery("b");
            query.InnerJoin(qdoc).On(qdoc.DocumentFilesID == query.DocumentFilesID);
            query.Where(query.TxId == Convert.ToInt64(Request.QueryString["id"]));
            query.Select(
                query.TxId,
                query.DocumentFilesID,
                qdoc.DocumentName,
                qdoc.DocumentNumber,
                query.Notes);
            query.OrderBy(qdoc.DocumentNumber.Ascending);

            grdItem.DataSource = query.LoadDataTable();
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (string.IsNullOrEmpty(txtReturnNotes.Text))
            {
                ShowInformationHeader("Return Notes required.");
                return false;
            }

            if (!IsValid) return false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                //update registration
                var entity = new MedicalRecordFileCompletenessHistory();
                if (entity.LoadByPrimaryKey(Convert.ToInt64(Request.QueryString["id"])))
                {
                    entity.ReturnDate = txtReturnDate.SelectedDate;
                    entity.ReturnNotes = txtReturnNotes.Text;
                    entity.ReturnDateTime = (new DateTime()).NowAtSqlServer();
                    entity.ReturnByUserID = cboReturnByUserID.SelectedValue;

                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                    var hd = new MedicalRecordFileCompleteness();
                    if (hd.LoadByPrimaryKey(Request.QueryString["regno"].ToString()))
                    {
                        hd.LastReturnDate = txtReturnDate.SelectedDate;
                        hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        hd.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        hd.Save();
                    }
                    entity.Save();
                }
                
                //Commit if success, Rollback if failed
                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboSubmitByUserID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppUserQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.UserID,
                    query.UserName
                );
            query.Where(query.UserName.Like(searchTextContain));

            cboReturnByUserID.DataSource = query.LoadDataTable();
            cboReturnByUserID.DataBind();
        }

        protected void cboSubmitByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        protected void cboReturnByUserID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new AppUserQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.UserID,
                    query.UserName
                );
            query.Where(query.UserName.Like(searchTextContain));

            cboReturnByUserID.DataSource = query.LoadDataTable();
            cboReturnByUserID.DataBind();
        }

        protected void cboReturnByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }
    }
}