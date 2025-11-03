<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientPrint.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PatientPrint" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdReportPatient" runat="server" AutoGenerateColumns="False" ShowGroupPanel="false"
        ShowHeader="false" Width="99%" Height="230px" GridLines="None">
        <MasterTableView DataKeyNames="ProgramID">
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
