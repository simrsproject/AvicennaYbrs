<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="InventoryIssueOrderList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.InventoryIssueOrderList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function onClientTabSelected(sender, eventArgs) {
            var tabIndex = eventArgs.get_tab().get_index();
            switch (tabIndex) {
                case 0:
                    __doPostBack("<%= grdList.UniqueID %>", "rebind1");
                    break;
                case 1:
                    __doPostBack("<%= grdList2.UniqueID %>", "rebind2");
                    break;
            }
        }

        function OnClientButtonClicking(sender, args) {
            var val = args.get_item().get_value();
            switch (val) {
                case "new":
                    var url = 'InventoryIssueDetail.aspx?md=new&req=&rod=&list=y';
                    window.location.href = url;
                    break;
            }
        }
    </script>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tabStrip">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDateBy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequestDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequestNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSearchToUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSearchFromUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSRItemTypePr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tabStrip">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequestDate2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchIssuedNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchRequestNo2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSearchFromUnit2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchSearchFromLocation2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSRItemTypePr2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSearchFromUnit2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSearchFromLocation2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New" Value="new" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
        ShowBaseLine="true" Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Inventory Issue Request Outstanding" PageViewID="pgPR"
                Selected="true" />
            <telerik:RadTab runat="server" Text="Inventory Issue" PageViewID="pgPO" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgPR" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFilterDateBy" runat="server" Text="Filter By"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <asp:RadioButtonList ID="rblFilterDateBy" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblFilterDateBy_OnSelectedIndexChanged">
                                            <asp:ListItem Value="REQ" Text="Request Date" Selected="True" />
                                            <asp:ListItem Value="APP" Text="Approved Date" />
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="20">
                                        <asp:ImageButton ID="btnFilterDateBy" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchRequestDate" runat="server" Text="Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFromRequestDate" runat="server" Width="100px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>&nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtToRequestDate" runat="server" Width="100px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchRequestDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRequestNo" runat="server" Text="Request No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtRequestNo" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRequestNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%">
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label1" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchToUnit" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchSearchToUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSearchFromUnit" runat="server" Text="Request From Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchFromUnit" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchSearchFromUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSRItemType" runat="server" Text="Item Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSRItemTypePr" Width="300px" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterSRItemTypePr" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                AllowPaging="true" PageSize="20" AllowSorting="true" AutoGenerateColumns="false"
                OnDetailTableDataBind="grdList_DetailTableDataBind">
                <MasterTableView DataKeyNames="TransactionNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="150px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="PoUrl" HeaderText="Issue Request No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ToUnit" HeaderText="Service Unit"
                            UniqueName="ToUnit" SortExpression="ToUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ServiceUnitName" HeaderText="Request From Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="FServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ApprovedByUserID" HeaderText="Approved By"
                            UniqueName="ApprovedByUserID" SortExpression="ApprovedByUserID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ApprovedDate" HeaderText="Approved Date" UniqueName="ApprovedDate"
                            SortExpression="ApprovedDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="135px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Qty Request"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyFinished" HeaderText="Qty Issue"
                                    UniqueName="QtyFinished" SortExpression="QtyFinished" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Qty Outstanding"
                                    UniqueName="QtyIssued" DataType="System.Double" DataFields="Quantity,QtyFinished"
                                    Expression="({0}-{1})" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <%--<telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />--%>
                                <telerik:GridTemplateColumn HeaderStyle-Width="150px" UniqueName="SRItemUnit_Conversion"
                                    HeaderText="Unit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRItemUnit_Conversion" runat="server" Text='<%# string.Format("{0}/{1}", DataBinder.Eval(Container.DataItem,"SRItemUnit"), ((decimal)DataBinder.Eval(Container.DataItem,"ConversionFactor")).ToString("n2")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPO" runat="server">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table>
                                <tr>
                                    <td class="label">Issue Date
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtFromRequestDate2" runat="server" Width="100px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>&nbsp;-&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtToRequestDate2" runat="server" Width="100px">
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="30px">
                                        <asp:ImageButton ID="btnSearchRequestDate2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">Issue No
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtIssueNo" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchIssuedNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">Request No
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox runat="server" ID="txtRequestNo2" Width="300px" />
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchRequestNo2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table>
                                <tr>
                                    <td class="label">Service Unit
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchFromUnit2" Width="300px" AllowCustomText="true"
                                            Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboSearchFromUnit2_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchSearchFromUnit2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">Location
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSearchFromLocation2" Width="300px" AllowCustomText="true"
                                            Filter="Contains">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20px">
                                        <asp:ImageButton ID="btnSearchSearchFromLocation2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">Item Type
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox runat="server" ID="cboSRItemTypePr2" Width="300px" />
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="btnFilterSRItemTypePr2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilterPR_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdList2" runat="server" OnNeedDataSource="grdList2_NeedDataSource"
                AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnDetailTableDataBind="grdList_DetailTableDataBind">
                <MasterTableView DataKeyNames="TransactionNo">
                    <Columns>
                        <telerik:GridHyperLinkColumn HeaderStyle-Width="130px" DataTextField="TransactionNo"
                            DataNavigateUrlFields="PoUrl" HeaderText="Issue No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                            HeaderText="Issue Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="FServiceUnitID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="LocationName" HeaderText="Location"
                            UniqueName="LocationName" SortExpression="LocationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ToUnit" HeaderText="To Unit"
                            UniqueName="ToUnit" SortExpression="ToUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemName" HeaderText="Item Type"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ReferenceNo" HeaderText="Reference No"
                            UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ApprovedByUserID"
                            HeaderText="Approved By" UniqueName="ApprovedByUserID" SortExpression="ApprovedByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="True" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataField="ApprovedDate" HeaderText="Approve Date"
                            UniqueName="ApprovedDate" SortExpression="ApprovedDate" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="SequenceNo" Name="grdDetail" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Qty"
                                    UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <%--<telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRItemUnit" HeaderText="Unit"
                                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn />--%>
                                <telerik:GridTemplateColumn HeaderStyle-Width="150px" UniqueName="SRItemUnit_Conversion"
                                    HeaderText="Unit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                    Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRItemUnit_Conversion" runat="server" Text='<%# string.Format("{0}/{1}", DataBinder.Eval(Container.DataItem,"SRItemUnit"), ((decimal)DataBinder.Eval(Container.DataItem,"ConversionFactor")).ToString("n2")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
