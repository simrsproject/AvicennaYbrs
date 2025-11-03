<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="InventoryStockAdjustmentTypeCoaSetting.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting.InventoryStockAdjustmentTypeCoaSetting" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Expense" Selected="True" PageViewID="pgLeft" />
                        <telerik:RadTab runat="server" Text="Inventory" PageViewID="pgRight" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
                    BorderColor="Gray">
                    <telerik:RadPageView ID="pgLeft" runat="server">
                        <telerik:RadGrid ID="grdLeft" runat="server" OnNeedDataSource="grdLeft_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                            PageSize="15">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="FromLocationID, FromServiceUnitID, ItemID"
                                GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="LocationName" HeaderText="Location " />
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="LocationName" SortOrder="None" />
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="AccCode" HeaderText="Account Code"
                                        UniqueName="AccCode" SortExpression="AccCode" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="AccName" HeaderText="Account Name" UniqueName="AccName"
                                        SortExpression="AccName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SublName" HeaderText="Subleger"
                                        UniqueName="SublName" SortExpression="SublName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
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
                        <telerik:RadGrid ID="grdRight" runat="server" OnNeedDataSource="grdRight_NeedDataSource"
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                            PageSize="15">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView CommandItemDisplay="None" DataKeyNames="FromLocationID, FromServiceUnitID, ItemID"
                                GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="LocationName" HeaderText="Location " />
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="LocationName" SortOrder="None" />
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="AccCode" HeaderText="Account Code"
                                        UniqueName="AccCode" SortExpression="AccCode" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridBoundColumn DataField="AccName" HeaderText="Account Name" UniqueName="AccName"
                                        SortExpression="AccName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SublName" HeaderText="Subleger"
                                        UniqueName="SublName" SortExpression="SublName" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
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