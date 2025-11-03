<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SnackOrderItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.SnackOrderItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumSnackOrderItem" runat="server" ValidationGroup="SnackOrderItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SnackOrderItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSnackID" runat="server" Text="Snack"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSnackID" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemDataBound="cboSnackID_ItemDataBound" OnItemsRequested="cboSnackID_ItemsRequested"
                            ValidationGroup="other">
                            <ItemTemplate>
                                <b>
                                    <%# DataBinder.Eval(Container.DataItem, "SnackName")%>
                                    &nbsp;(<%# DataBinder.Eval(Container.DataItem, "SnackID")%>) </b>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSnackID" runat="server" ErrorMessage="Snack required."
                            ControlToValidate="cboSnackID" SetFocusOnError="True" ValidationGroup="SnackOrderItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQtyShift1" runat="server" Text="Shift I"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQtyShift1" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQtyShift2" runat="server" Text="Shift II"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQtyShift2" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblQtyShift3" runat="server" Text="Shift III"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtQtyShift3" runat="server" Width="100px" MaxLength="10"
                            MinValue="0" NumberFormat-DecimalDigits="0" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="SnackOrderItem"
                            CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
                        </asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="SnackOrderItem" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
