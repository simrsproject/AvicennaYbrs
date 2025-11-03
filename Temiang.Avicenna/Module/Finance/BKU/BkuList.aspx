<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="BkuList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.BkuList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="Nomor" ClientDataKeyNames="Nomor">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Nomor" HeaderText="Nomor"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Nomor" SortExpression="Nomor"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="Tanggal" HeaderText="Tanggal"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Tanggal" SortExpression="Tanggal"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="NamaJenis" HeaderText="Jenis" UniqueName="NamaJenis"
                    SortExpression="NamaJenis" />
                <telerik:GridBoundColumn DataField="NamaPelanggan" HeaderText="Pelanggan"
                    UniqueName="NamaPelanggan" SortExpression="NamaPelanggan" />
                <telerik:GridBoundColumn DataField="NamaUnit" HeaderText="Unit"
                    UniqueName="NamaUnit" SortExpression="NamaUnit" />
                <telerik:GridBoundColumn DataField="NamaKasBank" HeaderText="Kas/Bank" UniqueName="NamaKasBank"
                    SortExpression="NamaKasBank" />
                <telerik:GridBoundColumn DataField="Uraian" HeaderText="Uraian" UniqueName="Uraian"
                    SortExpression="Uraian" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Total" HeaderText="Total" UniqueName="Total"
                    SortExpression="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
