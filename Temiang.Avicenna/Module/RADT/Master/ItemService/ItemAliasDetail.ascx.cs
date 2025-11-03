using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using System.Net;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ItemAliasDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            cboSsBridgingID.Visible = false;
            StandardReference.InitializeIncludeSpace(cboBridgingType, AppEnum.StandardReference.BridgingType);


            if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
            {
                cboSsBridgingID.Visible = true;
                cboServiceUnitAliasID.Visible = false;
                rfvServiceUnitAliasID.ControlToValidate = "cboSsBridgingID";
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;


            cboBridgingType.SelectedValue = (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.SRBridgingType);
            cboBridgingType_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboBridgingType.SelectedValue, string.Empty));
            if (cboServiceUnitAliasID.Items.Any())
            {
                if (cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString()) cboServiceUnitAliasID.SelectedValue = string.Format("{0}|{1}", (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.BridgingGroupID), (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.BridgingID));
                else cboServiceUnitAliasID.SelectedValue = (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.BridgingID);
            }
            else if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
                cboSsBridgingID.Text = (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.BridgingID);
            else
                cboServiceUnitAliasID.Text = (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.BridgingID);

            txtServiceUnitAliasName.Text = (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.BridgingName);
            txtItemIdExternal.Text = (String)DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.ItemIdExternal);
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ItemBridgingMetadata.ColumnNames.IsActive));
        }

        protected void cboBridgingType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSsBridgingID.Visible = false;
            //cboServiceUnitAliasID.Items.Clear();

            //if (e.Value == AppEnum.BridgingType.Inhealth.ToString() && Common.Helper.IsInhealthIntegration)
            //    StandardReference.InitializeIncludeSpace(cboServiceUnitAliasID, AppEnum.StandardReference.InhealthItemService);

            if (e.Value == AppEnum.BridgingType.LINK_LIS.ToString())
            {
                var lpar = new BusinessObject.Interop.LINKLIS.ListParameterQuery("a");
                lpar.es2.Connection.Name = AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME;
                var lpem = new BusinessObject.Interop.LINKLIS.ListPemeriksaanQuery("b");
                lpem.es2.Connection.Name = AppConstant.HIS_INTEROP.LINK_LIS_INTEROP_CONNECTION_NAME;

                lpar.Select(lpar.SelectAll(), lpem.NamaPemeriksaan);
                lpar.InnerJoin(lpem).On(lpar.KodePemeriksaan == lpem.KodePemeriksaan);
                lpar.Where(lpar.KodePemeriksaan != string.Empty);

                var table = lpar.LoadDataTable();
                cboServiceUnitAliasID.Items.Clear();
                cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow group in table.Rows)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0}|{1}", group["NamaPemeriksaan"].ToString(), group["NamaParameter"].ToString()), string.Format("{0}|{1}", group["KodePemeriksaan"].ToString(), group["KodeParameter"].ToString())));
                }
            }
            else if (e.Value.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
            {
                cboSsBridgingID.Visible = true;
                cboServiceUnitAliasID.Visible = false;
                rfvServiceUnitAliasID.ControlToValidate = "cboSsBridgingID";
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.APOTEKONLINE.ToString() && Common.Helper.IsApotekOnlineIntegration)
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DefaultConnectionLimit = Math.Max(ServicePointManager.DefaultConnectionLimit, 100);
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                var cacheKey = "APOL:DPHO_LIST";
                var svc = new Common.BPJS.Apotek.Service();

                Temiang.Avicenna.Common.BPJS.Apotek.Referensi.Dpho.Root dpho = null;
                bool fromCache = false;

                var cached = HttpRuntime.Cache[cacheKey] as Temiang.Avicenna.Common.BPJS.Apotek.Referensi.Dpho.Root;
                if (cached != null)
                {
                    dpho = cached;
                    fromCache = true;
                }
                else
                {
                    try
                    {
                        dpho = Retry(() => svc.GetObatDpho(), attempts: 2, delayMs: 400);
                    }
                    catch (WebException wex)
                    {
                        var http = wex.Response as HttpWebResponse;
                        var code = (int?)(http?.StatusCode) ?? 0;

                        cboServiceUnitAliasID.Items.Clear();

                        if (code == 504 && cached != null)
                        {
                            dpho = cached;
                            fromCache = true;
                        }
                        else
                        {
                            var msg = code == 504 ? "Gateway Timeout (504) — coba lagi." : $"Gagal memuat data ({code}).";
                            var it = new RadComboBoxItem(msg, string.Empty) { Enabled = false };
                            cboServiceUnitAliasID.Items.Add(it);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        cboServiceUnitAliasID.Items.Clear();
                        cboServiceUnitAliasID.Items.Add(new RadComboBoxItem($"Error: {ex.Message}", string.Empty) { Enabled = false });
                        return;
                    }
                }

                if (dpho == null || dpho.MetaData == null || !dpho.MetaData.IsApolValid)
                {
                    cboServiceUnitAliasID.Items.Clear();
                    var it = new RadComboBoxItem(
                        $"Server error (HTTP {dpho?.MetaData?.Code}: {dpho?.MetaData?.Message ?? "Tidak diketahui"})",
                        string.Empty)
                    { Enabled = false };
                    cboServiceUnitAliasID.Items.Add(it);
                    return;
                }

                cboServiceUnitAliasID.Items.Clear();
                var list = dpho.Response != null ? dpho.Response.List : null;

                if (list != null && list.Count > 0)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                    for (int i = 0; i < list.Count; i++)
                    {
                        var obat = list[i];
                        var text = $"{obat.Namaobat} - {obat.Kodeobat}";
                        cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(text, obat.Kodeobat));
                    }
                }
                else
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem("Tidak ada data.", string.Empty) { Enabled = false });
                }

                if (!fromCache)
                {
                    HttpRuntime.Cache.Insert(
                        cacheKey,
                        dpho,
                        null,
                        System.Web.Caching.Cache.NoAbsoluteExpiration,
                        TimeSpan.FromMinutes(10)
                    );
                }

                if (fromCache)
                {
                    cboServiceUnitAliasID.Items.Insert(0,
                        new RadComboBoxItem("ⓘ Menampilkan data cache (server timeout).", string.Empty) { Enabled = false });
                }
            }
            else
            {
                var lis = new AppStandardReferenceItem();
                if (lis.LoadByPrimaryKey(AppEnum.StandardReference.BridgingType.ToString(), e.Value) && !string.IsNullOrEmpty(lis.Note) && lis.Note == "LIS")
                    rfvServiceUnitAliasID.Visible = false;
                cboServiceUnitAliasID.Items.Clear();
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemBridgingCollection)Session["collItemBridging"];

                string itemID = cboServiceUnitAliasID.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.BridgingID.Equals(itemID) && item.SRBridgingType.Equals(cboBridgingType.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Bridging ID : {0} already exist", itemID);
                }
            }
        }

        public String BridgingType
        {
            get { return cboBridgingType.SelectedValue; }
        }

        public String BridgingTypeName
        {
            get { return cboBridgingType.Text; }
        }

        public String BridgingGroupID
        {
            get { return cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString() ? cboServiceUnitAliasID.SelectedValue.Split('|')[0] : string.Empty; }
        }

        public String BridgingGroupName
        {
            get { return cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString() ? cboServiceUnitAliasID.Text.Split('|')[0] : string.Empty; }
        }

        public String BridgingID
        {
            get {
                if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
                    return cboSsBridgingID.Text;
                return string.IsNullOrEmpty(cboServiceUnitAliasID.SelectedValue) ? cboServiceUnitAliasID.Text : cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString() ? cboServiceUnitAliasID.SelectedValue.Split('|')[1] : cboServiceUnitAliasID.SelectedValue; }
        }

        public String BridgingName
        {
            get { return cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString() ? cboServiceUnitAliasID.Text.Split('|')[1] : txtServiceUnitAliasName.Text; }
        }

        public String ItemIdExternal
        {
            get { return txtItemIdExternal.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        protected void cboBridgingGroupID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboServiceUnitAliasID.Items.Clear();
                return;
            }

            var svc = new Common.LinkLis.Service();
            var groups = svc.GetListParameter(e.Value);
            cboServiceUnitAliasID.Items.Clear();
            cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var group in groups.ListParameter)
            {
                cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(group.NamaPemeriksaan, group.Kode));
            }
        }

        //helper apol
        private T Retry<T>(Func<T> action, int attempts = 2, int delayMs = 400)
        {
            Exception last = null;
            for (int i = 0; i < attempts; i++)
            {
                try { return action(); }
                catch (WebException ex) { last = ex; System.Threading.Thread.Sleep(delayMs * (i + 1)); }
                catch (Exception ex) { last = ex; break; }
            }
            throw last ?? new Exception("Unknown error");
        }
    }
}