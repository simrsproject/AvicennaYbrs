<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="InventoryIssueConfirmDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.InventoryIssueConfirmDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIssueNo" runat="server" Text="Issue No / Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtIssueNo" runat="server" Width="200px" ReadOnly="True" />
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" DateInput-ReadOnly="True"
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
                            <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnit" runat="server" Text="From Unit"></asp:Label>
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
                            <asp:Label ID="lblFromLocation" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFromLocationID" runat="server" Width="100px" ReadOnly="True" />
                            <asp:Label ID="lblFromLocationName" runat="server"></asp:Label>
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
                            <asp:Label ID="lblToServiceUnit" runat="server" Text="To Unit"></asp:Label>
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
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSRItemType" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" ReadOnly="True" />
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
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RequestQty" HeaderText="Issue Qty"
                    UniqueName="RequestQty" SortExpression="RequestQty" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn HeaderText="Confirmed Qty" UniqueName="ApprovedQty" HeaderStyle-HorizontalAlign="center">
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <telerik:RadNumericTextBox runat="server" ID="txtQty" Width="80px" ReadOnly="True"
                            Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Quantity")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Item Unit" UniqueName="SRItemUnit" HeaderStyle-HorizontalAlign="center">
                    <HeaderStyle Width="100px" />
                    <ItemTemplate>
                        <span>
                            <%# string.Format("{0}/{1}", DataBinder.Eval(Container.DataItem, "SRItemUnit").ToString(), Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ConversionFactor")).ToString("G29"))%></span>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
