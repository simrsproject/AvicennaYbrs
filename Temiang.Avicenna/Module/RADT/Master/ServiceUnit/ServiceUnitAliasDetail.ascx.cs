using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ServiceUnitAliasDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboBridgingType, AppEnum.StandardReference.BridgingType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBridgingType.SelectedValue = (String)DataBinder.Eval(DataItem, ServiceUnitBridgingMetadata.ColumnNames.SRBridgingType);

            //if (cboBridgingType.SelectedValue == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            cboServiceUnitAliasID_ItemsRequested(null, new RadComboBoxItemsRequestedEventArgs() { Text = (String)DataBinder.Eval(DataItem, ServiceUnitBridgingMetadata.ColumnNames.BridgingID) });
            cboServiceUnitAliasID.SelectedValue = (String)DataBinder.Eval(DataItem, ServiceUnitBridgingMetadata.ColumnNames.BridgingID);

            txtServiceUnitAliasName.Text = (String)DataBinder.Eval(DataItem, ServiceUnitBridgingMetadata.ColumnNames.BridgingName);
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ServiceUnitBridgingMetadata.ColumnNames.IsActive));
        }

        protected void cboServiceUnitAliasID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            cboServiceUnitAliasID.DataSource = null;
            cboServiceUnitAliasID.DataBind();
            cboServiceUnitAliasID.Items.Clear();
            cboServiceUnitAliasID.SelectedValue = string.Empty;

            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
                if (e.Text.Length < 3)
                {
                    cboServiceUnitAliasID.DataSource = null;
                    cboServiceUnitAliasID.DataBind();
                    cboServiceUnitAliasID.Items.Clear();
                    cboServiceUnitAliasID.SelectedValue = string.Empty;
                    return;
                }

                var svc = new Common.BPJS.VClaim.v11.Service();
                var poli = svc.GetPoli(e.Text);
                if (!poli.MetaData.IsValid) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", poli.MetaData.Code, poli.MetaData.Message));
                cboServiceUnitAliasID.DataSource = poli.Response.Poli;
                cboServiceUnitAliasID.DataBind();
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.Inhealth.ToString() && Common.Helper.IsInhealthIntegration)
            {
                if (e.Text.Length < 3)
                {
                    cboServiceUnitAliasID.DataSource = null;
                    cboServiceUnitAliasID.DataBind();
                    cboServiceUnitAliasID.Items.Clear();
                    cboServiceUnitAliasID.SelectedValue = string.Empty;
                    return;
                }

                string searchTextContain = string.Format("%{0}%", e.Text);

                var std = new AppStandardReferenceItemQuery();
                std.Where(
                    std.StandardReferenceID == AppEnum.StandardReference.InhealthServiceUnit,
                    std.Or(
                        std.ItemID.Like(searchTextContain),
                        std.ItemName.Like(searchTextContain)
                    ));
                cboServiceUnitAliasID.DataSource = std.LoadDataTable();
                cboServiceUnitAliasID.DataBind();
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString())
            {
                var svc = new Common.LinkLis.Service();
                var poli = svc.GetRuangan();
                cboServiceUnitAliasID.DataSource = poli.Data;
                cboServiceUnitAliasID.DataBind();
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.ANTROL.ToString() && Common.Helper.IsBpjsAntrolIntegration)
            {
                var svc = new Common.BPJS.Antrian.Service();
                var poli = svc.GetReferensiPoli();
                if (!poli.Metadata.IsAntrolValid) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", poli.Metadata.Code, poli.Metadata.Message));
                cboServiceUnitAliasID.DataSource = poli.Response.List;
                cboServiceUnitAliasID.DataBind();
            }
            else if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
            {
                if (e.Text.Length < 3)
                {
                    cboServiceUnitAliasID.DataSource = null;
                    cboServiceUnitAliasID.DataBind();
                    cboServiceUnitAliasID.Items.Clear();
                    cboServiceUnitAliasID.SelectedValue = string.Empty;
                    return;
                }

                var util = new Bridging.SatuSehat.Utils();
                var token = string.Empty;
                cboServiceUnitAliasID.Items.Clear();
                var response = util.RestClientGet(String.Concat("Location?name==", e.Text), string.Empty, ref token);
                if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var searchResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location.LocationSearchResponse>(response.Content);
                    if (searchResponse.Total > 0)
                    {
                        foreach (var item in searchResponse.Entry)
                        {
                            cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(item.Resource.Name, item.Resource.Id));
                        }
                    }
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem("Not found, create a new Bridging ID when saving", "CREATE"));
                }
            }
        }

        protected void cboServiceUnitAliasID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
                e.Item.Text = ((Common.BPJS.VClaim.v11.Poli.Poli2)e.Item.DataItem).Kode + " - " + ((Common.BPJS.VClaim.v11.Poli.Poli2)e.Item.DataItem).Nama;
                e.Item.Value = ((Common.BPJS.VClaim.v11.Poli.Poli2)e.Item.DataItem).Kode;
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.Inhealth.ToString() && Common.Helper.IsInhealthIntegration)
            {
                e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
                e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString())
            {
                e.Item.Text = ((Common.LinkLis.Reference.Ruangan.Datum)e.Item.DataItem).KodeRuangan + " - " + ((Common.LinkLis.Reference.Ruangan.Datum)e.Item.DataItem).Nama;
                e.Item.Value = ((Common.LinkLis.Reference.Ruangan.Datum)e.Item.DataItem).KodeRuangan;
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.ANTROL.ToString() && Common.Helper.IsBpjsAntrolIntegration)
            {
                e.Item.Text = ((Common.BPJS.Antrian.Referensi.Poli.List)e.Item.DataItem).Nmpoli + " - " + ((Common.BPJS.Antrian.Referensi.Poli.List)e.Item.DataItem).Nmsubspesialis;
                e.Item.Value = ((Common.BPJS.Antrian.Referensi.Poli.List)e.Item.DataItem).Kdpoli + ";" + ((Common.BPJS.Antrian.Referensi.Poli.List)e.Item.DataItem).Kdsubspesialis;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ServiceUnitBridgingCollection)Session["collServiceUnitBridging"];

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

        public String BridgingID
        {
            get { return string.IsNullOrEmpty(cboServiceUnitAliasID.SelectedValue) ? cboServiceUnitAliasID.Text : cboServiceUnitAliasID.SelectedValue; }
        }

        public String BridgingName
        {
            get { return txtServiceUnitAliasName.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        protected void cboBridgingType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
                cboServiceUnitAliasID.Filter = RadComboBoxFilter.None;
            else 
                cboServiceUnitAliasID.Filter = RadComboBoxFilter.Contains;
        }
    }
}