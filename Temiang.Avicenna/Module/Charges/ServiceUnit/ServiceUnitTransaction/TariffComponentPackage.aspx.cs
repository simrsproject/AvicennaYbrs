using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class TariffComponentPackage : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.ServiceUnitTransaction;

            if (!IsPostBack)
            {
                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["item"]);
                Title = "Tariff Components : " + item.ItemName + " [" + Request.QueryString["item"] + "]";

                ViewState["PrimaryKey" + Request.UserHostName + Request.QueryString["pageId"]] = "|";
                PopulateParamedicByServiceUnit();
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.rebind = 'rebind'";
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var list = ((TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]]).Where(i => i.TransactionNo == Request.QueryString["trans"] &&
                                                                                                i.ParentNo == Request.QueryString["seq"] &&
                                                                                                i.IsPackage == false &&
                                                                                                i.IsItemTypeService == true &&
                                                                                                i.IsVoid == false).OrderBy(o => o.ServiceUnitName);
            grdList.DataSource = list;
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdTariff":
                    ViewState["PrimaryKey" + Request.UserHostName + Request.QueryString["pageId"]] = eventArgument;
                    PopulateTariffComponents();
                    break;
            }
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

            if (ViewState["SRTariffType" + Request.UserHostName + Request.QueryString["pageId"]] == null)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["reg"]);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                ViewState["SRTariffType" + Request.UserHostName + Request.QueryString["pageId"]] = grr.SRTariffType;
            }

            var str = ViewState["PrimaryKey" + Request.UserHostName + Request.QueryString["pageId"]].ToString().Split('|');
            var entity = (Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]] as TransChargesItemCollection).FindByPrimaryKey(str[0], str[1]);

            DataTable tbl = Helper.Tariff.GetItemTariffComponent(DateTime.Parse(Request.QueryString["date"]), (string)ViewState["SRTariffType" + Request.UserHostName + Request.QueryString["pageId"]],
                entity.ChargeClassID, dataItem["TariffComponentID"].Text, entity.ItemID);

            if (tbl.Rows.Count == 0)
                tbl = Helper.Tariff.GetItemTariffComponent(DateTime.Parse(Request.QueryString["date"]), (string)ViewState["SRTariffType" + Request.UserHostName + Request.QueryString["pageId"]],
                    AppSession.Parameter.DefaultTariffClass, dataItem["TariffComponentID"].Text, entity.ItemID);

            if (tbl.Rows.Count == 0)
                tbl = Helper.Tariff.GetItemTariffComponent(DateTime.Parse(Request.QueryString["date"]), AppSession.Parameter.DefaultTariffType,
                    entity.ChargeClassID, dataItem["TariffComponentID"].Text, entity.ItemID);

            if (tbl.Rows.Count == 0)
                tbl = Helper.Tariff.GetItemTariffComponent(DateTime.Parse(Request.QueryString["date"]), AppSession.Parameter.DefaultTariffType,
                    AppSession.Parameter.DefaultTariffClass, dataItem["TariffComponentID"].Text, entity.ItemID);

            TransChargesItemComp comp = (((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName + Request.QueryString["pageId"]])
                .Where(i => i.SequenceNo == str[1] &&
                       i.TariffComponentID == dataItem["TariffComponentID"].Text)).SingleOrDefault();

            var price = (dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox);
            price.ReadOnly = !(bool)tbl.Rows[0]["IsAllowVariable"];
            price.Value = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);

            var validPrice = (dataItem["TariffComponentName"].FindControl("rfvPrice") as RequiredFieldValidator);
            validPrice.Visible = (bool)tbl.Rows[0]["IsAllowVariable"];

            var discount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox);
            discount.Visible = (bool)tbl.Rows[0]["IsAllowDiscount"];
            discount.MaxValue = (comp == null ? Convert.ToDouble(dataItem["Price"].Text) : (double)comp.Price);
            discount.Value = (comp == null ? 0D : (double)comp.DiscountAmount);

            var validDiscount = (dataItem["TariffComponentName"].FindControl("rfvDiscount") as RequiredFieldValidator);
            validDiscount.Visible = (bool)tbl.Rows[0]["IsAllowDiscount"];

            var paramedic = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
            paramedic.Visible = (bool)tbl.Rows[0]["IsTariffParamedic"];

            var validParamedic = (dataItem["TariffComponentName"].FindControl("rfvPhysicianID") as RequiredFieldValidator);
            validParamedic.Visible = (bool)tbl.Rows[0]["IsTariffParamedic"];

            if (paramedic.Visible)
            {
                PopulateParamedicByServiceUnit();
                DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + Request.QueryString["pageId"]]).DefaultView;
                //if (comp != null & !string.IsNullOrEmpty(comp.ParamedicID))
                //    view.RowFilter = string.Format("ParamedicID = '{0}'", comp.ParamedicID);

                if (comp != null)
                    view.RowFilter = string.Format("ParamedicID = '{0}'", comp.ParamedicID);

                paramedic.DataSource = view;
                paramedic.DataBind();

                paramedic.SelectedValue = comp != null ? comp.ParamedicID : Request.QueryString["pid"];
            }
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            if (ViewState["paramedic" + Request.UserHostName + Request.QueryString["pageId"]] == null)
                PopulateParamedicByServiceUnit();

            DataView view = ((DataTable)ViewState["paramedic" + Request.UserHostName + Request.QueryString["pageId"]]).DefaultView;
            view.RowFilter = string.Format("ParamedicID LIKE '%{0}%' OR ParamedicName LIKE '%{0}%'", e.Text);

            var combo = o as RadComboBox;

            combo.DataSource = view;
            combo.DataBind();
        }

        private void PopulateTariffComponents()
        {
            var str = ViewState["PrimaryKey" + Request.UserHostName + Request.QueryString["pageId"]].ToString().Split('|');
            var list = from i in ((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName + Request.QueryString["pageId"]])
                       where i.TransactionNo == str[0] &&
                            i.SequenceNo == str[1]
                       select i;
            grdTariff.DataSource = list;
            grdTariff.DataBind();
        }

        private void PopulateParamedicByServiceUnit()
        {
            if (ViewState["paramedic" + Request.UserHostName + Request.QueryString["pageId"]] != null)
                return;

            var query = new ServiceUnitParamedicQuery("a");
            var medic = new ParamedicQuery("b");
            var que = new ServiceUnitQueQuery("c");

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["reg"]);

            if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            {
                query.Select
                    (
                        query.ParamedicID,
                        medic.ParamedicName
                    );

                query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);

                if (reg.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient)
                {
                    query.InnerJoin(que).On(medic.ParamedicID == que.ParamedicID);
                    query.Where(que.RegistrationNo == Request.QueryString["reg"]);
                }

                if (Request.QueryString["type"] == "tr" || Request.QueryString["type"] == "mcu")
                {
                    query.Where
                        (
                            query.ServiceUnitID == Request.QueryString["from"],
                            medic.IsActive == true
                        );
                }
                else
                {
                    query.Where
                        (
                            query.ServiceUnitID == Request.QueryString["to"],
                            medic.IsActive == true
                        );
                }

                ViewState["paramedic" + Request.UserHostName + Request.QueryString["pageId"]] = query.LoadDataTable();
            }
            else
            {
                medic.Where(medic.IsActive == true);
                ViewState["paramedic" + Request.UserHostName + Request.QueryString["pageId"]] = medic.LoadDataTable();
            }
        }

        protected void grdTariff_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                string ParamedicNames = string.Empty;
                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    var detail =
                        ((TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]]).FindByPrimaryKey(
                            dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text);
                    if (detail != null)
                    {
                        var medic = dataItem.FindControl("cboPhysicianID") as RadComboBox;
                        if (medic.Visible)
                        {
                            if (string.IsNullOrEmpty(ParamedicNames))
                            {
                                detail.ParamedicCollectionName = ParamedicNames = medic.Text;
                            }
                            else
                                detail.ParamedicCollectionName = ParamedicNames +="; " + medic.Text;
                        }
                    }

                    var entity = ((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName + Request.QueryString["pageId"]]).FindByPrimaryKey(
                        dataItem["TransactionNo"].Text,
                        dataItem["SequenceNo"].Text,
                        dataItem["TariffComponentID"].Text
                        );
                    if (entity != null)
                    {
                        var price = dataItem.FindControl("txtPrice") as RadNumericTextBox;
                        if (!price.ReadOnly)
                            entity.Price = Convert.ToDecimal(price.Value);
                        var disc = dataItem.FindControl("txtDiscount") as RadNumericTextBox;
                        if (disc.Visible && !disc.ReadOnly)
                            entity.DiscountAmount = Convert.ToDecimal(disc.Value);
                        var medic = dataItem.FindControl("cboPhysicianID") as RadComboBox;
                        if (medic.Visible)
                            entity.ParamedicID = medic.SelectedValue;
                    }
                }
            }
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.SequenceNo]);
            if (!string.IsNullOrEmpty(sequenceNo))
            {
                var entity = FindTransChargesItem(sequenceNo);
                if (entity != null)
                {
                    foreach (var comp in (Session["collTransChargesItemComp" + Request.UserHostName + Request.QueryString["pageId"]] as TransChargesItemCompCollection).Where(comp => comp.SequenceNo == entity.SequenceNo))
                    {
                        comp.MarkAsDeleted();
                    }

                    foreach (var cons in (Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]] as TransChargesItemConsumptionCollection).Where(cons => cons.SequenceNo == entity.SequenceNo))
                    {
                        cons.MarkAsDeleted();
                    }

                    if (AppSession.Parameter.IsReducePriceWhenDeletingMcuPackageDetails)
                    {
                        var parent = FindTransChargesItem(Request.QueryString["seq"]);
                        if (parent != null)
                        {
                            var package = new ItemPackage();
                            package.Query.Where(package.Query.ItemID == parent.ItemID && package.Query.DetailItemID == entity.ItemID);
                            if (package.Query.Load())
                            {
                                var comp = new ItemPackageTariffComponent();
                                comp.Query.Select(comp.Query.Price.Sum(), comp.Query.Discount.Sum());
                                comp.Query.Where(comp.Query.ItemID == parent.ItemID && comp.Query.DetailItemID == entity.ItemID);
                                if (comp.Query.Load())
                                {
                                    //parent.DiscountAmount += (comp.Price - ((package.IsDiscountInPercent ?? false) ? (((package.DiscountValue ?? 0) / 100) * comp.Price) : (package.DiscountValue ?? 0)));
                                    //parent.AutoProcessCalculation += 0 - (comp.Price - ((package.IsDiscountInPercent ?? false) ? (((package.DiscountValue ?? 0) / 100) * comp.Price) : (package.DiscountValue ?? 0)));
                                    decimal? discount = 0;
                                    discount = comp.Price - comp.Discount;
                                    parent.DiscountAmount += discount;
                                    parent.AutoProcessCalculation += (0 - discount);
                                }
                            }
                        }

                        var cmp = FindTransChargesItemComp(Request.QueryString["seq"], AppSession.Parameter.TariffComponentJasaSaranaID);
                        if (cmp != null)
                        {
                            cmp.DiscountAmount += parent.DiscountAmount;
                            cmp.AutoProcessCalculation += 0 - parent.DiscountAmount;
                        }
                    }

                    entity.IsVoid = true;
                    entity.IsApprove = false;
                    entity.IsBillProceed = false;
                    //entity.MarkAsDeleted();
                }
            }
        }

        private TransChargesItem FindTransChargesItem(String sequenceNo)
        {
            return ((TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]]).FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo));
        }

        private TransChargesItemComp FindTransChargesItemComp(String sequenceNo, string tariffComponentID)
        {
            return ((TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName + Request.QueryString["pageId"]]).FirstOrDefault(rec => rec.SequenceNo.Equals(sequenceNo) && rec.TariffComponentID.Equals(tariffComponentID));
        }
    }
}
