<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="PrinterDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.PrinterManagement.PrinterDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblPrinterID" runat="server" Text="Printer ID"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtPrinterID" runat="server" Width="300px" MaxLength="3"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPrinterID" runat="server" ErrorMessage="Printer ID required."
                    	ValidationGroup="entry" ControlToValidate="txtPrinterID" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPrinterName" runat="server" Text="Printer Name"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtPrinterName" runat="server" Width="300px" MaxLength="500"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPrinterName" runat="server" ErrorMessage="Printer Name required."
                    	ValidationGroup="entry" ControlToValidate="txtPrinterName" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
                <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Printer Location Host"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtPrinterLocationHost" runat="server" Width="300px" MaxLength="500"/>
            </td>
            <td width="20px">

            </td>
            <td>
            </td>
        </tr>
                <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Printer Manager Host"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtPrinterManagerHost" runat="server" Width="300px" MaxLength="500"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Printer Manager Host required."
                    	ValidationGroup="entry" ControlToValidate="txtPrinterManagerHost" 
						SetFocusOnError="True" Width="100%">
							<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ErrorMessage="Notes required."
                    	ValidationGroup="entry" ControlToValidate="txtNotes" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>

    </table>
</asp:Content>

