<%@ Page AutoEventWireup="true" CodeBehind="ReconcileDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.CashManagement.Reconcile.ReconcileDetail"
    Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">

</script>

    </telerik:RadCodeBlock>
    <asp:Label ID="lblJournalId" runat="server" Visible="false"></asp:Label>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 40%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Bank
                        </td>
                        <td class="entry">
                            <asp:Label ID="lblBankName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnExportToExcel" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png" Enabled="true" OnClick="btnExport_Click" ToolTip="Export to Excel" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date / Balance
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDate" runat="server" Width="105px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkAllTransaction" runat="server" Text="All Transaction" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;">
                            <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td style="text-align: left"></td>
                    </tr>
                </table>
            </td>
            <td style="width: 30%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Bank Balance
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBankBalance" runat="server"
                                OnTextChanged="txtBankBalance_TextChanged" AutoPostBack="true">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Reconciled Balance
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtBalance" runat="server">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 30%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">Different
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDifferent" runat="server">
                                            <NumberFormat DecimalDigits="2" />
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <a href="../CashEntryV2/CashEntryDetail.aspx?md=new&source=re&bankid=<%=Request.QueryString["bankid"] %>">Adjust</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Actual Balance
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCurrentBalance" runat="server">
                                <NumberFormat DecimalDigits="2" />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="width: 20px;"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdTransaction" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="grdTransaction_OnNeedDataSource"
        OnItemCommand="grdTransaction_ItemCommand"
        AllowPaging="True" PageSize="20" AllowSorting="False" AllowCustomPaging="true">
        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
        <MasterTableView ClientDataKeyNames="TransactionId" DataKeyNames="TransactionId"
            InsertItemPageIndexAction="ShowItemOnCurrentPage">
            <Columns>
                <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date"
                    DataFormatString="{0:MM/dd/yyyy}" DataType="System.DateTime"
                    SortExpression="TransactionDate" UniqueName="TransactionDate">
                    <HeaderStyle HorizontalAlign="Left" Width="130" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                    UniqueName="Description">
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="DebitAmount" HeaderText="Debit" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="DebitAmount" SortExpression="DebitAmount">
                    <HeaderStyle HorizontalAlign="Right" Width="150" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="CreditAmount" HeaderText="Credit" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="CreditAmount" SortExpression="CreditAmount">
                    <HeaderStyle HorizontalAlign="Right" Width="150" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lbStatusHd" runat="server"
                            CommandArgument='<%#string.Format("{0}|{1}", "page", "Reconcile") %>'
                            CommandName="setStatus"
                            ToolTip='<%#string.Format("{0}", "Reconcile By Page") %>'
                            OnClientClick='<%#string.Format("{0}", "return confirm(\"Are you sure want to reconcile this page?\");") %>'>
                            <%#string.Format("<img style=\"border: 0px; text-align:center; vertical-align: middle;\" src=\"../../../../Images/Toolbar/{0} \" />",
                            "post16_d.png")%>
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbStatus" runat="server"
                            CommandArgument='<%#string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "TransactionId"),((bool)DataBinder.Eval(Container.DataItem, "IsCleared")) ? "UnReconcile" : "Reconcile") %>'
                            CommandName="setStatus"
                            ToolTip='<%#string.Format("{0}",((bool)DataBinder.Eval(Container.DataItem, "IsCleared")) ? "Reconciled" : "") %>'
                            OnClientClick='<%#string.Format("{0}", ((bool)DataBinder.Eval(Container.DataItem, "IsCleared")) ? "return confirm(\"Are you sure want to mark this record as unreconciled?\");" : "return true;") %>'>
                            <%#string.Format("<img style=\"border: 0px; text-align:center; vertical-align: middle;\" src=\"../../../../Images/Toolbar/{0} \" />",
                            ((bool)DataBinder.Eval(Container.DataItem, "IsCleared")) ? "post16.png" : "post16_d.png")%>
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>