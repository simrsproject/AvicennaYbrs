using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.EmployeeHR
{
    public partial class PersonalDocumentImageZoomView : BasePageDialog
    {
        private int _defSelectedIndex = 0;

        public int PersonalDocumentID
        {
            get
            {
                return Request.QueryString["id"].ToInt();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Image File Gallery";
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            // Default CurrentItemIndex
            if (!IsPostBack)
                imgGallery.CurrentItemIndex = _defSelectedIndex;
        }
        protected void imgGallery_NeedDataSource(object sender, ImageGalleryNeedDataSourceEventArgs e)
        {
            imgGallery.DataSource = PersonalDocumentDataTable();
        }

        private DataTable PersonalDocumentDataTable()
        {
            var query = new PersonalDocumentQuery("a");
            query.Where(query.PersonID == Request.QueryString["pid"].ToInt(), query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false));

            query.Select(query.PersonalDocumentID, query.DocumentDate, query.DocumentName, query.FileAttachName, query.Notes,
                "<'ImageHandler.ashx?mode=employeedoc&id=' + CONVERT(VARCHAR,PersonalDocumentID) as ImageUrl>");
            query.OrderBy(query.DocumentDate.Descending, query.PersonalDocumentID.Descending);

            var dtb = query.LoadDataTable();

            // Delete non image
            var no = 0;
            foreach (DataRow row in dtb.Rows)
            {
                var fileName = row["FileAttachName"].ToString().ToLower();
                if (fileName.Contains(".pdf") || fileName.Contains(".dcm"))
                {
                    row.Delete();
                    continue;
                }
                if (row["PersonalDocumentID"].ToInt() == PersonalDocumentID)
                {
                    // Default CurrentItemIndex
                    _defSelectedIndex = no;
                }
                no++;
            }
            dtb.AcceptChanges();
            return dtb;
        }
    }
}