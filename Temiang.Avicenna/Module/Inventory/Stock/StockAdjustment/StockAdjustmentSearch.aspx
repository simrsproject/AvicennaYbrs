<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="StockAdjustmentSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Stock.StockAdjustmentSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboFromServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboFromLocationID"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td class="entry">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTransactionDate" runat="server" Text="Adjustment Date"></asp:Label>
            </td>
            <td class="searchfilter">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateFrom" runat="server" Width="100px" />
                        </td>
                        <td>
                            &nbsp;-&nbsp;
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtTransactionDateTo" runat="server" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="searchfilter">
                <telerik:RadComboBox ID="cboFromServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboFromServiceUnitID_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFromLocationID" runat="server" Text="Location"></asp:Label>
            </td>
            <td class="searchfilter">
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
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblAdjustmentType" runat="server" Text="Adjustment Type"></asp:Label>
            </td>
            <td class="searchfilter">
                <telerik:RadComboBox ID="cboSRAdjustmentType" Width="300px" runat="server" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemType" runat="server" Text="Item Type"></asp:Label>
            </td>
            <td class="searchfilter">
                <telerik:RadComboBox ID="cboSRItemType" Width="300px" runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Status" Width="100px"></asp:Label>
            </td>
            <td class="searchfilter">
                <telerik:RadComboBox ID="cboStatus" runat="server" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
