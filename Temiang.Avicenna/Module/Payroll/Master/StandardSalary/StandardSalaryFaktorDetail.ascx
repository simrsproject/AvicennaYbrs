<%@ Control Language="C#" AutoEventWireup="true" Codebehind="StandardSalaryFaktorDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.Master.StandardSalaryFaktorDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumStandardSalaryFaktor" runat="server" ValidationGroup="StandardSalaryFaktor" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="StandardSalaryFaktor"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
        <tr style="DISPLAY:none">
            <td class="label">
                <asp:Label ID="lblStandardSalaryFaktorID" runat="server" Text="Standard Salary Faktor ID"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtStandardSalaryFaktorID" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvStandardSalaryFaktorID" runat="server" ErrorMessage="Standard Salary Faktor ID required."
                    	ControlToValidate="txtStandardSalaryFaktorID" SetFocusOnError="True" ValidationGroup="StandardSalaryFaktor" Width="100%">
						<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblGradeServiceYear" runat="server" Text="Grade Year"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtGradeServiceYear" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvGradeServiceYear" runat="server" ErrorMessage="Grade Service Year required."
                    	ControlToValidate="txtGradeServiceYear" SetFocusOnError="True" ValidationGroup="StandardSalaryFaktor" Width="100%">
						<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAmountSalary" runat="server" Text="Amount Salary"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtAmountSalary" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvAmountSalary" runat="server" ErrorMessage="Amount Salary required."
                    	ControlToValidate="txtAmountSalary" SetFocusOnError="True" ValidationGroup="StandardSalaryFaktor" Width="100%">
						<asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="StandardSalaryFaktor" 
					Visible='<%# !(DataItem is GridInsertionObject) %>'>
                </asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="StandardSalaryFaktor"
                    Visible='<%# DataItem is GridInsertionObject %>'>
				</asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel">
				</asp:Button></td>
        </tr>
</table>

