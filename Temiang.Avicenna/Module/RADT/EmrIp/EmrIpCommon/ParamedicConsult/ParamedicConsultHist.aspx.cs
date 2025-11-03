using System;
using System.Collections.Generic;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ParamedicConsultHist : BasePageDialogList
    {

        #region QueryString Properties
        protected string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        protected string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        protected string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }

        protected string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        protected bool IsUserAddAble
        {
            get
            {
                return Request.QueryString["addable"] == "True";
            }
        }

        #endregion

        public override string UrlPageEntry
        {
            get { return "ParamedicConsultEntry.aspx"; }
        }

        public override int WidthWindowEntry
        {
            get { return 1100; }
        }
        public override int HeightWindowEntry
        {
            get { return 550; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = false;
            ToolBarMenuExport.Visible = false;
            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Consult Patient : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ", RegNo: " + RegistrationNo + ")";
                }
            }
        }
        public override string OnGetScriptToolBarNewClicking()
        {
            var script = string.Format("openWindowEntry('mod=new&patid={0}&crno={4}&regno={1}&parid={2}&addable={3}')", PatientID, RegistrationNo, ParamedicID, IsUserAddAble, string.Empty);
            return script;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo); 

            ToolBarMenuAdd.Enabled = ParamedicTeam.IsParamedicTeamStatusDpjpOrSharing(RegistrationNo, AppSession.UserLogin.ParamedicID) || ParamedicTeam.IsAllowAccessPatientWithServiceUnitParamedic(reg.ServiceUnitID, AppSession.UserLogin.ParamedicID); //selalu ambil status
        }

        protected void grdConsultHist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var curReg = new Registration();
            curReg.LoadByPrimaryKey(RegistrationNo);

            var query = new ParamedicConsultReferQuery("a");

            var parFrom = new ParamedicQuery("p1");
            query.InnerJoin(parFrom).On(query.ParamedicID == parFrom.ParamedicID);

            var parTo = new ParamedicQuery("p2");
            query.InnerJoin(parTo).On(query.ToParamedicID == parTo.ParamedicID);

            var su = new ServiceUnitQuery("su");
            query.LeftJoin(su).On(query.ToServiceUnitID == su.ServiceUnitID);

            query.Select
                (
                query,
                parFrom.ParamedicName,
                parTo.ParamedicName.As("ToParamedicName"),
                su.ServiceUnitName.As("ToServiceUnitName")
                );

            //Quick Search
            //ApplyQuickSearch(query, parTo, "ParamedicName");

            // Untuk consult ke Fisioterapi diambil terus krn perlu diisi di reg berikutnya
            //query.Where(
            //    query.Or(
            //        query.RegistrationNo == RegistrationNo,
            //        query.RegistrationNo == FromRegistrationNo,
            //        query.And(query.PatientID == curReg.PatientID, query.IsPhysiotherapy == true, query.ConsultDateTime < curReg.RegistrationDate.Value.AddDays(1))
            //        )
            //    );

            query.Where(
                query.Or(
                    query.RegistrationNo.In(MergeRegistrations),
                    query.And(query.PatientID == curReg.PatientID, query.IsPhysiotherapy == true, query.ConsultDateTime < curReg.RegistrationDate.Value.AddDays(1))
                )
            );

            query.OrderBy(query.ConsultDateTime.Descending);
            var dtb = query.LoadDataTable();

            grdConsultHist.DataSource = dtb;
        }

        private bool _isPhysiotherapy = false;
        protected void grdConsultHist_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                _isPhysiotherapy = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IsPhysiotherapy"].ToBoolean();
            }

            if (e.Item is GridNestedViewItem)
            {
                // Show Visible tab document
                var tabsNoteForm = (RadTabStrip)e.Item.FindControl("tabsNoteForm");
                tabsNoteForm.Tabs[1].Visible = _isPhysiotherapy;
            }
        }

        private DataTable ParamedicConsultForms(string consultReferNo, string toParamedicID)
        {
            var form = new QuestionFormQuery("a");
            form.Where(form.IsActive == true, form.SRQuestionFormType == QuestionForm.QuestionFormType.Physiotherapy);
            form.Select(form.QuestionFormID, form.QuestionFormName, form.IsSingleEntry.Coalesce("0").As("IsSingleEntry"), form.RestrictionUserType);

            var dtb = form.LoadDataTable();
            dtb.Columns.Add("RegistrationNo", typeof(string));
            dtb.Columns.Add("ToParamedicID", typeof(string));
            dtb.Columns.Add("PhrRegistrationNo", typeof(string));
            dtb.Columns.Add("ConsultReferNo", typeof(string));
            dtb.Columns.Add("PhrCreatedByUserID", typeof(string));
            dtb.Columns.Add("PhrCreatedDateTime", typeof(DateTime));
            dtb.Columns.Add("IsNewEnable", typeof(bool));

            // Query Last PHR
            foreach (DataRow row in dtb.Rows)
            {
                row["IsNewEnable"] = AppSession.UserLogin.ParamedicID.Equals(toParamedicID);
                row["RegistrationNo"] = RegistrationNo;
                row["ConsultReferNo"] = consultReferNo;
                row["ToParamedicID"] = toParamedicID;

                var cform = new ParamedicConsultForm();
                cform.Query.es.Top = 1;
                cform.Query.OrderBy(cform.Query.TransactionNo.Descending);
                cform.Query.Where(cform.Query.ConsultReferNo == consultReferNo, cform.Query.QuestionFormID == row["QuestionFormID"].ToString());
                if (cform.Query.Load())
                {
                    var phr = new PatientHealthRecord();
                    phr.Query.es.Top = 1;
                    phr.Query.OrderBy(phr.Query.CreateDateTime.Descending);
                    phr.Query.Where(phr.Query.TransactionNo == cform.TransactionNo, phr.Query.QuestionFormID == cform.QuestionFormID);
                    if (phr.Query.Load())
                    {
                        row["PhrRegistrationNo"] = phr.RegistrationNo;
                        row["PhrCreatedByUserID"] = phr.CreateByUserID ?? phr.LastUpdateByUserID;
                        row["PhrCreatedDateTime"] = phr.CreateDateTime ?? phr.LastUpdateDateTime;

                        // Check IsSingleEntry
                        if (Convert.ToBoolean(row["IsSingleEntry"]))
                            row["IsNewEnable"] = false;
                    }
                }
            }

            return dtb;
        }

        protected string PhysiotherapyFormNewLink(GridItem container)
        {
            
            if (!AppSession.UserLogin.ParamedicID.Equals(Eval("ToParamedicID")) || !DataBinder.Eval(container.DataItem, "IsNewEnable").ToBoolean())
            {
                return string.Format("<img src=\"{0}/Images/Toolbar/new16_d.png\" />", Helper.UrlRoot());
            }

            if (Eval("RestrictionUserType") == null || string.IsNullOrWhiteSpace(Eval("RestrictionUserType").ToString()) || Eval("RestrictionUserType").ToString().Contains(AppSession.UserLogin.SRUserType))
            {
                return string.Format(
                    "<a href=\"#\" onclick=\"entryPhrFromConsult('new', '{0}','{1}','{2}','{3}','{4}'); return false;\"><img src=\"{5}/Images/Toolbar/new16.png\" border=\"0\" /></a>",
                    string.Empty, DataBinder.Eval(container.DataItem, "RegistrationNo"), DataBinder.Eval(container.DataItem, "QuestionFormID"), DataBinder.Eval(container.DataItem, "ConsultReferNo"), container.OwnerGridID, Helper.UrlRoot());

            }

            return string.Format("<img src=\"{0}/Images/Toolbar/new16_d.png\" />", Helper.UrlRoot());
        }
        protected string PhysiotherapyFormViewLink(GridItem container)
        {
            var retval = string.Format(
                "<a href=\"#\" onclick=\"entryPhrFromConsult('view', '{0}','{1}','{2}','{3}','{4}'); return false;\"><img src=\"{5}/Images/Toolbar/views16.png\" border=\"0\" /></a>",
                DataBinder.Eval(container.DataItem, "TransactionNo"), DataBinder.Eval(container.DataItem, "RegistrationNo"), DataBinder.Eval(container.DataItem, "QuestionFormID"), DataBinder.Eval(container.DataItem, "ConsultReferNo"), container.OwnerGridID, Helper.UrlRoot());

            return retval;
        }
        protected void grdPhysiotherapyForm_OnDetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "grdPhrHist":
                    {
                        var consultReferNo = dataItem.GetDataKeyValue("ConsultReferNo").ToString();
                        var questionFormID = dataItem.GetDataKeyValue("QuestionFormID").ToString();
                        var toParamedicID = dataItem.GetDataKeyValue("ToParamedicID").ToString();

                        var qr = new ParamedicConsultFormQuery("b");
                        qr.Where(qr.QuestionFormID == questionFormID, qr.ConsultReferNo == consultReferNo);
                        qr.OrderBy(qr.CreatedDateTime.Descending);

                        var dtb = qr.LoadDataTable();
                        dtb.Columns.Add("ToParamedicID", typeof(string));
                        foreach (DataRow row in dtb.Rows)
                        {
                            row["ToParamedicID"] = toParamedicID;
                        }

                        e.DetailTableView.DataSource = dtb;
                        break;
                    }

            }

        }

        protected void grdPhysiotherapyForm_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            // ref: https://www.telerik.com/forums/nestedviewtemplate-parent-item-value
            var grd = ((RadGrid)sender);
            var item = (GridNestedViewItem)grd.NamingContainer;

            string consultReferNo = item.ParentItem.GetDataKeyValue("ConsultReferNo").ToString(); //Access the DataKeyValue
            string toParamedicID = item.ParentItem.GetDataKeyValue("ToParamedicID").ToString(); //Access the DataKeyValue
            //item.ParentItem["ContactTitle"].Text="Some Text";   //Access a bound column      

            grd.DataSource = ParamedicConsultForms(consultReferNo, toParamedicID);
        }

        protected object AnswerLink(GridItem container)
        {
            var mode = AppSession.UserLogin.ParamedicID.Equals(Eval("ToParamedicID")) ? "edit" : "view";
            var icon = AppSession.UserLogin.ParamedicID.Equals(Eval("ToParamedicID")) ? "edit16.png" : "views16.png";
            return string.Format(
                "<a style=\"cursor:pointer\" onclick=\"openAnswerEntry('mod={5}&patid={0}&crno={4}&regno={1}&parid={2}')\"><img src=\"{3}/Images/Toolbar/{6}\" border=\"0\" alt=\"\"/></a>",
                PatientID, RegistrationNo, ParamedicID, Helper.UrlRoot(), Eval("ConsultReferNo"), mode, icon);
        }
    }
}
