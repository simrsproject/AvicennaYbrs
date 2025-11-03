using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
//using Telerik.Web.UI.RadButton;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeRemunDataCleansingList : BasePage
    {
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.ParamedicFeeRemunerationByIDI;

            if (!IsPostBack)
            {

            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (!IsPostBack) RestoreValueFromCookie();
        }

        //protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        //{
        //    //grdRemun.Rebind();
        //    btnExportToExcel.Enabled = (newVal != AppEnum.DataMode.Read);
        //    //RefreshCommandItemGrid(oldVal, newVal);

        //}

        #region Grid Outstanding
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!txtDateStart.SelectedDate.HasValue || !txtDateEnd.SelectedDate.HasValue)
            {
                grdList.DataSource = null;
            }
            else {
                if (txtDateStart.SelectedDate.Value > txtDateEnd.SelectedDate.Value)
                {
                    grdList.DataSource = null;
                }
                else {
                    if (txtDateStart.SelectedDate.Value <= txtDateEnd.SelectedDate.Value.AddMonths(-1))
                    {
                        grdList.DataSource = null;
                    }
                    else {
                        grdList.DataSource = Remuns();
                    }
                }
            }
        }

        private DataTable Remuns()
        {
            //var dt = GetRemunDetails(0);

            var fee = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("fee");
            var tc = new TransChargesQuery("tc");
            var prd = new ParamedicFeeRemunByIdiDetailQuery("prd");
            var i = new ItemQuery("i");
            var ig = new ItemGroupQuery("ig");
            var its = new ItemIdiItemSmfQuery("its");//
            var idi = new ItemIdiQuery("idi");
            var par = new ParamedicQuery("par");
            var reg = new RegistrationQuery("reg");
            var pat = new PatientQuery("pat");
            var guar = new GuarantorQuery("guar");
            var su = new ServiceUnitQuery("su");
            var res = new TestResultQuery("res");
            var sm = new SmfQuery("sm");//
            var tpio = new TransPaymentItemOrderQuery("tpio");
            var tpiop = new TransPaymentQuery("tpiop");
            var cc = new CostCalculationQuery("cc");
            var tpib = new TransPaymentItemIntermBillQuery("tpib");
            var tpibp = new TransPaymentQuery("tpibp");
            var tpibg = new TransPaymentItemIntermBillGuarantorQuery("tpibg");
            var tpibgp = new TransPaymentQuery("tpibgp");

            //cek Join nya supaya data nya tidak double
            fee.InnerJoin(tc).On(fee.TransactionNo == tc.TransactionNo)
                //.InnerJoin(prd).On(fee.ParamedicID == prd.ParamedicID)
                .InnerJoin(i).On(fee.ItemID == i.ItemID)
                .InnerJoin(ig).On(i.ItemGroupID == ig.ItemGroupID)
                .InnerJoin(par).On(fee.ParamedicID == par.ParamedicID)
                .InnerJoin(its).On(its.ItemID == i.ItemID && its.SmfID == par.SRParamedicRL1)
                .InnerJoin(idi).On(idi.IdiCode == its.IdiCode)
                .InnerJoin(sm).On(sm.SmfID == its.SmfID)
                .InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo)
                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
                .InnerJoin(guar).On(reg.GuarantorID == guar.GuarantorID && guar.IsParamedicFeeRemun == true)
                //guar.SRGuarantorType == "09"/*BPJS*/)

                .InnerJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                .LeftJoin(res).On(fee.TransactionNo == res.TransactionNo && fee.ItemID == res.ItemID)

                .LeftJoin(tpio).On(fee.TransactionNo == tpio.TransactionNo && fee.SequenceNo == tpio.SequenceNo 
                && tpio.IsPaymentProceed == true && tpio.IsPaymentReturned == false)
                .LeftJoin(tpiop).On(tpio.PaymentNo == tpiop.PaymentNo)

                .LeftJoin(cc).On(fee.TransactionNo == cc.TransactionNo && fee.SequenceNo == cc.SequenceNo)

                .LeftJoin(tpib).On(cc.IntermBillNo == tpib.IntermBillNo && tpib.IsPaymentProceed == true 
                && tpib.IsPaymentReturned == false)
                
                .LeftJoin(tpibp).On(tpib.PaymentNo == tpibp.PaymentNo)

                .LeftJoin(tpibg).On(cc.IntermBillNo == tpibg.IntermBillNo && tpibg.IsPaymentProceed == true 
                && tpibg.IsPaymentReturned == false)

                .LeftJoin(tpibgp).On(tpibg.PaymentNo == tpibgp.PaymentNo)

                .Select(
                    fee.TransactionNo,
                    fee.SequenceNo,
                    fee.PriceItem,
                    fee.TariffComponentID, fee.ParamedicID,
                    fee.ItemID, 
                    sm.SmfID,
                    sm.SmfName,
                    par.ParamedicName,
                    guar.GuarantorName, 
                    i.ItemName,
                    ig.ItemGroupID, ig.ItemGroupName,
                    par.SRParamedicType,
                    //fee.SmfID.Coalesce("par.SRParamedicRL1"),
                    pat.MedicalNo, pat.PatientName,
                    its.IdiCode, 
                    reg.RegistrationNo, tc.ToServiceUnitID.As("ServiceUnitID"),
                    su.ServiceUnitName, tc.ExecutionDate, fee.Qty,
                    idi.Price.As("Score"),
                    idi.Rvu, idi.IdiName,
                    idi.Icd9Cm,
                    "<'' [ICD 10]>")
                .Where(
                    fee.DischargeDateMergeTo.IsNotNull(),
                    "<ISNULL(ISNULL(tpibgp.PaymentDate, tpibp.PaymentDate),tpiop.PaymentDate) between '" +
                    txtDateStart.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and '" + txtDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd") + "'>",
                    "<ISNULL(fee.IsWriteOff, 0) = 0>")
                .Where(
                    fee.Or(
                        fee.And(
                            //su.IsUsingJobOrder == true, 
                            i.SRItemType == "41",
                            res.ItemID.IsNotNull()),
                        //su.IsUsingJobOrder == false
                        i.SRItemType != "41"
                        )) // hanya yang sudah ada hasil
                ;
            //.Where(sm.SmfName.NotIn("umum", ""));

            fee.Where(sm.SmfName.NotIn("umum", ""));
            //fee.GroupBy(reg.RegistrationNo);
            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
            {
                fee.Where(fee.ParamedicID == cboParamedicID.SelectedValue);
            }
            var dtFee = fee.LoadDataTable();

            var duplicates = dtFee.AsEnumerable()
                .GroupBy(r =>
                    new
                    {
                        TransactionNo = r["TransactionNo"],
                        SequenceNo = r["SequenceNo"],
                        PriceItem = r["PriceItem"],
                        TariffComponentID = r["TariffComponentID"],
                        ParamedicID = r["ParamedicID"],
                        ParamedicName = r["ParamedicName"],
                        ItemGroupID = r["ItemGroupID"],
                        SmfID = r["SmfID"],
                        SmfName = r["SmfName"],
                        GuarantorName = r["GuarantorName"],
                        ItemName = r["ItemName"],
                        ItemGroupName = r["ItemGroupName"],
                        SRParamedicType = r["SRParamedicType"],
                        MedicalNo = r["MedicalNo"],
                        PatientName = r["PatientName"],
                        IdiCode = r["IdiCode"],
                        IdiName = r["IdiName"],
                        RegistrationNo = r["RegistrationNo"],
                        ToServiceUnitID = r["ServiceUnitID"],
                        ServiceUnitName = r["ServiceUnitName"],
                        ExecutionDate = Convert.ToDateTime(r["ExecutionDate"]).Date,
                        Qty = r["Qty"],
                        Price = r["Score"],
                        Rvu = r["Rvu"],
                        Icd9Cm = r["Icd9Cm"],
                        ItemID = r.Field<string>("ItemID")
                    }
                ).Where(gr => gr.Count() > 1)
                .SelectMany(r => r.Skip(1))
                //.CopyToDataTable();
                .ToList();

            //select g.OrderBy(dr => dr.Field<DateTime>("DateBirth")).ThenBy(dr => dr.Field<string>("Name")).Skip(1))

            //dt = dt.AsEnumerable().Select(x => new UniqueColumns(x)).Distinct().Select(y => y.row).CopyToDataTable();

        //if (duplicates.Any())
        //    Console.WriteLine("Duplicate found for Classes: {0}", String.Join(", ", duplicates.Select(dupl => dupl.Key)));
        //Console.ReadLine();

        //if (duplicates.Any())
        //{
        //    //var x = dtFee.AsEnumerable()
        //    //    .Where(r =>
        //    //    duplicates.SelectMany(
        //    //        new
        //    //        {

        //    //            //TransactionNo = r["TransactionNo"],
        //    //            //SequenceNo = r["SequenceNo"],
        //    //            //PriceItem = r["PriceItem"],
        //    //            //TariffComponentID = r["TariffComponentID"],
        //    //            //ParamedicID = r["ParamedicID"],
        //    //            //ParamedicName = r["ParamedicName"],
        //    //            //ItemGroupID = r["ItemGroupID"],
        //    //            //SmfID = r["SmfID"],
        //    //            //SmfName = r["SmfName"],
        //    //            //GuarantorName = r["GuarantorName"],
        //    //            //ItemName = r["ItemName"],
        //    //            //ItemGroupName = r["ItemGroupName"],
        //    //            //SRParamedicType = r["SRParamedicType"],
        //    //            //MedicalNo = r["MedicalNo"],
        //    //            //PatientName = r["PatientName"],
        //    //            //IdiCode = r["IdiCode"],
        //    //            //IdiName = r["IdiName"],
        //    //            //RegistrationNo = r["RegistrationNo"],
        //    //            //ToServiceUnitID = r["ServiceUnitID"],
        //    //            //ServiceUnitName = r["ServiceUnitName"],
        //    //            //ExecutionDate = Convert.ToDateTime(r["ExecutionDate"]).Date,
        //    //            //Qty = r["Qty"],
        //    //            //Price = r["Score"],
        //    //            //Rvu = r["Rvu"],
        //    //            //Icd9Cm = r["Icd9Cm"],
        //    //            ItemID = r.Field<string>("ItemID")
        //    //        })
        //    //        );
        //    //return duplicates.ToDataTable();
        //}
        //else
        //{
        //    return dtFee.Clone();
        //}

        return dtFee;
    }
        #endregion

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();
            grdList.Rebind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var tbl = Remuns();

            Common.CreateExcelFile.CreateExcelDocument(tbl, "DataCleansing.xls", this.Response);
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("query");
            var sm = new SmfQuery("sm");
            query.es.Top = 20;
            query.InnerJoin(sm).On(sm.SmfID == query.SRParamedicRL1)
            .Select
                (
                    query.ParamedicID,
                    (query.ParamedicName + " [" + query.ParamedicID + "]").As("ParamedicName")

                );
            query.Where
                (
                    query.Or
                    (
                       query.ParamedicID.Like(searchTextContain),
                       query.ParamedicName.Like(searchTextContain)

                    ),
                    query.IsActive == true,
                    
                    sm.SmfName.NotIn("umum", "")
                ) ;

            //query.Where(sm.SmfName.NotIn("umum", ""));
            query.OrderBy(sm.SmfID.Ascending);
            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "delete")
            {
                DeleteFee();
            }
        }

        private void DeleteFee() {
            var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("chkbox")).Checked)
                                                                                  .Select(dataItem => new
                                                                                  {
                                                                                      TransactionNo = dataItem.GetDataKeyValue("TransactionNo").ToString(),
                                                                                      SequenceNo = dataItem.GetDataKeyValue("SequenceNo").ToString(),
                                                                                      TariffComponentID = dataItem.GetDataKeyValue("TariffComponentID").ToString()
                                                                                  });
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            foreach (var item in items) {
                var fee = new ParamedicFeeTransChargesItemCompByDischargeDate();
                if (fee.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo, item.TariffComponentID)) {
                    if (fee.RemunByIdiID.HasValue || !string.IsNullOrEmpty(fee.VerificationNo)) {
                        // gak boleh hapus, sudah ada nomor remun atau sudah masuk verif jasmed
                        continue;
                    }
                    fee.IsWriteOff = true;
                    fee.LastUpdateDateTime = DateTime.Now;
                    fee.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    feeColl.AttachEntity(fee);
                }
            }
            if (feeColl.Count > 0) feeColl.Save();

            grdList.Rebind();
        }
    }
}