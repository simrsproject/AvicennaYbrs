using System;
using System.Drawing.Imaging;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI;
using System.Text;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class RaspaturEntry : BasePageDialog
    {
        private int RasproSeqNo
        {
            get
            {
                return Request.QueryString["rsno"].ToInt();
            }
        }

        private string Mode
        {
            get
            {
                return Request.QueryString["mod"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey(AppEnum.StandardReference.RASPRO.ToString(), AppConstant.RasproType.Raspatur);
                lblRasproName.Text = stdi.ItemName;
                lblRasproNote.Text = stdi.Note;

                txtRegistrationNo.Text = RegistrationNo;
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    txtPatientName.Text = pat.PatientName;
                    txtMedicalNo.Text = pat.MedicalNo;
                    txtDateOfBirth.SelectedDate = pat.DateOfBirth;
                }


                if (Mode.Equals("new"))
                {
                    txtRasproDateTime.SelectedDate = DateTime.Now;

                    // TODO: Betulkan ServiceUnit saat isi form Raspro 
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(reg.ServiceUnitID);
                    txtServiceUnitName.Text = su.ServiceUnitName;

                    txtParamedicName.Text = Paramedic.GetParamedicName(ParamedicTeam.DPJP(RegistrationNo).ParamedicID);
                }
                else
                {
                    var rr = new RegistrationRaspro();
                    rr.LoadByPrimaryKey(RegistrationNo, RasproSeqNo);

                    txtRasproDateTime.SelectedDate = rr.RasproDateTime;

                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(rr.ServiceUnitID);
                    txtServiceUnitName.Text = su.ServiceUnitName;

                    txtParamedicName.Text = Paramedic.GetParamedicName(rr.ParamedicID);

                    if (!string.IsNullOrWhiteSpace(rr.AdviseByParamedicID))
                        ComboBox.PopulateWithOneParamedic(cboAdviseByParamedicID, rr.AdviseByParamedicID);

                    chkIsExternalCultureLabTest.Checked = rr.IsExternalCultureLabTest;
                }

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
            btnOk.Visible = Mode.Equals("new");
        }


        private void SaveAndClose()
        {
            var rr = new RegistrationRaspro
            {
                SRRaspro = AppConstant.RasproType.Raspatur,
                RegistrationNo = RegistrationNo,
                ParamedicID = ParamedicTeam.DPJP(RegistrationNo).ParamedicID,
                RasproDateTime = DateTime.Now
            };

            // TODO: Betulkan ServiceUnit saat isi form Raspro 
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            rr.ServiceUnitID = reg.ServiceUnitID;

            // NewSeqNo
            var newSeqNo = 1;
            var lastrr = new RegistrationRaspro();
            lastrr.Query.Where(lastrr.Query.RegistrationNo == rr.RegistrationNo);
            lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
            lastrr.Query.es.Top = 1;
            if (lastrr.Query.Load())
                newSeqNo = (lastrr.SeqNo ?? 0) + 1;

            rr.SeqNo = newSeqNo;

            // Save Sign
            if (!string.IsNullOrWhiteSpace(hdnImage.Value))
            {
                var imgHelper = new ImageHelper();
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage.Value), new System.Drawing.Size(332, 185));
                rr.SignImage = imgHelper.ToByteArray(resized,ImageFormat.Png);
            }

            rr.AdviseByParamedicID = cboAdviseByParamedicID.SelectedValue;
            rr.AbRestrictionID = "RASPATUR"; //TODO: validasi dari hasil kultur
            rr.AntibioticLevel = 9999; // Set bisa All Antibiotics tapi dokternya diberi ref dari hasil kultur
            if (chkIsExternalCultureLabTest.Checked == false)
                rr.ReferenceNo = hdnTransactionNo.Value; // Order Lab No

            rr.IsExternalCultureLabTest = chkIsExternalCultureLabTest.Checked;
            rr.Save();

            // Utk refresh di PrescriptionEntry
            Session["RasproSeqNo"] = rr.SeqNo;
            Session["SRRaspro"] = rr.SRRaspro;
            Session["RasproRefNo"] = rr.ReferenceNo;

            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "save")
            {
                SaveAndClose();
            }
        }

        #region Laboratory
        protected void grdLaboratory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLaboratory.DataSource = LabCultureOrder();
        }
        protected void grdLaboratory_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //var dataItem = e.Item as GridDataItem;
                var grdResult = (RadGrid)e.Item.FindControl("grdLaboratoryResult");

                // InitializeCultureGrid manual krn tidak terjangkau oleh fungsi di basepage
                grdResult.InitializeCultureGrid();

                // Set Datasource
                string transactionNo;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "LINK_LIS":
                        transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                        string labNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ResultValue"]);
                        grdResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, transactionNo, labNo);
                        break;
                    default:
                        transactionNo = Convert.ToString(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["TransactionNo"]);
                        grdResult.DataSource = MainContent.ExamOrderHistCtl.LaboratoryResult(RegistrationNo, transactionNo);
                        break;
                }
                grdResult.Rebind();
            }

        }

        //private DataTable LabCultureOrder()
        //{
        //    // OutPatient maupun InPatient dimunculkan semua historynya (2019 09 13)
        //    var query = new TransChargesItemQuery("a");
        //    var tc = new TransChargesQuery("b");
        //    var item = new ItemQuery("c");
        //    var reg = new RegistrationQuery("f");
        //    var toUnit = new ServiceUnitQuery("tu");
        //    var fromUnit = new ServiceUnitQuery("fu");
        //    var itemLab = new ItemLaboratoryQuery("ilb");

        //    query.Select(
        //        tc.RegistrationNo,
        //        query.TransactionNo,
        //        query.SequenceNo,
        //        tc.TransactionDate,
        //        query.ItemID,
        //        item.ItemName,
        //        query.ChargeQuantity,
        //        query.Price,
        //        query.SRItemUnit,
        //        query.IsApprove,
        //        query.IsOrderRealization,
        //        query.IsVoid,
        //        query.IsBillProceed,
        //        toUnit.ServiceUnitName.As("ToServiceUnitName"),
        //        fromUnit.ServiceUnitName.As("fromServiceUnitName"),
        //        tc.ToServiceUnitID,
        //        tc.PhysicianSenders,
        //        tc.IsApproved.As("IsHdApproved"),
        //        tc.IsVoid.As("IsHdVoid"),
        //        query.CommunicationID,
        //        query.IsCasemixApproved,
        //        query.CasemixApprovedByUserID,
        //        query.Notes,
        //        query.ResultValue.Coalesce("''"),
        //        query.CommunicationID.Coalesce("''"),
        //        item.SRItemType
        //        );

        //    query.InnerJoin(tc).On(query.TransactionNo == tc.TransactionNo);
        //    query.InnerJoin(item).On(query.ItemID == item.ItemID);
        //    query.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo);
        //    query.InnerJoin(toUnit).On(tc.ToServiceUnitID == toUnit.ServiceUnitID);
        //    query.InnerJoin(fromUnit).On(tc.FromServiceUnitID == fromUnit.ServiceUnitID);
        //    query.InnerJoin(itemLab).On(query.ItemID == itemLab.ItemID);
        //    query.Where(reg.RegistrationNo.In(MergeRegistrations), itemLab.IsCulture == true);

        //    query.Where(

        //        tc.IsVoid == false,
        //        query.ParentNo == ""
        //        );

        //    query.Where(tc.IsOrder == true, tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID);


        //    query.OrderBy(tc.TransactionDate.Descending, tc.TransactionNo.Descending);
        //    var table = query.LoadDataTable();


        //    var tcs = from t in table.AsEnumerable()
        //              group t by new
        //              {
        //                  TransactionDate = t.Field<DateTime>("TransactionDate"),
        //                  TransactionNo = t.Field<string>("TransactionNo")
        //              }
        //                  into g
        //              select g;


        //    var dtb = new DataTable();
        //    dtb.Columns.Add(new DataColumn("RegistrationNo", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("SequenceNo", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("JobOrderSummary", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("IsVoid", typeof(bool)));
        //    dtb.Columns.Add(new DataColumn("IsHdVoid", typeof(bool)));
        //    dtb.Columns.Add(new DataColumn("IsHdApproved", typeof(bool)));
        //    dtb.Columns.Add(new DataColumn("IsBillProceed", typeof(bool)));
        //    dtb.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));
        //    dtb.Columns.Add(new DataColumn("ToServiceUnitName", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("FromServiceUnitName", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("ToServiceUnitID", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("PhysicianSenders", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("ResultValue", typeof(string)));
        //    dtb.Columns.Add(new DataColumn("IsResultAvailable", typeof(string)));

        //    var displayTotal = AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor;
        //    var urlRoot = Helper.UrlRoot();
        //    var risPacsInteropVendor = AppParameter.GetParameterValue(AppParameter.ParameterItem.RisPacsInteropVendor);

        //    foreach (var p in tcs)
        //    {
        //        int i = 0;
        //        var orderContent = new StringBuilder();
        //        //orderContent.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
        //        var row = dtb.NewRow();
        //        foreach (DataRow orderItem in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == p.Key.TransactionNo))
        //        {
        //            var toServiceUnitID = orderItem["ToServiceUnitID"].ToString();

        //            if (i == 0)
        //            {
        //                row["IsVoid"] = orderItem["IsVoid"];
        //                row["IsHdVoid"] = orderItem["IsHdVoid"];
        //                row["IsHdApproved"] = orderItem["IsHdApproved"];
        //                row["IsBillProceed"] = orderItem["IsBillProceed"];
        //                row["ToServiceUnitName"] = orderItem["ToServiceUnitName"];
        //                row["FromServiceUnitName"] = orderItem["FromServiceUnitName"];
        //                row["ToServiceUnitID"] = orderItem["ToServiceUnitID"];
        //                row["PhysicianSenders"] = orderItem["PhysicianSenders"];
        //                row["SequenceNo"] = orderItem["SequenceNo"];
        //                row["RegistrationNo"] = orderItem["RegistrationNo"];
        //                row["ResultValue"] = orderItem["ResultValue"];

        //                row["IsResultAvailable"] = "0";

        //            }
        //            i++;

        //            orderContent.Append("<br />");
        //        }

        //        row["TransactionNo"] = p.Key.TransactionNo;
        //        row["JobOrderSummary"] = orderContent.ToString();
        //        row["TransactionDate"] = p.Key.TransactionDate;

        //        dtb.Rows.Add(row);
        //    }
        //    return dtb;
        //}


        private DataTable LabCultureOrder()
        {
            var tcq = new TransChargesQuery("b");
            var itemLab = new ItemLaboratoryQuery("ilb");

            if (RasproSeqNo == 0) // New Mode
            {
                // Tampilkan test lab culture terakhir
                var tci = new TransChargesItemQuery("a");
                tcq.InnerJoin(tci).On(tcq.TransactionNo == tci.TransactionNo);

                tcq.InnerJoin(itemLab).On(tci.ItemID == itemLab.ItemID);

                tcq.Where(tcq.RegistrationNo.In(MergeRegistrations), itemLab.IsCulture == true);

                tcq.es.Top = 1;
                tcq.OrderBy(tcq.TransactionDate.Descending, tcq.TransactionNo.Descending);
                tcq.Select(tcq);

                var lastLabCulture = new TransCharges();

                if (lastLabCulture.Load(tcq))
                {
                    hdnTransactionNo.Value = lastLabCulture.TransactionNo;
                }
            }
            else // View Mode
            {
                var rr = new RegistrationRaspro();
                rr.LoadByPrimaryKey(RegistrationNo, RasproSeqNo);
                hdnTransactionNo.Value = rr.ReferenceNo;
            }


            var query = new TransChargesItemQuery("a");
            tcq = new TransChargesQuery("b");
            var item = new ItemQuery("c");
            var toUnit = new ServiceUnitQuery("tu");
            var fromUnit = new ServiceUnitQuery("fu");
            itemLab = new ItemLaboratoryQuery("ilb");

            query.Select(
                tcq.RegistrationNo,
                query.TransactionNo,
                query.SequenceNo,
                tcq.TransactionDate,
                query.ItemID,
                item.ItemName,
                query.SRItemUnit,
                query.IsApprove,
                query.IsOrderRealization,
                query.IsVoid,
                toUnit.ServiceUnitName.As("ToServiceUnitName"),
                fromUnit.ServiceUnitName.As("fromServiceUnitName"),
                tcq.ToServiceUnitID,
                tcq.PhysicianSenders,
                tcq.IsApproved.As("IsHdApproved"),
                tcq.IsVoid.As("IsHdVoid"),
                query.CommunicationID,
                query.IsCasemixApproved,
                query.CasemixApprovedByUserID,
                query.Notes,
                query.ResultValue.Coalesce("''"),
                query.CommunicationID.Coalesce("''"),
                item.SRItemType
                );

            query.InnerJoin(tcq).On(query.TransactionNo == tcq.TransactionNo);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(toUnit).On(tcq.ToServiceUnitID == toUnit.ServiceUnitID);
            query.InnerJoin(fromUnit).On(tcq.FromServiceUnitID == fromUnit.ServiceUnitID);
            query.InnerJoin(itemLab).On(query.ItemID == itemLab.ItemID);
            query.Where(query.TransactionNo == hdnTransactionNo.Value);
            var table = query.LoadDataTable();

            var tcs = from t in table.AsEnumerable()
                      group t by new
                      {
                          TransactionDate = t.Field<DateTime>("TransactionDate"),
                          TransactionNo = t.Field<string>("TransactionNo")
                      }
                          into g
                      select g;

            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("RegistrationNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("SequenceNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("JobOrderSummary", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsVoid", typeof(bool)));
            dtb.Columns.Add(new DataColumn("IsHdVoid", typeof(bool)));
            dtb.Columns.Add(new DataColumn("IsHdApproved", typeof(bool)));
            dtb.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));
            dtb.Columns.Add(new DataColumn("ToServiceUnitName", typeof(string)));
            dtb.Columns.Add(new DataColumn("FromServiceUnitName", typeof(string)));
            dtb.Columns.Add(new DataColumn("ToServiceUnitID", typeof(string)));
            dtb.Columns.Add(new DataColumn("PhysicianSenders", typeof(string)));
            dtb.Columns.Add(new DataColumn("ResultValue", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsResultAvailable", typeof(string)));


            foreach (var p in tcs)
            {
                int i = 0;
                var orderContent = new StringBuilder();
                var row = dtb.NewRow();
                foreach (DataRow orderItem in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == p.Key.TransactionNo))
                {
                    var toServiceUnitID = orderItem["ToServiceUnitID"].ToString();

                    if (i == 0)
                    {
                        row["IsVoid"] = orderItem["IsVoid"];
                        row["IsHdVoid"] = orderItem["IsHdVoid"];
                        row["IsHdApproved"] = orderItem["IsHdApproved"];
                        row["ToServiceUnitName"] = orderItem["ToServiceUnitName"];
                        row["FromServiceUnitName"] = orderItem["FromServiceUnitName"];
                        row["ToServiceUnitID"] = orderItem["ToServiceUnitID"];
                        row["PhysicianSenders"] = orderItem["PhysicianSenders"];
                        row["SequenceNo"] = orderItem["SequenceNo"];
                        row["RegistrationNo"] = orderItem["RegistrationNo"];
                        row["ResultValue"] = orderItem["ResultValue"];

                        row["IsResultAvailable"] = "0";

                    }

                    orderContent.AppendFormat("{0} {1}<br />", AppConstant.HtmlChar.Bullet, orderItem["ItemName"]);
                    i++;

                }

                row["TransactionNo"] = p.Key.TransactionNo;
                row["TransactionDate"] = p.Key.TransactionDate;
                row["JobOrderSummary"] = orderContent.ToString();
                dtb.Rows.Add(row);
            }
            return dtb;
        }

        #endregion

    }
}
