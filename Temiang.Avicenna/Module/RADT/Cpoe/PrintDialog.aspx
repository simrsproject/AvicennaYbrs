<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="PrintDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.PrintDialog" Title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdReport" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        ShowHeader="false" GridLines="None">
        <MasterTableView DataKeyNames="RelatedProgramID">
            <Columns>
                <telerik:GridBoundColumn DataField="ProgramName" HeaderText="Report" UniqueName="ProgramName"
                    SortExpression="ProgramName">
                    <HeaderStyle HorizontalAlign="Center" Width="100%" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings Selecting-AllowRowSelect="true">
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>