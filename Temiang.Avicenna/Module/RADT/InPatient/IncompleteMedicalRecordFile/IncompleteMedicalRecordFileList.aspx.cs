using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Drawing;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class IncompleteMedicalRecordFileList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.IncompleteMedicalRecordFile;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                if (!this.IsUserCrossUnitAble)
                {
                    var qusr = new AppUserServiceUnitQuery("u");
                    query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                    query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                }
                query.Where(
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsActive == true
                        );
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
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

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit) {
                grd.DataSource = new String[] { };
                return;
            } 
            
            var dataSource = MedicalRecordFileCompletenesss;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable MedicalRecordFileCompletenesss
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(cboParamedicID.SelectedValue) && string.IsNullOrEmpty(txtPatientName.Text) &&
                    txtSubmitDate.IsEmpty;
                if (!ValidateSearch(isEmptyFilter, "medical record files")) return null;

                var qa = new MedicalRecordFileCompletenessQuery("a");
                var qah = new MedicalRecordFileCompletenessHistoryQuery("ah");
                var qb = new RegistrationQuery("b");
                var qc = new PatientQuery("c");
                var qg = new ParamedicQuery("g");
                var qh = new ServiceUnitQuery("h");
                var asr = new AppStandardReferenceItemQuery("j");

                qa.es.Top = AppSession.Parameter.MaxResultRecord;
                qa.Select
                    (
                        qa.RegistrationNo,
                        qah.TxId,
                        qb.RegistrationDate,
                        qb.RegistrationTime,
                        qb.DischargeDate,
                        qb.DischargeTime,
                        qa.TransactionDate,
                        qah.SubmitDate,
                        qc.MedicalNo,
                        asr.ItemName.As("SalutationName"),
                        qc.PatientName,
                        qc.Sex,
                        qg.ParamedicName,
                        qh.ServiceUnitName,
                        qa.IsApproved
                    );

                qa.InnerJoin(qah).On(qah.RegistrationNo == qa.RegistrationNo && qah.ReturnDate.IsNull());
                qa.InnerJoin(qb).On(qa.RegistrationNo == qb.RegistrationNo);
                qa.InnerJoin(qc).On(qb.PatientID == qc.PatientID);
                qa.InnerJoin(qg).On(qb.ParamedicID == qg.ParamedicID);
                qa.InnerJoin(qh).On(qb.ServiceUnitID == qh.ServiceUnitID);
                qa.LeftJoin(asr).On(asr.StandardReferenceID == "Title" & qc.SRSalutation == asr.ItemID);

                qa.Where(qa.IsApproved == false);

                if (!txtSubmitDate.IsEmpty)
                    qa.Where(qah.SubmitDate == txtSubmitDate.SelectedDate);

                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchMedNo = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qa.Where(
                            qa.Or(
                                qa.RegistrationNo == searchMedNo,
                                qc.MedicalNo == searchMedNo,
                                qc.OldMedicalNo == searchMedNo,
                                string.Format("< OR REPLACE(c.MedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR REPLACE(c.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                    else
                        qa.Where(
                            qa.Or(
                                qa.RegistrationNo == searchMedNo,
                                qc.MedicalNo == searchMedNo,
                                qc.OldMedicalNo == searchMedNo,
                                string.Format("< OR c.MedicalNo LIKE '%{0}%'>", searchMedNo),
                                string.Format("< OR c.OldMedicalNo LIKE '%{0}%'>", searchMedNo)
                                )
                            );
                }

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    qa.Where(qb.ServiceUnitID == cboServiceUnitID.SelectedValue);

                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    qa.Where(qb.ParamedicID == cboParamedicID.SelectedValue);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";

                    qa.Where
                        (
                          string.Format("<RTRIM(c.FirstName+' '+c.MiddleName)+' '+c.LastName LIKE '{0}'>", searchPatient)
                        );
                }


                qa.OrderBy(qa.LastSubmitDate.Ascending, qa.RegistrationNo.Ascending, qah.TxId.Ascending);

                DataTable dtb = qa.LoadDataTable();
                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var registrationNo = e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString();

            var query = new MedicalRecordFileCompletenessHistoryQuery("a");
            var usrSubmit = new AppUserQuery("b");
            var usrReturn = new AppUserQuery("c");

            query.Select
                (
                query,
                usrSubmit.UserName.As("SubmitBy"),
                usrReturn.UserName.As("ReturnBy")
                );
            query.InnerJoin(usrSubmit).On(usrSubmit.UserID == query.SubmitByUserID);
            query.LeftJoin(usrReturn).On(usrReturn.UserID == query.ReturnByUserID);

            query.Where(query.RegistrationNo == registrationNo);
            query.OrderBy(query.SubmitDate.Ascending, query.SubmitDateTime.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.Select
                (
                    query.ParamedicID, query.ParamedicName
                );

            query.Where(
                query.ParamedicName.Like(searchTextContain),
                query.IsActive == true
                );

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }
    }
}