<%@ Page Title="Monitoring Status Klaim Apotek Online" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ApotekOnlineDashboard.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.ApotekOnline.ApotekOnlineDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" cellpadding="5" cellspacing="2">
        <tr>
            <td style="white-space:nowrap;text-align:right;padding-right:4px;font-weight:bold;">Bulan:</td>
            <td style="padding-right:10px;">
                <telerik:RadComboBox ID="cboMonth" runat="server" Width="120px">
                    <Items>
                        <telerik:RadComboBoxItem Value="1" Text="Januari" />
                        <telerik:RadComboBoxItem Value="2" Text="Februari" />
                        <telerik:RadComboBoxItem Value="3" Text="Maret" />
                        <telerik:RadComboBoxItem Value="4" Text="April" />
                        <telerik:RadComboBoxItem Value="5" Text="Mei" />
                        <telerik:RadComboBoxItem Value="6" Text="Juni" />
                        <telerik:RadComboBoxItem Value="7" Text="Juli" />
                        <telerik:RadComboBoxItem Value="8" Text="Agustus" />
                        <telerik:RadComboBoxItem Value="9" Text="September" />
                        <telerik:RadComboBoxItem Value="10" Text="Oktober" />
                        <telerik:RadComboBoxItem Value="11" Text="November" />
                        <telerik:RadComboBoxItem Value="12" Text="Desember" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td style="white-space:nowrap;text-align:right;padding-right:4px;font-weight:bold;">Tahun:</td>
            <td style="padding-right:10px;">
                <telerik:RadComboBox ID="cboPeriodYear" runat="server" Width="80px" AutoPostBack="true" />
            </td>
            <td style="white-space:nowrap;text-align:right;padding-right:4px;font-weight:bold;">Jenis Obat:</td>
            <td style="padding-right:10px;">
                <telerik:RadComboBox ID="cboJnsObt" runat="server" Width="120px">
                    <Items>
                        <telerik:RadComboBoxItem Value="0" Text="Semua" Selected="true" />
                        <telerik:RadComboBoxItem Value="1" Text="Obat PRB" />
                        <telerik:RadComboBoxItem Value="2" Text="Obat Kronis Blm Stabil" />
                        <telerik:RadComboBoxItem Value="3" Text="Obat Kemoterapi" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td style="white-space:nowrap;text-align:right;padding-right:4px;font-weight:bold;">Status:</td>
            <td style="padding-right:10px;">
                    <telerik:RadComboBox ID="cboStatus" runat="server" Width="120px">
                    <Items>
                        <telerik:RadComboBoxItem Value="0" Text="Belum diverifikasi" Selected="true" />
                        <telerik:RadComboBoxItem Value="1" Text="Sudah Verifikasi" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;text-align:right;padding-right:4px;font-weight:bold;"></td>
            <td style="padding-right:10px;">
                <asp:Button runat="server" ID="btnDataKlaim" Text="Get List Data Klaim BPJS" OnClick="btnDataKlaim_Click" />
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdListKlaim" runat="server" GridLines="None" AutoGenerateColumns="true"
        AllowPaging="true" PageSize="15" AllowSorting="true">
        <MasterTableView DataKeyNames="Nosepapotek">
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
