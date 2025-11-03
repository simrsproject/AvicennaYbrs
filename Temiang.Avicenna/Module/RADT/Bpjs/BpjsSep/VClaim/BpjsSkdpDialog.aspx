<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="BpjsSkdpDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsSep.VClaim.BpjsSkdpDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchBulan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTahun">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPatient" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table>
        <tr>
            <td class="label">Bulan</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboBulan" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnSearchBulan" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnSearchSkdp_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Tahun</td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtTahun" runat="server" Width="100px" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnSearchTahun" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnSearchSkdp_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Filter</td>
            <td class="entry">
                <asp:RadioButtonList ID="rblFilter" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Tanggal Entry" Value="1" Selected="True" />
                    <asp:ListItem Text="Tanggal Rencana Kontrol" Value="2" />
                </asp:RadioButtonList>
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnSearchFilter" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnSearchSkdp_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdPatient" runat="server" OnNeedDataSource="grdPatient_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="noSuratKontrol">
            <Columns>
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="NoSuratKontrol" HeaderText="No SRK"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoSuratKontrol" SortExpression="NoSuratKontrol"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TglRencanaKontrol" HeaderText="Tgl Rencana"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TglRencanaKontrol" SortExpression="TglRencanaKontrol"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TglTerbitKontrol" HeaderText="Tgl Terbit"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TglTerbit" SortExpression="TglTerbit"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="NamaPoliTujuan" HeaderText="Poli Tujuan" UniqueName="NamaPoliTujuan"
                    SortExpression="NamaPoliTujuan" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="NoSepAsalKontrol" HeaderText="No SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoSep" SortExpression="NoSep"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TglSEP" HeaderText="Tgl SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TglSep" SortExpression="TglSep"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="NoKartu" HeaderText="No Kartu"
                    UniqueName="NoKartu" SortExpression="NoKartu" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Nama" HeaderText="Nama Pasien" UniqueName="Nama"
                    SortExpression="Nama" />
                <telerik:GridBoundColumn DataField="NamaDokter" HeaderText="Nama DPJP" UniqueName="NamaDokter"
                    SortExpression="NamaDokter" />
                <telerik:GridBoundColumn DataField="TerbitSEP" HeaderText="Terbit SEP" UniqueName="TerbitSEP"
                    SortExpression="TerbitSEP" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
