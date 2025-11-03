<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ServiceUnitCorrectionItem.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitCorrectionItem" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%= grdTransChargesItem.UniqueID %>", args.getDataKeyValue("TransactionNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransCharges" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransCharges" />
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransCharges">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransCharges" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransCharges">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdTransChargesItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTransChargesItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Transaction Header">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblItemID" runat="server" Text="Item" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboItemID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                                    OnItemsRequested="cboItemID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchItem" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="grdTransCharges" runat="server" OnNeedDataSource="grdTransCharges_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" AllowPaging="True">
            <HeaderContextMenu>
            </HeaderContextMenu>
            <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo">
                <Columns>
                    <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                        SortExpression="TransactionNo">
                        <HeaderStyle HorizontalAlign="Center" Width="110px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn DataField="TransactionDate" HeaderText="Transaction Date"
                        UniqueName="TransactionDate" SortExpression="TransactionDate" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn DataField="ExecutionDate" HeaderText="Execution Date"
                        UniqueName="ExecutionDate" SortExpression="ExecutionDate" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridNumericColumn>
                    <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                        SortExpression="ServiceUnitName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                        SortExpression="RoomName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridNumericColumn>
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAutoBillTransaction"
                        HeaderText="Auto Bill" UniqueName="IsAutoBillTransaction" SortExpression="IsAutoBillTransaction"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsOrder" HeaderText="Order"
                        UniqueName="IsOrder" SortExpression="IsOrder" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                        HeaderText="User Update" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
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
    </cc:CollapsePanel>
    <asp:Panel ID="Panel1" runat="server" Height="4px" />
    <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Transaction Detail">
        <telerik:RadGrid ID="grdTransChargesItem" runat="server" OnNeedDataSource="grdTransChargesItem_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" AllowPaging="false" AllowMultiRowSelection="true">
            <HeaderContextMenu>
            </HeaderContextMenu>
            <MasterTableView DataKeyNames="TransactionNo, SequenceNo">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                runat="server"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="detailChkbox" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" SortExpression="TransactionNo"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ItemID" UniqueName="ItemID" SortExpression="ItemID"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                        SortExpression="Notes">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Physician"
                        UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateColumn1" HeaderStyle-Width="75px"
                        ItemStyle-HorizontalAlign="Center" HeaderText="Qty" HeaderStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQty" runat="server" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ChargeQuantity")) %>'
                                Width="50px" MinValue="0" MaxValue='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ChargeQuantity")) %>'>
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="SRItemUnit" HeaderText="Unit" UniqueName="SRItemUnit"
                        SortExpression="SRItemUnit">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                        SortExpression="Price" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn DataField="DiscountAmount" HeaderText="Discount" UniqueName="DiscountAmount"
                        SortExpression="DiscountAmount" DataFormatString="{0:n2}">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn DataField="CitoAmount" HeaderText="Cito" UniqueName="CitoAmount"
                        SortExpression="CitoAmount" DataFormatString="{0:n2}" Aggregate="Count">
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridNumericColumn>
                    <telerik:GridNumericColumn DataField="Total" HeaderText="Total" UniqueName="Total"
                        SortExpression="Total" DataFormatString="{0:n2}" Aggregate="Sum" FooterAggregateFormatString="{0:n2}">
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridNumericColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="true">
                <Resizing AllowColumnResize="True" />
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </cc:CollapsePanel>
</asp:Content>
