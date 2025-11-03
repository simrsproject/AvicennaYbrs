using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class MedicalRecordFileReturnedList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.MedicalRecordFileReturned;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now.Date;
                txtReceiveDate.SelectedDate = DateTime.Now.Date;

                ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        private DataTable MedicalRecordFileBorroweds
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && txtReceiveDate.IsEmpty &&
                    string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(txtBarcodeEntry.Text);
                if (!ValidateSearch(isEmptyFilter, "Medical Record File")) return null;

                var query = new MedicalRecordFileBorrowedQuery("a");
                var reg = new RegistrationQuery("b");
                var pat = new PatientQuery("c");
                var su = new ServiceUnitQuery("d");
                var usr = new AppUserQuery("e");
                var sal = new AppStandardReferenceItemQuery("sal");
                query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
                query.LeftJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
                query.LeftJoin(usr).On(query.NameOfRecipientID == usr.UserID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == pat.SRSalutation);
                query.Select
                    (
                        query.TransactionNo,
                        query.PatientID,
                        query.RegistrationNo,
                        pat.MedicalNo,
                        pat.PatientName,
                        query.DateOfBorrowing,
                        query.DateOfReturn,
                        query.ServiceUnitID,
                        su.ServiceUnitName,
                        query.NameOfTheBorrower,
                        query.SRForThePurposesOf,
                        query.Notes,
                        query.ReturnByName,
                        query.NameOfRecipientID,
                        usr.UserName,
                        sal.ItemName.As("SalutationName")
                    );

                if (!txtDate.IsEmpty)
                    query.Where(query.DateOfBorrowing == txtDate.SelectedDate);

                if (txtMedicalNo.Text != string.Empty)
                    query.Where(pat.MedicalNo == txtMedicalNo.Text);

                if (txtRegistrationNo.Text != string.Empty)
                    query.Where(query.RegistrationNo == txtRegistrationNo.Text);

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";

                    query.Where
                        (
                          string.Format("<c.MedicalNo LIKE '{0}' OR c.OldMedicalNo LIKE '{0}' OR RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient)
                        );
                }
                query.Where(query.DateOfReturn.IsNull());
                query.OrderBy(query.TransactionNo.Descending);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                
                return dtb;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = MedicalRecordFileBorroweds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected virtual void txtBarcodeEntry_OnTextChanged(object sender, System.EventArgs e)
        {
            if (txtBarcodeEntry.Text == "") return;

            var que = new PatientQuery();
            que.es.Top = 1;
            que.Where(que.MedicalNo == txtBarcodeEntry.Text);
            var entity = new Patient();
            entity.Load(que);

            if (entity.PatientID != null)
            {
                Helper.FileStatusInOut.FileBorrowedReceive(entity.PatientID, txtReceiveDate.SelectedDate.Value);
                grdList.Rebind();

            }
            txtBarcodeEntry.Text = "";
            txtBarcodeEntry.Focus();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                grdList.Rebind();
            }
            else if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                using (var trans = new esTransactionScope())
                {
                    var fs = new MedicalRecordFileBorrowed();
                    if (fs.LoadByPrimaryKey(param[1]))
                    {
                        fs.DateOfReturn = txtReceiveDate.SelectedDate;
                        fs.NameOfRecipientID = AppSession.UserLogin.UserID;
                        fs.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        fs.LastUpdateDateTime = DateTime.Now;
                        fs.Save();
                    }

                    trans.Complete();
                }

                grdList.Rebind();
            }
        }
    }
}
