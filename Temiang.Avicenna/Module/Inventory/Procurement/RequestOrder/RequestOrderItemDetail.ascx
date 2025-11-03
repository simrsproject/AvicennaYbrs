<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestOrderItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Procurement.RequestOrderItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
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
                        <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                            OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                                <br />
                                Stock&nbsp;(<%# DataBinder.Eval(Container.DataItem, "SRStockGroup") %>):&nbsp;[<%# DataBinder.Eval(Container.DataItem, "BalanceSG", "{0:n2}") %>]
                                Loc:&nbsp;[<%# DataBinder.Eval(Container.DataItem, "BalanceLoc", "{0:n2}") %>] Total:&nbsp;[<%# DataBinder.Eval(Container.DataItem, "BalanceTotal", "{0:n2}") %>]
                                <br />
                                Min:&nbsp;[<%# DataBinder.Eval(Container.DataItem, "Minimum", "{0:n2}") %>] &nbsp;Max:&nbsp;[<%# DataBinder.Eval(Container.DataItem, "Maximum", "{0:n2}") %>]
                                <br />
                                Out in last
                                <%# DataBinder.Eval(Container.DataItem, "DayForMinStock")%>
                                d:&nbsp;[<%# DataBinder.Eval(Container.DataItem, "QuantityOut", "{0:n2}") %>] &nbsp;<%# DataBinder.Eval(Container.DataItem, "Unit") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                        <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr runat="server" id="trSpecification">
                    <td class="label">
                        <asp:Label ID="lblSpecification" runat="server" Text="Specification"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSpecification" runat="server" Width="300px" MaxLength="250" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Request Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                        <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="100px" AutoPostBack="True"
                            OnSelectedIndexChanged="cboSRItemUnit_SelectedIndexChanged" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Unit Conversion"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr valign="top">
                                <td>
                                    <telerik:RadNumericTextBox ID="txtQtyUnitConversion" runat="server" Width="100px"
                                        NumberFormat-DecimalDigits="2" ReadOnly="true" Enabled="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="server" ID="txtItemUnitConversion" ReadOnly="true" Width="100px"
                                        Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table runat="server" id="tblPriceInfo" width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPrice" runat="server" Text="Price" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MaxLength="16"
                            MinValue="0" ReadOnly="True" />
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiscount1Percentage" runat="server" Text="Discount 1 (%)" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDiscount1Percentage" runat="server" Type="Percent"
                            Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="True" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDiscount2Percentage" runat="server" Text="Discount 2 (%)" />
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtDiscount2Percentage" runat="server" Type="Percent"
                            Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="True" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td style="width: 50%">
        </td>
        <td>
            <table width="100%">
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemTransactionItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            OnClick="btnCancel_ButtonClick" CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlBudgetPlan" runat="server">
    <table>
        <tr>
            <td class="labelcaption" colspan="4">
                <asp:Label ID="Label5" runat="server" Text="Budget Plan Information"></asp:Label>
            </td>
        </tr>
        <tr height="30">
            <td class="label">
                <asp:Label ID="Label3" runat="server" Text="Quota"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtQuota" runat="server" Width="100px" ReadOnly="true"
                    NumberFormat-DecimalDigits="2" />
                <asp:Label ID="lblQuota" runat="server" Text="" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr height="30">
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Offered Qty"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtQtyOffered" runat="server" Width="100px" ReadOnly="true"
                    NumberFormat-DecimalDigits="2" />
                <asp:Label ID="lblQtyOffered" runat="server" Text="" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr height="30">
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Balance"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtBalace" runat="server" Width="100px" ReadOnly="true"
                    NumberFormat-DecimalDigits="2" />
                <asp:Label ID="lblBalace" runat="server" Text="" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
