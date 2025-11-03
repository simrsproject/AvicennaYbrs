<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LinenItemsExterminationSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LinenItemsExterminationSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
            </td>
            <td class="searchfilter">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateFrom" runat="server" Width="100px" />
                        </td>
                        <td>
                            &nbsp;-&nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Status" Width="100px"></asp:Label>
            </td>
            <td class="searchfilter">
                <telerik:RadComboBox ID="cboStatus" runat="server" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>