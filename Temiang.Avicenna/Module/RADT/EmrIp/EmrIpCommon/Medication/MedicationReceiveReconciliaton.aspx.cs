using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class MedicationReceiveReconciliaton : BasePageDialog
    {

        private Registration _regCurr;
        private Registration RegistrationCurrent
        {
            get
            {
                if (_regCurr == null)
                {
                    _regCurr = new Registration();
                    _regCurr.LoadByPrimaryKey(RegistrationNo);
                }
                return _regCurr;
            }
        }
        public string RegistrationNo => Request.QueryString["regno"];
        protected string FromRegistrationNo
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["fregno"]))
                {
                    if (ViewState["fregno"] == null || string.IsNullOrEmpty(ViewState["fregno"].ToString()))
                    {
                        var reg = new Registration();
                        reg.LoadByPrimaryKey(RegistrationNo);
                        ViewState["fregno"] = reg.FromRegistrationNo;
                    }

                    return ViewState["fregno"].ToString();
                }
                else
                {
                    return Request.QueryString["fregno"];
                }
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (AppConstant.Program.PharmaceuticalCare.Equals(Request.QueryString["prgid"]))
                ProgramID = AppConstant.Program.PharmaceuticalCare;
            else
                ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                lnkDrugFromPatient.Visible = false;
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    var title = string.Empty;
                    switch (Request.QueryString["rectype"])
                    {
                        case "adm":
                            title = "Admission Reconciliation";
                            lnkDrugFromPatient.Visible = true;
                            break;
                        case "trf":
                            title = "Transfer Reconciliation";
                            break;
                        case "dcg":
                            title = "Discharge Reconciliation";
                            break;
                    }

                    this.Title = string.Format("{0} : {1}  (MRN: {2})", title, pat.PatientName, pat.MedicalNo);
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCancel.Text = "Close";
            ButtonOk.Visible = false;
        }

        #region MedicationReceive
        protected void grdMedicationReceive_ItemCommand(object source, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                //case "AppropriateAll":
                //    {
                //        var dtb = MedicationReceiveDataTable(RegistrationNo, FromRegistrationNo);
                //        foreach (DataRow row in dtb.Rows)
                //        {
                //            Appropriate(row["MedicationReceiveNo"].ToInt(), row);
                //        }
                //        grdMedicationReceive.Rebind();

                //        break;
                //    }
                //case "Appropriate":
                //    {
                //        var medicationReceiveNo = Convert.ToDecimal(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MedicationReceiveNo"]).ToInt();
                //        Appropriate(medicationReceiveNo, null);
                //        grdMedicationReceive.Rebind();
                //        break;
                //    }
                //case "Prescribed":
                //    {
                //        TogglePrescribed(e.CommandArgument.ToInt(), null, null);
                //        grdMedicationReceive.Rebind();
                //        break;
                //    }
                //case "PrescribedAll":
                //case "NotPrescribedAll":
                //    {
                //        var isPrescribed = e.CommandName == "PrescribedAll";
                //        var dtb = MedicationReceiveDataTable(RegistrationNo, FromRegistrationNo);
                //        foreach (DataRow row in dtb.Rows)
                //        {
                //            TogglePrescribed(row["MedicationReceiveNo"].ToInt(), row, isPrescribed);
                //        }
                //        grdMedicationReceive.Rebind();

                //        break;
                //    }
                case "Approve":
                case "UnApprove":
                    {
                        // ---- Hanya untuk Recon Admisi ----///
                        if ("adm".Equals(Request.QueryString["rectype"]))
                        {
                            var isApprove = e.CommandName.Equals("Approve");
                            var medicationReceiveNo = Convert.ToDecimal(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MedicationReceiveNo"]).ToInt();
                            var med = new MedicationReceive();
                            if (med.LoadByPrimaryKey(medicationReceiveNo))
                            {
                                med.IsApproveAdm = isApprove;
                                med.ApproveAdmByUserID = AppSession.UserLogin.UserID;
                                med.ApproveAdmDateTime = DateTime.Now;

                                // Continue stat
                                med.IsContinue = isApprove && !"ST".Equals(med.ReconStatusAdm);

                                //TODO: cari tahu ini IsClosed buat apa
                                //med.IsClosed = !med.IsContinue; 

                                med.Save();

                                // Tambah history stop
                                if ("ST".Equals(med.ReconStatusAdm)) // Stop
                                {
                                    var stat = new MedicationReceiveStatus();
                                    stat.MedicationReceiveNo = medicationReceiveNo;
                                    stat.StatusDateTime = DateTime.Now;
                                    stat.IsMedicationStop = !isApprove;
                                    stat.MedicationReason = String.Concat("Recon admision confirm", (isApprove ? String.Empty : " un"), "approve by paramedic");
                                    stat.Save();
                                }

                                grdMedicationReceive.Rebind();
                            }
                        }
                        break;
                    }
            }


        }

        private void Appropriate(int medicationReceiveNo, DataRow rowDtGrid)
        {
            var md = new MedicationReceive();
            if (md.LoadByPrimaryKey(medicationReceiveNo))
            {
                var isAppropriate = true;

                // Update pasien position
                md.ServiceUnitID = RegistrationCurrent.ServiceUnitID;
                md.RoomID = RegistrationCurrent.RoomID;
                md.BedID = RegistrationCurrent.BedID;

                // Update status
                switch (Request.QueryString["rectype"])
                {
                    case "adm":
                        md.IsAdmissionAppropriate = isAppropriate;
                        md.AdmissionAppropriateDateTime = DateTime.Now;
                        if (rowDtGrid != null)
                        {
                            rowDtGrid["IsAdmissionAppropriate"] = md.IsAdmissionAppropriate;
                            rowDtGrid["IsAppropriate"] = md.IsAdmissionAppropriate;
                            rowDtGrid["AdmissionAppropriateDateTime"] = md.AdmissionAppropriateDateTime;
                        }
                        break;
                    case "trf":
                        md.IsTransferAppropriate = isAppropriate;
                        md.TransferAppropriateDateTime = DateTime.Now;
                        if (rowDtGrid != null)
                        {
                            rowDtGrid["IsTransferAppropriate"] = md.IsTransferAppropriate;
                            rowDtGrid["IsAppropriate"] = md.IsTransferAppropriate;
                            rowDtGrid["TransferAppropriateDateTime"] = md.TransferAppropriateDateTime;
                        }
                        break;
                    case "dcg":
                        md.IsDischargeAppropriate = isAppropriate;
                        md.DischargeAppropriateDateTime = DateTime.Now;
                        if (rowDtGrid != null)
                        {
                            rowDtGrid["IsDischargeAppropriate"] = md.IsDischargeAppropriate;
                            rowDtGrid["IsAppropriate"] = md.IsDischargeAppropriate;
                            rowDtGrid["DischargeAppropriateDateTime"] = md.DischargeAppropriateDateTime;
                        }
                        break;
                }

                md.Save();


                // Hapus info Not Appropriate reason kalau ada
                var stat = new MedicationReceiveAppropriate();
                if (stat.LoadByPrimaryKey(medicationReceiveNo, Request.QueryString["rectype"].ToUpper()))
                {
                    stat.MarkAsDeleted();
                    stat.Save();
                }

                grdMedicationReceive.Rebind();
            }
        }

        private void TogglePrescribed(int medicationReceiveNo, DataRow rowDtGrid, bool? isPrescribed)
        {
            var nmd = new MedicationReceive();
            if (nmd.LoadByPrimaryKey(medicationReceiveNo))
            {
                switch (Request.QueryString["rectype"])
                {
                    case "adm":
                        nmd.IsAdmissionPresc = isPrescribed ?? !nmd.IsAdmissionPresc;
                        if (rowDtGrid != null)
                        {
                            rowDtGrid["IsAdmissionPresc"] = nmd.IsAdmissionPresc;
                            rowDtGrid["IsPrescribed"] = nmd.IsAdmissionPresc;
                        }
                        break;
                    case "trf":
                        nmd.IsTransferPresc = isPrescribed ?? !nmd.IsTransferPresc;
                        if (rowDtGrid != null)
                        {
                            rowDtGrid["IsTransferPresc"] = nmd.IsTransferPresc;
                            rowDtGrid["IsPrescribed"] = nmd.IsTransferPresc;
                        }
                        break;
                    case "dcg":
                        nmd.IsDischargePresc = isPrescribed ?? !nmd.IsTransferPresc;
                        if (rowDtGrid != null)
                        {
                            rowDtGrid["IsDischargePresc"] = nmd.IsDischargePresc;
                            rowDtGrid["IsPrescribed"] = nmd.IsDischargePresc;
                        }
                        break;
                }
                nmd.Save();
            }
        }
        protected string DisplayMenuAppropriateAll()
        {
            var isHasAppropriated = false;
            var dtb = MedicationReceiveDataTable(RegistrationNo, FromRegistrationNo);
            foreach (DataRow row in dtb.Rows)
            {
                switch (Request.QueryString["rectype"])
                {
                    case "adm":

                        if (row["AdmissionAppropriateDateTime"] != DBNull.Value)
                        {
                            isHasAppropriated = true;
                            break;
                        }
                        break;
                    case "trf":
                        if (row["TransferAppropriateDateTime"] != DBNull.Value)
                        {
                            isHasAppropriated = true;
                            break;
                        }
                        break;
                    case "dcg":
                        if (row["DischargeAppropriateDateTime"] != DBNull.Value)
                        {
                            isHasAppropriated = true;
                            break;
                        }
                        break;
                }

            }

            return isHasAppropriated ? "none" : "display";
        }
        protected void grdMedicationReceive_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdMedicationReceive.DataSource = MedicationReceiveDataTable(RegistrationNo, FromRegistrationNo);
        }


        private DataTable MedicationReceiveDataTable(string registrationNo, string fromRegistrationNo)
        {
            var query = new MedicationReceiveQuery("a");
            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var patrec = new MedicationReceiveFromPatientQuery("b");
            if (Request.QueryString["rectype"] == "adm") // hanya obat dari luar RS saja yg perlu direcon Admisi
                query.InnerJoin(patrec).On(query.MedicationReceiveNo == patrec.MedicationReceiveNo);
            else
                query.LeftJoin(patrec).On(query.MedicationReceiveNo == patrec.MedicationReceiveNo);

            query.Select(query, patrec.Condition, patrec.ExpireDate, patrec.ApprovedByParamedicID, patrec.LastConsumeDateTime, cm.SRConsumeMethodName);

            var appr = new MedicationReceiveAppropriateQuery("appr");
            query.LeftJoin(appr).On(query.MedicationReceiveNo == appr.MedicationReceiveNo &&
                                    appr.AppropriateType == Request.QueryString["rectype"].ToUpper());

            var newcm = new ConsumeMethodQuery("ncm");


            switch (Request.QueryString["rectype"])
            {
                case "adm": // Recon obat patient baru masuk
                    query.LeftJoin(newcm).On(query.SRConsumeMethodAdm == newcm.SRConsumeMethod);
                    query.Select(query.IsAdmissionAppropriate.As("IsAppropriate"), query.IsAdmissionPresc.As("IsPrescribed"), "<CONVERT(bit,1) as IsMenuReconVisible>"); // Semua haru di recon
                    if (!chkIncludeReconciled.Checked ?? false)
                        query.Where(query.ReconStatusAdm.IsNull());
                    break;
                case "trf": // Recon obat patient pindah ruangan
                    query.LeftJoin(newcm).On(query.SRConsumeMethodTrf == newcm.SRConsumeMethod);
                    query.Select(query.IsTransferAppropriate.As("IsAppropriate"), query.IsTransferPresc.As("IsPrescribed"), "<CONVERT(bit,1) as IsMenuReconVisible>");

                    if (!chkIncludeReconciled.Checked ?? false)
                        query.Where(query.ReconStatusTrf.IsNull());

                    // Hanya yg sudah di recon admisi dan di confirm oleh dokter
                    query.Where(query.IsApproveAdm == true);
                    break;
                case "dcg": // Recon obat patient pulang
                    query.LeftJoin(newcm).On(query.SRConsumeMethodDis == newcm.SRConsumeMethod);
                    query.Select(query.IsDischargeAppropriate.As("IsAppropriate"), query.IsDischargePresc.As("IsPrescribed"), "<CONVERT(bit,1) as IsMenuReconVisible>");

                    if (!chkIncludeReconciled.Checked ?? false)
                        query.Where(query.ReconStatusDis.IsNull());

                    // Hanya yg sudah di recon admisi dan di confirm oleh dokter
                    query.Where(query.IsApproveAdm == true);
                    break;
            }

            //query.Select(
            //    "<COALESCE(stdi.ItemName,'') + COALESCE(appr.MedicationReason,'') as MedicationReason>"); // Stop Information

            query.Select(newcm.SRConsumeMethodName.As("NewConsumeMethodName"));
            query.Where(query.Or(query.RegistrationNo == registrationNo, query.RegistrationNo == fromRegistrationNo));
            query.Where(query.RegistrationNo.In(MergeRegistrations));

            if (chkIncludeIsClosed.Checked == false)
                //query.Where(query.Or(query.IsClosed.IsNull(), query.IsClosed == false));
                query.Where(query.Or(query.IsContinue.IsNull(), query.IsContinue == true));

            if (chkIncludeEmptyBal.Checked == false)
                query.Where(query.BalanceRealQty > 0);

            query.OrderBy(query.MedicationReceiveNo.Descending);
            var dtb = query.LoadDataTable();
            dtb.Columns.Add("ReconStatusID", typeof(string));
            dtb.Columns.Add("ReconStatus", typeof(string));
            foreach (DataRow row in dtb.Rows)
            {
                var fieldStatName = string.Empty;
                switch (Request.QueryString["rectype"])
                {
                    case "adm": // Recon obat patient baru masuk
                        fieldStatName = "ReconStatusAdm";
                        row["IsMenuReconVisible"] = false.Equals(row["IsVoid"] ?? false) && row["IsApproveAdm"] == DBNull.Value; // Bisa diedit selama belum diconfirm approve / unapprove oleh dokter
                        break;
                    case "trf": // Recon obat patient pindah ruangan
                        fieldStatName = "ReconStatusTrf";
                        break;
                    case "dcg": // Recon obat patient pulang
                        fieldStatName = "ReconStatusDis";
                        break;
                }

                if (row[fieldStatName] != DBNull.Value)
                {
                    row["ReconStatus"] = ReconStatus(row[fieldStatName].ToString());
                    row["ReconStatusID"] = row[fieldStatName];
                }
            }
            return dtb;
        }

        private string ReconStatus(string statCode)
        {
            switch (statCode)
            {
                case "CN":
                    return "Continue with consume method not changed";
                case "CC":
                    return "Continue with consume method changed";
                case "ST":
                    return "Stop";
                case "NT":
                    return "New Therapies";
            }
            return string.Empty;
        }
        protected void grdMedicationReceive_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            //"Continue with consume method not changed", "CN"
            //"Continue with consume method changed", "CC"
            //"Stop", "ST"
            //"New Therapies", "NT"

            if (e.Item is GridDataItem)
            {
                var color = "black";
                GridDataItem item = (GridDataItem)e.Item;
                if (true.Equals(item.GetDataKeyValue("IsVoid")))
                {
                    item.Style.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    color = "gray";
                }
                else
                {
                    switch (item.GetDataKeyValue("ReconStatusID"))
                    {
                        case "CC":
                            color = "blue";
                            break;
                        case "CN":
                            color = "green";
                            break;
                        case "ST":
                            color = "gray";
                            break;
                        case "NT":
                            color = "orange";
                            break;
                    }
                }
                item.Style.Add(HtmlTextWriterStyle.Color, color);
            }
        }
        #endregion
    }
}
