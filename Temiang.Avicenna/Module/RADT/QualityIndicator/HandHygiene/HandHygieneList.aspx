<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="HandHygieneList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.HandHygieneList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo">
                    <HeaderStyle HorizontalAlign="Center" Width="135px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                    UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="StartTime" HeaderText="Start Time"
                    UniqueName="StartTime" SortExpression="StartTime" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="EndTime" HeaderText="End Time"
                    UniqueName="EndTime" SortExpression="EndTime" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="SessionLength" HeaderText="Session Length (Minutes)"
                    UniqueName="SessionLength" SortExpression="SessionLength" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DecimalDigits="0"/>
                <telerik:GridBoundColumn DataField="ObserverName" HeaderText="Observer Name" UniqueName="ObserverName"
                    SortExpression="ObserverName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeID" HeaderText="Employee ID"
                    UniqueName="EmployeeID" SortExpression="EmployeeID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ProfessionType" HeaderText="Profession Type" UniqueName="ProfessionType"
                    SortExpression="ProfessionType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DepartmentName" HeaderText="Department" UniqueName="DepartmentName"
                    SortExpression="DepartmentName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DivisionName" HeaderText="Division" UniqueName="DivisionName"
                    SortExpression="DivisionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SubDivisionName" HeaderText="Sub Division" UniqueName="SubDivisionName"
                    SortExpression="SubDivisionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVoid" HeaderText="Void"
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