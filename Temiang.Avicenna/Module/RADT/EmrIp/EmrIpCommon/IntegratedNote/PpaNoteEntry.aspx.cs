using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Collections;
using System.Text;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// PPA Notes Entry
    /// </summary>
    /// Create by: Teguh S.
    /// 
    /// Modif History:
    /// --------------------------
    /// 2023-09-26 Handono
    /// - Add deadline edit & add after discharge date (IPR), reg date (non IPR) 
    /// - Open deadline edit & add (Open MR)
    /// - Information deadline edit & add
    /// 
    public partial class PpaNoteEntry : BasePageDialog
    {
        private bool _isEditable = true;
        private bool _isAddable = true;

        #region Properties
        protected bool IsEditable
        {
            get { return _isEditable; }
        }
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }

        public string NsID
        {
            get
            {
                return Request.QueryString["rimid"];
            }
        }
        public DataTable ImplementationCountPerIntervention
        {
            get
            {
                if (ViewState["ImplementationCountPerIntervention"] != null)
                {
                    return (DataTable)ViewState["ImplementationCountPerIntervention"];
                }

                return null;
            }
            set
            {
                ViewState["ImplementationCountPerIntervention"] = value;
            }
        }

        private string OldData
        {
            get
            {
                return (Session["OldTemplateData"] ?? string.Empty).ToString();
            }
            set
            {
                Session["OldTemplateData"] = value;
            }
        }
        private string InputType
        {
            get
            {
                return (Session["impInputType"] ?? string.Empty).ToString();
            }
            set
            {
                Session["impInputType"] = value;
            }
        }
        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);
            //switch (eventArgument)
            //{

            //}
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            if (!IsPostBack)
            {
                ButtonCancel.Visible = false;
                //ButtonOk.Text = "Close";
                ButtonOk.Visible = false; // Request RSUD Tarakan karena user sering terkecoh untuk klik close sebelum klik tombol save (Handono 22-12-04)
                if (AppSession.Parameter.HealthcareInitial == "GRHA")
                {
                    tbInputType.FindItemByValue("HandOver").Visible = true;
                    tbInputType.FindItemByValue("HandOver").Enabled = true;
                    tbInputType.FindItemByValue("HandOver").Text = "Handover Patient";
                    tbInputType.FindItemByValue("HandOver").ImageUrl = "~/Images/Toolbar/insert16.png";
                }

                InitEntry();
                LoadTemplateRespond();
                //LoadParamedic(); 
                if (!string.IsNullOrEmpty(NsID))
                {
                    var nsName = LoadEntity(NsID);
                    SelectInputType(nsName);
                }
                else
                {
                    SelectInputType("");
                }
                grdCustomNic.Rebind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateStatusAddEditable();
        }

        private void ShowInformationStatus(string messageText)
        {
            lblInfoStatus.Text = messageText;
            pnlInfoStatus.Visible = true;
        }

        private void UpdateStatusAddEditable()
        {
            // Reset
            pnlInfoStatus.Visible = false;
            _isEditable = true;
            _isAddable = true;
            btnSave.Enabled = true;
            tbInputType.Items[0].Enabled = true;
            tbInputType.Items[1].Enabled = true;
            tbInputType.Items[2].Enabled = true;
            tbInputType.Items[3].Enabled = true;

            // Update stat
            var args = new ValidateArgs();
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            var msg = string.Empty;

            // Check IsOpenMR & deadline Edit  (Handono 20230925)
            MedicalRecordEditableValidate(args, reg);
            if (args.IsCancel)
            {
                msg = args.MessageText;
                btnSave.Enabled = false;
                _isEditable = false;
            }

            // Check IsOpenMR & deadline Add (Handono 20230925)
            args = new ValidateArgs();
            MedicalRecordAddableValidate(args, reg);
            if (args.IsCancel)
            {
                msg = string.IsNullOrEmpty(msg) ? args.MessageText : string.Concat(msg, "<br />", args.MessageText);

                tbInputType.Items[0].Enabled = false;
                tbInputType.Items[1].Enabled = false;
                tbInputType.Items[2].Enabled = false;
                tbInputType.Items[3].Enabled = false;

                _isAddable = false;
            }

            if (!string.IsNullOrEmpty(msg))
            {
                ShowInformationStatus(msg);
                return;
            }

            // Check Untuk DPJP harus sudah ada asesmen dulu (Handono 230822)
            if (IsUserParamedicDpjp() && AppSession.Parameter.IsEmrPhysicianAssessmentMandatory)
            {
                var pass = new PatientAssessment();
                var passq = new PatientAssessmentQuery();
                passq.Select(passq.RegistrationNo);
                passq.Where(passq.RegistrationNo == RegistrationNo,
                    passq.Or(passq.IsDeleted.IsNull(), passq.IsDeleted == false));
                passq.es.Top = 1;

                if (!pass.Load(passq))
                {
                    ShowInformationStatus("Add PPA Notes not allowed before assessment, please create assessment first");
                    btnSave.Enabled = true;
                    tbInputType.Items[0].Enabled = false;
                    tbInputType.Items[1].Enabled = false;
                    tbInputType.Items[2].Enabled = false;
                    tbInputType.Items[3].Enabled = false;
                    return;
                }
            }
        }
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Empty;
        }
        public override bool OnButtonOkClicked()
        {
            return true;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (InputType == "S B A R" || InputType == "S O A P" || InputType == "ADIME" || InputType == "Handover Patient")
            {
                if ((txtS.Text + txtO.Text + txtA.Text + txtP.Text + txtInfo5.Text) == string.Empty)
                {
                    ShowInformationHeader("Can not save empty data");
                    args.IsValid = false;
                }
            }
            if (InputType == "S B A R")
            {
                if (AppParameter.IsYes(AppParameter.ParameterItem.IsValidateParamedicSBAR))
                {
                    // validate paramedic
                    if (string.IsNullOrEmpty(cboParamedic.SelectedValue))
                    {
                        ShowInformationHeader("Paramedic required");
                        args.IsValid = false;
                    }
                }

            }
        }

        #region ...
        private void InitEntry()
        {
            HideInformationHeader();

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(reg.PatientID))
                {
                    this.Title = "Nursing notes of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                var hd = new NursingTransHDCollection();
                hd.Query.Where(hd.Query.RegistrationNo == RegistrationNo);
                hd.LoadAll();
                if (hd.Count > 0)
                {
                    hfTransNo.Value = hd[0].TransactionNo;
                }
                else
                {
                    hfTransNo.Value = string.Empty;
                }
            }

            hfIDImplementation.Value = string.Empty;
            hfRelatedPHRNo.Value = string.Empty;
            OldData = string.Empty;
            ImplementationCountPerIntervention = null;
            txtDateTimeImplementation.SelectedDate = DateTime.Now;
            txtDateTimeImplementation.Enabled = AppSession.Parameter.IsAllowEditDateTimeImplementation;
            txtImplementationText.Text = string.Empty;
            txtCustomRespond.Text = string.Empty;

            cboTemplate.SelectedValue = "0";
            cboTemplate.Text = "";
            cboTemplate_SelectedIndexChanged(cboTemplate, new RadComboBoxSelectedIndexChangedEventArgs(cboTemplate.SelectedValue, "xxx", cboTemplate.SelectedValue, "xxx"));

            txtS.Text = string.Empty;
            txtO.Text = string.Empty;
            txtA.Text = string.Empty;
            txtP.Text = string.Empty;
            txtPpaInstruction.Text = string.Empty;
            SelectCboPar(string.Empty);
            txtInfo5.Text = string.Empty;
            txtReceiveBy.Text = string.Empty;
            txtSubmitBy.Text = string.Empty;
            txtReceiveBySbar.Text = string.Empty;

            SelectInputType(hfLastIType.Value);
        }
        private void SelectInputType(string iType)
        {
            InputType = iType;
            txtImplementationText.Text = iType;
            hfLastIType.Value = iType;

            trImp.Visible = false;
            trRespCbo.Visible = false;
            trRespTxt.Visible = false;
            trInfo1.Visible = false;
            trInfo2.Visible = false;
            trInfo3.Visible = false;
            trInfo4.Visible = false;
            trtPpaInstruction.Visible = false;
            trPar.Visible = false;
            trInfo5.Visible = false;
            trReceiveBy.Visible = false;
            trSubmitBy.Visible = false;
            fsHandOver.Visible = false;
            trReceiveBy2.Visible = false;

            if (Equals(iType, "S B A R"))
            {
                trInfo1.Visible = true;
                trInfo2.Visible = true;
                trInfo3.Visible = true;
                trInfo4.Visible = true;
                trtPpaInstruction.Visible = true;
                trInfo5.Visible = true;
                trReceiveBy2.Visible = true;
                trPar.Visible = true;

                lblS.Text = "Situation (S)"; lblO.Text = "Background (B)";
                lblA.Text = "Assessment (A)"; lblP.Text = "Recommendation (R)";
                lblPpaInstruction.Text = "Instruction / Advice (I)";
                lblInfo5.Text = "Write, Read and Reconfirm Instructions (TBAK)";
                lblRB2.Text = "Receive By";
            }
            else if (Equals(iType, "S O A P"))
            {
                trInfo1.Visible = true;
                trInfo2.Visible = true;
                trInfo3.Visible = true;
                trInfo4.Visible = true;
                trtPpaInstruction.Visible = true;
                trInfo5.Visible = true;
                trPar.Visible = true;
                trReceiveBy.Visible = true;
                trSubmitBy.Visible = true;
                fsHandOver.Visible = true;

                lblS.Text = "Subjective (S)"; lblO.Text = "Objective (O)";
                lblA.Text = "Assessment / <br />Diagnosis (A)";
                lblP.Text = "Planning (P)";
                lblPpaInstruction.Text = "PPA Instruction / Intervention (I)";
                lblInfo5.Text = "Evalution (E)";
            }
            else if (Equals(iType, "ADIME"))
            {
                trInfo1.Visible = true;
                trInfo2.Visible = true;
                trInfo3.Visible = true;
                trInfo4.Visible = true;
                trInfo5.Visible = true;

                lblS.Text = "Assessment (A)"; lblO.Text = "Diagnosis (D)";
                lblA.Text = "Intervention (I)"; lblP.Text = "Monitoring (M)";
                lblInfo5.Text = "Evaluation (E)";
            }
            else if (Equals(iType, "Handover Patient"))
            {
                trInfo1.Visible = true;
                trInfo2.Visible = true;
                trInfo3.Visible = true;
                trInfo4.Visible = true;
                trtPpaInstruction.Visible = true;
                trPar.Visible = true;
                trReceiveBy.Visible = true;
                trSubmitBy.Visible = true;
                fsHandOver.Visible = true;

                lblS.Text = "Subjective (S)"; lblO.Text = "Objective (O)";
                lblA.Text = "Assessment / <br />Diagnosis (A)";
                lblP.Text = "Planning (P)";
            }
            else
            {
                hfLastIType.Value = string.Empty;

                trImp.Visible = true;
                trRespCbo.Visible = true;
                trRespTxt.Visible = true;

                var t = new NursingDiagnosa();
                if (t.LoadByPrimaryKey(hfNicCode.Value))
                {
                    if ((t.TemplateID ?? 0) != 0)
                    {
                        //cboTemplate.SelectedValue = t.TemplateID.Value.ToString();
                        cboTemplate_SelectedIndexChanged(
                            cboTemplate, new RadComboBoxSelectedIndexChangedEventArgs(
                                t.TemplateID.Value.ToString(), "",
                                t.TemplateID.Value.ToString(), ""
                                ));
                    }
                    txtCustomRespond.Text = t.RespondTemplate;
                }
            }
        }
        private NursingTransHD SetHeader()
        {
            NursingTransHD hd;
            var hdColl = new NursingTransHDCollection();
            hdColl.Query.Where(hdColl.Query.TransactionNo == hfTransNo.Value,
                    hdColl.Query.RegistrationNo == RegistrationNo);
            hdColl.LoadAll();

            if (hdColl.Count == 0)
            {
                //hd = new NursingTransHD();
                hd = hdColl.AddNew();
                //hd.AddNew();
                PopulateNewNo(true);


                hd.TransactionNo = hfTransNo.Value;
                hd.NursingTransDateTime = DateTime.Now;// txtNursingTransDateTime.SelectedDate;
                hd.RegistrationNo = RegistrationNo;
                //Last Update Status

                hd.CreateByUserID = AppSession.UserLogin.UserID;
                hd.CreateDateTime = DateTime.Now;
                hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hd.LastUpdateDateTime = DateTime.Now;
            }
            else
            {
                hd = hdColl[0];

                hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hd.LastUpdateDateTime = DateTime.Now;


            }
            hdColl.Save();
            return hd;
        }

        private void PopulateNewNo(bool TobeSave)
        {
            AppAutoNumberLast _autoNumberND;
            _autoNumberND = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.NursingCareNo);
            hfTransNo.Value = _autoNumberND.LastCompleteNumber;
            if (TobeSave)
            {
                // save autonumber immediately to decrease time gap between create and save
                _autoNumberND.Save();
            }
        }

        protected string GetFullImplementationNameFormatted(string nursingDiagnosaName,
            string S, string O, string A, string P, object PpaInstruction, object info5, object submitBy, object receiveBy)
        {

            if (Equals(nursingDiagnosaName, "S B A R"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "B: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "R: " + P + System.Environment.NewLine +
                    "I: " + (PpaInstruction ?? string.Empty) + System.Environment.NewLine +
                    "TBAK: " + (info5 ?? string.Empty) + System.Environment.NewLine +
                    "ReceiveBy: " + receiveBy
                    );
            }

            if (Equals(nursingDiagnosaName, "S O A P"))
            {
                var str = "S: " + S + System.Environment.NewLine +
                    "O: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + P;
                if (!string.IsNullOrEmpty(PpaInstruction.ToString()))
                {
                    str += System.Environment.NewLine + "I:" + PpaInstruction.ToString();
                }
                if (!string.IsNullOrEmpty(info5.ToString()))
                {
                    str += System.Environment.NewLine + "E:" + info5.ToString();
                }
                if (!string.IsNullOrEmpty(submitBy.ToString()) || !string.IsNullOrEmpty(receiveBy.ToString()))
                {
                    str += "<fieldset><legend>Hand Over By</legend>";
                    if (!string.IsNullOrEmpty(submitBy.ToString()))
                    {
                        str += "SubmitBy: " + submitBy.ToString();
                    }
                    if (!string.IsNullOrEmpty(receiveBy.ToString()))
                    {
                        str += System.Environment.NewLine + "ReceiveBy: " + receiveBy.ToString();
                    }
                    str += "</fieldset>";
                }


                return str;
            }

            if (Equals(nursingDiagnosaName, "ADIME"))
            {
                return (
                    "A: " + S + System.Environment.NewLine +
                    "D: " + O + System.Environment.NewLine +
                    "I: " + A + System.Environment.NewLine +
                    "M: " + P + System.Environment.NewLine +
                    "E: " + (info5 ?? string.Empty));
            }

            if (Equals(nursingDiagnosaName, "Handover Patient"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "O: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + P + System.Environment.NewLine +
                    "Submit By: " + (submitBy ?? string.Empty) + System.Environment.NewLine +
                    "Receive By: " + (receiveBy ?? string.Empty)
                    );

            }
            return nursingDiagnosaName;
        }

        public static string parsePhrlRespond(IEnumerable<PatientHealthRecordLine> phrls)
        {
            if (phrls.Count() == 0) return string.Empty;

            var respond = string.Empty;

            foreach (var phrl in phrls)
            {
                var Ass = string.Empty;
                var q = new Question();
                if (q.LoadByPrimaryKey(phrl.QuestionID))
                {
                    var Ret = NursingDiagnosaTransDT.GetAssessmentValueList(
                        q.SRAnswerType, phrl.QuestionAnswerText + " " + phrl.QuestionAnswerText2,
                        phrl.QuestionAnswerNum ?? 0, q.QuestionText,
                        q.AnswerDecimalDigit ?? 0, q.AnswerSuffix);
                    foreach (var r in Ret)
                    {
                        Ass += ((Ass.Length == 0) ? "" : ", ") + r.AssessmentName;
                    }
                }
                respond = respond + (respond.Length > 0 ? ", " : "") + Ass;
            }
            //}
            return respond;
        }

        private DataTable NursingImplementasi(bool OpenOnly)
        {
            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");
            NursingDiagnosaQuery prDiag = new NursingDiagnosaQuery("b");
            NursingDiagnosaTransDTQuery dt = new NursingDiagnosaTransDTQuery("c");
            NursingDiagnosaTransDTQuery dtDiag = new NursingDiagnosaTransDTQuery("d");

            query.es.Distinct = true;

            query.InnerJoin(prDiag).On(query.NursingDiagnosaParentID == prDiag.NursingDiagnosaID
                & query.SRNursingDiagnosaLevel == "30");
            query.InnerJoin(dtDiag).On(prDiag.NursingDiagnosaID == dtDiag.NursingDiagnosaID
                & dtDiag.TransactionNo == hfTransNo.Value);
            query.InnerJoin(dt).On(query.NursingDiagnosaID == dt.NursingDiagnosaID
                & dt.TransactionNo == hfTransNo.Value);

            if (OpenOnly)
            {
                query.Where("<ISNULL(c.SRNursingCarePlanning,'') <> '01'>", "<ISNULL(d.SRNursingCarePlanning,'') <> '01'>");
            }

            query.Select(
                query,
                prDiag.NursingDiagnosaID.As("DiagID"),
                prDiag.NursingDiagnosaName.As("DiagName"),
                dt.ID,
                dt.NursingDiagnosaID.As("TransNursingDiagnosaID"),
                "<ISNULL(c.NursingDiagnosaName, a.NursingDiagnosaName) NursingDiagnosaNameEdited>",
                dt.Priority,
                dt.EvalPeriod,
                dt.PeriodConversionInHour,
                dt.Skala,
                dt.Target,
                dt.Evaluasi,
                dt.SRNursingCarePlanning, dtDiag.Priority.As("DiagnosaPriority")
                )
                .OrderBy(dtDiag.Priority.Ascending);

            var dttbl = query.LoadDataTable();
            return dttbl;
        }
        #endregion

        #region Events
        #region grid interventions

        protected void cboParamedic_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((Paramedic)e.Item.DataItem).ParamedicName;
            e.Item.Value = ((Paramedic)e.Item.DataItem).ParamedicID;
        }
        protected void cboTemplate_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateID].ToString();
        }
        protected void cboTemplate_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtImplementationText.Text))
                txtImplementationText.Text = cboTemplate.Text;

            if (e.Value.Equals("0") || e.Value.Equals(""))
            {
                txtCustomRespond.Text = OldData;

                grdQuestionRespond.DataSource = null;
                grdQuestionRespond.Rebind();
                grdQuestionRespond.Visible = false;
            }
            else
            {
                OldData = txtCustomRespond.Text;
                var t = new NursingDiagnosaTemplate();
                if (t.LoadByPrimaryKey(int.Parse(e.Value)))
                {
                    txtCustomRespond.Text = t.TemplateText;

                    grdQuestionRespond.DataSource = null;
                    grdQuestionRespond.Rebind();
                    grdQuestionRespond.Visible = true;


                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "fillFormulaField", "alert('testing');", true);

                    var formulas = new Hashtable();

                    var dicQ = new Dictionary<string, string>();

                    foreach (GridDataItem gi in grdQuestionRespond.MasterTableView.Items)
                    {
                        var q = new Question();
                        if (q.LoadByPrimaryKey(gi.GetDataKeyValue("QuestionID").ToString()))
                        {
                            var qid = q.QuestionID.Replace('.', '_');
                            var ao = gi["AnswerObj"].FindControl("quest" + qid);
                            if (ao != null)
                            {
                                dicQ.Add(q.QuestionID, ao.ClientID);
                            }

                            if (!string.IsNullOrEmpty(q.Formula))
                            {
                                if (q.SRAnswerType == "TBL")
                                {

                                }
                                else
                                {
                                    formulas.Add(qid, q.Formula);
                                }
                            }
                        }
                    }

                    var baseClientID = ""; //string.Format("{0}_q_", this.ClientID);
                                           //var baseClientID = gi["AnswerObj"].Controls[0].ClientID    "ctl00_ContentPlaceHolder1_grdQuestionRespond_ctl00_ctl10_questAPG0004" string

                    var script = Temiang.Avicenna.CustomControl.Phr.PhrCtl.GenerateFormulaScript(baseClientID, formulas);

                    // replace disini saja
                    foreach (var dict in dicQ)
                    {
                        var qid = dict.Key.Replace('.', '_');
                        script = script.Replace(qid, dict.Value);
                    }

                    //Page.ClientScript.RegisterClientScriptBlock(GetType(), "formula", script.ToString());
                    //Page.ClientScript.RegisterStartupScript(GetType(), "formula", script.ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "formula", script.ToString(), false);
                    //pnl123.ajax
                    //Page.ClientScript.RegisterClientScriptBlock(pnl123.GetType(), "formula", script.ToString());
                    //ScriptManager.RegisterStartupScript(RadCodeBlock3, RadCodeBlock3.GetType(), "formula", script.ToString(), false);
                }
            }

            //BuildGridDataSourceFromTemplate();
        }

        protected void grdCustomNic_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem x = (GridDataItem)e.Item;
            var id = x.GetDataKeyValue("NursingDiagnosaID").ToString();
            hfNicCode.Value = x.GetDataKeyValue("NursingDiagnosaID").ToString();
            InitEntry();
            SelectInputType("");

            var diagName = x["NursingDiagnosaName"].Text;
            txtImplementationText.Text = diagName;
            var ns = new NursingDiagnosa();
            if (ns.LoadByPrimaryKey(id))
            {
                if (ns.TemplateID.HasValue)
                {
                    cboTemplate.SelectedValue = ns.TemplateID.ToString();
                    cboTemplate_SelectedIndexChanged(cboTemplate, new RadComboBoxSelectedIndexChangedEventArgs(cboTemplate.SelectedValue, "xxx", cboTemplate.SelectedValue, "xxx"));
                }
            }
        }
        protected void grdCustomNic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = NursingDiagnosa.GetDiagnosaByLevelAndServiceUnit("32", ServiceUnitID);
        }

        protected void gridListImplementasiDiagnosa_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dt = NursingImplementasi(true);
            ImplementationCountPerIntervention = (new NursingDiagnosaTransDTCollection()).ImplementationCountPerIntervention(hfTransNo.Value);

            ((RadGrid)source).DataSource = dt;
        }

        protected void gridListImplementasiDiagnosa_ItemDataBound(object source, GridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case (GridItemType.AlternatingItem):
                case (GridItemType.Item):
                    {
                        GridDataItem i = (GridDataItem)e.Item;
                        var IDNIC = i.GetDataKeyValue("NursingDiagnosaID").ToString();

                        var ds = ImplementationCountPerIntervention.AsEnumerable()
                            .Where(x => x.Field<string>("NursingDiagnosaID") == IDNIC);
                        if ((int)ds.First()["cID31"] == 0)
                        {
                            e.Item.ForeColor = System.Drawing.Color.Red;
                            e.Item.Font.Bold = true;
                        }
                        break;
                    }
            }
        }
        protected void gridListImplementasi_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            var nid = (Int64)item.GetDataKeyValue("ID");

            NursingDiagnosaTransDT entity = new NursingDiagnosaTransDT();
            if (entity.LoadByPrimaryKey(nid))
            {
                entity.MarkAsDeleted();
                entity.Save();
            }

            gridListImplementasi.Rebind();
        }
        protected void gridListImplementasiDiagnosa_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem x = (GridDataItem)e.Item;
            hfNicInt.Value = x.GetDataKeyValue("ID").ToString();
            hfNicCode.Value = x.GetDataKeyValue("NursingDiagnosaID").ToString();
            InitEntry();
            SelectInputType("");

            var diagName = x["NursingDiagnosaNameEdited"].Text;
            txtImplementationText.Text = Helper.FirstLetterToUpper(Helper.NounToVerb(diagName));
        }
        #endregion
        protected void gridListImplementasi_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var prColl = new NursingDiagnosaTransDTCollection();
            var regnos = Common.NursingCare.GetRelatedRegistrationsByNsTransNo(hfTransNo.Value);
            var ni = prColl.ImplementationByPage(regnos, false,
                    ((gridListImplementasi.CurrentPageIndex * gridListImplementasi.PageSize) + 1),
                    ((gridListImplementasi.CurrentPageIndex + 1) * gridListImplementasi.PageSize));

            var nRespond = ni.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Field<string>("ReferenceToPhrNo")));
            foreach (var n in nRespond)
            {
                // Generate summary untuk data lama yg tidak disimpan di asesmen field
                // Data summary PPA Notes respond tempate disimpan di kolom Objective per update app tgl 07 ds 20023 (Handono)
                if (n["S"] == DBNull.Value || string.IsNullOrWhiteSpace(n["S"].ToString()))
                {
                    var phrlColl = new PatientHealthRecordLineCollection();
                    if (phrlColl.LoadByTransactionNoRegNoOfTemplateEntry(n["ReferenceToPhrNo"].ToString(), n["RegistrationNo"].ToString()))
                    {
                        n["Respond2"] = parsePhrlRespond(phrlColl);
                    }
                }
                else 
                    n["Respond2"] = n["S"];
            }

            gridListImplementasi.VirtualItemCount = prColl.ImplementationCount(regnos, false);

            gridListImplementasi.DataSource = ni;
        }

        protected void gridListImplementasi_ItemDataBound(object source, GridItemEventArgs e)
        {
            //if (DataModeCurrent == AppEnum.DataMode.Edit)
            //{
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                // find user create 
                var UserCreate = DataBinder.Eval(e.Item.DataItem, "CreateByUserID");
                var IsDeleted = DataBinder.Eval(e.Item.DataItem, "IsDeleted") is DBNull ? false : (bool)DataBinder.Eval(e.Item.DataItem, "IsDeleted");
                if (string.Equals(UserCreate, AppSession.UserLogin.UserID) && !IsDeleted)
                {
                    // allow edit and delete

                }
                else
                {
                    // edit not allowed
                    //ImageButton lnke = item["customEdit"].Controls[1] as ImageButton;
                    //item["customEdit"].Controls.Remove(lnke);
                    item["customEdit"].Controls.Clear();

                    ImageButton lnkd = item["DeleteColumn"].Controls[0] as ImageButton;
                    item["DeleteColumn"].Controls.Remove(lnkd);

                    if (IsDeleted)
                    {
                        item.Font.Strikeout = true;
                        item.ForeColor = System.Drawing.Color.Gray;
                    }
                }
            }
            //}
        }
        protected void gridListImplementasi_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "editItem")
            {
                GridDataItem x = (GridDataItem)e.Item;
                var id = x.GetDataKeyValue("ID").ToString();
                InitEntry();
                var nsName = LoadEntity(id);
                SelectInputType(nsName);

                var script = "$(\"html, body\").animate({ scrollTop: 0 }, \"slow\");";
                //Create Startup Javascript for message
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollUp", script, true);

                gridListImplementasi.Rebind();
            }
        }
        protected void tbInputType_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string val = toolBarButton.Text;
            if (val == "Kegiatan Rutin") val = string.Empty;
            InitEntry();
            SelectInputType(val);
        }

        protected void btnSave_click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                gridListImplementasi.Rebind(); // kalau tidak rebind muncul semua tombol edit walau user lain yang create 
                return;
            }

            if (AppSession.Parameter.IsUsingValidationImplementationDateTimeOnPpaNotes)
            {
                if (txtDateTimeImplementation.SelectedDate > DateTime.Now)
                {
                    ShowInformationHeader("Implementation Date Time must not exceed System Date Time.");
                    return;
                }
            }

            var isNew = string.IsNullOrEmpty(hfIDImplementation.Value);
            var ndt = new NursingDiagnosaTransDT();
            if (isNew)
            {
                ndt.AddNew();
            }
            else
            {
                if (!ndt.LoadByPrimaryKey(System.Convert.ToInt64(hfIDImplementation.Value)))
                {
                    // raise error
                    ShowInformationHeader(AppConstant.Message.RecordNotExist);
                    return;
                }

                if (ndt.CreateByUserID != AppSession.UserLogin.UserID)
                {
                    ShowInformationHeader("You are not allowed to edit notes that are created by another user");
                    gridListImplementasi.Rebind(); // kalau tidak rebind muncul semua tombol edit walau user lain yang create 
                    return;
                }
            }


            ndt.LastUpdateByUserID = AppSession.UserLogin.UserID;
            ndt.LastUpdateDateTime = DateTime.Now;
            ndt.TmpNursingDiagnosaID = string.Empty;

            ndt.CreateByUserID = AppSession.UserLogin.UserID;
            ndt.CreateDateTime = DateTime.Now;
            ndt.TransactionNo = hfTransNo.Value;
            ndt.NursingDiagnosaID = string.Empty; // kosongkan saja lah
            ndt.NursingDiagnosaName = txtImplementationText.Text;
            ndt.SRNursingDiagnosaLevel = "31";
            if (!string.IsNullOrEmpty(hfNicInt.Value))
            {
                Int64 id = System.Convert.ToInt64(hfNicInt.Value);
                ndt.ParentID = id;
            }
            ndt.NursingDiagnosaParentID = hfNicCode.Value;
            ndt.Priority = 0;
            ndt.EvalPeriod = 0;
            ndt.PeriodConversionInHour = 24;
            ndt.Skala = 1;
            ndt.Target = 0;
            ndt.Evaluasi = 1;
            ndt.Respond = txtCustomRespond.Text;
            ndt.Reexamine = false;
            ndt.ExecuteDateTime = txtDateTimeImplementation.SelectedDate;

            ndt.S = txtS.Text;
            ndt.O = txtO.Text;
            ndt.A = txtA.Text;
            ndt.P = txtP.Text;
            ndt.Info5 = txtInfo5.Text;
            ndt.PpaInstruction = txtPpaInstruction.Text;
            ndt.ParamedicID = cboParamedic.SelectedValue;
            ndt.SubmitBy = txtSubmitBy.Text;
            ndt.ReceiveBy = txtReceiveBy.Text;
            if (InputType == "S B A R")
                ndt.ReceiveBy = txtReceiveBySbar.Text;

            ndt.TmpNursingDiagnosaParentID = string.Empty;

            var tid = cboTemplate.SelectedValue;
            if (tid != "0" && tid != string.Empty)
            {
                ndt.TemplateID = System.Convert.ToInt32(tid);
            }

            //userControl.UpdatePHRLine();
            var phr = new PatientHealthRecord();
            if (!string.IsNullOrEmpty(hfRelatedPHRNo.Value))
            {
                phr.Query.Where(phr.Query.TransactionNo == hfRelatedPHRNo.Value,
                    phr.Query.RegistrationNo == RegistrationNo);
                phr.Load(phr.Query);
            }
            else
            {
                phr.AddNew();
            }

            var phrlColl = new PatientHealthRecordLineCollection();
            if (!string.IsNullOrEmpty(ndt.ReferenceToPhrNo))
            {
                phrlColl.Query.Where(phrlColl.Query.TransactionNo == ndt.ReferenceToPhrNo,
                    phrlColl.Query.RegistrationNo == RegistrationNo);
                phrlColl.LoadAll();
                foreach (var phrl in phrlColl)
                {
                    phrl.MarkAsDeleted();
                }
            }

            if (!string.IsNullOrEmpty(cboTemplate.SelectedValue) && cboTemplate.SelectedValue != "0")
            {
                foreach (GridDataItem row in grdQuestionRespond.MasterTableView.Items)
                {
                    SetPatientHealthRecordLine(phrlColl, row);
                    //questionIDs.Add(row.GetDataKeyValue("QuestionID").ToString());
                }
            }

            AppAutoNumberLast _autoNumberPHR;

            using (esTransactionScope trans = new esTransactionScope())
            {
                if (phrlColl.Count() > 0)
                {
                    if (phr.es.IsAdded)
                    {
                        _autoNumberPHR = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.PatientHealthRecord);
                        phr.TransactionNo = _autoNumberPHR.LastCompleteNumber;
                        _autoNumberPHR.Save();
                    }
                    phr.RegistrationNo = RegistrationNo;
                    phr.QuestionFormID = cboTemplate.SelectedValue;

                    phr.EmployeeID = AppSession.UserLogin.UserID;
                    phr.IsComplete = true;
                    phr.ReferenceNo = string.Empty;
                    phr.ExaminerID = AppSession.UserLogin.UserID;
                    phr.ServiceUnitID = string.Empty;

                    //Last Update Status
                    if (phr.es.IsAdded || phr.es.IsModified)
                    {
                        phr.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        phr.LastUpdateDateTime = DateTime.Now;
                    }
                    if (phr.es.IsAdded)
                    {
                        phr.CreateByUserID = AppSession.UserLogin.UserID;
                        phr.CreateDateTime = DateTime.Now;
                    }

                    foreach (var phrl in phrlColl)
                    {
                        phrl.TransactionNo = phr.TransactionNo;
                        phrl.QuestionFormID = phr.QuestionFormID;
                        phrl.QuestionGroupID = string.Empty;

                        // Set PHR Date
                        var phrDate = phrl.LastUpdateDateTime.Value;
                        phr.RecordDate = phrDate;
                        phr.RecordTime = phrDate.ToString("HH:mm");
                    }
                    phr.Save();
                    phrlColl.Save();
                    ndt.ReferenceToPhrNo = phr.TransactionNo;

                    // Simpan rangkuman di Objective field untuk kecepatan nanti saat menampilkan data rangkumannya (Handono 2023 Des 03)
                    if (!string.IsNullOrEmpty(cboTemplate.SelectedValue) && cboTemplate.SelectedValue != "0")
                    {
                        var questions = new QuestionCollection();
                        ndt.O = RADT.Emr.NursingImplementationEntry.ParsePhrlRespond(phrlColl, questions);
                    }
                }

                ndt.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            gridListImplementasi.Rebind();
            InitEntry();
        }

        private void SetPatientHealthRecordLine(/*bool isNewRecord, string transactionNo, */
            PatientHealthRecordLineCollection collValue, GridDataItem gdi/*, string questionGroupID*/)
        {
            PatientHealthRecordLine hrLine;
            string questionID = gdi.GetDataKeyValue("QuestionID").ToString();

            var q = new Question();
            q.LoadByPrimaryKey(questionID);

            IEnumerable<PatientHealthRecordLine> hrLines = collValue.Where(x => x.QuestionID == questionID);

            if (hrLines.Count() > 1) throw new Exception("Multiple question in one form");
            hrLine = hrLines.Count() == 1 ? hrLines.First() : collValue.AddNew();

            hrLine.TransactionNo = hfRelatedPHRNo.Value;
            hrLine.RegistrationNo = RegistrationNo;
            hrLine.QuestionFormID = cboTemplate.SelectedValue;
            hrLine.QuestionGroupID = hfIDImplementation.Value;
            hrLine.QuestionID = questionID;

            hrLine.QuestionAnswerPrefix = q.AnswerPrefix.ToStringDefaultEmpty();
            hrLine.QuestionAnswerSuffix = q.AnswerSuffix.ToStringDefaultEmpty();

            hrLine.LastUpdateDateTime = txtDateTimeImplementation.SelectedDate; // Utk setting PHR Date

            string controlID = EmrIp.MainContent.NursingCare
                .NursingCareStandardViewImportedAssessment.QuestionControlID(questionID);
            string answerType = q.SRAnswerType;
            object obj = null;

            if (answerType != "DNT") //Dental Control
            {
                if (string.IsNullOrEmpty(q.ReferenceQuestionID.ToStringDefaultEmpty()))
                    obj = Helper.FindControlRecursive(gdi, controlID);
                else
                    obj = Helper.FindControlRecursive(gdi, EmrIp.MainContent.NursingCare
                .NursingCareStandardViewImportedAssessment.QuestionControlID(q.ReferenceQuestionID.ToString()));

                if (obj == null)
                {
                    hrLine.MarkAsDeleted();
                    return;
                }
            }

            switch (answerType)
            {
                case "MSK":
                    var mskAnswerValue = (obj as RadMaskedTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(mskAnswerValue.TextWithLiterals);
                    break;
                case "DAT":
                    var dat = (obj as RadDatePicker);
                    hrLine.QuestionAnswerText = (dat.SelectedDate ?? DateTime.Now).ToShortDateString();
                    break;
                case "TIM":
                    var tim = (obj as RadTimePicker);
                    hrLine.QuestionAnswerText = (tim.SelectedDate ?? DateTime.Now).ToString("HH:mm");
                    break;
                case "DTM":
                    var dattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = (dattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm");
                    break;
                case "NUM":
                    var numAnswerValue = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerNum = Convert.ToDecimal(numAnswerValue.Value);
                    break;
                case "MEM":
                    var memAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(memAnswerValue.Text);
                    break;
                case "TXT":
                    var txtAnswerValue = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txtAnswerValue.Text);
                    break;
                case "CBR":
                case "CBL":
                case "CBO":
                    {
                        var cboAnswerValue = (obj as RadComboBox);
                        hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cboAnswerValue.SelectedValue);
                        hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cboAnswerValue.Text);

                        // Simpan juga di QuestionAnswerNum jika Selection berasal dari RANGE int yg berarti adalah int (Handono 230308)
                        if (int.TryParse(cboAnswerValue.Text, out int numValue))
                            hrLine.QuestionAnswerNum = Convert.ToInt32(numValue);

                        break;
                    }
                case "RBT":
                    {
                        var rbl = (obj as RadioButtonList);
                        //if (rbl.SelectedValue != null)
                        //{
                        //    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(rbl.SelectedValue);
                        //    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(rbl.Text); // <- isinya sama dengan SelectedValue
                        //}
                        if (rbl.SelectedItem != null)
                        {
                            hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(rbl.SelectedItem.Value);
                            hrLine.QuestionAnswerText = HtmlTagHelper.Validate(rbl.SelectedItem.Text);
                        }
                        else
                        {
                            hrLine.str.QuestionAnswerSelectionLineID = string.Empty;
                            hrLine.str.QuestionAnswerText = string.Empty;
                        }

                        // Simpan juga di QuestionAnswerNum jika Selection berasal dari RANGE int yg berarti adalah int (Handono 231208)
                        if (rbl.SelectedItem != null)
                        {
                            if (int.TryParse(rbl.SelectedItem.Value, out int numValue))
                                hrLine.QuestionAnswerNum = Convert.ToInt32(numValue);
                        }
                        else
                            hrLine.str.QuestionAnswerNum = string.Empty;

                        break;
                    }
                case "CHK":
                    var chk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = chk != null && chk.Checked ? "1" : "0";
                    break;
                case "CTX":
                    var ctxChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctxChk != null && ctxChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var ctxTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctxTxt.Text));
                    break;
                case "CTM":
                    var ctmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = ctmChk != null && ctmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var ctmTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(ctmTxt.Text));
                    break;
                case "CNM":
                    var cnmChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cnmChk != null && cnmChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cnmNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cnmNum.Text));
                    break;
                case "CDT":
                    var cdtChk = (obj as CheckBox);
                    hrLine.QuestionAnswerText = cdtChk != null && cdtChk.Checked ? "1" : "0";

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cdtDattim = (obj as RadDateTimePicker);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate((cdtDattim.SelectedDate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm")));
                    break;
                case "CBT":
                    var cbtCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbo.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbtTxt = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxt.Text));
                    break;
                case "CBN":
                    var cbnCbo = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbnCbo.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbnCbo.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbnNum = (obj as RadNumericTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbnNum.Text));
                    break;
                case "CBM":
                    var cbtCbm = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbtCbm.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbtCbm.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbtTxm = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbtTxm.Text));
                    break;
                case "CB2":
                    var cbo1 = (obj as RadComboBox);
                    hrLine.QuestionAnswerSelectionLineID = HtmlTagHelper.Validate(cbo1.SelectedValue);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(cbo1.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var cbo2 = (obj as RadComboBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(cbo2.Text));

                    hrLine.QuestionAnswerSelectionLineID = string.Format("{0}|{1}", HtmlTagHelper.Validate(cbo1.SelectedValue), HtmlTagHelper.Validate(cbo2.SelectedValue));
                    break;
                case "TTX":
                    var txt1 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = HtmlTagHelper.Validate(txt1.Text);

                    obj = Helper.FindControlRecursive(gdi, controlID + "_2");
                    var txt2 = (obj as RadTextBox);
                    hrLine.QuestionAnswerText = string.Format("{0}|{1}", HtmlTagHelper.Validate(hrLine.QuestionAnswerText), HtmlTagHelper.Validate(txt2.Text));
                    break;
            }
        }

        private string LoadEntity(string implID)
        {
            var ndt = new NursingDiagnosaTransDT();
            if (string.IsNullOrEmpty(implID))
            {
                ndt.AddNew();
            }
            else
            {
                if (!ndt.LoadByPrimaryKey(System.Convert.ToInt64(implID)))
                {
                    ShowInformationHeader(AppConstant.Message.RecordNotExist);
                    return "";
                }
            }
            hfIDImplementation.Value = implID;
            txtDateTimeImplementation.SelectedDate = ndt.ExecuteDateTime ?? DateTime.Now;
            txtImplementationText.Text = ndt.NursingDiagnosaName;
            txtCustomRespond.Text = ndt.Respond;
            hfRelatedPHRNo.Value = ndt.ReferenceToPhrNo;
            if (ndt.TemplateID.HasValue)
            {
                if (cboTemplate.FindItemByValue(ndt.TemplateID.Value.ToString()) != null)
                {
                    cboTemplate.SelectedValue = ndt.TemplateID.Value.ToString();
                    cboTemplate_SelectedIndexChanged(cboTemplate, new RadComboBoxSelectedIndexChangedEventArgs(cboTemplate.SelectedValue, "", cboTemplate.SelectedValue, ""));
                }
            }
            txtS.Text = ndt.S;
            txtO.Text = ndt.O;
            txtA.Text = ndt.A;
            txtP.Text = ndt.P;
            txtPpaInstruction.Text = ndt.PpaInstruction;
            SelectCboPar(ndt.ParamedicID);
            txtInfo5.Text = ndt.Info5;
            txtReceiveBy.Text = ndt.ReceiveBy;
            if (InputType == "S B A R" || ndt.NursingDiagnosaName == "S B A R")
                txtReceiveBySbar.Text = ndt.ReceiveBy;
            txtSubmitBy.Text = ndt.SubmitBy;

            return ndt.NursingDiagnosaName;
        }

        private void SelectCboPar(string paramedicID)
        {
            if (string.IsNullOrEmpty(paramedicID))
            {
                cboParamedic.SelectedValue = "";
                cboParamedic.Text = "";
            }
            else
            {
                //var it = cboParamedic.FindItemByValue(paramedicID);
                //if (it != null)
                //{
                //    it.Selected = true;
                //}

                //ganti jadi cbo webservice (RSYS)
                ComboBox.PopulateWithOneParamedic(cboParamedic, paramedicID);
            }
        }

        #region GridRespond
        //Request RSYS dokter di SBAR bisa tampil semua dokter. Parameter -> IsAllPhysicianOnSbar (Fajri - 12/03/2023 - RSYS)
        //Ganti jadi cbo web service
        //private void LoadParamedic()
        //{
        //    //var parColl = new ParamedicCollection();
        //    //var par = new ParamedicQuery("par");
        //    //var pteam = new ParamedicTeamQuery("pteam");
        //    //par.InnerJoin(pteam).On(par.ParamedicID == pteam.ParamedicID)
        //    //    .Where(pteam.RegistrationNo == RegistrationNo);
        //    //parColl.Load(par);

        //    //var empty = parColl.AddNew();
        //    //empty.ParamedicID = "";
        //    //empty.ParamedicName = "";

        //    //cboParamedic.DataSource = parColl.OrderBy(x => x.ParamedicName);
        //    //cboParamedic.DataBind();

        //    ComboBox.PopulateWithParamedicTeam(cboParamedic, RegistrationNo, RegistrationType, DateTime.Today, cboParamedic.SelectedValue);
        //}
        private void LoadTemplateRespond()
        {
            var query = new NursingDiagnosaTemplateQuery("a");
            query.Where(query.IsActive == true)
                .Select(query.TemplateID,
                    query.TemplateName,
                    query.TemplateText);
            if (!string.IsNullOrEmpty(ServiceUnitID))
            {
                var su = new NursingDiagnosaTemplateServiceUnitQuery("ndts");
                query.LeftJoin(su).On(query.TemplateID == su.TemplateID)
                    .Where(query.Or(su.ServiceUnitID == ServiceUnitID, su.ServiceUnitID.IsNull()));
            }

            var dt = query.LoadDataTable();

            var dta = dt.Clone();
            dta.Rows.Clear();
            var dr = dta.NewRow();
            dr[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateID] = 0;
            dr[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateName] = string.Empty;
            dr[NursingDiagnosaTemplateMetadata.ColumnNames.TemplateText] = string.Empty;
            dta.Rows.Add(dr);
            dta.Merge(dt);

            cboTemplate.DataSource = dta;
            cboTemplate.DataBind();
        }

        protected void grdQuestionRespond_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            QuestionCollection cColl = new QuestionCollection();

            var templateID = cboTemplate.SelectedValue;
            if (templateID.Equals("0") || templateID.Equals(string.Empty))
            {
                //grdQuestionRespond.DataSource = null;
            }
            else
            {
                int templateId = System.Convert.ToInt32(cboTemplate.SelectedValue);
                var rQuestion = new NursingDiagnosaTemplateDetailCollection();
                rQuestion.Query.Where(rQuestion.Query.TemplateID == templateId);
                if (rQuestion.LoadAll())
                {
                    cColl = NursingDiagnosaTransDT.GetQuestionsByTemplateID(templateId);
                }
            }

            grdQuestionRespond.DataSource = cColl;
        }

        private PatientHealthRecordLineCollection GetPHRL()
        {
            var coll = new PatientHealthRecordLineCollection();
            if (!string.IsNullOrEmpty(hfRelatedPHRNo.Value))
            {
                coll.Query.Select("<*>", "<GETDATE() as refToPatientHealthRecord_RecordDate>");
                coll.Query.Where(coll.Query.TransactionNo == hfRelatedPHRNo.Value);
                coll.LoadAll();
            }
            return coll;
        }

        protected void grdQuestionRespond_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //BuildCustomEntryAssessment(e.Item as GridDataItem);

                var item = e.Item as GridDataItem;

                var id = item.GetDataKeyValue("QuestionID").ToString();

                Question q = new Question();
                q.LoadByPrimaryKey(id);

                var qAns = GetPHRL().Where(x => x.QuestionID == id).FirstOrDefault();

                item["AnswerObj"].Controls.Clear();
                if (!string.IsNullOrEmpty(q.SRAnswerType))
                {
                    EmrIp.MainContent.NursingCare.NursingCareStandardViewImportedAssessment
                        .InitializedQuestion(q, item["AnswerObj"],
                        qAns == null ? string.Empty : qAns.QuestionAnswerText,
                        qAns == null ? new double() : System.Convert.ToDouble(qAns.QuestionAnswerNum),
                        qAns == null ? string.Empty : qAns.QuestionAnswerSelectionLineID, false);
                }
            }
        }
        protected void grdQuestionRespond_ItemDataBound(object source, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //BuildCustomEntryAssessment(e.Item as GridDataItem);

                var item = e.Item as GridDataItem;

                var id = item.GetDataKeyValue("QuestionID").ToString();

                Question q = item.DataItem as Question;
                if (q == null)
                    return;

                var qAns = GetPHRL().Where(x => x.QuestionID == id).FirstOrDefault();

                item["AnswerObj"].Controls.Clear();
                if (!string.IsNullOrEmpty(q.SRAnswerType))
                {
                    EmrIp.MainContent.NursingCare.NursingCareStandardViewImportedAssessment
                        .InitializedQuestion(q, item["AnswerObj"],
                        qAns == null ? string.Empty : qAns.QuestionAnswerText,
                        qAns == null ? new double() : System.Convert.ToDouble(qAns.QuestionAnswerNum),
                        qAns == null ? string.Empty : qAns.QuestionAnswerSelectionLineID, false);
                }
            }
        }


        #endregion

        #endregion
    }
}
