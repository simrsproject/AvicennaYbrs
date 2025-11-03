using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Telerik.Web.UI;
using Temiang.Avicenna.Bridging.PCare.BusinessObject;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Bridging.PCare;
using Temiang.Avicenna.Bridging.PCare.Common;
using System.Text.RegularExpressions;
using static Temiang.Avicenna.BusinessObject.VitalSign;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class ProcessDataToPCare : BasePage
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ProcessDataToPCare;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;
            }
        }

        private DataTable Registrations
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);

                var qm = new ParamedicQuery("m");
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);

                var unit = new ServiceUnitQuery("s");
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);

                var mrg = new MergeBillingQuery("b");
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);

                var guar = new GuarantorQuery("g");
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);

                var pcare = new PCareKunjunganQuery("pc");
                qr.LeftJoin(pcare).On(qr.RegistrationNo == pcare.RegistrationNo);

                qr.es.Top = AppSession.Parameter.MaxResultRecord;


                // Sub Query Check status Prescription apakah sudah approval semua ->top 1 OrderBy(transPresc.IsApproval.Ascending)
                var transPresc = new TransPrescriptionQuery("tp");
                transPresc.Select(transPresc.IsApproval);
                transPresc.Where(transPresc.RegistrationNo == qr.RegistrationNo, "<tp.IsVoid = 0>");
                transPresc.es.Top = 1;
                transPresc.OrderBy(transPresc.IsApproval.Ascending);

                // Sub Query Check status ICD-10
                var icd = new EpisodeDiagnoseQuery("icd");
                icd.Select("<CAST('1' as BIT) as IsICD>");
                icd.Where(icd.RegistrationNo == qr.RegistrationNo);
                icd.es.Top = 1;

                // Sub Query Check status ICD-10
                var soap = new RegistrationInfoMedicQuery("soap");
                soap.Select("<CAST('1' as BIT) as IsSOAP>");
                soap.Where(soap.RegistrationNo == qr.RegistrationNo);
                soap.es.Top = 1;

                var pcareKdDokter = new PCareReferenceItemMappingQuery("pcmd");
                qr.LeftJoin(pcareKdDokter).On(qr.ParamedicID == pcareKdDokter.MappingWithID & pcareKdDokter.ReferenceID == Bridging.PCare.Common.Constant.ReferenceType.Dokter.ToString());

                qr.Select
                    (
                        qp.PatientID,
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        "<CAST(1 AS BIT) AS 'IsAllowClose'>",
                        guar.GuarantorName,
                        qr.GuarantorCardNo,
                        pcare.ErrorResponse,
                        string.Format("<IsPrescription=COALESCE( ({0}),CAST('0' as BIT))>", transPresc.Parse()),
                        string.Format("<IsICD=COALESCE( ({0} AND icd.SRDiagnoseType = 'DiagnoseType-001'),CAST('0' as BIT))>", icd.Parse()),
                        string.Format("<IsSOAP=COALESCE( ({0}),CAST('0' as BIT))>", soap.Parse()), pcareKdDokter.ItemID.As("PCareKdDokter"), qr.ParamedicID
                    );



                qr.Where(pcare.IsAllObatPosted.IsNull(), pcare.IsAllTindakanPosted.IsNull(), qr.Or(pcare.IsClosed.IsNull(), pcare.IsClosed == false));

                if (!txtDate.IsEmpty)
                    qr.Where(qr.RegistrationDate == txtDate.SelectedDate);

                if (txtMedicalNo.Text != string.Empty)
                    qr.Where(qp.MedicalNo == txtMedicalNo.Text);

                if (txtRegistrationNo.Text != string.Empty)
                    qr.Where(qr.RegistrationNo == txtRegistrationNo.Text);

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + txtPatientName.Text + "%";
                    qr.Where
                         (
                             string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                         );
                }

                qr.Where
                    (
                        guar.SRGuarantorType == AppSession.Parameter.GuarantorTypeBpjsKapitasi,
                        qr.IsVoid == false
                    );

                if (!chkIsIncludeNotClosed.Checked)
                    qr.Where(qr.IsClosed == true);

                qr.OrderBy(qr.RegistrationNo.Ascending);

                var tbl = qr.LoadDataTable();

                return tbl;
            }
        }

        protected void grdRegisteredList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegisteredList.DataSource = Registrations;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdRegisteredList.Rebind();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            SelectedState(((CheckBox)sender).Checked);
        }

        private void SelectedState(bool selected)
        {
            foreach (CheckBox chkBox in grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = selected;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if ((source is RadGrid))
            {
                if (eventArgument == "process")
                {
                    // Check apakah ada registrasi ke dokter yg tidak terdaftar di PCare 
                    // Jika ada munculkan popup penggantinya
                    var emptyPCareKdDokter = string.Empty;
                    foreach (
                        GridDataItem dataItem in
                        grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>()
                            .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                    {
                        if (dataItem["PCareKdDokter"].Text == "&nbsp" || string.IsNullOrEmpty(dataItem["PCareKdDokter"].Text))
                        {
                            emptyPCareKdDokter = string.Concat(emptyPCareKdDokter, "_", dataItem["ParamedicID"].Text);
                        }
                    }

                    foreach (
                        GridDataItem dataItem in
                            grdRegisteredList.MasterTableView.Items.Cast<GridDataItem>()
                                .Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                    {
                        PostDataToPcare(dataItem["RegistrationNo"].Text);
                    }

                    grdRegisteredList.Rebind();
                }
                else if (eventArgument.Contains("closestatus"))
                {
                    var kunjunganLog = new PCareKunjungan();
                    var regno = eventArgument.Split('|')[1];
                    if (!kunjunganLog.LoadByPrimaryKey(regno))
                    {
                        kunjunganLog = new PCareKunjungan();
                        kunjunganLog.RegistrationNo = regno;
                    }
                    kunjunganLog.IsClosed = true;
                    kunjunganLog.Save();
                    grdRegisteredList.Rebind();
                }
            }
        }

        #region PCare Webservice
        private string PostDataToPcare(string registrationNo)
        {
            var pcareUtils = new Utils();
            var pcareLog = new PCareKunjungan();
            if (pcareLog.LoadByPrimaryKey(registrationNo))
            {
                if (pcareLog.NoKunjungan == null)
                {
                    PostPendaftaranAndKunjungan(registrationNo, pcareLog, pcareUtils);
                }
                else
                {
                    PostKunjunganEdit(registrationNo, pcareLog, pcareUtils);
                }
            }
            else
            {
                PostPendaftaranAndKunjungan(registrationNo, pcareLog, pcareUtils);
            }

            // Post Tindakan dan Obat
            if (!string.IsNullOrEmpty(pcareLog.NoKunjungan))
            {
                pcareLog.IsAllTindakanPosted = SendDataTindakan(pcareLog.NoKunjungan, registrationNo, pcareUtils);
                pcareLog.IsAllObatPosted = SendDataObat(pcareLog.NoKunjungan, registrationNo, pcareUtils);
            }

            pcareLog.Save();

            return string.Empty;
        }

        private static void PostPendaftaranAndKunjungan(string registrationNo, PCareKunjungan pcareLog, Utils pcareUtils)
        {
            pcareLog.RegistrationNo = registrationNo;
            pcareLog.str.PendaftaranPostData = string.Empty;
            pcareLog.str.KunjunganPostData = string.Empty;
            pcareLog.str.ErrorResponse = string.Empty;

            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            var pat = new Patient();
            if (pat.LoadByPrimaryKey(reg.PatientID) && !string.IsNullOrEmpty(pat.GuarantorCardNo))
            {
                var noKartu = reg.GuarantorCardNo ?? pat.GuarantorCardNo;
                pcareLog.NoKartu = noKartu;

                string tglDaftar = Convert.ToDateTime(reg.RegistrationDate).ToString(Constant.DateFormatPCare);
                try
                {
                    // Update status No Kartu
                    var peserta = new Peserta();
                    peserta.SaveToLocalDataBase(noKartu);
                }
                catch (Exception)
                {
                    // Nothing 
                }

                // Load Status
                var bpjsPeserta = new BpjsPeserta();
                bpjsPeserta.LoadByPrimaryKey(noKartu);

                var su = new ServiceUnit();
                su.LoadByPrimaryKey(reg.ServiceUnitID);


                // Send
                try
                {
                    // Pendaftaran belum ada webservice editnya shg caranya dg didelete dahulu
                    if (!string.IsNullOrEmpty(pcareLog.NoUrutPendaftaran))
                    {
                        // Delete
                        pcareUtils.PendaftaranDelete(noKartu, reg.RegistrationDate ?? DateTime.Today,
                            pcareLog.NoUrutPendaftaran, pcareLog.KdPoli);
                    }

                    // Set KdPoli dari master
                    var pcareMap = new PCareReferenceItemMapping();
                    pcareMap.LoadByPrimaryKey(Constant.ReferenceType.PoliFktp.ToString(), su.ServiceUnitID);
                    pcareLog.KdPoli = pcareMap.ItemID;

                    var visitType = new VisitType();
                    visitType.LoadByPrimaryKey(reg.VisitTypeID);

                    // Prepare data Post Pendaftaran
                    //TODO: Pcare visitType.IsHealthyVisit
                    //var pendaftaranPostData = InitializedPendaftaranPostData(registrationNo, tglDaftar, reg.Complaint,
                    //pcareMap.ItemID, bpjsPeserta.KdProviderPst_kdProvider, noKartu, visitType.IsHealthyVisit ?? false);
                    //pcareLog.PendaftaranPostData = JsonConvert.SerializeObject(pendaftaranPostData);

                    var pendaftaranPostData = InitializedPendaftaranPostData(registrationNo, tglDaftar, reg.Complaint, pcareMap.ItemID, bpjsPeserta.KdProviderPst_kdProvider, noKartu, false);
                    pcareLog.PendaftaranPostData = JsonConvert.SerializeObject(pendaftaranPostData);


                    // Post Pendaftaran
                    var pendaftaranResponse = pcareUtils.PendaftaranAdd(pendaftaranPostData);
                    if (pendaftaranResponse.IsOk && !string.IsNullOrEmpty(pendaftaranResponse.Response.Message))
                    {
                        pcareLog.NoUrutPendaftaran = pendaftaranResponse.Response.Message;

                        // Post Kunjungan
                        PostKunjungan(pcareLog, pcareUtils, pendaftaranPostData, reg);
                    }
                    else
                    {
                        // Jika pendaftaran gagal, dicoba hanya kirim data Kunjungan karena kemungkinan Pasien terdaftar lewat App lain spt Antrean Faskes BPJS
                        var msg = pendaftaranResponse.MetaData.MessageDescription.ToLower();
                        if (msg.Contains("sudah") && msg.Contains("entri") && msg.Contains("sama"))
                        {
                            try
                            {
                                pcareLog.ErrorResponse = string.Empty;
                                PostKunjungan(pcareLog, pcareUtils, pendaftaranPostData, reg);
                            }
                            catch (Exception ex)
                            {
                                pcareLog.ErrorResponse = ex.Message;
                            }
                        }
                        else
                            pcareLog.ErrorResponse = pendaftaranResponse.MetaData.MessageDescription;
                    }
                }
                catch (Exception ex)
                {
                    pcareLog.ErrorResponse = ex.Message;
                }

            }
            else
            {
                pcareLog.ErrorResponse = "Guarantor Card No still empty";
            }
        }

        private static void PostKunjunganEdit(string registrationNo, PCareKunjungan pcareLog, Utils pcareUtils)
        {
            pcareLog.str.KunjunganPostData = string.Empty;
            pcareLog.str.ErrorResponse = string.Empty;

            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            // Send
            try
            {
                // Post Kunjungan
                var pendaftaranPostData = JsonConvert.DeserializeObject<PendaftaranPost>(pcareLog.PendaftaranPostData);
                PostKunjungan(pcareLog, pcareUtils, pendaftaranPostData, reg);
            }
            catch (Exception ex)
            {
                pcareLog.ErrorResponse = ex.Message;
            }
        }

        private static void PostKunjungan(PCareKunjungan pcareLog, Utils pcareUtils, PendaftaranPost pendaftaranPostData, Registration reg)
        {
            var registrationNo = reg.RegistrationNo;

            // Cek apakah dokernya merupakan dokter pengganti
            var paramedicID = reg.ParamedicID;
            //TODO: Pcare ParamedicSupportLog
            //var log = new ParamedicSupportLog();
            //if (log.LoadByPrimaryKey(reg.RegistrationDate ?? DateTime.Today, reg.SRShift, reg.ParamedicID))
            //{
            //    paramedicID = log.SupportForParamedicID;
            //}


            var kunjunganPostData = InitializedKunjunganPostData(pendaftaranPostData, paramedicID, registrationNo);
            pcareLog.KunjunganPostData = JsonConvert.SerializeObject(kunjunganPostData);

            // Post Kunjungan
            PostResponse kunjunganResponse;
            if (pcareLog.NoKunjungan == null)
            {
                // Add
                kunjunganResponse = pcareUtils.KunjunganAdd(kunjunganPostData);
            }
            else
            {
                // Edit
                kunjunganPostData.NoKunjungan = pcareLog.NoKunjungan;
                kunjunganResponse = pcareUtils.KunjunganEdit(kunjunganPostData);
            }
            if (kunjunganResponse.IsOk)
                pcareLog.NoKunjungan = kunjunganResponse.Response.Message;
            else
                pcareLog.ErrorResponse = kunjunganResponse.MetaData.MessageDescription;
        }

        #region post tindakan
        private void DeleteDataTindakan(string noKunjungan, Temiang.Avicenna.Bridging.PCare.Utils pcareUtils)
        {
            var getResponse = pcareUtils.KunjunganTindakanGet(noKunjungan);
            if (getResponse.Response != null)
                foreach (var item in getResponse.Response.List)
                {
                    //Delete
                    var delResponse = pcareUtils.KunjunganTindakanDelete(item.KdTindakanSk, noKunjungan);
                }
        }
        private bool SendDataTindakan(string noKunjungan, string registrationNo, Utils pcareUtils)
        {
            // Check jika tindakan sudah dikirim maka delete dulu
            var pcareTindakanLog = new PCareKunjunganTindakanCollection();
            pcareTindakanLog.Query.Where(pcareTindakanLog.Query.RegistrationNo == registrationNo);
            if (pcareTindakanLog.LoadAll())
            {
                foreach (var obat in pcareTindakanLog)
                {
                    obat.MarkAsDeleted();
                }
                pcareTindakanLog.Save();

                DeleteDataTindakan(noKunjungan, pcareUtils);
            }

            var charges = new TransChargesQuery("h");
            var chargesItem = new TransChargesItemQuery("d");
            var itemServ = new ItemServiceQuery("serv");
            var item = new ItemQuery("i");
            chargesItem.InnerJoin(itemServ).On(chargesItem.ItemID == itemServ.ItemID);
            chargesItem.InnerJoin(item).On(chargesItem.ItemID == item.ItemID);
            chargesItem.InnerJoin(charges).On(chargesItem.TransactionNo == charges.TransactionNo);

            var pcareMap = new PCareReferenceItemMappingQuery("pcmt");
            chargesItem.LeftJoin(pcareMap).On(chargesItem.ItemID == pcareMap.MappingWithID & pcareMap.ReferenceID == Bridging.PCare.Common.Constant.ReferenceType.Tindakan.ToString());

            chargesItem.Where(charges.IsApproved == true, charges.RegistrationNo == registrationNo);
            chargesItem.Select(chargesItem.TransactionNo, chargesItem.SequenceNo, chargesItem.Price, chargesItem.Notes, pcareMap.ItemID.As("PCareKdTindakan"), item.ItemName);
            var dtb = chargesItem.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                if (row["PCareKdTindakan"] == DBNull.Value) continue;
                var tindakan = new TindakanPost
                {
                    KdTindakanSK = 0,
                    NoKunjungan = noKunjungan,
                    KdTindakan = row["PCareKdTindakan"].ToString(),
                    Biaya = (decimal)row["Price"],
                    Keterangan = string.Format("{0} {1}", row["ItemName"], row["Notes"]),
                    Hasil = 0,
                };
                var response = pcareUtils.KunjunganTindakanAdd(tindakan);

                // Save history
                var pcareTindakan = new PCareKunjunganTindakan
                {
                    TransactionNo = row["TransactionNo"].ToString(),
                    SequenceNo = row["SequenceNo"].ToString(),
                    RegistrationNo = registrationNo,
                    NoKunjungan = noKunjungan
                };

                if (response.IsOk)
                {
                    pcareTindakan.KdTindakanSK = response.Response.Message.ToInt();
                }
                else
                {
                    pcareTindakan.ErrorResponse = JsonConvert.SerializeObject(response);
                }
                pcareTindakan.Save();
            }

            return true;
        }
        #endregion

        #region post obat
        private KunjunganObatPost ObatDpho(string noKunjungan, DataRow row)
        {
            // Obat non DPHO (tidak terdaftar di master obat DPHO PCare)
            var isRacikan = row["IsCompound"] != DBNull.Value && Convert.ToBoolean(row["IsCompound"]) == true;
            var consumeMethod = new ConsumeMethod();
            consumeMethod.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());

            var obat = new KunjunganObatPost
            {
                KdObatSK = 0,
                NoKunjungan = noKunjungan,
                Racikan = isRacikan,
                KdRacikan = isRacikan ? (row["ParentNo"] != DBNull.Value ? row["ParentNo"].ToString() : row["SequenceNo"].ToString()) : null,
                ObatDPHO = true,
                KdObat = row["PCareKdObat"].ToString(),
                //Signa1 = Convert.ToString(consumeMethod.Sygna ?? 1), //todo: filed Sygna hilang
                Signa1 = "1",
                Signa2 = row["ConsumeQty"].ToString(),
                JmlObat = isRacikan ? 1 : Convert.ToDecimal(row["TakenQty"]),
                JmlPermintaan = Convert.ToDecimal(row["ResultQty"]),
                NmObatNonDPHO = ""
            };
            return obat;
        }
        private KunjunganObatPost ObatNonDpho(string noKunjungan, DataRow row)
        {
            var isRacikan = row["IsCompound"] != DBNull.Value && Convert.ToBoolean(row["IsCompound"]) == true;
            var consumeMethod = new ConsumeMethod();
            consumeMethod.LoadByPrimaryKey(row["SRConsumeMethod"].ToString());

            var obat = new KunjunganObatPost
            {
                KdObatSK = 0,
                NoKunjungan = noKunjungan,
                Racikan = isRacikan,
                KdRacikan = isRacikan ? (row["ParentNo"] != DBNull.Value ? row["ParentNo"].ToString() : row["SequenceNo"].ToString()) : null,
                ObatDPHO = false,
                KdObat = row["ItemID"].ToString(),
                //Signa1 = Convert.ToString(consumeMethod.SygnaText ?? 1),
                Signa1 = "1",
                Signa2 = row["ConsumeQty"].ToString(),
                JmlObat = isRacikan ? 1 : Convert.ToDecimal(row["TakenQty"]),
                JmlPermintaan = Convert.ToDecimal(row["ResultQty"]),
                NmObatNonDPHO = row["ItemName"].ToString()
            };
            return obat;
        }

        private void DeleteDataObat(string noKunjungan, Temiang.Avicenna.Bridging.PCare.Utils pcareUtils)
        {
            var getResponse = pcareUtils.KunjunganObatGet(noKunjungan);
            if (getResponse.Response != null)
                foreach (var item in getResponse.Response.List)
                {
                    //Delete
                    var delResponse = pcareUtils.KunjunganObatDelete(item.KdObatSk, noKunjungan);
                }
        }

        private bool SendDataObat(string noKunjungan, string registrationNo, Temiang.Avicenna.Bridging.PCare.Utils pcareUtils)
        {
            #region sample data obat dari pcare
            //Daftar Pemberian Obat No Kunjungan : 0135B2360616Y000002
            //{
            //  "response": {
            //    "count": 4,
            //    "list": [

            // Non Racikan

            //      {
            //        "kdObatSK": 84291535,
            //        "kdRacikan": "N",
            //        "obat": {
            //          "kdObat": "NDPHO.03",
            //          "nmObat": "Non Racik",
            //          "sedia": 0
            //        },
            //        "signa1": 3.0,
            //        "signa2": 2.0,
            //        "jmlObat": 6.0,
            //        "jmlHari": 1.0,
            //        "kekuatan": 0.0,
            //        "jmlPermintaan": 0.0,
            //        "jmlObatRacikan": 0.0
            //      },
            //      {
            //        "kdObatSK": 84291827,
            //        "kdRacikan": "N",
            //        "obat": {
            //          "kdObat": "NDPHO.04",
            //          "nmObat": "NOn Racik 2",
            //          "sedia": 0
            //        },
            //        "signa1": 1.0,
            //        "signa2": 1.0,
            //        "jmlObat": 1.0,
            //        "jmlHari": 1.0,
            //        "kekuatan": 0.0,
            //        "jmlPermintaan": 0.0,
            //        "jmlObatRacikan": 0.0
            //      },

            // Racikan 

            //      {
            //        "kdObatSK": 84291114,
            //        "kdRacikan": "R.01",
            //        "obat": {
            //          "kdObat": "NDPHO.01",
            //          "nmObat": "CTM",
            //          "sedia": 0
            //        },
            //        "signa1": 3.0,
            //        "signa2": 1.0,
            //        "jmlObat": 1.0,
            //        "jmlHari": 0.0,
            //        "kekuatan": 3.0,
            //        "jmlPermintaan": 0.0,
            //        "jmlObatRacikan": 3.0
            //      },
            //      {
            //        "kdObatSK": 84291174,
            //        "kdRacikan": "R.01",
            //        "obat": {
            //          "kdObat": "NDPHO.02",
            //          "nmObat": "Panadol",
            //          "sedia": 0
            //        },
            //        "signa1": 3.0,
            //        "signa2": 1.0,
            //        "jmlObat": 1.0,
            //        "jmlHari": 0.0,
            //        "kekuatan": 3.0,
            //        "jmlPermintaan": 0.0,
            //        "jmlObatRacikan": 3.0
            //      }
            //    ]
            //  },
            //  "metaData": {
            //    "message": "OK",
            //    "code": "200",
            //    "MessageDescription": "Retrieve data success."
            //  },
            //  "IsOk": true
            //}
            #endregion

            // Check jika obat sudah dikirim maka delete dulu
            var pcareObatLog = new PCareKunjunganObatCollection();
            pcareObatLog.Query.Where(pcareObatLog.Query.RegistrationNo == registrationNo);
            if (pcareObatLog.LoadAll())
            {
                foreach (var obat in pcareObatLog)
                {
                    obat.MarkAsDeleted();
                }
                pcareObatLog.Save();

                DeleteDataObat(noKunjungan, pcareUtils);
            }


            // Add obat
            var presc = new TransPrescriptionQuery("h");
            var prescItem = new TransPrescriptionItemQuery("d");
            prescItem.InnerJoin(presc).On(prescItem.PrescriptionNo == presc.PrescriptionNo);

            var itemProduct = new ItemProductMedicQuery("ip");
            prescItem.InnerJoin(itemProduct).On(prescItem.ItemID == itemProduct.ItemID);

            var item = new ItemQuery("i");
            prescItem.InnerJoin(item).On(prescItem.ItemID == item.ItemID);

            var pcareMap = new PCareReferenceItemMappingQuery("pcmo");
            prescItem.LeftJoin(pcareMap).On(prescItem.ItemID == pcareMap.MappingWithID & pcareMap.ReferenceID == Bridging.PCare.Common.Constant.ReferenceType.Obat.ToString());

            prescItem.Where(presc.IsApproval == true, presc.RegistrationNo == registrationNo);
            prescItem.Select(prescItem.PrescriptionNo, prescItem.ItemID, prescItem.SequenceNo, prescItem.ParentNo, prescItem.IsCompound,
                prescItem.TakenQty, prescItem.ResultQty, prescItem.PrescriptionQty,
                prescItem.Price, prescItem.Notes, prescItem.SRConsumeMethod, prescItem.ConsumeQty, pcareMap.ItemID.As("PCareKdObat"), item.ItemName);
            var dtb = prescItem.LoadDataTable();
            foreach (DataRow row in dtb.Rows)
            {
                bool isDpho = (row["PCareKdObat"] != DBNull.Value);
                KunjunganObatPost obat;
                if (isDpho && !string.IsNullOrEmpty(row["PCareKdObat"].ToString()))
                    obat = ObatDpho(noKunjungan, row);
                else
                    obat = ObatNonDpho(noKunjungan, row);

                var postResponse = pcareUtils.KunjunganObatAdd(obat);
                // Save history
                var pcareObat = new PCareKunjunganObat
                {
                    PrescriptionNo = row["PrescriptionNo"].ToString(),
                    SequenceNo = row["SequenceNo"].ToString(),
                    RegistrationNo = registrationNo,
                    NoKunjungan = noKunjungan
                };

                if (postResponse.IsOk)
                {
                    foreach (Response response in postResponse.Response)
                    {
                        if (response.Field.ToLower() == "kdobatsk")
                        {
                            pcareObat.KdObatSK = response.Message.ToInt();
                        }
                        else if (response.Field.ToLower() == "kdracikan")
                        {
                            pcareObat.KdRacikan = response.Message;
                        }
                    }
                }
                else
                {
                    pcareObat.ErrorResponse = JsonConvert.SerializeObject(postResponse);
                }
                pcareObat.Save();
            }

            return true;
        }
        #endregion

        private static PendaftaranPost InitializedPendaftaranPostData(string registrationNo, string tglDaftar, string keluhan,
            string kdPoli, string kdProviderPeserta, string noKartu, bool isHealthyVisit)
        {
            // "tkp": [{ "kdTkp": "10", "nmTkp": "RJTP" }, { "kdTkp": "20", "nmTkp": "RITP" }, { "kdTkp": "50", "nmTkp": "Promotif" }]

            var postData = new PendaftaranPost
            {
                KdProviderPeserta = kdProviderPeserta,
                TglDaftar = tglDaftar,
                NoKartu = noKartu,
                KdPoli = kdPoli,
                Keluhan = keluhan,
                KunjSakit = !isHealthyVisit,
                Sistole = 0,
                Diastole = 0,
                BeratBadan = 0,
                TinggiBadan = 0,
                RespRate = 0,
                HeartRate = 0,
                KdTkp = "10"
            };

            ////Vitalsign
            //var qrPhr = new PatientHealthRecordLineQuery("phrl");
            //qrPhr.Where(qrPhr.RegistrationNo == registrationNo);
            //qrPhr.Select(qrPhr.QuestionID, qrPhr.QuestionAnswerNum, qrPhr.QuestionAnswerSelectionLineID);
            //qrPhr.OrderBy(qrPhr.TransactionNo.Descending); //Untuk ambil yg terakhir
            //var dtbPhr = qrPhr.LoadDataTable();

            //// TODO: Ganti QuestionID dg Parameter
            //foreach (DataRow row in dtbPhr.Rows)
            //{
            //    var vitalSignValue = row[PatientHealthRecordLineMetadata.ColumnNames.QuestionAnswerNum].ToInt();
            //    var questionAnswerSelectionLineID = row[PatientHealthRecordLineMetadata.ColumnNames.QuestionAnswerSelectionLineID].ToString();

            //    switch (row[PatientHealthRecordLineMetadata.ColumnNames.QuestionID].ToString())
            //    {
            //        case "VIT.SGN.01": // Sistole
            //            if (postData.Sistole == null || postData.Sistole == 0)
            //                postData.Sistole = vitalSignValue == 0 ? (int?)null : vitalSignValue;
            //            break;
            //        case "VIT.SGN.02": // Diastole
            //            if (postData.Diastole == null || postData.Diastole == 0)
            //                postData.Diastole = vitalSignValue == 0 ? (int?)null : vitalSignValue;
            //            break;

            //        case "GEN.SGN.02": // BeratBadan
            //        case "SKGZ01001":
            //            if (postData.BeratBadan == null || postData.BeratBadan == 0)
            //                postData.BeratBadan = vitalSignValue == 0 ? (int?)null : vitalSignValue;
            //            break;
            //        case "GEN.SGN.01": // TinggiBadan
            //        case "SKGZ01002":
            //            if (postData.TinggiBadan == null || postData.TinggiBadan == 0)
            //                postData.TinggiBadan = vitalSignValue == 0 ? (int?)null : vitalSignValue;
            //            break;
            //        case "VIT.SGN.04": // RespRate
            //            if (postData.RespRate == null || postData.RespRate == 0)
            //                postData.RespRate = vitalSignValue == 0 ? (int?)null : vitalSignValue;
            //            break;
            //        case "VIT.SGN.03": // HeartRate
            //            if (postData.HeartRate == null || postData.HeartRate == 0)
            //                postData.HeartRate = vitalSignValue == 0 ? (int?)null : vitalSignValue;
            //            break;
            //        case "VIT.SGN.07": // KdSadar / Glasgow Coma Scale
            //            if (string.IsNullOrEmpty(postData.KdSadar))
            //                postData.KdSadar = string.IsNullOrEmpty(questionAnswerSelectionLineID) ? "01" : questionAnswerSelectionLineID;
            //            break;
            //    }
            //}

            var vitalSignTime = DateTime.Now;
            var vitalSignValue = VitalSign.LastVitalSignValue(registrationNo, string.Empty, VitalSignEnum.BloodPressureSistolic, vitalSignTime);
            postData.Sistole = vitalSignValue == 0 ? (int?)null : vitalSignValue.ToInt();

            vitalSignValue = VitalSign.LastVitalSignValue(registrationNo, string.Empty, VitalSignEnum.BloodPressureDiastolic, vitalSignTime);
            postData.Diastole = vitalSignValue == 0 ? (int?)null : vitalSignValue.ToInt();

            vitalSignValue = VitalSign.LastVitalSignValue(registrationNo, string.Empty, VitalSignEnum.BodyWeight, vitalSignTime);
            postData.BeratBadan = vitalSignValue == 0 ? (int?)null : vitalSignValue.ToInt();

            vitalSignValue = VitalSign.LastVitalSignValue(registrationNo, string.Empty, VitalSignEnum.BodyHeight, vitalSignTime);
            postData.TinggiBadan = vitalSignValue == 0 ? (int?)null : vitalSignValue.ToInt();

            vitalSignValue = VitalSign.LastVitalSignValue(registrationNo, string.Empty, VitalSignEnum.RespiratoryRate, vitalSignTime);
            postData.RespRate = vitalSignValue == 0 ? (int?)null : vitalSignValue.ToInt();

            vitalSignValue = VitalSign.LastVitalSignValue(registrationNo, string.Empty, VitalSignEnum.HeartRate, vitalSignTime);
            postData.HeartRate = vitalSignValue == 0 ? (int?)null : vitalSignValue.ToInt();


            var qrPhr = new PatientHealthRecordLineQuery("phrl");
            qrPhr.Where(qrPhr.RegistrationNo == registrationNo, qrPhr.QuestionID == "VIT.SGN.07"); // KdSadar / Glasgow Coma Scale
            qrPhr.Select(qrPhr.QuestionAnswerSelectionLineID);
            qrPhr.OrderBy(qrPhr.TransactionNo.Descending); //Untuk ambil yg terakhir
            qrPhr.es.Top = 1;
            var phrl = new PatientHealthRecordLine();
            if (phrl.Load(qrPhr))
                postData.KdSadar = string.IsNullOrEmpty(phrl.QuestionAnswerSelectionLineID) ? "01" : phrl.QuestionAnswerSelectionLineID;
            else
                postData.KdSadar = "01";


            return postData;
        }

        private static KunjunganPost InitializedKunjunganPostData(PendaftaranPost pendaftaranPostData, string paramedicID, string registrationNo)
        {
            var postData = new KunjunganPost();
            postData.NoKunjungan = null;
            postData.NoKartu = pendaftaranPostData.NoKartu;
            postData.TglDaftar = pendaftaranPostData.TglDaftar;
            postData.KdPoli = pendaftaranPostData.KdPoli;
            postData.Keluhan = pendaftaranPostData.Keluhan;
            postData.KdSadar = pendaftaranPostData.KdSadar;

            //Vitalsign
            if (pendaftaranPostData.Sistole > 0)
                postData.Sistole = pendaftaranPostData.Sistole;

            if (pendaftaranPostData.Diastole > 0)
                postData.Diastole = pendaftaranPostData.Diastole;

            if (pendaftaranPostData.BeratBadan > 0)
                postData.BeratBadan = pendaftaranPostData.BeratBadan;

            if (pendaftaranPostData.TinggiBadan > 0)
                postData.TinggiBadan = pendaftaranPostData.TinggiBadan;

            if (pendaftaranPostData.RespRate > 0)
                postData.RespRate = pendaftaranPostData.RespRate;

            if (pendaftaranPostData.HeartRate > 0)
                postData.HeartRate = pendaftaranPostData.HeartRate;

            // Terapi
            postData.Terapi = "";
            var soape = new EpisodeSOAPEQuery("a");
            soape.Where(soape.RegistrationNo == registrationNo);
            soape.Select(soape.Planning);
            var dtbSoape = soape.LoadDataTable();
            foreach (DataRow row in dtbSoape.Rows)
            {
                postData.Terapi = postData.Terapi != string.Empty ? string.Concat(postData.Terapi, Environment.NewLine, row["Planning"]) : row["Planning"].ToString();
            }

            // Default Status Pulang
            postData.KdStatusPulang = "3"; // 3 Berobat jalan, 4 Rujuk lanjut, 5 Rujuk Internal
            postData.TglPulang = pendaftaranPostData.TglDaftar; //DateTime.Today.AddDays(-1).ToString(Constant.DateFormatPCare); //pendaftaranPostData.TglDaftar;

            //KdDokter
            var paramedic = new PCareReferenceItemMapping();
            paramedic.LoadByPrimaryKey(Bridging.PCare.Common.Constant.ReferenceType.Dokter.ToString(), paramedicID);
            postData.KdDokter = paramedic.ItemID;

            //Diagnose
            postData.KdDiag1 = null;
            postData.KdDiag2 = null;
            postData.KdDiag3 = null;
            var qrEpisodeDiag = new EpisodeDiagnoseQuery();
            qrEpisodeDiag.Where(qrEpisodeDiag.RegistrationNo == registrationNo);
            var dtbEpisodeDiag = qrEpisodeDiag.LoadDataTable();
            foreach (DataRow row in dtbEpisodeDiag.Rows)
            {
                if (postData.KdDiag1 == null && row[EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType].Equals("DiagnoseType-001"))
                {
                    postData.KdDiag1 = row[EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID].ToString();
                }
                else if (row[EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType].Equals("DiagnoseType-002"))
                {
                    postData.KdDiag2 = row[EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID].ToString();
                }
                else if (row[EpisodeDiagnoseMetadata.ColumnNames.SRDiagnoseType].Equals("DiagnoseType-003"))
                {
                    postData.KdDiag3 = row[EpisodeDiagnoseMetadata.ColumnNames.DiagnoseID].ToString();
                }
            }

            // Dirujuk
            postData.KdPoliRujukInternal = null;
            postData.RujukLanjut = new KunjunganRujukLanjut() { Kdppk = null, TglEstRujuk = null, SubSpesialis = null, Khusus = new KunjunganRujukLanjutKhusus() { KdKhusus = null, Catatan = null, KdSubSpesialis = null } };


            //var refer = new RegistrationReferred();
            //if (refer.LoadByPrimaryKey(registrationNo))
            //{
            //    // Dirujuk Internal
            //    if (refer.ServiceUnitID != null)
            //    {
            //        var su = new ServiceUnit();
            //        if (su.LoadByPrimaryKey(refer.ServiceUnitID))
            //        {
            //            postData.KdPoliRujukInternal = su.PCareKdPoli;
            //            postData.KdStatusPulang = "5"; // 3 Berobat jalan, 4 Rujuk lanjut, 5 Rujuk Internal
            //        }
            //    }
            //    else if (refer.ReferralID != null)
            //    {
            //        // Dirujuk Lanjut
            //        var r = new Referral();
            //        if (r.LoadByPrimaryKey(refer.ReferralID))
            //        {
            //            postData.RujukLanjut = new KunjunganRujukLanjut()
            //            {
            //                Kdppk = r.PCareKdProvider,
            //                TglEstRujuk = Convert.ToDateTime(refer.ReferredDate).ToString(Constant.DateFormatPCare),
            //                SubSpesialis = new SubSpesialisSarana() { KdSubSpesialis1 = refer.SRReferralServiceUnit, KdSarana = null },
            //                Khusus = null
            //                //Khusus = new KunjunganRujukLanjutKhusus() { KdKhusus = null, Catatan = null, KdSubSpesialis = null }
            //            };

            //            postData.KdStatusPulang = "4"; // 3 Berobat jalan, 4 Rujuk lanjut, 5 Rujuk Internal
            //        }
            //    }

            //}

            //TODO: PCare Rujuk internal

            // Dirujuk Lanjut
            var refExt = new ReferExternal();
            if (refExt.LoadByPrimaryKey(registrationNo))
            {
                var pcareMap = new PCareReferenceItemMapping();
                if (pcareMap.LoadByPrimaryKey("Provider", refExt.ReferralID))
                {
                    string kdSubSpesialis = null;
                    var stdi = new AppStandardReferenceItem();
                    if (stdi.LoadByPrimaryKey("ReferralServiceUnit", refExt.SRReferralServiceUnit) && !string.IsNullOrWhiteSpace(stdi.ReferenceID))
                        kdSubSpesialis = stdi.ReferenceID;

                    postData.RujukLanjut = new KunjunganRujukLanjut()
                    {
                        Kdppk = pcareMap.ItemID,
                        TglEstRujuk = Convert.ToDateTime(refExt.ReferralAgreedTime).ToString(Constant.DateFormatPCare),
                        SubSpesialis = new SubSpesialisSarana() { KdSubSpesialis1 = kdSubSpesialis, KdSarana = null },
                        Khusus = null
                        //Khusus = new KunjunganRujukLanjutKhusus() { KdKhusus = null, Catatan = null, KdSubSpesialis = null }
                    };

                    postData.KdStatusPulang = "4"; // 3 Berobat jalan, 4 Rujuk lanjut, 5 Rujuk Internal
                }
            }

            return postData;

        }
        #endregion

    }
}