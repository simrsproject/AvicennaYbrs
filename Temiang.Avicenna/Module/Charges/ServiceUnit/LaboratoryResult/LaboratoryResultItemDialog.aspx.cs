using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class LaboratoryResultItemDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LaboratoryResult;

            if (!IsPostBack)
            {
                var group = new ItemGroupCollection();
                group.Query.Where(group.Query.IsActive == true,
                                  group.Query.SRItemType == BusinessObject.Reference.ItemType.Laboratory);
                group.LoadAll();

                cboItemGroupID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ItemGroup entity in group)
                {
                    cboItemGroupID.Items.Add(new RadComboBoxItem(entity.ItemGroupName, entity.ItemGroupID));
                }

                StandardReference.InitializeIncludeSpace(cboSRLabUnit, AppEnum.StandardReference.LaboratoryUnit);

                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["id"]);

                cboItemGroupID.SelectedValue = item.ItemGroupID;

                txtItemName.Text = string.Format("{0} - {1}", item.ItemID, item.ItemName);

                var lab = new ItemLaboratory();
                lab.LoadByPrimaryKey(Request.QueryString["id"]);
                cboSRLabUnit.SelectedValue = lab.SRLaboratoryUnit;

                PopulateItemResultGrid();
            }
        }

        #region ItemResult

        private ItemLaboratoryDetailCollection ItemResults
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collItemLaboratoryDetail"];
                    if (obj != null) return ((ItemLaboratoryDetailCollection)(obj));
                }

                ItemLaboratoryDetailCollection coll = new ItemLaboratoryDetailCollection();

                ItemLaboratoryDetailQuery query = new ItemLaboratoryDetailQuery("a");
                AppStandardReferenceItemQuery item = new AppStandardReferenceItemQuery("b");
                AppStandardReferenceItemQuery item2 = new AppStandardReferenceItemQuery("c");
                QuestionAnswerSelectionQuery line = new QuestionAnswerSelectionQuery("d");

                query.Where(query.ItemID == Request.QueryString["id"]);
                query.Select(query,
                    item.ItemName.As("refToAppStandardReferenceItem_SRAgeUnit"),
                    item2.ItemName.As("refToAppStandardReferenceItem_SRAnswerType"),
                    line.QuestionAnswerSelectionText.As("refToQuestionAnswerSelection_AnswerTypeReferenceID"));
                query.InnerJoin(item).On(query.SRAgeUnit == item.ItemID && item.StandardReferenceID == AppEnum.StandardReference.AgeUnit);
                query.LeftJoin(item2).On(query.SRAnswerType == item2.ItemID && item2.StandardReferenceID == AppEnum.StandardReference.AnswerType);
                query.LeftJoin(line).On(query.AnswerTypeReferenceID == line.QuestionAnswerSelectionID);
                query.OrderBy(query.Sex.Ascending, query.SRAgeUnit.Ascending, query.AgeMin.Ascending);
                coll.Load(query);

                Session["collItemLaboratoryDetail"] = coll;
                return coll;
            }
            set { Session["collItemLaboratoryDetail"] = value; }
        }

        private void PopulateItemResultGrid()
        {
            //Display Data Detail
            ItemResults = null; //Reset Record Detail
            grdLabResult.DataSource = ItemResults; //Requery
            grdLabResult.MasterTableView.IsItemInserted = false;
            grdLabResult.MasterTableView.ClearEditItems();
            grdLabResult.DataBind();
        }

        protected void grdLabResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdLabResult.DataSource = ItemResults;
        }

        protected void grdLabResult_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String itemID = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo]);
            BusinessObject.ItemLaboratoryDetail entity = FindItemResult(itemID);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdLabResult_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemID = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][ItemLaboratoryDetailMetadata.ColumnNames.SequenceNo]);
            BusinessObject.ItemLaboratoryDetail entity = FindItemResult(itemID);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdLabResult_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.ItemLaboratoryDetail entity = ItemResults.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdLabResult.Rebind();
        }

        private BusinessObject.ItemLaboratoryDetail FindItemResult(String itemID)
        {
            ItemLaboratoryDetailCollection coll = ItemResults;
            BusinessObject.ItemLaboratoryDetail retEntity = null;
            foreach (BusinessObject.ItemLaboratoryDetail rec in coll)
            {
                if (rec.SequenceNo.Equals(itemID))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(BusinessObject.ItemLaboratoryDetail entity, GridCommandEventArgs e)
        {
            var userControl = (Temiang.Avicenna.Module.RADT.Master.ItemLaboratoryResult)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ItemID = Request.QueryString["id"];
                entity.SequenceNo = userControl.SequenceNo;
                entity.Sex = userControl.Sex;
                entity.SRAgeUnit = userControl.SRAgeUnit;
                entity.AgeUnitName = userControl.AgeUnit;
                entity.AgeMin = userControl.AgeMin;
                entity.TotalAgeMin = ItemLaboratoryDetail.CalculateTotalAge(userControl.SRAgeUnit, entity.AgeMin ?? 0);
                entity.AgeMax = userControl.AgeMax;
                entity.TotalAgeMax = ItemLaboratoryDetail.CalculateTotalAge(userControl.SRAgeUnit, entity.AgeMax ?? 0);
                entity.SRAnswerType = userControl.SRAnswerType;
                entity.AnswerTypeName = userControl.AnswerTypeName;
                entity.NormalValueMin = userControl.NormalValueMin;
                entity.NormalValueMax = userControl.NormalValueMax;
                entity.Notes = userControl.Notes;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.AnswerTypeReferenceID = userControl.AnswerTypeReferenceID;
                entity.AnswerTypeReferenceName = userControl.AnswerTypeReferenceName;
            }
        }

        #endregion

        public override bool OnButtonOkClicked()
        {
            if (string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
            {
                ShowInformationHeader("Item Group is required.");
                return false;
            }

            using (var trans = new esTransactionScope())
            {
                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["id"]);
                item.ItemGroupID = cboItemGroupID.SelectedValue;
                item.Save();

                var lab = new ItemLaboratory();
                lab.LoadByPrimaryKey(Request.QueryString["id"]);
                lab.SRLaboratoryUnit = cboSRLabUnit.SelectedValue;
                lab.Save();

                ItemResults.Save();

                var profile = new ItemLaboratoryProfileCollection();
                profile.Query.Where(profile.Query.ParentItemID == Request.QueryString["id"]);
                if (profile.Query.Load())
                {
                    foreach (var p in profile)
                    {
                        var i = new Item();
                        i.LoadByPrimaryKey(p.DetailItemID);
                        i.ItemGroupID = cboItemGroupID.SelectedValue;
                        i.Save();
                    }
                }

                trans.Complete();
            }
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = 'rebind'";
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemLaboratoryProfiles(Request.QueryString["id"]);
        }

        private DataTable ItemLaboratoryProfiles(string parentItemID)
        {
            var profile = new ItemLaboratoryProfileQuery("a");
            var item = new ItemQuery("b");
            var lab = new ItemLaboratoryQuery("c");

            profile.Select(
                profile.ParentItemID,
                profile.DetailItemID,
                item.ItemName.As("DetailItemName"),
                profile.DisplaySequence
                );
            profile.InnerJoin(item).On(profile.DetailItemID == item.ItemID);
            profile.InnerJoin(lab).On(profile.DetailItemID == lab.ItemID);
            profile.Where(profile.ParentItemID == parentItemID);
            return profile.LoadDataTable();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.DataSource = ItemLaboratoryProfiles(e.DetailTableView.ParentItem.GetDataKeyValue("DetailItemID").ToString());
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "order")
            {
                var args = e.CommandArgument.ToString().Split('|');

                var profiles = new ItemLaboratoryProfileCollection();
                profiles.Query.Where(profiles.Query.ParentItemID == args[0]);
                profiles.Query.Load();

                var profile1 = profiles.FindByPrimaryKey(args[0], args[1]);
                if (profile1.DisplaySequence == 0) return;
                else
                {
                    var profile2 = profiles.Where(p => p.DisplaySequence == profile1.DisplaySequence - 1).Take(1).SingleOrDefault();
                    if (profile2 != null) profile2.DisplaySequence += 1;

                    profile1.DisplaySequence -= 1;

                    profiles.Save();

                    if (args[2] == "0") grdList.Rebind();
                    else e.Item.OwnerTableView.Rebind();
                }
            }
        }
    }
}
