<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ItemTariffRequestProcessSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemTariffRequestProcessSearch" %>
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
                <asp:Label ID="lblStartDate" runat="server" Text="Request Date"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRTariffType" runat="server" Text="Tariff Type"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRTariffType" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
            </td>
            <td class="filter"></td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label"> 
            </td>
            <td class="filter">
				
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" />
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>