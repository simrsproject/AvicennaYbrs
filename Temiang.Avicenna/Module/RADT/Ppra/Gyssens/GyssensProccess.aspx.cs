using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Module.Charges;
using Temiang.Dal.DynamicQuery;
using System.Drawing.Imaging;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Ppra
{
    public partial class GyssensProccess : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Ppra;

            if (!IsPostBack)
            {
                ButtonOk.Visible = false;
                ButtonCancel.Text = "Close";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Gyssens Observation";
            if (!IsPostBack)
            {
                //Check 
                var qrgy = new RegistrationGyssensQuery("g");
                qrgy.es.Top = 1;
                qrgy.Where(qrgy.RegistrationNo == RegistrationNo);
                var dtb = qrgy.LoadDataTable();
                if (dtb.Rows.Count == 0)
                    ReproccessGyssens();
            }
        }

        protected void grdGyssens_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdGyssens.DataSource = GyssensItems(PatientID, MergeRegistrations);
        }

        int _seqNo = 0;
        protected void grdGyssens_OnItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                _seqNo = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SeqNo"]);
            }

            if (e.Item is GridNestedViewItem)
            {
                var gs = new RegistrationGyssens();
                if (gs.LoadByPrimaryKey(RegistrationNo, _seqNo))
                {

                    var rr = new RegistrationRasproQuery("rr");

                    var rab = new RegistrationRasproItemQuery("rab");
                    rab.InnerJoin(rr).On(rr.RegistrationNo == rab.RegistrationNo & rr.SeqNo == rab.RasproSeqNo);

                    var stdi = new AppStandardReferenceItemQuery("stdi");
                    rab.InnerJoin(stdi).On(rr.SRRaspro == stdi.ItemID & stdi.StandardReferenceID == "RASPRO");

                    var abr = new AbRestrictionQuery("abr");
                    rab.LeftJoin(abr).On(rr.AbRestrictionID == abr.AbRestrictionID);

                    var par = new ParamedicQuery("p");
                    rab.LeftJoin(par).On(rr.ParamedicID == par.ParamedicID);

                    rab.Where(rr.RegistrationNo == RegistrationNo, rr.RasproDateTime > gs.PrescriptionDateStart, rr.RasproDateTime < gs.PrescriptionDateEnd);

                    rab.Select(rr, rab.StartDateTime, rab.StopDateTime, stdi.ItemName,
                        string.Format("<'{0}' as PatientID>", PatientID),
                        string.Format("<'{0}' as RegistrationNo>", RegistrationNo),
                        abr.AbRestrictionName.Coalesce("''").As("FocusInfection"), par.ParamedicName);
                    rab.OrderBy(rr.SeqNo.Ascending);
                    var dtb = rab.LoadDataTable();

                    dtb.Columns.Add("Action", typeof(string));
                    // Ambil judul AB stratifikasi
                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["SRRaspro"].Equals("RASAL") || row["SRRaspro"].Equals("RASLAN"))
                        {
                            var rrl = new RegistrationRasproLine();
                            rrl.Query.Where(rrl.Query.RegistrationNo == row["RegistrationNo"].ToString(), rrl.Query.SeqNo == row["SeqNo"].ToString());
                            rrl.Query.es.Top = 1;
                            rrl.Query.OrderBy(rrl.Query.RasproLineID.Descending);
                            if (rrl.Query.Load())
                            {
                                if (row["ActionNo"] == DBNull.Value)
                                {
                                    var ras = new Raspro();
                                    ras.LoadByPrimaryKey(rrl.RasproLineID);

                                    row["Action"] = rrl.Condition == "1" ? ras.YesActionDescription : ras.NoActionDescription;
                                }
                                else
                                {
                                    var raa = new RasproAction();
                                    raa.LoadByPrimaryKey(rrl.RasproLineID, rrl.Condition, row["ActionNo"].ToInt());
                                    row["Action"] = raa.ActionDescription;
                                }

                            }
                        }

                    }

                    // Populate Gyssens
                    var grdGyssensRasproForm = (RadGrid)e.Item.FindControl("grdGyssensRasproForm");

                    InitializeCultureGrid(grdGyssensRasproForm); // Set date  format

                    grdGyssensRasproForm.DataSource = dtb;
                    grdGyssensRasproForm.Rebind();
                }

            }
        }
        //protected void grdRasproForm_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    grdRasproForm.DataSource = RasproFormHists(PatientID, RegistrationNo);
        //}

        private DataTable GyssensItems(string patientID, List<string> mergeRegistrations)
        {
            var qr = new RegistrationGyssensQuery("g");
            var item = new ItemQuery("i");
            qr.InnerJoin(item).On(qr.ItemID == item.ItemID);

            var za = new ZatActiveQuery("z");
            qr.InnerJoin(za).On(qr.ZatActiveID == za.ZatActiveID);

            var cm = new ConsumeMethodQuery("cm");
            qr.InnerJoin(cm).On(qr.SRConsumeMethod == cm.SRConsumeMethod);
            qr.Where(qr.RegistrationNo.In(mergeRegistrations));

            qr.Select(qr, cm.SRConsumeMethodName, string.Format("<'{0}' as PatientID>", patientID), item.ItemName, za.ZatActiveName);
            qr.OrderBy(item.ItemName.Ascending);
            return qr.LoadDataTable();
        }

        //private DataTable RasproFormHists(string patientID, string regNo)
        //{
        //    var qr = new RegistrationRasproQuery("g");
        //    var stdi = new AppStandardReferenceItemQuery("stdi");
        //    qr.InnerJoin(stdi).On(qr.SRRaspro == stdi.ItemID & stdi.StandardReferenceID == "RASPRO");

        //    var abr = new AbRestrictionQuery("abr");
        //    qr.LeftJoin(abr).On(qr.AbRestrictionID == abr.AbRestrictionID);

        //    var par = new ParamedicQuery("p");
        //    qr.LeftJoin(par).On(qr.ParamedicID == par.ParamedicID);

        //    qr.Where(qr.RegistrationNo == regNo);

        //    qr.Select(qr, stdi.ItemName,
        //        string.Format("<'{0}' as PatientID>", patientID),
        //        string.Format("<'{0}' as RegistrationNo>", regNo),
        //        abr.AbRestrictionName.Coalesce("''").As("FocusInfection"), par.ParamedicName);
        //    qr.OrderBy(qr.SeqNo.Ascending);
        //    var dtb = qr.LoadDataTable();
        //    dtb.Columns.Add("Action", typeof(string));
        //    // Ambil judul AB stratifikasi
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        if (row["SRRaspro"].Equals("RASAL") || row["SRRaspro"].Equals("RASLAN"))
        //        {
        //            var rrl = new RegistrationRasproLine();
        //            rrl.Query.Where(rrl.Query.RegistrationNo == row["RegistrationNo"].ToString(), rrl.Query.SeqNo == row["SeqNo"].ToString());
        //            rrl.Query.es.Top = 1;
        //            rrl.Query.OrderBy(rrl.Query.RasproLineID.Descending);
        //            if (rrl.Query.Load())
        //            {
        //                if (row["ActionNo"] == DBNull.Value)
        //                {
        //                    var ras = new Raspro();
        //                    ras.LoadByPrimaryKey(rrl.RasproLineID);

        //                    row["Action"] = rrl.Condition == "1" ? ras.YesActionDescription : ras.NoActionDescription;
        //                }
        //                else
        //                {
        //                    var raa = new RasproAction();
        //                    raa.LoadByPrimaryKey(rrl.RasproLineID, rrl.Condition, row["ActionNo"].ToInt());
        //                    row["Action"] = raa.ActionDescription;
        //                }

        //            }
        //        }

        //    }

        //    return dtb;
        //}

        protected void grdGyssens_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                grdGyssens.Rebind();
            }
            else if (e.CommandName == "reproccess")
            {
                ReproccessGyssens();
            }
        }

        private void ReproccessGyssens()
        {
            var abrForLine = AppParameter.GetParameterValue(AppParameter.ParameterItem.AntibioticRestrictionForLine);

            var presc = new TransPrescriptionQuery("tp");
            presc.Where(presc.RegistrationNo.In(MergeRegistrations), presc.IsApproval == true, presc.Or(presc.IsVoid.IsNull(), presc.IsVoid == false));
            presc.Select(presc.PrescriptionNo, presc.RegistrationNo, presc.CreatedDateTime);
            var dtbPresc = presc.LoadDataTable();

            using (var tr = new esTransactionScope())
            {
                foreach (DataRow row in dtbPresc.Rows)
                {
                    AddRegistrationGyssens(row["RegistrationNo"].ToString(), row["PrescriptionNo"].ToString(), Convert.ToDateTime(row["CreatedDateTime"]), abrForLine);
                }
                tr.Complete();
            }
            grdGyssens.Rebind();
        }

        private int RegistrationGyssensNewSeqNo(string regNo)
        {
            var qr = new RegistrationGyssensQuery("a");
            var ent = new RegistrationGyssens();
            qr.es.Top = 1;
            qr.Where(qr.RegistrationNo == regNo);
            qr.OrderBy(qr.SeqNo.Descending);

            if (ent.Load(qr))
            {
                return ent.SeqNo.ToInt() + 1;
            }
            return 1;
        }

        private void AddRegistrationGyssens(string registrationNo, string prescriptionNo, DateTime createdDateTime, string abrForLine)
        {
            var presc = new TransPrescriptionItemQuery("tpi");
            var qItemMedic = new ItemProductMedicQuery("im");
            presc.InnerJoin(qItemMedic).On(presc.ItemID == qItemMedic.ItemID);

            var za = new ItemProductMedicZatActiveQuery("za");
            presc.InnerJoin(za).On(presc.ItemID == za.ItemID);

            presc.es.Distinct = true;

            presc.Select
            (
                presc.ItemID, za.ZatActiveID, presc.SRConsumeMethod, presc.ConsumeQty, presc.SRConsumeUnit
            );

            if (string.IsNullOrWhiteSpace(abrForLine)) // Untuk semua stratifikasi / lini AB
                presc.Where(presc.PrescriptionNo == prescriptionNo, presc.ItemInterventionID == string.Empty, qItemMedic.IsAntibiotic == true);
            else
                presc.Where(presc.PrescriptionNo == prescriptionNo, presc.ItemInterventionID == string.Empty, qItemMedic.IsAntibiotic == true, qItemMedic.SRAntibioticLine == abrForLine);

            var dtb = presc.LoadDataTable();
            AddRegistrationGyssens(registrationNo, prescriptionNo, createdDateTime, dtb);

            // Intervention
            presc = new TransPrescriptionItemQuery("tpi");
            qItemMedic = new ItemProductMedicQuery("im");
            presc.InnerJoin(qItemMedic).On(presc.ItemInterventionID == qItemMedic.ItemID);

            za = new ItemProductMedicZatActiveQuery("za");
            presc.InnerJoin(za).On(presc.ItemInterventionID == za.ItemID);

            presc.es.Distinct = true;

            presc.Select
            (
                presc.ItemInterventionID.As("ItemID"), za.ZatActiveID, presc.SRConsumeMethod, presc.ConsumeQty, presc.SRConsumeUnit
            );
            presc.Where(presc.PrescriptionNo == prescriptionNo, presc.ItemInterventionID > string.Empty, qItemMedic.IsAntibiotic == true);

            var dtbInterv = presc.LoadDataTable();
            AddRegistrationGyssens(registrationNo, prescriptionNo, createdDateTime, dtbInterv);
        }

        private void AddRegistrationGyssens(string registrationNo, string prescriptionNo, DateTime createdDateTime, DataTable dtbSource)
        {
            foreach (DataRow row in dtbSource.Rows)
            {
                var itemID = row["ItemID"].ToString();

                var zid = row["ZatActiveID"].ToString();
                var cm = row["SRConsumeMethod"].ToString();
                var cmQty = row["ConsumeQty"].ToString();
                var cmUnit = row["SRConsumeUnit"].ToString();

                var qrgy = new RegistrationGyssensQuery("g");
                qrgy.Where(qrgy.RegistrationNo == registrationNo,
                    qrgy.ItemID == itemID,
                    qrgy.ZatActiveID == zid,
                    qrgy.SRConsumeMethod == cm,
                    qrgy.ConsumeQty == cmQty,
                    qrgy.SRConsumeUnit == cmUnit
                    );

                var gyssens = new RegistrationGyssens();
                if (!gyssens.Load(qrgy))
                {
                    gyssens = new RegistrationGyssens();
                    gyssens.RegistrationNo = registrationNo;
                    gyssens.SeqNo = RegistrationGyssensNewSeqNo(registrationNo);
                    gyssens.ItemID = itemID;
                    gyssens.ZatActiveID = zid;
                    gyssens.SRConsumeMethod = cm;
                    gyssens.ConsumeQty = cmQty;
                    gyssens.SRConsumeUnit = cmUnit;
                    gyssens.RasproSeqNo = RasproSeqNo(registrationNo, createdDateTime);
                    gyssens.PrescriptionNo = prescriptionNo;
                    gyssens.PrescriptionDateStart = createdDateTime;
                }
                gyssens.PrescriptionDateEnd = createdDateTime;
                gyssens.Save();
            }

        }

        private int RasproSeqNo(string registrationNo, DateTime createdDateTime)
        {
            var raspro = new RegistrationRaspro();
            raspro.Query.Where(raspro.Query.RegistrationNo == registrationNo, raspro.Query.RasproDateTime <= createdDateTime);
            raspro.Query.es.Top = 1;
            raspro.Query.Select(raspro.Query.SeqNo);
            raspro.Query.OrderBy(raspro.Query.RasproDateTime.Ascending);
            if (raspro.Query.Load())
                return raspro.SeqNo ?? 0;

            return 0;
        }

    }
}
