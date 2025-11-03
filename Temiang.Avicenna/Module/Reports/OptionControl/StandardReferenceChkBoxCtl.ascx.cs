using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class StandardReferenceChkBoxCtl : BaseOptionCtl
    {
        public override PrintJobParameterCollection PrintJobParameters()
        {
            PrintJobParameterCollection parameters = new PrintJobParameterCollection();
            var itemIds = string.Empty;
            var collection = cboItemID.CheckedItems;

            if (collection.Count != 0)
            {
                foreach (var item in collection)
                    itemIds = string.Concat(itemIds, ";", item.Value);

                itemIds = itemIds.Substring(1);
            }

            parameters.AddNew("p_ItemIDs", itemIds);

            //Retun List
            return parameters;
        }

        public override string ParameterCaption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }

        public override string ReferenceID
        {
            set
            {
                var query = new AppStandardReferenceItemQuery("a");
                query.Select(query.ItemID, query.ItemName);
                query.Where(query.StandardReferenceID == value);
                query.OrderBy(query.LineNumber.Ascending, query.ItemName.Ascending);
                query.es.Top = 50;
                var dtb = query.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    cboItemID.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(),row["ItemID"].ToString()));
                }
            }
        }

        public override string ReportSubTitle
        {
            get
            {
                return string.Format("Item : {0}", cboItemID.SelectedValue);
            }
        }

    }
}