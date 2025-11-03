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

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare
{
    public partial class NursingCareCtl : BaseMainContentCtl
    {

        public RadGrid GridFormPengkajian
        {
            get { return gridFormPengkajian; }
        }
        public RadGrid GridNursingDiagnosa
        {
            get { return gridD; }
        }
        #region ASKEP
        public NursingTransHD nsHD
        {
            get
            {
                if (Session["NursingTransHD" + RegistrationNo] == null)
                {
                    Session["NursingTransHD" + RegistrationNo] = Common.NursingCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);
                }

                return (NursingTransHD)Session["NursingTransHD" + RegistrationNo];
            }
            set
            {
                Session["NursingTransHD" + RegistrationNo] = value;
            }
        }

        #region Assessment

        private DataTable FormAssessmentList()
        {
            return FormAssessmentList(null);
        }

        private DataTable FormAssessmentList(List<string> SRNsDianosaType)
        {
            var q = new PatientHealthRecordQuery("q");
            var f = new QuestionFormQuery("f");
            var nas = new NursingAssessmentTransHDQuery("nas");

            q.InnerJoin(f).On(q.QuestionFormID == f.QuestionFormID)
                .LeftJoin(nas).On(q.TransactionNo == nas.QuestionFormReference);
            q.Where(q.RegistrationNo == RegistrationNo, f.IsAskepForm == true);
            q.Select(q.TransactionNo, q.QuestionFormID, f.QuestionFormName, q.RegistrationNo, nas.Id, q.RecordDate, q.RecordTime);

            if (SRNsDianosaType != null)
            {
                if (SRNsDianosaType.Count() == 0) SRNsDianosaType.Add("xxxx"); // add sembarang biar gak nemu

                var NsType = new AppStandardReferenceItemQuery("NsType");
                q.InnerJoin(NsType).On(
                    NsType.StandardReferenceID == AppEnum.StandardReference.NsType.ToString() &&
                    f.SRNsType == NsType.ItemID && NsType.ReferenceID.In(SRNsDianosaType));
            }

            //qColl.Load(q);
            var dt = q.LoadDataTable();
            return dt;
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
            var na = new NursingAssessmentTransHD();
            if (na.LoadByPrimaryKey(System.Convert.ToInt64(no)))
            {
                var nd = new NursingAssessmentTransDTCollection();
                nd.Query.Where(nd.Query.Hdid == na.Id);
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
            var filters = new List<string>();
            if (chkNsType01.Checked) filters.Add("01");
            if (chkNsType02.Checked) filters.Add("02");
            if (chkNsType03.Checked) filters.Add("03");

            gridFormPengkajian.DataSource = FormAssessmentList(filters);
        }
        protected void gridFormPengkajian_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "ImportPHR")
            {
                var phrTransNo = e.CommandArgument.ToString();
                var hd = Common.NursingCare.SetTransHD(RegistrationNo, AppSession.UserLogin.UserID);
                Question.ImportFromAssessmentForm(
                    RegistrationNo, phrTransNo, hd.TransactionNo, AppSession.UserLogin.UserID);
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
            //var filters = ((RadGrid)source).Columns[2].ListOfFilterValues;
            var filters = new List<string>();
            if (chkNsType01.Checked) filters.Add("01");
            if (chkNsType02.Checked) filters.Add("02");
            if (chkNsType03.Checked) filters.Add("03");

            var dts = NursingDiagnosaTransDT.NursingDiagnosaFullDefinitionWithNicNoc(nsHD.TransactionNo, filters);
            //dts.Columns.Add("IsUserEditAble", typeof(bool)); // Utk hak akses icon di row grid

            foreach (System.Data.DataRow row in dts.Rows)
            {
                row["NIC"] = row["NIC"].ToString().Replace("[BASEURL]", Helper.UrlRoot());
                //row["IsUserEditAble"] = IsUserEditAble;
            }



            ((RadGrid)source).DataSource = dts;
        }
        protected void gridD_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteD")
            {
                var str = e.CommandArgument.ToString();
                // bla bla
                str = NursingDiagnosaTransDT.DeleteByIdL10(System.Convert.ToInt64(str));
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
                        var lastEval = (DateTime)DataBinder.Eval(e.Item.DataItem, "LastEvaluationDateTime");
                        var evalPeriod = (int)DataBinder.Eval(e.Item.DataItem, "EvalPeriod");
                        var periodeConvertion = (int)DataBinder.Eval(e.Item.DataItem, "PeriodConversionInHour");
                        var evalDate = lastEval.AddHours(evalPeriod * periodeConvertion);

                        var SrStatus = DataBinder.Eval(e.Item.DataItem, "SRNursingCarePlanning").ToString();

                        if (SrStatus != "01" && DateTime.Now > evalDate)
                        {
                            var lbl = e.Item.FindControl("lblNDName") as Label;
                            if (lbl != null)
                            {
                                lbl.ForeColor = System.Drawing.Color.Red;
                                lbl.Font.Bold = true;
                                //e.Item.ForeColor = System.Drawing.Color.Red;
                                //e.Item.Font.Bold = true;
                            }
                        }
                        break;
                    }
            }
        }

        protected void gridD_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            string DataField = (e.Column as IGridDataColumn).GetActiveDataField();
            var appstdref = new AppStandardReferenceItemCollection();
            appstdref.Query.Where(appstdref.Query.StandardReferenceID == AppEnum.StandardReference.NsDiagnosaType,
                appstdref.Query.IsActive == true);
            appstdref.LoadAll();
            e.ListBox.DataSource = appstdref;
            e.ListBox.DataKeyField = "ItemID";
            e.ListBox.DataTextField = "ItemName";
            e.ListBox.DataValueField = "ItemID";
            e.ListBox.DataBind();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkNsType01.Checked = false;
                chkNsType02.Checked = false;
                chkNsType03.Checked = false;

                var apstd = new AppStandardReferenceItem();
                if (apstd.LoadByPrimaryKey(AppEnum.StandardReference.UserType.ToString(),
                    string.IsNullOrEmpty(AppSession.UserLogin.SRUserType) ? string.Empty : AppSession.UserLogin.SRUserType))
                {
                    if (string.IsNullOrEmpty(apstd.CustomField))
                    {
                        chkNsType01.Checked = true;
                        chkNsType02.Checked = true;
                        chkNsType03.Checked = true;
                    }
                    else
                    {
                        var strs = apstd.CustomField.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var str in strs)
                        {
                            if (str.Contains("01")) chkNsType01.Checked = true;
                            if (str.Contains("02")) chkNsType02.Checked = true;
                            if (str.Contains("03")) chkNsType03.Checked = true;
                        }
                    }
                }

                lblSwitch01.Attributes.Add("for", chkNsType01.ClientID);
                lblSwitch02.Attributes.Add("for", chkNsType02.ClientID);
                lblSwitch03.Attributes.Add("for", chkNsType03.ClientID);

                initToolbar();
            }
        }

        protected void tbarNursingCare_ButtonClick(object sender, RadToolBarEventArgs e)
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
                        initToolbar();
                        gridFormPengkajian.Rebind();
                        gridD.Rebind();
                        break;
                    }
            }
        }

        private void initToolbar()
        {
            if (!IsUserAddAble)
            {
                var item0 = (tbarNursingCare.Items.FindItemByText("Diagnosis").Controls[0] as RadToolBarButton);
                item0.Enabled = IsUserAddAble;
                var item4 = (tbarNursingCare.Items.FindItemByText("Diagnosis").Controls[4] as RadToolBarButton);
                item4.Enabled = IsUserAddAble;
            }

            var item1 = (tbarNursingCare.Items.FindItemByText("Diagnosis").Controls[1] as RadToolBarButton);
            item1.Visible = chkNsType01.Checked;
            item1.Enabled = IsUserAddAble;

            var item2 = (tbarNursingCare.Items.FindItemByText("Diagnosis").Controls[2] as RadToolBarButton);
            item2.Visible = chkNsType02.Checked;
            item2.Enabled = IsUserAddAble;

            var item3 = (tbarNursingCare.Items.FindItemByText("Diagnosis").Controls[3] as RadToolBarButton);
            item3.Visible = chkNsType03.Checked;
            item3.Enabled = IsUserAddAble;
        }
    }
}