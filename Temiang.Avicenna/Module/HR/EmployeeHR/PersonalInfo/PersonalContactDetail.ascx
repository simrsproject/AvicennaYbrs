<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PersonalContactDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalContactDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalContact" runat="server" ValidationGroup="PersonalContact" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalContact"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblPersonalContactID" runat="server" Text="Personal Contact ID"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtPersonalContactID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPersonalContactID" runat="server" ErrorMessage="Personal Contact ID required."
                    	ControlToValidate="txtPersonalContactID" SetFocusOnError="True" ValidationGroup="PersonalContact" Width="100%">
						<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRContactType" runat="server" Text="Contact Type"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadComboBox ID="cboSRContactType" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRContactType" runat="server" ErrorMessage="Contact Type required."
                    	ControlToValidate="cboSRContactType" SetFocusOnError="True" ValidationGroup="PersonalContact" Width="100%">
						<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblContactValue" runat="server" Text="Contact Value"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtContactValue" runat="server" Width="300px" MaxLength="50"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvContactValue" runat="server" ErrorMessage="ContactValue required."
                    	ControlToValidate="txtContactValue" SetFocusOnError="True" ValidationGroup="PersonalContact" Width="100%">
						<asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalContact" 
					Visible='<%# !(DataItem is GridInsertionObject) %>'>
                </asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PersonalContact"
                    Visible='<%# DataItem is GridInsertionObject %>'>
				</asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel">
				</asp:Button></td>
        </tr>
</table>

