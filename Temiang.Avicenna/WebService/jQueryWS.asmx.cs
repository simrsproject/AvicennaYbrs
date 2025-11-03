using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Xml.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.WebService
{
    /// <summary>
    /// Summary description for jQueryWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class jQueryWS : JsonRetWS
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World JsonRetWS";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetRegList()
        {
            var req = new jQueryDatatableRequest();
            req.RetrieveQueryString();

            var SUID = HttpContext.Current.Request.Params["ServiceUnitID"];
            var DateFrom = HttpContext.Current.Request.Params["DateFrom"];
            var DateTo = HttpContext.Current.Request.Params["DateTo"];
            var pendingOnly = HttpContext.Current.Request.Params["PendingOnly"] == "1";

            var regC = new RegistrationQuery("a");
            var unitC = new ServiceUnitQuery("b");
            var patientC = new PatientQuery("c");
            var tcC = new TransChargesQuery("tc");
            var tciC = new TransChargesItemQuery("tci");

            // count
            regC.Select
                (
                    regC.RegistrationNo.Count().Distinct()
                )
                .InnerJoin(unitC).On(regC.ServiceUnitID == unitC.ServiceUnitID)
                .InnerJoin(patientC).On(regC.PatientID == patientC.PatientID)
                .Where
                (
                   regC.IsClosed == false,
                   regC.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                   regC.IsHoldTransactionEntry == false,
                   regC.IsVoid == false,
                   regC.IsFromDispensary == false,
                   regC.IsNonPatient == true,
                   regC.ServiceUnitID == SUID,
                   regC.DischargeMedicalNotes != string.Empty,
                   regC.DischargeNotes != string.Empty,
                   regC.RegistrationDate.Date().Between(DateFrom, DateTo)
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                regC.Where(regC.Or(
                    regC.RegistrationNo.Like(string.Format("%{0}%", req.searchKey)),
                    regC.DischargeMedicalNotes.Like(string.Format("%{0}%", req.searchKey)),
                    regC.DischargeNotes.Like(string.Format("%{0}%", req.searchKey)),
                    string.Format("< OR RTRIM(RTRIM(c.FirstName + ' ' + c.MiddleName) + ' ' + c.LastName) like '%{0}%'>", req.searchKey)
                ));
            }
            if (pendingOnly)
            {
                regC.InnerJoin(tcC).On(regC.RegistrationNo == tcC.RegistrationNo)
                    .InnerJoin(tciC).On(tcC.TransactionNo == tciC.TransactionNo && tciC.IsApprove == true && tciC.IsOrderRealization == false);
                regC.es.Distinct = true;
            }

            var iCount = System.Convert.ToInt32(regC.LoadDataTable().Rows[0][0]);
            // end count

            var reg = new RegistrationQuery("a");
            var unit = new ServiceUnitQuery("b");
            var patient = new PatientQuery("c");
            var tc = new TransChargesQuery("tc");
            var tci = new TransChargesItemQuery("tci");
            var pay = new TransPaymentQuery("pay");

            reg.Select
                (
                    reg.RegistrationNo,
                    "<CONVERT(varchar(10), a.RegistrationDate, 103) RegistrationDate>",
                    unit.ServiceUnitName,
                    patient.PatientName,
                    reg.ServiceUnitID,
                    reg.DischargeMedicalNotes.As("TableNo"),
                    reg.DischargeNotes.As("CustomerName"),
                    pay.PaymentNo
                )
                .InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID)
                .InnerJoin(patient).On(reg.PatientID == patient.PatientID)
                .LeftJoin(pay).On(reg.RegistrationNo == pay.RegistrationNo && pay.IsVoid == false && pay.IsApproved == true)
                .Where
                (
                   reg.IsClosed == false,
                   reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                   reg.IsHoldTransactionEntry == false,
                   reg.IsVoid == false,
                   reg.IsFromDispensary == false,
                   reg.IsNonPatient == true,
                   reg.ServiceUnitID == SUID,
                   reg.DischargeMedicalNotes != string.Empty,
                   reg.DischargeNotes != string.Empty,
                   reg.RegistrationDate.Date().Between(DateFrom, DateTo)
                );

            reg.es.PageSize = req.limit;
            reg.es.PageNumber = System.Convert.ToInt32(req.start / req.limit) + 1;

            switch (req.GetColumnName(req.orderColumn))
            {
                case "RegistrationNo":
                    {
                        if (req.orderDir == "asc")
                            reg.OrderBy(reg.RegistrationNo.Ascending);
                        else
                            reg.OrderBy(reg.RegistrationNo.Descending);
                        break;
                    }
                case "RegistrationDate":
                    {
                        if (req.orderDir == "asc")
                            reg.OrderBy("<CONVERT(varchar(10), a.RegistrationDate, 103) asc>");
                        else
                            reg.OrderBy("<CONVERT(varchar(10), a.RegistrationDate, 103) desc>");
                        break;
                    }
                case "TableNo":
                    {
                        if (req.orderDir == "asc")
                            reg.OrderBy(reg.DischargeMedicalNotes.Ascending);
                        else
                            reg.OrderBy(reg.DischargeMedicalNotes.Descending);
                        break;
                    }
                case "CustomerName":
                    {
                        if (req.orderDir == "asc")
                            reg.OrderBy(reg.DischargeNotes.Ascending);
                        else
                            reg.OrderBy(reg.DischargeNotes.Descending);
                        break;
                    }
                case "ServiceUnitName":
                    {
                        if (req.orderDir == "asc")
                            reg.OrderBy(unit.ServiceUnitName.Ascending);
                        else
                            reg.OrderBy(unit.ServiceUnitName.Descending);
                        break;
                    }
            }

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                reg.Where(reg.Or(
                    reg.RegistrationNo.Like(string.Format("%{0}%", req.searchKey)),
                    reg.DischargeMedicalNotes.Like(string.Format("%{0}%", req.searchKey)),
                    reg.DischargeNotes.Like(string.Format("%{0}%", req.searchKey)),
                    string.Format("< OR RTRIM(RTRIM(c.FirstName + ' ' + c.MiddleName) + ' ' + c.LastName) like '%{0}%'>", req.searchKey)
                ));
            }
            if (pendingOnly)
            {
                reg.InnerJoin(tc).On(reg.RegistrationNo == tc.RegistrationNo)
                    .InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo && tci.IsApprove == true && tci.IsOrderRealization == false);
                reg.GroupBy(
                    "<a.RegistrationNo>",
                    "<CONVERT(varchar(10), a.RegistrationDate, 103)>",
                    unit.ServiceUnitName,
                    "<RTRIM(LTRIM(((RTRIM(LTRIM(((c.[FirstName]+' ')+c.[MiddleName])))+' ')+c.[LastName])))>",
                    "<a.ServiceUnitID>",
                    reg.DischargeMedicalNotes,
                    reg.DischargeNotes,
                    pay.PaymentNo);
            }

            DataTable dtb = reg.LoadDataTable();

            //var dt = ConvertDataTabletoJSON(dtb);

            var ret = new jQueryDatatableReturn
            {
                status = "success",
                draw = req.draw,
                recordsTotal = iCount,
                recordsFiltered = iCount,
                data = ConvertDataTabletoObject(dtb)
            };

            //return ret.Serialize();
            Context.Response.Write(ret.Serialize());
        }

        #region "Detail transaction
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCSaveReg()
        {
            var regNo = HttpContext.Current.Request.Params["regNo"];
            var suID = HttpContext.Current.Request.Params["serviceUnitID"];
            var tblNo = HttpContext.Current.Request.Params["txtTableNo"];
            var custName = HttpContext.Current.Request.Params["txtCustomerName"];

            // validate
            var ErrMsg = string.Empty;
            if (string.IsNullOrEmpty(tblNo))
            {
                ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + "Table Number";
            }
            if (string.IsNullOrEmpty(custName))
            {
                ErrMsg += (ErrMsg.Length == 0 ? "":", ") + "Customer Name";
            }

            if (!string.IsNullOrEmpty(ErrMsg))
            {
                ErrMsg += " required!!!";
                Context.Response.Write(JSonRetFormatted(ErrMsg, false));
            }
            else {
                try
                {
                    var reg = CreateRegistration(regNo, suID, tblNo, custName);

                    Context.Response.Write(JSonRetFormatted(reg.RegistrationNo));
                }
                catch (Exception ex) {
                    Context.Response.Write(JSonRetFormatted(ex.Message, false));
                }
            }
        }
        private void SetEntityValue(Registration reg, TransCharges transC, string suID, string tblNo, string custName)
        {
            //AppSession.UserLogin.UserID = "test";

            AppAutoNumberLast _autoNumber;
            _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.NonPatientCharges);

            ServiceUnitLocationCollection sulColl = new ServiceUnitLocationCollection();
            sulColl.Query.Where(sulColl.Query.ServiceUnitID == suID, sulColl.Query.IsLocationMain == true);
            sulColl.Query.es.Top = 1;

            if (sulColl.LoadAll())
            {
                Avicenna.Module.Charges.ServiceUnitTransactionDetail.
                    SetEntityValue(transC, transC.es.IsAdded, transC.TransactionNo, reg.RegistrationNo,
                        "npc", _autoNumber,
                        DateTime.Now, "00:00",
                        DateTime.Now, "00:00",
                        string.Empty, false,
                        suID, suID, sulColl.First().LocationID,
                        string.Empty, AppSession.Parameter.OutPatientClassID, string.Empty, string.Empty,
                        false, 0, false, false, string.Empty, string.Empty, string.Empty,
                        new TransChargesItemCollection(), new TransChargesItemCompCollection(),
                        new TransChargesItemConsumptionCollection(),
                        string.Empty, string.Empty, reg.GuarantorID, string.Empty, string.Empty, string.Empty, string.Empty);
            }
            else {
                throw new Exception("Invalid inventory location");
            }

            
        }
        private Registration CreateRegistration(string regNo, string suID, string tblNo, string custName) {
            if (string.IsNullOrEmpty(tblNo)) tblNo = "1";
            if (string.IsNullOrEmpty(custName)) custName = "New Customer";

            var reg = new Registration();
            if (string.IsNullOrEmpty(regNo))
            {
                reg.AddNew();
            }
            else
            {
                reg.LoadByPrimaryKey(regNo);
            }

            SaveEntity(reg, suID, tblNo, custName);
            return reg;
        }
        private void SaveEntity(Registration reg, string ServiceUnitID, string tblNo, string custName)
        {
            using (var trans = new esTransactionScope())
            {
                if (reg.es.IsAdded)
                {
                    var date = (new DateTime()).NowAtSqlServer();

                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(ServiceUnitID);

                    AppAutoNumberLast _autoNumberReg;
                    _autoNumberReg = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, BusinessObject.Reference.TransactionCode.Registration, su.DepartmentID);
                    var regNo = _autoNumberReg.LastCompleteNumber;

                    reg.RegistrationNo = regNo;
                    reg.SRRegistrationType = AppConstant.RegistrationType.OutPatient;

                    var pat = new Patient();
                    pat.LoadByPrimaryKey(AppSession.Parameter.PatientIDForCafe);

                    reg.PatientID = pat.PatientID;
                    reg.ClassID = AppSession.Parameter.OutPatientClassID;
                    reg.RegistrationDate = date;
                    reg.RegistrationTime = date.ToString("HH:mm");
                    reg.AgeInYear = Convert.ToByte(0);
                    reg.AgeInMonth = Convert.ToByte(0);
                    reg.AgeInDay = Convert.ToByte(0);
                    reg.SRShift = Registration.GetShiftID();

                    reg.DepartmentID = su.DepartmentID;
                    reg.ServiceUnitID = su.ServiceUnitID;
                    reg.ChargeClassID = AppSession.Parameter.OutPatientClassID;
                    reg.CoverageClassID = AppSession.Parameter.OutPatientClassID;
                    reg.GuarantorID = pat.GuarantorID;
                    reg.IsFromDispensary = false;
                    reg.LastCreateDateTime = date;
                    reg.LastCreateUserID = AppSession.UserLogin.UserID;
                    reg.str.ParamedicID = null;
                    reg.IsNonPatient = true;

                    //Last Update Status
                    reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    reg.LastUpdateDateTime = date;

                    reg.DischargeMedicalNotes = tblNo;
                    reg.DischargeNotes = custName;

                    reg.IsClusterAssessment = true; // penanda pos pake field ini saja deh

                    reg.Save();

                    var mrg = new MergeBilling();
                    mrg.RegistrationNo = reg.RegistrationNo;
                    mrg.FromRegistrationNo = string.Empty;

                    //Last Update Status
                    mrg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    mrg.LastUpdateDateTime = date;

                    mrg.Save();
                    _autoNumberReg.Save();
                }
                else
                {
                    reg.DischargeMedicalNotes = tblNo;
                    reg.DischargeNotes = custName;
                    reg.Save();
                }
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetReg() {
            var regNo = HttpContext.Current.Request.Params["regNo"];

            var reg = new Registration();
            if (!string.IsNullOrEmpty(regNo)) {
                reg.LoadByPrimaryKey(regNo);
            }

            var data = new
            {
                RegistrationNo = reg.RegistrationNo,
                ServiceUnitID = reg.ServiceUnitID,
                DischargeMedicalNotes = reg.DischargeMedicalNotes,
                DischargeNotes = reg.DischargeNotes
            };

            //List<object> oRet = new List<object>();
            //oRet.Add(reg);

            Context.Response.Write(JSonRetFormatted(data));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetTransList()
        {
            var req = new jQueryDatatableRequest();
            req.RetrieveQueryString();

            var regNo = HttpContext.Current.Request.Params["RegistrationNo"];

            var tcC = new TransChargesQuery("tc");
            var tciC = new TransChargesItemQuery("tci");
            var iC = new ItemQuery("i");

            // count
            tcC.Select
                (
                    tciC.TransactionNo.Count()
                )
                .InnerJoin(tciC).On(tcC.TransactionNo == tciC.TransactionNo)
                .InnerJoin(iC).On(tciC.ItemID == iC.ItemID)
                .Where
                (
                   tcC.RegistrationNo == regNo
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                tcC.Where(tcC.Or(
                    tciC.ItemID.Like(string.Format("%{0}%", req.searchKey)),
                    iC.ItemName.Like(string.Format("%{0}%", req.searchKey))
                ));
            }

            var iCount = System.Convert.ToInt32(tcC.LoadDataTable().Rows[0][0]);
            // end count

            var tc = new TransChargesQuery("tc");
            var tci = new TransChargesItemQuery("tci");
            var i = new ItemQuery("i");

            tc.Select
                (
                    tci.TransactionNo,
                    tci.SequenceNo,
                    tci.ItemID,
                    i.ItemName,
                    tci.Notes,
                    tci.ChargeQuantity.As("Qty"),
                    tci.Price,
                    tci.DiscountAmount,
                    (tci.ChargeQuantity * tci.Price - tci.DiscountAmount).As("Total"),
                    tci.IsApprove,
                    tci.IsOrderRealization
                )
                .InnerJoin(tci).On(tc.TransactionNo == tci.TransactionNo)
                .InnerJoin(i).On(tci.ItemID == i.ItemID)
                .Where
                (
                   tc.RegistrationNo == regNo
                );
            if (req.limit >= 0)
            {
                tc.es.PageSize = req.limit;
                tc.es.PageNumber = System.Convert.ToInt32(req.start / req.limit) + 1;
            }

            switch (req.GetColumnName(req.orderColumn))
            {
                case "ItemID":
                    {
                        if (req.orderDir == "asc")
                            tc.OrderBy(tci.ItemID.Ascending);
                        else
                            tc.OrderBy(tci.ItemID.Descending);
                        break;
                    }
                case "ItemName":
                    {
                        if (req.orderDir == "asc")
                            tc.OrderBy(i.ItemName.Ascending);
                        else
                            tc.OrderBy(i.ItemName.Descending);
                        break;
                    }
            }

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                tc.Where(tc.Or(
                    tci.ItemID.Like(string.Format("%{0}%", req.searchKey)),
                    i.ItemName.Like(string.Format("%{0}%", req.searchKey))
                ));
            }

            DataTable dtb = tc.LoadDataTable();

            //var dt = ConvertDataTabletoJSON(dtb);

            var ret = new jQueryDatatableReturn
            {
                status = "success",
                draw = req.draw,
                recordsTotal = iCount,
                recordsFiltered = iCount,
                data = ConvertDataTabletoObject(dtb)
            };

            //return ret.Serialize();
            Context.Response.Write(ret.Serialize());
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetPrintList()
        {
            var regNo = HttpContext.Current.Request.Params["RegistrationNo"];

            var tcColl = new TransChargesCollection();
            // count
            tcColl.Query.Where
                (
                   tcColl.Query.RegistrationNo == regNo,
                   tcColl.Query.IsApproved == 1,
                   tcColl.Query.IsVoid == 0
                );
            tcColl.LoadAll();

            var tpColl = new TransPaymentCollection();
            tpColl.Query.Where(
                tpColl.Query.RegistrationNo == regNo,
                tpColl.Query.IsVoid == false,
                tpColl.Query.IsApproved == true
                );
            tpColl.LoadAll();

            var oRet = tcColl.Select(x => new { Type = "Transaction", Value = x.TransactionNo }).Union(
                    tpColl.Select(y => new { Type = "Payment", Value = y.PaymentNo})
                );

            Context.Response.Write(JSonRetFormatted(oRet));
        }
        #endregion

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetItemGroupList(string ServiceUnitID)
        {
            var igColl = new ItemGroupCollection();
            var ig = new ItemGroupQuery("ig");
            var i = new ItemQuery("i");
            var suis = new ServiceUnitItemServiceQuery("suis");

            ig.InnerJoin(i).On(ig.ItemGroupID == i.ItemGroupID)
                .InnerJoin(suis).On(i.ItemID == suis.ItemID)
                .Where(suis.ServiceUnitID == ServiceUnitID)
                .Select(ig);
            ig.es.Distinct = true;
            igColl.Load(ig);

            Context.Response.Write(JSonRetFormatted(igColl.Select(x => 
                new { ItemGroupID = x.ItemGroupID, ItemGroupName = x.ItemGroupName, CssClass = x.CssClass})));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetItemList()
        {
            var req = new jQueryDatatableRequest();
            req.RetrieveQueryString();

            var ItemGroupID = HttpContext.Current.Request.Params["ItemGroupID"];
            var suID = HttpContext.Current.Request.Params["serviceUnitID"];

            var su = new ServiceUnit();
            su.LoadByPrimaryKey(suID);
            var locID = su.GetMainLocationId(suID); //su.LocationID;

            var iC = new ItemQuery("i");
            var suisC = new ServiceUnitItemServiceQuery("suis");
            var ibC = new ItemBalanceQuery("ib");
            var ipmC = new ItemProductMedicQuery("ipm");
            var ipnmC = new ItemProductNonMedicQuery("ipnm");
            var ikC = new ItemKitchenQuery("ik");

            // count
            iC.Select
                (
                    iC.ItemID.Count()
                )
                .LeftJoin(suisC).On(iC.ItemID == suisC.ItemID && suisC.ServiceUnitID == suID)
                .LeftJoin(ibC).On(iC.ItemID == ibC.ItemID && ibC.LocationID == locID)
                .LeftJoin(ipmC).On(ibC.ItemID == ipmC.ItemID)
                .LeftJoin(ipnmC).On(ibC.ItemID == ipnmC.ItemID)
                .LeftJoin(ikC).On(ibC.ItemID == ikC.ItemID)
                .Where(iC.IsActive == true,
                    iC.Or(
                    "< ISNULL(suis.ItemID, ib.ItemID) is not null>",
                    "< OR ISNULL(ipm.ItemID, ISNULL(ipnm.ItemID, ik.ItemID)) is not null>"
                    ),
                    "< CASE ISNULL(ipm.ItemID, '') WHEN '' THEN 1 ELSE ipm.IsSalesAvailable END = 1>",
                    "< CASE ISNULL(ipnm.ItemID, '') WHEN '' THEN 1 ELSE ipnm.IsSalesAvailable END = 1>",
                    "< CASE ISNULL(ik.ItemID, '') WHEN '' THEN 1 ELSE ik.IsSalesAvailable END = 1>"
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                iC.Where(iC.Or(
                    iC.ItemID.Like(string.Format("%{0}%", req.searchKey)),
                    iC.ItemName.Like(string.Format("%{0}%", req.searchKey))
                ));
            }
            if (!string.IsNullOrEmpty(ItemGroupID)) {
                iC.Where(iC.ItemGroupID == ItemGroupID);
            }

            var iCount = System.Convert.ToInt32(iC.LoadDataTable().Rows[0][0]);
            // end count

            var i = new ItemQuery("i");
            var suis = new ServiceUnitItemServiceQuery("suis");
            var ib = new ItemBalanceQuery("ib");
            var ipm = new ItemProductMedicQuery("ipm");
            var ipnm = new ItemProductNonMedicQuery("ipnm");
            var ik = new ItemKitchenQuery("ik");

            // count
            i.Select
                (
                    i.ItemID,
                    i.ItemName,
                    "<ISNULL(CAST(ib.Balance as varchar(10)), '*') as Balance>",
                    "<ISNULL(ISNULL(ipm.SRItemUnit, ISNULL(ipnm.SRItemUnit, ik.SRItemUnit)),'Item') ItemUnit>"
                )
                .LeftJoin(suis).On(i.ItemID == suis.ItemID && suis.ServiceUnitID == suID)
                .LeftJoin(ib).On(i.ItemID == ib.ItemID && ib.LocationID == locID)
                .LeftJoin(ipm).On(ib.ItemID == ipm.ItemID)
                .LeftJoin(ipnm).On(ib.ItemID == ipnm.ItemID)
                .LeftJoin(ik).On(ib.ItemID == ik.ItemID)
                .Where(i.IsActive == true,
                    i.Or(
                    "< ISNULL(suis.ItemID, ib.ItemID) is not null>",
                    "< OR ISNULL(ipm.ItemID, ISNULL(ipnm.ItemID, ik.ItemID)) is not null>"
                    ),
                    "< CASE ISNULL(ipm.ItemID, '') WHEN '' THEN 1 ELSE ipm.IsSalesAvailable END = 1>",
                    "< CASE ISNULL(ipnm.ItemID, '') WHEN '' THEN 1 ELSE ipnm.IsSalesAvailable END = 1>",
                    "< CASE ISNULL(ik.ItemID, '') WHEN '' THEN 1 ELSE ik.IsSalesAvailable END = 1>"
                );

            if (!string.IsNullOrEmpty(req.searchKey))
            {
                i.Where(i.Or(
                    i.ItemID.Like(string.Format("%{0}%", req.searchKey)),
                    i.ItemName.Like(string.Format("%{0}%", req.searchKey))
                ));
            }
            if (!string.IsNullOrEmpty(ItemGroupID))
            {
                i.Where(i.ItemGroupID == ItemGroupID);
            }

            i.es.PageSize = req.limit;
            i.es.PageNumber = System.Convert.ToInt32(req.start / req.limit) + 1;

            switch (req.GetColumnName(req.orderColumn))
            {
                case "ItemID":
                    {
                        if (req.orderDir == "asc")
                            i.OrderBy(i.ItemID.Ascending);
                        else
                            i.OrderBy(i.ItemID.Descending);
                        break;
                    }
                case "ItemName":
                    {
                        if (req.orderDir == "asc")
                            i.OrderBy(i.ItemName.Ascending);
                        else
                            i.OrderBy(i.ItemName.Descending);
                        break;
                    }
            }
            DataTable dtb = i.LoadDataTable();

            //var dt = ConvertDataTabletoJSON(dtb);

            var ret = new jQueryDatatableReturn
            {
                status = "success",
                draw = req.draw,
                recordsTotal = iCount,
                recordsFiltered = iCount,
                data = ConvertDataTabletoObject(dtb)
            };

            //return ret.Serialize();
            Context.Response.Write(ret.Serialize());
        }

        
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCAddItem()
        {
            var regNo = HttpContext.Current.Request.Params["RegistrationNo"];
            var itemID = HttpContext.Current.Request.Params["ItemID"];
            var add = System.Convert.ToInt32(HttpContext.Current.Request.Params["add"]);
            var suID = HttpContext.Current.Request.Params["ServiceUnitID"];

            // validate
            var ErrMsg = string.Empty;
            if (string.IsNullOrEmpty(itemID))
            {
                ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + "Invalid Item";
            }

            var reg = new Registration();
            if (string.IsNullOrEmpty(regNo))
            {
                try {
                    reg = CreateRegistration(regNo, suID, "", "");
                }
                catch (Exception ex)
                {
                    ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + ex.Message;
                }
            }
            else {
                if (!reg.LoadByPrimaryKey(regNo))
                {
                    ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + "Invalid Registration No";
                }

                // kalau sudah ada payment tidak boleh tambah transaksi
                var pay = new TransPaymentCollection();
                pay.Query.Where(pay.Query.RegistrationNo == reg.RegistrationNo, pay.Query.IsApproved == true);
                if (pay.LoadAll()) {
                    ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + "Bill is paid already, please create new one";
                }
            }

            var su = new ServiceUnit();
            if(!su.LoadByPrimaryKey(suID)){
                ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + "Invalid Service Unit";
            }
            var suloc = new ServiceUnitLocationCollection();
            suloc.Query.Where(suloc.Query.ServiceUnitID == suID, suloc.Query.IsLocationMain == true);
            
            if(!suloc.LoadAll()){
                ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + ("Invalid main location for service unit " + su.ServiceUnitName);
            }
            var locID = suloc.First().LocationID;

            var item = new Item();
            if(!item.LoadByPrimaryKey(itemID)){
                ErrMsg += (ErrMsg.Length == 0 ? "" : ", ") + ("Item not found");
            }

            if (!string.IsNullOrEmpty(ErrMsg))
            {
                Context.Response.Write(JSonRetFormatted(ErrMsg, false));
            }
            else
            {
                try
                {
                    DateTime date = (new DateTime()).NowAtSqlServer();

                    decimal tciQty = 0;

                    if (add < 0)
                    {
                        // find unapproved trans detail
                        var tciColl = new TransChargesItemCollection();
                        var tciq = new TransChargesItemQuery("tci");
                        var tcq = new TransChargesQuery("tc");
                        tciq.InnerJoin(tcq).On(tciq.TransactionNo == tcq.TransactionNo)
                            .Where(tcq.IsApproved == false, tcq.IsVoid == false,
                                tciq.IsApprove == false, tciq.IsApprove == false,
                                tcq.RegistrationNo == reg.RegistrationNo, tciq.ItemID == itemID);
                        if (tciColl.Load(tciq))
                        {
                            var tci = tciColl.First();
                            if (tci.ChargeQuantity == 0)
                            {
                                throw new Exception("There is no unapproved data to be deleted");
                            }
                            else if (tci.ChargeQuantity + add < 0) {
                                throw new Exception("Can not remove this record due to minus result");
                            }
                            else if (tci.ChargeQuantity > 0)
                            {
                                var tc = new TransCharges();
                                var tcColl = new TransChargesCollection();
                                tcColl.Query.Where(tcColl.Query.TransactionNo == tci.TransactionNo);
                                if (tcColl.LoadAll())
                                {
                                    tc = tcColl.First();

                                    var tcicpColl = new TransChargesItemConsumptionCollection();
                                    var tcicColl = new TransChargesItemCompCollection();

                                    if (tci.ChargeQuantity + add == 0)
                                    {
                                        // delete
                                        tcicpColl.Query.Where(tcicpColl.Query.TransactionNo == tci.TransactionNo,
                                            tcicpColl.Query.SequenceNo == tci.SequenceNo);
                                        tcicpColl.LoadAll();

                                        tcicColl.Query.Where(tcicColl.Query.TransactionNo == tci.TransactionNo,
                                            tcicColl.Query.SequenceNo == tci.SequenceNo);
                                        tcicColl.LoadAll();

                                        tcicpColl.MarkAllAsDeleted();
                                        tcicColl.MarkAllAsDeleted();

                                        tci.MarkAsDeleted();
                                    }
                                    else
                                    {
                                        // update - 1
                                        tci.ChargeQuantity += add;
                                    }

                                    using (var trans = new esTransactionScope())
                                    {
                                        tcicpColl.Save();
                                        tcicColl.Save();

                                        if (tci.es.IsDeleted)
                                        {
                                            tciQty = 0;

                                            var tci2 = new TransChargesItemCollection();
                                            tci2.Query.Where(tci2.Query.TransactionNo == tc.TransactionNo,
                                                tci2.Query.ItemID != itemID);
                                            if (!tci2.LoadAll())
                                            {
                                                tc.MarkAsDeleted();
                                            }
                                        }
                                        else
                                        {
                                            tciQty = tci.ChargeQuantity.Value;
                                        }

                                        tciColl.Save();
                                        tcColl.Save();

                                        //Commit if success, Rollback if failed
                                        trans.Complete();
                                    }
                                }
                                else
                                {
                                    throw new Exception("Header transaction can not be found");
                                }
                                // remove
                                tci.MarkAsDeleted();
                            }
                        }
                        else
                        {
                            throw new Exception("There is no unapproved data to be deleted");
                        }
                    }
                    else
                    {
                        var tc = new TransCharges();
                        var tcColl = new TransChargesCollection();
                        var tcq = new TransChargesQuery("tc");
                        if (item.SRItemType == "61")
                        {
                            // khusus package create header baru karena package tidak boleh mix dengan non package

                            // cari header yang masih open untuk item yang sama
                            var tciq = new TransChargesItemQuery("tci");
                            tcq.InnerJoin(tciq).On(tcq.TransactionNo == tciq.TransactionNo)
                                .Where(tcq.RegistrationNo == reg.RegistrationNo,
                                    tcq.IsVoid == false, tcq.IsApproved == false,
                                    tciq.IsVoid == false, tciq.IsApprove == false,
                                    tcq.IsPackage == true, tciq.IsPackage == true)
                                .Select(tcq);
                            tcq.es.Distinct = true;
                            if (tcColl.Load(tcq))
                            {
                                //hd = hdColl.First();
                            }
                        }
                        else
                        {
                            tcq.Where(tcq.RegistrationNo == reg.RegistrationNo,
                            tcq.IsVoid == false, tcq.IsApproved == false, tcq.IsPackage == false);
                            if (tcColl.Load(tcq))
                            {
                                //hd = hdColl.First();
                            }
                        }

                        if (tcColl.Count > 0)
                        {
                            tc = tcColl.First();
                        }
                        else
                        {
                            tc = tcColl.AddNew();

                            AppAutoNumberLast _autoNumber;
                            _autoNumber = Helper.GetNewAutoNumber(date.Date, AppEnum.AutoNumber.NonPatientCharges);
                            var TransactionNo = _autoNumber.LastCompleteNumber;
                            _autoNumber.Save();

                            tc.TransactionNo = TransactionNo;
                            tc.RegistrationNo = reg.RegistrationNo;
                            tc.TransactionDate = date;
                            tc.ExecutionDate = date;
                            tc.ReferenceNo = string.Empty;
                            tc.ResponUnitID = string.Empty;

                            tc.FromServiceUnitID = su.ServiceUnitID;
                            tc.IsBillProceed = false;
                            tc.IsApproved = false;

                            tc.ToServiceUnitID = su.ServiceUnitID;
                            tc.SRTypeResult = "TypeResult-001";

                            tc.LocationID = locID;

                            tc.ClassID = reg.ChargeClassID;
                            tc.RoomID = reg.RoomID;
                            tc.BedID = reg.BedID;
                            tc.IsRoomIn = false;
                            tc.TariffDiscountForRoomIn = 0;
                            tc.DueDate = date;
                            tc.SRShift = Registration.GetShiftID();
                            tc.SRItemType = string.Empty;
                            tc.IsProceed = false;
                            tc.IsVoid = false;
                            tc.IsAutoBillTransaction = false;
                            tc.IsOrder = false;
                            tc.IsCorrection = false;
                            tc.Notes = string.Empty;

                            tc.IsNonPatient = true;

                            tc.SurgicalPackageID = string.Empty;
                            tc.ServiceUnitBookingNo = string.Empty;

                            tc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            tc.LastUpdateDateTime = date;

                            tc.CreatedByUserID = AppSession.UserLogin.UserID;
                            tc.CreatedDateTime = date;

                            // pastikan di TransChargesItems tidak ada mixing item package dan non-package dulu ya
                            tc.IsPackage = item.SRItemType == "61";//(Request.QueryString["type"] == "mcu");

                            tc.PhysicianSenders = string.Empty;
                            tc.SRProdiaContractID = string.Empty;
                        }

                        var tciColl = new TransChargesItemCollection();
                        var tci = new TransChargesItem();

                        tciColl.Query.Where(tciColl.Query.TransactionNo == (tcColl.Count == 0 ? "xxx" : tc.TransactionNo));
                        if (tciColl.LoadAll())
                        {
                            if (tciColl.Where(x => x.ItemID == itemID).Any())
                            {
                                tci = tciColl.Where(x => x.ItemID == itemID).First();
                            }
                            else
                            {
                                tci = tciColl.AddNew();
                            }
                        }
                        else
                        {
                            tci = tciColl.AddNew();
                        }

                        var tcicColl = new TransChargesItemCompCollection();
                        var tcicpColl = new TransChargesItemConsumptionCollection();

                        if (tci.es.IsAdded)
                        {
                            // transchargesitem
                            string seqNo = "001";
                            var seqList = (tciColl
                                    .Where(c => c.ParentNo == string.Empty && c.es.IsAdded == false)
                                    .OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo));
                            if (seqList.Count() > 0)
                            {
                                seqNo = string.Format("{0:000}", int.Parse(seqList.First()) + 1);
                            }

                            var srItemUnit = "X";
                            decimal costPrice = 0;
                            switch (item.SRItemType)
                            {
                                case ItemType.Medical:
                                    var ipm = new ItemProductMedic();
                                    ipm.LoadByPrimaryKey(item.ItemID);
                                    srItemUnit = ipm.SRItemUnit;
                                    costPrice = ipm.CostPrice ?? 0;
                                    break;
                                case ItemType.NonMedical:
                                    var ipn = new ItemProductNonMedic();
                                    ipn.LoadByPrimaryKey(item.ItemID);
                                    srItemUnit = ipn.SRItemUnit;
                                    costPrice = ipn.CostPrice ?? 0;
                                    break;
                                case ItemType.Kitchen:
                                    var ik = new ItemKitchen();
                                    ik.LoadByPrimaryKey(item.ItemID);
                                    srItemUnit = ik.SRItemUnit;
                                    costPrice = ik.CostPrice ?? 0;
                                    break;
                            }

                            var grr = new Guarantor();
                            grr.LoadByPrimaryKey(reg.GuarantorID);

                            var transDate = DateTime.Now.Date;
                            if (grr.TariffCalculationMethod == 1) transDate = reg.RegistrationDate.Value.Date;

                            var tariff = (Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                  Helper.Tariff.GetItemTariff(transDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                 (Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                  Helper.Tariff.GetItemTariff(transDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, itemID, reg.GuarantorID, false, reg.SRRegistrationType));

                            if (tariff == null) {
                                Context.Response.Write(JSonRetFormatted("Undefined tariff for selected item", false));
                                return;
                            }

                            bool isItemTypeSevice = (item.SRItemType != ItemType.Medical &&
                                                     item.SRItemType != ItemType.NonMedical &&
                                                     item.SRItemType != ItemType.Kitchen);

                            Temiang.Avicenna.Module.Charges.ServiceUnitTransactionDetail.SetEntityDetail(
                                tci, seqNo, item.ItemID, item.ItemName, string.Empty, string.Empty, false, false,
                                false, 1, 1, srItemUnit, costPrice, tariff.Price, 0,
                                string.Empty, false, string.Empty, item.SRItemType == "61", false, string.Empty,
                                string.Empty, tcicColl, tci.es.IsAdded, false, string.Empty,
                                tc.TransactionNo, reg.RegistrationNo, reg.ChargeClassID, tc.ToServiceUnitID, tcicColl, date, tciColl,
                                tcicpColl, reg.GuarantorID, string.Empty, string.Empty, string.Empty, isItemTypeSevice, string.Empty, string.Empty);

                        }
                        else {
                            tci.ChargeQuantity += add;
                        }

                        using (var trans = new esTransactionScope())
                        {
                            tcColl.Save();
                            tciColl.Save();
                            tcicColl.Save();
                            tcicpColl.Save();
                            
                            tciQty = tci.ChargeQuantity.Value;

                            //Commit if success, Rollback if failed
                            trans.Complete();
                        }
                    }

                    Context.Response.Write(JSonRetFormatted(
                        new { 
                            RegistrationNo = reg.RegistrationNo,
                            ItemID = itemID,
                            Qty = tciQty
                        }));
                }
                catch (Exception ex)
                {
                    Context.Response.Write(JSonRetFormatted(ex.Message, false));
                }
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetCustomMenu()
        {
            var TransactionNo = HttpContext.Current.Request.Params["TransactionNo"];
            var SequenceNo = HttpContext.Current.Request.Params["SequenceNo"];

            var tci = new TransChargesItem();
            if (tci.LoadByPrimaryKey(TransactionNo, SequenceNo))
            {
                Context.Response.Write(JSonRetFormatted(tci.Notes));
            }
            else
            {
                Context.Response.Write(JSonRetFormatted("Data not found", false));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCSetCustomMenu()
        {
            var TransactionNo = HttpContext.Current.Request.Params["TransactionNo"];
            var SequenceNo = HttpContext.Current.Request.Params["SequenceNo"];
            var AdditionalMessage = HttpContext.Current.Request.Params["AdditionalMessage"];

            var tci = new TransChargesItem();
            if (tci.LoadByPrimaryKey(TransactionNo, SequenceNo))
            {
                try
                {
                    tci.Notes = AdditionalMessage;
                    tci.Save();

                    Context.Response.Write(JSonRetFormatted("Success"));
                }
                catch (Exception e)
                {
                    Context.Response.Write(JSonRetFormatted(e.Message, false));
                }
            }
            else
            {
                Context.Response.Write(JSonRetFormatted("Data not found", false));
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCSetDelivered()
        {
            var TransactionNo = HttpContext.Current.Request.Params["TransactionNo"];
            var SequenceNo = HttpContext.Current.Request.Params["SequenceNo"];
            var status = HttpContext.Current.Request.Params["Status"];

            var tci = new TransChargesItem();
            if (tci.LoadByPrimaryKey(TransactionNo, SequenceNo))
            {
                try
                {
                    tci.IsOrderRealization = status == "1";
                    tci.RealizationDateTime = (new DateTime()).NowAtSqlServer();
                    tci.RealizationUserID = AppSession.UserLogin.UserID;
                    tci.Save();

                    Context.Response.Write(JSonRetFormatted("Success"));
                }
                catch (Exception e)
                {
                    Context.Response.Write(JSonRetFormatted(e.Message, false));
                }
            }
            else
            {
                Context.Response.Write(JSonRetFormatted("Data not found", false));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCApprove(){
            var regNo = HttpContext.Current.Request.Params["RegistrationNo"];

            var tcColl = new TransChargesCollection();
            tcColl.Query.Where(tcColl.Query.RegistrationNo == regNo,
                tcColl.Query.IsVoid == false,
                tcColl.Query.IsApproved == false);
            if (tcColl.LoadAll())
            {
                ////Context.Response.Write(JSonRetFormatted("Under construction!!!", false));
                //Context.Response.Write(JSonRetFormatted("Success"));

                //return;

                DateTime dateExec = (new DateTime()).NowAtSqlServer();

                var msg = string.Empty;
                foreach (var tc in tcColl) {
                    var entity = new TransCharges();
                    entity.LoadByPrimaryKey(tc.TransactionNo);
                    entity.ExecutionDate = dateExec;

                    var tciColl = new TransChargesItemCollection();
                    tciColl.Query.Where(tciColl.Query.TransactionNo == tc.TransactionNo);
                    tciColl.LoadAll();

                    foreach (var tci in tciColl) {
                        tci.IsOrderRealization = true;
                    }

                    var tcicColl = new TransChargesItemCompCollection();
                    tcicColl.Query.Where(tcicColl.Query.TransactionNo == tc.TransactionNo);
                    tcicColl.LoadAll();

                    var tcipColl = new TransChargesItemConsumptionCollection();
                    tcipColl.Query.Where(tcipColl.Query.TransactionNo == tc.TransactionNo);
                    tcipColl.LoadAll();

                    var ccColl = new CostCalculationCollection();
                    ccColl.Query.Where(ccColl.Query.TransactionNo == tc.TransactionNo);
                    ccColl.LoadAll();

                    AppAutoNumberLast _amplopFilmAutoNumber = new AppAutoNumberLast();
                    ValidateArgs args = new ValidateArgs();

                    string retMsg = Temiang.Avicenna.Module.Charges.ServiceUnitTransactionDetail
                        .SetApproval(entity, true,
                        tciColl, tcicColl, tcipColl,
                        tc.ClassID, ccColl, string.Empty, tc.FromServiceUnitID, tc.ToServiceUnitID,
                        tc.TransactionDate.Value, _amplopFilmAutoNumber, args);
                    if (!retMsg.Equals(string.Empty))
                    {
                        msg += (msg.Length == 0 ? "":", ") + retMsg;
                    }
                    else
                    {
                        if (AppSession.Parameter.IsAutoPrintCafeSlipOrder)
                        {
                            PrintMenuOrder(tc.TransactionNo);
                        }
                    }
                }

                if (string.IsNullOrEmpty(msg))
                {
                    Context.Response.Write(JSonRetFormatted("Success"));
                }
                else {
                    Context.Response.Write(JSonRetFormatted(msg, false));
                }
            }
            else {
                Context.Response.Write(JSonRetFormatted("All data have been approved!", false));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetPendingApprove(string regNo)
        {
            var tcColl = new TransChargesCollection();
            tcColl.Query.Where(tcColl.Query.RegistrationNo == regNo,
                tcColl.Query.IsVoid == false,
                tcColl.Query.IsApproved == false);
            Context.Response.Write(JSonRetFormatted(tcColl.LoadAll()));
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCPrint()
        {
            var Type = HttpContext.Current.Request.Params["Type"];
            var Value = HttpContext.Current.Request.Params["Value"];

            var printerName = string.Empty;
            switch (Type) {
                case "Transaction": {
                        printerName = PrintMenuOrder(Value);
                        break;
                    }
                case "Payment": {
                        printerName = PrintPaymentSlip(Value);
                        break;
                    }
            }
            
            if (!string.IsNullOrEmpty(printerName))
            {
                Context.Response.Write(JSonRetFormatted(printerName));
            }
            else
            {
                Context.Response.Write(JSonRetFormatted("Printer not set", false));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TransactionNo"></param>
        /// <returns>PrinterName</returns>
        private string PrintMenuOrder(string TransactionNo)
        {
            return "Not Yet Available";
            // set auto print
            //var slip = AppParameter.GetParameterValue(AppParameter.ParameterItem.AppProgramCafeSlipOrder);
            //if (!string.IsNullOrEmpty(slip))
            //{
            //    var parametersSlip = new PrintJobParameterCollection();
            //    parametersSlip.AddNew("TransactionNo", TransactionNo, null, null);
            //    return PrintManager.CreatePrintJob(slip, parametersSlip);
            //}
        }
        public static string PrintPaymentSlip(string PaymentNo)
        {
            // set auto print
            var slip = AppParameter.GetParameterValue(AppParameter.ParameterItem.AppProgramCafePaymentReceive);
            if (!string.IsNullOrEmpty(slip))
            {
                var parametersSlip = new PrintJobParameterCollection();
                parametersSlip.AddNew("PaymentNo", PaymentNo, null, null);
                return PrintManager.CreatePrintJob(slip, parametersSlip);
            }
            return "Parameter: AppProgramCafePaymentReceive: Value Not Set";
        }

        #region Notification
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCNotifGetSession()
        {
            if (!string.IsNullOrEmpty(AppSession.UserLogin.UserID))
            {
                Context.Response.Write(JSonRetFormatted(AppSession.UserLogin));
            }
            else {
                Context.Response.Write(JSonRetFormatted("Session expired!!!", false));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NPCGetMainNotif()
        {
            var SUID = HttpContext.Current.Request.Params["ServiceUnitID"];
            var DateFrom = HttpContext.Current.Request.Params["DateFrom"];
            var DateTo = HttpContext.Current.Request.Params["DateTo"];

            var regColl = new RegistrationCollection();
            var dtb = regColl.RegistrationPendingRealizationForCafe(SUID, DateFrom, DateTo);

            //var xx = dtb.AsEnumerable().Select(x =>
            //    new { sTimeLimit = x.Field<string>("TimeLimit"), iCount = x.Field<int>("RegistrationNo") });

            var xx = dtb.AsEnumerable().Select(x =>
                new { sTimeLimit = x.Field<string>("TimeLimit"), RegistrationNo = x.Field<string>("RegistrationNo") });

            Context.Response.Write(JSonRetFormatted(xx));
        }
        #endregion
    }
}
