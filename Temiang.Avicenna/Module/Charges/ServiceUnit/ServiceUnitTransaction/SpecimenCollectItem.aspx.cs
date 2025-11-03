using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using Telerik.Web.Spreadsheet;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class SpecimenCollectItem : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var reg = new Registration();
                if (reg.LoadByPrimaryKey(Request.QueryString["reg"]))
                {
                    var patient = new Patient();
                    patient.LoadByPrimaryKey(reg.PatientID);

                    Title = "Specimen List : " + patient.PatientName + "  [MRN : " + patient.MedicalNo + " | " + reg.RegistrationNo + "]";
                }

                var tc = new TransCharges();
                tc.LoadByPrimaryKey(Request.QueryString["id"]);

                grdList.Rebind();

            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new TransChargesItemQuery("a");
            var item = new ItemQuery("b");
            var header = new TransChargesQuery("c");
            var reg = new RegistrationQuery("f");
            var pat = new PatientQuery("i");
            var lab = new ItemLaboratoryQuery("l");
            var appl = new AppStandardReferenceItemQuery("p");

            query.Select
                (
                    header.RegistrationNo,
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.ChargeQuantity,
                    query.SRItemUnit,
                    "<b.[ItemName] + case ISNULL(a.[ParamedicCollectionName],'') when '' then '' else (' (' + a.[ParamedicCollectionName] + ')') end as ItemName>",
                    header.TransactionDate,
                    pat.PatientName,
                    //header.ExecutionDate,
                    query.SpecimenCollectDateTime.As("txtSpecimenCollectDateTime"),
                    query.SpecimenCollectByUserID,
                    query.SpecimenReceiveDateTime,
                    query.SpecimenReceiveByUserID,
                    query.SRCollectMethod,
                    lab.SRSpecimenType,
                    appl.ItemID,
                    appl.ItemName.As("SRSpecimenTypes")

                );

            query.InnerJoin(header).On(query.TransactionNo == header.TransactionNo);
            query.InnerJoin(reg).On(header.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.InnerJoin(lab).On(query.ItemID == lab.ItemID);
            query.LeftJoin(appl).On(lab.SRSpecimenType == appl.ItemID && appl.StandardReferenceID == AppEnum.StandardReference.SpecimenType.ToString());

            query.Where(
                header.RegistrationNo == Request.QueryString["reg"].ToString(),
                query.Or(
                        header.PackageReferenceNo == string.Empty,
                        header.PackageReferenceNo.IsNull()
                        ),
                header.IsVoid == false,
                query.IsVoid == false,
                query.Or(
                    query.ParentNo == string.Empty,
                    query.ParentNo.IsNull()
                    ),
                header.TransactionNo == Request.QueryString["id"].ToString()
                );

            query.OrderBy
                (
                    header.TransactionDate.Ascending,
                    query.TransactionNo.Ascending,
                    query.SequenceNo.Ascending
                );

            DataTable tbl = query.LoadDataTable();

            tbl.AcceptChanges();

            grdList.DataSource = tbl;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                string sequenceNo = dataItem.GetDataKeyValue("SequenceNo").ToString();

                var tci = new TransChargesItem();
                tci.Query.Where(tci.Query.TransactionNo == Request.QueryString["id"], tci.Query.SequenceNo == sequenceNo);
                tci.Load(tci.Query);

                var specimenCollectDate = (RadDatePicker)dataItem.FindControl("txtSpecimenCollectDateTime");
                var specimenCollectTime = (RadTimePicker)dataItem.FindControl("txtSpecimenCollectTime");
                var specimenReceivedDate = (RadDatePicker)dataItem.FindControl("txtSpecimenReceiveDateTime");
                var specimenReceivedTime = (RadTimePicker)dataItem.FindControl("txtSpecimenReceiveTime");
                var collectMethod = (RadComboBox)dataItem.FindControl("cboSRCollectMethod");


                if (tci.SpecimenCollectDateTime.HasValue)
                {
                    specimenCollectDate.SelectedDate = tci.SpecimenCollectDateTime.Value.Date;
                    specimenCollectTime.SelectedTime = tci.SpecimenCollectDateTime.Value.TimeOfDay;
                }
                else
                {
                    specimenCollectDate.SelectedDate = DateTime.Now.Date;
                    specimenCollectTime.SelectedTime = DateTime.Now.TimeOfDay;
                }
                if (!string.IsNullOrEmpty(tci.SRCollectMethod))
                {
                    collectMethod.SelectedValue = tci.SRCollectMethod;
                    collectMethod.DataSource = GetCollectMethods();
                    collectMethod.DataBind();
                }

                if (tci.SpecimenReceiveDateTime.HasValue)
                {
                    specimenReceivedDate.SelectedDate = tci.SpecimenReceiveDateTime.Value.Date;
                    specimenReceivedTime.SelectedTime = tci.SpecimenReceiveDateTime.Value.TimeOfDay;
                }
                else
                {
                    specimenReceivedDate.SelectedDate = DateTime.Now.Date;
                    specimenReceivedTime.SelectedTime = DateTime.Now.TimeOfDay;
                }

                if (Request.QueryString["sc"] == "0")
                {
                    specimenReceivedDate.Enabled = false;
                    specimenReceivedTime.Enabled = false;
                }
            }
        }

        protected void cboSRCollectMethod_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRCollectMethod_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.CollectMethod.ToString(),
                        query.IsActive == true, query.ItemName.Like("%" + e.Text + "%"));
            query.Select(query.ItemID, query.ItemName);
            var dts = query.LoadDataTable();

            var combo = o as RadComboBox;
            combo.DataSource = dts;
            combo.DataBind();
        }

        private DataTable GetCollectMethods()
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.CollectMethod.ToString(),
                        query.IsActive == true);
            query.Select(query.ItemID, query.ItemName);
            return query.LoadDataTable();
        }

        public override bool OnButtonOkClicked()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                string sequenceNo = dataItem.GetDataKeyValue("SequenceNo").ToString();

                var tci = new TransChargesItem();
                tci.Query.Where(tci.Query.TransactionNo == Request.QueryString["id"], tci.Query.SequenceNo == sequenceNo);
                tci.Load(tci.Query);

                var specimenCollectDate = (RadDatePicker)dataItem.FindControl("txtSpecimenCollectDateTime");
                var specimenCollectTime = (RadTimePicker)dataItem.FindControl("txtSpecimenCollectTime");
                var specimenReceivedDate = (RadDatePicker)dataItem.FindControl("txtSpecimenReceiveDateTime");
                var specimenReceivedTime = (RadTimePicker)dataItem.FindControl("txtSpecimenReceiveTime");
                var collectMethod = (RadComboBox)dataItem.FindControl("cboSRCollectMethod");

                bool isCollectMethodSelected = collectMethod.SelectedValue != null && collectMethod.SelectedValue != "";

                var ilab = new ItemLaboratory();
                ilab.LoadByPrimaryKey(tci.ItemID);
                ilab.Query.Where(ilab.ItemID == tci.ItemID);

                if (string.IsNullOrEmpty(ilab.SRSpecimenType))
                {
                    ShowInformationHeader(string.Format("Please select Specimen Type first in Master Item Laboratory."));
                    return false;
                }

                if (isCollectMethodSelected && Request.QueryString["sc"] == "0")
                {                    
                    tci.SpecimenCollectDateTime = specimenCollectDate.SelectedDate.Value.Add(specimenCollectTime.SelectedTime.Value);
                    tci.SpecimenCollectByUserID = AppSession.UserLogin.UserID;
                    tci.SRCollectMethod = collectMethod.SelectedValue;
                    tci.Save();
                }

                if (tci.SRCollectMethod == null && Request.QueryString["sc"] == "1")
                {
                    if (isCollectMethodSelected)
                    {                        
                        tci.SpecimenCollectDateTime = specimenCollectDate.SelectedDate.Value.Add(specimenCollectTime.SelectedTime.Value);
                        tci.SpecimenCollectByUserID = AppSession.UserLogin.UserID;
                        tci.SRCollectMethod = collectMethod.SelectedValue;
                        tci.SpecimenReceiveDateTime = specimenReceivedDate.SelectedDate.Value.Add(specimenReceivedTime.SelectedTime.Value);
                        tci.SpecimenReceiveByUserID = AppSession.UserLogin.UserID;
                        tci.Save();
                    }
                }
                if (isCollectMethodSelected && Request.QueryString["sc"] == "1")
                {
                    tci.SpecimenReceiveDateTime = specimenReceivedDate.SelectedDate.Value.Add(specimenReceivedTime.SelectedTime.Value);
                    tci.SpecimenReceiveByUserID = AppSession.UserLogin.UserID;
                    tci.Save();
                }
                                
            }

            return true;
        }

    }
}