<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DistributionConfirmPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.DistributionConfirmPickList"
    Title="Untitled Page" %>

<%@ Register Assembly="Temiang.Avicenna" Namespace="Temiang.Avicenna.CustomControl"
    TagPrefix="cc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script language="javascript" type="text/javascript">
            function RowSelected(sender, args) {
                __doPostBack("<%=grdDetail.UniqueID%>", "rebind:" + args.getDataKeyValue("TransactionNo"));
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDistributionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRequestNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterLocation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboFromServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboFromLocationID"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Distribution">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDistributionDate" runat="server" Text="Distribution Date" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="110px">
                                </telerik:RadDatePicker>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Distribution Date" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDistributionNo" runat="server" Text="Distribution No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDistributionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterDistributionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Distribution Date" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRequestNo" runat="server" Text="Request No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRequestNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterRequestNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By Distribution Date" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromUnit" runat="server" Text="From Unit" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboFromServiceUnitID_ItemDataBound"
                                    OnItemsRequested="cboFromServiceUnitID_ItemsRequested" OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 10 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By From Unit" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLocation" runat="server" Text="Location" />
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboFromLocationID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboFromLocationID_ItemDataBound"
                                    OnItemsRequested="cboFromLocationID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "LocationName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 10 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterLocation" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Filter By From Unit" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadGrid ID="grdList" runat="server" ShowStatusBar="true" OnNeedDataSource="grdList_NeedDataSource"
            AllowPaging="True" AutoGenerateColumns="False">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo"
                PageSize="10">
                <Columns>
                    <telerik:GridBoundColumn DataField="TransactionNo" HeaderText="Distribution No" UniqueName="TransactionNo"
                        SortExpression="TransactionNo">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                        SortExpression="TransactionDate">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="From Unit"
                        UniqueName="FromServiceUnit" SortExpression="FromServiceUnit">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FromLocation" HeaderText="Location"
                        UniqueName="FromLocation" SortExpression="FromLocation">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                        SortExpression="Notes">
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Request No" UniqueName="ReferenceNo"
                        SortExpression="ReferenceNo">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TransactionDateReff" HeaderText="Request Date"
                        UniqueName="TransactionDateReff" SortExpression="TransactionDateReff">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridDateTimeColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnRowSelected="RowSelected" />
            </ClientSettings>
        </telerik:RadGrid>
    </cc:CollapsePanel>
    <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Distribution Item">
        <telerik:RadGrid ID="grdDetail" runat="server" AutoGenerateColumns="False" GridLines="None"
            OnNeedDataSource="grdDetail_NeedDataSource" OnPageIndexChanged="grdDetail_PageIndexChanged"
            AllowPaging="False">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="110px" DataField="ItemID" HeaderText="Item ID"
                        UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                        SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Quantity" HeaderText="Qty Distribution"
                        UniqueName="Quantity" SortExpression="Quantity" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="QuantityFinishInBaseUnit"
                        HeaderText="Prev. Confirmed" UniqueName="QuantityFinishInBaseUnit" SortExpression="QuantityFinishInBaseUnit"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="False" />
                    <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Qty Confirmed" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtQtyInput" runat="server" Width="80px" DbValue='<%#Eval("QtyInput")%>'
                                NumberFormat-DecimalDigits="2" ReadOnly="True" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="SRItemUnit" HeaderText="Item Unit"
                        UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </cc:CollapsePanel>
</asp:Content>
