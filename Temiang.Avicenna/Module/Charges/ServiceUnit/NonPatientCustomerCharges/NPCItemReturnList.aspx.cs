using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class NPCItemReturnList : BasePage
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

            ProgramID = AppConstant.Program.NonPatientCustomerChargesItemReturn;
            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");
                var qtc = new ServiceUnitTransactionCodeQuery("qtc");

                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.InnerJoin(qtc).On(query.ServiceUnitID == qtc.ServiceUnitID && qtc.SRTransactionCode == "008");
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                query.Where(query.IsActive == true);

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

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(ItemTransactionMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("NonPatientCustomerChargesDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;
            var dataSource = TransChargess;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }

            //if (!e.IsFromDetailTable)
            //    grdList.DataSource = (new TransChargesExtramuralItemsCollection()).GetOutstanding(
            //        cboServiceUnitID.SelectedValue, txtRegistrationNo.Text, txtRegistrationDate.SelectedDate, txtPatientName.Text
            //        );
        }

        private DataTable TransChargess
        {
            get 
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) && txtRegistrationDate.IsEmpty && string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Transaction")) return null;

                DataTable dtb = (new TransChargesExtramuralItemsCollection()).GetOutstanding(
                    cboServiceUnitID.SelectedValue, txtRegistrationNo.Text, txtRegistrationDate.SelectedDate, txtPatientName.Text
                    );
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //Load record
            var tce = new TransChargesExtramuralItemsQuery("tce");
            var sr = new AppStandardReferenceItemQuery("sr");

            tce.InnerJoin(sr).On(sr.StandardReferenceID == "ExtramuralItem" && tce.SRExtramuralItem == sr.ItemID)
                .Where(tce.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString())
                .Select
                (
                    tce,
                    sr.ItemName
                ).OrderBy(tce.SRExtramuralItem.Ascending);

            //Apply
            e.DetailTableView.DataSource = tce.LoadDataTable();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
        }
    }
}