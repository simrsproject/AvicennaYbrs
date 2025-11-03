<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="InvoicingPickListUpdate.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Payable.InvoicingPickListUpdate" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblInvoiceSuppNo" runat="server" Text="Invoice Supplier No / Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtInvoiceSuppNo" runat="server" Width="200px" MaxLength="100" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>
                                    <telerik:RadDatePicker ID="txtInvoiceSupplierDate" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblContractNo" runat="server" Text="Contract No / Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtContractNo" runat="server" Width="200px" MaxLength="50" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>
                                    <telerik:RadDatePicker ID="txtContractDate" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblCheckNo" runat="server" Text="Check No / Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtCheckNo" runat="server" Width="200px" MaxLength="50" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>
                                    <telerik:RadDatePicker ID="txtCheckDate" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" AutoGenerateColumns="False" GridLines="None"
        OnNeedDataSource="grdItem_NeedDataSource" OnItemCreated="grdItem_ItemCreated"
        AllowPaging="False">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo, ItemID, ConversionFactor">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="ConversionFactor"
                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Quantity" HeaderText="Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="90px" DbValue='<%#Eval("Price")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsDiscountInPercent"
                    HeaderText="Disc In %" UniqueName="IsDiscountInPercent" SortExpression="IsDiscountInPercent"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc In %" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsDiscInPercent" runat="server" Width="50px" Checked='<%#Eval("IsDiscountInPercent")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"
                    Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Disc 1 (%)" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
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
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDisc2" runat="server" Width="50px" DbValue='<%#Eval("Discount2Percentage")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc. Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Disc Amount" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtDiscAmt" runat="server" Width="90px" DbValue='<%#Eval("Discount")%>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BatchNumber" HeaderText="Batch Number"
                    UniqueName="BatchNumber" SortExpression="BatchNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExpiredDate" HeaderText="Expire Date"
                    UniqueName="ExpiredDate" SortExpression="ExpiredDate" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsTaxable"
                    HeaderText="PPn" UniqueName="IsTaxable" SortExpression="IsTaxable"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="PPn" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsTaxable" runat="server" Width="50px" Checked='<%#Eval("IsTaxable")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="PPh" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsTaxablePph" runat="server" Width="50px" Checked='<%#Eval("IsTaxablePph")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="200px" HeaderText="PPh" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cboSRPph" runat="server" Width="150px" EnableLoadOnDemand="true"
                            MarkFirstMatch="true" HighlightTemplatedItems="true">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SRPph" UniqueName="SRPph" Visible="False" />
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="False" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
