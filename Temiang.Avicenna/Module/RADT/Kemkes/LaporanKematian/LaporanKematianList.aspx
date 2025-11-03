<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="LaporanKematianList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Kemkes.LaporanKematianList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None" OnDeleteCommand="grdList_DeleteCommand">
        <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Nik" HeaderText="Nik"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Nik" SortExpression="Nik"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalMasuk" HeaderText="Tgl Masuk"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalMasuk" SortExpression="TanggalMasuk"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SaturasiOksigen" HeaderText="Saturasi Oksigen"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="SaturasiOksigen" SortExpression="SaturasiOksigen"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalKematian" HeaderText="Tgl Kematian"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalKematian" SortExpression="TanggalKematian"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="LokasiKematian" HeaderText="Lokasi Kematian"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="LokasiKematian" SortExpression="LokasiKematian"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PenyebabKematianLangsungId" HeaderText="Penyebab Kematian Langsung"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="PenyebabKematianLangsungId" SortExpression="PenyebabKematianLangsungId"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="KasusKematian" HeaderText="Kasus Kematian"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="KasusKematian" SortExpression="KasusKematian"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="StatusKomorbid" HeaderText="Status Komorbid"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="StatusKomorbid" SortExpression="StatusKomorbid"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Komorbid" HeaderText="Komorbid Id"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Komorbid" SortExpression="Komorbid"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="StatusKehamilan" HeaderText="StatusKehamilan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="StatusKehamilan" SortExpression="StatusKehamilan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
