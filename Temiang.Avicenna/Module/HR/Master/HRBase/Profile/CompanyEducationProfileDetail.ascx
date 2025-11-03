<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CompanyEducationProfileDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.Master.CompanyEducationProfileDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCompanyEducationProfile" runat="server" ValidationGroup="CompanyEducationProfile" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CompanyEducationProfile"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblCompanyEducationProfileID" runat="server" Text="Company Education Profile ID"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtCompanyEducationProfileID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyEducationProfileID" runat="server" ErrorMessage="Company Education Profile ID required."
                    	ControlToValidate="txtCompanyEducationProfileID" SetFocusOnError="True" ValidationGroup="CompanyEducationProfile" Width="100%">
						<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCompanyEducationProfileCode" runat="server" Text="Education Profile Code"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtCompanyEducationProfileCode" runat="server" Width="300px" MaxLength="20"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyEducationProfileCode" runat="server" ErrorMessage="Company Education Profile Code required."
                    	ControlToValidate="txtCompanyEducationProfileCode" SetFocusOnError="True" ValidationGroup="CompanyEducationProfile" Width="100%">
						<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCompanyEducationProfileName" runat="server" Text="Education Profile Name"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtCompanyEducationProfileName" runat="server" Width="300px" MaxLength="200"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyEducationProfileName" runat="server" ErrorMessage="Company Education Profile Name required."
                    	ControlToValidate="txtCompanyEducationProfileName" SetFocusOnError="True" ValidationGroup="CompanyEducationProfile" Width="100%">
						<asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CompanyEducationProfile" 
					Visible='<%# !(DataItem is GridInsertionObject) %>'>
                </asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="CompanyEducationProfile"
                    Visible='<%# DataItem is GridInsertionObject %>'>
				</asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel">
				</asp:Button></td>
        </tr>
</table>

