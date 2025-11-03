<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemLinenItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Master.ItemLinenItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumItemLinenItem" runat="server" ValidationGroup="ItemLinenItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ItemLinenItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblDetailItemID" runat="server" Text="Item Detail"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboItemID" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboItemID_ItemDataBound"
                OnItemsRequested="cboItemID_ItemsRequested" OnSelectedIndexChanged="cboItemID_SelectedIndexChanged"
                ValidationGroup="other">
                <ItemTemplate>
                    <b>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDetailItemID" runat="server" ErrorMessage="Detail Item ID required."
                ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ItemLinenItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
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
                        <telerik:RadNumericTextBox ID="txtQtyDetail" runat="server" Width="65px" />
                    </td>
                    <td>
                        &nbsp;<telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" ReadOnly="true" />
                    </td>
                    <td>
                        &nbsp;/&nbsp;
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="65px" MinValue="1" />
                    </td>
                    <td>
                        &nbsp;<telerik:RadTextBox ID="txtSRItemUnitKg" runat="server" Width="60px" ReadOnly="true" Text="Kg" />
                    </td>
                </tr>
            </table>
            
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQtyDetail" runat="server" ErrorMessage="Quantity Detail required."
                ControlToValidate="txtQtyDetail" SetFocusOnError="True" ValidationGroup="ItemLinenItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvSRItemUnit" runat="server" ErrorMessage="Item Unit Detail required."
                ControlToValidate="txtSRItemUnit" SetFocusOnError="True" ValidationGroup="ItemLinenItem"
                Width="100%">
                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Quantity (KG) required."
                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="ItemLinenItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ItemLinenItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ItemLinenItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
