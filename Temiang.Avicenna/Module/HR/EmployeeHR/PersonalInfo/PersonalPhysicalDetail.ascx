<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PersonalPhysicalDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.PersonalPhysicalDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPersonalPhysical" runat="server" ValidationGroup="PersonalPhysical" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PersonalPhysical"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblPersonalPhysicalID" runat="server" Text="Personal Physical ID"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtPersonalPhysicalID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPersonalPhysicalID" runat="server" ErrorMessage="Personal Physical ID required."
                    	ControlToValidate="txtPersonalPhysicalID" SetFocusOnError="True" ValidationGroup="PersonalPhysical" Width="100%">
						<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
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
                    	ControlToValidate="cboSRPhysicalCharacteristic" SetFocusOnError="True" ValidationGroup="PersonalPhysical" Width="100%">
						<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPhysicalValue" runat="server" Text="PhysicalValue"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtPhysicalValue" runat="server" Width="300px" MaxLength="20"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPhysicalValue" runat="server" ErrorMessage="PhysicalValue required."
                    	ControlToValidate="txtPhysicalValue" SetFocusOnError="True" ValidationGroup="PersonalPhysical" Width="100%">
						<asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
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
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
	
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PersonalPhysical" 
					Visible='<%# !(DataItem is GridInsertionObject) %>'>
                </asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PersonalPhysical"
                    Visible='<%# DataItem is GridInsertionObject %>'>
				</asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel">
				</asp:Button></td>
        </tr>
</table>
