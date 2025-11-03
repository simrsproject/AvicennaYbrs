using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.CustomControl
{
    /// <summary>
    /// Summary description for QueryData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RadComboBoxExtData : System.Web.Services.WebService
    {
        private const int MinSearchLength = 0;
        private const int MaxQueryRecord = 15;

        #region base function

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, esDynamicQuery query)
        {
            return PopulateComboBoxDataItems(context, query, "ValueField", "TextField");
        }

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, esDynamicQuery query, string valueField, string textField)
        {
            var result = new List<RadComboBoxItemData>(context.NumberOfItems);
            var comboData = new RadComboBoxData {Items = result.ToArray()};

            try
            {
                if (!string.IsNullOrEmpty(context.Text) && context.Text.Trim().Length >= MinSearchLength)
                {
                    var data = query.LoadDataTable();

                    const int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > data.Rows.Count)
                    {
                        endOffset = data.Rows.Count;
                    }
                    if (endOffset == data.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>(endOffset - itemOffset);
                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();
                        itemData.Text = data.Rows[i][textField].ToString().Trim();
                        itemData.Value = data.Rows[i][valueField].ToString();

                        var otherFields = (from DataColumn column in data.Columns
                                           where column.ColumnName != valueField && column.ColumnName != textField
                                           select column.ColumnName).ToArray();

                        if (otherFields.Length > 0)
                        {
                            foreach (string fieldName in otherFields)
                            {
                                itemData.Attributes.Add(fieldName, data.Rows[i][fieldName].ToString());
                            }
                        }
                        result.Add(itemData);
                    }

                    if (data.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Records <b>1</b>-<b>{0}</b> out of <b>{1}{2}</b>", endOffset.ToString(),
                            data.Rows.Count.ToString(), data.Rows.Count <= MaxQueryRecord ? string.Empty : "+");
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }

                    comboData.Items = result.ToArray();

                }
            }
            catch (Exception e)
            {
                comboData.Message = e.Message;
            }

            return comboData;
        }

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, esEntityCollection data, string textField,
    string valueField, params string[] otherFields)
        {
            List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
            RadComboBoxData comboData = new RadComboBoxData();

            int itemsPerRequest = 10;
            int itemOffset = context.NumberOfItems;
            int endOffset = itemOffset + itemsPerRequest;
            if (endOffset > data.Count)
            {
                endOffset = data.Count;
            }
            if (endOffset == data.Count)
            {
                comboData.EndOfItems = true;
            }
            else
            {
                comboData.EndOfItems = false;
            }
            result = new List<RadComboBoxItemData>(endOffset - itemOffset);
            for (int i = itemOffset; i < endOffset; i++)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                var entity = ((esEntity)data[i]);
                itemData.Text = entity.GetColumn(textField).ToString().Trim();
                itemData.Value = entity.GetColumn(valueField).ToString();
                if (otherFields.Length > 0)
                {
                    foreach (string fieldName in otherFields)
                    {
                        itemData.Attributes.Add(fieldName, entity.GetColumn(fieldName).ToString());
                    }
                }
                result.Add(itemData);
            }

            if (data.Count > 0)
            {
                comboData.Message = String.Format("Records <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(),
                    data.Count.ToString());
            }
            else
            {
                comboData.Message = "No matches";
            }

            comboData.Items = result.ToArray();
            return comboData;
        }
        #endregion

        [WebMethod]
        public RadComboBoxData MedicalNos(RadComboBoxContext context)
        {
            List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
            RadComboBoxData comboData = new RadComboBoxData();
            try
            {
                if (!string.IsNullOrEmpty(context.Text) && context.Text.Trim().Length >= MinSearchLength)
                {
                    var qr = new PatientQuery("p");
                    qr.Select(qr.PatientID, qr.MedicalNo, qr.PatientName, qr.CityOfBirth, qr.DateOfBirth, qr.Address);

                    qr.Where(qr.MedicalNo.Like(string.Format("{0}%", context.Text)) ||
                             qr.FirstName.Like(string.Format("%{0}%", context.Text)));

                    qr.OrderBy(qr.PatientName.Ascending);
                    qr.es.Top = MaxQueryRecord;
                    var data = qr.LoadDataTable();

                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > data.Rows.Count)
                    {
                        endOffset = data.Rows.Count;
                    }
                    if (endOffset == data.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>(endOffset - itemOffset);
                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();
                        itemData.Text = data.Rows[i]["MedicalNo"].ToString().Trim();
                        itemData.Value = data.Rows[i]["PatientID"].ToString();
                        itemData.Attributes.Add("PatientName", data.Rows[i]["PatientName"].ToString());
                        itemData.Attributes.Add("CityOfBirth", data.Rows[i]["CityOfBirth"].ToString());
                        itemData.Attributes.Add("DateOfBirth",
                            Convert.ToDateTime(data.Rows[i]["DateOfBirth"]).ToShortDateString());
                        itemData.Attributes.Add("Address", data.Rows[i]["Address"].ToString());

                        result.Add(itemData);
                    }

                    if (data.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Records <b>1</b>-<b>{0}</b> out of <b>{1}+</b>",
                            endOffset.ToString(), data.Rows.Count.ToString());
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }
                }
            }
            catch (Exception e)
            {
                comboData.Message = e.Message;
            }

            comboData.Items = result.ToArray();
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData Guarantors(RadComboBoxContext context)
        {
            var qr = new GuarantorQuery("p");
            qr.Select(qr.GuarantorID.As("ValueField"), qr.GuarantorName.As("TextField"));
            qr.Where(qr.GuarantorName.Like(string.Format("%{0}%", context.Text)), qr.IsActive == true);
            qr.OrderBy(qr.GuarantorName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData GuarantorGroups(RadComboBoxContext context)
        {
            var qr = new GuarantorQuery("p");
            var qrd = new GuarantorQuery("q");
            qr.InnerJoin(qrd).On(qr.GuarantorHeaderID == qrd.GuarantorID);
            qr.Select(qr.GuarantorHeaderID.As("ValueField"), qrd.GuarantorName.As("TextField"));
            qr.Where(qrd.GuarantorName.Like(string.Format("%{0}%", context.Text)), qr.IsActive == true);
            qr.OrderBy(qrd.GuarantorName.Ascending);
            qr.es.Top = MaxQueryRecord;
            qr.es.Distinct = true;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ZipCodes(RadComboBoxContext context)
        {
            var qr = new BusinessObject.ZipCodeQuery();
            qr.Select
                (
                    qr.ZipCode,
                    qr.District,
                    qr.County,
                    qr.City
                );
            qr.Where
                (
                    qr.Or
                        (
                            qr.ZipCode.Like(string.Format("%{0}%", context.Text)),
                            qr.District.Like(string.Format("%{0}%", context.Text)),
                            qr.County.Like(string.Format("%{0}%", context.Text)),
                            qr.City.Like(string.Format("%{0}%", context.Text))
                        )
                );

            qr.OrderBy(qr.District.Ascending);
            qr.es.Top = MaxQueryRecord;
            var comboData = PopulateComboBoxDataItems(context, qr, "ZipCode", "ZipCode");
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ChartOfAccountCodes(RadComboBoxContext context)
        {
            var result = new List<RadComboBoxItemData>(context.NumberOfItems);
            var comboData = new RadComboBoxData { Items = result.ToArray() };

            try
            {
                if (!string.IsNullOrEmpty(context.Text) && context.Text.Trim().Length >= MinSearchLength)
                {
                    var colls = ChartOfAccounts.GetLike(context.Text, true, true);
                    comboData = PopulateComboBoxDataItems(context, colls, "ChartOfAccountCode", "ChartOfAccountCode",
                        "ChartOfAccountName");
                }
            }
            catch (Exception e)
            {
                comboData.Message = e.Message;
            }

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData JournalCode(RadComboBoxContext context)
        {
            var result = new List<RadComboBoxItemData>(context.NumberOfItems);
            var comboData = new RadComboBoxData { Items = result.ToArray() };

            try
            {
                if (!string.IsNullOrEmpty(context.Text))
                {
                    var colls = JournalCodes.GetLike(context.Text, false);
                    comboData = PopulateComboBoxDataItems(context, colls, "JournalCode", "JournalCode",
                        "Description");
                }
            }
            catch (Exception e)
            {
                comboData.Message = e.Message;
            }

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData Diagnoses(RadComboBoxContext context)
        {
            var qr = new DiagnoseQuery("d");
            var dtdQr = new DtdQuery("dtd");
            qr.InnerJoin(dtdQr).On(qr.DtdNo == dtdQr.DtdNo);
            qr.Select
                (
                    qr.DiagnoseID.As("ValueField"),
                    ("[" + qr.DiagnoseID.Trim() + "] " + qr.DiagnoseName.Trim()).As("TextField"),
                    qr.DiagnoseName,
                    qr.DtdNo,
                    dtdQr.DtdName,
                    qr.IsDisease,
                    qr.IsChronicDisease
                );
            qr.Where
                (
                    qr.IsActive == 1,
                    qr.Or
                        (
                            qr.DiagnoseID.Like(string.Format("%{0}%", context.Text)),
                            qr.DiagnoseName.Like(string.Format("%{0}%", context.Text)),
                            dtdQr.DtdName.Like(string.Format("%{0}%", context.Text))
                        )
                );

            qr.OrderBy(qr.DiagnoseName.Ascending);
            qr.es.Top = MaxQueryRecord;

            return PopulateComboBoxDataItems(context, qr);
        }

    }
}
