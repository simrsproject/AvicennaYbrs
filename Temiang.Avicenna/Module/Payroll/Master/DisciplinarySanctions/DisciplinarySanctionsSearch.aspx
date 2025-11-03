<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="DisciplinarySanctionsSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.DisciplinarySanctionsSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type" Width="100px"></asp:Label>
			</td>
            <td class="filter">
            </td>
            <td class="entry">
				<telerik:RadComboBox ID="cboSREmploymentType" runat="server" Width="300px" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>