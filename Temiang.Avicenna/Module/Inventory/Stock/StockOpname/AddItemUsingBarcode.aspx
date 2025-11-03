<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="AddItemUsingBarcode.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.StockOpname.AddItemUsingBarcode"
    Title="Stock Opname" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function txtBarcodeEntryKeyPress(sender, eventArgs) {
                var code = eventArgs.get_keyCode();
                if (code == 13) {
                    __doPostBack(sender._clientID.replace(/_/g, "$"), "entrybarcode|" + sender.get_value());
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%">
        <tr>
            <td class="label">Barcode
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="100%" AutoCompleteType="Disabled"
                    Font-Size="40px" SelectionOnFocus="SelectAll">
                    <ClientEvents OnKeyPress="txtBarcodeEntryKeyPress" />
                </telerik:RadTextBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Item
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="100%" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                    OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>)
                        <br />
                        Bal: &nbsp;<%# DataBinder.Eval(Container.DataItem, "Balance")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "Unit")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Total Current Qty
            </td>
            <td class="entry">
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                                MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px"></td>
        </tr>
        <tr runat="server" id="trPrevBalance">
            <td class="label">
                <asp:Label ID="lblPrevBal" runat="server" Text="Prev. Balance"></asp:Label>
            </td>
            <td class="entry">
                <table cellspacing="0" cellpadding="0">
                    <tr valign="top">
                        <td>
                            <telerik:RadNumericTextBox ID="txtPrevBal" runat="server" Width="100px"
                                NumberFormat-DecimalDigits="2" ReadOnly="true" Enabled="false" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtSRItemUnitPrev" ReadOnly="true" Width="100px"
                                Enabled="false">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Note
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNote" runat="server" Width="100%" TextMode="MultiLine" MaxLength="500" />
            </td>
            <td width="20px"></td>
        </tr>
    </table>
</asp:Content>
