<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ReconcileList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.ReconcileV2.ReconcileList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoViewUrl(bankid) {
                var url = 'ReconcileDetail.aspx?md=view&bankid=' + bankid + '';
                window.location.href = url;
            }

            function OnClientClose(oWnd, args) {
                __doPostBack("<%= grdList.UniqueID %>", "rebind");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
        AllowSorting="true">
        <MasterTableView DataKeyNames="BankID" ClientDataKeyNames="BankID" >
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                            DataBinder.Eval(Container.DataItem, "BankID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="BankID" HeaderText="Bank ID"
                    UniqueName="BankID" SortExpression="BankID">
                    <HeaderStyle HorizontalAlign="Left" Width="70" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BankName" HeaderText="Bank Name"
                    UniqueName="BankName" SortExpression="BankName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountCode" HeaderText="Chart Of Account Code"
                    UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode">
                    <HeaderStyle HorizontalAlign="Left" Width="130" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Chart Of Account Name"
                    UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="BalanceCleared" HeaderText="Balance Cleared" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="BalanceCleared" SortExpression="BalanceCleared" >
                    <HeaderStyle HorizontalAlign="Right" Width="150" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Balance" HeaderText="Balance" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="Balance" SortExpression="Balance" >
                    <HeaderStyle HorizontalAlign="Right" Width="150" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
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
