<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="CoorporateGradeSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.CoorporateGradeSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblCoorporateGradeLevel" runat="server" Text="Level" Width="100px"></asp:Label>
            </td>
            <td class="filter">

                <telerik:RadComboBox ID="cboFilterCoorporateGradeLevel" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>

                </telerik:RadComboBox>

            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtCoorporateGradeLevel" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
