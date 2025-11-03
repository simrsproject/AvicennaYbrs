<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" 
	CodeBehind="SubLedgerBalance.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.SubLedgerBalance" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function gotoDetailUrl(acc, subacc) {
                var ddlYear = $find("<%=ddlYear.ClientID %>");
                var year = ddlYear.get_selectedItem().get_value();

                var ddlMonth = $find("<%=ddlMonth.ClientID %>");
                var month = ddlMonth.get_selectedItem().get_value();
                location.replace('SubLedgerBalanceDetail.aspx?md=view&acc=' + acc + '&month=' + month + '&year=' + year + '&sub=' + subacc);
            }
        </script>

    </telerik:RadCodeBlock>
    
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Year
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="ddlYear" Width="304px"  MarkFirstMatch="true" AutoPostback="true">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                            </td>
                        </tr>
                        
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Month</td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="ddlMonth" Width="304px" MarkFirstMatch="true">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                 <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Periode" />
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AllowPaging="true" AllowCustomPaging="true" PageSize="50" >
        <MasterTableView DataKeyNames="ChartOfAccountId,SubLedgerId">
            <Columns>
                                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="40px">
                    <ItemTemplate>
                      <%#string.Format("<a style='cursor:pointer;' onclick='javascript:gotoDetailUrl(\"{0}\",\"{1}\")'><img src='../../../../Images/Toolbar/search16.png'/></a>", Eval("ChartOfAccountId"), Eval("SubLedgerId"))%>  
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
<%--                <telerik:GridButtonColumn DataTextFormatString="Select {0}"
                        ButtonType="ImageButton" UniqueName="column" HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                        CommandName="Select" DataTextField="ChartOfAccountId" ImageUrl="~/Images/Toolbar/search16.png">
                        <HeaderStyle Width="28px" />
                </telerik:GridButtonColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="95px" DataField="ChartOfAccountCode" AllowSorting="false" HeaderText="Account Code" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="210px" ItemStyle-Wrap="false" AllowSorting="false"  DataField="ChartOfAccountName" HeaderText="Account Name" UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn DataField="SubLedgerName" ItemStyle-Wrap="false" AllowSorting="false" HeaderText="Subledger" UniqueName="SubLedgerName" SortExpression="SubLedgerName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridBoundColumn HeaderStyle-Width="210px" DataField="SubLedgerNameDescription" ItemStyle-Wrap="false" AllowSorting="false" HeaderText="Description" UniqueName="SubLedgerNameDescription" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="InitialBalance" AllowSorting="false" HeaderText="Initial Balance" UniqueName="InitialBalance" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="Debit" AllowSorting="false" HeaderText="Debit" UniqueName="Debit" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="Credit" AllowSorting="false" HeaderText="Credit" UniqueName="Credit" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridNumericColumn ItemStyle-Wrap="false" DataField="FinalBalance" AllowSorting="false" HeaderText="Final Balance" UniqueName="FinalBalance" DataFormatString="{0:N2}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"/>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>