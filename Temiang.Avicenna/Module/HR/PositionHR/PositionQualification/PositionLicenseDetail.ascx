<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PositionLicenseDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionLicenseDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPositionLicense" runat="server" ValidationGroup="PositionLicense" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionLicense"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPositionLicenseID" runat="server" Text="Position License ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPositionLicenseID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPositionLicenseID" runat="server" ErrorMessage="Position License ID required."
                    	        ControlToValidate="txtPositionLicenseID" SetFocusOnError="True" ValidationGroup="PositionLicense" Width="100%">
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
                    	        ControlToValidate="cboSRRequirement" SetFocusOnError="True" ValidationGroup="PositionLicense" Width="100%">
						        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRLicenseType" runat="server" Text="License Type"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRLicenseType" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRLicenseType" runat="server" ErrorMessage="License Type required."
                    	        ControlToValidate="cboSRLicenseType" SetFocusOnError="True" ValidationGroup="PositionLicense" Width="100%">
						        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
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
                        <asp:Label ID="lblLicenseNotes" runat="server" Text="License Notes"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtLicenseNotes" runat="server" Width="300px" Height="80px" MaxLength="400" TextMode="MultiLine"/>
                    </td>
                    
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        
</table>