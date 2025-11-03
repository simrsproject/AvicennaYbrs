<%@ Control Language="C#" AutoEventWireup="true" Codebehind="BankAccountDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.BankAccountDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumBankAccount" runat="server" ValidationGroup="BankAccount" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="BankAccount"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblBankAccountNo" runat="server" Text="Bank Account No"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtBankAccountNo" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvBankAccountNo" runat="server" ErrorMessage="Bank Account No required."
                    	ControlToValidate="txtBankAccountNo" SetFocusOnError="True" ValidationGroup="BankAccount" Width="100%">
						<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRCurrency" runat="server" Text="Currency"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadComboBox ID="cboSRCurrency" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRCurrency" runat="server" ErrorMessage="Currency required."
                    	ControlToValidate="cboSRCurrency" SetFocusOnError="True" ValidationGroup="BankAccount" Width="100%">
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
				<telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="300" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ErrorMessage="Notes required."
                    	ControlToValidate="txtNotes" SetFocusOnError="True" ValidationGroup="BankAccount" Width="100%">
						<asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
			</td>        
			<td class="entry">
				<asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" />
            </td>
            <td width="20px">
           </td>
            <td>
            </td>
        </tr>
	
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="BankAccount" 
					Visible='<%# !(DataItem is GridInsertionObject) %>'>
                </asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="BankAccount"
                    Visible='<%# DataItem is GridInsertionObject %>'>
				</asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel">
				</asp:Button></td>
        </tr>
</table>

