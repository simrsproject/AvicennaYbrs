using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Configuration;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using System.IO;
using System.Text.RegularExpressions;
using Temiang.Avicenna.Module.Finance.Master;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.Finance.Integration.Eklaim
{
    public partial class eKlaimDetail : BasePageDialog
    {
        private string GetTransactionMode
        {
            get { return Session["md_eklaim"].ToString(); }
            set { Session["md_eklaim"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.InacbgProcess;

            if (!IsPostBack)
            {
                var grr = new GuarantorQuery("a");
                var brid = new GuarantorBridgingQuery("b");

                grr.es.Distinct = true;
                grr.Select(grr.GuarantorID, grr.GuarantorName, brid.BridgingID, brid.BridgingCode);
                grr.InnerJoin(brid).On(grr.GuarantorID == brid.GuarantorID && brid.SRBridgingType == AppEnum.BridgingType.INACBG.ToString());
                var dtb = grr.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    cboJaminan.Items.Add(new Telerik.Web.UI.RadComboBoxItem(row["GuarantorName"].ToString() + " (" + row["BridgingCode"].ToString() + ")", row["GuarantorID"].ToString() + "|" + row["BridgingID"].ToString() + "|" + row["BridgingCode"].ToString()));
                }

                txtTglLahir.MinDate = DateTime.MinValue;
                txtTglMasuk.MinDate = DateTime.MinValue;
                txtTglPulang.MinDate = DateTime.MinValue;

                var medics = new ParamedicCollection();
                medics.Query.Where(medics.Query.IsActive == true);
                medics.Query.Load();

                foreach (var medic in medics)
                {
                    cboDPJP.Items.Add(new Telerik.Web.UI.RadComboBoxItem(medic.ParamedicName, medic.ParamedicID));
                }

                StandardReference.Initialize(cboJenisTariff, AppEnum.StandardReference.BpjsTariffType);

                var std = new AppStandardReference();
                std.LoadByPrimaryKey(AppEnum.StandardReference.BpjsTariffType.ToString());
                cboJenisTariff.SelectedValue = std.Note;

                StandardReference.Initialize(cboCaraPulang, AppEnum.StandardReference.BpjsDischargeMethod);

                this.OnMenuNewClick();

                //ButtonCustom.OnClientClick = "javascript:if (!confirm('Are you sure?')) return false;";
                //ButtonCustom.Text = "Grouper";
                //ButtonCustom.Visible = true;
                //ButtonCustom.Click += new EventHandler(ButtonCustom_Click);

                //ButtonOk.Visible = false;

                ButtonCancel.Enabled = false;
            }
        }

        protected void cboDiagnosa_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            btnFilterDiagnosa_Click(null, null);
        }

        private EpisodeProcedureCollection EpisodeProcedures
        {
            get
            {
                if (ViewState["icd9cm"] != null) return ViewState["icd9cm"] as EpisodeProcedureCollection;

                EpisodeProcedureCollection coll = new EpisodeProcedureCollection();

                EpisodeProcedureQuery query = new EpisodeProcedureQuery("a");
                ProcedureQuery proc = new ProcedureQuery("b");
                ParamedicQuery param = new ParamedicQuery("c");

                query.LeftJoin(proc).On(query.ProcedureID == proc.ProcedureID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);

                query.Select
                     (
                         query,
                         param.ParamedicName.As("refToParamedic_ParamedicName"),
                         proc.ProcedureName.As("refToProcedure_ProcedureName")
                     );

                query.Where(query.RegistrationNo == Request.QueryString["regno"], query.IsVoid == false);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                ViewState["icd9cm"] = coll;

                return coll;
            }
            set
            {
                ViewState["icd9cm"] = value;
            }
        }

        private EpisodeProcedureInaGroupperCollection EpisodeProcedureInaGrouppers
        {
            get
            {
                if (ViewState["icd9cm2"] != null) return ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection;

                EpisodeProcedureInaGroupperCollection coll = new EpisodeProcedureInaGroupperCollection();

                EpisodeProcedureInaGroupperQuery query = new EpisodeProcedureInaGroupperQuery("a");
                ProcedureQuery proc = new ProcedureQuery("b");
                ParamedicQuery param = new ParamedicQuery("c");

                query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);

                query.Select
                     (
                         query,
                         param.ParamedicName.As("refToParamedic_ParamedicName"),
                         proc.ProcedureName.As("refToProcedure_ProcedureName")
                     );

                query.Where(query.RegistrationNo == Request.QueryString["regno"], query.IsVoid == false);
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                ViewState["icd9cm2"] = coll;

                return coll;
            }
            set
            {
                ViewState["icd9cm2"] = value;
            }
        }

        protected void grdProsedur_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

            var coll = ViewState["icd9cm"] as EpisodeProcedureCollection;
            var ep = coll.FindByPrimaryKey(Request.QueryString["regno"], id);
            if (ep != null)
            {
                //db:20250714 - hanya yg no booking-nya kosong yg bisa dihapus, selebihnya dihapus/ diedit dari menu surgical history
                if (string.IsNullOrEmpty(ep.BookingNo))
                {
                    if (GetTransactionMode == "edit")
                    {
                        var diag = string.Empty;
                        foreach (var d in (ViewState["icd9cm"] as EpisodeProcedureCollection).Where(d => d.ProcedureID != ep.ProcedureID))
                        {
                            diag += d.ProcedureID + "#";
                        }
                        if (string.IsNullOrEmpty(diag)) diag = "#";

                        var svc = new Common.Inacbg.v51.Service();
                        var detail = svc.UpdateProcedure(new Common.Inacbg.v51.Detail.Data()
                        {
                            nomor_sep = txtNoSep.Text,
                            payor_id = cboJaminan.SelectedValue.Split('|')[1],
                            procedure = diag
                        });
                        if (detail.Metadata.IsValid)
                        {
                            ep.MarkAsDeleted();
                            coll.Save();
                            ViewState["icd9cm"] = null;

                            grdProsedur.Rebind();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "grouper", string.Format("alert('{0}.');", detail.Metadata.Message), true);
                        }
                    }
                    else
                    {
                        ep.MarkAsDeleted();
                        coll.Save();
                        ViewState["icd9cm"] = null;

                        grdProsedur.Rebind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "grouper", string.Format("alert('{0}.');", "This data is linked to the surgical record. If there are any changes to the data, this can be done via the surgical history menu"), true);
                }
            }
        }

        protected void grdProsedur2_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

            var coll = ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection;
            var ep = coll.FindByPrimaryKey(Request.QueryString["regno"], id);
            if (ep != null)
            {
                if (GetTransactionMode == "edit")
                {
                    //var diag = string.Empty;
                    //foreach (var d in (ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection).Where(d => d.ProcedureID != ep.ProcedureID))
                    //{
                    //    diag += d.ProcedureID + "#";
                    //}
                    //if (string.IsNullOrEmpty(diag)) diag = "#";

                    //var svc = new Common.Inacbg.v51.Service();
                    //var detail = svc.UpdateProcedure(new Common.Inacbg.v51.Detail.Data()
                    //{
                    //    nomor_sep = txtNoSep.Text,
                    //    payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    //    procedure = diag
                    //});
                    //if (detail.Metadata.IsValid)
                    //{
                    ep.MarkAsDeleted();
                    coll.Save();
                    ViewState["icd9cm2"] = null;

                    grdProsedur2.Rebind();
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "grouper", string.Format("alert('{0}.');", detail.Metadata.Message), true);
                    //}
                }
                else
                {
                    ep.MarkAsDeleted();
                    coll.Save();
                    ViewState["icd9cm2"] = null;

                    grdProsedur2.Rebind();
                }
            }
        }

        protected void grdProsedur_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    var item = e.Item as Telerik.Web.UI.GridDataItem;
                    if (item == null) return;

                    ViewState["seq9cm"] = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

                    var coll = ViewState["icd9cm"] as EpisodeProcedureCollection;
                    var ep = coll.FindByPrimaryKey(Request.QueryString["regno"], ViewState["seq9cm"].ToString());
                    if (ep != null)
                    {
                        string namaDiagnosa = string.Format("{0}-{1}", ep.ProcedureID, ep.ProcedureName);
                        cboProsedur.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ep.ProcedureName));
                        cboProsedur.SelectedValue = ep.ProcedureID;
                    }
                    break;
            }
        }

        protected void grdProsedur2_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    var item = e.Item as Telerik.Web.UI.GridDataItem;
                    if (item == null) return;

                    ViewState["seq9cm2"] = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

                    var coll = ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection;
                    var ep = coll.FindByPrimaryKey(Request.QueryString["regno"], ViewState["seq9cm2"].ToString());
                    if (ep != null)
                    {
                        string namaDiagnosa = string.Format("{0}-{1}", ep.ProcedureID, ep.ProcedureName);
                        cboProsedur2.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ep.ProcedureName));
                        cboProsedur2.SelectedValue = ep.ProcedureID;
                    }
                    break;
            }
        }

        protected void grdProsedur_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdProsedur.DataSource = EpisodeProcedures;
        }

        protected void grdProsedur2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdProsedur2.DataSource = EpisodeProcedureInaGrouppers;
        }

        protected void btnInsertProsedur_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedur.SelectedValue)) return;

            var proc = new Procedure();
            if (!proc.LoadByPrimaryKey(cboProsedur.SelectedValue))
            {
                proc = new Procedure()
                {
                    ProcedureID = cboProsedur.SelectedValue,
                    ProcedureName = cboProsedur.Text,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                proc.Save();

                proc = new Procedure();
                proc.LoadByPrimaryKey(cboProsedur.SelectedValue);
            }

            var id = ViewState["seq9cm"] == null ? string.Empty : ViewState["seq9cm"].ToString();
            //var coll = ViewState["icd9cm"] as EpisodeProcedureCollection;
            var ep = EpisodeProcedures.Where(eps => eps.RegistrationNo == Request.QueryString["regno"] && eps.ProcedureID == proc.ProcedureID).SingleOrDefault();

            #region db:20250714 - remark (data operation note di surgical history hilang karena u/ data yg sudah ada, kolom bookingNo direplace menjadi string.Empty)
            //if (ep == null) ep = EpisodeProcedures.AddNew();

            //ep.RegistrationNo = Request.QueryString["regno"];

            //// Tambah prefix supaya sama dan mencegah error diprogram entry lainnya (Handono 231004)
            //// ep.SequenceNo = string.IsNullOrEmpty(id) ? (EpisodeProcedures.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
            //id = string.IsNullOrEmpty(id) ? (EpisodeProcedures.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
            //ep.SequenceNo = string.Format("{0:000}", int.Parse(id));


            //var reg = new Registration();
            //reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            //ep.ProcedureDate = reg.RegistrationDate;
            //ep.ProcedureTime = reg.RegistrationTime;
            //ep.ProcedureDate2 = reg.RegistrationDate;
            //ep.ProcedureTime2 = reg.RegistrationTime;
            //ep.ParamedicID = string.Empty;
            //ep.ParamedicID2 = string.Empty;
            //ep.ProcedureID = proc.ProcedureID;
            //ep.ProcedureName = proc.ProcedureName;
            //ep.SRProcedureCategory = string.Empty;
            //ep.SRAnestesi = string.Empty;
            //ep.RoomID = string.Empty;
            //ep.IsCito = false;
            //ep.IsVoid = false;
            //ep.LastUpdateDateTime = DateTime.Now;
            //ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
            //ep.AssistantID1 = string.Empty;
            //ep.AssistantID2 = string.Empty;
            //ep.Notes = string.Empty;
            //ep.BookingNo = string.Empty;
            //ep.ParamedicID2a = string.Empty;
            //ep.ParamedicID3a = string.Empty;
            //ep.ParamedicID4a = string.Empty;
            //ep.ParamedicIDAnestesi = string.Empty;
            //ep.AssistantIDAnestesi = string.Empty;
            //ep.InstrumentatorID1 = string.Empty;
            //ep.InstrumentatorID2 = string.Empty;
            //ep.IsFromOperatingRoom = true;
            //ep.CreateByUserID = AppSession.UserLogin.UserID;
            //ep.CreateDateTime = DateTime.Now;
            #endregion

            //db:20250714 - skip edit, hanya procedure baru saja yg langsung di-insert, supaya data operation note di surgical history tidak hilang
            if (ep == null)
            {
                ep = EpisodeProcedures.AddNew();

                ep.RegistrationNo = Request.QueryString["regno"];

                // Tambah prefix supaya sama dan mencegah error diprogram entry lainnya (Handono 231004)
                // ep.SequenceNo = string.IsNullOrEmpty(id) ? (EpisodeProcedures.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
                id = string.IsNullOrEmpty(id) ? (EpisodeProcedures.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
                ep.SequenceNo = string.Format("{0:000}", int.Parse(id));

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                ep.ProcedureDate = reg.RegistrationDate;
                ep.ProcedureTime = reg.RegistrationTime;
                ep.ProcedureDate2 = reg.RegistrationDate;
                ep.ProcedureTime2 = reg.RegistrationTime;
                ep.ParamedicID = string.Empty;
                ep.ParamedicID2 = string.Empty;
                ep.ProcedureID = proc.ProcedureID;
                ep.ProcedureName = proc.ProcedureName;
                ep.SRProcedureCategory = string.Empty;
                ep.SRAnestesi = string.Empty;
                ep.RoomID = string.Empty;
                ep.IsCito = false;
                ep.IsVoid = false;
                ep.LastUpdateDateTime = DateTime.Now;
                ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
                ep.AssistantID1 = string.Empty;
                ep.AssistantID2 = string.Empty;
                ep.Notes = string.Empty;
                ep.BookingNo = string.Empty;
                ep.ParamedicID2a = string.Empty;
                ep.ParamedicID3a = string.Empty;
                ep.ParamedicID4a = string.Empty;
                ep.ParamedicIDAnestesi = string.Empty;
                ep.AssistantIDAnestesi = string.Empty;
                ep.InstrumentatorID1 = string.Empty;
                ep.InstrumentatorID2 = string.Empty;
                ep.IsFromOperatingRoom = true;
                ep.CreateByUserID = AppSession.UserLogin.UserID;
                ep.CreateDateTime = DateTime.Now;
            }

            if (GetTransactionMode == "edit")
            {
                var procedure = string.Empty;
                foreach (var d in (ViewState["icd9cm"] as EpisodeProcedureCollection))
                {
                    procedure += d.ProcedureID + "#";
                }

                var param = new Common.Inacbg.v51.Detail.Data()
                {
                    nomor_sep = txtNoSep.Text,
                    payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    procedure = procedure,
                    coder_nik = string.IsNullOrWhiteSpace(AppSession.UserLogin.LicenseNo)
                        ? ConfigurationManager.AppSettings["InacbgUserID"]
                        : AppSession.UserLogin.LicenseNo
                };
                var svc = new Common.Inacbg.v51.Service();
                var detail = svc.UpdateProcedure(param);
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "Eklaim Bridging - Update Procedure",
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(detail),
                    Totalms = 0
                };
                log.Save();
                if (detail.Metadata.IsValid)
                {
                    EpisodeProcedures.Save();
                    //ViewState["seq9cm"] = null;
                }
            }
            else EpisodeProcedures.Save();

            grdProsedur.Rebind();

            cboProsedur.Items.Clear();
            cboProsedur.SelectedValue = string.Empty;
            cboProsedur.Text = string.Empty;
            cboProsedur.OpenDropDownOnLoad = false;

            ViewState["seq9cm"] = string.Empty;

            if ((ViewState["icd9cm"] as EpisodeProcedureCollection).Any(p => p.ProcedureID == "39.95") && string.IsNullOrEmpty(cboDializer.SelectedValue))
            {
                cboDializer.SelectedValue = "1";
            }
        }

        protected void btnInsertProsedur2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedur2.SelectedValue)) return;

            var proc = new ProcedureInaGroupper();
            if (!proc.LoadByPrimaryKey(cboProsedur2.SelectedValue))
            {
                proc = new ProcedureInaGroupper()
                {
                    ProcedureID = cboProsedur2.SelectedValue,
                    ProcedureName = cboProsedur2.Text,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                proc.Save();

                proc = new ProcedureInaGroupper();
                proc.LoadByPrimaryKey(cboProsedur2.SelectedValue);
            }

            var id = ViewState["seq9cm2"] == null ? string.Empty : ViewState["seq9cm2"].ToString();
            //var coll = ViewState["icd9cm"] as EpisodeProcedureCollection;
            var ep = EpisodeProcedureInaGrouppers.Where(eps => eps.RegistrationNo == Request.QueryString["regno"] && eps.ProcedureID == proc.ProcedureID).SingleOrDefault();
            if (ep == null) ep = EpisodeProcedureInaGrouppers.AddNew();

            ep.RegistrationNo = Request.QueryString["regno"];
            id = string.IsNullOrEmpty(id) ? (EpisodeProcedures.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
            ep.SequenceNo = string.Format("{0:000}", int.Parse(id));

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            ep.ProcedureDate = reg.RegistrationDate;
            ep.ProcedureTime = reg.RegistrationTime;
            ep.ProcedureDate2 = reg.RegistrationDate;
            ep.ProcedureTime2 = reg.RegistrationTime;
            ep.ParamedicID = string.Empty;
            ep.ParamedicID2 = string.Empty;
            ep.ProcedureID = proc.ProcedureID;
            ep.ProcedureName = proc.ProcedureName;
            ep.SRProcedureCategory = string.Empty;
            ep.SRAnestesi = string.Empty;
            ep.RoomID = string.Empty;
            ep.IsCito = false;
            ep.IsVoid = false;
            ep.LastUpdateDateTime = DateTime.Now;
            ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
            ep.AssistantID1 = string.Empty;
            ep.AssistantID2 = string.Empty;
            ep.Notes = string.Empty;
            ep.BookingNo = string.Empty;
            ep.ParamedicID2a = string.Empty;
            ep.ParamedicID3a = string.Empty;
            ep.ParamedicID4a = string.Empty;
            ep.ParamedicIDAnestesi = string.Empty;
            ep.AssistantIDAnestesi = string.Empty;
            ep.InstrumentatorID1 = string.Empty;
            ep.InstrumentatorID2 = string.Empty;
            ep.IsFromOperatingRoom = true;
            ep.CreateByUserID = AppSession.UserLogin.UserID;
            ep.CreateDateTime = DateTime.Now;

            if (GetTransactionMode == "edit")
            {
                //var procedure = string.Empty;
                //foreach (var d in (ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection))
                //{
                //    procedure += d.ProcedureID + "#";
                //}

                //var svc = new Common.Inacbg.v51.Service();
                //var detail = svc.UpdateProcedure(new Common.Inacbg.v51.Detail.Data()
                //{
                //    nomor_sep = txtNoSep.Text,
                //    payor_id = cboJaminan.SelectedValue.Split('|')[1],
                //    procedure = procedure,
                //    coder_nik = AppSession.UserLogin.LicenseNo
                //});
                //if (detail.Metadata.IsValid)
                //{
                //    EpisodeProcedureInaGrouppers.Save();
                //    //ViewState["seq9cm"] = null;
                //}
            }
            else EpisodeProcedureInaGrouppers.Save();

            grdProsedur2.Rebind();

            cboProsedur2.Items.Clear();
            cboProsedur2.SelectedValue = string.Empty;
            cboProsedur2.Text = string.Empty;
            cboProsedur2.OpenDropDownOnLoad = false;

            ViewState["seq9cm2"] = string.Empty;
        }

        protected void btnFilterProsedur_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedur.Text))
            {
                cboProsedur.Items.Clear();
                cboProsedur.SelectedValue = string.Empty;
                cboProsedur.Text = string.Empty;
                cboProsedur.OpenDropDownOnLoad = false;
                return;
            }

            //string searchProsedur = "%" + cboProsedur.Text + "%";

            //var proc = new ProcedureCollection();
            //proc.Query.es.Top = 50;
            //proc.Query.Where(proc.Query.Or(proc.Query.ProcedureID.Like(searchProsedur), proc.Query.ProcedureName.Like(searchProsedur)));
            //if (proc.Query.Load())
            //{
            //    cboProsedur.Items.Clear();
            //    cboProsedur.SelectedValue = string.Empty;

            //    foreach (var entity in proc)
            //    {
            //        string namaProsedur = string.Format("{0}-{1}", entity.ProcedureID, entity.ProcedureName);
            //        cboProsedur.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaProsedur, entity.ProcedureID));
            //    }
            //    cboProsedur.OpenDropDownOnLoad = true;
            //}
            //else
            //{
            //    cboProsedur.Items.Clear();
            //    cboProsedur.SelectedValue = string.Empty;
            //    cboProsedur.Text = string.Empty;
            //    cboProsedur.OpenDropDownOnLoad = false;
            //}

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = cboProsedur.Text }, false);
            if (diag.Metadata.IsValid)
            {
                cboProsedur.Items.Clear();
                cboProsedur.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);
                    cboProsedur.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd[1]));
                }
                cboProsedur.OpenDropDownOnLoad = true;
            }
            else
            {
                cboProsedur.Items.Clear();
                cboProsedur.SelectedValue = string.Empty;
                cboProsedur.Text = string.Empty;
                cboProsedur.OpenDropDownOnLoad = false;
            }
        }

        protected void btnFilterProsedur2_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedur2.Text))
            {
                cboProsedur2.Items.Clear();
                cboProsedur2.SelectedValue = string.Empty;
                cboProsedur2.Text = string.Empty;
                cboProsedur2.OpenDropDownOnLoad = false;
                return;
            }

            //string searchProsedur = "%" + cboProsedur.Text + "%";

            //var proc = new ProcedureCollection();
            //proc.Query.es.Top = 50;
            //proc.Query.Where(proc.Query.Or(proc.Query.ProcedureID.Like(searchProsedur), proc.Query.ProcedureName.Like(searchProsedur)));
            //if (proc.Query.Load())
            //{
            //    cboProsedur.Items.Clear();
            //    cboProsedur.SelectedValue = string.Empty;

            //    foreach (var entity in proc)
            //    {
            //        string namaProsedur = string.Format("{0}-{1}", entity.ProcedureID, entity.ProcedureName);
            //        cboProsedur.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaProsedur, entity.ProcedureID));
            //    }
            //    cboProsedur.OpenDropDownOnLoad = true;
            //}
            //else
            //{
            //    cboProsedur.Items.Clear();
            //    cboProsedur.SelectedValue = string.Empty;
            //    cboProsedur.Text = string.Empty;
            //    cboProsedur.OpenDropDownOnLoad = false;
            //}

            var svc = new Common.Inacbg.v54.Service();
            var diag = svc.GetProcedureInagroupper(cboProsedur2.Text);
            if (diag.Metadata.IsValid)
            {
                cboProsedur2.Items.Clear();
                cboProsedur2.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd.Code, icd.Description);
                    cboProsedur2.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd.Code));
                }
                cboProsedur2.OpenDropDownOnLoad = true;
            }
            else
            {
                cboProsedur2.Items.Clear();
                cboProsedur2.SelectedValue = string.Empty;
                cboProsedur2.Text = string.Empty;
                cboProsedur2.OpenDropDownOnLoad = false;
            }
        }

        private EpisodeDiagnoseCollection EpisodeDiagnoses
        {
            get
            {
                if (ViewState["icd10"] != null) return ViewState["icd10"] as EpisodeDiagnoseCollection;

                EpisodeDiagnoseCollection coll = new EpisodeDiagnoseCollection();

                EpisodeDiagnoseQuery query = new EpisodeDiagnoseQuery("a");
                DiagnoseQuery diag = new DiagnoseQuery("b");
                AppStandardReferenceItemQuery item = new AppStandardReferenceItemQuery("e");
                MorphologyQuery morph = new MorphologyQuery("c");
                ParamedicQuery param = new ParamedicQuery("d");

                query.LeftJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
                query.InnerJoin(item).On(query.SRDiagnoseType == item.ItemID && item.StandardReferenceID == AppEnum.StandardReference.DiagnoseType);
                query.LeftJoin(morph).On(query.MorphologyID == morph.MorphologyID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);

                query.Select
                    (
                        query,
                        //diag.DiagnoseName.As("refToDiagnose_DiagnoseName"),
                        "<ISNULL(b.DiagnoseName, a.DiagnosisText) AS refToDiagnose_DiagnoseName>",
                        item.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"),
                        morph.MorphologyName.As("refToMorphology_MorphologyName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName")
                    );

                query.Where(query.RegistrationNo == Request.QueryString["regno"], query.IsVoid == false);
                query.OrderBy(query.SRDiagnoseType.Ascending);

                coll.Load(query);

                ViewState["icd10"] = coll;

                return coll;
            }
            set
            {
                ViewState["icd10"] = value;
            }
        }

        private EpisodeDiagnoseInaGroupperCollection EpisodeDiagnoseInaGrouppers
        {
            get
            {
                if (ViewState["icd102"] != null) return ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection;

                EpisodeDiagnoseInaGroupperCollection coll = new EpisodeDiagnoseInaGroupperCollection();

                EpisodeDiagnoseInaGroupperQuery query = new EpisodeDiagnoseInaGroupperQuery("a");
                DiagnoseInaGroupperQuery diag = new DiagnoseInaGroupperQuery("b");
                AppStandardReferenceItemQuery item = new AppStandardReferenceItemQuery("e");
                MorphologyQuery morph = new MorphologyQuery("c");
                ParamedicQuery param = new ParamedicQuery("d");

                query.InnerJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
                query.InnerJoin(item).On(query.SRDiagnoseType == item.ItemID && item.StandardReferenceID == AppEnum.StandardReference.DiagnoseType);
                query.LeftJoin(morph).On(query.MorphologyID == morph.MorphologyID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);

                query.Select
                    (
                        query,
                        diag.DiagnoseName.As("refToDiagnose_DiagnoseName"),
                        item.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"),
                        morph.MorphologyName.As("refToMorphology_MorphologyName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName")
                    );

                query.Where(query.RegistrationNo == Request.QueryString["regno"], query.IsVoid == false);
                query.OrderBy(query.SRDiagnoseType.Ascending);

                coll.Load(query);

                ViewState["icd102"] = coll;

                return coll;
            }
            set
            {
                ViewState["icd102"] = value;
            }
        }

        protected void grdDiagnosa_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    {
                        var item = e.Item as Telerik.Web.UI.GridDataItem;
                        if (item == null) return;

                        ViewState["seq10"] = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

                        var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;
                        var ed = coll.FindByPrimaryKey(Request.QueryString["regno"], ViewState["seq10"].ToString());
                        if (ed != null)
                        {
                            string namaDiagnosa = string.Format("{0}-{1}", ed.DiagnoseID, ed.DiagnoseName);
                            cboDiagnosa.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ed.DiagnoseID));
                            cboDiagnosa.SelectedValue = ed.DiagnoseID;
                        }
                    }
                    break;
                case "Primer":
                    {
                        var item = e.Item as Telerik.Web.UI.GridDataItem;
                        if (item == null) return;

                        //ViewState["seq10"] = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

                        //var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;
                        //var ed = coll.FindByPrimaryKey(Request.QueryString["regno"], ViewState["seq10"].ToString());
                        //if (ed != null)
                        //{
                        //    string namaDiagnosa = string.Format("{0}-{1}", ed.DiagnoseID, ed.DiagnoseName);
                        //    cboDiagnosa.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ed.DiagnoseID));
                        //    cboDiagnosa.SelectedValue = ed.DiagnoseID;

                        //    cboTipeDiagnosa.SelectedValue = ed.SRDiagnoseType;
                        //}

                        foreach (var ed in EpisodeDiagnoses)
                        {
                            if (ed.SequenceNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"])) ed.SRDiagnoseType = AppSession.Parameter.DiagnoseTypeMain;
                            else ed.SRDiagnoseType = "DiagnoseType-004";
                        }
                        EpisodeDiagnoses.Save();
                        EpisodeDiagnoses = null;

                        grdDiagnosa.Rebind();
                    }
                    break;
            }
        }

        protected void grdDiagnosa2_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    {
                        var item = e.Item as Telerik.Web.UI.GridDataItem;
                        if (item == null) return;

                        ViewState["seq102"] = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

                        var coll = ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection;
                        var ed = coll.FindByPrimaryKey(Request.QueryString["regno"], ViewState["seq102"].ToString());
                        if (ed != null)
                        {
                            string namaDiagnosa = string.Format("{0}-{1}", ed.DiagnoseID, ed.DiagnoseName);
                            cboDiagnosa2.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ed.DiagnoseID));
                            cboDiagnosa2.SelectedValue = ed.DiagnoseID;
                        }
                    }
                    break;
                case "Primer":
                    {
                        var item = e.Item as Telerik.Web.UI.GridDataItem;
                        if (item == null) return;

                        //ViewState["seq10"] = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

                        //var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;
                        //var ed = coll.FindByPrimaryKey(Request.QueryString["regno"], ViewState["seq10"].ToString());
                        //if (ed != null)
                        //{
                        //    string namaDiagnosa = string.Format("{0}-{1}", ed.DiagnoseID, ed.DiagnoseName);
                        //    cboDiagnosa.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ed.DiagnoseID));
                        //    cboDiagnosa.SelectedValue = ed.DiagnoseID;

                        //    cboTipeDiagnosa.SelectedValue = ed.SRDiagnoseType;
                        //}

                        foreach (var ed in EpisodeDiagnoseInaGrouppers)
                        {
                            if (ed.SequenceNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"])) ed.SRDiagnoseType = AppSession.Parameter.DiagnoseTypeMain;
                            else ed.SRDiagnoseType = "DiagnoseType-004";
                        }
                        EpisodeDiagnoseInaGrouppers.Save();
                        EpisodeDiagnoseInaGrouppers = null;

                        grdDiagnosa2.Rebind();
                    }
                    break;
            }
        }

        protected void grdDiagnosa_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

            var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;
            var ed = coll.FindByPrimaryKey(Request.QueryString["regno"], id);
            if (ed != null)
            {
                if (GetTransactionMode == "edit")
                {
                    var diag = string.Empty;
                    foreach (var d in (ViewState["icd10"] as EpisodeDiagnoseCollection).Where(d => d.DiagnoseID != ed.DiagnoseID))
                    {
                        diag += d.DiagnoseID + "#";
                    }
                    if (string.IsNullOrEmpty(diag)) diag = "#";

                    var svc = new Common.Inacbg.v51.Service();
                    var detail = svc.UpdateDiagnose(new Common.Inacbg.v51.Detail.Data()
                    {
                        nomor_sep = txtNoSep.Text,
                        payor_id = cboJaminan.SelectedValue.Split('|')[1],
                        diagnosa = diag
                    });
                    if (detail.Metadata.IsValid)
                    {
                        ed.MarkAsDeleted();
                        coll.Save();
                        ViewState["icd10"] = null;

                        grdDiagnosa.Rebind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "grouper", string.Format("alert('{0}.');", detail.Metadata.Message), true);
                    }
                }
                else
                {
                    ed.MarkAsDeleted();
                    coll.Save();
                    ViewState["icd10"] = null;

                    grdDiagnosa.Rebind();
                }
            }
        }

        protected void grdDiagnosa2_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

            var coll = ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection;
            var ed = coll.FindByPrimaryKey(Request.QueryString["regno"], id);
            if (ed != null)
            {
                if (GetTransactionMode == "edit")
                {
                    //var diag = string.Empty;
                    //foreach (var d in (ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection).Where(d => d.DiagnoseID != ed.DiagnoseID))
                    //{
                    //    diag += d.DiagnoseID + "#";
                    //}
                    //if (string.IsNullOrEmpty(diag)) diag = "#";

                    //var svc = new Common.Inacbg.v51.Service();
                    //var detail = svc.UpdateDiagnose(new Common.Inacbg.v51.Detail.Data()
                    //{
                    //    nomor_sep = txtNoSep.Text,
                    //    payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    //    diagnosa = diag
                    //});
                    //if (detail.Metadata.IsValid)
                    //{
                    ed.MarkAsDeleted();
                    coll.Save();
                    ViewState["icd102"] = null;

                    grdDiagnosa2.Rebind();
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "grouper", string.Format("alert('{0}.');", detail.Metadata.Message), true);
                    //}
                }
                else
                {
                    ed.MarkAsDeleted();
                    coll.Save();
                    ViewState["icd102"] = null;

                    grdDiagnosa2.Rebind();
                }
            }
        }

        protected void grdDiagnosa_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdDiagnosa.DataSource = EpisodeDiagnoses;
        }

        protected void grdDiagnosa2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdDiagnosa2.DataSource = EpisodeDiagnoseInaGrouppers;
        }

        protected void btnInsertDiagnosa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosa.SelectedValue)) return;

            var diag = new Diagnose();
            if (!diag.LoadByPrimaryKey(cboDiagnosa.SelectedValue))
            {
                diag = new Diagnose()
                {
                    DiagnoseID = cboDiagnosa.SelectedValue,
                    DtdNo = "0",
                    DiagnoseName = cboDiagnosa.Text.Replace("'", "`"),
                    IsChronicDisease = false,
                    IsDisease = false,
                    IsActive = true,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                diag.Save();

                diag = new Diagnose();
                diag.LoadByPrimaryKey(cboDiagnosa.SelectedValue);
            }

            var id = ViewState["seq10"] == null ? string.Empty : ViewState["seq10"].ToString();
            //var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;

            var ed = EpisodeDiagnoses.Where(eds => eds.RegistrationNo == Request.QueryString["regno"] && eds.DiagnoseID == diag.DiagnoseID).SingleOrDefault();
            if (ed == null) ed = EpisodeDiagnoses.AddNew();
            ed.RegistrationNo = Request.QueryString["regno"];
            ed.SequenceNo = string.Format("{0:000}", string.IsNullOrEmpty(id) ? (EpisodeDiagnoses.Max(p => p.SequenceNo.ToInt()) + 1).ToInt() : id.ToInt());
            ed.DiagnoseID = diag.DiagnoseID;
            ed.DiagnoseName = diag.DiagnoseName;
            ed.SRDiagnoseType = (EpisodeDiagnoses.Any(d => d.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain) ? "DiagnoseType-004" : AppSession.Parameter.DiagnoseTypeMain);

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
            ed.DiagnoseType = std.ItemName;

            ed.DiagnosisText = cboDiagnosa.Text;
            ed.MorphologyID = string.Empty;
            ed.ParamedicID = cboDPJP.SelectedValue;
            ed.IsAcuteDisease = false;
            ed.IsChronicDisease = diag.IsChronicDisease;
            ed.IsOldCase = false;
            ed.IsConfirmed = true;
            ed.IsVoid = false;
            ed.Notes = string.Empty;
            ed.LastUpdateDateTime = DateTime.Now;
            ed.LastUpdateByUserID = AppSession.UserLogin.UserID;
            ed.ExternalCauseID = string.Empty;
            ed.CreateByUserID = AppSession.UserLogin.UserID;
            ed.CreateDateTime = DateTime.Now;

            if (GetTransactionMode == "edit")
            {
                var diagnosa = string.Empty;
                foreach (var d in (ViewState["icd10"] as EpisodeDiagnoseCollection))
                {
                    diagnosa += d.DiagnoseID + "#";
                }

                var svc = new Common.Inacbg.v51.Service();
                var param = new Common.Inacbg.v51.Detail.Data()
                {
                    nomor_sep = txtNoSep.Text,
                    payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    diagnosa = diagnosa,
                    coder_nik = string.IsNullOrWhiteSpace(AppSession.UserLogin.LicenseNo)
                        ? ConfigurationManager.AppSettings["InacbgUserID"]
                        : AppSession.UserLogin.LicenseNo
                };
                var detail = svc.UpdateDiagnose(param);
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "Eklaim Bridging - Update Diagnose",
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(detail),
                    Totalms = 0
                };
                log.Save();
                if (detail.Metadata.IsValid)
                {
                    EpisodeDiagnoses.Save();
                    //ViewState["seq10"] = null;
                }
            }
            else EpisodeDiagnoses.Save();

            grdDiagnosa.Rebind();

            cboDiagnosa.Items.Clear();
            cboDiagnosa.SelectedValue = string.Empty;
            cboDiagnosa.Text = string.Empty;
            cboDiagnosa.OpenDropDownOnLoad = false;

            ViewState["seq10"] = string.Empty;
        }

        protected void btnInsertDiagnosa2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosa2.SelectedValue)) return;

            var diag = new DiagnoseInaGroupper();
            if (!diag.LoadByPrimaryKey(cboDiagnosa2.SelectedValue))
            {
                diag = new DiagnoseInaGroupper()
                {
                    DiagnoseID = cboDiagnosa2.SelectedValue,
                    DtdNo = "0",
                    DiagnoseName = cboDiagnosa2.Text.Replace("'", "`"),
                    IsChronicDisease = false,
                    IsDisease = false,
                    IsActive = true,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                diag.Save();

                diag = new DiagnoseInaGroupper();
                diag.LoadByPrimaryKey(cboDiagnosa2.SelectedValue);
            }

            var id = ViewState["seq102"] == null ? string.Empty : ViewState["seq102"].ToString();
            //var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;

            var ed = EpisodeDiagnoseInaGrouppers.Where(eds => eds.RegistrationNo == Request.QueryString["regno"] && eds.DiagnoseID == diag.DiagnoseID).SingleOrDefault();
            if (ed == null) ed = EpisodeDiagnoseInaGrouppers.AddNew();
            ed.RegistrationNo = Request.QueryString["regno"];
            ed.SequenceNo = string.IsNullOrEmpty(id) ? (EpisodeDiagnoseInaGrouppers.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
            ed.DiagnoseID = diag.DiagnoseID;
            ed.DiagnoseName = diag.DiagnoseName;
            ed.SRDiagnoseType = (EpisodeDiagnoseInaGrouppers.Any(d => d.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain) ? "DiagnoseType-004" : AppSession.Parameter.DiagnoseTypeMain);

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
            ed.DiagnoseType = std.ItemName;

            ed.DiagnosisText = cboDiagnosa2.Text;
            ed.MorphologyID = string.Empty;
            ed.ParamedicID = cboDPJP.SelectedValue;
            ed.IsAcuteDisease = false;
            ed.IsChronicDisease = diag.IsChronicDisease;
            ed.IsOldCase = false;
            ed.IsConfirmed = true;
            ed.IsVoid = false;
            ed.Notes = string.Empty;
            ed.LastUpdateDateTime = DateTime.Now;
            ed.LastUpdateByUserID = AppSession.UserLogin.UserID;
            ed.ExternalCauseID = string.Empty;
            ed.CreateByUserID = AppSession.UserLogin.UserID;
            ed.CreateDateTime = DateTime.Now;

            //if (GetTransactionMode == "edit")
            //{
            //    var diagnosa = string.Empty;
            //    foreach (var d in (ViewState["icd102"] as EpisodeDiagnoseCollection))
            //    {
            //        diagnosa += d.DiagnoseID + "#";
            //    }

            //    var svc = new Common.Inacbg.v51.Service();
            //    var detail = svc.UpdateDiagnose(new Common.Inacbg.v51.Detail.Data()
            //    {
            //        nomor_sep = txtNoSep.Text,
            //        payor_id = cboJaminan.SelectedValue.Split('|')[1],
            //        diagnosa = diagnosa,
            //        coder_nik = AppSession.UserLogin.LicenseNo
            //    });
            //    if (detail.Metadata.IsValid)
            //    {
            //        EpisodeDiagnoseInaGrouppers.Save();
            //        //ViewState["seq10"] = null;
            //    }
            //}
            //else 
            EpisodeDiagnoseInaGrouppers.Save();

            grdDiagnosa2.Rebind();

            cboDiagnosa2.Items.Clear();
            cboDiagnosa2.SelectedValue = string.Empty;
            cboDiagnosa2.Text = string.Empty;
            cboDiagnosa2.OpenDropDownOnLoad = false;

            ViewState["seq102"] = string.Empty;
        }

        protected void btnFilterDiagnosa_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosa.Text))
            {
                cboDiagnosa.Items.Clear();
                cboDiagnosa.SelectedValue = string.Empty;
                cboDiagnosa.Text = string.Empty;
                cboDiagnosa.OpenDropDownOnLoad = false;
                return;
            }

            //string searchDiagnosa = "%" + cboDiagnosa.Text + "%";

            //var diag = new DiagnoseCollection();
            //diag.Query.es.Top = 50;
            //diag.Query.Where(diag.Query.Or(diag.Query.DiagnoseID.Like(searchDiagnosa), diag.Query.DiagnoseName.Like(searchDiagnosa)));
            //if (diag.Query.Load())
            //{
            //    cboDiagnosa.Items.Clear();
            //    cboDiagnosa.SelectedValue = string.Empty;

            //    foreach (var entity in diag)
            //    {
            //        string namaDiagnosa = string.Format("{0}-{1}", entity.DiagnoseID, entity.DiagnoseName);
            //        cboDiagnosa.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, entity.DiagnoseID));
            //    }
            //    cboDiagnosa.OpenDropDownOnLoad = true;
            //}
            //else
            //{
            //    cboDiagnosa.Items.Clear();
            //    cboDiagnosa.SelectedValue = string.Empty;
            //    cboDiagnosa.OpenDropDownOnLoad = false;
            //}

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = cboDiagnosa.Text }, true);
            if (diag.Metadata.IsValid)
            {
                cboDiagnosa.Items.Clear();
                cboDiagnosa.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);
                    cboDiagnosa.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd[1]));
                }
                cboDiagnosa.OpenDropDownOnLoad = true;
            }
            else
            {
                cboDiagnosa.Items.Clear();
                cboDiagnosa.SelectedValue = string.Empty;
                cboDiagnosa.Text = string.Empty;
                cboDiagnosa.OpenDropDownOnLoad = false;
            }
        }

        protected void btnFilterDiagnosa2_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosa2.Text))
            {
                cboDiagnosa2.Items.Clear();
                cboDiagnosa2.SelectedValue = string.Empty;
                cboDiagnosa2.Text = string.Empty;
                cboDiagnosa2.OpenDropDownOnLoad = false;
                return;
            }

            //string searchDiagnosa = "%" + cboDiagnosa.Text + "%";

            //var diag = new DiagnoseCollection();
            //diag.Query.es.Top = 50;
            //diag.Query.Where(diag.Query.Or(diag.Query.DiagnoseID.Like(searchDiagnosa), diag.Query.DiagnoseName.Like(searchDiagnosa)));
            //if (diag.Query.Load())
            //{
            //    cboDiagnosa.Items.Clear();
            //    cboDiagnosa.SelectedValue = string.Empty;

            //    foreach (var entity in diag)
            //    {
            //        string namaDiagnosa = string.Format("{0}-{1}", entity.DiagnoseID, entity.DiagnoseName);
            //        cboDiagnosa.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, entity.DiagnoseID));
            //    }
            //    cboDiagnosa.OpenDropDownOnLoad = true;
            //}
            //else
            //{
            //    cboDiagnosa.Items.Clear();
            //    cboDiagnosa.SelectedValue = string.Empty;
            //    cboDiagnosa.OpenDropDownOnLoad = false;
            //}

            var svc = new Common.Inacbg.v54.Service();
            var diag = svc.GetDiagnoseInagroupper(cboDiagnosa2.Text);
            if (diag.Metadata.IsValid)
            {
                cboDiagnosa2.Items.Clear();
                cboDiagnosa2.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd.Code, icd.Description);
                    cboDiagnosa2.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd.Code));
                }
                cboDiagnosa2.OpenDropDownOnLoad = true;
            }
            else
            {
                cboDiagnosa2.Items.Clear();
                cboDiagnosa2.SelectedValue = string.Empty;
                cboDiagnosa2.Text = string.Empty;
                cboDiagnosa2.OpenDropDownOnLoad = false;
            }
        }

        protected void chkNaikKelas_CheckedChanged(object sender, EventArgs e)
        {
            rblNaikKelasRawat.ClearSelection();
            txtLamaNaikKelas.Value = rblJenisRawat.SelectedValue != AppConstant.RegistrationType.InPatient ? 1 : !txtTglPulang.IsEmpty ? 0 : (txtTglPulang.SelectedDate.Value.Date - txtTglMasuk.SelectedDate.Value.Date).TotalDays + 1;
        }

        protected void chkRawatIntensif_CheckedChanged(object sender, EventArgs e)
        {
            txtRawatIntensif.Value = 0;
            txtVentilator.Value = 0;
        }

        protected void txtTglPulang_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtLOS.Value = rblJenisRawat.SelectedValue != AppConstant.RegistrationType.InPatient ? 1 : !txtTglPulang.IsEmpty ? (txtTglPulang.SelectedDate.Value.Date - txtTglMasuk.SelectedDate.Value.Date).TotalDays + 1 : 0;
            txtLamaNaikKelas.Value = txtLOS.Value;
        }

        private void OnBeforeMenuNewClick(ValidateArgs args)
        {
            var medicalNo = !string.IsNullOrWhiteSpace(AppSession.Parameter.EklaimRemoveDashSeparatorOnMedicalNo) && AppSession.Parameter.EklaimRemoveDashSeparatorOnMedicalNo.ToLower() == "yes" ? txtNoMR.Text.Replace("-", string.Empty) : txtNoMR.Text;
            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "YBRSGKP":
                    var pat = new Patient();
                    pat.LoadByMedicalNo(txtNoMR.Text);
                    if (!string.IsNullOrEmpty(pat.OldMedicalNo)) medicalNo = medicalNo.ToInt().ToString();
                    medicalNo = medicalNo.ToInt().ToString();
                    break;
            }

            var svc = new Common.Inacbg.v51.Service();
            var response = svc.Insert(new Common.Inacbg.v51.Claim.Create.Data()
            {
                nomor_kartu = txtNoPeserta.Text,
                nomor_sep = txtNoSep.Text,
                nomor_rm = medicalNo,
                nama_pasien = txtNamaPasien.Text,
                tgl_lahir = txtTglLahir.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                gender = (rblJenisKelamin.SelectedValue == "M" ? "1" : "2")
            });
            if (!response.Metadata.IsValid && response.Metadata.ErrorNo == "E2007") // Duplikasi nomor SEP
                response.Metadata.Code = "200";

            if (response.Metadata.IsValid)
            {
                if (response.Response != null)
                {
                    var ncc = new NccInacbg();
                    if (!ncc.LoadByPrimaryKey(Request.QueryString["regno"])) ncc = new NccInacbg();
                    ncc.RegistrationNo = Request.QueryString["regno"];
                    ncc.PatientId = response.Response.PatientId;
                    ncc.AdmissionId = response.Response.AdmissionId;
                    ncc.HospitalAdmissionId = response.Response.HospitalAdmissionId;
                    ncc.LastUpdateDateTime = DateTime.Now;
                    ncc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ncc.AddPaymentAmt = 0;
                    ncc.Save();
                }

                var episode = string.Empty;
                foreach (GridDataItem dataItem in grdEpisodeRuangRawat.MasterTableView.Items)
                {
                    var id = dataItem["ID"].Text;
                    var jumlah = ((RadNumericTextBox)dataItem.FindControl("txtJumlah")).Value;
                    episode += string.Format("{0};{1}", id, jumlah.ToInt().ToString()) + "#";
                }

                var diag = string.Empty;
                foreach (var d in (ViewState["icd10"] as EpisodeDiagnoseCollection).OrderBy(e => e.SRDiagnoseType))
                {
                    diag += d.DiagnoseID + "#";
                }
                if (string.IsNullOrEmpty(diag)) diag = "#";

                var proc = string.Empty;
                foreach (var d in (ViewState["icd9cm"] as EpisodeProcedureCollection))
                {
                    proc += d.ProcedureID + "#";
                }
                if (string.IsNullOrEmpty(proc)) proc = "#";

                var svc54 = new Common.Inacbg.v58.Service();
                var param = new Common.Inacbg.v58.Detail.Data()
                {
                    nomor_sep = txtNoSep.Text,
                    nomor_kartu = txtNoPeserta.Text,
                    tgl_masuk = txtTglMasuk.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    tgl_pulang = txtTglPulang.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    cara_masuk = cboCaraMasuk.SelectedValue,
                    jenis_rawat = rblJenisRawat.SelectedValue.Split('|')[1],
                    kelas_rawat = rblJenisRawat.SelectedValue.Split('|')[1] == "1"
                        ? rblKelasRawat.SelectedValue.Split('|')[1]
                        : rblKelasRawat.SelectedValue,
                    adl_sub_acute = txtSubAcute.Text,
                    adl_chronic = txtChronic.Text,
                    icu_indikator = chkRawatIntensif.Checked ? "1" : "0",
                    icu_los = chkRawatIntensif.Checked ? txtRawatIntensif.Value.ToInt().ToString() : "0",
                    ventilator_hour = txtVentilator.Value.ToInt().ToString(),

                    use_ind = chkVentilator.Checked ? "1" : "0",
                    start_dttm = chkVentilator.Checked
                        ? $"{txtTglIntubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamIntubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}"
                        : string.Empty,
                    stop_dttm = chkVentilator.Checked
                        ? $"{txtTglEkstubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamEkstubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}"
                        : string.Empty,

                    upgrade_class_ind = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? "0"
                        : (chkNaikKelas.Checked ? "1" : "0"),
                    upgrade_class_class = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? string.Empty
                        : rblNaikKelasRawat.SelectedValue.Split('|')[1],
                    upgrade_class_los = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? string.Empty
                        : txtLamaNaikKelas.Value.ToInt().ToString(),
                    upgrade_class_payor = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? string.Empty
                        : cboPenjaminNaikKelas.SelectedValue,
                    add_payment_pct = txtSelisihPersen.Visible ? (txtSelisihPersen.Value ?? 0).ToString() : "0",
                    birth_weight = txtBeratLahir.Value.ToInt().ToString(),
                    sistole = txtSistole.Value.ToString(),
                    diastole = txtDiastole.Value.ToString(),
                    discharge_status = cboCaraPulang.SelectedValue,
                    diagnosa = diag,
                    procedure = proc,
                    tarif_rs = new Common.Inacbg.v54.Detail.TarifRs()
                    {
                        prosedur_non_bedah = txtProsedurNonBedah.Value.ToInt().ToString(),
                        prosedur_bedah = txtProsedurBedah.Value.ToInt().ToString(),
                        konsultasi = txtKonsultasi.Value.ToInt().ToString(),
                        tenaga_ahli = txtTenagaAhli.Value.ToInt().ToString(),
                        keperawatan = txtKeperawatan.Value.ToInt().ToString(),
                        penunjang = txtPenunjang.Value.ToInt().ToString(),
                        radiologi = txtRadiologi.Value.ToInt().ToString(),
                        laboratorium = txtLaboratorium.Value.ToInt().ToString(),
                        pelayanan_darah = txtPelayananDarah.Value.ToInt().ToString(),
                        rehabilitasi = txtRehabilitasi.Value.ToInt().ToString(),
                        kamar = txtKamarAkomodasi.Value.ToInt().ToString(),
                        rawat_intensif = txtRawatIntensifTarif.Value.ToInt().ToString(),
                        obat = txtObat.Value.ToInt().ToString(),
                        obat_kronis = txtObatKronis.Value.ToInt().ToString(),
                        obat_kemoterapi = txtObatKemoterapi.Value.ToInt().ToString(),
                        alkes = txtAlkes.Value.ToInt().ToString(),
                        bmhp = txtBMHP.Value.ToInt().ToString(),
                        sewa_alat = txtSewaAlat.Value.ToInt().ToString()
                    },
                    pemulasaraan_jenazah = chkPemulasaraanJenazah.Checked ? "1" : "0",
                    kantong_jenazah = chkKantongJenazah.Checked ? "1" : "0",
                    peti_jenazah = chkPetiJenazah.Checked ? "1" : "0",
                    plastik_erat = chkPlastikErat.Checked ? "1" : "0",
                    desinfektan_jenazah = chkDesinfektanJenazah.Checked ? "1" : "0",
                    mobil_jenazah = chkTransportMobil.Checked ? "1" : "0",
                    desinfektan_mobil_jenazah = chkDesinfektanMobil.Checked ? "1" : "0",
                    covid19_status_cd = cboStatusCov.SelectedValue,
                    nomor_kartu_t = cboNoPeserta.SelectedValue,
                    episodes = episode,
                    covid19_cc_ind = rblKomorbid.SelectedValue,
                    covid19_rs_darurat_ind = rblRsDaruratLapangan.SelectedValue, //
                    covid19_co_insidense_ind = rblCoInsidens.SelectedValue, //
                    covid19_penunjang_pengurang = new Common.Inacbg.v54.Detail.Covid19PenunjangPengurang()
                    {
                        lab_asam_laktat = chkAsamLaktat.Checked ? "1" : "0", //
                        lab_procalcitonin = chkProcalcitonin.Checked ? "1" : "0", //
                        lab_crp = chkCRP.Checked ? "1" : "0", //
                        lab_kultur = chkKultur.Checked ? "1" : "0", //
                        lab_d_dimer = chkDDimer.Checked ? "1" : "0", //
                        lab_pt = chkPT.Checked ? "1" : "0", //
                        lab_aptt = chkAPTT.Checked ? "1" : "0", //
                        lab_waktu_pendarahan = chkWaktuPendarahan.Checked ? "1" : "0", //
                        lab_anti_hiv = chkAntiHIV.Checked ? "1" : "0", //
                        lab_analisa_gas = chkAnalisaGas.Checked ? "1" : "0", //
                        lab_albumin = chkAlbumin.Checked ? "1" : "0", //
                        rad_thorax_ap_pa = chkThoraxAPPA.Checked ? "1" : "0" //
                    },
                    terapi_konvalesen = txtTerapiKovalesen.Value.ToInt().ToString(), //
                    akses_naat = rblKriteriaAksesNaat.SelectedValue, //
                    isoman_ind = rblIsolasiMandiri.SelectedValue, //
                    bayi_lahir_status_cd = cboStatusKelainan.SelectedValue, //

                    dializer_single_use = cboDializer.SelectedValue,
                    kantong_darah = txtKantongDarah.Value > 0 ? txtKantongDarah.Value.ToString() : string.Empty,
                    menit_1_appearance = txt1Appearance.Value > 0 ? txt1Appearance.Value.ToString() : string.Empty,
                    menit_1_pulse = txt1Pulse.Value > 0 ? txt1Pulse.Value.ToString() : string.Empty,
                    menit_1_grimace = txt1Grimace.Value > 0 ? txt1Grimace.Value.ToString() : string.Empty,
                    menit_1_activity = txt1Activity.Value > 0 ? txt1Activity.Value.ToString() : string.Empty,
                    menit_1_respiration = txt1Respiration.Value > 0 ? txt1Respiration.Value.ToString() : string.Empty,
                    menit_5_appearance = txt5Appearance.Value > 0 ? txt5Appearance.Value.ToString() : string.Empty,
                    menit_5_pulse = txt5Pulse.Value > 0 ? txt5Pulse.Value.ToString() : string.Empty,
                    menit_5_grimace = txt5Grimace.Value > 0 ? txt5Grimace.Value.ToString() : string.Empty,
                    menit_5_activity = txt5Activity.Value > 0 ? txt5Activity.Value.ToString() : string.Empty,
                    menit_5_respiration = txt5Respiration.Value > 0 ? txt5Respiration.Value.ToString() : string.Empty,
                    usia_kehamilan = txtUsiaKehamilan.Value > 0 ? txtUsiaKehamilan.Value.ToString() : string.Empty,
                    gravida = txtGravida.Value > 0 ? txtGravida.Value.ToString() : string.Empty,
                    partus = txtPartus.Value > 0 ? txtPartus.Value.ToString() : string.Empty,
                    abortus = txtAbortus.Value > 0 ? txtAbortus.Value.ToString() : string.Empty,
                    onset_kontraksi = cboOnsetKontraksi.SelectedValue,
                    delivery = Deliveries.Any() ? JsonConvert.SerializeObject(Deliveries) : string.Empty,

                    tarif_poli_eks = txtTariffPoliEks.Value.ToInt().ToString(),
                    nama_dokter = cboDPJP.Text,
                    kode_tarif = cboJenisTariff.SelectedValue,
                    payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    payor_cd = cboJaminan.SelectedValue.Split('|')[2],
                    cob_cd = string.IsNullOrEmpty(cboCOB.SelectedValue)
                        ? string.Empty
                        : cboCOB.SelectedValue.Split('|')[1],
                    coder_nik = string.IsNullOrWhiteSpace(AppSession.UserLogin.LicenseNo)
                        ? ConfigurationManager.AppSettings["InacbgUserID"]
                        : AppSession.UserLogin.LicenseNo
                };
                var detail = svc54.Insert(param);
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "Eklaim Bridging - Insert",
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(detail),
                    Totalms = 0
                };
                log.Save();
                if (!detail.Metadata.IsValid)
                {
                    args.MessageText = string.Format("{0} - {1}", detail.Metadata.ErrorNo, detail.Metadata.Message);
                    args.IsCancel = true;
                }
            }
            //else if (response.Metadata.IsDuplicate)
            //{
            //    var str = string.Empty;
            //    var duplicate = response.Duplicate;
            //    foreach (var list in duplicate)
            //    {
            //        DateTime date;
            //        str += string.Format("{0}[No. RM : {1}, Nama Pasien : {2}, Tgl. Masuk : {3}]",
            //            string.IsNullOrEmpty(str) ? string.Empty : " dan ",
            //            list.NomorRm, list.NamaPasien,
            //            DateTime.TryParse(list.TglMasuk, out date) ? date.ToString("MM/dd/yyyy HH:mm") : string.Empty);
            //    }

            //    args.MessageText = string.Format("{0} - {1} {2}", response.Metadata.ErrorNo, response.Metadata.Message, str);
            //    args.IsCancel = true;
            //}
            else
            {
                args.MessageText = string.Format("{0} - {1}", response.Metadata.ErrorNo, response.Metadata.Message);
                args.IsCancel = true;
            }
        }

        private void OnMenuNewClick()
        {
            GetTransactionMode = Request.QueryString["md"];

            Deliveries = null;

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var mergeRegs = Registration.RelatedRegistrations(reg.RegistrationNo);
            var table = new DataTable();

            if (new[] { AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient }.Contains(reg.SRRegistrationType))
                table = VitalSign.VitalSignLastValue(reg.RegistrationNo, mergeRegs, true, DateTime.Now.Date);
            else table = VitalSign.VitalSignLastValue(reg.RegistrationNo, mergeRegs, true, reg.DischargeDate == null ? DateTime.Now.Date : reg.DischargeDate.Value.Date);

            if (table.Rows.Count > 0)
            {
                var sistolic = table.AsEnumerable().SingleOrDefault(t => t.Field<string>("VitalSignID") == "BP1");
                if (sistolic != null) txtSistole.Value = sistolic["QuestionAnswerText"].ToString().ToInt();
                var diastolic = table.AsEnumerable().SingleOrDefault(t => t.Field<string>("VitalSignID") == "BP2");
                if (diastolic != null) txtDiastole.Value = diastolic["QuestionAnswerText"].ToString().ToInt();
            }
            else
            {
                var nthd = new NursingTransHD();
                nthd.Query.Where(nthd.Query.RegistrationNo == Request.QueryString["regno"]);
                if (nthd.Query.Load())
                {
                    var ndtdt = new NursingDiagnosaTransDTCollection();
                    ndtdt.Query.Where(ndtdt.Query.TransactionNo == nthd.TransactionNo, ndtdt.Query.ReferenceToPhrNo.IsNotNull());
                    ndtdt.Query.OrderBy(ndtdt.Query.ID.Ascending);
                    if (ndtdt.Query.Load())
                    {
                        foreach (var item in ndtdt)
                        {
                            var phrl = new PatientHealthRecordLineCollection();
                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                            {
                                phrl.Query.Where(phrl.Query.TransactionNo == item.ReferenceToPhrNo, phrl.Query.RegistrationNo == Request.QueryString["regno"], phrl.Query.QuestionID.In("KTHD1.038", "KTHD1.039"));
                                if (phrl.Query.Load())
                                {
                                    txtSistole.Value = Convert.ToDouble(phrl.Single(p => p.QuestionID == "KTHD1.038").QuestionAnswerNum ?? 0);
                                    txtDiastole.Value = Convert.ToDouble(phrl.Single(p => p.QuestionID == "KTHD1.039").QuestionAnswerNum ?? 0);

                                    break;
                                }
                            }
                            else if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSEE")
                            {
                                phrl.Query.Where(phrl.Query.TransactionNo == item.ReferenceToPhrNo, phrl.Query.RegistrationNo == Request.QueryString["regno"], phrl.Query.QuestionID.In("A.KDV.1TDS", "A.KDV.2TDD"));
                                if (phrl.Query.Load())
                                {
                                    txtSistole.Value = Convert.ToDouble(phrl.Single(p => p.QuestionID == "A.KDV.1TDS").QuestionAnswerNum ?? 0);
                                    txtDiastole.Value = Convert.ToDouble(phrl.Single(p => p.QuestionID == "A.KDV.2TDD").QuestionAnswerNum ?? 0);

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (txtSistole.Value == 0 || txtDiastole.Value == 0)
            {
                var nthd = new NursingTransHD();
                nthd.Query.Where(nthd.Query.RegistrationNo == Request.QueryString["regno"]);
                if (nthd.Query.Load())
                {
                    var ndtdt = new NursingDiagnosaTransDTCollection();
                    ndtdt.Query.Where(ndtdt.Query.TransactionNo == nthd.TransactionNo, ndtdt.Query.ReferenceToPhrNo.IsNotNull());
                    ndtdt.Query.OrderBy(ndtdt.Query.ID.Ascending);
                    if (ndtdt.Query.Load())
                    {
                        foreach (var item in ndtdt)
                        {
                            var phrl = new PatientHealthRecordLineCollection();
                            if (AppSession.Parameter.HealthcareInitialAppsVersion == "YBRSGKP")
                            {
                                phrl.Query.Where(phrl.Query.TransactionNo == item.ReferenceToPhrNo, phrl.Query.RegistrationNo == Request.QueryString["regno"], phrl.Query.QuestionID.In("KTHD1.038", "KTHD1.039"));
                                if (phrl.Query.Load())
                                {
                                    txtSistole.Value = Convert.ToDouble(phrl.Single(p => p.QuestionID == "KTHD1.038").QuestionAnswerNum ?? 0);
                                    txtDiastole.Value = Convert.ToDouble(phrl.Single(p => p.QuestionID == "KTHD1.039").QuestionAnswerNum ?? 0);

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            txtNoMR.Text = pat.MedicalNo;
            txtNamaPasien.Text = pat.PatientName;
            rblJenisKelamin.SelectedValue = pat.Sex;
            txtTglLahir.SelectedDate = pat.DateOfBirth.Value.Date;

            for (int i = 0; i < cboJaminan.Items.Count; i++)
            {
                if (cboJaminan.Items[i].Value.Split('|')[0] == reg.GuarantorID && cboJaminan.Items[i].Value.Split('|')[1] == "3") //JKN
                {
                    cboJaminan.SelectedIndex = i;
                    break;
                }
                else if (cboJaminan.Items[i].Value.Split('|')[0] == reg.GuarantorID && cboJaminan.Items[i].Value.Split('|')[1] == "71") //JAMINAN COVID-19
                {
                    cboJaminan.SelectedIndex = i;
                    break;
                }
                else if (cboJaminan.Items[i].Value.Split('|')[0] == reg.GuarantorID && cboJaminan.Items[i].Value.Split('|')[1] == "72") //JAMINAN KIPI
                {
                    cboJaminan.SelectedIndex = i;
                    break;
                }
                else if (cboJaminan.Items[i].Value.Split('|')[0] == reg.GuarantorID && cboJaminan.Items[i].Value.Split('|')[1] == "73") //JAMINAN BAYI BARU LAHIR
                {
                    cboJaminan.SelectedIndex = i;
                    break;
                }
                else if (cboJaminan.Items[i].Value.Split('|')[0] == reg.GuarantorID && cboJaminan.Items[i].Value.Split('|')[1] == "74") //JAMINAN PERPANJANGAN MASA RAWAT
                {
                    cboJaminan.SelectedIndex = i;
                    break;
                }
                else if (cboJaminan.Items[i].Value.Split('|')[0] == reg.GuarantorID && cboJaminan.Items[i].Value.Split('|')[1] == "75") //JAMINAN CO-INSIDENSE
                {
                    cboJaminan.SelectedIndex = i;
                    break;
                }
            }

            rblJenisRawat.SelectedValue =
                reg.SRRegistrationType == AppConstant.RegistrationType.InPatient ?
                    AppConstant.RegistrationType.InPatient + "|1" :
                    AppConstant.RegistrationType.OutPatient + "|2";

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient) cboCaraMasuk.SelectedValue = "emd";
            else cboCaraMasuk.SelectedValue = "gp";

            cboJaminan_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboJaminan.SelectedValue, string.Empty));

            txtNoPeserta.Text = reg.GuarantorCardNo;
            //txtNoSep.Text = reg.BpjsSepNo;

            tdNaikKelas.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
            chkNaikKelas.Checked = reg.ChargeClassID != reg.CoverageClassID;
            tdRawatIntensif.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;

            txtTglMasuk.SelectedDate = reg.RegistrationDate.Value.Date;
            if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            {
                txtTglPulang.SelectedDate = txtTglMasuk.SelectedDate;
                txtTglPulang.DateInput.ReadOnly = false;
                txtTglPulang.DatePopupButton.Enabled = false;
            }
            else
                if (reg.DischargeDate != null) txtTglPulang.SelectedDate = reg.DischargeDate.Value.Date;

            grdEpisodeRuangRawat.DataSource = EpisodeRuangRawatCollection;
            grdEpisodeRuangRawat.DataBind();

            Class cls;
            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient && chkNaikKelas.Checked)
            {
                trNaikKelasRawat.Visible = true;
                trNaikKelasRawat2.Visible = true;

                cls = new Class();
                cls.LoadByPrimaryKey(reg.ChargeClassID);

                foreach (ListItem item in rblNaikKelasRawat.Items)
                {
                    if (item.Value.Split('|')[0] == cls.SRClassRL)
                    {
                        rblNaikKelasRawat.SelectedValue = item.Value;
                        break;
                    }
                }
            }

            if (rblNaikKelasRawat.SelectedValue == "ClassRL-001|vip" || rblNaikKelasRawat.SelectedValue == "ClassRL-000|vvip") txtSelisihPersen.Visible = true;
            else txtSelisihPersen.Visible = false;

            trRawatIntensif.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
            txtLOS.Value = reg.SRRegistrationType != AppConstant.RegistrationType.InPatient ? 1 : reg.DischargeDate != null ? (reg.DischargeDate.Value.Date - reg.RegistrationDate.Value.Date).TotalDays + 1 : 0;
            cboDPJP.SelectedValue = reg.ParamedicID;

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                rblKelasRawat.Items.Add(new ListItem() { Text = "Kelas 1", Value = "ClassRL-002|1" });
                rblKelasRawat.Items.Add(new ListItem() { Text = "Kelas 2", Value = "ClassRL-003|2" });
                rblKelasRawat.Items.Add(new ListItem() { Text = "Kelas 3", Value = "ClassRL-004|3" });
            }
            else
            {
                rblKelasRawat.Items.Add(new ListItem() { Text = "Regular", Value = "3" });
                rblKelasRawat.Items.Add(new ListItem() { Text = "Eksekutif", Value = "1" });
            }

            cls = new Class();
            cls.LoadByPrimaryKey(reg.CoverageClassID);

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                foreach (ListItem item in rblKelasRawat.Items)
                {
                    if (item.Value.Split('|')[0] == cls.SRClassRL)
                    {
                        rblKelasRawat.SelectedValue = item.Value;
                        break;
                    }
                }
            }
            else rblKelasRawat.SelectedValue = "3";

            txtUmur.Text = string.Format("{0}th {1}bl {2}hr", reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay);

            trVentilator.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
            trVentilator2.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
            trCoInsidenseCovid.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
            trNaikKelasRawat2.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                if (string.IsNullOrEmpty(reg.SRDischargeCondition)) cboCaraPulang.SelectedValue = string.Empty;
                else
                {
                    var dc = new string[3] { AppSession.Parameter.DischargeConditionDie, AppSession.Parameter.DischargeConditionDieLessThen48, AppSession.Parameter.DischargeConditionDieMoreThen48 };
                    if (dc.Contains(reg.SRDischargeCondition)) cboCaraPulang.SelectedValue = "4"; //default : meninggal
                    else
                    {
                        if (AppSession.Parameter.HealthcareInitial == "RSI") cboCaraPulang.SelectedValue = "1"; //default : atas persetujuan dokter
                        else
                        {
                            var asri = new AppStandardReferenceItem();
                            asri.Query.Where(asri.Query.StandardReferenceID == AppEnum.StandardReference.BpjsDischargeMethod.ToString(), asri.Query.Note.Like("%" + reg.SRDischargeMethod + "%"));
                            if (asri.Query.Load()) cboCaraPulang.SelectedValue = asri.ItemID;
                        }
                    }
                }
            }
            else cboCaraPulang.SelectedValue = "1"; //default : atas persetujuan dokter

            cboCaraPulang_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboCaraPulang.SelectedValue, string.Empty));

            rblKelasRawat_SelectedIndexChanged(null, null);

            btnFinal.Visible = true;
            btnEdit.Visible = false;

            if (!string.IsNullOrEmpty(txtNoSep.Text))
            {
                var svc = new Common.Inacbg.v58.Service();
                var response = svc.GetDetail(new Common.Inacbg.v51.Claim.Get.GetDetail.Data() { nomor_sep = txtNoSep.Text });
                if (response.Metadata.IsValid)
                {
                    GetTransactionMode = "edit";

                    var data = response.Response.Data;

                    cboJaminan.SelectedValue = cboJaminan.Items.Where(c => c.Value.Split('|')[0] == reg.GuarantorID && c.Value.Split('|')[1] == data.PayorId).Single().Value;
                    cboJaminan_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboJaminan.SelectedValue, string.Empty));

                    chkRawatIntensif.Checked = (data.IcuIndikator == 1);
                    txtRawatIntensif.Value = Convert.ToDouble(data.IcuLos);
                    txtChronic.Text = data.AdlChronic.ToString();
                    txtSubAcute.Text = data.AdlSubAcute.ToString();
                    txtLamaNaikKelas.Value = Convert.ToDouble(data.UpgradeClassLos);
                    txtVentilator.Value = Convert.ToDouble(data.VentilatorHour);
                    txtBeratLahir.Value = Convert.ToDouble(data.BeratLahir);

                    cboCaraPulang.SelectedValue = data.DischargeStatus.ToString();
                    cboCaraPulang_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboCaraPulang.SelectedValue, string.Empty));

                    cboJenisTariff.SelectedValue = data.KodeTarif;
                    txtTariffPoliEks.Value = Convert.ToDouble(data.TarifPoliEks);
                    txtTerapiKovalesen.Value = 0;

                    var tariff = response.Response.Data.TarifRs;
                    txtProsedurNonBedah.Value = Convert.ToDouble(tariff.ProsedurNonBedah);
                    txtTenagaAhli.Value = Convert.ToDouble(tariff.TenagaAhli);
                    txtRadiologi.Value = Convert.ToDouble(tariff.Radiologi);
                    txtRehabilitasi.Value = Convert.ToDouble(tariff.Rehabilitasi);
                    txtObat.Value = Convert.ToDouble(tariff.Obat);
                    txtSewaAlat.Value = Convert.ToDouble(tariff.SewaAlat);
                    txtProsedurBedah.Value = Convert.ToDouble(tariff.ProsedurBedah);
                    txtKeperawatan.Value = Convert.ToDouble(tariff.Keperawatan);
                    txtLaboratorium.Value = Convert.ToDouble(tariff.Laboratorium);
                    txtKamarAkomodasi.Value = Convert.ToDouble(tariff.Kamar);
                    txtObatKronis.Value = Convert.ToDouble(tariff.ObatKronis);
                    txtAlkes.Value = Convert.ToDouble(tariff.Alkes);
                    txtKonsultasi.Value = Convert.ToDouble(tariff.Konsultasi);
                    txtPenunjang.Value = Convert.ToDouble(tariff.Penunjang);
                    txtPelayananDarah.Value = Convert.ToDouble(tariff.PelayananDarah);
                    txtRawatIntensifTarif.Value = Convert.ToDouble(tariff.RawatIntensif);
                    txtObatKemoterapi.Value = Convert.ToDouble(tariff.ObatKemoterapi);
                    txtBMHP.Value = Convert.ToDouble(tariff.Bmhp);

                    txtProsedurNonBedah_TextChanged(null, null);

                    var ib = new IntermBillCollection();
                    ib.Query.Where(ib.Query.RegistrationNo == Request.QueryString["regno"], ib.Query.IsVoid == false);
                    if (ib.Query.Load())
                    {
                        var admin = ib.Select(b => b.AdministrationAmount + b.GuarantorAdministrationAmount).Sum();

                        var cc = new CostCalculationQuery("a");
                        var i = new ItemQuery("b");

                        cc.Select(i.SREklaimTariffGroup, i.SREklaimFactorGroup, (cc.PatientAmount.Sum() + cc.GuarantorAmount.Sum()).As("Amount"));
                        cc.InnerJoin(i).On(cc.ItemID == i.ItemID);
                        cc.Where(cc.IntermBillNo.In(ib.Select(b => b.IntermBillNo)));
                        cc.GroupBy(i.SREklaimTariffGroup, i.SREklaimFactorGroup);

                        var tbl = cc.LoadDataTable();
                        if (tbl.Rows.Count > 0)
                        {
                            var total = tbl.AsEnumerable().Sum(t => t.Field<decimal>("Amount"));
                            if (txtTarifRS.Value != Convert.ToDouble(total))
                            {
                                var pnb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "01").Sum(t => t.Field<decimal>("Amount"));
                                txtProsedurNonBedah.Value = pnb != null ? Convert.ToDouble(pnb) : 0;
                                var ta = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "04").Sum(t => t.Field<decimal>("Amount"));
                                txtTenagaAhli.Value = ta != null ? Convert.ToDouble(ta) : 0;
                                var rd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "07").Sum(t => t.Field<decimal>("Amount"));
                                txtRadiologi.Value = rd != null ? Convert.ToDouble(rd) : 0;
                                var rh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "10").Sum(t => t.Field<decimal>("Amount"));
                                txtRehabilitasi.Value = rh != null ? Convert.ToDouble(rh) : 0;
                                var ob = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "13").Sum(t => t.Field<decimal>("Amount"));
                                txtObat.Value = ob != null ? Convert.ToDouble(ob) : 0;
                                var sa = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "16").Sum(t => t.Field<decimal>("Amount"));
                                txtSewaAlat.Value = sa != null ? Convert.ToDouble(sa) : 0;
                                var pb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "02").Sum(t => t.Field<decimal>("Amount"));
                                txtProsedurBedah.Value = pb != null ? Convert.ToDouble(pb) : 0;
                                var kp = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "05").Sum(t => t.Field<decimal>("Amount"));
                                txtKeperawatan.Value = kp != null ? Convert.ToDouble(kp) : 0;
                                var lb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "08").Sum(t => t.Field<decimal>("Amount"));
                                txtLaboratorium.Value = lb != null ? Convert.ToDouble(lb) : 0;
                                var ko = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "11").Sum(t => t.Field<decimal>("Amount"));
                                txtKamarAkomodasi.Value = ko != null ? Convert.ToDouble(ko + (admin ?? 0)) : 0;
                                var al = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "14").Sum(t => t.Field<decimal>("Amount"));
                                txtAlkes.Value = al != null ? Convert.ToDouble(al) : 0;
                                var kn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "03").Sum(t => t.Field<decimal>("Amount"));
                                txtKonsultasi.Value = kn != null ? Convert.ToDouble(kn) : 0;
                                var pn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "06").Sum(t => t.Field<decimal>("Amount"));
                                txtPenunjang.Value = pn != null ? Convert.ToDouble(pn) : 0;
                                var pd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "09").Sum(t => t.Field<decimal>("Amount"));
                                txtPelayananDarah.Value = pd != null ? Convert.ToDouble(pd) : 0;
                                var ri = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "12").Sum(t => t.Field<decimal>("Amount"));
                                txtRawatIntensifTarif.Value = ri != null ? Convert.ToDouble(ri) : 0;
                                var bh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "15").Sum(t => t.Field<decimal>("Amount"));
                                txtBMHP.Value = bh != null ? Convert.ToDouble(bh) : 0;
                                var ok = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "17").Sum(t => t.Field<decimal>("Amount"));
                                txtObatKronis.Value = ok != null ? Convert.ToDouble(ok) : 0;
                                var om = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "18").Sum(t => t.Field<decimal>("Amount"));
                                txtObatKemoterapi.Value = om != null ? Convert.ToDouble(om) : 0;
                            }

                            chkAsamLaktat.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_asam_laktat");
                            chkProcalcitonin.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_procalcitonin");
                            chkCRP.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_crp");
                            chkKultur.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_kultur");
                            chkDDimer.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_d_dimer");
                            chkPT.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_pt");
                            chkAPTT.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_aptt");
                            chkWaktuPendarahan.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_waktu_pendarahan");
                            chkAntiHIV.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_anti_hiv");
                            chkAnalisaGas.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_analisa_gas");
                            chkAlbumin.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_albumin");
                            chkThoraxAPPA.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "rad_thorax_ap_pa");
                        }
                    }

                    var ncc = new NccInacbg();
                    if (!ncc.LoadByPrimaryKey(Request.QueryString["regno"])) ncc = new NccInacbg();
                    ncc.RegistrationNo = Request.QueryString["regno"];
                    ncc.PatientId = response.Response.Data.PatientId.ToInt();
                    ncc.AdmissionId = response.Response.Data.AdmissionId.ToInt();
                    ncc.HospitalAdmissionId = response.Response.Data.HospitalAdmissionId.ToInt();
                    ncc.LastUpdateDateTime = DateTime.Now;
                    ncc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ncc.AddPaymentAmt = string.IsNullOrEmpty(response.Response.Data.AddPaymentAmt) ? 0 : Convert.ToDecimal(response.Response.Data.AddPaymentAmt);
                    ncc.Save();

                    if (!string.IsNullOrEmpty(data.Diagnosa))
                    {
                        EpisodeDiagnoseCollection coll = new EpisodeDiagnoseCollection();
                        coll.Query.Where(coll.Query.RegistrationNo == Request.QueryString["regno"]);
                        coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
                        coll.Query.Load();

                        foreach (var c in coll.Where(c => !data.Diagnosa.Split('#').Contains(c.DiagnoseID)))
                        {
                            c.MarkAsDeleted();
                        }

                        int idx = 1;
                        foreach (var d in data.Diagnosa.Split('#').Where(d => !coll.Select(c => c.DiagnoseID).Contains(d)))
                        {
                            var di = new Diagnose();
                            di.LoadByPrimaryKey(d);

                            var id = coll.Any() ? coll.Max(c => c.SequenceNo.ToInt()) + 1 : 1;

                            var ed = coll.AddNew();
                            ed.RegistrationNo = Request.QueryString["regno"];
                            ed.SequenceNo = string.Format("{0:000}",id);
                            ed.DiagnoseID = di.DiagnoseID;
                            ed.DiagnoseName = di.DiagnoseName;
                            ed.SRDiagnoseType = idx == 1 ? "DiagnoseType-001" : "DiagnoseType-004";

                            var asri = new AppStandardReferenceItem();
                            asri.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
                            ed.DiagnoseType = asri.ItemName;

                            ed.DiagnoseType = asri.ItemName;
                            ed.DiagnosisText = cboDiagnosa.Text;
                            ed.MorphologyID = string.Empty;
                            ed.ParamedicID = cboDPJP.SelectedValue;
                            ed.IsAcuteDisease = false;
                            ed.IsChronicDisease = di.IsChronicDisease;
                            ed.IsOldCase = false;
                            ed.IsConfirmed = true;
                            ed.IsVoid = false;
                            ed.Notes = string.Empty;
                            ed.LastUpdateDateTime = DateTime.Now;
                            ed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            ed.ExternalCauseID = string.Empty;
                            ed.CreateByUserID = AppSession.UserLogin.UserID;
                            ed.CreateDateTime = DateTime.Now;

                            idx++;
                        }

                        coll.Save();
                    }

                    if (!string.IsNullOrEmpty(data.Procedure))
                    {
                        EpisodeProcedureCollection coll = new EpisodeProcedureCollection();
                        coll.Query.Where(coll.Query.RegistrationNo == Request.QueryString["regno"]);
                        coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
                        coll.Query.Load();

                        foreach (var c in coll.Where(c => !data.Procedure.Split('#').Contains(c.ProcedureID)))
                        {
                            c.MarkAsDeleted();
                        }

                        int idx = 1;
                        foreach (var d in data.Procedure.Split('#').Where(d => !coll.Select(c => c.ProcedureID).Contains(d)))
                        {
                            var proc = new Procedure();
                            proc.LoadByPrimaryKey(d);

                            var id = coll.Any() ? coll.Max(c => c.SequenceNo.ToInt()) + 1 : 1;

                            var ep = coll.AddNew();

                            ep.RegistrationNo = Request.QueryString["regno"];
                            ep.SequenceNo = string.Format("{0:000}", id);

                            ep.ProcedureDate = reg.RegistrationDate;
                            ep.ProcedureTime = reg.RegistrationTime;
                            ep.ProcedureDate2 = reg.RegistrationDate;
                            ep.ProcedureTime2 = reg.RegistrationTime;
                            ep.ParamedicID = string.Empty;
                            ep.ParamedicID2 = string.Empty;
                            ep.ProcedureID = proc.ProcedureID;
                            ep.ProcedureName = proc.ProcedureName;
                            ep.SRProcedureCategory = string.Empty;
                            ep.SRAnestesi = string.Empty;
                            ep.RoomID = string.Empty;
                            ep.IsCito = false;
                            ep.IsVoid = false;
                            ep.LastUpdateDateTime = DateTime.Now;
                            ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            ep.AssistantID1 = string.Empty;
                            ep.AssistantID2 = string.Empty;
                            ep.Notes = string.Empty;
                            ep.BookingNo = string.Empty;
                            ep.ParamedicID2a = string.Empty;
                            ep.ParamedicID3a = string.Empty;
                            ep.ParamedicID4a = string.Empty;
                            ep.ParamedicIDAnestesi = string.Empty;
                            ep.AssistantIDAnestesi = string.Empty;
                            ep.InstrumentatorID1 = string.Empty;
                            ep.InstrumentatorID2 = string.Empty;
                            ep.IsFromOperatingRoom = true;

                            idx++;
                        }

                        coll.Save();
                    }

                    var grouper = response.Response.Data.Grouper;
                    if (grouper.GResponse != null)
                    {
                        var cov = grouper.GResponse.Covid19Data;
                        if (cov != null)
                        {
                            cboNoPeserta.SelectedValue = cov.NoKartuT;
                            cboStatusCov.SelectedValue = cov.Covid19StatusCd;
                            var episodes = cov.Episodes;
                            if (episodes != null && episodes.Any())
                            {
                                foreach (var episode in episodes)
                                {
                                    EpisodeRuangRawatCollection.Add(new EpisodeRuangRawat() { Key = EpisodeRuangRawatCollection.Count() + 1, ID = episode.EpisodeClassCd, Nama = episode.EpisodeClassNm, Jumlah = episode.Los.ToInt() });
                                }
                                grdEpisodeRuangRawat.DataSource = EpisodeRuangRawatCollection;
                                grdEpisodeRuangRawat.DataBind();
                            }
                            var pemulasaraan = cov.PemulasaraanJenazah;
                            if (pemulasaraan != null)
                            {
                                chkPemulasaraanJenazah.Checked = pemulasaraan.Pemulasaraan == "1";
                                chkKantongJenazah.Checked = pemulasaraan.Kantong == "1";
                                chkPetiJenazah.Checked = pemulasaraan.Peti == "1";
                                chkPlastikErat.Checked = pemulasaraan.Plastik == "1";
                                chkDesinfektanJenazah.Checked = pemulasaraan.DesinfektanJenazah == "1";
                                chkTransportMobil.Checked = pemulasaraan.Mobil == "1";
                                chkDesinfektanMobil.Checked = pemulasaraan.DesinfektanMobil == "1";
                            }
                            rblKomorbid.SelectedValue = cov.CcInd;
                        }

                        txtGroupName.Text = grouper.GResponse.Cbg.Description;
                        txtGroupID.Text = grouper.GResponse.Cbg.Code;
                        txtGroupPrice.Value = Convert.ToDouble(grouper.GResponse.Cbg.Tariff);

                        var cmg = grouper.GResponse.SpecialCmg;
                        if (cmg != null)
                        {
                            if (cmg.Any(c => c.Type == "Special Drug"))
                            {
                                cboSpecialDrug.Items.Clear();
                                cboSpecialDrug.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                            }
                            foreach (var sco in cmg)
                            {
                                if (sco.Type == "Special Drug")
                                {
                                    if (!string.IsNullOrWhiteSpace(sco.Code))
                                    {
                                        string code = sco.Code.Split('-')[0] + sco.Code.Split('-')[1];
                                        cboSpecialDrug.Items.Add(new RadComboBoxItem(sco.Description, code));
                                    }
                                }
                            }
                            if (cmg.Any(c => c.Type == "Special Drug"))
                            {
                                if (!string.IsNullOrEmpty(ncc.SpecialDrugID)) cboSpecialDrug.SelectedValue = ncc.SpecialDrugID;
                                if ((ncc.SpecialDrugAmount ?? 0) > 0) txtSpecialDrugPrice.Value = Convert.ToDouble(ncc.SpecialDrugAmount ?? 0);
                            }
                        }

                        ncc = new NccInacbg();
                        if (!ncc.LoadByPrimaryKey(Request.QueryString["regno"])) ncc = new NccInacbg();
                        ncc.CoverageAmount = Convert.ToDecimal(txtGroupPrice.Value);
                        ncc.Save();
                    }

                    txtTambahanBiaya.Value = string.IsNullOrEmpty(response.Response.Data.AddPaymentAmt) ? 0 : Convert.ToDouble(response.Response.Data.AddPaymentAmt);

                    if (AppSession.Parameter.HealthcareInitial != "RSI")
                    {
                        reg = new Registration();
                        reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                        reg.PlavonAmount = Convert.ToDecimal(txtGrouperTotal.Value);
                        reg.Save();
                    }

                    if (response.Response.Data.KlaimStatusCd == "final")
                    {
                        btnGroupper.Enabled = false;
                        btnFinal.Visible = false;
                        btnEdit.Visible = true;
                        btnKirim.Enabled = true;
                    }

                    LoadUploadFile();
                }
                else
                {
                    GetTransactionMode = "new";

                    txtRawatIntensif.Value = 0;
                    txtLamaNaikKelas.Value = 0;
                    txtVentilator.Value = 0;
                    txtBeratLahir.Value = 0;
                    txtTariffPoliEks.Value = 0;
                    txtTerapiKovalesen.Value = 0;

                    txtProsedurNonBedah.Value = 0;
                    txtTenagaAhli.Value = 0;
                    txtRadiologi.Value = 0;
                    txtRehabilitasi.Value = 0;
                    txtObat.Value = 0;
                    txtObatKronis.Value = 0;
                    txtSewaAlat.Value = 0;
                    txtProsedurBedah.Value = 0;
                    txtKeperawatan.Value = 0;
                    txtLaboratorium.Value = 0;
                    txtKamarAkomodasi.Value = 0;
                    txtAlkes.Value = 0;
                    txtKonsultasi.Value = 0;
                    txtPenunjang.Value = 0;
                    txtPelayananDarah.Value = 0;
                    txtRawatIntensifTarif.Value = 0;
                    txtObatKemoterapi.Value = 0;
                    txtBMHP.Value = 0;

                    var ib = new IntermBillCollection();
                    ib.Query.Where(ib.Query.RegistrationNo == Request.QueryString["regno"], ib.Query.IsVoid == false);
                    if (ib.Query.Load())
                    {
                        var admin = ib.Select(b => b.AdministrationAmount + b.GuarantorAdministrationAmount).Sum();

                        var cc = new CostCalculationQuery("a");
                        var i = new ItemQuery("b");

                        cc.Select(i.SREklaimTariffGroup, i.SREklaimFactorGroup, (cc.PatientAmount.Sum() + cc.GuarantorAmount.Sum()).As("Amount"));
                        cc.InnerJoin(i).On(cc.ItemID == i.ItemID);
                        cc.Where(cc.IntermBillNo.In(ib.Select(b => b.IntermBillNo)));
                        cc.GroupBy(i.SREklaimTariffGroup, i.SREklaimFactorGroup);

                        var tbl = cc.LoadDataTable();
                        if (tbl.Rows.Count > 0)
                        {
                            var pnb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "01").Sum(t => t.Field<decimal>("Amount"));
                            txtProsedurNonBedah.Value = pnb != null ? Convert.ToDouble(pnb) : 0;
                            var ta = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "04").Sum(t => t.Field<decimal>("Amount"));
                            txtTenagaAhli.Value = ta != null ? Convert.ToDouble(ta) : 0;
                            var rd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "07").Sum(t => t.Field<decimal>("Amount"));
                            txtRadiologi.Value = rd != null ? Convert.ToDouble(rd) : 0;
                            var rh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "10").Sum(t => t.Field<decimal>("Amount"));
                            txtRehabilitasi.Value = rh != null ? Convert.ToDouble(rh) : 0;
                            var ob = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "13").Sum(t => t.Field<decimal>("Amount"));
                            txtObat.Value = ob != null ? Convert.ToDouble(ob) : 0;
                            var sa = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "16").Sum(t => t.Field<decimal>("Amount"));
                            txtSewaAlat.Value = sa != null ? Convert.ToDouble(sa) : 0;
                            var pb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "02").Sum(t => t.Field<decimal>("Amount"));
                            txtProsedurBedah.Value = pb != null ? Convert.ToDouble(pb) : 0;
                            var kp = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "05").Sum(t => t.Field<decimal>("Amount"));
                            txtKeperawatan.Value = kp != null ? Convert.ToDouble(kp) : 0;
                            var lb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "08").Sum(t => t.Field<decimal>("Amount"));
                            txtLaboratorium.Value = lb != null ? Convert.ToDouble(lb) : 0;
                            var ko = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "11").Sum(t => t.Field<decimal>("Amount"));
                            txtKamarAkomodasi.Value = ko != null ? Convert.ToDouble(ko + (admin ?? 0)) : 0;
                            var al = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "14").Sum(t => t.Field<decimal>("Amount"));
                            txtAlkes.Value = al != null ? Convert.ToDouble(al) : 0;
                            var kn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "03").Sum(t => t.Field<decimal>("Amount"));
                            txtKonsultasi.Value = kn != null ? Convert.ToDouble(kn) : 0;
                            var pn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "06").Sum(t => t.Field<decimal>("Amount"));
                            txtPenunjang.Value = pn != null ? Convert.ToDouble(pn) : 0;
                            var pd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "09").Sum(t => t.Field<decimal>("Amount"));
                            txtPelayananDarah.Value = pd != null ? Convert.ToDouble(pd) : 0;
                            var ri = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "12").Sum(t => t.Field<decimal>("Amount"));
                            txtRawatIntensifTarif.Value = ri != null ? Convert.ToDouble(ri) : 0;
                            var bh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "15").Sum(t => t.Field<decimal>("Amount"));
                            txtBMHP.Value = bh != null ? Convert.ToDouble(bh) : 0;
                            var ok = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "17").Sum(t => t.Field<decimal>("Amount"));
                            txtObatKronis.Value = ok != null ? Convert.ToDouble(ok) : 0;
                            var om = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "18").Sum(t => t.Field<decimal>("Amount"));
                            txtObatKemoterapi.Value = om != null ? Convert.ToDouble(om) : 0;

                            chkAsamLaktat.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_asam_laktat");
                            chkProcalcitonin.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_procalcitonin");
                            chkCRP.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_crp");
                            chkKultur.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_kultur");
                            chkDDimer.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_d_dimer");
                            chkPT.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_pt");
                            chkAPTT.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_aptt");
                            chkWaktuPendarahan.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_waktu_pendarahan");
                            chkAntiHIV.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_anti_hiv");
                            chkAnalisaGas.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_analisa_gas");
                            chkAlbumin.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_albumin");
                            chkThoraxAPPA.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "rad_thorax_ap_pa");
                        }
                    }

                    txtGroupPrice.Value = 0;
                    txtChronicPrice.Value = 0;
                    txtSubAcutePrice.Value = 0;
                    txtSpecialProcedurePrice.Value = 0;
                    txtSpecialProsthesisPrice.Value = 0;
                    txtSpecialInvestigationPrice.Value = 0;
                    txtSpecialDrugPrice.Value = 0;

                    txtTambahanBiaya.Value = 0;
                }
            }
            else
            {
                GetTransactionMode = "new";

                txtRawatIntensif.Value = 0;
                txtLamaNaikKelas.Value = 0;
                txtVentilator.Value = 0;
                txtBeratLahir.Value = 0;
                txtTariffPoliEks.Value = 0;
                txtTerapiKovalesen.Value = 0;

                txtProsedurNonBedah.Value = 0;
                txtTenagaAhli.Value = 0;
                txtRadiologi.Value = 0;
                txtRehabilitasi.Value = 0;
                txtObat.Value = 0;
                txtObatKronis.Value = 0;
                txtSewaAlat.Value = 0;
                txtProsedurBedah.Value = 0;
                txtKeperawatan.Value = 0;
                txtLaboratorium.Value = 0;
                txtKamarAkomodasi.Value = 0;
                txtAlkes.Value = 0;
                txtKonsultasi.Value = 0;
                txtPenunjang.Value = 0;
                txtPelayananDarah.Value = 0;
                txtRawatIntensifTarif.Value = 0;
                txtObatKemoterapi.Value = 0;
                txtBMHP.Value = 0;

                var ib = new IntermBillCollection();
                ib.Query.Where(ib.Query.RegistrationNo == Request.QueryString["regno"], ib.Query.IsVoid == false);
                if (ib.Query.Load())
                {
                    var admin = ib.Select(b => b.AdministrationAmount + b.GuarantorAdministrationAmount).Sum();

                    var cc = new CostCalculationQuery("a");
                    var i = new ItemQuery("b");

                    cc.Select(i.SREklaimTariffGroup, i.SREklaimFactorGroup, (cc.PatientAmount.Sum() + cc.GuarantorAmount.Sum()).As("Amount"));
                    cc.InnerJoin(i).On(cc.ItemID == i.ItemID);
                    cc.Where(cc.IntermBillNo.In(ib.Select(b => b.IntermBillNo)));
                    cc.GroupBy(i.SREklaimTariffGroup, i.SREklaimFactorGroup);

                    var tbl = cc.LoadDataTable();
                    if (tbl.Rows.Count > 0)
                    {
                        var pnb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "01").Sum(t => t.Field<decimal>("Amount"));
                        txtProsedurNonBedah.Value = pnb != null ? Convert.ToDouble(pnb) : 0;
                        var ta = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "04").Sum(t => t.Field<decimal>("Amount"));
                        txtTenagaAhli.Value = ta != null ? Convert.ToDouble(ta) : 0;
                        var rd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "07").Sum(t => t.Field<decimal>("Amount"));
                        txtRadiologi.Value = rd != null ? Convert.ToDouble(rd) : 0;
                        var rh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "10").Sum(t => t.Field<decimal>("Amount"));
                        txtRehabilitasi.Value = rh != null ? Convert.ToDouble(rh) : 0;
                        var ob = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "13").Sum(t => t.Field<decimal>("Amount"));
                        txtObat.Value = ob != null ? Convert.ToDouble(ob) : 0;
                        var sa = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "16").Sum(t => t.Field<decimal>("Amount"));
                        txtSewaAlat.Value = sa != null ? Convert.ToDouble(sa) : 0;
                        var pb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "02").Sum(t => t.Field<decimal>("Amount"));
                        txtProsedurBedah.Value = pb != null ? Convert.ToDouble(pb) : 0;
                        var kp = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "05").Sum(t => t.Field<decimal>("Amount"));
                        txtKeperawatan.Value = kp != null ? Convert.ToDouble(kp) : 0;
                        var lb = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "08").Sum(t => t.Field<decimal>("Amount"));
                        txtLaboratorium.Value = lb != null ? Convert.ToDouble(lb) : 0;
                        var ko = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "11").Sum(t => t.Field<decimal>("Amount"));
                        txtKamarAkomodasi.Value = ko != null ? Convert.ToDouble(ko + (admin ?? 0)) : 0;
                        var al = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "14").Sum(t => t.Field<decimal>("Amount"));
                        txtAlkes.Value = al != null ? Convert.ToDouble(al) : 0;
                        var kn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "03").Sum(t => t.Field<decimal>("Amount"));
                        txtKonsultasi.Value = kn != null ? Convert.ToDouble(kn) : 0;
                        var pn = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "06").Sum(t => t.Field<decimal>("Amount"));
                        txtPenunjang.Value = pn != null ? Convert.ToDouble(pn) : 0;
                        var pd = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "09").Sum(t => t.Field<decimal>("Amount"));
                        txtPelayananDarah.Value = pd != null ? Convert.ToDouble(pd) : 0;
                        var ri = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "12").Sum(t => t.Field<decimal>("Amount"));
                        txtRawatIntensifTarif.Value = ri != null ? Convert.ToDouble(ri) : 0;
                        var bh = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "15").Sum(t => t.Field<decimal>("Amount"));
                        txtBMHP.Value = bh != null ? Convert.ToDouble(bh) : 0;
                        var ok = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "17").Sum(t => t.Field<decimal>("Amount"));
                        txtObatKronis.Value = ok != null ? Convert.ToDouble(ok) : 0;
                        var om = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "18").Sum(t => t.Field<decimal>("Amount"));
                        txtObatKemoterapi.Value = om != null ? Convert.ToDouble(om) : 0;

                        chkAsamLaktat.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_asam_laktat");
                        chkProcalcitonin.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_procalcitonin");
                        chkCRP.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_crp");
                        chkKultur.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_kultur");
                        chkDDimer.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_d_dimer");
                        chkPT.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_pt");
                        chkAPTT.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_aptt");
                        chkWaktuPendarahan.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_waktu_pendarahan");
                        chkAntiHIV.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_anti_hiv");
                        chkAnalisaGas.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_analisa_gas");
                        chkAlbumin.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "lab_albumin");
                        chkThoraxAPPA.Checked = !tbl.AsEnumerable().Any(t => t.Field<string>("SREklaimFactorGroup") == "rad_thorax_ap_pa");
                    }
                }

                txtGroupPrice.Value = 0;
                txtChronicPrice.Value = 0;
                txtSubAcutePrice.Value = 0;
                txtSpecialProcedurePrice.Value = 0;
                txtSpecialProsthesisPrice.Value = 0;
                txtSpecialInvestigationPrice.Value = 0;
                txtSpecialDrugPrice.Value = 0;

                txtTambahanBiaya.Value = 0;
            }

            txtProsedurNonBedah_TextChanged(null, null);
            cboSpecialDrug_SelectedIndexChanged(null, null);
        }

        protected void cboSpecialDrug_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cbo = (o as RadComboBox);

            if (e != null)
            {
                if (!string.IsNullOrEmpty(e.Value))
                {
                    var cmg = string.Empty;

                    var procedure = cboSpecialProcedure.SelectedValue;
                    if (!string.IsNullOrWhiteSpace(procedure)) cmg += cboSpecialProcedure.SelectedValue + "#";
                    var prostesis = cboSpecialProsthesis.SelectedValue;
                    if (!string.IsNullOrWhiteSpace(prostesis)) cmg += cboSpecialProsthesis.SelectedValue + "#";
                    var investigation = cboSpecialInvestigation.SelectedValue;
                    if (!string.IsNullOrWhiteSpace(investigation)) cmg += cboSpecialInvestigation.SelectedValue + "#";
                    var drug = cboSpecialDrug.SelectedValue;
                    if (!string.IsNullOrWhiteSpace(drug)) cmg += cboSpecialDrug.SelectedValue + "#";

                    var svc = new Common.Inacbg.v51.Service();
                    var response = svc.Grouper2(new Temiang.Avicenna.Common.Inacbg.v51.Grouper.Grouper2.Data()
                    {
                        nomor_sep = txtNoSep.Text,
                        special_cmg = cmg
                    });
                    if (response.Metadata.IsValid)
                    {
                        var tariff = response.TarifAlt.Where(r => r.Kelas == response.Response.Kelas).SingleOrDefault();
                        if (tariff != null)
                        {
                            if (cbo.ID == cboSpecialDrug.ID)
                                txtSpecialDrugPrice.Value = Convert.ToDouble(tariff.TarifSd);
                            else if (cbo.ID == cboSpecialProcedure.ID)
                                txtSpecialProcedurePrice.Value = Convert.ToDouble(tariff.TarifSp);
                            else if (cbo.ID == cboSpecialInvestigation.ID)
                                txtSpecialInvestigationPrice.Value = Convert.ToDouble(tariff.TarifSi);
                            else if (cbo.ID == cboSpecialProsthesis.ID)
                                txtSpecialProsthesisPrice.Value = Convert.ToDouble(tariff.TarifSr);

                            cbo.Focus();
                        }
                        else
                        {
                            txtSpecialDrugPrice.Value = 0;
                            txtSpecialProcedurePrice.Value = 0;
                            txtSpecialInvestigationPrice.Value = 0;
                            txtSpecialProsthesisPrice.Value = 0;
                        }
                    }
                    else
                    {
                        txtSpecialDrugPrice.Value = 0;
                        txtSpecialProcedurePrice.Value = 0;
                        txtSpecialInvestigationPrice.Value = 0;
                        txtSpecialProsthesisPrice.Value = 0;
                    }
                }
                else
                {
                    txtSpecialDrugPrice.Value = 0;
                    txtSpecialProcedurePrice.Value = 0;
                    txtSpecialInvestigationPrice.Value = 0;
                    txtSpecialProsthesisPrice.Value = 0;
                }

                var ncc = new NccInacbg();
                ncc.LoadByPrimaryKey(Request.QueryString["regno"]);
                ncc.SpecialDrugID = cboSpecialDrug.SelectedValue;
                ncc.SpecialDrugAmount = Convert.ToDecimal(txtSpecialDrugPrice.Value ?? 0);
                ncc.SpecialProcedureID = cboSpecialProcedure.SelectedValue;
                ncc.SpecialProcedureAmount = Convert.ToDecimal(txtSpecialProcedurePrice.Value ?? 0);
                ncc.SpecialInvestigationID = cboSpecialInvestigation.SelectedValue;
                ncc.SpecialInvestigationAmount = Convert.ToDecimal(txtSpecialInvestigationPrice.Value ?? 0);
                ncc.SpecialProsthesisID = cboSpecialProsthesis.SelectedValue;
                ncc.SpecialProsthesisAmount = Convert.ToDecimal(txtSpecialProsthesisPrice.Value ?? 0);
                ncc.Save();
            }

            txtGrouperTotal.Value = (txtGroupPrice.Value ?? 0) + (txtChronicPrice.Value ?? 0) +
                                    (txtSubAcutePrice.Value ?? 0) +
                                    (txtSpecialProcedurePrice.Value ?? 0) + (txtSpecialProsthesisPrice.Value ?? 0) +
                                    (txtSpecialInvestigationPrice.Value ?? 0) + (txtSpecialDrugPrice.Value ?? 0);

            if (AppSession.Parameter.HealthcareInitial != "RSI")
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                //if (txtTarifRS.Value < txtGrouperTotal.Value) reg.PlavonAmount = Convert.ToDecimal(txtTarifRS.Value);
                //else 
                reg.PlavonAmount = Convert.ToDecimal(txtGrouperTotal.Value);
                reg.Save();
            }
        }

        private void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var medicalNo = !string.IsNullOrWhiteSpace(AppSession.Parameter.EklaimRemoveDashSeparatorOnMedicalNo) && AppSession.Parameter.EklaimRemoveDashSeparatorOnMedicalNo.ToLower() == "yes" ? txtNoMR.Text.Replace("-", string.Empty) : txtNoMR.Text;
            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "YBRSGKP":
                    var pat = new Patient();
                    pat.LoadByMedicalNo(txtNoMR.Text);
                    if (!string.IsNullOrEmpty(pat.OldMedicalNo)) medicalNo = medicalNo.ToInt().ToString();
                    medicalNo = medicalNo.ToInt().ToString();
                    break;
            }

            var svc = new Common.Inacbg.v51.Service();
            var response = svc.Update(new Common.Inacbg.v51.Patient.Update.Data()
            {
                nomor_kartu = txtNoPeserta.Text,
                nomor_rm = medicalNo,
                nama_pasien = txtNamaPasien.Text,
                tgl_lahir = txtTglLahir.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                gender = (rblJenisKelamin.SelectedValue == "M" ? "1" : "2")
            });
            if (!response.Metadata.IsValid)
            {
                args.MessageText = string.Format("{0} - {1}", response.Metadata.ErrorNo, response.Metadata.Message);
                args.IsCancel = true;
            }
            else
            {
                var episode = string.Empty;
                foreach (GridDataItem dataItem in grdEpisodeRuangRawat.MasterTableView.Items)
                {
                    var id = dataItem["ID"].Text;
                    var jumlah = ((RadNumericTextBox)dataItem.FindControl("txtJumlah")).Value;
                    episode += string.Format("{0};{1}", id, jumlah.ToInt().ToString()) + "#";
                }

                var diag = string.Empty;
                foreach (var d in (ViewState["icd10"] as EpisodeDiagnoseCollection).Where(v => !string.IsNullOrWhiteSpace(v.DiagnoseID)).OrderBy(e => e.SRDiagnoseType))
                {
                    diag += d.DiagnoseID + "#";
                }
                if (string.IsNullOrEmpty(diag)) diag = "#";

                var proc = string.Empty;
                foreach (var d in (ViewState["icd9cm"] as EpisodeProcedureCollection).Where(v => !string.IsNullOrWhiteSpace(v.ProcedureID)))
                {
                    proc += d.ProcedureID + "#";
                }
                if (string.IsNullOrEmpty(proc)) proc = "#";

                var svc54 = new Common.Inacbg.v58.Service();
                var param = new Common.Inacbg.v58.Detail.Data()
                {
                    nomor_sep = txtNoSep.Text,
                    nomor_kartu = txtNoPeserta.Text,
                    tgl_masuk = txtTglMasuk.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    tgl_pulang = txtTglPulang.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    cara_masuk = cboCaraMasuk.SelectedValue,
                    jenis_rawat = rblJenisRawat.SelectedValue.Split('|')[1],
                    kelas_rawat = rblJenisRawat.SelectedValue.Split('|')[1] == "1"
                        ? rblKelasRawat.SelectedValue.Split('|')[1]
                        : rblKelasRawat.SelectedValue,
                    adl_sub_acute = txtSubAcute.Text,
                    adl_chronic = txtChronic.Text,
                    icu_indikator = chkRawatIntensif.Checked ? "1" : "0",
                    icu_los = chkRawatIntensif.Checked ? txtRawatIntensif.Value.ToInt().ToString() : "0",
                    ventilator_hour = txtVentilator.Value.ToInt().ToString(),

                    use_ind = chkVentilator.Checked ? "1" : "0",
                    start_dttm = chkVentilator.Checked
                        ? $"{txtTglIntubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamIntubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}"
                        : string.Empty,
                    stop_dttm = chkVentilator.Checked
                        ? $"{txtTglEkstubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamEkstubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}"
                        : string.Empty,

                    upgrade_class_ind = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? "0"
                        : (chkNaikKelas.Checked ? "1" : "0"),
                    upgrade_class_class = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? string.Empty
                        : rblNaikKelasRawat.SelectedValue.Split('|')[1],
                    upgrade_class_los = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? string.Empty
                        : txtLamaNaikKelas.Value.ToInt().ToString(),
                    upgrade_class_payor = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue)
                        ? string.Empty
                        : cboPenjaminNaikKelas.SelectedValue,
                    add_payment_pct = txtSelisihPersen.Visible ? (txtSelisihPersen.Value ?? 0).ToString() : "0",
                    birth_weight = txtBeratLahir.Value.ToInt().ToString(),
                    sistole = txtSistole.Value.ToString(),
                    diastole = txtDiastole.Value.ToString(),
                    discharge_status = cboCaraPulang.SelectedValue,
                    diagnosa = diag,
                    procedure = proc,
                    tarif_rs = new Common.Inacbg.v54.Detail.TarifRs()
                    {
                        prosedur_non_bedah = txtProsedurNonBedah.Value.ToInt().ToString(),
                        prosedur_bedah = txtProsedurBedah.Value.ToInt().ToString(),
                        konsultasi = txtKonsultasi.Value.ToInt().ToString(),
                        tenaga_ahli = txtTenagaAhli.Value.ToInt().ToString(),
                        keperawatan = txtKeperawatan.Value.ToInt().ToString(),
                        penunjang = txtPenunjang.Value.ToInt().ToString(),
                        radiologi = txtRadiologi.Value.ToInt().ToString(),
                        laboratorium = txtLaboratorium.Value.ToInt().ToString(),
                        pelayanan_darah = txtPelayananDarah.Value.ToInt().ToString(),
                        rehabilitasi = txtRehabilitasi.Value.ToInt().ToString(),
                        kamar = txtKamarAkomodasi.Value.ToInt().ToString(),
                        rawat_intensif = txtRawatIntensifTarif.Value.ToInt().ToString(),
                        obat = txtObat.Value.ToInt().ToString(),
                        obat_kronis = txtObatKronis.Value.ToInt().ToString(),
                        obat_kemoterapi = txtObatKemoterapi.Value.ToInt().ToString(),
                        alkes = txtAlkes.Value.ToInt().ToString(),
                        bmhp = txtBMHP.Value.ToInt().ToString(),
                        sewa_alat = txtSewaAlat.Value.ToInt().ToString()
                    },
                    pemulasaraan_jenazah = chkPemulasaraanJenazah.Checked ? "1" : "0",
                    kantong_jenazah = chkKantongJenazah.Checked ? "1" : "0",
                    peti_jenazah = chkPetiJenazah.Checked ? "1" : "0",
                    plastik_erat = chkPlastikErat.Checked ? "1" : "0",
                    desinfektan_jenazah = chkDesinfektanJenazah.Checked ? "1" : "0",
                    mobil_jenazah = chkTransportMobil.Checked ? "1" : "0",
                    desinfektan_mobil_jenazah = chkDesinfektanMobil.Checked ? "1" : "0",
                    covid19_status_cd = cboStatusCov.SelectedValue,
                    nomor_kartu_t = cboNoPeserta.SelectedValue,
                    episodes = episode,
                    covid19_cc_ind = rblKomorbid.SelectedValue,
                    covid19_rs_darurat_ind = rblRsDaruratLapangan.SelectedValue, //
                    covid19_co_insidense_ind = rblCoInsidens.SelectedValue, //
                    covid19_penunjang_pengurang = new Common.Inacbg.v54.Detail.Covid19PenunjangPengurang()
                    {
                        lab_asam_laktat = chkAsamLaktat.Checked ? "1" : "0", //
                        lab_procalcitonin = chkProcalcitonin.Checked ? "1" : "0", //
                        lab_crp = chkCRP.Checked ? "1" : "0", //
                        lab_kultur = chkKultur.Checked ? "1" : "0", //
                        lab_d_dimer = chkDDimer.Checked ? "1" : "0", //
                        lab_pt = chkPT.Checked ? "1" : "0", //
                        lab_aptt = chkAPTT.Checked ? "1" : "0", //
                        lab_waktu_pendarahan = chkWaktuPendarahan.Checked ? "1" : "0", //
                        lab_anti_hiv = chkAntiHIV.Checked ? "1" : "0", //
                        lab_analisa_gas = chkAnalisaGas.Checked ? "1" : "0", //
                        lab_albumin = chkAlbumin.Checked ? "1" : "0", //
                        rad_thorax_ap_pa = chkThoraxAPPA.Checked ? "1" : "0" //
                    },
                    terapi_konvalesen = txtTerapiKovalesen.Value.ToInt().ToString(), //
                    akses_naat = rblKriteriaAksesNaat.SelectedValue, //
                    isoman_ind = rblIsolasiMandiri.SelectedValue, //
                    bayi_lahir_status_cd = cboStatusKelainan.SelectedValue, //
                    dializer_single_use = cboDializer.SelectedValue,
                    kantong_darah = txtKantongDarah.Value > 0 ? txtKantongDarah.Value.ToString() : string.Empty,
                    menit_1_appearance = txt1Appearance.Value > 0 ? txt1Appearance.Value.ToString() : string.Empty,
                    menit_1_pulse = txt1Pulse.Value > 0 ? txt1Pulse.Value.ToString() : string.Empty,
                    menit_1_grimace = txt1Grimace.Value > 0 ? txt1Grimace.Value.ToString() : string.Empty,
                    menit_1_activity = txt1Activity.Value > 0 ? txt1Activity.Value.ToString() : string.Empty,
                    menit_1_respiration = txt1Respiration.Value > 0 ? txt1Respiration.Value.ToString() : string.Empty,
                    menit_5_appearance = txt5Appearance.Value > 0 ? txt5Appearance.Value.ToString() : string.Empty,
                    menit_5_pulse = txt5Pulse.Value > 0 ? txt5Pulse.Value.ToString() : string.Empty,
                    menit_5_grimace = txt5Grimace.Value > 0 ? txt5Grimace.Value.ToString() : string.Empty,
                    menit_5_activity = txt5Activity.Value > 0 ? txt5Activity.Value.ToString() : string.Empty,
                    menit_5_respiration = txt5Respiration.Value > 0 ? txt5Respiration.Value.ToString() : string.Empty,
                    usia_kehamilan = txtUsiaKehamilan.Value > 0 ? txtUsiaKehamilan.Value.ToString() : string.Empty,
                    gravida = txtGravida.Value > 0 ? txtGravida.Value.ToString() : string.Empty,
                    partus = txtPartus.Value > 0 ? txtPartus.Value.ToString() : string.Empty,
                    abortus = txtAbortus.Value > 0 ? txtAbortus.Value.ToString() : string.Empty,
                    onset_kontraksi = cboOnsetKontraksi.SelectedValue,
                    delivery = Deliveries.Any() ? JsonConvert.SerializeObject(Deliveries) : string.Empty,

                    tarif_poli_eks = txtTariffPoliEks.Value.ToInt().ToString(),
                    nama_dokter = cboDPJP.Text,
                    kode_tarif = cboJenisTariff.SelectedValue,
                    payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    payor_cd = cboJaminan.SelectedValue.Split('|')[2],
                    cob_cd = string.IsNullOrEmpty(cboCOB.SelectedValue)
                        ? string.Empty
                        : cboCOB.SelectedValue.Split('|')[1],
                    coder_nik = string.IsNullOrWhiteSpace(AppSession.UserLogin.LicenseNo)
                        ? ConfigurationManager.AppSettings["InacbgUserID"]
                        : AppSession.UserLogin.LicenseNo
                };
                var detail = svc54.Insert(param);
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = "Eklaim Bridging - Insert",
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(detail),
                    Totalms = 0
                };
                log.Save();
                if (!detail.Metadata.IsValid)
                {
                    args.MessageText = string.Format("{0} - {1}", detail.Metadata.ErrorNo, detail.Metadata.Message);
                    args.IsCancel = true;
                }
            }
        }

        private void OnMenuDeleteClick(ValidateArgs args)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var patient = new Patient();
            patient.LoadByPrimaryKey(reg.PatientID);

            var svc = new Common.Inacbg.v51.Service();
            var param = new Common.Inacbg.v51.Patient.Delete.Data()
            {
                nomor_rm = patient.MedicalNo,
                coder_nik = string.IsNullOrWhiteSpace(AppSession.UserLogin.LicenseNo)
                    ? ConfigurationManager.AppSettings["InacbgUserID"]
                    : AppSession.UserLogin.LicenseNo
            };
            var response = svc.Delete(param);
            var log = new WebServiceAPILog
            {
                DateRequest = DateTime.Now,
                IPAddress = "Eklaim Bridging - Delete",
                UrlAddress = string.Empty,
                Params = JsonConvert.SerializeObject(param),
                Response = JsonConvert.SerializeObject(response),
                Totalms = 0
            };
            log.Save();
            if (response.Metadata.IsValid)
            {
                var result = response.Response;
                var ncc = new NccInacbg();
                ncc.LoadByPrimaryKey(Request.QueryString["regno"]);
                ncc.MarkAsDeleted();
                ncc.Save();
            }
            else
            {
                args.MessageText = string.Format("{0} - {1}", response.Metadata.ErrorNo, response.Metadata.Message);
                args.IsCancel = true;
            }
        }

        protected void btnGroupper_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);
            reg.GuarantorCardNo = txtNoPeserta.Text;
            reg.BpjsSepNo = txtNoSep.Text;
            reg.Save();

            var args = new ValidateArgs();
            if (GetTransactionMode == "new") OnBeforeMenuNewClick(args);
            else if (GetTransactionMode == "edit") OnBeforeMenuEditClick(args);
            if (args.IsCancel)
            {
                ShowInformationHeader(args.MessageText);
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var response = svc.Grouper1(new Common.Inacbg.v51.Grouper.Grouper1.Data() { nomor_sep = txtNoSep.Text });
            if (response.Metadata.IsValid)
            {
                var data = response.Response;
                if (data != null)
                {
                    txtInfo.Text = data.InacbgVersion;
                    txtJenisRawat.Text = data.Kelas;
                }
                else
                {
                    ShowInformationHeader(string.Format("Grouping No. SEP : {0} gagal.", txtNoSep.Text));
                    return;
                };

                var cbg = data.Cbg;
                if (cbg != null)
                {
                    txtGroupName.Text = cbg.Description;
                    txtGroupID.Text = cbg.Code;
                    txtGroupPrice.Value = Convert.ToDouble(cbg.Tariff);
                }
                var chronic = data.Chronic;
                if (chronic != null)
                {
                    txtChronicName.Text = chronic.Description;
                    txtChronicID.Text = chronic.Code;
                    txtChronicPrice.Value = Convert.ToDouble(chronic.Tariff);

                }
                var acute = data.SubAcute;
                if (acute != null)
                {
                    txtSubAcute.Text = acute.Description;
                    txtSubAcute.Text = acute.Code;
                    txtSubAcutePrice.Value = Convert.ToDouble(acute.Tariff);
                }

                var cmg = response.SpecialCmgOption;
                if (cmg != null)
                {
                    if (cmg.Any(c => c.Type == "Special Drug"))
                    {
                        cboSpecialDrug.Items.Clear();
                        cboSpecialDrug.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    }
                    if (cmg.Any(c => c.Type == "Special Procedure"))
                    {
                        cboSpecialProcedure.Items.Clear();
                        cboSpecialProcedure.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    }
                    if (cmg.Any(c => c.Type == "Special Investigation"))
                    {
                        cboSpecialInvestigation.Items.Clear();
                        cboSpecialInvestigation.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    }
                    if (cmg.Any(c => c.Type == "Special Prosthesis"))
                    {
                        cboSpecialProsthesis.Items.Clear();
                        cboSpecialProsthesis.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    }
                    foreach (var sco in cmg)
                    {
                        if (sco.Type == "Special Drug") cboSpecialDrug.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                        if (sco.Type == "Special Procedure") cboSpecialProcedure.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                        if (sco.Type == "Special Investigation") cboSpecialInvestigation.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                        if (sco.Type == "Special Prosthesis") cboSpecialProsthesis.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                    }
                }

                var alt = response.TarifAlt;

                cboSpecialDrug_SelectedIndexChanged(null, null);

                txtTambahanBiaya.Value = string.IsNullOrEmpty(data.AddPaymentAmt) ? 0 : Convert.ToDouble(data.AddPaymentAmt);

                var ncc = new NccInacbg();
                if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
                {
                    ncc.AddPaymentAmt = string.IsNullOrEmpty(data.AddPaymentAmt) ? 0 : Convert.ToDecimal(data.AddPaymentAmt);
                    ncc.CoverageAmount = Convert.ToDecimal(txtGrouperTotal.Value);
                    ncc.CbgID = txtGroupID.Text;
                    ncc.CbgName = txtGroupName.Text;

                    //if (alt != null || alt.Any())
                    //{
                    //    foreach (var a in alt)
                    //    {

                    //    }
                    //}
                    ncc.Save();
                }

                if (AppSession.Parameter.HealthcareInitial != "RSI")
                {
                    //-------------------------------------------
                    //update ke registrasi dan verifikasi billing
                    //-------------------------------------------
                    reg = new Registration();
                    reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                    //if (txtTarifRS.Value < txtGrouperTotal.Value) reg.PlavonAmount = Convert.ToDecimal(txtTarifRS.Value);
                    //else 
                    reg.PlavonAmount = Convert.ToDecimal(txtGrouperTotal.Value);
                    reg.Save();
                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "grouper", string.Format("alert('Grouping No. SEP : {0} berhasil.');", txtNoSep.Text), true);
            }

            if (GetTransactionMode == "new")
            {
                var diags = (ViewState["icd10"] as EpisodeDiagnoseCollection);
                foreach (var diag in diags)
                {
                    diag.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    diag.LastUpdateDateTime = DateTime.Now;
                }
                diags.Save();

                var procs = (ViewState["icd9cm"] as EpisodeProcedureCollection);
                foreach (var proc in procs)
                {
                    proc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    proc.LastUpdateDateTime = DateTime.Now;
                }
                procs.Save();
            }

            GetTransactionMode = "edit";

            btnGroupper.Focus();
        }

        protected void txtProsedurNonBedah_TextChanged(object sender, EventArgs e)
        {
            txtTarifRS.Value = txtProsedurNonBedah.Value + txtTenagaAhli.Value + txtRadiologi.Value + txtRehabilitasi.Value + txtObat.Value + txtSewaAlat.Value +
                txtProsedurBedah.Value + txtKeperawatan.Value + txtLaboratorium.Value + txtKamarAkomodasi.Value + txtAlkes.Value +
                txtKonsultasi.Value + txtPenunjang.Value + txtPelayananDarah.Value + txtRawatIntensifTarif.Value + txtBMHP.Value + txtObatKronis.Value + txtObatKemoterapi.Value;
        }

        protected void btnFinal_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var param = new Common.Inacbg.v51.Claim.Final.Data()
            {
                nomor_sep = txtNoSep.Text,
                coder_nik = string.IsNullOrWhiteSpace(AppSession.UserLogin.LicenseNo)
                    ? ConfigurationManager.AppSettings["InacbgUserID"]
                    : AppSession.UserLogin.LicenseNo
            };
            var response = svc.Final(param);
            var log = new WebServiceAPILog
            {
                DateRequest = DateTime.Now,
                IPAddress = "Eklaim Bridging - Final",
                UrlAddress = string.Empty,
                Params = JsonConvert.SerializeObject(param),
                Response = JsonConvert.SerializeObject(response),
                Totalms = 0
            };
            log.Save();
            if (response != null)
            {
                if (response.Metadata.IsValid)
                {
                    btnGroupper.Enabled = false;
                    btnFinal.Visible = false;
                    btnEdit.Visible = true;
                    btnKirim.Enabled = true;

                    var ncc = new NccInacbg();
                    if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
                    {
                        ncc.CbgStatus = "final";
                        ncc.Save();
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "final", string.Format("alert('Status klaim No. SEP : {0} = FINAL.');", txtNoSep.Text), true);
                }
                else ScriptManager.RegisterStartupScript(this, GetType(), "final", string.Format("alert('Code : {0}, Message : {1}');", response.Metadata.Code, response.Metadata.Message), true);

                btnFinal.Focus();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var response = svc.Edit(new Common.Inacbg.v51.Claim.Edit.Data() { nomor_sep = txtNoSep.Text });
            if (response != null)
            {
                if (response.Metadata.IsValid)
                {
                    btnGroupper.Enabled = true;
                    btnFinal.Visible = true;
                    btnEdit.Visible = false;
                    btnKirim.Enabled = false;

                    if (AppSession.Parameter.HealthcareID != "RSI")
                    {
                        //void guarantor a / r
                        if (GuarantorReceipts.Rows.Count > 0)
                        {
                            var paymentNo = GuarantorReceipts.AsEnumerable().Select(g => g.Field<string>("PaymentNo")).Single();

                            //cek invoice
                            var invcoll = new InvoicesItemCollection();
                            var invit = new InvoicesItemQuery("a");
                            var inv = new InvoicesQuery("b");
                            invit.InnerJoin(inv).On(invit.InvoiceNo == inv.InvoiceNo && inv.IsInvoicePayment == false && inv.IsVoid == false);
                            invit.Where(invit.PaymentNo == paymentNo);
                            invcoll.Load(invit);

                            bool allowVoid = invcoll.Count <= 0;

                            if (!allowVoid)
                            {
                                ShowInformationHeader("This transaction has been proceed to Invoice. If you still want to void this data, please void Invoice first.");
                                return;
                            }

                            // cek sudah tarik ke jasmed (jasmed by dischargedate) atau belum 
                            var msg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(paymentNo, true);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                ShowInformationHeader(msg);
                                return;
                            }

                            var etp = new TransPayment();
                            if (etp.LoadByPrimaryKey(paymentNo))
                            {
                                using (var trans = new esTransactionScope())
                                {
                                    etp.IsApproved = false;
                                    etp.IsVoid = true;
                                    etp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    etp.LastUpdateDateTime = DateTime.Now;

                                    etp.VoidByUserID = AppSession.UserLogin.UserID;
                                    etp.VoidDate = DateTime.Now;

                                    var collibguar = new TransPaymentItemIntermBillGuarantorCollection();
                                    collibguar.Query.Where(collibguar.Query.PaymentNo == etp.PaymentNo);
                                    collibguar.LoadAll();

                                    foreach (var item in collibguar)
                                    {
                                        item.IsPaymentProceed = false;
                                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        item.LastUpdateDateTime = DateTime.Now;
                                    }

                                    var collib = new TransPaymentItemIntermBillCollection();
                                    collib.Query.Where(collib.Query.PaymentNo == etp.PaymentNo);
                                    collib.LoadAll();

                                    foreach (var item in collib)
                                    {
                                        item.IsPaymentProceed = false;
                                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        item.LastUpdateDateTime = DateTime.Now;
                                    }

                                    var colltpio = new TransPaymentItemOrderCollection();
                                    colltpio.Query.Where(colltpio.Query.PaymentNo == etp.PaymentNo);
                                    colltpio.LoadAll();
                                    foreach (var item in colltpio)
                                    {
                                        item.IsPaymentProceed = false;
                                        item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        item.LastUpdateDateTime = DateTime.Now;
                                    }

                                    var collbuffer = new CostCalculationBufferCollection();
                                    collbuffer.Query.Where(collbuffer.Query.PaymentNo == etp.PaymentNo);
                                    collbuffer.LoadAll();

                                    foreach (var item in collbuffer)
                                    {
                                        item.PaymentNo = null;
                                    }

                                    var collac = new AskesCovered2Collection();
                                    collac.Query.Where(collac.Query.PaymentNo == etp.PaymentNo);
                                    collac.LoadAll();

                                    foreach (var item in collac)
                                    {
                                        item.PaymentNo = null;
                                        item.IsPaid = false;
                                    }

                                    var colltpi = new TransPaymentItemCollection();
                                    colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo);
                                    colltpi.LoadAll();

                                    var total = colltpi.Sum(detail => (decimal)detail.Amount);

                                    var reg = new Registration();
                                    reg.LoadByPrimaryKey(etp.RegistrationNo);
                                    reg.RemainingAmount += total;

                                    var collDP = new TransPaymentCollection();
                                    collDP.Query.Where(collDP.Query.PaymentReferenceNo.Equal(etp.PaymentNo));
                                    collDP.LoadAll();
                                    foreach (var dp in collDP)
                                    {
                                        dp.PaymentReferenceNo = string.Empty;
                                        dp.ReceiptIsReturned = null;
                                        dp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        dp.LastUpdateDateTime = DateTime.Now;
                                    }

                                    // unset payment jasmed
                                    var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                                    feeColl.UnSetPayment(etp, AppSession.UserLogin.UserID);

                                    //using (esTransactionScope trans = new esTransactionScope())
                                    //{
                                    etp.Save();
                                    reg.Save();
                                    collib.Save();
                                    collibguar.Save();
                                    colltpio.Save();
                                    collbuffer.Save();
                                    collac.Save();
                                    collDP.Save();
                                    feeColl.Save();

                                    if (AppSession.Parameter.IsUsingIntermBill)
                                    {

                                        int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP", etp.PaymentNo, AppSession.UserLogin.UserID, 0);
                                    }
                                    else
                                    {

                                        int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP", AppSession.UserLogin.UserID, 0);
                                    }

                                    //if (AppSession.Parameter.IsPhysicianFeeArCreateOnArReceipt == "Yes")
                                    //{
                                    //    int? x = ParamedicFeeTransChargesItemCompSettled.DeleteSettled(etp, false);
                                    //}

                                    #region Guarantor Deposit Balance

                                    colltpi = new TransPaymentItemCollection();
                                    colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo, colltpi.Query.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR);
                                    colltpi.LoadAll();
                                    if (colltpi.Count > 0)
                                    {
                                        total = colltpi.Sum(detail => (decimal)detail.Amount);

                                        var balance = new GuarantorDepositBalance();
                                        var movement = new GuarantorDepositMovement();
                                        GuarantorDepositBalance.PrepareGuarantorDepositBalances(etp.PaymentNo, etp.GuarantorID, "A/R Process (Void)", AppSession.UserLogin.UserID, total, 0, ref balance, ref movement);
                                        balance.Save();
                                        movement.Save();
                                    }

                                    #endregion

                                    trans.Complete();
                                }
                            }
                        }
                    }

                    var ncc = new NccInacbg();
                    if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
                    {
                        ncc.CbgStatus = "normal";
                        ncc.Save();
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "final", string.Format("alert('Status klaim No. SEP : {0} = NORMAL.');", txtNoSep.Text), true);
                }
                else ScriptManager.RegisterStartupScript(this, GetType(), "edit", string.Format("alert('Code : {0}, Message : {1}');", response.Metadata.Code, response.Metadata.Message), true);
            }

            btnEdit.Focus();
        }

        protected void btnKirim_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var response = svc.Send(new Common.Inacbg.v51.Claim.Create.Data() { nomor_sep = txtNoSep.Text });
            if (response.Metadata.IsValid)
            {
                foreach (var data in response.Response.Data)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "send", string.Format("alert('Pengiriman data No. SEP : {0} berhasil.');", data.NoSep), true);

                    var ncc = new NccInacbg();
                    if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
                    {
                        ncc.CbgSentStatus = data.KemkesDcStatus;
                        ncc.Save();
                    }
                }
            }
            else ScriptManager.RegisterStartupScript(this, GetType(), "send", string.Format("alert('Code : {0}, Message : {1}');", response.Metadata.Code, response.Metadata.Message), true);

            btnKirim.Focus();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var response = svc.GetDetail(new Common.Inacbg.v51.Claim.Get.GetDetail.Data() { nomor_sep = txtNoSep.Text });
            if (response.Metadata.IsValid)
            {
                if (response.DataResponse.Data.KlaimStatusCd != "final")
                {
                    ShowInformationHeader("Status klaim belum final.");
                    return;
                }

                svc = new Common.Inacbg.v51.Service();
                var print = svc.Print(new Temiang.Avicenna.Common.Inacbg.v51.Claim.Create.Data() { nomor_sep = txtNoSep.Text });
                if (print.Metadata.IsValid) ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("<script language='javascript' type='text/javascript'>openPrint('{0}');</script>", txtNoSep.Text), true);
                else ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("alert('{0} - {1}');", print.Metadata.Code, print.Metadata.Message), true);

                btnPrint.Focus();
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("alert('{0} - {1}');", "000", "Test"), true);
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oWnd.argument.mode = '{0}'", Request.QueryString["regno"]);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if ((sourceControl is Button) && (eventArgument == "payment")) btnFinal_Click(null, null);
        }

        private DataTable GuarantorReceipts
        {
            get
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var srPaymentType = string.IsNullOrEmpty(grr.SRPaymentType) ? AppSession.Parameter.PaymentTypeCorporateAR : grr.SRPaymentType;

                var query = new TransPaymentQuery("a");
                var detail = new TransPaymentItemQuery("b");
                var guar = new GuarantorQuery("c");

                query.InnerJoin(detail).On(query.PaymentNo == detail.PaymentNo && query.IsApproved == true && query.IsVoid == false && query.TransactionCode == TransactionCode.Payment);
                query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);

                query.es.Top = 1;
                query.Select
                    (
                        query.PaymentNo,
                        query.PaymentDate,
                        query.PaymentTime,
                        query.GuarantorID,
                        guar.GuarantorName,
                        detail.Amount.Sum().As("PaymentAmount"),
                        @"<ISNULL((SELECT SUM(x.Amount) FROM TransPaymentItem x WHERE x.PaymentNo = a.PaymentNo AND x.SRPaymentType = 'PaymentType-005'), 0) AS DiscountAmount>",
                        query.IsApproved,
                        query.IsVoid,
                        query.Notes,
                        query.LastUpdateByUserID,
                        "<'' AS InvoiceNo>",
                        "<CAST(0 AS BIT) AS IsProceed>"
                    );
                query.GroupBy(
                    query.PaymentNo,
                    query.PaymentDate,
                    query.PaymentTime,
                    query.GuarantorID,
                    guar.GuarantorName,
                    query.IsApproved,
                    query.IsVoid,
                    query.Notes,
                    query.LastUpdateByUserID
                    );

                query.Where(query.RegistrationNo.In(MergeRegistrationList()), query.IsApproved == true, query.IsVoid == false, detail.SRPaymentType == srPaymentType);
                query.OrderBy(query.PaymentNo.Ascending);

                DataTable tbl = query.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    var c = new InvoicesItemCollection();
                    var qd = new InvoicesItemQuery("a");
                    var qh = new InvoicesQuery("b");
                    qd.InnerJoin(qh).On(qd.InvoiceNo == qh.InvoiceNo && qh.IsVoid == false && qh.IsInvoicePayment == false);
                    qd.Where(qd.PaymentNo == row["PaymentNo"].ToString());
                    if (c.Load(qd)) row["IsProceed"] = true;
                }

                tbl.AcceptChanges();

                return tbl;
            }
        }

        private string[] MergeRegistrationList()
        {
            return Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"]);
        }

        public string IsReceiptAvalilable
        {
        
             get {
                    if (AppSession.Parameter.HealthcareID != "RSI") 
                    return GuarantorReceipts.Rows.Count.ToString();
            else
            return "0";
            }
        }

        public bool IsFinal
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtNoSep.Text)) return false;

                    var svc = new Common.Inacbg.v51.Service();
                    var response = svc.GetDetail(new Common.Inacbg.v51.Claim.Get.GetDetail.Data() { nomor_sep = txtNoSep.Text });
                    if (response.Metadata.IsValid)
                    {
                        if (response.DataResponse.Data.KlaimStatusCd != "final") return true;
                        else return false;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        protected void cboJaminan_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var value = e.Value.Split('|')[1];
            if (value == "71")
            {
                if (GetTransactionMode == "new")
                {
                    var svc = new Common.Inacbg.v54.Service();
                    var response = svc.Generate(new Common.Inacbg.v54.Claim.Generate.Data() { payor_id = value });
                    if (response.Metadata.IsValid) txtNoSep.Text = response.ResponseData.ClaimNumber;
                    else ScriptManager.RegisterStartupScript(this, GetType(), "generate", string.Format("alert('Code : {0}, Message : {1}');", response.Metadata.Code, response.Metadata.Message), true);
                }
                cboEpisode.Enabled = (rblJenisRawat.SelectedValue.Split('|')[0] == AppConstant.RegistrationType.InPatient);
            }
            else if (value == "73")
            {
                cboStatusKelainan.Enabled = (rblJenisRawat.SelectedValue.Split('|')[0] == AppConstant.RegistrationType.InPatient);
                cboStatusKelainan.SelectedValue = string.Empty;
            }
            else
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                txtNoSep.Text = reg.BpjsSepNo;

                cboEpisode.Enabled = false;
            }

            txtNoSep.ReadOnly = (value == "71");

            cboCOB.Enabled = (value != "71");
            cboCOB.SelectedValue = string.Empty;

            cboStatusCov.Enabled = (value == "71");
            cboStatusCov.SelectedValue = string.Empty;

            rblKriteriaAksesNaat.Enabled = (value == "71");

            rblRsDaruratLapangan.Enabled = (value == "71");

            rblKomorbid.Enabled = (value == "71");

            rblIsolasiMandiri.Enabled = (value == "71");

            rblCoInsidens.Enabled = (value == "71");

            txtTerapiKovalesen.ReadOnly = (value != "71");

            EnableFileUpload((value == "71"));
        }

        private void EnableFileUpload(bool isEnabled)
        {
            fuResumeMedis.Enabled = isEnabled;
            fuResumeMedis.Attributes.Clear();
            btnFuResumeMedis.Enabled = isEnabled;

            fuRuangPerawatan.Enabled = isEnabled;
            fuRuangPerawatan.Attributes.Clear();
            btnFuRuangPerawatan.Enabled = isEnabled;

            fuLaboratorium.Enabled = isEnabled;
            fuLaboratorium.Attributes.Clear();
            btnFuLaboratorium.Enabled = isEnabled;

            fuRadiologi.Enabled = isEnabled;
            fuRadiologi.Attributes.Clear();
            btnFuRadiologi.Enabled = isEnabled;

            fuPenunjang.Enabled = isEnabled;
            fuPenunjang.Attributes.Clear();
            btnFuPenunjang.Enabled = isEnabled;

            fuObat.Enabled = isEnabled;
            fuObat.Attributes.Clear();
            btnFuObat.Enabled = isEnabled;

            fuBilling.Enabled = isEnabled;
            fuBilling.Attributes.Clear();
            btnFuBilling.Enabled = isEnabled;

            fuKartu.Enabled = isEnabled;
            fuKartu.Attributes.Clear();
            btnFuKartu.Enabled = isEnabled;

            fuLainlain.Enabled = isEnabled;
            fuLainlain.Attributes.Clear();
            btnFuLainlain.Enabled = isEnabled;
        }

        private void UploadFile(System.Web.UI.WebControls.FileUpload fileUpload, string className)
        {
            var fs = fileUpload.PostedFile.InputStream;
            var br = new BinaryReader(fs);
            var bytes = br.ReadBytes((Int32)fs.Length);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            var svc54 = new Common.Inacbg.v54.Service();
            var upload = svc54.UploadFile(new Common.Inacbg.v54.File.Upload.RootObject()
            {
                Metadata = new Common.Inacbg.v54.File.Upload.Metadata() { NomorSep = txtNoSep.Text, FileClass = className, FileName = fileUpload.PostedFile.FileName },
                Data = base64String
            });
            if (!upload.Metadata.IsValid)
                ScriptManager.RegisterStartupScript(this, GetType(), "upload", string.Format("alert('Code : {0}, Message : {1}');", upload.Metadata.Code, upload.Metadata.Message), true);

            LoadUploadFile();
        }

        private void LoadUploadFile()
        {
            var svc54 = new Common.Inacbg.v54.Service();
            var files = svc54.GetFile(new Common.Inacbg.v54.File.Get.Data() { NomorSep = txtNoSep.Text });
            if (files.Metadata.IsValid)
            {
                if (files.FileResponse.Count.ToInt() > 0)
                {
                    FileUploadCollection.Clear();
                    foreach (var file in files.FileResponse.DataResponse)
                    {
                        FileUploadCollection.Add(new FileUpload()
                        {
                            FileId = file.FileId,
                            FileName = file.UploadDcBpjs == "1" ? file.FileName : file.FileName + " - " + file.UploadDcBpjsResponse.Metadata.Message,
                            FileSize = file.FileSize,
                            FileType = file.FileType,
                            FileClass = file.FileClass,
                            UploadDcBpjs = file.UploadDcBpjs == "1" ? true : false,
                            Code = file.UploadDcBpjsResponse.Metadata.Code,
                            Message = file.UploadDcBpjsResponse.Metadata.Message
                        });
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "resume_medis"))
                    {
                        grdResumeMedis.DataSource = FileUploadCollection.Where(f => f.FileClass == "resume_medis");
                        grdResumeMedis.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "ruang_rawat"))
                    {
                        grdRuangPerawatan.DataSource = FileUploadCollection.Where(f => f.FileClass == "ruang_rawat");
                        grdRuangPerawatan.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "laboratorium"))
                    {
                        grdLaboratorium.DataSource = FileUploadCollection.Where(f => f.FileClass == "laboratorium");
                        grdLaboratorium.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "radiologi"))
                    {
                        grdRadiologi.DataSource = FileUploadCollection.Where(f => f.FileClass == "radiologi");
                        grdRadiologi.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "penunjang_lain"))
                    {
                        grdPenunjang.DataSource = FileUploadCollection.Where(f => f.FileClass == "penunjang_lain");
                        grdPenunjang.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "resep_obat"))
                    {
                        grdObat.DataSource = FileUploadCollection.Where(f => f.FileClass == "resep_obat");
                        grdObat.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "tagihan"))
                    {
                        grdBilling.DataSource = FileUploadCollection.Where(f => f.FileClass == "tagihan");
                        grdBilling.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "kartu_identitas"))
                    {
                        grdKartu.DataSource = FileUploadCollection.Where(f => f.FileClass == "kartu_identitas");
                        grdKartu.DataBind();
                    }
                    if (FileUploadCollection.Any(f => f.FileClass == "lain_lain"))
                    {
                        grdLainlain.DataSource = FileUploadCollection.Where(f => f.FileClass == "lain_lain");
                        grdLainlain.DataBind();
                    }
                }
            }
        }

        protected void btnFileUpload_Click(object sender, ImageClickEventArgs e)
        {
            if ((sender as ImageButton).ID == btnFuResumeMedis.ID)
            {
                if (fuResumeMedis.HasFile) UploadFile(fuResumeMedis, "resume_medis");
            }
            else if ((sender as ImageButton).ID == btnFuRuangPerawatan.ID)
            {
                if (fuRuangPerawatan.HasFile) UploadFile(fuRuangPerawatan, "ruang_rawat");
            }
            else if ((sender as ImageButton).ID == btnFuLaboratorium.ID)
            {
                if (fuLaboratorium.HasFile) UploadFile(fuLaboratorium, "laboratorium");
            }
            else if ((sender as ImageButton).ID == btnFuRadiologi.ID)
            {
                if (fuRadiologi.HasFile) UploadFile(fuRadiologi, "radiologi");
            }
            else if ((sender as ImageButton).ID == btnFuPenunjang.ID)
            {
                if (fuPenunjang.HasFile) UploadFile(fuPenunjang, "penunjang_lain");
            }
            else if ((sender as ImageButton).ID == btnFuObat.ID)
            {
                if (fuObat.HasFile) UploadFile(fuObat, "resep_obat");
            }
            else if ((sender as ImageButton).ID == btnFuBilling.ID)
            {
                if (fuBilling.HasFile) UploadFile(fuBilling, "tagihan");
            }
            else if ((sender as ImageButton).ID == btnFuKartu.ID)
            {
                if (fuKartu.HasFile) UploadFile(fuKartu, "kartu_identitas");
            }
            else if ((sender as ImageButton).ID == btnFuKipi.ID)
            {
                if (fuKipi.HasFile) UploadFile(fuKartu, "dokumen_kipi");
            }
            else if ((sender as ImageButton).ID == btnFuLainlain.ID)
            {
                if (fuLainlain.HasFile) UploadFile(fuLainlain, "lain_lain");
            }
        }

        protected void cboEpisode_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EpisodeRuangRawatCollection.Add(new EpisodeRuangRawat() { Key = EpisodeRuangRawatCollection.Count() + 1, ID = e.Value, Nama = e.Text, Jumlah = 1 });
            txtTotalEpisode.Value = EpisodeRuangRawatCollection.Sum(ep => ep.Jumlah).ToDouble();
            grdEpisodeRuangRawat.DataSource = EpisodeRuangRawatCollection;
            grdEpisodeRuangRawat.DataBind();
        }

        protected void grdEpisodeRuangRawat_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Key"]);

            EpisodeRuangRawatCollection.Remove(EpisodeRuangRawatCollection.Where(epi => epi.Key == id).Single());
            grdEpisodeRuangRawat.DataSource = EpisodeRuangRawatCollection;
            grdEpisodeRuangRawat.DataBind();
        }

        private List<EpisodeRuangRawat> EpisodeRuangRawatCollection
        {
            get
            {
                if (ViewState["episodeRuangRawat"] == null) ViewState["episodeRuangRawat"] = new List<EpisodeRuangRawat>();
                return ViewState["episodeRuangRawat"] as List<EpisodeRuangRawat>;
            }
            set
            {
                ViewState["episodeRuangRawat"] = value;
            }
        }

        protected void rblKelasRawat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblJenisRawat.SelectedValue.Split('|')[1] == "2")
            {
                txtTariffPoliEks.ReadOnly = rblKelasRawat.SelectedValue != "1";
            }
            else txtTariffPoliEks.ReadOnly = true;
        }

        private void EnableCheckBoxMeninggalCovid(bool isEnabled)
        {
            chkPemulasaraanJenazah.Enabled = isEnabled;
            chkKantongJenazah.Enabled = isEnabled;
            chkPetiJenazah.Enabled = isEnabled;
            chkPlastikErat.Enabled = isEnabled;
            chkDesinfektanJenazah.Enabled = isEnabled;
            chkTransportMobil.Enabled = isEnabled;
            chkDesinfektanMobil.Enabled = isEnabled;
        }

        protected void cboCaraPulang_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "4") EnableCheckBoxMeninggalCovid(cboJaminan.SelectedValue.Split('|')[1] == "71");
            else EnableCheckBoxMeninggalCovid(false);
        }

        private List<FileUpload> FileUploadCollection
        {
            get
            {
                if (ViewState["fileUpload"] == null) ViewState["fileUpload"] = new List<FileUpload>();
                return ViewState["fileUpload"] as List<FileUpload>;
            }
            set
            {
                ViewState["fileUpload"] = value;
            }
        }

        protected void grdResumeMedis_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["FileId"]);
            var svc = new Common.Inacbg.v54.Service();
            var delete = svc.DeleteFile(new Common.Inacbg.v54.File.Delete.Data() { FileId = id, NomorSep = txtNoSep.Text });
            if (!delete.Metadata.IsValid)
                ScriptManager.RegisterStartupScript(this, GetType(), "delete", string.Format("alert('Code : {0}, Message : {1}');", delete.Metadata.Code, delete.Metadata.Message), true);
            else LoadUploadFile();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);
            reg.GuarantorCardNo = txtNoPeserta.Text;
            reg.BpjsSepNo = txtNoSep.Text;
            reg.Save();

            if (EpisodeProcedures.Any(p => p.ProcedureID.Trim() == "99.25") && txtObatKemoterapi.Value == 0) // pasien kemo
            {
                ShowInformationHeader("Dilakukan prosedur kemoterapi, tapi nilai obat kemoterapi masih kosong.");
                return;
            }

            var args = new ValidateArgs();
            if (GetTransactionMode == "new") OnBeforeMenuNewClick(args);
            else if (GetTransactionMode == "edit") OnBeforeMenuEditClick(args);
            if (args.IsCancel)
            {
                if (args.MessageText == "E2008 - Nomor RM tidak ditemukan")
                {
                    GetTransactionMode = "new";
                    OnBeforeMenuNewClick(args);
                }
                if (args.IsCancel)
                {
                    ShowInformationHeader(args.MessageText);
                    return;
                }
            }

            if (GetTransactionMode == "new")
            {
                var diags = (ViewState["icd10"] as EpisodeDiagnoseCollection);
                foreach (var diag in diags)
                {
                    diag.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    diag.LastUpdateDateTime = DateTime.Now;
                }
                diags.Save();

                var procs = (ViewState["icd9cm"] as EpisodeProcedureCollection);
                foreach (var proc in procs)
                {
                    proc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    proc.LastUpdateDateTime = DateTime.Now;
                }
                procs.Save();
            }

            GetTransactionMode = "edit";

            //ScriptManager.RegisterStartupScript(this, GetType(), "save", string.Format("alert('Simpan klaim No. SEP : {0} berhasil.');", txtNoSep.Text), true);

            btnSave.Focus();
        }

        protected void cboSpecialProcedure_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void cboNoPeserta_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var patient = new Patient();
            if (e.Value == "nik")
            {
                patient.LoadByMedicalNo(txtNoMR.Text);
                txtNoPeserta.Text = patient.Ssn;
            }
            else if (e.Value == "kartu_jkn")
            {
                patient.LoadByMedicalNo(txtNoMR.Text);
                txtNoPeserta.Text = patient.GuarantorCardNo;
            }
            else txtNoPeserta.Text = string.Empty;
        }

        protected void btnImportDiagnosa_Click(object sender, EventArgs e)
        {
            ImportDiagnose();
            ImportProcedure();
        }

        protected void btnImportProcedure_Click(object sender, EventArgs e)
        {
            ImportProcedure();
            ImportDiagnose();
        }

        private void ImportDiagnose()
        {
            foreach (var diag in EpisodeDiagnoses)
            {
                var ina = new DiagnoseInaGroupper();
                if (!ina.LoadByPrimaryKey(diag.DiagnoseID))
                {
                    var d = new Diagnose();
                    d.LoadByPrimaryKey(diag.DiagnoseID);

                    ina = new DiagnoseInaGroupper()
                    {
                        DiagnoseID = d.DiagnoseID,
                        DtdNo = "0",
                        DiagnoseName = d.DiagnoseName,
                        IsChronicDisease = false,
                        IsDisease = false,
                        IsActive = true,
                        LastUpdateDateTime = DateTime.Now,
                        LastUpdateByUserID = AppSession.UserLogin.UserID
                    };
                    ina.Save();
                }

                var id = ViewState["seq102"] == null ? string.Empty : ViewState["seq102"].ToString();
                //var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;

                var ed = EpisodeDiagnoseInaGrouppers.Where(eds => eds.RegistrationNo == diag.RegistrationNo && eds.DiagnoseID == diag.DiagnoseID).SingleOrDefault();
                if (ed == null) ed = EpisodeDiagnoseInaGrouppers.AddNew();
                ed.RegistrationNo = diag.RegistrationNo;
                ed.SequenceNo = string.IsNullOrEmpty(id) ? (EpisodeDiagnoseInaGrouppers.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
                ed.DiagnoseID = diag.DiagnoseID;
                ed.DiagnoseName = diag.DiagnoseName;
                ed.SRDiagnoseType = diag.SRDiagnoseType;
                ed.DiagnoseType = diag.DiagnosisText;
                ed.DiagnosisText = diag.DiagnosisText;
                ed.MorphologyID = string.Empty;
                ed.ParamedicID = diag.ParamedicID;
                ed.IsAcuteDisease = false;
                ed.IsChronicDisease = diag.IsChronicDisease;
                ed.IsOldCase = false;
                ed.IsConfirmed = true;
                ed.IsVoid = false;
                ed.Notes = string.Empty;
                ed.LastUpdateDateTime = DateTime.Now;
                ed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                ed.ExternalCauseID = string.Empty;
                ed.CreateByUserID = AppSession.UserLogin.UserID;
                ed.CreateDateTime = DateTime.Now;

                //if (GetTransactionMode == "edit")
                //{
                //    var diagnosa = string.Empty;
                //    foreach (var d in (ViewState["icd102"] as EpisodeDiagnoseCollection))
                //    {
                //        diagnosa += d.DiagnoseID + "#";
                //    }

                //    var svc = new Common.Inacbg.v51.Service();
                //    var detail = svc.UpdateDiagnose(new Common.Inacbg.v51.Detail.Data()
                //    {
                //        nomor_sep = txtNoSep.Text,
                //        payor_id = cboJaminan.SelectedValue.Split('|')[1],
                //        diagnosa = diagnosa,
                //        coder_nik = AppSession.UserLogin.LicenseNo
                //    });
                //    if (detail.Metadata.IsValid)
                //    {
                //        EpisodeDiagnoseInaGrouppers.Save();
                //        //ViewState["seq10"] = null;
                //    }
                //}
                //else 

                ViewState["seq102"] = string.Empty;
            }

            if (EpisodeDiagnoses.Count > 0)
            {
                EpisodeDiagnoseInaGrouppers.Save();

                grdDiagnosa2.Rebind();
            }
        }

        private void ImportProcedure()
        {
            foreach (var proc in EpisodeProcedures)
            {
                var ina = new ProcedureInaGroupper();
                if (!ina.LoadByPrimaryKey(proc.ProcedureID))
                {
                    var d = new Procedure();
                    d.LoadByPrimaryKey(proc.ProcedureID);

                    ina = new ProcedureInaGroupper()
                    {
                        ProcedureID = d.ProcedureID,
                        ProcedureName = d.ProcedureName,
                        LastUpdateDateTime = DateTime.Now,
                        LastUpdateByUserID = AppSession.UserLogin.UserID
                    };
                    ina.Save();
                }

                var id = ViewState["seq9cm2"] == null ? string.Empty : ViewState["seq9cm2"].ToString();
                //var coll = ViewState["icd9cm"] as EpisodeProcedureCollection;
                var ep = EpisodeProcedureInaGrouppers.Where(eps => eps.RegistrationNo == proc.RegistrationNo && eps.ProcedureID == proc.ProcedureID).SingleOrDefault();
                if (ep == null) ep = EpisodeProcedureInaGrouppers.AddNew();

                ep.RegistrationNo = proc.RegistrationNo;
                //ep.SequenceNo = string.IsNullOrEmpty(id) ? (EpisodeProcedureInaGrouppers.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
                id = string.IsNullOrEmpty(id) ? (EpisodeProcedureInaGrouppers.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
                ep.SequenceNo = string.Format("{0:000}", int.Parse(id));
                ep.ProcedureDate = proc.ProcedureDate;
                ep.ProcedureTime = proc.ProcedureTime;
                ep.ProcedureDate2 = proc.ProcedureDate2;
                ep.ProcedureTime2 = proc.ProcedureTime2;
                ep.ParamedicID = string.Empty;
                ep.ParamedicID2 = string.Empty;
                ep.ProcedureID = proc.ProcedureID;
                ep.ProcedureName = proc.ProcedureName;
                ep.SRProcedureCategory = string.Empty;
                ep.SRAnestesi = string.Empty;
                ep.RoomID = string.Empty;
                ep.IsCito = false;
                ep.IsVoid = false;
                ep.LastUpdateDateTime = DateTime.Now;
                ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
                ep.AssistantID1 = string.Empty;
                ep.AssistantID2 = string.Empty;
                ep.Notes = string.Empty;
                ep.BookingNo = string.Empty;
                ep.ParamedicID2a = string.Empty;
                ep.ParamedicID3a = string.Empty;
                ep.ParamedicID4a = string.Empty;
                ep.ParamedicIDAnestesi = string.Empty;
                ep.AssistantIDAnestesi = string.Empty;
                ep.InstrumentatorID1 = string.Empty;
                ep.InstrumentatorID2 = string.Empty;
                ep.IsFromOperatingRoom = true;
                ep.CreateByUserID = AppSession.UserLogin.UserID;
                ep.CreateDateTime = DateTime.Now;

                ViewState["seq9cm2"] = string.Empty;
            }

            if (EpisodeProcedureInaGrouppers.Count > 0)
            {
                EpisodeDiagnoseInaGrouppers.Save();

                grdProsedur2.Rebind();
            }
        }

        protected void btnValidasiSitb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNoSep.Text) || string.IsNullOrEmpty(txtSitbNo.Text)) return;

            var svc = new Common.Inacbg.v57.Service();
            var response = svc.SetValidateSitb(txtNoSep.Text, txtSitbNo.Text);
            if (response.Metadata.IsValid)
                ScriptManager.RegisterStartupScript(this, GetType(), "sitb", string.Format($"alert('Status : {response.Response.Status}, Detail : {response.Response.Detail}.');"), true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "sitb", string.Format($"alert('Status : {response.Metadata.Code}, Detail : {response.Metadata.Message}.');"), true);
        }

        //public string ValidasiSitb
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (string.IsNullOrWhiteSpace(txtNoSep.Text) || string.IsNullOrWhiteSpace(txtSitbNo.Text))
        //            {
        //                return string.Format($"Status : 201, Detail : No SEP atau No SITB tidak boleh kosong.");
        //            }

        //            var svc = new Common.Inacbg.v57.Service();
        //            var response = svc.SetValidateSitb(txtNoSep.Text, txtSitbNo.Text);
        //            if (!response.Metadata.IsValid)
        //            {
        //                return string.Format($"Status : {response.Metadata.Code}, Detail : {response.Metadata.Message}.");
        //            }
        //            else
        //            {
        //                if (response.Response.Status.Trim().ToLower() == "invalid")
        //                    return string.Format($"Status : {response.Response.Status}, Detail : {response.Response.Detail}.");
        //            }
        //            return string.Empty;
        //        }
        //        catch (Exception ex)
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}

        protected void chkPasienTb_CheckedChanged(object sender, EventArgs e)
        {
            txtSitbNo.Text = string.Empty;
            txtSitbNo.Enabled = chkPasienTb.Checked;
            btnValidasiSitb.Enabled = chkPasienTb.Checked;
        }

        protected void chkVentilator_CheckedChanged(object sender, EventArgs e)
        {
            txtTglIntubasi.Enabled = chkVentilator.Checked;
            txtTglIntubasi.Clear();
            txtJamIntubasi.Enabled = chkVentilator.Checked;
            txtJamIntubasi.Clear();
            txtTglEkstubasi.Enabled = chkVentilator.Checked;
            txtTglEkstubasi.Clear();
            txtJamEkstubasi.Enabled = chkVentilator.Checked;
            txtJamEkstubasi.Clear();
        }

        protected void chkCoinsidenseCovid_CheckedChanged(object sender, EventArgs e)
        {
            txtNomorKlaimCovid.Text = string.Empty;
            txtNomorKlaimCovid.Enabled = chkCoinsidenseCovid.Checked;
            //btnValidasiCoinsidenseCovid.Enabled = chkCoinsidenseCovid.Checked;
        }

        protected void btnValidasiCoinsidenseCovid_Click(object sender, EventArgs e)
        {

        }

        private List<Delivery> Deliveries
        {
            get
            {
                if (Session["deliveryEklaim"] == null) Session["deliveryEklaim"] = new List<Delivery>();
                return Session["deliveryEklaim"] as List<Delivery>;
            }
            set
            {
                Session["deliveryEklaim"] = value;
            }
        }

        protected void grdDelivery_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDelivery.DataSource = Deliveries;
        }

        protected void grdDelivery_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["DeliverySequence"]);
            var entity = Deliveries.FirstOrDefault(d => d.DeliverySequence == id);
            if (entity != null) Deliveries.Remove(entity);
        }

        protected void grdDelivery_InsertCommand(object sender, GridCommandEventArgs e)
        {
            var entity = new Delivery();
            SetEntityValue(entity, e);
            Deliveries.Add(entity);

            //Stay in insert mode
            grdDelivery.Rebind();
        }

        protected void grdDelivery_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            var id = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["DeliverySequence"]);
            var entity = Deliveries.FirstOrDefault(d => d.DeliverySequence == id);
            if (entity != null) SetEntityValue(entity, e);

        }

        private void SetEntityValue(Delivery entity, GridCommandEventArgs e)
        {
            var userControl = (Persalinan)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.DeliverySequence = userControl.DeliverySequence;
                entity.DeliveryMethod = userControl.DeliveryMethod;
                entity.DeliveryDttm = userControl.DeliveryDttm;
                entity.LetakJanin = userControl.LetakJanin;
                entity.Kondisi = userControl.Kondisi;
                entity.UseManual = userControl.UseManual;
                entity.UseForcep = userControl.UseForcep;
                entity.UseVacuum = userControl.UseVacuum;
            }
        }

        protected void cboDializer_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            btnGroupper_Click(null, null);
        }
    }

    [Serializable]
    public class EpisodeRuangRawat
    {
        public int Key { get; set; }

        public string ID { get; set; }

        public string Nama { get; set; }

        public int Jumlah { get; set; }
    }

    [Serializable]
    public class FileUpload
    {
        public string FileId { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }

        public string FileType { get; set; }

        public string FileClass { get; set; }

        public bool UploadDcBpjs { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }

    public class Delivery
    {
        [JsonProperty("delivery_sequence")]
        public string DeliverySequence { get; set; }

        [JsonProperty("delivery_method")]
        public string DeliveryMethod { get; set; }

        [JsonProperty("delivery_dttm")]
        public string DeliveryDttm { get; set; }

        [JsonProperty("letak_janin")]
        public string LetakJanin { get; set; }

        [JsonProperty("kondisi")]
        public string Kondisi { get; set; }

        [JsonProperty("use_manual")]
        public string UseManual { get; set; }

        [JsonProperty("use_forcep")]
        public string UseForcep { get; set; }

        [JsonProperty("use_vacuum")]
        public string UseVacuum { get; set; }
    }
}
