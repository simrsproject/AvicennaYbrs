<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ItemTransactionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ItemTransactionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
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
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                                OnItemsRequested="cboItemID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchItem" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnSearchTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="False" GridLines="None" ShowFooter="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="RegistrationNo,TransactionNo,SequenceNo">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit " />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitID" SortOrder="Ascending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="Group" HeaderText="Transaction No " />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="Group" SortOrder="None" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsApprove" UniqueName="IsApprove" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="ApproveTemplateColumn" HeaderText="App">
                    <ItemTemplate>
                        <%# GetStatus(DataBinder.Eval(Container.DataItem, "IsOrder"), DataBinder.Eval(Container.DataItem, "IsOrderRealization"), DataBinder.Eval(Container.DataItem, "IsApprove")) %>
                    </ItemTemplate>
                    <HeaderStyle Width="25px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="IsVoid" UniqueName="IsVoid" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsBillProceed" UniqueName="IsBillProceed" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="BillingTemplateColumn" Visible="false">
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsBillProceed") %>'
                            Enabled='false'></asp:CheckBox>
                    </ItemTemplate>
                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="IsOrder" UniqueName="IsOrder" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsOrderRealization" UniqueName="IsOrderRealization"
                    Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="OrderTemplateColumn" HeaderText="Ord | Sta">
                    <HeaderStyle Width="65px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="orderChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOrder") %>'
                            Enabled="false"></asp:CheckBox>
                        <asp:CheckBox ID="realizationChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsOrderRealization") %>'
                            Enabled="false"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="StatusTemplateColumn" HeaderText="Paid | IBill" Visible="false">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="paymentChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsPaymentProceed") %>'
                            Enabled="false"></asp:CheckBox>
                        <asp:CheckBox ID="intermbillChkbox" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsIntermBillProceed") %>'
                            Enabled="false"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ExecutionDate" HeaderText="Execution Date" UniqueName="ExecutionDate"
                    SortExpression="ExecutionDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="ChargeQuantity" HeaderText="Qty" UniqueName="ChargeQuantity"
                    SortExpression="ChargeQuantity">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                    SortExpression="SRItemUnit">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                    SortExpression="Price" DataFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="85px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridTemplateColumn UniqueName="Discount" HeaderText="Discount">
                    <HeaderStyle HorizontalAlign="Center" Width="85px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <label title='<%# DataBinder.Eval(Container.DataItem, "DiscountReason")%>'>
                            <%# String.Format("{0:n2}", DataBinder.Eval(Container.DataItem, "DiscountAmount"))%></label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn DataField="CitoAmount" HeaderText="Cito" UniqueName="CitoAmount"
                    SortExpression="CitoAmount" DataFormatString="{0:n2}" Aggregate="Count" FooterAggregateFormatString="Total :">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="Total" HeaderText="Total" UniqueName="Total"
                    SortExpression="Total" DataFormatString="{0:n2}" Aggregate="Sum" FooterAggregateFormatString="{0:n2}">
                    <FooterStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="95px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="LastUpdateByUserID" HeaderText="Update By" UniqueName="LastUpdateByUserID"
                    SortExpression="LastUpdateByUserID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastUpdateDateTime" HeaderText="Last Update"
                    UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime" DataType="System.DateTime"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
