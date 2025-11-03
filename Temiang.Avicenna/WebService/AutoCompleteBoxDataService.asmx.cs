using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for AutoCompleteBoxDataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteBoxDataService : System.Web.Services.WebService
    {

        private const int MaxRecordCount = 20;

        private string SearchString(object context)
        {
            return ((Dictionary<string, object>)context)["Text"].ToString();
        }

        private AutoCompleteBoxData InitializedFromStandardReference(object context, AppEnum.StandardReference standardReference)
        {
            string searchString = SearchString(context);
            var coll = StandardReference.LoadStandardReferenceItemCollection(standardReference, searchString);
            List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

            foreach (var row in coll)
            {
                AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                childNode.Text = row.ItemName;
                childNode.Value = row.ItemID;
                result.Add(childNode);
            }

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            res.Items = result.ToArray();

            return res;
        }

        [WebMethod]
        public AutoCompleteBoxData InfusLocation(object context)
        {
            return InitializedFromStandardReference(context, AppEnum.StandardReference.InfusLocation);
        }

        [WebMethod]
        public AutoCompleteBoxData UserType(object context)
        {
            return InitializedFromStandardReference(context, AppEnum.StandardReference.UserType);
        }

        [WebMethod]
        public AutoCompleteBoxData LiquidTime(object context)
        {
            return InitializedFromStandardReference(context, AppEnum.StandardReference.LiquidTime);
        }

        private AutoCompleteBoxData PopulateAutoCompleteBoxData(DataTable dtbSource, string fieldText, string fieldValue)
        {
            List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

            foreach (DataRow row in dtbSource.Rows)
            {
                AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
                childNode.Text = row[fieldText].ToString();
                childNode.Value = row[fieldValue].ToString();
                result.Add(childNode);
            }

            AutoCompleteBoxData res = new AutoCompleteBoxData();
            res.Items = result.ToArray();

            return res;
        }


        [WebMethod]
        public AutoCompleteBoxData Antibiotic(object context)
        {
            string searchString = SearchString(context);

            var qr = new ItemQuery("i");
            var ipm = new ItemProductMedicQuery("ip");
            qr.InnerJoin(ipm).On(qr.ItemID == ipm.ItemID);
            qr.Where(ipm.IsAntibiotic == true, qr.ItemName.Like(string.Format("%{0}%", searchString)));
            qr.Select(qr.ItemID, qr.ItemName);
            qr.es.Top = MaxRecordCount;
            var dtb = qr.LoadDataTable();

            return PopulateAutoCompleteBoxData(dtb, "ItemName", "ItemID");
        }

        [WebMethod]
        public AutoCompleteBoxData OtherDrug(object context)
        {
            string searchString = SearchString(context);

            var qr = new ItemQuery("i");
            var ipm = new ItemProductMedicQuery("ip");
            qr.InnerJoin(ipm).On(qr.ItemID == ipm.ItemID);
            qr.Where(ipm.IsAntibiotic == false, qr.ItemName.Like(string.Format("%{0}%", searchString)));
            qr.Select(qr.ItemID, qr.ItemName);
            qr.es.Top = MaxRecordCount;
            var dtb = qr.LoadDataTable();

            return PopulateAutoCompleteBoxData(dtb, "ItemName", "ItemID");
        }

        [WebMethod]
        public AutoCompleteBoxData ZatActive(object context)
        {
            string searchString = SearchString(context);

            var qr = new ZatActiveQuery("i");
            var ipm = new ItemProductMedicQuery("ip");
            qr.Where(qr.ZatActiveName.Like(string.Format("%{0}%", searchString)));
            qr.Select(qr.ZatActiveID, qr.ZatActiveName);
            qr.es.Top = MaxRecordCount;
            var dtb = qr.LoadDataTable();

            return PopulateAutoCompleteBoxData(dtb, "ZatActiveName", "ZatActiveID");
        }
    }
}
