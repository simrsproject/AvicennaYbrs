<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    Codebehind="AttedanceMatrixSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.AttedanceMatrixSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label"> 
                <asp:Label ID="lblAttedanceMatrixName" runat="server" Text="Attedance Matrix Name" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterAttedanceMatrixName" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtAttedanceMatrixName" runat="server" Width="300px" MaxLength="200"/>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"> 
                <asp:Label ID="lblAttedanceMatrixFieldt" runat="server" Text="Attedance Matrix Field" Width="100px"></asp:Label>
			</td>
            <td class="filter">
			
                <telerik:RadComboBox ID="cboFilterAttedanceMatrixFieldt" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </telerik:RadComboBox>
				
            </td>
            <td class="entry">
				<telerik:RadTextBox ID="txtAttedanceMatrixFieldt" runat="server" Width="300px" MaxLength="100"/>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>

