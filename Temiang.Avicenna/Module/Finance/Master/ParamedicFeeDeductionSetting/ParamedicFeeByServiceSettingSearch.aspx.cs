using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ParamedicFeeByServiceSettingSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.PhysicianFeeByServiceSetting;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRRegistrationType, AppEnum.StandardReference.RegistrationType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeCaseType, AppEnum.StandardReference.ParamedicFeeCaseType);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeIsTeam, AppEnum.StandardReference.ParamedicFeeIsTeam);
                StandardReference.InitializeIncludeSpace(cboSRParamedicFeeTeamStatus, AppEnum.StandardReference.ParamedicFeeTeamStatus);
            
                //Class
                var coll = new ClassCollection();
                coll.Query.Where(coll.Query.IsActive == true);
                coll.LoadAll();

                cboClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Class c in coll)
                {
                    cboClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ParamedicFeeByServiceSettingQuery("a");
            var srRegType = new AppStandardReferenceItemQuery("b");
            var su = new ServiceUnitQuery("c");
            var item = new ItemQuery("d");
            var cls = new ClassQuery("e");
            var srFeeCaseType = new AppStandardReferenceItemQuery("f");
            var srFeeIsTeam = new AppStandardReferenceItemQuery("g");
            var srFeeTeamStatus = new AppStandardReferenceItemQuery("h");
            query.LeftJoin(srRegType).On(query.SRRegistrationType.Equal(srRegType.ItemID) && srRegType.StandardReferenceID.Equal(AppEnum.StandardReference.RegistrationType))
                .LeftJoin(su).On(query.ServiceUnitID.Equal(su.ServiceUnitID))
                .InnerJoin(item).On(query.ItemID.Equal(item.ItemID))
                .LeftJoin(cls).On(query.ClassID.Equal(cls.ClassID))
                .LeftJoin(srFeeCaseType).On(query.SRParamedicFeeCaseType.Equal(srFeeCaseType.ItemID) && srFeeCaseType.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeCaseType))
                .LeftJoin(srFeeIsTeam).On(query.SRParamedicFeeIsTeam.Equal(srFeeIsTeam.ItemID) && srFeeIsTeam.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeIsTeam))
                .LeftJoin(srFeeTeamStatus).On(query.SRParamedicFeeTeamStatus.Equal(srFeeTeamStatus.ItemID) && srFeeTeamStatus.StandardReferenceID.Equal(AppEnum.StandardReference.ParamedicFeeTeamStatus))
                .Select
                (
                    query.Id,
                    query.SRRegistrationType,
                    srRegType.ItemName.As("RegistrationTypeName"),
                    query.ServiceUnitID,
                    su.ServiceUnitName,
                    query.ItemID,
                    item.ItemName,
                    query.ClassID,
                    cls.ClassName,
                    query.SRParamedicFeeCaseType,
                    srFeeCaseType.ItemName.As("ParamedicFeeCaseTypeName"),
                    query.SRParamedicFeeIsTeam,
                    srFeeIsTeam.ItemName.As("ParamedicFeeIsTeamName"),
                    query.SRParamedicFeeTeamStatus,
                    srFeeTeamStatus.ItemName.As("ParamedicFeeTeamStatusName"),
                    query.FeeValue,
                    query.CountMax
                )
                .OrderBy(
                    query.SRRegistrationType.Ascending,
                    query.ServiceUnitID.Ascending,
                    query.ItemID.Ascending,
                    query.ClassID.Ascending,
                    query.SRParamedicFeeCaseType.Ascending,
                    query.SRParamedicFeeIsTeam.Ascending,
                    query.SRParamedicFeeTeamStatus.Ascending
                );

            if (!string.IsNullOrEmpty(cboSRRegistrationType.SelectedValue))
            {
                string searchTextContain = string.Format("%{0}%", cboSRRegistrationType.SelectedValue);
                query.Where(query.SRRegistrationType.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(cboServiceUnit.SelectedValue))
            {
                string searchTextContain = string.Format("%{0}%", cboServiceUnit.SelectedValue);
                query.Where(query.ServiceUnitID.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(cboItem.SelectedValue))
            {
                string searchTextContain = string.Format("%{0}%", cboItem.SelectedValue);
                query.Where(query.ItemID.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(cboClass.SelectedValue))
            {
                string searchTextContain = string.Format("%{0}%", cboClass.SelectedValue);
                query.Where(query.ClassID.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(cboSRParamedicFeeCaseType.SelectedValue))
            {
                string searchTextContain = string.Format("%{0}%", cboSRParamedicFeeCaseType.SelectedValue);
                query.Where(query.SRParamedicFeeCaseType.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(cboSRParamedicFeeIsTeam.SelectedValue))
            {
                string searchTextContain = string.Format("%{0}%", cboSRParamedicFeeIsTeam.SelectedValue);
                query.Where(query.SRParamedicFeeIsTeam.Like(searchTextContain));
            }
            if (!string.IsNullOrEmpty(cboSRParamedicFeeTeamStatus.SelectedValue))
            {
                string searchTextContain = string.Format("%{0}%", cboSRParamedicFeeTeamStatus.SelectedValue);
                query.Where(query.SRParamedicFeeTeamStatus.Like(searchTextContain));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }

        protected void cboServiceUnit_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var RegistrationType = cboSRRegistrationType.SelectedValue;
            if (RegistrationType.Equals(string.Empty)) RegistrationType = "xxx";

            string searchTextContain = string.Format("%{0}%", e.Text);

            var query = new ServiceUnitQuery("a");
            var srQ = new ServiceRoomQuery("b");
            var bedQ = new BedQuery("c");
            query.InnerJoin(srQ).On(query.ServiceUnitID == srQ.ServiceUnitID);

            if (RegistrationType == AppConstant.RegistrationType.InPatient)
                query.InnerJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);
            else
                query.LeftJoin(bedQ).On(srQ.RoomID == bedQ.RoomID);

            query.Select
                (
                    query.ServiceUnitID,
                    query.ServiceUnitName
                );
            query.es.Distinct = true;
            query.OrderBy(query.ServiceUnitID.Ascending);
            query.Where
                (
                    query.ServiceUnitName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.Where(query.SRRegistrationType == RegistrationType);
            var dt = query.LoadDataTable();
            // insert empty row
            var r = dt.NewRow();
            r["ServiceUnitID"] = r["ServiceUnitName"] = string.Empty;
            dt.Rows.InsertAt(r, 0);

            cboServiceUnit.DataSource = dt;
            cboServiceUnit.DataBind();
        }

        protected void cboServiceUnit_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboItem_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var i = new ItemQuery("a");
            var isv = new ItemServiceQuery("b");
            i.InnerJoin(isv).On(i.ItemID.Equal(isv.ItemID))
                .Where(
                    i.IsActive == true,
                    i.Or(i.ItemID.Like("%" + e.Text + "%"), i.ItemName.Like("%" + e.Text + "%"))
                )
                .Select(i.ItemID, i.ItemName)
                .OrderBy(i.ItemName.Ascending);
            i.es.Top = 20;

            DataTable tbl = i.LoadDataTable();
            cboItem.DataSource = tbl;
            cboItem.DataBind();
        }

        protected void cboItem_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
    }
}
