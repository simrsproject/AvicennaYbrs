<%@ Page Title="Item With Stock Less Than Unit Request / By Reference No" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RequestOrderByDistReqPickList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Procurement.RequestOrderByDistReqPickList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnByReferenceNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnByItemGroupID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <fieldset>
        <legend>Filter</legend>
        <table>
            <tr runat="server" id="trRequestType">
                <td class="label">
                    <asp:Label ID="lblRequestType" runat="server" Text="Request Type"></asp:Label>
                </td>
                <td class="entry300">
                    <asp:RadioButtonList ID="rbtRequestType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="DR" Text="Distribution Request" Selected="True" />
                        <asp:ListItem Value="IR" Text="Inventory Issue Request" />
                    </asp:RadioButtonList>
                </td>
                <td width="10px"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblReferenceNo" runat="server" Text="Reference No"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
                </td>
                <td>&nbsp;</td>
                <td style="text-align: left">
                    <asp:ImageButton ID="btnByReferenceNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnByItemGroupID_Click" ToolTip="Search" />
                </td>
                <td></td>
            </tr>
            <tr>
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
                    <asp:ImageButton ID="btnByItemGroupID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                        OnClick="btnByItemGroupID_Click" ToolTip="Search" />
                </td>
                <td></td>
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
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QtyRequest" HeaderText="Total Demand"
                    UniqueName="QtyRequest" SortExpression="QtyRequest" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRItemUnit" HeaderText="Item Unit"
                    UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="QtyOrder" HeaderText="Suggest in Purc. Unit"
                    UniqueName="QtyOrder" SortExpression="QtyOrder" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridTemplateColumn UniqueName="QtyOrderColumn" HeaderStyle-Width="80px"
                    HeaderText="Request" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtQtyOrder" runat="server" Width="90%" DbValue='<%# Eval("QtyOrder") %>'
                            NumberFormat-DecimalDigits="2" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="Unit" HeaderText="Purchase Unit"
                    UniqueName="Unit" SortExpression="Unit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="SRPurchaseUnit" HeaderText="Purchase Unit"
                    UniqueName="SRPurchaseUnit" SortExpression="SRPurchaseUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
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
