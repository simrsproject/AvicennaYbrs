using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges.Billing
{

    public partial class UnLockFinalizeBillingList : BasePage
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

            ProgramID = AppConstant.Program.UnLockFinalizeBilling;
            if (!IsPostBack)
            {
                var coll = new ServiceUnitCollection();
                coll.Query.Where(coll.Query.IsActive == true, coll.Query.SRRegistrationType != string.Empty);
                coll.LoadAll();

                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit item in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
                }
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

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        private DataTable LockVerifiedBillings()
        {
            var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text);
            if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

            var query = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var su = new ServiceUnitQuery("c");
            var par = new ParamedicQuery("e");

            esQueryItem group = new esQueryItem(query, "Group", esSystemType.String);
            group = su.ServiceUnitName;

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            query.Select(query.RegistrationNo, query.LockVerifiedBillingDateTime, query.RoomID, query.BedID,
                         pat.MedicalNo, pat.PatientName, pat.Sex, par.ParamedicName, query.LockVerifiedBillingByUserID,
                         group.As("Group"));

            query.InnerJoin(pat).On(query.PatientID == pat.PatientID);
            query.InnerJoin(su).On(query.ServiceUnitID == su.ServiceUnitID);
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);

            if (cboServiceUnitID.Text != string.Empty)
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            if (txtRegistrationNo.Text != string.Empty)
                query.Where(
                    query.Or(
                            query.RegistrationNo == txtRegistrationNo.Text,
                            pat.MedicalNo == txtRegistrationNo.Text
                        )
                    );
            if (!(string.IsNullOrEmpty(txtPatientName.Text)))
            {
                if (txtPatientName.Text.Trim().Contains(" "))
                {
                    var searchs = txtPatientName.Text.Trim().Split(' ');
                    foreach (var search in searchs)
                    {
                        var searchLike = "%" + search + "%";
                        query.Where(
                            pat.Or(
                                pat.FirstName.Like(searchLike),
                                pat.LastName.Like(searchLike),
                                pat.MiddleName.Like(searchLike)
                                )
                            );
                    }
                }
                else
                {
                    var searchLike = "%" + txtPatientName.Text.Trim() + "%";
                    query.Where(
                        pat.Or(
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike)
                            )
                        );
                }
            }

            query.Where(query.IsVoid == false, query.IsLockVerifiedBilling == true);

            query.OrderBy(query.DischargeDate.Ascending);

            return query.LoadDataTable();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = LockVerifiedBillings();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                string[] regList = Helper.MergeBilling.GetMergeRegistration(param[1]);

                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.RegistrationNo.In(regList));
                regColl.LoadAll();

                using (var trans = new esTransactionScope())
                {
                    foreach (var reg in regColl)
                    {
                        reg.IsLockVerifiedBilling = false;
                        reg.LockVerifiedBillingByUserID = AppSession.UserLogin.UserID;
                        reg.LockVerifiedBillingDateTime = DateTime.Now;
                    }

                    regColl.Save();
                    trans.Complete();
                }

                grdList.Rebind();
            }
        }
    }
}
