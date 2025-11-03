<%@ Page Title="Transaction Item List Outstanding" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="IntermBillPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Billing.IntermBillPickList" %>

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
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="False" AllowMultiRowSelection="true"
        ShowFooter="true">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo, SequenceNo"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit ">
                        </telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" Checked="false"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked="true"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="SequenceNo" UniqueName="SequenceNo"
                    SortExpression="SequenceNo" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID"
                    SortExpression="ItemID" HeaderStyle-Width="90px">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCalculatedColumn HeaderStyle-Width="120px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="PatientAmount,GuarantorAmount" SortExpression="Total"
                    Expression="{0} + {1}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="PatientAmount" HeaderText="Patient Amount" UniqueName="PatientAmount"
                    SortExpression="PatientAmount" DataFormatString="{0:n2}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                    FooterAggregateFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GuarantorAmount" HeaderText="Guarantor Amount"
                    UniqueName="GuarantorAmount" SortExpression="GuarantorAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DiscountAmount" HeaderText="Discount Amount"
                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum" FooterStyle-HorizontalAlign="Right" FooterAggregateFormatString="{0:n2}">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IntermBillNo" HeaderText="IntermBillNo" UniqueName="IntermBillNo"
                    SortExpression="IntermBillNo" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
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
