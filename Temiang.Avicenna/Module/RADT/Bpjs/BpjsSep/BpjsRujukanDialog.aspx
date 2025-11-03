<%@  Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="BpjsRujukanDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.BpjsRujukanDialog"
    Title="Data Rujukan" %>
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
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label id=lblNomor runat=server Text="No" />
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNoKartu" runat="server" Width="300px" />
            </td>
            <td width="20" />
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
        <MasterTableView DataKeyNames="NoKunjungan">
            <Columns>
                <telerik:GridBoundColumn DataField="NoKunjungan" HeaderText="No Rujukan" UniqueName="NoKunjungan"
                    SortExpression="NoKunjungan">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="TglKunjungan" HeaderText="Tgl Rujukan" UniqueName="TglKunjungan"
                    SortExpression="TglKunjungan">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="NoKartu" HeaderText="No Kartu" UniqueName="NoKartu"
                    SortExpression="NoKartu">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NamaPeserta" HeaderText="Nama" UniqueName="NamaPeserta"
                    SortExpression="NamaPeserta">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NamaPPKPerujuk" HeaderText="PPK Perujuk" UniqueName="NamaPPKPerujuk"
                    SortExpression="NamaPPKPerujuk">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
