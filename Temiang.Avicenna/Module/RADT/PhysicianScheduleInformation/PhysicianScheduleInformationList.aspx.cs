using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PhysicianScheduleInformationList : BasePage
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

            ProgramID = AppConstant.Program.PhysicianScheduleInformation;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;
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

        private DataTable ParamedicLeaves
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(cboParamedicID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Physician Schedule")) return null;

                var query = new ParamedicScheduleDateQuery("a");
                var unit = new ServiceUnitQuery("b");
                var par = new ParamedicQuery("c");
                var ot = new OperationalTimeQuery("d");

                query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);
                query.InnerJoin(par).On(par.ParamedicID == query.ParamedicID);
                query.InnerJoin(ot).On(ot.OperationalTimeID == query.OperationalTimeID);

                query.Select
                    (
                        query.ServiceUnitID,
                        unit.ServiceUnitName,
                        query.ParamedicID,
                        par.ParamedicName,
                        par.Foto,
                        query.ScheduleDate,
                        query.OperationalTimeID,
                        ot.OperationalTimeName
                    );
                query.Where(query.ScheduleDate == txtDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                query.OrderBy(par.ParamedicName.Ascending);
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


            var dataSource = ParamedicLeaves;
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

        #region ComboBox ParamedicID

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery();
            query.es.Top = 15;
            query.Select(
                query.ParamedicID,
                query.ParamedicName
                );
            query.Where(
                query.ParamedicName.Like(searchTextContain),
                query.IsActive == true
                );
            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        #endregion
    }
}