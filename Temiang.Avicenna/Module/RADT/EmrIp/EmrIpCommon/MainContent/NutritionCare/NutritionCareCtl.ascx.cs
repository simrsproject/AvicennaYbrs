using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare
{
    public partial class NutritionCareCtl : BaseMainContentCtl
    {
        public RadGrid GridFormPengkajian
        {
            get { return gridFormPengkajian; }
        }
        public RadGrid GridNursingDiagnosa
        {
            get { return gridD; }
        }

        #region ASUHAN GIZI
        public NutritionCareTransHD nsHD
        {
            get
            {
                if (Session["NutritionCareTransHD" + RegistrationNo] == null)
                {
                    Session["NutritionCareTransHD" + RegistrationNo] = Common.NutritionCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);
                }

                return (NutritionCareTransHD)Session["NutritionCareTransHD" + RegistrationNo];
            }
            set
            {
                Session["NutritionCareTransHD" + RegistrationNo] = value;
            }
        }

        #region Assessment
        private NutritionCareAssessmentTransHD AssessmentHDCreateAndSave(NutritionCareAssessmentTransHD hdAss, PatientHealthRecord phr)
        {
            hdAss.AddNew();
            hdAss.TransactionNo = nsHD.TransactionNo;
            hdAss.AssessmentDateTime = phr.RecordDate;
            hdAss.CreateByUserID = AppSession.UserLogin.UserID;
            hdAss.CreateDateTime = DateTime.Now;
            hdAss.LastUpdateByUserID = AppSession.UserLogin.UserID;
            hdAss.LastUpdateDateTime = DateTime.Now;
            hdAss.QuestionFormReference = phr.TransactionNo;
            hdAss.Save();
            return hdAss;
        }

        private DataTable FormAssessmentList()
        {
            var q = new PatientHealthRecordQuery("q");
            var f = new QuestionFormQuery("f");
            var nas = new NutritionCareAssessmentTransHDQuery("nas");

            q.InnerJoin(f).On(q.QuestionFormID == f.QuestionFormID)
                .LeftJoin(nas).On(q.TransactionNo == nas.QuestionFormReference);
            q.Where(q.RegistrationNo == RegistrationNo, f.IsNutritionCareForm == true);
            q.Select(q.TransactionNo, q.QuestionFormID, f.QuestionFormName, q.RegistrationNo, nas.ID);
            //qColl.Load(q);
            var dt = q.LoadDataTable();
            return dt;
        }
        private void ImportFromAssessmentForm(string TransactionNo)
        {
            var phr = new PatientHealthRecordCollection();
            phr.Query.Where(phr.Query.TransactionNo == TransactionNo);
            phr.LoadAll();

            // hapus import-an sebelumnya
            NutritionCareAssessmentTransHD nHD;
            var nah = new NutritionCareAssessmentTransHDCollection();
            nah.Query.Where(nah.Query.QuestionFormReference == TransactionNo);
            nah.LoadAll();
            if (nah.Count > 0)
            {
                nHD = nah.First();
                // emptying the details
                var nad = new NutritionCareAssessmentTransDTCollection();
                nad.Query.Where(nad.Query.HDID == nHD.ID);
                nad.LoadAll();
                nad.MarkAllAsDeleted();
                nad.Save();
            }
            else
            {
                // create new one
                nHD = AssessmentHDCreateAndSave(new NutritionCareAssessmentTransHD(), phr[0]);
            }

            var phrl = new PatientHealthRecordLineQuery("phrl");
            var na = new NutritionCareAssessmentQuestionQuery("na");
            var qu = new QuestionQuery("qu");
            phrl.InnerJoin(na).On(phrl.QuestionID == na.RelatedQuestionID)
                .InnerJoin(qu).On(phrl.QuestionID == qu.QuestionID)
                .Select(
                    na.QuestionID,
                    na.QuestionText,
                    na.IsSubjective,
                    na.IsObjective,
                    phrl.QuestionAnswerPrefix,
                    phrl.QuestionAnswerSuffix,
                    phrl.QuestionAnswerText,
                    phrl.QuestionAnswerNum,
                    phrl.QuestionAnswerSelectionLineID
                ).Where(phrl.TransactionNo == TransactionNo);
            var dt = phrl.LoadDataTable();

            var natColl = new NutritionCareAssessmentTransDTCollection();
            foreach (DataRow d in dt.Rows)
            {
                var nat = natColl.AddNew();
                nat.QuestionID = d["QuestionID"].ToString();
                nat.QuestionText = d["QuestionText"].ToString();
                nat.IsSubjective = (bool)d["IsSubjective"];
                nat.IsObjective = (bool)d["IsObjective"];
                nat.AnswerPrefix = d["QuestionAnswerPrefix"].ToString();
                nat.AnswerSuffix = d["QuestionAnswerSuffix"].ToString();
                nat.AnswerText = d["QuestionAnswerText"].ToString();
                nat.AnswerNum = (d["QuestionAnswerNum"] is DBNull) ? null : (decimal?)d["QuestionAnswerNum"];
                nat.AnswerSelectionLineID = d["QuestionAnswerSelectionLineID"].ToString();
                nat.HDID = nHD.ID;

                //Last Update Status
                nat.CreateByUserID = AppSession.UserLogin.UserID;
                nat.CreateDateTime = DateTime.Now;
                nat.LastUpdateByUserID = AppSession.UserLogin.UserID;
                nat.LastUpdateDateTime = DateTime.Now;
            }

            natColl.Save();
        }
        private void PrintFromAssessmentForm(string PhrID)
        {
            var phr = new PatientHealthRecord();
            phr.Query.Where(phr.Query.TransactionNo == PhrID);
            if (phr.Load(phr.Query))
            {
                var jobParameters = new PrintJobParameterCollection();
                jobParameters.AddNew("p_RegistrationNo", phr.RegistrationNo);
                jobParameters.AddNew("p_QuestionFormID", phr.QuestionFormID);
                jobParameters.AddNew("p_TransactionNo", PhrID);

                AppSession.PrintJobParameters = jobParameters;

                var form = new QuestionForm();
                form.LoadByPrimaryKey(phr.QuestionFormID);
                AppSession.PrintJobReportID = form.ReportProgramID;

                ShowPrintPreview();
            }
        }
        private void DeleteImportedAssessmentForm(string no)
        {
            var na = new NutritionCareAssessmentTransHD();
            if (na.LoadByPrimaryKey(System.Convert.ToInt64(no)))
            {
                var nd = new NutritionCareAssessmentTransDTCollection();
                nd.Query.Where(nd.Query.HDID == na.ID);
                nd.LoadAll();

                try
                {
                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        nd.MarkAllAsDeleted();
                        na.MarkAsDeleted();

                        nd.Save();
                        na.Save();
                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
                catch (Exception e)
                {
                    // something goes wrong
                }
            }
        }
        #endregion


        protected void gridFormPengkajian_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            gridFormPengkajian.DataSource = FormAssessmentList();
        }
        protected void gridFormPengkajian_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ImportPHR")
            {
                var str = e.CommandArgument.ToString();
                var hd = Common.NutritionCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);
                ImportFromAssessmentForm(str);
                gridFormPengkajian.Rebind();
                gridD.Rebind();
            }
            else if (e.CommandName == "PrintPHR")
            {
                var str = e.CommandArgument.ToString();
                PrintFromAssessmentForm(str);
            }
            else if (e.CommandName == "DeletePHR")
            {
                var str = e.CommandArgument.ToString();
                DeleteImportedAssessmentForm(str);
                gridFormPengkajian.Rebind();
                gridD.Rebind();
            }
        }
        protected void gridD_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource =
                NutritionCareDiagnoseTransDT.NutritionCareDiagnosaFullDefinitionWithNicNoc(nsHD.TransactionNo);
        }
        protected void gridD_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteD")
            {
                var str = e.CommandArgument.ToString();
                // bla bla
                str = NutritionCareDiagnoseTransDT.DeleteByIdL10(System.Convert.ToInt64(str));
                if (!string.IsNullOrEmpty(str))
                {
                    //ShowInformationHeader(str);
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('" + str + "');", true);
                }
                else
                {
                    gridD.Rebind();
                }
            }
        }
        protected void gridD_ItemDataBound(object source, GridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case (GridItemType.AlternatingItem):
                case (GridItemType.Item):
                    {
                        var lbl = e.Item.FindControl("lblNDName") as Label;
                        if (lbl != null)
                        {
                            lbl.ForeColor = System.Drawing.Color.Red;
                            lbl.Font.Bold = true;
                        }
                        break;
                    }
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tbarNutritionCare_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "print":
                    {
                        var jobParameters = new PrintJobParameterCollection();
                        switch (toolBarButton.Value)
                        {
                            case "SLP.10.0003":
                                {
                                    jobParameters.AddNew("p_RegistrationNo", RegistrationNo);
                                    break;
                                }
                            default:
                                {
                                    jobParameters.AddNew("p_TransactionNo", nsHD.TransactionNo);
                                    break;
                                }
                        }


                        AppSession.PrintJobParameters = jobParameters;

                        AppSession.PrintJobReportID = toolBarButton.Value;

                        ShowPrintPreview();

                        break;
                    }
                case "refresh":
                    {
                        gridFormPengkajian.Rebind();
                        gridD.Rebind();
                        break;
                    }
            }
        }
    }
}