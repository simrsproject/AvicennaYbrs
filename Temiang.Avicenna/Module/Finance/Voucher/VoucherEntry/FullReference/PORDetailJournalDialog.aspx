<%@ Page Title="Detail Journal Reference" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PORDetailJournalDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.PORDetailJournalDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdVoucherEntryItem" runat="server" ShowFooter="True" OnNeedDataSource="grdVoucherEntryItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="Horizontal" OnUpdateCommand="grdVoucherEntryItem_UpdateCommand"
        OnDeleteCommand="grdVoucherEntryItem_DeleteCommand" OnInsertCommand="grdVoucherEntryItem_InsertCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="DetailId" ShowGroupFooter="True" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="HeaderDescription" HeaderText="Description"></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="JournalGrouping" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="25px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="100px" DataField="ChartOfAccountCode"
                    HeaderText="Code" UniqueName="ChartOfAccountCode" SortExpression="ChartOfAccountCode" />
                <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="300px" DataField="ChartOfAccountName"
                    HeaderText="Account Name" UniqueName="ChartOfAccountName" />
                <telerik:GridBoundColumn DataField="SubLedgerId" HeaderText="Subledger ID" UniqueName="SubLedgerId"
                    Visible="false" />
                <telerik:GridBoundColumn ItemStyle-Wrap="True" HeaderStyle-Width="150px" DataField="SubLedgerName"
                    HeaderText="Subledger" UniqueName="SubLedgerName" />
                <telerik:GridBoundColumn ItemStyle-Wrap="true" HeaderStyle-Width="150px" DataField="DocumentNumber"
                    HeaderText="Reference#" UniqueName="DocumentNumber" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Count" FooterAggregateFormatString="Total :" />
                <telerik:GridBoundColumn DataField="Debit" HeaderText="Debit" UniqueName="Debit"
                    DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right">
                    <HeaderStyle HorizontalAlign="Center" Width="115px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Credit" HeaderText="Credit" UniqueName="Credit"
                    DataFormatString="{0:N2}" Aggregate="sum" FooterAggregateFormatString="{0:n2}"
                    FooterStyle-HorizontalAlign="Right">
                    <HeaderStyle HorizontalAlign="Center" Width="115px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                    ItemStyle-Wrap="True">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="25px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="VoucherEntryItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandColumn1">
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
