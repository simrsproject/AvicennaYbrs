<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ZatActiveList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ZatActiveList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ZatActiveID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ZatActiveID" HeaderText="Zat Active ID"
                    UniqueName="ZatActiveID" SortExpression="ZatActiveID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ZatActiveName" HeaderText="Zat Active Name" UniqueName="ZatActiveName"
                    SortExpression="ZatActiveName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ZatActiveGroupName" HeaderText="Group" UniqueName="ZatActiveGroupName"
                    SortExpression="ZatActiveGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
