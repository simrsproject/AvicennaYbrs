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
using Telerik.Web.UI;
using DevExpress.Data.Linq;
using System.Drawing;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class BpjsVerificationResultImport : BasePageDialog
    {
        private string _message;
        private string _coaNotRegistered;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_INVOICING;
        }

        private string GuarantorID
        {
            get { return Request.QueryString["gid"]; }
        }
        public override bool OnButtonOkClicked()
        {
            //if (!fileuploadExcel.HasFile) return true;
            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return true;
            //string targetFolder = ConfigurationManager.AppSettings["DocumentFolder"];
            //if (!System.IO.Directory.Exists(targetFolder))
            //    System.IO.Directory.CreateDirectory(targetFolder);

            //string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["DocumentFolder"], fileuploadExcel.PostedFile.FileName);

            //fileuploadExcel.SaveAs(path);

            //try
            //{
            //    var table = Common.ExcelUtil.LoadFirstSheetToDataTable(path);

            //    if (table != null && table.Rows.Count > 0)
            //    {
            //        ApplyToInvoiceDetail(table);
            //    }
            //    File.Delete(path);
            //}
            //catch (Exception ex)
            //{
            //    _message = ex.Message;
            //    File.Delete(path);
            //}

            ApplyToInvoiceDetail();

            return true;
        }

        private InvoicesItemCollection InvoicesItems
        {
            get
            {
                object obj = Session["collInvoicesItem" + Request.UserHostName];
                return ((InvoicesItemCollection)(obj));
            }
        }

        private InvoicesItemHistoryCollection InvoicesItemHistorys
        {
            get
            {
                object obj = Session["collInvoicesItemHistory" + Request.UserHostName];
                return ((InvoicesItemHistoryCollection)(obj));
            }
        }

        //private void ApplyToInvoiceDetail(DataTable table)
        //{
        //    if (InvoicesItems.Count > 0)
        //        InvoicesItems.MarkAllAsDeleted();

        //    foreach (DataRow row in table.Rows)
        //    {
        //        var reg = new Registration();
        //        var qrReg = new RegistrationQuery();
        //        qrReg.Where(qrReg.BpjsSepNo == row["SepNo"].ToString());
        //        qrReg.es.Top = 1;
        //        if (reg.Load(qrReg))
        //        {
        //            var tp = new TransPayment();
        //            var qrTp = new TransPaymentQuery();
        //            qrTp.Where(qrTp.RegistrationNo == reg.RegistrationNo && qrTp.GuarantorID == GuarantorID &&
        //                       qrTp.IsApproved == 1);
        //            qrTp.es.Top = 1;


        //            if (tp.Load(qrTp))
        //            {
        //                InvoicesItem entity = InvoicesItems.AddNew();
        //                entity.InvoiceNo = Request.QueryString["inv"];
        //                entity.PaymentNo = tp.PaymentNo;
        //                entity.PaymentDate = tp.PaymentDate;
        //                entity.RegistrationNo = reg.RegistrationNo;
        //                entity.PatientID = reg.PatientID;

        //                var pat = new Patient();
        //                pat.LoadByPrimaryKey(reg.PatientID);
        //                entity.PatientName = pat.PatientName;

        //                entity.Amount = Convert.ToDecimal(row["ApprovedAmount"]);
        //                entity.VerifyAmount = Convert.ToDecimal(row["RequestAmount"]);
        //                entity.Notes = string.Empty;

        //                entity.LastUpdateDateTime = DateTime.Now;
        //                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

        //                var guar = new Guarantor();
        //                guar.LoadByPrimaryKey(reg.GuarantorID);
        //                entity.GuarantorName = guar.GuarantorName;
        //            }
        //        }
        //    }
        //}
        private void ApplyToInvoiceDetail()
        {
            if (InvoicesItems.Count > 0)
            {
                if (AppSession.Parameter.IsSaveHistoryInImportBpjsVerification)
                {
                    if (InvoicesItemHistorys.Count > 0)
                        InvoicesItemHistorys.MarkAllAsDeleted();

                    foreach (InvoicesItem item in InvoicesItems)
                    {
                        InvoicesItemHistory entity = InvoicesItemHistorys.AddNew();
                        entity.InvoiceNo = Request.QueryString["inv"];
                        entity.PaymentNo = item.PaymentNo;
                        entity.PaymentDate = item.PaymentDate;
                        entity.RegistrationNo = item.RegistrationNo;
                        entity.PatientID = item.PatientID;
                        entity.PatientName = item.PatientName;
                        entity.Amount = item.Amount;
                        entity.VerifyAmount = item.VerifyAmount;
                        entity.Notes = string.Empty;

                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                InvoicesItems.MarkAllAsDeleted();
            }
                
            foreach (GridDataItem row in grdList.MasterTableView.Items)
            {
                var reg = new Registration();
                var qrReg = new RegistrationQuery();
                qrReg.Where(qrReg.RegistrationNo == row["RegistrationNo"].Text);
                qrReg.es.Top = 1;
                if (reg.Load(qrReg))
                {
                    var tp = new TransPayment();
                    var qrTp = new TransPaymentQuery();
                    qrTp.Where(qrTp.PaymentNo == row["Paymentno"].Text &&
                               qrTp.IsApproved == 1);
                    qrTp.es.Top = 1;

                    if (tp.Load(qrTp))
                    {
                        InvoicesItem entity = InvoicesItems.AddNew();
                        entity.InvoiceNo = Request.QueryString["inv"];
                        entity.PaymentNo = tp.PaymentNo;
                        entity.PaymentDate = tp.PaymentDate;
                        entity.RegistrationNo = reg.RegistrationNo;
                        entity.PatientID = reg.PatientID;

                        var pat = new Patient();
                        pat.LoadByPrimaryKey(reg.PatientID);
                        entity.PatientName = pat.PatientName;

                        entity.Amount = Convert.ToDecimal(row["ApprovedAmount"].Text);
                        entity.VerifyAmount = Convert.ToDecimal(row["RequestAmount"].Text);
                        entity.Notes = string.Empty;
                        entity.ClaimDifferenceAmount= Convert.ToDecimal(row["RequestAmountSys"].Text) - Convert.ToDecimal(row["ApprovedAmount"].Text);

                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        var guar = new Guarantor();
                        guar.LoadByPrimaryKey(reg.GuarantorID);
                        entity.GuarantorName = guar.GuarantorName;
                    }
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (string.IsNullOrEmpty(_message))
                return "oWnd.argument.command = 'rebind';";
            return string.Format("alert(\"{0}\");oWnd.argument.command = 'rebind';", _message);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!fileuploadExcel.HasFile) return;

            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return;
            //string targetFolder = ConfigurationManager.AppSettings["DocumentFolder"];
            //if (!System.IO.Directory.Exists(targetFolder)) System.IO.Directory.CreateDirectory(targetFolder);
            //string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["DocumentFolder"], fileuploadExcel.PostedFile.FileName);

            string tmp_doc = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpDocumentFolder);
            if (string.IsNullOrEmpty(tmp_doc))
                tmp_doc = ConfigurationManager.AppSettings["DocumentFolder"];

            if (string.IsNullOrEmpty(tmp_doc)) return;
            if (!Directory.Exists(tmp_doc))
                Directory.CreateDirectory(tmp_doc);
            string path = tmp_doc + fileuploadExcel.PostedFile.FileName;

            fileuploadExcel.SaveAs(path);

            try
            {
                var table = Common.ExcelUtil.LoadFirstSheetToDataTable(path);

                if (table != null && table.Rows.Count > 0)
                {
                    var dtb = GetImported(table);
                    grdList.DataSource = null;
                    grdList.DataSource = dtb;
                    grdList.DataBind();
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                AjaxManager.Alert(_message);
                File.Delete(path);
            }
        }

        private DataTable GetImported(DataTable table)
        {
            var dtRes = new DataTable();
            dtRes.Columns.Add("PaymentNo", typeof(string));
            dtRes.Columns.Add("RegistrationNo", typeof(string));
            dtRes.Columns.Add("GuarantorName", typeof(string));
            dtRes.Columns.Add("GuarantorCardNo", typeof(string));
            dtRes.Columns.Add("SepNo", typeof(string));
            dtRes.Columns.Add("PatientName", typeof(string));

            dtRes.Columns.Add("RequestAmountSys", typeof(decimal));
            dtRes.Columns.Add("RequestAmount", typeof(decimal));
            dtRes.Columns.Add("ApprovedAmount", typeof(decimal));

            var su = new ServiceUnit();
            if (!su.LoadByPrimaryKey(AppSession.Parameter.ServiceUnitPharmacyIdOpr)) {
                su = new ServiceUnit();
                su.ServiceUnitName = "Parameter ServiceUnitPharmacyIdOpr is not configured";
            }

            foreach (DataRow row in table.Rows)
            {
                var regNo = string.Empty;
                var patientID = string.Empty;
                var guarantorID = string.Empty;

                if (string.IsNullOrEmpty(row["SepNo"].ToString())) continue;

                var rr = dtRes.NewRow();
                rr["SepNo"] = row["SepNo"].ToString();

                if (!chkPrescTrans.Checked)
                {
                    var reg = new Registration();
                    var qrReg = new RegistrationQuery("a");
                    var qrGrr = new GuarantorQuery("b");
                    qrReg.InnerJoin(qrGrr).On(qrGrr.GuarantorID == qrReg.GuarantorID);
                    qrReg.Where(qrReg.BpjsSepNo == row["SepNo"].ToString(), qrGrr.GuarantorHeaderID == GuarantorID, qrReg.IsVoid == false);
                    qrReg.es.Top = 1;
                    if (reg.Load(qrReg))
                    {
                        rr["SepNo"] = row["SepNo"].ToString();
                        rr["GuarantorCardNo"] = reg.GuarantorCardNo;

                        regNo = reg.RegistrationNo;
                        patientID = reg.PatientID;
                        guarantorID = reg.GuarantorID;

                        //var regColl2 = new RegistrationCollection();
                        qrReg = new RegistrationQuery("reg");
                        var tp = new TransPaymentQuery("tp");
                        var tpi = new TransPaymentItemQuery("tpi");
                        var iv = new InvoicesQuery("iv");
                        var ivi = new InvoicesItemQuery("ivi");
                        var guar = new GuarantorQuery("guar");

                        qrReg.InnerJoin(tp).On(qrReg.RegistrationNo == tp.RegistrationNo && tp.IsVoid == false)
                            .InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo && tpi.SRPaymentType == "PaymentType-004")
                            .InnerJoin(guar).On(tp.GuarantorID == guar.GuarantorID)
                            .LeftJoin(ivi).On(tp.PaymentNo == ivi.PaymentNo && qrReg.RegistrationNo == ivi.RegistrationNo)
                            .LeftJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo && iv.IsVoid == false && iv.IsInvoicePayment == false)
                            .Where(qrReg.RegistrationNo == reg.RegistrationNo, 
                                guar.GuarantorHeaderID == GuarantorID,
                                qrReg.IsVoid == false,
                                iv.IsVoid.Coalesce("0") == false//,
                                                                //iv.InvoiceNo.IsNull()
                                )
                            .OrderBy(iv.InvoiceNo.Descending)
                            .Select(
                                qrReg, 
                                iv.InvoiceNo.As("refToClass_ClassName"),// pinjam ClassName
                                tp.PaymentNo.As("refToServiceRoom_RoomName")); // pinjam RoomName
                        qrReg.es.Top = 1;
                        if (reg.Load(qrReg))
                        {
                            if (!string.IsNullOrEmpty(reg.ClassName))
                            {
                                // kosongin reg biar gak keambil
                                rr["PatientName"] = string.Format("Error: registration {0} has been invoiced to {1}", reg.RegistrationNo, reg.ClassName);
                                reg.RegistrationNo = string.Empty;
                                regNo = string.Empty;
                            }
                            else
                            {
                                rr["PaymentNo"] = reg.RoomName;
                                rr["SepNo"] = row["SepNo"].ToString();
                                rr["GuarantorCardNo"] = reg.GuarantorCardNo;
                                regNo = reg.RegistrationNo;
                                patientID = reg.PatientID;
                                guarantorID = reg.GuarantorID;
                            }
                        }
                    }
                }
                else
                {
                    // coba baca dari nomor kartu untuk obat yang langsung beli ke apotik
                    // nomor sep dan nomor kartu belum tentu ada di nomor registrasi direct prescription
                    // baca sep sebelumnya
                    var regColl = new RegistrationCollection();
                    //var reg = new Registration();
                    var qrReg = new RegistrationQuery("reg");
                    var guar = new GuarantorQuery("guar");
                    qrReg.InnerJoin(guar).On(qrReg.GuarantorID == guar.GuarantorID && guar.SRGuarantorType == "09"  /*bpjs*/);
                    qrReg.Where(qrReg.BpjsSepNo == row["SepNo"].ToString(), qrReg.IsVoid == false)//, guar.GuarantorHeaderID == GuarantorID)
                        .OrderBy(qrReg.RegistrationDate.Descending);
                    //qrReg.es.Top = 1; 
                    if (regColl.Load(qrReg))
                    {
                        foreach (var reg in regColl) {
                            regNo = reg.RegistrationNo;
                            patientID = reg.PatientID;
                            guarantorID = reg.GuarantorID;

                            var guarCardNo = reg.GuarantorCardNo;

                            //var regColl2 = new RegistrationCollection();
                            qrReg = new RegistrationQuery("reg");
                            guar = new GuarantorQuery("guar");
                            var tp = new TransPaymentQuery("tp");
                            var tpi = new TransPaymentItemQuery("tpi");
                            var iv = new InvoicesQuery("iv");
                            var ivi = new InvoicesItemQuery("ivi");

                            qrReg.InnerJoin(guar).On(qrReg.GuarantorID == guar.GuarantorID && guar.SRGuarantorType == "09"  /*bpjs*/)
                                .InnerJoin(tp).On(qrReg.RegistrationNo == tp.RegistrationNo && tp.IsVoid == false)
                                .InnerJoin(tpi).On(tp.PaymentNo == tpi.PaymentNo && tpi.SRPaymentType == "PaymentType-004")
                                .LeftJoin(ivi).On(tp.PaymentNo == ivi.PaymentNo && qrReg.RegistrationNo == ivi.RegistrationNo)
                                .LeftJoin(iv).On(ivi.InvoiceNo == iv.InvoiceNo && iv.IsVoid == false)
                                .Where(
                                    qrReg.PatientID == reg.PatientID,
                                    qrReg.RegistrationDate >= reg.RegistrationDate,
                                    qrReg.RegistrationDate <= reg.RegistrationDate.Value.AddDays(7), /*sementara dianggap jeda sep reg poli ke reg PM max 7 hari*/
                                    qrReg.IsVoid == false,
                                    qrReg.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyIdOpr,
                                    iv.IsVoid.Coalesce("0") == false//,
                                    //iv.InvoiceNo.IsNull()
                                    )
                                .Select(qrReg, iv.InvoiceNo.As("refToClass_ClassName")) // pinjam ClassName
                                .OrderBy(qrReg.RegistrationDate.Ascending);
                            qrReg.es.Top = 1;
                            if (reg.Load(qrReg))
                            {
                                if (!string.IsNullOrEmpty(reg.ClassName))
                                {
                                    // kosongin reg biar gak keambil
                                    rr["PatientName"] = string.Format("Error: registration {0} has been invoiced to {1}", reg.RegistrationNo, reg.ClassName);
                                    reg.RegistrationNo = string.Empty;
                                    regNo = string.Empty;
                                }
                                else {
                                    rr["SepNo"] = row["SepNo"].ToString();
                                    rr["GuarantorCardNo"] = guarCardNo;
                                    regNo = reg.RegistrationNo;
                                    guarantorID = reg.GuarantorID;
                                    break;
                                }
                            }
                            else
                            {
                                // kosongin reg biar gak keambil
                                reg.RegistrationNo = string.Empty;
                                regNo = string.Empty;
                                rr["PatientName"] = string.Format("Error: no data registered to {0}", su.ServiceUnitName);

                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(regNo))
                {
                    rr["RegistrationNo"] = regNo;

                    var pat = new Patient();
                    pat.LoadByPrimaryKey(patientID);
                    rr["PatientName"] = pat.PatientName;

                    var tpi = new TransPaymentItem();
                    var tpiq = new TransPaymentItemQuery("tpi");
                    var qrTp = new TransPaymentQuery("tp");
                    var guar = new GuarantorQuery("guar");
                    tpiq.InnerJoin(qrTp).On(qrTp.PaymentNo == tpiq.PaymentNo && tpiq.SRPaymentType == "PaymentType-004")
                        .InnerJoin(guar).On(qrTp.GuarantorID == guar.GuarantorID)
                        .Where(qrTp.RegistrationNo == regNo, qrTp.IsApproved == 1); //, guar.GuarantorHeaderID == GuarantorID);
                    if (!string.IsNullOrEmpty(rr["PaymentNo"].ToString())) {
                        tpiq.Where(qrTp.PaymentNo == rr["PaymentNo"].ToString());
                    }
                    tpiq.es.Top = 1;

                    if (tpi.Load(tpiq))
                    {
                        rr["PaymentNo"] = tpi.PaymentNo;
                        rr["RequestAmountSys"] = Convert.ToDecimal(tpi.Amount);
                        rr["RequestAmount"] = Convert.ToDecimal(row["RequestAmount"]);
                        rr["ApprovedAmount"] = Convert.ToDecimal(row["ApprovedAmount"]);

                        var guar2 = new Guarantor();
                        guar2.LoadByPrimaryKey(guarantorID);
                        rr["GuarantorName"] = guar2.GuarantorName;
                    }
                }

                dtRes.Rows.Add(rr);
            }
            return dtRes;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                if (string.IsNullOrEmpty(dataItem["PaymentNo"].Text.Replace("&nbsp;","")) && 
                    (!string.IsNullOrEmpty(dataItem["PatientName"].Text.Replace("&nbsp;", ""))))
                {
                    dataItem.ForeColor = Color.Red;
                }
            }
        }
    }
}
