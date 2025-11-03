<%@ Page Title="Request Order Editor" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="RequestOrderEditorDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.RequestOrderEditorDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPRNo" runat="server" Text="Request No / Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPRNo" runat="server" Width="200px" ReadOnly="True" />
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" DateInput-ReadOnly="True"
                                            DatePopupButton-Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblToServiceUnit" runat="server" Text="Request From"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFromServiceUnitID" runat="server" Width="100px" ReadOnly="True" />
                            <asp:Label ID="lblFromServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitCostID" runat="server" Text="Cost For Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitCostID" runat="server" Width="100px" ReadOnly="True" />
                            <asp:Label ID="lblServiceUnitCostName" runat="server"></asp:Label>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Purchasing Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtToServiceUnitID" runat="server" Width="100px" ReadOnly="True" />
                            <asp:Label ID="lblToServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="TransactionNo, SequenceNo" GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="300px" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Minimum" HeaderText="Min Qty"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Maximum" HeaderText="Max Qty"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="BalanceSG" HeaderText="Balance SG"
                    UniqueName="BalanceSG" SortExpression="BalanceSG" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Balance" HeaderText="Balance Loc"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="BalanceTotal" HeaderText="Balance Total"
                    UniqueName="BalanceTotal" SortExpression="BalanceTotal" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRMasterBaseUnit" HeaderText="Unit"
                    UniqueName="SRMasterBaseUnit" SortExpression="SRMasterBaseUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RequestQty" HeaderText="Request Qty"
                    UniqueName="RequestQty" SortExpression="RequestQty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn HeaderText="Approved Qty" UniqueName="ApprovedQty" HeaderStyle-HorizontalAlign="center">
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <telerik:RadNumericTextBox runat="server" ID="txtQty" Width="80px" ReadOnly='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsClosed")) %>'
                            Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Quantity")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Req. Unit" UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="center">
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <span>
                            <%# string.Format("{0}/{1}", DataBinder.Eval(Container.DataItem, "SRItemUnit").ToString(), Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ConversionFactor")).ToString("G29"))%></span>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount1Percentage"
                    HeaderText="Disc #1(%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Discount2Percentage"
                    HeaderText="Disc #2(%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount" HeaderText="Disc Amount"
                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
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
