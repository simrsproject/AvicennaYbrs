<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="WorkOrderSentToThirdPartiesDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderSentToThirdPartiesDialog"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDateSent" runat="server" Text="Date Sent"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtDateSent" runat="server" Width="100px">
                </telerik:RadDatePicker>
            </td>
            <td width="20px">
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLetterNo" runat="server" Text="Letter No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLetterNo" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20px">
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSupplierID" runat="server" Text="Supplier"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSupplierID" Width="300px" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" OnItemDataBound="cboSupplierID_ItemDataBound"
                    ValidationGroup="other" OnItemsRequested="cboSupplierID_ItemsRequested">
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
            </td>
        </tr>
    </table>
</asp:Content>
