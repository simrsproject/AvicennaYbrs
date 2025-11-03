<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="ClosingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Process.ClosingDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblPostingId" runat="server" visible="false"></asp:Label>
    <table width="100%">
        <tr>
            <td class="label">Month</td>
			<td class="entry" style="width: 80%">
				<telerik:RadComboBox ID="ddlMonth" runat="server" Width="125px" />
            </td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Year</td>
			<td class="entry"><telerik:RadTextBox ID="txtYear" runat="server" Width="125px" MaxLength="4"/></td>
            <td style="width: 20px;">
                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year Required"
                    	ValidationGroup="entry" ControlToValidate="txtYear" 
						SetFocusOnError="True" Width="100%">
							<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
				</asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr style="display:none">
            <td class="label"></td>
			<td class="entry"><asp:CheckBox ID="chkIsPostingFinal" runat="server" Text="Mark this periode as posted" /></td>
            <td style="width: 20px;"></td>
            <td></td>
        </tr>
    </table>
</asp:Content>

