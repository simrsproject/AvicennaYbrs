<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="TransactionCodeDetail.aspx.cs" Inherits="Temiang.Avicenna.ControlPanel.Setting.TransactionCodeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSRTransactionCode" runat="server" Text="Transaction Code"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadComboBox ID="cboSRTransactionCode" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRTransactionCode" runat="server" ErrorMessage="Transaction Code required."
                    	ValidationGroup="entry" ControlToValidate="cboSRTransactionCode" 
						SetFocusOnError="True" Width="100%">
							<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRAutoNumber" runat="server" Text="Auto Number"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadComboBox ID="cboSRAutoNumber" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRAutoNumber" runat="server" ErrorMessage="Auto Number required."
                    	ValidationGroup="entry" ControlToValidate="cboSRAutoNumber" 
						SetFocusOnError="True" Width="100%">
							<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>
