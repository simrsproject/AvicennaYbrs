using System;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Ppra
{
    public partial class GyssensEntry : BasePageDialogEntry
    {
        public override string RegistrationNo => Request.QueryString["regno"];
        public override string PatientID => Request.QueryString["patid"];
        protected int SeqNo => Request.QueryString["seqno"].ToInt();

        RegistrationGyssens _registrationGyssensCurrent;
        private RegistrationGyssens RegistrationGyssensCurrent
        {
            get
            {
                if (_registrationGyssensCurrent == null)
                {
                    var gys = new RegistrationGyssens();
                    if (gys.LoadByPrimaryKey(RegistrationNo, SeqNo))
                        _registrationGyssensCurrent = gys;
                    else
                        _registrationGyssensCurrent = new RegistrationGyssens();
                }
                return _registrationGyssensCurrent;

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Ppra;

            // Program Fiture
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            litSoapInfo.Text = FirstSoap();

            if (!IsPostBack)
            {
                Title = "Gyssens Observation Form";

                var gys = RegistrationGyssensCurrent;

                var item = new Item();
                item.LoadByPrimaryKey(gys.ItemID);

                txtItemName.Text = item.ItemName;

                var za = new ZatActive();
                if (za.LoadByPrimaryKey(gys.ZatActiveID ?? string.Empty))
                    txtZaztActiveName.Text = za.ZatActiveName;

                LoadGyssens(RegistrationNo, SeqNo);
            }
        }

        private void LoadGyssens(string registrationNo, int seqNo)
        {
            // Load datasource
            var qr = new RasproQuery("rpo");
            var gys = new RegistrationGyssensLineQuery("gys");
            qr.LeftJoin(gys).On(qr.RasproLineID == gys.RasproLineID & gys.RegistrationNo == RegistrationNo & gys.SeqNo == seqNo);
            qr.Where(qr.SRRaspro == "GYSSENS");
            qr.OrderBy(qr.SeqNo.Ascending);
            qr.Select(qr, gys.Condition, gys.GyssensCategory,
                "<'' as SelectedValue>",
                "<CONVERT(BIT,CASE WHEN COALESCE(gys.RasproLineID,'')='' THEN 0 ELSE 1 END) as IsEntryVisible>",
                "<CONVERT(BIT,0) as IsStop>",
                "<'' as ParameterInfo>");
            var dtb = qr.LoadDataTable();


            foreach (DataRow row in dtb.Rows)
            {
                row["IsEntryVisible"] = true;
                break;
            }


            // Apply SelectedValue History / Default
            bool isExitLoop = false;
            foreach (DataRow row in dtb.Rows)
            {
                if (row["Condition"] == DBNull.Value) // No record in RegistrationGyssensLine 
                {
                    if (isExitLoop)
                        break;
                }

                // Default Value
                switch (row["SeqNo"].ToInt())
                {
                    case 1: //Data Lengkap
                        {
                            var isSoapExist = !string.IsNullOrEmpty(litSoapInfo.Text);
                            var isRasproExist = (grdGyssensRasproForm.DataSource != null && ((DataTable)grdGyssensRasproForm.DataSource).Rows.Count > 0);// !string.IsNullOrEmpty(txtRasproType.Text);

                            row["ParameterInfo"] = string.Format("<ul><li>SOAP&nbsp;<img src=\"../../../../../Images/Toolbar/{0}\" border=\"0\" alt=\"\" title=\"\" /></li> <li>RASPRO Form&nbsp;<img src=\"../../../../../Images/Toolbar/{1}\" border=\"0\" alt=\"\" title=\"\" /></li> </ul>",
                                isSoapExist ? "post16.png" : "delete16.png",
                                isRasproExist ? "post16.png" : "delete16.png"
                                );

                            if (isSoapExist && isRasproExist)
                                row["SelectedValue"] = "yes";
                            else
                            {
                                row["SelectedValue"] = "no";
                                isExitLoop = true; // Baris 1 dan 2 jika no maka beri flag utk keluar dari loop
                            }

                            SetGyssensValue(dtb, row["RasproLineID"].ToString(), row["SelectedValue"].ToString());
                            break;
                        }
                    case 2: //Indikasi Antibiotik sesuai
                        {
                            var isDiagnoseExist = false; // !string.IsNullOrEmpty(txtDiagnose.Text);
                            row["ParameterInfo"] = string.Format("<ul><li>Diagnose in RASPRO Form&nbsp;<img src=\"../../../../../Images/Toolbar/{0}\" border=\"0\" alt=\"\" title=\"\" /></li></ul>",
                                isDiagnoseExist ? "post16.png" : "delete16.png"
                                );

                            if (isDiagnoseExist)
                                row["SelectedValue"] = "yes";
                            else
                            {
                                row["SelectedValue"] = "no"; // Baris 1 dan 2 jika no maka beri flag utk keluar dari loop
                                isExitLoop = true;
                            }
                            SetGyssensValue(dtb, row["RasproLineID"].ToString(), row["SelectedValue"].ToString());
                            break;
                        }
                    default:
                        break;
                }



                // Override with recorded value
                if (row["Condition"] != DBNull.Value)
                {
                    row["SelectedValue"] = "1".Equals(row["Condition"]) ? "yes" : "no";

                    var isStop = (row["SelectedValue"].Equals("yes") ? row["IsYesContinue"] : row["IsNoContinue"]).ToBoolean() == false;

                    row["IsStop"] = isStop;
                }

            }

            Session["dtbGyssens"] = dtb;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {

        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {

        }
        protected override void OnMenuNewClick()
        {

        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args, true);
        }

        private bool Save(ValidateArgs args, bool isNewRecord)
        {
            using (var trans = new esTransactionScope())
            {
                var ent = RegistrationGyssensCurrent;

                var qr = new RegistrationGyssensLineQuery();
                qr.Where(qr.RegistrationNo == RegistrationNo, qr.SeqNo == SeqNo);
                var coll = new RegistrationGyssensLineCollection();
                coll.Load(qr);

                if (coll.Count > 0)
                    coll.MarkAllAsDeleted();

                var dtb = (DataTable)Session["dtbGyssens"];
                foreach (DataRow row in dtb.Rows)
                {
                    var line = coll.AddNew();
                    line.RegistrationNo = RegistrationNo;
                    line.SeqNo = SeqNo;
                    line.RasproLineID = row["RasproLineID"].ToString();

                    if (row["SelectedValue"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["SelectedValue"].ToString()))
                        line.Condition = "yes".Equals(row["SelectedValue"]) ? "1" : "0";

                    if (!string.IsNullOrWhiteSpace(line.Condition))
                        line.GyssensCategory = (line.Condition.Equals("1") ? row["YesAction"] : row["NoAction"]).ToString();

                    if (true.Equals(row["IsStop"]))
                    {
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(ent.GyssensCreateByUserID))
                {
                    ent.GyssensCreateByUserID = AppSession.UserLogin.UserID;
                    ent.GyssensCreateDateTime = DateTime.Now;
                }

                ent.Save();
                coll.Save();
                trans.Complete();
            }

            return true;
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args, false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return false;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return false;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return false;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        protected void grdList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = (DataTable)Session["dtbGyssens"];
        }

        protected void grdList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "setvalue":
                    {
                        var vals = e.CommandArgument.ToString().Split('_');
                        SetGyssensValue((DataTable)Session["dtbGyssens"], vals[0], vals[1]);

                        grdList.Rebind();
                        break;
                    }
            }
        }

        private void SetGyssensValue(DataTable dtbGyssens, string rasproLineID, string value)
        {
            var isNextEntryVisible = true;
            var isFound = false;
            var isStop = false;
            foreach (DataRow row in dtbGyssens.Rows)
            {
                row["IsEntryVisible"] = isNextEntryVisible;

                if (isFound && (isStop || (false.Equals(row["IsYesContinue"]) || false.Equals(row["IsNoContinue"]))))
                    isNextEntryVisible = false;

                if (isStop) //Reset value setelahnya
                {
                    row["SelectedValue"] = DBNull.Value;
                }

                if (rasproLineID.Equals(row["RasproLineID"]))
                {
                    isFound = true;
                    row["SelectedValue"] = value;
                    isStop = (value.Equals("yes") ? row["IsYesContinue"] : row["IsNoContinue"]).ToBoolean() == false;

                    row["IsStop"] = isStop;

                    if (isStop)
                    {
                        isNextEntryVisible = false;
                    }
                }

            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                var chk = ((System.Web.UI.WebControls.CheckBox)item["IsEntryVisible"].Controls[0]);
                if (!chk.Checked)
                {
                    item.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        private string FirstSoap()
        {
            // First SOAP
            var rim = new RegistrationInfoMedic();
            rim.Query.es.Top = 1;
            rim.Query.OrderBy(rim.Query.RegistrationInfoMedicID.Descending);
            rim.Query.Where(rim.Query.RegistrationNo == RegistrationNo, rim.Query.SRMedicalNotesInputType == "SOAP");
            if (rim.Query.Load())
            {
                var sbNote = new StringBuilder();
                var info1 = ReplaceWitBreakLineHTML(rim.Info1);
                sbNote.AppendLine("<table style=\"width:100%\">");
                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "S", info1, 10);
                sbNote.AppendLine();

                var info2 = ReplaceWitBreakLineHTML(rim.Info2);
                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "O", info2, 10);
                sbNote.AppendLine();

                var info3 = ReplaceWitBreakLineHTML(rim.Info3);
                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold;width:{2}px; padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "A", info3, 10);
                sbNote.AppendLine();

                // Planning
                string planning;
                // Dari asesmen tambah hist resepnya di Planning
                if (!string.IsNullOrEmpty(rim.PrescriptionCurrentDay))
                    planning = string.Format("{0}<br/><br/>{1}", FormatToHtml(rim.Info4),
                        FormatToHtml(rim.PrescriptionCurrentDay));
                else
                    planning = FormatToHtml(rim.Info4);

                sbNote.AppendFormat(
                    "<tr><td class='label' valign='top' style='font-weight: bold; width:{2}px;padding-left:2px'>{0}:</td><td valign='top'>{1}</td></tr>",
                    "P", planning, 10);
                sbNote.AppendLine("</table>");
                return sbNote.ToString();
            }
            return string.Empty;
        }
        private static string FormatToHtml(object value)
        {
            return Regex.Replace(value == null || value == DBNull.Value ? String.Empty : value.ToString(), @"\r\n?|\n", "<br />");
        }

        private static string ReplaceWitBreakLineHTML(string text)
        {
            return Regex.Replace(text, @"\r\n?|\n", "<br />");
        }

        protected void grdGyssensRasproForm_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGyssensRasproForm.DataSource = RasproFormHists();
        }
        private DataTable RasproFormHists()
        {
            var gys = RegistrationGyssensCurrent;
            if (string.IsNullOrWhiteSpace(gys.RegistrationNo))
                return null;

            var rab = new RegistrationRasproItemQuery("rab");

            var rr = new RegistrationRasproQuery("rr");
            rab.InnerJoin(rr).On(rr.RegistrationNo == rab.RegistrationNo & rr.SeqNo == rab.RasproSeqNo);

            var stdi = new AppStandardReferenceItemQuery("stdi");
            rab.InnerJoin(stdi).On(rr.SRRaspro == stdi.ItemID & stdi.StandardReferenceID == "RASPRO");


            var abr = new AbRestrictionQuery("abr");
            rab.LeftJoin(abr).On(rr.AbRestrictionID == abr.AbRestrictionID);

            var par = new ParamedicQuery("p");
            rab.LeftJoin(par).On(rr.ParamedicID == par.ParamedicID);

            rab.Where(rab.RegistrationNo == RegistrationNo, rab.ItemID == gys.ItemID, rab.ZatActiveID == gys.ZatActiveID,
                rab.SRConsumeMethod == gys.SRConsumeMethod, rab.ConsumeQty == gys.ConsumeQty, rab.SRConsumeUnit == gys.SRConsumeUnit);

            if (gys.PrescriptionDateEnd > gys.PrescriptionDateStart)
                rab.Where(rr.RegistrationNo == RegistrationNo, rr.Or(rr.RasproDateTime <= gys.PrescriptionDateStart, rr.RasproDateTime <= gys.PrescriptionDateEnd));
            else
                rab.Where(rr.RegistrationNo == RegistrationNo, rr.RasproDateTime <= gys.PrescriptionDateStart);


            rab.Select(rr, rab.StartDateTime, rab.StopDateTime, stdi.ItemName.As("RasproName"),
                string.Format("<'{0}' as PatientID>", PatientID),
                string.Format("<'{0}' as RegistrationNo>", RegistrationNo),
                abr.AbRestrictionName.Coalesce("''").As("FocusInfection"), par.ParamedicName);
            rab.OrderBy(rr.SeqNo.Ascending);
            var dtb = rab.LoadDataTable();

            AddActionColumn(dtb);

            return dtb;
        }


        private DataTable RasproFormOtherHists()
        {
            var gys = RegistrationGyssensCurrent;
            if (string.IsNullOrWhiteSpace(gys.RegistrationNo))
                return null;

            var rr = new RegistrationRasproQuery("rr");

            var stdi = new AppStandardReferenceItemQuery("stdi");
            rr.InnerJoin(stdi).On(rr.SRRaspro == stdi.ItemID & stdi.StandardReferenceID == "RASPRO");


            var abr = new AbRestrictionQuery("abr");
            rr.LeftJoin(abr).On(rr.AbRestrictionID == abr.AbRestrictionID);

            var par = new ParamedicQuery("p");
            rr.LeftJoin(par).On(rr.ParamedicID == par.ParamedicID);


            if (gys.PrescriptionDateEnd > gys.PrescriptionDateStart)
                rr.Where(rr.RegistrationNo == RegistrationNo, rr.Or(rr.RasproDateTime <= gys.PrescriptionDateStart, rr.RasproDateTime <= gys.PrescriptionDateEnd));
            else
                rr.Where(rr.RegistrationNo == RegistrationNo, rr.RasproDateTime <= gys.PrescriptionDateStart);


            rr.Select(rr, stdi.ItemName.As("RasproName"),
                string.Format("<'{0}' as PatientID>", PatientID),
                string.Format("<'{0}' as RegistrationNo>", RegistrationNo),
                abr.AbRestrictionName.Coalesce("''").As("FocusInfection"), par.ParamedicName);
            rr.OrderBy(rr.SeqNo.Ascending);
            var dtb = rr.LoadDataTable();

            AddActionColumn(dtb);

            return dtb;
        }

        private static void AddActionColumn(DataTable dtb)
        {
            dtb.Columns.Add("Action", typeof(string));
            // Ambil judul AB stratifikasi
            foreach (DataRow row in dtb.Rows)
            {
                if (row["SRRaspro"].Equals("RASAL") || row["SRRaspro"].Equals("RASLAN"))
                {
                    var rrl = new RegistrationRasproLine();
                    rrl.Query.Where(rrl.Query.RegistrationNo == row["RegistrationNo"].ToString(), rrl.Query.SeqNo == row["SeqNo"].ToString());
                    rrl.Query.es.Top = 1;
                    rrl.Query.OrderBy(rrl.Query.RasproLineID.Descending);
                    if (rrl.Query.Load())
                    {
                        if (row["ActionNo"] == DBNull.Value)
                        {
                            var ras = new Raspro();
                            ras.LoadByPrimaryKey(rrl.RasproLineID);

                            row["Action"] = rrl.Condition == "1" ? ras.YesActionDescription : ras.NoActionDescription;
                        }
                        else
                        {
                            var raa = new RasproAction();
                            raa.LoadByPrimaryKey(rrl.RasproLineID, rrl.Condition, row["ActionNo"].ToInt());
                            row["Action"] = raa.ActionDescription;
                        }

                    }
                }

            }
        }

        protected void grdGyssensRasproFormOther_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdGyssensRasproFormOther.DataSource = RasproFormHists();
        }
    }
}
