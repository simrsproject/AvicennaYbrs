using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Common
{
    public class PopUpSearch
    {
        #region Base Initialize for Page

        private static void InitializeWDynamicFilter(AppEnum.PopUpSearch popUpSearch, Page page, RadInputControl inputControl, RadInputControl descControl, params string[] clientIDforFilterBases)
        {
            var filterBase = "";
            if (clientIDforFilterBases.Length > 1)
            {
                filterBase = clientIDforFilterBases.Aggregate(filterBase, (current, str) => current + ("filterBase = filterBase + $find('" + str + "').get_value() + '|';"));
                filterBase = filterBase.Substring(0, filterBase.Length - 1);
            }
            Initialize(popUpSearch, page, inputControl, descControl, filterBase);
        }

        private static void InitializeWStaticFilter(AppEnum.PopUpSearch popUpSearch, Page page, RadInputControl inputControl, RadInputControl descControl, string filterBase)
        {
            var newFilterBase = "filterBase = '" + filterBase + "'";

            RegisterClientScript(popUpSearch, page, inputControl, descControl, newFilterBase);

            InitializeOnButtonClick(popUpSearch, inputControl);
        }

        private static void Initialize(AppEnum.PopUpSearch popUpSearch, Page page, RadInputControl inputControl, RadInputControl descControl, string filterBase)
        {
            RegisterClientScript(popUpSearch, page, inputControl, descControl, filterBase);

            InitializeOnButtonClick(popUpSearch, inputControl);
        }

        public static void InitializeOnButtonClick(AppEnum.PopUpSearch popUpSearch, RadInputControl inputControl)
        {
            inputControl.ShowButton = true;
            inputControl.ClientEvents.OnButtonClick = string.Format("openPopUp{0}", popUpSearch);
        }

        private static void RegisterClientScript(AppEnum.PopUpSearch popUpSearch, Page page, RadInputControl inputControl, RadInputControl descControl, string filterBase)
        {
            if (!page.ClientScript.IsClientScriptIncludeRegistered("window"))
                page.ClientScript.RegisterClientScriptInclude("window", Helper.UrlRoot() + "/JavaScript/Windows.js");

            var scriptKey = "popup" + popUpSearch;
            if (!page.ClientScript.IsClientScriptBlockRegistered(scriptKey))
            {
                var script = GetOpenPopUpScript(popUpSearch, inputControl, descControl, filterBase);
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), scriptKey, script);
            }
        }

        private static void RegisterClientScript(AppEnum.PopUpSearch popUpSearch, Page page, RadInputControl inputControl, RadInputControl descControl, params string[] clientIDforFilterBases)
        {
            if (!page.ClientScript.IsClientScriptIncludeRegistered("window"))
                page.ClientScript.RegisterClientScriptInclude("window", Helper.UrlRoot() + "/JavaScript/Windows.js");

            string scriptKey = "popup" + popUpSearch;
            if (!page.ClientScript.IsClientScriptBlockRegistered(scriptKey))
            {
                string filterBase = "";
                if (clientIDforFilterBases.Length > 1)
                {
                    foreach (string str in clientIDforFilterBases)
                    {
                        filterBase += "filterBase = filterBase + $find('" + str + "').get_value() + '|';";
                    }
                    filterBase = filterBase.Substring(0, filterBase.Length - 1);
                }
                string script = GetOpenPopUpScript(popUpSearch, inputControl, descControl, filterBase);
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), scriptKey, script);
            }
        }

        private static string GetOpenPopUpScript(AppEnum.PopUpSearch popUpSearch, RadInputControl inputControl, RadInputControl descControl, string filterBase)
        {
            var script = "";
            script += "<script type=\"text/javascript\">\n";
            script += "//<![CDATA[\n";
            script += string.Format("function openPopUp{0}(sender, args)\n", popUpSearch);
            script += "{\n";

            if (filterBase.Contains("filterBase"))
                script += "var filterBase='';" + filterBase + ";";
            else
                script += "var filterBase='" + filterBase + "';";

            if (descControl != null)
            {
                script +=
                    string.Format(
                        "  openPopupCenter(\"" + Helper.UrlRoot() + "/SearchPopUp/StandardPopUp.aspx?type={0}&id1={1}&id2={2}&filterBase=\"+filterBase, 800, 445);",
                        popUpSearch, "\"+sender._clientID+\"", descControl.ClientID);
            }
            else
            {
                script +=
                    string.Format(
                        "  openPopupCenter(\"" + Helper.UrlRoot() + "/SearchPopUp/StandardPopUp.aspx?type={0}&id1={1}&filterBase=\"+filterBase, 800, 445);",
                        popUpSearch, "\"+sender._clientID+\"");
            }

            script += "}\n";
            script += "//]]>\n";
            script += "</script>";
            return script;
        }

        private static void RegisterClientScript(AppEnum.PopUpSearch popUpSearch, Page page, HiddenField hiddenField)
        {
            if (!page.ClientScript.IsClientScriptIncludeRegistered("window"))
                page.ClientScript.RegisterClientScriptInclude("window", Helper.UrlRoot() + "/JavaScript/Windows.js");

            var scriptKey = "popup" + popUpSearch;
            if (!page.ClientScript.IsClientScriptBlockRegistered(scriptKey))
            {
                var filterBase = "filterBase = document.getElementById('" + hiddenField.ClientID + "').value;";
                var script = GetOpenPopUpScript(popUpSearch, null, null, filterBase);
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), scriptKey, script);
            }
        }

        private static void RegisterClientScript(AppEnum.PopUpSearch popUpSearch, Page page, RadInputControl inputControl)
        {
            if (!page.ClientScript.IsClientScriptIncludeRegistered("window"))
                page.ClientScript.RegisterClientScriptInclude("window", Helper.UrlRoot() + "/JavaScript/Windows.js");

            var scriptKey = "popup" + popUpSearch;
            if (!page.ClientScript.IsClientScriptBlockRegistered(scriptKey))
            {
                var filterBase = "filterBase = $find('" + inputControl.ClientID + "').get_value();";
                var script = GetOpenPopUpScript(popUpSearch, null, null, filterBase);
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), scriptKey, script);
            }
        }

        #endregion

        #region Custom Initialize

        public static void RegisterClientScript(AppEnum.PopUpSearch popUpSearch, Page page)
        {
            RegisterClientScript(popUpSearch, page, null, null, string.Empty);
        }

        public static void RegisterClientScriptParamedicByRegistration(Page page, string registrationNo)
        {
            RegisterClientScript(AppEnum.PopUpSearch.ParamedicByRegistration, page, null, null, registrationNo);
        }

        public static void RegisterClientScriptParamedicByServiceUnit(Page page, HiddenField hiddenField)
        {
            RegisterClientScript(AppEnum.PopUpSearch.ParamedicByServiceUnitID, page, hiddenField);
        }

        public static void RegisterClientScriptMorphologyByDiagnose(Page page, string diagnoseID)
        {
            RegisterClientScript(AppEnum.PopUpSearch.MorphologyByDiagnoseID, page, null, null, diagnoseID);
        }

        public static void RegisterClientScriptItemBalanceByLocation(Page page, HiddenField hiddenField)
        {
            RegisterClientScript(AppEnum.PopUpSearch.ItemBalanceByLocation, page, hiddenField);
        }

        public static void RegisterClientScriptItemJobOrder(Page page, HiddenField hiddenField)
        {
            RegisterClientScript(AppEnum.PopUpSearch.ItemJobOrder, page, hiddenField);
        }

        public static void RegisterClientScriptParamedicClusterDetail(Page page, HiddenField hiddenField)
        {
            RegisterClientScript(AppEnum.PopUpSearch.ParamedicClusterDetail, page, hiddenField);
        }

        public static void RegisterClientScriptTariffByClass(Page page, string itemType, HiddenField hiddenField)
        {
            if (itemType == BusinessObject.Reference.ItemType.Service)
                RegisterClientScript(AppEnum.PopUpSearch.ItemServiceTariff, page, hiddenField);
            if (itemType == BusinessObject.Reference.ItemType.Diagnostic)
                RegisterClientScript(AppEnum.PopUpSearch.ItemDiagnosticTariff, page, hiddenField);
            if (itemType == BusinessObject.Reference.ItemType.Laboratory)
                RegisterClientScript(AppEnum.PopUpSearch.ItemLaboratoryTariff, page, hiddenField);
            if (itemType == BusinessObject.Reference.ItemType.NonMedical)
                RegisterClientScript(AppEnum.PopUpSearch.ItemProductNonMedicalTariff, page, hiddenField);
        }

        public static void RegisterClientScriptAssetByServiceUnit(Page page, RadInputControl serviceUnitID)
        {
            RegisterClientScript(AppEnum.PopUpSearch.AssetByServiceUnit, page, serviceUnitID);
        }

        public static void RegisterClientScriptItemTariffByServiceUnit(Page page, HiddenField hiddenField, string type)
        {
            RegisterClientScript(type == "tr" ? AppEnum.PopUpSearch.ItemTariffByServiceUnitTransaction : 
                                                AppEnum.PopUpSearch.ItemTariffByServiceUnitOrder, page, hiddenField);
        }

        public static void RegisterClientScriptItemTariffByServiceUnit(Page page, HiddenField hiddenField)
        {
            RegisterClientScript(AppEnum.PopUpSearch.ItemTariffByServiceUnitOrder, page, hiddenField);
        }

        public static void Initialize(AppEnum.PopUpSearch popUpSearch, Page page, RadInputControl inputControl)
        {
            InitializeWStaticFilter(popUpSearch, page, inputControl, null, "");
        }

        public static void InitializeServiceUnitVisitType(Page page, RadInputControl inputControl, RadInputControl serviceUnitControl)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.ServiceUnitVisitType, page, inputControl, null, serviceUnitControl.ClientID);
        }

        public static void InitializeServiceUnitVisitType(Page page, RadInputControl inputControl, string serviceUnitID)
        {
            InitializeWStaticFilter(AppEnum.PopUpSearch.ServiceUnitVisitType, page, inputControl, null, serviceUnitID);
        }

        public static void InitializeServiceUnitByRegistrationType(Page page, RadInputControl inputControl, string registrationType)
        {
            InitializeWStaticFilter(AppEnum.PopUpSearch.ServiceUnitByRegistrationType, page, inputControl, null, registrationType);
        }

        public static void InitializeRegistration(Page page, RadInputControl inputControl, string serviceUnitID)
        {
            InitializeWStaticFilter(AppEnum.PopUpSearch.Registration, page, inputControl, null, serviceUnitID);
        }

        public static void InitializeRegistration(Page page, RadInputControl inputControl, RadInputControl serviceUnitControl)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.Registration, page, inputControl, null, serviceUnitControl.ClientID);
        }

        public static void InitializeRegistration(Page page, RadInputControl inputControl, RadComboBox serviceUnitControl)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.Registration, page, inputControl, null, serviceUnitControl.ClientID);
        }

        public static void InitializeRegistrationByDepartment(Page page, RadInputControl inputControl, string departmentID)
        {
            InitializeWStaticFilter(AppEnum.PopUpSearch.Registration, page, inputControl, null, departmentID);
        }

        public static void InitializeRegistrationByRegType(Page page, RadInputControl inputControl, string registrationType)
        {
            InitializeWStaticFilter(AppEnum.PopUpSearch.RegistrationByRegType, page, inputControl, null, registrationType);
        }

        public static void InitializeServiceRoomByServiceUnit(Page page, RadInputControl inputControl, RadInputControl serviceUnitControl)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.ServiceRoomByServiceUnitID, page, inputControl, null, serviceUnitControl.ClientID);
        }

        public static void InitializeBedByServiceRoom(Page page, RadInputControl inputControl, RadInputControl serviceRoomControl)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.BedByServiceRoomID, page, inputControl, null, serviceRoomControl.ClientID);
        }

        public static void InitializeParamedicByServiceUnit(Page page, RadInputControl inputControl, RadInputControl serviceUnitControl)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.ParamedicByServiceUnitID, page, inputControl, null, serviceUnitControl.ClientID);
        }

        public static void InitializeMorphologyByDiagnose(Page page, RadInputControl inputControl, RadInputControl diagnoseControl)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.MorphologyByDiagnoseID, page, inputControl, null, diagnoseControl.ClientID);
        }

        public static void InitializeParamedicByRegistration(Page page, RadInputControl inputControl, string registratioNo)
        {
            InitializeWStaticFilter(AppEnum.PopUpSearch.ParamedicByRegistration, page, inputControl, null, registratioNo);
        }

        public static void InitializeTransactionNoByRegistration(Page page, RadInputControl inputControl, RadInputControl registrationNo)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.TransactionNoByRegistration, page, inputControl, null, registrationNo.ClientID);
        }

        public static void InitializeJobOrderNoByRegistration(Page page, RadInputControl inputControl, RadInputControl registrationNo)
        {
            InitializeWDynamicFilter(AppEnum.PopUpSearch.JobOrderNoByRegistration, page, inputControl, null, registrationNo.ClientID);
        }

        public static void InitializeAssetByServiceUnit(Page page, RadInputControl inputControl, RadComboBox serviceUnitControl, RadComboBox maintenanceUnitControl)
        {
            var arr = new string[2];
            arr.SetValue(serviceUnitControl.ClientID, 0);
            arr.SetValue(maintenanceUnitControl.ClientID, 1);

            InitializeWDynamicFilter(AppEnum.PopUpSearch.AssetByServiceUnit, page, inputControl, null, arr);
        }
        #endregion
    }
}