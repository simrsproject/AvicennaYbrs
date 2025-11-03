<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="VerificationList2.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.VerificationList2" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "process":
                        __doPostBack("<%= grdList.UniqueID %>", 'process');
                        break;
                    case "payment":
                        __doPostBack("<%= grdList.UniqueID %>", 'payment');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchInvoice">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSupplier">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPurchaseCategorization">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnShowAll">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterVerifPlanningPaymentDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterVerifPaymentOrdersNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                    <telerik:AjaxUpdatedControl ControlID="RadToolBar2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPaymentOrdersDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInvoiceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
    </telerik:RadAjaxPanel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%">
            <tr>
                <td class="label">Invoice Due Date
                </td>
                <td class="entry">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                            <td>&nbsp;-&nbsp;
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="20px">
                    <asp:ImageButton ID="btnSearchInvoice" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnSearchInvoice_Click" />
                </td>
                <td />
            </tr>
            <tr>
                <td class="label">Supplier Name
                </td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboSupplierID" runat="server" AllowCustomText="false" Filter="Contains"
                        Width="300px" />
                </td>
                <td width="20px">
                    <asp:ImageButton ID="btnSearchSupplier" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnSearchInvoice_Click" />
                </td>
                <td />
            </tr>

        </table>
    </cc:CollapsePanel>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process" Value="process" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
            <telerik:RadToolBarButton IsSeparator="true" />
            <telerik:RadToolBarButton runat="server" Text="Payment Orders" Value="payment" ImageUrl="~/Images/Toolbar/mail16.png"
                HoveredImageUrl="~/Images/Toolbar/mail16.png" DisabledImageUrl="~/Images/Toolbar/mail16.png" />
        </Items>
    </telerik:RadToolBar>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="MultiPage1" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding" PageViewID="pgDetail" Selected="True" />
            <telerik:RadTab runat="server" Text="Payment Orders" PageViewID="pgPaymentOrders" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="MultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgDetail" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table>
                            <asp:Panel runat="server" ID="pnlPaymentOrders">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPlanningPaymentDate" runat="server" Text="Planning Payment Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtPlanningPaymentDate" runat="server" Width="110px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td width="30px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblInvoicePayment" runat="server" Text="Invoice Payment"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSRInvoicePayment" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            HighlightTemplatedItems="true" OnItemDataBound="cboSRInvoicePayment_ItemDataBound" OnItemsRequested="cboSRInvoicePayment_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 20 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="30px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBank" runat="server" Text="Bank"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboBankID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            HighlightTemplatedItems="true" OnItemDataBound="cboBankID_ItemDataBound" OnItemsRequested="cboBankID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 20 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="30px"></td>
                                    <td></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlVerif">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPlanningPaymentDate2" runat="server" Text="Planning Payment Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtPlanningPaymentDateFrom" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>&nbsp;-&nbsp;</td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtPlanningPaymentDateTo" runat="server" Width="110px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnFilterVerifPlanningPaymentDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchInvoice_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPaymentOrdersNo" runat="server" Text="Payment Orders No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPaymentOrdersNo" runat="server" Width="300px" MaxLength="20" />
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnFilterVerifPaymentOrdersNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchInvoice_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPurchaseCategorization" runat="server" Text="Inventory Category"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRPurchaseCategorization" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="30px">
                                    <asp:ImageButton ID="btnFilterPurchaseCategorization" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnSearchInvoice_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <asp:Panel runat="server" ID="pnlVerif2">
                                <tr>
                                    <td class="label" />
                                    <td class="entry">
                                        <asp:CheckBox ID="chkShowAll" runat="server" Text="Show All (with history)" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnShowAll" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearchInvoice_Click" />
                                    </td>
                                    <td />
                                </tr>
                            </asp:Panel>
                        </table>
                    </td>
                    <td width="50%"></td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdList_NeedDataSource"
                AllowPaging="True" PageSize="15" OnDetailTableDataBind="grdList_DetailTableDataBind"
                OnItemCreated="grdList_ItemCreated" OnItemCommand="grdList_ItemCommand">
                <MasterTableView DataKeyNames="InvoiceNo,PaymentOrderNo" GroupLoadMode="client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="SupplierName" HeaderText="Supplier "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="SupplierName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="checkbox" HeaderText="Process">
                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="processChkBox" runat="server" Checked="false" Enabled="true" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="datepicker" HeaderText="Aging Date" Visible="false">
                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <telerik:RadDatePicker ID="txtAgingDate" runat="server" Width="100%" SelectedDate='<%# DataBinder.Eval(Container.DataItem, "AgingDate") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="SupplierID" UniqueName="SupplierID" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceNo" HeaderText="Invoice No"
                            UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="InvoiceDate" HeaderText="Invoice Date"
                            UniqueName="InvoiceDate" SortExpression="InvoiceDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="VerifyDate" HeaderText="Verify Date"
                            UniqueName="VerifyDate" SortExpression="VerifyDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="planningdate" HeaderText="Planning Payment Date">
                            <HeaderStyle Width="150px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <telerik:RadDatePicker ID="txtPlanningDate" runat="server" Width="100px" SelectedDate='<%# (Eval("InvoicePaymentPlanDate") != null && Eval("InvoicePaymentPlanDate") is DateTime) ? Convert.ToDateTime(Eval("InvoicePaymentPlanDate")) : (DateTime?)null %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="invoicepayment" HeaderText="Invoice Payment">
                            <HeaderStyle Width="170px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <telerik:RadComboBox ID="cboSRInvoicePayment" runat="server" Width="152px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRInvoicePayment_ItemDataBound"
                                    OnItemsRequested="cboSRInvoicePayment_ItemsRequested">
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="bank" HeaderText="Bank">
                            <HeaderStyle Width="322px" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <telerik:RadComboBox ID="cboBankID" runat="server" Width="100%" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboBankID_ItemDataBound"
                                    OnItemsRequested="cboBankID_ItemsRequested">
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="PaymentOrderNo" HeaderText="Payment Orders No"
                            UniqueName="PaymentOrderNo" SortExpression="PaymentOrderNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="InvoicePaymentPlanDate" HeaderText="Payment Plan Date"
                            UniqueName="InvoicePaymentPlanDate" SortExpression="InvoicePaymentPlanDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoicePaymentName" HeaderText="Invoice Payment"
                            UniqueName="InvoicePaymentName" SortExpression="InvoicePaymentName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BankName" HeaderText="Bank"
                            UniqueName="BankName" SortExpression="BankName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" UniqueName="banksupp" HeaderText="Supplier Bank Account No">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <telerik:RadComboBox ID="cboBankAccountNo" runat="server" Width="100%" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboBankAccountNo_ItemDataBound"
                                    OnItemsRequested="cboBankAccountNo_ItemsRequested">
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Total" HeaderText="Total Amount"
                            UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridBoundColumn DataField="SRInvoicePayment" UniqueName="SRInvoicePayment"
                            Visible="False" />
                        <telerik:GridBoundColumn DataField="BankID" UniqueName="BankID" Visible="False" />
                        <telerik:GridBoundColumn DataField="BankAccountNo" UniqueName="BankAccountNo" Visible="False" />
                        <telerik:GridBoundColumn DataField="PaymentOrderNo" UniqueName="PaymentOrderNo" Visible="False" />
                        <telerik:GridTemplateColumn />
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="center"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkValidate" runat="server" OnClientClick="javascript:if(!confirm('Are you sure to cancel verification of selected item?'))return false;"
                                    CommandName="cancel" Visible='<%# (DataBinder.Eval(Container.DataItem, "SRPayableStatus").ToString()) == "1" %>'><img src="../../../../Images/cancel16.png" border="0" /></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="InvoiceNo, TransactionNo" Name="grdDetail" Width="100%"
                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="InvoiceSuppNo" HeaderText="Invoice Supplier No"
                                    UniqueName="InvoiceSuppNo" SortExpression="InvoiceSuppNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TransactionNo" HeaderText="Transaction No"
                                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="VerifyAmount" HeaderText="Amount"
                                    UniqueName="VerifyAmount" SortExpression="VerifyAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Ppn" HeaderText="PPn"
                                    UniqueName="Ppn" SortExpression="Ppn" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Pph22" HeaderText="PPh 22"
                                    UniqueName="Pph22" SortExpression="Pph22" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Pph23" HeaderText="PPh 23"
                                    UniqueName="Pph23" SortExpression="Pph23" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Pph" HeaderText="Pph"
                                    UniqueName="Pph" SortExpression="Pph" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Stamp" HeaderText="Stamp / Shipping"
                                    UniqueName="Stamp" SortExpression="Stamp" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DownPaymentAmount"
                                    HeaderText="Down Payment" UniqueName="DownPaymentAmount" SortExpression="DownPaymentAmount"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="OtherDeduction" HeaderText="Deduction"
                                    UniqueName="OtherDeduction" SortExpression="OtherDeduction" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SubTotal" HeaderText="Sub Total"
                                    UniqueName="SubTotal" SortExpression="SubTotal" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="80px" UniqueName="RoundingAmount" HeaderText="Rounding" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox id="txtRoundingAmount" runat="server" Width="80px" IncrementSettings-InterceptMouseWheel="false"></telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="SRInvoicePayment" UniqueName="SRInvoicePayment"
                                    Visible="False" />
                                <telerik:GridBoundColumn DataField="BankID" UniqueName="BankID" Visible="False" />
                                <telerik:GridBoundColumn DataField="BankAccountNo" UniqueName="BankAccountNo" Visible="False" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPaymentOrders" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPaymentOrdersDate" runat="server" Text="Payment Orders Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtPaymentOrdersDateFrom" runat="server" Width="110px">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;-&nbsp;</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtPaymentOrdersDateTo" runat="server" Width="110px">
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="30px">
                                    <asp:ImageButton ID="btnFilterPaymentOrdersDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterPaymentOrders_Click" ToolTip="Search" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnPrintPaymentOrder" runat="server" ImageUrl="~/Images/Toolbar/print16.png"
                                        OnClick="btnPrintPaymentOrders_Click" ToolTip="Print" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Invoice No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtInvoiceNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td width="30px">
                                    <asp:ImageButton ID="btnFilterInvoiceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterPaymentOrders_Click" ToolTip="Search" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList2" runat="server" AutoGenerateColumns="false" OnNeedDataSource="grdList2_NeedDataSource"
                AllowPaging="True" PageSize="15" OnItemCommand="grdList2_ItemCommand">
                <MasterTableView DataKeyNames="InvoiceNo" GroupLoadMode="client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="PaymentOrderNo" HeaderText="Payment Order No "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="PaymentOrderNo" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="Print">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="print" ToolTip='Print'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PaymentOrderNo") %>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier Name"
                            UniqueName="SupplierName" SortExpression="SupplierName" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoiceNo" HeaderText="Invoice No"
                            UniqueName="InvoiceNo" SortExpression="InvoiceNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="InvoicePaymentPlanDate" HeaderText="Plan Date"
                            UniqueName="InvoicePaymentPlanDate" SortExpression="InvoicePaymentPlanDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="InvoicePaymentName" HeaderText="Invoice Payment"
                            UniqueName="InvoicePaymentName" SortExpression="InvoicePaymentName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="BankName" HeaderText="Bank"
                            UniqueName="BankName" SortExpression="BankName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RoundingAmount" HeaderText="Rounding"
                            UniqueName="RoundingAmount" SortExpression="RoundingAmount" HeaderStyle-HorizontalAlign="Center"
                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Total" HeaderText="Total Amount"
                            UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center"
                            DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="center"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkValidate" runat="server" OnClientClick="javascript:if(!confirm('Are you sure to cancel payment orders of selected item?'))return false;"
                                    CommandName="cancel" Visible='<%# (DataBinder.Eval(Container.DataItem, "SRPayableStatus").ToString()) == "0" %>'><img src="../../../../Images/cancel16.png" border="0" /></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>

        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
