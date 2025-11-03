using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionRankingDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;               
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtRankingName.Text = (String)DataBinder.Eval(DataItem, PositionRankingMetadata.ColumnNames.RankingName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, PositionRankingMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionRankingCollection coll = (PositionRankingCollection)Session["collPositionRanking"];

                //TODO: Betulkan cara pengecekannya
                string RankingName = txtRankingName.Text;
                bool isExist = false;
                foreach (PositionRanking item in coll)
                {
                    if (item.RankingName.Equals(RankingName))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Ranking Name: {0} has exist", RankingName);
                }
            }
        }

        #region Properties for return entry value
        public String RankingName
        {
            get { return txtRankingName.Text; }
        }

        public String Description
        {
            get { return txtDescription.Text; }
        }
        #endregion
    }
}