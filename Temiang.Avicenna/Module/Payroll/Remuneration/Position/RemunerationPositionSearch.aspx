<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RemunerationPositionSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.RemunerationPosition.RemunerationPositionSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Valid From" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                        </td>
                        <td>
                            to &nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                    OnItemsRequested="cboPersonID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitName" runat="server" Text="Service Unit"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnitName" runat="server" Width="304px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitName_ItemDataBound"
                    OnItemsRequested="cboServiceUnitName_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboStatus" runat="server" Width="304px">
                </telerik:RadComboBox>
            </td>
            <td />
        </tr>
    </table>
</asp:Content>