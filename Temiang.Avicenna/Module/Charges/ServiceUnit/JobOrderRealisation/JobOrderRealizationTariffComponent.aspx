<%@ Page Title="Tariff Components" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="JobOrderRealizationTariffComponent.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.JobOrderRealizationTariffComponent" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <ajaxsettings>
            <telerik:AjaxSetting AjaxControlID="cboParamedicID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdTariff" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </ajaxsettings>
    </telerik:RadAjaxManagerProxy>
    <asp:ValidationSummary ID="vsumTransChargesItem" runat="server" ValidationGroup="TransChargesItem" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" OnSelectedIndexChanged="cboParamedicID_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFilmNo" runat="server" Text="Film No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtFilmNo" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Discount Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboDiscountReason" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr runat="server" id="pnlNonComponent" visible="false">
            <td>
                <table>
                    <tr>
                        <td colspan="3" class="labelcaption">
                            <asp:Label ID="lblPrice1" runat="server" Width="170px" Text="Price"></asp:Label>
                        </td>
                        <td colspan="3" class="labelcaption">
                            <asp:Label ID="lblDiscountAmount1" runat="server" Width="170px" Text="Discount Amount"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkIsVariable" runat="server" Enabled="False" AutoPostBack="True"
                                OnCheckedChanged="chkIsVariable_CheckedChanged" Text="Variable" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtPrice1" runat="server" Width="150px" ReadOnly="true"
                                Value="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPrice1" runat="server" ErrorMessage="Price required."
                                ControlToValidate="txtPrice1" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsDiscount" runat="server" Enabled="False" AutoPostBack="True"
                                OnCheckedChanged="chkIsDiscount_CheckedChanged" Text="Discount" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtDiscountPercent" runat="server" Width="100px" ReadOnly="true"
                                Value="0" MaxValue="100" EmptyMessage="Discount (%)" AutoPostBack="true" OnTextChanged="txtDiscountPercent_TextChanged" />
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtDiscountAmount1" runat="server" Width="100px" ReadOnly="true"
                                Value="0" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvDiscountAmount1" runat="server" ErrorMessage="Discount Amount required."
                                ControlToValidate="txtDiscountAmount1" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" id="pnlComponent" visible="false">
            <td>
                <telerik:RadGrid ID="grdTariff" runat="server" OnItemCreated="grdTariff_ItemCreated"
                    AutoGenerateColumns="False" GridLines="None" Height="300px" OnNeedDataSource="grdTariff_NeedDataSource">
                    <headercontextmenu>
                    </headercontextmenu>
                    <mastertableview datakeynames="TariffComponentID">
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
                            <telerik:GridTemplateColumn HeaderText="Price" UniqueName="PriceText" HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="120px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtPrice" Width="100px">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="Price required."
                                        ControlToValidate="txtPrice" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
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
                                <HeaderStyle Width="120px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtDiscount" Width="100px">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ErrorMessage="Discount required."
                                        ControlToValidate="txtDiscount" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Physician" UniqueName="PhysicianText" HeaderStyle-HorizontalAlign="left">
                                <HeaderStyle Width="300px" />
                                <ItemTemplate>
                                    <telerik:RadComboBox ID="cboPhysicianID" runat="server" Width="100%">
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician ID required."
                                        ControlToValidate="cboPhysicianID" SetFocusOnError="True" ValidationGroup="TransChargesItem"
                                        Width="100%">
                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="IsAllowVariable" UniqueName="IsAllowVariable"
                                SortExpression="IsAllowVariable" Visible="False" />
                            <telerik:GridBoundColumn DataField="IsAllowDiscount" UniqueName="IsAllowDiscount"
                                SortExpression="IsAllowDiscount" Visible="False" />
                            <telerik:GridBoundColumn DataField="IsTariffParamedic" UniqueName="IsTariffParamedic"
                                SortExpression="IsTariffParamedic" Visible="False" />
                                
                            <telerik:GridTemplateColumn HeaderText="Fee Disc (%)" UniqueName="PercentFeeDiscount"
                                HeaderStyle-HorizontalAlign="center">
                                <HeaderStyle Width="60px" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox runat="server" ID="txtPercentFeeDiscount" Width="40px" MaxValue="100" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="FeeDiscount" UniqueName="FeeDiscount" Visible="False" />
                            <telerik:GridBoundColumn DataField="FeeDiscountPercentage" UniqueName="FeeDiscountPercentage" Visible="False" />
                        </Columns>
                    </mastertableview>
                    <filtermenu>
                    </filtermenu>
                    <clientsettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </clientsettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
