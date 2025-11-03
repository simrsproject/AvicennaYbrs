using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class Billing : BasePageDialog
    {
        #region querystring
        protected string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        protected string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        private string _patientID;
        private string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {

                    _patientID = RegistrationCurrent.PatientID;
                }
                else
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }

        Registration _reg;
        protected Registration RegistrationCurrent
        {
            get
            {
                if (_reg == null)
                {
                    _reg = new Registration();
                    _reg.LoadByPrimaryKey(RegistrationNo);
                }
                return _reg;
            }
        }
        protected string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }
        protected string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        protected string RoomID
        {
            get
            {
                return Request.QueryString["room"];
            }
        }
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            Footer.Visible = false;

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Billing of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }

            //tbarServiceUnitTrans.Items[0].Enabled = IsUserAddAble;
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            tbarServiceUnitTrans.Items[0].Enabled = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsBillingEmrAddButtonEnabled) == "Yes" ? reg.IsHoldTransactionEntry == false : IsUserAddAble;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;
        }

        private bool CheckAccess
        {
            get
            {
                if (!IsUserEditAble)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "unauthorized", "alert('Unauthorized access');", true);
                    return false;
                }
                return true;
            }
        }

        #region Service Unit Transaction
        protected void grdServiceUnitTrans_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
            }
            else if (e.CommandName == "Rebind")
            {
            }
        }

        protected void grdServiceUnitTrans_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdServiceUnitTrans.DataSource = ServiceUnitTransDataTable();
        }

        // Dilakukan di layardetil utk void nya
        //protected void grdServiceUnitTrans_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    if (!CheckAccess) return;

        //    var item = e.Item as GridDataItem;
        //    if (item == null) return;

        //    if (Helper.IsDeadlineEditedOver(Convert.ToDateTime(item["TransactionDate"].Text).Date))
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid void to system');", true);
        //        return;
        //    }

        //    using (var trans = new esTransactionScope())
        //    {
        //        var coll = new TransChargesItemCollection();
        //        coll.Query.Where(coll.Query.TransactionNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["TransactionNo"]));
        //        coll.LoadAll();

        //        foreach (var c in coll)
        //        {
        //            if (c.IsOrderRealization ?? false) continue;

        //            c.IsApprove = false;
        //            c.IsBillProceed = false;
        //            c.IsVoid = true;
        //        }

        //        coll.Save();

        //        if (coll.Count() == coll.Count(c => c.IsVoid ?? false))
        //        {
        //            var entity = new TransCharges();
        //            entity.LoadByPrimaryKey(Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["TransactionNo"]));
        //            if (!(entity.IsVoid ?? false))
        //            {
        //                entity.IsApproved = false;
        //                entity.IsBillProceed = false;
        //                entity.IsVoid = true;
        //                entity.Save();
        //            }
        //        }

        //        trans.Complete();
        //    }
        //    grdServiceUnitTrans.Rebind();
        //}

        private DataTable ServiceUnitTransDataTable()
        {
            // Transaksi hanya dimunculkan utk episode bersangkutan krn layar ini utk keperluan dokter entri
            // Untuk historynya diakses di Service Unit Transaction entry
            var dtb = ServiceUnitTransDataTable(FromRegistrationNo);
            var dtb2 = ServiceUnitTransDataTable(RegistrationNo);
            dtb2.Merge(dtb);
            return dtb2;
        }

        private DataTable ServiceUnitTransDataTable(string regNo)
        {
            var query = new TransChargesItemQuery("a");
            var transCharges = new TransChargesQuery("b");
            var item = new ItemQuery("c");
            var unit = new ServiceUnitQuery("x");
            var user = new AppUserQuery("u");

            query.Select(
                query.TransactionNo,
                query.SequenceNo,
                transCharges.TransactionDate,
                query.ItemID,
                item.ItemName,
                query.ChargeQuantity,
                query.Price,
                query.SRItemUnit,
                query.IsApprove,
                query.IsOrderRealization,
                query.IsVoid,
                query.IsBillProceed,
                unit.ServiceUnitName,
                unit.ServiceUnitID,
                user.UserName
                );

            query.InnerJoin(transCharges).On(query.TransactionNo == transCharges.TransactionNo);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(unit).On(transCharges.ToServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(user).On(transCharges.CreatedByUserID == user.UserID);

            query.Where(
                transCharges.RegistrationNo == regNo,
                transCharges.IsOrder == false,
                transCharges.IsVoid == false
                );

            if (AppSession.UserLogin.SRUserType == UserLogin.UserType.Doctor && !AppParameter.IsYes(AppParameter.ParameterItem.IsShowOtherParamedicBillingAtEmr))
            {
                query.Where(transCharges.CreatedByUserID == AppSession.UserLogin.UserID);
            }

            query.OrderBy(
                transCharges.TransactionNo.Descending
                );
            var table = query.LoadDataTable();


            var prescs = from t in table.AsEnumerable()
                         group t by new
                         {
                             TransactionNo = t.Field<string>("TransactionNo"),
                             UserName = t.Field<string>("UserName"),
                             TransactionDate = t.Field<DateTime>("TransactionDate")
                         }
                             into g
                         select g;


            var dtb = ServiceUnitTransTable.Clone();

            foreach (var p in prescs)
            {
                int i = 0;
                double total = 0;
                var orderContent = new StringBuilder();
                orderContent.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
                var row = dtb.NewRow();
                foreach (DataRow orderItem in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == p.Key.TransactionNo))
                {
                    if (i == 0)
                    {
                        orderContent.AppendLine("<tr><td align='left' style='font-style: italic'>");

                        row["IsVoid"] = orderItem["IsVoid"];
                        row["IsApprove"] = orderItem["IsApprove"];
                        row["IsBillProceed"] = orderItem["IsBillProceed"];
                        row["ServiceUnitName"] = orderItem["ServiceUnitName"];
                        row["ServiceUnitID"] = orderItem["ServiceUnitID"];
                    }
                    i++;

                    orderContent.AppendFormat("{0} {1} {2}<br />", orderItem["ItemName"], orderItem["ChargeQuantity"], orderItem["SRItemUnit"]);

                    total += Convert.ToDouble(orderItem["ChargeQuantity"]) * Convert.ToDouble(orderItem["Price"]);
                }

                orderContent.AppendFormat("<b>{0}</b>", " (Rp. " + string.Format("{0:n2}", (total)) + ")");

                orderContent.Append("</td></tr></table>");

                row["TransactionNo"] = p.Key.TransactionNo;
                row["ServiceUnitTransSummary"] = orderContent.ToString();
                row["TransactionDate"] = p.Key.TransactionDate;
                row["UserName"] = p.Key.UserName;

                dtb.Rows.Add(row);
            }
            return dtb;
        }

        private DataTable ServiceUnitTransTable
        {
            get
            {
                if (ViewState["ServiceUnitTransTable" + Request.UserHostName] != null) return ViewState["ServiceUnitTransTable" + Request.UserHostName] as DataTable;

                var table = new DataTable();
                table.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
                table.Columns.Add(new DataColumn("ServiceUnitTransSummary", typeof(string)));
                table.Columns.Add(new DataColumn("IsVoid", typeof(bool)));
                table.Columns.Add(new DataColumn("IsApprove", typeof(bool)));
                table.Columns.Add(new DataColumn("IsBillProceed", typeof(bool)));
                table.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));
                table.Columns.Add(new DataColumn("ServiceUnitName", typeof(string)));
                table.Columns.Add(new DataColumn("ServiceUnitID", typeof(string)));
                table.Columns.Add(new DataColumn("UserName", typeof(string)));

                ViewState["ServiceUnitTransTable" + Request.UserHostName] = table;
                return ViewState["ServiceUnitTransTable" + Request.UserHostName] as DataTable;
            }
        }
        #endregion

    }
}
