<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="InvoicingPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.InvoicingPickList" %>

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
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.command != null) {
                    __doPostBack("<%= grdItem.UniqueID %>", "rebind:");
                    oWnd.argument = null;
                }
            }

            function LoadPajak(sender, eventArgs) {
                var code = prompt("Scan QR code ePajak", "");
                if (code != null) {
                    $.ajax({
                        type: "POST",
                        url: "../../../../WebService/ePajak.asmx/LoadPajak",
                        data: "{'url':'" + code + "'}", // if ur method take parameters
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            var result = response.d;
                            if (result != '') sender.set_value(result);
                        },
                        dataType: "json",
                        failure: function (response) {
                            var result = response.d;
                        }
                    });
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="750px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" Title="Purchase Order Receive" ID="winItemTransaction"
        OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterSRPurchaseCategorization">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPurchaseCategorization" runat="server" Text="Inventory Category"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPurchaseCategorization" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:ImageButton ID="btnFilterSRPurchaseCategorization" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
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
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%#Eval("IsEdit")%>'></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                    SortExpression="TransactionNo" HeaderStyle-Width="132px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="75px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="InvoiceSuppNo" HeaderText="Invoice Supp No."
                    UniqueName="InvoiceSuppNo" SortExpression="InvoiceSuppNo" HeaderStyle-Width="250px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn HeaderStyle-Width="75px" DataField="InvoiceSupplierDate"
                    HeaderText="Invoice Supp. Date" UniqueName="InvoiceSupplierDate" SortExpression="InvoiceSupplierDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Reference No" UniqueName="ReferenceNo"
                    SortExpression="ReferenceNo" HeaderStyle-Width="132px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn DataField="ChargesAmount" HeaderText="Amount" UniqueName="ChargesAmount"
                    SortExpression="ChargesAmount" DataFormatString="{0:n2}" HeaderStyle-Width="110px"
                    Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="TaxAmount" HeaderText="PPn" UniqueName="TaxAmount"
                    SortExpression="TaxAmount" DataFormatString="{0:n2}" Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="PPH22Amount" HeaderText="PPh 22" UniqueName="PPH22Amount"
                    SortExpression="PPH22Amount" DataFormatString="{0:n2}" Aggregate="Sum" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="PPH23Amount" HeaderText="PPh 23" UniqueName="PPH23Amount"
                    SortExpression="PPH23Amount" DataFormatString="{0:n2}" Aggregate="Sum" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridCalculatedColumn HeaderStyle-Width="110px" HeaderText="Pph" UniqueName="Pph"
                    DataType="System.Double" DataFields="PPH22Amount,PPH23Amount,PphAmount" SortExpression="Total"
                    Expression="{0}+{1}+{2}" FooterStyle-HorizontalAlign="Right" Aggregate="Sum"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="false" />
                <telerik:GridNumericColumn DataField="PphAmount" HeaderText="Basic PPh Calc." UniqueName="PphAmount"
                    SortExpression="PphAmount" DataFormatString="{0:n2}" Aggregate="Sum" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="StampAmount" HeaderText="Stamp / Shipping"
                    UniqueName="StampAmount" SortExpression="StampAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="DownPaymentAmount" HeaderText="Down Payment"
                    UniqueName="DownPaymentAmount" SortExpression="DownPaymentAmount" DataFormatString="{0:n2}"
                    Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn DataField="OtherDeduction" HeaderText="Other Deduction"
                    UniqueName="OtherDeduction" SortExpression="OtherDeduction" DataFormatString="{0:n2}"
                    Aggregate="Sum">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                    <FooterStyle HorizontalAlign="Right" />
                </telerik:GridNumericColumn>
                <telerik:GridCalculatedColumn HeaderStyle-Width="110px" HeaderText="Total" UniqueName="Total"
                    DataType="System.Double" DataFields="ChargesAmount,TaxAmount,PPH22Amount,PPH23Amount,StampAmount,OtherDeduction,DownPaymentAmount,PphAmount"
                    SortExpression="Total" Expression="{0}+{1}-{2}-{3}+{4}-{5}-{6}-{7}" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                    DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Tax Serial No"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtInvoiceSN" runat="server" Width="90px" DbValue='<%#Eval("InvoiceSN")%>'
                            ShowButton="true" ClientEvents-OnButtonClick="LoadPajak" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Tax Invoice Date"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadDatePicker runat="server" ID="txtTaxInvoiceDate" Width="105px" DateInput-DateFormat="dd/MM/yyyy"
                            DbValue='<%#Eval("TaxInvoiceDate")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <a href="#" onclick="viewItemTransaction('<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>'); return false;">
                            <img src="../../../../Images/Toolbar/edit16.png" border="0" title="Transaction Item List" /></a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
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
</asp:Content>
