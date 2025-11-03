using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class MergeDocumentList : BasePage
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

            ProgramID = AppConstant.Program.AR_MERGE_RECEIPT;

            if (!IsPostBack)
            {
                StandardReference.Initialize(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                cboSRRegistrationType.SelectedValue = AppConstant.RegistrationType.InPatient;
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

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = TransCharges();
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }

            //grdList2.DataSource = TransCharges(true);
        }

        private DataTable TransCharges()
        {            
                var isEmptyFilter = string.IsNullOrEmpty(txtRegistrationNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue) 
                && txtOrderDate1.IsEmpty && txtOrderDate2.IsEmpty && txtDischargeDateFrom.IsEmpty && txtDischargeDateTo.IsEmpty && txtPaymentFromDate.IsEmpty && txtPaymentToDate.IsEmpty
                && string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "A/R Receipt")) return null;            
                  
            var query = new RegistrationQuery("a");
            var patient = new PatientQuery("b");
            var grr = new GuarantorQuery("c");
            var sal = new AppStandardReferenceItemQuery("sal");
            var tpa = new TransPaymentQuery("d");
            var tpi = new TransPaymentItemQuery("e");

            var isFilterMax = true;

            query.Select
            (                
                query.RegistrationDate,                              
                query.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                patient.Sex,
                grr.GuarantorName,
                query.PatientID,                                                                        
                sal.ItemName.As("SalutationName"),                
                tpa.PaymentDate
            );
          
            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);          
            query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation.ToString() && patient.SRSalutation == sal.ItemID);
            query.InnerJoin(tpa).On(query.RegistrationNo == tpa.RegistrationNo)
                .InnerJoin(tpi).On(
                    tpa.PaymentNo == tpi.PaymentNo && tpa.IsApproved == true &&
                    tpa.TransactionCode == TransactionCode.Payment);

            query.Where(query.RegistrationNo == tpa.RegistrationNo, tpa.IsVoid == false, 
                grr.GuarantorID != "SELF", grr.GuarantorID != "SELF.RJ",
                query.SRRegistrationType == cboSRRegistrationType.SelectedValue);
            query.GroupBy(query.RegistrationNo, query.RegistrationDate, patient.MedicalNo, patient.FirstName, patient.MiddleName,
               patient.LastName, patient.Sex, grr.GuarantorName, query.PatientID, sal.ItemName, tpa.PaymentDate);

            if (txtRegistrationNo.Text != string.Empty)
            {
                string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                //query.Where
                //(
                //    query.Or
                //    (
                //        query.RegistrationNo == searchReg,
                //        patient.MedicalNo == searchReg,
                //        patient.OldMedicalNo == searchReg,
                //        string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                //        string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                //    )
                //);
                Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
            }
            if (txtPatientName.Text != string.Empty)
            {
                string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                query.Where(string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchPatient));
            }
            if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);
            if (!txtOrderDate1.IsEmpty && !txtOrderDate2.IsEmpty)
                query.Where(query.RegistrationDate >= txtOrderDate1.SelectedDate, query.RegistrationDate < txtOrderDate2.SelectedDate.Value.AddDays(1));
            if (!txtDischargeDateFrom.IsEmpty && !txtDischargeDateTo.IsEmpty)
                query.Where(query.DischargeDate >= txtDischargeDateFrom.SelectedDate, query.DischargeDate < txtDischargeDateTo.SelectedDate.Value.AddDays(1));
            if (!txtPaymentFromDate.IsEmpty && !txtPaymentToDate.IsEmpty)
                query.Where(tpa.PaymentDate >= txtPaymentFromDate.SelectedDate, tpa.PaymentDate < txtPaymentToDate.SelectedDate.Value.AddDays(1));
            if (cboSRRegistrationType.SelectedValue == AppConstant.RegistrationType.InPatient)
                isFilterMax = false;
            query.OrderBy(query.RegistrationDate.Descending, query.RegistrationNo.Descending);
            query.es.Top = AppSession.Parameter.MaxResultRecord;

            DataTable dtbl = query.LoadDataTable();                                                            
                return dtbl;            
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.Rebind();
        }        

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    query.IsActive == true
                );
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }
        protected void cboSRRegistrationType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboGuarantorID.Items.Clear();
            cboGuarantorID.SelectedValue = string.Empty;
            cboGuarantorID.Text = string.Empty;
        }
    }
}
