<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="WageStructureAndScaleSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.WageStructureAndScaleSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblWageStructureAndScaleType" runat="server" Text="Wage Structure And Scale Type"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterWageStructureAndScaleType" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRWageStructureAndScaleType" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
