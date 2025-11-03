<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="RujukanList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.RujukanList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="noSep" ClientDataKeyNames="noSep">
            <Columns>
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="NoRujukan" HeaderText="No Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoRujukan" SortExpression="NoRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="noSep" HeaderText="No SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="noSep" SortExpression="noSep"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="tglRujukan" HeaderText="Tgl Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="tglRujukan" SortExpression="tglRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="tglRencana" HeaderText="Tgl Rencana"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="tglRencana" SortExpression="tglRencana"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="NomorKartu" HeaderText="No Kartu"
                    UniqueName="NomorKartu" SortExpression="NomorKartu" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="NamaPasienJK" HeaderText="Nama Pasien (JK)" UniqueName="NamaPasienJK"
                    SortExpression="NamaPasienJK" />
                <telerik:GridBoundColumn DataField="TypeOfService" HeaderText="Jenis Pelayanan" UniqueName="TypeOfService"
                    SortExpression="TypeOfService" />
                <telerik:GridBoundColumn DataField="namaPoliRujukan" HeaderText="Poli Tujuan" UniqueName="namaPoliRujukan"
                    SortExpression="namaPoliRujukan" />
                <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnosa Awal" UniqueName="DiagnoseName"
                    SortExpression="DiagnoseName" />
                <telerik:GridBoundColumn DataField="catatan" HeaderText="Catatan" UniqueName="catatan"
                    SortExpression="catatan" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
