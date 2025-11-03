<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitCoaMatrixDetailProductAccount.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitCoaMatrixDetailProductAccount" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblServiceUnit" Text="Service Unit" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtServiceUnitID" Width="100px" ReadOnly="True" />
                            <asp:Label runat="server" ID="lblServiceUnitName" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label runat="server" ID="lblLocation" Text="Location" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtLocationID" Width="100px" ReadOnly="True" />
                            <asp:Label runat="server" ID="lblLocationName" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnItemCreated="grdList_ItemCreated"
        OnNeedDataSource="grdList_NeedDataSource" AllowPaging="False" AllowSorting="true"
        ShowStatusBar="true">
        <MasterTableView DataKeyNames="ProductAccountID, SRRegistrationType" ClientDataKeyNames="ProductAccountID, SRRegistrationType"
            AutoGenerateColumns="false" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ProductAccountName" HeaderText="Product Account ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ProductAccountName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="ProductAccountID" UniqueName="ProductAccountID"
                    SortExpression="ProductAccountID" Visible="false" />
                <telerik:GridBoundColumn DataField="SRRegistrationType" UniqueName="SRRegistrationType"
                    SortExpression="SRRegistrationType" Visible="false" />
                <telerik:GridBoundColumn DataField="ChartOfAccountIdIncome" UniqueName="ChartOfAccountIdIncome"
                    SortExpression="ChartOfAccountIdIncome" Visible="false" />
                <telerik:GridBoundColumn DataField="SubledgerIdIncome" UniqueName="SubledgerIdIncome"
                    SortExpression="SubledgerIdIncome" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Income" UniqueName="ChartOfAccountIdIncome"
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    COA Income
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboCOAIncome" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="ChartOfAccountName" DataValueField="ChartOfAccountId"
                                        OnItemsRequested="cboCOAExpense_ItemsRequested" OnItemDataBound="cboCOAExpense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SL
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSLIncome" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="Description" DataValueField="SubLedgerId"
                                        OnItemsRequested="cboSLExprense_ItemsRequested" OnItemDataBound="cboSLExprense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountIdAccrual" UniqueName="ChartOfAccountIdAccrual"
                    SortExpression="ChartOfAccountIdAccrual" Visible="false" />
                <telerik:GridBoundColumn DataField="SubledgerIdAccrual" UniqueName="SubledgerIdAccrual"
                    SortExpression="SubledgerIdAccrual" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Accrual" UniqueName="ChartOfAccountIdAccrual"
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    COA Accrual
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboCOAAccrual" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="ChartOfAccountName" DataValueField="ChartOfAccountId"
                                        OnItemsRequested="cboCOAExpense_ItemsRequested" OnItemDataBound="cboCOAExpense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SL
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSLAccrual" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="Description" DataValueField="SubLedgerId"
                                        OnItemsRequested="cboSLExprense_ItemsRequested" OnItemDataBound="cboSLExprense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountIdDiscount" UniqueName="ChartOfAccountIdDiscount"
                    SortExpression="ChartOfAccountIdDiscount" Visible="false" />
                <telerik:GridBoundColumn DataField="SubledgerIdDiscount" UniqueName="SubledgerIdDiscount"
                    SortExpression="SubledgerIdDiscount" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Discount" UniqueName="ChartOfAccountIdDiscount"
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    COA Discount
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboCOADiscount" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="ChartOfAccountName" DataValueField="ChartOfAccountId"
                                        OnItemsRequested="cboCOAExpense_ItemsRequested" OnItemDataBound="cboCOAExpense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SL
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSLDiscount" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="Description" DataValueField="SubLedgerId"
                                        OnItemsRequested="cboSLExprense_ItemsRequested" OnItemDataBound="cboSLExprense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountIdInventory" UniqueName="ChartOfAccountIdInventory"
                    SortExpression="ChartOfAccountIdInventory" Visible="false" />
                <telerik:GridBoundColumn DataField="SubledgerIdInventory" UniqueName="SubledgerIdInventory"
                    SortExpression="SubledgerIdInventory" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Inventory" UniqueName="ChartOfAccountIdInventory"
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    COA Inventory
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboCOAInventory" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="ChartOfAccountName" DataValueField="ChartOfAccountId"
                                        OnItemsRequested="cboCOAExpense_ItemsRequested" OnItemDataBound="cboCOAExpense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SL
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSLInventory" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="Description" DataValueField="SubLedgerId"
                                        OnItemsRequested="cboSLExprense_ItemsRequested" OnItemDataBound="cboSLExprense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountIdCOGS" UniqueName="ChartOfAccountIdCOGS"
                    SortExpression="ChartOfAccountIdCOGS" Visible="false" />
                <telerik:GridBoundColumn DataField="SubledgerIdCOGS" UniqueName="SubledgerIdCOGS"
                    SortExpression="SubledgerIdCOGS" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="COGS" UniqueName="ChartOfAccountIdCOGS" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    COA Cogs
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboCOACOGS" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="ChartOfAccountName" DataValueField="ChartOfAccountId"
                                        OnItemsRequested="cboCOAExpense_ItemsRequested" OnItemDataBound="cboCOAExpense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SL
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSLCOGS" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="Description" DataValueField="SubLedgerId"
                                        OnItemsRequested="cboSLExprense_ItemsRequested" OnItemDataBound="cboSLExprense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountIdExpense" UniqueName="ChartOfAccountIdExpense"
                    SortExpression="ChartOfAccountIdExpense" Visible="false" />
                <telerik:GridBoundColumn DataField="SubledgerIdExpense" UniqueName="SubledgerIdExpense"
                    SortExpression="SubledgerIdExpense" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Expense" UniqueName="ChartOfAccountIdExpense"
                    HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    COA
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboCOAExpense" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="ChartOfAccountName" DataValueField="ChartOfAccountId"
                                        OnItemsRequested="cboCOAExpense_ItemsRequested" OnItemDataBound="cboCOAExpense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SL
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSLExprense" runat="server" Width="100%" EnableLoadOnDemand="true"
                                        AllowCustomText="true" DataTextField="Description" DataValueField="SubLedgerId"
                                        OnItemsRequested="cboSLExprense_ItemsRequested" OnItemDataBound="cboSLExprense_ItemDataBound">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
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
</asp:Content>
