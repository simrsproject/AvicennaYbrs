<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ItemTariffRequest2CompList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemTariffRequest2CompList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="TariffRequestNo,ItemID,ClassID,TariffComponentID">
                        <Columns>
                            <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component" UniqueName="TariffComponentName"
                                SortExpression="TariffComponentName">
                                <HeaderStyle HorizontalAlign="Left" Width="250px"/>
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Price" HeaderText="Price" UniqueName="Price"
                                SortExpression="Price" DataFormatString="{0:n2}">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="IsAllowDiscount" HeaderText="Discount"
                                UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridCheckBoxColumn>
                            <telerik:GridCheckBoxColumn DataField="IsAllowVariable" HeaderText="Variable"
                                UniqueName="IsAllowVariable" SortExpression="IsAllowVariable">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridCheckBoxColumn>
                           <telerik:GridTemplateColumn />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="True">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>