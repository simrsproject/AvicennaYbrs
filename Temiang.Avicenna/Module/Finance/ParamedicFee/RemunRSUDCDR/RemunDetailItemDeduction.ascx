<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemunDetailItemDeduction.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.RemunDetailItemDeduction" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumMemorialItem" runat="server" ValidationGroup="MemorialItem" />
<asp:CustomValidator ErrorMessage="" ID="customValidator" OnServerValidate="customValidator_ServerValidate"
    runat="server" ValidationGroup="MemorialItem">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 100%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        Deduction Name
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtDeductionName" Width="300px" ReadOnly="true"></telerik:RadTextBox>
                        <asp:HiddenField runat="server" ID="hfID" />
                    </td>
                    <td style="width: 20px">

                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Amount
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtAmount" runat="server" Width="300px" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                    </td>
                    <td style="width: 20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Amount required."
                            ControlToValidate="txtAmount" SetFocusOnError="True" ValidationGroup="MemorialItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="Button1" Text="Update" runat="server" CommandName="Update" ValidationGroup="MemorialItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="Button2" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="MemorialItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="Button3" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                        </asp:Button>
                    </td>
                    <td style="width: 20px">
                    </td>
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
