using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl
{
    public class RadComboBoxExt:RadComboBox
    {
        public enum LookUpTypeEnum
        {
            ServiceUnit,
            Paramedic,
            ItemMedic,
            ItemNonMedic,
            
            GuarantorGroups,
            Guarantors
        }

        public string ItemTypeComboBoxName
        {
            get
            {
                var obj = ViewState["ItemTypeComboBoxName"];
                if (obj == null) return "";
                return (string)obj;
            }
            set { ViewState["ItemTypeComboBoxName"] = value; }
        }

        public string ServiceUnitComboBoxName
        {
            get
            {
                var obj = ViewState["ServiceUnitComboBoxName"];
                if (obj == null) return "";
                return (string)obj;
            }
            set { ViewState["ServiceUnitComboBoxName"] = value; }
        }

        public LookUpTypeEnum LookUpType
        {
            get
            {
                var obj = ViewState["LookUpType"];
                if (obj == null) return LookUpTypeEnum.ServiceUnit;
                return (LookUpTypeEnum)obj;
            }
            set { ViewState["LookUpType"] = value; }
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            EnableLoadOnDemand = true;
            ShowMoreResultsBox = true;
            EnableVirtualScrolling = true;
            this.WebServiceSettings.Method = LookUpType.ToString();
            this.WebServiceSettings.Path = "~/CustomControl/RadComboBoxExtData.asmx";

            //switch (LookUpType)
            //{
            //    case LookUpTypeEnum.ServiceUnit:
            //        break;
            //    case LookUpTypeEnum.Paramedic:
            //        break;
            //    case LookUpTypeEnum.ItemMedic:
            //        break;
            //    case LookUpTypeEnum.ItemNonMedic:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
        }

        protected override void OnItemsRequested(RadComboBoxItemsRequestedEventArgs args)
        {
            switch (LookUpType)
            {
                case LookUpTypeEnum.ServiceUnit:
                    break;
                case LookUpTypeEnum.Paramedic:
                    break;
                case LookUpTypeEnum.ItemMedic:
                case LookUpTypeEnum.ItemNonMedic:
                    ItemsRequestedItem(args.Text);
                    break;
            }

        }

        public void PopulateItemWithValue(string value)
        {
            switch (LookUpType)
            {
                case LookUpTypeEnum.GuarantorGroups:
                case LookUpTypeEnum.Guarantors:
                    PopulateWithOneGuarantor(value);
                    break;

            }
        }

        #region Data Query

        public void ItemsRequestedItem(string textSearch )
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var cboSRItemType = (RadComboBox)Helper.FindControlRecursive(Page, ItemTypeComboBoxName);
            var cboServiceUnit = (RadComboBox)Helper.FindControlRecursive(Page, ServiceUnitComboBoxName);

            var itemType=cboSRItemType.SelectedValue;
            var serviceUnitID = cboServiceUnit.SelectedValue;
            var serviceUnit = new ServiceUnit();
            var locationID = string.Empty;
            if (serviceUnit.LoadByPrimaryKey(serviceUnitID))
                locationID = serviceUnit.GetMainLocationId();

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            query.LeftJoin(bal).On(query.ItemID == bal.ItemID && bal.LocationID ==  locationID);

            query.Where(
                query.SRItemType == itemType,
                query.Or(query.ItemID == textSearch, query.ItemName.Like(string.Format("%{0}%", textSearch)))
                );

            if (itemType == BusinessObject.Reference.ItemType.Medical)
            {
                var prod = new ItemProductMedicQuery("c");
                query.LeftJoin(prod).On(query.ItemID == prod.ItemID);
                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");
                query.Select(query.ItemID, query.ItemName, bal.Balance, bal.Minimum, bal.Maximum, std.ItemName.As("Unit"));
            }
            else
            {
                var prod = new ItemProductNonMedicQuery("c");
                query.LeftJoin(prod).On(query.ItemID == prod.ItemID);
                var std = new AppStandardReferenceItemQuery("d");
                query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");
                query.Select(query.ItemID, query.ItemName, bal.Balance, bal.Minimum, bal.Maximum, std.ItemName.As("Unit"));

            }
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            DataSource = dtb;
            DataBind();
            if (dtb.Rows.Count > 0)
            {
                Text = dtb.Rows[0]["ItemName"].ToString();
                SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        private void PopulateWithOneGuarantor(string guarantorId)
        {
            this.Items.Clear();
            if (string.IsNullOrEmpty(guarantorId))
                return;

            var qr = new GuarantorQuery();
            qr.Select(qr.GuarantorID, qr.GuarantorName);
            qr.Where(qr.GuarantorID == guarantorId);

            var dtb = qr.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                var row = dtb.Rows[0];
                var text = string.Format("{0}", row["GuarantorName"]);
                var value = row["GuarantorID"].ToString();
                this.Items.Add(new RadComboBoxItem(text, value));
                this.SelectedValue = value;
            }
        }

        private void PopulateWithOneGuarantor(string[] guarantorId)
        {
            this.Items.Clear();
            if (!guarantorId.Any()) return;

            var qr = new GuarantorQuery();
            qr.Select(qr.GuarantorID, qr.GuarantorName);
            qr.Where(qr.GuarantorID.In(guarantorId));

            var dtb = qr.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                var row = dtb.Rows[0];
                //var text = string.Format("[{0}] {1}", row["GuarantorID"], row["GuarantorName"]);
                var text = string.Format("{0}", row["GuarantorName"]);
                var value = row["GuarantorID"].ToString();
                this.Items.Add(new RadComboBoxItem(text, value));
                this.SelectedValue = value;
            }
        }

        #endregion
    }
}
