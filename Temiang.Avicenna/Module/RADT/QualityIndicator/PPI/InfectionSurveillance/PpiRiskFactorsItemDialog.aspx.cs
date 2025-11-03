using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiRiskFactorsItemDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PpiInfectionSurveillance;

            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            txtRegistrationNo.Text = Request.QueryString["regno"];
            txtSequenceNo.Text = Request.QueryString["seqno"];

            StandardReference.InitializeIncludeSpace(cboSRRiskFactorsType, AppEnum.StandardReference.RiskFactorsType);
            cboSRRiskFactorsType.SelectedValue = Request.QueryString["rftype"];

            PopulateRiskFactorsId(cboSRRiskFactorsType.SelectedValue);
            cboRiskFactorsID.SelectedValue = Request.QueryString["rfid"];
            
            StandardReference.InitializeIncludeSpace(cboSRRiskFactorsLocation, AppEnum.StandardReference.RiskFactorsLocation);
            cboSRRiskFactorsLocation.SelectedValue = Request.QueryString["rfloc"];
        }

        private PpiRiskFactorsItemCollection PpiRiskFactorsItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collPpiRiskFactorsItem"];
                    if (obj != null)
                    {
                        return ((PpiRiskFactorsItemCollection)(obj));
                    }
                }

                var coll = new PpiRiskFactorsItemCollection();
                var query = new PpiRiskFactorsItemQuery("a");
                var soiQ = new AppStandardReferenceItemQuery("b");
                query.InnerJoin(soiQ).On(query.SRSignsOfInfection == soiQ.ItemID &&
                                         soiQ.StandardReferenceID ==
                                         AppEnum.StandardReference.SignsOfInfection.ToString());
                query.Select
                    (
                        query,
                        soiQ.ItemName.As("refToAppStdRef_SignsOfInfection")
                    );
                query.Where(query.RegistrationNo == txtRegistrationNo.Text, query.SequenceNo == txtSequenceNo.Text);
                coll.Load(query);

                Session["collPpiRiskFactorsItem"] = coll;
                return coll;
            }
            set
            {
                Session["collPpiRiskFactorsItem"] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = PpiRiskFactorsItems;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            DateTime date =
                Convert.ToDateTime(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection]);
            String id =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        PpiRiskFactorsItemMetadata.ColumnNames.SRSignsOfInfection]);
            PpiRiskFactorsItem entity = FindPpiRiskFactorsItem(date, id);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                PpiRiskFactorsItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            DateTime date =
                Convert.ToDateTime(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        PpiRiskFactorsItemMetadata.ColumnNames.DateOfInfection]);
            String id =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        PpiRiskFactorsItemMetadata.ColumnNames.SRSignsOfInfection]);
            PpiRiskFactorsItem entity = FindPpiRiskFactorsItem(date, id);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                PpiRiskFactorsItems.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = PpiRiskFactorsItems.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                PpiRiskFactorsItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private PpiRiskFactorsItem FindPpiRiskFactorsItem(DateTime date, String id)
        {
            PpiRiskFactorsItemCollection coll = PpiRiskFactorsItems;
            PpiRiskFactorsItem retEntity = null;
            foreach (PpiRiskFactorsItem rec in coll)
            {
                if (rec.DateOfInfection.Equals(date) && rec.SRSignsOfInfection.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(PpiRiskFactorsItem entity, GridCommandEventArgs e)
        {
            var userControl = (PpiRiskFactorsItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RegistrationNo = txtRegistrationNo.Text;
                entity.SequenceNo = txtSequenceNo.Text;
                entity.DateOfInfection = userControl.DateOfInfection;
                entity.SRSignsOfInfection = userControl.SRSignsOfInfection;
                entity.SignsOfInfectionName = userControl.SignsOfInfectionName;
                entity.Notes = userControl.Notes;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void PopulateRiskFactorsId(string srRiskFactorsType)
        {
            cboRiskFactorsID.Items.Clear();

            var query = new RiskFactorsQuery();
            query.Select
                (
                    query.RiskFactorsID,
                    query.RiskFactorsName
                );
            query.Where
                (
                    query.SRRiskFactorsType == srRiskFactorsType
                );
            query.OrderBy(query.RiskFactorsID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboRiskFactorsID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboRiskFactorsID.Items.Add(new RadComboBoxItem(row["RiskFactorsName"].ToString(), row["RiskFactorsID"].ToString()));
            }
        }
    }
}
