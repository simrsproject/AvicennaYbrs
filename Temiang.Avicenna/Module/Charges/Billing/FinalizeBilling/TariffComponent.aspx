<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="TariffComponent.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.FinalizeBilling.TariffComponent"
    Title="Transaction Detail Setting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="vsumTransChargesItem" runat="server" ValidationGroup="TransChargesItem" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkIsVariable">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPrice1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkIsDiscount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDiscountAmount1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtDiscountPercent">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtDiscountAmount1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table width="100%" style="display: none">
        <tr runat="server" id="pnlParamedicID" visible="false">
            <td class="label">
                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
            </td>
            <td class="entrydescription">
                <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table width="100%" runat="server" id="pnlVariableAndDiscount" visible="false">
        <tr>
            <td class="label">
                <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" ReadOnly="true" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Discount (%) / Item"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtDiscountPercent" runat="server" Width="100px" Value="0"
                    MinValue="0" MaxValue="100" AutoPostBack="true" OnTextChanged="txtDiscountPercent_TextChanged" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDiscountPercent" runat="server" ErrorMessage="Discount (%) required."
                    ControlToValidate="txtDiscountPercent" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDiscount" runat="server" Text="Discount (Rp) / Item"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtDiscountAmount1" runat="server" Width="100px" Value="0" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDiscountAmount1" runat="server" ErrorMessage="Discount (Rp) required."
                    ControlToValidate="txtDiscountAmount1" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                    Width="100%">
                    <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr runat="server" id="pnlComponentTariff" visible="false">
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Tariff Component"></asp:Label>
            </td>
            <td class="entrydescription">
                <telerik:RadGrid ID="grdTariff" runat="server" OnItemCreated="grdTariff_ItemCreated"
                    AutoGenerateColumns="False" GridLines="None" Height="300px" OnNeedDataSource="grdTariff_NeedDataSource">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView DataKeyNames="TariffComponentID, TariffComponentName" CellSpacing="-1">
                        <Columns>
                            <telerik:GridBoundColumn DataField="TariffComponentID" UniqueName="TariffComponentID"
                                SortExpression="TariffComponentID" Visible="False" />
                            <telerik:GridBoundColumn DataField="TariffComponentName" HeaderText="Tariff Component"
                                UniqueName="TariffComponentName" SortExpression="TariffComponentName">
                                <HeaderStyle HorizontalAlign="Left"  Width="110px"/>
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Price" UniqueName="Price" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtPrice" Width="90px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Price")) %>'>
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="Price required." Enabled="false"
                                        ControlToValidate="txtDiscount" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Cito" UniqueName="Cito" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ReadOnly="True" ID="txtCitoAmount" Width="90px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "CitoAmount")) %>'>
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Discount (%)" UniqueName="DiscountPercent"
                                HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="60px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtDiscountPercent" Width="40px" MinValue="0"
                                        MaxValue="100">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Discount (Rp)" UniqueName="DiscountText"
                                HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtDiscount" Width="90px">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ErrorMessage="Discount required."
                                        ControlToValidate="txtDiscount" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Physician" UniqueName="DiscountText" HeaderStyle-HorizontalAlign="left">
                                <HeaderStyle Width="260px" />
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="250px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician Name required."
                                        ControlToValidate="cboPhysicianID" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="IsAllowVariable" UniqueName="IsAllowVariable"
                                Visible="False" />
                            <telerik:GridBoundColumn DataField="IsAllowDiscount" UniqueName="IsAllowDiscount"
                                Visible="False" />
                            <telerik:GridBoundColumn DataField="DiscountAmount" UniqueName="DiscountAmount" Visible="False" />
                            <telerik:GridBoundColumn DataField="IsTariffParamedic" UniqueName="IsTariffParamedic"
                                Visible="False" />
                            <telerik:GridBoundColumn DataField="ParamedicID" UniqueName="ParamedicID" Visible="False" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Tp" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" Visible="False">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsTariffParamedic" runat="server" Width="50px" Checked='<%#Eval("IsTariffParamedic")%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Fee Disc (%)" UniqueName="PercentFeeDiscount"
                                HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="60px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtPercentFeeDiscount" Width="40px" MaxValue="100" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="FeeDiscount" UniqueName="FeeDiscount" Visible="False" />
                            <telerik:GridBoundColumn DataField="FeeDiscountPercentage" UniqueName="FeeDiscountPercentage" Visible="False" />
                            <telerik:GridBoundColumn DataField="IsCito" UniqueName="IsCito" Visible="False" />
                            <telerik:GridTemplateColumn HeaderText="BasicCitoAmount" UniqueName="BasicCitoAmount" HeaderStyle-HorizontalAlign="center" Visible="False">
                                <HeaderStyle Width="110px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ReadOnly="True" ID="txtBasicCitoAmount" Width="90px" Value='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "BasicCitoAmount")) %>'>
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
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr runat="server" id="trSRDiscountReason">
            <td class="label">
                <asp:Label ID="lblSRDiscountReason" runat="server" Text="Discount Reason"></asp:Label>
            </td>
            <td class="entrydescription">
                <telerik:RadComboBox ID="cboSRDiscountReason" runat="server" Width="300px" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr runat="server" id="trNotes">
            <td class="label">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entrydescription">
                <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" MaxLength="1000" />
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
