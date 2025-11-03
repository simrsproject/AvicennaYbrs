using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class BirthAttendantsDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRMidwivesType, AppEnum.StandardReference.MidwivesType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                return;
            }

            ViewState["IsNewRecord"] = false;

            var par = new ParamedicQuery();
            par.Select
                (
                    par.ParamedicID,
                    par.ParamedicName
                );
            par.Where(par.ParamedicID == (String)DataBinder.Eval(DataItem, BirthAttendantsRecordMetadata.ColumnNames.ParamedicID));
            cboParamedicID.DataSource = par.LoadDataTable();
            cboParamedicID.DataBind();

            cboParamedicID.SelectedValue = (String)DataBinder.Eval(DataItem, BirthAttendantsRecordMetadata.ColumnNames.ParamedicID);
            cboSRMidwivesType.SelectedValue = (String)DataBinder.Eval(DataItem, BirthAttendantsRecordMetadata.ColumnNames.SRMidwivesType);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, BirthAttendantsRecordMetadata.ColumnNames.Notes);
            
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (BirthAttendantsRecordCollection)Session["collBirthAttendantsRecord"];

                string parId = cboParamedicID.SelectedValue;
                
                bool isExist = false;
                foreach (BirthAttendantsRecord item in coll)
                {
                    if (item.ParamedicID.Equals(parId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Paramedic: {0} has exist", cboParamedicID.Text);
                }
            }
        }

        #region Properties for return entry value

        public String ParamedicId
        {
            get { return cboParamedicID.SelectedValue; }
        }

        public String ParamedicName
        {
            get { return cboParamedicID.Text; }
        }

        public String SrMidwivesType
        {
            get { return cboSRMidwivesType.SelectedValue; }
        }

        public String MidwivesType
        {
            get { return cboSRMidwivesType.Text; }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }

        #endregion

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var par = new ParamedicQuery();
            par.es.Top = 10;
            par.Select
                (
                    par.ParamedicID,
                    par.ParamedicName
                );
            par.Where
                (
                    par.Or
                        (
                            par.ParamedicID.Like(searchTextContain),
                            par.ParamedicName.Like(searchTextContain)
                        ),
                    par.IsActive == true
                );
            par.OrderBy(par.ParamedicID.Ascending);

            cboParamedicID.DataSource = par.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }
    }
}