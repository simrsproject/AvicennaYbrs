<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="LedgerBalance.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.LedgerBalance" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function gotoDetailUrl(acc) {
                var ddlYear = $find("<%=ddlYear.ClientID %>");
                var year = ddlYear.get_selectedItem().get_value();

                var ddlMonth = $find("<%=ddlMonth.ClientID %>");
                var month = ddlMonth.get_selectedItem().get_value();
                location.replace('LedgerBalanceDetail.aspx?md=view&acc=' + acc + '&month=' + month + '&year=' + year);
            }
        </script>

    </telerik:RadCodeBlock>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Year
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="ddlYear" Width="304px" MarkFirstMatch="true"
                                    AutoPostBack="true">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                            </td>
                        </tr>
                         <tr>
                            <td class="label">
                                Month
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="ddlMonth" Width="304px" MarkFirstMatch="true">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterPeriode" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Periode" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                Chart Of Account
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboCoaID" Width="304px" HighlightTemplatedItems="true" EnableLoadOnDemand="true"
                                    OnItemsRequested="cboChartOfAccount_ItemsRequested" OnItemDataBound="cboChartOfAccount_ItemDataBound"
                                    DataTextField="ChartOfAccountCode" DataValueField="ChartOfAccountId" NoWrap="true">
                                    <ItemTemplate>
                                        <div>
                                            <b>
                                                <%# Eval("ChartOfAccountCode")%></b><%# Eval("ChartOfAccountName", " - {0}") %>
                                        </div>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterCoaID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Periode" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td width="200px">
                    <telerik:RadButton runat="server" ID="btnSendJournalToHo" ImageUrl="~/Images/Toolbar/process16.png" OnClick="btnSendJournalToHo_Click" Text="Create Balance as Journal in Head Office"/>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" AllowCustomPaging="true" PageSize="50">
        <MasterTableView DataKeyNames="ChartOfAccountId">
            <Columns>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="40px">
                    <ItemTemplate>
                      <%#string.Format("<a style='cursor:pointer;' onclick='javascript:gotoDetailUrl(\"{0}\")'><img src='../../../../Images/Toolbar/search16.png'/></a>", Eval("ChartOfAccountId"))%>  
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
<%--                <telerik:GridButtonColumn DataTextFormatString="Select {0}" ButtonType="ImageButton"
                    UniqueName="column" HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                    CommandName="Select" DataTextField="ChartOfAccountId" ImageUrl="~/Images/Toolbar/search16.png">
                    <HeaderStyle Width="28px" />
                </telerik:GridButtonColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="95px" DataField="ChartOfAccountCode"
                    AllowSorting="false" HeaderText="Account Code" UniqueName="ChartOfAccountCode"
                    SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" ItemStyle-Wrap="false" AllowSorting="false"
                    DataField="ChartOfAccountName" HeaderText="Account Name" UniqueName="ChartOfAccountName"
                    SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="InitialBalance" AllowSorting="false"
                    HeaderText="Initial Balance" UniqueName="InitialBalance" DataFormatString="{0:N2}"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="Debit" AllowSorting="false"
                    HeaderText="Debit" UniqueName="Debit" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="Credit" AllowSorting="false"
                    HeaderText="Credit" UniqueName="Credit" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="FinalBalance" AllowSorting="false"
                    HeaderText="Final Balance" UniqueName="FinalBalance" DataFormatString="{0:N2}"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
