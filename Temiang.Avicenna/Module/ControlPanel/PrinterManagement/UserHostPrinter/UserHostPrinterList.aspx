<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" 
	CodeBehind="UserHostPrinterList.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.PrinterManagement.UserHostPrinterList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
            <MasterTableView DataKeyNames="UserHostName">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="UserHostName" HeaderText="User Host Name" UniqueName="UserHostName" SortExpression="UserHostName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Description" HeaderText="Description" UniqueName="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="PrinterName" HeaderText="Printer Default" UniqueName="PrinterName" SortExpression="PrinterName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

