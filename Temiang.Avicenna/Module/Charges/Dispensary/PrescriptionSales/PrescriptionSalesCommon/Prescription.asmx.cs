using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;
using System.Threading;
using static Temiang.Avicenna.Module.Charges.PrescriptionSalesItemDetail;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using DocumentFormat.OpenXml.Drawing;

namespace Temiang.Avicenna.Module.Charges.Dispensary.PrescriptionSales.PrescriptionSalesCommon
{
    /// <summary>
    /// Summary description for Prescription
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Prescription : System.Web.Services.WebService
    {
        public static string RenderView(string path, object data)
        {
            Page pageHolder = new Page();
            UserControl viewControl = (UserControl)pageHolder.LoadControl(path);

            if (data != null)
            {
                Type viewControlType = viewControl.GetType();
                FieldInfo field = viewControlType.GetField("Data");

                if (field != null)
                {
                    field.SetValue(viewControl, data);
                }
                else
                {
                    throw new Exception("View file: " + path + " does not have a public Data property");
                }
            }

            pageHolder.Controls.Add(viewControl);
            StringWriter output = new StringWriter();
            HttpContext.Current.Server.Execute(pageHolder, output, false);
            return output.ToString();
        }

        [WebMethod]
        public string PrevBuyToolTipData(object context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            string filterInfo = ((string)contextDictionary["Value"]);

            if (filterInfo == string.Empty)
            {
                throw new Exception("No Value argument is provided to the webservice!");
            }

            // var val = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", string.Join( ",", PatientIds),txtPrescriptionNo.Text, tpi.ItemID, tpi.ItemName, tpi.ItemInterventionID, tpi.ItemInterventionName);
            var infos = filterInfo.Split('|');
            var patiendIds = infos[0].Split(',');
            var prescriptionNo = infos[1];
            var itemID = infos[2];
            var itemName = infos[3];
            var itemInterventionID = infos[4];
            var itemInterventionName = infos[5];

            var tp = new TransPrescriptionQuery("tp");

            var reg = new RegistrationQuery("r1");
            tp.InnerJoin(reg).On(tp.RegistrationNo == reg.RegistrationNo);

            var tpi = new TransPrescriptionItemQuery("tpi");
            tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);

            if (patiendIds.Length > 1)
                tp.Where(reg.PatientID.In(patiendIds));
            else
                tp.Where(reg.PatientID == patiendIds[0]);
            tp.Where(tp.IsApproval.Equal(true), tp.PrescriptionNo != prescriptionNo);

            if (!string.IsNullOrWhiteSpace(itemInterventionID))
                tp.Where(tp.Or(tpi.ItemID.In(new string[] { itemID, itemInterventionID }), tpi.ItemInterventionID.In(new string[] { itemID, itemInterventionID })));
            else
                tp.Where(tp.Or(tpi.ItemID == itemID, tpi.ItemInterventionID == itemID));

            tp.OrderBy(tp.PrescriptionDate.Descending);
            tp.Select(tpi.ItemID, tpi.TakenQty, tpi.SRItemUnit,
                tp.PrescriptionDate,
                "<ISNULL(tpi.DaysOfUsage,0) DaysOfUsage>", tpi.SRConsumeMethod, tpi.ConsumeQty, tpi.SRConsumeUnit);
            tp.es.Top = 1;
            var dtb = tp.LoadDataTable();
            dtb.Columns.Add("ItemName", typeof(string));
            dtb.Columns.Add("Qty", typeof(string));
            dtb.Columns.Add("Date", typeof(string));
            dtb.Columns.Add("TotalDays", typeof(string));
            dtb.Columns.Add("ConsumeMethod", typeof(string));
            dtb.Columns.Add("Color", typeof(string));
            if (dtb.Rows.Count > 0)
            {
                var row = dtb.Rows[0];
                row["ItemName"] = itemID.Equals(row["ItemID"]) ? itemName : itemInterventionName;

                row["Qty"] = Helper.RemoveZeroDigits(Convert.ToDecimal(row["TakenQty"]));
                row["Date"] = ((DateTime)row["PrescriptionDate"]).ToString("dd/MM/yyyy");
                row["TotalDays"] = (DateTime.Now.Date - ((DateTime)row["PrescriptionDate"])).TotalDays.ToString();


                var cm = new ConsumeMethod();
                cm.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());

                var cmUnit = new AppStandardReferenceItem();
                cmUnit.LoadByPrimaryKey("DosageUnit", row["SRConsumeUnit"].ToString());
                row["ConsumeMethod"] = String.Format("{0} @{1} {2}", cm.SRConsumeMethodName, row["ConsumeQty"], cmUnit.ItemName);

                var cl = new System.Drawing.Color();
                if ((int)row["DaysOfUsage"] > 0 && ((DateTime)row["PrescriptionDate"]).AddDays((int)row["DaysOfUsage"]) > DateTime.Now)
                {
                    // RED TEXT
                    row["Color"] = "red";
                }
                else
                {
                    row["Color"] = "black";
                }
            }
            else
            {
                var row = dtb.NewRow();
                row["ItemName"] = string.IsNullOrEmpty(itemInterventionName) ? itemName : itemInterventionName;
                row["Qty"] = "-";
                row["Date"] = "-";
                row["TotalDays"] = "-";
                row["ConsumeMethod"] = "-";
                row["Color"] = "black";
                dtb.Rows.Add(row);
            }
            return RenderView("~/Module/Charges/Dispensary/PrescriptionSales/PrescriptionSalesCommon/PrevBuyInfo.ascx", dtb);
        }
    }
}
