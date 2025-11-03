<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ItemKitchenDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ItemKitchenDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function viewEdDetail(locId, t) {
            var oWnd = $find("<%= winEdItem.ClientID %>");
            var oitemId = $find("<%= txtItemID.ClientID %>");
            oWnd.setUrl('../../Stock/BalanceDetailExpiredDate/BalanceDetailExpiredDateList.aspx?iid=' + oitemId.get_value() + '&lid=' + locId + '&t=' + t);

            oWnd.set_width(document.body.offsetWidth);
            oWnd.show();
        }
    </script>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Modal="true" VisibleStatusbar="false"
        DestroyOnClose="false" Behavior="Close, Move" ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="500px" Behavior="Move, Close"
                ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winEdItem">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemIDExternal" runat="server" Text="Item ID External"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemIDExternal" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="Item Name required."
                                ValidationGroup="entry" ControlToValidate="txtItemName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboItemGroupID_ItemDataBound"
                                OnItemsRequested="cboItemGroupID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvItemGroupID" runat="server" ErrorMessage="Item Group required."
                                ValidationGroup="entry" ControlToValidate="cboItemGroupID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBarcode" runat="server" Text="Barcode"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBarcode" runat="server" Width="300px" MaxLength="4000"
                                Font-Names="C39T" Font-Size="36" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblABCClass" runat="server" Text="ABC Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtABCClass" runat="server" Width="100px" MaxLength="1" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvABCClass" runat="server" ErrorMessage="ABC Class required."
                                ValidationGroup="entry" ControlToValidate="txtABCClass" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBrandName" runat="server" Text="Brand Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtBrandName" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemUnit" runat="server" Text="Item Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboStandardReferenceItem_ItemDataBound"
                                OnItemsRequested="cboSRItemUnit_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Item Unit required."
                                ValidationGroup="entry" ControlToValidate="cboSRItemUnit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPurchaseUnit" runat="server" Text="Purchase Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPurchaseUnit" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboStandardReferenceItem_ItemDataBound"
                                OnItemsRequested="cboSRPurchaseUnit_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRPurchaseUnit" runat="server" ErrorMessage="Purchase Unit required."
                                ValidationGroup="entry" ControlToValidate="cboSRPurchaseUnit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblConversionFactor" runat="server" Text="Conversion Factor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtConversionFactor" runat="server" Width="100px"
                                MaxLength="6" MinValue="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvConversionFactor" runat="server" ErrorMessage="Conversion Factor required."
                                ValidationGroup="entry" ControlToValidate="txtConversionFactor" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSalesFixedPrice" runat="server" Text="Sales Fixed Price"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSalesFixedPrice" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSalesFixedPrice" runat="server" ErrorMessage="Sales Fixed Price required."
                                ValidationGroup="entry" ControlToValidate="txtSalesFixedPrice" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMarginPercentage" runat="server" Text="Margin"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtMarginPercentage" runat="server" Type="Percent"
                                            Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cboMarginID" runat="server" Width="197px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboMarginID_ItemDataBound"
                                            OnItemsRequested="cboMarginID_ItemsRequested">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MarginID")%>
                                                &nbsp;-&nbsp;
                                                <%# DataBinder.Eval(Container.DataItem, "MarginName")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Note : Show max 10 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMarginPercentage" runat="server" ErrorMessage="Margin Percentage required."
                                ValidationGroup="entry" ControlToValidate="txtMarginPercentage" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPriceInPurchaseUnit" runat="server" Text="Price In Purchase Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPriceInPurchaseUnit" runat="server" Width="100px"
                                MaxLength="16" MaxValue="9999999999999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPriceInPurchaseUnit" runat="server" ErrorMessage="Price In Purchase Unit required."
                                ValidationGroup="entry" ControlToValidate="txtPriceInPurchaseUnit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPriceInBaseUnit" runat="server" Text="Price In Base Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPriceInBaseUnit" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPriceInBasedUnitWVat" runat="server" Text="Price In Based Unit WVat"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPriceInBasedUnitWVat" runat="server" Width="100px"
                                MaxLength="16" MaxValue="9999999999999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblHighestPriceInBasedUnit" runat="server" Text="Highest Price In Based Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtHighestPriceInBasedUnit" runat="server" Width="100px"
                                MaxLength="16" MaxValue="9999999999999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCostPrice" runat="server" Text="Cost Price"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCostPrice" runat="server" Width="100px" MaxLength="16"
                                MaxValue="9999999999999.99" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPurchaseDiscount1" runat="server" Text="Purchase Discount #1"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPurchaseDiscount1" Type="Percent" runat="server"
                                Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPurchaseDiscount2" runat="server" Text="Purchase Discount #2"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPurchaseDiscount2" Type="Percent" runat="server"
                                Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="True" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlNotUsedYet" runat="server" Visible="false">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSafetyStock" runat="server" Text="Safety Stock"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtSafetyStock" runat="server" Width="100px" MaxLength="10"
                                    MaxValue="9999999.99" MinValue="0" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvSafetyStock" runat="server" ErrorMessage="Safety Stock required."
                                    ValidationGroup="entry" ControlToValidate="txtSafetyStock" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image24" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSafetyTime" runat="server" Text="Safety Time"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtSafetyTime" runat="server" Width="100px" MaxLength="2"
                                    MaxValue="99" MinValue="0">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvSafetyTime" runat="server" ErrorMessage="Safety Time required."
                                    ValidationGroup="entry" ControlToValidate="txtSafetyTime" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLeadTime" runat="server" Text="Lead Time"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtLeadTime" runat="server" Width="100px" MaxLength="2"
                                    MaxValue="99" MinValue="0">
                                    <NumberFormat DecimalDigits="0" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvLeadTime" runat="server" ErrorMessage="Lead Time required."
                                    ValidationGroup="entry" ControlToValidate="txtLeadTime" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image26" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTolerancePercentage" runat="server" Text="Tolerance Percentage"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtTolerancePercentage" runat="server" Type="Percent"
                                    Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" />
                            </td>
                            <td width="20px">
                                <asp:RequiredFieldValidator ID="rfvTolerancePercentage" runat="server" ErrorMessage="Tolerance Percentage required."
                                    ValidationGroup="entry" ControlToValidate="txtTolerancePercentage" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image27" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 155px;">
                                        <asp:CheckBox ID="chkIsInventoryItem" runat="server" Text="Inventory Item" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsItemProduction" runat="server" Text="Production Item" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 155px;">
                                        <asp:CheckBox ID="chkIsControlExpired" runat="server" Text="Control Expired" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIsSalesAvailable" runat="server" Text="Sales Available" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 155px;">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemCategory" runat="server" Text="Item Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemCategory" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBillingGroup" runat="server" Text="Billing Statement Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboBillingGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblProductAccount" runat="server" Text="Product Account"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboProductAccount" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboProductAccount_ItemDataBound"
                                OnItemsRequested="cboProductAccount_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ProductAccountID")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "ProductAccountName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvProductAccount" runat="server" ErrorMessage="Product Account required."
                                ValidationGroup="entry" ControlToValidate="cboProductAccount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trBpjsItemGroup">
                        <td class="label">
                            <asp:Label ID="lblSRBpjsItemGroup" runat="server" Text="BPJS Item Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRBpjsItemGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            E-Klaim Item Group
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREklaimGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Price History" PageViewID="pgvPriceHistory"
                Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Related Supplier" PageViewID="pgvRelatedSupplier">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Location" PageViewID="pgvLocation">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvPriceHistory" runat="server">
            <telerik:RadGrid ID="grdPriceHistory" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnNeedDataSource="grdList_NeedDataSource">
                <MasterTableView DataKeyNames="SRTariffType, ItemID, ClassID, StartingDate">
                    <Columns>
                        <telerik:GridBoundColumn DataField="TariffTypeName" HeaderText="Tariff Type" UniqueName="TariffTypeName"
                            SortExpression="TariffTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class" UniqueName="ClassName"
                            SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartingDate" HeaderText="Starting Date"
                            UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <%--<telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAdminCalculation"
                            HeaderText="Admin Calculation" UniqueName="IsAdminCalculation" SortExpression="IsAdminCalculation"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowDiscount"
                            HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowVariable"
                            HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowCito" HeaderText="Cito"
                            UniqueName="IsAllowCito" SortExpression="IsAllowCito" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CitoValue" HeaderText="Cito Value"
                            UniqueName="CitoValue" SortExpression="CitoValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />--%>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReferenceNo" HeaderText="Reference No"
                            UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="UpdateBy" HeaderText="Update By"
                            UniqueName="UpdateBy" SortExpression="UpdateBy" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRelatedSupplier" runat="server">
            <telerik:RadGrid ID="grdSupplierItem" AllowPaging="true" PageSize="20" runat="server"
                OnNeedDataSource="grdSupplierItem_NeedDataSource" AutoGenerateColumns="False"
                GridLines="None" OnUpdateCommand="grdSupplierItem_UpdateCommand" OnDeleteCommand="grdSupplierItem_DeleteCommand"
                OnInsertCommand="grdSupplierItem_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SupplierID, ItemID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="35px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SupplierID" HeaderText="Supplier ID"
                            UniqueName="SupplierID" SortExpression="SupplierID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier Name" UniqueName="SupplierName"
                            SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                            UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                            HeaderText="Conversion Factor" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PriceInPurchaseUnit"
                            HeaderText="Price In Purchase Unit" UniqueName="PriceInPurchaseUnit" SortExpression="PriceInPurchaseUnit"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount1"
                            HeaderText="Discount #1" UniqueName="PurchaseDiscount1" SortExpression="PurchaseDiscount1"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="PurchaseDiscount2"
                            HeaderText="Discount #2" UniqueName="PurchaseDiscount2" SortExpression="PurchaseDiscount2"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                            UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime">
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="~\Module\Inventory\Master\ItemProductMedical\ItemProductMedicalSupplierDetail.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="SupplierItemDetailCommand">
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
        <telerik:RadPageView ID="pgvLocation" runat="server">
            <telerik:RadGrid ID="grdLocation" runat="server" OnNeedDataSource="grdLocation_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdLocation_UpdateCommand"
                OnDeleteCommand="grdLocation_DeleteCommand" OnInsertCommand="grdLocation_InsertCommand"
                ShowStatusBar="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="LocationID" AllowPaging="true"
                    PageSize="20">
                    <PagerStyle AlwaysVisible="true" />
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LocationID" HeaderText="ID"
                            UniqueName="LocationID" SortExpression="LocationID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="LocationName" HeaderText="Name" UniqueName="LocationName"
                            SortExpression="LocationName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRItemBinName" HeaderText="Bin" UniqueName="SRItemBinName"
                            HeaderStyle-Width="250px" SortExpression="SRItemBinName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemSubBin" HeaderStyle-Width="250px" HeaderText="Sub Bin"
                            UniqueName="ItemSubBin" SortExpression="ItemSubBin" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Minimum" HeaderText="Minimum"
                            UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Maximum" HeaderText="Maximum"
                            UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Balance" HeaderText="Balance"
                            UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Booking" HeaderText="Booking"
                            UniqueName="Booking" SortExpression="Booking" HeaderStyle-HorizontalAlign="Right"
                            Visible="false" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnitName" HeaderText="Unit"
                            UniqueName="SRItemUnitName" SortExpression="SRItemUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="editED" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsPowerUser.Equals(false) ? string.Format("<a href=\"#\" onclick=\"viewEdDetail('{0}', '1'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "LocationID"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />") : string.Format("<a href=\"#\" onclick=\"viewEdDetail('{0}', '0'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "LocationID"), "<img src=\"../../../../Images/calendar16.png\" border=\"0\" title=\"Batch No. & ED\" />"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ItemKitchenBalanceDetail2.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ItemKitchenBalanceEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
