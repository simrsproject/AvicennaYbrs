using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;

namespace Temiang.Avicenna.Module.Finance.Tariff
{
    public partial class ItemTariffRequestImport : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = getPageID == "import" ? AppConstant.Program.ItemServiceTariffRequestImport : AppConstant.Program.ItemServiceTariffRequestImportNew;
        }

        public override bool OnButtonOkClicked()
        {
            if (!fileuploadExcel.HasFile) return true;

            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return true;
            //if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"])) Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            //string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;

            string tmp_doc = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpDocumentFolder);
            if (string.IsNullOrEmpty(tmp_doc))
                tmp_doc = ConfigurationManager.AppSettings["DocumentFolder"];

            if (string.IsNullOrEmpty(tmp_doc)) return true;
            if (!Directory.Exists(tmp_doc))
                Directory.CreateDirectory(tmp_doc);
            string path = tmp_doc + fileuploadExcel.PostedFile.FileName;

            fileuploadExcel.SaveAs(path);

            try
            {
                DataTable table = Common.CreateExcelFile.LoadExcelFileToDataTable(path);

                if (table != null && table.Rows.Count > 0) 
                {
                    var refNo = table.Rows[0]["ReferenceNo"].ToString();
                    ItemTariffRequestItemToImport.DeletePrevRecord(refNo);

                    var coll = new ItemTariffRequestItemToImportCollection();

                    if (getPageID == "import")
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (!(row["NewPrice"] is DBNull) && Convert.ToDecimal(row["NewPrice"]) > 0)
                            {
                                var x = coll.AddNew();
                                x.ReferenceNo = row["ReferenceNo"].ToString();
                                x.StartingDate = Convert.ToDateTime(row["StartingDate"]);
                                x.SRTariffType = row["SRTariffType"].ToString();
                                x.ItemGroupID = row["ItemGroupID"].ToString();
                                x.ItemID = row["ItemID"].ToString();
                                x.ClassID = row["ClassID"].ToString();
                                x.ItemName = row["ItemName"].ToString();
                                x.TariffComponentID = row["TariffComponentID"].ToString();
                                x.GeneralPrice = Convert.ToDecimal(row["GeneralPrice"]);
                                x.OldPrice = Convert.ToDecimal(row["OldPrice"]);
                                x.NewPrice = Convert.ToDecimal(row["NewPrice"]);

                                x.LastUpdateDateTime = DateTime.Now;
                                x.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }
                        coll.Save();
                    }
                    else
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (!(row["NewPrice"] is DBNull) && Convert.ToDecimal(row["NewPrice"]) > 0)
                            {
                                var x = coll.AddNew();
                                x.ReferenceNo = row["ReferenceNo"].ToString();
                                x.StartingDate = Convert.ToDateTime(row["StartingDate"]);
                                x.SRTariffType = row["SRTariffType"].ToString();
                                x.ItemGroupID = row["ItemGroupID"].ToString();
                                x.ItemID = row["ItemID"].ToString();
                                x.ClassID = row["ClassID"].ToString();
                                x.ItemName = row["ItemName"].ToString();
                                x.TariffComponentID = row["TariffComponentID"].ToString();
                                x.GeneralPrice = Convert.ToDecimal(0);
                                x.OldPrice = Convert.ToDecimal(0);
                                x.NewPrice = Convert.ToDecimal(row["NewPrice"]);

                                x.LastUpdateDateTime = DateTime.Now;
                                x.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }
                        }

                        coll.Save();
                    }
                }
                File.Delete(path);
            }
            catch (Exception e)
            {
                var i = e.Message.ToString();
                AjaxManager.Alert(i);
                File.Delete(path);
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = '';oWnd.argument.trno = ''";
        }
    }
}
