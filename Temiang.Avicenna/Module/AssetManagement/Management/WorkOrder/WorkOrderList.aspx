<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="WorkOrderList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.WorkOrderList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "WorkOrderDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="OrderNo" GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="OrderNo" HeaderText="Work Order No" UniqueName="OrderNo"
                    SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="OrderDate" HeaderText="Order Date" UniqueName="OrderDate"
                    SortExpression="OrderDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RequiredDate" HeaderText="Required Date" UniqueName="RequiredDate"
                    SortExpression="RequiredDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="Request Unit" UniqueName="FromServiceUnit"
                    SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ToServiceUnit" HeaderText="To Unit" UniqueName="ToServiceUnit"
                    SortExpression="ToServiceUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkType" HeaderText="Work Type"
                    UniqueName="WorkType" SortExpression="WorkType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkStatus" HeaderText="Work Status"
                    UniqueName="WorkStatus" SortExpression="WorkStatus" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="AssetName" HeaderText="Asset" UniqueName="AssetName"
                    SortExpression="AssetName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProblemDescription" HeaderText="Problem Description"
                    UniqueName="ProblemDescription" SortExpression="ProblemDescription" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsPreventiveMaintenance"
                    HeaderText="PM" UniqueName="IsPreventiveMaintenance" SortExpression="IsPreventiveMaintenance"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
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
