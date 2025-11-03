using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ItemProductMarginValueDetail : BaseUserControl
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
                //ViewState["SequenceNo"] = "-1";

                var coll = (ItemProductMarginClassValueCollection)Session["collItemProductMarginClassValue"];
                if (coll.Count == 0) ViewState["SequenceNo"] = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1).SingleOrDefault();
                    int seqNo = sequenceNo.ToInt() + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo); //seqNo.ToString();
                }

                grdItemProductMarginValue_NeedDataSource(null, null);
                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (string)DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.SequenceNo);
            txtStartingValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.StartingValue));
            txtEndingValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.EndingValue));
            txtMarginPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.MarginPercentage));
            chkIsGlobalWithoutVAT.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.IsGlobalWithoutVAT));

            txtInpatientMarginPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.InpatientMarginPercentage));
            chkIsIpWithoutVAT.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.IsIpWithoutVAT));
            txtOutpatientMarginPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.OutpatientMarginPercentage));
            chkIsOpWithoutVAT.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.IsOpWithoutVAT));
            txtOTCMarginPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.OTCMarginPercentage));
            chkIsOtcWithoutVAT.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.IsOtcWithoutVAT));
            txtEmergencyMarginPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.EmergencyMarginPercentage));
            chkIsEmWithoutVAT.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ItemProductMarginValueMetadata.ColumnNames.IsEmWithoutVAT));

            grdItemProductMarginValue_NeedDataSource(null, null);
        }

        protected void grdItemProductMarginValue_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var session = (Session["collItemProductMarginClassValue"] as ItemProductMarginClassValueCollection).Where(s => s.SequenceNo == ViewState["SequenceNo"].ToString());

            var cls = new ClassQuery();
            cls.Select(cls.ClassID, cls.ClassName, "<CAST(0 AS NUMERIC(18, 2)) AS MarginValuePercentage>");
            cls.Where(cls.IsInPatientClass == true, cls.IsActive == true);
            cls.OrderBy(cls.ClassName.Ascending);
            var dtb = cls.LoadDataTable();

            if (session.Any())
            {
                foreach (DataRow row in dtb.Rows)
                {
                    var s = session.Where(n => n.ClassID == row["ClassID"].ToString()).SingleOrDefault();
                    if (s == null) continue;
                    row["MarginValuePercentage"] = s.MarginValuePercentage ?? 0;
                }
            }

            grdItemProductMarginValue.DataSource = dtb;
            grdItemProductMarginValue.DataBind();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemProductMarginValueCollection coll = (ItemProductMarginValueCollection)Session["collItemProductMarginValue"];

                double sValue = txtStartingValue.Value ?? 0;
                bool isExist = false;
                foreach (ItemProductMarginValue item in coll)
                {
                    if (item.StartingValue.Equals(sValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Starting Value: {0} has exist", sValue);
                }
            }
        }

        #region Properties for return entry value

        public string SequenceNo
        {
            get { return ViewState["SequenceNo"].ToString(); }
        }

        public Decimal StartingValue
        {
            get { return Convert.ToDecimal(txtStartingValue.Value); }
        }

        public Decimal EndingValue
        {
            get { return Convert.ToDecimal(txtEndingValue.Value); }
        }

        public Decimal MarginPercentage
        {
            get { return Convert.ToDecimal(txtMarginPercentage.Value); }
        }

        public bool IsGlobalWithoutVAT
        {
            get { return chkIsGlobalWithoutVAT.Checked; }
        }

        public Boolean IsMinusDiscount
        {
            get { return chkIsMinusDiscount.Checked; }
        }

        public Decimal InpatientMarginPercentage
        {
            get { return Convert.ToDecimal(txtInpatientMarginPercentage.Value); }
        }

        public bool IsIpWithoutVAT
        {
            get { return chkIsIpWithoutVAT.Checked; }
        }

        public Decimal OutpatientMarginPercentage
        {
            get { return Convert.ToDecimal(txtOutpatientMarginPercentage.Value); }
        }

        public bool IsOpWithoutVAT
        {
            get { return chkIsOpWithoutVAT.Checked; }
        }

        public Decimal OTCMarginPercentage
        {
            get { return Convert.ToDecimal(txtOTCMarginPercentage.Value); }
        }

        public bool IsOtcWithoutVAT
        {
            get { return chkIsOtcWithoutVAT.Checked; }
        }

        public Decimal EmergencyMarginPercentage
        {
            get { return Convert.ToDecimal(txtEmergencyMarginPercentage.Value); }
        }

        public bool IsEmWithoutVAT
        {
            get { return chkIsEmWithoutVAT.Checked; }
        }

        public GridDataItemCollection GridItemProductMarginValue
        {
            get { return grdItemProductMarginValue.MasterTableView.Items; }
        }

        #endregion
    }
}