<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeAddDeducDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.ParamedicFeeAddDeducDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRParamedicFeeAdjustType" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRParamedicFeeAdjustType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRParamedicFeeAdjustType" runat="server" ErrorMessage="Type required."
                                ControlToValidate="cboSRParamedicFeeAdjustType" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Chart Of Account Template
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboChartOfAccountTemplateId" Height="190px"
                                OnSelectedIndexChanged="cboChartOfAccountTemplateId_SelectedIndexChanged" AutoPostBack="true"
                                Width="300px" EnableLoadOnDemand="true" HighlightTemplatedItems="true" OnItemDataBound="cboChartOfAccountTemplateId_ItemDataBound"
                                OnItemsRequested="cboChartOfAccountTemplateId_ItemsRequested">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ListId")%>
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAmount" runat="server" Text="Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" MinValue="0" />
                        </td>
                        <td width="20">
                        </td>
                    </tr>
                    <tr runat="server" id="tr1">
                        <td class="label">

                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsIncludeInTaxCalc" runat="server" Text="Include In Tax Calculation" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr runat="server" id="trPrice">
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Pph Base Amount"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MinValue="0" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTariffComponentID" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPphType" runat="server" ErrorMessage="Tariff component required."
                                ControlToValidate="cboTariffComponentID" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdVoucherEntryItem" runat="server" ShowFooter="True" OnNeedDataSource="grdVoucherEntryItem_NeedDataSource" Visible="false"
        AutoGenerateColumns="False" GridLines="Horizontal" OnUpdateCommand="grdVoucherEntryItem_UpdateCommand"
        OnDeleteCommand="grdVoucherEntryItem_DeleteCommand" OnInsertCommand="grdVoucherEntryItem_InsertCommand">
        <MasterTableView DataKeyNames="ListItemId" GroupLoadMode="Client" CommandItemDisplay="None">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="25px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Account Name"
                    UniqueName="ChartOfAccountName" />
                <telerik:GridBoundColumn DataField="SubLedgerName" HeaderText="Subledger" UniqueName="SubLedgerName"
                    Aggregate="Count" FooterAggregateFormatString="Total :" FooterStyle-HorizontalAlign="Right" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="Amount" HeaderText="Amount"
                    UniqueName="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right" Aggregate="sum" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="25px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="ParamedicFeeAddDeducItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandParamedicFeeAddDeducItem">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
