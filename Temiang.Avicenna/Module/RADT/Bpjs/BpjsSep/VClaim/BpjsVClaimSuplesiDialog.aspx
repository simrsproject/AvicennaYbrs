<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="BpjsVClaimSuplesiDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.VClaim.BpjsVClaimSuplesiDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCariData">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table>
        <tr>
            <td class="label">
                No Peserta
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNoPesertaPeserta" runat="server" Width="300px" ReadOnly="true" />
            </td>
            <td width="20px">
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                Tgl. Pelayanan
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtTglPelayanan" runat="server" Width="100px" DateInput-DateFormat="yyyy-MM-dd"
                    DateInput-DisplayDateFormat="yyyy-MM-dd" />
                *yyyy-mm-dd
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label" />
            <td class="entry">
                <asp:Button ID="btnCariData" runat="server" Text="Cari Data" OnClick="btnCariData_Click" />
            </td>
            <td width="20" />
            <td />
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        AllowPaging="True" PageSize="15" AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="noSep">
            <Columns>
                <telerik:GridBoundColumn DataField="noRegister" HeaderText="No Register" UniqueName="noRegister"
                    SortExpression="noRegister">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="noSep" HeaderText="No SEP" UniqueName="noSep"
                    SortExpression="noSep">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="noSepAwal" HeaderText="No SEP Awal" UniqueName="noSepAwal"
                    SortExpression="noSepAwal">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="noSuratJaminan" HeaderText="No. Surat Jaminan"
                    UniqueName="noSuratJaminan" SortExpression="noSuratJaminan">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="tglKejadian" HeaderText="Tgl Kejadian" UniqueName="tglKejadian"
                    SortExpression="tglKejadian">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="tglSep" HeaderText="Tgl SEP" UniqueName="tglSep"
                    SortExpression="tglSep">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
