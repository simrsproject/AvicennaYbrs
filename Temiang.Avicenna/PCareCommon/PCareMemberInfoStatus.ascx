<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PCareMemberInfoStatus.ascx.cs" Inherits="Temiang.Avicenna.PCareCommon.PCareMemberInfoStatus" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<cc:CollapsePanel ID="cpBpjsInfo" runat="server" Title="BPJS Member Status" Width="100%">
    <table style="width: 100%">
        <tr>
            <td class="label">
                <asp:Label ID="Label5" runat="server" Text="Tgl Mulai Aktif"></asp:Label>
            </td>
            <td class="entry300">
                <telerik:RadTextBox ID="txtTglMulaiAktif" runat="server" Width="285px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Tgl Mulai Berlaku"></asp:Label>
            </td>
            <td class="entry300">
                <telerik:RadTextBox ID="txtTglAkhirBerlaku" runat="server" Width="285px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label14" runat="server" Text="Status Aktif"></asp:Label>
            </td>
            <td class="entry300">
                <asp:CheckBox runat="server" ID="chkAktif" />
                <telerik:RadTextBox ID="txtKetAktif" runat="server" Width="262px" ReadOnly="True" />
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label8" runat="server" Text="Member Klinik Pratama"></asp:Label>
            </td>
            <td class="entry300">
                <telerik:RadTextBox ID="txtKdProviderPst_kdProvider" runat="server" Width="70px" ReadOnly="True" />&nbsp;
                        <telerik:RadTextBox ID="txtKdProviderPst_nmProvider" runat="server" Width="208px" ReadOnly="True" />
            </td>
        </tr>
    </table>
</cc:CollapsePanel>
