<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="SalaryTemplateSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryTemplateSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblSalaryTemplateID" runat="server" Text="Salary Template Code" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterSalaryTemplateID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtSalaryTemplateID" runat="server" Width="300px" MaxLength="10"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
                <asp:Label ID="lblSalaryTemplateName" runat="server" Text="Salary Template Name" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterSalaryTemplateName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtSalaryTemplateName" runat="server" Width="300px" MaxLength="50"/>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>

