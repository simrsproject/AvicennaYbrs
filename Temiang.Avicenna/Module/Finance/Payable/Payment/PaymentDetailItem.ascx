<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentDetailItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Payable.PaymentDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">Supp Invoice No.
        </td>
        <td class="entry">
            <telerik:RadTextBox runat="server" ID="txtInvoiceReferenceNo" Width="100%" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">Payment Amount
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox runat="server" ID="txtPaymentAmount" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvBridgingType" runat="server" ErrorMessage="Payment Amount required."
                ControlToValidate="txtPaymentAmount" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">Currency
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboCurrency" Width="100%" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvCurrency" runat="server" ErrorMessage="Currency required."
                ControlToValidate="cboCurrency" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr id="trSRItemType" runat="server" visible="false">
        <td class="label">
            <asp:Label ID="Label3" runat="server" Text="Item Type"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboSRItemType" runat="server" Visible="false" OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged"
                AutoPostBack="true" Width="100%">
                <Items>
                    <telerik:RadComboBoxItem Text="" Value="" />
                    <telerik:RadComboBoxItem Text="Medical" Value="11" />
                    <telerik:RadComboBoxItem Text="Non Medical" Value="21" />
                </Items>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRItemType" runat="server" ErrorMessage="Item Type required."
                ValidationGroup="ServiceUnitAutoBillItem" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                Width="100%" Enabled="false">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Chart Of Account (Debit Account)
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboChartOfAccountCode" Height="190px"
                Width="100%" EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                OnSelectedIndexChanged="cboChartOfAccountCode_SelectedIndexChanged" 
                OnItemDataBound="cboChartOfAccountCode_ItemDataBound"
                OnItemsRequested="cboChartOfAccountCode_ItemsRequested"  >
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
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">Subledger
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSubledgerId" Width="100%" EnableLoadOnDemand="true"
                HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSubledgerId_ItemDataBound"
                OnItemsRequested="cboSubledgerId_ItemsRequested">
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
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">Notes
        </td>
        <td class="entry">
            <telerik:RadTextBox runat="server" ID="txtNotes" Width="100%" TextMode="MultiLine" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
