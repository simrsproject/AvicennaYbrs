<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="SterileItemsRequestList.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterileItemsRequestList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" AllowPaging="true" PageSize="15"
        AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="RequestNo" GroupLoadMode="Client">
            <Columns>
                <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="RequestNo"
                    DataNavigateUrlFields="RUrl" HeaderText="Transaction No" UniqueName="RequestNo"
                    SortExpression="RequestNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="RequestDate" HeaderText="Date"
                    UniqueName="RequestDate" SortExpression="RequestDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="FromServiceUnitName" HeaderText="From Unit" UniqueName="FromServiceUnitName"
                    SortExpression="FromServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="FromRoomName" HeaderText="From Room" UniqueName="FromRoomName"
                    SortExpression="FromRoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />    
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SenderBy" HeaderText="Sender By"
                    UniqueName="SenderBy" SortExpression="SenderBy" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="RequestNo, RequestSeqNo" Name="grdDetail"
                    AutoGenerateColumns="False" AllowPaging="true" PageSize="15">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="RequestSeqNo" HeaderText="Seq No"
                            UniqueName="RequestSeqNo" SortExpression="RequestSeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CssdItemUnit" HeaderText="Unit"
                            UniqueName="CssdItemUnit" SortExpression="CssdItemUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn /> 
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>