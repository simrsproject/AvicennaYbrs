<%@ Control Language="C#" AutoEventWireup="true" Codebehind="EmployeeLanguageProficiencyDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeLanguageProficiencyDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeLanguageProficiency" runat="server" ValidationGroup="EmployeeLanguageProficiency" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeLanguageProficiency"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>                
				<tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeLanguageProficiencyID" runat="server" Text="Employee Language Proficiency ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtEmployeeLanguageProficiencyID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEmployeeLanguageProficiencyID" runat="server" ErrorMessage="Employee Language Proficiency ID required."
                    	        ControlToValidate="txtEmployeeLanguageProficiencyID" SetFocusOnError="True" ValidationGroup="EmployeeLanguageProficiency" Width="100%">
						        <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEvaluationDate" runat="server" Text="Evaluation Date"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadDatePicker ID="txtEvaluationDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEvaluationDate" runat="server" ErrorMessage="Evaluation Date required."
                    	        ControlToValidate="txtEvaluationDate" SetFocusOnError="True" ValidationGroup="EmployeeLanguageProficiency" Width="100%">
						        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRLanguage" runat="server" Text="Language"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRLanguage" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRLanguage" runat="server" ErrorMessage="Language required."
                    	        ControlToValidate="cboSRLanguage" SetFocusOnError="True" ValidationGroup="EmployeeLanguageProficiency" Width="100%">
						        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRConversation" runat="server" Text="Conversation"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRConversation" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRConversation" runat="server" ErrorMessage="Conversation required."
                    	        ControlToValidate="cboSRConversation" SetFocusOnError="True" ValidationGroup="EmployeeLanguageProficiency" Width="100%">
						        <asp:Image ID="Image4" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRTranslation" runat="server" Text="Translation"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRTranslation" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRTranslation" runat="server" ErrorMessage="Translation required."
                    	        ControlToValidate="cboSRTranslation" SetFocusOnError="True" ValidationGroup="EmployeeLanguageProficiency" Width="100%">
						        <asp:Image ID="Image5" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" Height="80px"/>
                    </td>
                    <td width="20px">
                        
                    </td>
                    <td>
                    </td>
                </tr>
        	
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeLanguageProficiency" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="EmployeeLanguageProficiency"
                            Visible='<%# DataItem is GridInsertionObject %>'>
				        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel">
				        </asp:Button></td>
                </tr>
            </table>
        </td>
    </tr>        
</table>
    
<table width="100%">
        
        
</table>

