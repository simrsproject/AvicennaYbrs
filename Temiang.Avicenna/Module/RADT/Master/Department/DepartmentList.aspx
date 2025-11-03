<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="DepartmentList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.DepartmentList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="DepartmentID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DepartmentID" HeaderText="Department ID"
                    UniqueName="DepartmentID" SortExpression="DepartmentID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DepartmentName" HeaderText="Department Name"
                    UniqueName="DepartmentName" SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ShortName" HeaderText="Short Name"
                    UniqueName="ShortName" SortExpression="ShortName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="Initial" HeaderText="Initial"
                    UniqueName="Initial" SortExpression="Initial" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="DepartmentManager" HeaderText="Department Manager" UniqueName="DepartmentManager"
                    SortExpression="DepartmentManager" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsHasRegistration"
                    HeaderText="Has Reg" UniqueName="IsHasRegistration" SortExpression="IsHasRegistration"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsAllowBackDateRegistration"
                    HeaderText="Back Date Reg" UniqueName="IsAllowBackDateRegistration" SortExpression="IsAllowBackDateRegistration"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
