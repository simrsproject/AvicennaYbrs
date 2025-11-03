<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="UpdateDiscountAndVariableStatusDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Tariff.UpdateDiscountAndVariableStatusDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="100%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemName" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtItemName" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGroupName" runat="server" Text="Group"></asp:Label>
                        </td>
                        <td class="entry">
                            <table width="100%" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtGroupID" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGroupName" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsAllowCito" runat="server" Text="Allow Cito" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCitoValue" runat="server" Text="Cito Value"></asp:Label>
                        </td>
                        <td class="entry">
                            
                            <telerik:RadNumericTextBox ID="txtCitoValue" runat="server" Width="100px" />
                            <asp:CheckBox ID="chkIsCitoInPercent" runat="server" Text="In Percent" />
                            <asp:CheckBox ID="chkUseCitoFromAppSR" runat="server" 
                                Text="Use Cito From Standard Reference" 
                                OnCheckedChanged="chkUseCitoFromAppSR_CheckedChanged" 
                                AutoPostBack="true" />
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr id="trCitoRef" runat="server">
                        <td class="label">
                        </td>
                        <td class="entry">
                            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False"
                                SkinID="none" GridLines="None">
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID" SkinID="none">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID"
                                            HeaderText="ID" UniqueName="ItemID" SortExpression="ItemID"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemName"
                                            HeaderText="Name" UniqueName="ItemName" SortExpression="ItemName"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridNumericColumn HeaderText="Value" DataField="NumericValue" 
                                            UniqueName="NumericValue" SortExpression="NumericValue">
                                        </telerik:GridNumericColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="false">
                                    <Resizing AllowColumnResize="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="Label2" runat="server" Text="Tariff Component"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%" style="vertical-align: top">
                <telerik:RadGrid ID="grdItemTariffComp" runat="server" AutoGenerateColumns="False"
                    SkinID="none" GridLines="None">
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="TariffComponentID" SkinID="none">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TariffComponentID"
                                HeaderText="ID" UniqueName="TariffComponentID" SortExpression="TariffComponentID"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="TariffComponentName"
                                HeaderText="Component" UniqueName="TariffComponentName" SortExpression="TariffComponentName"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn HeaderText="Allow Discount" UniqueName="IsAllowDiscount">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsAllowDiscount" Checked='<%#  DataBinder.Eval(Container.DataItem, "IsAllowDiscount") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Allow Variable" UniqueName="IsAllowVariable">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkIsAllowVariable" Checked='<%#  DataBinder.Eval(Container.DataItem, "IsAllowVariable") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
