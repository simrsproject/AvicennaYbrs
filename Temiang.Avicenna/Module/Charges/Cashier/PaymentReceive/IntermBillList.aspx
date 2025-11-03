<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="IntermBillList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Cashier.IntermBillList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdIntermBill">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdIntermBill" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="grdIntermBill" runat="server" OnNeedDataSource="grdIntermBill_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" ShowFooter="true" AllowPaging="false">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="IntermBillNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="45px"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" Checked="false"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked="true"></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="IntermBillNo" HeaderText="Interim Bill No"
                    UniqueName="IntermBillNo" SortExpression="IntermBillNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="IntermBillDate" HeaderText="Date"
                    UniqueName="IntermBillDate" SortExpression="IntermBillDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                    UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                    UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="PatientAmount" HeaderText="Patient Amount"
                    UniqueName="PatientAmount" SortExpression="PatientAmount" HeaderStyle-HorizontalAlign="Center"
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" FooterAggregateFormatString="{0:n2}"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="GuarantorAmount"
                    HeaderText="Guarantor Amount" UniqueName="GuarantorAmount" SortExpression="GuarantorAmount"
                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    FooterAggregateFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="AdministrationAmount"
                    HeaderText="Patient Adm." UniqueName="AdministrationAmount" SortExpression="AdministrationAmount"
                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    FooterAggregateFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="GuarantorAdministrationAmount"
                    HeaderText="Guarantor Adm." UniqueName="GuarantorAdministrationAmount" SortExpression="GuarantorAdministrationAmount"
                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    FooterAggregateFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="DiscAdmPatient"
                    HeaderText="Pat Adm Disc" UniqueName="DiscAdmPatient" SortExpression="DiscAdmPatient"
                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    FooterAggregateFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="DiscAdmGuarantor"
                    HeaderText="Guar Adm Disc" UniqueName="DiscAdmGuarantor" SortExpression="DiscAdmGuarantor"
                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    FooterAggregateFormatString="{0:n2}" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />    
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total Amount"
                    UniqueName="TotalAmount" DataType="System.Double" DataFields="PatientAmount,GuarantorAmount,AdministrationAmount,GuarantorAdministrationAmount,DiscAdmPatient,DiscAdmGuarantor"
                    Expression="{0}+{1}+{2}+{3}-{4}-{5}" FooterText=" " FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
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
