<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="AssetItemList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.AssetItemList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(fa) {
                var url = "AssetItemDetail.aspx?md=new&fa=" + fa;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="AssetID" GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetID" HeaderText="Asset ID"
                    UniqueName="AssetID" SortExpression="AssetID" />
                <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset Name" UniqueName="AssetName"
                    SortExpression="AssetName" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BrandName" HeaderText="Model No" UniqueName="BrandName"
                    SortExpression="BrandName" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SerialNumber" HeaderText="Serial No" UniqueName="SerialNumber"
                    SortExpression="SerialNumber" />        
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssetGroupName" HeaderText="Asset Group"
                    UniqueName="AssetGroupName" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Service Unit"
                    UniqueName="ServiceUnitName" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LocationName" HeaderText="Room"
                    UniqueName="LocationName" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PurchaseOrderNumber" HeaderText="PO Received No"
                    UniqueName="PurchaseOrderNumber" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="AssetStatus" HeaderText="Status"
                    UniqueName="AssetStatus" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsFixedAsset" HeaderText="Fixed Asset"
                    UniqueName="IsFixedAsset" SortExpression="IsFixedAsset" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
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
