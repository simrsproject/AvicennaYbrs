using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Globalization;
using Telerik.Web.UI.ButtonRendering;
using System.Web.UI.HtmlControls;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class UpdateICUPhysicianList : BasePage
    {
        private List<string> keys = new List<string>();

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.UpdateICUPhysician;

            AddCboParamedic();
            if (!IsPostBack)
            {
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);

                query.Where(
                        query.SRRegistrationType.In(
                            AppConstant.RegistrationType.ClusterPatient,
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                            ),
                        query.IsActive == true
                        );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        private void AddCboParamedic() {
            //paramedic
            var parColl = new ParamedicCollection();
            parColl.Query.Where
                (
                    parColl.Query.IsActive == true,
                    //parColl.Query.IsAvailable == true,
                    parColl.Query.ParamedicFee == true
                );
            parColl.Query.OrderBy(parColl.Query.ParamedicName.Ascending);
            parColl.LoadAll();

            var tcColl = new TariffComponentCollection();
            tcColl.Query.Where(tcColl.Query.IsTariffParamedic == true)
                .OrderBy(tcColl.Query.TariffComponentID.Descending);
            if (tcColl.LoadAll()) {
                foreach (var tc in tcColl) {
                    var row = new HtmlTableRow();
                    var cell = new HtmlTableCell();
                    cell.InnerText = tc.TariffComponentName; cell.Attributes.Add("class", "label");
                    row.Cells.Add(cell);
                    cell = new HtmlTableCell();

                    var cbo = new RadComboBox();
                    cbo.ID = "cbo_" + tc.TariffComponentID.Trim();
                    cbo.Width = new Unit("300px");
                    cbo.Filter = RadComboBoxFilter.Contains;
                    cbo.AllowCustomText = true;
                    cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (Paramedic par in parColl)
                    {
                        cbo.Items.Add(new RadComboBoxItem(par.ParamedicName, par.ParamedicID));
                    }

                    cell.Controls.Add(cbo);

                    row.Cells.Add(cell);
                    tblControl.Rows.Insert(0, row);
                }
            }
        }

        private DataTable TransChargesItemComp
        {
            get
            {
                var query = new TransChargesItemCompQuery("query");
                var tci = new TransChargesItemQuery("tci");
                var par = new ParamedicQuery("par");
                var it = new ItemQuery("it");
                var su = new ServiceUnitQuery("su");
                var su2 = new ServiceUnitQuery("su2");
                var tc = new TransChargesQuery("tc");
                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");
                var sal = new AppStandardReferenceItemQuery("sal");
                var tfc = new TariffComponentQuery("tfc");

                query.InnerJoin(tci).On(tci.TransactionNo == query.TransactionNo && tci.SequenceNo == query.SequenceNo)
                    .InnerJoin(it).On(tci.ItemID == it.ItemID)
                    .InnerJoin(tc).On(tc.TransactionNo == query.TransactionNo)
                    .LeftJoin(par).On(par.ParamedicID == query.ParamedicID)
                    .InnerJoin(su).On(tc.ToServiceUnitID == su.ServiceUnitID)
                    .InnerJoin(su2).On(tc.FromServiceUnitID == su2.ServiceUnitID)
                    .InnerJoin(reg).On(reg.RegistrationNo == tc.RegistrationNo)
                    .InnerJoin(pat).On(pat.PatientID == reg.PatientID)
                    .LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & pat.SRSalutation == sal.ItemID)
                    .InnerJoin(tfc).On(tfc.TariffComponentID == query.TariffComponentID)
                    ;

                query.Select(
                    query.TransactionNo,
                    tci.SequenceNo,
                    tci.ItemID,
                    it.ItemName,
                    tfc.TariffComponentID,
                    tfc.TariffComponentName,
                    query.ParamedicID,
                    par.ParamedicName,
                    //tci.ParamedicCollectionName,
                    tc.TransactionDate,
                    pat.PatientName,
                    sal.ItemName.As("SalutationName"),
                    su2.ServiceUnitName.As("ClusterName"),
                    su.ServiceUnitName.As("ServiceUnitName")
                    );
                query.Where(tci.ItemID == cboItemID.SelectedValue);

                if (txtDate.SelectedDate.HasValue) {
                    query.Where( /*transaction date ada jamnya*/
                        tc.TransactionDate >= txtDate.SelectedDate.Value,
                        tc.TransactionDate < txtDate.SelectedDate.Value.AddDays(1));
                }
                    

                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(tc.ToServiceUnitID == cboServiceUnitID.SelectedValue);

                query.OrderBy(tc.TransactionDate.Ascending, it.ItemName.Ascending);

                query.es2.Connection.CommandTimeout = 120;

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }


        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            grdList.DataSource = TransChargesItemComp;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            if (!txtDate.SelectedDate.HasValue) return;
            if (string.IsNullOrEmpty(cboItemID.SelectedValue)) return;
            grdList.Rebind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> RegNos = new List<string>();
            List<string> TransNoSeqNo = new List<string>();
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                var ts = string.Format("{0}|{1}", 
                    dataItem.GetDataKeyValue("TransactionNo").ToString(),
                    dataItem.GetDataKeyValue("SequenceNo").ToString()) ;
                if (!TransNoSeqNo.Contains(ts)) {
                    TransNoSeqNo.Add(ts);
                }
            }

            var tcpColl = new TariffComponentCollection();
            tcpColl.Query.Where(tcpColl.Query.IsTariffParamedic == true)
                .OrderBy(tcpColl.Query.TariffComponentID.Ascending);
            tcpColl.LoadAll();

            var parColl = new ParamedicCollection();
            parColl.Query.Where(parColl.Query.IsActive == true)
                .Select(
                    parColl.Query.ParamedicID,
                    parColl.Query.ParamedicName,
                    parColl.Query.IsPrintInSlip
                );
            parColl.LoadAll();

            foreach (var ts in TransNoSeqNo) {
                var pParamedicCollectionName = string.Empty;

                var tsno = ts.Split('|');
                var tciComps = new TransChargesItemCompCollection();
                tciComps.Query.Where(tciComps.Query.TransactionNo == tsno[0], tciComps.Query.SequenceNo == tsno[1]);
                if (tciComps.LoadAll()) {
                    foreach (var tcic in tciComps) {
                        var tcp = tcpColl.Where(t => t.TariffComponentID == tcic.TariffComponentID).FirstOrDefault();
                        if (tcp != null) {
                            var ctl = Helper.FindControlRecursive(Page, "cbo_" + tcic.TariffComponentID.Trim());
                            if (ctl != null) {
                                var cbo = (ctl as RadComboBox);
                                if (!string.IsNullOrEmpty(cbo.SelectedValue)) {
                                    tcic.ParamedicID = cbo.SelectedValue;
                                }

                                if (tcp.IsPrintParamedicInSlip ?? false)
                                {
                                    var par = parColl.Where(p => p.ParamedicID == tcic.ParamedicID).First();
                                    if (par.IsPrintInSlip ?? true) {
                                        if (pParamedicCollectionName.Length == 0)
                                        {
                                            pParamedicCollectionName = par.ParamedicName;
                                        }
                                        else if (!pParamedicCollectionName.Contains(par.ParamedicName))
                                        {
                                            pParamedicCollectionName = pParamedicCollectionName + "; " + par.ParamedicName;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //-- update ParamedicCollectionName di table TransChargesItem
                    var tci = new TransChargesItem();
                    tci.LoadByPrimaryKey(tsno[0], tsno[1]);
                    tci.ParamedicCollectionName = pParamedicCollectionName;

                    tciComps.Save();
                    tci.Save();

                    var tc = new TransCharges();
                    if (tc.LoadByPrimaryKey(tci.TransactionNo)){
                        if (!RegNos.Contains(tc.RegistrationNo))
                            RegNos.Add(tc.RegistrationNo);
                    }
                }
            }

            if (RegNos.Count > 0) {
                // perlu hitung ulang jasa medis
                var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                var feeQ = feeColl.MainQuery();
                feeQ.Where(feeQ.VerificationNo.IsNull());
                feeQ.Where(feeQ.RegistrationNoMergeTo.In(RegNos));
                if (feeColl.Load(feeQ))
                {
                    feeColl.CalculateGrossFee(AppSession.UserLogin.UserID);
                    feeColl.Save();
                }
            }

            grdList.Rebind();
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboItemID.SelectedValue = string.Empty;
                cboItemID.Text = string.Empty;
            }

            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"] + " [" + ((DataRowView)e.Item.DataItem)["ItemID"] + "]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            query.Where(query.IsActive == true);

            if (!string.IsNullOrEmpty(e.Text)) {
                query.Where(
                        query.Or(
                                query.ItemName.Like(searchTextContain),
                                query.ItemID.Like(searchTextContain)
                            )
                        );
            }

            query.OrderBy(query.ItemName.Ascending);

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            pnlInfo.Visible = false;

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind") grdList.Rebind();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
            }

            RestoreValueFromCookie();
        }
    }
}