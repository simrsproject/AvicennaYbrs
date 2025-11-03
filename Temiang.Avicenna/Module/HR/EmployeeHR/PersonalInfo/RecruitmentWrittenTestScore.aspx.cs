using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class RecruitmentWrittenTestScore : BasePageDialog
    {

        protected bool IsNewRecord
        {
            get { return (bool)ViewState["mode"]; }
            set
            {
                ViewState["mode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private PersonalRecruitmentTestEvaluatorCollection PersonalRecruitmentTest
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["PersonalRecruitmentTestEvaluatorCollection"];
                    if (obj != null)
                        return ((PersonalRecruitmentTestEvaluatorCollection)(obj));
                }
                var coll = new PersonalRecruitmentTestEvaluatorCollection();

                var query = new PersonalRecruitmentTestEvaluatorQuery("a");
                var vwEmp = new VwEmployeeTableQuery("b");
                var pos = new PositionQuery("c");

                query.LeftJoin(vwEmp).On(query.EvaluatorID == vwEmp.PersonID);
                query.LeftJoin(pos).On(query.PositionID == pos.PositionID);



                query.Select
                    (query,
                        vwEmp.EmployeeName.As("refTo_EvaluatorName"),
                        pos.PositionName.As("refTo_PositionName")
                    );

                coll.Load(query);

                Session["PersonalRecruitmentTestEvaluatorCollection"] = coll;
                return coll;
            }
            set
            {
                Session["PersonalRecruitmentTestEvaluatorCollection"] = value;
            }
        }


        protected void grdPersonalRecruitmentTest_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPersonalRecruitmentTest.DataSource = PersonalRecruitmentTest;
        }

        protected void grdPersonalRecruitmentTest_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;
            String PersonalRecruitmentTestID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PersonalRecruitmentTestID]);
            String EvaluatorID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.EvaluatorID]);
            String PositionID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.PositionID]);
            String Score = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][PersonalRecruitmentTestEvaluatorMetadata.ColumnNames.Score]);

            PersonalRecruitmentTestEvaluator entity = FindPersonalRecruitmentTest(EvaluatorID, PositionID, Score);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                PersonalRecruitmentTest.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdPersonalRecruitmentTest_InsertCommand(object source, GridCommandEventArgs e)
        {
            PersonalRecruitmentTestEvaluator entity = PersonalRecruitmentTest.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdPersonalRecruitmentTest.Rebind();
        }


        private PersonalRecruitmentTestEvaluator FindPersonalRecruitmentTest(String EvaluatorID, String PositionID, string Score)
        {
            PersonalRecruitmentTestEvaluatorCollection coll = PersonalRecruitmentTest;
            PersonalRecruitmentTestEvaluator retEntity = null;
            foreach (PersonalRecruitmentTestEvaluator rec in coll)
            {
                if (rec.EvaluatorID.Equals(EvaluatorID) && rec.PositionID.Equals(PositionID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;

            //return ((ServiceUnitItemServiceCompMappingCollection)Session["ServiceUnitItemServiceCompMappingCollection"]).FindByPrimaryKey(txtServiceUnitID.Text, txtItemID.Text, componentID, cboRegType.SelectedValue);
        }



        private void SetEntityValue(PersonalRecruitmentTestEvaluator entity, GridCommandEventArgs e)
        {

            var userControl = (RecruitmentWrittenTestScoreDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                {
                    entity.PersonalRecruitmentTestID = userControl.PersonalRecruitmentTestID;
                    entity.EvaluatorID = userControl.EvaluatorID ;
                    entity.EvaluatorName = userControl.EvaluatorName;
                    entity.PositionID = userControl.PositionID;
                    entity.PositionName = userControl.PositionName;
                    entity.Score = userControl.Score;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;




            bool retval = IsNewRecord ;

            return retval;
        }


        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = 'rebind'";
        }
    }
}