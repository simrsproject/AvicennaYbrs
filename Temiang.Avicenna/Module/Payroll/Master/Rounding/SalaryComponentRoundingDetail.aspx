<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="SalaryComponentRoundingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryComponentRoundingDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblSalaryComponentRoundingID" runat="server" Text="Component Rounding ID"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtSalaryComponentRoundingID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSalaryComponentRoundingName" runat="server" Text="Component Rounding Name"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtSalaryComponentRoundingName" runat="server" Width="300px" Height="80px" MaxLength="250" TextMode="MultiLine" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSalaryComponentRoundingName" runat="server" ErrorMessage="Salary Component Rounding Name required."
                    	ValidationGroup="entry" ControlToValidate="txtSalaryComponentRoundingName" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNominalValue" runat="server" Text="Nominal Value"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtNominalValue" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNominalValue" runat="server" ErrorMessage="Nominal Value required."
                    	ValidationGroup="entry" ControlToValidate="txtNominalValue" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNearestValue" runat="server" Text="Nearest Value"></asp:Label>
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtNearestValue" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNearestValue" runat="server" ErrorMessage="Nearest Value required."
                    	ValidationGroup="entry" ControlToValidate="txtNearestValue" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
    </table>
</asp:Content>

