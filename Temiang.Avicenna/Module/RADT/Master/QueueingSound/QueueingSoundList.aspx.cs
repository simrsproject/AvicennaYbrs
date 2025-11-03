using System;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class QueueingSoundList : BasePage
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

            ProgramID = AppConstant.Program.QueueingSound;
            //IsShowValueFromCookie = true;

            //if (this.IsUserAddAble == false || this.IsUserEditAble == false)
            //{
            //    btnNew.Visible = this.IsUserAddAble;
            //}
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();
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
            grdList.DataSource = QueueingSounds;
        }

        private DataTable QueueingSounds
        {
            get
            {

                QueueingSoundQuery query;
                query = new QueueingSoundQuery();
                query.OrderBy(query.Name.Ascending);
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtbs = query.LoadDataTable();
                return dtbs;
            }
        }

        protected void grdPoli_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //if (!IsPostBack && !IsListLoadRecordOnInit) return;

            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = PoliSounds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable PoliSounds
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Queueing Sound")) return null;

                string[] regTypes = { "OPR" ,"MCU" };

                ServiceUnitQuery query;
                query = new ServiceUnitQuery("su");
                var stdLocReg = new AppStandardReferenceItemQuery("stdLocReg");
                var stdLocPoli = new AppStandardReferenceItemQuery("stdLocPoli");
                query.LeftJoin(stdLocReg).On(stdLocReg.StandardReferenceID == "QueueingLocation" && stdLocReg.ItemID == query.SrqueueinglocationReg)
                    .LeftJoin(stdLocPoli).On(stdLocPoli.StandardReferenceID == "QueueingLocation" && stdLocPoli.ItemID == query.SrqueueinglocationPoli)
                    .Where(query.SRRegistrationType.In(regTypes))
                    .Select(
                        query, 
                        stdLocReg.ItemName.As("stdLocRegName"),
                        stdLocPoli.ItemName.As("stdLocPoliName")
                    );

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                //query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.ServiceUnitID.Ascending);

                DataTable dtb = query.LoadDataTable();
                return dtb;
            }
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery();
            query.es.Top = 15;
            var sun = "OPR";
            query.Select(
                query.ServiceUnitID,
                query.ServiceUnitName
                );
            query.Where(
                query.ServiceUnitName.Like(searchTextContain),
                query.SRRegistrationType == sun
                );
            //query.Where(query.SRRegistrationType == sun);
            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //grdList.Rebind();
            grdPoli.Rebind();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdPoli.Rebind();
            grdList.Rebind();
        }
    }
}

