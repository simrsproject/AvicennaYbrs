<%@ Page Title="Patient Information" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="BPJSRegistrationInfo.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.BPJSRegistrationInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
        AllowSorting="True" GridLines="None">
        <MasterTableView DataKeyNames="NoSEP">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="160px" DataField="NoSEP" HeaderText="No SEP"
                    UniqueName="NoSEP" SortExpression="NoSEP" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalSEP" HeaderText="Tgl SEP"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalSEP" SortExpression="TanggalSEP"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="NoRujukan" HeaderText="No Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="NoRujukan" SortExpression="NoRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalRujukan" HeaderText="Tgl Rujukan"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalRujukan" SortExpression="TanggalRujukan"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="NomorKartu" HeaderText="No Kartu"
                    UniqueName="NomorKartu" SortExpression="NomorKartu" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="NamaPasienJK" HeaderText="Nama Pasien (JK)" UniqueName="NamaPasienJK"
                    SortExpression="NamaPasienJK" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TanggalLahir" HeaderText="Tgl Lahir"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="TanggalLahir" SortExpression="TanggalLahir"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TypeOfService" HeaderText="Jenis Pelayanan" UniqueName="TypeOfService"
                    SortExpression="TypeOfService" />
                <telerik:GridBoundColumn DataField="BridgingName" HeaderText="Poli Tujuan" UniqueName="BridgingName"
                    SortExpression="BridgingName" />
                <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnosa Awal" UniqueName="DiagnoseName"
                    SortExpression="DiagnoseName" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsLakaLantas" HeaderText="Laka Lantas"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsLakaLantas" SortExpression="IsLakaLantas"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsRegistration" HeaderText="Registrasi"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="IsRegistration" SortExpression="IsLakaLantas"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
