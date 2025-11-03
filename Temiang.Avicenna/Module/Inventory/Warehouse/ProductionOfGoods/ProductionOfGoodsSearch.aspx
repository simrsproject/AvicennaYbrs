<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ProductionOfGoodsSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Warehouse.ProductionOfGoodsSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblProductionNo" runat="server" Text="Production No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterProductionNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProductionNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblProductionDate" runat="server" Text="Production Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtProductionDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFormulaID" runat="server" Text="Formula" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboFormulaID" Width="300px" AutoPostBack="True"
                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                    OnItemDataBound="cboFormulaID_ItemDataBound" OnItemsRequested="cboFormulaID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FormulaName")%>
                        &nbsp;(<%# DataBinder.Eval(Container.DataItem, "FormulaID")%>)
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Status" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboStatus" runat="server" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
