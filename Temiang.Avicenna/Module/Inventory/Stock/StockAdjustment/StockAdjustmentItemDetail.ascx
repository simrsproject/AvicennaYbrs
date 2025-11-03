<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StockAdjustmentItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Stock.StockAdjustmentItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" AutoPostBack="True"
                            EnableLoadOnDemand="True" MarkFirstMatch="False" HighlightTemplatedItems="true"
                            OnItemDataBound="cboItemID_ItemDataBound" OnItemsRequested="cboItemID_ItemsRequested"
                            OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                                    &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                                <br />
                                Stock :
                                <%# DataBinder.Eval(Container.DataItem, "Balance")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item Name is required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Label ID="lblFillInStock" runat="server" Text="Fill In Stock (-) / Stock (+)" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity is required."
                            ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRItemUnit" runat="server" Text="Item Unit"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" MaxLength="20"
                            ReadOnly="True" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Item Unit is required."
                            ControlToValidate="txtSRItemUnit" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                            Width="100%">
                            <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <asp:Panel runat="server" ID="pnlEd">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBatchNumber" runat="server" Text="Batch Number"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="cboBatchNumber" runat="server" Width="300px" AutoPostBack="True"
                                            EnableLoadOnDemand="True" MarkFirstMatch="False" HighlightTemplatedItems="true"
                                            OnItemDataBound="cboBatchNumber_ItemDataBound" OnItemsRequested="cboBatchNumber_ItemsRequested"
                                            OnSelectedIndexChanged="cboBatchNumber_SelectedIndexChanged">
                                            <ItemTemplate>
                                                <b>
                                                    <%# DataBinder.Eval(Container.DataItem, "BatchNumber") %>
                                                    &nbsp;(ED: <%# DataBinder.Eval(Container.DataItem, "ExpiredDate")%>) </b>
                                                <br />
                                                Stock :
                                                <%# DataBinder.Eval(Container.DataItem, "Balance")%>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:CheckBox ID="chkIsControlExpired" runat="server" Enabled="false"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblExpiredDate" runat="server" Text="Expired Date "></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtExpiredDate" runat="server" Width="100px" Enabled="false" MinDate="01/01/1900" MaxDate="12/31/2999">
                                            <DateInput runat="server" DateFormat="dd/MM/yyyy"></DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="dd/MM/yyyy" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemTransactionItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ItemTransactionItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" OnClick="btnCancel_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="labelcaption" colspan="4">
                        <asp:Label ID="Label2" runat="server" Text="Information"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBalance" runat="server" Text="Balance"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtBalace" runat="server" Width="100px" ReadOnly="true" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr runat="server" id="trQtyPending">
                    <td class="label">
                        <asp:Label ID="lblPending" runat="server" Text="Pending"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPending" runat="server" Width="100px" ReadOnly="true" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
