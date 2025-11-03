using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.Integration.Eklaim
{
    public partial class iDRGDetail : BasePageDialog
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
            }
        }

        protected void cboDiagnosaIdrg_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            btnFilterDiagnosaIdrg_Click(null, null);
        }

        protected void cboDiagnosaInacbg_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
        {
            btnFilterDiagnosaInacbg_Click(null, null);
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

        protected void grdProsedurIdrg_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var regno = Request.QueryString["regno"];
            var nomorSep = txtNoSep.Text;
            if (string.IsNullOrWhiteSpace(nomorSep))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                    "alert('Nomor SEP kosong.');", true);
                return;
            }

            var seq = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);
            var svc = new Common.Inacbg.v510.Service();

            var coll = ViewState["icd9cm"] as EpisodeProcedureCollection;
            var ed = coll?.FindByPrimaryKey(regno, seq);

            string codeToRemove = ed?.ProcedureID;
            if (string.IsNullOrWhiteSpace(codeToRemove))
            {
                try { codeToRemove = (item["ProcedureID"]?.Text ?? "").Trim(); } catch { /* no-op */ }
            }
            if (string.IsNullOrWhiteSpace(codeToRemove)) return;

            List<string> current = new List<string>();

            if (coll != null && coll.Any())
            {
                current = coll
                    .Where(p => !string.IsNullOrWhiteSpace(p.ProcedureID) && p.IsVoid == false)
                    .SelectMany(p =>
                    {
                        var code = p.ProcedureID.Trim();
                        int qty = Math.Max(1, p.QtyICD ?? 0);  // <- int, aman dari null & <= 0
                        return Enumerable.Repeat(code, qty);
                    })
                    .ToList();
            }
            else
            {
                try
                {
                    var getRes = svc.IDRGGetProcedure(new Common.Inacbg.v510.Procedure.Get.Data { nomor_sep = nomorSep });
                    SaveNccIdrg("IdrgProcedureGet", new { nomor_sep = nomorSep }, getRes);

                    LogApi("IDRGGetProcedure", new { nomor_sep = nomorSep }, getRes, 0);

                    if (getRes?.Metadata?.IsValid == true)
                    {
                        var s = getRes?.Data?.String;
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            foreach (var raw in s.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var t = raw.Trim();
                                if (string.IsNullOrWhiteSpace(t)) continue;
                                var plus = t.IndexOf('+');
                                if (plus > 0)
                                {
                                    var code = t.Substring(0, plus).Trim();
                                    var rest = t.Substring(plus + 1).Trim();
                                    int qty = 1; int.TryParse(rest, out qty);
                                    qty = qty <= 0 ? 1 : qty;
                                    for (int i = 0; i < qty; i++) current.Add(code);
                                }
                                else
                                {
                                    current.Add(t);
                                }
                            }
                        }
                        else if (getRes?.Data?.Expanded != null)
                        {
                            foreach (var row in getRes.Data.Expanded)
                            {
                                if (string.IsNullOrWhiteSpace(row.Code)) continue;
                                var code = row.Code.Trim();
                                var times = row.Multiply <= 0 ? 1 : row.Multiply;
                                for (int i = 0; i < times; i++) current.Add(code);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogApi("IDRGGetProcedure(EX)", new { nomor_sep = nomorSep }, new { error = ex.Message }, 0);
                }
            }

            //var idx = current.FindIndex(c => string.Equals(c, codeToRemove, StringComparison.OrdinalIgnoreCase));
            //if (idx >= 0) current.RemoveAt(idx);

            int timesprcd = Math.Max(1, ed?.QtyICD ?? 1);
            for (int i = 0; i < timesprcd; i++)
            {
                var idx = current.FindIndex(c => string.Equals(c, codeToRemove, StringComparison.OrdinalIgnoreCase));
                if (idx < 0) break;
                current.RemoveAt(idx);
            }

            string payload;
            if (current.Any())
            {
                var tokens = current
                    .GroupBy(c => c, StringComparer.OrdinalIgnoreCase)
                    .Select(g =>
                    {
                        var code = g.Key;
                        var cnt = g.Count();
                        return cnt > 1 ? $"{code}+{cnt}" : code;
                    })
                    .ToList();

                payload = tokens.Count == 0 ? "#" : string.Join("#", tokens);
            }
            else
            {
                payload = "#";
            }

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var setRes = svc.IDRGSetProcedure(new Common.Inacbg.v510.Procedure.Data
            {
                nomor_sep = nomorSep,
                procedure = payload
            });
            sw.Stop();
            LogApi("IDRGSetProcedure", new { nomor_sep = nomorSep, procedure = payload }, setRes, sw.ElapsedMilliseconds);
            SaveNccIdrg("IdrgProcedureSet", new { nomor_sep = nomorSep, procedure = payload }, setRes);

            if (!setRes.Metadata.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                    $"alert('{setRes.Metadata.Message}.');", true);
                return;
            }

            if (ed != null)
            {
                ed.MarkAsDeleted();
                coll.Save();
            }

            txtIDRGInfo.Text = string.Empty;
            txtIDRGJenisRawat.Text = string.Empty;
            txtIDRGMDC.Text = string.Empty;
            txtIDRGMDCCode.Text = string.Empty;
            txtIDRGDRG.Text = string.Empty;
            txtIDRGDRGCode.Text = string.Empty;
            txtCostWeight.Text = string.Empty;
            txtNBR.Text = string.Empty;
            rowCostWeight.Visible = false;
            rowNBR.Visible = false;
            txtIDRGStatus.Text = string.Empty;

            ViewState["icd9cm"] = null;
            grdProsedurIdrg.Rebind();
            //pnlIdrgResult.Visible = false;
            ShowAlert("idrg-proc-del-ok", "Prosedur iDRG berhasil dihapus.");
        }

        protected void grdProsedurInacbg_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var regno = Request.QueryString["regno"];
            var nomorSep = txtNoSep.Text;
            if (string.IsNullOrWhiteSpace(nomorSep))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                    "alert('Nomor SEP kosong.');", true);
                return;
            }

            var seqToDelete = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

            var coll = ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection;
            var ep = coll?.FindByPrimaryKey(regno, seqToDelete);

            string codeToRemove = ep?.ProcedureID;
            if (string.IsNullOrWhiteSpace(codeToRemove))
            {
                try
                {
                    codeToRemove = System.Web.HttpUtility.HtmlDecode(item["ProcedureID"]?.Text ?? "").Trim();
                }
                catch { /* no-op */ }
            }
            if (string.IsNullOrWhiteSpace(codeToRemove)) return;

            var svc = new Common.Inacbg.v510.Service();

            List<string> current = new List<string>();
            if (coll != null && coll.Any())
            {
                current = coll
                    .Where(p => !string.IsNullOrWhiteSpace(p.ProcedureID) && p.IsVoid == false)
                    .Select(p => p.ProcedureID.Trim())
                    .ToList();
            }
            else
            {
                try
                {
                    var getRes = svc.InacbgGetProcedure(new Common.Inacbg.v510.Procedure.Get.Data { nomor_sep = nomorSep });
                    SaveNccIdrg("InacbgProcedureGet", new { nomor_sep = nomorSep }, getRes);

                    if (getRes?.Metadata?.IsValid == true)
                    {
                        var s = getRes?.Data?.String;
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            current = s.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(x => x.Trim())
                                       .Where(x => !string.IsNullOrWhiteSpace(x))
                                       .ToList();
                        }
                        else if (getRes?.Data?.Expanded != null)
                        {
                            current = getRes.Data.Expanded
                                       .Where(x => !string.IsNullOrWhiteSpace(x.Code))
                                       .Select(x => x.Code.Trim())
                                       .ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogApi("INACBGGetProcedure(EX)", new { nomor_sep = nomorSep }, new { error = ex.Message }, 0);
                }
            }

            var idx = current.FindIndex(c => string.Equals(c, codeToRemove, StringComparison.OrdinalIgnoreCase));
            if (idx >= 0) current.RemoveAt(idx);

            var payload = (current != null && current.Any())
                ? string.Join("#", current)
                : "#";

            Common.Inacbg.v510.Procedure.Response.Result detail = null;
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                detail = svc.INACBGSetProcedure(new Common.Inacbg.v510.Procedure.Data
                {
                    nomor_sep = nomorSep,
                    procedure = payload
                });
                sw.Stop();

                SaveNccIdrg("InacbgProcedureSet", new { nomor_sep = nomorSep, procedure = payload }, detail);
                LogApi("INACBGSetProcedure", new { nomor_sep = nomorSep, procedure = payload }, detail, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-proc-del-ex",
                    $"alert('Gagal menghubungi INACBG: {ex.Message}');", true);
                return; // lokal jangan dihapus
            }

            if (detail?.Metadata?.IsValid != true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-proc-del-fail",
                    $"alert('{detail?.Metadata?.Message ?? "Penghapusan prosedur ke INACBG gagal."}');", true);
                return; // lokal jangan dihapus
            }

            if (ep != null)
            {
                ep.MarkAsDeleted();
                coll.Save();
            }
            ViewState["icd9cm2"] = null;

            txtInfo.Text = string.Empty;
            txtJenisRawat.Text = string.Empty;
            txtGroupName.Text = string.Empty;
            txtGroupID.Text = string.Empty;
            txtGroupPrice.Text = string.Empty;
            txtSubAcuteName.Text = string.Empty;
            txtSubAcuteID.Text = string.Empty;
            txtSubAcutePrice.Text = string.Empty;
            txtChronicName.Visible = false;
            txtChronicID.Visible = false;
            txtChronicPrice.Text = string.Empty;
            cboSpecialProcedure.SelectedValue = cboSpecialProcedure.Text = string.Empty;
            txtSpecialProcedurePrice.Text = string.Empty;
            cboSpecialProsthesis.SelectedValue = cboSpecialProsthesis.Text = string.Empty;
            txtSpecialProsthesisPrice.Text = string.Empty;
            cboSpecialInvestigation.SelectedValue = cboSpecialInvestigation.Text = string.Empty;
            txtSpecialInvestigationPrice.Text = string.Empty;
            cboSpecialDrug.SelectedValue = cboSpecialDrug.Text = string.Empty;
            txtSpecialDrugPrice.Text = string.Empty;
            txtGrouperTotal.Text = string.Empty;
            txtSelisihPersen.Text = string.Empty;
            txtTambahanBiaya.Text = string.Empty;

            grdProsedurInacbg.Rebind();
            ShowAlert("inacbg-proc-del-ok", "Prosedur INACBG berhasil dihapus.");
        }

        //protected void grdProsedurInacbg_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    var item = e.Item as Telerik.Web.UI.GridDataItem;
        //    if (item == null) return;

        //    var seqToDelete = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

        //    var coll = ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection;
        //    var ep = coll.FindByPrimaryKey(Request.QueryString["regno"], seqToDelete);
        //    if (ep != null)
        //    {
        //        if (GetTransactionMode == "edit")
        //        {
        //            var svc = new Common.Inacbg.v510.Service();

        //            ep.MarkAsDeleted();
        //            coll.Save();
        //            ViewState["icd9cm2"] = null;

        //            var remaining = EpisodeProcedureInaGrouppers;
        //            var procedure = BuildProcedureStringWithQty2(remaining);

        //            var detail = svc.INACBGSetProcedure(new Common.Inacbg.v510.Procedure.Data()
        //            {
        //                nomor_sep = txtNoSep.Text,
        //                procedure = procedure
        //            });

        //            SaveNccIdrg("InacbgProcedureSet", new Common.Inacbg.v510.Procedure.Data
        //            {
        //                nomor_sep = txtNoSep.Text,
        //                procedure = procedure
        //            }, detail);


        //            if (detail.Metadata.IsValid)
        //            {
        //                txtInfo.Text = string.Empty;
        //                txtJenisRawat.Text = string.Empty;
        //                txtGroupName.Text = string.Empty;
        //                txtGroupID.Text = string.Empty;
        //                txtGroupPrice.Text = string.Empty;
        //                txtSubAcuteName.Text = string.Empty;
        //                txtSubAcuteID.Text = string.Empty;
        //                txtSubAcutePrice.Text = string.Empty;
        //                txtChronicName.Visible = false;
        //                txtChronicID.Visible = false;
        //                txtChronicPrice.Text = string.Empty;
        //                cboSpecialProcedure.SelectedValue = string.Empty;
        //                cboSpecialProcedure.Text = string.Empty;
        //                txtSpecialProcedurePrice.Text = string.Empty;
        //                cboSpecialProsthesis.SelectedValue = string.Empty;
        //                cboSpecialProsthesis.Text = string.Empty;
        //                txtSpecialProsthesisPrice.Text = string.Empty;
        //                cboSpecialInvestigation.SelectedValue = string.Empty;
        //                cboSpecialInvestigation.Text = string.Empty;
        //                txtSpecialInvestigationPrice.Text = string.Empty;
        //                cboSpecialDrug.SelectedValue = string.Empty;
        //                cboSpecialDrug.Text = string.Empty;
        //                txtSpecialDrugPrice.Text = string.Empty;
        //                txtGrouperTotal.Text = string.Empty;
        //                txtSelisihPersen.Text = string.Empty;
        //                txtTambahanBiaya.Text = string.Empty;

        //                grdProsedurInacbg.Rebind();
        //                ShowAlert("inacbg-proc-del-ok", "Prosedur INACBG berhasil dihapus.");
        //                //pnlInacbgResult.Visible = false;
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
        //                    $"alert('{detail.Metadata.Message}.');", true);
        //            }
        //        }
        //        else
        //        {
        //            ep.MarkAsDeleted();
        //            coll.Save();
        //            ViewState["icd9cm2"] = null;
        //            grdProsedurInacbg.Rebind();
        //        }
        //    }
        //}

        protected void grdProsedurIdrg_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
                        cboProsedurIdrg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ep.ProcedureName));
                        cboProsedurIdrg.SelectedValue = ep.ProcedureID;
                    }
                    break;
            }
        }

        protected void grdProsedurInacbg_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
                        cboProsedurInacbg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ep.ProcedureName));
                        cboProsedurInacbg.SelectedValue = ep.ProcedureID;
                    }
                    break;
            }
        }

        protected void grdProsedurIdrg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdProsedurIdrg.DataSource = EpisodeProcedures;
        }

        protected void grdProsedurInacbg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var coll = EpisodeProcedureInaGrouppers;

            var ds = coll
                .Cast<EpisodeProcedureInaGroupper>()
                .GroupBy(x => (x.ProcedureID ?? "").Trim(), StringComparer.OrdinalIgnoreCase)
                .Select(g =>
                {
                    var pick = g.FirstOrDefault(r => !string.IsNullOrWhiteSpace(r.Notes))
                           ?? g.OrderBy(x => x.SequenceNo).First();

                    var aggNotes = g.Select(x => x.Notes).FirstOrDefault(n => !string.IsNullOrWhiteSpace(n)) ?? string.Empty;

                    return new
                    {
                        pick.RegistrationNo,
                        pick.SequenceNo,
                        ProcedureID = g.Key,
                        ProcedureName = pick.ProcedureName,
                        pick.ParamedicID,
                        pick.SRProcedureCategory,
                        pick.SRAnestesi,
                        pick.RoomID,
                        pick.IsCito,
                        Notes = aggNotes
                    };
                })
                .ToList();

            grdProsedurInacbg.DataSource = ds;
        }

        protected void btnInsertProsedurIdrg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedurIdrg.SelectedValue)) return;

            var proc = new Procedure();
            if (!proc.LoadByPrimaryKey(cboProsedurIdrg.SelectedValue))
            {
                proc = new Procedure()
                {
                    ProcedureID = cboProsedurIdrg.SelectedValue,
                    ProcedureName = cboProsedurIdrg.Text,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                proc.Save();

                proc = new Procedure();
                proc.LoadByPrimaryKey(cboProsedurIdrg.SelectedValue);
            }

            var qty = (int)(txtQtyProc?.Value ?? 1);
            if (qty < 1) qty = 1;

            var ep = EpisodeProcedures.AddNew();

            ep.RegistrationNo = Request.QueryString["regno"];
            var nextSeq = GetNextEpisodeProcedureSeq(Request.QueryString["regno"]);
            ep.SequenceNo = PadSeq(nextSeq);

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

            ep.QtyICD = qty;

            if (GetTransactionMode == "edit")
            {
                var svc = new Common.Inacbg.v510.Service();

                var procedure = BuildProcedureStringWithQty(EpisodeProcedures);

                var req = new Common.Inacbg.v510.Procedure.Data
                {
                    nomor_sep = txtNoSep.Text,
                    procedure = procedure
                };

                Common.Inacbg.v510.Procedure.Response.Result detail = null;
                var sw = Stopwatch.StartNew();
                try
                {
                    detail = svc.IDRGSetProcedure(req);
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    LogApi("IDRGSetProcedure(EX)", req, new { error = ex.Message }, sw.ElapsedMilliseconds);
                    return;
                }
                sw.Stop();
                LogApi("IDRGSetProcedure", req, detail, sw.ElapsedMilliseconds);
                SaveNccIdrg("IdrgProcedureSet", req, detail);

                if (!detail.Metadata.IsValid)
                {
                    try { ep.MarkAsDeleted(); } catch { /* no-op */ }
                    ViewState["icd9cm"] = null;

                    grdProsedurIdrg.Rebind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                        $"alert('{detail.Metadata.Message}.');", true);
                    return;
                }
            }

            if (!TrySaveEpisodeProceduresWithRetry(EpisodeProcedures, Request.QueryString["regno"], ep))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "pk-dup",
                    "alert('Gagal menyimpan prosedur (duplikat kunci).');", true);
                return;
            }

            ViewState["icd9cm"] = null;
            var _ = EpisodeProcedures;

            txtQtyProc.Value = 1;
            txtQtyProc.Text = "1";

            var script = "$find('" + txtQtyProc.ClientID + "').set_value(1);";

            var ram = Telerik.Web.UI.RadAjaxManager.GetCurrent(Page);
            if (ram != null)
                ram.ResponseScripts.Add(script);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "resetQtyProc", script, true);

            grdProsedurIdrg.Rebind();

            cboProsedurIdrg.Items.Clear();
            cboProsedurIdrg.SelectedValue = string.Empty;
            cboProsedurIdrg.Text = string.Empty;
            cboProsedurIdrg.OpenDropDownOnLoad = false;
            txtQtyProc.Value = 1; // [IDRG] reset qty
            ViewState["seq9cm"] = string.Empty;

            var coll = ViewState["icd9cm"] as EpisodeProcedureCollection;
            if (coll != null && coll.Any(p => p.ProcedureID == "39.95") && string.IsNullOrEmpty(cboDializer.SelectedValue))
            {
                cboDializer.SelectedValue = "1";
            }
        }

        protected void btnInsertProsedurInacbg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedurInacbg.SelectedValue)) return;

            var proc = new Procedure();
            if (!proc.LoadByPrimaryKey(cboProsedurInacbg.SelectedValue))
            {
                proc = new Procedure()
                {
                    ProcedureID = cboProsedurInacbg.SelectedValue,
                    ProcedureName = cboProsedurInacbg.Text,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                proc.Save();

                proc = new Procedure();
                proc.LoadByPrimaryKey(cboProsedurInacbg.SelectedValue);
            }

            var ep = EpisodeProcedureInaGrouppers
                .FirstOrDefault(x => x.RegistrationNo == Request.QueryString["regno"]
                                  && x.ProcedureID == proc.ProcedureID);

            if (ep == null)
            {
                ep = EpisodeProcedureInaGrouppers.AddNew();

                ep.RegistrationNo = Request.QueryString["regno"];
                var maxSeq = EpisodeProcedureInaGrouppers.Any() ? EpisodeProcedureInaGrouppers.Max(p => p.SequenceNo.ToInt()) : 0;
                ep.SequenceNo = $"{maxSeq + 1:000}";

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
            else
            {
                ep.LastUpdateDateTime = DateTime.Now;
                ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }

            if (GetTransactionMode == "edit")
            {
                var svc = new Common.Inacbg.v510.Service();
                var procedure = BuildProcedureStringWithQty2(EpisodeProcedureInaGrouppers);

                var req = new Common.Inacbg.v510.Procedure.Data
                {
                    nomor_sep = txtNoSep.Text,
                    procedure = procedure
                };

                Common.Inacbg.v510.Procedure.Response.Result detail = null;
                var sw = Stopwatch.StartNew();
                try
                {
                    detail = svc.INACBGSetProcedure(req);
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    LogApi("INACBGSetProcedure(EX)", req, new { error = ex.Message }, sw.ElapsedMilliseconds);
                    return;
                }
                sw.Stop();
                LogApi("INACBGSetProcedure", req, detail, sw.ElapsedMilliseconds);
                SaveNccIdrg("InacbgProcedureSet", req, detail);

                if (!detail.Metadata.IsValid)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                        $"alert('{detail.Metadata.Message}.');", true);
                    return;
                }
            }

            EpisodeProcedureInaGrouppers.Save();
            grdProsedurInacbg.Rebind();

            cboProsedurInacbg.Items.Clear();
            cboProsedurInacbg.SelectedValue = string.Empty;
            cboProsedurInacbg.Text = string.Empty;
            cboProsedurInacbg.OpenDropDownOnLoad = false;
            ViewState["seq9cm2"] = string.Empty;

            var coll = ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection;
            if (coll != null && coll.Any(p => p.ProcedureID == "39.95") && string.IsNullOrEmpty(cboDializer.SelectedValue))
            {
                cboDializer.SelectedValue = "1";
            }
        }

        protected void btnFilterProsedurIdrg_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedurIdrg.Text))
            {
                cboProsedurIdrg.Items.Clear();
                cboProsedurIdrg.SelectedValue = string.Empty;
                cboProsedurIdrg.Text = string.Empty;
                cboProsedurIdrg.OpenDropDownOnLoad = false;
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = cboProsedurIdrg.Text }, false);
            if (diag.Metadata.IsValid)
            {
                cboProsedurIdrg.Items.Clear();
                cboProsedurIdrg.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);
                    cboProsedurIdrg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd[1]));
                }
                cboProsedurIdrg.OpenDropDownOnLoad = true;
            }
            else
            {
                cboProsedurIdrg.Items.Clear();
                cboProsedurIdrg.SelectedValue = string.Empty;
                cboProsedurIdrg.Text = string.Empty;
                cboProsedurIdrg.OpenDropDownOnLoad = false;
            }
        }

        protected void btnFilterProsedurInacbg_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboProsedurInacbg.Text))
            {
                cboProsedurInacbg.Items.Clear();
                cboProsedurInacbg.SelectedValue = string.Empty;
                cboProsedurInacbg.Text = string.Empty;
                cboProsedurInacbg.OpenDropDownOnLoad = false;
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = cboProsedurInacbg.Text }, false);
            if (diag.Metadata.IsValid)
            {
                cboProsedurInacbg.Items.Clear();
                cboProsedurInacbg.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);
                    cboProsedurInacbg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd[1]));
                }
                cboProsedurInacbg.OpenDropDownOnLoad = true;
            }
            else
            {
                cboProsedurInacbg.Items.Clear();
                cboProsedurInacbg.SelectedValue = string.Empty;
                cboProsedurInacbg.Text = string.Empty;
                cboProsedurInacbg.OpenDropDownOnLoad = false;
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
                DiagnoseQuery diag = new DiagnoseQuery("b");
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

        protected void grdDiagnosaIdrg_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
                            cboDiagnosaIdrg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ed.DiagnoseID));
                            cboDiagnosaIdrg.SelectedValue = ed.DiagnoseID;
                        }
                    }
                    break;
                case "Primer":
                    {
                        var item = e.Item as Telerik.Web.UI.GridDataItem;
                        if (item == null) return;

                        foreach (var ed in EpisodeDiagnoses)
                        {
                            if (ed.SequenceNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"])) ed.SRDiagnoseType = AppSession.Parameter.DiagnoseTypeMain;
                            else ed.SRDiagnoseType = "DiagnoseType-004";
                        }
                        EpisodeDiagnoses.Save();
                        EpisodeDiagnoses = null;

                        grdDiagnosaIdrg.Rebind();

                        var hasPrim = HasPrimaryDx();
                        hfHasPrimaryDx.Value = hasPrim ? "1" : "0";
                        RadAjaxManager.GetCurrent(Page)?.ResponseScripts.Add($@"
                         (function(){{
                           var hf = document.getElementById('{hfHasPrimaryDx.ClientID}');
                           if(hf) hf.value = '{(hasPrim ? "1" : "0")}';
                           var cbo = $find('{cboDiagnosaIdrg.ClientID}');
                           if (cbo) {{
                             cbo.get_items().clear();
                             cbo.requestItems(cbo.get_text() || '', false);
                           }}
                         }})();");
                    }
                    break;
            }
        }

        protected void grdDiagnosaInacbg_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
                            cboDiagnosaInacbg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, ed.DiagnoseID));
                            cboDiagnosaInacbg.SelectedValue = ed.DiagnoseID;
                        }
                    }
                    break;
                case "Primer":
                    {
                        var item = e.Item as Telerik.Web.UI.GridDataItem;
                        if (item == null) return;

                        foreach (var ed in EpisodeDiagnoseInaGrouppers)
                        {
                            if (ed.SequenceNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"])) ed.SRDiagnoseType = AppSession.Parameter.DiagnoseTypeMain;
                            else ed.SRDiagnoseType = "DiagnoseType-004";
                        }
                        EpisodeDiagnoseInaGrouppers.Save();
                        EpisodeDiagnoses = null;

                        grdDiagnosaInacbg.Rebind();
                    }
                    break;
            }
        }

        protected void grdDiagnosaIdrg_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var regno = Request.QueryString["regno"];
            var nomorSep = txtNoSep.Text;
            if (string.IsNullOrWhiteSpace(nomorSep))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                    "alert('Nomor SEP kosong.');", true);
                return;
            }

            var seq = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);
            var svc = new Common.Inacbg.v510.Service();
            var coll = ViewState["icd10"] as EpisodeDiagnoseCollection;
            var ed = coll?.FindByPrimaryKey(regno, seq);

            string codeToRemove = ed?.DiagnoseID;
            if (string.IsNullOrWhiteSpace(codeToRemove))
            {
                try { codeToRemove = (item["DiagnoseID"]?.Text ?? "").Trim(); } catch { /* no-op */ }
            }

            List<string> keep = new List<string>();
            if (coll != null && coll.Any())
            {
                keep = coll
                    .Where(d => !string.IsNullOrWhiteSpace(d.DiagnoseID)
                            && !string.Equals(d.DiagnoseID, codeToRemove, StringComparison.OrdinalIgnoreCase))
                    .Select(d => d.DiagnoseID.Trim())
                    .ToList();
            }
            else
            {
                try
                {
                    var getRes = svc.IDRGGetDiagnose(new Common.Inacbg.v510.Diagnose.Get.Data { nomor_sep = nomorSep });
                    SaveNccIdrg("IdrgDiagnosaGet", new { nomor_sep = nomorSep }, getRes);
                    LogApi("IDRGGetDiagnose", new { nomor_sep = nomorSep }, getRes, 0);

                    if (getRes?.Metadata?.IsValid == true)
                    {
                        var s = getRes?.Data?.String;
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            keep = s.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(x => x.Trim())
                                    .Where(x => !string.IsNullOrWhiteSpace(x))
                                    .ToList();
                        }
                        else if (getRes?.Data?.Expanded != null)
                        {
                            keep = getRes.Data.Expanded
                                    .Where(x => !string.IsNullOrWhiteSpace(x.Code))
                                    .Select(x => x.Code.Trim())
                                    .ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogApi("IDRGGetDiagnose(EX)", new { nomor_sep = nomorSep }, new { error = ex.Message }, 0);
                }

                if (!string.IsNullOrWhiteSpace(codeToRemove))
                    keep = keep.Where(c => !string.Equals(c, codeToRemove, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var payload = (keep != null && keep.Any()) ? string.Join("#", keep) : "#";

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var setRes = svc.IDRGSetDiagnose(new Common.Inacbg.v510.Diagnose.Data
            {
                nomor_sep = nomorSep,
                diagnosa = payload
            });
            sw.Stop();
            LogApi("IDRGSetDiagnose", new { nomor_sep = nomorSep, diagnosa = payload }, setRes, sw.ElapsedMilliseconds);
            SaveNccIdrg("IdrgDiagnosaSet", new { nomor_sep = nomorSep, diagnosa = payload }, setRes);

            if (!setRes.Metadata.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                    $"alert('{setRes.Metadata.Message}.');", true);
                return;
            }

            if (ed != null)
            {
                ed.MarkAsDeleted();
                coll.Save();
            }

            txtIDRGInfo.Text = string.Empty;
            txtIDRGJenisRawat.Text = string.Empty;
            txtIDRGMDC.Text = string.Empty;
            txtIDRGMDCCode.Text = string.Empty;
            txtIDRGDRG.Text = string.Empty;
            txtIDRGDRGCode.Text = string.Empty;
            txtCostWeight.Text = string.Empty;
            txtNBR.Text = string.Empty;
            rowCostWeight.Visible = false;
            rowNBR.Visible = false;
            txtIDRGStatus.Text = string.Empty;

            ViewState["icd10"] = null;
            grdDiagnosaIdrg.Rebind();
            //pnlIdrgResult.Visible = false;
            ShowAlert("idrg-dx-del-ok", "Diagnosa iDRG berhasil dihapus.");

            var hasPrim = HasPrimaryDx();
            hfHasPrimaryDx.Value = hasPrim ? "1" : "0";
            RadAjaxManager.GetCurrent(Page)?.ResponseScripts.Add($@"
             (function(){{
               var hf = document.getElementById('{hfHasPrimaryDx.ClientID}');
               if(hf) hf.value = '{(hasPrim ? "1" : "0")}';
               var cbo = $find('{cboDiagnosaIdrg.ClientID}');
               if (cbo) {{
                 // bersihkan dan request ulang supaya MarkIMItems jalan dgn flag terbaru
                 cbo.get_items().clear();
                 cbo.requestItems(cbo.get_text() || '', false);
               }}
             }})();");
        }

        protected void grdDiagnosaInacbg_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var item = e.Item as Telerik.Web.UI.GridDataItem;
            if (item == null) return;

            var regno = Request.QueryString["regno"];
            var nomorSep = txtNoSep.Text;
            if (string.IsNullOrWhiteSpace(nomorSep))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                    "alert('Nomor SEP kosong.');", true);
                return;
            }

            var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

            var coll = ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection;
            var ed = coll?.FindByPrimaryKey(regno, id);

            string codeToRemove = ed?.DiagnoseID;
            if (string.IsNullOrWhiteSpace(codeToRemove))
            {
                try
                {
                    codeToRemove = System.Web.HttpUtility.HtmlDecode(item["DiagnoseID"]?.Text ?? "").Trim();
                }
                catch { /* no-op */ }
            }
            if (string.IsNullOrWhiteSpace(codeToRemove)) return;

            var svc = new Common.Inacbg.v510.Service();

            List<string> keep = new List<string>();
            try
            {
                var getRes = svc.InacbgGetDiagnose(new Common.Inacbg.v510.Diagnose.Get.Data { nomor_sep = nomorSep });
                SaveNccIdrg("InacbgDiagnosaGet", new { nomor_sep = nomorSep }, getRes);
                LogApi("INACBGGetDiagnose", new { nomor_sep = nomorSep }, getRes, 0);

                if (getRes?.Metadata?.IsValid == true)
                {
                    var s = getRes?.Data?.String;
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        keep = s.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim())
                                .Where(x => !string.IsNullOrWhiteSpace(x))
                                .ToList();
                    }
                    else if (getRes?.Data?.Expanded != null)
                    {
                        keep = getRes.Data.Expanded
                                .Where(x => !string.IsNullOrWhiteSpace(x.Code))
                                .Select(x => x.Code.Trim())
                                .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LogApi("INACBGGetDiagnose(EX)", new { nomor_sep = nomorSep }, new { error = ex.Message }, 0);
                if (coll != null && coll.Any())
                {
                    keep = coll.Where(d => !string.IsNullOrWhiteSpace(d.DiagnoseID))
                               .Select(d => d.DiagnoseID.Trim())
                               .ToList();
                }
            }

            var idx = keep.FindIndex(c => string.Equals(c, codeToRemove, StringComparison.OrdinalIgnoreCase));
            if (idx >= 0) keep.RemoveAt(idx);

            var payload = (keep != null && keep.Any()) ? string.Join("#", keep) : "#"; // kosongkan jika tidak ada

            Common.Inacbg.v510.Diagnose.Response.Result detail = null;
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                detail = svc.INACBGSetDiagnose(new Common.Inacbg.v510.Diagnose.Data
                {
                    nomor_sep = nomorSep,
                    diagnosa = payload
                });
                sw.Stop();

                SaveNccIdrg("InacbgDiagnosaSet", new { nomor_sep = nomorSep, diagnosa = payload }, detail);
                LogApi("INACBGSetDiagnose", new { nomor_sep = nomorSep, diagnosa = payload }, detail, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-dx-del-ex",
                    $"alert('Gagal menghubungi INACBG: {ex.Message}');", true);
                return; // lokal jangan dihapus
            }

            if (detail?.Metadata?.IsValid != true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                    $"alert('{detail?.Metadata?.Message ?? "Penghapusan diagnosa ke INACBG gagal."}');", true);
                return; // lokal jangan dihapus
            }

            if (ed != null)
            {
                ed.MarkAsDeleted();
                coll.Save();
            }
            ViewState["icd10"] = null;

            grdDiagnosaInacbg.Rebind();

            txtInfo.Text = string.Empty;
            txtJenisRawat.Text = string.Empty;
            txtGroupName.Text = string.Empty;
            txtGroupID.Text = string.Empty;
            txtGroupPrice.Text = string.Empty;
            txtSubAcuteName.Text = string.Empty;
            txtSubAcuteID.Text = string.Empty;
            txtSubAcutePrice.Text = string.Empty;
            txtChronicName.Visible = false;
            txtChronicID.Visible = false;
            txtChronicPrice.Text = string.Empty;
            cboSpecialProcedure.SelectedValue = cboSpecialProcedure.Text = string.Empty;
            txtSpecialProcedurePrice.Text = string.Empty;
            cboSpecialProsthesis.SelectedValue = cboSpecialProsthesis.Text = string.Empty;
            txtSpecialProsthesisPrice.Text = string.Empty;
            cboSpecialInvestigation.SelectedValue = cboSpecialInvestigation.Text = string.Empty;
            txtSpecialInvestigationPrice.Text = string.Empty;
            cboSpecialDrug.SelectedValue = cboSpecialDrug.Text = string.Empty;
            txtSpecialDrugPrice.Text = string.Empty;
            txtGrouperTotal.Text = string.Empty;
            txtSelisihPersen.Text = string.Empty;
            txtTambahanBiaya.Text = string.Empty;

            ShowAlert("inacbg-dx-del-ok", "Diagnosa INACBG berhasil dihapus.");
        }

        //protected void grdDiagnosaInacbg_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    var item = e.Item as Telerik.Web.UI.GridDataItem;
        //    if (item == null) return;

        //    var id = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]);

        //    var coll = ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection;
        //    var ed = coll.FindByPrimaryKey(Request.QueryString["regno"], id);
        //    if (ed != null)
        //    {
        //        if (GetTransactionMode == "edit")
        //        {
        //            var diag = string.Empty;
        //            foreach (var d in (ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection).Where(d => d.DiagnoseID != ed.DiagnoseID))
        //            {
        //                diag += d.DiagnoseID + "#";
        //            }
        //            if (string.IsNullOrEmpty(diag)) diag = "#";

        //            var svc = new Common.Inacbg.v510.Service();
        //            var detail = svc.INACBGSetDiagnose(new Common.Inacbg.v510.Diagnose.Data()
        //            {
        //                nomor_sep = txtNoSep.Text,
        //                diagnosa = diag
        //            });

        //            var req = new Common.Inacbg.v510.Diagnose.Data
        //            {
        //                nomor_sep = txtNoSep.Text,
        //                diagnosa = diag
        //            };
        //            SaveNccIdrg("InacbgDiagnosaSet", req, detail);

        //            if (detail.Metadata.IsValid)
        //            {
        //                ed.MarkAsDeleted();
        //                coll.Save();
        //                ViewState["icd10"] = null;

        //                grdDiagnosaInacbg.Rebind();

        //                txtInfo.Text = string.Empty;
        //                txtJenisRawat.Text = string.Empty;
        //                txtGroupName.Text = string.Empty;
        //                txtGroupID.Text = string.Empty;
        //                txtGroupPrice.Text = string.Empty;
        //                txtSubAcuteName.Text = string.Empty;
        //                txtSubAcuteID.Text = string.Empty;
        //                txtSubAcutePrice.Text = string.Empty;
        //                txtChronicName.Visible = false;
        //                txtChronicID.Visible = false;
        //                txtChronicPrice.Text = string.Empty;
        //                cboSpecialProcedure.SelectedValue = string.Empty;
        //                cboSpecialProcedure.Text = string.Empty;
        //                txtSpecialProcedurePrice.Text = string.Empty;
        //                cboSpecialProsthesis.SelectedValue = string.Empty;
        //                cboSpecialProsthesis.Text = string.Empty;
        //                txtSpecialProsthesisPrice.Text = string.Empty;
        //                cboSpecialInvestigation.SelectedValue = string.Empty;
        //                cboSpecialInvestigation.Text = string.Empty;
        //                txtSpecialInvestigationPrice.Text = string.Empty;
        //                cboSpecialDrug.SelectedValue = string.Empty;
        //                cboSpecialDrug.Text = string.Empty;
        //                txtSpecialDrugPrice.Text = string.Empty;
        //                txtGrouperTotal.Text = string.Empty;
        //                txtSelisihPersen.Text = string.Empty;
        //                txtTambahanBiaya.Text = string.Empty;

        //                //pnlInacbgResult.Visible = false;
        //                ShowAlert("inacbg-dx-del-ok", "Diagnosa INACBG berhasil dihapus.");
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(), "grouper", string.Format("alert('{0}.');", detail.Metadata.Message), true);
        //            }
        //        }
        //        else
        //        {
        //            ed.MarkAsDeleted();
        //            coll.Save();
        //            ViewState["icd10"] = null;

        //            grdDiagnosaInacbg.Rebind();
        //        }
        //    }
        //}

        protected void grdDiagnosaIdrg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdDiagnosaIdrg.DataSource = EpisodeDiagnoses;
        }

        protected void grdDiagnosaInacbg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdDiagnosaInacbg.DataSource = EpisodeDiagnoseInaGrouppers;
        }

        protected void grdDiagnosaInacbg_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem it)
            {
                var lbl = it.FindControl("lblwarn") as Label;
                if (lbl != null)
                {
                    var notes = Convert.ToString(DataBinder.Eval(it.DataItem, "Notes"));
                    lbl.Text = notes;
                    lbl.Visible = !string.IsNullOrWhiteSpace(notes);
                }
            }
        }

        protected void grdProsedurInacbg_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem it)
            {
                var lbl = it.FindControl("lblwarnPx") as Label;
                if (lbl != null)
                {
                    var notes = Convert.ToString(DataBinder.Eval(it.DataItem, "Notes"));
                    lbl.Text = notes;
                    lbl.Visible = !string.IsNullOrWhiteSpace(notes);
                }
            }
        }

        protected void btnInsertDiagnosaIdrg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosaIdrg.SelectedValue)) return;

            var diag = new Diagnose();
            if (!diag.LoadByPrimaryKey(cboDiagnosaIdrg.SelectedValue))
            {
                diag = new Diagnose()
                {
                    DiagnoseID = cboDiagnosaIdrg.SelectedValue,
                    DtdNo = "0",
                    DiagnoseName = cboDiagnosaIdrg.Text.Replace("'", "`"),
                    IsChronicDisease = false,
                    IsDisease = false,
                    IsActive = true,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                diag.Save();

                diag = new Diagnose();
                diag.LoadByPrimaryKey(cboDiagnosaIdrg.SelectedValue);
            }

            var id = ViewState["seq10"] == null ? string.Empty : ViewState["seq10"].ToString();

            var ed = EpisodeDiagnoses.Where(eds => eds.RegistrationNo == Request.QueryString["regno"] && eds.DiagnoseID == diag.DiagnoseID).SingleOrDefault();
            if (ed == null) ed = EpisodeDiagnoses.AddNew();
            ed.RegistrationNo = Request.QueryString["regno"];
            //ed.SequenceNo = string.IsNullOrEmpty(id) ? (EpisodeDiagnoses.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
            var nextSeq = EpisodeDiagnoses.Any()
            ? EpisodeDiagnoses.Max(p => p.SequenceNo.ToInt()) + 1
            : 1;

            ed.SequenceNo = string.IsNullOrEmpty(id)
                ? nextSeq.ToString("000")
                : id;

            ed.DiagnoseID = diag.DiagnoseID;
            ed.DiagnoseName = diag.DiagnoseName;
            ed.SRDiagnoseType = (EpisodeDiagnoses.Any(d => d.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain) ? "DiagnoseType-004" : AppSession.Parameter.DiagnoseTypeMain);

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
            ed.DiagnoseType = std.ItemName;

            ed.DiagnosisText = cboDiagnosaIdrg.Text;
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
                var src = (ViewState["icd10"] as EpisodeDiagnoseCollection) ?? EpisodeDiagnoses;
                var diagnosa = string.Join("#", src.Select(d => d.DiagnoseID).Where(x => !string.IsNullOrWhiteSpace(x)));

                var svc = new Common.Inacbg.v510.Service();

                var req = new Common.Inacbg.v510.Diagnose.Data
                {
                    nomor_sep = txtNoSep.Text,
                    diagnosa = diagnosa
                };

                Common.Inacbg.v510.Diagnose.Response.Result detail = null;
                var sw = Stopwatch.StartNew();
                try
                {
                    detail = svc.IDRGSetDiagnose(req);
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    LogApi("IDRGSetDiagnose(EX)", req, new { error = ex.Message }, sw.ElapsedMilliseconds);
                    return;
                }
                sw.Stop();
                LogApi("IDRGSetDiagnose", req, detail, sw.ElapsedMilliseconds);
                SaveNccIdrg("IdrgDiagnosaSet", req, detail);

                if (detail.Metadata.IsValid)
                {
                    EpisodeDiagnoses.Save();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "grouper",
                        $"alert('{detail.Metadata.Message}.');", true);
                    return;
                }
            }
            else EpisodeDiagnoses.Save();

            grdDiagnosaIdrg.Rebind();

            cboDiagnosaIdrg.Items.Clear();
            cboDiagnosaIdrg.SelectedValue = string.Empty;
            cboDiagnosaIdrg.Text = string.Empty;
            cboDiagnosaIdrg.OpenDropDownOnLoad = false;

            ViewState["seq10"] = string.Empty;
        }

        protected void btnInsertDiagnosaInacbg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosaInacbg.SelectedValue)) return;

            var diag = new Diagnose();
            if (!diag.LoadByPrimaryKey(cboDiagnosaInacbg.SelectedValue))
            {
                diag = new Diagnose()
                {
                    DiagnoseID = cboDiagnosaInacbg.SelectedValue,
                    DtdNo = "0",
                    DiagnoseName = cboDiagnosaInacbg.Text.Replace("'", "`"),
                    IsChronicDisease = false,
                    IsDisease = false,
                    IsActive = true,
                    LastUpdateDateTime = DateTime.Now,
                    LastUpdateByUserID = AppSession.UserLogin.UserID
                };
                diag.Save();

                diag = new Diagnose();
                diag.LoadByPrimaryKey(cboDiagnosaInacbg.SelectedValue);
            }

            var id = ViewState["seq102"] == null ? string.Empty : ViewState["seq102"].ToString();

            var ed = EpisodeDiagnoseInaGrouppers.Where(eds => eds.RegistrationNo == Request.QueryString["regno"] && eds.DiagnoseID == diag.DiagnoseID).SingleOrDefault();
            if (ed == null) ed = EpisodeDiagnoseInaGrouppers.AddNew();
            ed.RegistrationNo = Request.QueryString["regno"];
            //ed.SequenceNo = string.IsNullOrEmpty(id) ? (EpisodeDiagnoses.Max(p => p.SequenceNo.ToInt()) + 1).ToString() : id;
            var nextSeq = EpisodeDiagnoseInaGrouppers.Any()
            ? EpisodeDiagnoseInaGrouppers.Max(p => p.SequenceNo.ToInt()) + 1
            : 1;

            ed.SequenceNo = string.IsNullOrEmpty(id)
                ? nextSeq.ToString("000")
                : id;

            ed.DiagnoseID = diag.DiagnoseID;
            ed.DiagnoseName = diag.DiagnoseName;
            ed.SRDiagnoseType = (EpisodeDiagnoses.Any(d => d.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain) ? "DiagnoseType-004" : AppSession.Parameter.DiagnoseTypeMain);

            var std = new AppStandardReferenceItem();
            std.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
            ed.DiagnoseType = std.ItemName;

            ed.DiagnosisText = cboDiagnosaInacbg.Text;
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
                var src = (ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection) ?? EpisodeDiagnoseInaGrouppers;
                var diagnosa = string.Join("#", src.Select(d => d.DiagnoseID).Where(x => !string.IsNullOrWhiteSpace(x)));

                var svc = new Common.Inacbg.v510.Service();

                var req = new Common.Inacbg.v510.Diagnose.Data
                {
                    nomor_sep = txtNoSep.Text,
                    diagnosa = diagnosa
                };

                Common.Inacbg.v510.Diagnose.Response.Result detail = null;
                var sw = Stopwatch.StartNew();
                try
                {
                    detail = svc.INACBGSetDiagnose(req);
                    SaveNccIdrg("InacbgDiagnosaSet", req, detail);
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    LogApi("INACBGSetDiagnose(EX)", req, new { error = ex.Message }, sw.ElapsedMilliseconds);
                    return;
                }
                sw.Stop();
                LogApi("INACBGSetDiagnose", req, detail, sw.ElapsedMilliseconds);
                SaveNccIdrg("InacbgDiagnosaSet", req, detail);

                if (detail.Metadata.IsValid)
                {
                    EpisodeDiagnoseInaGrouppers.Save();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-setdx",
                        $"alert('{detail.Metadata.Message}.');", true);
                    return;
                }
            }
            else EpisodeDiagnoseInaGrouppers.Save();

            grdDiagnosaInacbg.Rebind();

            cboDiagnosaInacbg.Items.Clear();
            cboDiagnosaInacbg.SelectedValue = string.Empty;
            cboDiagnosaInacbg.Text = string.Empty;
            cboDiagnosaInacbg.OpenDropDownOnLoad = false;

            ViewState["seq102"] = string.Empty;
        }

        protected void btnFilterDiagnosaIdrg_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosaIdrg.Text))
            {
                cboDiagnosaIdrg.Items.Clear();
                cboDiagnosaIdrg.SelectedValue = string.Empty;
                cboDiagnosaIdrg.Text = string.Empty;
                cboDiagnosaIdrg.OpenDropDownOnLoad = false;
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = cboDiagnosaIdrg.Text }, true);
            if (diag.Metadata.IsValid)
            {
                cboDiagnosaIdrg.Items.Clear();
                cboDiagnosaIdrg.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);
                    cboDiagnosaIdrg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd[1]));
                }
                cboDiagnosaIdrg.OpenDropDownOnLoad = true;
            }
            else
            {
                cboDiagnosaIdrg.Items.Clear();
                cboDiagnosaIdrg.SelectedValue = string.Empty;
                cboDiagnosaIdrg.Text = string.Empty;
                cboDiagnosaIdrg.OpenDropDownOnLoad = false;
            }
        }

        protected void btnFilterDiagnosaInacbg_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(cboDiagnosaInacbg.Text))
            {
                cboDiagnosaInacbg.Items.Clear();
                cboDiagnosaInacbg.SelectedValue = string.Empty;
                cboDiagnosaInacbg.Text = string.Empty;
                cboDiagnosaInacbg.OpenDropDownOnLoad = false;
                return;
            }

            var svc = new Common.Inacbg.v51.Service();
            var diag = svc.Search(new Common.Inacbg.v51.Procedure.Search.Data() { keyword = cboDiagnosaInacbg.Text }, true);
            if (diag.Metadata.IsValid)
            {
                cboDiagnosaInacbg.Items.Clear();
                cboDiagnosaInacbg.SelectedValue = string.Empty;
                foreach (var entity in diag.Response.Data)
                {
                    var icd = entity;
                    string namaDiagnosa = string.Format("{0}-{1}", icd[1], icd[0]);
                    cboDiagnosaInacbg.Items.Add(new Telerik.Web.UI.RadComboBoxItem(namaDiagnosa, icd[1]));
                }
                cboDiagnosaInacbg.OpenDropDownOnLoad = true;
            }
            else
            {
                cboDiagnosaInacbg.Items.Clear();
                cboDiagnosaInacbg.SelectedValue = string.Empty;
                cboDiagnosaInacbg.Text = string.Empty;
                cboDiagnosaInacbg.OpenDropDownOnLoad = false;
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
            var medicalNo = GetCurrentMedicalNo();
            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "YBRSGKP":
                    var pat = new Patient();
                    pat.LoadByMedicalNo(txtNoMR.Text);
                    if (!string.IsNullOrEmpty(pat.OldMedicalNo)) medicalNo = medicalNo.ToInt().ToString();
                    medicalNo = medicalNo.ToInt().ToString();
                    break;
            }

            var svc = new Common.Inacbg.v510.Service();
            var reqCreate = new Common.Inacbg.v510.Claim.Create.Data()
            {
                nomor_kartu = txtNoPeserta.Text,
                nomor_sep = txtNoSep.Text,
                nomor_rm = medicalNo,
                nama_pasien = txtNamaPasien.Text,
                tgl_lahir = txtTglLahir.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                gender = (rblJenisKelamin.SelectedValue == "M" ? "1" : "2")
            };
            var response = svc.Insert(reqCreate);
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

                    SaveNccIdrg("ClaimData", reqCreate, response);
                }

                var episode = string.Empty;
                foreach (GridDataItem dataItem in grdEpisodeRuangRawat.MasterTableView.Items)
                {
                    var id = dataItem["ID"].Text;
                    var jumlah = ((RadNumericTextBox)dataItem.FindControl("txtJumlah")).Value;
                    episode += string.Format("{0};{1}", id, jumlah.ToInt().ToString()) + "#";
                }

                var diag = string.Empty;
                var EpisodeDiagnose = ViewState["icd10"] as EpisodeDiagnoseCollection;
                if (EpisodeDiagnose != null)
                {
                    foreach (var d in EpisodeDiagnose.Where(v => !string.IsNullOrEmpty(v.DiagnoseID)).OrderBy(e => e.SRDiagnoseType))
                    {
                        diag += d.DiagnoseID + "#";
                    }
                    if (string.IsNullOrEmpty(diag)) diag = "#";
                }

                var proc = string.Empty;
                foreach (var d in (ViewState["icd9cm"] as EpisodeProcedureCollection))
                {
                    proc += d.ProcedureID + "#";
                }
                if (string.IsNullOrEmpty(proc)) proc = "#";

                //inacbg
                var diag2 = string.Empty;
                var EpisodeDiagnoseIna = ViewState["icd102"] as EpisodeDiagnoseInaGroupperCollection;
                if (EpisodeDiagnoseIna != null)
                {
                    foreach (var d in EpisodeDiagnoseIna.Where(v => !string.IsNullOrEmpty(v.DiagnoseID)).OrderBy(e => e.SRDiagnoseType))
                    {
                        diag2 += d.DiagnoseID + "#";
                    }
                    if (string.IsNullOrEmpty(diag2)) diag2 = "#";
                }

                var proc2 = string.Empty;
                foreach (var d in (ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection))
                {
                    proc2 += d.ProcedureID + "#";
                }
                if (string.IsNullOrEmpty(proc2)) proc2 = "#";


                DateTime datemasukEntry = txtTglMasuk.SelectedDate.Value
                        .AddHours(System.Convert.ToDouble(txtJamMasuk.Text.Substring(0, 2)))
                        .AddMinutes(System.Convert.ToDouble(txtJamMasuk.Text.Substring(3, 2)));

                DateTime datepulangEntry = txtTglPulang.SelectedDate.Value
                        .AddHours(System.Convert.ToDouble(txtJamPulang.Text.Substring(0, 2)))
                        .AddMinutes(System.Convert.ToDouble(txtJamPulang.Text.Substring(3, 2)));

                var jrParts = (rblJenisRawat.SelectedValue ?? string.Empty).Split('|');
                var jenis_rawat_val = jrParts.Length > 1 ? jrParts[1] : (rblJenisRawat.SelectedValue ?? string.Empty);

                var krRaw = (rblKelasRawat.SelectedValue ?? string.Empty);
                var krParts = krRaw.Split('|');
                var kelas_rawat_val = jenis_rawat_val == "1"
                    ? (krParts.Length > 1 ? krParts[1] : krRaw)
                    : krRaw;

                string upgrade_class_class_val = string.Empty;
                if (!string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue))
                {
                    var upParts = rblNaikKelasRawat.SelectedValue.Split('|');
                    upgrade_class_class_val = upParts.Length > 1 ? upParts[1] : rblNaikKelasRawat.SelectedValue;
                }

                var jamParts = (cboJaminan.SelectedValue ?? string.Empty).Split('|');
                var payor_id_val = jamParts.Length > 1 ? jamParts[1] : string.Empty;
                var payor_cd_val = jamParts.Length > 2 ? jamParts[2] : string.Empty;

                string cob_cd_val = string.Empty;
                if (!string.IsNullOrEmpty(cboCOB.SelectedValue))
                {
                    var cobParts = cboCOB.SelectedValue.Split('|');
                    cob_cd_val = cobParts.Length > 1 ? cobParts[1] : string.Empty;
                }

                var svc510 = new Common.Inacbg.v510.Service();
                var detail = svc510.Insert(new Common.Inacbg.v510.Detail.Datass()
                {
                    nomor_sep = txtNoSep.Text,
                    nomor_kartu = txtNoPeserta.Text,
                    tgl_masuk = datemasukEntry.ToString("yyyy-MM-dd HH:mm:ss"),
                    tgl_pulang = datepulangEntry.ToString("yyyy-MM-dd HH:mm:ss"),
                    cara_masuk = cboCaraMasuk.SelectedValue,
                    //jenis_rawat = rblJenisRawat.SelectedValue.Split('|')[1],
                    //kelas_rawat = rblJenisRawat.SelectedValue.Split('|')[1] == "1" ? rblKelasRawat.SelectedValue.Split('|')[1] : rblKelasRawat.SelectedValue,
                    jenis_rawat = jenis_rawat_val,
                    kelas_rawat = kelas_rawat_val,
                    adl_sub_acute = txtSubAcute.Text,
                    adl_chronic = txtChronic.Text,
                    icu_indikator = chkRawatIntensif.Checked ? "1" : "0",
                    icu_los = chkRawatIntensif.Checked ? txtRawatIntensif.Value.ToInt().ToString() : "0",
                    ventilator_hour = txtVentilator.Value.ToInt().ToString(),

                    use_ind = chkVentilator.Checked ? "1" : "0",
                    start_dttm = chkVentilator.Checked ? $"{txtTglIntubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamIntubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}" : string.Empty,
                    stop_dttm = chkVentilator.Checked ? $"{txtTglEkstubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamEkstubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}" : string.Empty,

                    upgrade_class_ind = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? "0" : (chkNaikKelas.Checked ? "1" : "0"),
                    //upgrade_class_class = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? string.Empty : rblNaikKelasRawat.SelectedValue.Split('|')[1],
                    upgrade_class_class = upgrade_class_class_val,
                    upgrade_class_los = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? string.Empty : txtLamaNaikKelas.Value.ToInt().ToString(),
                    upgrade_class_payor = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? string.Empty : cboPenjaminNaikKelas.SelectedValue,
                    add_payment_pct = txtSelisihPersen.Visible ? (txtSelisihPersen.Value ?? 0).ToString() : "0",
                    birth_weight = txtBeratLahir.Value.ToInt().ToString(),
                    sistole = txtSistole.Value.ToString(),
                    diastole = txtDiastole.Value.ToString(),
                    discharge_status = cboCaraPulang.SelectedValue,
                    tarif_rs = new Common.Inacbg.v510.Detail.TarifRss()
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
                    covid19_rs_darurat_ind = rblRsDaruratLapangan.SelectedValue,
                    covid19_co_insidense_ind = rblCoInsidens.SelectedValue,
                    covid19_penunjang_pengurang = new Common.Inacbg.v510.Detail.Covid19PenunjangPengurang()
                    {
                        lab_asam_laktat = chkAsamLaktat.Checked ? "1" : "0",
                        lab_procalcitonin = chkProcalcitonin.Checked ? "1" : "0",
                        lab_crp = chkCRP.Checked ? "1" : "0",
                        lab_kultur = chkKultur.Checked ? "1" : "0",
                        lab_d_dimer = chkDDimer.Checked ? "1" : "0",
                        lab_pt = chkPT.Checked ? "1" : "0",
                        lab_aptt = chkAPTT.Checked ? "1" : "0",
                        lab_waktu_pendarahan = chkWaktuPendarahan.Checked ? "1" : "0",
                        lab_anti_hiv = chkAntiHIV.Checked ? "1" : "0",
                        lab_analisa_gas = chkAnalisaGas.Checked ? "1" : "0",
                        lab_albumin = chkAlbumin.Checked ? "1" : "0",
                        rad_thorax_ap_pa = chkThoraxAPPA.Checked ? "1" : "0" 
                    },
                    terapi_konvalesen = txtTerapiKovalesen.Value.ToInt().ToString(),
                    akses_naat = rblKriteriaAksesNaat.SelectedValue,
                    isoman_ind = rblIsolasiMandiri.SelectedValue,
                    bayi_lahir_status_cd = cboStatusKelainan.SelectedValue,

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
                    //payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    //payor_cd = cboJaminan.SelectedValue.Split('|')[2],
                    //cob_cd = string.IsNullOrEmpty(cboCOB.SelectedValue) ? string.Empty : cboCOB.SelectedValue.Split('|')[1],
                    payor_id = payor_id_val,
                    payor_cd = payor_cd_val,
                    cob_cd = cob_cd_val,
                    coder_nik = AppSession.UserLogin.LicenseNo
                });
                if (!detail.Metadata.IsValid)
                {
                    args.MessageText = string.Format("{0} - {1}", detail.Metadata.ErrorNo, detail.Metadata.Message);
                    args.IsCancel = true;
                }
            }
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
                nthd.Query.es.Top = 1;
                nthd.Query.Where(nthd.Query.RegistrationNo == Request.QueryString["regno"]);
                nthd.Query.OrderBy(nthd.Query.TransactionNo.Ascending);
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
                nthd.Query.es.Top = 1;
                nthd.Query.Where(nthd.Query.RegistrationNo == Request.QueryString["regno"]);
                nthd.Query.OrderBy(nthd.Query.TransactionNo.Ascending);
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

            tdNaikKelas.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;
            chkNaikKelas.Checked = reg.ChargeClassID != reg.CoverageClassID;
            tdRawatIntensif.Visible = reg.SRRegistrationType == AppConstant.RegistrationType.InPatient;

            txtTglMasuk.SelectedDate = reg.RegistrationDate.Value.Date;
            txtJamMasuk.Text = reg.RegistrationTime;
            if (reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            {
                txtTglPulang.SelectedDate = txtTglMasuk.SelectedDate;
                txtJamPulang.Text = reg.RegistrationTime;
                if (AppSession.Parameter.HealthcareID != "RSCDR")
                {
                    txtTglPulang.DateInput.ReadOnly = true;
                    txtTglPulang.DatePopupButton.Enabled = false;
                    txtJamPulang.ReadOnly = true;
                }
            }
            else
            {
                if (reg.DischargeDate != null)
                {
                    txtTglPulang.SelectedDate = reg.DischargeDate.Value.Date;
                    txtJamPulang.Text = reg.DischargeTime;
                }
            }
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
            btnEdit.Enabled = false;

            if (!string.IsNullOrEmpty(txtNoSep.Text))
            {
                var svc = new Common.Inacbg.v510.Service();
                var response = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data() { nomor_sep = txtNoSep.Text });
                if (response.Metadata.IsValid)
                {
                    GetTransactionMode = "edit";

                    var data = response.DataResponse.Data;
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

                    var tariff = response.DataResponse.Data.TarifRs;
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
                        var regg = new RegistrationQuery("c");

                        cc.Select(regg.IsFromDispensary, i.SREklaimTariffGroup, i.SREklaimFactorGroup, (cc.PatientAmount.Sum() + cc.GuarantorAmount.Sum()).As("Amount"));
                        cc.InnerJoin(i).On(cc.ItemID == i.ItemID);
                        cc.InnerJoin(regg).On(cc.RegistrationNo == regg.RegistrationNo);
                        cc.Where(cc.IntermBillNo.In(ib.Select(b => b.IntermBillNo)));
                        cc.GroupBy(regg.IsFromDispensary, i.SREklaimTariffGroup, i.SREklaimFactorGroup);

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
                                var ob = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "13").Sum(t => t.Field<decimal>("Amount"));
                                txtObat.Value = ob != null ? Convert.ToDouble(ob) : 0;
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
                    ncc.PatientId = response.DataResponse.Data.PatientId.ToInt();
                    ncc.AdmissionId = response.DataResponse.Data.AdmissionId.ToInt();
                    ncc.HospitalAdmissionId = response.DataResponse.Data.HospitalAdmissionId.ToInt();
                    ncc.LastUpdateDateTime = DateTime.Now;
                    ncc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    ncc.AddPaymentAmt = string.IsNullOrEmpty(response.DataResponse.Data.AddPaymentAmt) ? 0 : Convert.ToDecimal(response.DataResponse.Data.AddPaymentAmt);
                    ncc.Save();

                    if (!string.IsNullOrEmpty(data.Diagnosa))
                    {
                        var dxRaw = response?.DataResponse?.Data?.DiagnosaInagrouper;
                        if (string.IsNullOrWhiteSpace(dxRaw))
                            dxRaw = response?.DataResponse?.Data?.Diagnosa;

                        if (!string.IsNullOrWhiteSpace(dxRaw))
                        {
                            var dxList = dxRaw
                                .Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => s.Trim())
                                .Where(s => !string.IsNullOrEmpty(s))
                                .ToList();

                            var coll = new EpisodeDiagnoseCollection();
                            coll.Query.Where(coll.Query.RegistrationNo == Request.QueryString["regno"]);
                            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
                            coll.Query.Load();

                            foreach (var c in coll.Where(c => !dxList.Contains(c.DiagnoseID)).ToList())
                                c.MarkAsDeleted();
                            coll.Save();

                            coll = new EpisodeDiagnoseCollection();
                            coll.Query.Where(coll.Query.RegistrationNo == Request.QueryString["regno"]);
                            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
                            coll.Query.Load();

                            int nextSeq = coll.Any() ? coll.Max(c => c.SequenceNo.ToInt()) + 1 : 1;
                            int idx = 1;
                            foreach (var code in dxList)
                            {
                                if (coll.Any(c => c.DiagnoseID == code)) { idx++; continue; }

                                var di = new Diagnose();
                                if (!di.LoadByPrimaryKey(code)) continue;

                                var ed = coll.AddNew();
                                ed.RegistrationNo = Request.QueryString["regno"];
                                ed.SequenceNo = nextSeq.ToString();
                                ed.DiagnoseID = di.DiagnoseID;
                                ed.DiagnoseName = di.DiagnoseName;
                                ed.SRDiagnoseType = idx == 1 ? "DiagnoseType-001" : "DiagnoseType-004";

                                var asri = new AppStandardReferenceItem();
                                asri.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
                                ed.DiagnoseType = asri.ItemName;

                                ed.DiagnosisText = di.DiagnoseName;
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

                                nextSeq++;
                                idx++;
                            }

                            coll.Save();
                        }
                    }

                    if (!string.IsNullOrEmpty(data.Procedure))
                    {
                        var piRaw = response?.DataResponse?.Data?.ProcedureInagrouper;
                        if (string.IsNullOrWhiteSpace(piRaw))
                            piRaw = response?.DataResponse?.Data?.Procedure;

                        if (!string.IsNullOrWhiteSpace(piRaw))
                        {
                            var segments = new List<(string code, int qty)>();
                            foreach (var raw in piRaw.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var s = raw.Trim(); if (s == "") continue;
                                var plus = s.IndexOf('+');
                                var code = (plus > 0 ? s.Substring(0, plus) : s).Trim();
                                int qty = 1;
                                if (plus > 0 && int.TryParse(s.Substring(plus + 1).Trim(), out var n) && n > 0) qty = n;
                                segments.Add((code, qty));
                            }
                            var requiredCodes = new HashSet<string>(segments.Select(x => x.code), StringComparer.OrdinalIgnoreCase);

                            var coll = new EpisodeProcedureCollection();
                            coll.Query.Where(coll.Query.RegistrationNo == Request.QueryString["regno"]);
                            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
                            coll.Query.Load();

                            foreach (var row in coll.Where(ep => !requiredCodes.Contains((ep.ProcedureID ?? "").Trim())).ToList())
                                row.MarkAsDeleted();
                            coll.Save();

                            coll = new EpisodeProcedureCollection();
                            coll.Query.Where(coll.Query.RegistrationNo == Request.QueryString["regno"]);
                            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
                            coll.Query.Load();

                            foreach (var row in coll.Where(ep => requiredCodes.Contains((ep.ProcedureID ?? "").Trim())).ToList())
                                row.MarkAsDeleted();
                            coll.Save();

                            coll = new EpisodeProcedureCollection();
                            coll.Query.Where(coll.Query.RegistrationNo == Request.QueryString["regno"]);
                            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
                            coll.Query.Load();

                            int nextSeq = coll.Any() ? coll.Max(c => c.SequenceNo.ToInt()) + 1 : 1;

                            var reggForProc = new Registration();
                            reggForProc.LoadByPrimaryKey(Request.QueryString["regno"]);

                            foreach (var (code, qty) in segments)
                            {
                                var proc = new Procedure();
                                if (!proc.LoadByPrimaryKey(code)) continue;

                                var ep = coll.AddNew();
                                ep.RegistrationNo = Request.QueryString["regno"];
                                ep.SequenceNo = $"{nextSeq:000}";
                                ep.ProcedureDate = reggForProc.RegistrationDate;
                                ep.ProcedureTime = reggForProc.RegistrationTime;
                                ep.ProcedureDate2 = reggForProc.RegistrationDate;
                                ep.ProcedureTime2 = reggForProc.RegistrationTime;
                                ep.ParamedicID = ep.ParamedicID2 = string.Empty;

                                ep.ProcedureID = proc.ProcedureID;
                                ep.ProcedureName = proc.ProcedureName;

                                ep.SRProcedureCategory = ep.SRAnestesi = ep.RoomID = string.Empty;
                                ep.IsCito = false; ep.IsVoid = false;
                                ep.LastUpdateDateTime = DateTime.Now; ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                ep.AssistantID1 = ep.AssistantID2 = string.Empty;
                                ep.Notes = ep.BookingNo = string.Empty;
                                ep.ParamedicID2a = ep.ParamedicID3a = ep.ParamedicID4a = string.Empty;
                                ep.ParamedicIDAnestesi = ep.AssistantIDAnestesi = string.Empty;
                                ep.InstrumentatorID1 = ep.InstrumentatorID2 = string.Empty;
                                ep.IsFromOperatingRoom = true;

                                ep.QtyICD = qty;

                                nextSeq++;
                            }
                            coll.Save();

                        }
                    }

                    var grouper = response.DataResponse.Data.Grouper;
                    if(grouper.ResponseIdrg != null)
                    {
                        var jr = response?.DataResponse?.Data?.JenisRawat?.ToString(); // "1","2","3"
                        var kr = response?.DataResponse?.Data?.KelasRawat?.ToString(); // "1","2","3"
                        int.TryParse(response?.DataResponse?.Data?.Los?.ToString(), out var los); // int

                        string jenis = jr == "1" ? "Rawat Inap"
                                    : jr == "2" ? "Rawat Jalan"
                                    : jr == "3" ? "IGD"
                                    : "";

                        string kelas = kr == "1" ? "Kelas 1"
                                    : kr == "2" ? "Kelas 2"
                                    : kr == "3" ? "Kelas 3"
                                    : "";

                        string text = jenis
                                    + (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas))
                                    + (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");

                        var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));

                        var s = response?.DataResponse?.Data?.Grouper.ResponseIdrg.status_cd;

                        var kdtrf = response?.DataResponse?.Data?.KodeTarif?.ToString();

                        var trf = new AppStandardReferenceItem();
                        trf.LoadByPrimaryKey("BpjsTariffType", kdtrf);

                        var stausklaim = Convert.ToString(response?.DataResponse?.Data?.KlaimStatusCd)?.Trim();
                        var isFinalklaim = string.Equals(stausklaim, "final", StringComparison.OrdinalIgnoreCase);
                        var statusidrg = Convert.ToString(grouper.ResponseIdrg.status_cd)?.Trim();
                        bool isFinalidrg = string.Equals(statusidrg, "final", StringComparison.OrdinalIgnoreCase);
                        var statusiancbg = Convert.ToString(grouper?.ResponseInacbg?.status_cd)?.Trim();
                        var desc = Convert.ToString(grouper?.ResponseInacbg?.Cbg?.Description) ?? string.Empty;
                        var isFinalinacbg = string.Equals(statusiancbg, "final", StringComparison.OrdinalIgnoreCase);
                        var hasFailed = desc.IndexOf("GAGAL:", StringComparison.OrdinalIgnoreCase) >= 0
                                       || desc.IndexOf("FAILED:", StringComparison.OrdinalIgnoreCase) >= 0;

                        txtIDRGInfo.Text =
                            $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} - " +
                            $"{grouper?.ResponseIdrg?.script_version ?? "-"} / {grouper?.ResponseIdrg?.logic_version ?? "-"}";

                        txtIDRGJenisRawat.Text = text.Trim();

                        var mdcDesc = grouper.ResponseIdrg.mdc_description?.Trim();
                        var mdcCode = grouper.ResponseIdrg.mdc_number?.Trim();
                        var drgDesc = grouper.ResponseIdrg.drg_description?.Trim();
                        var drgCode = grouper.ResponseIdrg.drg_code?.Trim();

                        bool isUngroup = HasUngroupFlag(mdcDesc) || HasUngroupFlag(drgDesc);

                        bool hasIdrgResult =
                            !string.IsNullOrWhiteSpace(mdcCode) ||
                            !string.IsNullOrWhiteSpace(drgCode) ||
                            !string.IsNullOrWhiteSpace(mdcDesc) ||
                            !string.IsNullOrWhiteSpace(drgDesc);

                        bool canGroupIdrg = !isFinalidrg;
                        bool canFinalizeIdrg = !isFinalidrg && hasIdrgResult && !isUngroup;
                        bool canEditIdrg = isFinalidrg && !isFinalklaim;

                        txtIDRGMDC.Text = mdcDesc;
                        txtIDRGMDCCode.Text = mdcCode;
                        txtIDRGDRG.Text = drgDesc;
                        txtIDRGDRGCode.Text = drgCode;

                        txtCostWeight.Text = grouper.ResponseIdrg.total_cost_weight;
                        txtNBR.Text = Convert.ToInt64(grouper.ResponseIdrg.nbr)
                                          .ToString("N0", new System.Globalization.CultureInfo("id-ID"));

                        bool hasCostWeight = !string.IsNullOrWhiteSpace(grouper.ResponseIdrg.total_cost_weight);
                        bool hasNBR = !string.IsNullOrWhiteSpace(grouper.ResponseIdrg.nbr);

                        rowCostWeight.Visible = hasCostWeight;
                        rowNBR.Visible = hasNBR;

                        rowNoteBelumFinal.Visible = hasCostWeight || hasNBR;

                        txtIDRGStatus.Text = string.IsNullOrWhiteSpace(s) ? "" : char.ToUpper(s[0]) + (s.Length > 1 ? s.Substring(1).ToLower() : "");

                        btnIdrgGroup.Enabled = canGroupIdrg;
                        btnFnliDRG.Enabled = canFinalizeIdrg;
                        btnIdrgEdit.Enabled = canEditIdrg;

                        //btnFinalIna.Enabled = !isFinalinacbg && !hasFailed;
                        //btnImprtIdrg.Enabled = !isFinalinacbg && !hasFailed;
                        //btnInacbgGroup.Enabled = !isFinalinacbg && !hasFailed;
                        //btnInacbgEdit.Enabled = isFinalinacbg && !hasFailed && !isFinalklaim;

                        btnFinal.Enabled = !isFinalklaim && isFinalinacbg && isFinalidrg;
                        btnEdit.Enabled = isFinalklaim;
                        btnPrint.Enabled = isFinalklaim;
                        btnKirim.Enabled = isFinalklaim;
                    }

                    if (grouper.ResponseInacbg != null)
                    {
                        var cov = grouper.ResponseInacbg.Covid19Data;
                        if (cov != null)
                        {
                            cboNoPeserta.SelectedValue = cov.NoKartuT;
                            cboStatusCov.SelectedValue = cov.Covid19StatusCd;
                            var episodes = cov.Episodes;
                            if (episodes != null && episodes.Any())
                            {
                                foreach (var episode in episodes)
                                {
                                    EpisodeRuangRawatCollection.Add(new EpisodeRuangRawat2() { Key = EpisodeRuangRawatCollection.Count() + 1, ID = episode.EpisodeClassCd, Nama = episode.EpisodeClassNm, Jumlah = episode.Los.ToInt() });
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

                        var jr = response?.DataResponse?.Data?.JenisRawat?.ToString(); // "1","2","3"
                        var kr = response?.DataResponse?.Data?.KelasRawat?.ToString(); // "1","2","3"
                        int.TryParse(response?.DataResponse?.Data?.Los?.ToString(), out var los); // int

                        string jenis = jr == "1" ? "Rawat Inap"
                                    : jr == "2" ? "Rawat Jalan"
                                    : jr == "3" ? "IGD"
                                    : "";

                        string kelas = kr == "1" ? "Kelas 1"
                                    : kr == "2" ? "Kelas 2"
                                    : kr == "3" ? "Kelas 3"
                                    : "";

                        string text = jenis
                                    + (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas))
                                    + (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");

                        var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));

                        var s = response?.DataResponse?.Data?.Grouper.ResponseIdrg.status_cd;

                        var kdtrf = response?.DataResponse?.Data?.KodeTarif?.ToString();

                        var trf = new AppStandardReferenceItem();
                        trf.LoadByPrimaryKey("BpjsTariffType", kdtrf);

                        txtInfo.Text = $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} •• " + $"{kelas} •• Tarif : " + $"{trf.ItemName}";
                        txtStatusIna.Text = string.IsNullOrWhiteSpace(grouper.ResponseInacbg.status_cd) ? "" : char.ToUpper(grouper.ResponseInacbg.status_cd[0]) + (grouper.ResponseInacbg.status_cd.Length > 1 ? grouper.ResponseInacbg.status_cd.Substring(1).ToLower() : "");
                        txtJenisRawat.Text = text.Trim();
                        txtGroupName.Text = grouper.ResponseInacbg.Cbg.Description;
                        txtGroupID.Text = grouper.ResponseInacbg.Cbg.Code;
                        txtGroupPrice.Value = Convert.ToDouble(grouper.ResponseInacbg.BaseTariff);

                        txtStatusKlaim.Text = string.IsNullOrWhiteSpace(response?.DataResponse?.Data?.KlaimStatusCd) ? "" : char.ToUpper(response.DataResponse.Data.KlaimStatusCd[0]) + (response.DataResponse.Data.KlaimStatusCd.Length > 1 ? response.DataResponse.Data.KlaimStatusCd.Substring(1).ToLower() : "");

                        var stausklaim = Convert.ToString(response?.DataResponse?.Data?.KlaimStatusCd)?.Trim();
                        var isFinalklaim = string.Equals(stausklaim, "final", StringComparison.OrdinalIgnoreCase);
                        var statusidrg = Convert.ToString(grouper.ResponseIdrg.status_cd)?.Trim();
                        bool isFinalidrg = string.Equals(statusidrg, "final", StringComparison.OrdinalIgnoreCase);
                        var statusiancbg = Convert.ToString(grouper?.ResponseInacbg?.status_cd)?.Trim();
                        var desc = Convert.ToString(grouper?.ResponseInacbg?.Cbg?.Description) ?? string.Empty;
                        var isFinalinacbg = string.Equals(statusiancbg, "final", StringComparison.OrdinalIgnoreCase);
                        var hasFailed = desc.IndexOf("GAGAL:", StringComparison.OrdinalIgnoreCase) >= 0
                                       || desc.IndexOf("FAILED:", StringComparison.OrdinalIgnoreCase) >= 0;

                        txtIDRGInfo.Text =
                            $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} - " +
                            $"{grouper?.ResponseIdrg?.script_version ?? "-"} / {grouper?.ResponseIdrg?.logic_version ?? "-"}";

                        txtIDRGJenisRawat.Text = text.Trim();

                        var mdcDesc = grouper?.ResponseIdrg?.mdc_description?.Trim();
                        var mdcCode = grouper?.ResponseIdrg?.mdc_number?.Trim();
                        var drgDesc = grouper?.ResponseIdrg?.drg_description?.Trim();
                        var drgCode = grouper?.ResponseIdrg?.drg_code?.Trim();

                        bool isUngroup = HasUngroupFlag(mdcDesc) || HasUngroupFlag(drgDesc);

                        bool hasIdrgResult =
                            !string.IsNullOrWhiteSpace(mdcCode) ||
                            !string.IsNullOrWhiteSpace(drgCode) ||
                            !string.IsNullOrWhiteSpace(mdcDesc) ||
                            !string.IsNullOrWhiteSpace(drgDesc);

                        bool canGroupIdrg = !isFinalidrg;
                        bool canFinalizeIdrg = !isFinalidrg && hasIdrgResult && !isUngroup;
                        bool canEditIdrg = isFinalidrg && !isFinalklaim;

                        txtIDRGMDC.Text = mdcDesc;
                        txtIDRGMDCCode.Text = mdcCode;
                        txtIDRGDRG.Text = drgDesc;
                        txtIDRGDRGCode.Text = drgCode;

                        txtCostWeight.Text = grouper.ResponseIdrg.total_cost_weight;
                        txtNBR.Text = Convert.ToInt64(grouper.ResponseIdrg.nbr)
                                          .ToString("N0", new System.Globalization.CultureInfo("id-ID"));

                        bool hasCostWeight = !string.IsNullOrWhiteSpace(grouper.ResponseIdrg.total_cost_weight);
                        bool hasNBR = !string.IsNullOrWhiteSpace(grouper.ResponseIdrg.nbr);

                        rowCostWeight.Visible = hasCostWeight;
                        rowNBR.Visible = hasNBR;

                        rowNoteBelumFinal.Visible = hasCostWeight || hasNBR;

                        txtIDRGStatus.Text = string.IsNullOrWhiteSpace(s) ? "" : char.ToUpper(s[0]) + (s.Length > 1 ? s.Substring(1).ToLower() : "");

                        btnIdrgGroup.Enabled = canGroupIdrg;
                        btnFnliDRG.Enabled = canFinalizeIdrg;
                        btnIdrgEdit.Enabled = canEditIdrg;

                        btnFinalIna.Enabled = !isFinalinacbg && !hasFailed;
                        btnImprtIdrg.Enabled = !isFinalinacbg && !hasFailed;
                        btnInacbgGroup.Enabled = !isFinalinacbg && !hasFailed;
                        btnInacbgEdit.Enabled = isFinalinacbg && !hasFailed && !isFinalklaim;

                        btnFinal.Enabled = !isFinalklaim && isFinalinacbg && isFinalidrg;
                        btnEdit.Enabled = isFinalklaim;
                        btnPrint.Enabled = isFinalklaim;
                        btnKirim.Enabled = isFinalklaim;

                        //var cmg = grouper.ResponseInacbg.SpecialCmg;
                        //if (cmg != null)
                        //{
                        //    if (cmg.Any(c => c.Type == "Special Drug"))
                        //    {
                        //        cboSpecialDrug.Items.Clear();
                        //        cboSpecialDrug.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                        //    }
                        //    foreach (var sco in cmg)
                        //    {
                        //        if (sco.Type == "Special Drug")
                        //        {
                        //            if (!string.IsNullOrWhiteSpace(sco.Code))
                        //            {
                        //                string code = sco.Code.Split('-')[0] + sco.Code.Split('-')[1];
                        //                cboSpecialDrug.Items.Add(new RadComboBoxItem(sco.Description, code));
                        //            }
                        //        }
                        //    }
                        //    if (cmg.Any(c => c.Type == "Special Drug"))
                        //    {
                        //        if (!string.IsNullOrEmpty(ncc.SpecialDrugID)) cboSpecialDrug.SelectedValue = ncc.SpecialDrugID;
                        //        if ((ncc.SpecialDrugAmount ?? 0) > 0) txtSpecialDrugPrice.Value = Convert.ToDouble(ncc.SpecialDrugAmount ?? 0);
                        //    }
                        //}

                        var cmg = grouper.ResponseInacbg?.SpecialCmg
                                  ?? Array.Empty<Common.Inacbg.v510.Claim.Get.GetDetailResponse.SpecialCmg>();

                        cboSpecialProcedure.Items.Clear();
                        cboSpecialProsthesis.Items.Clear();
                        cboSpecialInvestigation.Items.Clear();
                        cboSpecialDrug.Items.Clear();

                        cboSpecialProcedure.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                        cboSpecialProsthesis.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                        cboSpecialInvestigation.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                        cboSpecialDrug.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                        foreach (var sco in cmg)
                        {
                            if (sco == null || string.IsNullOrWhiteSpace(sco.Code)) continue;

                            if (sco.Type.Equals("Special Procedure", StringComparison.OrdinalIgnoreCase))
                                cboSpecialProcedure.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                            else if (sco.Type.Equals("Special Prosthesis", StringComparison.OrdinalIgnoreCase))
                                cboSpecialProsthesis.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                            else if (sco.Type.Equals("Special Investigation", StringComparison.OrdinalIgnoreCase))
                                cboSpecialInvestigation.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                            else if (sco.Type.Equals("Special Drug", StringComparison.OrdinalIgnoreCase))
                                cboSpecialDrug.Items.Add(new RadComboBoxItem(sco.Description, sco.Code));
                        }

                        Func<string, string> norm = spc => System.Text.RegularExpressions.Regex.Replace((spc ?? "").Trim(), @"[^0-9A-Za-z]", "");

                        var _ncc = new NccInacbg();
                        if (!_ncc.LoadByPrimaryKey(Request.QueryString["regno"])) _ncc = new NccInacbg();

                        void selectByValue(RadComboBox cbo, string value)
                        {
                            cbo.ClearSelection();
                            if (string.IsNullOrWhiteSpace(value)) { cbo.Text = string.Empty; return; }

                            var it = cbo.FindItemByValue(value);
                            if (it == null)
                            {
                                string norms(string spcs) => System.Text.RegularExpressions.Regex.Replace((spcs ?? "").Trim(), @"[^0-9A-Za-z]", "");
                                it = cbo.Items.Cast<RadComboBoxItem>()
                                     .FirstOrDefault(x => string.Equals(norms(x.Value), norm(value), StringComparison.OrdinalIgnoreCase));
                            }

                            if (it != null)
                            {
                                it.Selected = true;
                                cbo.SelectedIndex = cbo.Items.IndexOf(it);
                                if (cbo.AllowCustomText) cbo.Text = it.Text;
                            }
                            else
                            {
                                cbo.Text = string.Empty;
                            }
                        }

                        selectByValue(cboSpecialProcedure, _ncc.SpecialProcedureID);
                        selectByValue(cboSpecialProsthesis, _ncc.SpecialProsthesisID);
                        selectByValue(cboSpecialInvestigation, _ncc.SpecialInvestigationID);
                        selectByValue(cboSpecialDrug, _ncc.SpecialDrugID);


                        double tariffOf(string code, string type) =>
                            (double)(cmg.FirstOrDefault(x =>
                                x.Type.Equals(type, StringComparison.OrdinalIgnoreCase) &&
                                x.Code.Equals(code ?? "", StringComparison.OrdinalIgnoreCase))?.Tariff ?? 0);

                        txtSpecialProcedurePrice.Value = (_ncc.SpecialProcedureAmount ?? 0) > 0
                            ? (double?)_ncc.SpecialProcedureAmount
                            : (!string.IsNullOrEmpty(cboSpecialProcedure.SelectedValue)
                                ? tariffOf(cboSpecialProcedure.SelectedValue, "Special Procedure") : 0);

                        txtSpecialProsthesisPrice.Value = (_ncc.SpecialProsthesisAmount ?? 0) > 0
                            ? (double?)_ncc.SpecialProsthesisAmount
                            : (!string.IsNullOrEmpty(cboSpecialProsthesis.SelectedValue)
                                ? tariffOf(cboSpecialProsthesis.SelectedValue, "Special Prosthesis") : 0);

                        txtSpecialInvestigationPrice.Value = (_ncc.SpecialInvestigationAmount ?? 0) > 0
                            ? (double?)_ncc.SpecialInvestigationAmount
                            : (!string.IsNullOrEmpty(cboSpecialInvestigation.SelectedValue)
                                ? tariffOf(cboSpecialInvestigation.SelectedValue, "Special Investigation") : 0);

                        txtSpecialDrugPrice.Value = (_ncc.SpecialDrugAmount ?? 0) > 0
                            ? (double?)_ncc.SpecialDrugAmount
                            : (!string.IsNullOrEmpty(cboSpecialDrug.SelectedValue)
                                ? tariffOf(cboSpecialDrug.SelectedValue, "Special Drug") : 0);

                        txtGrouperTotal.Value =
                            (txtGroupPrice.Value ?? 0) + (txtChronicPrice.Value ?? 0) + (txtSubAcutePrice.Value ?? 0) +
                            (txtSpecialProcedurePrice.Value ?? 0) + (txtSpecialProsthesisPrice.Value ?? 0) +
                            (txtSpecialInvestigationPrice.Value ?? 0) + (txtSpecialDrugPrice.Value ?? 0);

                        ncc = new NccInacbg();
                        if (!ncc.LoadByPrimaryKey(Request.QueryString["regno"])) ncc = new NccInacbg();
                        ncc.CoverageAmount = Convert.ToDecimal(txtGroupPrice.Value);
                        ncc.Save();
                    }

                    txtTambahanBiaya.Value = string.IsNullOrEmpty(response.DataResponse.Data.AddPaymentAmt) ? 0 : Convert.ToDouble(response.DataResponse.Data.AddPaymentAmt);

                    reg = new Registration();
                    reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                    reg.PlavonAmount = Convert.ToDecimal(txtGrouperTotal.Value);
                    reg.Save();

                    LoadUploadFile();
                }
                else
                {
                    GetTransactionMode = "new";

                    //pnlIdrgResult.Visible = false;
                    //btnFnliDRG.Visible = false;
                    //btnFnliDRG.Enabled = false;
                    //btnIdrgEdit.Visible = false;

                    //fsINACBG.Visible = false;
                    //pnlInacbgResult.Visible = false;
                    //btnFinalIna.Visible = false;
                    //btnFinalIna.Enabled = false;
                    //btnInacbgEdit.Visible = false;

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
                        var regg = new RegistrationQuery("c");

                        cc.Select(regg.IsFromDispensary, i.SREklaimTariffGroup, i.SREklaimFactorGroup, (cc.PatientAmount.Sum() + cc.GuarantorAmount.Sum()).As("Amount"));
                        cc.InnerJoin(i).On(cc.ItemID == i.ItemID);
                        cc.InnerJoin(regg).On(cc.RegistrationNo == regg.RegistrationNo);
                        cc.Where(cc.IntermBillNo.In(ib.Select(b => b.IntermBillNo)));
                        cc.GroupBy(regg.IsFromDispensary, i.SREklaimTariffGroup, i.SREklaimFactorGroup);

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

                            var ob = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "13").Sum(t => t.Field<decimal>("Amount"));
                            txtObat.Value = ob != null ? Convert.ToDouble(ob) : 0;
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

                //pnlIdrgResult.Visible = false;
                //btnFnliDRG.Visible = false;
                //btnFnliDRG.Enabled = false;
                //btnIdrgEdit.Visible = false;

                //fsINACBG.Visible = false;
                //pnlInacbgResult.Visible = false;
                //btnFinalIna.Visible = false;
                //btnFinalIna.Enabled = false;
                //btnInacbgEdit.Visible = false;

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
                    var regg = new RegistrationQuery("c");

                    cc.Select(regg.IsFromDispensary, i.SREklaimTariffGroup, i.SREklaimFactorGroup, (cc.PatientAmount.Sum() + cc.GuarantorAmount.Sum()).As("Amount"));
                    cc.InnerJoin(i).On(cc.ItemID == i.ItemID);
                    cc.InnerJoin(regg).On(cc.RegistrationNo == regg.RegistrationNo);
                    cc.Where(cc.IntermBillNo.In(ib.Select(b => b.IntermBillNo)));
                    cc.GroupBy(regg.IsFromDispensary, i.SREklaimTariffGroup, i.SREklaimFactorGroup);

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

                        var ob = tbl.AsEnumerable().Where(t => t.Field<string>("SREklaimTariffGroup") == "13").Sum(t => t.Field<decimal>("Amount"));
                        txtObat.Value = ob != null ? Convert.ToDouble(ob) : 0;
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

        protected void btnIdrgGroup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoSep.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-sep", "alert('Nomor SEP belum diisi.');", true);
                return;
            }

            try
            {
                var svc = new Common.Inacbg.v510.Service();

                var detailBefore = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text });
                var klaimStatus = detailBefore?.DataResponse?.Data?.KlaimStatusCd;

                var reqGroup = new Common.Inacbg.v510.Gruoper.IdrgGrouper.Data { nomor_sep = txtNoSep.Text };
                var run = (klaimStatus != null && klaimStatus.Equals("final", StringComparison.OrdinalIgnoreCase))
                    ? svc.EditIdrgGrouper(reqGroup)
                    : svc.IdrgGrouper(reqGroup);
                if (run == null || run.Meta == null || !run.Meta.IsValid)
                {
                    var msg = run?.Meta?.Message ?? "Gagal menjalankan (Group/Edit) iDRG.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-group-fail", $"alert('{msg}');", true);
                    return;
                }

                SaveNccIdrg("IdrgGroup", reqGroup, run);

                var detail = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text });
                if (detail == null || detail.Metadata == null || !detail.Metadata.IsValid)
                {
                    var msg = detail?.Metadata?.Message ?? "Gagal mengambil detail setelah (Group/Edit) iDRG.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-detail-fail", $"alert('{msg}');", true);
                    return;
                }

                var d = detail.DataResponse?.Data;
                var g = d?.Grouper;

                if (g?.ResponseIdrg != null)
                {
                    var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));
                    txtIDRGInfo.Text =
                        $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} - " +
                        $"{g.ResponseIdrg.script_version ?? "-"} / {g.ResponseIdrg.logic_version ?? "-"}";

                    var jr = d?.JenisRawat?.ToString();
                    var kr = d?.KelasRawat?.ToString();
                    int.TryParse(d?.Los, out var los);

                    string jenis = jr == "1" ? "Rawat Inap" : jr == "2" ? "Rawat Jalan" : jr == "3" ? "IGD" : string.Empty;
                    string kelas = kr == "1" ? "Kelas 1" : kr == "2" ? "Kelas 2" : kr == "3" ? "Kelas 3" : string.Empty;
                    string jenisKelasText = jenis + (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas)) + (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");
                    txtIDRGJenisRawat.Text = jenisKelasText.Trim();
                    txtCostWeight.Text = g.ResponseIdrg.total_cost_weight;
                    txtNBR.Text = Convert.ToInt64(g.ResponseIdrg.nbr)
                                      .ToString("N0", new System.Globalization.CultureInfo("id-ID"));

                    bool hasCostWeight = !string.IsNullOrWhiteSpace(g.ResponseIdrg.total_cost_weight);
                    bool hasNBR = !string.IsNullOrWhiteSpace(g.ResponseIdrg.nbr);

                    rowCostWeight.Visible = hasCostWeight;
                    rowNBR.Visible = hasNBR;
                    rowNoteBelumFinal.Visible = hasCostWeight || hasNBR;
                    txtIDRGStatus.Text = g?.ResponseIdrg.status_cd ?? string.Empty;

                    var mdcDesc = g?.ResponseIdrg?.mdc_description?.Trim();
                    var mdcCode = g?.ResponseIdrg?.mdc_number?.Trim();
                    var drgDesc = g?.ResponseIdrg?.drg_description?.Trim();
                    var drgCode = g?.ResponseIdrg?.drg_code?.Trim();

                    txtIDRGMDC.Text = string.IsNullOrWhiteSpace(mdcDesc) ? "-" : mdcDesc;
                    txtIDRGMDCCode.Text = string.IsNullOrWhiteSpace(mdcCode) ? "-" : mdcCode;
                    txtIDRGDRG.Text = string.IsNullOrWhiteSpace(drgDesc) ? "-" : drgDesc;
                    txtIDRGDRGCode.Text = string.IsNullOrWhiteSpace(drgCode) ? "-" : drgCode;

                    bool isUngroup = HasUngroupFlag(mdcDesc) || HasUngroupFlag(drgDesc);

                    hfIdrgIsUngroup.Value = isUngroup ? "1" : "0";

                    SetErrorColor(txtIDRGMDC, isUngroup);
                    SetErrorColor(txtIDRGMDCCode, isUngroup);
                    SetErrorColor(txtIDRGDRG, isUngroup);
                    SetErrorColor(txtIDRGDRGCode, isUngroup);

                    btnFnliDRG.Enabled = !isUngroup;
                    //btnFnliDRG.Visible = !isUngroup;

                    //pnlIdrgResult.Visible = true;

                    ShowAlert("idrg-group-ok", isUngroup
                        ? "(Group/Edit) Grouping iDRG Sukses, namun hasil UNGROUPABLE/UNRELATED."
                        : "(Group/Edit) Grouping iDRG Sukses.");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-empty",
                        "alert('(Group/Edit) iDRG selesai, tetapi response_idrg tidak ada.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-ex",
                     $"alert('Terjadi kesalahan saat (Group/Edit) iDRG: {ex.Message.Replace("'", "`")}');", true);
            }
        }

        protected void btnFnliDRG_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoSep.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-sep", "alert('Nomor SEP belum diisi.');", true);
                return;
            }

            try
            {
                var svc = new Common.Inacbg.v510.Service();

                var reqCheck = new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Claim.Get.GetDetailResponse.Response check = null;
                var swCheck = Stopwatch.StartNew();
                try
                {
                    check = svc.GetDetail(reqCheck);
                    SaveNccIdrg("GetClaimData", reqCheck, check);
                }
                catch (Exception ex)
                {
                    swCheck.Stop();
                    LogApi("GetDetail(Before Final iDRG)(EX)", reqCheck, new { error = ex.Message }, swCheck.ElapsedMilliseconds);
                    return;
                }
                swCheck.Stop();
                LogApi("GetDetail(Before Final iDRG)", reqCheck, check, swCheck.ElapsedMilliseconds);

                var idrgResp = check?.DataResponse?.Data?.Grouper?.ResponseIdrg;

                if (!(check?.Metadata?.IsValid == true))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-no-final",
                        "alert('iDRG belum menghasilkan group yang valid (Ungroupable/Unrelated). Tidak bisa FINAL.');", true);
                    return;
                }

                var reqFinal = new Common.Inacbg.v510.Gruoper.IdrgGrouper.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Gruoper.IdrgGrouper.Result run = null;
                var swFinal = Stopwatch.StartNew();
                try
                {
                    run = svc.FinalIdrgGrouper(reqFinal);
                    SaveNccIdrg("IdrgFinal", reqFinal, run);
                }
                catch (Exception ex)
                {
                    swFinal.Stop();
                    LogApi("FinalIdrgGrouper(EX)", reqFinal, new { error = ex.Message }, swFinal.ElapsedMilliseconds);
                    return;
                }
                swFinal.Stop();
                LogApi("FinalIdrgGrouper", reqFinal, run, swFinal.ElapsedMilliseconds);

                if (run == null || run.Meta == null || !run.Meta.IsValid)
                {
                    var msg = run?.Meta?.Message ?? "Gagal final iDRG.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-final-fail", $"alert('{msg}');", true);
                    return;
                }

                var reqAfter = new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Claim.Get.GetDetailResponse.Response detail = null;
                var swAfter = Stopwatch.StartNew();
                try
                {
                    detail = svc.GetDetail(reqAfter);
                    SaveNccIdrg("GetClaimData", reqAfter, detail);
                }
                catch (Exception ex)
                {
                    swAfter.Stop();
                    LogApi("GetDetail(After Final iDRG)(EX)", reqAfter, new { error = ex.Message }, swAfter.ElapsedMilliseconds);
                    return;
                }
                swAfter.Stop();
                LogApi("GetDetail(After Final iDRG)", reqAfter, detail, swAfter.ElapsedMilliseconds);

                if (detail?.Metadata?.IsValid == true)
                {
                    var d = detail.DataResponse?.Data;
                    var g = d?.Grouper;

                    if (g?.ResponseIdrg != null)
                    {
                        var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));
                        txtIDRGInfo.Text =
                            $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} - " +
                            $"{g.ResponseIdrg.script_version ?? "-"} / {g.ResponseIdrg.logic_version ?? "-"}";

                        string jenis = d?.JenisRawat?.ToString() == "1" ? "Rawat Inap"
                                     : d?.JenisRawat?.ToString() == "2" ? "Rawat Jalan"
                                     : d?.JenisRawat?.ToString() == "3" ? "IGD" : string.Empty;

                        string kelas = d?.KelasRawat?.ToString() == "1" ? "Kelas 1"
                                     : d?.KelasRawat?.ToString() == "2" ? "Kelas 2"
                                     : d?.KelasRawat?.ToString() == "3" ? "Kelas 3" : string.Empty;

                        int.TryParse(d?.Los, out var los);
                        var jenisKelasText = jenis +
                            (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas)) +
                            (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");

                        txtIDRGJenisRawat.Text = jenisKelasText.Trim();
                        txtCostWeight.Text = g.ResponseIdrg.total_cost_weight;
                        txtNBR.Text = Convert.ToInt64(g.ResponseIdrg.nbr)
                                          .ToString("N0", new System.Globalization.CultureInfo("id-ID"));

                        bool hasCostWeight = !string.IsNullOrWhiteSpace(g.ResponseIdrg.total_cost_weight);
                        bool hasNBR = !string.IsNullOrWhiteSpace(g.ResponseIdrg.nbr);

                        rowCostWeight.Visible = hasCostWeight;
                        rowNBR.Visible = hasNBR;

                        rowNoteBelumFinal.Visible = hasCostWeight || hasNBR;
                        txtIDRGStatus.Text = g?.ResponseIdrg.status_cd ?? string.Empty;

                        var mdcDesc = g?.ResponseIdrg?.mdc_description?.Trim();
                        var mdcCode = g?.ResponseIdrg?.mdc_number?.Trim();
                        var drgDesc = g?.ResponseIdrg?.drg_description?.Trim();
                        var drgCode = g?.ResponseIdrg?.drg_code?.Trim();

                        txtIDRGMDC.Text = string.IsNullOrWhiteSpace(mdcDesc) ? "-" : mdcDesc;
                        txtIDRGMDCCode.Text = string.IsNullOrWhiteSpace(mdcCode) ? "-" : mdcCode;
                        txtIDRGDRG.Text = string.IsNullOrWhiteSpace(drgDesc) ? "-" : drgDesc;
                        txtIDRGDRGCode.Text = string.IsNullOrWhiteSpace(drgCode) ? "-" : drgCode;

                        bool isUngroup = HasUngroupFlag(mdcDesc) || HasUngroupFlag(drgDesc);

                        hfIdrgIsUngroup.Value = isUngroup ? "1" : "0";

                        SetErrorColor(txtIDRGMDC, isUngroup);
                        SetErrorColor(txtIDRGMDCCode, isUngroup);
                        SetErrorColor(txtIDRGDRG, isUngroup);
                        SetErrorColor(txtIDRGDRGCode, isUngroup);

                        cboDiagnosaIdrg.Enabled = false;
                        btnInsertDiagnosaIdrg.Enabled = false;
                        btnFilterDiagnosaIdrg.Enabled = false;
                        grdDiagnosaIdrg.Enabled = false;

                        cboProsedurIdrg.Enabled = false;
                        txtQtyProc.Enabled = false;
                        btnInsertProsedurIdrg.Enabled = false;
                        btnFilterProsedurIdrg.Enabled = false;
                        grdProsedurIdrg.Enabled = false;

                        btnIdrgGroup.Enabled = false;
                        btnFnliDRG.Enabled = false;
                        btnIdrgEdit.Enabled = true;
                        btnIdrgEdit.Visible = true;
                        btnImprtIdrg.Enabled = true;
                        btnInacbgGroup.Enabled = true;

                        //fsINACBG.Visible = true;

                        var dxDel = grdDiagnosaIdrg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                        if (dxDel != null) dxDel.Display = false;

                        var dxPrimer = grdDiagnosaIdrg.MasterTableView.Columns.FindByUniqueNameSafe("PrimerColumn");
                        if (dxPrimer != null) dxPrimer.Display = false;

                        var prDel = grdProsedurIdrg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                        if (prDel != null) prDel.Display = false;

                        grdDiagnosaIdrg.Rebind();
                        grdProsedurIdrg.Rebind();
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-final-ok",
                    "alert('iDRG telah difinalkan.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-final-ex",
                    $"alert('Terjadi kesalahan saat final iDRG: {ex.Message.Replace("'", "`")}');", true);
            }
        }

        protected void btnFinalIna_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoSep.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-sep", "alert('Nomor SEP belum diisi.');", true);
                return;
            }

            try
            {
                var svc = new Common.Inacbg.v510.Service();

                var reqCheck = new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Claim.Get.GetDetailResponse.Response check = null;
                var swCheck = Stopwatch.StartNew();
                try
                {
                    check = svc.GetDetail(reqCheck);
                }
                catch (Exception ex)
                {
                    swCheck.Stop();
                    LogApi("GetDetail(Before Final INACBG)(EX)", reqCheck, new { error = ex.Message }, swCheck.ElapsedMilliseconds);
                    return;
                }
                swCheck.Stop();
                LogApi("GetDetail(Before Final INACBG)", reqCheck, check, swCheck.ElapsedMilliseconds);

                var idrgResp = check?.DataResponse?.Data?.Grouper?.ResponseIdrg;

                if (!(check?.Metadata?.IsValid == true))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-no-final",
                        "alert('iDRG belum menghasilkan group yang valid (Ungroupable/Unrelated). Tidak bisa FINAL.');", true);
                    return;
                }

                var reqFinal = new Common.Inacbg.v510.Gruoper.IdrgGrouper.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Gruoper.IdrgGrouper.Result run = null;
                var swFinal = Stopwatch.StartNew();
                try
                {
                    run = svc.FinalInacbgGrouper(reqFinal);
                }
                catch (Exception ex)
                {
                    swFinal.Stop();
                    LogApi("FinalInacbgGrouper(EX)", reqFinal, new { error = ex.Message }, swFinal.ElapsedMilliseconds);
                    return;
                }
                swFinal.Stop();
                LogApi("FinalInacbgGrouper", reqFinal, run, swFinal.ElapsedMilliseconds);

                if (run == null || run.Meta == null || !run.Meta.IsValid)
                {
                    var msg = run?.Meta?.Message ?? "Gagal final INACBG.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-final-fail", $"alert('{msg}');", true);
                    return;
                }

                var reqAfter = new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Claim.Get.GetDetailResponse.Response detail = null;
                var swAfter = Stopwatch.StartNew();
                try
                {
                    detail = svc.GetDetail(reqAfter);
                }
                catch (Exception ex)
                {
                    swAfter.Stop();
                    LogApi("GetDetail(After Final INACBG)(EX)", reqAfter, new { error = ex.Message }, swAfter.ElapsedMilliseconds);
                    return;
                }
                swAfter.Stop();
                LogApi("GetDetail(After Final INACBG)", reqAfter, detail, swAfter.ElapsedMilliseconds);

                if (detail?.Metadata?.IsValid == true)
                {
                    var jr = detail?.DataResponse?.Data?.JenisRawat?.ToString(); // "1","2","3"
                    var kr = detail?.DataResponse?.Data?.KelasRawat?.ToString(); // "1","2","3"
                    int.TryParse(detail?.DataResponse?.Data?.Los?.ToString(), out var los); // int

                    string jenis = jr == "1" ? "Rawat Inap"
                                : jr == "2" ? "Rawat Jalan"
                                : jr == "3" ? "IGD"
                                : "";

                    string kelas = kr == "1" ? "Kelas 1"
                                : kr == "2" ? "Kelas 2"
                                : kr == "3" ? "Kelas 3"
                                : "";

                    string text = jenis
                                + (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas))
                                + (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");

                    var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));

                    var s = detail?.DataResponse?.Data?.Grouper.ResponseInacbg.status_cd;

                    var kdtrf = detail?.DataResponse?.Data?.KodeTarif?.ToString();

                    var trf = new AppStandardReferenceItem();
                    trf.LoadByPrimaryKey("BpjsTariffType", kdtrf);

                    txtInfo.Text = $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} •• " + $"{kelas} •• Tarif : " + $"{trf.ItemName}";
                    txtStatusIna.Text = string.IsNullOrWhiteSpace(s) ? "" : char.ToUpper(s[0]) + (s.Length > 1 ? s.Substring(1).ToLower() : "");
                    txtJenisRawat.Text = text.Trim();
                    txtStatusIna.Text = string.IsNullOrWhiteSpace(detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd) ? "" : char.ToUpper(detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd[0]) + (detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd.Length > 1 ? detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd.Substring(1).ToLower() : "");
                    txtIDRGStatus.Text = detail.DataResponse.Data.Grouper.ResponseIdrg.status_cd ?? string.Empty;

                    txtStatusKlaim.Text = detail.DataResponse.Data.KlaimStatusCd ?? "";

                    cboDiagnosaIdrg.Enabled = false;
                    btnInsertDiagnosaIdrg.Enabled = false;
                    btnFilterDiagnosaIdrg.Enabled = false;
                    grdDiagnosaIdrg.Enabled = false;

                    cboProsedurIdrg.Enabled = false;
                    txtQtyProc.Enabled = false;
                    btnInsertProsedurIdrg.Enabled = false;
                    btnFilterProsedurIdrg.Enabled = false;
                    grdProsedurIdrg.Enabled = false;

                    btnIdrgGroup.Enabled = false;
                    btnFnliDRG.Enabled = false;
                    btnIdrgEdit.Enabled = true;
                    btnIdrgEdit.Visible = true;

                    cboDiagnosaInacbg.Enabled = false;
                    btnInsertDiagnosaInacbg.Enabled = false;
                    btnFilterDiagnosaInacbg.Enabled = false;
                    grdDiagnosaInacbg.Enabled = false;

                    cboProsedurInacbg.Enabled = false;
                    btnInsertProsedurInacbg.Enabled = false;
                    btnFilterProsedurInacbg.Enabled = false;
                    grdProsedurIdrg.Enabled = false;

                    btnInacbgGroup.Enabled = false;
                    btnImprtIdrg.Enabled = false;
                    btnFinalIna.Enabled = false;
                    btnInacbgEdit.Enabled = true;
                    btnInacbgEdit.Visible = true;
                    btnFinal.Enabled = true;

                    var dxDel = grdDiagnosaIdrg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                    if (dxDel != null) dxDel.Display = false;

                    var dxPrimer = grdDiagnosaIdrg.MasterTableView.Columns.FindByUniqueNameSafe("PrimerColumn");
                    if (dxPrimer != null) dxPrimer.Display = false;

                    var prDel = grdProsedurIdrg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                    if (prDel != null) prDel.Display = false;

                    var dxDelIna = grdDiagnosaInacbg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                    if (dxDelIna != null) dxDelIna.Display = false;

                    var dxPrimerIna = grdDiagnosaInacbg.MasterTableView.Columns.FindByUniqueNameSafe("PrimerColumn");
                    if (dxPrimerIna != null) dxPrimerIna.Display = false;

                    var prDelIna = grdProsedurInacbg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                    if (prDelIna != null) prDelIna.Display = false;

                    grdDiagnosaIdrg.Rebind();
                    grdDiagnosaIdrg.Rebind();
                    grdDiagnosaInacbg.Rebind();
                    grdProsedurInacbg.Rebind();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-final-ok",
                    "alert('INACBG telah difinalkan.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-final-ex",
                    $"alert('Terjadi kesalahan saat final INACBG: {ex.Message.Replace("'", "`")}');", true);
            }
        }

        protected void btnInacbgEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoSep.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-edit-sep", "alert('Nomor SEP belum diisi.');", true);
                return;
            }

            try
            {
                var svc = new Common.Inacbg.v510.Service();
                var reqEdit = new Common.Inacbg.v510.Gruoper.IdrgGrouper.Data
                {
                    nomor_sep = txtNoSep.Text
                };

                Common.Inacbg.v510.Gruoper.IdrgGrouper.Result resp = null;
                var swEdit = Stopwatch.StartNew();
                try
                {
                    resp = svc.EditInacbgGrouper(reqEdit);
                }
                catch (Exception ex)
                {
                    swEdit.Stop();
                    LogApi("EditInacbgGrouper(EX)", reqEdit, new { error = ex.Message }, swEdit.ElapsedMilliseconds);
                    return;
                }
                swEdit.Stop();
                LogApi("EditInacbgGrouper", reqEdit, resp, swEdit.ElapsedMilliseconds);

                if (resp == null || resp.Meta == null || !resp.Meta.IsValid)
                {
                    var msg = resp?.Meta?.Message ?? "Gagal membuka INACBG untuk diedit.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-edit-fail", $"alert('{msg}');", true);
                    return;
                }

                var reqDetail = new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Claim.Get.GetDetailResponse.Response detail = null;
                var swDetail = Stopwatch.StartNew();
                try
                {
                    detail = svc.GetDetail(reqDetail);
                }
                catch (Exception ex)
                {
                    swDetail.Stop();
                    LogApi("GetDetail(After Open INACBG Edit)(EX)", reqDetail, new { error = ex.Message }, swDetail.ElapsedMilliseconds);
                    return;
                }
                swDetail.Stop();
                LogApi("GetDetail(After Open INACBG Edit)", reqDetail, detail, swDetail.ElapsedMilliseconds);

                if (detail?.Metadata?.IsValid == true)
                {
                    var jr = detail?.DataResponse?.Data?.JenisRawat?.ToString(); // "1","2","3"
                    var kr = detail?.DataResponse?.Data?.KelasRawat?.ToString(); // "1","2","3"
                    int.TryParse(detail?.DataResponse?.Data?.Los?.ToString(), out var los); // int

                    string jenis = jr == "1" ? "Rawat Inap"
                                : jr == "2" ? "Rawat Jalan"
                                : jr == "3" ? "IGD"
                                : "";

                    string kelas = kr == "1" ? "Kelas 1"
                                : kr == "2" ? "Kelas 2"
                                : kr == "3" ? "Kelas 3"
                                : "";

                    string text = jenis
                                + (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas))
                                + (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");

                    var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));

                    var s = detail?.DataResponse?.Data?.KlaimStatusCd;

                    var kdtrf = detail?.DataResponse?.Data?.KodeTarif?.ToString();

                    var trf = new AppStandardReferenceItem();
                    trf.LoadByPrimaryKey("BpjsTariffType", kdtrf);

                    txtInfo.Text = $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} •• " + $"{kelas} •• Tarif : " + $"{trf.ItemName}";
                    txtStatusIna.Text = string.IsNullOrWhiteSpace(detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd) ? "" : char.ToUpper(detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd[0]) + (detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd.Length > 1 ? detail.DataResponse.Data.Grouper.ResponseInacbg.status_cd.Substring(1).ToLower() : "");
                    txtJenisRawat.Text = text.Trim();

                    txtStatusKlaim.Text = detail.DataResponse.Data.KlaimStatusCd ?? "";
                }

                cboDiagnosaInacbg.Enabled = true;
                btnInsertDiagnosaInacbg.Enabled = true;
                btnFilterDiagnosaInacbg.Enabled = true;
                grdDiagnosaInacbg.Enabled = true;

                cboProsedurInacbg.Enabled = true;
                btnInsertProsedurInacbg.Enabled = true;
                btnFilterProsedurInacbg.Enabled = true;
                grdProsedurInacbg.Enabled = true;

                btnInacbgGroup.Enabled = true;
                btnImprtIdrg.Enabled = true;
                btnFinal.Enabled = false;
                btnInacbgEdit.Enabled = false;

                CollapsePanel1.Enabled = true;
                CollapsePanel2.Enabled = true;
                CollapsePanel3.Enabled = true;

                var dxDel = grdDiagnosaInacbg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (dxDel != null) dxDel.Display = true;

                var dxPrimer = grdDiagnosaInacbg.MasterTableView.Columns.FindByUniqueNameSafe("PrimerColumn");
                if (dxPrimer != null) dxPrimer.Display = true;

                var prDel = grdProsedurInacbg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (prDel != null) prDel.Display = true;

                grdDiagnosaInacbg.Rebind();
                grdProsedurInacbg.Rebind();

                //pnlInacbgResult.Visible = false;

                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-edit-ok",
                    "alert('Form INACBG dibuka untuk diedit.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "inacbg-edit-ex",
                     $"alert('Terjadi kesalahan saat membuka edit INACBG: {ex.Message.Replace("'", "`")}');", true);
            }
        }

        protected void btnIdrgEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoSep.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-edit-sep", "alert('Nomor SEP belum diisi.');", true);
                return;
            }

            try
            {
                var svc = new Common.Inacbg.v510.Service();
                var reqEdit = new Common.Inacbg.v510.Gruoper.IdrgGrouper.Data
                {
                    nomor_sep = txtNoSep.Text
                };

                Common.Inacbg.v510.Gruoper.IdrgGrouper.Result resp = null;
                var swEdit = Stopwatch.StartNew();
                try
                {
                    resp = svc.EditIdrgGrouper(reqEdit);
                    SaveNccIdrg("IdrgReEdit", reqEdit, resp);
                }
                catch (Exception ex)
                {
                    swEdit.Stop();
                    LogApi("EditIdrgGrouper(EX)", reqEdit, new { error = ex.Message }, swEdit.ElapsedMilliseconds);
                    return;
                }
                swEdit.Stop();
                LogApi("EditIdrgGrouper", reqEdit, resp, swEdit.ElapsedMilliseconds);

                if (resp == null || resp.Meta == null || !resp.Meta.IsValid)
                {
                    var msg = resp?.Meta?.Message ?? "Gagal membuka iDRG untuk diedit.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-edit-fail", $"alert('{msg}');", true);
                    return;
                }

                var reqDetail = new Common.Inacbg.v510.Claim.Get.GetDetail.Data { nomor_sep = txtNoSep.Text };
                Common.Inacbg.v510.Claim.Get.GetDetailResponse.Response detail = null;
                var swDetail = Stopwatch.StartNew();
                try
                {
                    detail = svc.GetDetail(reqDetail);
                }
                catch (Exception ex)
                {
                    swDetail.Stop();
                    LogApi("GetDetail(After Open iDRG Edit)(EX)", reqDetail, new { error = ex.Message }, swDetail.ElapsedMilliseconds);
                    return;
                }
                swDetail.Stop();
                LogApi("GetDetail(After Open iDRG Edit)", reqDetail, detail, swDetail.ElapsedMilliseconds);

                if (detail?.Metadata?.IsValid == true)
                {
                    var d = detail.DataResponse?.Data;
                    var g = d?.Grouper;

                    txtIDRGStatus.Text = d?.Grouper.ResponseIdrg.status_cd ?? string.Empty;

                    if (g?.ResponseIdrg != null)
                    {
                        var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));
                        txtIDRGInfo.Text =
                            $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} - " +
                            $"{g.ResponseIdrg.script_version ?? "-"} / {g.ResponseIdrg.logic_version ?? "-"}";

                        string jenis = d?.JenisRawat?.ToString() == "1" ? "Rawat Inap"
                                     : d?.JenisRawat?.ToString() == "2" ? "Rawat Jalan"
                                     : d?.JenisRawat?.ToString() == "3" ? "IGD" : string.Empty;

                        string kelas = d?.KelasRawat?.ToString() == "1" ? "Kelas 1"
                                     : d?.KelasRawat?.ToString() == "2" ? "Kelas 2"
                                     : d?.KelasRawat?.ToString() == "3" ? "Kelas 3" : string.Empty;

                        int.TryParse(d?.Los, out var los);
                        var jenisKelasText = jenis +
                            (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas)) +
                            (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");

                        txtIDRGJenisRawat.Text = jenisKelasText.Trim();
                        txtCostWeight.Text = g.ResponseIdrg.total_cost_weight;
                        txtNBR.Text = Convert.ToInt64(g.ResponseIdrg.nbr)
                                          .ToString("N0", new System.Globalization.CultureInfo("id-ID"));
                        bool hasCostWeight = !string.IsNullOrWhiteSpace(g.ResponseIdrg.total_cost_weight);
                        bool hasNBR = !string.IsNullOrWhiteSpace(g.ResponseIdrg.nbr);

                        rowCostWeight.Visible = hasCostWeight;
                        rowNBR.Visible = hasNBR;

                        rowNoteBelumFinal.Visible = hasCostWeight || hasNBR;
                    }
                }

                cboDiagnosaIdrg.Enabled = true;
                btnInsertDiagnosaIdrg.Enabled = true;
                btnFilterDiagnosaIdrg.Enabled = true;
                grdDiagnosaIdrg.Enabled = true;

                cboProsedurIdrg.Enabled = true;
                txtQtyProc.Enabled = true;
                btnInsertProsedurIdrg.Enabled = true;
                btnFilterProsedurIdrg.Enabled = true;
                grdProsedurIdrg.Enabled = true;

                btnIdrgGroup.Enabled = true;
                btnFnliDRG.Enabled = false;
                btnIdrgEdit.Enabled = false;

                CollapsePanel1.Enabled = true;
                CollapsePanel2.Enabled = true;
                CollapsePanel3.Enabled = true;

                var dxDel = grdDiagnosaIdrg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (dxDel != null) dxDel.Display = true;

                var dxPrimer = grdDiagnosaIdrg.MasterTableView.Columns.FindByUniqueNameSafe("PrimerColumn");
                if (dxPrimer != null) dxPrimer.Display = true;

                var prDel = grdProsedurIdrg.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (prDel != null) prDel.Display = true;

                grdDiagnosaIdrg.Rebind();
                grdProsedurIdrg.Rebind();

                //pnlIdrgResult.Visible = false;
                //pnlInacbgResult.Visible = false;
                //fsINACBG.Visible = false;
                btnInacbgEdit.Visible = false;

                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-edit-ok",
                    "alert('Form iDRG dibuka untuk diedit.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-edit-ex",
                     $"alert('Terjadi kesalahan saat membuka edit iDRG: {ex.Message.Replace("'", "`")}');", true);
            }
        }

        protected void btnImprtIdrg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoSep.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-sep",
                    "alert('Nomor SEP belum diisi.');", true);
                return;
            }

            try
            {
                var svc = new Common.Inacbg.v510.Service();
                var reqImport = new Common.Inacbg.v510.Gruoper.importcoding.Data
                {
                    nomor_sep = txtNoSep.Text
                };
                var run = svc.ImportIdrgToInacbg(reqImport);

                if (run == null || run.Meta == null || !run.Meta.IsValid)
                {
                    var msg = run?.Meta?.Message ?? "Gagal mengimpor iDRG ke INACBG.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "idrg-import-fail",
                        $"alert('{msg.Replace("'", "`")}');", true);
                    return;
                }

                SaveNccIdrg("ImportIdrgToInacbg", reqImport, run);

                var regno = Convert.ToString(Request.QueryString["regno"]);
                var reg = new Registration();
                reg.LoadByPrimaryKey(regno);

                using (var trans = new esTransactionScope())
                {
                    // 1) Hapus EpisodeDiagnoseInaGroupper
                    var dxOld = new EpisodeDiagnoseInaGroupperCollection();
                    {
                        var q = new EpisodeDiagnoseInaGroupperQuery("dxOld");
                        q.Where(q.RegistrationNo == regno);
                        dxOld.Load(q);
                    }
                    if (dxOld.Count > 0)
                    {
                        dxOld.MarkAllAsDeleted();
                        dxOld.Save();
                    }

                    // 2) Hapus EpisodeProcedureInaGroupper
                    var pxOld = new EpisodeProcedureInaGroupperCollection();
                    {
                        var q = new EpisodeProcedureInaGroupperQuery("pxOld");
                        q.Where(q.RegistrationNo == regno);
                        pxOld.Load(q);
                    }
                    if (pxOld.Count > 0)
                    {
                        pxOld.MarkAllAsDeleted();
                        pxOld.Save();
                    }

                    trans.Complete();
                }

                var srcDx = new EpisodeDiagnoseCollection();
                {
                    var qsrc = new EpisodeDiagnoseQuery("src");
                    qsrc.Where(qsrc.RegistrationNo == regno);
                    qsrc.OrderBy(qsrc.SequenceNo.Ascending);
                    srcDx.Load(qsrc);
                }

                var dxColl = new EpisodeDiagnoseInaGroupperCollection();
                {
                    var q = new EpisodeDiagnoseInaGroupperQuery("dx");
                    q.Where(q.RegistrationNo == regno);                 // tanpa filter IsVoid
                    q.OrderBy(q.SequenceNo.Ascending);
                    dxColl.Load(q);
                }

                var importedDx = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var dxList = run.Data?.Diagnosa?.Expanded;
                if (dxList != null)
                {
                    foreach (var it in dxList)
                    {
                        var code = it?.Code?.Trim();
                        if (string.IsNullOrEmpty(code)) continue;
                        if (!importedDx.Add(code)) continue;

                        var diag = new Diagnose();
                        if (!diag.LoadByPrimaryKey(code))
                        {
                            diag = new Diagnose
                            {
                                DiagnoseID = code,
                                DtdNo = "0",
                                DiagnoseName = (it?.Display ?? code).Replace("'", "`"),
                                IsChronicDisease = false,
                                IsDisease = false,
                                IsActive = true,
                                LastUpdateDateTime = DateTime.Now,
                                LastUpdateByUserID = AppSession.UserLogin.UserID
                            };
                            diag.Save();
                        }

                        var ed = dxColl.FirstOrDefault(d =>
                            d.RegistrationNo == regno &&
                            string.Equals(d.DiagnoseID?.Trim(), code, StringComparison.OrdinalIgnoreCase));

                        if (ed == null)
                        {
                            ed = dxColl.AddNew();
                            ed.RegistrationNo = regno;

                            int next = dxColl.Any() ? dxColl.Max(r => r.SequenceNo.ToInt()) + 1 : 1;
                            string seq = next.ToString("000");
                            while (dxColl.Any(r => string.Equals(r.SequenceNo, seq, StringComparison.OrdinalIgnoreCase)))
                            {
                                next++;
                                seq = next.ToString("000");
                            }
                            ed.SequenceNo = seq;

                            ed.DiagnoseID = code;
                            ed.DiagnoseName = diag.DiagnoseName;
                            ed.CreateByUserID = AppSession.UserLogin.UserID;
                            ed.CreateDateTime = DateTime.Now;
                        }

                        var srcRow = srcDx.FirstOrDefault(d => string.Equals(d.DiagnoseID?.Trim(), code, StringComparison.OrdinalIgnoreCase));

                        if (srcRow != null)
                        {
                            ed.SRDiagnoseType = srcRow.SRDiagnoseType;
                            var std = new AppStandardReferenceItem();
                            std.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
                            ed.DiagnoseType = std.ItemName;
                            ed.DiagnosisText = string.IsNullOrWhiteSpace(srcRow.DiagnosisText)
                                ? $"{code}-{diag.DiagnoseName}"
                                : srcRow.DiagnosisText;
                            ed.IsChronicDisease = srcRow.IsChronicDisease;
                            ed.ParamedicID = srcRow.ParamedicID;
                        }
                        else
                        {                            
                            bool inaHasMain = dxColl.Any(x => string.Equals(x.SRDiagnoseType, AppSession.Parameter.DiagnoseTypeMain, StringComparison.OrdinalIgnoreCase));
                            ed.SRDiagnoseType = inaHasMain ? "DiagnoseType-004" : AppSession.Parameter.DiagnoseTypeMain;

                            var std = new AppStandardReferenceItem();
                            std.LoadByPrimaryKey(AppEnum.StandardReference.DiagnoseType.ToString(), ed.SRDiagnoseType);
                            ed.DiagnoseType = std.ItemName;
                            ed.DiagnosisText = $"{code}-{diag.DiagnoseName}";
                            ed.IsChronicDisease = diag.IsChronicDisease;
                            ed.ParamedicID = cboDPJP.SelectedValue;
                        }

                        ed.IsAcuteDisease = false;
                        ed.IsOldCase = false;
                        ed.IsConfirmed = true;
                        ed.IsVoid = false;
                        ed.Notes = ed.Notes ?? string.Empty;
                        ed.LastUpdateDateTime = DateTime.Now;
                        ed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    dxColl.Save();
                }

                var srcPx = new EpisodeProcedureCollection();
                {
                    var qsrc = new EpisodeProcedureQuery("srcpx");
                    qsrc.Where(qsrc.RegistrationNo == regno);
                    qsrc.OrderBy(qsrc.SequenceNo.Ascending);
                    srcPx.Load(qsrc);
                }

                var pxColl = new EpisodeProcedureInaGroupperCollection();
                {
                    var q = new EpisodeProcedureInaGroupperQuery("px");
                    q.Where(q.RegistrationNo == regno);
                    q.OrderBy(q.SequenceNo.Ascending);
                    pxColl.Load(q);
                }

                var importedPx = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var pxList = run.Data?.Procedure?.Expanded;
                if (pxList != null)
                {
                    foreach (var it in pxList)
                    {
                        var code = it?.Code?.Trim();
                        if (string.IsNullOrEmpty(code)) continue;
                        if (!importedPx.Add(code)) continue;

                        var proc = new Procedure();
                        if (!proc.LoadByPrimaryKey(code))
                        {
                            proc = new Procedure
                            {
                                ProcedureID = code,
                                ProcedureName = (it?.Display ?? code),
                                LastUpdateDateTime = DateTime.Now,
                                LastUpdateByUserID = AppSession.UserLogin.UserID
                            };
                            proc.Save();
                        }

                        var ep = pxColl.FirstOrDefault(x =>
                            x.RegistrationNo == regno &&
                            string.Equals(x.ProcedureID?.Trim(), code, StringComparison.OrdinalIgnoreCase));

                        if (ep == null)
                        {
                            ep = pxColl.AddNew();
                            ep.RegistrationNo = regno;

                            int next = pxColl.Any() ? pxColl.Max(r => r.SequenceNo.ToInt()) + 1 : 1;
                            string seq = next.ToString("000");
                            while (pxColl.Any(r => string.Equals(r.SequenceNo, seq, StringComparison.OrdinalIgnoreCase)))
                            {
                                next++;
                                seq = next.ToString("000");
                            }
                            ep.SequenceNo = seq;

                            ep.ProcedureID = code;
                            ep.CreateByUserID = AppSession.UserLogin.UserID;
                            ep.CreateDateTime = DateTime.Now;
                        }

                        var srcRow = srcPx.FirstOrDefault(p =>
                            string.Equals(p.ProcedureID?.Trim(), code, StringComparison.OrdinalIgnoreCase));
                        if (srcRow != null)
                        {
                            ep.ProcedureName = string.IsNullOrWhiteSpace(srcRow.ProcedureName)
                                ? (it?.Display ?? code)
                                : srcRow.ProcedureName;

                            ep.SRProcedureCategory = srcRow.SRProcedureCategory;
                            ep.SRAnestesi = srcRow.SRAnestesi;
                            ep.ParamedicID = srcRow.ParamedicID;
                            ep.ParamedicID2 = srcRow.ParamedicID2;
                            ep.RoomID = srcRow.RoomID;
                            ep.ProcedureDate = srcRow.ProcedureDate;
                            ep.ProcedureTime = srcRow.ProcedureTime;
                            ep.ProcedureDate2 = srcRow.ProcedureDate2;
                            ep.ProcedureTime2 = srcRow.ProcedureTime2;
                            ep.IsCito = srcRow.IsCito;
                        }
                        else
                        {
                            ep.ProcedureName = (it?.Display ?? code);
                            ep.ProcedureDate = reg.RegistrationDate;
                            ep.ProcedureTime = reg.RegistrationTime;
                            ep.ProcedureDate2 = reg.RegistrationDate;
                            ep.ProcedureTime2 = reg.RegistrationTime;
                            ep.ParamedicID = string.Empty;
                            ep.ParamedicID2 = string.Empty;
                            ep.SRProcedureCategory = string.Empty;
                            ep.SRAnestesi = string.Empty;
                            ep.RoomID = string.Empty;
                            ep.IsCito = false;
                        }

                        ep.IsVoid = false;
                        ep.Notes = ep.Notes ?? string.Empty;
                        ep.InstrumentatorID1 = ep.InstrumentatorID1 ?? string.Empty;
                        ep.InstrumentatorID2 = ep.InstrumentatorID2 ?? string.Empty;
                        ep.LastUpdateDateTime = DateTime.Now;
                        ep.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }

                    pxColl.Save();

                    var collVs = ViewState["icd9cm2"] as EpisodeProcedureInaGroupperCollection;
                    if ((collVs ?? pxColl).Any(p => p.ProcedureID == "39.95") && string.IsNullOrEmpty(cboDializer.SelectedValue))
                    {
                        cboDializer.SelectedValue = "1";
                    }
                }

                var warnDx = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                if (dxList != null)
                {
                    foreach (var it in dxList)
                    {
                        var code = it?.Code?.Trim();
                        if (string.IsNullOrEmpty(code)) continue;

                        var metaCode = it?.Metadata?.Code;
                        if (!string.Equals(metaCode, "200", StringComparison.OrdinalIgnoreCase))
                        {
                            var msg = (it?.Metadata?.Message ?? "Tidak valid").Trim();
                            warnDx[code] = msg;
                        }
                    }
                }
                if (warnDx.Count > 0)
                {
                    foreach (var row in dxColl)
                    {
                        var code = (row.DiagnoseID ?? string.Empty).Trim();
                        row.Notes = warnDx.TryGetValue(code, out var msg) ? msg : string.Empty;
                    }
                    dxColl.Save();
                }

                var warnPx = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var pxList2 = run.Data?.Procedure?.Expanded; // re-use
                if (pxList2 != null)
                {
                    foreach (var it in pxList2)
                    {
                        var code = it?.Code?.Trim();
                        if (string.IsNullOrEmpty(code)) continue;

                        var metaCode = it?.Metadata?.Code;
                        if (!string.Equals(metaCode, "200", StringComparison.OrdinalIgnoreCase))
                        {
                            var msg = (it?.Metadata?.Message ?? "Tidak valid").Trim();
                            warnPx[code] = msg;
                        }
                    }
                }
                if (warnPx.Count > 0)
                {
                    foreach (var row in pxColl)
                    {
                        var code = (row.ProcedureID ?? string.Empty).Trim();
                        row.Notes = warnPx.TryGetValue(code, out var msg) ? msg : string.Empty;
                    }
                    pxColl.Save();
                }

                ViewState["icd102"] = null;
                ViewState["icd9cm2"] = null;

                btnInacbgGroup.Enabled = true;

                grdDiagnosaInacbg.Rebind();
                grdProsedurInacbg.Rebind();

                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-import-ok",
                    "alert('Import iDRG → INACBG berhasil.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "idrg-import-ex",
                    $"alert('Terjadi kesalahan saat import iDRG → INACBG: {ex.Message.Replace("'", "`")}');", true);
            }
        }

        protected void btnInacbgGroup_Click(object sender, EventArgs e)
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

            var svc = new Common.Inacbg.v510.Service();
            var response = svc.InacbgGrouper1(new Common.Inacbg.v510.Gruoper.Grouper1.Data() { nomor_sep = txtNoSep.Text });

            if (response.Metadata.IsValid)
            {
                var data = response.Response ?? response.ResponseInacbg;
                if (data != null)
                {
                    var responses = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data() { nomor_sep = txtNoSep.Text });
                    if (responses.Metadata.IsValid)
                    {
                        var jr = responses?.DataResponse?.Data?.JenisRawat?.ToString(); // "1","2","3"
                        var kr = responses?.DataResponse?.Data?.KelasRawat?.ToString(); // "1","2","3"
                        int.TryParse(responses?.DataResponse?.Data?.Los?.ToString(), out var los);

                        string jenis = jr == "1" ? "Rawat Inap"
                                    : jr == "2" ? "Rawat Jalan"
                                    : jr == "3" ? "IGD" : "";

                        string kelas = kr == "1" ? "Kelas 1"
                                    : kr == "2" ? "Kelas 2"
                                    : kr == "3" ? "Kelas 3" : "";

                        string text = jenis
                                    + (jenis == "IGD" ? "" : (string.IsNullOrEmpty(kelas) ? "" : " " + kelas))
                                    + (jenis == "Rawat Inap" && los > 0 ? $" ({los} Hari)" : "");

                        var ts = DateTime.Now.ToString("dd MMM yyyy HH:mm", new CultureInfo("id-ID"));
                        var kdtrf = responses?.DataResponse?.Data?.KodeTarif?.ToString();

                        var trf = new AppStandardReferenceItem();
                        trf.LoadByPrimaryKey("BpjsTariffType", kdtrf);

                        txtInfo.Text = $"{AppSession.Parameter.HealthcareInitialAppsVersion} @ {ts} •• {kelas} •• Tarif : {trf.ItemName}";
                        txtStatusIna.Text = string.IsNullOrWhiteSpace(responses.DataResponse.Data.Grouper.ResponseInacbg.status_cd) ? "" : char.ToUpper(responses.DataResponse.Data.Grouper.ResponseInacbg.status_cd[0]) + (responses.DataResponse.Data.Grouper.ResponseInacbg.status_cd.Length > 1 ? responses.DataResponse.Data.Grouper.ResponseInacbg.status_cd.Substring(1).ToLower() : "");
                        txtJenisRawat.Text = text.Trim();

                        txtStatusKlaim.Text = string.IsNullOrWhiteSpace(responses.DataResponse.Data.KlaimStatusCd) ? "" : char.ToUpper(responses.DataResponse.Data.KlaimStatusCd[0]) + (responses.DataResponse.Data.KlaimStatusCd.Length > 1 ? responses.DataResponse.Data.KlaimStatusCd.Substring(1).ToLower() : "");
                    }

                    var cbg = data.Cbg;
                    var cbgDesc = cbg?.Description ?? string.Empty;
                    bool isGagal = cbgDesc.IndexOf("GAGAL:", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (cbg != null)
                    {
                        txtGroupName.Text = cbg.Description;
                        txtGroupID.Text = cbg.Code;
                        decimal groupPrice = 0;
                        var priceCandidate = data.BaseTariff ?? data.Tariff ?? cbg?.Tariff;
                        if (!string.IsNullOrWhiteSpace(priceCandidate))
                        {
                            decimal.TryParse(priceCandidate, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out groupPrice);
                        }
                        txtGroupPrice.Value = Convert.ToDouble(groupPrice);
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

                    cboSpecialDrug_SelectedIndexChanged(null, null);
                    txtTambahanBiaya.Value = string.IsNullOrEmpty(data.AddPaymentAmt) ? 0 : Convert.ToDouble(data.AddPaymentAmt);

                    var ncc = new NccInacbg();
                    if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
                    {
                        ncc.AddPaymentAmt = string.IsNullOrEmpty(data.AddPaymentAmt) ? 0 : Convert.ToDecimal(data.AddPaymentAmt);
                        ncc.CoverageAmount = Convert.ToDecimal(txtGrouperTotal.Value);
                        ncc.CbgID = txtGroupID.Text;
                        ncc.CbgName = txtGroupName.Text;
                        ncc.Save();
                    }

                    reg = new Registration();
                    reg.LoadByPrimaryKey(Request.QueryString["regno"]);
                    reg.PlavonAmount = Convert.ToDecimal(txtGrouperTotal.Value);
                    reg.Save();

                    //pnlInacbgResult.Visible = true;
                    btnFinalIna.Visible = true;
                    btnFinalIna.Enabled = !isGagal;

                    if (isGagal)
                    {
                        ShowAlert("inacbg-group1-gagal", $"Grouping INACBG (Tahap 1) mengembalikan status {cbgDesc}");
                    }
                    else
                    {
                        ShowAlert("inacbg-group1-ok", "Grouping INACBG (Tahap 1) berhasil.");
                    }
                }
                else
                {
                    ShowInformationHeader($"Grouping No. SEP : {txtNoSep.Text} gagal.");
                    return;
                }
            }
            else
            {
                ShowInformationHeader($"Grouping No. SEP : {txtNoSep.Text} gagal {response.Metadata.Message}.");
                return;
            }

            //btnInacbgGroup.Focus();
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

                    var svc = new Common.Inacbg.v510.Service();
                    var response = svc.InacbgGrouper2(new Common.Inacbg.v510.Gruoper.Grouper2.Data()
                    {
                        nomor_sep = txtNoSep.Text,
                        special_cmg = cmg
                    });
                    if (response.Metadata.IsValid)
                    {
                        var data = response.Response ?? response.ResponseInacbg;
                        var kelas = data?.Kelas;
                        var tariffAlt = response.TarifAlt?.SingleOrDefault(r => string.Equals(r.Kelas, kelas, StringComparison.OrdinalIgnoreCase));
                        if (tariffAlt != null)
                        {
                            if (cbo.ID == cboSpecialDrug.ID) txtSpecialDrugPrice.Value = Convert.ToDouble(tariffAlt.TarifSd);
                            else if (cbo.ID == cboSpecialProcedure.ID) txtSpecialProcedurePrice.Value = Convert.ToDouble(tariffAlt.TarifSp);
                            else if (cbo.ID == cboSpecialInvestigation.ID) txtSpecialInvestigationPrice.Value = Convert.ToDouble(tariffAlt.TarifSi);
                            else if (cbo.ID == cboSpecialProsthesis.ID) txtSpecialProsthesisPrice.Value = Convert.ToDouble(tariffAlt.TarifSr);

                            //cbo.Focus();
                        }
                        else
                        {
                            //txtSpecialDrugPrice.Value = 0;
                            //txtSpecialProcedurePrice.Value = 0;
                            //txtSpecialInvestigationPrice.Value = 0;
                            //txtSpecialProsthesisPrice.Value = 0;

                            var cmgs = data?.SpecialCmg ?? Array.Empty<Common.Inacbg.v510.Gruoper.Grouper2.Result.SpecialCmg>();

                            string wantedType =
                                  (cbo.ID == cboSpecialDrug.ID) ? "Special Drug"
                                : (cbo.ID == cboSpecialProcedure.ID) ? "Special Procedure"
                                : (cbo.ID == cboSpecialInvestigation.ID) ? "Special Investigation"
                                : (cbo.ID == cboSpecialProsthesis.ID) ? "Special Prosthesis"
                                : null;

                            if (wantedType != null && cmgs.Length > 0)
                            {
                                var selectedCode =
                                      (cbo.ID == cboSpecialDrug.ID) ? cboSpecialDrug.SelectedValue
                                    : (cbo.ID == cboSpecialProcedure.ID) ? cboSpecialProcedure.SelectedValue
                                    : (cbo.ID == cboSpecialInvestigation.ID) ? cboSpecialInvestigation.SelectedValue
                                    : (cbo.ID == cboSpecialProsthesis.ID) ? cboSpecialProsthesis.SelectedValue
                                    : string.Empty;

                                var byCode = cmgs.FirstOrDefault(x => string.Equals(x.Code, selectedCode, StringComparison.OrdinalIgnoreCase));
                                var byType = cmgs.FirstOrDefault(x => string.Equals(x.Type, wantedType, StringComparison.OrdinalIgnoreCase));
                                var pick = byCode ?? byType;

                                var t = (double)(pick?.Tariff ?? 0);

                                if (cbo.ID == cboSpecialDrug.ID) txtSpecialDrugPrice.Value = t;
                                else if (cbo.ID == cboSpecialProcedure.ID) txtSpecialProcedurePrice.Value = t;
                                else if (cbo.ID == cboSpecialInvestigation.ID) txtSpecialInvestigationPrice.Value = t;
                                else if (cbo.ID == cboSpecialProsthesis.ID) txtSpecialProsthesisPrice.Value = t;

                                //cbo.Focus();
                            }
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

            txtGrouperTotal.Value = (txtGroupPrice.Value ?? 0) + (txtChronicPrice.Value ?? 0) + (txtSubAcutePrice.Value ?? 0) +
                (txtSpecialProcedurePrice.Value ?? 0) + (txtSpecialProsthesisPrice.Value ?? 0) + (txtSpecialInvestigationPrice.Value ?? 0) + (txtSpecialDrugPrice.Value ?? 0);

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);
            reg.PlavonAmount = Convert.ToDecimal(txtGrouperTotal.Value);
            reg.Save();
        }

        private void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var medicalNo = GetCurrentMedicalNo();
            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "YBRSGKP":
                    var pat = new Patient();
                    pat.LoadByMedicalNo(txtNoMR.Text);
                    if (!string.IsNullOrEmpty(pat.OldMedicalNo)) medicalNo = medicalNo.ToInt().ToString();
                    medicalNo = medicalNo.ToInt().ToString();
                    break;
            }

            var svc = new Common.Inacbg.v510.Service();
            var response = svc.Update(new Common.Inacbg.v510.Patient.Update.Data()
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

                var icd10 = ViewState["icd10"] as EpisodeDiagnoseCollection;

                var diagItems = (icd10 ?? Enumerable.Empty<EpisodeDiagnose>())
                    .Where(v => !string.IsNullOrWhiteSpace(v.DiagnoseID))
                    .OrderBy(e => e.SRDiagnoseType)
                    .Select(d => d.DiagnoseID);

                diag = diagItems.Any() ? string.Join("#", diagItems) + "#" : "#";

                var proc = string.Empty;
                var procItems = ((ViewState["icd9cm"] as EpisodeProcedureCollection) ?? Enumerable.Empty<EpisodeProcedure>())
                    .Where(v => !string.IsNullOrWhiteSpace(v.ProcedureID))
                    .Select(p => p.ProcedureID);

                proc = procItems.Any() ? string.Join("#", procItems) + "#" : "#";

                DateTime datemasukEntry = txtTglMasuk.SelectedDate.Value
                        .AddHours(System.Convert.ToDouble(txtJamMasuk.Text.Substring(0, 2)))
                        .AddMinutes(System.Convert.ToDouble(txtJamMasuk.Text.Substring(3, 2)));

                DateTime datepulangEntry = txtTglPulang.SelectedDate.Value
                        .AddHours(System.Convert.ToDouble(txtJamPulang.Text.Substring(0, 2)))
                        .AddMinutes(System.Convert.ToDouble(txtJamPulang.Text.Substring(3, 2)));

                var jrParts = (rblJenisRawat.SelectedValue ?? string.Empty).Split('|');
                var jenis_rawat_val = jrParts.Length > 1 ? jrParts[1] : (rblJenisRawat.SelectedValue ?? string.Empty);

                var krRaw = (rblKelasRawat.SelectedValue ?? string.Empty);
                var krParts = krRaw.Split('|');
                var kelas_rawat_val = jenis_rawat_val == "1"
                    ? (krParts.Length > 1 ? krParts[1] : krRaw)
                    : krRaw;

                string upgrade_class_class_val = string.Empty;
                if (!string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue))
                {
                    var upParts = rblNaikKelasRawat.SelectedValue.Split('|');
                    upgrade_class_class_val = upParts.Length > 1 ? upParts[1] : rblNaikKelasRawat.SelectedValue;
                }

                var jamParts = (cboJaminan.SelectedValue ?? string.Empty).Split('|');
                var payor_id_val = jamParts.Length > 1 ? jamParts[1] : string.Empty;
                var payor_cd_val = jamParts.Length > 2 ? jamParts[2] : string.Empty;

                string cob_cd_val = string.Empty;
                if (!string.IsNullOrEmpty(cboCOB.SelectedValue))
                {
                    var cobParts = cboCOB.SelectedValue.Split('|');
                    cob_cd_val = cobParts.Length > 1 ? cobParts[1] : string.Empty;
                }

                var svc510 = new Common.Inacbg.v510.Service();
                var detail = svc510.Insert(new Common.Inacbg.v510.Detail.Datass()
                {
                    nomor_sep = txtNoSep.Text,
                    nomor_kartu = txtNoPeserta.Text,
                    tgl_masuk = datemasukEntry.ToString("yyyy-MM-dd HH:mm:ss"),
                    tgl_pulang = datepulangEntry.ToString("yyyy-MM-dd HH:mm:ss"),
                    cara_masuk = cboCaraMasuk.SelectedValue,
                    //jenis_rawat = rblJenisRawat.SelectedValue.Split('|')[1],
                    //kelas_rawat = rblJenisRawat.SelectedValue.Split('|')[1] == "1" ? rblKelasRawat.SelectedValue.Split('|')[1] : rblKelasRawat.SelectedValue,
                    jenis_rawat = jenis_rawat_val,
                    kelas_rawat = kelas_rawat_val,
                    adl_sub_acute = txtSubAcute.Text,
                    adl_chronic = txtChronic.Text,
                    icu_indikator = chkRawatIntensif.Checked ? "1" : "0",
                    icu_los = chkRawatIntensif.Checked ? txtRawatIntensif.Value.ToInt().ToString() : "0",
                    ventilator_hour = txtVentilator.Value.ToInt().ToString(),

                    use_ind = chkVentilator.Checked ? "1" : "0",
                    start_dttm = chkVentilator.Checked ? $"{txtTglIntubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamIntubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}" : string.Empty,
                    stop_dttm = chkVentilator.Checked ? $"{txtTglEkstubasi.SelectedDate.Value.ToString("yyyy-MM-dd")} {txtJamEkstubasi.SelectedTime.Value.ToString("hh\\:mm\\:ss")}" : string.Empty,

                    upgrade_class_ind = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? "0" : (chkNaikKelas.Checked ? "1" : "0"),
                    //upgrade_class_class = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? string.Empty : rblNaikKelasRawat.SelectedValue.Split('|')[1],
                    upgrade_class_class = upgrade_class_class_val,
                    upgrade_class_los = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? string.Empty : txtLamaNaikKelas.Value.ToInt().ToString(),
                    upgrade_class_payor = string.IsNullOrEmpty(rblNaikKelasRawat.SelectedValue) ? string.Empty : cboPenjaminNaikKelas.SelectedValue,
                    add_payment_pct = txtSelisihPersen.Visible ? (txtSelisihPersen.Value ?? 0).ToString() : "0",
                    birth_weight = txtBeratLahir.Value.ToInt().ToString(),
                    sistole = txtSistole.Value.ToString(),
                    diastole = txtDiastole.Value.ToString(),
                    discharge_status = cboCaraPulang.SelectedValue,
                    tarif_rs = new Common.Inacbg.v510.Detail.TarifRss()
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
                    covid19_penunjang_pengurang = new Common.Inacbg.v510.Detail.Covid19PenunjangPengurang()
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
                        rad_thorax_ap_pa = chkThoraxAPPA.Checked ? "1" : "0"  //
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
                    //payor_id = cboJaminan.SelectedValue.Split('|')[1],
                    //payor_cd = cboJaminan.SelectedValue.Split('|')[2],
                    //cob_cd = string.IsNullOrEmpty(cboCOB.SelectedValue) ? string.Empty : cboCOB.SelectedValue.Split('|')[1],
                    payor_id = payor_id_val,
                    payor_cd = payor_cd_val,
                    cob_cd = cob_cd_val,
                    coder_nik = AppSession.UserLogin.LicenseNo
                });
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

            var svc = new Common.Inacbg.v510.Service();
            var response = svc.Delete(new Common.Inacbg.v510.Patient.Delete.Data()
            {
                nomor_rm = patient.MedicalNo,
                coder_nik = AppSession.UserLogin.LicenseNo
            });
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

            var svc = new Common.Inacbg.v510.Service();
            var response = svc.Final(new Common.Inacbg.v510.Claim.Final.Data()
            {
                nomor_sep = txtNoSep.Text,
                coder_nik = AppSession.UserLogin.LicenseNo
            });
            if (response != null)
            {
                if (response.Metadata.IsValid)
                {
                    var ncc = new NccInacbg();
                    if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
                    {
                        ncc.CbgStatus = "final";
                        ncc.Save();
                    }

                    txtStatusKlaim.Text = "final";

                    btnKirim.Enabled = true;
                    btnKirim.Visible = true;
                    btnPrint.Enabled = true;
                    btnFinal.Enabled = false;
                    btnEdit.Enabled = true;
                    btnIdrgEdit.Visible = false;
                    btnIdrgEdit.Enabled = false;
                    btnInacbgEdit.Visible = false;
                    btnInacbgEdit.Enabled = false;

                    ScriptManager.RegisterStartupScript(this, GetType(), "final", string.Format("alert('Status klaim No. SEP : {0} = FINAL.');", txtNoSep.Text), true);
                }
                else ScriptManager.RegisterStartupScript(this, GetType(), "final", string.Format("alert('Code : {0}, Message : {1}');", response.Metadata.Code, response.Metadata.Message), true);
               
            }

            //btnFinal.Focus();
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

            var isChemo = (EpisodeProcedures?.Any(p => ((p.ProcedureID ?? string.Empty).Trim() == "99.25")) ?? false);
            if (isChemo && (txtObatKemoterapi.Value ?? 0) == 0)
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
                if (diags != null)
                {
                    foreach (var diag in diags)
                    {
                        diag.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        diag.LastUpdateDateTime = DateTime.Now;
                    }
                    diags.Save();
                }

                var procs = (ViewState["icd9cm"] as EpisodeProcedureCollection);
                if (procs != null)
                {
                    foreach (var proc in procs)
                    {
                        proc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        proc.LastUpdateDateTime = DateTime.Now;
                    }
                    procs.Save();
                }
            }

            try
            {
                string diagnosa = string.Empty;

                var src = (ViewState["icd10"] as EpisodeDiagnoseCollection) ?? EpisodeDiagnoses;
                if (src != null)
                {
                    diagnosa = string.Join("#",
                        src.Where(v => !string.IsNullOrWhiteSpace(v.DiagnoseID))
                            .OrderBy(v => v.SRDiagnoseType)
                            .Select(v => v.DiagnoseID));
                }

                if (!string.IsNullOrWhiteSpace(diagnosa))
                {
                    var svc = new Common.Inacbg.v510.Service();

                    var reqDx = new Common.Inacbg.v510.Diagnose.Data
                    {
                        nomor_sep = txtNoSep.Text,
                        diagnosa = diagnosa
                    };

                    var swDx = System.Diagnostics.Stopwatch.StartNew();
                    var resDx = svc.IDRGSetDiagnose(reqDx);
                    swDx.Stop();
                    LogApi("IDRGSetDiagnose", reqDx, resDx, swDx.ElapsedMilliseconds);

                    if (!resDx.Metadata.IsValid)
                    {
                        ShowInformationHeader($"Gagal update Diagnosa ke eKlaim: {resDx.Metadata.Message}");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogApi("IDRGSetDiagnose(EX)", new { nomor_sep = txtNoSep.Text }, new { error = ex.Message }, 0);
                ShowInformationHeader("Terjadi kesalahan saat update Diagnosa ke eKlaim.");
                return;
            }

            try
            {
                var procColl = (ViewState["icd9cm"] as EpisodeProcedureCollection) ?? EpisodeProcedures;

                string procedure;
                if (procColl == null || !procColl.Any(p => !string.IsNullOrWhiteSpace(p.ProcedureID)))
                {
                    procedure = string.Empty;
                }
                else
                {
                    try
                    {
                        procedure = BuildProcedureStringWithQty(procColl);
                    }
                    catch
                    {
                        var list = procColl
                            .Where(p => !string.IsNullOrWhiteSpace(p.ProcedureID))
                            .Select(p =>
                            {
                                var code = (p.ProcedureID ?? string.Empty).Trim();
                                var qty = (p.QtyICD > 0 ? p.QtyICD : 1);
                                return qty > 1 ? $"{code}+{qty}" : code;
                            });
                        procedure = string.Join("#", list);
                    }
                }

                if (!string.IsNullOrWhiteSpace(procedure))
                {
                    var svc = new Common.Inacbg.v510.Service();

                    var reqPr = new Common.Inacbg.v510.Procedure.Data
                    {
                        nomor_sep = txtNoSep.Text,
                        procedure = procedure
                    };

                    var swPr = System.Diagnostics.Stopwatch.StartNew();
                    var resPr = svc.IDRGSetProcedure(reqPr);
                    swPr.Stop();
                    LogApi("IDRGSetProcedure", reqPr, resPr, swPr.ElapsedMilliseconds);

                    if (!resPr.Metadata.IsValid)
                    {
                        ShowInformationHeader($"Gagal update Prosedur ke eKlaim: {resPr.Metadata.Message}");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogApi("IDRGSetProcedure(EX)", new { nomor_sep = txtNoSep.Text }, new { error = ex.Message }, 0);
                ShowInformationHeader("Terjadi kesalahan saat update Prosedur ke eKlaim.");
                return;
            }

            GetTransactionMode = "edit";
            //btnSave.Focus();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowAlert("edit-missing",
                    "No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            string paymentNo = null;
            if (GuarantorReceipts.Rows.Count > 0)
            {
                paymentNo = GuarantorReceipts.AsEnumerable()
                             .Select(g => g.Field<string>("PaymentNo")).Single();

                var invcoll = new InvoicesItemCollection();
                var invit = new InvoicesItemQuery("a");
                var inv = new InvoicesQuery("b");
                invit.InnerJoin(inv).On(invit.InvoiceNo == inv.InvoiceNo && inv.IsInvoicePayment == false && inv.IsVoid == false);
                invit.Where(invit.PaymentNo == paymentNo);
                invcoll.Load(invit);

                bool allowVoid = invcoll.Count <= 0;
                if (!allowVoid)
                {
                    ShowAlert("void-blocked","Transaksi ini sudah diproses ke Invoice. Jika tetap ingin Edit, silakan void Invoice terlebih dahulu.");
                    return;
                }

                var msg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(paymentNo, true);
                if (!string.IsNullOrEmpty(msg))
                {
                    ShowAlert("fee-verified", SanitizeJs(msg));
                    return;
                }
            }

            var svc = new Common.Inacbg.v510.Service();
            var ncc = new NccInacbg();
            if (!ncc.LoadByPrimaryKey(Request.QueryString["regno"])) ncc = new NccInacbg();

            try
            {
                using (var trans = new esTransactionScope())
                {
                    // 1) VOID & unset pembayaran lokal (jika ada payment)
                    if (!string.IsNullOrEmpty(paymentNo))
                    {
                        var etp = new TransPayment();
                        if (etp.LoadByPrimaryKey(paymentNo))
                        {
                            // Tandai void & un-approve
                            etp.IsApproved = false;
                            etp.IsVoid = true;
                            etp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            etp.LastUpdateDateTime = DateTime.Now;
                            etp.VoidByUserID = AppSession.UserLogin.UserID;
                            etp.VoidDate = DateTime.Now;

                            // IntermBill Guarantor
                            var collibguar = new TransPaymentItemIntermBillGuarantorCollection();
                            collibguar.Query.Where(collibguar.Query.PaymentNo == etp.PaymentNo);
                            collibguar.LoadAll();
                            foreach (var item in collibguar)
                            {
                                item.IsPaymentProceed = false;
                                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                item.LastUpdateDateTime = DateTime.Now;
                            }

                            // IntermBill Patient
                            var collib = new TransPaymentItemIntermBillCollection();
                            collib.Query.Where(collib.Query.PaymentNo == etp.PaymentNo);
                            collib.LoadAll();
                            foreach (var item in collib)
                            {
                                item.IsPaymentProceed = false;
                                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                item.LastUpdateDateTime = DateTime.Now;
                            }

                            // Item Order
                            var colltpio = new TransPaymentItemOrderCollection();
                            colltpio.Query.Where(colltpio.Query.PaymentNo == etp.PaymentNo);
                            colltpio.LoadAll();
                            foreach (var item in colltpio)
                            {
                                item.IsPaymentProceed = false;
                                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                item.LastUpdateDateTime = DateTime.Now;
                            }

                            // Buffer kalkulasi biaya
                            var collbuffer = new CostCalculationBufferCollection();
                            collbuffer.Query.Where(collbuffer.Query.PaymentNo == etp.PaymentNo);
                            collbuffer.LoadAll();
                            foreach (var item in collbuffer)
                                item.PaymentNo = null;

                            // Askes covered
                            var collac = new AskesCovered2Collection();
                            collac.Query.Where(collac.Query.PaymentNo == etp.PaymentNo);
                            collac.LoadAll();
                            foreach (var item in collac)
                            {
                                item.PaymentNo = null;
                                item.IsPaid = false;
                            }

                            // Hitung total & kembalikan ke RemainingAmount
                            var colltpi = new TransPaymentItemCollection();
                            colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo);
                            colltpi.LoadAll();
                            var total = colltpi.Sum(detail => (decimal)detail.Amount);

                            var reg = new Registration();
                            reg.LoadByPrimaryKey(etp.RegistrationNo);
                            reg.RemainingAmount += total;

                            // Lepas DP yang refer ke payment ini
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

                            // Unset payment jasmed
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.UnSetPayment(etp, AppSession.UserLogin.UserID);

                            // Save perubahan lokal (sementara di transaksi)
                            etp.Save();
                            reg.Save();
                            collib.Save();
                            collibguar.Save();
                            colltpio.Save();
                            collbuffer.Save();
                            collac.Save();
                            collDP.Save();
                            feeColl.Save();

                            // Jurnal
                            if (AppSession.Parameter.IsUsingIntermBill)
                            {
                                int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(
                                    BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP", etp.PaymentNo,
                                    AppSession.UserLogin.UserID, 0);
                            }
                            else
                            {
                                int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(
                                    BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP",
                                    AppSession.UserLogin.UserID, 0);
                            }
                        }
                    }

                    var response = svc.Edit(new Common.Inacbg.v510.Claim.Edit.Data()
                    {
                        nomor_sep = txtNoSep.Text
                    });

                    if (response == null || !response.Metadata.IsValid)
                    {
                        var code = response?.Metadata?.Code;
                        var msg = response?.Metadata?.Message ?? "Tidak ada respons dari server.";
                        ShowAlert("edit-failed",
                            SanitizeJs($"Edit klaim gagal. Code: {code}, Message: {msg}"));
                        return;
                    }

                    ncc.CbgStatus = "normal";
                    ncc.Save();

                    trans.Complete();
                }

                btnKirim.Enabled = false;
                btnPrint.Enabled = false;
                btnPrint.Visible = false;
                btnFinal.Enabled = true;
                btnEdit.Enabled = false;
                btnIdrgEdit.Visible = true;
                btnIdrgEdit.Enabled = true;
                btnInacbgEdit.Visible = true;
                btnInacbgEdit.Enabled = true;

                var alrt = $"alert('Status klaim No. SEP : {SanitizeJs(txtNoSep.Text)} = NORMAL.');";
                var ram = Telerik.Web.UI.RadAjaxManager.GetCurrent(Page);
                if (ram != null) ram.ResponseScripts.Add(alrt);
                else ScriptManager.RegisterStartupScript(this, GetType(), "edit-ok-" + DateTime.Now.Ticks, alrt, true);
            }
            catch (Exception ex)
            {
                ShowAlert("edit-ex",
                    SanitizeJs("Terjadi kesalahan saat proses Edit: " + ex.Message));
            }
        }


        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    HideInformationHeader();

        //    if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
        //    {
        //        ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
        //        return;
        //    }

        //    var svc = new Common.Inacbg.v510.Service();
        //    var response = svc.Edit(new Common.Inacbg.v510.Claim.Edit.Data() { nomor_sep = txtNoSep.Text });
        //    if (response != null)
        //    {
        //        if (response.Metadata.IsValid)
        //        {
        //            //void guarantor a/r
        //            if (GuarantorReceipts.Rows.Count > 0)
        //            {
        //                var paymentNo = GuarantorReceipts.AsEnumerable().Select(g => g.Field<string>("PaymentNo")).Single();

        //                //cek invoice
        //                var invcoll = new InvoicesItemCollection();
        //                var invit = new InvoicesItemQuery("a");
        //                var inv = new InvoicesQuery("b");
        //                invit.InnerJoin(inv).On(invit.InvoiceNo == inv.InvoiceNo && inv.IsInvoicePayment == false && inv.IsVoid == false);
        //                invit.Where(invit.PaymentNo == paymentNo);
        //                invcoll.Load(invit);

        //                bool allowVoid = invcoll.Count <= 0;

        //                if (!allowVoid)
        //                {
        //                    ShowAlert("void-blocked",
        //                        "Edit Berhasil Status Klaim: Normal, Transaksi ini sudah diproses ke Invoice. Jika tetap ingin Edit, silakan void Invoice terlebih dahulu.");
        //                    return;
        //                }

        //                // cek sudah tarik ke jasmed (jasmed by dischargedate) atau belum 
        //                var msg = ParamedicFeeTransChargesItemCompByDischargeDate.IsParamedicFeeVerified(paymentNo, true);
        //                if (!string.IsNullOrEmpty(msg))
        //                {
        //                    ShowInformationHeader(msg);
        //                    return;
        //                }

        //                var etp = new TransPayment();
        //                if (etp.LoadByPrimaryKey(paymentNo))
        //                {
        //                    using (var trans = new esTransactionScope())
        //                    {
        //                        etp.IsApproved = false;
        //                        etp.IsVoid = true;
        //                        etp.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                        etp.LastUpdateDateTime = DateTime.Now;

        //                        etp.VoidByUserID = AppSession.UserLogin.UserID;
        //                        etp.VoidDate = DateTime.Now;

        //                        var collibguar = new TransPaymentItemIntermBillGuarantorCollection();
        //                        collibguar.Query.Where(collibguar.Query.PaymentNo == etp.PaymentNo);
        //                        collibguar.LoadAll();

        //                        foreach (var item in collibguar)
        //                        {
        //                            item.IsPaymentProceed = false;
        //                            item.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                            item.LastUpdateDateTime = DateTime.Now;
        //                        }

        //                        var collib = new TransPaymentItemIntermBillCollection();
        //                        collib.Query.Where(collib.Query.PaymentNo == etp.PaymentNo);
        //                        collib.LoadAll();

        //                        foreach (var item in collib)
        //                        {
        //                            item.IsPaymentProceed = false;
        //                            item.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                            item.LastUpdateDateTime = DateTime.Now;
        //                        }

        //                        var colltpio = new TransPaymentItemOrderCollection();
        //                        colltpio.Query.Where(colltpio.Query.PaymentNo == etp.PaymentNo);
        //                        colltpio.LoadAll();
        //                        foreach (var item in colltpio)
        //                        {
        //                            item.IsPaymentProceed = false;
        //                            item.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                            item.LastUpdateDateTime = DateTime.Now;
        //                        }

        //                        var collbuffer = new CostCalculationBufferCollection();
        //                        collbuffer.Query.Where(collbuffer.Query.PaymentNo == etp.PaymentNo);
        //                        collbuffer.LoadAll();

        //                        foreach (var item in collbuffer)
        //                        {
        //                            item.PaymentNo = null;
        //                        }

        //                        var collac = new AskesCovered2Collection();
        //                        collac.Query.Where(collac.Query.PaymentNo == etp.PaymentNo);
        //                        collac.LoadAll();

        //                        foreach (var item in collac)
        //                        {
        //                            item.PaymentNo = null;
        //                            item.IsPaid = false;
        //                        }

        //                        var colltpi = new TransPaymentItemCollection();
        //                        colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo);
        //                        colltpi.LoadAll();

        //                        var total = colltpi.Sum(detail => (decimal)detail.Amount);

        //                        var reg = new Registration();
        //                        reg.LoadByPrimaryKey(etp.RegistrationNo);
        //                        reg.RemainingAmount += total;

        //                        var collDP = new TransPaymentCollection();
        //                        collDP.Query.Where(collDP.Query.PaymentReferenceNo.Equal(etp.PaymentNo));
        //                        collDP.LoadAll();
        //                        foreach (var dp in collDP)
        //                        {
        //                            dp.PaymentReferenceNo = string.Empty;
        //                            dp.ReceiptIsReturned = null;
        //                            dp.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //                            dp.LastUpdateDateTime = DateTime.Now;
        //                        }

        //                        // unset payment jasmed
        //                        var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
        //                        feeColl.UnSetPayment(etp, AppSession.UserLogin.UserID);

        //                        etp.Save();
        //                        reg.Save();
        //                        collib.Save();
        //                        collibguar.Save();
        //                        colltpio.Save();
        //                        collbuffer.Save();
        //                        collac.Save();
        //                        collDP.Save();
        //                        feeColl.Save();

        //                        if (AppSession.Parameter.IsUsingIntermBill)
        //                        {

        //                            int? journalId = JournalTransactions.AddNewPaymentCorrectionJournalCashBased(BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP", etp.PaymentNo, AppSession.UserLogin.UserID, 0);
        //                        }
        //                        else
        //                        {

        //                            int? journalId = JournalTransactions.AddNewPaymentCorrectionJournal(BusinessObject.JournalType.ARCreating, etp, reg, colltpi, "TP", AppSession.UserLogin.UserID, 0);
        //                        }

        //                        #region Guarantor Deposit Balance

        //                        colltpi = new TransPaymentItemCollection();
        //                        colltpi.Query.Where(colltpi.Query.PaymentNo == etp.PaymentNo, colltpi.Query.SRPaymentType == AppSession.Parameter.PaymentTypeSaldoAR);
        //                        colltpi.LoadAll();
        //                        if (colltpi.Count > 0)
        //                        {
        //                            total = colltpi.Sum(detail => (decimal)detail.Amount);

        //                            var balance = new GuarantorDepositBalance();
        //                            var movement = new GuarantorDepositMovement();
        //                            GuarantorDepositBalance.PrepareGuarantorDepositBalances(etp.PaymentNo, etp.GuarantorID, "A/R Process (Void)", AppSession.UserLogin.UserID, total, 0, ref balance, ref movement);
        //                            balance.Save();
        //                            movement.Save();
        //                        }

        //                        #endregion

        //                        trans.Complete();
        //                    }
        //                }
        //            }

        //            var ncc = new NccInacbg();
        //            if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
        //            {
        //                ncc.CbgStatus = "normal";
        //                ncc.Save();
        //            }

        //            btnKirim.Enabled = false;
        //            btnPrint.Enabled = false;
        //            btnPrint.Visible = false;
        //            btnFinal.Enabled = true;
        //            btnEdit.Enabled = false;
        //            btnIdrgEdit.Visible = true;
        //            btnIdrgEdit.Enabled = true;
        //            btnInacbgEdit.Visible = true;
        //            btnInacbgEdit.Enabled = true;

        //            var alrt = $"alert('Status klaim No. SEP : {txtNoSep.Text.Replace("'", "`")} = NORMAL.');";
        //            var ram = Telerik.Web.UI.RadAjaxManager.GetCurrent(Page);
        //            if (ram != null)
        //            {
        //                ram.ResponseScripts.Add(alrt);
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(),
        //                    "edit-ok-" + DateTime.Now.Ticks.ToString(),
        //                    alrt, true);
        //            }
        //        }
        //        else ScriptManager.RegisterStartupScript(this, GetType(), "edit", string.Format("alert('Code : {0}, Message : {1}');", response.Metadata.Code, response.Metadata.Message), true);
        //    }

        //}

        protected void btnKirim_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var svc = new Common.Inacbg.v510.Service();
            var response = svc.Send(new Common.Inacbg.v510.Claim.Create.Data() { nomor_sep = txtNoSep.Text });
            if (response.Metadata.IsValid)
            {
                foreach (var data in response.Response.Data)
                {
                    var ncc = new NccInacbg();
                    if (ncc.LoadByPrimaryKey(Request.QueryString["regno"]))
                    {
                        ncc.CbgSentStatus = data.KemkesDcStatus;
                        ncc.Save();
                    }
                }
            }
            else ScriptManager.RegisterStartupScript(this, GetType(), "send", string.Format("alert('Code : {0}, Message : {1}');", response.Metadata.Code, response.Metadata.Message), true);

            //btnKirim.Focus();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (string.IsNullOrEmpty(txtNoPeserta.Text) || string.IsNullOrEmpty(txtNoSep.Text))
            {
                ShowInformationHeader("No. Peserta dan No. SEP tidak boleh kosong.");
                return;
            }

            var svc = new Common.Inacbg.v510.Service();
            var response = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data() { nomor_sep = txtNoSep.Text });
            if (response.Metadata.IsValid)
            {
                if (response.DataResponse.Data.KlaimStatusCd != "final")
                {
                    ShowInformationHeader("Status klaim belum final.");
                    return;
                }

                svc = new Common.Inacbg.v510.Service();
                var print = svc.Print(new Temiang.Avicenna.Common.Inacbg.v510.Claim.Create.Data() { nomor_sep = txtNoSep.Text });
                if (print.Metadata.IsValid) ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("<script language='javascript' type='text/javascript'>openPrint('{0}');</script>", txtNoSep.Text), true);
                else ScriptManager.RegisterStartupScript(this, GetType(), "print", string.Format("alert('{0} - {1}');", print.Metadata.Code, print.Metadata.Message), true);

                //btnPrint.Focus();
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {

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

        //public string IsReceiptAvalilable
        //{
        //    get { return GuarantorReceipts.Rows.Count.ToString(); }
        //}

        public string IsReceiptAvalilable
        {

            get
            {
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

                    var svc = new Common.Inacbg.v510.Service();
                    var response = svc.GetDetail(new Common.Inacbg.v510.Claim.Get.GetDetail.Data() { nomor_sep = txtNoSep.Text });
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
                        FileUploadCollection.Add(new FileUpload2()
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
            EpisodeRuangRawatCollection.Add(new EpisodeRuangRawat2() { Key = EpisodeRuangRawatCollection.Count() + 1, ID = e.Value, Nama = e.Text, Jumlah = 1 });
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

        private List<EpisodeRuangRawat2> EpisodeRuangRawatCollection
        {
            get
            {
                if (ViewState["episodeRuangRawat"] == null) ViewState["episodeRuangRawat"] = new List<EpisodeRuangRawat2>();
                return ViewState["episodeRuangRawat"] as List<EpisodeRuangRawat2>;
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

        private List<FileUpload2> FileUploadCollection
        {
            get
            {
                if (ViewState["fileUpload"] == null) ViewState["fileUpload"] = new List<FileUpload2>();
                return ViewState["fileUpload"] as List<FileUpload2>;
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

        protected void chkPasienTb_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkPasienTb.Checked)
            //{
            //    txtSitbNo.Text = string.Empty;
            //    txtSitbNo.Enabled = true;
            //    btnValidasiSitb.Enabled = true;
            //}
            //else
            //{
            //    txtSitbNo.Text = string.Empty;
            //    txtSitbNo.Enabled = false;
            //    btnValidasiSitb.Enabled = false;
            //}

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
        }

        protected void btnValidasiCoinsidenseCovid_Click(object sender, EventArgs e)
        {

        }

        private List<Delivery2> Deliveries
        {
            get
            {
                if (Session["deliveryEklaim"] == null) Session["deliveryEklaim"] = new List<Delivery2>();
                return Session["deliveryEklaim"] as List<Delivery2>;
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
            var entity = new Delivery2();
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

        private void SetEntityValue(Delivery2 entity, GridCommandEventArgs e)
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
            btnInacbgGroup_Click(null, null);
        }

        #region helper
        private static bool HasUngroupFlag(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            return s.IndexOf("UNGROUPABLE", StringComparison.OrdinalIgnoreCase) >= 0
                || s.IndexOf("UNRELATED", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void SetErrorColor(WebControl ctrl, bool isError)
        {
            ctrl.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.SystemColors.ControlText;
        }

        private static string SanitizeJs(string s) => (s ?? "").Replace("'", "`");

        private void ShowAlert(string key, string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), key, $"alert('{SanitizeJs(message)}');", true);
        }

        private void SaveNccIdrg(string action, object reqObj, object resObj)
        {
            var regno = Request.QueryString["regno"];
            if (string.IsNullOrWhiteSpace(regno)) return;

            var medicalNo = GetCurrentMedicalNo();
            var sep = txtNoSep.Text;

            var row = new NccIDRG();
            if (!row.LoadByPrimaryKey(regno))
                row = new NccIDRG { RegistrationNo = regno };

            row.MedicalNo = medicalNo;
            row.SEP = sep;

            string reqJson = reqObj == null ? null : JsonConvert.SerializeObject(reqObj);
            string resJson = resObj == null ? null : JsonConvert.SerializeObject(resObj);

            switch (action)
            {
                case "ClaimData": row.ClaimDataRequest = reqJson; row.ClaimDataResponse = resJson; break;
                case "IdrgDiagnosaSet": row.IdrgDiagnosaSetReq = reqJson; row.IdrgDiagnosaSetRes = resJson; break;
                case "IdrgDiagnosaGet": row.IdrgDiagnosaGetReq = reqJson; row.IdrgDiagnosaGetRes = resJson; break;
                case "IdrgProcedureSet": row.IdrgProcedureSetReq = reqJson; row.IdrgProcedureSetRes = resJson; break;
                case "IdrgProcedureGet": row.IdrgProcedureGetReq = reqJson; row.IdrgProcedureGetRes = resJson; break;
                case "IdrgGroup": row.GroupingIdrgReq = reqJson; row.GroupingIdrgRes = resJson; break;
                case "IdrgFinal": row.FinalIdrgReq = reqJson; row.FinalIdrgRes = resJson; break;
                case "IdrgReEdit": row.ReEditIdrgReq = reqJson; row.ReEditIdrgRes = resJson; break;
                case "ImportIdrgToInacbg": row.IdrgToInacbgImportReq = reqJson; row.IdrgToInacbgImportRes = resJson; break;

                case "InacbgDiagnosaGet": row.InacbgDiagnosaGetReq = reqJson; row.InacbgDiagnosaGetRes = resJson; break;
                case "InacbgDiagnosaSet": row.InacbgDiagnosaSetReq = reqJson; row.InacbgDiagnosaSetRes = resJson; break;
                case "InacbgProcedureSet": row.InacbgProcedureSetReq = reqJson; row.InacbgProcedureSetRes = resJson; break;
                case "InacbgProcedureGet": row.InacbgProcedureGetReq = reqJson; row.InacbgProcedureGetRes = resJson; break;
                case "InacbgStage1": row.GroupingInacbgStage1Req = reqJson; row.GroupingInacbgStage1Res = resJson; break;
                case "InacbgStage2": row.GroupingInacbgStage2Req = reqJson; row.GroupingInacbgStage2Res = resJson; break;
                case "InacbgFinal": row.FinalInacbgReq = reqJson; row.FinalInacbgRes = resJson; break;
                case "InacbgReEdit": row.ReEditInacbgReq = reqJson; row.ReEditInacbgRes = resJson; break;
                case "ClaimFinal": row.ClaimFinalReq = reqJson; row.ClaimFinalRes = resJson; break;
                case "ClaimReEdit": row.ClaimReEditReq = reqJson; row.ClaimReEditRes = resJson; break;
                case "ClaimSend": row.ClaimSendReq = reqJson; row.ClaimSendRes = resJson; break;
                case "GetClaimData": row.GetClaimDataReq = reqJson; row.GetClaimDataRes = resJson; break;
                default:
                    break;
            }

            row.LastUpdateDateTime = DateTime.Now;
            row.LastUpdateByUserID = AppSession.UserLogin.UserID;
            row.Save();
        }

        private void LogApi(string url, object param, object response, long elapsedMs)
        {
            try
            {
                var log = new WebServiceAPILog
                {
                    DateRequest = DateTime.Now,
                    IPAddress = txtNoSep.Text,
                    UrlAddress = url,
                    Params = JsonConvert.SerializeObject(param),
                    Response = JsonConvert.SerializeObject(response),
                    Totalms = (int)elapsedMs
                };
                log.Save();
            }
            catch
            {

            }
        }

        private string GetCurrentMedicalNo()
        {
            var medicalNo = !string.IsNullOrWhiteSpace(AppSession.Parameter.EklaimRemoveDashSeparatorOnMedicalNo)
                            && AppSession.Parameter.EklaimRemoveDashSeparatorOnMedicalNo.Equals("yes", StringComparison.OrdinalIgnoreCase)
                            ? txtNoMR.Text.Replace("-", string.Empty)
                            : txtNoMR.Text;

            switch (AppSession.Parameter.HealthcareInitialAppsVersion)
            {
                case "YBRSGKP":
                    var pat = new Patient();
                    pat.LoadByMedicalNo(txtNoMR.Text);
                    if (!string.IsNullOrEmpty(pat.OldMedicalNo)) medicalNo = medicalNo.ToInt().ToString();
                    medicalNo = medicalNo.ToInt().ToString();
                    break;
            }

            return medicalNo;
        }

        private bool HasPrimaryDx()
        {
            var main = AppSession.Parameter.DiagnoseTypeMain ?? string.Empty;

            return EpisodeDiagnoses != null &&
                   EpisodeDiagnoses.Any(d => string.Equals(d.SRDiagnoseType, main, StringComparison.OrdinalIgnoreCase));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            bool isUngroup = hfIdrgIsUngroup.Value == "1";

            SetErrorColor(txtIDRGMDC, isUngroup);
            SetErrorColor(txtIDRGMDCCode, isUngroup);
            SetErrorColor(txtIDRGDRG, isUngroup);
            SetErrorColor(txtIDRGDRGCode, isUngroup);

            //btnFnliDRG.Enabled = !isUngroup;

            hfHasPrimaryDx.Value = HasPrimaryDx() ? "1" : "0";

            var sep = txtNoSep.Text?.Trim();
            if (string.IsNullOrWhiteSpace(sep)) return;

            var svc = new Common.Inacbg.v510.Service();

            try
            {
                var dxRes = svc.InacbgGetDiagnose(new Common.Inacbg.v510.Diagnose.Get.Data { nomor_sep = sep });
                var warnDx = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                var dx = dxRes?.Data?.Expanded;
                if (dx != null)
                {
                    foreach (var it in dx)
                    {
                        var code = it?.Code?.Trim();
                        if (string.IsNullOrEmpty(code)) continue;

                        var mcode = it?.Metadata?.Code?.ToString();
                        if (!string.Equals(mcode, "200", StringComparison.OrdinalIgnoreCase))
                        {
                            var msg = (it?.Metadata?.Message ?? "Tidak valid").Trim();
                            warnDx[code] = msg;
                        }
                    }
                }

                foreach (Telerik.Web.UI.GridDataItem row in grdDiagnosaInacbg.MasterTableView.Items)
                {
                    var lbl = row.FindControl("lblwarn") as Label;
                    var hid = row.FindControl("hidDxCode") as HiddenField;
                    if (lbl == null || hid == null) continue;

                    var code = hid.Value?.Trim();
                    if (!string.IsNullOrEmpty(code) && warnDx.TryGetValue(code, out var warnMsg))
                    {
                        lbl.Text = warnMsg;
                        lbl.Visible = true;
                    }
                    else
                    {
                        lbl.Visible = false;
                    }
                }
            }
            catch
            {
                // optional: log
            }

            try
            {
                var pxRes = svc.InacbgGetProcedure(new Common.Inacbg.v510.Procedure.Get.Data { nomor_sep = sep });
                var warnPx = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                var px = pxRes?.Data?.Expanded;
                if (px != null)
                {
                    foreach (var it in px)
                    {
                        var code = it?.Code?.Trim();
                        if (string.IsNullOrEmpty(code)) continue;

                        var mcode = it?.Metadata?.Code?.ToString();
                        if (!string.Equals(mcode, "200", StringComparison.OrdinalIgnoreCase))
                        {
                            var msg = (it?.Metadata?.Message ?? "Tidak valid").Trim();
                            warnPx[code] = msg;
                        }
                    }
                }

                foreach (Telerik.Web.UI.GridDataItem row in grdProsedurInacbg.MasterTableView.Items)
                {
                    var lbl = row.FindControl("lblwarnPx") as Label;
                    var hid = row.FindControl("hidPxCode") as HiddenField;
                    if (lbl == null || hid == null) continue;

                    var code = hid.Value?.Trim();
                    if (!string.IsNullOrEmpty(code) && warnPx.TryGetValue(code, out var warnMsg))
                    {
                        lbl.Text = warnMsg;
                        lbl.Visible = true;
                    }
                    else
                    {
                        lbl.Visible = false;
                    }
                }
            }
            catch
            {
                // optional: log
            }
        }

        private static string BuildProcedureStringWithQty(IEnumerable<EpisodeProcedure> rows)
        {
            var tokens = rows
                .Where(x => x != null && x.IsVoid == false)
                .OrderBy(x => x.SequenceNo.ToInt())
                .Select(x =>
                {
                    var q = (x.QtyICD <= 0 ? 1 : x.QtyICD);
                    return q > 1 ? $"{x.ProcedureID}+{q}" : x.ProcedureID;
                })
                .ToList();

            return tokens.Count == 0 ? "#" : string.Join("#", tokens);
        }

        private static string BuildProcedureStringWithQty2(IEnumerable<EpisodeProcedureInaGroupper> rows)
        {
            var tokens = rows
                .Where(x => x != null && x.IsVoid == false)
                .OrderBy(x => x.SequenceNo.ToInt())
                .Select(x =>
                {
                    return x.ProcedureID;
                })
                .ToList();

            return tokens.Count == 0 ? "#" : string.Join("#", tokens);
        }

        private int GetNextEpisodeProcedureSeq(string regno)
        {
            var used = new HashSet<int>();

            var q = new EpisodeProcedureQuery("ep");
            q.Select(q.SequenceNo);
            q.Where(q.RegistrationNo == regno); // TIDAK filter IsVoid
            var all = new EpisodeProcedureCollection();
            all.Load(q);

            foreach (var row in all)
            {
                if (int.TryParse(row.SequenceNo, out var sn) && sn > 0) used.Add(sn);
            }

            var i = 1;
            while (used.Contains(i)) i++;
            return i;
        }

        private static string PadSeq(int n) => n.ToString("000");

        private bool TrySaveEpisodeProceduresWithRetry(EpisodeProcedureCollection coll, string regno, EpisodeProcedure ep)
        {
            try
            {
                coll.Save();
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                var newSeq = GetNextEpisodeProcedureSeq(regno);
                ep.SequenceNo = PadSeq(newSeq);
                coll.Save();
                return true;
            }
        }

        //private void SetCtl(object ctrl, bool enabled)
        //{
        //    if (ctrl == null) return;

        //    // Telerik
        //    if (ctrl is Telerik.Web.UI.RadTextBox rtb) { rtb.ReadOnly = !enabled; return; }
        //    if (ctrl is Telerik.Web.UI.RadNumericTextBox rnum) { rnum.Enabled = enabled; rnum.ReadOnly = !enabled; return; }
        //    if (ctrl is Telerik.Web.UI.RadComboBox rcb) { rcb.Enabled = enabled; return; }
        //    if (ctrl is Telerik.Web.UI.RadDatePicker rdp) { rdp.Enabled = enabled; rdp.DateInput.ReadOnly = !enabled; if (rdp.DatePopupButton != null) rdp.DatePopupButton.Enabled = enabled; return; }
        //    if (ctrl is Telerik.Web.UI.RadTimePicker rtp) { rtp.Enabled = enabled; rtp.DateInput.ReadOnly = !enabled; return; }
        //    if (ctrl is Telerik.Web.UI.RadGrid rg) { rg.Enabled = enabled; return; }
        //    if (ctrl is Telerik.Web.UI.RadWindow rw) { rw.VisibleOnPageLoad = enabled && rw.VisibleOnPageLoad; return; }

        //    // ASP.NET WebForms
        //    if (ctrl is TextBox tb) { tb.ReadOnly = !enabled; return; }
        //    if (ctrl is Button btn) { btn.Enabled = enabled; return; }
        //    if (ctrl is ImageButton ib) { ib.Enabled = enabled; return; }
        //    if (ctrl is CheckBox cbx) { cbx.Enabled = enabled; return; }
        //    if (ctrl is RadioButtonList rbl) { rbl.Enabled = enabled; return; }
        //    if (ctrl is DropDownList ddl) { ddl.Enabled = enabled; return; }
        //}

        //public void DisableAllInputs()
        //{
        //    bool en = false;

        //    // ===== BIODATA =====
        //    SetCtl(txtNoMR, en);
        //    SetCtl(txtNamaPasien, en);
        //    SetCtl(rblJenisKelamin, en);
        //    SetCtl(txtTglLahir, en);

        //    // ===== GUARANTOR / IDENTITAS =====
        //    SetCtl(cboJaminan, en);
        //    SetCtl(cboNoPeserta, en);
        //    SetCtl(txtNoPeserta, en);
        //    SetCtl(txtNoSep, en);
        //    SetCtl(cboCOB, en);

        //    // ===== REGISTRASI / RAWAT =====
        //    SetCtl(rblJenisRawat, en);
        //    SetCtl(chkNaikKelas, en);
        //    SetCtl(chkRawatIntensif, en);
        //    SetCtl(txtTglMasuk, en);
        //    SetCtl(txtJamMasuk, en);
        //    SetCtl(txtTglPulang, en);
        //    SetCtl(txtJamPulang, en);
        //    SetCtl(cboCaraMasuk, en);

        //    // Ventilator detail
        //    SetCtl(chkVentilator, en);
        //    SetCtl(txtTglIntubasi, en);
        //    SetCtl(txtJamIntubasi, en);
        //    SetCtl(txtTglEkstubasi, en);
        //    SetCtl(txtJamEkstubasi, en);

        //    // Episode ruang rawat
        //    SetCtl(cboEpisode, en);
        //    SetCtl(grdEpisodeRuangRawat, en);

        //    // COVID flags & related
        //    SetCtl(cboStatusCov, en);
        //    SetCtl(rblKriteriaAksesNaat, en);
        //    SetCtl(rblRsDaruratLapangan, en);

        //    // Naik kelas
        //    SetCtl(rblNaikKelasRawat, en);
        //    SetCtl(cboPenjaminNaikKelas, en);
        //    SetCtl(txtLamaNaikKelas, en);

        //    // Angka rawat & umur
        //    SetCtl(txtRawatIntensif, en);
        //    SetCtl(txtLOS, en);
        //    SetCtl(txtSubAcute, en);
        //    SetCtl(txtChronic, en);
        //    SetCtl(cboDPJP, en);
        //    SetCtl(txtUmur, en);

        //    // TB & SITB
        //    SetCtl(chkPasienTb, en);
        //    SetCtl(txtSitbNo, en);
        //    SetCtl(btnValidasiSitb, en);

        //    // Co-insidense Covid
        //    SetCtl(chkCoinsidenseCovid, en);
        //    SetCtl(txtNomorKlaimCovid, en);
        //    SetCtl(btnValidasiCoinsidenseCovid, en);

        //    // Status kelainan / komorbid / isolasi
        //    SetCtl(cboStatusKelainan, en);
        //    SetCtl(rblKomorbid, en);
        //    SetCtl(rblIsolasiMandiri, en);
        //    SetCtl(rblCoInsidens, en);

        //    // Lain-lain umumnya
        //    SetCtl(txtVentilator, en);
        //    SetCtl(txtBeratLahir, en);
        //    SetCtl(cboCaraPulang, en);
        //    SetCtl(cboJenisTariff, en);
        //    SetCtl(txtTariffPoliEks, en);
        //    SetCtl(txtTerapiKovalesen, en);

        //    // ===== TARIF RS =====
        //    SetCtl(txtTarifRS, en);
        //    SetCtl(txtProsedurNonBedah, en);
        //    SetCtl(txtTenagaAhli, en);
        //    SetCtl(txtRadiologi, en);
        //    SetCtl(txtRehabilitasi, en);
        //    SetCtl(txtObat, en);
        //    SetCtl(txtSewaAlat, en);
        //    SetCtl(txtProsedurBedah, en);
        //    SetCtl(txtKeperawatan, en);
        //    SetCtl(txtLaboratorium, en);
        //    SetCtl(txtKamarAkomodasi, en);
        //    SetCtl(txtObatKronis, en);
        //    SetCtl(txtAlkes, en);
        //    SetCtl(txtKonsultasi, en);
        //    SetCtl(txtPenunjang, en);
        //    SetCtl(txtPelayananDarah, en);
        //    SetCtl(txtRawatIntensifTarif, en);
        //    SetCtl(txtObatKemoterapi, en);
        //    SetCtl(txtBMHP, en);

        //    // ===== TTV =====
        //    SetCtl(txtSistole, en);
        //    SetCtl(txtDiastole, en);

        //    // ===== COVID – unggahan dokumen =====
        //    SetCtl(fuResumeMedis, en); SetCtl(btnFuResumeMedis, en); SetCtl(grdResumeMedis, en);
        //    SetCtl(fuRuangPerawatan, en); SetCtl(btnFuRuangPerawatan, en); SetCtl(grdRuangPerawatan, en);
        //    SetCtl(fuLaboratorium, en); SetCtl(btnFuLaboratorium, en); SetCtl(grdLaboratorium, en);
        //    SetCtl(fuRadiologi, en); SetCtl(btnFuRadiologi, en); SetCtl(grdRadiologi, en);
        //    SetCtl(fuPenunjang, en); SetCtl(btnFuPenunjang, en); SetCtl(grdPenunjang, en);
        //    SetCtl(fuObat, en); SetCtl(btnFuObat, en); SetCtl(grdObat, en);
        //    SetCtl(fuBilling, en); SetCtl(btnFuBilling, en); SetCtl(grdBilling, en);
        //    SetCtl(fuKartu, en); SetCtl(btnFuKartu, en); SetCtl(grdKartu, en);
        //    SetCtl(fuKipi, en); SetCtl(btnFuKipi, en); SetCtl(grdKipi, en);
        //    SetCtl(fuLainlain, en); SetCtl(btnFuLainlain, en); SetCtl(grdLainlain, en);

        //    // ===== APGAR =====
        //    SetCtl(txt1Appearance, en);
        //    SetCtl(txt1Pulse, en);
        //    SetCtl(txt1Grimace, en);
        //    SetCtl(txt1Activity, en);
        //    SetCtl(txt1Respiration, en);
        //    SetCtl(txt5Appearance, en);
        //    SetCtl(txt5Pulse, en);
        //    SetCtl(txt5Grimace, en);
        //    SetCtl(txt5Activity, en);
        //    SetCtl(txt5Respiration, en);

        //    // ===== PREGNANT / DELIVERY =====
        //    SetCtl(txtUsiaKehamilan, en);
        //    SetCtl(txtGravida, en);
        //    SetCtl(txtPartus, en);
        //    SetCtl(txtAbortus, en);
        //    SetCtl(cboOnsetKontraksi, en);
        //    SetCtl(grdDelivery, en);

        //    // ===== iDRG =====
        //    SetCtl(cboDiagnosaIdrg, en);
        //    SetCtl(btnFilterDiagnosaIdrg, en);
        //    SetCtl(btnInsertDiagnosaIdrg, en);
        //    SetCtl(grdDiagnosaIdrg, en);

        //    SetCtl(cboProsedurIdrg, en);
        //    SetCtl(btnFilterProsedurIdrg, en);
        //    SetCtl(txtQtyProc, en);
        //    SetCtl(btnInsertProsedurIdrg, en);
        //    SetCtl(grdProsedurIdrg, en);

        //    SetCtl(btnIdrgGroup, en);
        //    SetCtl(btnIdrgEdit, en);

        //    // ===== INACBG =====
        //    SetCtl(cboDiagnosaInacbg, en);
        //    SetCtl(btnFilterDiagnosaInacbg, en);
        //    SetCtl(btnInsertDiagnosaInacbg, en);
        //    SetCtl(grdDiagnosaInacbg, en);

        //    SetCtl(cboProsedurInacbg, en);
        //    SetCtl(btnFilterProsedurInacbg, en);
        //    SetCtl(btnInsertProsedurInacbg, en);
        //    SetCtl(grdProsedurInacbg, en);

        //    SetCtl(btnImprtIdrg, en);
        //    SetCtl(btnInacbgGroup, en);
        //    SetCtl(btnInacbgEdit, en);

        //    // ===== TOOLBAR / FOOTER =====
        //    SetCtl(btnSave, en);
        //    SetCtl(btnFinal, en);
        //    SetCtl(btnEdit, en);
        //    SetCtl(btnPrint, en);
        //    SetCtl(btnKirim, en);
        //}
        #endregion
    }

    #region class
    [Serializable]
    public class EpisodeRuangRawat2
    {
        public int Key { get; set; }

        public string ID { get; set; }

        public string Nama { get; set; }

        public int Jumlah { get; set; }
    }

    [Serializable]
    public class FileUpload2
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

    public class Delivery2
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
    #endregion
}
