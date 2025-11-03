<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DistributionRequestItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DistributionRequestItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 40%" valign="top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSequenceNo" runat="server" Text="Sequence No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="3"
                            Enabled="false" Text="d" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server" ErrorMessage="Sequence No required."
                            ControlToValidate="txtSequenceNo" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                            OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                                <br />
                                Stock :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Balance","{0:n2}") %>
                                &nbsp;Min :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Minimum", "{0:n2}")%>
                                &nbsp;Max :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Maximum","{0:n2}") %>
                                &nbsp;<%# DataBinder.Eval(Container.DataItem, "Unit") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr height="30">
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                        <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" AutoPostBack="True"
                            OnSelectedIndexChanged="cboSRItemUnit_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Item Unit required."
                            ControlToValidate="cboSRItemUnit" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblConversionFactor" runat="server" Text="Conversion Factor" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtConversionFactor" runat="server" Width="100px"
                            MinValue="1" Enabled="False" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvConversionFactor" runat="server" ErrorMessage="Conversion Factor required."
                            ControlToValidate="txtConversionFactor" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemTransactionItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 30%" valign="top">
            <asp:Panel runat="server" ID="pnlStockInfo">
                <table width="100%">
                    <tr>
                        <td class="labelcaption" colspan="4">
                            <asp:Label ID="lblStockInformation" runat="server" Text="Stock Information"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBalance" runat="server" Text="Balance From Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBalanceFrom" runat="server" Width="100px" NumberFormat-DecimalDigits="2" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBalanceTo" runat="server" Text="Balance To Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBalanceTo" runat="server" Width="100px" NumberFormat-DecimalDigits="2" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td style="width: 30%" valign="top">
            <asp:Panel ID="pnlBudgetPlan" runat="server">
                <table>
                    <tr>
                        <td class="labelcaption" colspan="4">
                            <asp:Label ID="Label2" runat="server" Text="Budget Plan Information"></asp:Label>
                        </td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Quota"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQuota" runat="server" Width="100px" ReadOnly="true" NumberFormat-DecimalDigits="2" />
                            <asp:Label ID="lblQuota" runat="server" Text="" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Offered Qty"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtQtyOffered" runat="server" Width="100px" ReadOnly="true" NumberFormat-DecimalDigits="2" />
                            <asp:Label ID="lblQtyOffered" runat="server" Text="" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Balance"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBalace" runat="server" Width="100px" ReadOnly="true" NumberFormat-DecimalDigits="2" />
                            <asp:Label ID="lblBalace" runat="server" Text="" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>

