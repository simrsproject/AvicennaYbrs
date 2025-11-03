using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientRelatedItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            PatientQuery qPat = new PatientQuery("a");
            qPat.Where
                (
                    qPat.PatientID == (String)DataBinder.Eval(DataItem, PatientRelatedMetadata.ColumnNames.RelatedPatientID)
                );
            DataTable tbl = qPat.LoadDataTable();
            cboPatientID.DataSource = tbl;
            cboPatientID.DataBind();

            cboPatientID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, PatientRelatedMetadata.ColumnNames.RelatedPatientID));

            PopulatePatient(cboPatientID.SelectedValue);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    ItemProductMarginValueCollection coll =
            //        (ItemProductMarginValueCollection)Session["collItemProductMarginValue"];

            //    double sValue = txtStartingValue.Value ?? 0;
            //    bool isExist = false;
            //    foreach (ItemProductMarginValue item in coll)
            //    {
            //        if (item.StartingValue.Equals(sValue))
            //        {
            //            isExist = true;
            //            break;
            //        }
            //    }
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Starting Value: {0} has exist", sValue);
            //    }
            //}
        }

        private void PopulatePatient(string patientID)
        {
            Patient pat = new Patient();
            pat.LoadByPrimaryKey(patientID);
            txtMedicalNo.Text = pat.MedicalNo;
            txtFirstName.Text = pat.FirstName;
            txtMiddleName.Text = pat.MiddleName;
            txtLastName.Text = pat.LastName;
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Patient pat = new Patient();
            if (!pat.LoadByPrimaryKey(e.Value))
            {
                cboPatientID.Text = string.Empty;
                return;
            }
            PopulatePatient(e.Value);
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            PatientQuery query = new PatientQuery("a");
            PatientRelatedQuery qRel = new PatientRelatedQuery("b");
            query.LeftJoin(qRel).On(query.PatientID == qRel.RelatedPatientID);
            query.es.Top = 10;
            query.Select
                (
                    query.PatientID,
                    query.MedicalNo,
                    query.FirstName,
                    query.MiddleName,
                    query.LastName,
                    query.PatientName,
                    query.DateOfBirth,
                    query.StreetName,
                    query.City,
                    query.County,
                    query.ZipCode,
                    query.Address
                );
            if(Common.Helper.IsNumeric(e.Text.Replace("-","").Trim()))
            {
                query.Where(string.Format("<REPLACE(a.MedicalNo,'-','') LIKE '%{0}%'>", e.Text.Replace("-", "").Trim()));
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", e.Text);
                query.Where
                    (
                        query.Or
                            (
                                query.PatientID.Like(searchTextContain),
                                query.MedicalNo.Like(searchTextContain),
                                query.FirstName.Like(searchTextContain),
                                query.MiddleName.Like(searchTextContain),
                                query.LastName.Like(searchTextContain),
                                query.StreetName.Like(searchTextContain),
                                query.City.Like(searchTextContain)
                            )
                        
                    );
            }
            query.Where(query.IsActive == true,
                        query.PatientID != ((RadTextBox) Helper.FindControlRecursive(Page, "txtPatientID")).Text,
                        qRel.RelatedPatientID.IsNull());

            cboPatientID.DataSource = query.LoadDataTable();
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        #region Properties for return entry value

        public String RelatedPatientID
        {
            get { return cboPatientID.SelectedValue; }
        }
        public String MedicalNo
        {
            get { return txtMedicalNo.Text; }
        }
        public String FirstName
        {
            get { return txtFirstName.Text; }
        }
        public String MiddleName
        {
            get { return txtMiddleName.Text; }
        }
        public String LastName
        {
            get { return txtLastName.Text; }
        }
        #endregion
    }
}