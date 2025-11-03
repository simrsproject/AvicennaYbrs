using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Common
{
    public class StandardReference
    {
        public static void InitializeWithOneRow(RadComboBox comboBox, AppEnum.StandardReference standardReference, string selectedValue)
        {
            comboBox.Items.Clear();
            comboBox.Text = string.Empty;
            if (selectedValue == null)
                return;

            var item = new AppStandardReferenceItem();
            if (item.LoadByPrimaryKey(standardReference.ToString(), selectedValue))
            {
                comboBox.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                comboBox.SelectedIndex = 0;
            }
        }

        public static AppStandardReferenceItem LoadStandardReferenceItem(AppEnum.StandardReference standardReference, string itemID)
        {
            var stdi = new AppStandardReferenceItem();
            stdi.LoadByPrimaryKey(standardReference.ToString(), itemID);
            return stdi;
        }
        public static string GetItemName(AppEnum.StandardReference standardReference, string itemID)
        {
            if (itemID == null) return string.Empty;

            var qr = new AppStandardReferenceItemQuery("a");
            qr.Select(qr.ItemName);
            qr.Where(qr.StandardReferenceID == standardReference.ToString(), qr.ItemID== itemID);

            var ent = new AppStandardReferenceItem();
            if (ent.Load(qr))
                return ent.ItemName;

            return string.Empty;
        }
        public static AppStandardReferenceItemCollection LoadStandardReferenceItemCollection(AppEnum.StandardReference standardReference, string searchString, bool isShortByLineNumber = false)
        {
            return LoadStandardReferenceItemCollection(standardReference.ToString(), searchString, isShortByLineNumber);
        }
        public static AppStandardReferenceItemCollection LoadStandardReferenceItemCollection(string standardReference, string searchString, bool isShortByLineNumber = false, string refId=null)
        {
            var coll = new AppStandardReferenceItemCollection();
            coll.Query.Where(
                coll.Query.StandardReferenceID == standardReference,
                coll.Query.IsActive == true
            );
            if (!string.IsNullOrEmpty(searchString))
            {
                coll.Query.Where(coll.Query.Or(
                    coll.Query.ItemName.Like(string.Format("%{0}%", searchString)),
                    coll.Query.ItemID.Like(string.Format("{0}%", searchString))));
            }

            if (!string.IsNullOrEmpty(refId))
                coll.Query.Where(coll.Query.Or(coll.Query.ReferenceID == refId,
                                                         coll.Query.ReferenceID.IsNull(),
                                                         coll.Query.ReferenceID == string.Empty));

            if (isShortByLineNumber)
                coll.Query.OrderBy(coll.Query.LineNumber.Ascending, coll.Query.ItemName.Ascending);
            else
                coll.Query.OrderBy(coll.Query.ItemName.Ascending);
            coll.LoadAll();
            return coll;
        }

        public static AppStandardReferenceItemCollection LoadStandardReferenceItemCollection(AppEnum.StandardReference standardReference)
        {
            return LoadStandardReferenceItemCollection(standardReference, string.Empty);
        }

        public static void Initialize(RadComboBox comboBox, AppEnum.StandardReference standardReference)
        {
            var coll = LoadStandardReferenceItemCollection(standardReference);
            comboBox.DataSource = coll;
            comboBox.DataTextField = AppStandardReferenceItemMetadata.ColumnNames.ItemName;
            comboBox.DataValueField = AppStandardReferenceItemMetadata.ColumnNames.ItemID;
            comboBox.DataBind();
        }

        public static void Initialize(RadComboBox comboBox, AppEnum.StandardReference standardReference, bool isOrderByItemId)
        {
            var coll = new AppStandardReferenceItemCollection();
            coll.Query.Where(
                coll.Query.StandardReferenceID == standardReference.ToString(),
                coll.Query.IsActive == true
            );
            coll.Query.OrderBy(isOrderByItemId ? coll.Query.ItemID.Ascending : coll.Query.ItemName.Ascending);
            coll.LoadAll();
            comboBox.DataSource = coll;
            comboBox.DataTextField = AppStandardReferenceItemMetadata.ColumnNames.ItemName;
            comboBox.DataValueField = AppStandardReferenceItemMetadata.ColumnNames.ItemID;
            comboBox.DataBind();
        }

        public static void InitializeIncludeSpace(RadComboBox comboBox, AppEnum.StandardReference standardReference, string[] itemIdExclude)
        {
            InitializeIncludeSpace(comboBox, standardReference, itemIdExclude, false);
        }
        public static void InitializeIncludeSpace(RadComboBox comboBox, string standardReference, string[] itemIdExclude)
        {
            InitializeIncludeSpace(comboBox, standardReference, itemIdExclude, false);
        }

        public static void InitializeIncludeSpace(RadComboBox comboBox, AppEnum.StandardReference standardReference, string[] itemIdExclude, bool isSortByName)
        {
            InitializeIncludeSpace(comboBox, standardReference.ToString(), itemIdExclude, isSortByName);
        }
        public static void InitializeIncludeSpace(RadComboBox comboBox, string standardReference, string[] itemIdExclude, bool isSortByName)
        {
            var collTitle = new AppStandardReferenceItemCollection();
            collTitle.Query.Where(
                collTitle.Query.StandardReferenceID == standardReference,
                collTitle.Query.IsActive == true
            );
            if (itemIdExclude.Length > 0)
            {
                collTitle.Query.Where(collTitle.Query.ItemID.NotIn(itemIdExclude));
            }
            if (isSortByName)
            {
                collTitle.Query.OrderBy(collTitle.Query.ItemName.Ascending);
            }
            else
            {
                collTitle.Query.OrderBy(collTitle.Query.ItemID.Ascending);
            }
            collTitle.LoadAll();

            var val = comboBox.SelectedValue;
            comboBox.Items.Clear();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var item in collTitle)
            {
                comboBox.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }

            if (!string.IsNullOrEmpty(val))
                ComboBox.SelectedValue(comboBox, val);
        }

        public static void InitializeIncludeSpaceSortByLineNumber(RadComboBox comboBox, AppEnum.StandardReference standardReference, string[] itemIdExclude)
        {
            var collTitle = new AppStandardReferenceItemCollection();
            collTitle.Query.Where(
                collTitle.Query.StandardReferenceID == standardReference.ToString(),
                collTitle.Query.IsActive == true
            );
            if (itemIdExclude.Length > 0)
            {
                collTitle.Query.Where(collTitle.Query.ItemID.NotIn(itemIdExclude));
            }
            collTitle.Query.OrderBy(collTitle.Query.LineNumber.Ascending, collTitle.Query.ItemID.Ascending);

            collTitle.LoadAll();

            var val = comboBox.SelectedValue;
            comboBox.Items.Clear();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var item in collTitle)
            {
                comboBox.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }

            if (!string.IsNullOrEmpty(val))
                ComboBox.SelectedValue(comboBox, val);
        }

        public static void InitializeIncludeSpace(RadComboBox comboBox, AppEnum.StandardReference standardReference, bool isSortByName)
        {
            InitializeIncludeSpace(comboBox, standardReference, new string[] { }, isSortByName);
        }
        public static void InitializeIncludeSpace(RadComboBox comboBox, string standardReference, bool isSortByName)
        {
            InitializeIncludeSpace(comboBox, standardReference, new string[] { }, isSortByName);
        }

        public static void InitializeIncludeSpaceSortByLineNumber(RadComboBox comboBox, AppEnum.StandardReference standardReference)
        {
            InitializeIncludeSpaceSortByLineNumber(comboBox, standardReference, new string[] { });
        }

        public static void InitializeIncludeSpace(RadComboBox comboBox, AppEnum.StandardReference standardReference)
        {
            InitializeIncludeSpace(comboBox, standardReference, new string[] { });
        }

        public static void InitializeIncludeSpace(RadDropDownList downList, AppEnum.StandardReference standardReference, bool isShortByLineNumber = false, string refId = null)
        {
            InitializeIncludeSpace(downList, standardReference.ToString(), isShortByLineNumber, refId);
        }

        public static void InitializeIncludeSpace(RadDropDownList downList, string standardReference, bool isShortByLineNumber = false, string refId = null)
        {
            var val = downList.SelectedValue;

            var collTitle = LoadStandardReferenceItemCollection(standardReference, string.Empty, isShortByLineNumber, refId);
            downList.Items.Clear();
            downList.Items.Add(new DropDownListItem(string.Empty, string.Empty));
            foreach (var item in collTitle)
            {
                downList.Items.Add(new DropDownListItem(item.ItemName, item.ItemID));
            }

            if (!string.IsNullOrEmpty(val))
                ComboBox.SelectedValue(downList, val);
        }

        //public static void InitializeIncludeSpace(RadComboBox comboBox, AppEnum.StandardReference standardReference)
        //{
        //    var val = comboBox.SelectedValue;

        //    var collTitle = LoadStandardReferenceItemCollection(standardReference);

        //    comboBox.Items.Clear();
        //    comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
        //    foreach (var item in collTitle)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
        //    }

        //    if (!string.IsNullOrEmpty(val))
        //        ComboBox.SelectedValue(comboBox, val);
        //}

        public static void InitializeIncludeSpace(RadComboBox comboBox, AppEnum.StandardReference standardReference, string refId)
        {
            var val = comboBox.SelectedValue;
            var collTitle = new AppStandardReferenceItemCollection();
            collTitle.Query.Where(
                collTitle.Query.StandardReferenceID == standardReference.ToString(),
                collTitle.Query.IsActive == true
                );
            if (!string.IsNullOrEmpty(refId))
                collTitle.Query.Where(collTitle.Query.Or(collTitle.Query.ReferenceID == refId,
                                                         collTitle.Query.ReferenceID.IsNull(),
                                                         collTitle.Query.ReferenceID == string.Empty));
            collTitle.Query.OrderBy(collTitle.Query.ItemID.Ascending);
            collTitle.LoadAll();
            comboBox.Items.Clear();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var item in collTitle)
            {
                comboBox.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }
            if (!string.IsNullOrEmpty(val))
                ComboBox.SelectedValue(comboBox, val);

        }

        public static void InitializeIncludeSpaceOrderByRefId(RadComboBox comboBox, AppEnum.StandardReference standardReference)
        {
            var collTitle = new AppStandardReferenceItemCollection();
            collTitle.Query.Where(
                collTitle.Query.StandardReferenceID == standardReference.ToString(),
                collTitle.Query.IsActive == true
                );
            collTitle.Query.OrderBy(collTitle.Query.ReferenceID.Ascending);
            collTitle.LoadAll();
            comboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var item in collTitle)
            {
                comboBox.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }
        }

        public static void PopulateCboSRItemBin(RadComboBox comboBox, string textSearch, string ReferenceID)
        {
            PopulateCboSR(comboBox, "ItemBin", textSearch, ReferenceID);
        }
        public static void PopulateCboSR(RadComboBox comboBox, string StandardReferenceID, string textSearch, string ReferenceID)
        {
            if (textSearch == null) textSearch = string.Empty;

            AppStandardReferenceItemQuery query = new AppStandardReferenceItemQuery("a");
            query.Select(query.ItemID, query.ItemName);
            query.Where(query.StandardReferenceID == StandardReferenceID, query.ReferenceID == ReferenceID,
                query.Or(
                    query.ItemID == textSearch,
                    query.ItemName.Like(string.Format("%{0}%", textSearch))
                )
            );
            query.OrderBy(query.ItemName.Ascending);
            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            // add row kosong
            var r = dtb.NewRow();
            r["ItemID"] = "";
            r["ItemName"] = "";
            dtb.Rows.InsertAt(r, 0);

            comboBox.DataSource = dtb;
            comboBox.DataBind();

            if (comboBox.FindItemByValue(textSearch) != null)
            {
                comboBox.SelectedValue = textSearch;
            }
        }
    }
}
