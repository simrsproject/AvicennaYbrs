<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LaunderedProcessDetailItemConsumption.ascx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LaunderedProcessDetailItemConsumption" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumLaunderedProcessItemConsumption" runat="server" ValidationGroup="LaunderedProcessItemConsumption" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="LaunderedProcessItemConsumption"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="350px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested"
                            AutoPostBack="True" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="LaunderedProcessItemConsumption"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr height="30">
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MaxLength="10"
                                        MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="cboSRItemUnit" Width="100px" AllowCustomText="true"
                                        Filter="Contains" Enabled="false">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="LaunderedProcessItemConsumption"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Unit required."
                            ControlToValidate="cboSRItemUnit" SetFocusOnError="True" ValidationGroup="LaunderedProcessItemConsumption"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPrice" runat="server" Text="Price" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MaxLength="16"
                            MinValue="0" />
                        <telerik:RadNumericTextBox ID="txtCostPrice" runat="server" Width="100px" MaxLength="16"
                            MinValue="0" />
                        <asp:CheckBox ID="chkIsInventoryItem" runat="server" Text="Inventory Item" />
                    </td>
                    <td width="20px" />
                    <td />
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="LaunderedProcessItemConsumption"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="LaunderedProcessItemConsumption" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
