<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LiquidFoodDietSettingItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.LiquidFoodDietSettingItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumLiquidFoodDietTimeSettingItem" runat="server" ValidationGroup="LiquidFoodDietTimeSettingItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="LiquidFoodDietTimeSettingItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTime" runat="server" Text="Time"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboTime" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvTime" runat="server" ErrorMessage="Time required."
                            ControlToValidate="cboTime" SetFocusOnError="True" ValidationGroup="LiquidFoodDietTimeSettingItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFoodID" runat="server" Text="Formula (Adults)"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboFoodID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound" OnItemsRequested="cboFoodID_ItemsRequested"
                            ValidationGroup="LiquidFoodDietTimeSettingItem">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "FoodName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFoodID" runat="server" ErrorMessage="Formula (Adults) required."
                            ControlToValidate="cboFoodID" SetFocusOnError="True" ValidationGroup="LiquidFoodDietTimeSettingItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChildrenFoodID" runat="server" Text="Formula (Children)"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboChildrenFoodID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound" OnItemsRequested="cboFoodID_ItemsRequested"
                            ValidationGroup="LiquidFoodDietTimeSettingItem">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "FoodName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvChildrenFoodID" runat="server" ErrorMessage="Formula (Children) required."
                            ControlToValidate="cboChildrenFoodID" SetFocusOnError="True" ValidationGroup="LiquidFoodDietTimeSettingItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="LiquidFoodDietTimeSettingItem"
                            CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="LiquidFoodDietTimeSettingItem" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
