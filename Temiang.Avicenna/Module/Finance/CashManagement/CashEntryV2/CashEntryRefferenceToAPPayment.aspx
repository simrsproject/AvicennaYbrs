<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CashEntryRefferenceToAPPayment.aspx.cs" 
    Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryRefferenceToAPPayment" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInvoicePaymentNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInvoiceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSupplierID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                Payment Date
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtInvoicePaymentDateFrom" runat="server" Width="105px">
                                <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtInvoicePaymentDateTo" runat="server" Width="105px">
                                <DateInput ID="DateInput2" runat="server" DateFormat="dd/MM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 20px" />
                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                OnClick="btnFilter_Click" ToolTip="Search" />
            <td />
        </tr>
        <tr>
            <td class="label">
                Invoice Payment No
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInvoicePaymentNo" runat="server" Width="344px" MaxLength="150"></telerik:RadTextBox>
            </td>
            <td style="width: 20px" />
                <asp:ImageButton ID="btnFilterInvoicePaymentNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnFilter_Click" ToolTip="Search" />
            <td />
        </tr>
        <tr>
            <td class="label">
                Invoice No
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="344px" MaxLength="150"></telerik:RadTextBox>
            </td>
            <td style="width: 20px" />
                <asp:ImageButton ID="btnFilterInvoiceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnFilter_Click" ToolTip="Search" />
            <td />
        </tr>
        <tr>
            <td class="label">
                Supplier
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSupplierID" Width="344px" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                    OnItemDataBound="cboSupplierID_ItemDataBound" OnItemsRequested="cboSupplierID_ItemsRequested">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                            &nbsp;(<%# DataBinder.Eval(Container.DataItem, "SupplierID")%>) </b>
                        <br />
                        <%# DataBinder.Eval(Container.DataItem, "Address") %>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td style="width: 20px" />
                <asp:ImageButton ID="btnFilterSupplierID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnFilter_Click" ToolTip="Search" />
            <td />
        </tr>
    </table>
    <telerik:RadGrid ID="grdListItem" runat="server" AllowPaging="true" AllowCustomPaging="true"
        PageSize="18" ShowFooter="True" OnNeedDataSource="grdListItem_NeedDataSource"
        OnDetailTableDataBind="grdListItem_DetailTableDataBind"
        AutoGenerateColumns="False" GridLines="Horizontal">
        <MasterTableView DataKeyNames="InvoicePaymentNo" GroupLoadMode="Client" CommandItemDisplay="None">
            <Columns>
                <telerik:GridBoundColumn DataField="InvoicePaymentNo" UniqueName="InvoicePaymentNo"
                    SortExpression="InvoicePaymentNo" HeaderText="Invoice Payment No" HeaderStyle-Width="130px" />
                <telerik:GridBoundColumn DataField="InvoiceNo" UniqueName="InvoiceNo"
                    SortExpression="InvoiceNo" HeaderText="InvoiceNo No" HeaderStyle-Width="130px" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="PaymentDate"
                    HeaderText="Payment Date" UniqueName="PaymentDate" SortExpression="PaymentDate" />
                <telerik:GridBoundColumn DataField="SupplierName" UniqueName="SupplierName" SortExpression="SupplierName"
                    HeaderText="Supplier Name" />    
                
                <telerik:GridNumericColumn DataField="PaymentAmount" HeaderText="Payment Amount" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="PaymentAmount" SortExpression="PaymentAmount" >
                    <HeaderStyle HorizontalAlign="Right" Width="100" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                
                <telerik:GridNumericColumn DataField="OtherDeduction" HeaderText="Deduction" DataType="System.Decimal" DataFormatString="{0:N2}"
                    UniqueName="OtherDeduction" SortExpression="OtherDeduction" >
                    <HeaderStyle HorizontalAlign="Right" Width="100" />
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                
                <telerik:GridBoundColumn DataField="PaymentMethodName" UniqueName="PaymentMethodName" SortExpression="PaymentMethodName"
                    HeaderText="Payment Method" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <br />
    <br />
</asp:Content>
