<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PaymentReceiveSummaryList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.PaymentReceiveSummaryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "process":
                        __doPostBack("<%= grdBillingSummary.UniqueID %>", 'process');
                        break;
                    case "refresh":
                        __doPostBack("<%= grdBillingSummary.UniqueID %>", 'refresh');
                        break;
                    case "lock":
                        if (args.get_item().get_text() == "Lock")
                            args.get_item().set_text("Unlock");
                        else
                            args.get_item().set_text("Lock");
                        __doPostBack("<%= grdBillingSummary.UniqueID %>", 'lock');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdBillingSummary">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBillingSummary" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPendingSummary">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPendingSummary" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdBillingSummary" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPendingSummary" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="fw_RadAjaxManager">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPendingSummary" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdBillingSummary" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Lock" Value="lock" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
            <telerik:RadToolBarButton runat="server" IsSeparator="true" />
            <telerik:RadToolBarButton>
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                Stamp :&nbsp;
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cboItemMateraiID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemMateraiID_ItemDataBound"
                                    OnItemsRequested="cboItemMateraiID_ItemsRequested">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerik:RadToolBarButton>
            <telerik:RadToolBarButton runat="server" Value="process" ImageUrl="~/Images/Toolbar/insert16.png"
                HoveredImageUrl="~/Images/Toolbar/insert16_h.png" DisabledImageUrl="~/Images/Toolbar/insert16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="Approved" PageViewID="pgApproved" Selected="True" />
            <telerik:RadTab runat="server" Text="Pending" PageViewID="pgPending" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgApproved" runat="server">
            <telerik:RadGrid ID="grdBillingSummary" runat="server" OnNeedDataSource="grdBillingSummary_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="TransactionDate" HeaderText="Transaction Date "
                                    FormatString="{0:dd/MM/yyyy}"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="TransactionDate" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                            Aggregate="Count" FooterAggregateFormatString="Total :" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PatientAmount" HeaderText="Patient Amount"
                            UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="GuarantorAmount"
                            HeaderText="Guarantor Amount" UniqueName="GuarantorAmount" SortExpression="GuarantorAmount"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                            Aggregate="sum" FooterAggregateFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount Amount"
                            UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
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
        <telerik:RadPageView ID="pgPending" runat="server">
            <telerik:RadGrid ID="grdPendingSummary" runat="server" OnNeedDataSource="grdPendingSummary_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="true"
                PageSize="15">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="TransactionDate" HeaderText="Transaction Date "
                                    FormatString="{0:dd/MM/yyyy}"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="TransactionDate" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left"
                            Aggregate="Count" FooterAggregateFormatString="Total :" FooterStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Total" HeaderText="Total"
                            UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                            FooterStyle-HorizontalAlign="Right" />
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
