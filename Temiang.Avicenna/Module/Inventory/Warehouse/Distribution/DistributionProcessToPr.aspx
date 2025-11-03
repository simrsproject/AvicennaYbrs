<%@ Page Title="New Purchase Request" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="DistributionProcessToPr.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DistributionProcessToPr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Request Date"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Request Date required."
                    ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Request Unit"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" Enabled="False" >
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Request Unit required."
                    ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblToServiceUnitID" runat="server" Text="Purchasing Unit"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvToServiceUnitID" runat="server" ErrorMessage="Purchasing Unit required."
                    ValidationGroup="entry" ControlToValidate="cboToServiceUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" Enabled="False" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="Item Type required."
                    ValidationGroup="entry" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
