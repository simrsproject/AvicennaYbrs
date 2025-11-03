<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ReOrderPoBasedOnPrConfirm.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.ReOrderPoBasedOnPrConfirm" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnItemCreated="grdList_ItemCreated"
        OnNeedDataSource="grdList_NeedDataSource" AllowPaging="True" PageSize="15" AllowSorting="true" AllowFilteringByColumn="True"
        ShowStatusBar="true">
        <MasterTableView DataKeyNames="RowID" ClientDataKeyNames="RowID" AutoGenerateColumns="false">
            <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="ItemName" FieldName="ItemName" ></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="ItemName" SortOrder="Ascending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px" AllowFiltering="False">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"IsSelect").ToInt()==1%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="0px" DataField="RowID" HeaderText="Row"
                    UniqueName="RowID" SortExpression="RowID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"  />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                                         UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                                         ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="FromUnit" HeaderText="From Unit" UniqueName="FromUnit"
                    SortExpression="FromUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" AllowFiltering="False"/>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="FromBalance" HeaderText="Balance"
                    UniqueName="FromBalance" SortExpression="FromBalance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" AllowFiltering="False"/>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="ToBalance" HeaderText="PU. Balance"
                    UniqueName="ToBalance" SortExpression="ToBalance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" AllowFiltering="False"/>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="QtyOutstanding" HeaderText="PR Bal"
                    UniqueName="QtyOutstanding" SortExpression="QtyOutstanding" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" AllowFiltering="False"/>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" AllowFiltering="False"/>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="Unit" HeaderText="Purchase Unit"
                    UniqueName="Unit" SortExpression="Unit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" AllowFiltering="False"/>
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Qty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" AllowFiltering="False">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="40px" DbValue='<%#DataBinder.Eval(Container.DataItem,"QtyOrder")%>'
                            NumberFormat-DecimalDigits="0" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="90px" HeaderText="Item Unit" UniqueName="ItemUnit"
                    HeaderStyle-HorizontalAlign="left" AllowFiltering="False">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboItemUnitSelected" runat="server" Width="100%">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Supplier" UniqueName="SupplierID" HeaderStyle-HorizontalAlign="left" AllowFiltering="False">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboSupplierID" runat="server" Width="100%" EnableLoadOnDemand="true"
                            OnItemsRequested="cboSupplierID_ItemsRequested" OnItemDataBound="cboSupplierID_ItemDataBound">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
    
</asp:Content>
