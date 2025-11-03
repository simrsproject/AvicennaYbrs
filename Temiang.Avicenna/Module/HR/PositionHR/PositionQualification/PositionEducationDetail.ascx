<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PositionEducationDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionEducationDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPositionEducation" runat="server" ValidationGroup="PositionEducation" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionEducation"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
               <tr style="DISPLAY:none">
                <td class="label">
                    <asp:Label ID="lblPositionEducationID" runat="server" Text="Position Education ID"></asp:Label>
		        </td>        
		        <td class="entry">
			        <telerik:RadNumericTextBox ID="txtPositionEducationID" runat="server" Width="300px" />
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvPositionEducationID" runat="server" ErrorMessage="Position Education ID required."
                	        ControlToValidate="txtPositionEducationID" SetFocusOnError="True" ValidationGroup="PositionEducation" Width="100%">
					        <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
				        </asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblSRRequirement" runat="server" Text="Requirement"></asp:Label>
		        </td>        
		        <td class="entry">
			        <telerik:RadComboBox ID="cboSRRequirement" runat="server" Width="300px" />
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvSRRequirement" runat="server" ErrorMessage="Requirement required."
                	        ControlToValidate="cboSRRequirement" SetFocusOnError="True" ValidationGroup="PositionEducation" Width="100%">
					        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
				        </asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
		        </td>        
		        <td class="entry">
			        <telerik:RadComboBox ID="cboSREducationLevel" runat="server" Width="300px" />
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvSREducationLevel" runat="server" ErrorMessage="Education Level required."
                	        ControlToValidate="cboSREducationLevel" SetFocusOnError="True" ValidationGroup="PositionEducation" Width="100%">
					        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
				        </asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblSREducationField" runat="server" Text="Education Field"></asp:Label>
		        </td>        
		        <td class="entry">
			        <telerik:RadComboBox ID="cboSREducationField" runat="server" Width="300px" />
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvSREducationField" runat="server" ErrorMessage="Education Field required."
                	        ControlToValidate="cboSREducationField" SetFocusOnError="True" ValidationGroup="PositionEducation" Width="100%">
					        <asp:Image ID="Image4" runat="server" SkinID="rfvImage"/>
				        </asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
            </tr> 
			   <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PositionPhysical" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PositionPhysical"
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
                        <asp:Label ID="lblEducationNotes" runat="server" Text="Education Notes"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtEducationNotes" runat="server" Width="300px" Height="80px" MaxLength="200" TextMode="MultiLine"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEducationNotes" runat="server" ErrorMessage="Education Notes required."
                    	        ControlToValidate="txtEducationNotes" SetFocusOnError="True" ValidationGroup="PositionEducation" Width="100%">
						        <asp:Image ID="Image5" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        
</table>
