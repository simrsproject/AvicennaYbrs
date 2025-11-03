<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="ItemLinenList.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Master.ItemLinenList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
                switch (tabIndex) {
                    case 0:
                        __doPostBack("<%= grdListItem.UniqueID %>", "rebind");
                        break;
                }
            }

            function openEditItem(itemId) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("LaundryItemsDetail.aspx?id=" + itemId);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openAddBundle() {
                var url = 'ItemLinenDetail.aspx?md=new&id=';
                window.location.href = url;
            }

            function openViewBundle(id) {
                var url = 'ItemLinenDetail.aspx?md=view&id=' + id;
                window.location.href = url;
            }

            function onClientClose(oWnd, args) {
                __doPostBack("<%= grdListItem.UniqueID %>", "rebind");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" ID="winDialog" Animation="None" Behaviors="Move, Close"
        Width="900px" Height="600px" VisibleStatusbar="false" ShowContentDuringLoad="False"
        Modal="true" OnClientClose="onClientClose" />
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchItemId2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListItem" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="btnSearchBundleId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListBundle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchBundleId2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListBundle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListBundle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListBundle" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Laundry Items" PageViewID="pgItem"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Linen Bundle" PageViewID="pgBundle" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgItem" runat="server" Selected="true">
            <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblItemID" runat="server" Text="Item Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" Text="" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnSearchItemId" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearch_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="Label4" runat="server" Text="Or" />
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                                            OnItemsRequested="cboItemID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 20 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnSearchItemId2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearch_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdListItem" runat="server" OnNeedDataSource="grdListItem_NeedDataSource"
                AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="ItemID" ClientDataKeyNames="ItemID" GroupLoadMode="client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openEditItem('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "ItemID")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="150px" DataField="Weight"
                            HeaderText="Weight (in grams)" UniqueName="PriceInPurchaseUnit" SortExpression="Weight"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />

                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>


        </telerik:RadPageView>
        <telerik:RadPageView ID="pgBundle" runat="server">
            <cc:CollapsePanel ID="CollapsePanel2" runat="server" Title="Search Filter">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%" style="vertical-align: top">
                            <table>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBundleID" runat="server" Text="Bundle Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtBundleName" runat="server" Width="300px" Text="" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnSearchBundleId" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearch2_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBundleId2" runat="server" Text="Or" />
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboBundleID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboBundleID_ItemDataBound"
                                            OnItemsRequested="cboBundleID_ItemsRequested">
                                            <FooterTemplate>
                                                Note : Show max 20 items
                                            </FooterTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnSearchBundleId2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnSearch2_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="50%" style="vertical-align: top">
                            <table>
                            </table>
                        </td>
                    </tr>
                </table>
            </cc:CollapsePanel>
            <telerik:RadGrid ID="grdListBundle" runat="server" OnNeedDataSource="grdListBundle_NeedDataSource"
                AllowPaging="true" PageSize="15" AutoGenerateColumns="false">
                <MasterTableView DataKeyNames="ItemID" ClientDataKeyNames="ItemID" GroupLoadMode="client"
                    CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;
                <asp:LinkButton ID="lbNew" runat="server" OnClientClick="javascript:openAddBundle();return false;">
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/new16.png" />&nbsp;<asp:Label
                        runat="server" ID="lblNew" Text="New"></asp:Label>
                </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openViewBundle('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "ItemID")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
