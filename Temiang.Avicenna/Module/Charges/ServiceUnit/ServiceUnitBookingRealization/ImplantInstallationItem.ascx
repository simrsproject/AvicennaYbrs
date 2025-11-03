<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImplantInstallationItem.ascx.cs" Inherits="Temiang.Avicenna.Module.Charges.ImplantInstallationItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumImplantInstallation" runat="server" ValidationGroup="ImplantInstallation" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ImplantInstallation"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblImplantType" runat="server" Text="Implant Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtImplantType" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Implant Type required."
                            ControlToValidate="txtImplantType" SetFocusOnError="True" ValidationGroup="ImplantInstallation"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSerialNo" runat="server" Text="Serial No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSerialNo" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ErrorMessage="Serial No required."
                            ControlToValidate="txtSerialNo" SetFocusOnError="True" ValidationGroup="ImplantInstallation"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQty" runat="server" Text="Qty"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MinValue="0" NumberFormat-DecimalDigits="0"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Qty required."
                            ControlToValidate="txtQty" SetFocusOnError="True" ValidationGroup="ImplantInstallation"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPlacementSite" runat="server" Text="Placement Site"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPlacementSite" runat="server" Width="300px" MaxLength="200" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPlacementSite" runat="server" ErrorMessage="Placement Site required."
                            ControlToValidate="txtPlacementSite" SetFocusOnError="True" ValidationGroup="ImplantInstallation"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ImplantInstallation"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ImplantInstallation" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>