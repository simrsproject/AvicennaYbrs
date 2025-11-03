<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="EmployeePeriodicSalarySearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.EmployeePeriodicSalarySearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterEmployeeNumber" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFirstName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFirstName" runat="server" Width="300px" MaxLength="500" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboLastName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLastName" runat="server" Width="300px" MaxLength="500" />
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSalaryComponent" runat="server" Text="Salary Component" Width="100px"></asp:Label>
            </td>
            <td class="filter" />
            <td class="entry">
                <telerik:RadComboBox ID="cboSalaryComponent" runat="server" Width="300px" Filter="Contains" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPayrollPeriod" runat="server" Text="Payroll Period" Width="100px"></asp:Label>
            </td>
            <td class="filter" />
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
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td />
        </tr>
    </table>
</asp:Content>
