using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for QueryData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ComboBoxDataService : System.Web.Services.WebService
    {
        #region Helper Methods
        private readonly string _clientID = SatuSehatKey("SatuSehatClientID");
        private readonly string _secretKey = SatuSehatKey("SatuSehatClientSecretKey");
        private readonly string _baseUrl = SatuSehatKey("SatuSehatBaseUrl");
        private readonly string _KFAbaseUrl = SatuSehatKey("SatuSehatKFABaseUrl");
        private readonly string _authUrl = SatuSehatKey("SatuSehatAuthUrl");
        private readonly string _organizationID = SatuSehatKey("SatuSehatOrganizationID");
        private static string SatuSehatKey(string key)
        {
            string configKey = string.Empty;
            var entity = new AppParameter();
            if (entity.LoadByPrimaryKey(key))
            {
                configKey = entity.ParameterValue;
            }
            else
            {
                configKey = ConfigurationManager.AppSettings[key];
                if (!HttpContext.Current.IsDebuggingEnabled)
                {
                    entity = new AppParameter
                    {
                        ParameterID = key,
                        ParameterName = key,
                        ParameterValue = configKey,
                        ParameterType = string.Empty,
                        IsUsedBySystem = true
                    };
                    entity.Save();
                }
            }
            return configKey;
        }

        private Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.TokenResponse GetToken()
        {
            var url = $"{_authUrl}/accesstoken?grant_type=client_credentials";
            var client = new RestClient(url);
            var request = new RestRequest { Method = Method.Post };
            var timeOutPar = AppParameter.GetParameterValue(AppParameter.ParameterItem.PCareTimeOutInSecond);
            int timeOut = Convert.ToInt16(timeOutPar) * 1000;
            request.Timeout = timeOut;
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", _clientID);
            request.AddParameter("client_secret", _secretKey);
            var response = client.Execute(request);
            try
            {
                if (response.Content.IsValidJson())
                {
                    var tokenResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.TokenResponse>(response.Content);
                    return tokenResponse;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}{Environment.NewLine}{response.Content}", ex);
            }
        }
        #endregion

        private const int MinSearchLength = 0;
        private int MaxQueryRecord = AppParameter.GetParameterValue(AppParameter.ParameterItem.ComboBoxDataServiceMaxResultRecord).ToInt();

        #region base function

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, esDynamicQuery query)
        {
            return PopulateComboBoxDataItems(context, query, "ValueField", "TextField");
        }

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, esDynamicQuery query, string valueField, string textField)
        {
            var result = new List<RadComboBoxItemData>(context.NumberOfItems);
            var comboData = new RadComboBoxData { Items = result.ToArray() };

            try
            {
                var dataTable = query.LoadDataTable();
                comboData = PopulateComboBoxDataItems(context, dataTable, valueField, textField);
            }
            catch (Exception e)
            {
                comboData.Message = e.Message;
            }

            return comboData;
        }

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, DataTable dataTable, string valueField, string textField)
        {
            var result = new List<RadComboBoxItemData>(context.NumberOfItems);
            var comboData = new RadComboBoxData { Items = result.ToArray() };

            try
            {
                if (!string.IsNullOrEmpty(context.Text) && context.Text.Trim().Length >= MinSearchLength)
                {
                    const int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dataTable.Rows.Count)
                    {
                        endOffset = dataTable.Rows.Count;
                    }

                    if (endOffset == dataTable.Rows.Count)
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
                        itemData.Text = dataTable.Rows[i][textField].ToString().Trim();
                        itemData.Value = dataTable.Rows[i][valueField].ToString();

                        var otherFields = (from DataColumn column in dataTable.Columns
                                           where column.ColumnName != valueField && column.ColumnName != textField
                                           select column.ColumnName).ToArray();


                        if (otherFields.Length > 0)
                        {
                            foreach (string fieldName in otherFields)
                            {
                                itemData.Attributes.Add(fieldName, dataTable.Rows[i][fieldName].ToString());
                            }
                        }

                        result.Add(itemData);
                    }

                    if (dataTable.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Records <b>1</b>-<b>{0}</b> out of <b>{1}{2}</b>", endOffset.ToString(),
                            dataTable.Rows.Count.ToString(), dataTable.Rows.Count <= MaxQueryRecord ? string.Empty : "+");
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

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, DataTable dtb, string textField, string valueField, params string[] otherFields)
        {
            List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
            RadComboBoxData comboData = new RadComboBoxData();

            int itemsPerRequest = 10;
            int itemOffset = context.NumberOfItems;
            int endOffset = itemOffset + itemsPerRequest;
            var rowCount = dtb.Rows.Count;
            if (endOffset > rowCount)
            {
                endOffset = rowCount;
            }
            if (endOffset == rowCount)
            {
                comboData.EndOfItems = true;
            }
            else
            {
                comboData.EndOfItems = false;
            }
            result = new List<RadComboBoxItemData>(endOffset - itemOffset);

            int i = 0;
            foreach (DataRow row in dtb.Rows)
            {
                if (i >= itemOffset)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = row[textField].ToString().Trim();
                    itemData.Value = row[valueField].ToString();
                    if (otherFields.Length > 0)
                    {
                        foreach (string fieldName in otherFields)
                        {
                            itemData.Attributes.Add(fieldName, row[fieldName].ToString());
                        }
                    }

                    result.Add(itemData);
                }
                i++;
                if (i > endOffset)
                    break;

            }
            if (rowCount > 0)
            {
                comboData.Message = String.Format("Records <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(),
                    rowCount.ToString());
            }
            else
            {
                comboData.Message = "No matches";
            }

            comboData.Items = result.ToArray();
            return comboData;
        }

        private RadComboBoxData PopulateComboBoxDataItems(RadComboBoxContext context, esEntityCollection data, string textField, string valueField, params string[] otherFields)
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
        public RadComboBoxData PatientMedicalNos(RadComboBoxContext context)
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
            qr.Where(qr.GuarantorName.Like(string.Format("%{0}%", context.Text)));
            qr.Where(qr.IsActive == true);
            qr.OrderBy(qr.GuarantorName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod(EnableSession = true)]
        public RadComboBoxData GuarantorsNotTypeSelf(RadComboBoxContext context)
        {
            var qr = new GuarantorQuery("p");
            qr.Select(qr.GuarantorID.As("ValueField"), qr.GuarantorName.As("TextField"));
            qr.Where(qr.GuarantorName.Like(string.Format("%{0}%", context.Text)));
            qr.Where(qr.SRGuarantorType != AppSession.Parameter.GuarantorTypeSelf);
            qr.Where(qr.IsActive == true);
            qr.OrderBy(qr.GuarantorName.Ascending);
            qr.es.Top = MaxQueryRecord;

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
                    var colls = JournalCodes.GetLike(context.Text, true);
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
                    string.Format("<CASE WHEN  CHARINDEX('{0}', d.DiagnoseName) = 0 THEN 1000 ELSE CHARINDEX('{0}', d.DiagnoseName) END  SearchIdx>", context.Text),
                    qr.DiagnoseID.As("ValueField"),
                    // ("[" + qr.DiagnoseID.Trim() + "] " + qr.DiagnoseName.Trim()).As("TextField"),
                    qr.DiagnoseID.As("TextField"),
                    qr.DiagnoseName,
                    qr.DtdNo,
                    dtdQr.DtdName,
                    qr.IsDisease,
                    qr.IsChronicDisease
                );
            qr.Where
                (
                    qr.IsActive == 1
                );

            if (!string.IsNullOrWhiteSpace(context.Text))
            {
                if (!context.Text.Contains(" "))
                {
                    qr.Where
                    (
                        qr.Or
                            (
                                qr.Synonym.Like(string.Format("%{0}%", context.Text)),
                                qr.DiagnoseID.Like(string.Format("%{0}%", context.Text)),
                                qr.DiagnoseName.Like(string.Format("%{0}%", context.Text)),
                                dtdQr.DtdName.Like(string.Format("%{0}%", context.Text))
                            )
                    );
                }
                else
                {
                    var searchs = context.Text.Split(' ');
                    var compSynonym = new List<Dal.DynamicQuery.esComparison>();
                    var compDiagnoseName = new List<Dal.DynamicQuery.esComparison>();
                    var compDtdName = new List<Dal.DynamicQuery.esComparison>();

                    foreach (string filter in searchs)
                    {
                        if (string.IsNullOrWhiteSpace(filter)) continue;

                        compSynonym.Add(qr.Synonym.Like(string.Format("%{0}%", filter)));
                        compDiagnoseName.Add(qr.DiagnoseName.Like(string.Format("%{0}%", filter)));
                        compDtdName.Add(dtdQr.DtdName.Like(string.Format("%{0}%", filter)));
                    }


                    qr.Where
                    (
                        qr.Or(qr.And(compSynonym.ToArray()), qr.And(compDiagnoseName.ToArray()), qr.And(compDtdName.ToArray()))
                    );
                }
            }


            qr.OrderBy("<1,3>", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending); // SearchIdx, DiagnoseName
            qr.es.Top = MaxQueryRecord;

            return PopulateComboBoxDataItems(context, qr);
        }
        [WebMethod(EnableSession = true)]
        public RadComboBoxData DiagnosePerSmf(RadComboBoxContext context)
        {
            var qr = new DiagnoseQuery("d");
            var dtdQr = new DtdQuery("dtd");
            qr.InnerJoin(dtdQr).On(qr.DtdNo == dtdQr.DtdNo);
            qr.Select
                (
                    string.Format("<CASE WHEN  CHARINDEX('{0}', d.DiagnoseName) = 0 THEN 1000 ELSE CHARINDEX('{0}', d.DiagnoseName) END  SearchIdx>", context.Text),
                    qr.DiagnoseID.As("ValueField"),
                    qr.DiagnoseID.As("TextField"),
                    qr.DiagnoseName,
                    qr.DtdNo,
                    dtdQr.DtdName,
                    qr.IsDisease,
                    qr.IsChronicDisease,
                    qr.Synonym
                );
            qr.Where
                (
                    qr.IsActive == 1
                );

            if (!string.IsNullOrWhiteSpace(context.Text))
            {
                if (!context.Text.Contains(" "))
                {
                    qr.Where
                    (
                        qr.Or
                            (
                                qr.Synonym.Like(string.Format("%{0}%", context.Text)),
                                qr.DiagnoseID.Like(string.Format("%{0}%", context.Text)),
                                qr.DiagnoseName.Like(string.Format("%{0}%", context.Text)),
                                dtdQr.DtdName.Like(string.Format("%{0}%", context.Text))
                            )
                    );
                }
                else
                {
                    var searchs = context.Text.Split(' ');
                    var compSynonym = new List<Dal.DynamicQuery.esComparison>();
                    var compDiagnoseName = new List<Dal.DynamicQuery.esComparison>();
                    var compDtdName = new List<Dal.DynamicQuery.esComparison>();

                    foreach (string filter in searchs)
                    {
                        if (string.IsNullOrWhiteSpace(filter)) continue;

                        compSynonym.Add(qr.Synonym.Like(string.Format("%{0}%", filter)));
                        compDiagnoseName.Add(qr.DiagnoseName.Like(string.Format("%{0}%", filter)));
                        compDtdName.Add(dtdQr.DtdName.Like(string.Format("%{0}%", filter)));
                    }


                    qr.Where
                    (
                        qr.Or(qr.And(compSynonym.ToArray()), qr.And(compDiagnoseName.ToArray()), qr.And(compDtdName.ToArray()))
                    );
                }
            }

            // Jika user adalah dokter dg smf terisi maka filter dg matrix SMF Diagnose
            if (!string.IsNullOrWhiteSpace(AppSession.UserLogin.SmfID))
            {
                // Cek jika ada matrix baru dipakai filter
                var mtx = new SmfDiagnose();
                mtx.Query.es.Top = 1;
                mtx.Query.Where(mtx.Query.SmfID == AppSession.UserLogin.SmfID);
                if (mtx.Query.Load())
                {
                    var smfdiag = new SmfDiagnoseQuery("smfis");
                    qr.InnerJoin(smfdiag).On(qr.DiagnoseID == smfdiag.DiagnoseID & smfdiag.SmfID == AppSession.UserLogin.SmfID);

                    qr.Where(smfdiag.IsVisible == true);
                }
            }

            qr.OrderBy("<1,3>", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending); // SearchIdx, DiagnoseName
            qr.es.Top = 50; // MaxQueryRecord;  untuk diagnose sementara di hardcode

            return PopulateComboBoxDataItems(context, qr);
        }

        [WebMethod()]
        public RadComboBoxData ExternalCauseDiagnose(RadComboBoxContext context)
        {
            var qr = new DiagnoseQuery("d");
            var dtdQr = new DtdQuery("dtd");
            qr.InnerJoin(dtdQr).On(qr.DtdNo == dtdQr.DtdNo);
            qr.Select
                (
                    string.Format("<CASE WHEN  CHARINDEX('{0}', d.DiagnoseName) = 0 THEN 1000 ELSE CHARINDEX('{0}', d.DiagnoseName) END  SearchIdx>", context.Text),
                    qr.DiagnoseID.As("ValueField"),
                    qr.DiagnoseID.As("TextField"),
                    qr.DiagnoseName,
                    qr.DtdNo,
                    dtdQr.DtdName,
                    qr.IsDisease,
                    qr.IsChronicDisease,
                    qr.Synonym
                );
            qr.Where
                (
                    qr.IsActive == 1,
                    //qr.Or
                    //    (
                    //        qr.Synonym.Like(string.Format("%{0}%", context.Text)),
                    //        qr.DiagnoseID.Like(string.Format("%{0}%", context.Text)),
                    //        qr.DiagnoseName.Like(string.Format("%{0}%", context.Text)),
                    //        dtdQr.DtdName.Like(string.Format("%{0}%", context.Text))
                    //    ),
                    qr.Or(qr.DiagnoseID.Like("V%"),
                             qr.DiagnoseID.Like("W%"),
                             qr.DiagnoseID.Like("X%"),
                             qr.DiagnoseID.Like("Y%"))
                );


            if (!string.IsNullOrWhiteSpace(context.Text))
            {
                if (!context.Text.Contains(" "))
                {
                    qr.Where
                    (
                        qr.Or
                            (
                                qr.Synonym.Like(string.Format("%{0}%", context.Text)),
                                qr.DiagnoseID.Like(string.Format("%{0}%", context.Text)),
                                qr.DiagnoseName.Like(string.Format("%{0}%", context.Text)),
                                dtdQr.DtdName.Like(string.Format("%{0}%", context.Text))
                            )
                    );
                }
                else
                {
                    var searchs = context.Text.Split(' ');
                    var compSynonym = new List<Dal.DynamicQuery.esComparison>();
                    var compDiagnoseName = new List<Dal.DynamicQuery.esComparison>();
                    var compDtdName = new List<Dal.DynamicQuery.esComparison>();

                    foreach (string filter in searchs)
                    {
                        if (string.IsNullOrWhiteSpace(filter)) continue;

                        compSynonym.Add(qr.Synonym.Like(string.Format("%{0}%", filter)));
                        compDiagnoseName.Add(qr.DiagnoseName.Like(string.Format("%{0}%", filter)));
                        compDtdName.Add(dtdQr.DtdName.Like(string.Format("%{0}%", filter)));
                    }


                    qr.Where
                    (
                        qr.Or(qr.And(compSynonym.ToArray()), qr.And(compDiagnoseName.ToArray()), qr.And(compDtdName.ToArray()))
                    );
                }
            }

            qr.OrderBy("<1,3>", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending); // SearchIdx, DiagnoseName
            qr.es.Top = 50; // MaxQueryRecord;  untuk diagnose sementara di hardcode

            return PopulateComboBoxDataItems(context, qr);
        }


        /// <summary>
        /// Dipakai untuk entrian Prescription
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [WebMethod]
        public RadComboBoxData ItemProductMedicByLocation(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            var suID = (string)contextDictionary["ServiceUnitID"];
            var locID = (string)contextDictionary["LocationID"];
            var isOnlyInStock = (bool)contextDictionary["IsOnlyInStock"];
            var guarantorID = (string)contextDictionary["GuarantorID"];
            var userType = (string)contextDictionary["UserType"];


            bool isFornas = false;
            bool isFormularium = false;
            bool isGeneric = false;
            bool isNonGeneric = false;
            bool isNonGenericLimited = false;

            if (!string.IsNullOrWhiteSpace(guarantorID))
            {
                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(guarantorID))
                {
                    isFornas = guar.IsItemRestrictionsFornas ?? false;
                    isFormularium = guar.IsItemRestrictionsFormularium ?? false;
                    isGeneric = guar.IsItemRestrictionsGeneric ?? false;
                    isNonGeneric = guar.IsItemRestrictionsNonGeneric ?? false;
                    isNonGenericLimited = guar.IsItemRestrictionsNonGenericLimited ?? false;
                }
            }

            if (string.IsNullOrWhiteSpace(locID))
            {
                locID = (new ServiceUnit()).GetMainLocationId(suID);
                if (string.IsNullOrEmpty(locID))
                {
                    var unit = new ServiceUnit();
                    // Kasus bisa terjadi utk database yg belum di update
                    locID = unit.LoadByPrimaryKey(suID) ? unit.LocationPharmacyID : string.Empty;
                }
            }

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            var product = new VwItemProductMedicNonMedicQuery("c");
            var zai = new ItemProductMedicZatActiveQuery("d");
            var za = new ZatActiveQuery("e");

            query.es.Top = MaxQueryRecord;
            query.es.Distinct = true;
            query.Select
                (
                    string.Format("<CASE WHEN  CHARINDEX('{0}', a.ItemName) = 0 THEN 1000 ELSE CHARINDEX('{0}', a.ItemName) END  SearchIdx>", context.Text),
                    query.ItemID.As("ValueField"),
                    query.ItemName.As("TextField"),
                    product.GenericFlag,
                    (bal.Balance.Coalesce("0") - bal.Booking.Coalesce("0")).As("Balance"),
                    product.SRItemUnit
                //string.Format("<CASE(CHARINDEX('par', a.ItemName)) when 0 then 1000 else CHARINDEX('{0}', a.ItemName) end SearchIdx>", context.Text)

                );
            query.InnerJoin(product).On(query.ItemID == product.ItemID);
            query.InnerJoin(bal).On(
                query.ItemID == bal.ItemID &&
                bal.LocationID == locID
                );
            query.LeftJoin(zai).On(query.ItemID == zai.ItemID);
            query.LeftJoin(za).On(zai.ZatActiveID == za.ZatActiveID);

            if (context.Text != "[showall]")
                query.Where(
                    query.Or(
                        query.ItemID == context.Text,
                        query.ItemName.Like(string.Format("{0}%", context.Text)),
                        query.ItemName.Like(string.Format("%{0}%", context.Text)),
                        product.BrandName.Like(string.Format("%{0}%", context.Text)),
                        za.ZatActiveName == context.Text
                        )
                    );
            query.Where(
                query.IsActive == true
                );

            if (userType != AppUser.UserType.Doctor)
            {
                // Cek jika ada mapping maka filter
                var cekMap = new ItemGroupUserType();
                cekMap.Query.Where(cekMap.Query.SRUserType == userType);
                cekMap.Query.es.Top = 1;
                if (cekMap.Query.Load() && cekMap.SRUserType == userType)
                {
                    // Filter by Restriction
                    // Perawat bisa buat resep tetapi dibatasi item2nya yg diset di entrian Master Item Group
                    var rest = new ItemGroupUserTypeQuery("ut");
                    query.InnerJoin(rest).On(query.ItemGroupID == rest.ItemGroupID);
                    query.Where(rest.SRUserType == userType);
                }
            }

            // Antibiotic Parameter (Base Raspro)
            var isAntibioticRestriction = (bool)contextDictionary["IsAntibioticRestriction"];

            if (isAntibioticRestriction)
            {
                var abRestrictionID = (string)contextDictionary["AbRestrictionID"];
                var regNo = (string)contextDictionary["RegistrationNo"];

                var abLevel = (string)contextDictionary["AbLevel"];

                // Prev Zat Active
                var prevItemAb = new RegistrationGyssensQuery("pza");
                prevItemAb.Select(prevItemAb.ItemID);
                prevItemAb.Where(prevItemAb.RegistrationNo == regNo, prevItemAb.ItemID == query.ItemID);

                // Jika abLevel 9999 Semua AB diperbolehkan
                if (abLevel.ToInt() == 0)
                {
                    // Tidak diperbolehkan entry AB kecuali AB yg sudah pernah diresepkan
                    query.Where(query.Or(product.IsAntibiotic == false, query.ItemID.In(prevItemAb)));
                }
                else if (abLevel.ToInt() < 9999 && !string.IsNullOrEmpty(abRestrictionID))
                {
                    // AB Restriction
                    var abri = new AbRestrictionItemQuery("abri");
                    var zam = new ItemProductMedicZatActiveQuery("zam");
                    abri.InnerJoin(zam).On(abri.ZatActiveID == zam.ZatActiveID);
                    abri.Select(zam.ItemID);
                    abri.Where(abri.AbRestrictionID == abRestrictionID, abri.AbLevel == abLevel, zam.ItemID == query.ItemID);

                    query.Where(query.Or(product.IsAntibiotic == false, query.ItemID.In(abri), query.ItemID.In(prevItemAb)));
                }
                else if (abLevel.ToInt() == 9999)
                {
                    // All Antibiotics allowed 
                }
                else
                    query.Where(product.IsAntibiotic == false);

            }

            query.OrderBy("<1,3>", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending);

            var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

            if (isFornas)
                xx.Add(product.IsFornas == true);

            if (isFormularium)
                xx.Add(product.IsFormularium == true);

            if (isGeneric)
                xx.Add(product.IsGeneric == true);

            if (isNonGeneric)
                xx.Add(product.IsNonGeneric == true);

            if (isNonGenericLimited)
                xx.Add(product.IsNonGenericLimited == true);

            if (xx.Count > 0)
                query.Where(query.Or(xx.ToArray()));
            else
            {
                var restrictions = new GuarantorItemRestrictionsQuery("rest");
                var item = new ItemQuery("i");
                restrictions.InnerJoin(item).On(restrictions.ItemID == item.ItemID);

                restrictions.Where(restrictions.GuarantorID == guarantorID, restrictions.Or(item.SRItemType == ItemType.Medical, item.SRItemType == ItemType.NonMedical));
                restrictions.es.Top = 1;
                var dtRest = restrictions.LoadDataTable();
                if (dtRest.Rows.Count > 0)
                {
                    query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID);
                    query.Where(restrictions.GuarantorID == guarantorID);
                }
            }

            if (isOnlyInStock)
                query.Where(bal.Balance > 0);

            var dtb = query.LoadDataTable();

            var comboData = PopulateComboBoxDataItems(context, dtb, "ValueField", "TextField");
            foreach (RadComboBoxItemData itemData in comboData.Items)
            {
                var qr = new ItemProductMedicZatActiveQuery("m");
                var qrZat = new ZatActiveQuery("z");
                qr.InnerJoin(qrZat).On(qr.ZatActiveID == qrZat.ZatActiveID);
                qr.Select(qrZat.ZatActiveName);
                qr.Where(qr.ItemID == itemData.Value);
                var dtbZatActive = qr.LoadDataTable();
                var zatActive = dtbZatActive.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, row) => string.Concat(current, ", ", row["ZatActiveName"].ToString()));
                itemData.Attributes.Add("ZatActive", zatActive);
            }

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData PrescriptionItemSelection(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            var suID = (string)contextDictionary["ServiceUnitID"];
            var locID = (string)contextDictionary["LocationID"];
            var isOnlyInStock = (bool)contextDictionary["IsOnlyInStock"];
            var guarantorID = (string)contextDictionary["GuarantorID"];
            var userType = (string)contextDictionary["UserType"];
            var isBalTotalInfo = (contextDictionary["IsBalTot"] ?? "false").ToBoolean();
            var isUdd = (contextDictionary["IsUdd"] ?? "false").ToBoolean();
            var registrationNo = (string)contextDictionary["RegistrationNo"];
            var isFormPrescOrderForm = (contextDictionary["IsFormPrescOrderForm"] ?? "false").ToBoolean();
            var antibioticRestrictionForLine = (string)contextDictionary["ArLine"];

            if (string.IsNullOrWhiteSpace(locID))
            {
                locID = (new ServiceUnit()).GetMainLocationId(suID);
                if (string.IsNullOrEmpty(locID))
                {
                    var unit = new ServiceUnit();
                    // Kasus bisa terjadi utk database yg belum di update
                    locID = unit.LoadByPrimaryKey(suID) ? unit.LocationPharmacyID : string.Empty;
                }
            }

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            var product = new VwItemProductMedicNonMedicQuery("p");
            var zai = new ItemProductMedicZatActiveQuery("ipza");
            var za = new ZatActiveQuery("za");

            query.es.Top = MaxQueryRecord;
            query.es.Distinct = true;
            query.Select
                (
                    string.Format("<CASE WHEN  CHARINDEX('{0}', a.ItemName) = 0 THEN 1000 ELSE CHARINDEX('{0}', a.ItemName) END  SearchIdx>", context.Text),
                    query.ItemID.As("ValueField"),
                    query.ItemName.As("TextField"),
                    product.GenericFlag,
                    //(bal.Balance.Coalesce("0") - bal.Booking.Coalesce("0")).As("Balance"), //kolom booking itu skr ganti fungsi jd buat nyimpan pendingan distribution confirm (Deby info 20220621)
                    bal.Balance.Coalesce("0").As("Balance"),
                    product.SRItemUnit,
                    product.IsAntibiotic,
                    product.FornasRestrictionNotes
                );
            query.InnerJoin(product).On(query.ItemID == product.ItemID);
            query.InnerJoin(bal).On(
                query.ItemID == bal.ItemID &&
                bal.LocationID == locID
                );
            query.LeftJoin(zai).On(query.ItemID == zai.ItemID);
            query.LeftJoin(za).On(zai.ZatActiveID == za.ZatActiveID);

            if (context.Text != "[showall]")
            {
                if (!context.Text.Contains(" "))
                {
                    query.Where(
                    query.Or(
                        query.ItemID == context.Text,
                        query.ItemName.Like(string.Format("{0}%", context.Text)),
                        query.ItemName.Like(string.Format("%{0}%", context.Text)),
                        product.BrandName.Like(string.Format("%{0}%", context.Text)),
                        za.ZatActiveName.Like(string.Format("%{0}%", context.Text))
                        )
                    );
                }
                else
                {
                    // Search per word
                    var searchs = context.Text.Split(' ');
                    var compItemName = new List<Dal.DynamicQuery.esComparison>();
                    var compBrandName = new List<Dal.DynamicQuery.esComparison>();
                    var compZatActiveName = new List<Dal.DynamicQuery.esComparison>();

                    foreach (string filter in searchs)
                    {
                        if (string.IsNullOrWhiteSpace(filter)) continue;

                        compItemName.Add(query.ItemName.Like(string.Format("%{0}%", filter)));
                        compBrandName.Add(product.BrandName.Like(string.Format("%{0}%", filter)));
                        compZatActiveName.Add(za.ZatActiveName.Like(string.Format("%{0}%", filter)));
                    }
                    query.Where
                    (
                        query.Or(query.And(compItemName.ToArray()), query.And(compBrandName.ToArray()), query.And(compZatActiveName.ToArray()))
                    );
                }
            }

            query.Where(
                query.IsActive == true, product.IsSalesAvailable == true
                );

            if (isUdd)
                query.Where(product.IsMedication == true);

            if (isFormPrescOrderForm && userType != AppUser.UserType.Doctor)
            {
                // Cek jika ada mapping maka filter
                var cekMap = new ItemGroupUserType();
                cekMap.Query.Where(cekMap.Query.SRUserType == userType);
                cekMap.Query.es.Top = 1;
                if (cekMap.Query.Load() && cekMap.SRUserType == userType)
                {
                    // Filter by Restriction
                    // Perawat bisa buat resep tetapi dibatasi item2nya yg diset di entrian Master Item Group
                    var rest = new ItemGroupUserTypeQuery("ut");
                    query.InnerJoin(rest).On(query.ItemGroupID == rest.ItemGroupID);
                    query.Where(rest.SRUserType == userType);
                }
                else
                    query.Where(query.ItemGroupID == "XXX");
            }

            // Antibiotic Parameter (Base Raspro)
            var isRasproEnable = (bool)contextDictionary["IsRasproEnable"];

            if (isRasproEnable)
            {
                var useRasproSeqNo = contextDictionary["RasproSeqNo"].ToInt();
                var regNo = (string)contextDictionary["RegistrationNo"];
                var abLevel = (string)contextDictionary["AbLevel"];

                if (useRasproSeqNo > 0)
                {
                    // Tidak diperbolehkan entry AB kecuali AB pada form RASPRO yg berlaku
                    // Profilaxys dgn kategori Operasi Tercemat dan Kotor membutuhkan antibiotik empirik
                    // menggunakan form raspro RASAL/RASLAN/RASPATUR sebelumnya dan ini di set pada form entriannya
                    // disini hanya tinggal menggunakan raspro seqno no berapa
                    var prevItemAb = new RegistrationRasproItemQuery("pza");
                    if ("RASPRAJA".Equals(contextDictionary["RasproType"])) //TODO: AB yg masih belum 7 hari realisasinya
                        query.LeftJoin(prevItemAb).On(query.ItemID == prevItemAb.ItemID & prevItemAb.RegistrationNo == regNo & prevItemAb.RasprajaSeqNo == useRasproSeqNo);
                    else
                        query.LeftJoin(prevItemAb).On(query.ItemID == prevItemAb.ItemID & prevItemAb.RegistrationNo == regNo & prevItemAb.RasproSeqNo == useRasproSeqNo);

                    if (string.IsNullOrEmpty(antibioticRestrictionForLine))
                        query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 AND pza.ItemID IS NULL THEN 0 ELSE 1 END) as IsAllowed>");
                    else
                        query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 AND p.SRAntibioticLine='" + antibioticRestrictionForLine + "' AND pza.ItemID IS NULL THEN 0 ELSE 1 END) as IsAllowed>");
                }
                else if (abLevel.ToInt() == AppConstant.AntibioticLevel.AllAntibiotic)
                {
                    // Semua AB diperbolehkan
                    query.Select("<CONVERT(BIT,1) as IsAllowed>");
                }
                else if (abLevel.ToInt() == AppConstant.AntibioticLevel.NoNeedAntibiotic)
                {
                    // Tidak perlu AB / tidak bisa menambah antibiotik dgn level line yg diisi di parameter
                    if (string.IsNullOrEmpty(antibioticRestrictionForLine))
                        query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 THEN 0 ELSE 1 END) as IsAllowed>");
                    else
                        query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 AND p.SRAntibioticLine='" + antibioticRestrictionForLine + "' THEN 0 ELSE 1 END) as IsAllowed>");
                }
                else
                {
                    var abRestrictionID = (string)contextDictionary["AbRestrictionID"];
                    if (abLevel.ToInt() > 0 && !string.IsNullOrEmpty(abRestrictionID))
                    {
                        // AB Restriction
                        var subQr = string.Format(@"SELECT zam.[ItemID]
                                          FROM   [AbRestrictionItem] abri
                                                 INNER JOIN [ItemProductMedicZatActive] zam
                                                      ON  abri.[ZatActiveID] = zam.[ZatActiveID]
                                          WHERE  abri.[AbRestrictionID] = '{0}'
                                                 AND abri.[AbLevel] = '{1}'
                                                 AND zam.[ItemID] = a.[ItemID]", abRestrictionID, abLevel);
                        if (string.IsNullOrEmpty(antibioticRestrictionForLine))
                            query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 AND a.ItemID NOT IN (" + subQr + ") THEN 0 ELSE 1 END) as IsAllowed>");
                        else
                            query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 AND p.SRAntibioticLine='" + antibioticRestrictionForLine + "' AND a.ItemID NOT IN (" + subQr + ") THEN 0 ELSE 1 END) as IsAllowed>");
                    }
                    else
                    {
                        //query.Select("<CONVERT(BIT,0) as IsAllowed>"); //Tidak diijinkan menambah obat AB
                        if (string.IsNullOrEmpty(antibioticRestrictionForLine))
                            query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 THEN 0 ELSE 1 END) as IsAllowed>");
                        else
                            query.Select("<CONVERT(BIT, CASE WHEN p.IsAntibiotic = 1 AND p.SRAntibioticLine='" + antibioticRestrictionForLine + "' THEN 0 ELSE 1 END) as IsAllowed>");
                    }
                }
            }
            else
            {
                query.Select("<CONVERT(BIT,1) as IsAllowed>");
            }

            query.OrderBy("<1,3>", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending);


            if (!string.IsNullOrWhiteSpace(guarantorID))
            {
                // cek item restriction

                bool isFornas = false;

                //if ("NULL".Equals(contextDictionary["IsFornas"]))
                //{
                //    var guar = new Guarantor();
                //    if (guar.LoadByPrimaryKey(guarantorID))
                //        isFornas = guar.IsItemRestrictionsFornas ?? false;
                //}
                //else
                //    isFornas = (bool)contextDictionary["IsFornas"];

                //if (isFornas)
                //{
                //    query.Where(query.Or(product.IsFornas == true));
                //}

                bool isFormularium = false;
                bool isGeneric = false;
                bool isNonGeneric = false;
                bool isNonGenericLimited = false;

                var guar = new Guarantor();
                if (guar.LoadByPrimaryKey(guarantorID))
                {
                    isFornas = guar.IsItemRestrictionsFornas ?? false;
                    isFormularium = guar.IsItemRestrictionsFormularium ?? false;
                    isGeneric = guar.IsItemRestrictionsGeneric ?? false;
                    isNonGeneric = guar.IsItemRestrictionsNonGeneric ?? false;
                    isNonGenericLimited = guar.IsItemRestrictionsNonGenericLimited ?? false;
                }

                var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

                if (isFornas)
                    xx.Add(product.IsFornas == true);

                if (isFormularium)
                    xx.Add(product.IsFormularium == true);

                if (isGeneric)
                    xx.Add(product.IsGeneric == true);

                if (isNonGeneric)
                    xx.Add(product.IsNonGeneric == true);

                if (isNonGenericLimited)
                    xx.Add(product.IsNonGenericLimited == true);

                if (xx.Count > 0)
                    query.Where(query.Or(xx.ToArray()));
                else
                {
                    var restrictions = new GuarantorItemRestrictionsQuery("rest");
                    var item = new ItemQuery("i");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(item).On(restrictions.ItemID == item.ItemID);
                    restrictions.Where(restrictions.GuarantorID == guarantorID, restrictions.Or(item.SRItemType == ItemType.Medical, item.SRItemType == ItemType.NonMedical));

                    var dtRest = restrictions.LoadDataTable();
                    if (dtRest.Rows.Count > 0)
                    {
                        //query.InnerJoin(restrictions).On(query.ItemID == restrictions.ItemID && restrictions.GuarantorID == guarantorID);
                        if (guar.IsItemProductRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }

                }

                // Bugs Fix TO modif by Handono (22-08-08)
                //cek item coverad casemix
                //if (AppParameter.IsYes(AppParameter.ParameterItem.IsCasemixCoveredEnable) 
                //    &&  Helper.GuarantorBpjsCasemix.Contains(guarantorID))
                if (Helper.GuarantorBpjsCasemix.Contains(guarantorID)) //--> deby: cukup pake settingan ini, soalnya casemix semua ngacunya ke CasemixCoveredDetail
                {
                    var reg = new Registration();
                    if (reg.LoadByPrimaryKey(registrationNo))
                    {
                        if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                        {
                            //var cmixCover = new CasemixCoveredDetailQuery("cmix");
                            //var cmixGuarCover = new CasemixCoveredGuarantorQuery("cmixGuar");
                            //cmixCover.InnerJoin(cmixGuarCover).On(cmixGuarCover.CasemixCoveredID == cmixCover.CasemixCoveredID && cmixGuarCover.GuarantorID == reg.GuarantorID);
                            //cmixCover.Select(cmixCover.ItemID);
                            //cmixCover.Where(cmixCover.IsAllowedToOrder == true, cmixCover.ItemID == query.ItemID);
                            //cmixCover.es.Distinct = true;

                            //var cmixCoverRule = new CasemixCoveredRegistrationRuleQuery("cmixrule");
                            //cmixCoverRule.Select(cmixCoverRule.ItemID);
                            //cmixCoverRule.Where(cmixCoverRule.RegistrationNo == registrationNo, cmixCoverRule.ItemID == query.ItemID);

                            //query.Where(query.Or(query.ItemID.In(cmixCover), query.ItemID.In(cmixCoverRule)));

                            //db:20231204 - penyesuaian perubahan table CasemixCoveredDetail
                            var cmixCover = new CasemixCoveredDetailQuery("cmix");
                            var cmixGuarCover = new CasemixCoveredGuarantorQuery("cmixGuar");
                            cmixCover.InnerJoin(cmixGuarCover).On(cmixGuarCover.CasemixCoveredID == cmixCover.CasemixCoveredID && cmixGuarCover.GuarantorID == reg.GuarantorID);
                            cmixCover.Select(cmixCover.ItemID);
                            //cmixCover.Where(cmixCover.ItemID == query.ItemID);
                            switch (reg.SRRegistrationType)
                            {
                                case "IPR":
                                    {
                                        cmixCover.Where(cmixCover.Or(
                                            cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                            cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderIpr == true)));
                                    }
                                    break;
                                case "EMR":
                                    {
                                        cmixCover.Where(cmixCover.Or(
                                            cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                            cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderEmr == true)));
                                    }
                                    break;
                                default: //OPR & MCU
                                    {
                                        cmixCover.Where(cmixCover.Or(
                                            cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                            cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderOpr == true)));
                                    }
                                    break;
                            }

                            //cmixCover.es.Distinct = true;

                            var cmixCoverRule = new CasemixCoveredRegistrationRuleQuery("cmixrule");
                            cmixCoverRule.Select(cmixCoverRule.ItemID);
                            //cmixCoverRule.Where(cmixCoverRule.RegistrationNo == registrationNo, cmixCoverRule.ItemID == query.ItemID);
                            cmixCoverRule.Where(cmixCoverRule.RegistrationNo == registrationNo);


                            var dtCmixCover = cmixCover.LoadDataTable();
                            bool isCmixCover = (dtCmixCover.Rows.Count > 0);

                            var dtCmixCoverRule = cmixCoverRule.LoadDataTable();
                            bool isCmixCoverRule = (dtCmixCoverRule.Rows.Count > 0);

                            if (isCmixCover & isCmixCoverRule)
                            {
                                query.Where(query.Or(query.ItemID.In(cmixCover), query.ItemID.In(cmixCoverRule)));
                            }
                            else
                            {
                                if (isCmixCover)
                                {
                                    query.Where(query.ItemID.In(cmixCover));
                                }
                                else if (isCmixCoverRule)
                                {
                                    query.Where(query.ItemID.In(cmixCoverRule));
                                }
                            }
                        }
                    }
                }
            }

            if (isOnlyInStock)
                query.Where(bal.Balance > 0);

            var dtb = query.LoadDataTable();

            if (isBalTotalInfo)
            {
                var itemIds = new List<string>();
                foreach (DataRow row in dtb.Rows)
                {
                    itemIds.Add(row["ValueField"].ToString());
                }

                dtb.Columns.Add("BalanceTotal", typeof(decimal));

                if (itemIds.Count > 0)
                {
                    var balTot = new ItemBalanceQuery("b");
                    balTot.Select(balTot.ItemID, balTot.Balance.Coalesce("0").Sum().As("BalanceTotal"));
                    balTot.Where(balTot.ItemID.In(itemIds));
                    balTot.GroupBy(balTot.ItemID);
                    var dtbTot = balTot.LoadDataTable();

                    dtbTot.PrimaryKey = new DataColumn[] { dtbTot.Columns["ItemID"] };
                    foreach (DataRow row in dtb.Rows)
                    {
                        var rowTot = dtbTot.Rows.Find(row["ValueField"]);
                        row["BalanceTotal"] = rowTot["BalanceTotal"] ?? 0;
                    }
                }
            }

            var comboData = PopulateComboBoxDataItems(context, dtb, "ValueField", "TextField");
            foreach (RadComboBoxItemData itemData in comboData.Items)
            {
                var qr = new ItemProductMedicZatActiveQuery("m");
                var qrZat = new ZatActiveQuery("z");
                qr.InnerJoin(qrZat).On(qr.ZatActiveID == qrZat.ZatActiveID);
                qr.Select(qrZat.ZatActiveName);
                qr.Where(qr.ItemID == itemData.Value);
                var dtbZatActive = qr.LoadDataTable();
                var zatActive = dtbZatActive.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, row) => string.Concat(current, ", ", row["ZatActiveName"].ToString()));
                itemData.Attributes.Add("ZatActive", zatActive);


                if (itemData.Attributes["IsAllowed"].ToBoolean() == false)
                {
                    itemData.Attributes.Add("Style", "color:gray;");
                }
                else if (itemData.Attributes["IsAntibiotic"].Equals("1"))
                {
                    itemData.Attributes.Add("Style", "color:blue;");
                }
            }

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ItemProductMedicAntibiotics(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;

            var query = new ItemQuery("a");
            var product = new ItemProductMedicQuery("ipm");
            var zai = new ItemProductMedicZatActiveQuery("d");
            var za = new ZatActiveQuery("e");

            query.es.Top = MaxQueryRecord;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemID.As("ValueField"),
                    query.ItemName.As("TextField"),
                    @"<CASE WHEN ipm.IsGeneric = 1 THEN 'Generic' WHEN ipm.IsNonGeneric = 1 THEN 'Non Generic' ELSE '' END AS GenericFlag>",
                    product.IsGeneric,
                    product.IsNonGeneric,
                    product.SRItemUnit,
                    product.IsAntibiotic
                );
            query.InnerJoin(product).On(query.ItemID == product.ItemID);
            query.LeftJoin(zai).On(query.ItemID == zai.ItemID);
            query.LeftJoin(za).On(zai.ZatActiveID == za.ZatActiveID);
            query.Where(
                query.Or(
                    query.ItemID == context.Text,
                    query.ItemName.Like(string.Format("{0}%", context.Text)),
                    query.ItemName.Like(string.Format("%{0}%", context.Text)),
                    product.BrandName.Like(string.Format("%{0}%", context.Text)),
                    za.ZatActiveName == context.Text
                    ),
                query.IsActive == true,
                product.IsAntibiotic == true
                );

            var comboData = PopulateComboBoxDataItems(context, query);
            foreach (RadComboBoxItemData itemData in comboData.Items)
            {
                var qr = new ItemProductMedicZatActiveQuery("m");
                var qrZat = new ZatActiveQuery("z");
                qr.InnerJoin(qrZat).On(qr.ZatActiveID == qrZat.ZatActiveID);
                qr.Select(qrZat.ZatActiveName);
                qr.Where(qr.ItemID == itemData.Value);
                var dtbZatActive = qr.LoadDataTable();
                var zatActive = dtbZatActive.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, row) => string.Concat(current, ", ", row["ZatActiveName"].ToString()));
                itemData.Attributes.Add("ZatActive", zatActive);
            }

            return comboData;
        }


        [WebMethod]
        public RadComboBoxData ItemProductMedics(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;

            var query = new ItemQuery("a");
            var product = new ItemProductMedicQuery("ipm");
            var zai = new ItemProductMedicZatActiveQuery("d");
            var za = new ZatActiveQuery("e");

            query.es.Top = MaxQueryRecord;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemID.As("ValueField"),
                    query.ItemName.As("TextField"),
                    @"<CASE WHEN ipm.IsGeneric = 1 THEN 'Generic' WHEN ipm.IsNonGeneric = 1 THEN 'Non Generic' ELSE '' END AS GenericFlag>",
                    product.IsGeneric,
                    product.IsNonGeneric,
                    product.SRItemUnit,
                    product.IsAntibiotic
                );
            query.InnerJoin(product).On(query.ItemID == product.ItemID);
            query.LeftJoin(zai).On(query.ItemID == zai.ItemID);
            query.LeftJoin(za).On(zai.ZatActiveID == za.ZatActiveID);
            query.Where(
                query.Or(
                    query.ItemID == context.Text,
                    query.ItemName.Like(string.Format("{0}%", context.Text)),
                    query.ItemName.Like(string.Format("%{0}%", context.Text)),
                    product.BrandName.Like(string.Format("%{0}%", context.Text)),
                    za.ZatActiveName == context.Text
                    ),
                query.IsActive == true
                );

            var comboData = PopulateComboBoxDataItems(context, query);
            foreach (RadComboBoxItemData itemData in comboData.Items)
            {
                var qr = new ItemProductMedicZatActiveQuery("m");
                var qrZat = new ZatActiveQuery("z");
                qr.InnerJoin(qrZat).On(qr.ZatActiveID == qrZat.ZatActiveID);
                qr.Select(qrZat.ZatActiveName);
                qr.Where(qr.ItemID == itemData.Value);
                var dtbZatActive = qr.LoadDataTable();
                var zatActive = dtbZatActive.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, row) => string.Concat(current, ", ", row["ZatActiveName"].ToString()));
                itemData.Attributes.Add("ZatActive", zatActive);
            }

            return comboData;
        }


        [WebMethod]
        public RadComboBoxData ItemProductMedicNonMedics(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;

            var query = new ItemQuery("a");
            var product = new VwItemProductMedicNonMedicQuery("c");
            var zai = new ItemProductMedicZatActiveQuery("d");
            var za = new ZatActiveQuery("e");

            query.es.Top = MaxQueryRecord;
            query.es.Distinct = true;
            query.Select
                (
                    query.ItemID.As("ValueField"),
                    query.ItemName.As("TextField"),
                    product.GenericFlag,
                    product.SRItemUnit
                );
            query.InnerJoin(product).On(query.ItemID == product.ItemID);
            query.LeftJoin(zai).On(query.ItemID == zai.ItemID);
            query.LeftJoin(za).On(zai.ZatActiveID == za.ZatActiveID);
            if (context.Text == "[showall]")
                query.Where(query.IsActive == true);
            else
                query.Where(
                    query.Or(
                        query.ItemID == context.Text,
                        query.ItemName.Like(string.Format("{0}%", context.Text)),
                        query.ItemName.Like(string.Format("%{0}%", context.Text)),
                        product.BrandName.Like(string.Format("%{0}%", context.Text)),
                        za.ZatActiveName == context.Text
                        ),
                    query.IsActive == true
                    );

            var comboData = PopulateComboBoxDataItems(context, query);
            foreach (RadComboBoxItemData itemData in comboData.Items)
            {
                var qr = new ItemProductMedicZatActiveQuery("m");
                var qrZat = new ZatActiveQuery("z");
                qr.InnerJoin(qrZat).On(qr.ZatActiveID == qrZat.ZatActiveID);
                qr.Select(qrZat.ZatActiveName);
                qr.Where(qr.ItemID == itemData.Value);
                var dtbZatActive = qr.LoadDataTable();
                var zatActive = dtbZatActive.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, row) => string.Concat(current, ", ", row["ZatActiveName"].ToString()));
                itemData.Attributes.Add("ZatActive", zatActive);
            }

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData CssdItemPackageSelection(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;

            var isItemProduction = (bool)contextDictionary["IsItemProduction"];
            var itemId = (string)contextDictionary["ItemID"];

            var query = new ItemQuery("a");

            query.es.Top = MaxQueryRecord;
            query.Select
                (
                    query.ItemID.As("ValueField"),
                    query.ItemName.As("TextField")
                );
            if (context.Text == "[showall]")
                query.Where(query.IsActive == true);
            else
                query.Where(
                    query.Or(
                        query.ItemID == context.Text,
                        query.ItemName.Like(string.Format("{0}%", context.Text)),
                        query.ItemName.Like(string.Format("%{0}%", context.Text))),
                    query.IsActive == true
                    );
            if (isItemProduction)
                query.Where(query.ItemID != itemId);
            else
                query.Where(query.ItemID == itemId);

            query.Where(query.IsNeedToBeSterilized == true);

            var comboData = PopulateComboBoxDataItems(context, query);

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ItemTransactionEntrySelection(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;

            var query = new ItemQuery("a");

            query.es.Top = MaxQueryRecord;
            query.Select
                (
                    query.ItemID.As("ValueField"),
                    @"<a.ItemName + ' [' + a.ItemID + ']' AS 'TextField'>"
                //, query.ItemName.As("TextField")
                );
            if (context.Text == "[showall]")
                query.Where(query.IsActive == true);
            else
                query.Where(
                    query.Or(
                        query.ItemID == context.Text,
                        query.ItemName.Like(string.Format("{0}%", context.Text)),
                        query.ItemName.Like(string.Format("%{0}%", context.Text))),
                    query.IsActive == true
                    );

            var comboData = PopulateComboBoxDataItems(context, query);

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData Procedures(RadComboBoxContext context)
        {
            var qr = new ProcedureQuery("p");
            qr.Select(qr.ProcedureID.As("ValueField"), qr.ProcedureID.As("TextField"), qr.ProcedureName);
            qr.Where(qr.Or(qr.ProcedureID.Like(string.Format("%{0}%", context.Text)),
                qr.ProcedureName.Like(string.Format("%{0}%", context.Text))));
            qr.OrderBy(qr.ProcedureName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        //[WebMethod]
        //public RadComboBoxData Paramedics(RadComboBoxContext context)
        //{
        //    var qr = new ParamedicQuery("p");
        //    var stdi = new AppStandardReferenceItemQuery("stdi");
        //    qr.LeftJoin(stdi).On(qr.SRSpecialty == stdi.ItemID && stdi.StandardReferenceID == "Specialty");
        //    qr.Select(qr.ParamedicID.As("ValueField"), qr.ParamedicName.As("TextField"), stdi.ItemName.As("SpecialtyName"));
        //    qr.Where(qr.IsActive == true);
        //    if (context.Text != "[showall]")
        //        qr.Where(qr.ParamedicName.Like(string.Format("%{0}%", context.Text)));
        //    qr.OrderBy(qr.ParamedicName.Ascending);
        //    qr.es.Top = MaxQueryRecord;

        //    var comboData = PopulateComboBoxDataItems(context, qr);
        //    return comboData;
        //}
        [WebMethod]
        public RadComboBoxData Paramedics(RadComboBoxContext context)
        {
            var qr = new ParamedicQuery("p");
            var smf = new SmfQuery("s");
            qr.LeftJoin(smf).On(qr.SRParamedicRL1 == smf.SmfID);
            qr.Select(qr.ParamedicID.As("ValueField"), qr.ParamedicName.As("TextField"), smf.SmfName.As("SpecialtyName"));
            qr.Where(qr.IsActive == true); // Tambah filter IsActive krn dipakai untuk entrian data transaksi (Handono 231016)
            if (context.Text != "[showall]")
                qr.Where(qr.ParamedicName.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(qr.ParamedicName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ServiceUnitParamedics(RadComboBoxContext context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["serviceUnitID"]);

            var qr = new ParamedicQuery("p");
            var smf = new SmfQuery("s");
            var sup = new ServiceUnitParamedicQuery("sup");

            qr.InnerJoin(sup).On(qr.ParamedicID == sup.ParamedicID);
            qr.LeftJoin(smf).On(qr.SRParamedicRL1 == smf.SmfID);
            qr.Select(qr.ParamedicID.As("ValueField"), qr.ParamedicName.As("TextField"), smf.SmfName.As("SpecialtyName"));
            qr.Where(qr.IsActive == true, sup.ServiceUnitID == filter);
            if (context.Text != "[showall]")
                qr.Where(qr.ParamedicName.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(qr.ParamedicName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData Referrals(RadComboBoxContext context)
        {
            var qr = new ReferralQuery("p");
            qr.Select(qr.ReferralID.As("ValueField"), qr.ReferralName.As("TextField"), qr.PhoneNo, (qr.StreetName + " " + qr.City.Trim() + " " + qr.County.Trim() + " " + (qr.ZipCode ?? string.Empty)).As("Address"));
            qr.Where(qr.IsActive == true, qr.ReferralName.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(qr.ReferralName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ReferralTo(RadComboBoxContext context)
        {
            var qr = new ReferralQuery("p");
            qr.Select(qr.ReferralID.As("ValueField"), qr.ReferralName.As("TextField"), qr.PhoneNo, (qr.StreetName + " " + qr.City.Trim() + " " + qr.County.Trim() + " " + (qr.ZipCode ?? string.Empty)).As("Address"));
            qr.Where(qr.IsActive == true, qr.IsRefferalTo == true, qr.ReferralName.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(qr.ReferralName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }
        [WebMethod]
        public RadComboBoxData ReferralFrom(RadComboBoxContext context)
        {
            var qr = new ReferralQuery("p");
            qr.Select(qr.ReferralID.As("ValueField"), qr.ReferralName.As("TextField"), qr.PhoneNo, (qr.StreetName + " " + qr.City.Trim() + " " + qr.County.Trim() + " " + (qr.ZipCode ?? string.Empty)).As("Address"));
            qr.Where(qr.IsActive == true, qr.IsRefferalFrom == true, qr.ReferralName.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(qr.ReferralName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }
        [WebMethod]
        public RadComboBoxData Users(RadComboBoxContext context)
        {
            var qr = new AppUserQuery("p");
            qr.Select(qr.UserID.As("ValueField"), qr.UserName.As("TextField"));
            qr.Where(qr.Or(qr.UserID == context.Text, qr.UserName.Like(string.Format("%{0}%", context.Text))));
            qr.OrderBy(qr.UserName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }
        [WebMethod]
        public RadComboBoxData NursingDiagnosa(RadComboBoxContext context)
        {
            var qr = new NursingDiagnosaQuery("p");
            qr.Select(qr.NursingDiagnosaID.As("ValueField"), qr.NursingDiagnosaName.As("TextField"));
            qr.Where
                (
                    qr.SRNursingDiagnosaLevel == "10",
                    qr.SRNsDiagnosaType == "01",
                    qr.Or(qr.NursingDiagnosaID == context.Text, qr.NursingDiagnosaName.Like(string.Format("%{0}%", context.Text)))
                );
            qr.OrderBy(qr.NursingDiagnosaName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }
        [WebMethod]
        public RadComboBoxData Classes(RadComboBoxContext context)
        {
            var qr = new ClassQuery("p");
            qr.Select(qr.ClassID.As("ValueField"), qr.ClassName.As("TextField"));
            qr.Where(qr.Or(qr.ClassID == context.Text, qr.ClassName.Like(string.Format("%{0}%", context.Text))));
            qr.OrderBy(qr.ClassName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }
        [WebMethod]
        public RadComboBoxData ServiceRooms(RadComboBoxContext context)
        {
            var searching = string.Format("%{0}%", context.Text);
            var qr = new ServiceRoomQuery("p");
            var su = new ServiceUnitQuery("s");
            qr.InnerJoin(su).On(qr.ServiceUnitID == su.ServiceUnitID);
            qr.Select(qr.RoomID, qr.RoomName, su.ServiceUnitName);
            qr.Where(qr.Or(qr.RoomID == context.Text, qr.RoomName.Like(searching), su.ServiceUnitName.Like(searching)));
            qr.OrderBy(qr.RoomName.Ascending);
            qr.es.Top = MaxQueryRecord;
            var dtb = qr.LoadDataTable();
            var comboData = PopulateComboBoxDataItems(context, dtb, "RoomName", "RoomID", "ServiceUnitName");
            return comboData;
        }
        [WebMethod]
        public RadComboBoxData ServiceUnitCares(RadComboBoxContext context)
        {
            var searching = string.Format("%{0}%", context.Text);

            var qr = new ServiceUnitQuery("s");

            // ServiceUnit OK SubQuery
            var srOK = new ServiceRoomQuery("sr");
            srOK.Select(srOK.ServiceUnitID);
            srOK.Where(srOK.IsOperatingRoom == true, srOK.IsShowOnBookingOT == true,
                srOK.ServiceUnitID == qr.ServiceUnitID);

            qr.Select(qr.ServiceUnitID.As("ValueField"), qr.ServiceUnitName.As("TextField"));
            if (context.Text != "[showall]")
                qr.Where(qr.ServiceUnitName.Like(searching));

            qr.Where(qr.Or(
                    qr.SRRegistrationType.In(
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                    ), qr.ServiceUnitID.In(srOK)),
                qr.IsActive == true
            );
            qr.OrderBy(qr.ServiceUnitName.Ascending);
            qr.es.Top = MaxQueryRecord;
            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ServiceUnitFoRegistration(RadComboBoxContext context)
        {
            var searchText = string.Format("%{0}%", context.Text);

            var qr = new ServiceUnitQuery("s");

            qr.Select(qr.ServiceUnitID.As("ValueField"), qr.ServiceUnitName.As("TextField"));
            if (context.Text != "[showall]")
                qr.Where(qr.ServiceUnitName.Like(searchText));

            qr.Where(qr.Or(
                    qr.SRRegistrationType.In(
                        AppConstant.RegistrationType.InPatient,
                        AppConstant.RegistrationType.EmergencyPatient,
                        AppConstant.RegistrationType.OutPatient,
                        AppConstant.RegistrationType.MedicalCheckUp
                    )),
                qr.IsActive == true
            );
            qr.OrderBy(qr.ServiceUnitName.Ascending);
            qr.es.Top = MaxQueryRecord;
            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ConsumeMethods(RadComboBoxContext context)
        {
            var qr = new ConsumeMethodQuery("p");
            qr.Select(qr.SRConsumeMethod.As("ValueField"), (qr.SygnaText + " (" + qr.SRConsumeMethodName + ")").As("TextField"), qr.TimeSequence);
            qr.Where(qr.IsActive == true, qr.Or(qr.SygnaText.Like(string.Format("%{0}%", context.Text)), qr.SRConsumeMethodName.Like(string.Format("%{0}%", context.Text))));
            qr.OrderBy(qr.SRConsumeMethodName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData SubLedger(RadComboBoxContext context)
        {
            var query = new SubLedgersQuery("a");
            var grp = new SubLedgerGroupsQuery("b");

            query.es.Top = MaxQueryRecord;
            query.es.Distinct = true;
            query.Select
                (
                    query.SubLedgerId.As("ValueField"),
                    query.Description.As("TextField"),
                    grp.GroupName
                );
            query.InnerJoin(grp).On(query.GroupId == grp.SubLedgerGroupId);
            query.Where(query.Description.Like(string.Format("%{0}%", context.Text)));
            var comboData = PopulateComboBoxDataItems(context, query);
            foreach (RadComboBoxItemData itemData in comboData.Items)
            {
                var qr = new ItemProductMedicZatActiveQuery("m");
                var qrZat = new ZatActiveQuery("z");
                qr.InnerJoin(qrZat).On(qr.ZatActiveID == qrZat.ZatActiveID);
                qr.Select(qrZat.ZatActiveName);
                qr.Where(qr.ItemID == itemData.Value);
                var dtbZatActive = qr.LoadDataTable();
                var zatActive = dtbZatActive.Rows.Cast<DataRow>().Aggregate(string.Empty, (current, row) => string.Concat(current, ", ", row["ZatActiveName"].ToString()));
                itemData.Attributes.Add("ZatActive", zatActive);
            }

            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ZatActives(RadComboBoxContext context)
        {
            var qr = new ZatActiveQuery("p");
            qr.Select(qr.ZatActiveID.As("ValueField"), qr.ZatActiveName.As("TextField"));
            qr.Where(qr.ZatActiveName.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(qr.ZatActiveName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        #region Standard Reference
        private RadComboBoxData StandardReferenceRadComboBoxData(RadComboBoxContext context, AppEnum.StandardReference refID)
        {
            var qr = new AppStandardReferenceItemQuery("p");
            qr.Select(qr.ItemID.As("ValueField"), qr.ItemName.As("TextField"));
            qr.Where(qr.StandardReferenceID == refID);
            if (!string.IsNullOrWhiteSpace(context.Text))
                qr.Where(qr.ItemName.Like(string.Format("%{0}%", context.Text)));

            qr.OrderBy(qr.ItemName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        /// <summary>
        /// StandardReference Source untuk RadComboBox WebServiceSettings
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// Create By: Handono
        /// Sample:
        ////<telerik:RadComboBox ID="cboMedicationConsume" runat="server" Width="100%" EmptyMessage="Select a Item"
        ////EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
        ////OnClientItemsRequesting="cboMedicationConsume_ClientItemsRequesting" OnClientFocus="showDropDown">
        ////<WebServiceSettings Method="StandardReference" Path="~/WebService/ComboBoxDataService.asmx" />
        ////</telerik:RadComboBox>
        ////<script type="text/javascript">
        ////             (function (global, undefined) {
        ////    function cboMedicationConsume_ClientItemsRequesting(sender, eventArgs) {
        ////        var context = eventArgs.get_context();
        ////        context["RefID"] = "MedicationConsume";
        ////    }
        ////    global.cboMedicationConsume_ClientItemsRequesting = cboMedicationConsume_ClientItemsRequesting;
        ////})(window);
        ////</script>
        [WebMethod]
        public RadComboBoxData StandardReference(RadComboBoxContext context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var refId = (string)contextDictionary["RefID"];

            var qr = new AppStandardReferenceItemQuery("p");
            qr.Select(qr.ItemID.As("ValueField"), qr.ItemName.As("TextField"));

            qr.Where(qr.StandardReferenceID == refId);
            if (context.Text != "[showall]")
            {
                var searchText = string.Format("%{0}%", context.Text);
                qr.Where(qr.ItemName.Like(searchText));
            }

            qr.OrderBy(qr.LineNumber.Ascending, qr.ItemName.Ascending);

            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData ItemUnits(RadComboBoxContext context)
        {
            return StandardReferenceRadComboBoxData(context, AppEnum.StandardReference.ItemUnit);
        }

        [WebMethod]
        public RadComboBoxData DosageUnits(RadComboBoxContext context)
        {
            return StandardReferenceRadComboBoxData(context, AppEnum.StandardReference.DosageUnit);
        }

        #region Consume Unit
        private DataTable ConsumeUnitRSCH(string searchText, string itemID)
        {
            var qr = new AppStandardReferenceItemQuery("p");
            qr.Select(qr.ItemID.As("ValueField"), qr.ItemName.As("TextField"));
            qr.Where(qr.StandardReferenceID == "DosageUnit", qr.IsActive == true);
            if (searchText != "[showall]")
                qr.Where(qr.ItemName.Like(string.Format("%{0}%", searchText)));

            qr.OrderBy(qr.ItemName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var emb = new EmbalaceQuery("emb");
            emb.Select(emb.EmbalaceID.As("ValueField"), emb.EmbalaceName.As("TextField"));
            if (searchText != "[showall]")
                emb.Where(emb.EmbalaceName.Like(string.Format("%{0}%", searchText)));
            emb.OrderBy(emb.EmbalaceName.Ascending);

            var dtb = qr.LoadDataTable();
            emb.es.Top = MaxQueryRecord - dtb.Rows.Count;
            dtb.Merge(emb.LoadDataTable());
            return dtb;
        }
        private DataTable ConsumeUnit(string searchText, string itemID)
        {
            var srItemUnit = string.Empty;
            var srDosageUnit = string.Empty;

            var vwItem = new VwItemProductMedicNonMedicQuery("vwi");
            var iu = new AppStandardReferenceItemQuery("iu");
            vwItem.InnerJoin(iu).On(iu.StandardReferenceID == "ItemUnit" && iu.ItemID == vwItem.SRItemUnit);
            vwItem.Select(vwItem.SRItemUnit.As("ValueField"), iu.ItemName.As("TextField"), vwItem.SRDosageUnit);
            vwItem.Where(vwItem.ItemID == itemID);
            if (searchText != "[showall]")
                vwItem.Where(iu.ItemName.Like(string.Format("%{0}%", searchText)));

            var dtb = vwItem.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                srItemUnit = dtb.Rows[0]["ValueField"].ToString();
                srDosageUnit = !string.IsNullOrEmpty(dtb.Rows[0]["SRDosageUnit"].ToString()) ? dtb.Rows[0]["SRDosageUnit"].ToString() : string.Empty;
            }

            if (srDosageUnit != srItemUnit && !string.IsNullOrEmpty(srDosageUnit))
            {
                var vwItem2 = new VwItemProductMedicNonMedicQuery("vwi2");
                var iu2 = new AppStandardReferenceItemQuery("iu2");
                vwItem2.InnerJoin(iu2).On(iu2.StandardReferenceID == "DosageUnit" && iu2.ItemID == vwItem2.SRDosageUnit);
                vwItem2.Select(vwItem2.SRDosageUnit.As("ValueField"), iu2.ItemName.As("TextField"), @"<'' AS SRDosageUnit>");
                vwItem2.Where(vwItem2.ItemID == itemID);
                if (searchText != "[showall]")
                    vwItem2.Where(iu2.ItemName.Like(string.Format("%{0}%", searchText)));
                dtb.Merge(vwItem2.LoadDataTable());
            }

            var dosages = new ItemProductDosageDetailQuery("dosages");
            var iu3 = new AppStandardReferenceItemQuery("iu3");
            dosages.InnerJoin(iu3).On(iu3.StandardReferenceID == "DosageUnit" && iu3.ItemID == dosages.SRDosageUnit);
            dosages.Select(dosages.SRDosageUnit.As("ValueField"), iu3.ItemName.As("TextField"), @"<'' AS SRDosageUnit>");
            dosages.Where(dosages.ItemID == itemID, dosages.SRDosageUnit.NotIn(srItemUnit, srDosageUnit));
            if (searchText != "[showall]")
                dosages.Where(iu3.ItemName.Like(string.Format("%{0}%", searchText)));
            dosages.es.Top = MaxQueryRecord - dtb.Rows.Count;
            dtb.Merge(dosages.LoadDataTable());

            var std = new AppStandardReferenceItemQuery("std");
            std.Select(std.ItemID.As("ValueField"), std.ItemName.As("TextField"), @"<'' AS SRDosageUnit>");
            std.Where(std.StandardReferenceID == AppEnum.StandardReference.GlobalConsumeUnit, std.IsActive == true);
            if (searchText != "[showall]")
                std.Where(std.ItemName.Like(string.Format("%{0}%", searchText)));
            std.es.Top = MaxQueryRecord - dtb.Rows.Count;
            dtb.Merge(std.LoadDataTable());

            var emb = new EmbalaceQuery("emb");
            emb.Select(emb.EmbalaceID.As("ValueField"), emb.EmbalaceName.As("TextField"), @"<'' AS SRDosageUnit>");
            if (searchText != "[showall]")
                emb.Where(emb.EmbalaceName.Like(string.Format("%{0}%", searchText)));
            emb.es.Top = MaxQueryRecord - dtb.Rows.Count;
            dtb.Merge(emb.LoadDataTable());

            dtb.AsEnumerable().Distinct(System.Data.DataRowComparer.Default).ToList();

            return dtb;

            //var ipd = new ItemProductDosageDetailQuery("ipd");
            //var dsg = new AppStandardReferenceItemQuery("p");
            //ipd.Select(ipd.ItemID.As("ValueField"), dosages.ItemName.As("TextField"));
            //ipd.Where(ipd.ItemID == itemID);
            //var dtbDosage = ipd.LoadDataTable();

            //var qr = new AppStandardReferenceItemQuery("p");
            //qr.Select(qr.ItemID.As("ValueField"), qr.ItemName.As("TextField"));
            //qr.Where(qr.StandardReferenceID == AppEnum.StandardReference.GlobalConsumeUnit, qr.ItemName.Like(string.Format("%{0}%", searchText)));
            //qr.OrderBy(qr.ItemName.Ascending);
            //qr.es.Top = MaxQueryRecord - dtbDosage.Rows.Count;


            //var emb = new EmbalaceQuery("emb");
            //emb.Select(emb.EmbalaceID.As("ValueField"), emb.EmbalaceName.As("TextField"));
            //emb.Where(emb.EmbalaceName.Like(string.Format("%{0}%", searchText)));
            //emb.OrderBy(emb.EmbalaceName.Ascending);

            //var dtb = qr.LoadDataTable();
            //emb.es.Top = MaxQueryRecord - dtb.Rows.Count;
            //dtb.Merge(emb.LoadDataTable());
            //return dtb;
        }
        [WebMethod(EnableSession = true)]
        public RadComboBoxData ConsumeUnits(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            var oval = contextDictionary["ItemID"];
            var itemID = (string)(oval ?? string.Empty);

            var result = new List<RadComboBoxItemData>(context.NumberOfItems);
            var comboData = new RadComboBoxData { Items = result.ToArray() };

            try
            {
                // Untuk consume unit ini masih galau uing
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSCH")
                {
                    var dtb = ConsumeUnitRSCH(context.Text, itemID);
                    comboData = PopulateComboBoxDataItems(context, dtb, "ValueField", "TextField");
                }
                else
                {
                    //var dtb = ConsumeUnitRSCH(context.Text, itemID);
                    var dtb = ConsumeUnit(context.Text, itemID);
                    comboData = PopulateComboBoxDataItems(context, dtb, "ValueField", "TextField");
                }
            }
            catch (Exception e)
            {
                comboData.Message = e.Message;
            }

            return comboData;
        }

        #endregion
        #endregion

        [WebMethod]
        public RadComboBoxData MyPrograms(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            var userID = (string)contextDictionary["UserID"];
            var userLogID = (string)contextDictionary["UserLogID"];
            var srUserType = (string)contextDictionary["SRUserType"];

            var qr = new AppProgramQuery("a");
            var grpProgQ = new AppUserGroupProgramQuery("c");
            qr.InnerJoin(grpProgQ).On(qr.ProgramID == grpProgQ.ProgramID);

            var userQ = new AppUserUserGroupQuery("b");
            qr.InnerJoin(userQ).On(grpProgQ.UserGroupID == userQ.UserGroupID);

            var parentProg = new AppProgramQuery("p");
            qr.LeftJoin(parentProg).On(qr.ParentProgramID == parentProg.ProgramID);

            qr.Where(
                userQ.UserID == userID,
                qr.IsVisible == true,
                qr.NavigateUrl != string.Empty,
                qr.ProgramType == "PRG",
                qr.Or(
                qr.ProgramName.Like(string.Format("%{0}%", context.Text)), parentProg.ProgramName.Like(string.Format("%{0}%", context.Text)))
            );



            qr.Select
                (
                    string.Format("<CASE WHEN  CHARINDEX('{0}', a.ProgramName) = 0 THEN 1000 ELSE CHARINDEX('{0}', a.ProgramName) END  SearchIdx>", context.Text),
                    qr.ProgramID,
                    qr.ProgramName,
                    qr.NavigateUrl,
                    qr.ParentProgramID,
                    qr.ApplicationID,
                    parentProg.ProgramName.As("ParentProgramName")
                );

            qr.OrderBy("<1,3>", Temiang.Dal.DynamicQuery.esOrderByDirection.Ascending); // SearchIdx, ProgramName
            qr.es.Top = MaxQueryRecord;

            var dtbMenu = qr.LoadDataTable();
            dtbMenu.Columns.Add("NavigateUrlUpdated", typeof(string));
            BasePage.FixNavigateUrl(dtbMenu, userLogID.ToInt(), userID, srUserType);

            return PopulateComboBoxDataItems(context, dtbMenu, "ProgramID", "ProgramName");
        }


        [WebMethod]
        public RadComboBoxData ParamedicSbarPpaNotes(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            var isLoadAllPhysician = (bool)contextDictionary["IsLoadAllPhysician"];

            if (isLoadAllPhysician)
            {
                // Search from all Physician
                var qr = new ParamedicQuery("p");
                qr.Select(
                    qr.ParamedicID.As("ValueField"),
                    qr.ParamedicName.As("TextField"));
                qr.Where(qr.IsActive == true);
                if (!context.Text.Contains(" "))
                {
                    qr.Where(qr.ParamedicName.Like(string.Format("%{0}%", context.Text)));
                }
                else
                {
                    // Search per word
                    var searchs = context.Text.Split(' ');
                    var compText = new List<Dal.DynamicQuery.esComparison>();

                    foreach (string filter in searchs)
                    {
                        if (string.IsNullOrWhiteSpace(filter)) continue;

                        compText.Add(qr.ParamedicName.Like(string.Format("%{0}%", filter)));
                    }

                    qr.Where
                    (
                        qr.Or(qr.And(compText.ToArray()))
                    );
                }

                qr.OrderBy(qr.ParamedicName.Ascending);
                qr.es.Top = MaxQueryRecord;
                return PopulateComboBoxDataItems(context, qr);
            }


            // Search from Physician Team and Surgery Team

            var regNo = (string)contextDictionary["RegNo"];
            var regType = (string)contextDictionary["RegType"];
            var dateEntry = DateTime.Today;

            // 1. Ambil dari ParamedicTeam
            var pt = new ParamedicTeamQuery("pt");
            var md = new ParamedicQuery("m");
            pt.InnerJoin(md).On(pt.ParamedicID == md.ParamedicID);
            pt.Where(pt.RegistrationNo == regNo, pt.Or(pt.EndDate.IsNull(), pt.EndDate >= dateEntry.Date),
                md.IsActive == true);

            if (!context.Text.Contains(" "))
            {
                pt.Where(md.ParamedicName.Like(string.Format("%{0}%", context.Text)));
            }
            else
            {
                // Search per word
                var searchs = context.Text.Split(' ');
                var compText = new List<Dal.DynamicQuery.esComparison>();

                foreach (string filter in searchs)
                {
                    if (string.IsNullOrWhiteSpace(filter)) continue;

                    compText.Add(md.ParamedicName.Like(string.Format("%{0}%", filter)));
                }

                pt.Where
                (
                    pt.Or(pt.And(compText.ToArray()))
                );
            }

            pt.Select(pt.ParamedicID, md.ParamedicName);
            var dtb = pt.LoadDataTable();

            // 2. Tambah dg dokter Emergency jika tipe reg emergency
            if (regType == AppConstant.RegistrationType.EmergencyPatient)
            {
                md = new ParamedicQuery("m");
                var sp = new ServiceUnitParamedicQuery("sp");
                sp.InnerJoin(md).On(sp.ParamedicID == md.ParamedicID);

                var su = new ServiceUnitQuery("su");
                sp.InnerJoin(su).On(sp.ServiceUnitID == su.ServiceUnitID &
                                    su.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient);
                sp.Select(sp.ParamedicID, md.ParamedicName);

                if (!context.Text.Contains(" "))
                {
                    sp.Where(md.ParamedicName.Like(string.Format("%{0}%", context.Text)));
                }
                else
                {
                    // Search per word
                    var searchs = context.Text.Split(' ');
                    var compText = new List<Dal.DynamicQuery.esComparison>();

                    foreach (string filter in searchs)
                    {
                        if (string.IsNullOrWhiteSpace(filter)) continue;

                        compText.Add(md.ParamedicName.Like(string.Format("%{0}%", filter)));
                    }

                    sp.Where
                    (
                        sp.Or(pt.And(compText.ToArray()))
                    );
                }


                dtb.Merge(sp.LoadDataTable());
            }

            // 3. Tambah dokter Bedah & Anestesi
            var subColl = new ServiceUnitBookingCollection();
            subColl.Query.Where(subColl.Query.RegistrationNo == regNo,
                subColl.Query.Or(subColl.Query.IsVoid.IsNull(), subColl.Query.IsVoid == false));
            if (subColl.LoadAll())
            {
                foreach (var sub in subColl)
                {

                    if (!string.IsNullOrEmpty(sub.ParamedicID))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicID2))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID2;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID2);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicID3))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID3;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID3);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicID4))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID4;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID4);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicIDAnestesi))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicIDAnestesi;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicIDAnestesi);
                        dtb.Rows.Add(newRow);
                    }
                }
            }

            // kalau bukan rawat inap, tambahkan dokter dari registrasi
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(regNo))
            {
                if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                {
                    var newRow = dtb.NewRow();
                    newRow["ParamedicID"] = reg.ParamedicID;
                    newRow["ParamedicName"] = Paramedic.GetParamedicName(reg.ParamedicID);
                    dtb.Rows.Add(newRow);
                }
            }

            // Sort dan hapus redundance
            var sorted = dtb.Select(null, "ParamedicName ASC");
            var dtbSorted = dtb.Clone();
            var prevID = string.Empty;
            foreach (var row in sorted)
            {
                if (!prevID.Equals(row["ParamedicID"]))
                    dtbSorted.Rows.Add(row.ItemArray);

                prevID = row["ParamedicID"].ToString();
            }

            var result = new List<RadComboBoxItemData>(context.NumberOfItems);
            var comboData = new RadComboBoxData { Items = result.ToArray() };

            comboData = PopulateComboBoxDataItems(context, dtbSorted, "ParamedicID", "ParamedicName");

            return comboData;

        }

        [WebMethod]
        public RadComboBoxData PatientRegistrationByServiceUnitUsers(RadComboBoxContext context)
        {
            var contextDictionary = (IDictionary<string, object>)context;
            var filter = ((string)contextDictionary["EmployeeNumber"]);

            var qr = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var ausu = new AppUserServiceUnitQuery("c");
            var au = new AppUserQuery("d");

            qr.InnerJoin(pat).On(qr.PatientID == pat.PatientID)
                .InnerJoin(ausu).On(qr.ServiceUnitID == qr.ServiceUnitID)
                .InnerJoin(au).On(ausu.UserID == au.UserID);
            qr.Select(qr.RegistrationNo.As("ValueField"), @"<'(' + b.MedicalNo + ') ' + RTRIM(LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName)) AS 'TextField'>");
            qr.Where(au.PersonID == filter && qr.IsVoid == 0 && qr.SRRegistrationType == AppConstant.RegistrationType.InPatient);

            if (context.Text != "[showall]")
            {
                if (context.Text.ToLower().Contains("reg"))
                    qr.Where(qr.RegistrationNo == context.Text);
                else
                {
                    string sNumber = context.Text.Replace("-", "").Replace("/", "").Replace(".", "");
                    bool containsInt = sNumber.Any(char.IsDigit); //Will return true if the string contains a digit
                    if (!containsInt)
                        qr.Where(string.Format("<RTRIM(LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName)) LIKE '%{0}%'>", context.Text));
                    else
                        qr.Where(qr.Or(pat.MedicalNo == context.Text, string.Format("< OR b.MedicalNo LIKE '%{0}%'>", context.Text)));
                }
            }

            qr.es.Distinct = true;
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

        [WebMethod]
        public RadComboBoxData SatuSehatItem(RadComboBoxContext context)
        {
            IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
            var itemType = (string)contextDictionary["tp"];
            switch (itemType)
            {
                case "kfa":
                    {
                        var qr = new SatuSehatKfaQuery("p");
                        qr.Select(qr.SsUuid.As("Code"), qr.SsNama.As("ItemName"));
                        qr.Where(qr.Or(qr.SsType == "farmasi", qr.SsUuid == context.Text, qr.SsNama.Like(string.Format("%{0}%", context.Text))));
                        qr.OrderBy(qr.SsNama.Ascending);
                        qr.es.Top = MaxQueryRecord;
                        var dtb = qr.LoadDataTable();
                        var comboData = PopulateComboBoxDataItems(context, dtb, "Code", "Code", "ItemName");
                        return comboData;
                    }
                case "kfaza":
                    {
                        var qr = new SatuSehatKfaZaQuery("p");
                        qr.Select(qr.KfaCode.As("Code"), qr.KfaName.As("ItemName"));
                        qr.Where(qr.Or(qr.KfaCode == context.Text, qr.KfaName.Like(string.Format("%{0}%", context.Text))));
                        qr.OrderBy(qr.KfaName.Ascending);
                        qr.es.Top = MaxQueryRecord;
                        var dtb = qr.LoadDataTable();
                        var comboData = PopulateComboBoxDataItems(context, dtb, "Code", "Code", "ItemName");
                        return comboData;
                    }
                case "lab":
                    {
                        var qr = new LoincItemQuery("p");
                        qr.Select(qr.Code, qr.Display.As("ItemName"));
                        qr.Where(qr.SRLoinc == "lab", qr.Or(qr.Code == context.Text, qr.Display.Like(string.Format("%{0}%", context.Text))));
                        qr.OrderBy(qr.Display.Ascending);
                        qr.es.Top = MaxQueryRecord;
                        var dtb = qr.LoadDataTable();
                        var comboData = PopulateComboBoxDataItems(context, dtb, "Code", "Code", "ItemName");
                        return comboData;
                    }
                case "alg": // Obsolete diganti zat aktif (Handono 2025-08)
                    //{
                    //    var qr = new SatuSehatKfaQuery("p");
                    //    qr.Select(qr.SsNama.As("ItemName"), qr.SsUuid.As("Code"));
                    //    qr.Where(qr.SsNama.Like(string.Format("%{0}%", context.Text)));
                    //    qr.OrderBy(qr.SsNama.Ascending);
                    //    qr.es.Top = MaxQueryRecord;
                    //    var dtb = qr.LoadDataTable();
                    //    var comboData = PopulateComboBoxDataItems(context, dtb, "ItemName", "Code", "ItemName");
                    //    return comboData;
                    //}
                    {
                        try
                        {
                            var tokenResponse = GetToken();
                            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                                throw new Exception("Unable to retrieve access token.");

                            string keyword = context.Text;
                            string apiUrl = $"{_KFAbaseUrl}/kfa-v2/products/all?page=1&size=100&product_type=farmasi&keyword={HttpUtility.UrlEncode(keyword)}";

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                            request.Method = "GET";
                            request.Headers.Add("Authorization", "Bearer " + tokenResponse.AccessToken);
                            request.Accept = "application/json";

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                string responseJson = reader.ReadToEnd();
                                JObject json = JObject.Parse(responseJson);
                                var items = json["items"]?["data"]?.ToList();

                                if (items == null || items.Count == 0)
                                    return null;

                                // Prepare DataTable
                                DataTable dt = new DataTable();
                                dt.Columns.Add("Code");
                                dt.Columns.Add("ItemName");

                                foreach (var item in items)
                                {
                                    string namaProduk = item["name"]?.ToString();
                                    var activeIngredients = item["active_ingredients"] as JArray;

                                    if (activeIngredients != null && activeIngredients.Count > 0)
                                    {
                                        foreach (var ingredient in activeIngredients)
                                        {
                                            string zatAktif = ingredient["zat_aktif"]?.ToString();
                                            string kfaCode = ingredient["kfa_code"]?.ToString();

                                            if (!string.IsNullOrWhiteSpace(kfaCode) && !string.IsNullOrWhiteSpace(zatAktif))
                                            {
                                                string displayName = $"{namaProduk} - zat_aktif: <b style='color:red;'>{zatAktif}</b>";
                                                dt.Rows.Add(kfaCode, displayName);
                                            }
                                        }
                                    }
                                }

                                return PopulateComboBoxDataItems(context, dt, "ItemName", "Code", "ItemName");
                            }
                        }
                        catch (WebException webEx)
                        {
                            using (var stream = webEx.Response?.GetResponseStream())
                            using (var reader = new StreamReader(stream))
                            {
                                string errorText = reader.ReadToEnd();
                                throw new Exception($"API call failed: {webEx.Message} - {errorText}");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error fetching KFA items: " + ex.Message, ex);
                        }
                    }

            }
            return null;
        }

        [WebMethod]
        public RadComboBoxData ItemLaboratories(RadComboBoxContext context)
        {
            var qr = new ItemLaboratoryQuery("il");
            var itm = new ItemQuery("i");
            qr.LeftJoin(itm).On(qr.ItemID == itm.ItemID);
            qr.Select(qr.ItemID, itm.ItemName);
            qr.Where(itm.IsActive == true); // Tambah filter IsActive krn dipakai untuk entrian data transaksi (Handono 231016)
            if (context.Text != "[showall]")
                qr.Where(itm.ItemName.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(itm.ItemName.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr, "ItemID", "ItemName");
            return comboData;
        }


        [WebMethod]
        public RadComboBoxData SnomedctChiefComplaint(RadComboBoxContext context)
        {
            var qr = new SnomedctQuery("p");
            qr.Select(qr.Code.As("ValueField"), qr.DisplayNative.As("TextField"));
            qr.Where(qr.SRSnomedct == "ChiefComplaint", qr.IsActive == true);
            if (context.Text != "[showall]")
                qr.Where(qr.Display.Like(string.Format("%{0}%", context.Text)));
            qr.OrderBy(qr.Display.Ascending);
            qr.es.Top = MaxQueryRecord;

            var comboData = PopulateComboBoxDataItems(context, qr);
            return comboData;
        }

    }
}