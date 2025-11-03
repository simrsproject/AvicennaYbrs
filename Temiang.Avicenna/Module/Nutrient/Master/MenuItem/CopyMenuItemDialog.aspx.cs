using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class CopyMenuItemDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MenuItem;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtMenuItemID.Text = Request.QueryString["id"];
                var menuItem = new MenuItem();
                if (menuItem.LoadByPrimaryKey(txtMenuItemID.Text))
                {
                    txtMenuItemName.Text = menuItem.MenuItemName;
                    txtMenuID.Text = menuItem.MenuID;
                    var menu = new Menu();
                    menu.LoadByPrimaryKey(txtMenuID.Text);
                    txtMenu.Text = menu.MenuName;
                    txtVersionID.Text = menuItem.VersionID;
                    var menuVersion = new MenuVersion();
                    menuVersion.LoadByPrimaryKey(txtVersionID.Text);
                    txtVersion.Text = menuVersion.VersionName;
                    txtSeqNo.Text = menuItem.SeqNo;
                    txtClassID.Text = menuItem.ClassID;
                    var cls = new Class();
                    cls.LoadByPrimaryKey(txtClassID.Text);
                    txtClass.Text = cls.ClassName;

                    ComboBox.PopulateWithInpatientClassTariff(cboClassID);

                    var mv = new MenuVersionQuery();
                    mv.Select(mv.Cycle);
                    mv.Where(mv.IsExtra == false, mv.IsActive == true);

                    mv.OrderBy(mv.Cycle.Descending);
                    mv.es.Top = 1;
                    DataTable mvdtb = mv.LoadDataTable();

                    var max = Convert.ToInt16(mvdtb.Rows[0]["Cycle"]);
                    var i = 1;
                    cboFromSeqNo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    while (i <= max)
                    {
                        cboFromSeqNo.Items.Add(i < 10
                                                   ? new RadComboBoxItem("0" + string.Format("{0}", i),
                                                                         "0" + string.Format("{0}", i))
                                                   : new RadComboBoxItem(string.Format("{0}", i),
                                                                         string.Format("{0}", i)));
                        i += 1;
                    }
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            if (!string.IsNullOrEmpty(cboClassID.SelectedValue) & !string.IsNullOrEmpty(cboFromSeqNo.SelectedValue))
            {
                if (txtClassID.Text != cboClassID.SelectedValue || txtSeqNo.Text != cboFromSeqNo.SelectedValue)
                {
                    var tempColl = new MenuItemFoodCollection();
                    var tempQ = new MenuItemFoodQuery("a");
                    var tempHdQ = new MenuItemQuery("b");
                    tempQ.InnerJoin(tempHdQ).On(tempQ.MenuItemID == tempHdQ.MenuItemID);
                    tempQ.Where(tempHdQ.MenuID == txtMenuID.Text, tempHdQ.VersionID == txtVersionID.Text,
                                tempHdQ.SeqNo == cboFromSeqNo.SelectedValue, tempHdQ.ClassID == cboClassID.SelectedValue,
                                tempHdQ.IsActive == true);
                    tempColl.Load(tempQ);

                    if (tempColl.Count > 0)
                    {
                        using (var trans = new esTransactionScope())
                        {
                            var coll = new MenuItemFoodCollection();
                            coll.Query.Where(coll.Query.MenuItemID == txtMenuItemID.Text);
                            coll.LoadAll();
                            coll.MarkAllAsDeleted();
                            coll.Save();

                            foreach (var temp in tempColl)
                            {
                                var c = coll.AddNew();
                                c.MenuItemID = txtMenuItemID.Text;
                                c.SRMealSet = temp.SRMealSet;
                                c.FoodID = temp.FoodID;
                                c.SRMenuItemFoodGroup = temp.SRMenuItemFoodGroup;
                                c.IsOptional = temp.IsOptional;
                                c.IsStandard = temp.IsStandard;
                                c.LastUpdateDateTime = DateTime.Now;
                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            }

                            coll.Save();

                            trans.Complete();
                        }
                    }
                }
            }
            
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }
    }
}
