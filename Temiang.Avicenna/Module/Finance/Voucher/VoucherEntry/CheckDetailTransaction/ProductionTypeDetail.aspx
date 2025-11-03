<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductionTypeDetail.aspx.cs" MasterPageFile="~/MasterPage/MasterDialog.Master"
    Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckDetailTransaction.ProductionTypeDetail" %>


<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Production" Selected="True" PageViewID="pgLeft" />
            <telerik:RadTab runat="server" Text="Materials" PageViewID="pgRight" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgLeft" runat="server">
            <telerik:RadGrid ID="gridProd" runat="server" OnNeedDataSource="gridProd_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="15">
                <HeaderContextMenu>
                    
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ProductionNo" HeaderText="Production No"
                            UniqueName="ProductionNo" SortExpression="ProductionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn  DataField="ServiceUnitName" HeaderStyle-Width="150px" ItemStyle-Wrap="true"
                            HeaderText="Service Unit" UniqueName="ServiceUnitName" SortExpression="ServiceUnitName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn  DataField="LocationName" HeaderStyle-Width="180px" ItemStyle-Wrap="true"
                            HeaderText="Location" UniqueName="LocationName" SortExpression="LocationName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn  DataField="ItemName"
                            HeaderText="Item Name" UniqueName="ItemName" SortExpression="ItemName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />                            
                        <telerik:GridNumericColumn DataField="Qty" HeaderText="Quantity" HeaderStyle-Width="80px"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="Unit" SortExpression="Unit" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center"  FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CostPrice" HeaderText="Cost"
                            UniqueName="CostPrice" SortExpression="CostPrice" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"  FooterStyle-HorizontalAlign="Right" 
                            Aggregate="Count" FooterAggregateFormatString="Total :"/>                                                        
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Total" HeaderText="Total"
                            UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum"
                            FooterAggregateFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                    
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRight" runat="server">
            <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="15">
                <HeaderContextMenu>
                    
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ProductionNo" HeaderText="Production No"
                            UniqueName="ProductionNo" SortExpression="ProductionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn  DataField="ServiceUnitName" HeaderStyle-Width="150px" ItemStyle-Wrap="true"
                            HeaderText="Service Unit" UniqueName="ServiceUnitName" SortExpression="ServiceUnitName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn  DataField="LocationName" HeaderStyle-Width="180px" ItemStyle-Wrap="true"
                            HeaderText="Location" UniqueName="LocationName" SortExpression="LocationName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn  DataField="ItemName"
                            HeaderText="Item Name" UniqueName="ItemName" SortExpression="ItemName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />                            
                        <telerik:GridNumericColumn DataField="Qty" HeaderText="Quantity" HeaderStyle-Width="80px"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                            UniqueName="Unit" SortExpression="Unit" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center"  FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CostPrice" HeaderText="Cost"
                            UniqueName="CostPrice" SortExpression="CostPrice" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"  FooterStyle-HorizontalAlign="Right" 
                            Aggregate="Count" FooterAggregateFormatString="Total :"/>                                                        
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Total" HeaderText="Total"
                            UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum"
                            FooterAggregateFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                    
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>

