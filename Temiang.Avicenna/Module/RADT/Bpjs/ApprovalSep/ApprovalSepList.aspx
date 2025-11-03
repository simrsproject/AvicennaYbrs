<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ApprovalSepList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.ApprovalSepList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="noKartu,tglSep,jnsPelayanan,jnsPengajuan" ClientDataKeyNames="noKartu,tglSep,jnsPelayanan,jnsPengajuan">
            <Columns>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="tglSep" HeaderText="Tgl SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="tglSep" SortExpression="tglSep"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="noKartu" HeaderText="No Kartu"
                    UniqueName="noKartu" SortExpression="noKartu" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="NamaPasienJK" HeaderText="Nama Pasien (JK)" UniqueName="NamaPasienJK"
                    SortExpression="NamaPasienJK" />
                <telerik:GridBoundColumn DataField="TypeOfService" HeaderText="Jenis Pelayanan" UniqueName="TypeOfService"
                    SortExpression="TypeOfService" />
                <telerik:GridBoundColumn DataField="TypeOfApproval" HeaderText="Jenis Pengajuan" UniqueName="TypeOfApproval"
                    SortExpression="TypeOfApproval" />
                <telerik:GridBoundColumn DataField="catatan" HeaderText="Keterangan" UniqueName="keterangan"
                    SortExpression="keterangan" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsApproved" SortExpression="IsApproved"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
