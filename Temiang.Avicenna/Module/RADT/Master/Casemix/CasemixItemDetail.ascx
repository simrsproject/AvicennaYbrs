<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CasemixItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.CasemixItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">Procedure Name
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtAssesmentID" Width="300px" AutoPostBack="true"
                            OnTextChanged="txtAssesmentID_TextChanged" />
                        <telerik:RadTextBox runat="server" ID="txtAssesmentName" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td witdh="20px">
                        <asp:RequiredFieldValidator ID="rfvAssesmentName" runat="server" ErrorMessage="Procedure Name required."
                            ControlToValidate="txtAssesmentName" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">Notes
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox runat="server" ID="txtNotes" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox runat="server" ID="chkIsActive" Text="Active" />
                    </td>
                    <td witdh="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>' />
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
                        &nbsp;
                       
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
        </td>
    </tr>
</table>
