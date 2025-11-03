<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="MergePurchaseOrderList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.MergePurchaseOrderList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "generate":
                        if (confirm('Are you sure to generate selected item?'))
                            __doPostBack("<%= grdList.UniqueID %>", 'generate');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboToServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRItemType" />
                    <telerik:AjaxUpdatedControl ControlID="cboItemGroupID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTrNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="cboSupplierID" />
                    <telerik:AjaxUpdatedControl ControlID="cboFromServiceUnitID" />
                    <telerik:AjaxUpdatedControl ControlID="cboSRItemType" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Merge Purchase Order" Value="generate"
                ImageUrl="~/Images/Toolbar/process16.png" HoveredImageUrl="~/Images/Toolbar/process16_h.png"
                DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
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
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="PO No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" MaxLength="20" Width="300px" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterTrNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierID" runat="server" Text="Supplier"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSupplierID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" Enabled="False" OnItemDataBound="cboSupplierID_ItemDataBound"/>
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFromServiceUnit" runat="server" Text="Purchasing Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" Enabled="False" OnItemDataBound="cboFromServiceUnitID_ItemDataBound"/>
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRItemType" runat="server" Text="Item Type" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" Enabled="False" OnItemDataBound="cboSRItemType_ItemDataBound"/>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="PO Outstanding" PageViewID="pgPO" Selected="True">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgPO" runat="server" Selected="True">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                AutoGenerateColumns="false" OnDetailTableDataBind="grdList_DetailTableDataBind"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo">
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
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Order Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="SupplierName" HeaderText="Supplier Name"
                            UniqueName="SupplierName" SortExpression="SupplierName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FServiceUnitID" HeaderText="Purchasing Unit"
                            UniqueName="FServiceUnitID" SortExpression="FServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PurchaseOrderType"
                            HeaderText="Order Type" UniqueName="PurchaseOrderType" SortExpression="PurchaseOrderType"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="ReferenceNo"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                            ShowFooter="true" AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Item Name" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ConversionFactor"
                                    HeaderText="Conversion" UniqueName="ConversionFactor" SortExpression="ConversionFactor"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="50" DataField="Quantity" HeaderText="Qty"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount1Percentage"
                                    HeaderText="Disc #1 (%)" UniqueName="Discount1Percentage" SortExpression="Discount1Percentage"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Discount2Percentage"
                                    HeaderText="Disc #2 (%)" UniqueName="Discount2Percentage" SortExpression="Discount2Percentage"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Discount" HeaderText="Disc Amount"
                                    UniqueName="Discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="TotalPrice"
                                    DataType="System.Double" DataFields="Price, Discount, Quantity" Expression="(({0} - {1}) * {2})"
                                    FooterText=" " FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true"
                                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
