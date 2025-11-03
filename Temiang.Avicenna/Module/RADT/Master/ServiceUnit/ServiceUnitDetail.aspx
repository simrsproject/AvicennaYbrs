<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/PCareCommon/LookUp/PCareReferenceCtl.ascx" TagPrefix="uc2" TagName="PCareReferenceCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openItemClass(serviceUnitID, itemID) {
                var oWnd = $find("<%= winItemClass.ClientID %>");
                oWnd.SetUrl("ServiceUnitItemServiceClass.aspx?unitID=" + serviceUnitID + "&itemID=" + itemID);
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openItemComp(serviceUnitID, itemID) {
                var oWnd = $find("<%= winItemClass.ClientID %>");
                oWnd.SetUrl("ServiceUnitItemServiceComp.aspx?unitID=" + serviceUnitID + "&itemID=" + itemID);
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPickList() {
                var unit = $find("<%= txtServiceUnitID.ClientID %>");

                var oWnd = $find("<%= winItemClass.ClientID %>");
                oWnd.setUrl('ServiceUnitCoaMatrixDetail.aspx?id=' + unit.get_value());
                oWnd.set_title('Account Mapping');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPickListOp() {
                var unit = $find("<%= txtServiceUnitID.ClientID %>");

                var oWnd = $find("<%= winItemClass.ClientID %>");
                oWnd.setUrl('ServiceUnitCoaMatrixDetailOutPatient.aspx?id=' + unit.get_value());
                oWnd.set_title('Account Mapping');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPickListPA() {
                var unit = $find("<%= txtServiceUnitID.ClientID %>");
                var loc = $find("<%= txtLocationID.ClientID %>");
                var regt = $find("<%= cboSRRegistrationType.ClientID %>");

                var oWnd = $find("<%= winItemClass.ClientID %>");
                oWnd.setUrl('ServiceUnitCoaMatrixDetailProductAccount.aspx?id=' + unit.get_value() + "&loc=" + loc.get_value() + "&regt=" + regt.get_value() + "&type=1");
                oWnd.set_title('Account Mapping');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinPickListPA2(locId) {
                var unit = $find("<%= txtServiceUnitID.ClientID %>");
                var regt = $find("<%= cboSRRegistrationType.ClientID %>");

                var oWnd = $find("<%= winItemClass.ClientID %>");
                oWnd.setUrl('ServiceUnitCoaMatrixDetailProductAccount.aspx?id=' + unit.get_value() + "&loc=" + locId + "&regt=" + regt.get_value() + "&type=2");
                oWnd.set_title('Account Expense Mapping');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            function openScheduleGlobal(parId) {
                var oUnit = $find("<%= txtServiceUnitID.ClientID %>");
                var oWnd = $find("<%= winItemClass.ClientID %>");
                oWnd.SetUrl("../../Master/Paramedic/ParamedicGlobalScheduleDialog.aspx?parId=" + parId + "&unitId=" + oUnit.get_value());
                oWnd.Show();
                oWnd.Maximize();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winItemClass" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit ID required."
                                ValidationGroup="entry" ControlToValidate="txtServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitName" runat="server" Text="Service Unit Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" MaxLength="4000" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitName" runat="server" ErrorMessage="Service Unit Name required."
                                ValidationGroup="entry" ControlToValidate="txtServiceUnitName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trPcare">
                        <td colspan="4">
                            <uc2:PCareReferenceCtl runat="server" ReferenceType="PoliFktp" ID="pcareReference" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDepartmentID" runat="server" Text="Department"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="100px">
                                        <telerik:RadTextBox ID="txtDepartmentID" runat="server" Width="100px" MaxLength="10"
                                            AutoPostBack="true" OnTextChanged="txtDepartmentID_TextChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDepartmentName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvDepartmentID" runat="server" ErrorMessage="Department ID required."
                                ValidationGroup="entry" ControlToValidate="txtDepartmentID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtShortName" runat="server" Width="100px" MaxLength="35" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitOfficer" runat="server" Text="Service Unit Officer"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitOfficer" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblLocationID" runat="server" Text="Inventory Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td width="100px">
                                        <telerik:RadTextBox ID="txtLocationID" runat="server" Width="100px" MaxLength="10"
                                            AutoPostBack="true" OnTextChanged="txtLocationID_TextChanged" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLocationName" runat="server" CssClass="labeldescription" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRRegistrationType" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSRGenderType" runat="server" Text="Specifically for Gender"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRGenderType" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsGenerateMedicalNo" runat="server" Text="Generate Medical No" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsingJobOrder" runat="server" Text="Using Job Order" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Service Unit / Location Pharmacy (*)"
                                ToolTip="Default setting is located in AppParameter (ServiceUnitPharmacyID/ServiceUnitPharmacyIdOpr)"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboServiceUnitPharmacy" runat="server" Width="152px" AutoPostBack="true"
                                            OnSelectedIndexChanged="cboServiceUnitPharmacy_OnSelectedIndexChanged" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <telerik:RadComboBox ID="cboLocationPharmacy" runat="server" Width="152px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitPorID" runat="server" Text="Service Unit / Location POR"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox runat="server" ID="cboServiceUnitPorID" Width="152px" EnableLoadOnDemand="True"
                                            HighlightTemplatedItems="True" MarkFirstMatch="False" OnItemDataBound="cboServiceUnitPorID_ItemDataBound"
                                            OnItemsRequested="cboServiceUnitPorID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboServiceUnitPorID_OnSelectedIndexChanged">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Note : Show max 20 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td></td>
                                    <td>
                                        <telerik:RadComboBox ID="cboLocationPorID" runat="server" Width="152px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDefaultChargeClassID" runat="server" Text="Default Charge Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDefaultChargeClassID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Service Unit Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRServiceUnitGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Kemenkes Group
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboInpatientType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trMedicalFileFolderColor">
                        <td class="label">
                            <asp:Label ID="lblMedicalFileFolderColor" runat="server" Text="Medical File Folder Color"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadColorPicker ShowIcon="true" ID="txtMedicalFileFolderColor" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRBuilding" runat="server" Text="Building"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRBuilding" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlAccrual" Visible="false">
                        <tr style="display: none">
                            <td class="label" colspan="4">Inpatient Accrual
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="lblChartOfAccountIdIncome" runat="server" Text="Revenue (Temporary)"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdIncome" Width="300px"
                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboChartOfAccountIdIncome_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdIncome_ItemDataBound"
                                    OnItemsRequested="cboChartOfAccountIdIncome_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                            &nbsp;-&nbsp;
                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                        </b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr style="display: none">
                            <td class="label">
                                <asp:Label ID="lblSubledgerIdIncome" runat="server" Text="Subledger"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdIncome" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboSubledgerIdIncome_ItemDataBound"
                                    OnItemsRequested="cboSubledgerIdIncome_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChartOfAccountIdAcrual" runat="server" Text="COA Patient In Process (Accrual)"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdAcrual" Width="300px"
                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboChartOfAccountIdAcrual_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdAcrual_ItemDataBound"
                                    OnItemsRequested="cboChartOfAccountIdAcrual_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                            &nbsp;-&nbsp;
                                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                        </b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSubledgerIdAcrual" runat="server" Text="Subledger"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSubledgerIdAcrual" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboSubledgerIdAcrual_ItemDataBound"
                                    OnItemsRequested="cboSubledgerIdAcrual_ItemsRequested">
                                    <ItemTemplate>
                                        <b>
                                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                            &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdDiscount" runat="server" Text="COA Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdDiscount" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdDiscount_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountIdDiscount_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountIdDiscount_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdDiscount" runat="server" Text="Subledger Discount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdDiscount" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdDiscount_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdDiscount_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp; (<%# DataBinder.Eval(Container.DataItem, "Description")%>) </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCost" runat="server" Text="COA Cost Medic"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCost" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountIdCost_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdCost_ItemDataBound" OnItemsRequested="cboChartOfAccountIdCost_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCost" runat="server" Text="Subledger Cost Medic"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCost" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdCost_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdCost_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblChartOfAccountIdCostNonMedic" runat="server" Text="COA Cost Non Medic"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCostNonMedic" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCostNonMedic_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdCostNonMedic_ItemDataBound" OnItemsRequested="cboChartOfAccountIdCostNonMedic_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblSubledgerIdCostNonMedic" runat="server" Text="Subledger Cost Non Medic"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCostNonMedic" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCostNonMedic_ItemDataBound" OnItemsRequested="cboSubledgerIdCostNonMedic_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="COA Cost Paramedic Fee"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdCostParamedicFee" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountIdCostParamedicFee_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdCostParamedicFee_ItemDataBound" OnItemsRequested="cboChartOfAccountIdCostParamedicFee_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Subledger Cost Paramedic Fee"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdCostParamedicFee" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerIdCostParamedicFee_ItemDataBound" OnItemsRequested="cboSubledgerIdCostParamedicFee_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr id="trCoaPpnIn" runat="server">
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="COA Ppn In"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountIdPpnIn" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="true" OnSelectedIndexChanged="cboChartOfAccountIdPpnIn_SelectedIndexChanged"
                                OnItemDataBound="cboChartOfAccountIdPpnIn_ItemDataBound" OnItemsRequested="cboChartOfAccountIdPpnIn_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr id="trSlPpnIn" runat="server">
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Subledger Ppn In"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerIdPpnIn" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerIdPpnIn_ItemDataBound"
                                OnItemsRequested="cboSubledgerIdPpnIn_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPatientTransaction" runat="server" Text="Patient Transaction" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsNeedConfirmationOfAttendance" runat="server" Text="Need Confirmation Of Attendance" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkShoOnKiosk" runat="server" Text="Show On KIOSK" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsDirectPayment" runat="server" Text="Direct Payment" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsTransactionEntry" runat="server" Text="Transaction Entry" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsDispensaryUnit" runat="server" Text="Dispensary Unit" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsPurchasingUnit" runat="server" Text="Purchasing Unit" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabHeader" runat="server" MultiPageID="mpgHeader" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Common" PageViewID="pgvCommon" Selected="true" />
            <telerik:RadTab runat="server" Text="EMR" PageViewID="pgvEmr" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgHeader" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvCommon" runat="server">
            <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Visit Type" PageViewID="pgvSeviceUnitVisitType"
                        Selected="true" />
                    <telerik:RadTab runat="server" Text="Physician" PageViewID="pgvServiceUnitParamedic" />
                    <telerik:RadTab runat="server" Text="Item Service & Package" PageViewID="pgvServiceUnitItemService" />
                    <telerik:RadTab runat="server" Text="Auto Bill Item" PageViewID="pgvAutoBillItem" />
                    <telerik:RadTab runat="server" Text="Transaction Access" PageViewID="pgvTransactionCode" />
                    <telerik:RadTab runat="server" Text="Bridging & Integration" PageViewID="pgvAliasName" />
                    <telerik:RadTab runat="server" Text="Inventory Location" PageViewID="pgvLocation" />
                    <telerik:RadTab runat="server" Text="Global Schedule" PageViewID="pgvSchedule" Visible="false" />
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvSeviceUnitVisitType" runat="server">
                    <telerik:RadGrid ID="grdServiceUnitVisitType" runat="server" OnNeedDataSource="grdServiceUnitVisitType_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdServiceUnitVisitType_UpdateCommand"
                        OnDeleteCommand="grdServiceUnitVisitType_DeleteCommand" OnInsertCommand="grdServiceUnitVisitType_InsertCommand"
                        AllowPaging="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, VisitTypeID"
                            PageSize="15">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VisitTypeID" HeaderText="Visit Type ID"
                                    UniqueName="VisitTypeID" SortExpression="VisitTypeID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="VisitTypeName" HeaderText="Visit Type Name" UniqueName="VisitTypeName"
                                    SortExpression="VisitTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VisitDuration" HeaderText="Visit Duration"
                                    UniqueName="VisitDuration" SortExpression="VisitDuration" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridTemplateColumn />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="ServiceUnitVisitTypeDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="ServiceUnitVisitTypeEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvServiceUnitParamedic">
                    <telerik:RadGrid ID="grdServiceUnitParamedic" runat="server" OnNeedDataSource="grdServiceUnitParamedic_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdServiceUnitParamedic_UpdateCommand"
                        OnDeleteCommand="grdServiceUnitParamedic_DeleteCommand" OnInsertCommand="grdServiceUnitParamedic_InsertCommand"
                        AllowPaging="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, ParamedicID"
                            PageSize="15">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ParamedicID" HeaderText="Physician ID"
                                    UniqueName="ParamedicID" SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician Name" UniqueName="ParamedicName"
                                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Default Service Rooms"
                                    UniqueName="RoomName" SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsUsingQue" HeaderText="Using Que Slot"
                                    UniqueName="IsUsingQue" SortExpression="IsUsingQue" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsAcceptBPJS" HeaderText="Accept BPJS"
                                    UniqueName="IsAcceptBPJS" SortExpression="IsAcceptBPJS" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsAcceptNonBPJS" HeaderText="Accept Non BPJS"
                                    UniqueName="IsAcceptNonBPJS" SortExpression="IsAcceptNonBPJS" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="ScheduleTemplate">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openScheduleGlobal('{0}'); return false;\"><img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Schedule Template\" /></a>",
                                                                                                                                                DataBinder.Eval(Container.DataItem, "ParamedicID"))%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="ServiceUnitParamedicDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="ServiceUnitParamedicEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvServiceUnitItemService">
                    <telerik:RadTabStrip ID="tabServiceUnitItemService" runat="server" MultiPageID="mpgServiceUnitItemService" SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Detail Item Service & Package" PageViewID="pgvItemServiveDetail" Selected="true" />
                            <telerik:RadTab runat="server" Text="Mapping per Item Group" PageViewID="pgvItemServiveGroup" />
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="mpgServiceUnitItemService" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderColor="gray">
                        <telerik:RadPageView ID="pgvItemServiveDetail" runat="server">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%; vertical-align: top;">
                                        <table width="100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblFilterItemService" runat="server" Text="Item ID / Item Name"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtFilterItemService" runat="server" Width="300px" MaxLength="100" />
                                                </td>
                                                <td width="20">
                                                    <asp:ImageButton ID="btnFilterItemService" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <telerik:RadGrid ID="grdServiceUnitItemService" runat="server" OnNeedDataSource="grdServiceUnitItemService_NeedDataSource"
                                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdServiceUnitItemService_UpdateCommand"
                                OnDeleteCommand="grdServiceUnitItemService_DeleteCommand" OnInsertCommand="grdServiceUnitItemService_InsertCommand"
                                AllowPaging="true" OnDataBound="grdServiceUnitItemService_DataBound">
                                <HeaderContextMenu>
                                </HeaderContextMenu>
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, ItemID" PageSize="15"
                                    PagerStyle-Mode="NextPrevNumericAndAdvanced">
                                    <CommandItemTemplate>
                                        &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdServiceUnitItemService.MasterTableView.IsItemInserted %>'>
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                                </asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lbPickListPA" runat="server" Visible="False" OnClientClick="javascript:openWinPickListPA();return false;">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                                    &nbsp;<asp:Label runat="server" ID="Label1" Text="Account Mapping (Item Product)"></asp:Label>
                                </asp:LinkButton>
                                    </CommandItemTemplate>
                                    <CommandItemStyle Height="29px" />
                                    <Columns>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                            <HeaderStyle Width="35px" />
                                            <ItemStyle CssClass="MyImageButton" />
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IdiCode" HeaderText="IDI Code"
                                            UniqueName="IdiCode" SortExpression="IdiCode" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                        <telerik:GridBoundColumn DataField="IdiName" HeaderText="IDI Name" UniqueName="IdiName"
                                            SortExpression="IdiName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowEditByUserVerificated"
                                            HeaderText="Allow Edit" UniqueName="IsAllowEditByUserVerificated" SortExpression="IsAutoPayment"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVisible" HeaderText="Visible"
                                            UniqueName="IsVisible" SortExpression="IsVisible" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ChartOfAccountCode"
                                            HeaderText="COA Revenue" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                        <telerik:GridBoundColumn DataField="SubLedgerName" HeaderText="Subledger Revenue"
                                            UniqueName="SubLedgerName" SortExpression="SubLedgerName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                        <telerik:GridTemplateColumn />
                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                                            <HeaderStyle Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridTemplateColumn UniqueName="process">
                                            <ItemTemplate>
                                                <%# string.Format("<a href=\"#\" onclick=\"openItemClass('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"Open Item Class\" /></a>",
                                                DataBinder.Eval(Container.DataItem, "ServiceUnitID"), DataBinder.Eval(Container.DataItem, "ItemID"))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="processItem">
                                            <ItemTemplate>
                                                <%# string.Format("<a href=\"#\" onclick=\"openItemComp('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/new16_d.png\" border=\"0\" alt=\"Open Item Component\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "ServiceUnitID"), DataBinder.Eval(Container.DataItem, "ItemID"))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings UserControlName="ServiceUnitItemServiceDetail.ascx" EditFormType="WebUserControl">
                                        <EditColumn UniqueName="ServiceUnitItemServiceEditCommand">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                                <FilterMenu>
                                </FilterMenu>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pgvItemServiveGroup" runat="server">
                            <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
                                BorderColor="#FFC080" BorderStyle="Solid">
                                <table width="100%">
                                    <tr>
                                        <td width="10px" valign="top">
                                            <asp:Image ID="Image7" ImageUrl="~/Images/boundleft.gif" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="50%" style="vertical-align: top">
                                        <table width="100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblMappingItemType" runat="server" Text="Item Type"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboMappingItemType" runat="server" Width="300px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="cboMappingItemType_SelectedIndexChanged" />
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblMappingItemGroup" runat="server" Text="Item Group"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboMappingItemGroup" Width="300px" runat="server" EnableLoadOnDemand="true"
                                                        HighlightTemplatedItems="true" OnItemDataBound="cboMappingItemGroup_ItemDataBound"
                                                        OnItemsRequested="cboMappingItemGroup_ItemsRequested">
                                                        <ItemTemplate>
                                                            <b>
                                                                <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                                                &nbsp;-&nbsp;
                                                                <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                                            </b>
                                                            <br />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Note : Show max 20 items
                                                        </FooterTemplate>
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label"></td>
                                                <td class="entry">
                                                    <asp:Button ID="btnMappingProcess" Text="Mapping Process" runat="server" OnClick="btnMappingProcess_Click"></asp:Button>
                                                </td>
                                                <td width="20"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="50%" style="vertical-align: top"></td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvAutoBillItem">
                    <telerik:RadGrid ID="grdServiceUnitAutoBillItem" runat="server" OnNeedDataSource="grdServiceUnitAutoBillItem_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdServiceUnitAutoBillItem_UpdateCommand"
                        OnDeleteCommand="grdServiceUnitAutoBillItem_DeleteCommand" OnInsertCommand="grdServiceUnitAutoBillItem_InsertCommand"
                        AllowPaging="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, ItemID" PageSize="15">
                            <ColumnGroups>
                                <telerik:GridColumnGroup HeaderText="Generate On" Name="Generate" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Quantity" HeaderText="Qty"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemUnit" HeaderText="Unit"
                                    UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAutoPayment" HeaderText="Auto Payment"
                                    UniqueName="IsAutoPayment" SortExpression="IsAutoPayment" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsGenerateOnRegistration"
                                    HeaderText="Old Patient Reg." UniqueName="IsGenerateOnRegistration"
                                    SortExpression="IsGenerateOnRegistration" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsGenerateOnNewRegistration"
                                    HeaderText="New Patient Reg." UniqueName="IsGenerateOnNewRegistration"
                                    SortExpression="IsGenerateOnNewRegistration" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsGenerateOnReferral"
                                    HeaderText="Referral" UniqueName="IsGenerateOnReferral" SortExpression="IsGenerateOnReferral"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsGenerateOnFirstRegistration"
                                    HeaderText="1# Registration" UniqueName="IsGenerateOnFirstRegistration"
                                    SortExpression="IsGenerateOnFirstRegistration" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="90px" DataField="IsGenerateOnSchedule"
                                    HeaderText="Schedule" UniqueName="IsGenerateOnSchedule"
                                    SortExpression="IsGenerateOnSchedule" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Generate" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="ServiceUnitAutoBillItemDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="ServiceUnitAutoBillItemEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvTransactionCode">
                    <telerik:RadGrid ID="grdServiceUnitTransactionCode" runat="server" AutoGenerateColumns="False"
                        GridLines="None" OnNeedDataSource="grdServiceUnitTransactionCode_NeedDataSource">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="SRTransactionCode">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRTransactionCode" HeaderText="ID"
                                    UniqueName="SRTransactionCode" SortExpression="SRTransactionCode" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="TransactionName" HeaderText="Transaction"
                                    UniqueName="TransactionName" SortExpression="TransactionName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Item Product Medic">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsItemProductMedic" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsItemProductMedic") %>' Visible='<%#DataBinder.Eval(Container.DataItem, "IsVisible") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Item Product Non Medic">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsItemProductNonMedic" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsItemProductNonMedic") %>'
                                            Visible='<%#DataBinder.Eval(Container.DataItem, "IsVisible") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="150px" HeaderText="Item Kitchen">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsItemKitchen" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsItemKitchen") %>' Visible='<%#DataBinder.Eval(Container.DataItem, "IsVisible") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvAliasName">
                    <telerik:RadGrid ID="grdAliasName" runat="server" OnNeedDataSource="grdAliasName_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAliasName_UpdateCommand"
                        OnDeleteCommand="grdAliasName_DeleteCommand" OnInsertCommand="grdAliasName_InsertCommand"
                        AllowPaging="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, SRBridgingType, BridgingID"
                            PageSize="15">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="BridgingTypeName" HeaderText="Bridging Type"
                                    UniqueName="BridgingTypeName" SortExpression="BridgingTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BridgingID" HeaderText="Bridging ID"
                                    UniqueName="BridgingID" SortExpression="BridgingID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="BridgingName" HeaderText="Bridging Name" UniqueName="BridgingName"
                                    SortExpression="BridgingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="ServiceUnitAliasDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="ServiceUnitAliasEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvLocation">
                    <telerik:RadGrid ID="grdLocation" runat="server" OnNeedDataSource="grdLocation_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdLocation_UpdateCommand"
                        OnDeleteCommand="grdLocation_DeleteCommand" OnInsertCommand="grdLocation_InsertCommand"
                        AllowPaging="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, LocationID"
                            PageSize="15">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LocationID" HeaderText="Location ID"
                                    UniqueName="LocationID" SortExpression="LocationID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="LocationName" HeaderText="Location Name" UniqueName="LocationName"
                                    SortExpression="LocationName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsLocationMain"
                                    HeaderText="Main Location" UniqueName="IsLocationMain" SortExpression="IsLocationMain"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                                <telerik:GridTemplateColumn UniqueName="processItem">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"openWinPickListPA2('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"Account Expense Mapping (Item Product)\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "LocationID"))%>
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings UserControlName="ServiceUnitLocationDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="ServiceUnitLocationEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvSchedule">
                    <telerik:RadGrid ID="grdSchedule" runat="server" OnNeedDataSource="grdSchedule_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSchedule_UpdateCommand"
                        OnDeleteCommand="grdSchedule_DeleteCommand" OnInsertCommand="grdSchedule_InsertCommand"
                        AllowPaging="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, DayOfWeek"
                            PageSize="15">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="DayOfWeekName" HeaderText="Day Of Week" UniqueName="DayOfWeekName"
                                    SortExpression="DayOfWeekName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="StartTime" HeaderText="Start Time"
                                    UniqueName="StartTime" SortExpression="StartTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EndTime" HeaderText="End Time"
                                    UniqueName="EndTime" SortExpression="EndTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="ServiceUnitScheduleDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="ServiceUnitScheduleEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvEmr" runat="server">
            <table width="100%">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRAssessmentType" runat="server" Text="Assessment Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRAssessmentType" runat="server" Width="300px" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsAllowAccessPatientWithServiceUnitParamedic" runat="server" Text="Allow Access Patient With Mapping Service Unit - Physician" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top"></td>
                </tr>
            </table>

            <telerik:RadTabStrip ID="tabDetail2" runat="server" MultiPageID="mpgDetail2" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Body Diagram" PageViewID="pgvBodyDiagram"
                        Selected="true" />
                    <telerik:RadTab runat="server" Text="Image Template" PageViewID="pgvImageTemplate" />
                    <telerik:RadTab runat="server" Text="Vital Sign" PageViewID="pgvVitalSign" />
                    <telerik:RadTab runat="server" Text="Other Assessment Type" PageViewID="pgvAssessmentType" />
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail2" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView runat="server" ID="pgvBodyDiagram">
                    <telerik:RadGrid ID="grdBodyDiagram" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnNeedDataSource="grdBodyDiagram_NeedDataSource">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="BodyID">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BodyID" HeaderText="ID"
                                    UniqueName="BodyID" SortExpression="BodyID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="BodyName" HeaderText="Name"
                                    UniqueName="BodyName" SortExpression="BodyName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Description" HeaderText="Description"
                                    UniqueName="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBinaryImageColumn DataField="BodyImage" HeaderText="Body Diagram" UniqueName="BodyImage"
                                    ImageHeight="100px" ImageWidth="100px" ResizeMode="Fit" />
                                <telerik:GridTemplateColumn>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvImageTemplate">
                    <telerik:RadGrid ID="grdImageTemplate" runat="server" AutoGenerateColumns="False"
                        GridLines="None" OnNeedDataSource="grdImageTemplate_NeedDataSource">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ImageTemplateID">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ImageTemplateID" HeaderText="ID"
                                    UniqueName="ImageTemplateID" SortExpression="ImageTemplateID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ImageTemplateName"
                                    HeaderText="Name" UniqueName="TransactionName" SortExpression="TransactionName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ImageTemplateType"
                                    HeaderText="Type" UniqueName="ImageTemplateType" SortExpression="ImageTemplateType"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Description" HeaderText="Description"
                                    UniqueName="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBinaryImageColumn DataField="Image" HeaderText="Image Template" UniqueName="Image"
                                    ImageHeight="100px" ImageWidth="100px" ResizeMode="Fit" />
                                <telerik:GridTemplateColumn>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvVitalSign">
                    <telerik:RadGrid ID="grdVitalSign" runat="server" OnNeedDataSource="grdVitalSign_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdVitalSign_UpdateCommand"
                        OnDeleteCommand="grdVitalSign_DeleteCommand" OnInsertCommand="grdVitalSign_InsertCommand"
                        AllowPaging="true">
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID, VitalSignID"
                            PageSize="15">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VitalSignID" HeaderText="Vital Sign ID"
                                    UniqueName="VitalSignID" SortExpression="VitalSignID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="VitalSignName" HeaderText="Vital Sign Name" UniqueName="VitalSignName"
                                    SortExpression="VitalSignName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="ServiceUnitVitalSignDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="ServiceUnitVitalSignEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pgvAssessmentType">
                    <telerik:RadGrid ID="grdServiceUnitAssessmentType" runat="server" AutoGenerateColumns="False"
                        GridLines="None" OnNeedDataSource="grdServiceUnitAssessmentType_NeedDataSource">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemName" HeaderText="Assessment Name"
                                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
