<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="AutoNumberSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Setting.AppAutoNumberSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblSRAutoNumber" runat="server" Text="Auto Number ID" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterSRAutoNumber" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtSRAutoNumber" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>

