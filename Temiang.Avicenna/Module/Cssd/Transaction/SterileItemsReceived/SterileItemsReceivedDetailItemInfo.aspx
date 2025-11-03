<%@ Page Title="Items Package Received" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="SterileItemsReceivedDetailItemInfo.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Cssd.Transaction.SterileItemsReceivedDetailItemInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item ID" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr height="30">
                        <td class="label">
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" NumberFormat-DecimalDigits="0"
                                            ReadOnly="True" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtSRItemUnit" runat="server" Width="100px" ReadOnly="True" />
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
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource">
                    <MasterTableView DataKeyNames="ReceivedNo,ReceivedSeqNo,ItemID,ItemDetailID" ClientDataKeyNames="ItemDetailID">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ReceivedNo" HeaderText="ReceivedNo"
                                UniqueName="ReceivedNo" SortExpression="ReceivedNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ReceivedSeqNo" HeaderText="ReceivedSeqNo"
                                UniqueName="ReceivedSeqNo" SortExpression="ReceivedSeqNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ItemID"
                                UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />

                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemDetailID" HeaderText="ID"
                                UniqueName="ItemDetailID" SortExpression="ItemDetailID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="400px" DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                                SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty Package"
                                UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderText="Qty Received" UniqueName="TxtQtyReceived"
                                HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtQtyReceived" Width="80px"
                                        Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "QtyReceived")) %>'
                                        MinValue="0"
                                        MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Qty")) %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyReceived" HeaderText="Qty Received"
                                UniqueName="QtyReceived" SortExpression="QtyReceived" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />

                            <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBrokenInstrument" HeaderText="Broken Instrument"
                                UniqueName="IsBrokenInstrument" SortExpression="IsBrokenInstrument" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Broken Instrument" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" UniqueName="chkIsBrokenInstrument">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsBrokenInstrument" runat="server" Width="50px" Checked='<%#Eval("IsBrokenInstrumentX")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyReplacements" HeaderText="Qty Replacements"
                                UniqueName="QtyReplacements" SortExpression="QtyReplacements" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn HeaderText="Qty Replacements" UniqueName="TxtQtyReplacements"
                                HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtQtyReplacements" Width="80px"
                                        Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "QtyReplacementsX")) %>'
                                        MinValue="0"
                                        MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyReceived")) %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Unit"
                                UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
