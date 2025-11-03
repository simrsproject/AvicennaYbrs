using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Common
{
    public class AutoCompleteBox
    {
        #region Standard Reference
        public static void Initialized(RadAutoCompleteBox autoCompleteBox, AppEnum.StandardReference standardReference, bool allowCustomEntry = true, bool allowTokenEditing = true, string delimiter=";")
        {
            Initialized(autoCompleteBox, standardReference.ToString(), allowCustomEntry, allowTokenEditing, delimiter);
        }
        private static void Initialized(RadAutoCompleteBox autoCompleteBox, string methodName, bool allowCustomEntry, bool allowTokenEditing, string delimiter)
        {
            autoCompleteBox.WebServiceSettings.Path = "~/WebService/AutoCompleteBoxDataService.asmx";
            autoCompleteBox.WebServiceSettings.Method = methodName;
            autoCompleteBox.Delimiter = delimiter;
            autoCompleteBox.AllowCustomEntry = allowCustomEntry;
            autoCompleteBox.TokensSettings.AllowTokenEditing = allowTokenEditing;
        }
        public static void Initialized(RadAutoCompleteBox autoCompleteBox, string itemsSepBySemiColon, bool allowCustomEntry = true, bool allowTokenEditing = true)
        {
            autoCompleteBox.AllowCustomEntry = allowCustomEntry;
            autoCompleteBox.TokensSettings.AllowTokenEditing = allowTokenEditing;
            autoCompleteBox.Delimiter = ";";

            if (itemsSepBySemiColon.Contains(";"))
            {
                autoCompleteBox.DataSource = itemsSepBySemiColon.Split(';');
            }
            else
            {
                string[] itemsList = { itemsSepBySemiColon };
                autoCompleteBox.DataSource = itemsList;
            }
        }
        public static void SetValue(RadAutoCompleteBox autoCompleteBox, AppEnum.StandardReference standardReference, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                autoCompleteBox.Entries.Clear();
            }
            else
            {
                var coll = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.InfusLocation);
                var vals = value.Split(';');
                foreach (var loc in vals)
                {
                    if (string.IsNullOrWhiteSpace(loc)) continue;

                    var isExist = false;
                    foreach (var item in coll)
                    {
                        if (loc.Trim().ToLower() == item.ItemName.Trim().ToLower())
                        {
                            isExist = true;
                            autoCompleteBox.Entries.Add(new AutoCompleteBoxEntry(item.ItemName, item.ItemID));
                            break;
                        }
                    }

                    if (!isExist && autoCompleteBox.AllowCustomEntry)
                        autoCompleteBox.Entries.Add(new AutoCompleteBoxEntry(loc, null));
                }
            }
        }
        public static void SetValue(RadAutoCompleteBox autoCompleteBox, AppEnum.AutoCompleteBox standardReference, string value)
        {
            var coll = StandardReference.LoadStandardReferenceItemCollection(AppEnum.StandardReference.InfusLocation);
            var vals = value.Split(';');
            foreach (var loc in vals)
            {
                if (string.IsNullOrWhiteSpace(loc)) continue;

                var isExist = false;
                foreach (var item in coll)
                {
                    if (loc.Trim().ToLower() == item.ItemName.Trim().ToLower())
                    {
                        isExist = true;
                        autoCompleteBox.Entries.Add(new AutoCompleteBoxEntry(item.ItemName, item.ItemID));
                        break;
                    }
                }

                if (!isExist && autoCompleteBox.AllowCustomEntry)
                    autoCompleteBox.Entries.Add(new AutoCompleteBoxEntry(loc, null));
            }
        }
        #endregion


        public static void SetValue(RadAutoCompleteBox autoCompleteBox, string textDisplay, char delimiter = ';')
        {
            if (string.IsNullOrWhiteSpace(textDisplay)) return;

            if (!textDisplay.Contains(delimiter))
                textDisplay = textDisplay + delimiter;

            var vals = textDisplay.Split(delimiter);
            autoCompleteBox.Entries.Clear();
            foreach (var loc in vals)
            {
                if (string.IsNullOrWhiteSpace(loc)) continue;
                    autoCompleteBox.Entries.Add(new AutoCompleteBoxEntry(loc, null));
            }
        }


        public static void Initialized(RadAutoCompleteBox autoCompleteBox, AppEnum.AutoCompleteBox lookUp, bool allowCustomEntry = true, bool allowTokenEditing = true, string delimiter=";")
        {
            Initialized(autoCompleteBox, lookUp.ToString(), allowCustomEntry, allowTokenEditing, delimiter);
        }
    }
}
