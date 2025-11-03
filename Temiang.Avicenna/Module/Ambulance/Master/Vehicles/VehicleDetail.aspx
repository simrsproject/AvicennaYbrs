<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="VehicleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Ambulance.Master.VehicleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlatNo" runat="server" Text="Plat No"></asp:Label>
                            <asp:HiddenField ID="hfVehicleID" runat="server" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlatNo" runat="server" Width="100px" MaxLength="15" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPlatNo" runat="server" ErrorMessage="Plat ID required."
                                ValidationGroup="entry" ControlToValidate="txtPlatNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Vehicle Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboVehicleType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vehicle type required."
                                ValidationGroup="entry" ControlToValidate="cboVehicleType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRVehicleStatus" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboVehicleStatus" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRVehicleStatus" runat="server" ErrorMessage="Vehicle status required."
                                ValidationGroup="entry" ControlToValidate="cboVehicleStatus" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="255" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td width="20"></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
            </td>
        </tr>
    </table>
</asp:Content>
