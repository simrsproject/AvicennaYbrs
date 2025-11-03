<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="SalesDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.Dispensary.SalesDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Sales No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Sales Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                DatePopupButton-Enabled="False" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Transaction Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvFromServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboFromServiceUnitID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCustomerNo" runat="server" Text="Customer"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboCustomerID" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" OnItemDataBound="cboCustomerID_ItemDataBound"
                                ValidationGroup="other" AutoPostBack="true" OnItemsRequested="cboCustomerID_ItemsRequested" OnSelectedIndexChanged="cboCustomerID_SelectedIndexChanged"
                                EmptyMessage="Select...">
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvCustomerID" runat="server" ErrorMessage="Customer ID required."
                                ValidationGroup="entry" ControlToValidate="cboCustomerID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Sales Margin Percentage"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSalesMarginPercentage" runat="server" Type="Percent"
                                Width="100px" MaxLength="5" MaxValue="999.99" MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Tax</td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUseTax" runat="server" Text="Use Tax" AutoPostBack="True" OnCheckedChanged="chkIsUseTax_CheckedChanged" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrderNo" runat="server" Text="Order No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="25" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>


                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRPaymentType" runat="server" Text="Payment Type" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRPaymentType" runat="server" Width="300px" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvSRPaymentType" runat="server" ErrorMessage="Payment Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRPaymentType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxInvoiceNo" runat="server" Text="Tax Invoice No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTaxInvoiceNo" runat="server" Width="300px" MaxLength="25" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxInvoiceDate" runat="server" Text="Tax Invoice Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTaxTransactionDate" runat="server" Width="100px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsApproved" runat="server" Text="Approved" Enabled="false" />
                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Enabled="false" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItemTransactionItem" runat="server" OnNeedDataSource="grdItemTransactionItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItemTransactionItem_UpdateCommand"
        OnDeleteCommand="grdItemTransactionItem_DeleteCommand" OnInsertCommand="grdItemTransactionItem_InsertCommand"
        AllowPaging="true" PageSize="15">
        <PagerStyle Mode="NextPrevAndNumeric" />
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo" PageSize="10" ShowFooter="false">
            <CommandItemTemplate>
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                </asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton ID="lbPickList" runat="server" OnClientClick="javascript:openWinPickList();return false;"
                    Visible="false">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/views16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Import from distribution request"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="ConversionFactor"
                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="65" DataField="Quantity" HeaderText="Qty"
                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridBoundColumn HeaderStyle-Width="75px" DataField="SRItemUnit" HeaderText="Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Discount2Percentage"
                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Disc Amount"
                    UniqueName="DiscAmount" DataType="System.Double" DataFields="Price,Discount1Percentage,Discount2Percentage, Quantity"
                    Expression="(({0}*{1}/100) + (({0} - ({0}*{1}/100)) * {2}/100)) * {3}" FooterText=" "
                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="TotalPrice2"
                    DataType="System.Double" DataFields="Price, Discount, Quantity" Expression="(({0}-{1}) * {2})"
                    FooterText=" " FooterStyle-HorizontalAlign="Right" Aggregate="Sum" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn />
            </Columns>
            <EditFormSettings UserControlName="SalesItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ItemTransactionItemEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top"></td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChargesAmount" runat="server" Text="Sales Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtChargesAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvChargesAmount" runat="server" ErrorMessage="Sales Amount required."
                                ValidationGroup="entry" ControlToValidate="txtChargesAmount" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxPercentage" runat="server" Text="Tax Percentage" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxPercentage" runat="server" Type="Percent" Width="100px"
                                MaxLength="5" MaxValue="999.99" MinValue="0" AutoPostBack="true"
                                ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTaxAmount" runat="server" Text="Tax Amount" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTaxAmount" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTotal" runat="server" Text="Total" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="16"
                                MinValue="0" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

