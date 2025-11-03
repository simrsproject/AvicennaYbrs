using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data.Linq;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class BillingAdjustSettingDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string StandardReferenceID
        {
            get
            { return "Specialty"; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            InitValues();
            
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            hfId.Value = ((int)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.Id)).ToString();
            cboParamedic.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.ParamedicID));
            cboSpecialty.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.SRSpecialty));
            cboSRRegistrationType.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.SRRegistrationType));
            cboSRRegistrationType_SelectedIndexChanged(cboSRRegistrationType,
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.Text,
                    cboSRRegistrationType.SelectedValue,
                    cboSRRegistrationType.SelectedValue));
            cboTariffType.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.SRTariffType));
            cboGuarantor_ItemsRequested(cboGuarantor, new RadComboBoxItemsRequestedEventArgs() { Text = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.GuarantorID)) });
            cboGuarantor.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.GuarantorID));
            cboServiceUnit.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.ServiceUnitID));
            cboItem_ItemsRequested(cboItem, new RadComboBoxItemsRequestedEventArgs() { Text = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.ItemID))});
            cboItem.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.ItemID));
            cboItem_SelectedIndexChanged(cboItem,
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboItem.Text,
                    cboItem.Text,
                    cboItem.SelectedValue,
                    cboItem.SelectedValue));
            cboClass.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.ClassID));
            cboClass_SelectedIndexChanged(cboClass,
                new RadComboBoxSelectedIndexChangedEventArgs(
                    cboClass.Text,
                    cboClass.Text,
                    cboClass.SelectedValue,
                    cboClass.SelectedValue));
            cboTariffComponent.SelectedValue = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.TariffComponentID));
            chkIsFeeValueInPercent.Checked = ((bool)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.IsFeeValueInPercent));
            txtFeeValue.Value = System.Convert.ToDouble(((decimal)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.FeeValue)));

            //xxxxxxxxx
        }

        private void InitValues() {
            var parColl = new ParamedicCollection();
            parColl.Query.Where(parColl.Query.IsActive.Equal(true),
                parColl.Query.ParamedicFee.Equal(true))
                .OrderBy(parColl.Query.ParamedicName.Ascending);
            parColl.LoadAll();
            cboParamedic.Items.Clear();
            cboParamedic.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var par in parColl)
            {
                cboParamedic.Items.Add(new RadComboBoxItem(par.ParamedicName, par.ParamedicID));
            }
            StandardReference.InitializeIncludeSpace(cboSpecialty, AppEnum.StandardReference.Specialty, true);
            StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);

            StandardReference.InitializeIncludeSpace(cboTariffType, AppEnum.StandardReference.TariffType);

            var guarColl = new GuarantorCollection();
            guarColl.Query.Where(guarColl.Query.IsActive.Equal(true));
            guarColl.LoadAll();
            cboGuarantor.Items.Clear();
            cboGuarantor.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var guar in guarColl)
            {
                cboGuarantor.Items.Add(new RadComboBoxItem(guar.GuarantorName, guar.GuarantorID));
            }

            var igColl = new ItemGroupCollection();
            igColl.Query.Where(igColl.Query.IsActive == true)
                .OrderBy(igColl.Query.ItemGroupName.Ascending);
            igColl.LoadAll();

            LoadClass();

            LoadTariffComponentToCBO();
        }

        protected void cboGuarantor_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery("a");
            query.Select
                (
                    query.GuarantorID,
                    query.GuarantorName
                );
            query.es.Distinct = true;
            query.OrderBy(query.GuarantorName.Ascending);
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.IsActive == true,
                    query.Or(query.GuarantorID.Like("%" + e.Text + "%"), query.GuarantorName.Like("%" + e.Text + "%"))
                );
            var dt = query.LoadDataTable();
            // insert empty row
            var r = dt.NewRow();
            r["GuarantorID"] = r["GuarantorName"] = string.Empty;
            dt.Rows.InsertAt(r, 0);

            cboGuarantor.DataSource = dt;
            cboGuarantor.DataBind();
        }

        protected void cboGuarantor_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboSRRegistrationType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // load service unit
            cboServiceUnit_ItemsRequested(cboServiceUnit, new RadComboBoxItemsRequestedEventArgs() { Text = cboServiceUnit.Text });
        }
        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var RegistrationType = cboSRRegistrationType.SelectedValue;
            if (RegistrationType.Equals(string.Empty)) RegistrationType = "xxx";

            var query = new ServiceUnitQuery("a");
            var srQ = new ServiceRoomQuery("b");
            var bedQ = new BedQuery("c");
            query.InnerJoin(srQ).On(query.ServiceUnitID == srQ.ServiceUnitID);

            if (RegistrationType == AppConstant.RegistrationType.InPatient)
                query.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);
            else
                query.LeftJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);

            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.es.Distinct = true;
            query.OrderBy(query.ServiceUnitID.Ascending);
            query.Where
                (
                    query.ServiceUnitName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.Where(query.SRRegistrationType == RegistrationType);
            var dt = query.LoadDataTable();
            // insert empty row
            var r = dt.NewRow();
            r["ServiceUnitID"] = r["ServiceUnitName"] = string.Empty;
            dt.Rows.InsertAt(r, 0);

            cboServiceUnit.DataSource = dt;
            cboServiceUnit.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboItem_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var i = new ItemQuery("a");
            //var isv = new ItemServiceQuery("b");
            var std = new AppStandardReferenceItemQuery("c");
            //i.InnerJoin(isv).On(i.ItemID.Equal(isv.ItemID))
            i.InnerJoin(std).On(std.StandardReferenceID.Equal("ItemType") &&
                i.SRItemType.Equal(std.ItemID) && std.ReferenceID.Equal("Service"))
                .Where(
                i.IsActive == true,
                i.Or(i.ItemID.Like("%" + e.Text + "%"), i.ItemName.Like("%" + e.Text + "%"))
            )
            .Select(i.ItemID, i.ItemName)
            .OrderBy(i.ItemName.Ascending);
            i.es.Top = 20;

            DataTable tbl = i.LoadDataTable();
            var r = tbl.NewRow();
            r["ItemID"] = r["ItemName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);

            cboItem.DataSource = tbl;
            cboItem.DataBind();
        }

        protected void cboItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItem_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // load tariff component paramedic
            LoadClass();
            LoadTariffComponentToCBO();
        }

        protected void cboClass_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // load tariff component paramedic
            LoadTariffComponentToCBO();
        }

        private void LoadClass() {
            var clColl = new ClassCollection();
            clColl.Query.Where(clColl.Query.IsActive == true)
                .OrderBy(clColl.Query.ClassID.Ascending);
            clColl.LoadAll();
            cboClass.Items.Clear();
            cboClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (Class c in clColl)
            {
                cboClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
            }
        }

        private void LoadTariffComponentToCBO()
        {
            var itemID = cboItem.SelectedValue;
            var classID = "";// cboClass.SelectedValue;
            if (cboClass.SelectedValue != string.Empty) {
                classID = cboClass.SelectedValue;
            }
            var oldVal = cboTariffComponent.SelectedValue;

            var tcColl = new TariffComponentCollection();
            var tc = new TariffComponentQuery("a");

            if (!string.IsNullOrEmpty(itemID))
            {
                var itc = new ItemTariffComponentQuery("b");
                tc.InnerJoin(itc).On(tc.TariffComponentID.Equal(itc.TariffComponentID))
                    .Where(itc.ItemID.Equal(itemID))
                    .Where(tc.Or(itc.IsAllowVariable.Equal(true), itc.Price.GreaterThan(0)));

                if (!string.IsNullOrEmpty(classID))
                {
                    tc.Where(tc.Or(itc.ClassID.Equal(classID), itc.ClassID.Equal(AppSession.Parameter.DefaultTariffClass)));
                }
            }
            tc.Where(tc.IsTariffParamedic.Equal(true));
            tc.Select(tc);
            tc.es.Distinct = true;

            cboTariffComponent.Items.Clear();
            cboTariffComponent.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            if (tcColl.Load(tc))
            {
                foreach (var c in tcColl)
                {
                    cboTariffComponent.Items.Add(new RadComboBoxItem(c.TariffComponentName, c.TariffComponentID));
                }
            }

            // return old selected
            cboTariffComponent.SelectedValue = oldVal;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {

            }
        }

        private void LoadDataGridItemGroup(){
            var igs = new ItemGroupCollection();
            igs.Query.Where(igs.Query.IsActive == true);
            igs.Query.Select(igs.Query.ItemGroupID, igs.Query.ItemGroupName);
            igs.Query.OrderBy(igs.Query.ItemGroupName.Ascending);
            igs.LoadAll();

            radgrdItemGroup.DataSource = null;
            radgrdItemGroup.DataSource = igs;
            radgrdItemGroup.DataBind();
        }

        protected void radgrdItemGroup_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var igs = new ItemGroupCollection();
            igs.Query.Where(igs.Query.IsActive == true);
            igs.Query.Select(igs.Query.ItemGroupID, igs.Query.ItemGroupName);
            igs.Query.OrderBy(igs.Query.ItemGroupName.Ascending);
            igs.LoadAll();

            radgrdItemGroup.DataSource = igs;
        }

        protected void radgrdItemGroup_DataBound(object sender, EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            string[] ItemGroupIDsReplacement = ((string)DataBinder.Eval(DataItem, BillingAdjustItemSettingMetadata.ColumnNames.ItemGroupIDsReplacement)).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var igID in ItemGroupIDsReplacement)
            {
                foreach (GridDataItem gi in radgrdItemGroup.Items)
                {
                    if (gi.GetDataKeyValue("ItemGroupID").ToString() == igID)
                    {
                        var chk = gi.FindControl("chkItemGroup") as CheckBox;
                        if (chk != null)
                        {
                            chk.Checked = true;
                            break;
                        }
                    }
                }
            }
        }
        
        #region Properties for return entry value

        public String ParamedicID
        {
            get { return cboParamedic.SelectedValue; }
        }

        public String SRSpecialty
        {
            get { return cboSpecialty.SelectedValue; }
        }

        public String SRRegistrationType
        {
            get { return cboSRRegistrationType.SelectedValue; }
        }

        public String SRTariffType
        {
            get { return cboTariffType.SelectedValue; }
        }

        public String GuarantorID
        {
            get { return cboGuarantor.SelectedValue; }
        }

        public String ServiceUnitID
        {
            get { return cboServiceUnit.SelectedValue; }
        }

        public String ItemID
        {
            get { return cboItem.SelectedValue; }
        }

        public String ClassID
        {
            get { return cboClass.SelectedValue; }
        }

        public String TariffComponentID
        {
            get { return cboTariffComponent.SelectedValue; }
        }

        public bool IsFeeValueInPercent
        {
            get { return chkIsFeeValueInPercent.Checked; }
        }

        public decimal FeeValue
        {
            get { return (decimal)txtFeeValue.Value.Value;  }
        }

        public string ItemGroupIDsReplacement {
            get {
                string IDs = string.Empty;
                foreach (GridDataItem gi in radgrdItemGroup.Items)
                {
                    var chk = gi.FindControl("chkItemGroup") as CheckBox;
                    if (chk != null)
                    {
                        if (chk.Checked) IDs = IDs + (IDs == string.Empty ? "" : "|") + gi.GetDataKeyValue("ItemGroupID").ToString();
                    }
                }

                return IDs;
            }
        }
        #endregion
    }
}