<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="InventoryIssueConfirmList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.InventoryIssueConfirmList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.rebind != null) {
                    __doPostBack("<%= grdList.UniqueID %>", "rebind");
                }
            }

            function openWinDialog(id) {
                var oWnd = $find("<%= winDialog.ClientID %>");

                oWnd.setUrl('InventoryIssueConfirmDialog.aspx?id=' + id);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterIssueNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterIncludeConfirmed">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterToServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterFromServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSRItemType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="onClientClose"
        ID="winDialog">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDate" runat="server" Text="Issue Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromDate" runat="server" Width="100px" />
                                        </td>
                                        <td>
                                            &nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblIssueNo" runat="server" Text="Issue No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtIssueNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterIssueNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                            </td>
                            <td class="entry">
                                <asp:CheckBox ID="chkIncludeConfirmed" Text="Include Confirmed" runat="server" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterIncludeConfirmed" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnit" runat="server" Text="From Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboFromServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterFromServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblToServiceUnit" runat="server" Text="To Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboToServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterToServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSearchSRItemType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" AllowPaging="true" PageSize="15"
        AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="TransactionNo, FromLocationID, SRItemType" GroupLoadMode="Client">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openWinDialog('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Confirm\" /></a>", DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Issue No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ReferenceNo" HeaderText="Reference No"
                    UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="FServiceUnitID" HeaderText="From Unit" UniqueName="FServiceUnitID"
                    SortExpression="FServiceUnitID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="FLocationID" HeaderText="Location" UniqueName="FLocationID"
                    SortExpression="FLocationID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="TServiceUnitID" HeaderText="To Unit" UniqueName="TServiceUnitID"
                    SortExpression="TServiceUnitID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="TransactionNo, SequenceNo" Name="grdDetail"
                    Width="100%" AutoGenerateColumns="false" AllowPaging="False" PageSize="15" ShowFooter="True">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RequestQty" HeaderText="Issue Qty"
                            UniqueName="RequestQty" SortExpression="RequestQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Confirmed Qty"
                            UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="150px" UniqueName="SRItemUnit_Conversion"
                            HeaderText="Unit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                            Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSRItemUnit_Conversion" runat="server" Text='<%# string.Format("{0}/{1}", DataBinder.Eval(Container.DataItem,"SRItemUnit"), ((decimal)DataBinder.Eval(Container.DataItem,"ConversionFactor")).ToString("n2")) %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed"
                            UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
