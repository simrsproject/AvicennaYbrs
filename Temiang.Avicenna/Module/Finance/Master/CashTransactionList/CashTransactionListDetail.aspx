<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashTransactionListDetail.aspx.cs"
    MasterPageFile="~/MasterPage/MasterDetail.Master" Inherits="Temiang.Avicenna.Module.Finance.Master.CashTransactionListDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            List ID
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtListId" runat="server" Width="100px" MaxLength="100" Visible="true" />
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvListId" runat="server" ErrorMessage="ID required."
                                ValidationGroup="entry" ControlToValidate="txtListId" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Description
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Description required."
                                ValidationGroup="entry" ControlToValidate="txtDescription" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Cash Type
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboCashType" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboCashType_SelectedIndexChanged" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Chart Of Account Code
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountId" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="cboChartOfAccountId_SelectedIndexChanged" OnItemDataBound="cboChartOfAccountId_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountId_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ChartOfAccountName")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px;" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Subledger
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSubledgerId" Height="190px" Width="300px"
                                EnableLoadOnDemand="true" HighlightTemplatedItems="true" AutoPostBack="false"
                                OnItemDataBound="cboSubledgerId_ItemDataBound" OnItemsRequested="cboSubledgerId_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "SubLedgerName")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    </b>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px;" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdListItem" runat="server" AllowPaging="true" AllowCustomPaging="true"
        PageSize="18" ShowFooter="True" OnNeedDataSource="grdListItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="Horizontal" OnUpdateCommand="grdListItem_UpdateCommand"
        OnDeleteCommand="grdListItem_DeleteCommand" OnInsertCommand="grdListItem_InsertCommand">
        <MasterTableView DataKeyNames="ListItemId, ListId" GroupLoadMode="Client" CommandItemDisplay="None">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="25px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ChartOfAccountCode"
                    HeaderText="Code" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" />
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Account Name"
                    UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName" />
                <telerik:GridBoundColumn DataField="SubLedgerName" HeaderText="Subledger" UniqueName="SubLedgerName"
                    SortExpression="SubLedgerName" FooterAggregateFormatString="Total :" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" SortExpression="Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="sum" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="25px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="CashTransactionListItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1" />
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
