<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialEconomyCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.Phr.InputControl.SocialEconomyCtl" %>
<table style="width: 100%;">
    <tr>
        <td style="width: 500px">
            <table style="width: 100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRMaritalStatus" runat="server" Text="Status Pernihakan"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRMaritalStatus" runat="server" Width="304px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="10px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label2" runat="server" Text="Hubungan pasien dengan anggota keluarga"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRRelationshipQuality" runat="server" Width="304px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="10px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 500px">
            <table style="width: 100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Tempat tinggal"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRResidentialHome" runat="server" Width="304px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="10px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSROccupation" runat="server" Text="Pekerjaan"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSROccupation" runat="server" Width="304px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="10px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td></td>
    </tr>
</table>
