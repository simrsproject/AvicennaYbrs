<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="AttedanceMatrixDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.AttedanceMatrixDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblAttedanceMatrixID" runat="server" Text="Attedance Matrix ID"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtAttedanceMatrixID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvAttedanceMatrixID" runat="server" ErrorMessage="Attedance Matrix ID required."
                    	ValidationGroup="entry" ControlToValidate="txtAttedanceMatrixID" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAttedanceMatrixName" runat="server" Text="Attedance Matrix Name"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtAttedanceMatrixName" runat="server" Width="300px" MaxLength="200" TextMode="MultiLine"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvAttedanceMatrixName" runat="server" ErrorMessage="Attedance Matrix Name required."
                    	ValidationGroup="entry" ControlToValidate="txtAttedanceMatrixName" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAttedanceMatrixFieldt" runat="server" Text="Attedance Matrix Field"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtAttedanceMatrixFieldt" runat="server" Width="300px" MaxLength="100"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvAttedanceMatrixFieldt" runat="server" ErrorMessage="Attedance Matrix Field required."
                    	ValidationGroup="entry" ControlToValidate="txtAttedanceMatrixFieldt" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>

