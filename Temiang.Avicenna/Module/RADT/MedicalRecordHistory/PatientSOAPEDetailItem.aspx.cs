using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Appointment = Temiang.Avicenna.BusinessObject.Appointment;
using System.Web.UI.WebControls;
using System.Data;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class PatientSOAPEDetailItem : BasePageDialog
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string regType = Page.Request.QueryString["rt"];
            switch (regType)
            {
                case AppConstant.RegistrationType.OutPatient:
                    ProgramID = AppConstant.Program.OutPatientEpisode;
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    ProgramID = AppConstant.Program.ClusterPatientEpisode;
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    ProgramID = AppConstant.Program.EmergencyPatientEpisode;
                    break;
                case AppConstant.RegistrationType.InPatient:
                    ProgramID = AppConstant.Program.InPatientEpisode;
                    break;
            }

            Button btnOK = ((Button)Helper.FindControlRecursive(Page, "btnOk"));
            Button btnCancel = ((Button)Helper.FindControlRecursive(Page, "btnCancel"));
            btnOK.Visible = false;
            btnCancel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                PopulateEpisodeDiagnoseGrid();
                PopulateEpisodeProcedureGrid();
                PopulateEpisodeSOAPEGrid();

            }
        }

        private EpisodeDiagnoseCollection EpisodeDiagnoses
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeDiagnose"];
                    if (obj != null)
                        return ((EpisodeDiagnoseCollection)(obj));
                }

                EpisodeDiagnoseCollection coll = new EpisodeDiagnoseCollection();
                EpisodeDiagnoseQuery query = new EpisodeDiagnoseQuery("a");
                DiagnoseQuery diag = new DiagnoseQuery("b");
                AppStandardReferenceItemQuery item = new AppStandardReferenceItemQuery("e");
                MorphologyQuery morph = new MorphologyQuery("c");
                ParamedicQuery param = new ParamedicQuery("d");

                query.InnerJoin(diag).On(query.DiagnoseID == diag.DiagnoseID);
                query.InnerJoin(item).On(query.SRDiagnoseType == item.ItemID);
                query.LeftJoin(morph).On(query.MorphologyID == morph.MorphologyID);
                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);

                query.Select
                    (
                        query.RegistrationNo,
                        query.SequenceNo,
                        query.DiagnoseID,
                        diag.DiagnoseName.As("refToDiagnose_DiagnoseName"),
                        query.SRDiagnoseType,
                        item.ItemName.As("refToAppStandardReferenceItem_SRDiagnoseType"),
                        query.DiagnosisText,
                        query.MorphologyID,
                        morph.MorphologyName.As("refToMorphology_MorphologyName"),
                        query.ParamedicID,
                        param.ParamedicName.As("refToParamedic_ParamedicName"),
                        query.IsChronicDisease,
                        query.IsOldCase,
                        query.IsConfirmed,
                        query.IsVoid,
                        query.Notes,
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                    );

                query.Where(query.RegistrationNo == Page.Request.QueryString["regno"].ToString());
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEpisodeDiagnose"] = coll;
                return coll;
            }
            set { Session["collEpisodeDiagnose"] = value; }
        }

        private void PopulateEpisodeDiagnoseGrid()
        {
            //Display Data Detail
            EpisodeDiagnoses = null; //Reset Record Detail

            grdEpisodeDiagnose.MasterTableView.IsItemInserted = false;
            grdEpisodeDiagnose.MasterTableView.ClearEditItems();

            grdEpisodeDiagnose.Rebind();
        }

        protected void grdEpisodeDiagnose_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeDiagnose.DataSource = EpisodeDiagnoses;
        }

        private EpisodeProcedureCollection EpisodeProcedures
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeProcedure"];
                    if (obj != null)
                        return ((EpisodeProcedureCollection)(obj));
                }

                EpisodeProcedureCollection coll = new EpisodeProcedureCollection();
                EpisodeProcedureQuery query = new EpisodeProcedureQuery("a");
                ParamedicQuery param = new ParamedicQuery("b");
                ProcedureQuery proc = new ProcedureQuery("c");

                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);

                query.Select
                     (
                         query.RegistrationNo,
                         query.SequenceNo,
                         query.ProcedureDate,
                         query.ProcedureTime,
                         query.ProcedureDate2,
                         query.ProcedureTime2,
                         query.ParamedicID,
                         param.ParamedicName.As("refToParamedic_ParamedicName"),
                         query.ParamedicID2,
                         query.ProcedureID,
                         query.SRProcedureCategory,
                         query.SRAnestesi,
                         query.RoomID,
                         query.IsCito,
                         proc.ProcedureName.As("refToProcedure_ProcedureName"),
                         query.LastUpdateByUserID,
                         query.LastUpdateDateTime
                     );

                query.Where(query.RegistrationNo == Page.Request.QueryString["regno"].ToString());
                query.OrderBy(query.SequenceNo.Ascending);

                coll.Load(query);

                Session["collEpisodeProcedure"] = coll;
                return coll;
            }
            set { Session["collEpisodeProcedure"] = value; }
        }

        private void PopulateEpisodeProcedureGrid()
        {
            //Display Data Detail
            EpisodeProcedures = null; //Reset Record Detail

            grdEpisodeProcedure.MasterTableView.IsItemInserted = false;
            grdEpisodeProcedure.MasterTableView.ClearEditItems();

            grdEpisodeProcedure.Rebind(); //Ambil ulang record detail
        }

        protected void grdEpisodeProcedure_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeProcedure.DataSource = EpisodeProcedures;
        }

        private EpisodeSOAPECollection EpisodeSOAPEs
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEpisodeSOAPE"];
                    if (obj != null)
                        return ((EpisodeSOAPECollection)(obj));
                }

                EpisodeSOAPECollection coll = new EpisodeSOAPECollection();
                EpisodeSOAPEQuery query = new EpisodeSOAPEQuery("a");
                ParamedicQuery param = new ParamedicQuery("b");

                query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);

                esQueryItem item = new esQueryItem(query, "SOAPETime", esSystemType.String);
                item = query.SOAPETime.Substring(1, 2) + ":" + query.SOAPETime.Substring(3, 2);

                query.Select
                    (
                        query.RegistrationNo,
                        query.SequenceNo,
                        query.ParamedicID,
                        param.ParamedicName.As("refToParamedic_ParamedicName"),
                        query.SOAPEDate,
                        query.SOAPETime,
                        query.Subjective,
                        query.Objective,
                        query.Assesment,
                        query.Planning,
                        query.Evaluation,
                        query.IsSummary,
                        query.AttendingNotes,
                        query.IsVoid,
                        query.LastUpdateByUserID,
                        query.LastUpdateDateTime
                    );

                query.Where(query.RegistrationNo == Page.Request.QueryString["regno"].ToString());
                query.OrderBy(query.SequenceNo.Ascending);
                coll.Load(query);

                Session["collEpisodeSOAPE"] = coll;
                return coll;
            }
            set { Session["collEpisodeSOAPE"] = value; }
        }

        private void PopulateEpisodeSOAPEGrid()
        {
            //Display Data Detail
            EpisodeSOAPEs = null; //Reset Record Detail

            grdEpisodeSOAPE.MasterTableView.IsItemInserted = false;
            grdEpisodeSOAPE.MasterTableView.ClearEditItems();

            grdEpisodeSOAPE.Rebind(); //Ambil ulang record detail
        }

        protected void grdEpisodeSOAPE_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEpisodeSOAPE.DataSource = EpisodeSOAPEs;
        }

        protected void grdEpisodeDiagnose_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                EpisodeDiagnose item = EpisodeDiagnoses[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid.Value)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        protected void grdEpisodeSOAPE_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                EpisodeSOAPE item = EpisodeSOAPEs[e.Item.DataSetIndex];
                if (item != null)
                {
                    if (item.IsVoid.Value)
                    {
                        for (int i = 0; i < e.Item.Cells.Count; i++)
                        {
                            if (i > 0 && i < e.Item.Cells.Count)
                                e.Item.Cells[i].Font.Strikeout = true;
                        }
                    }
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            return true;
        }

    }
}
