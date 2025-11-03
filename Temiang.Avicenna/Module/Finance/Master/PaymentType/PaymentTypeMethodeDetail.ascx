<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PaymentTypeMethodeDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.PaymentTypeMethodeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumBankAccount" runat="server" ValidationGroup="PaymentMethod" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PaymentMethod"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>

<table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblSRPaymentMethodID" runat="server" Text="Payment Method ID"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtSRPaymentMethodID" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRPaymentMethodID" runat="server" ErrorMessage="Payment Method ID No required."
                    	ControlToValidate="txtSRPaymentMethodID" SetFocusOnError="True" ValidationGroup="PaymentMethod" Width="100%">
						<asp:Image ID="Image1" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPaymentMethodName" runat="server" Text="Payment Method Name"></asp:Label>
			</td>        
			<td class="entry">
				<telerik:RadTextBox ID="txtPaymentMethodName" runat="server" Width="300px" MaxLength="300" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvPaymentMethodName" runat="server" ErrorMessage="Payment Method Name required."
                    	ControlToValidate="txtPaymentMethodName" SetFocusOnError="True" ValidationGroup="PaymentMethod" Width="100%">
						<asp:Image ID="Image3" runat="server" SkinID="rfvImage"/>
					</asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblChartOfAccountId" runat="server" Text="Chart Of Account"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Height="190px" Width="300px"
                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged"
                    OnItemDataBound="cboChartOfAccountId_ItemDataBound" OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                            &nbsp;-&nbsp;
                            <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                        </b>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSubledgerId" runat="server" Text="Subledger"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSubledgerId" Height="190px" Width="300px"
                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                    OnItemDataBound="cboSubledgerId_ItemDataBound" OnItemsRequested="cboSubledgerId_ItemsRequested">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%> 
                            &nbsp;-&nbsp;
                            <%# DataBinder.Eval(Container.DataItem, "Description")%> 
                        </b>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="height: 26px">
                <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PaymentMethod" 
					Visible='<%# !(DataItem is GridInsertionObject) %>'>
                </asp:Button>
                <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" ValidationGroup="PaymentMethod"
                    Visible='<%# DataItem is GridInsertionObject %>'>
				</asp:Button>
                &nbsp;
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                    CommandName="Cancel">
				</asp:Button></td>
        </tr>
</table>


