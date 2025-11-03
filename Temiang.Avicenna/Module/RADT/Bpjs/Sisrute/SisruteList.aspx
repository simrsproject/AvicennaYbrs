<%@  Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="SisruteList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.SisruteList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDeleteCommand="grdList_DeleteCommand"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="RUJUKAN.NOMOR">
            <Columns>
                <telerik:GridBoundColumn DataField="RUJUKAN.STATUS.NAMA" HeaderText="Status" HeaderStyle-HorizontalAlign="Left"
                    UniqueName="RUJUKAN.STATUS.NAMA" SortExpression="RUJUKAN.STATUS.NAMA" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="RUJUKAN.NOMOR" HeaderText="No. Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="RUJUKAN.NOMOR" SortExpression="Rujukan.Nomor"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="RUJUKAN.TANGGAL"
                    HeaderText="Tgl. Rujukan" HeaderStyle-HorizontalAlign="Center" UniqueName="RUJUKAN.TANGGAL"
                    SortExpression="RUJUKAN.TANGGAL" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="RUJUKAN.FASKESASAL.NAMA" HeaderText="Faskes Asal"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="RUJUKAN.FASKESASAL.NAMA" SortExpression="RUJUKAN.FASKESASAL.NAMA"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RUJUKAN.FASKESTUJUAN.NAMA" HeaderText="Faskes Tujuan"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="RUJUKAN.FASKESTUJUAN.NAMA" SortExpression="RUJUKAN.FASKESTUJUAN.NAMA"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RUJUKAN.JENISRUJUKAN.NAMA" HeaderText="Jenis Rujukan"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="RUJUKAN.JENISRUJUKAN.NAMA" SortExpression="RUJUKAN.JENISRUJUKAN.NAMA"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="RUJUKAN.ALASAN.NAMA" HeaderText="Alasan" HeaderStyle-HorizontalAlign="Left"
                    UniqueName="RUJUKAN.ALASAN.NAMA" SortExpression="RUJUKAN.ALASAN.NAMA" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="PASIEN.NORM" HeaderText="No. RM"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="PASIEN.NORM" SortExpression="PASIEN.NORM"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PASIEN.NOKARTUJKN"
                    HeaderText="No. Kartu JKN" HeaderStyle-HorizontalAlign="Center" UniqueName="PASIEN.NOKARTUJKN"
                    SortExpression="PASIEN.NOKARTUJKN" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="PASIEN.NAMA" HeaderText="Nama Pasien" HeaderStyle-HorizontalAlign="Left"
                    UniqueName="PASIEN.NAMA" SortExpression="PASIEN.NAMA" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="PASIEN.TANGGALLAHIR"
                    HeaderText="Tgl. Lahir" HeaderStyle-HorizontalAlign="Center" UniqueName="PASIEN.TANGGALLAHIR"
                    SortExpression="PASIEN.TANGGALLAHIR" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="PASIEN.ALAMAT" HeaderText="Alamat" HeaderStyle-HorizontalAlign="Left"
                    UniqueName="PASIEN.ALAMAT" SortExpression="PASIEN.ALAMAT" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
