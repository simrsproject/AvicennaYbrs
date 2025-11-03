<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="VoucherCodeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.VoucherCodeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                Journal Code
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtJournalCode" runat="server" Width="300px" MaxLength="10"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvJournalCode" runat="server" ErrorMessage="Journal Code required."
                    	ValidationGroup="entry" ControlToValidate="txtJournalCode" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Description
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="50"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Description required."
                    	ValidationGroup="entry" ControlToValidate="txtDescription" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
	    <tr>
            <td class="label">
                Current Number
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtCurrentNumber" runat="server" Width="300px" NumberFormat-DecimalDigits="0" Value="1" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCurrentNumber" runat="server" ErrorMessage="Current Number required."
                    	ValidationGroup="entry" ControlToValidate="txtCurrentNumber" 
						SetFocusOnError="True" Width="100%">
							<asp:Image runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Number Format
			</td>
			<td class="entry">
				<telerik:RadTextBox ID="txtNumberFormat" runat="server" Width="300px" MaxLength="15"/>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNumberFormat" runat="server" ErrorMessage="Number Format required."
                    	ValidationGroup="entry" ControlToValidate="txtNumberFormat" 
						SetFocusOnError="True" Width="100%">
							<asp:Image  runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Sample
			</td>
			<td class="entry">
				<asp:label runat="server" id="lblNumberFormatSample" text="" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Number Seed
			</td>
			<td class="entry">
				<telerik:RadNumericTextBox ID="txtNumberSeed" runat="server" Width="300px" Value="1" NumberFormat-DecimalDigits="0" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvNumberSeed" runat="server" ErrorMessage="Number Seed required."
                    	ValidationGroup="entry" ControlToValidate="txtNumberSeed" 
						SetFocusOnError="True" Width="100%">
							<asp:Image  runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Bank
			</td>
			<td class="entry">
				<telerik:RadComboBox ID="txtBankAccount" runat="server" Width="300px" AutoPostBack="true" 
				    HighlightTemplatedItems="true" DataTextField="AccountName" DataValueField="BankId" NoWrap="true">
				    <ItemTemplate>
                        <div>
                            <b>
                                <%# Eval("BankName")%></b><%# Eval("NoRek", " - {0}") %>
                        </div>
                    </ItemTemplate>
				    
				</telerik:RadComboBox>
            </td>
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                Cash Type
			</td>
			<td class="entry">
				<telerik:RadComboBox ID="cboCashType" runat="server" Width="300px" />				    
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvCashType" runat="server" ErrorMessage="Cash Type required."
                    	ValidationGroup="entry" ControlToValidate="cboCashType" 
						SetFocusOnError="True" Width="100%">
							<asp:Image ID="Image1"  runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
			</td>
			<td class="entry">
				<asp:checkbox runat="server" id="chkIsEnabled" text="Enabled" Checked="true" />
				<asp:checkbox runat="server" id="chkIsAutoNumber" text="Auto Number" />
				<asp:checkbox runat="server" id="chkIsBKU" text="BKU Code" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

