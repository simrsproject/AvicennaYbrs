<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="RujukBalikList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.RujukBalikList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None" OnDeleteCommand="grdList_DeleteCommand">
        <MasterTableView DataKeyNames="NoSRB, NoSEP" ClientDataKeyNames="NoSRB, NoSEP">
            <Columns>
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="NoSRB" HeaderText="No SRB"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoSRB" SortExpression="NoSRB"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TglSRB" HeaderText="Tgl SRB"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TglSRB" SortExpression="tglSRB"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ProgramPRBNama" HeaderText="Program PRB" UniqueName="ProgramPRBNama"
                    SortExpression="ProgramPRBNama" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="NoSEP" HeaderText="No SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoSEP" SortExpression="NoSEP"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="PesertaNoKartu" HeaderText="No Kartu"
                    UniqueName="PesertaNoKartu" SortExpression="PesertaNoKartu" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="PesertaNama" HeaderText="Nama Pasien" UniqueName="PesertaNama"
                    SortExpression="PesertaNama" />
                <telerik:GridBoundColumn DataField="DPJPNama" HeaderText="Nama DPJP" UniqueName="DPJPNama"
                    SortExpression="DPJPNama" />
                <telerik:GridBoundColumn DataField="Keterangan" HeaderText="Keterangan" UniqueName="Keterangan"
                    SortExpression="Keterangan" />
                <telerik:GridBoundColumn DataField="Saran" HeaderText="Saran" UniqueName="Saran"
                    SortExpression="Saran" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
