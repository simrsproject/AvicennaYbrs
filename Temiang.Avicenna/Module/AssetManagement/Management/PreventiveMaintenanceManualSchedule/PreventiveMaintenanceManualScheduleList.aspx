<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="PreventiveMaintenanceManualScheduleList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.PreventiveMaintenanceManualScheduleList" Title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" PageSize="15">
        <MasterTableView DataKeyNames="AssetID,PeriodYear" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="PeriodYear" HeaderText="Year ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="PeriodYear" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="AssetID" HeaderText="Asset ID"
                    UniqueName="AssetID" SortExpression="AssetID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset Name"
                    UniqueName="AssetName" SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />    
                <telerik:GridBoundColumn HeaderStyle-Width="200px"  DataField="BrandName" HeaderText="Model Number"
                    UniqueName="BrandName" SortExpression="BrandName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SerialNumber" HeaderText="Serial Number"
                    UniqueName="SerialNumber" SortExpression="SerialNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />  
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ToServiceUnitName" HeaderText="Maintenance Unit"
                    UniqueName="ToServiceUnitName" SortExpression="ToServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />      
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ServiceUnitName" HeaderText="Asset Location"
                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
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