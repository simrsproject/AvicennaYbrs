<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="WorkingScheduleInterventionSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.WorkingScheduleInterventionSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblOrganizationUnit" runat="server" Text="Organization Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboOrganizationUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound"
                    OnItemsRequested="cboOrganizationUnitID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitCode")%>
                                &nbsp;-&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "OrganizationUnitName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPayrollPeriod" runat="server" Text="Payroll Period" Width="100px"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
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
            <td></td>
        </tr>
        <tr>
            <td class="label">Employee Name
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboEmployeeName" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
