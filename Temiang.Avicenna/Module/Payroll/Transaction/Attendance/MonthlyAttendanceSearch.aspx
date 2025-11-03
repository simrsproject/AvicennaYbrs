<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="MonthlyAttendanceSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.Attendance.MonthlyAttendanceSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblPeriodName" runat="server" Text="Period Name" />
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterPeriodName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                    OnItemsRequested="cboPayrollPeriodID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 12 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name" />
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterEmployeeName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                Organization Unit
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterUnitName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboUnitName" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td />
        </tr>
    </table>
</asp:Content>