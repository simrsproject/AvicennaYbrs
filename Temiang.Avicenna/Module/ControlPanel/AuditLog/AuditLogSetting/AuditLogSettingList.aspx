<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="AuditLogSettingList.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.AuditLogSettingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TableName">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="TableName" HeaderText="Table Name"
                    UniqueName="TableName" SortExpression="TableName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TableDescription" HeaderText="Table Description"
                    UniqueName="TableDescription" SortExpression="TableDescription" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsAuditLog" HeaderText="Audit Log"
                    UniqueName="IsAuditLog" SortExpression="IsAuditLog" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
