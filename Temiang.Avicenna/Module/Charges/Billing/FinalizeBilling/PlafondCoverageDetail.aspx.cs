using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Charges.Billing.FinalizeBilling
{
    public partial class PlafondCoverageDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            if (!IsPostBack)
            {
                ButtonCancel.Visible = false;

                var cls = new ClassCollection();
                cls.Query.Where(cls.Query.IsActive == true);
                cls.Query.OrderBy(cls.Query.ClassID.Ascending);
                cls.Query.Load();

                cboClassID.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var c in cls)
                {
                    cboClassID.Items.Add(new Telerik.Web.UI.RadComboBoxItem(c.ClassName, c.ClassID));
                }

                txtCoverageAmount.Value = 0;

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
                txtBpjsSepNo.Text = reg.BpjsSepNo;

                //btnAdd.Enabled = (reg.CoverageClassID != reg.ChargeClassID);

                if (!string.IsNullOrEmpty(reg.BpjsPackageID))
                {
                    var bp = new BpjsPackageQuery();
                    bp.Where(bp.PackageID == reg.BpjsPackageID);
                    cboBpjsPackageID.DataSource = bp.LoadDataTable();
                    cboBpjsPackageID.DataBind();

                    cboBpjsPackageID.SelectedValue = reg.BpjsPackageID;
                }
                else
                {
                    cboBpjsPackageID.Items.Clear();
                    cboBpjsPackageID.Text = string.Empty;
                }

                var bridging = new GuarantorBridging();
                bridging.Query.Where(bridging.Query.GuarantorID == reg.GuarantorID,
                                     bridging.Query.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString(),
                                                                      AppEnum.BridgingType.BPJS_TNI_POLRI_PNS.ToString(),
                                                                      AppEnum.BridgingType.BPJS_PASIEN_UMUM.ToString()));
                if (bridging.Query.Load())
                {
                    txtBpjsCoverageFormula.Value = Convert.ToDouble(reg.BpjsCoverageFormula ?? (bridging.CoverageValue ?? 0));
                    txtMarginValue.Value = Convert.ToDouble(bridging.MarginValue ?? 0);
                }
                else
                {
                    txtBpjsCoverageFormula.Value = Convert.ToDouble(reg.BpjsCoverageFormula ?? 0);
                    txtMarginValue.Value = 0;
                }

                txtBpjsCoverageFormula.MaxValue = 100;

                if (!this.IsUserEditAble)
                {
                    ButtonOk.Visible = false;

                    ButtonCancel.Visible = true;
                    ButtonCancel.Text = "Close";

                    trBtnAdd.Visible = false;
                }
            }
        }

        protected void grdPlafondHistory_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    var item = e.Item as Telerik.Web.UI.GridDataItem;
                    if (item == null) return;

                    var classID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ClassID"]);

                    var cov = new RegistrationCoverageDetail();
                    cov.LoadByPrimaryKey(Request.QueryString["regNo"], classID);
                    cboClassID.SelectedValue = cov.ClassID;
                    txtCoverageAmount.Value = Convert.ToDouble(cov.CoverageAmount);
                    break;
            }
        }

        protected void grdPlafondHistory_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var classID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ClassID"]);

            var cov = new RegistrationCoverageDetail();
            cov.LoadByPrimaryKey(Request.QueryString["regNo"], classID);
            cov.MarkAsDeleted();
            cov.Save();

            grdPlafondHistory.Rebind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboClassID.SelectedValue)) return;
            if (string.IsNullOrEmpty(txtCoverageAmount.Text) || txtCoverageAmount.Value == 0) return;
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);
            if (txtMarginValue.Value > 0 && reg.ChargeClassID != cboClassID.SelectedValue) return;

            //save
            var cov = new RegistrationCoverageDetail();
            if (!cov.LoadByPrimaryKey(Request.QueryString["regNo"], cboClassID.SelectedValue)) cov = new RegistrationCoverageDetail();
            cov.RegistrationNo = Request.QueryString["regNo"];
            cov.ClassID = cboClassID.SelectedValue;
            cov.CoverageAmount = Convert.ToDecimal(txtCoverageAmount.Value);
            cov.CalculatedAmount = 0;
            cov.LastCreateDateTime = DateTime.Now;
            cov.LastCreateUserID = AppSession.UserLogin.UserID;
            cov.Save();

            //rebind and clear
            grdPlafondHistory.Rebind();
            cboClassID.SelectedValue = string.Empty;
            txtCoverageAmount.Value = 0;
        }

        protected void grdPlafondHistory_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var cov = new RegistrationCoverageDetailQuery("a");
            var cls = new ClassQuery("b");
            var asr = new AppStandardReferenceItemQuery("c");

            cov.Select(cov, cls.ClassName);
            cov.InnerJoin(cls).On(cov.ClassID == cls.ClassID);
            cov.InnerJoin(asr).On(cls.SRClassRL == asr.ItemID && asr.StandardReferenceID == AppEnum.StandardReference.ClassRL.ToString());
            cov.Where(cov.RegistrationNo == Request.QueryString["regNo"]);
            cov.OrderBy(asr.Note.Cast(Temiang.Dal.DynamicQuery.esCastType.Int32).Ascending, cls.ClassID.Ascending);

            grdPlafondHistory.DataSource = cov.LoadDataTable();
        }

        public override bool OnButtonOkClicked()
        {
            double formula = (txtBpjsCoverageFormula.Value ?? 0) / 100; // AppSession.Parameter.BpjsCoverageFormula;
            double margin = (txtMarginValue.Value ?? 0) / 100.00;
            //var vips = new string[2] { "ClassRL-000", "ClassRL-001" };

            var vips = new ClassCollection();
            vips.Query.Where(vips.Query.SRClassRL.In("ClassRL-000", "ClassRL-001"));
            vips.Query.Load();

            var cov = new RegistrationCoverageDetailQuery("a");
            var cls = new ClassQuery("b");
            var asr = new AppStandardReferenceItemQuery("c");

            cov.Select(cov, cls.ClassName, asr.Note, cls.SRClassRL);
            cov.InnerJoin(cls).On(cov.ClassID == cls.ClassID);
            cov.InnerJoin(asr).On(cls.SRClassRL == asr.ItemID && asr.StandardReferenceID == AppEnum.StandardReference.ClassRL.ToString());
            cov.Where(cov.RegistrationNo == Request.QueryString["regNo"]);
            cov.OrderBy(asr.Note.Cast(Temiang.Dal.DynamicQuery.esCastType.Int32).Ascending, cls.ClassID.Ascending);
            var tbl = cov.LoadDataTable();

            //if (tbl.Rows.Count == 0) return true;

            using (var trans = new Temiang.Dal.Interfaces.esTransactionScope())
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                if (tbl.Rows.Count > 0)
                {
                    var row = tbl.AsEnumerable().Where(t => t.Field<string>("ClassID") == reg.CoverageClassID).SingleOrDefault();
                    if (txtMarginValue.Value == 0)
                        reg.PlavonAmount = row == null ? 0 : Convert.ToDecimal(row["CoverageAmount"]);
                    else
                        reg.PlavonAmount = 0;
                }
                else
                    reg.PlavonAmount = 0;

                reg.BpjsCoverageFormula = Convert.ToDecimal(txtBpjsCoverageFormula.Value);
                reg.BpjsPackageID = cboBpjsPackageID.SelectedValue;
                reg.BpjsSepNo = txtBpjsSepNo.Text;
                reg.LastUpdateDateTime = DateTime.Now;
                reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                reg.Save();

                if (tbl.Rows.Count > 0)
                {
                    var entity = new RegistrationCoverageDetail();
                    DataRow row1, row2, row3;

                    if (txtMarginValue.Value == 0)
                    {
                        switch (tbl.Rows.Count)
                        {
                            case 1:
                                row1 = tbl.Rows[0];
                                entity.LoadByPrimaryKey(Request.QueryString["regNo"], row1["ClassID"].ToString());
                                if (entity.Query.Load())
                                {
                                    if (vips.Any(v => v.ClassID.Contains(reg.ChargeClassID)))
                                        entity.CalculatedAmount = Convert.ToDecimal(formula) * (entity.CoverageAmount ?? 0);
                                    else
                                        entity.CalculatedAmount = 0;
                                    entity.Save();
                                }
                                break;
                            case 2:
                                row1 = tbl.Rows[0];
                                entity.LoadByPrimaryKey(Request.QueryString["regNo"], row1["ClassID"].ToString());
                                if (entity.Query.Load())
                                {
                                    if (vips.Any(v => v.ClassID.Contains(reg.ChargeClassID)))
                                        entity.CalculatedAmount = Convert.ToDecimal(formula) * (entity.CoverageAmount ?? 0);
                                    else
                                        entity.CalculatedAmount = 0;
                                    entity.Save();
                                }

                                row2 = tbl.Rows[1];
                                entity = new RegistrationCoverageDetail();
                                entity.LoadByPrimaryKey(Request.QueryString["regNo"], row2["ClassID"].ToString());
                                if (entity.Query.Load())
                                {
                                    entity.CalculatedAmount = Convert.ToDecimal(row1["CoverageAmount"]) - (entity.CoverageAmount ?? 0);
                                    entity.Save();
                                }
                                break;
                            case 3:
                                row1 = tbl.Rows[0];
                                entity.LoadByPrimaryKey(Request.QueryString["regNo"], row1["ClassID"].ToString());
                                if (entity.Query.Load())
                                {
                                    if (vips.Any(v => v.ClassID.Contains(reg.ChargeClassID)))
                                        entity.CalculatedAmount = Convert.ToDecimal(formula) * (entity.CoverageAmount ?? 0);
                                    else
                                        entity.CalculatedAmount = 0;
                                    entity.Save();
                                }

                                row2 = tbl.Rows[1];
                                entity = new RegistrationCoverageDetail();
                                entity.LoadByPrimaryKey(Request.QueryString["regNo"], row2["ClassID"].ToString());
                                if (entity.Query.Load())
                                {
                                    entity.CalculatedAmount = Convert.ToDecimal(row1["CoverageAmount"]) - (entity.CoverageAmount ?? 0);
                                    entity.Save();
                                }

                                row3 = tbl.Rows[2];
                                entity = new RegistrationCoverageDetail();
                                entity.LoadByPrimaryKey(Request.QueryString["regNo"], row3["ClassID"].ToString());
                                if (entity.Query.Load())
                                {
                                    entity.CalculatedAmount = Convert.ToDecimal(row2["CoverageAmount"]) - (entity.CoverageAmount ?? 0);
                                    entity.Save();
                                }
                                break;
                        }
                    }
                    else
                    {
                        row1 = tbl.Rows[0];
                        entity.LoadByPrimaryKey(Request.QueryString["regNo"], row1["ClassID"].ToString());
                        if (entity.Query.Load())
                        {
                            if (vips.Any(v => v.ClassID.Contains(reg.ChargeClassID)))
                                entity.CalculatedAmount = (entity.CoverageAmount ?? 0) +
                                                          (Convert.ToDecimal(formula) * (entity.CoverageAmount ?? 0));
                            else
                                entity.CalculatedAmount = (entity.CoverageAmount ?? 0) +
                                                          (Convert.ToDecimal(margin) * (entity.CoverageAmount ?? 0));
                            entity.Save();
                        }
                    }
                }

                trans.Complete();
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void cboBpjsPackageID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PackageName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PackageID"].ToString();
        }

        protected void cboBpjsPackageID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new BpjsPackageQuery();
            query.es.Top = 15;
            query.Where
                (
                    query.PackageName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.PackageName.Ascending);

            cboBpjsPackageID.DataSource = query.LoadDataTable();
            cboBpjsPackageID.DataBind();
        }

        protected void cboClassID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cboBpjsPackageID.SelectedValue))
            {
                var c = new Class();
                if (c.LoadByPrimaryKey(e.Value))
                {
                    var classRl = c.SRClassRL;
                    if (classRl == "ClassRL-000" || classRl == "ClassRL-001")
                        classRl = "ClassRL-002";

                    var q = new BpjsPackageTariffQuery();
                    q.Where(q.PackageID == cboBpjsPackageID.SelectedValue, q.ClassID == classRl,
                            q.StartingDate.Date() <= (new DateTime()).NowAtSqlServer().Date);
                    q.OrderBy(q.StartingDate.Descending);
                    q.es.Top = 1;
                    var bp = new BpjsPackageTariff();
                    try
                    {
                        bp.Load(q);
                        txtCoverageAmount.Value = Convert.ToDouble(bp.Price);
                    }
                    catch (Exception)
                    {
                        txtCoverageAmount.Value = 0;
                    }
                }
                else
                    txtCoverageAmount.Value = 0;
            }
            else
                txtCoverageAmount.Value = 0;
        }
    }
}
