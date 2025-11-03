<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="PkpSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.TaxRegulation.PkpSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
			</td>
            <td class="filter">
				Valid From
            </td>
            <td class="entry">
				<telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999"/>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>

