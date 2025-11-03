<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeCoaSetting.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting.ParamedicFeeCoaSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openItemComp(serviceUnitID, itemID) {
                var oWnd = $find("<%= winItemClass.ClientID %>");

                oWnd.SetUrl("../../../../RADT/Master/ServiceUnit/ServiceUnitItemServiceComp.aspx?unitID=" + serviceUnitID + "&itemID=" + itemID);
                oWnd.Show();
                oWnd.Maximize();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) {
                    if (oWnd.argument.mode == 'rebind') {
                        __doPostBack("<%= grdDebit.UniqueID %>", "rebind");
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboSettingStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDebit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="winItemClass">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDebit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winItemClass" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Coa Cost" Selected="True" PageViewID="pgLeft" />
            <telerik:RadTab runat="server" Text="AP" PageViewID="pgRight" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgLeft" runat="server">
            <table width="100%">
                <tr>
                    <td class="label">
                        Setting Status
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSettingStatus" runat="server" Width="300px" AutoPostBack="true"
                            OnSelectedIndexChanged="cboSettingStatus_SelectedIndexChanged">
                            <Items>
                                <telerik:RadComboBoxItem Text="All" Value="0" Selected="true" />
                                <telerik:RadComboBoxItem Text="Not Configured" Value="1" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
            <telerik:RadGrid ID="grdDebit" runat="server" OnNeedDataSource="grdDebit_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="15">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SRRegistrationType, ServiceUnitID, ItemID, TariffComponentID"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="None" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRRegistrationType" HeaderText="Reg Type"
                            UniqueName="SRRegistrationType" SortExpression="SRRegistrationType" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderStyle-Width="150px" ItemStyle-Wrap="true"
                            HeaderText="Item Name" UniqueName="ItemName" SortExpression="ItemName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="TariffComponentID" HeaderText="Component ID"
                            UniqueName="TariffComponentID" SortExpression="TariffComponentID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="TariffComponentName" HeaderStyle-Width="150px" ItemStyle-Wrap="true"
                            HeaderText="Component Name" UniqueName="TariffComponentName" SortExpression="TariffComponentName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ChartOfAccountCode" HeaderText="Account Code"
                            UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Account Name" UniqueName="ChartOfAccountName"
                            SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SubLedgerName" HeaderText="Subleger"
                            UniqueName="SubLedgerName" SortExpression="SubLedgerName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="processItem">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openItemComp('{0}', '{1}'); return false;\"><img src=\"../../../../../Images/Toolbar/new16_d.png\" border=\"0\" alt=\"Open Item Component\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "ServiceUnitID"), DataBinder.Eval(Container.DataItem, "ItemID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRight" runat="server">
            <telerik:RadGrid ID="grdCredit" runat="server" OnNeedDataSource="grdCredit_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="15">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParamedicID"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ParamedicID" HeaderText="Paramedic ID"
                            UniqueName="ParamedicID" SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Paramedic Name" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ChartOfAccountCode" HeaderText="Account Code"
                            UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Account Name" UniqueName="ChartOfAccountName"
                            SortExpression="ChartOfAccountName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
