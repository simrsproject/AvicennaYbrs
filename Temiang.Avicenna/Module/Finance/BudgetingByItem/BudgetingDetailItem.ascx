<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BudgetingDetailItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.BudgetingByItem.BudgetingDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumvg1" runat="server" ValidationGroup="vg1" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="vg1"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="100%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true" AutoPostBack="true"
                                        HighlightTemplatedItems="true" MarkFirstMatch="False" OnItemDataBound="cboItemID_ItemDataBound"
                                        OnSelectedIndexChanged="cboItemID_SelectedIndexChanged" OnItemsRequested="cboItemID_ItemsRequested">
                                        <FooterTemplate>
                                            Note : Show max 10 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsAsset" runat="server" Text="Asset" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="vg1"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label"><asp:Label ID="Label2" runat="server" Text="Qty"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>Jan</td>
                                <td>Feb</td>
                                <td>Mar</td>
                                <td>Apr</td>
                                <td>May</td>
                                <td>Jun</td>
                                <td>Jul</td>
                                <td>Aug</td>
                                <td>Sep</td>
                                <td>Oct</td>
                                <td>Nov</td>
                                <td>Dec</td>
                            </tr>
                            <tr>
                                <td><telerik:RadNumericTextBox ID="txtQty01" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty02" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty03" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty04" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty05" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty06" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty07" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty08" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty09" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty10" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty11" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                                <td><telerik:RadNumericTextBox ID="txtQty12" runat="server" Width="60px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label"><asp:Label ID="Label1" runat="server" Text="Item Unit / Conversion"></asp:Label>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                     <telerik:RadComboBox ID="cboSRItemUnit" runat="server" Width="197px" AutoPostBack="true" OnSelectedIndexChanged="cboSRItemUnit_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtConversion" runat="server" Width="100px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Item unit required."
                            ControlToValidate="cboSRItemUnit" SetFocusOnError="True" ValidationGroup="vg1"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label"><asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="300px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                    </td>
                    <td width="20">

                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="vg1"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="vg1" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
