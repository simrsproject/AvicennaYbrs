using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.HR.K3RS.MedicalHistory
{
    public partial class RadiologyResultTest : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.K3RS_EmployeeMedicalHistory;

            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                Page.Title = "Radiology Result for : " + pat.PatientName + " [MRN: " + pat.MedicalNo + " / Reg#: " + reg.RegistrationNo + "]";

                //(Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        private DataTable JobOrderTable
        {
            get
            {
                if (ViewState["JobOrderTable" + Request.UserHostName] != null) return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;

                var table = new DataTable();
                table.Columns.Add(new DataColumn("RegistrationNo", typeof(string)));
                table.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
                table.Columns.Add(new DataColumn("SequenceNo", typeof(string)));
                table.Columns.Add(new DataColumn("JobOrderSummary", typeof(string)));
                table.Columns.Add(new DataColumn("IsVoid", typeof(bool)));
                table.Columns.Add(new DataColumn("IsHdVoid", typeof(bool)));
                table.Columns.Add(new DataColumn("IsHdApproved", typeof(bool)));
                table.Columns.Add(new DataColumn("IsBillProceed", typeof(bool)));
                table.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));
                table.Columns.Add(new DataColumn("ToServiceUnitName", typeof(string)));
                table.Columns.Add(new DataColumn("FromServiceUnitName", typeof(string)));
                table.Columns.Add(new DataColumn("ToServiceUnitID", typeof(string)));
                table.Columns.Add(new DataColumn("PhysicianSenders", typeof(string)));
                table.Columns.Add(new DataColumn("ResultValue", typeof(string)));
                table.Columns.Add(new DataColumn("EpsUrlLocation", typeof(string)));
                table.Columns.Add(new DataColumn("DcomUrlLocation", typeof(string)));
                table.Columns.Add(new DataColumn("IsResultAvailable", typeof(string)));

                ViewState["JobOrderTable" + Request.UserHostName] = table;
                return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;
            }
        }

        private DataTable TransChargesDataTable(string transType)
        {
            // OutPatient maupun InPatient dimunculkan semua historynya (2019 09 13)
            var query = new TransChargesItemQuery("a");
            var tc = new TransChargesQuery("b");
            var item = new ItemQuery("c");
            var reg = new RegistrationQuery("f");
            var toUnit = new ServiceUnitQuery("tu");
            var fromUnit = new ServiceUnitQuery("fu");

            query.Select(
                tc.RegistrationNo,
                query.TransactionNo,
                query.SequenceNo,
                tc.TransactionDate,
                query.ItemID,
                item.ItemName,
                query.ChargeQuantity,
                query.Price,
                query.SRItemUnit,
                query.IsApprove,
                query.IsOrderRealization,
                query.IsVoid,
                query.IsBillProceed,
                toUnit.ServiceUnitName.As("ToServiceUnitName"),
                fromUnit.ServiceUnitName.As("fromServiceUnitName"),
                tc.ToServiceUnitID,
                tc.PhysicianSenders,
                tc.IsApproved.As("IsHdApproved"),
                tc.IsVoid.As("IsHdVoid"),
                query.CommunicationID,
                query.IsCasemixApproved,
                query.CasemixApprovedByUserID,
                query.Notes,
                query.ResultValue.Coalesce("''"),
                query.CommunicationID.Coalesce("''"),
                item.SRItemType
                );

            query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(toUnit).On(tc.ToServiceUnitID == toUnit.ServiceUnitID);
            query.InnerJoin(fromUnit).On(tc.FromServiceUnitID == fromUnit.ServiceUnitID);

            query.Where(
                tc.RegistrationNo == Request.QueryString["regNo"],
                tc.IsVoid == false,
                query.ParentNo == ""
                );

            switch (transType)
            {
                case "LAB":
                    {
                        query.Where(tc.IsOrder == true, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID);
                        break;
                    }
                case "RAD":
                    {
                        query.Where(tc.IsOrder == true);

                        var others = AppSession.Parameter.ServiceUnitRadiologyIDs;
                        if (!string.IsNullOrWhiteSpace(others))
                        {
                            if (others.Contains(";"))
                                query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                    tc.ToServiceUnitID.In(others.Split(';'))));
                            else
                                query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2,
                                    tc.ToServiceUnitID == others));
                        }
                        else
                            query.Where(query.Or(tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID,
                                    tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2));
                        break;
                    }
                case "PAT":
                    {
                        query.Where(tc.IsOrder == true, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitPathologyAnatomyID);
                        break;
                    }
                default:
                    {
                        // List selain LAB,RAD, PAT ditambah dari ServiceUnit Transaction
                        query.Where(tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitLaboratoryID,
                            tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID,
                            tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitRadiologyID2, tc.ToServiceUnitID != AppSession.Parameter.ServiceUnitPathologyAnatomyID);

                        var others = AppSession.Parameter.ServiceUnitRadiologyIDs;
                        if (!string.IsNullOrWhiteSpace(others))
                        {
                            if (others.Contains(";"))
                                query.Where(tc.ToServiceUnitID.NotIn(others.Split(';')));
                            else
                                query.Where(tc.ToServiceUnitID != others);
                        }
                        break;
                    }
            }

            query.OrderBy(tc.TransactionDate.Descending, tc.TransactionNo.Descending);
            var table = query.LoadDataTable();


            var tcs = from t in table.AsEnumerable()
                      group t by new
                      {
                          TransactionDate = t.Field<DateTime>("TransactionDate"),
                          TransactionNo = t.Field<string>("TransactionNo")
                      }
                          into g
                      select g;


            var dtb = JobOrderTable.Clone();

            // For NovaWeb
            dtb.Columns.Add("NovaWebResultUrl", typeof(string));

            var urlRoot = Helper.UrlRoot();
            var isRisPacsDataNotLoaded = true;
            var risPacsInteropVendor = AppParameter.GetParameterValue(AppParameter.ParameterItem.RisPacsInteropVendor);

            foreach (var p in tcs)
            {
                int i = 0;
                var orderContent = new StringBuilder();
                //orderContent.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
                var row = dtb.NewRow();
                foreach (DataRow orderItem in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == p.Key.TransactionNo))
                {
                    var toServiceUnitID = orderItem["ToServiceUnitID"].ToString();

                    if (i == 0)
                    {
                        //var menuView = string.Empty; // hasil sudah ditampilkan
                        //orderContent.AppendLine("<tr><td style='font-weight: bold'>");
                        //orderContent.AppendFormat("{2}: {0} - {1} {3}<br />", orderItem["TransactionNo"], Convert.ToDateTime(orderItem["TransactionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth), orderItem["ToServiceUnitName"], menuView);
                        //orderContent.AppendLine("</td></tr>");

                        //orderContent.AppendLine("<tr><td align='left'>");

                        row["IsVoid"] = orderItem["IsVoid"];
                        row["IsHdVoid"] = orderItem["IsHdVoid"];
                        row["IsHdApproved"] = orderItem["IsHdApproved"];
                        row["IsBillProceed"] = orderItem["IsBillProceed"];
                        row["ToServiceUnitName"] = orderItem["ToServiceUnitName"];
                        row["FromServiceUnitName"] = orderItem["FromServiceUnitName"];
                        row["ToServiceUnitID"] = orderItem["ToServiceUnitID"];
                        row["PhysicianSenders"] = orderItem["PhysicianSenders"];
                        row["SequenceNo"] = orderItem["SequenceNo"];
                        row["RegistrationNo"] = orderItem["RegistrationNo"];
                        row["ResultValue"] = orderItem["ResultValue"];

                        if (ConfigurationManager.AppSettings["EpsUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EpsUrlLocation"])) row["EpsUrlLocation"] = ConfigurationManager.AppSettings["EpsUrlLocation"];
                        else row["EpsUrlLocation"] = string.Empty;

                        if (ConfigurationManager.AppSettings["DcomUrlLocation"] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DcomUrlLocation"])) row["DcomUrlLocation"] = ConfigurationManager.AppSettings["DcomUrlLocation"];
                        else row["DcomUrlLocation"] = string.Empty;

                        row["IsResultAvailable"] = "0";

                        if (transType == "RAD" && AppParameter.IsYes(AppParameter.ParameterItem.IsUsingRisPacsInterop))
                        {
                            switch (risPacsInteropVendor)
                            {
                                case "INTIWID":
                                    {
                                        if (!string.IsNullOrEmpty(orderItem["ResultValue"].ToString()))
                                        {
                                            if (string.IsNullOrEmpty(orderItem["CommunicationID"].ToString()))
                                            {
                                                try
                                                {

                                                    var service = new Common.Worklist.RSI.Service();
                                                    var json = service.GetJsonOrder(new Common.Worklist.RSI.Json.Order.Root()
                                                    { uid = orderItem["ResultValue"].ToString() }, isRisPacsDataNotLoaded);

                                                    isRisPacsDataNotLoaded = false;

                                                    //row["IsResultAvailable"] = json.status == "APPROVED" ? "1" : "0";
                                                    row["IsResultAvailable"] = "1";
                                                    if (row["IsResultAvailable"].ToString() == "1")
                                                    {
                                                        var tci = new TransChargesItem();
                                                        tci.LoadByPrimaryKey(orderItem["TransactionNo"].ToString(),
                                                            orderItem["SequenceNo"].ToString());
                                                        tci.CommunicationID = row["IsResultAvailable"].ToString();
                                                        tci.Save();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                            }
                                        }
                                        else row["IsResultAvailable"] = "1";
                                        break;
                                    }
                                case "NOVAWEB":
                                    {
                                        //novrad novaris bridging

                                        if (orderItem["SRItemType"].ToString() == ItemType.Radiology && orderItem["CommunicationID"] != DBNull.Value)
                                        {
                                            var hl7 = new HL7Message();
                                            hl7.Query.es.Top = 1;
                                            hl7.Query.Where(hl7.Query.RefferenceNo == orderItem["CommunicationID"].ToString());
                                            hl7.Query.OrderBy(hl7.Query.MessageDateTime.Descending);

                                            if (hl7.Query.Load() && !string.IsNullOrEmpty(hl7.Message))
                                            {
                                                row["NovaWebResultUrl"] = Common.HL7Helper.GetResultUrl(hl7.Message);
                                            }
                                        }
                                        break;
                                    }
                            }


                        }

                    }
                    i++;

                    if (AppSession.Parameter.CasemixValidationRegistrationType.Any())
                    {
                        var casemixApprovedByUserID = string.Empty;
                        if (orderItem["CasemixApprovedByUserID"] != null)
                            casemixApprovedByUserID = orderItem["CasemixApprovedByUserID"].ToString();

                        var isOrderRealization = orderItem["IsOrderRealization"].ToBoolean();

                        orderContent.AppendFormat("{5} {6}{0} {1} {2}{7} <img src='{4}/Images/Toolbar/{3}' />&nbsp;<input type='image' id='image' onclick=\"alert('Notes : {9}')\" src='{4}/Images/{8}' alt='{9}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                            (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "<strike>" : string.Empty,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "</strike>" : string.Empty,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? "infoblue16.png" : string.Empty,
                            !isOrderRealization && !string.IsNullOrEmpty(casemixApprovedByUserID) ? orderItem["Notes"].ToString() : string.Empty);
                    }
                    else
                    {
                        orderContent.AppendFormat("{5} {0} {1} {2} <img src='{4}/Images/Toolbar/{3}' />&nbsp;&nbsp;", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"],
                            (Convert.ToBoolean(orderItem["IsOrderRealization"]) ? "post16.png" : "post16_d.png"), urlRoot, AppConstant.HtmlChar.Bullet);
                    }

                    // Radiology Result
                    if (toServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID ||
                        toServiceUnitID == AppSession.Parameter.ServiceUnitRadiologyID2 || AppSession.Parameter.ServiceUnitRadiologyIDs.Contains(toServiceUnitID))
                    {
                        //if (AppSession.UserLogin.IsRadilogyParamedic)
                        //    orderContent.AppendFormat(
                        //        "<a href=\"#\" onclick=\"javascript:examOrderRadilogyResultEdit('{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/edit16.png\"  alt=\"View\" /></a>",
                        //        orderItem["TransactionNo"], orderItem["SequenceNo"], AppSession.UserLogin.ParamedicID, urlRoot);
                        //else
                        //    orderContent.AppendFormat("<img src=\"{0}/Images/Toolbar/edit16_d.png\"  alt=\"View\" />", urlRoot);

                        if (row["NovaWebResultUrl"] != DBNull.Value && !string.IsNullOrEmpty(row["NovaWebResultUrl"].ToString()))
                        {
                            orderContent.AppendFormat(
    "<a href=\"#\" onclick=\"javascript:openUrlInNewWindow('{0}'); return false;\"><img src=\"{1}/Images/Medical/radiology.png\"  alt=\"DICOM\" /></a>",
    row["NovaWebResultUrl"], urlRoot);
                        }
                        else if (orderItem["CommunicationID"] != DBNull.Value && !string.IsNullOrEmpty(orderItem["CommunicationID"].ToString()))
                        {
                            orderContent.AppendFormat(
                                "<a href=\"#\" onclick=\"javascript:openDicom('{0}'); return false;\"><img src=\"{1}/Images/Medical/radiology.png\"  alt=\"DICOM\" /></a>",
                                orderItem["CommunicationID"], urlRoot);
                        }

                        orderContent.Append("<br />");
                    }
                    else
                    {
                        orderContent.Append("<br />");
                    }
                }

                row["TransactionNo"] = p.Key.TransactionNo;
                row["JobOrderSummary"] = orderContent.ToString();
                row["TransactionDate"] = p.Key.TransactionDate;

                dtb.Rows.Add(row);
            }
            return dtb;
        }


        #region Radiology
        protected void grdRadiology_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRadiology.DataSource = TransChargesDataTable("RAD");
        }
        protected void grdRadiology_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //var dataItem = e.Item as GridDataItem;
                var grdResult = (RadGrid)e.Item.FindControl("grdRadiologyResult");
                var transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                PopulateWithTestResult(grdResult, transactionNo);
            }
        }

        protected void grdRadiologyResult_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //var dataItem = e.Item as GridDataItem;
                var lvItemDocumentImage = (RadListView)e.Item.FindControl("lvItemDocumentImage");

                var transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                var sequenceNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SequenceNo"]);

                var qr = new TransChargesItemImageQuery();
                qr.Where(qr.TransactionNo == transactionNo, qr.SequenceNo == sequenceNo);
                var dtb = qr.LoadDataTable();

                lvItemDocumentImage.DataSource = dtb;
                lvItemDocumentImage.Rebind();
            }
        }
        #endregion

        private static void PopulateWithTestResult(RadGrid grdResult, string transactionNo)
        {
            var result = new TestResultQuery("r");

            var item = new ItemQuery("i");
            result.InnerJoin(item).On(result.ItemID == item.ItemID);

            var tci = new TransChargesItemQuery("tci");
            result.InnerJoin(tci).On(result.ItemID == tci.ItemID & tci.TransactionNo == transactionNo);

            result.Where(result.TransactionNo == transactionNo, tci.TransactionNo == transactionNo);
            result.Select(result.TransactionNo, tci.SequenceNo, item.ItemName, result.ParamedicID, result.TestResultDateTime, result.TestResult);

            var dtb = result.LoadDataTable();

            if (dtb != null && dtb.Rows.Count > 0)
            {
                // Format               
                foreach (DataRow row in dtb.Rows)
                {
                    var medic = new Paramedic();
                    medic.LoadByPrimaryKey(row["ParamedicID"].ToString());
                    var strb = new StringBuilder();
                    strb.AppendLine("<fieldset>");
                    strb.AppendFormat("<legend>Result By: {0}<br />Date: ({1})</legend>", medic.ParamedicName, Convert.ToDateTime(row["TestResultDateTime"]).ToString(AppConstant.DisplayFormat.DateHourMinute));
                    strb.AppendLine(row["TestResult"].ToString());
                    strb.AppendLine("</fieldset>");

                    row["TestResult"] = strb.ToString();
                }

                grdResult.DataSource = dtb;
                grdResult.Rebind();
            }
            else
                grdResult.Visible = false;
        }


        public override bool OnButtonOkClicked()
        {
            return true;
        }
    }
}