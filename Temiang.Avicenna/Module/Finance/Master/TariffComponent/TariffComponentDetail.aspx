<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="TariffComponentDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.TariffComponentDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTariffComponentID" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTariffComponentID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTariffComponentID" runat="server" ErrorMessage="ID required."
                    ValidationGroup="entry" ControlToValidate="txtTariffComponentID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTariffComponentName" runat="server" Text="Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTariffComponentName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvTariffComponentName" runat="server" ErrorMessage="Name required."
                    ValidationGroup="entry" ControlToValidate="txtTariffComponentName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td class="label">
                <asp:Label ID="lblSRTariffComponentType" runat="server" Text="Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRTariffComponentType" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRTariffComponentType" runat="server" ErrorMessage="Type required."
                    ControlToValidate="cboSRTariffComponentType" SetFocusOnError="True" ValidationGroup="entry"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
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
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="200" TextMode="MultiLine" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsTariffParamedic" runat="server" Text="Tariff Physician" />
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsIncludeInTaxCalc" runat="server" Text="Include In Tax Calculation" />
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsPrintParamedicInSlip" runat="server" Text="Print Paramedic Name In Slip" />
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsAutoChecklist" runat="server" Text="Auto Checklist Corrected Fee Verification" />
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="PPh Type Of Physician Fee"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPphType" runat="server" Width="300px" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Physician Fee Verification Default Selected"></asp:Label>
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkFeeVerificationDefaultSelected" runat="server" Text="Auto Checklist Corrected Fee Verification" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>