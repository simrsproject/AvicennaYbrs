using System;
using System.Linq;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class HealthScreeningTariffComponent : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.HealthScreeningMonitoring;

            if (!IsPostBack)
                PopulateParamedicByServiceUnit();
        }

        protected void grdTariff_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdTariff_ItemPreRender;
        }

        private void grdTariff_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var paramedic = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
            paramedic.Visible = true;

            if (paramedic.Visible)
            {
                paramedic.Items.Clear();
                paramedic.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                if (ViewState["paramedic" + Request.UserHostName] == null)
                    PopulateParamedicByServiceUnit();

                var table = ((DataTable)ViewState["paramedic" + Request.UserHostName]);
                foreach (DataRow row in table.Rows)
                {
                    paramedic.Items.Add(new RadComboBoxItem((string)row["ParamedicName"], (string)row["ParamedicID"]));
                }

                paramedic.SelectedValue = dataItem["ParamedicID"].Text;
            }
        }

        private DataTable TariffComponents
        {
            get
            {
                var comp = new TransChargesItemCompQuery("a");
                var tariff = new TariffComponentQuery("b");

                comp.Select(
                    comp.TariffComponentID,
                    tariff.TariffComponentName,
                    comp.ParamedicID
                    );
                comp.InnerJoin(tariff).On(
                    comp.TariffComponentID == tariff.TariffComponentID &&
                    tariff.IsTariffParamedic == true
                    );
                comp.Where(
                    comp.TransactionNo == Request.QueryString["transNo"],
                    comp.SequenceNo == Request.QueryString["seqNo"]
                    );
                comp.OrderBy(comp.TariffComponentID.Ascending);

                return comp.LoadDataTable();
            }
        }

        protected void grdTariff_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            PopulateParamedicByServiceUnit();

            grdTariff.DataSource = TariffComponents;
        }

        private void PopulateParamedicByServiceUnit()
        {
            if (ViewState["paramedic" + Request.UserHostName] != null)
                return;

            var hd = new TransCharges();
            hd.LoadByPrimaryKey(Request.QueryString["transNo"]);

            var query = new ServiceUnitParamedicQuery("a");
            var medic = new ParamedicQuery("b");

            query.Select(
                    query.ParamedicID,
                    medic.ParamedicName
                );
            query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
            query.Where(
                    query.ServiceUnitID == hd.ToServiceUnitID,
                    medic.IsActive == true
                );

            ViewState["paramedic" + Request.UserHostName] = query.LoadDataTable();
        }

        public override bool OnButtonOkClicked()
        {
            using (var trans = new esTransactionScope())
            {
                var comp = new TransChargesItemCompQuery("a");
                var tariff = new TariffComponentQuery("b");

                comp.Select(
                    comp,//.TariffComponentID,
                    tariff.TariffComponentName//,
                    //comp.ParamedicID,
                    //comp.TransactionNo,
                    //comp.SequenceNo
                    );
                comp.InnerJoin(tariff).On(
                    comp.TariffComponentID == tariff.TariffComponentID &&
                    tariff.IsTariffParamedic == true
                    );
                comp.Where(
                    comp.TransactionNo == Request.QueryString["transNo"],
                    comp.SequenceNo == Request.QueryString["seqNo"]
                    );

                var compColl = new TransChargesItemCompCollection();
                compColl.Load(comp);

                foreach (var compEntity in compColl)
                {
                    foreach (var dataItem in grdTariff.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => compEntity.TariffComponentID == dataItem["TariffComponentID"].Text))
                    {
                        compEntity.ParamedicID = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox).SelectedValue;
                        compEntity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        compEntity.LastUpdateDateTime = DateTime.Now;
                        break;
                    }
                }

                compColl.Save();

                //Update juga nama dokter di TransChargesItem
                var tci = new TransChargesItemQuery();
                tci.Where(tci.TransactionNo == Request.QueryString["transNo"],
                          tci.SequenceNo == Request.QueryString["seqNo"]);


                var itmColl = new TransChargesItemCollection();
                itmColl.Load(tci);

                foreach (var itmEntity in itmColl)
                {
                    foreach (var dataItem in grdTariff.MasterTableView.Items.Cast<GridDataItem>())
                    {
                        var pr = new Paramedic();
                        pr.LoadByPrimaryKey(
                            ((dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox).
                                SelectedValue) ?? string.Empty);
                        if (pr.IsPrintInSlip ?? true)
                            itmEntity.ParamedicCollectionName = pr.ParamedicName;
                        break;
                    }
                }

                itmColl.Save();

                if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                {
                    // extract fee
                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    feeColl.SetFeeByTCIC(compColl, AppSession.UserLogin.UserID);
                    feeColl.Save();
                    //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                    //feeColl.Save();
                }

                trans.Complete();
            }

            return true;
        }
    }
}
