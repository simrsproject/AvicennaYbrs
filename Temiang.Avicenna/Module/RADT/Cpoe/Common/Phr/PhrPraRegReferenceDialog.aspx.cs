using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Emr.Phr
{
    public partial class PhrPraRegReferenceDialog : BasePageDialog
    {
        private string QuestionFormID
        {
            get { return Request.QueryString["fid"]; }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Select From Pra Registration Form";
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var form = new QuestionFormQuery("a");
            var phr = new PatientHealthRecordQuery("b");
            form.LeftJoin(phr).On(form.QuestionFormID == phr.QuestionFormID & phr.RegistrationNo == PatientID);

            form.Where(form.IsActive == true, form.SRQuestionFormType == QuestionForm.QuestionFormType.PraRegistration, form.QuestionFormID == QuestionFormID);
            form.Select("<'" + PatientID + "' as PatientID>", form.QuestionFormID, form.RmNO, form.QuestionFormName,
                phr.TransactionNo, phr.CreateDateTime, phr.CreateByUserID,
                phr.ServiceUnitID, phr.RegistrationNo, phr.IsApproved, phr.ApprovedDatetime, phr.ApprovedByUserID,
                form.RestrictionUserType, form.IsSingleEntry);

            var dtb = form.LoadDataTable();
            grdList.DataSource = dtb;
        }

        public override bool OnButtonOkClicked()
        {
            var trNo = grdList.SelectedValue.ToString();
            var phr = new PatientHealthRecord();
            if (phr.LoadByPrimaryKey(trNo, PatientID, QuestionFormID))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);

                // Ganti isi RegistrationNo yg asalanya berisi PatientID menjadi RegistrationNo
                var newPhr = new PatientHealthRecord();
                newPhr.ApprovedByUserID = phr.ApprovedByUserID;
                newPhr.ApprovedDatetime = phr.ApprovedDatetime;
                newPhr.CreateByUserID = phr.CreateByUserID;
                newPhr.CreateDateTime = phr.CreateDateTime;
                newPhr.EmployeeID = phr.EmployeeID;
                newPhr.ExaminerID = phr.ExaminerID;
                newPhr.IsApproved = phr.IsApproved;
                newPhr.IsComplete = phr.IsComplete;
                newPhr.LastUpdateByUserID = phr.LastUpdateByUserID;
                newPhr.LastUpdateDateTime = phr.LastUpdateDateTime;
                newPhr.QuestionFormID = phr.QuestionFormID;
                newPhr.RecordDate = phr.RecordDate;
                newPhr.RecordTime = phr.RecordTime;
                newPhr.ReferenceNo = phr.ReferenceNo;
                newPhr.RegistrationNo = RegistrationNo;
                newPhr.ServiceUnitID =reg.ServiceUnitID;
                newPhr.TransactionNo = phr.TransactionNo;

              
                var phriColl = new PatientHealthRecordLineCollection();
                phriColl.Query.Where(phriColl.Query.TransactionNo == trNo, phriColl.Query.QuestionFormID == QuestionFormID,
                    phriColl.Query.RegistrationNo == PatientID);
                phriColl.LoadAll();

                var newPhriColl = new PatientHealthRecordLineCollection();

                foreach (PatientHealthRecordLine phri in phriColl)
                {
                    var newPhri = newPhriColl.AddNew();
                    newPhri.BodyImage = phri.BodyImage;
                    newPhri.LastUpdateByUserID = phri.LastUpdateByUserID;
                    newPhri.LastUpdateDateTime = phri.LastUpdateDateTime;
                    newPhri.QuestionAnswerNum = phri.QuestionAnswerNum;
                    newPhri.QuestionAnswerPrefix = phri.QuestionAnswerPrefix;
                    newPhri.QuestionAnswerSelectionLineID = phri.QuestionAnswerSelectionLineID;
                    newPhri.QuestionAnswerSuffix = phri.QuestionAnswerSuffix;
                    newPhri.QuestionAnswerText = phri.QuestionAnswerText;
                    newPhri.QuestionAnswerText2 = phri.QuestionAnswerText2;
                    newPhri.QuestionFormID = phri.QuestionFormID;
                    newPhri.QuestionGroupID = phri.QuestionGroupID;
                    newPhri.QuestionID = phri.QuestionID;
                    newPhri.RegistrationNo = RegistrationNo;
                    newPhri.TransactionNo = phri.TransactionNo;
                }

                using (var tr = new esTransactionScope())
                {
                    
                    newPhr.Save();
                    phr.MarkAsDeleted();
                    phr.Save();

                    newPhriColl.Save();
                    phriColl.MarkAllAsDeleted();
                    phriColl.Save();
                    tr.Complete();
                }


                //var url = 'PhrPraRegDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regno + '&unit=' + unit + '&fid=' + fid + '&menu=su' + '&patid=' + patid + '&ccm=rebind&cet=<%=grdList.ClientID%>';

                var url = string.Format(
                    "PatientHealthRecordDetail.aspx?md=edit&id={0}&regno={1}&unit={2}&fid={3}&patid={4}&ccm=rebind&cet={5}",
                    grdList.SelectedValue,
                    reg.RegistrationNo,
                    reg.ServiceUnitID,
                    Request.QueryString["fid"],
                    Request.QueryString["patid"],
                    Request.QueryString["cet"]);
                Response.Redirect(url);
            }

            return true;
        }
    }
}
