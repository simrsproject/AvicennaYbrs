<%@ Page Title="Tariff Component" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="ItemPackageTariffComponentDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ItemPackageTariffComponentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server" />
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblItemID" runat="server" Text="Item" />
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemID" runat="server" Width="300px" ReadOnly="True" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr id="pnlDiscount" runat="server" >
            <td class="label">
                <asp:Label ID="lblDiscount" runat="server" Text="Discount" />
            </td>
            <td class="entry">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadNumericTextBox ID="txtDiscountValue" runat="server" Width="100px" ReadOnly="True" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsDiscountInPercent" runat="server" Text="In Percent (%)" Enabled="False" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblTariffType" runat="server" Text="Tariff Type" />
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboTariffType" runat="server" Width="300px" AutoPostBack="True"
                    OnSelectedIndexChanged="cboTariffType_OnSelectedIndexChanged" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblClassID" runat="server" Text="Class ID" />
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px" AutoPostBack="True"
                    OnSelectedIndexChanged="cboTariffType_OnSelectedIndexChanged" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr id="pnlProduct" runat="server" visible="False">
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Price" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtProductPrice" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Price Reference" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtProductPriceReference" runat="server" Width="100px"
                                ReadOnly="true" Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="pnlService" runat="server" visible="False">
            <td colspan="4">
                <telerik:RadGrid ID="grdTariff" runat="server" OnItemCreated="grdTariff_ItemCreated"
                    AutoGenerateColumns="False" GridLines="None" Height="300px" OnNeedDataSource="grdTariff_NeedDataSource">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView DataKeyNames="TariffComponentID">
                        <Columns>
                            <telerik:GridBoundColumn DataField="TariffComponentID" UniqueName="TariffComponentID"
                                SortExpression="TariffComponentID" Visible="False" />
                            <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component Name"
                                UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Price" UniqueName="Price" SortExpression="Price"
                                Visible="False" />
                            <telerik:GridTemplateColumn HeaderText="Normal Price" UniqueName="PriceText" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="120px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtNormalPrice" Width="100px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Price")) %>'>
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="Discount" UniqueName="Discount" SortExpression="Discount"
                                Visible="False" />

                            <telerik:GridBoundColumn DataField="Discount" UniqueName="Discount" SortExpression="Discount"
                                Visible="False" />
                            <telerik:GridTemplateColumn HeaderText="Disc(%)" Aggregate="Sum" UniqueName="Discount" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="120px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtDiscountPersen" Width="100px" Type="Percent">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn HeaderText="Disc(nominal)" UniqueName="Discount" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="120px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtDiscount" Width="100px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Discount")) %>'>
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Price" UniqueName="Price" Aggregate="Sum" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="120px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtPrice" Width="100px" Type="Number">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
