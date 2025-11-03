<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DriverDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Ambulance.Master.DriverDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Driver Name"></asp:Label>
                            <asp:HiddenField ID="hfDriverID" runat="server" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDriverName" runat="server" Width="300px" MaxLength="100" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Driver Name required."
                                ValidationGroup="entry" ControlToValidate="txtDriverName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboDriverStatus" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Driver status required."
                                ValidationGroup="entry" ControlToValidate="cboDriverStatus" SetFocusOnError="True"
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
