<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="MenuList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="MenuID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MenuID" HeaderText="Menu ID"
                    UniqueName="MenuID" SortExpression="MenuID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="MenuName" HeaderText="Menu Name"
                    UniqueName="MenuName" SortExpression="MenuName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsExtra" HeaderText="Extra"
                    UniqueName="IsExtra" SortExpression="IsExtra" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />    
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>