<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DietLiquidPatientsTimeDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.DietLiquidPatientsTimeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumDietLiquid" runat="server" ValidationGroup="DietLiquid" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="DietLiquid"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDietTime" runat="server" Text="Time"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDietTime" runat="server" Width="300px" ReadOnly="True" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFoodID" runat="server" Text="Formula"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboFoodID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound" OnItemsRequested="cboFoodID_ItemsRequested"
                            AutoPostBack="True" OnSelectedIndexChanged="cboFoodID_SelectedIndexChanged" ValidationGroup="other">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "FoodName")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblMeasure" runat="server" Text="Measure"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtMeasure" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblAmountOfWater" runat="server" Text="Amount Of Water"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAmountOfWater" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="0" />&nbsp;&nbsp;ML
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEtc" runat="server" Text="Etc"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtEtc" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="0" />&nbsp;&nbsp;ML
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="DietLiquid"
                            CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="DietLiquid" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
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
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
