using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.Module.HR.TrainingHR
{
    public partial class EmployeeTrainingDetailItemCompDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboComponentID, AppEnum.StandardReference.EmployeeTrainingComponent);


            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }

            ViewState["IsNewRecord"] = false;



            (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;





            cboComponentID.SelectedValue = (string)DataBinder.Eval(DataItem, EmployeeTrainingItemMetadata.ColumnNames.SRComponentID);
            cboComponentID.Enabled = false;


        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ////Check duplicate key
            //if (ViewState["IsNewRecord"].Equals(true))
            //{
            //    var coll = (EmployeeTrainingItemCollection)Session["collEmployeeTrainingItem"];


            //    string regType = cboComponentID.SelectedValue;
            //    bool isExist = false;
            //    foreach (EmployeeTrainingItem item in coll)
            //    {
            //        if (item.SRSubComponentID.Equals(regType))
            //        {
            //            isExist = true;
            //            break;
            //        }
            //    }
            //    if (isExist)
            //    {
            //        args.IsValid = false;
            //        ((CustomValidator)source).ErrorMessage = string.Format("Data Registration Type: {0} already exist", cboComponentID.Text);
            //    }
            //}

        }

        private void PopulateTariffComponent(bool isAddNewRecord)
        {
            //if (isAddNewRecord)
            //{
            //    var itemTariffCompQ = new ItemTariffComponentQuery("a");
            //    var tariffCompQ = new TariffComponentQuery("b");
            //    itemTariffCompQ.InnerJoin(tariffCompQ).On(itemTariffCompQ.TariffComponentID == tariffCompQ.TariffComponentID);
            //    itemTariffCompQ.Select(itemTariffCompQ.TariffComponentID, tariffCompQ.TariffComponentName);
            //    //itemTariffCompQ.Where(itemTariffCompQ.ItemID == TxtItemId.Text);
            //    itemTariffCompQ.OrderBy(itemTariffCompQ.TariffComponentID.Ascending);
            //    itemTariffCompQ.es.Distinct = true;
            //    DataTable dtb = itemTariffCompQ.LoadDataTable();
            //    foreach (DataRow row in dtb.Rows)
            //    {
            //        //cboTariffComponentID.Items.Add(new RadComboBoxItem(row["TariffComponentName"].ToString(), row["TariffComponentID"].ToString()));
            //    }

            //    var tci = new TransChargesItemQuery("tci");
            //    var tcic = new TransChargesItemCompQuery("tcic");
            //    tci.InnerJoin(tcic).On(tci.TransactionNo == tcic.TransactionNo)
            //        .Where(tci.ItemID == TxtItemId.Text)
            //        .Select(tcic.TariffComponentID);
            //    tci.es.Distinct = true;
            //    var tbl = tci.LoadDataTable();
            //    var tcColl = new TariffComponentCollection();
            //    tcColl.LoadAll();

            //    foreach (System.Data.DataRow row in tbl.AsEnumerable()
            //        .Where(x => !dtb.AsEnumerable()
            //                    .Select(y => y["TariffComponentID"].ToString())
            //                    .Contains(x[0].ToString()))) {
            //        cboTariffComponentID.Items.Add(new RadComboBoxItem(
            //            tcColl.Where(x => x.TariffComponentID == row[0].ToString()).Select(x => x.TariffComponentName).FirstOrDefault(),
            //            row[0].ToString()));
            //    }
            //}
            //else
            //{
            //    var tariffCompQ = new TariffComponentQuery("a");
            //    tariffCompQ.Select(tariffCompQ.TariffComponentID, tariffCompQ.TariffComponentName);
            //    tariffCompQ.OrderBy(tariffCompQ.TariffComponentID.Ascending);
            //    DataTable dtb = tariffCompQ.LoadDataTable();
            //    foreach (DataRow row in dtb.Rows)
            //    {
            //        cboTariffComponentID.Items.Add(new RadComboBoxItem(row["TariffComponentName"].ToString(), row["TariffComponentID"].ToString()));
            //    }
            //}
        }

        //private void PopulateCombo(bool isCOA, RadComboBox comboBox, int? value)
        //{
        //    if (isCOA)
        //    {
        //        var coa = new ChartOfAccountsQuery();
        //        coa.Where(coa.ChartOfAccountId == (value ?? 0));

        //        comboBox.DataSource = coa.LoadDataTable();
        //    }
        //    else
        //    {
        //        var sub = new SubLedgersQuery();
        //        sub.Where(sub.SubLedgerId == (value ?? 0));

        //        comboBox.DataSource = sub.LoadDataTable();
        //    }
        //    comboBox.DataBind();

        //    if (comboBox.Items.Count > 0)
        //        comboBox.SelectedValue = (value ?? 0).ToString();
        //}



        public string SRComponentID
        {
            get
            {
                return cboComponentID.SelectedValue;
            }
        }

        public String ComponentName
        {
            get
            {
                return cboComponentID.Text;
            }
        }

        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }

    }
}
