using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Configuration;
using System.IO;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Xml;

namespace Temiang.Avicenna.Module.Finance.Integration.JasaRaharja
{
    public partial class UploadDocumentDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.JasaRaharjaDataQuery;

            if (IsPostBack) return;

            var medic = new ParamedicCollection();
            medic.Query.Where(medic.Query.IsActive == true);
            medic.Query.Load();

            cboDokterBerwenang.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
            foreach (var m in medic)
            {
                cboDokterBerwenang.Items.Add(new Telerik.Web.UI.RadComboBoxItem(m.ParamedicName, m.ParamedicID));
            }

            var log = new BusinessObject.Interop.JasaRaharja.SendReceiveLog();
            log.Query.es2.Connection.Name = AppConstant.HIS_INTEROP.JASA_RAHAJA_INTEROP_CONNECTION_NAME;
            log.Query.es.Top = 1;
            log.Query.Where(
                log.Query.OperationType == 2,
                log.Query.RegistrationNo == Request.QueryString["regno"],
                log.Query.IsOperationSuccess == true
            );
            log.Query.OrderBy(log.Query.SendDateTime.Descending);

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            if (log.Query.Load())
            {
                var xml = new XmlDocument();
                xml.LoadXml(log.SendParameter);
                var nodes = xml.DocumentElement.ChildNodes;

                cboSifatCedera.SelectedValue = nodes[1].ChildNodes[0]["SIFAT_CEDERA"].InnerText;
                txtJenisTindakan.Text = nodes[1].ChildNodes[0]["JENIS_TINDAKAN"].InnerText;
                cboDokterBerwenang.SelectedValue = reg.ParamedicID;
                txtBiayaPerawatan.Value = double.Parse(nodes[1].ChildNodes[0]["BIAYA"].InnerText);
            }
            else
            {
                cboDokterBerwenang.SelectedValue = reg.ParamedicID;
                txtBiayaPerawatan.Value = 0;
            }

            Session["jasaRaharjaUploads"] = null;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return ViewState["isSuccess"] == null ? "oWnd.argument.isSuccess = ''" : "oWnd.argument.isSuccess = '" + ViewState["isSuccess"].ToString() + "'";
        }

        public override bool OnButtonOkClicked()
        {
            var jc = new WebService.JasaRaharjaClass();
            jc.ID_REGISTER = Request.QueryString["id"];
            jc.SIFAT_CEDERA = cboSifatCedera.SelectedValue;
            jc.JENIS_TINDAKAN = txtJenisTindakan.Text;
            jc.DOKTER_BERWENANG = cboDokterBerwenang.Text.Replace("/", string.Empty); ;
            jc.BIAYA = Convert.ToDecimal(txtBiayaPerawatan.Value);
            jc.KODE_KEJADIAN = Request.QueryString["regno"];

            var sendTemplate = string.Empty;

            foreach (var upload in JasaRaharjaUploads)
            {
                sendTemplate += string.Format(@"<attachments>
<ATTACHMENT>{0}</ATTACHMENT>
<NAMA_FILE>{1}</NAMA_FILE>
<DESKRIPSI>{2}</DESKRIPSI>
</attachments>", upload.ATTACHMENT, upload.NAMA_FILE, upload.DESKRIPSI);
            }

            var svc = new WebService.JasaRaharja();
            var isSuccess = svc.UPLOAD_DOCUMENT(jc, sendTemplate);

            ViewState["isSuccess"] = string.IsNullOrEmpty(sendTemplate) ? string.Empty : isSuccess ? "true" : "false";

            return true;
        }

        private List<WebService.JasaRaharjaUploadClass> JasaRaharjaUploads
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["jasaRaharjaUploads"];
                    if (obj != null) return ((List<WebService.JasaRaharjaUploadClass>)(obj));
                }

                var list = new List<WebService.JasaRaharjaUploadClass>();
                Session["jasaRaharjaUploads"] = list;
                return list;
            }
            set
            {
                Session["jasaRaharjaUploads"] = value;
            }
        }

        #region Record Detail Method Function

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = JasaRaharjaUploads;
        }

        protected void grdList_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String sequenceNo = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);
            WebService.JasaRaharjaUploadClass entity = FindJasaRaharjaUploadClass(sequenceNo);
            if (entity != null) SetEntityValue(entity, e);
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ID"]);
            WebService.JasaRaharjaUploadClass entity = FindJasaRaharjaUploadClass(sequenceNo);
            if (entity != null) JasaRaharjaUploads.Remove(entity);
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            WebService.JasaRaharjaUploadClass entity = new WebService.JasaRaharjaUploadClass();
            SetEntityValue(entity, e);
            JasaRaharjaUploads.Add(entity);
        }

        private WebService.JasaRaharjaUploadClass FindJasaRaharjaUploadClass(String id)
        {
            WebService.JasaRaharjaUploadClass retEntity = null;
            foreach (var rec in JasaRaharjaUploads)
            {
                if (rec.ID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(WebService.JasaRaharjaUploadClass entity, GridCommandEventArgs e)
        {
            JasaRaharjaUploadItem userControl = (JasaRaharjaUploadItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.ID = userControl.ID;
                entity.NAMA_FILE = userControl.NAMA_FILE;

                byte[] pdfBytes = File.ReadAllBytes(userControl.PATH);
                string pdfBase64 = Convert.ToBase64String(pdfBytes);

                entity.ATTACHMENT = pdfBase64;

                File.Delete(userControl.PATH);

                entity.DESKRIPSI = userControl.DESKRIPSI;
                entity.DESKRIPSI_NAMA = userControl.DESKRIPSI_NAMA;
                entity.SEND_DATETIME = DateTime.Now;
            }
        }

        #endregion
    }
}
