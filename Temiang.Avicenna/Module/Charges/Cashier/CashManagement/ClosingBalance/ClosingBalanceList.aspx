<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="ClosingBalanceList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.ClosingBalanceList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinProcess(cmno) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("ClosingBalanceDialog.aspx?cmno=" + cmno);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winProcess" Animation="None" Width="1000px" Height="500px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterOpeningDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterClosingDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOpeningDate" runat="server" Text="Opening Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtOpeningDate" runat="server" Width="100px" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterOpeningDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClosingDate" runat="server" Text="Closing Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtClosingDate" runat="server" Width="100px" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterClosingDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AllowPaging="true" AllowSorting="true" ShowStatusBar="true">
        <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
            GroupLoadMode="client" PageSize="15" AutoGenerateColumns="false">
            <Columns>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsVisible").Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}'); return false;\">{1}</a>", 
                                        DataBinder.Eval(Container.DataItem, "TransactionNo"),
                                                                    "<img src=\"../../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Closing\" />"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="TransactionNo" HeaderText="Transaction No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="OpeningDate" HeaderText="Opening Date" UniqueName="OpeningDate"
                    SortExpression="OpeningDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ShiftName" HeaderText="Shift"
                    UniqueName="ShiftName" SortExpression="ShiftName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="CashierUserName" HeaderText="Cashier Name" UniqueName="CashierUserName"
                    SortExpression="CashierUserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CashierCounterName"
                    HeaderText="Cashier Counter" UniqueName="CashierCounterName" SortExpression="CashierCounterName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="OpeningBalance" HeaderText="Opening Balance"
                    UniqueName="OpeningBalance" SortExpression="OpeningBalance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="CashAmount" HeaderText="Cash Amount"
                    UniqueName="CashAmount" SortExpression="CashAmount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="ClosingBalance" HeaderText="Closing Balance"
                    UniqueName="ClosingBalance" SortExpression="ClosingBalance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridBoundColumn DataField="ClosingDate" HeaderText="Closing Date" UniqueName="ClosingDate"
                    SortExpression="ClosingDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
