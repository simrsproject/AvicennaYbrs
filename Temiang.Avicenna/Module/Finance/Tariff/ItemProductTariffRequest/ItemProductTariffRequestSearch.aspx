<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="ItemProductTariffRequestSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemProductTariffRequestSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblTariffRequestNo" runat="server" Text="Tariff Request No" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterTariffRequestNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtTariffRequestNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
			</td>
            <td class="filter">
				
            </td>
            <td class="entry">
				<asp:CheckBox ID="chkIsApproved" runat="server" Text="Is Approval" />
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>
