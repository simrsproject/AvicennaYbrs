<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DistributionRequestByUsedPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DistributionRequestByUsedPickList"
    Title="Pick from Usage History" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnProccess">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnByAvgSales">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <fieldset>
        <legend>By Sales History</legend>
        <table>
            <tr runat="server" id="trHist1">
                <td class="label">Period
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                </td>
                <td style="width: 30px; text-align: center;">To
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />
                </td>
                <td class="label" style="text-align: right">Divide By&nbsp;
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox ID="txtDivideBy" runat="server" Width="50px" MaxLength="3"
                        MaxValue="999" MinValue="0" NumberFormat-DecimalDigits="0" Value="1" />
                </td>
                <td class="label" style="text-align: right">Mark Up (%)&nbsp;
                </td>
                <td class="entry">
                    <telerik:RadNumericTextBox ID="txtMarkUp" runat="server" Width="50px" MaxLength="5"
                        MaxValue="1000" MinValue="0" NumberFormat-DecimalDigits="0" Value="10" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnProccess" Text="Query" OnClick="btnProccess_Click" />
                </td>
                <td></td>
            </tr>
            <tr runat="server" id="trHist2">
                <td style="width: 95%">
                    <table>
                        <tr>
                            <td class="label" style="width: 100px;">Average sales in
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txtBaseSalesDay" Width="60px" NumberFormat-DecimalDigits="0" MinValue="1" Value="30">
                                </telerik:RadNumericTextBox>&nbsp;days
                            </td>
                            <td>&nbsp;</td>
                            <td class="label" style="width: 60px;">Stock for
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txtForStockDay" Width="60px" NumberFormat-DecimalDigits="0" MinValue="1" Value="7">
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
                                    OnItemsRequested="cboItemGroupID_ItemsRequested" >
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
                </td>
            </tr>
        </table>
    </fieldset>
    <telerik:RadGrid ID="grdDetail" runat="server" PageSize="15" AutoGenerateColumns="False"
        GridLines="None" OnNeedDataSource="grdDetail_NeedDataSource" AllowPaging="True">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" PageSize="13">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px"
                    AllowFiltering="False">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%# GetInt(DataBinder.Eval(Container.DataItem,"IsSelect"))==1%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="BalanceCw" HeaderText="To Location Balance"
                    UniqueName="BalanceCw" SortExpression="BalanceCw" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Minimum" HeaderText="Min"
                    UniqueName="Minimum" SortExpression="Minimum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Maximum" HeaderText="Max"
                    UniqueName="Maximum" SortExpression="Maximum" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Balance" HeaderText="Balance"
                    UniqueName="Balance" SortExpression="Balance" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyUse" HeaderText="Used Hist"
                    UniqueName="QtyUse" SortExpression="QtyUse" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Request Qty" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQtyInput" runat="server" Width="70px" DbValue='<%#DataBinder.Eval(Container.DataItem,"QtyInput")%>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="100px"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Request Qty" HeaderStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQtyInput2" runat="server" NumberFormat-DecimalDigits="2"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyInput")) %>'
                            MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "QtyMax")) %>'
                            Width="80px">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRItemUnit" HeaderText="Item Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                    UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ConversionFactor"
                    HeaderText="Conversion Factor" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
