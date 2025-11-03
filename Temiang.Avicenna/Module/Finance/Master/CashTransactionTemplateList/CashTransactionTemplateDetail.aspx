<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashTransactionTemplateDetail.aspx.cs"
    MasterPageFile="~/MasterPage/MasterDetail.Master" Inherits="Temiang.Avicenna.Module.Finance.Master.CashTransactionTemplateDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="100%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            Template ID
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTemplateId" runat="server" Width="100px" MaxLength="100" ReadOnly="true" />
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            Template Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTemplateName" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td style="width: 20px;">
                            <asp:RequiredFieldValidator ID="rfvTemplateName" runat="server" ErrorMessage="Template name required."
                                ValidationGroup="entry" ControlToValidate="txtTemplateName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
                        </td>
                        <td style="width: 20px;">
                        </td>
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
        <MasterTableView DataKeyNames="TemplateDetailId" GroupLoadMode="Client" CommandItemDisplay="None">
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
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountVariablePercentage" HeaderText="Amount Variable Percentage"
                    UniqueName="AmountVariablePercentage" SortExpression="AmountVariablePercentage" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2} %" FooterStyle-HorizontalAlign="Right"
                    Aggregate="sum" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountFixed" HeaderText="Amount Fixed"
                    UniqueName="AmountFixed" SortExpression="AmountFixed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="sum" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="25px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="CashTransactionTemplateItem.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1" />
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
