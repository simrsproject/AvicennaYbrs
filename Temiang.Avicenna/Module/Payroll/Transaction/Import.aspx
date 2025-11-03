<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Transaction.Import" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr runat="server" id="trPeriod">
            <td class="label">
                <asp:Label ID="lblPayrollPeriodID" runat="server" Text="Payroll Period"></asp:Label>
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
            <td width="20px" />
            <td />
        </tr>
        <tr runat="server" id="trFormat">
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Format"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboFormat" runat="server" Width="300px" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr runat="server" id="trComponent">
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Salary Component"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSalaryComponetID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSalaryComponetID_ItemDataBound"
                    OnItemsRequested="cboSalaryComponetID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "SalaryComponentCode")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">
                Excel Path File
            </td>
            <td class="entry">
                <asp:FileUpload ID="fileuploadExcel" runat="server" />
            </td>
            <td width="20" />
            <td />
        </tr>
    </table>
</asp:Content>