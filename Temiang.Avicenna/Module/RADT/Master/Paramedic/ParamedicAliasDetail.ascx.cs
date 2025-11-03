using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Newtonsoft.Json;
using System.Data;
using Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PractitionerSearchResponse;
using RestSharp;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicAliasDetail : BaseUserControl
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
            cboServiceUnitAliasID.Visible = true;
            rfvItemID.ControlToValidate = "cboServiceUnitAliasID";

            StandardReference.InitializeIncludeSpace(cboBridgingType, AppEnum.StandardReference.BridgingType);
            pnlBpjs.Visible = false;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBridgingType.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicBridgingMetadata.ColumnNames.SRBridgingType);
            cboBridgingType_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboBridgingType.SelectedValue, string.Empty));
            if (pnlBpjs.Visible)
            {
                cboSpecialistic.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicBridgingMetadata.ColumnNames.SpecialisticID);
                rblJenis.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicBridgingMetadata.ColumnNames.DutyType);
            }

            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var poli = svc.GetDokterDpjp(rblJenis.SelectedValue == "2" ? Common.BPJS.VClaim.Enum.JenisPelayanan.Jalan : Common.BPJS.VClaim.Enum.JenisPelayanan.Inap, DateTime.Now.Date, cboSpecialistic.SelectedValue);
                if (poli.MetaData.IsValid)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var item in poli.Response.List)
                    {
                        cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", item.Kode, item.Nama), item.Kode));
                    }
                }
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.Inhealth.ToString() && Common.Helper.IsInhealthIntegration) StandardReference.InitializeIncludeSpace(cboServiceUnitAliasID, AppEnum.StandardReference.InhealthParamedic);
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.ANTROL.ToString() && Common.Helper.IsBpjsAntrolIntegration)
            {
                var svc = new Common.BPJS.Antrian.Service();
                var poli = svc.GetReferensiDokter();
                if (!poli.Metadata.IsAntrolValid) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", poli.Metadata.Code, poli.Metadata.Message));
                foreach (var item in poli.Response.List)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", item.Kodedokter, item.Namadokter), item.Kodedokter.ToString()));
                }
            }
            //else if (cboBridgingType.SelectedValue.ToLower() == AppEnum.BridgingType.SATUSEHAT.ToString().ToLower())
            else if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID))
            {

                cboSsBridgingID.Visible = true;
                cboServiceUnitAliasID.Visible = false;
                rfvItemID.ControlToValidate = "cboSsBridgingID";

                cboSsBridgingID.DataSource = SatuSehatPractitioner();
                cboSsBridgingID.DataBind();
            }
            if (cboSsBridgingID.Items.Any(c => c.Value == (String)DataBinder.Eval(DataItem, ParamedicBridgingMetadata.ColumnNames.BridgingID)))
            {
                cboSsBridgingID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicBridgingMetadata.ColumnNames.BridgingID);
            }
            txtServiceUnitAliasName.Text = (String)DataBinder.Eval(DataItem, ParamedicBridgingMetadata.ColumnNames.BridgingName);
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ParamedicBridgingMetadata.ColumnNames.IsActive));
        }

        private DataTable SatuSehatPractitioner()
        {
            var util = new Bridging.SatuSehat.Utils();
            var token = string.Empty;

            var dtb = new DataTable();
            dtb.Columns.Add("ID", typeof(string));
            dtb.Columns.Add("Name", typeof(string));
            dtb.Columns.Add("DOB", typeof(string));
            dtb.Columns.Add("Address", typeof(string));

            // NIK
            var nik = ((RadTextBox)Helper.FindControlRecursive(this.Page, "txtSsn1")).Text;
            if (!string.IsNullOrEmpty(nik))
            {
                var respNik = util.RestClientGet(String.Concat("Practitioner?identifier=https://fhir.kemkes.go.id/id/nik|", nik), string.Empty, ref token);
                PopulateDtbSatuSehatPractioner(dtb, respNik);
                if (dtb.Rows.Count > 0) return dtb;
            }

            // Saerch by Name
            var parNameSearch = ParamedicNameJustName();

            // Male
            var response = util.RestClientGet(String.Concat("Practitioner?name=", parNameSearch, "&gender=male&birthdate=1910"), string.Empty, ref token);
            PopulateDtbSatuSehatPractioner(dtb, response);

            //Female
            response = util.RestClientGet(String.Concat("Practitioner?name=", parNameSearch, "&gender=female&birthdate=1910"), string.Empty, ref token);
            PopulateDtbSatuSehatPractioner(dtb, response);
            return dtb;
        }

        private string ParamedicNameJustName()
        {
            // ex. dr. Dimas Septiar, Sp.PD, ss.
            var parNameSearch = ((RadTextBox)Helper.FindControlRecursive(this.Page, "txtParamedicName")).Text;
            if (string.IsNullOrWhiteSpace(parNameSearch)) return null;

            // Clear title
            var names = parNameSearch.Split(',');
            parNameSearch = string.Empty;
            foreach (var name in names)
            {
                if (name.Contains("."))
                {
                    var subNames = name.Split(' ');
                    foreach (var subName in subNames)
                    {
                        if (subName.Contains(".")) continue;
                        parNameSearch = string.Concat(parNameSearch, " ", subName);
                    }
                    parNameSearch = parNameSearch.Trim();
                }
                else
                    parNameSearch = string.Concat(parNameSearch, " ", name);
            }

            parNameSearch = parNameSearch.Trim();
            return parNameSearch;
        }
        private static void PopulateDtbSatuSehatPractioner(DataTable dtb, RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Created || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var searchResponse = JsonConvert.DeserializeObject<Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PractitionerSearchResponse.Root>(response.Content);
                if (searchResponse.Total > 0)
                {
                    foreach (var item in searchResponse.Entry)
                    {
                        if (item.Resource.Address != null && item.Resource.Address.Count > 0)
                        {
                            var newRow = dtb.NewRow();
                            newRow["ID"] = item.Resource.Id;
                            newRow["Name"] = item.Resource.Name[0].Text;
                            newRow["DOB"] = item.Resource.BirthDate;

                            if (item.Resource.Address != null && item.Resource.Address.Count > 0)
                            {
                                if (item.Resource.Address[0].Line != null && item.Resource.Address[0].Line.Count > 0)
                                    newRow["Address"] = string.Format("{0}", item.Resource.Address[0].Line[0]);

                                newRow["Address"] = string.Format("{0} {1}", newRow["Address"], item.Resource.Address[0].City).Trim();
                            }
                            dtb.Rows.Add(newRow);
                        }
                        else
                        {
                            var newRow = dtb.NewRow();
                            newRow["ID"] = item.Resource.Id;
                            newRow["Name"] = item.Resource.Name[0].Text;
                            dtb.Rows.Add(newRow);
                        }
                    }
                }
            }
        }



        protected void cboBridgingType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSsBridgingID.Visible = false;
            cboServiceUnitAliasID.Visible = true;
            rfvItemID.ControlToValidate = "cboServiceUnitAliasID";

            cboServiceUnitAliasID.Items.Clear();
            cboServiceUnitAliasID.SelectedValue = string.Empty;
            cboServiceUnitAliasID.Text = string.Empty;

            if (e.Value == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
                pnlBpjs.Visible = true;

                var svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.GetSpesialistik();
                if (response.MetaData.IsValid)
                {
                    cboSpecialistic.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var item in response.Response.List)
                    {
                        cboSpecialistic.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                    }
                }
            }
            else if (e.Value == AppEnum.BridgingType.Inhealth.ToString() && Common.Helper.IsInhealthIntegration)
            {
                pnlBpjs.Visible = false;

                StandardReference.InitializeIncludeSpace(cboServiceUnitAliasID, AppEnum.StandardReference.InhealthParamedic);
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.LINK_LIS.ToString())
            {
                pnlBpjs.Visible = false;

                var svc = new Common.LinkLis.Service();
                var poli = svc.GetDokter();
                cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in poli.Data)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(item.Nama, "DR-" + item.IdDokter));
                }

                svc = new Common.LinkLis.Service();
                var pk = svc.GetDokterPK();
                foreach (var item in pk.Data)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(item.Nama, "PK-" + item.IdDokterpk));
                }

                svc = new Common.LinkLis.Service();
                var an = svc.GetAnalis();
                foreach (var item in an.Data)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(item.Nama, "AN-" + item.IdAnalis));
                }
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.ANTROL.ToString() && Common.Helper.IsBpjsAntrolIntegration)
            {
                var svc = new Common.BPJS.Antrian.Service();
                var poli = svc.GetReferensiDokter();
                if (!poli.Metadata.IsAntrolValid) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", poli.Metadata.Code, poli.Metadata.Message));
                foreach (var item in poli.Response.List)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", item.Kodedokter, item.Namadokter), item.Kodedokter.ToString()));
                }
            }
            else if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
            {
                cboSsBridgingID.Visible = true;
                cboServiceUnitAliasID.Visible = false;
                rfvItemID.ControlToValidate = "cboSsBridgingID";
                cboSsBridgingID.DataSource = SatuSehatPractitioner();
                cboSsBridgingID.DataBind();
            }
        }


        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicBridgingCollection)Session["collParamedicBridging"];

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
            get
            {
                if (cboBridgingType.SelectedValue.ToLower() == AppParameter.GetParameterValue(AppParameter.ParameterItem.SatuSehatBridgingTypeID).ToLower())
                    return cboSsBridgingID.SelectedValue;
                else
                    return string.IsNullOrEmpty(cboServiceUnitAliasID.SelectedValue) ? cboServiceUnitAliasID.Text : cboServiceUnitAliasID.SelectedValue;
            }
        }

        public String BridgingName
        {
            get { return txtServiceUnitAliasName.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public String SpesialisticID
        {
            get { return cboSpecialistic.SelectedValue; }
        }

        public String SpesialisticName
        {
            get { return cboSpecialistic.Text; }
        }

        public String DutyType
        {
            get { return rblJenis.SelectedValue; }
        }

        protected void cboSpecialistic_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                cboServiceUnitAliasID.Items.Clear();
                cboServiceUnitAliasID.SelectedValue = string.Empty;
                cboServiceUnitAliasID.Text = string.Empty;
                return;
            }
            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
                cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                var svc = new Common.BPJS.VClaim.v11.Service();
                var poli = svc.GetDokterDpjp(rblJenis.SelectedValue == "2" ? Common.BPJS.VClaim.Enum.JenisPelayanan.Jalan : Common.BPJS.VClaim.Enum.JenisPelayanan.Inap, DateTime.Now.Date, cboSpecialistic.SelectedValue);
                if (!poli.MetaData.IsValid) cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", poli.MetaData.Code, poli.MetaData.Message), string.Empty));
                else
                {
                    foreach (var item in poli.Response.List)
                    {
                        cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", item.Kode, item.Nama), item.Kode));
                    }
                }
            }
        }

        protected void rblJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboServiceUnitAliasID.Items.Clear();
            cboServiceUnitAliasID.SelectedValue = string.Empty;
            cboServiceUnitAliasID.Text = string.Empty;
            cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
                var svc = new Common.BPJS.VClaim.v11.Service();
                var poli = svc.GetDokterDpjp(rblJenis.SelectedValue == "2" ? Common.BPJS.VClaim.Enum.JenisPelayanan.Jalan : Common.BPJS.VClaim.Enum.JenisPelayanan.Inap, DateTime.Now.Date, cboSpecialistic.SelectedValue);
                if (!poli.MetaData.IsValid) cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", poli.MetaData.Code, poli.MetaData.Message), string.Empty));
                else
                {
                    foreach (var item in poli.Response.List)
                    {
                        cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Format("{0} - {1}", item.Kode, item.Nama), item.Kode));
                    }
                }
            }
        }
    }
}