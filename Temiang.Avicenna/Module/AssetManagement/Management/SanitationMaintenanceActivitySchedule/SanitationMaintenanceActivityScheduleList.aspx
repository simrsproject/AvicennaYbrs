<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="SanitationMaintenanceActivityScheduleList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationMaintenanceActivityScheduleList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="SRWorkTradeItem,ServiceUnitID,PeriodYear" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="PeriodYear" HeaderText="Year ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="PeriodYear" SortOrder="Descending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitID" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="SRWorkTradeItem" HeaderText="ID"
                    UniqueName="SRWorkTradeItem" SortExpression="SRWorkTradeItem" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="WorkTradeItemName" HeaderText="Work Trade Item"
                    UniqueName="WorkTradeItemName" SortExpression="WorkTradeItemName" HeaderStyle-HorizontalAlign="Left"
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
</asp:Content>