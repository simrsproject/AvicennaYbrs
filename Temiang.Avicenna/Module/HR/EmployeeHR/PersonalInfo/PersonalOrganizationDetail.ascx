<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PersonalOrganizationDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalOrganizationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalOrganization" runat="server" ValidationGroup="PersonalOrganization" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalOrganization"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>                
				<tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPersonalOrganizationID" runat="server" Text="Personal Organization ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPersonalOrganizationID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonalOrganizationID" runat="server" ErrorMessage="Personal Organization ID required."
                    	        ControlToValidate="txtPersonalOrganizationID" SetFocusOnError="True" ValidationGroup="PersonalOrganization" Width="100%">
						        <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOrganizationName" runat="server" Text="Organization Name"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtOrganizationName" runat="server" Width="300px" MaxLength="40"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvOrganizationName" runat="server" ErrorMessage="Organization Name required."
                    	        ControlToValidate="txtOrganizationName" SetFocusOnError="True" ValidationGroup="PersonalOrganization" Width="100%">
						        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtLocation" runat="server" Width="300px" MaxLength="60"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Location required."
                    	        ControlToValidate="txtLocation" SetFocusOnError="True" ValidationGroup="PersonalOrganization" Width="100%">
						        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSROrganizationType" runat="server" Text="Organization Type"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSROrganizationType" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSROrganizationType" runat="server" ErrorMessage="Organization Type required."
                    	        ControlToValidate="cboSROrganizationType" SetFocusOnError="True" ValidationGroup="PersonalOrganization" Width="100%">
						        <asp:Image ID="Image4" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSROrganizationRole" runat="server" Text="Organization Role"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSROrganizationRole" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSROrganizationRole" runat="server" ErrorMessage="Organization Role required."
                    	        ControlToValidate="cboSROrganizationRole" SetFocusOnError="True" ValidationGroup="PersonalOrganization" Width="100%">
						        <asp:Image ID="Image5" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalOrganization" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PersonalOrganization"
                            Visible='<%# DataItem is GridInsertionObject %>'>
				        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel">
				        </asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999"/>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900" MaxDate="12/31/2999"/>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNote" runat="server" Text="Note"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" Height="80px" MaxLength="500" TextMode="MultiLine"/>
                    </td>
                    <td width="20px">
                       
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>        
</table>

<table width="100%">
        
        
	
        
</table>

