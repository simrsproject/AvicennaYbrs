<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PaymentTypeCoaSetting.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting.PaymentTypeCoaSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openItemComp(serviceUnitID, itemID, sRItemType) {
                var oWnd = $find("<%= winItemClass.ClientID %>");

                if (sRItemType == '11' || sRItemType == '21')
                    oWnd.SetUrl("ProductAccountSetting.aspx?itemID=" + itemID);
                else
                    oWnd.SetUrl("../../../../RADT/Master/ServiceUnit/ServiceUnitItemServiceComp.aspx?unitID=" + serviceUnitID + "&itemID=" + itemID);
                oWnd.Show();
                oWnd.Maximize();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.mode != null) {
                    if (oWnd.argument.mode == 'rebind') {
                        __doPostBack("<%= grdTransItem.UniqueID %>", "rebind");
                    }
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboSettingStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="winItemClass">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransItem" />
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
            <telerik:RadTab runat="server" Text="Payment Item" Selected="True" PageViewID="pgLeft" />
            <telerik:RadTab runat="server" Text="Transaction Item" PageViewID="pgRight" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgLeft" runat="server">
            <telerik:RadGrid ID="grdDetail" runat="server" OnNeedDataSource="grdDetail_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="100">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="PaymentType" HeaderText="Payment Type" HeaderStyle-Width="110px"
                            UniqueName="PaymentType" SortExpression="PaymentType" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn DataField="PaymentMethod" HeaderText="Payment Method" HeaderStyle-Width="110px"
                            UniqueName="PaymentMethod" SortExpression="PaymentMethod" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CardProvider" HeaderText="Card Provider"
                            UniqueName="CardProvider" SortExpression="CardProvider" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="EDCMachine" HeaderText="EDC Machine"
                            UniqueName="EDCMachine" SortExpression="EDCMachine" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BankName" HeaderText="Bank"
                            UniqueName="BankName" SortExpression="BankName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="dAccCode" HeaderText="COA Code"
                            UniqueName="dAccCode" SortExpression="dAccCode" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="dAccName" HeaderText="COA Name" UniqueName="dAccName"
                            SortExpression="dAccName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="dSublName" HeaderText="Subleger"
                            UniqueName="dSublName" SortExpression="dSublName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
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
            <telerik:RadGrid ID="grdTransItem" runat="server" OnNeedDataSource="grdTransItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="15">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitName, ItemID"
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
                        <telerik:GridBoundColumn DataField="ItemID" HeaderStyle-Width="100px" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TariffComponentName" HeaderStyle-Width="120px"
                            HeaderText="Tariff Component" UniqueName="TariffComponentName" SortExpression="TariffComponentName"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SRRegistrationType" HeaderStyle-Width="60px"
                            HeaderText="Reg Type" UniqueName="SRRegistrationType" SortExpression="SRRegistrationType"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CoaCodeIncome" HeaderStyle-Width="120px" HeaderText="COA Code Income"
                            UniqueName="CoaCodeIncome" SortExpression="CoaCodeIncome" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="CoaNameIncome" HeaderStyle-Width="150px" HeaderText="COA Name Income"
                            UniqueName="CoaNameIncome" SortExpression="CoaNameIncome" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SublIncome" HeaderStyle-Width="150px" HeaderText="SubLedger Income"
                            UniqueName="SublIncome" SortExpression="SublIncome" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="CoaCodeDisc" HeaderStyle-Width="120px" HeaderText="COA Code Discount"
                            UniqueName="CoaCodeDisc" SortExpression="CoaCodeDisc" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="CoaNameDisc" HeaderStyle-Width="150px" HeaderText="COA Name Discount"
                            UniqueName="CoaNameDisc" SortExpression="CoaNameDisc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SublDisc" HeaderStyle-Width="150px" HeaderText="SubLedger Discount"
                            UniqueName="SublDisc" SortExpression="SublDisc" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="processItem">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openItemComp('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../../Images/Toolbar/new16_d.png\" border=\"0\" alt=\"Open Item Component\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "ServiceUnitID"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "SRItemType"))%>
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
    </telerik:RadMultiPage>
</asp:Content>
