<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CashEntrySearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntrySearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblJournalNumber" runat="server" Text="Journal Number" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtJournalNumber" runat="server" Width="300px" MaxLength="25" />
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblIsApproved" runat="server" Text="Status" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPostingStatus" runat="server" Width="300px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="-" Value="-1" />
                        <telerik:RadComboBoxItem runat="server" Text="Non-Approved" Value="0" />
                        <telerik:RadComboBoxItem runat="server" Text="Approved" Value="1" />
                    </Items>

                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label3" runat="server" Text="Bank Name" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboBankName" runat="server" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblModuleName" runat="server" Text="Module" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboModuleName" runat="server" Width="300px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="-" Value="-1" />
                        <telerik:RadComboBoxItem runat="server" Text="GL" Value="GL" />
                        <telerik:RadComboBoxItem runat="server" Text="AR" Value="AR" />
                        <telerik:RadComboBoxItem runat="server" Text="AP" Value="AP" />
                    </Items>

                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblTransactionType" runat="server" Text="TR Type" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboTransactionType" runat="server" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblDocumentNumber" runat="server" Text="Document Number" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDocumentNumber" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date" Width="110px"></asp:Label>
            </td>
            <td class="entry">

                <table cellpadding="0" cellspacing="">
                    <tr>
                        <td colspan="3">
                            <asp:RadioButtonList runat="server" ID="rbRangeFilter">
                                <asp:ListItem Value="0" Text="All Range"></asp:ListItem>
                                <asp:ListItem Value="1" Text="This Month"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Last One Month"></asp:ListItem>
                                <asp:ListItem Value="3" Text="This Year"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Last One Year"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                        </td>
                        <td style="width: 10px">-</td>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>

            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label1" runat="server" Text="Description" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label2" runat="server" Text="Description Detail" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDescriptioinDetail" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                <asp:Label ID="Label4" runat="server" Text="Amount" Width="110px"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="300px" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>

