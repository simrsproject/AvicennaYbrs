<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="EmployeeLeaveRequestSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Leave.EmployeeLeaveRequestSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
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
                <asp:Label ID="lblRequestDate" runat="server" Text="Request Date"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtFromRequestDate" runat="server" Width="100px" />
                        </td>
                        <td>
                            to &nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtToRequestDate" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
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
