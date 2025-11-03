using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.CustomControl
{
    public partial class MatrixCtl : UserControl
    {
        private const string SelectedName = "rightds";
        private const string SourceName = "leftds";

        private DataTable SourceMatrix
        {
            get { return (DataTable)Session[SourceName]; }
        }

        /// <summary>
        /// TextBox ID for get value matrix filter
        /// </summary>
        public string LinkTextBoxToHeader
        {
            get { return (string)ViewState["ltxth"] ?? string.Empty; }
            set { ViewState["ltxth"] = value; }
        }

        /// <summary>
        /// Entity class name for store matrix record
        /// </summary>
        public string EntityClassNameMatrix
        {
            get { return (string)ViewState["enm"] ?? string.Empty; }
            set { ViewState["enm"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool MatrixContainFieldRowIndex
        {
            get { return (bool?)ViewState["cri"] ?? false; }
            set { ViewState["cri"] = value; }
        }

        /// <summary>
        /// Entity class name for Selection Table
        /// </summary>
        public string EntityClassNameSelection
        {
            get { return (string)ViewState["ens"] ?? string.Empty; }
            set { ViewState["ens"] = value; }
        }

        /// <summary>
        /// Field Name in matrix table for link to header / parent table
        /// </summary>
        public string FieldNameLinkToHeaderTable
        {
            get { return (string)ViewState["lfnth"] ?? string.Empty; }
            set { ViewState["lfnth"] = value; }
        }

        /// <summary>
        /// Field Name in matrix table for link to Selection Table
        /// </summary>
        public string FieldNameLinkToSelectionTable
        {
            get { return (string)ViewState["lfnts"] ?? string.Empty; }
            set { ViewState["lfnts"] = value; }
        }

        /// <summary>
        /// Field name in Selection table for selected
        /// </summary>
        public string FieldNameValueInSelectionTable
        {
            get { return (string)ViewState["svfn"] ?? string.Empty; }
            set { ViewState["svfn"] = value; }
        }

        /// <summary>
        /// Field name in Selection table for description
        /// </summary>
        public string FieldNameDisplayInSelectionTable
        {
            get { return (string)ViewState["stfn"] ?? string.Empty; }
            set { ViewState["stfn"] = value; }
        }

        /// <summary>
        /// Return datatable matrix selected
        /// </summary>
        public DataTable SelectedMatrix
        {
            get { return (DataTable)Session[SelectedName]; }
        }

        protected void grdSource_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == grdSource.ClientID)
                {
                    // items are drag from selection to selected grid 
                    if ((e.DestDataItem == null && SelectedMatrix.Rows.Count == 0) ||
                        e.DestDataItem != null &&
                        e.DestDataItem.OwnerGridID == grdSelected.ClientID)
                        MoveRow(e, SourceMatrix, SelectedMatrix);
                }
            }
        }

        protected void grdSelected_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == grdSelected.ClientID)
                {
                    // items are drag from selected to source grid 
                    if ((e.DestDataItem == null && SourceMatrix.Rows.Count == 0) ||
                        e.DestDataItem != null &&
                        e.DestDataItem.OwnerGridID == grdSource.ClientID)
                    {
                        MoveRow(e, SelectedMatrix, SourceMatrix);
                    }
                    else if (e.DestDataItem != null && e.DestDataItem.OwnerGridID == grdSelected.ClientID)
                    {
                        //reorder items in grdSelected
                        var selectedDataSource = SelectedMatrix;
                        var selectedRow = GetRow(selectedDataSource, e.DestDataItem.GetDataKeyValue(FieldNameValueInSelectionTable));
                        var destinationIndex = selectedDataSource.Rows.IndexOf(selectedRow);

                        if (e.DropPosition == GridItemDropPosition.Above && e.DestDataItem.ItemIndex > e.DraggedItems[0].ItemIndex)
                            destinationIndex -= 1;

                        if (e.DropPosition == GridItemDropPosition.Below && e.DestDataItem.ItemIndex < e.DraggedItems[0].ItemIndex)
                            destinationIndex += 1;

                        var selectedToMove = selectedDataSource.Clone();
                        foreach (var draggedItem in e.DraggedItems)
                        {
                            var tmpRow = GetRow(selectedDataSource, draggedItem.GetDataKeyValue(FieldNameValueInSelectionTable));
                            if (tmpRow != null)
                            {

                                DataRow newRow = selectedToMove.NewRow();
                                CopyRowValue(tmpRow, newRow);
                                selectedToMove.Rows.Add(newRow);
                            }
                        }

                        foreach (DataRow rowToMove in selectedToMove.Rows)
                        {
                            var tmpRow = GetRow(selectedDataSource, rowToMove[FieldNameValueInSelectionTable]);
                            var newRow = selectedDataSource.NewRow();
                            CopyRowValue(tmpRow, newRow);
                            selectedDataSource.Rows.Remove(tmpRow);
                            selectedDataSource.Rows.InsertAt(newRow, destinationIndex);
                        }
                        Session[SelectedName] = selectedDataSource;
                        grdSelected.Rebind();

                        var destinationItemIndex = destinationIndex - (grdSelected.PageSize * grdSelected.CurrentPageIndex);
                        e.DestinationTableView.Items[destinationItemIndex].Selected = true;
                    }
                }
            }
        }

        private void MoveRow(GridDragDropEventArgs e, DataTable sources, DataTable destinations)
        {
            var destinationIndex = -1;
            if (e.DestDataItem != null)
            {
                var destinationRow = GetRow(destinations, e.DestDataItem.GetDataKeyValue(FieldNameValueInSelectionTable));
                destinationIndex = (destinationRow != null) ? destinations.Rows.IndexOf(destinationRow) : -1;
            }

            foreach (var draggedItem in e.DraggedItems)
            {
                var sourceRow = GetRow(sources, draggedItem.GetDataKeyValue(FieldNameValueInSelectionTable));
                if (sourceRow != null)
                {
                    var newRow = destinations.NewRow();
                    CopyRowValue(sourceRow, newRow);
                    sources.Rows.Remove(sourceRow);

                    if (destinationIndex > -1)
                        destinations.Rows.InsertAt(newRow, destinationIndex);
                    else
                        destinations.Rows.Add(newRow);
                }
            }

            grdSource.Rebind();
            grdSelected.Rebind();
        }

        private DataRow GetRow(DataTable dtb, object keyID)
        {
            return dtb.Rows.Cast<DataRow>().FirstOrDefault(row => row[FieldNameValueInSelectionTable].Equals(keyID));
        }

        protected void grdSelected_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSelected.DataSource = SelectedMatrix;
        }

        protected void grdSource_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSource.DataSource = SourceMatrix;
        }

        public void ResetDataSource()
        {
            InitializeDataSource();
            grdSource.Rebind();
            grdSelected.Rebind();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
                InitializeDataSource();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeColumn(grdSource);
            InitializeColumn(grdSelected);
        }

        private string[] _displayFields;

        private string[] DisplayFields
        {
            get
            {
                if (_displayFields == null)
                {
                    var field1 = FieldNameValueInSelectionTable;
                    var field2 = FieldNameDisplayInSelectionTable;
                    if (field2.Contains(","))
                    {
                        var fields = field2.Split(',');
                        field1 = fields[0];
                        field2 = fields[1];
                    }
                    _displayFields = new[] { field1, field2 };
                }
                return _displayFields;
            }
        }

        private void InitializeColumn(RadGrid grd)
        {
            grd.MasterTableView.DataKeyNames = new[] { FieldNameValueInSelectionTable };
            var col1 = ((GridBoundColumn)grd.Columns[1]);
            col1.DataField = DisplayFields[0];
            var col2 = ((GridBoundColumn)grd.Columns[2]);
            col2.DataField = DisplayFields[1];
        }

        public void SetFirstColumnWidth(int columnWidth)
        {
            var pixelWidth = new Unit(columnWidth, UnitType.Pixel);
            grdSource.MasterTableView.Columns[1].HeaderStyle.Width = pixelWidth;
            grdSelected.MasterTableView.Columns[1].HeaderStyle.Width = pixelWidth;
        }

        public void SetColumnCaption(string captionFirstColumn, string captionSecondColumn)
        {
            grdSource.MasterTableView.Columns[1].HeaderText = captionFirstColumn;
            grdSelected.MasterTableView.Columns[1].HeaderText = captionFirstColumn;

            grdSource.MasterTableView.Columns[2].HeaderText = captionSecondColumn;
            grdSelected.MasterTableView.Columns[2].HeaderText = captionSecondColumn;
        }

        /// <summary>
        /// Save Selected Record to Database in Matrix table
        /// </summary>
        public void SaveMatrix()
        {
            // 1. Delete record not exist in Matrix Selected Datasource
            // 2. Add record not exist but exist in Matrix Selected Datasource
            using (var trans = new esTransactionScope())
            {
                DataTable oldMatrixs = OriginalMatrix(), newMatrixs = SelectedMatrix;
                var headerId = ((RadInputControl)Helper.FindControlRecursive(Page, LinkTextBoxToHeader)).Text;
                // Delete
                foreach (esEntityWAuditLog matrixEntity in oldMatrixs.Rows.Cast<DataRow>().Where(row => newMatrixs.Rows.Find(row[FieldNameValueInSelectionTable]) == null)
                                                                                          .Select(row => string.Format("{0}='{1}' AND {2}='{3}'", FieldNameLinkToHeaderTable, headerId, FieldNameLinkToSelectionTable, row[FieldNameValueInSelectionTable]))
                                                                                          .Select(filter => new { filter, matrixEntity = Utils.GetEntity(EntityClassNameMatrix) })
                                                                                          .Where(@t => @t.matrixEntity.Load(@t.filter)).Select(@t => @t.matrixEntity))
                {
                    matrixEntity.MarkAsDeleted();
                    matrixEntity.Save();
                }

                // Add new record
                var i = 1;
                foreach (DataRow row in newMatrixs.Rows)
                {
                    if (oldMatrixs.Rows.Find(row[FieldNameValueInSelectionTable]) == null)
                    {
                        var matrixEntity = Utils.GetEntity(EntityClassNameMatrix);
                        matrixEntity.SetProperty(FieldNameLinkToHeaderTable, headerId);
                        matrixEntity.SetProperty(FieldNameLinkToSelectionTable, row[FieldNameValueInSelectionTable]);
                        if (MatrixContainFieldRowIndex)
                            matrixEntity.SetProperty("RowIndex", i);
                        matrixEntity.Save();
                    }
                    else if (MatrixContainFieldRowIndex)
                    {
                        var filter = string.Format("{0}='{1}' AND {2}='{3}'", FieldNameLinkToHeaderTable, headerId, FieldNameLinkToSelectionTable, row[FieldNameValueInSelectionTable]);
                        var matrixEntity = Utils.GetEntity(EntityClassNameMatrix);
                        if (matrixEntity.Load(filter))
                        {
                            matrixEntity.SetProperty("RowIndex", i);
                            matrixEntity.Save();
                        }
                    }
                    i++;
                }

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        public DataTable OriginalMatrix()
        {
            var matrixQr = Utils.GetEntityQuery(EntityClassNameMatrix);
            string headerId = ((RadInputControl)Helper.FindControlRecursive(Page, LinkTextBoxToHeader)).Text;
            matrixQr.Where(string.Format("<{0}='{1}'>", FieldNameLinkToHeaderTable, headerId));
            matrixQr.SelectAll();

            var dtb = matrixQr.LoadDataTable();
            dtb.PrimaryKey = new[] { dtb.Columns[FieldNameValueInSelectionTable] };
            return dtb;
        }

        private void InitializeDataSource()
        {
            // Query for Left Datasource
            var query = Utils.GetEntityQuery(EntityClassNameSelection);
            query.es.JoinAlias = "a";

            var matrixQr = Utils.GetEntityQuery(EntityClassNameMatrix);
            matrixQr.es.JoinAlias = "b";

            string headerID = ((RadInputControl)Helper.FindControlRecursive(Page, LinkTextBoxToHeader)).Text;
            matrixQr.Where(string.Format("<b.{0}='{1}'>", FieldNameLinkToHeaderTable, headerID));
            matrixQr.Select(string.Format("<b.{0}>", FieldNameLinkToSelectionTable));
            var queryItem = new esQueryItem(query, FieldNameValueInSelectionTable, esSystemType.String);

            query.Where(queryItem.NotIn(matrixQr));
            if (DisplayFields[0].ToLower().Equals(FieldNameValueInSelectionTable.ToLower()))
                query.Select(string.Format("<a.{0},a.{1}>", FieldNameValueInSelectionTable, DisplayFields[1]));
            else if (DisplayFields[1].ToLower().Equals(FieldNameValueInSelectionTable.ToLower()))
                query.Select(string.Format("<a.{0},a.{1}>", FieldNameValueInSelectionTable, DisplayFields[0]));
            else
                query.Select(string.Format("<a.{0},a.{1}, a.{2}>", FieldNameValueInSelectionTable, DisplayFields[0], DisplayFields[1]));

            var dtb1 = query.LoadDataTable();
            dtb1.PrimaryKey = new[] { dtb1.Columns[FieldNameValueInSelectionTable] };

            // Query for Right Datasource
            // New Query
            query = Utils.GetEntityQuery(EntityClassNameSelection);
            query.es.JoinAlias = "a";
            matrixQr = Utils.GetEntityQuery(EntityClassNameMatrix);
            matrixQr.es.JoinAlias = "b";
            // Inner Join
            var leftField = new esQueryItem(query, FieldNameValueInSelectionTable, esSystemType.String);
            var rightField = new esQueryItem(matrixQr, FieldNameLinkToSelectionTable, esSystemType.String);
            query.InnerJoin(matrixQr).On(leftField == rightField);

            //Filter
            query.Where(string.Format("<b.{0}='{1}'>", FieldNameLinkToHeaderTable, headerID));

            //Select
            if (DisplayFields[0].ToLower().Equals(FieldNameValueInSelectionTable.ToLower()))
                query.Select(string.Format("<a.{0},a.{1}>", FieldNameValueInSelectionTable, DisplayFields[1]));
            else if (DisplayFields[1].ToLower().Equals(FieldNameValueInSelectionTable.ToLower()))
                query.Select(string.Format("<a.{0},a.{1}>", FieldNameValueInSelectionTable, DisplayFields[0]));
            else
                query.Select(string.Format("<a.{0},a.{1}, a.{2}>", FieldNameValueInSelectionTable, DisplayFields[0], DisplayFields[1]));

            if (MatrixContainFieldRowIndex)
                query.OrderBy("RowIndex", esOrderByDirection.Ascending);
            var dtb2 = query.LoadDataTable();
            dtb2.PrimaryKey = new[] { dtb2.Columns[FieldNameValueInSelectionTable] };

            Session[SourceName] = dtb1;
            Session[SelectedName] = dtb2;
        }

        protected void btnMoveRightAll_Click(object sender, EventArgs e)
        {
            MoveAllRow(SourceMatrix, SelectedMatrix);
        }

        protected void btnMoveRight_Click(object sender, EventArgs e)
        {
            var selecteds = grdSource.SelectedItems;
            MoveSelectedRow(SourceMatrix, SelectedMatrix, selecteds);
        }

        protected void btnMoveLeft_Click(object sender, EventArgs e)
        {
            var selecteds = grdSelected.SelectedItems;
            MoveSelectedRow(SelectedMatrix, SourceMatrix, selecteds);
        }

        protected void btnMoveLeftAll_Click(object sender, EventArgs e)
        {
            MoveAllRow(SelectedMatrix, SourceMatrix);
        }

        private void MoveAllRow(DataTable sources, DataTable destinations)
        {
            //Move source to list, add to destination, delete source
            var list = sources.Rows.Cast<DataRow>().ToList();
            foreach (var item in list)
            {
                var newRow = destinations.NewRow();
                CopyRowValue(item, newRow);
                destinations.Rows.Add(newRow);
                sources.Rows.Remove(item);
            }
            grdSource.Rebind();
            grdSelected.Rebind();
        }

        private void CopyRowValue(DataRow source, DataRow destination)
        {
            destination[DisplayFields[0]] = source[DisplayFields[0]];
            destination[DisplayFields[1]] = source[DisplayFields[1]];
            if (!FieldNameDisplayInSelectionTable.ToLower().Contains(FieldNameValueInSelectionTable.ToLower()))
                destination[FieldNameValueInSelectionTable] = source[FieldNameValueInSelectionTable];
        }

        private void MoveSelectedRow(DataTable sources, DataTable destinations, GridItemCollection selecteds)
        {
            foreach (GridItem item in selecteds)
            {
                var selectedRow = GetRow(sources, ((GridEditableItem)item).GetDataKeyValue(FieldNameValueInSelectionTable));
                var newRow = destinations.NewRow();
                CopyRowValue(selectedRow, newRow);
                destinations.Rows.Add(newRow);
                sources.Rows.Remove(selectedRow);
            }
            grdSource.Rebind();
            grdSelected.Rebind();
        }
    }
}