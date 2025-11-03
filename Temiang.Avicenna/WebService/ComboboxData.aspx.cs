using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.WebService
{
    public partial class ComboboxData : Page
    {
        [WebMethod]
        public static RadComboBoxData Paramedic(RadComboBoxContext context)
        {
            var query = new ParamedicQuery("b");
            query.Select
                (
                    query.ParamedicID,
                    query.ParamedicName
                );
            query.OrderBy(query.ParamedicName.Ascending);

            //In case the user typed something - filter the result set
            string text = context.Text;
            if (!String.IsNullOrEmpty(text))
            {
                string searchText = string.Format("%{0}%", text);
                query.Where(query.Or(query.ParamedicName.Like(searchText),
                    query.ParamedicID.Like(searchText)));
            }

            var dtb = query.LoadDataTable();
            var comboData = new RadComboBoxData();
            var itemOffset = context.NumberOfItems;
            var endOffset = Math.Min(itemOffset + 15, dtb.Rows.Count);
            comboData.EndOfItems = endOffset == dtb.Rows.Count;

            var result = new List<RadComboBoxItemData>(endOffset - itemOffset);

            for (var i = itemOffset; i < endOffset; i++)
            {
                var itemData = new RadComboBoxItemData
                                   {
                                       Text = string.Format("{0} [{1}]", dtb.Rows[i]["ParamedicName"], dtb.Rows[i]["ParamedicID"]),
                                       Value = dtb.Rows[i]["ParamedicID"].ToString()
                                   };

                result.Add(itemData);
            }

            comboData.Message = GetStatusMessage(endOffset, dtb.Rows.Count);

            comboData.Items = result.ToArray();
            comboData.EndOfItems = true;

            return comboData;
        }
        private static string GetStatusMessage(int offset, int total)
        {
            return total <= 0 ? "No matches" : String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", offset, total);
        }
    }
}
