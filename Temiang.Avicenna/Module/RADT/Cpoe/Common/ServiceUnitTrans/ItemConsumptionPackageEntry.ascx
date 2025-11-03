<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemConsumptionPackageEntry.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Cpoe.ItemConsumptionPackageEntry" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumTransChargesItemConsumption" runat="server" ValidationGroup="TransChargesItemConsumption" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TransChargesItemConsumption"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblDetailItemID" runat="server" Text="Item ID"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboDetailItemID" Width="304px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" AutoPostBack="true" MarkFirstMatch="False" OnItemDataBound="cboDetailItemID_ItemDataBound"
                OnItemsRequested="cboDetailItemID_ItemsRequested" OnSelectedIndexChanged="cboDetailItemID_SelectedIndexChanged">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                    &nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "ItemID")%>) </b>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvDetailItemID" runat="server" ErrorMessage="Detail Item ID required."
                ControlToValidate="cboDetailItemID" SetFocusOnError="True" ValidationGroup="TransChargesItemConsumption"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" />
            <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" ReadOnly="true" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required."
                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="TransChargesItemConsumption"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TransChargesItemConsumption"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="TransChargesItemConsumption" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
