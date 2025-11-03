<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ZipCodeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ZipCodeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblZipCode" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtZipCode" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ErrorMessage="ID required."
                    ValidationGroup="entry" ControlToValidate="txtZipCode"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblZipPostalCode" runat="server" Text="Zip Code"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtZipPostalCode" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvZipPostalCode" runat="server" ErrorMessage="Zip Code required."
                    ValidationGroup="entry" ControlToValidate="txtZipPostalCode"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStreetName" runat="server" Text="Street Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStreetName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvStreetName" runat="server" ErrorMessage="Street Name required."
                    ValidationGroup="entry" ControlToValidate="txtStreetName"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDistrict" runat="server" Text="District"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDistrict" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ErrorMessage="District required."
                    ValidationGroup="entry" ControlToValidate="txtDistrict"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCounty" runat="server" Text="County"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCounty" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCounty" runat="server" ErrorMessage="County required."
                    ValidationGroup="entry" ControlToValidate="txtCounty"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCity" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="City required."
                    ValidationGroup="entry" ControlToValidate="txtCity"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRProvince" runat="server" Text="Province"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRProvince" runat="server" Width="300px" AllowCustomText="true" Filter="Contains"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRProvince" runat="server" ErrorMessage="Province required."
                    ValidationGroup="entry" ControlToValidate="cboSRProvince"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLatitude" runat="server" Text="Latitude"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtLatitude" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvLatitude" runat="server" ErrorMessage="Latitude required."
                    ValidationGroup="entry" ControlToValidate="txtLatitude"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLongitude" runat="server" Text="Longitude"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtLongitude" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvLongitude" runat="server" ErrorMessage="Longitude required."
                    ValidationGroup="entry" ControlToValidate="txtLongitude"
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>

    </table>
</asp:Content>


