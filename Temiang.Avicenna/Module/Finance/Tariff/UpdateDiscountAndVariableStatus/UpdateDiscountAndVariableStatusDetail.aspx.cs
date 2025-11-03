using System;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class UpdateDiscountAndVariableStatusDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.TariffUpdateDiscountAndVariableStatus;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtItemID.Text = Request.QueryString["itemId"];

                var item = new Item();
                item.LoadByPrimaryKey(txtItemID.Text);
                txtItemName.Text = item.ItemName;
                txtGroupID.Text = item.ItemGroupID;

                var gr = new ItemGroup();
                gr.LoadByPrimaryKey(txtGroupID.Text);
                txtGroupName.Text = gr.ItemGroupName;

                var itq = new ItemTariffQuery();
                itq.Where(itq.ItemID == txtItemID.Text, itq.StartingDate.Date() <= DateTime.Now.Date,
                          itq.SRTariffType == AppSession.Parameter.DefaultTariffType);
                itq.OrderBy(itq.StartingDate.Descending);
                itq.es.Top = 1;
                var it = new ItemTariff();
                it.Load(itq);

                chkIsAllowCito.Checked = it.IsAllowCito ?? false;
                chkIsCitoInPercent.Checked = it.IsCitoInPercent ?? false;
                txtCitoValue.Value = Convert.ToDouble(it.CitoValue);

                //-- display grid
                PopulateTariffComponent(txtItemID.Text);

                chkUseCitoFromAppSR.Checked = it.IsCitoFromStandardReference ?? false;

                var appStdRef = new AppStandardReferenceItemCollection();
                appStdRef.Query.Where(appStdRef.Query.StandardReferenceID == AppEnum.StandardReference.CitoPercentage);
                appStdRef.LoadAll();
                RadGrid1.DataSource = appStdRef;
                RadGrid1.DataBind();

                trCitoRef.Visible = chkUseCitoFromAppSR.Checked;
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            var tarifftype = new AppStandardReferenceItemCollection();
            tarifftype.Query.Where(tarifftype.Query.StandardReferenceID == AppEnum.StandardReference.TariffType,
                                   tarifftype.Query.IsActive == true);
            tarifftype.LoadAll();

            var clss = new ClassCollection();
            clss.LoadAll();

            foreach (var tt in tarifftype)
            {
                foreach (var cls in clss)
                {
                    var qtariff = new ItemTariffQuery();
                    qtariff.Where(qtariff.ItemID == txtItemID.Text, qtariff.StartingDate <= DateTime.Now.Date,
                                  qtariff.SRTariffType == tt.ItemID, qtariff.ClassID == cls.ClassID);
                    qtariff.OrderBy(qtariff.StartingDate.Descending);
                    qtariff.Select(qtariff.StartingDate);
                    qtariff.es.Top = 1;
                    var dtb = qtariff.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        var date = Convert.ToDateTime(dtb.Rows[0]["StartingDate"]);

                        bool globallAllowDiscount = false;
                        bool globalAllowVariable = false;
                        
                        foreach (GridDataItem dataItem in grdItemTariffComp.MasterTableView.Items)
                        {
                            bool isAllowDiscount = ((CheckBox)dataItem.FindControl("chkIsAllowDiscount")).Checked;
                            bool isAllowVariable = ((CheckBox)dataItem.FindControl("chkIsAllowVariable")).Checked;

                            if (isAllowDiscount)
                                globallAllowDiscount = true;
                            if (isAllowVariable)
                                globalAllowVariable = true;

                            var compColl = new ItemTariffComponentCollection();
                            compColl.Query.Where(compColl.Query.ItemID == txtItemID.Text,
                                                 compColl.Query.TariffComponentID == dataItem["TariffComponentID"].Text,
                                                 compColl.Query.StartingDate == date,
                                                 compColl.Query.SRTariffType == tt.ItemID);
                            compColl.LoadAll();
                            foreach (var c in compColl)
                            {
                                c.IsAllowDiscount = isAllowDiscount;
                                c.IsAllowVariable = isAllowVariable;
                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                c.LastUpdateDateTime = DateTime.Now;
                            }
                            compColl.Save();

                            
                        }

                        var tariffColl = new ItemTariffCollection();
                        tariffColl.Query.Where(tariffColl.Query.ItemID == txtItemID.Text,
                                               tariffColl.Query.StartingDate == date,
                                               tariffColl.Query.SRTariffType == tt.ItemID);
                        tariffColl.LoadAll();
                        foreach (var t in tariffColl)
                        {
                            t.IsAllowCito = chkIsAllowCito.Checked;
                            if (chkIsAllowCito.Checked)
                            {
                                t.IsCitoInPercent = chkIsCitoInPercent.Checked;
                                t.CitoValue = string.IsNullOrEmpty(txtCitoValue.Text) ? 0 : Convert.ToDecimal(txtCitoValue.Value);
                                t.IsCitoFromStandardReference = chkUseCitoFromAppSR.Checked;
                            }
                            else
                            {
                                t.IsCitoInPercent = false;
                                t.CitoValue = 0;
                                t.IsCitoFromStandardReference = false;
                            }
                            t.IsAllowDiscount = globallAllowDiscount;
                            t.IsAllowVariable = globalAllowVariable;
                            t.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            t.LastUpdateDateTime = DateTime.Now;
                        }
                        tariffColl.Save();
                    }
                }
            }

            return true;
        }


        protected void chkUseCitoFromAppSR_CheckedChanged(object sender, EventArgs e)
        {
            txtCitoValue.Value = 0;
            chkIsCitoInPercent.Checked = false;
            trCitoRef.Visible = chkUseCitoFromAppSR.Checked;
        }

        #region Grid Event

        private void PopulateTariffComponent(string itemId)
        {
            var query = new ItemTariffComponentQuery("a");
            var tcq = new TariffComponentQuery("b");
            
            query.InnerJoin(tcq).On(query.TariffComponentID == tcq.TariffComponentID);
            query.Where(query.ItemID == itemId, query.StartingDate.Date() <= DateTime.Now.Date,
                        query.SRTariffType == AppSession.Parameter.DefaultTariffType);
            query.Select(
                query.ItemID, 
                query.TariffComponentID, 
                tcq.TariffComponentName, 
                @"<CAST(0 AS BIT) AS IsAllowDiscount>",
                @"<CAST(0 AS BIT) AS IsAllowVariable>"
                );
            query.es.Distinct = true;
            query.OrderBy(query.TariffComponentID.Ascending);
            DataTable dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                var q = new ItemTariffComponentQuery();
                q.Where(q.ItemID == row["ItemID"].ToString(), q.TariffComponentID == row["TariffComponentID"].ToString(),
                        q.StartingDate.Date() <= DateTime.Now.Date, q.SRTariffType == AppSession.Parameter.DefaultTariffType);
                q.OrderBy(q.StartingDate.Date().Descending);
                q.es.Top = 1;

                var v = new ItemTariffComponent();
                v.Load(q);

                row["IsAllowDiscount"] = v.IsAllowDiscount;
                row["IsAllowVariable"] = v.IsAllowVariable;
            }

            dtb.AcceptChanges();

            grdItemTariffComp.DataSource = dtb;
            grdItemTariffComp.Rebind();
        }

        #endregion

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}
