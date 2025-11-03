using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.IO;
using System.Configuration;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Finance.Integration.JasaRaharja
{
    public partial class JasaRaharjaUploadItem : BaseUserControl
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
                var coll = Session["jasaRaharjaUploads"] as List<WebService.JasaRaharjaUploadClass>;
                ViewState["ID"] = coll.Any() ? coll[coll.Count - 1].ID.ToInt() + 1 : 1;
                ViewState["PATH"] = string.Empty;
                return;
            }
        }

        public string ID
        {
            get { return ViewState["ID"].ToString(); }
        }

        public string NAMA_FILE
        {
            get { return fileUpload1.PostedFile.FileName; }
        }

        public string PATH
        {
            get { return ViewState["PATH"].ToString(); }
        }

        public string DESKRIPSI
        {
            get { return cboDeskripsi.SelectedValue; }
        }

        public string DESKRIPSI_NAMA
        {
            get { return cboDeskripsi.Text; }
        }

        protected void btnInsert_Click(object source, EventArgs e)
        {
            if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            //string path = Server.MapPath(string.Format("~//{0}//{1}", ConfigurationManager.AppSettings["DocumentFolder"], fileUpload1.PostedFile.FileName));
            string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileUpload1.PostedFile.FileName;
            ViewState["PATH"] = path;
            fileUpload1.SaveAs(path);
        }
    }
}