<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashTransactionListSearch.aspx.cs" MasterPageFile="~/MasterPage/MasterDialog.Master"
Inherits="Temiang.Avicenna.Module.Finance.Master.CashTransactionListSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblListID" runat="server" Text="ID" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterListID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtListID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
                <asp:Label ID="lblDescription" runat="server" Text="Description" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterDescription" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>
