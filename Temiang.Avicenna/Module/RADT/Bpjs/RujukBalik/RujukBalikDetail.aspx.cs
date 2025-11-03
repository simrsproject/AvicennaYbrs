using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class RujukBalikDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RujukBalikSearch.aspx";
            UrlPageList = "RujukBalikList.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.BpjsRujukBalik;

            if (!IsPostBack)
            {
                cboProgramPrb.Items.Clear();

                var svc = new Common.BPJS.VClaim.v11.Service();
                var response = svc.GetDiagnosaPrb();
                if (response.MetaData.IsValid)
                {
                    if (response.Response.List != null && response.Response.List.Any())
                    {
                        cboProgramPrb.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                        foreach (var item in response.Response.List)
                        {
                            cboProgramPrb.Items.Add(new RadComboBoxItem(item.Nama, item.Kode));
                        }
                    }
                }
            }
        }

        protected void cboDpjp_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //cboDpjpSep.DataSource = null;
            //cboDpjpSep.DataBind();

            var cbo = (o as RadComboBox);

            cbo.Items.Clear();
            cbo.SelectedValue = string.Empty;

            var sub = new ParamedicBridgingQuery("a");
            var su = new ParamedicQuery("b");

            sub.Select(sub.BridgingID, "<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END AS ParamedicName>");
            sub.InnerJoin(su).On(sub.ParamedicID == su.ParamedicID && sub.SRBridgingType.In(AppEnum.BridgingType.BPJS.ToString()));
            //sub.Where(string.Format("<CASE WHEN a.BridgingName = '' THEN b.ParamedicName ELSE a.BridgingName END LIKE '%{0}%'>", e.Text));
            sub.Where(sub.BridgingID == e.Text);

            cbo.DataSource = sub.LoadDataTable();
            cbo.DataBind();
        }

        protected void cboDpjp_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = (string.IsNullOrEmpty(((DataRowView)e.Item.DataItem)["BridgingID"].ToString()) ? string.Empty : ((DataRowView)e.Item.DataItem)["BridgingID"].ToString() + " - ") + ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BridgingID"].ToString();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (sourceControl is RadTextBox)
            {
                if (((RadTextBox)sourceControl).ID == txtNoSep.ID)
                {
                    var sep = new BpjsSEP();
                    sep.Query.Where(sep.Query.NoSEP == txtNoSep.Text);
                    if (sep.Query.Load())
                    {
                        txtNoPeserta.Text = sep.NomorKartu;
                        txtNamaPeserta.Text = sep.NamaPasien;
                    }
                }
            }
        }

        //protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        //{
        //    ajax.AddAjaxSetting(btnAddObat, grdList);
        //}

        protected override void OnMenuNewClick()
        {
            txtNoSRB.Text = string.Empty;
            txtNoSep.Text = string.Empty;
            txtNoPeserta.Text = string.Empty;
            txtNamaPeserta.Text = string.Empty;
            txtAlamat.Text = string.Empty;
            txtEmail.Text = string.Empty;

            cboDpjp.DataSource = null;
            cboDpjp.DataBind();
            cboDpjp.Items.Clear();
            cboDpjp.SelectedValue = string.Empty;
            cboDpjp.Text = string.Empty;

            //cboProgramPrb.DataSource = null;
            //cboProgramPrb.DataBind();
            //cboProgramPrb.Items.Clear();

            cboProgramPrb.SelectedValue = string.Empty;
            cboProgramPrb.Text = string.Empty;

            txtKeterangan.Text = string.Empty;
            txtSaran.Text = string.Empty;

            cboKodeObat.DataSource = null;
            cboKodeObat.DataBind();
            cboKodeObat.Items.Clear();
            cboKodeObat.SelectedValue = string.Empty;
            cboKodeObat.Text = string.Empty;

            txtJmlObat.Value = 0;
            txtSigna1.Text = string.Empty;
            txtSigna2.Text = string.Empty;

            Obats = new List<Common.BPJS.VClaim.v11.RujukanBalik.Obat>();
            grdList.Rebind();
        }

        protected override void OnMenuEditClick()
        {
            txtNoSep.ReadOnly = true;
            btnCariPasien.Enabled = false;
            //cboProgramPrb.Enabled = false;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.Insert(new Common.BPJS.VClaim.v11.RujukanBalik.Insert.Request.Root()
            {
                Request = new Common.BPJS.VClaim.v11.RujukanBalik.Insert.Request.TRequest()
                {
                    TPrb = new Common.BPJS.VClaim.v11.RujukanBalik.Insert.Request.TPrb()
                    {
                        NoSep = txtNoSep.Text,
                        NoKartu = txtNoPeserta.Text,
                        Alamat = txtAlamat.Text,
                        Email = txtEmail.Text,
                        ProgramPRB = cboProgramPrb.SelectedValue,
                        KodeDPJP = cboDpjp.SelectedValue,
                        Keterangan = txtKeterangan.Text,
                        Saran = txtSaran.Text,
                        User = ConfigurationManager.AppSettings["BPJSConsumerID"],
                        Obat = Obats
                    }
                }
            });
            if (response.MetaData.IsValid)
            {
                txtNoSRB.Text = response.Response.NoSRB;
            }
            else
            {
                args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                args.IsCancel = true;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.Update(new Common.BPJS.VClaim.v11.RujukanBalik.Update.Request.Root()
            {
                Request = new Common.BPJS.VClaim.v11.RujukanBalik.Update.Request.TRequest()
                {
                    TPrb = new Common.BPJS.VClaim.v11.RujukanBalik.Update.Request.TPrb()
                    {
                        NoSrb = txtNoSRB.Text,
                        NoSep = txtNoSep.Text,
                        Alamat = txtAlamat.Text,
                        Email = txtEmail.Text,
                        KodeDPJP = cboDpjp.SelectedValue,
                        Keterangan = txtKeterangan.Text,
                        Saran = txtSaran.Text,
                        User = ConfigurationManager.AppSettings["BPJSConsumerID"],
                        Obat = Obats
                    }
                }
            });
            if (response.MetaData.IsValid)
            {

            }
            else
            {
                args.MessageText = string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message);
                args.IsCancel = true;
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            if (parameters.Length > 0)
            {
                string param = parameters[0];
                if (!parameters[0].Equals(string.Empty))
                {
                    var noSrb = param.Split('|')[0];
                    var noSep = param.Split('|')[1];

                    var svc = new Common.BPJS.VClaim.v11.Service();
                    var response = svc.GetPrbByNo(noSrb, noSep);
                    if (response.MetaData.IsValid)
                    {
                        var srb = response.Response.Prb;
                        txtNoSRB.Text = srb.NoSRB;
                        txtNoSep.Text = srb.NoSEP;
                        txtNoPeserta.Text = srb.Peserta.NoKartu;
                        txtNamaPeserta.Text = srb.Peserta.Nama;
                        txtAlamat.Text = srb.Peserta.Alamat;
                        txtEmail.Text = srb.Peserta.Email;
                        cboDpjp_ItemsRequested(cboDpjp, new Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs() { Text = srb.DPJP.Kode });
                        cboDpjp.SelectedValue = srb.DPJP.Kode;

                        cboProgramPrb.SelectedValue = srb.ProgramPRB.Kode;
                        cboProgramPrb.Text = srb.ProgramPRB.Nama;

                        txtKeterangan.Text = srb.Keterangan;
                        txtSaran.Text = srb.Saran;

                        Obats = new List<Common.BPJS.VClaim.v11.RujukanBalik.Obat>();

                        foreach (var obat in srb.Obat.Obat)
                        {
                            Obats.Add(new Common.BPJS.VClaim.v11.RujukanBalik.Obat()
                            {
                                KdObat = obat.KdObat,
                                NmObat = obat.NmObat,
                                JmlObat = obat.JmlObat,
                                Signa1 = obat.Signa1,
                                Signa2 = obat.Signa2
                            });
                        }
                    }
                    else
                    {
                        ShowInformationHeader(string.Format("Code : {0}, Message : {1}", response.MetaData.Code, response.MetaData.Message));
                    }
                }
                else OnMenuNewClick();
            }
            else
            {

            };

            grdList.Rebind();
        }

        private List<Common.BPJS.VClaim.v11.RujukanBalik.Obat> Obats
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collPrbObat"];
                    if (obj != null) return (List<Common.BPJS.VClaim.v11.RujukanBalik.Obat>)obj;
                }
                //Session["collPrbObat"] = new List<Common.BPJS.VClaim.v11.RujukanBalik.Obat>();
                return (List<Common.BPJS.VClaim.v11.RujukanBalik.Obat>)Session["collPrbObat"];
            }
            set { Session["collPrbObat"] = value; }
        }

        protected void grdList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Obats.Select(o => new
            {
                o.KdObat,
                o.NmObat,
                o.JmlObat,
                o.Signa1,
                o.Signa2
            }).ToArray();
        }

        protected void btnAddObat_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (Obats.Any(o => o.KdObat == cboKodeObat.SelectedValue))
            {
                ShowInformationHeader(cboKodeObat.Text + " sudah ada, duplikasi data.");
                return;
            }

            var obat = new Common.BPJS.VClaim.v11.RujukanBalik.Obat()
            {
                KdObat = cboKodeObat.SelectedValue,
                NmObat = cboKodeObat.Text,
                JmlObat = txtJmlObat.Value.ToString(),
                Signa1 = txtSigna1.Text,
                Signa2 = txtSigna2.Text
            };
            Obats.Add(obat);
            grdList.Rebind();

            cboKodeObat.DataSource = null;
            //cboKodeObat.DataBind();
            cboKodeObat.Items.Clear();
            cboKodeObat.SelectedValue = string.Empty;
            cboKodeObat.Text = string.Empty;
            txtJmlObat.Value = 0;
            txtSigna1.Text = string.Empty;
            txtSigna2.Text = string.Empty;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            var svc = new Common.BPJS.VClaim.v11.Service();
            var response = svc.GetPrbByNo(txtNoSRB.Text, txtNoSep.Text);
            if (response.MetaData.IsValid)
            {
                if (string.IsNullOrWhiteSpace(response.Response.Prb.NoSRB)) response.Response.Prb.NoSRB = txtNoSRB.Text;
                if (string.IsNullOrWhiteSpace(response.Response.Prb.NoSEP)) response.Response.Prb.NoSEP = txtNoSep.Text;
                if (string.IsNullOrWhiteSpace(response.Response.Prb.Peserta.NoKartu)) response.Response.Prb.Peserta.NoKartu = txtNoPeserta.Text;
                if (string.IsNullOrWhiteSpace(response.Response.Prb.Peserta.Nama)) response.Response.Prb.Peserta.Nama = txtNamaPeserta.Text;
                if (string.IsNullOrWhiteSpace(response.Response.Prb.Peserta.Alamat)) response.Response.Prb.Peserta.Alamat = txtAlamat.Text;
                if (string.IsNullOrWhiteSpace(response.Response.Prb.Peserta.Email)) response.Response.Prb.Peserta.Email = txtEmail.Text;



                var json = new JsonBridgingRujukBalikValueTemp();
                if (!json.LoadByPrimaryKey(txtNoSRB.Text, txtNoSep.Text)) json = new JsonBridgingRujukBalikValueTemp();
                json.NoSRB = txtNoSRB.Text;
                json.NoSep = txtNoSep.Text;
                json.JsonValue = JsonConvert.SerializeObject(response.Response);
                json.Save();


                var jobParameters = new PrintJobParameterCollection();

                printJobParameters.AddNew("p_NoSRB", txtNoSRB.Text);
                printJobParameters.AddNew("p_NoSep", txtNoSep.Text);
                //var NoSRB = jobParameters.AddNew();
                //NoSRB.Name = "p_NoSRB";
                //NoSRB.ValueString = txtNoSRB.Text;

                //var NoSep = jobParameters.AddNew();
                //NoSep.Name = "p_NoSep";
                //NoSep.ValueString = txtNoSep.Text;

                //printJobParameters.AddNew("p_NoSep", txtNoSep.Text);
            }
        }

        protected void grdList_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var kdObat = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["KdObat"]);
            var obat = Obats.SingleOrDefault(o => o.KdObat == kdObat);
            if (obat != null)
            {
                Obats.Remove(obat);
                grdList.Rebind();
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            OnPopulateEntryControl(new string[0]);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            OnPopulateEntryControl(new string[0]);
        }

        protected void cboKodeObat_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["Nama"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["Kode"].ToString();
        }
    }
}