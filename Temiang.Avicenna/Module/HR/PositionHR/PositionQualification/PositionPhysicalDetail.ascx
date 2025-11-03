<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PositionPhysicalDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionPhysicalDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPositionPhysical" runat="server" ValidationGroup="PositionPhysical" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PositionPhysical"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
               <tr style="DISPLAY:none">
                    <td class="label">
                        <asp:Label ID="lblPositionPhysicalID" runat="server" Text="Position Physical ID"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadNumericTextBox ID="txtPositionPhysicalID" runat="server" Width="300px" />
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
                        <asp:Label ID="lblSRPhysicalCharacteristic" runat="server" Text="Physical Characteristic"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRPhysicalCharacteristic" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRPhysicalCharacteristic" runat="server" ErrorMessage="Physical Characteristic required."
                    	        ControlToValidate="cboSRPhysicalCharacteristic" SetFocusOnError="True" ValidationGroup="PositionPhysical" Width="100%">
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
                    	        ControlToValidate="cboSROperandType" SetFocusOnError="True" ValidationGroup="PositionPhysical" Width="100%">
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
                        <asp:Label ID="lblPhysicalValue" runat="server" Text="Physical Value"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadTextBox ID="txtPhysicalValue" runat="server" Width="300px" MaxLength="20"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPhysicalValue" runat="server" ErrorMessage="PhysicalValue required."
                    	        ControlToValidate="txtPhysicalValue" SetFocusOnError="True" ValidationGroup="PositionPhysical" Width="100%">
						        <asp:Image ID="Image5" runat="server" SkinID="rfvImage"/>
					        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRMeasurementCode" runat="server" Text="Measurement Code"></asp:Label>
			        </td>        
			        <td class="entry">
				        <telerik:RadComboBox ID="cboSRMeasurementCode" runat="server" Width="300px" />
                    </td>
                   
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        
</table>