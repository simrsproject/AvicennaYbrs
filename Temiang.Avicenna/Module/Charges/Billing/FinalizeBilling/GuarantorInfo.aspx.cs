using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Module.RADT;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class GuarantorInfo : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                txtRegistrationNo.Text = reg.RegistrationNo;
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                Page.Title = "Edit Guarantor Information Detail for " + pat.PatientName;

                StandardReference.InitializeIncludeSpace(cboGuarSRRelationship, AppEnum.StandardReference.Relationship);

                cboGuarSRRelationship.SelectedValue = reg.SREmployeeRelationship;
                txtInsuranceID.Text = reg.InsuranceID;
                txtGuarIDCardNo.Text = reg.GuarantorCardNo;
                txtSepNo.Text = reg.BpjsSepNo;

                pnlEmployeeInfo.Visible = AppSession.Parameter.IsUsingHumanResourcesModul;

                if (!this.IsUserEditAble)
                {
                    (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                    (Helper.FindControlRecursive(this, "btnCancel") as Button).Text = "Close";

                    grdRegistrationGuarantor.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                }
            }
        }

        protected void grdHistoryUpdateGuarantor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = RegistrationGuarantorHistorys();
        }

        private DataTable RegistrationGuarantorHistorys()
        {
            var query = new RegistrationGuarantorHistoryQuery("a");
            var fromGuarQ = new GuarantorQuery("b");
            var toGuarQ = new GuarantorQuery("c");
            
            query.Select(
                query.RegistrationNo,
                query.FromGuarantorID,
                query.ToGuarantorID,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID,
                fromGuarQ.GuarantorName.As("FromGuarantorName"),
                toGuarQ.GuarantorName.As("ToGuarantorName")
                );
            query.InnerJoin(fromGuarQ).On(query.FromGuarantorID == fromGuarQ.GuarantorID);
            query.InnerJoin(toGuarQ).On(query.ToGuarantorID == toGuarQ.GuarantorID);
            query.Where(query.RegistrationNo == Request.QueryString["regNo"]);
            query.OrderBy(query.LastUpdateDateTime.Ascending);

            var dtb = query.LoadDataTable();

            return dtb;
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            var entity = new Registration();
            entity.LoadByPrimaryKey(Request.QueryString["regNo"]);
            entity.SREmployeeRelationship = cboGuarSRRelationship.SelectedValue;
            entity.InsuranceID = txtInsuranceID.Text;
            entity.GuarantorCardNo = txtGuarIDCardNo.Text;
            entity.BpjsSepNo = txtSepNo.Text;
            entity.LastUpdateDateTime = DateTime.Now;
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            
            var pat = new Patient();
            pat.LoadByPrimaryKey(entity.PatientID);
            pat.GuarantorCardNo = txtGuarIDCardNo.Text;
            pat.LastUpdateByUserID = AppSession.UserLogin.UserID;
            pat.LastUpdateDateTime = DateTime.Now;

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                pat.Save();
                RegistrationGuarantors.Save();

                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        #region Record Detail Method Function - Registration Guarantor
        private RegistrationGuarantorCollection RegistrationGuarantors
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collRegistrationGuarantor" + Request.UserHostName];
                    if (obj != null)
                        return ((RegistrationGuarantorCollection)(obj));
                }

                var coll = new RegistrationGuarantorCollection();
                var query = new RegistrationGuarantorQuery("a");
                var gq = new GuarantorQuery("b");

                query.Select
                    (
                        query,
                        gq.GuarantorName.As("refToGuarantor_GuarantorName")
                    );

                query.InnerJoin(gq).On(query.GuarantorID == gq.GuarantorID);
                query.Where(query.RegistrationNo == Request.QueryString["regNo"]);

                query.OrderBy(query.GuarantorID, esOrderByDirection.Ascending);

                coll.Load(query);

                Session["collRegistrationGuarantor" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collRegistrationGuarantor" + Request.UserHostName] = value; }
        }

        protected void grdRegistrationGuarantor_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationGuarantor.DataSource = RegistrationGuarantors;
        }

        protected void grdRegistrationGuarantor_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            String id =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);

            var tpColl = new TransPaymentCollection();
            var tp = new TransPaymentQuery("a");
            var tpi = new TransPaymentItemQuery("b");
            tp.InnerJoin(tpi).On(tpi.PaymentNo == tp.PaymentNo && tpi.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR);
            tp.Where(tp.RegistrationNo == Request.QueryString["regNo"], tp.GuarantorID == id,
                           tp.IsVoid == false);
            tpColl.Load(tp);
            if (tpColl.Count > 0)
                return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdRegistrationGuarantor_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;
            String id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][RegistrationGuarantorMetadata.ColumnNames.GuarantorID]);

            var tpColl = new TransPaymentCollection();
            var tp = new TransPaymentQuery("a");
            var tpi = new TransPaymentItemQuery("b");
            tp.InnerJoin(tpi).On(tpi.PaymentNo == tp.PaymentNo && tpi.SRPaymentType == AppSession.Parameter.PaymentTypeCorporateAR);
            tp.Where(tp.RegistrationNo == Request.QueryString["regNo"], tp.GuarantorID == id,
                           tp.IsVoid == false);
            tpColl.Load(tp);
            if (tpColl.Count > 0)
                return;

            RegistrationGuarantor entity = FindItemGuarantorGrid(id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdRegistrationGuarantor_InsertCommand(object source, GridCommandEventArgs e)
        {
            RegistrationGuarantor entity = RegistrationGuarantors.AddNew();
            SetEntityValue(entity, e);

            //e.Canceled = true;
            grdRegistrationGuarantor.Rebind();
        }

        private void SetEntityValue(RegistrationGuarantor entity, GridCommandEventArgs e)
        {
            var userControl = (RegistrationGuarantorDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = Request.QueryString["regNo"];
                entity.GuarantorID = userControl.GuarantorID;
                entity.GuarantorName = userControl.GuarantorName;
                entity.PlafondAmount = userControl.PlafondAmount;
                entity.Notes = userControl.Notes;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private RegistrationGuarantor FindItemGuarantorGrid(string guarantorId)
        {
            RegistrationGuarantorCollection coll = RegistrationGuarantors;
            RegistrationGuarantor retval = null;
            foreach (RegistrationGuarantor rec in coll)
            {
                if (rec.GuarantorID.Equals(guarantorId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }
        #endregion
    }
}
