<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisitPackageItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.VisitPackageItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumVisitPackageItem" runat="server" ValidationGroup="VisitPackageItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="VisitPackageItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" OnItemDataBound="cboItemID_ItemDataBound"
                OnItemsRequested="cboItemID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ItemName") %>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 20 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="VisitPackageItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblQty" runat="server" Text="Quantity"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" Value="1" MinValue="0" NumberFormat-DecimalDigits="0" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Quantity required."
                ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="VisitPackageItem"
                Width="100%">
                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="VisitPackageItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="VisitPackageItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button></td>
    </tr>
</table>
