<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AssetGroupList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.AssetGroupList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="AssetGroupId">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AssetGroupId" HeaderText="Group ID"
                    UniqueName="AssetGroupId" SortExpression="AssetGroupId">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group Name" UniqueName="GroupName"
                    SortExpression="GroupName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="COA Asset"
                    UniqueName="ChartOfAccountName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CoaAssetDepreciation" HeaderText="COA Asset Depreciation"
                    UniqueName="CoaAssetDepreciationName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CoaCostOfDepreciation" HeaderText="COA Cost of Depreciation"
                    UniqueName="CoaCostOfDepreciationName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CoaCostOfDestruction" HeaderText="COA Cost of Destruction"
                    UniqueName="CoaCostOfDestructionName">
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
