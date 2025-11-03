<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="BankDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.BankDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblBankID" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtBankID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvBankID" runat="server" ErrorMessage="ID required."
                    ValidationGroup="entry" ControlToValidate="txtBankID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblBankName" runat="server" Text="Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtBankName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ErrorMessage="Name required."
                    ValidationGroup="entry" ControlToValidate="txtBankName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblNoRek" runat="server" Text="Account No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNoRek" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRCurrency" runat="server" Text="Currency"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRCurrency" runat="server" Width="300px" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvSRCurrency" runat="server" ErrorMessage="Currency required."
                    ValidationGroup="entry" ControlToValidate="cboSRCurrency" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
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
                    OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                    OnItemsRequested="cboChartOfAccountId_ItemsRequested">
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
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Journal Code"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtJournalCode" runat="server" Width="300px" MaxLength="3" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsToBeCleared" runat="server" Text="Reconcile" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCrossRefference" runat="server" Text="Cross Reference" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsCashierFrontOffice" runat="server" Text="Cashier Front Office" />
                        </td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsCashierFrontOfficeDpReturn" runat="server" Text="Cashier Front Office (DP Return)" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsArPayment" runat="server" Text="AR Payment" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="ChkIsApPayment" runat="server" Text="AP Payment" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="ChkIsFeePayment" runat="server" Text="Fee Payment" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
        </tr>
        <tr>
            <td class="label">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsAssetAuctionPayment" runat="server" Text="Asset Auction Payment" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
        </tr>
        <tr runat="server" id="trIsBKU">
            <td class="label">
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:CheckBox ID="ChkIsBKU" runat="server" Text="Create BKU Journal" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20">
            </td>
        </tr>
    </table>
</asp:Content>
