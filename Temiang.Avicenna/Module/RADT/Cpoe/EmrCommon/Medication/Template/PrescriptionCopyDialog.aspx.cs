using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PrescriptionCopyDialog : BasePageDialog
    {
        protected string TemplateNo
        {
            get { return Request.QueryString["tno"]; }
        }
        private string PrescriptionNo
        {
            get { return Request.QueryString["prescno"]; }
        }
        private string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        private string SessionNamePrescItem
        {
            get { return Request.QueryString["sn"]; }
        }
        private string ParamedicID
        {
            get
            {
                return ViewState["ParamedicID"].ToString();
            }
            set
            {
                ViewState["ParamedicID"] = value;
            }
        }
        private string LocationID
        {
            get { return Request.QueryString["locID"]; }
        }
        private string GuarantorID
        {
            get { return string.IsNullOrEmpty(Request.QueryString["guarId"]) ? string.Empty : Request.QueryString["guarId"]; }
        }
        private TransPrescriptionItemCollection TransPrescriptionItems
        {
            get
            {
                if (IsPostBack)
                {
                    // Nama session harus sama dg yg di prescription entry
                    object obj = null;
                    if (!string.IsNullOrWhiteSpace(SessionNamePrescItem))
                        obj = Session[SessionNamePrescItem];
                    else
                        obj = Session["collTransPrescriptionItem" + Request.UserHostName];

                    if (obj != null)
                        return ((TransPrescriptionItemCollection)(obj));
                }

                var coll = new TransPrescriptionItemCollection();

                var query = new TransPrescriptionItemQuery("a");
                var qItem = new ItemQuery("b");
                var qItemIntervention = new ItemQuery("c");
                var cons = new ConsumeMethodQuery("d");
                var emb = new EmbalaceQuery("e");

                query.Select
                    (
                        query,
                        qItem.ItemName.As("refToItem_ItemName"),
                        qItem.IsActive.As("refToItem_IsActive"),
                        qItemIntervention.ItemName.Coalesce("''").As("refToItem_ItemInterventionName"),
                        //"<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        cons.SRConsumeMethodName.Coalesce("''").As("refToConsumeMethod_SRConsumeMethodName"),
                        emb.EmbalaceLabel.Coalesce("''").As("refToEmbalace_EmbalaceLabel")
                    );
                query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
                query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);
                query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
                query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
                query.Where(query.PrescriptionNo == string.Empty);
                //query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);
                query.OrderBy(query.SequenceNo.Ascending);
                coll.Load(query);

                if (!string.IsNullOrWhiteSpace(SessionNamePrescItem))
                    Session[SessionNamePrescItem] = coll;
                else
                    Session["collTransPrescriptionItem" + Request.UserHostName] = coll;

                return coll;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Footer.Visible = false;
            if (!IsPostBack)
            {
                ParamedicID = string.Empty;
                var user = new AppUser();
                if (user.LoadByPrimaryKey(AppSession.UserLogin.UserID))
                {
                    var par = new Paramedic();
                    if (par.LoadByPrimaryKey(user.ParamedicID))
                    {
                        Page.Title = "Prescription template for: " + par.ParamedicName + " (" + par.ParamedicID + ")";
                        ParamedicID = par.ParamedicID;
                    }
                    else
                    {
                        Page.Title = "Error: User is not a physician";
                    }
                }
                else
                {
                    Page.Title = "Error";
                }
            }
        }


        protected void gridItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            ((RadGrid)source).DataSource = string.IsNullOrEmpty(TemplateNo) ? PrescriptionItem(PrescriptionNo) : TemplateItem(TemplateNo);
        }

        protected void gridItem_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detailCompound":
                    {
                        // kalau bukan racikan jangan load apa2
                        if (e.DetailTableView.ParentItem["IsCompound"].Text != "True")
                        {
                            e.DetailTableView.DataSource = null;
                            e.DetailTableView.ParentItem.Expanded = false;
                            return;
                        }

                        DataTable dtb;
                        if (string.IsNullOrEmpty(PrescriptionNo))
                        {
                            // Ambil dari template
                            var query = new TransPrescriptionItemTemplateQuery("tpit");
                            var item = new ItemQuery("item");
                            var emb = new EmbalaceQuery("e");
                            var cons = new ConsumeMethodQuery("cm");
                            query.InnerJoin(item).On(query.ItemID == item.ItemID)
                                .LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod)
                                .LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID)
                                .Where(
                                    query.TemplateNo ==
                                    e.DetailTableView.ParentItem.GetDataKeyValue("HeaderNo").ToString())
                                .Where(query.Or(
                                    query.ParentNo ==
                                    e.DetailTableView.ParentItem.GetDataKeyValue("SequenceNo").ToString(),
                                    query.SequenceNo ==
                                    e.DetailTableView.ParentItem.GetDataKeyValue("SequenceNo").ToString()))
                                .Select(query,
                                    item.ItemName,
                                    emb.EmbalaceLabel,
                                    cons.SRConsumeMethodName
                                );
                            dtb = query.LoadDataTable();

                        }
                        else
                        {
                            // Ambil dari Prescription
                            var query = new TransPrescriptionItemQuery("tpit");
                            var item = new ItemQuery("item");
                            var emb = new EmbalaceQuery("e");
                            var cons = new ConsumeMethodQuery("cm");
                            query.InnerJoin(item).On(query.ItemID == item.ItemID)
                                .LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod)
                                .LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID)
                                .Where(
                                    query.PrescriptionNo ==
                                    e.DetailTableView.ParentItem.GetDataKeyValue("HeaderNo").ToString())
                                .Where(query.Or(
                                    query.ParentNo ==
                                    e.DetailTableView.ParentItem.GetDataKeyValue("SequenceNo").ToString(),
                                    query.SequenceNo ==
                                    e.DetailTableView.ParentItem.GetDataKeyValue("SequenceNo").ToString()))
                                .Select(query,
                                    item.ItemName,
                                    emb.EmbalaceLabel,
                                    cons.SRConsumeMethodName
                                );
                            dtb = query.LoadDataTable();

                        }


                        e.DetailTableView.DataSource = dtb;
                        break;
                    }
            }
        }

        protected void gridItem_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += gridItem_ItemPreRenderItem;
        }

        private void gridItem_ItemPreRenderItem(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            if (dataItem.OwnerTableView.Name == "master")
            {
                // cbo consume method 
                var cboConsumeMethod = (dataItem["SRConsumeMethodCbo"].FindControl("cboConsumeMethod") as RadComboBox);
                if (cboConsumeMethod != null)
                {
                    cboConsumeMethod.DataSource = ConsumeMethod();
                    cboConsumeMethod.DataValueField = "SRConsumeMethod";
                    cboConsumeMethod.DataTextField = "SRConsumeMethodName";
                    cboConsumeMethod.DataBind();

                    var SRCMid = dataItem["SRConsumeMethod"].Text;
                    if (!string.IsNullOrEmpty(SRCMid) && SRCMid != "&nbsp;")
                        ComboBox.SelectedValue(cboConsumeMethod, SRCMid);
                }

                // cbo comsume unit
                var cboConsumeUnit = (dataItem["SRConsumeUnitCbo"].FindControl("cboConsumeUnit") as RadComboBox);
                if (cboConsumeUnit != null)
                {
                    cboConsumeUnit.DataSource = ConsumeUnit(dataItem["ItemID"].Text,
                        (dataItem["IsCompound"].Text == "True"), dataItem["EmbalaceID"].Text);
                    cboConsumeUnit.DataValueField = "ItemID";
                    cboConsumeUnit.DataTextField = "ItemName";
                    cboConsumeUnit.DataBind();

                    var SRCUid = dataItem["SRConsumeUnit"].Text;
                    if (!string.IsNullOrEmpty(SRCUid) && SRCUid != "&nbsp;")
                        ComboBox.SelectedValue(cboConsumeUnit, SRCUid);

                }

                // cbo Medication Consume 
                var cboMedicationConsume = (dataItem["SRMedicationConsumeCbo"].FindControl("cboMedicationConsume") as RadComboBox);
                if (cboMedicationConsume != null)
                {
                    cboMedicationConsume.DataSource = MedicationConsume();
                    cboMedicationConsume.DataValueField = "ItemID";
                    cboMedicationConsume.DataTextField = "ItemName";
                    cboMedicationConsume.DataBind();

                    var SRConsume = dataItem["SRMedicationConsume"].Text;
                    if (!string.IsNullOrEmpty(SRConsume) && SRConsume != "&nbsp;")
                        ComboBox.SelectedValue(cboMedicationConsume, SRConsume);
                }

                var cbo = (dataItem["TemplateSRItemUnit"].FindControl("cboEmbalace") as RadComboBox);
                SetCboEmbalace(cbo);
                var lblSRItemUnit = (dataItem["TemplateSRItemUnit"].FindControl("lblSRItemUnit") as Label);
                ComboBox.SelectedValue(cbo, dataItem["EmbalaceID"].Text.Replace("&nbsp;", ""));

                if (dataItem["IsCompound"].Text == "True")
                {
                    // hide notes compound
                    (dataItem["Notes"].FindControl("txtNotes") as TextBox).Visible = false;
                    // show embalace
                    cbo.Visible = true;
                    lblSRItemUnit.Visible = false;
                }
                else
                {
                    // show hide expand collapse
                    //dataItem.Cells[0].Text = "0";
                    //dataItem.Cells[1].Text = "1";
                    //dataItem.Cells[2].Text = "2";
                    //dataItem.Cells[3].Text = "3";
                    dataItem.Cells[0].Text = "&nbsp;";

                    // hide embalce
                    cbo.Visible = false;
                    lblSRItemUnit.Visible = true;
                }
            }
            else if (dataItem.OwnerTableView.Name == "detailCompound")
            {
                // cbo dosage unit
                var SRDUid = dataItem["SRDosageUnit"].Text;
                if (!string.IsNullOrEmpty(SRDUid) && SRDUid != "&nbsp;")
                {
                    var cboDosageUnit = (dataItem["Formula"].FindControl("cboDosageUnit") as RadComboBox);
                    if (cboDosageUnit != null)
                    {
                        SetDosageUnit(dataItem["ItemID"].Text, cboDosageUnit);
                        cboDosageUnit.SelectedValue = SRDUid;
                    }
                }
            }
        }
        private void SetDosageUnit(string ItemID, RadComboBox cbo)
        {
            cbo.Items.Clear();

            var ipm = new ItemProductMedic();
            if (ipm.LoadByPrimaryKey(ItemID))
            {
                if (!string.IsNullOrEmpty(ipm.SRItemUnit))
                {
                    cbo.Items.Add(new RadComboBoxItem(ipm.SRItemUnit, ipm.SRItemUnit));
                }

                //decimal? factor = 1;
                if (!string.IsNullOrEmpty(ipm.SRDosageUnit) && (ipm.SRItemUnit != ipm.SRDosageUnit))
                {
                    cbo.Items.Add(new RadComboBoxItem(ipm.SRDosageUnit, ipm.SRDosageUnit));
                }

                var dosages = new ItemProductDosageDetailCollection();
                dosages.Query.Where(dosages.Query.ItemID == ItemID);
                dosages.LoadAll();

                foreach (var dosage in dosages)
                {
                    if (!cbo.Items.Select(c => c.Value).Contains(dosage.SRDosageUnit))
                    {
                        cbo.Items.Add(new RadComboBoxItem(dosage.SRDosageUnit, dosage.SRDosageUnit));
                    }
                }
            }
        }
        private ConsumeMethodCollection ConsumeMethod()
        {
            if (ViewState["ConsumeMethod"] != null)
                return ViewState["ConsumeMethod"] as ConsumeMethodCollection;

            var cm = new ConsumeMethodQuery("b");
            var cmColl = new ConsumeMethodCollection();

            cm.Select(
                cm
                );
            cmColl.Load(cm);

            ViewState["ConsumeMethod"] = cmColl;
            return ViewState["ConsumeMethod"] as ConsumeMethodCollection;
        }
        private DataTable ConsumeUnit(string itemID, bool isCompound, string embalaceID)
        {
            if (ViewState["ConsumeUnit"] != null)
                return ViewState["ConsumeUnit"] as DataTable;

            var std = new AppStandardReferenceItemQuery();
            std.Select(std.ItemID, std.ItemName);
            std.Where
                (
                    std.StandardReferenceID == AppEnum.StandardReference.DosageUnit
                );

            var dtb = std.LoadDataTable();

            if (isCompound)
            {
                var emb = new EmbalaceQuery();
                emb.Where(emb.EmbalaceID == embalaceID);
                emb.Select(emb.EmbalaceID.As("ItemID"), emb.EmbalaceName.As("ItemName"));
                dtb.Merge(emb.LoadDataTable());
            }

            ViewState["ConsumeUnit"] = dtb;
            return ViewState["ConsumeUnit"] as DataTable;
        }
        private DataTable MedicationConsume()
        {
            if (ViewState["MedicationConsume"] != null)
                return ViewState["MedicationConsume"] as DataTable;

            var std = new AppStandardReferenceItemQuery();
            std.Select(std.ItemID, std.ItemName);
            std.Where
                (
                    std.StandardReferenceID == AppEnum.StandardReference.MedicationConsume
                );

            var dtb = std.LoadDataTable();

            var rowBlank = dtb.NewRow();
            rowBlank[0] = string.Empty;
            rowBlank[1] = string.Empty;
            dtb.Rows.InsertAt(rowBlank, 0);

            ViewState["MedicationConsume"] = dtb;
            return ViewState["MedicationConsume"] as DataTable;
        }

        private void SetCboEmbalace(RadComboBox cbo)
        {
            EmbalaceCollection embs;
            if (ViewState["EmbalaceColl"] != null)
                embs = ViewState["EmbalaceColl"] as EmbalaceCollection;
            else
            {
                embs = new EmbalaceCollection();
                embs.LoadAll();
            }

            foreach (var emb in embs)
            {
                cbo.Items.Add(new RadComboBoxItem(emb.EmbalaceLabel, emb.EmbalaceID));
            }
        }

        private DataTable TemplateItem(string templateNo)
        {
            if (IsPostBack && Session["template_item"] != null)
            {
                return (DataTable)Session["template_item"];
            }

            var parGuarantorId = GuarantorID;

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                parGuarantorId = reg.GuarantorID;
            }

            //db:20231110 -- restriksi u/ template dokter baca status dia allow atau tidak, sedang template dari farmasi tidak baca restriksi (disamakan dg inputan di prescription sales)
            var query = new TransPrescriptionItemTemplateQuery("tpit");
            var item = new ItemQuery("item");
            var emb = new EmbalaceQuery("e");
            var cons = new ConsumeMethodQuery("cm");
            var itemVwQuery = new VwItemProductMedicNonMedicQuery("c");
            query.InnerJoin(item).On(query.ItemID == item.ItemID)
                .InnerJoin(itemVwQuery).On(query.ItemID == itemVwQuery.ItemID)
                .LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod)
                .LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID)
                .Where(
                    query.TemplateNo == templateNo,
                    query.IsRFlag == true)
                .Select(query,
                    item.ItemName,
                    emb.EmbalaceLabel,
                    cons.SRConsumeMethodName
                );
            query.Select(query, query.TemplateNo.As("HeaderNo"));
            query.OrderBy(query.SequenceNo.Ascending);

            if (string.IsNullOrWhiteSpace(SessionNamePrescItem))
            {
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(parGuarantorId);

                bool isFornas = (guar.IsItemRestrictionsFornas ?? false);
                bool isFormularium = (guar.IsItemRestrictionsFormularium ?? false);
                bool isGeneric = (guar.IsItemRestrictionsGeneric ?? false);
                bool isNonGeneric = (guar.IsItemRestrictionsNonGeneric ?? false);
                bool isNonGenericLimited = (guar.IsItemRestrictionsNonGenericLimited ?? false);

                var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

                if (isFornas)
                    xx.Add(itemVwQuery.IsFornas == true);

                if (isFormularium)
                    xx.Add(itemVwQuery.IsFormularium == true);

                if (isGeneric)
                    xx.Add(itemVwQuery.IsGeneric == true);

                if (isNonGeneric)
                    xx.Add(itemVwQuery.IsNonGeneric == true);

                if (isNonGenericLimited)
                    xx.Add(itemVwQuery.IsNonGenericLimited == true);

                if (xx.Count > 0)
                    query.Where(query.Or(xx.ToArray()));
                else
                {
                    var restrictions = new GuarantorItemRestrictionsQuery("rest");
                    var itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == parGuarantorId, itmrest.SRItemType == ItemType.Medical);
                    DataTable dtRest = restrictions.LoadDataTable();
                    if (dtRest.Rows.Count > 0)
                    {
                        if (guar.IsItemProductRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }
                }

                //db:20240127 - cek settingan casemix (disamakan dg ComboBoxDataService/PrescriptionItemSelection)
                if (Helper.GuarantorBpjsCasemix.Contains(parGuarantorId))
                {
                    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    {
                        var cmixCover = new CasemixCoveredDetailQuery("cmix");
                        var cmixGuarCover = new CasemixCoveredGuarantorQuery("cmixGuar");
                        cmixCover.InnerJoin(cmixGuarCover).On(cmixGuarCover.CasemixCoveredID == cmixCover.CasemixCoveredID && cmixGuarCover.GuarantorID == reg.GuarantorID);
                        cmixCover.Select(cmixCover.ItemID);
                        switch (reg.SRRegistrationType)
                        {
                            case "IPR":
                                {
                                    cmixCover.Where(cmixCover.Or(
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderIpr == true)));
                                }
                                break;
                            case "EMR":
                                {
                                    cmixCover.Where(cmixCover.Or(
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderEmr == true)));
                                }
                                break;
                            default: //OPR & MCU
                                {
                                    cmixCover.Where(cmixCover.Or(
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderOpr == true)));
                                }
                                break;
                        }

                        var cmixCoverRule = new CasemixCoveredRegistrationRuleQuery("cmixrule");
                        cmixCoverRule.Select(cmixCoverRule.ItemID);
                        cmixCoverRule.Where(cmixCoverRule.RegistrationNo == RegistrationNo);


                        var dtCmixCover = cmixCover.LoadDataTable();
                        bool isCmixCover = (dtCmixCover.Rows.Count > 0);

                        var dtCmixCoverRule = cmixCoverRule.LoadDataTable();
                        bool isCmixCoverRule = (dtCmixCoverRule.Rows.Count > 0);

                        if (isCmixCover & isCmixCoverRule)
                        {
                            query.Where(query.Or(query.ItemID.In(cmixCover), query.ItemID.In(cmixCoverRule)));
                        }
                        else
                        {
                            if (isCmixCover)
                            {
                                query.Where(query.ItemID.In(cmixCover));
                            }
                            else if (isCmixCoverRule)
                            {
                                query.Where(query.ItemID.In(cmixCoverRule));
                            }
                        }
                    }
                }
            }

            var dtb1 = query.LoadDataTable();
            Session["template_item"] = dtb1;
            return dtb1;
        }

        private DataTable PrescriptionItem(string prescriptionNo)
        {
            if (IsPostBack && Session["template_item"] != null)
            {
                return (DataTable)Session["template_item"];
            }
            var query = new TransPrescriptionItemQuery("tpit");
            var item = new ItemQuery("item");
            var itemInt = new ItemQuery("int");
            var emb = new EmbalaceQuery("e");
            var cons = new ConsumeMethodQuery("cm");
            var itemVwQuery = new VwItemProductMedicNonMedicQuery("c1");
            
            query.InnerJoin(item).On(query.ItemID == item.ItemID)
                .LeftJoin(itemInt).On(query.ItemInterventionID == itemInt.ItemID)
                .InnerJoin(itemVwQuery).On(query.ItemID == itemVwQuery.ItemID)
                .LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod)
                .LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID)
                .Where(
                    query.PrescriptionNo == prescriptionNo,
                    query.IsRFlag == true)
                .Select(query,
                    query.Acpcdc.As("SRMedicationConsume"),
                    item.ItemName,
                    emb.EmbalaceLabel,
                    cons.SRConsumeMethodName,
                    itemInt.ItemName.As("ItemInterventionName")
                );

            query.Select(query, query.PrescriptionNo.As("HeaderNo"));

            //if (string.IsNullOrWhiteSpace(SessionNamePrescItem))
            {
                var parGuarantorId = GuarantorID;

                var reg = new Registration();
                if (reg.LoadByPrimaryKey(RegistrationNo))
                {
                    parGuarantorId = reg.GuarantorID;
                }

                var guar = new Guarantor();
                guar.LoadByPrimaryKey(parGuarantorId);

                bool isFornas = (guar.IsItemRestrictionsFornas ?? false);
                bool isFormularium = (guar.IsItemRestrictionsFormularium ?? false);
                bool isGeneric = (guar.IsItemRestrictionsGeneric ?? false);
                bool isNonGeneric = (guar.IsItemRestrictionsNonGeneric ?? false);
                bool isNonGenericLimited = (guar.IsItemRestrictionsNonGenericLimited ?? false);

                var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

                if (isFornas)
                    xx.Add(itemVwQuery.IsFornas == true);

                if (isFormularium)
                    xx.Add(itemVwQuery.IsFormularium == true);

                if (isGeneric)
                    xx.Add(itemVwQuery.IsGeneric == true);

                if (isNonGeneric)
                    xx.Add(itemVwQuery.IsNonGeneric == true);

                if (isNonGenericLimited)
                    xx.Add(itemVwQuery.IsNonGenericLimited == true);

                if (xx.Count > 0)
                    query.Where(query.Or(xx.ToArray()));
                else
                {
                    var restrictions = new GuarantorItemRestrictionsQuery("rest");
                    var itmrest = new ItemQuery("itmrest");
                    restrictions.Select(restrictions.ItemID);
                    restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                    restrictions.Where(restrictions.GuarantorID == parGuarantorId, itmrest.SRItemType == ItemType.Medical);
                    DataTable dtRest = restrictions.LoadDataTable();
                    if (dtRest.Rows.Count > 0)
                    {
                        if (guar.IsItemProductRestrictionStatusAllowed ?? true)
                            query.Where(query.ItemID.In(restrictions));
                        else
                            query.Where(query.ItemID.NotIn(restrictions));
                    }
                }

                //db:20240127 - cek settingan casemix (disamakan dg ComboBoxDataService/PrescriptionItemSelection)
                if (Helper.GuarantorBpjsCasemix.Contains(parGuarantorId))
                {
                    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    {
                        var cmixCover = new CasemixCoveredDetailQuery("cmix");
                        var cmixGuarCover = new CasemixCoveredGuarantorQuery("cmixGuar");
                        cmixCover.InnerJoin(cmixGuarCover).On(cmixGuarCover.CasemixCoveredID == cmixCover.CasemixCoveredID && cmixGuarCover.GuarantorID == reg.GuarantorID);
                        cmixCover.Select(cmixCover.ItemID);
                        switch (reg.SRRegistrationType)
                        {
                            case "IPR":
                                {
                                    cmixCover.Where(cmixCover.Or(
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderIpr == true)));
                                }
                                break;
                            case "EMR":
                                {
                                    cmixCover.Where(cmixCover.Or(
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderEmr == true)));
                                }
                                break;
                            default: //OPR & MCU
                                {
                                    cmixCover.Where(cmixCover.Or(
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == true, cmixCover.IsAllowedToOrder == true),
                                        cmixCover.And(cmixCover.IsUsingGlobalSetting == false, cmixCover.IsAllowedToOrderOpr == true)));
                                }
                                break;
                        }

                        var cmixCoverRule = new CasemixCoveredRegistrationRuleQuery("cmixrule");
                        cmixCoverRule.Select(cmixCoverRule.ItemID);
                        cmixCoverRule.Where(cmixCoverRule.RegistrationNo == RegistrationNo);


                        var dtCmixCover = cmixCover.LoadDataTable();
                        bool isCmixCover = (dtCmixCover.Rows.Count > 0);

                        var dtCmixCoverRule = cmixCoverRule.LoadDataTable();
                        bool isCmixCoverRule = (dtCmixCoverRule.Rows.Count > 0);

                        if (isCmixCover & isCmixCoverRule)
                        {
                            query.Where(query.Or(query.ItemID.In(cmixCover), query.ItemID.In(cmixCoverRule)));
                        }
                        else
                        {
                            if (isCmixCover)
                            {
                                query.Where(query.ItemID.In(cmixCover));
                            }
                            else if (isCmixCoverRule)
                            {
                                query.Where(query.ItemID.In(cmixCoverRule));
                            }
                        }
                    }
                }
            }

            var dtb = query.LoadDataTable();

            // Cek Item Intervention

            foreach (DataRow row in dtb.Rows)
            {
                if (chkIsUseIntervention.Checked)
                {
                    if (row["ItemInterventionID"] != DBNull.Value &&
                        !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
                    {
                        row["ItemID"] = row["ItemInterventionID"];
                        row["ItemName"] = row["ItemInterventionName"];
                    }
                }
                else
                {
                    if (row["OriConsumeQty"] != DBNull.Value)
                        row["ConsumeQty"] = row["OriConsumeQty"];

                    if (row["OriDosageQty"] != DBNull.Value)
                        row["DosageQty"] = row["OriDosageQty"];

                    if (row["OriSRDosageUnit"] != DBNull.Value)
                        row["SRDosageUnit"] = row["OriSRDosageUnit"];

                    if (row["OriItemQtyInString"] != DBNull.Value)
                        row["ItemQtyInString"] = row["OriItemQtyInString"];

                    if (row["OriSRItemUnit"] != DBNull.Value)
                        row["SRItemUnit"] = row["OriSRItemUnit"];

                    if (row["OriSRConsumeMethod"] != DBNull.Value)
                        row["SRConsumeMethod"] = row["OriSRConsumeMethod"];

                    if (row["OriSRConsumeUnit"] != DBNull.Value)
                        row["SRConsumeUnit"] = row["OriSRConsumeUnit"];
                }
            }

            Session["template_item"] = dtb;
            return dtb;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    var coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName];
            //    var isExist =
            //        coll.Any(
            //            entity =>
            //            entity.ItemID.Equals(cboItemID.SelectedValue) &&
            //            entity.ParentNo.Equals(cboParentNo.SelectedValue) &&
            //            entity.IsCompound.Equals(chkIsCompound.Checked));
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Item: {0} has exist", cboItemID.Text);
            //    }
            //}

            //if (string.IsNullOrEmpty(cboItemID.SelectedValue))
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = "Item name is not selected properly";
            //}
        }


        private void CopyTemplateToPrescription()
        {
            var sequenceNo = string.Empty;
            if (!TransPrescriptionItems.HasData || TransPrescriptionItems.Count == 0)
                sequenceNo = (GuarantorID == string.Empty ? "d00" : "e00");
            else
            {
                sequenceNo = TransPrescriptionItems[TransPrescriptionItems.Count - 1].SequenceNo;
            }

            var gi = gridItem.MasterTableView.Items; //.GetItems(GridItemType.Item);
            var isCopied = true;
            foreach (GridDataItem item in gi)
            {
                var itemQtyInString = (item.FindControl("txtItemQtyInString") as RadTextBox).Text;
                if (Equals(itemQtyInString, "0")) continue;

                if (isCopied)
                    sequenceNo = (GuarantorID == string.Empty ? "d" : "e") + string.Format("{0:00}", int.Parse(sequenceNo.Substring(1, 2)) + 1);

                var consumeMethodID = (item.FindControl("cboConsumeMethod") as RadComboBox).SelectedValue;
                var consumeMethodName = (item.FindControl("cboConsumeMethod") as RadComboBox).Text;
                var consumeQty = (item.FindControl("txtConsumeQty") as RadTextBox).Text;
                var consumeUnitID = (item.FindControl("cboConsumeUnit") as RadComboBox).SelectedValue;
                var medicationConsume = (item.FindControl("cboMedicationConsume") as RadComboBox).SelectedValue;
                if (item["IsCompound"].Text == "True")
                {
                    var embalaceID = (item.FindControl("cboEmbalace") as RadComboBox).SelectedValue;
                    var embalaceName = (item.FindControl("cboEmbalace") as RadComboBox).Text;
                    isCopied = PopulatePrescriptionEntityValueFromCompound(item, ref sequenceNo, itemQtyInString, embalaceID, embalaceName, consumeMethodID, consumeMethodName, consumeQty, consumeUnitID, medicationConsume);
                }
                else
                {
                    isCopied = PopulatePrescriptionEntityValue(item, sequenceNo, itemQtyInString, consumeMethodID, consumeMethodName, consumeQty, consumeUnitID, medicationConsume);
                }
            }
        }

        private bool PopulatePrescriptionEntityValue(GridDataItem item, string sequenceNo, string itemQtyInString, string consumeMethodID, string consumeMethodName, string consumeQty, string consumeUnitID, string medicationConsume)
        {

            var dosageQty = "0";
            var txtDosageQty = item.FindControl("txtDosageQty");
            if (txtDosageQty != null)
                dosageQty = (txtDosageQty as RadTextBox).Text;


            var resultQty = PrescriptionItemDetail.ResultQtyFrom(item["IsCompound"].Text == "True",
                dosageQty, itemQtyInString, item["SRDosageUnit"].Text, item["ItemID"].Text);

            if (AppSession.Parameter.IsPrescriptionOnlyInStock)
            {
                // Check stock
                var ib = new ItemBalance();
                ib.LoadByPrimaryKey(LocationID, item["ItemID"].Text);
                if (ib.Balance < resultQty)
                    return false;
            }

            var entity = TransPrescriptionItems.AddNew();

            PrescriptionEntry.SetPrescriptionEntityValue(
                entity,
                Request.QueryString["regno"],
                string.Empty,
                sequenceNo,
                item["ParentNo"].Text.Replace("&nbsp;", "").Trim(),
                false,
                item["ItemID"].Text,
                (item.FindControl("lblSRItemUnit") as Label).Text,
                itemQtyInString,
                (item.FindControl("lblSRItemUnit") as Label).Text,
                resultQty,
                -1, -1,
                string.Empty, 0,
                (item.FindControl("txtNotes") as TextBox).Text,
                consumeMethodID,
                consumeMethodName,
                itemQtyInString,
                "0", string.Empty,
                consumeQty,
                consumeUnitID,
                medicationConsume,
                GuarantorID,
                true
                );

            return true;
        }

        private bool PopulatePrescriptionEntityValueFromCompound(GridDataItem item, ref string sequenceNo, string itemQtyInString, string embalaceID, string embalaceName, string consumeMethodID, string consumeMethodName, string consumeQty, string consumeUnitID, string medicationConsume)
        {
            var nv = item.ChildItem.NestedTableViews.First();

            // Check Stock
            if (AppSession.Parameter.IsPrescriptionOnlyInStock)
            {
                foreach (GridDataItem compoundItem in nv.Items)
                {
                    var dosageQty = (compoundItem.FindControl("txtDosageQty") as RadTextBox).Text;
                    var dosageUnit = (compoundItem.FindControl("cboDosageUnit") as RadComboBox).SelectedValue;
                    var itemID = compoundItem["ItemID"].Text;

                    var resultQty = PrescriptionItemDetail.ResultQtyFrom(true, dosageQty, itemQtyInString, dosageUnit, itemID);

                    // Check stock
                    var ib = new ItemBalance();
                    ib.LoadByPrimaryKey(LocationID, itemID);
                    if (ib.Balance < resultQty)
                        return false;
                }
            }

            var i = 0;
            var parentSequenceNo = string.Empty;
            foreach (GridDataItem compoundItem in nv.Items)
            {
                i++;
                if (i == 1)
                    parentSequenceNo = sequenceNo;
                else
                    sequenceNo = (GuarantorID == string.Empty ? "d" : "e") + string.Format("{0:00}", int.Parse(sequenceNo.Substring(1, 2)) + 1);

                var entity = TransPrescriptionItems.AddNew();


                //PrescriptionItemDetail.GetResult(
                //        true, compoundItem["ItemID"].Text, itemQtyInString,
                //        (compoundItem.FindControl("txtDosageQty") as RadNumericTextBox).Text,
                //        (compoundItem.FindControl("cboDosageUnit") as RadComboBox).SelectedValue)


                var dosageQty = (compoundItem.FindControl("txtDosageQty") as RadTextBox).Text;
                var dosageUnit = (compoundItem.FindControl("cboDosageUnit") as RadComboBox).SelectedValue;
                var itemID = compoundItem["ItemID"].Text;

                var resultQty = PrescriptionItemDetail.ResultQtyFrom(true, dosageQty, itemQtyInString, dosageUnit, itemID);

                PrescriptionEntry.SetPrescriptionEntityValue(
                    entity,
                    RegistrationNo,
                    string.Empty,
                    sequenceNo,
                    i == 1 ? string.Empty : parentSequenceNo,
                   true,
                    itemID,
                    compoundItem["SRItemUnit"].Text.Replace("&nbsp;", "").Trim(),
                    itemQtyInString,
                    dosageUnit,
                    resultQty,
                    -1, -1,
                    embalaceID,
                    -1,
                    (compoundItem.FindControl("txtNotes") as TextBox).Text,
                    consumeMethodID,
                    consumeMethodName,
                    dosageQty,
                    itemQtyInString,
                    embalaceName,
                    consumeQty,
                    consumeUnitID,
                    medicationConsume, 
                    GuarantorID, 
                    true
                    );
            }
            return true;
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            CopyTemplateToPrescription();
            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }

        protected void btnOverwrite_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            if (TransPrescriptionItems.Count > 0)
                TransPrescriptionItems.MarkAllAsDeleted();

            CopyTemplateToPrescription();
            var script = "<script type='text/javascript'>CloseAndApply();</script>";
            //Create Startup Javascript for close window              
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closeMe", script);
        }

        protected void chkIsUseIntervention_CheckedChanged(object sender, EventArgs e)
        {
            chkIsUseIntervention.Checked = ((CheckBox)sender).Checked;
            Session.Remove("template_item");
            gridItem.Rebind();
        }
    }
}
