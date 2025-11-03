<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CompanyFieldOfWorkProfileDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.Master.CompanyFieldOfWorkProfileDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumCompanyFieldOfWorkProfile" runat="server" ValidationGroup="CompanyFieldOfWorkProfile" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="CompanyFieldOfWorkProfile"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblCompanyFieldOfWorkProfileID" runat="server" Text="Company Field Of Work Profile ID"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtCompanyFieldOfWorkProfileID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyFieldOfWorkProfileID" runat="server" ErrorMessage="Company Field Of Work Profile ID required."
                    	ControlToValidate="txtCompanyFieldOfWorkProfileID" SetFocusOnError="True" ValidationGroup="CompanyFieldOfWorkProfile" Width="100%">
						<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCompanyFieldOfWorkProfileCode" runat="server" Text="Field Of Work Profile Code"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtCompanyFieldOfWorkProfileCode" runat="server" Width="300px" MaxLength="10"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyFieldOfWorkProfileCode" runat="server" ErrorMessage="Company Field Of Work Profile Code required."
                    	ControlToValidate="txtCompanyFieldOfWorkProfileCode" SetFocusOnError="True" ValidationGroup="CompanyFieldOfWorkProfile" Width="100%">
						<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblCompanyFieldOfWorkProfileName" runat="server" Text="Field Of Work Profile Name"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtCompanyFieldOfWorkProfileName" runat="server" Width="300px" MaxLength="200"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCompanyFieldOfWorkProfileName" runat="server" ErrorMessage="Company Field Of Work Profile Name required."
                    	ControlToValidate="txtCompanyFieldOfWorkProfileName" SetFocusOnError="True" ValidationGroup="CompanyFieldOfWorkProfile" Width="100%">
						<asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="CompanyFieldOfWorkProfile" 
					Visible='<%# !(DataItem is GridInsertionObject) %>'>
                </asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="CompanyFieldOfWorkProfile"
                    Visible='<%# DataItem is GridInsertionObject %>'>
				</asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel">
				</asp:Button></td>
        </tr>
</table>

