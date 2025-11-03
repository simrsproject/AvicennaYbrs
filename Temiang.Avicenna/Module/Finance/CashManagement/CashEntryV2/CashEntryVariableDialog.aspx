<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CashEntryVariableDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryVariableDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="txtCashList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="fw_ajxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCalculate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" LoadingPanelID="fw_ajxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lblPctg" />
                    <telerik:AjaxUpdatedControl ControlID="lblVariable" />
                    <telerik:AjaxUpdatedControl ControlID="lblFixed" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotal" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="fw_ajxLoadingPanel1" runat="server" MinDisplayTime="10"
            InitialDelayTime="200" EnableEmbeddedScripts="false" BackgroundPosition="Center" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Template
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTemplate" TabIndex="0" runat="server" Height="190px"
                                Width="304px" AutoPostBack="true" HighlightTemplatedItems="true" DataTextField="TemplateName"
                                DataValueField="TemplateId" NoWrap="true">
                                <ItemTemplate>
                                    <div>
                                        <%# Eval("TemplateName", "{0}") %>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td style="width: 20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Total Amount
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="100px" MinValue="0" />
                        </td>
                        <td style="width: 20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                        </td>
                        <td style="width: 20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table cellpadding="0" cellspacing="0" class="info success" style="font-size:medium; width: 100%;">
                    <tr>
                        <td style="width: 120px">Percentage</td>
                        <td style="text-align:center">:</td>
                        <td style="text-align:right">
                            <asp:Label ID="lblPctg" runat="server"></asp:Label></td>
                        <td style="text-align:left">&nbsp;%</td>
                    </tr>
                    <tr>
                        <td>Variable Amount</td>
                        <td style="text-align:center">:</td>
                        <td style="text-align:right">
                            <asp:Label ID="lblVariable" runat="server"></asp:Label></td>
                        <td style="text-align:left"></td>
                    </tr>
                    <tr>
                        <td>Fixed Amount</td>
                        <td style="text-align:center">:</td>
                        <td style="text-align:right">
                            <asp:Label ID="lblFixed" runat="server"></asp:Label></td>
                        <td style="text-align:left"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><hr /></td>
                        <td>
                            <hr /></td>
                        <td style="text-align:left"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="text-align:right">
                            <asp:Label ID="lblTotal" runat="server"></asp:Label></td>
                        <td style="text-align:left"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <telerik:RadGrid ID="grdListItem" runat="server" AllowPaging="true" AllowCustomPaging="true"
        PageSize="18" ShowFooter="True" OnNeedDataSource="grdListItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="Horizontal">
        <MasterTableView DataKeyNames="TemplateDetailId, TemplateId" GroupLoadMode="Client" CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="ChartOfAccountId" UniqueName="ChartOfAccountId"
                    SortExpression="ChartOfAccountId" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ChartOfAccountCode"
                    HeaderText="Code" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" />
                <telerik:GridBoundColumn DataField="ChartOfAccountName" HeaderText="Account Name"
                    UniqueName="ChartOfAccountName" SortExpression="ChartOfAccountName" />
                <telerik:GridBoundColumn DataField="NormalBalance" UniqueName="NormalBalance" SortExpression="NormalBalance" 
                    Visible="false" />
                <telerik:GridBoundColumn DataField="SubLedgerId" UniqueName="SubLedgerId" SortExpression="SubLedgerId"
                    Visible="false" />
                <telerik:GridBoundColumn DataField="SubLedgerDesc" HeaderText="Subledger" UniqueName="SubLedgerDesc"
                    SortExpression="SubLedgerDesc" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountVariablePercentage" HeaderText="Value (%)"
                    UniqueName="AmountVariablePercentage" SortExpression="AmountVariablePercentage" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" FooterStyle-HorizontalAlign="Right"
                    Aggregate="sum" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" DataField="AmountVariableCalculated" HeaderText="Amount Fixed"
                    UniqueName="AmountVariableCalculated" SortExpression="AmountVariableCalculated" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtAmountVariableCalculated" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "AmountVariableCalculated")) %>'
                            Width="100%" ReadOnly="true">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" DataField="AmountFixed" HeaderText="Amount Fixed"
                    UniqueName="AmountFixed" SortExpression="AmountFixed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtAmountFixed" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "AmountFixed")) %>'
                            Width="100%" ReadOnly="true">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
