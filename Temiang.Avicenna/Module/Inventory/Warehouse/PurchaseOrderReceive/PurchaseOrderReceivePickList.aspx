<%@ Page Title="Purchase Order Outstanding" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="PurchaseOrderReceivePickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.PurchaseOrderReceivePickList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdDetail.UniqueID%>", "rebind:" + args.getDataKeyValue("TransactionNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchOrderNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSupplier">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchServiceUnitReq">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTypesOfTaxes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel runat="server" ID="clpPanel1" Title="Purchase Order Outstanding">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOrderDate" runat="server" Text="Order Date" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterOrder_Click" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Order No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchOrderNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterOrder_Click" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplierID" runat="server" Text="Supplier Name" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSupplierName" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboSupplierName_ItemDataBound"
                                    OnItemsRequested="cboSupplierName_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchSupplier" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterOrder_Click" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Purchasing Unit" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitName" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboServiceUnitName_ItemDataBound"
                                    OnItemsRequested="cboServiceUnitName_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterOrder_Click" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitReqID" runat="server" Text="Request Unit" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitReqName" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboServiceUnitName_ItemDataBound"
                                    OnItemsRequested="cboServiceUnitReqName_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchServiceUnitReq" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterOrder_Click" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblTypesOfTaxes" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="true">Exclude Tax</asp:ListItem>
                                    <asp:ListItem>Include Tax</asp:ListItem>
                                    <asp:ListItem>No Tax</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchTypesOfTaxes" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilterOrder_Click" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="grdList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdList_NeedDataSource"
            AllowPaging="True" AutoGenerateColumns="False">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
                PageSize="10">
                <Columns>
                    <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Order No" UniqueName="TransactionNo"
                        SortExpression="TransactionNo">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                        SortExpression="TransactionDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="SupplierName" HeaderText="Supplier Name" UniqueName="SupplierName"
                        SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-Width="300px" />
                    <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="Purchasing Unit"
                        UniqueName="FromServiceUnit" SortExpression="FromServiceUnit">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                        SortExpression="Notes">
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="InventoryCategory" HeaderText="Inventory Category" UniqueName="InventoryCategory"
                        SortExpression="InventoryCategory">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Request No" UniqueName="ReferenceNo"
                        SortExpression="ReferenceNo">
                        <HeaderStyle HorizontalAlign="Left" Width="140px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ServiceUnitRequest" HeaderText="Request Unit"
                        UniqueName="RequestServiceUnit" SortExpression="RequestServiceUnit">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsInventoryItem"
                        HeaderText="Inventory Item" UniqueName="IsInventoryItem" SortExpression="IsInventoryItem"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsConsignment" HeaderText="Consignment"
                        UniqueName="IsConsignment" SortExpression="IsConsignment" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnRowSelected="RowSelected" />
            </ClientSettings>
        </telerik:RadGrid>
    </cc:CollapsePanel>
    <asp:Panel ID="Panel1" runat="server" Height="4px" />
    <cc:CollapsePanel runat="server" ID="CollapsePanel1" Title="Detail Item">
        <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnNeedDataSource="grdDetail_NeedDataSource" OnPageIndexChanged="grdDetail_PageIndexChanged"
            AllowPaging="False">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                <Columns>
                    <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                    runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkIsSelect" Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ItemID" HeaderText="Item ID"
                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                        SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="FabricName" HeaderText="Factory" UniqueName="FabricName"
                        SortExpression="FabricName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ConversionFactor"
                        HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QtyPO" HeaderText="PO Qty"
                        UniqueName="QtyPO" SortExpression="QtyPO" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QuantityFinish" HeaderText="Received"
                        UniqueName="QuantityFinish" SortExpression="QuantityFinish" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                        UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Price" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="txtPrice">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="90px" DbValue='<%#Eval("Price")%>'
                                NumberFormat-DecimalDigits="2" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsDiscountInPercent"
                        HeaderText="Disc In %" UniqueName="IsDiscountInPercent" SortExpression="IsDiscountInPercent"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc In %" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="chkIsDiscInPercent">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsDiscInPercent" runat="server" Width="50px" Checked='<%#Eval("IsDiscountInPercent")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount1Percentage"
                        HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                        Visible="false" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc 1 (%)" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="txtDisc1">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDisc1" runat="server" Width="50px" DbValue='<%#Eval("Discount1Percentage")%>'
                                NumberFormat-DecimalDigits="2" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount2Percentage"
                        HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                        Visible="false" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc 2 (%)" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="txtDisc2">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDisc2" runat="server" Width="50px" DbValue='<%#Eval("Discount2Percentage")%>'
                                NumberFormat-DecimalDigits="2" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc. Amount"
                        UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Disc Amount" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="txtDiscAmt">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDiscAmt" runat="server" Width="90px" DbValue='<%#Eval("Discount")%>'
                                NumberFormat-DecimalDigits="2" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Receive Qty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="txtQtyRecv">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQtyRecv" runat="server" Width="60px" DbValue='<%#Eval("QtyRecv")%>'
                                NumberFormat-DecimalDigits="2" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Receive Qty" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="False" UniqueName="txtQtyRecv2">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQtyRecv2" runat="server" Width="60px" DbValue='<%#Eval("QtyRecv")%>'
                                NumberFormat-DecimalDigits="2" ReadOnly="True" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QtyPendingX" HeaderText="Receive Qty Pending"
                        UniqueName="QtyPendingX" SortExpression="QtyPendingX" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SRItemUnit" HeaderText="Unit"
                        UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Batch No" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="txtBatchNumber">
                        <ItemTemplate>
                            <telerik:RadTextBox ID="txtBatchNumber" runat="server" Width="80px" DbValue='<%#Eval("BatchNumber")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="110px" HeaderText="Expired Date" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="txtExpiredDate">
                        <ItemTemplate>
                            <telerik:RadDatePicker runat="server" ID="txtExpiredDate" Width="100px" DateInput-DateFormat="dd/MM/yyyy"
                                DbValue='<%#Eval("ExpiredDate")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Qty Pending" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="False" UniqueName="txtQtyPending">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQtyPending" runat="server" Width="60px" DbValue='<%#Eval("QtyPending")%>'
                                NumberFormat-DecimalDigits="2" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Price" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="False" UniqueName="txtPriceOri">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtPriceOri" runat="server" Width="90px" DbValue='<%#Eval("Price")%>'
                                NumberFormat-DecimalDigits="2" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Taxable" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" UniqueName="chkIsTaxable">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsTaxable" runat="server" Width="50px" Checked='<%#Eval("IsTaxable")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Consignment" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="False" UniqueName="chkIsConsignment">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsConsignment" runat="server" Width="50px" Checked='<%#Eval("IsConsignment")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Receive Qty Pending" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="false" UniqueName="txtQtyPendingX">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQtyPendingX" runat="server" Width="60px" DbValue='<%#Eval("QtyPendingX")%>'
                                NumberFormat-DecimalDigits="2" ReadOnly="true" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </cc:CollapsePanel>
</asp:Content>
