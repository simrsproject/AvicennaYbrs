<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DestructionOfExpiredItemsDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DestructionOfExpiredItemsDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemTransactionItem" runat="server" ValidationGroup="ItemTransactionItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemTransactionItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr style="display: none">
        <td class="label">
            <asp:Label ID="lblSequenceNo" runat="server" Text="Sequence No"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtSequenceNo" runat="server" Width="100px" MaxLength="3"
                Enabled="false" Text="" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server" ErrorMessage="Sequence No required."
                ControlToValidate="txtSequenceNo" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboItemID" Height="190px" Width="350px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                    <br />
                    Stock :&nbsp;<%# DataBinder.Eval(Container.DataItem, "Balance","{0:n2}") %>
                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "Unit") %>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Width="100px" MaxLength="10"
                MaxValue="99999999" MinValue="0" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                ControlToValidate="txtQuantity" SetFocusOnError="True" ValidationGroup="ItemTransactionItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblItemUnit" runat="server" Text="Item Unit"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" ReadOnly="true" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
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