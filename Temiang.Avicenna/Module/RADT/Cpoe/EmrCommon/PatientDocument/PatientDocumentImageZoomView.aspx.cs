using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PatientDocumentImageZoomView : BasePageDialog
    {
        private int _defSelectedIndex = 0;

        private List<string> _patientRelateds;
        protected List<string> PatientRelateds
        {
            get
            {
                if (_patientRelateds == null)
                {
                    _patientRelateds = Patient.PatientRelateds(PatientID);
                }

                return _patientRelateds;
            }
        }

        public int PatientDocumentID
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
            imgGallery.DataSource = PatientDocumentDataTable();
        }

        private DataTable PatientDocumentDataTable()
        {
            var query = new PatientDocumentQuery("a");

            if (PatientRelateds.Count == 1)
                query.Where(query.PatientID == PatientID);
            else
                query.Where(query.PatientID.In(PatientRelateds));

            query.Where(query.Or(query.IsDeleted.IsNull(), query.IsDeleted == false));

            query.Select(query.PatientID, query.PatientDocumentID, query.DocumentDate, query.DocumentName, query.FileAttachName, query.Notes,
                "<'ImageHandler.ashx?id=' + CONVERT(VARCHAR,PatientDocumentID) as ImageUrl>");
            query.OrderBy(query.DocumentDate.Descending, query.PatientDocumentID.Descending);

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
                if (PatientID.Equals(row["PatientID"]) && row["PatientDocumentID"].ToInt() == PatientDocumentID)
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