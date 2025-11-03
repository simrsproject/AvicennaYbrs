<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CashEntryRefferenceToPOReturn.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2.CashEntryRefferenceToPOReturn" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%= grdDetail.UniqueID %>", args.getDataKeyValue("TransactionNo"));
            }

            function viewItemTransaction(transNo) {
                var oWnd = $find("<%= winItemTransaction.ClientID %>");
                oWnd.setUrl('InvoicingPickListUpdate.aspx?tno=' + transNo);

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.command != null) {
                    __doPostBack("<%= grdItem.UniqueID %>", "rebind:");
                    oWnd.argument = null;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winItemTransaction"
        OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSupplierID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Transaction Date
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtReturnDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnFilterTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Transaction No
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="344px" MaxLength="20"/>
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnFilterTransactionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Supplier Name
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSupplierID" runat="server" Width="344px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:ImageButton ID="btnFilterSupplierID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="False" AllowMultiRowSelection="true"
        ShowFooter="True">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px"
                    Visible="false">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%#Eval("IsEdit")%>'></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo" HeaderStyle-Width="130px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="InvoiceSuppNo" HeaderText="Invoice Supp No."
                    UniqueName="InvoiceSuppNo" SortExpression="InvoiceSuppNo">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="InvoiceSupplierDate"
                    HeaderText="Invoice Supp. Date" UniqueName="InvoiceSupplierDate" SortExpression="InvoiceSupplierDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Reference No" UniqueName="ReferenceNo"
                    SortExpression="ReferenceNo" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn DataField="ChargesAmount" HeaderText="Amount" UniqueName="ChargesAmount"
                    SortExpression="ChargesAmount" DataFormatString="{0:n2}" Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="TaxAmount" HeaderText="PPn" UniqueName="TaxAmount"
                    SortExpression="TaxAmount" DataFormatString="{0:n2}" Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="PPH22Amount" HeaderText="PPh 22" UniqueName="PPH22Amount"
                    SortExpression="PPH22Amount" DataFormatString="{0:n2}" Aggregate="Sum" Visible="False">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="PPH23Amount" HeaderText="PPh 23" UniqueName="PPH23Amount"
                    SortExpression="PPH23Amount" DataFormatString="{0:n2}" Aggregate="Sum" Visible="False">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="PphAmount" HeaderText="PPh" UniqueName="PphAmount"
                    SortExpression="PphAmount" DataFormatString="{0:n2}" Aggregate="Sum" Visible="False">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="StampAmount" HeaderText="Stamp / Shipping"
                    UniqueName="StampAmount" SortExpression="StampAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DownPaymentAmount" HeaderText="Down Payment"
                    UniqueName="DownPaymentAmount" SortExpression="DownPaymentAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="OtherDeduction" HeaderText="Other Deduction"
                    UniqueName="OtherDeduction" SortExpression="OtherDeduction" DataFormatString="{0:n2}"
                    Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Right" Width="120px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridCalculatedColumn HeaderStyle-Width="110px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="ChargesAmount,TaxAmount,PPH22Amount,PPH23Amount,StampAmount,OtherDeduction,DownPaymentAmount,PphAmount"
                    SortExpression="Total" Expression="{0}+{1}+{2}+{3}+{4}-{5}-{6}-{7}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true" AllowRowsDragDrop="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowSelected="RowSelected" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:Panel ID="Panel1" runat="server" Height="4px" />
    <cc:CollapsePanel runat="server" ID="CollapsePanel2" Title="Detail Item">
        <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnNeedDataSource="grdDetail_NeedDataSource" OnPageIndexChanged="grdDetail_PageIndexChanged"
            AllowPaging="True">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ConversionFactor"
                        HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Quantity" HeaderText="Qty"
                        UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                        UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount1Percentage"
                        HeaderText="Disc #1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount2Percentage"
                        HeaderText="Disc #2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                        UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                        UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BatchNumber" HeaderText="Batch Number"
                        UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExpiredDate" HeaderText="Expire Date"
                        UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </cc:CollapsePanel>
    <br />
    <br />
</asp:Content>
