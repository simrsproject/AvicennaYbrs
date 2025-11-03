<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdditionalMealOrderItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.AdditionalMealOrderItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumAdditionalMealOrderItem" runat="server" ValidationGroup="AdditionalMealOrderItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="AdditionalMealOrderItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFoodID" runat="server" Text="Food"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboFoodID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound" OnItemsRequested="cboFoodID_ItemsRequested"
                            OnSelectedIndexChanged="cboFoodID_SelectedIndexChanged" AutoPostBack="True">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "FoodName")%>
                                    &nbsp;(<%# DataBinder.Eval(Container.DataItem, "FoodID")%>) </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFoodID" runat="server" ErrorMessage="Food required."
                            ControlToValidate="cboFoodID" SetFocusOnError="True" ValidationGroup="AdditionalMealOrderItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQty" runat="server" Text="Qty"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="AdditionalMealOrderItem"
                            CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="AdditionalMealOrderItem" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%" valign="top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
