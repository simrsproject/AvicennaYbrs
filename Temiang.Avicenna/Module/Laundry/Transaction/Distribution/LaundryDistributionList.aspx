<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="LaundryDistributionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LaundryDistributionList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" AllowPaging="true" PageSize="15"
        AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="DistributionNo" GroupLoadMode="Client">
            <Columns>
                <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="DistributionNo"
                    DataNavigateUrlFields="RUrl" HeaderText="Distribution No" UniqueName="DistributionNo"
                    SortExpression="DistributionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DistributionDate" HeaderText="Date"
                    UniqueName="DistributionDate" SortExpression="DistributionDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DistributionTime" HeaderText="Time"
                    UniqueName="DistributionTime" SortExpression="DistributionTime" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ToServiceUnitName" HeaderText="To Unit" UniqueName="ToServiceUnitName"
                    SortExpression="ToServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="HandedByUserName" HeaderText="Handed By"
                    UniqueName="HandedByUserName" SortExpression="HandedByUserName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ReceivedBy"
                    HeaderText="Received By" UniqueName="ReceivedBy" SortExpression="ReceivedBy"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="DistributionNo, SeqNo" Name="grdDetail"
                    AutoGenerateColumns="False" AllowPaging="true" PageSize="15">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="SeqNo" HeaderText="Seq No"
                            UniqueName="SeqNo" SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ItemUnit" HeaderText="Unit"
                            UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
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