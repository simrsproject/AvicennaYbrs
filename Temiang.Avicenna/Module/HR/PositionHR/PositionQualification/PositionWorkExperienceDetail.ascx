<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PositionWorkExperienceDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionWorkExperienceDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPositionWorkExperience" runat="server" ValidationGroup="PositionWorkExperience" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionWorkExperience"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPositionWorkExperienceID" runat="server" Text="Position Work Experience ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPositionWorkExperienceID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPositionWorkExperienceID" runat="server" ErrorMessage="Position Work Experience ID required."
                    	        ControlToValidate="txtPositionWorkExperienceID" SetFocusOnError="True" ValidationGroup="PositionWorkExperience" Width="100%">
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
                    	        ControlToValidate="cboSRRequirement" SetFocusOnError="True" ValidationGroup="PositionWorkExperience" Width="100%">
						        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRLineBusiness" runat="server" Text="Line Business"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRLineBusiness" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRLineBusiness" runat="server" ErrorMessage="Line Business required."
                    	        ControlToValidate="cboSRLineBusiness" SetFocusOnError="True" ValidationGroup="PositionWorkExperience" Width="100%">
						        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblYearExperience" runat="server" Text="Year Experience"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtYearExperience" runat="server" Width="300px" NumberFormat-DecimalDigits=0/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvYearExperience" runat="server" ErrorMessage="Year Experience required."
                    	        ControlToValidate="txtYearExperience" SetFocusOnError="True" ValidationGroup="PositionWorkExperience" Width="100%">
						        <asp:Image ID="Image4" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
				<tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PositionLicense" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PositionLicense"
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
                    <asp:Label ID="lblWorkExperienceNotes" runat="server" Text="Notes"></asp:Label>
			    </td>        
			    <td class="entry">
				    <telerik:RadTextBox ID="txtWorkExperienceNotes" runat="server" Width="300px" Height="80px" MaxLength="400" TextMode="MultiLine"/>
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvWorkExperienceNotes" runat="server" ErrorMessage="Work Experience Notes required."
                    	    ControlToValidate="txtWorkExperienceNotes" SetFocusOnError="True" ValidationGroup="PositionWorkExperience" Width="100%">
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