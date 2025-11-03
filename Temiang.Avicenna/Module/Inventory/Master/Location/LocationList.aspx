<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="LocationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.LocationList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="LocationID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LocationID" HeaderText="Location ID"
                    UniqueName="LocationID" SortExpression="LocationID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="LocationName" HeaderText="Location Name" UniqueName="LocationName"
                    SortExpression="LocationName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ShortName" HeaderText="Short Name"
                    UniqueName="ShortName" SortExpression="ShortName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ParentID" HeaderText="Parent ID"
                    UniqueName="ParentID" SortExpression="ParentID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemGroupID" HeaderText="Item Group ID"
                    UniqueName="ItemGroupID" SortExpression="ItemGroupID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemSubGroupID" HeaderText="Item Sub Group ID"
                    UniqueName="ItemSubGroupID" SortExpression="ItemSubGroupID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn DataField="StockGroupName" HeaderText="Stock Group"
                    UniqueName="StockGroupName" SortExpression="StockGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsHoldForTransaction" HeaderText="Hold For Transaction"
                    UniqueName="IsHoldForTransaction" SortExpression="IsHoldForTransaction" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsConsignment" HeaderText="Consignment"
                    UniqueName="IsConsignment" SortExpression="IsConsignment" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
