using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Module.Charges;
using Temiang.Dal.DynamicQuery;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class RasprajaEntry : BasePageDialog
    {
        private string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        private int RasproSeqNo
        {
            get
            {
                return Request.QueryString["rsno"].ToInt();
            }
        }

        private string Mode
        {
            get
            {
                return Request.QueryString["mod"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;

            if (!IsPostBack)
            {
                var stdi = new AppStandardReferenceItem();
                stdi.LoadByPrimaryKey(AppEnum.StandardReference.RASPRO.ToString(), AppConstant.RasproType.Raspraja);
                Title = stdi.ItemName;
                lblRasproNote.Text = stdi.Note;

                txtParamedicName.Text = Paramedic.GetParamedicName(ParamedicTeam.DPJP(RegistrationNo).ParamedicID);

                txtRegistrationNo.Text = RegistrationNo;
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    txtPatientName.Text = pat.PatientName;
                    txtMedicalNo.Text = pat.MedicalNo;
                    txtDateOfBirth.SelectedDate = pat.DateOfBirth;
                }


                if (Mode == "new")
                {
                    txtRasproDateTime.SelectedDate = DateTime.Now;

                    // TODO: Betulkan ServiceUnit saat isi form Raspro 
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);

                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(reg.ServiceUnitID);
                    txtServiceUnitName.Text = su.ServiceUnitName;

                    // Ambil work diagnose
                    txtDiagnose.Text = RegistrationInfoMedicDiagnose.DiagnoseSummary(RegistrationNo);

                }
                else
                {
                    var rr = new RegistrationRaspro();
                    if (rr.LoadByPrimaryKey(RegistrationNo, RasproSeqNo))
                    {
                        txtRasproDateTime.SelectedDate = rr.RasproDateTime;

                        var su = new ServiceUnit();
                        su.LoadByPrimaryKey(rr.ServiceUnitID);
                        txtServiceUnitName.Text = su.ServiceUnitName;

                        txtDiagnose.Text = rr.Diagnose;

                        txtAbIndication.Text = rr.AntibioticIndication;
                        optComorbidYes.Checked = rr.IsComorbid ?? false || !string.IsNullOrEmpty(rr.Comorbid);
                        if (!string.IsNullOrEmpty(rr.Comorbid))
                        {
                            foreach (Telerik.Web.UI.ButtonListItem item in cblComorbid.Items)
                            {
                                item.Selected = rr.Comorbid.Contains(item.Value);
                            }
                        }
                        txtOtherComorbid.Text = rr.ComorbidOther;
                        optAbYes.Checked = rr.IsAntibioticIndication ?? false;
                        optSymptomYes.Checked = rr.IsInfectionSymptom ?? false;
                        txtInfectionSymptom.Text = rr.InfectionSymptom;
                        txtRasprajaReason.Text = rr.RasprajaReason;
                        txtNotPpraConsultReason.Text = rr.NotPpraConsultReason;
                        optPpraConsultYes.Checked = (rr.IsPpraConsult ?? false) || string.IsNullOrEmpty(rr.NotPpraConsultReason);
                    }
                }

                //// Infection Selection
                //var inf = new AbRestrictionQuery();
                //inf.Select(inf.AbRestrictionID, inf.ParentID, inf.AbRestrictionName);
                //inf.Where(inf.SRAbRestrictionType == "INF");
                //inf.OrderBy(inf.AbRestrictionName.Ascending);
                //var dtbInf = inf.LoadDataTable();
                //var newRow = dtbInf.NewRow();
                //newRow["AbRestrictionID"] = string.Empty;
                //newRow["AbRestrictionName"] = "No Infection";
                //dtbInf.Rows.InsertAt(newRow, 0);
                //cboAbRestrictionID.DataSource = dtbInf;
                //cboAbRestrictionID.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
            btnOk.Visible = !Mode.Equals("view");
        }

        private void SaveAndClose()
        {
            // NewSeqNo
            var newSeqNo = 1;
            var lastrr = new RegistrationRaspro();
            lastrr.Query.Where(lastrr.Query.RegistrationNo == RegistrationNo);
            lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
            lastrr.Query.es.Top = 1;
            if (lastrr.Query.Load())
                newSeqNo = (lastrr.SeqNo ?? 0) + 1;

            var newRaspraja = new RegistrationRaspro
            {
                SRRaspro = AppConstant.RasproType.Raspraja,
                RegistrationNo = RegistrationNo,
                ParamedicID = ParamedicTeam.DPJP(RegistrationNo).ParamedicID,
                RasproDateTime = DateTime.Now,
                SeqNo = newSeqNo
            };

            // TODO: Betulkan ServiceUnit saat isi form Raspro 
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            newRaspraja.ServiceUnitID = reg.ServiceUnitID;

            // Save Sign
            if (!string.IsNullOrWhiteSpace(hdnImage.Value))
            {
                var imgHelper = new ImageHelper();
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage.Value), new System.Drawing.Size(332, 185));
                newRaspraja.SignImage = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }

            newRaspraja.Diagnose = txtDiagnose.Text;

            // Raspraja Field
            newRaspraja.AntibioticIndication = txtAbIndication.Text;
            newRaspraja.IsComorbid = optComorbidYes.Checked;
            if (optComorbidYes.Checked)
                newRaspraja.Comorbid = string.Join(";", cblComorbid.SelectedValues);
            else
                newRaspraja.Comorbid = String.Empty;

            newRaspraja.ComorbidOther = txtOtherComorbid.Text;
            newRaspraja.IsAntibioticIndication = optAbYes.Checked;
            newRaspraja.IsInfectionSymptom = optSymptomYes.Checked;
            newRaspraja.InfectionSymptom = txtInfectionSymptom.Text;
            newRaspraja.RasprajaReason = txtRasprajaReason.Text;
            newRaspraja.NotPpraConsultReason = txtNotPpraConsultReason.Text;
            newRaspraja.IsPpraConsult = optPpraConsultYes.Checked || string.IsNullOrEmpty(newRaspraja.NotPpraConsultReason);
            newRaspraja.AntibioticLevel = -1; // Base on prev Raspro
            newRaspraja.AbRestrictionID = String.Empty;
            newRaspraja.Save();

            if (optAbYes.Checked)
            {
                foreach (GridDataItem item in grdAntibioticItem.MasterTableView.Items)
                {
                    var itemId = item.GetDataKeyValue("ItemID").ToString();
                    var rasproSeqNo = item.GetDataKeyValue("RasproSeqNo").ToInt();

                    // Set RasprajaSeqNo in RegistrationRasproItem
                    var ritem = new RegistrationRasproItem();
                    if (ritem.LoadByPrimaryKey(RegistrationNo, rasproSeqNo, itemId))
                    {
                        if (item.Selected)
                            ritem.RasprajaSeqNo = newRaspraja.SeqNo;
                        else
                            ritem.str.RasprajaSeqNo = String.Empty;
                        ritem.Save();
                    }


                    // Set RasprajaSeqNo in UddItem
                    var uddItem = new UddItem();
                    uddItem.Query.es.Top = 1;
                    uddItem.Query.Where(uddItem.Query.RegistrationNo == RegistrationNo, uddItem.Query.RasproSeqNo == rasproSeqNo, uddItem.Query.ItemID == itemId);
                    if (uddItem.Query.Load())
                    {
                        if (!string.IsNullOrEmpty(uddItem.ParentNo)) // Racikan
                        {
                            var uddCompound = new UddItem();
                            uddCompound.LoadByPrimaryKey(uddItem.RegistrationNo, uddItem.LocationID, uddItem.ParentNo);

                            if (item.Selected)
                                uddCompound.RasprajaSeqNo = newRaspraja.SeqNo;
                            else
                                uddCompound.str.RasprajaSeqNo = String.Empty;

                            uddCompound.Save();
                        }
                        else
                        {
                            if (item.Selected)
                                uddItem.RasprajaSeqNo = newRaspraja.SeqNo;
                            else
                                uddItem.str.RasprajaSeqNo = String.Empty;

                            uddItem.Save();

                        }
                    }
                }
            }
            // Utk refresh di PrescriptionEntry
            Session["RasproSeqNo"] = newRaspraja.SeqNo;
            Session["SRRaspro"] = newRaspraja.SRRaspro;

            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (eventArgument == "save")
            {
                SaveAndClose();
            }
        }

        protected void grdAntibioticItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Mode.Equals("new"))
            {
                var lastrr = new RegistrationRaspro();
                lastrr.Query.Where(lastrr.Query.RegistrationNo == RegistrationNo, lastrr.Query.SRRaspro.NotIn(AppConstant.RasproType.Raspraja, AppConstant.RasproType.Prophylaxis));
                lastrr.Query.es.Top = 1;
                lastrr.Query.OrderBy(lastrr.Query.SeqNo.Descending);
                if (lastrr.Query.Load())
                {
                    var query = new RegistrationRasproItemQuery("a");
                    var qItem = new ItemQuery("b");
                    query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);

                    var qza = new ZatActiveQuery("za");
                    query.InnerJoin(qza).On(query.ZatActiveID == qza.ZatActiveID);

                    var cm = new ConsumeMethodQuery("cm");
                    query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

                    query.Select
                        (
                          query.RasproSeqNo, query.ItemID, query.SRConsumeMethod, cm.SRConsumeMethodName, cm.SygnaText, query.ConsumeQty, query.SRConsumeUnit, query.ZatActiveID, qItem.ItemName, qza.ZatActiveName, query.StartDateTime
                        );

                    query.Where(query.RegistrationNo == RegistrationNo, query.RasproSeqNo == lastrr.SeqNo, query.StopDateTime.IsNull());
                    query.OrderBy(qza.ZatActiveName.Ascending);
                    var dtb = query.LoadDataTable();
                    dtb.Columns.Add("ConsumeDayNo", typeof(int));

                    // ConsumeDayNo diambil dari Realisasi Medication
                    foreach (DataRow row in dtb.Rows)
                    {
                        row["ConsumeDayNo"] = MedicationReceiveUsed.ConsumedDay(RegistrationNo, row["ItemID"].ToString(),
                            row["SRConsumeMethod"].ToString(), row["ConsumeQty"].ToString(), row["SRConsumeUnit"].ToString());
                    }

                    // Hapus yg < AppParameter.ParameterItem.RasprajaEnableOnDay
                    var antibioticMaxConsumeDay = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticMaxConsumeDay).ToInt();
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["ConsumeDayNo"].ToInt() < antibioticMaxConsumeDay)
                            row.Delete();
                    }
                    dtb.AcceptChanges();

                    grdAntibioticItem.DataSource = dtb;
                }
                else
                    grdAntibioticItem.DataSource = string.Empty;



            }
            else
            {
            }
        }

        protected void grdAntibioticItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && optAbNo.Checked)
            {
                ((e.Item as GridDataItem)["Select"].Controls[0] as CheckBox).Enabled = false;
            }
            if (e.Item is GridHeaderItem && optAbNo.Checked)
            {
                ((e.Item as GridHeaderItem)["Select"].Controls[0] as CheckBox).Enabled = false;
            }
        }

        protected void optAbNo_CheckedChanged(object sender, EventArgs e)
        {
            grdAntibioticItem.Rebind();
        }

        protected void optComorbidNo_CheckedChanged(object sender, EventArgs e)
        {
            cblComorbid.Enabled = optComorbidYes.Checked;
        }
    }
}
