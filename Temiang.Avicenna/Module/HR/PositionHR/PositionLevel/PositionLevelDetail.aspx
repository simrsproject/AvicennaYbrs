<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="PositionLevelDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionLevelDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblPositionLevelID" runat="server" Text="Position Level ID"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtPositionLevelID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPositionLevelID" runat="server" ErrorMessage="Position Level ID required."
                    	ValidationGroup="entry" ControlToValidate="txtPositionLevelID" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPositionLevelCode" runat="server" Text="Position Level Code"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtPositionLevelCode" runat="server" Width="300px" MaxLength="10"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPositionLevelCode" runat="server" ErrorMessage="Position Level Code required."
                    	ValidationGroup="entry" ControlToValidate="txtPositionLevelCode" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPositionLevelName" runat="server" Text="Position Level Name"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtPositionLevelName" runat="server" Width="300px" MaxLength="200"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPositionLevelName" runat="server" ErrorMessage="Position Level Name required."
                    	ValidationGroup="entry" ControlToValidate="txtPositionLevelName" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblRanking" runat="server" Text="Ranking"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtRanking" runat="server" Width="100px" NumberFormat-DecimalDigits=0/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvRanking" runat="server" ErrorMessage="Ranking required."
                    	ValidationGroup="entry" ControlToValidate="txtRanking" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        
    </table>
</asp:Content>

