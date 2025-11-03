<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PositionPsychologicalDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionPsychologicalDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPositionPsychological" runat="server" ValidationGroup="PositionPsychological" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionPsychological"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
               <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPositionPsychologicalID" runat="server" Text="Position Psychological ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPositionPsychologicalID" runat="server" Width="300px" />
                    </td>                    
                    <td>
                    </td>
                </tr>
                <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPositionID" runat="server" Text="Position ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPositionID" runat="server" Width="300px" />
                    </td>                    
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRPsychological" runat="server" Text="Psychological"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRPsychological" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRPsychological" runat="server" ErrorMessage="Psychological required."
                    	        ControlToValidate="cboSRPsychological" SetFocusOnError="True" ValidationGroup="PositionPsychological" Width="100%">
						        <asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSROperandType" runat="server" Text="Operand Type"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSROperandType" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSROperandType" runat="server" ErrorMessage="Operand Type required."
                    	        ControlToValidate="cboSROperandType" SetFocusOnError="True" ValidationGroup="PositionPsychological" Width="100%">
						        <asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
			   <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="PositionPhysical" 
					        Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PositionPhysical"
                            Visible='<%# DataItem is GridInsertionObject %>'>
				        </asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel">
				        </asp:Button></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>                
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPsychologicalValue" runat="server" Text="Psychological Value"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtPsychologicalValue" runat="server" Width="300px" MaxLength="20" TextMode ="MultiLine"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPsychologicalValue" runat="server" ErrorMessage="PsychologicalValue required."
                    	        ControlToValidate="txtPsychologicalValue" SetFocusOnError="True" ValidationGroup="PositionPsychological" Width="100%">
						        <asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        
</table>
