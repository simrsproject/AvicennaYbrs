using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class BillingAdjustSettingDialog : BasePageDialog
    {
        private BillingAdjustItemSettingCollection BillingAdjustItemSettings
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBillingAdjustSetting"];
                    if (obj != null)
                        return ((BillingAdjustItemSettingCollection)(obj));
                }

                BillingAdjustItemSettingQuery query;

                query = new BillingAdjustItemSettingQuery("a");
                var par = new ParamedicQuery("par");
                var pSpec = new AppStandardReferenceItemQuery("pSpec");
                var rType = new AppStandardReferenceItemQuery("rType");
                var tType = new AppStandardReferenceItemQuery("tType");
                var g = new GuarantorQuery("g");
                var su = new ServiceUnitQuery("su");
                var i = new ItemQuery("i");
                var tc = new TariffComponentQuery("tc");
                var cl = new ClassQuery("cl");

                query.LeftJoin(par).On(query.ParamedicID.Equal(par.ParamedicID))
                    .LeftJoin(pSpec).On(query.SRSpecialty.Equal(pSpec.ItemID) && pSpec.StandardReferenceID.Equal(AppEnum.StandardReference.Specialty))
                    .LeftJoin(rType).On(query.SRRegistrationType.Equal(rType.ItemID) && rType.StandardReferenceID.Equal(AppEnum.StandardReference.RegistrationType))
                    .LeftJoin(tType).On(query.SRTariffType.Equal(tType.ItemID) && tType.StandardReferenceID.Equal(AppEnum.StandardReference.TariffType))
                    .LeftJoin(g).On(query.GuarantorID.Equal(g.GuarantorID))
                    .LeftJoin(su).On(query.ServiceUnitID.Equal(su.ServiceUnitID))
                    .LeftJoin(i).On(query.ItemID.Equal(i.ItemID))
                    .LeftJoin(tc).On(query.TariffComponentID.Equal(tc.TariffComponentID))
                    .LeftJoin(cl).On(query.ClassID.Equal(cl.ClassID))
                    .Select
                    (
                        query,
                        par.ParamedicName.As("refToParamedic_Name"),
                        pSpec.ItemName.As("refToSRSpecialty_Name"),
                        rType.ItemName.As("refToSRRegistrationType_Name"),
                        tType.ItemName.As("refToSRTariffType_Name"),
                        g.GuarantorName.As("refToGuarantor_Name"),
                        su.ServiceUnitName.As("refToServiceUnit_Name"),
                        i.ItemName.As("refToItem_Name"),
                        tc.TariffComponentName.As("refToTariffComponent_Name"),
                        cl.ClassName.As("refToClass_Name"),
                        "<'' refToItemGroup_ReplacementName>"
                    )
                    .OrderBy(
                        par.ParamedicName.Ascending,
                        pSpec.ItemName.Ascending,
                        rType.ItemName.Ascending,
                        tType.ItemName.Ascending,
                        g.GuarantorName.Ascending,
                        su.ServiceUnitName.Ascending,
                        i.ItemName.Ascending,
                        tc.TariffComponentName.Ascending,
                        cl.ClassName.Ascending
                    );

                var coll = new BillingAdjustItemSettingCollection();
                coll.Load(query);

                foreach (var bais in coll) {
                    bais.FillReferrence(true);
                }

                //Bila nama session dirubah, rubah jug yg di StandardReferenceItemDetail.ascx.cs
                Session["collBillingAdjustSetting"] = coll;
                return coll;
            }
            set
            {
                Session["collBillingAdjustSetting"] = value;
            }
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Reset Record Detail
                //BillingAdjustSettings = null;
            }
        }

        protected void grid_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var rdg = (RadGrid)source;
            switch (rdg.ID) {
                case "grdList":
                    {
                    rdg.DataSource = BillingAdjustItemSettings;
                    break;
                }
                case "": {
                    break;
                }
            }
        }

        protected void grid_InsertCommand(object source, GridCommandEventArgs e)
        {
            var rdg = (RadGrid)source;
            switch (rdg.ID)
            {
                case "grdList":
                    {
                        BillingAdjustItemSetting entity = BillingAdjustItemSettings.AddNew();
                        SetEntityValue(entity, e);

                        //grid not close first
                        e.Canceled = true;
                        grdList.Rebind();
                        break;
                    }
            }
        }

        protected void grid_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var rdg = (RadGrid)source;
            switch (rdg.ID)
            {
                case "grdList":
                    {
                        GridEditableItem editedItem = e.Item as GridEditableItem;
                        int id = (int)editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id"];

                        BillingAdjustItemSetting item = BillingAdjustItemSettings.FindByPrimaryKey(id);

                        SetEntityValue(item, e);
                        break;
                    }
                case "":
                    {
                        break;
                    }
            }
        }

        protected void grid_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var rdg = (RadGrid)source;
            switch (rdg.ID)
            {
                case "grdList":
                    {
                        var item = e.Item as GridDataItem;
                        if (item == null)
                            return;

                        var id = System.Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][BillingAdjustItemSettingMetadata.ColumnNames.Id]);
                        var entity = BillingAdjustItemSettings.FindByPrimaryKey(id);
                        entity.MarkAsDeleted();
                        break;
                    }
            }
        }

        protected void grid_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem row in grdList.Items)
            {
                CheckBox chkPercent = (CheckBox)row["IsFeeValueInPercent"].Controls[0];
                if (chkPercent != null)
                {
                    RadTextBox x = (RadTextBox)row["colFeeValue"].FindControl("txtFeeValue");
                    if (x != null)
                    {
                        x.Text = row["FeeValue"].Text + (chkPercent.Checked ? "%" : "");
                    }
                }
            }

            for (int rowIndex = grdList.Items.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridDataItem row = grdList.Items[rowIndex];
                GridDataItem previousRow = grdList.Items[rowIndex + 1];
                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text)
                {
                    row["ParamedicName"].RowSpan = previousRow["ParamedicName"].RowSpan < 2 ?
                        2 : previousRow["ParamedicName"].RowSpan + 1;
                    previousRow["ParamedicName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text)
                {
                    row["SpecialtyName"].RowSpan = previousRow["SpecialtyName"].RowSpan < 2 ?
                        2 : previousRow["SpecialtyName"].RowSpan + 1;
                    previousRow["SpecialtyName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text)
                {
                    row["RegistrationTypeName"].RowSpan = previousRow["RegistrationTypeName"].RowSpan < 2 ?
                        2 : previousRow["RegistrationTypeName"].RowSpan + 1;
                    previousRow["RegistrationTypeName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text)
                {
                    row["TariffTypeName"].RowSpan = previousRow["TariffTypeName"].RowSpan < 2 ?
                        2 : previousRow["TariffTypeName"].RowSpan + 1;
                    previousRow["TariffTypeName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text)
                {
                    row["GuarantorName"].RowSpan = previousRow["GuarantorName"].RowSpan < 2 ?
                        2 : previousRow["GuarantorName"].RowSpan + 1;
                    previousRow["GuarantorName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text)
                {
                    row["ServiceUnitName"].RowSpan = previousRow["ServiceUnitName"].RowSpan < 2 ?
                        2 : previousRow["ServiceUnitName"].RowSpan + 1;
                    previousRow["ServiceUnitName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text)
                {
                    row["ItemName"].RowSpan = previousRow["ItemName"].RowSpan < 2 ?
                        2 : previousRow["ItemName"].RowSpan + 1;
                    previousRow["ItemName"].Visible = false;
                }

                if (row["ParamedicName"].Text == previousRow["ParamedicName"].Text &&
                    row["SpecialtyName"].Text == previousRow["SpecialtyName"].Text &&
                    row["RegistrationTypeName"].Text == previousRow["RegistrationTypeName"].Text &&
                    row["TariffTypeName"].Text == previousRow["TariffTypeName"].Text &&
                    row["GuarantorName"].Text == previousRow["GuarantorName"].Text &&
                    row["ServiceUnitName"].Text == previousRow["ServiceUnitName"].Text &&
                    row["ItemName"].Text == previousRow["ItemName"].Text &&
                    row["ClassName"].Text == previousRow["ClassName"].Text)
                {
                    row["ClassName"].RowSpan = previousRow["ClassName"].RowSpan < 2 ?
                        2 : previousRow["ClassName"].RowSpan + 1;
                    previousRow["ClassName"].Visible = false;
                }
            }

            // hide empty columns
            int colParamedicName = 0, colSpecialtyName = 0, colRegistrationTypeName = 0,
                colTariffTypeName = 0, colGuarantorName = 0, colServiceUnitName = 0,
                colItemGroupName = 0, colItemName = 0, colClassName = 0;
            for (int rowIndex = 0; rowIndex < grdList.Items.Count; rowIndex++)
            {
                GridDataItem row = grdList.Items[rowIndex];

                colParamedicName += (row["ParamedicName"].Text.Replace("&nbsp;","") == string.Empty) ? 0 : 1;
                colSpecialtyName += (row["SpecialtyName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colRegistrationTypeName += (row["RegistrationTypeName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colTariffTypeName += (row["TariffTypeName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colGuarantorName += (row["GuarantorName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colServiceUnitName += (row["ServiceUnitName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colItemName += (row["ItemName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
                colClassName += (row["ClassName"].Text.Replace("&nbsp;", "") == string.Empty) ? 0 : 1;
            }
            grdList.Columns.FindByDataField("ParamedicName").Visible = colParamedicName > 0;
            grdList.Columns.FindByDataField("SpecialtyName").Visible = colSpecialtyName > 0;
            grdList.Columns.FindByDataField("RegistrationTypeName").Visible = colRegistrationTypeName > 0;
            grdList.Columns.FindByDataField("TariffTypeName").Visible = colTariffTypeName > 0;
            grdList.Columns.FindByDataField("GuarantorName").Visible = colGuarantorName > 0;
            grdList.Columns.FindByDataField("ServiceUnitName").Visible = colServiceUnitName > 0;
            grdList.Columns.FindByDataField("ItemName").Visible = colItemName > 0;
            grdList.Columns.FindByDataField("ClassName").Visible = colClassName > 0;
        }

        private void SetEntityValue(BillingAdjustItemSetting entity, GridCommandEventArgs e)
        {
            BillingAdjustSettingDetail ctl = (BillingAdjustSettingDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (ctl != null)
            {
                entity.ParamedicID = ctl.ParamedicID;
                entity.SRSpecialty = ctl.SRSpecialty;
                entity.SRRegistrationType = ctl.SRRegistrationType;
                entity.SRTariffType = ctl.SRTariffType;
                entity.GuarantorID = ctl.GuarantorID;
                entity.ServiceUnitID = ctl.ServiceUnitID;
                entity.ItemID = ctl.ItemID;
                entity.ClassID = ctl.ClassID;
                entity.TariffComponentID = ctl.TariffComponentID;
                entity.IsFeeValueInPercent = ctl.IsFeeValueInPercent;
                entity.FeeValue = ctl.FeeValue;
                entity.ItemGroupIDsReplacement = ctl.ItemGroupIDsReplacement;
                //Last Update Status
                if (entity.es.IsAdded)
                {
                    entity.CreateByUserID = AppSession.UserLogin.UserID;
                    entity.CreateDateTime = DateTime.Now;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
                if (entity.es.IsModified)
                {
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }

                entity.FillReferrence(false);
            }
        }

        #region Tab 2
        private BillingAdjustItemGroupSettingCollection ItemGroups
        {
            get
            {
                var obj = ViewState["BillingAdjustSetting:BillingAdjustItemGroupSetting"];
                if (obj != null)
                    return ((BillingAdjustItemGroupSettingCollection)(obj));

                var igColl = new BillingAdjustItemGroupSettingCollection();
                var baigQ = new BillingAdjustItemGroupSettingQuery("a");
                var igQ = new ItemGroupQuery("b");
                baigQ.RightJoin(igQ).On(igQ.ItemGroupID == baigQ.ItemGroupID)
                    .Where(igQ.IsActive == true)
                    .Select(igQ.ItemGroupID, baigQ.DiscValue, baigQ.DiscSelection, igQ.ItemGroupName.As("refToItemGroup_Name"))
                    .OrderBy(igQ.ItemGroupName.Ascending);
                igColl.Load(baigQ);

                ViewState["BillingAdjustSetting:BillingAdjustItemGroupSetting"] = igColl;

                return igColl;
            }
        }

        protected void grdItemGroup_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItemGroup.DataSource = ItemGroups;
        }

        protected void grdItemGroup_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var ba = e.Item.DataItem as BillingAdjustItemGroupSetting;
                if ((ba.DiscValue ?? 0) > 0)
                {
                    var txtDisc = e.Item.FindControl("txtDisc") as RadNumericTextBox;
                    if (txtDisc != null)
                    {
                        txtDisc.Value = System.Convert.ToDouble(ba.DiscValue);
                    }

                    var rblDisc = e.Item.FindControl("rblDiscSelection") as RadioButtonList;
                    if (rblDisc != null)
                    {
                        rblDisc.SelectedValue = ba.DiscSelection.ToString();
                    }
                }
            }
        }
        #endregion

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (false)
            {
                ShowInformationHeader("Raise Error here.");
                return false;
            }

            var baigColl = new BillingAdjustItemGroupSettingCollection();
            baigColl.LoadAll();
            List<string> ItemGroupIDs = new List<string>();

            foreach (GridDataItem row in grdItemGroup.Items) {
                string ItemGroupID = row.GetDataKeyValue("ItemGroupID").ToString();
                RadNumericTextBox txtDisc = row.FindControl("txtDisc") as RadNumericTextBox;
                RadioButtonList rblDiscSelection = row.FindControl("rblDiscSelection") as RadioButtonList;
                if(txtDisc == null) continue;
                if (!txtDisc.Value.HasValue) continue;

                var baig = baigColl.Where(x => x.ItemGroupID == ItemGroupID).FirstOrDefault();
                if (baig == null) {
                    baig = baigColl.AddNew();
                    baig.ItemGroupID = ItemGroupID;
                }
                baig.DiscValue = System.Convert.ToDecimal(txtDisc.Value ?? 0);
                baig.DiscSelection = System.Convert.ToInt32(rblDiscSelection.SelectedValue);
                if (baig.es.IsAdded)
                {
                    baig.CreateDateTime = DateTime.Now;
                    baig.CreateByUserID = AppSession.UserLogin.UserID;
                }
                baig.LastUpdateDateTime = DateTime.Now;
                baig.LastUpdateByUserID = AppSession.UserLogin.UserID;

                ItemGroupIDs.Add(ItemGroupID);
            }

            IEnumerable<BillingAdjustItemGroupSetting> tobeDel;
            if (ItemGroupIDs.Count > 0)
            {
                tobeDel = baigColl.Where(x => !ItemGroupIDs.Contains(x.ItemGroupID));
            }
            else {
                tobeDel = baigColl as IEnumerable<BillingAdjustItemGroupSetting>;
            }
            foreach (var ItemGroup in tobeDel)
            {
                ItemGroup.MarkAsDeleted();
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                BillingAdjustItemSettings.Save();
                baigColl.Save();

                trans.Complete();
            }

            return true;
        }
    }
}
