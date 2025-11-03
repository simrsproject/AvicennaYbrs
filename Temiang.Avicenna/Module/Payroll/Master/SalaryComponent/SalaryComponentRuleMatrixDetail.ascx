<%@ Control Language="C#" AutoEventWireup="true" Codebehind="SalaryComponentRuleMatrixDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.Master.SalaryComponentRuleMatrixDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSalaryComponentRuleMatrix" runat="server" ValidationGroup="SalaryComponentRuleMatrix" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SalaryComponentRuleMatrix"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
    <tr style="DISPLAY:none">
        <td class="label">
            <asp:Label ID="lblSalaryComponentRuleMatrixID" runat="server" Text="Salary Component Rule Matrix ID"></asp:Label>
		</td>        
		<td class="entry">
			<telerik:RadNumericTextBox ID="txtSalaryComponentRuleMatrixID" runat="server" Width="300px" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblSalaryComponetID" runat="server" Text="Salary Componet Name"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSalaryComponetID" runat="server" Width="304px" 
                EnableLoadOnDemand="true" MarkFirstMatch="true" HighlightTemplatedItems="true" 
                AutoPostBack="false" OnItemDataBound="cboSalaryComponetID_ItemDataBound"
                OnItemsRequested="cboSalaryComponetID_ItemsRequested">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentCode")%>
                    &nbsp;-&nbsp;
                    <%# DataBinder.Eval(Container.DataItem, "SalaryComponentName")%>
                </ItemTemplate>
                <FooterTemplate>
                    Note : Show max 10 items
                </FooterTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
           <asp:RequiredFieldValidator ID="rfvSalaryComponentID" runat="server" ErrorMessage="Salary Component Name required."
                	ControlToValidate="cboSalaryComponetID" SetFocusOnError="True" ValidationGroup="SalaryComponentRuleMatrix" Width="100%">
					<asp:Image ID="Image2" runat="server" SkinID="rfvImage"/>
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
			<telerik:RadComboBox ID="cboSROperandType" runat="server" Width="304px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSROperandType" runat="server" ErrorMessage="Operand Type required."
                	ControlToValidate="cboSROperandType" SetFocusOnError="True" ValidationGroup="SalaryComponentRuleMatrix" Width="100%">
					<asp:Image ID="Image4" runat="server" SkinID="rfvImage"/>
				</asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>        

    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SalaryComponentRuleMatrix" 
				Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="SalaryComponentRuleMatrix"
                Visible='<%# DataItem is GridInsertionObject %>'>
			</asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel">
			</asp:Button></td>
    </tr>
</table>

