<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="SalaryComponentRoundingSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryComponentRoundingSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblSalaryComponentRoundingName" runat="server" Text="Salary Component Rounding Name" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterSalaryComponentRoundingName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtSalaryComponentRoundingName" runat="server" Width="300px" MaxLength="250"/>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>

