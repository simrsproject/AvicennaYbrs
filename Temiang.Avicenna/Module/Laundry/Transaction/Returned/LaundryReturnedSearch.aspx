<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="LaundryReturnedSearch.aspx.cs" Inherits="Temiang.Avicenna.Module.Laundry.Transaction.LaundryReturnedSearch" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblReturnNo" runat="server" Text="Return No" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterReturnNo" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Contains" Value="Contains" />
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtReturnNo" runat="server" Width="300px" MaxLength="20" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblReturnDate" runat="server" Text="Return Date" Width="100px"></asp:Label>
            </td>
            <td class="filter">
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtReturnDate" runat="server" Width="100px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblToServiceUnitID" runat="server" Text="To Service Unit" Width="100px"></asp:Label>
            </td>
            <td class="filter">
                <telerik:RadComboBox ID="cboFilterToServiceUnitID" runat="server" Width="100%">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Equal" Value="Equal" />
                    </Items>
                </telerik:RadComboBox>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboToServiceUnitID" Width="300px" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" OnItemDataBound="cboToServiceUnitID_ItemDataBound"
                    OnItemsRequested="cboToServiceUnitID_ItemsRequested">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                        </b>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
