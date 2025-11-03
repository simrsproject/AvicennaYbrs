using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class MovementHistoryEditor : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                this.lblTransactionNo.Visible = false;
                this.txtTransactionNo.Visible = true;
                return;
            }

            this.AssetId = (string)DataBinder.Eval(DataItem, "AssetId");
            this.TransactionNo = (string)DataBinder.Eval(DataItem, "AssetMovementNo");
            this.MovementDate = (DateTime)DataBinder.Eval(DataItem, "MovementDate");
            this.FromServiceUnitID = (string)DataBinder.Eval(DataItem, "FromServiceUnitId");
            this.FromLocationID = (string)DataBinder.Eval(DataItem, "FromLocationId");
            this.Description = (string)DataBinder.Eval(DataItem, "Description");
            this.ToServiceUnitID = (string)DataBinder.Eval(DataItem, "ToServiceUnitId");
            this.ToLocationID = (string)DataBinder.Eval(DataItem, "ToLocationId");

            this.lblTransactionNo.Text = this.TransactionNo;
            this.txtTransactionNo.Visible = false;
            this.lblTransactionNo.Visible = true;

            //TODO: Genereate AutoNumber
        }

        #region Method & Event
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            if (string.IsNullOrEmpty(this.TransactionNo))
            {
                ((CustomValidator)source).ErrorMessage = @"Transaction No Required.";
                return;
            }
            if (string.IsNullOrEmpty(this.FromServiceUnitID))
            {
                ((CustomValidator)source).ErrorMessage = @"From Service Unit Required.";
                return;
            }
            if (string.IsNullOrEmpty(this.FromLocationID))
            {
                ((CustomValidator)source).ErrorMessage = @"From Location Required.";
                return;
            }
            if (!(this.txtMovementDate.SelectedDate.HasValue))
            {
                ((CustomValidator)source).ErrorMessage = @"Movement Date Required.";
                return;
            }
            if (string.IsNullOrEmpty(this.ToServiceUnitID))
            {
                ((CustomValidator)source).ErrorMessage = @"To Service Unit Required.";
                return;
            }
            if (string.IsNullOrEmpty(this.ToLocationID))
            {
                ((CustomValidator)source).ErrorMessage = @"To Location Required.";
                return;
            }
            args.IsValid = true;
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            Helper.SetupComboBox(this.cboFromLocation);
            Helper.SetupComboBox(this.cboFromServiceUnit);
            Helper.SetupComboBox(this.cboToLocation);
            Helper.SetupComboBox(this.cboToServiceUnit);
            //FROM
            const int top = 20;
            this.cboFromServiceUnit.ItemsRequested += ((obj, arg) =>
            {
                string searchTextContain = string.Format("%{0}%", arg.Text);
                var query = new ServiceUnitQuery();
                query.Select(query.ServiceUnitID, query.ServiceUnitName);
                query.Where(
                    query.Or(
                        query.ServiceUnitID.Like(searchTextContain),
                        query.ServiceUnitName.Like(searchTextContain)));
                query.OrderBy(query.ServiceUnitID.Ascending);
                query.es.Top = top;
                var tbl = query.LoadDataTable();
                this.cboFromServiceUnit.DataSource = tbl;
                this.cboFromServiceUnit.DataBind();
            });
            this.cboFromServiceUnit.ItemDataBound += ((obj, arg) =>
            {
                arg.Item.Text = ((DataRowView)arg.Item.DataItem)["ServiceUnitID"] + @" - " + ((DataRowView)arg.Item.DataItem)["ServiceUnitName"];
                arg.Item.Value = ((DataRowView)arg.Item.DataItem)["ServiceUnitID"].ToString();
            });
            this.cboFromLocation.ItemsRequested += ((obj, arg) =>
            {
                string searchTextContain = string.Format("%{0}%", arg.Text);
                var query = new LocationQuery();
                query.Select(query.LocationID, query.LocationName);
                query.Where(
                    query.Or(
                        query.LocationID.Like(searchTextContain),
                        query.LocationName.Like(searchTextContain)));
                query.OrderBy(query.LocationID.Ascending);
                query.es.Top = top;
                var tbl = query.LoadDataTable();
                this.cboFromLocation.DataSource = tbl;
                this.cboFromLocation.DataBind();
            });
            this.cboFromLocation.ItemDataBound += ((obj, arg) =>
            {
                arg.Item.Text = ((DataRowView)arg.Item.DataItem)["LocationID"] + @" - " + ((DataRowView)arg.Item.DataItem)["LocationName"];
                arg.Item.Value = ((DataRowView)arg.Item.DataItem)["LocationID"].ToString();
            });
            //TO
            this.cboToServiceUnit.ItemsRequested += ((obj, arg) =>
            {
                string searchTextContain = string.Format("%{0}%", arg.Text);
                var query = new ServiceUnitQuery();
                query.Select(query.ServiceUnitID, query.ServiceUnitName);
                query.Where(
                    query.Or(
                        query.ServiceUnitID.Like(searchTextContain),
                        query.ServiceUnitName.Like(searchTextContain)));
                query.OrderBy(query.ServiceUnitID.Ascending);
                query.es.Top = top;
                var tbl = query.LoadDataTable();
                this.cboToServiceUnit.DataSource = tbl;
                this.cboToServiceUnit.DataBind();
            });
            this.cboToServiceUnit.ItemDataBound += ((obj, arg) =>
            {
                arg.Item.Text = ((DataRowView)arg.Item.DataItem)["ServiceUnitID"] + @" - " + ((DataRowView)arg.Item.DataItem)["ServiceUnitName"];
                arg.Item.Value = ((DataRowView)arg.Item.DataItem)["ServiceUnitID"].ToString();
            });
            this.cboToLocation.ItemsRequested += ((obj, arg) =>
            {
                string searchTextContain = string.Format("%{0}%", arg.Text);
                var query = new LocationQuery();
                query.Select(query.LocationID, query.LocationName);
                query.Where(
                    query.Or(
                        query.LocationID.Like(searchTextContain),
                        query.LocationName.Like(searchTextContain)));
                query.OrderBy(query.LocationID.Ascending);
                query.es.Top = top;
                var tbl = query.LoadDataTable();
                this.cboToLocation.DataSource = tbl;
                this.cboToLocation.DataBind();
            });
            this.cboToLocation.ItemDataBound += ((obj, arg) =>
            {
                arg.Item.Text = ((DataRowView)arg.Item.DataItem)["LocationID"] + @" - " + ((DataRowView)arg.Item.DataItem)["LocationName"];
                arg.Item.Value = ((DataRowView)arg.Item.DataItem)["LocationID"].ToString();
            });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Properties for return entry value
        public string AssetId
        {
            get { return this.lblAssetId.Text; }
            set { this.lblAssetId.Text = value; }
        }
        public string TransactionNo
        {
            get { return this.txtTransactionNo.Text; }
            set { this.txtTransactionNo.Text = value; }
        }
        public string FromServiceUnitID
        {
            get { return this.cboFromServiceUnit.SelectedValue; }
            set
            {
                this.SelectComboBox(this.cboFromServiceUnit, value, delegate(string selectedValue)
                {
                    var q = new ServiceUnitQuery();
                    q.Select(q.ServiceUnitID,
                             q.ServiceUnitName);
                    q.Where(q.ServiceUnitID == selectedValue);
                    return q.LoadDataTable();
                });
            }
        }
        public string FromLocationID
        {
            get { return this.cboFromLocation.SelectedValue; }
            set
            {
                this.SelectComboBox(this.cboFromLocation, value, delegate(string selectedValue)
                {
                    var q = new LocationQuery();
                    q.Select(q.LocationID, q.LocationName);
                    q.Where(q.LocationID == selectedValue);
                    return q.LoadDataTable();
                });
            }
        }
        public string Description
        {
            get { return this.txtDescription.Text; }
            set { this.txtDescription.Text = value; }
        }
        public DateTime MovementDate
        {
            get { return this.txtMovementDate.SelectedDate.Value; }
            set { this.txtMovementDate.SelectedDate = value; }
        }
        public string ToServiceUnitID
        {
            get { return this.cboToServiceUnit.SelectedValue; }
            set
            {
                this.SelectComboBox(this.cboToServiceUnit, value, delegate(string selectedValue)
                {
                    var q = new ServiceUnitQuery();
                    q.Select(q.ServiceUnitID,
                             q.ServiceUnitName);
                    q.Where(q.ServiceUnitID == selectedValue);
                    return q.LoadDataTable();
                });
            }
        }
        public string ToLocationID
        {
            get { return this.cboToLocation.SelectedValue; }
            set
            {
                this.SelectComboBox(this.cboToLocation, value, delegate(string selectedValue)
                {
                    var q = new LocationQuery();
                    q.Select(q.LocationID, q.LocationName);
                    q.Where(q.LocationID == selectedValue);
                    return q.LoadDataTable();
                });
            }
        }

        private void SelectComboBox(RadComboBox comboBox, string selectedValue, Func<string, DataTable> query)
        {
            var arg = string.Empty;
            if (!string.IsNullOrEmpty(selectedValue))
                arg = selectedValue;

            var tbl = query.Invoke(arg);
            comboBox.DataSource = tbl;
            comboBox.DataBind();
            comboBox.SelectedValue = selectedValue;
        }
        #endregion
    }
}