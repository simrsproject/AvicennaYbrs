using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetGroupSearch : BasePageDialog
    {
        public class SearchValue
        {
            public string AssetGroupId;
            public string GroupName;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProgramID = AppConstant.Program.ASSET_GROUP;
        }

        public override bool OnButtonOkClicked()
        {
            SearchValue result;
            if (string.IsNullOrEmpty(this.txtAssetGroupID.Text) && string.IsNullOrEmpty(this.txtAssetGroupName.Text))
            {
                result = null;
            }
            else
            {
                result = new SearchValue
                             {
                                 AssetGroupId = this.txtAssetGroupID.Text, 
                                 GroupName = this.txtAssetGroupName.Text
                             };
            }

            Session[SessionNameForQuery] = result;
            return true;
        }
    }
}