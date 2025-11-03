<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemProductTariffRequestItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemProductTariffRequestItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTariffRequestItem" runat="server" ValidationGroup="ItemTariffRequestItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTariffRequestItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry" valign="middle">
                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="True"
                            HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboItemID_ItemDataBound"
                            OnItemsRequested="cboItemID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                &nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 result
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ItemTariffRequestItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRItemUnit" runat="server" Text="Item Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="300px" MaxLength="100"
                            Enabled="false" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRPurchaseUnit" runat="server" Text="Purchase Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSRPurchaseUnit" runat="server" Width="300px" MaxLength="100"
                            Enabled="false" />
                    </td>
                    <td width="20px">
                        
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
                            MaxLength="6" MinValue="0" Enabled="false" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPriceInPurchaseUnit" runat="server" Text="Price In Purchase Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPriceInPurchaseUnit" runat="server" Type="Number"
                            Width="100px" MaxLength="16" MaxValue="9999999999999.99" MinValue="0" AutoPostBack="true"
                            OnTextChanged="txtPriceInPurchaseUnit_TextChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPriceInPurchaseUnit" runat="server" ErrorMessage="Price In Purchase Unit required."
                            ControlToValidate="txtPriceInPurchaseUnit" SetFocusOnError="True" ValidationGroup="ItemTariffRequestItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
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
                            MaxValue="9999999999999.99" MinValue="0" AutoPostBack="true" OnTextChanged="txtPriceInBaseUnit_TextChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPriceInBaseUnit" runat="server" ErrorMessage="Price In Base Unit required."
                            ControlToValidate="txtPriceInBaseUnit" SetFocusOnError="True" ValidationGroup="ItemTariffRequestItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
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
                            MaxLength="16" MaxValue="9999999999999.99" MinValue="0" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPriceInBasedUnitWVat" runat="server" ErrorMessage="Price In Based Unit WVat required."
                            ControlToValidate="txtPriceInBasedUnitWVat" SetFocusOnError="True" ValidationGroup="ItemTariffRequestItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
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
                            MaxValue="9999999999999.99" MinValue="0" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvCostPrice" runat="server" ErrorMessage="Cost Price required."
                            ControlToValidate="txtCostPrice" SetFocusOnError="True" ValidationGroup="ItemTariffRequestItem"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="tblSalesDisocunt" runat="server">
                    <td class="label">
                        <asp:Label ID="lblDiscPercentage" runat="server" Text="Sales Discount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDiscPercentage" runat="server" Width="100px" MaxLength="6"
                            MaxValue="999.99" MinValue="0" Type="Percent" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTariffRequestItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemTariffRequestItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
