<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ItemList.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Master.ItemList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ItemGroupName" HeaderText="Item Group"
                    UniqueName="ItemGroupName" SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Item Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="CssdItemGroup" HeaderText="Cssd Item Group"
                    UniqueName="CssdItemGroup" SortExpression="CssdItemGroup" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CssdPackagingCostAmount" HeaderText="Cost Amount"
                    UniqueName="CssdPackagingCostAmount" SortExpression="CssdPackagingCostAmount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsItemProduction" HeaderText="Item Production"
                    UniqueName="IsItemProduction" SortExpression="IsItemProduction" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
        <PagerStyle Position="TopAndBottom" />
    </telerik:RadGrid>
</asp:Content>
