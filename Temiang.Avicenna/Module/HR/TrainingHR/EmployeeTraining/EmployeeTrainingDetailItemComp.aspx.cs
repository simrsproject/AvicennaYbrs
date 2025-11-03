using System;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingDetailItemComp : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeTraining;


            if (!IsPostBack)
            {
                txtTrainingID.Text = Request.QueryString["tId"];

                var pps = new EmployeeTraining();
                if (pps.LoadByPrimaryKey(Request.QueryString["tId"].ToInt()))
                {
                    var query = new EmployeeTrainingQuery();
                    query.Where(query.EmployeeTrainingID == pps.EmployeeTrainingID);

                    txtEmployeeTrainingName.Text = pps.EmployeeTrainingName;
                }
                    //var unit = new EmployeeTrainingHistory();
                    //unit.LoadByPrimaryKey(txtTrainingID.Text.ToInt());
                    //txtEmployeeTrainingName.Text = unit.EventName;

                    txtPersonID.Text = Request.QueryString["pId"];
                    var item = new PersonalInfo();
                    item.LoadByPrimaryKey(txtPersonID.Text.ToInt());
                    txtPersonName.Text = item.FirstName;

                if (Request.QueryString["type"] != "view")
                {
                    (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                }


            }
        }


        private EmployeeTrainingItemCollection EmployeeTrainingItems
        {
            get
            {
                //if (IsPostBack)
                {
                    var obj = Session["collEmployeeTrainingItem" + Request.UserHostName];
                    if (obj != null)
                        return ((EmployeeTrainingItemCollection)(obj));
                }
                var coll = new EmployeeTrainingItemCollection();

                var query = new EmployeeTrainingItemQuery("a");
                var rtype = new AppStandardReferenceItemQuery("i");

                query.Select(
                    query,
                     rtype.ItemName.As("refToEmployeeTrainingItem_ComponentName")


                    );

                query.InnerJoin(rtype).On(query.SRComponentID == rtype.ItemID &&
                                          rtype.StandardReferenceID == AppEnum.StandardReference.EmployeeTrainingComponent);


                query.Where(query.EmployeeTrainingID == (string.IsNullOrEmpty(txtTrainingID.Text) ? -2 : Convert.ToInt32(txtTrainingID.Text)));

                coll.Load(query);

                Session["collEmployeeTrainingItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collEmployeeTrainingItem" + Request.UserHostName] = value;
            }
        }

        protected void grdEmployeeTrainingComp_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var ds = from d in EmployeeTrainingItems
                     where d.PersonID.Equals(txtPersonID.Text.ToInt())
                     select d;
            grdEmployeeTrainingComp.DataSource = ds;
        }

        protected void grdEmployeeTrainingComp_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null)
                return;

            String SRComponentID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeTrainingItemMetadata.ColumnNames.SRComponentID]);


            EmployeeTrainingItem entity = FindEmployeeTrainingItemComponent(SRComponentID);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                EmployeeTrainingItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdEmployeeTrainingComp_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeTrainingItem entity = EmployeeTrainingItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeTrainingComp.Rebind();
        }


        protected void grdEmployeeTrainingComp_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String subCompId =
            Convert.ToString(
                item.OwnerTableView.DataKeyValues[item.ItemIndex][
                    EmployeeTrainingItemMetadata.ColumnNames.SRComponentID]);


            EmployeeTrainingItem entity = FindEmployeeTrainingItemComponent(subCompId);
            if (entity != null) entity.MarkAsDeleted();
        }

        private EmployeeTrainingItem FindEmployeeTrainingItemComponent(String subCompId)
        {
            EmployeeTrainingItemCollection coll = EmployeeTrainingItems;
            EmployeeTrainingItem retEntity = null;
            foreach (EmployeeTrainingItem rec in coll)
            {
                if (rec.SRComponentID.Equals(subCompId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }





        private void SetEntityValue(EmployeeTrainingItem entity, GridCommandEventArgs e)
        {
            var userControl = (EmployeeTrainingDetailItemCompDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.EmployeeTrainingID = txtTrainingID.Text.ToInt();
                entity.PersonID = txtPersonID.Text.ToInt();
                entity.SRComponentID = userControl.SRComponentID;
                entity.ComponentName = userControl.ComponentName;
                entity.Price = userControl.Price;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = 'rebind'";
        }
    }
}

