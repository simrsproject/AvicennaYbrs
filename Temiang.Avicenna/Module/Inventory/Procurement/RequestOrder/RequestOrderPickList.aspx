<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="RequestOrderPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.RequestOrderPickList"
    Title="Item With Stock Less Than Minimum Stock" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <fieldset>
        <legend>By Stock Total</legend>

        <asp:ImageButton ID="btnByStockLocation" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
            OnClick="btnByStockTotal_Click" ToolTip="Search" />
    </fieldset>

    <asp:Panel runat="server" ID="pnlFilterStockGroup">
        <fieldset>
            <legend>By Stock Group</legend>
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Stock Group"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRStockGroup" Height="190px" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td style="text-align: left">
                        <asp:ImageButton ID="btnByStockGroup" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                            OnClick="btnByStockGroup_Click" ToolTip="Search" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    <fieldset>
        <legend>By Sales History</legend>
        <table width="100%">
            <tr>
                <td style="width: 95%">
                    <table>
                        <tr>
                            <td class="label" style="width: 100px;">Average sales in
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txtPorBaseSalesDay" Width="60px" NumberFormat-DecimalDigits="0" MinValue="1" Value="30">
                                </telerik:RadNumericTextBox>&nbsp;days
                            </td>
                            <td>&nbsp;</td>
                            <td class="label" style="width: 60px;">Stock for
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txtPorForStockDay" Width="60px" NumberFormat-DecimalDigits="0" MinValue="1" Value="7">
                                </telerik:RadNumericTextBox>&nbsp;days
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkIsIgnoreBalance" Text="Ignore Balance" />
                            </td>
                            <td>&nbsp;</td>
                            <td class="label" style="width: 60px;">Item Group
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboItemGroupID_ItemDataBound"
                                    OnItemsRequested="cboItemGroupID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 10 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td>&nbsp;</td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnByAvgSales" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnByAvgSales_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5%">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Toolbar/imp_exp_excel16.png"
                                    OnClick="btnPrint_Click" ToolTip="Export To Excel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
        OnNeedDataSource="grdDetail_NeedDataSource" OnPageIndexChanged="grdDetail_PageIndexChanged">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" ClientDataKeyNames="ItemID">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn DataField="ConversionFactor" UniqueName="ConversionFactor"
                    SortExpression="ConversionFactor" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Minimum" HeaderText="Min"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Maximum" HeaderText="Max"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Balance" HeaderText="Balance"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="BalanceTotal" HeaderText="Balance Total"
                    UniqueName="BalanceTotal" SortExpression="BalanceTotal" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Item Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="PurcUnitBalance" HeaderText="Suggest in Purc. Unit"
                    UniqueName="PurcUnitBalance" SortExpression="PurcUnitBalance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn UniqueName="QtyOrderColumn" HeaderStyle-Width="80px"
                    HeaderText="Request" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQtyOrder" runat="server" Width="90%" DbValue='<%# Eval("QtyOrder") %>'
                            NumberFormat-DecimalDigits="0" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="QtyOrderColumn2" HeaderStyle-Width="80px"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Request" HeaderStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQtyOrder2" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyOrder")) %>'
                            MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyOrder")) %>'
                            Width="90%">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="Unit" HeaderText="Purchase Unit"
                    UniqueName="Unit" SortExpression="Unit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                    UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Price" HeaderText="Price"
                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Discount1Percentage"
                    HeaderText="Disc 1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="Discount2Percentage"
                    HeaderText="Disc 2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
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
