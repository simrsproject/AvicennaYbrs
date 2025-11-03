<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="AssetItemDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.AssetItemDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="winPOR" Animation="None" Width="900px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false" Modal="true"
        OnClientClose="onClientClose" />
    <telerik:RadWindow ID="winCopy" Animation="None" Width="1000px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false" Modal="true"
        OnClientClose="onClientCloseCopy" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function openWinPorAssetList() {
                var oWnd = $find("<%= winPOR.ClientID %>");
                oWnd.setUrl('PorAssetList.aspx');
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                var txtRefNo = $find("<%= txtPONumber.ClientID %>");

                if (oWnd.argument)
                    txtRefNo.set_value(oWnd.argument.tno);
            }
            function openWinAssetIdCopyList() {
                var oWnd = $find("<%= winCopy.ClientID %>");
                oWnd.setUrl('CopyAssetList.aspx');
                oWnd.show();
            }
            function onClientCloseCopy(oWnd, args) {
                var txtRefNo = $find("<%= txtAssetIdCopy.ClientID %>");

                if (oWnd.argument)
                    txtRefNo.set_value(oWnd.argument.aid);
            }
        </script>

    </telerik:RadCodeBlock>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Asset ID
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssetId" runat="server" Width="300px" MaxLength="30" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Asset Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssetName" runat="server" Width="300px" MaxLength="250"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAssetName" runat="server" ErrorMessage="Asset Name required."
                                ValidationGroup="entry" ControlToValidate="txtAssetName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Item
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" AutoPostBack="False"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                    </b>
                                    <br />
                                    Item ID :
                                    <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <%--<asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item ID required."
                                ValidationGroup="entry" ControlToValidate="cboItemID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Service Unit
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="300px" HighlightTemplatedItems="True"
                                AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboServiceUnit_ItemDataBound"
                                OnItemsRequested="cboServiceUnit_ItemsRequested" OnSelectedIndexChanged="cboServiceUnit_SelectedIndexChanged">
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvServiceUnit" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Room
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboLocation" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Asset Group
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboAssetGroup" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboAssetGroup_ItemDataBound"
                                OnItemsRequested="cboAssetGroup_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboAssetGroup_SelectedIndexChanged">
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAssetGroup" runat="server" ErrorMessage="Asset Group required."
                                ValidationGroup="entry" ControlToValidate="cboAssetGroup" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Asset Sub Group
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboAssetSubGroupId" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Asset Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboAssetType" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAssetType" runat="server" ErrorMessage="Asset Type required."
                                ValidationGroup="entry" ControlToValidate="cboAssetType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Asset Status
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAssetsStatus" Width="300px" AllowCustomText="true"
                                Filter="Contains" Enabled="False">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAssetStatus" runat="server" ErrorMessage="Asset Status required."
                                ValidationGroup="entry" ControlToValidate="cboSRAssetsStatus" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Criticality
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAssetsCriticality" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCriticality" runat="server" ErrorMessage="Criticality required."
                                ValidationGroup="entry" ControlToValidate="cboSRAssetsCriticality" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Notes To Technician
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotesToTechnician" runat="server" Width="300px" MaxLength="4000"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Asset ID Copy
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAssetIdCopy" runat="server" Width="300px" ShowButton="true"
                                AutoPostBack="true" OnTextChanged="txtAssetIdCopy_TextChanged" ClientEvents-OnButtonClick="openWinAssetIdCopyList" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Insurance
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboInsurance" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Insurance Polis No
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInsurancePolisNo" runat="server" Width="300px" MaxLength="30" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 16px">Insurance Amount
                        </td>
                        <td class="entry" style="width: 361px">
                            <telerik:RadNumericTextBox ID="txtInsuranceAmount" runat="server" Value="0" Width="150px">
                                <EnabledStyle HorizontalAlign="Right" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Warranty / Contract
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRAssetsWarrantyContract" Width="300px"
                                AllowCustomText="true" Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Warranty Expiry Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtGuaranteeExpiredDate" runat="server" Width="105px">
                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar ID="Calendar1" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;">
                            <%--<asp:RequiredFieldValidator ID="rfvGuaranteeExpiredDate" runat="server" ErrorMessage="Warranty Expiry Date required."
                                ValidationGroup="entry" ControlToValidate="txtGuaranteeExpiredDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Warranty / Contract Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtWarrantyContractNotes" runat="server" Width="300px" MaxLength="250"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Asset Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="250"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">Manufacture
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboManufacture" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Model Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBrandName" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Serial Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSerialNumber" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSerialNumber" runat="server" ErrorMessage="Serial Number required."
                                ValidationGroup="entry" ControlToValidate="txtSerialNumber" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Maintenance Service Unit
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboMaintenanceServiceUnitID" runat="server" Width="300px"
                                HighlightTemplatedItems="True" AutoPostBack="True" MarkFirstMatch="false" EnableLoadOnDemand="true"
                                OnItemDataBound="cboMaintenanceServiceUnitID_ItemDataBound" OnItemsRequested="cboMaintenanceServiceUnitID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMaintenanceServiceUnitID" runat="server" ErrorMessage="Maintenance Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboMaintenanceServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Maintenance Interval
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtMaintenanceInterval" runat="server" Width="70px" />
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbtMaintenanceIntervalIn" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow">
                                            <asp:ListItem Value="d" Text="Day" />
                                            <asp:ListItem Value="m" Text="Month" />
                                            <asp:ListItem Value="y" Text="Year" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsContinuousMaintenanceSchedule" runat="server" Text="Continuous Schedule"
                                            Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Maintenance Start Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtLastMaintenanceDate" runat="server" Width="105px">
                                <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar ID="Calendar2" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;">
                            <%--<asp:RequiredFieldValidator ID="rfvLastMaintenanceDate" runat="server" ErrorMessage="Last Maintenance Date required."
                                ValidationGroup="entry" ControlToValidate="txtLastMaintenanceDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>--%>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">Next Maintenance Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtNextMaintenanceDate" runat="server" Width="105px">
                                <DateInput ID="DateInput3" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                                <Calendar ID="Calendar3" runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                    ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">PO Received Number
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPONumber" runat="server" Width="300px" MaxLength="30"
                                ShowButton="true" AutoPostBack="true" OnTextChanged="txtPONumber_TextChanged"
                                ClientEvents-OnButtonClick="openWinPorAssetList" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Supplier
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSupplierID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                MarkFirstMatch="false" EnableLoadOnDemand="true" OnItemDataBound="cboSupplierID_ItemDataBound"
                                OnItemsRequested="cboSupplierID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 result
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 16px">Purchase Price
                        </td>
                        <td class="entry" style="width: 361px">
                            <telerik:RadNumericTextBox ID="txtPurchasePrice" runat="server" Value="0" Width="150px" AutoPostBack="true" OnTextChanged="txtPurchasePrice_TextChanged">
                                <EnabledStyle HorizontalAlign="Right" />
                            </telerik:RadNumericTextBox>
                            <asp:CheckBox ID="chkIsFixedAsset" runat="server" Text="Fixed Asset (With Depreciation)"/>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Purchase Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtPurchaseDate" runat="server" Width="105px">
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvPurchaseDate" runat="server" ErrorMessage="Purchase Date required."
                                ValidationGroup="entry" ControlToValidate="txtPurchaseDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date Of Use
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtStartUsingDate" runat="server" Width="105px">
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date Disposed
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtDateDisposed" runat="server" Width="105px" Enabled="False">
                            </telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Asset" PageViewID="pgvAsset" Selected="true" />
            <telerik:RadTab runat="server" Text="Asset Depreciation" PageViewID="pgvAssetDepreciation" />
            <telerik:RadTab runat="server" Text="Movement History" PageViewID="pgvMovementHistory" />
            <telerik:RadTab runat="server" Text="Maintenance History" PageViewID="pgvMaintenanceHistory" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvAsset" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td class="label">Date Acquired
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtStartDepreciationDate" runat="server" Width="105px">
                                    </telerik:RadDatePicker>
                                </td>
                                <td style="width: 20px;"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Usage Time Estimation
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRAssetUsageTimeEstimation" Width="300px" AllowCustomText="true"
                                        Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRAssetUsageTimeEstimation_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px">
                                    <asp:RequiredFieldValidator ID="rfvSRAssetUsageTimeEstimation" runat="server" ErrorMessage="Usage Time Estimation required."
                                        ValidationGroup="entry" ControlToValidate="cboSRAssetUsageTimeEstimation" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">Usage Time Estimation (Value)
                                </td>
                                <td class="entry">
                                    <table>
                                        <tr>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtUsageTimeEstimation" runat="server" Width="70px" ReadOnly="true" />
                                            </td>
                                            <td>
                                                &nbsp;Month(s)
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 16px">Residual Value
                                </td>
                                <td class="entry" style="width: 361px">
                                    <telerik:RadNumericTextBox ID="txtResidualValue" runat="server" Value="0" Width="150px">
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 16px">Current Price
                                </td>
                                <td class="entry" style="width: 361px">
                                    <telerik:RadNumericTextBox ID="txtCurrentPrice" runat="server" Value="0" Width="150px">
                                        <EnabledStyle HorizontalAlign="Right" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td width="20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAssetDepreciation" runat="server">
            <telerik:RadGrid ID="grdAssetDepreciation" runat="server" Width="100%" AllowPaging="true"
                AllowCustomPaging="true" PageSize="50">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="PeriodeNo" ShowGroupFooter="True">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnProcessAssetDepreciation" runat="server" CommandName="InitInsert"
                            Visible='<%# !grdAssetDepreciation.MasterTableView.IsItemInserted %>'>
                            <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                            &nbsp;<asp:Label runat="server" ID="lblProcessAssetDepreciation" Text="Process"></asp:Label>
                        </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="PeriodeNo" HeaderText="No"
                            UniqueName="PeriodeNo" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Period" HeaderText="Period"
                            UniqueName="Period" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" ItemStyle-Wrap="false" DataField="CurrentAmount"
                            AllowSorting="false" HeaderText="Current Price" UniqueName="CurrentAmount" DataFormatString="{0:N2}"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" ItemStyle-Wrap="false" DataField="DepreciationAmount"
                            AllowSorting="false" HeaderText="Depreciation" UniqueName="DepreciationAmount"
                            DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" ItemStyle-Wrap="false" DataField="AssetValue"
                            AllowSorting="false" HeaderText="Residual Value" UniqueName="AssetValue" DataFormatString="{0:N2}"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" ItemStyle-Wrap="false" DataField="AccumulationAmount"
                            AllowSorting="false" HeaderText="Accumulation" UniqueName="AccumulationAmount"
                            DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPosted" HeaderText="Posted"
                            UniqueName="IsPosted" AllowSorting="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="JournalNumber" HeaderText="Journal No" UniqueName="JournalNo" />
                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="User Update"
                            UniqueName="LastUpdateByUserID" />
                    </Columns>
                    <EditFormSettings UserControlName="AssetDepreciationEditor.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemAssetDepreciationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvMovementHistory" runat="server">
            <telerik:RadGrid ID="grdMovementHistory" runat="server" Width="100%" AllowPaging="true"
                AllowCustomPaging="true" PageSize="50">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="AssetMovementNo" ShowGroupFooter="True">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="AssetMovementNo" HeaderText="Transaction No"
                            UniqueName="AssetMovementNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="MovementDate" HeaderText="Date"
                            UniqueName="MovementDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="FromServiceUnitName" HeaderText="From ServiceUnit"
                            UniqueName="FromServiceUnitName" />
                        <telerik:GridBoundColumn DataField="FromLocationName" HeaderText="From Location"
                            UniqueName="FromLocationName" />
                        <telerik:GridBoundColumn DataField="ToServiceUnitName" HeaderText="To ServiceUnit"
                            UniqueName="ToServiceUnitName" />
                        <telerik:GridBoundColumn DataField="ToLocationName" HeaderText="To Location" UniqueName="ToLocationName" />
                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="User Update"
                            UniqueName="LastUpdateByUserID" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvMaintenanceHistory" runat="server">
            <telerik:RadGrid ID="grdMaintenanceHistory" runat="server" Width="100%" AllowPaging="true"
                AllowCustomPaging="true" PageSize="50">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="AssetMaintenanceNo" ShowGroupFooter="True">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AssetMaintenanceNo"
                            HeaderText="Transaction No" UniqueName="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="MaintenanceDate"
                            HeaderText="Date" UniqueName="MaintenanceDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="MaintenanceType" HeaderText="Maintenance Type"
                            UniqueName="MaintenanceType" />
                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Notes" />
                        <telerik:GridBoundColumn DataField="Condition" HeaderText="Condition" UniqueName="Condition" />
                        <telerik:GridBoundColumn DataField="MaintenanceBy" HeaderText="Maintenance By" UniqueName="MaintenanceBy" />
                        <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="User Update"
                            UniqueName="LastUpdateByUserID" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <br />

    <script type="text/javascript">
        function LoadLocation(sender, eventArgs) {
            var locationCombo = $find("<%= cboLocation.ClientID %>");
            locationCombo.clearItems();
            locationCombo.set_text(" ");

            var item = eventArgs.get_item();
            if (item != null) {
                locationCombo.requestItems(item.get_value(), true);
            }
        }
    </script>

</asp:Content>
