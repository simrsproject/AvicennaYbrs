using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class EmployeePositionDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtEmployeePositionID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeePositionID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.EmployeePositionID));
            PopulatecboPositionID(cboPositionID, (int)DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.PositionID));
            chkIsPrimaryPosition.Checked = (bool)DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.IsPrimaryPosition);
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.ValidTo);
            txtAssignmentNo.Text = (string)DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.AssignmentNo);
            txtResignmentNo.Text = (string)DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.ResignmentNo);
            cboCoorporateGradeID_ItemsRequested(cboCoorporateGradeID, new RadComboBoxItemsRequestedEventArgs());
            var cgid = DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.CoorporateGradeID);
            cboCoorporateGradeID.SelectedValue = (cgid == null) ? "" : cgid.ToString();
            txtCoorporateGradeValue.DbValue = DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.CoorporateGradeValue);
            txtPositionDescription.Text = (string)DataBinder.Eval(DataItem, EmployeePositionMetadata.ColumnNames.PositionDescription);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chkIsPrimaryPosition.Checked)
            {
                var coll = (EmployeePositionCollection)Session["collEmployeePosition" + Request.UserHostName + PageId];
                bool isExist = false;
                //Check duplicate key
                if (ViewState["IsNewRecord"].Equals(true))
                {
                    foreach (EmployeePosition item in coll)
                    {
                        if (item.IsPrimaryPosition ?? false)
                        {
                            isExist = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (EmployeePosition item in coll)
                    {
                        if (item.IsPrimaryPosition ?? false && item.EmployeePositionID != txtEmployeePositionID.Text.ToInt())
                        {
                            isExist = true;
                            break;
                        }
                    }
                }

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Employee Primary Position has exist.");
                    return;
                }
            }
            // validate coorporategradevalue
            if (!string.IsNullOrEmpty(cboCoorporateGradeID.SelectedValue))
            {
                var cg = new CoorporateGrade();
                if (cg.LoadByPrimaryKey(Convert.ToInt32(cboCoorporateGradeID.SelectedValue)))
                {
                    if (!txtCoorporateGradeValue.Value.HasValue)
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Coorporate grade value must be filled.");
                        return;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtCoorporateGradeValue.Value.Value) < cg.CoorporateGradeMin.Value)
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Coorporate grade must be more than or equal to coorporate minimum value.");
                            return;
                        }
                        if (Convert.ToDecimal(txtCoorporateGradeValue.Value.Value) > cg.CoorporateGradeMax.Value)
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Coorporate grade must be less than or equal to coorporate maximum value.");
                            return;
                        }
                    }
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeePositionID
        {
            get { return Convert.ToInt32(txtEmployeePositionID.Text); }
        }
        public Int32 PositionID
        {
            get { return Convert.ToInt32(cboPositionID.SelectedValue); }
        }
        public string PositionName
        {
            get { return cboPositionID.Text; }
        }
        public Boolean IsPrimaryPosition
        {
            get { return chkIsPrimaryPosition.Checked; }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }

        public string AssignmentNo
        {
            get { return txtAssignmentNo.Text; }
        }
        public string ResignmentNo
        {
            get { return txtResignmentNo.Text; }
        }
        public string PositionDescription
        {
            get { return txtPositionDescription.Text; }
        }
        public Int32? CoorporateGradeID
        {
            get
            {
                if (string.IsNullOrEmpty(cboCoorporateGradeID.SelectedValue) || cboCoorporateGradeID.SelectedValue == "-1")
                {
                    return new Int32?();
                }
                else
                {
                    return Convert.ToInt32(cboCoorporateGradeID.SelectedValue);
                }
            }
        }
        public short? CoorporateGradeLevel
        {
            get
            {
                if (CoorporateGradeID.HasValue)
                {
                    var cg = new CoorporateGrade();
                    if (cg.LoadByPrimaryKey(CoorporateGradeID.Value)) return cg.CoorporateGradeLevel;
                }
                return new short?();
            }
        }
        public Decimal? CoorporateGradeValue
        {
            get
            {
                if (txtCoorporateGradeValue.Value.HasValue)
                {
                    return Convert.ToDecimal(txtCoorporateGradeValue.Value.Value);
                }
                return new Decimal?();
            }
        }
        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox 
        protected void cboPositionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboPositionID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            PositionQuery query = new PositionQuery();

            query.Where(
                query.PositionName.Like(searchTextContain));

            query.Select(query.PositionID, query.PositionCode, query.PositionName);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionID"].ToString();
            }
        }
        private void PopulatecboPositionID(RadComboBox comboBox, int positionId)
        {
            PositionQuery query = new PositionQuery();

            query.Where(query.PositionID == positionId);

            query.Select(query.PositionID, query.PositionCode, query.PositionName);

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionID"].ToString();
            }
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionCode"].ToString() + " " + ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }
        #endregion

        protected void cboCoorporateGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            if (DataBinder.Eval(e.Item.DataItem, "CoorporateGradeID").ToString() == "-1")
            {
                e.Item.Text = "";
            }
            else
            {
                e.Item.Text = string.Format("Min:{0} - Interval:{1} - Max:{2}",
                                    DataBinder.Eval(e.Item.DataItem, "CoorporateGradeMin"),
                                    DataBinder.Eval(e.Item.DataItem, "CoorporateGradeInterval"),
                                    DataBinder.Eval(e.Item.DataItem, "CoorporateGradeMax"));
            }


            e.Item.Value = DataBinder.Eval(e.Item.DataItem, "CoorporateGradeID").ToString();
        }

        protected void cboCoorporateGradeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            CoorporateGradeCollection coll = new CoorporateGradeCollection();

            coll.Query.OrderBy(coll.Query.CoorporateGradeLevel.Ascending);
            coll.LoadAll();

            var n = new CoorporateGrade();
            n.CoorporateGradeID = -1;
            coll.AttachEntity(n);

            cboCoorporateGradeID.DataSource = coll;
            cboCoorporateGradeID.DataBind();
        }
    }
}
