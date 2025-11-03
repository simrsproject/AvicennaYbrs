<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="UnitClassMenuExtraSettingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.UnitClassMenuExtraSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="10"
                    ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit ID required."
                    ValidationGroup="entry" ControlToValidate="txtServiceUnitID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitName" runat="server" Text="Service Unit Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvServiceUnitName" runat="server" ErrorMessage="Service Unit Name required."
                    ValidationGroup="entry" ControlToValidate="txtServiceUnitName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboClassID" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class required."
                    ValidationGroup="entry" ControlToValidate="cboClassID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblMenuID" runat="server" Text="Menu"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboMenuID" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvMenuID" runat="server" ErrorMessage="Menu required."
                    ValidationGroup="entry" ControlToValidate="cboMenuID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
