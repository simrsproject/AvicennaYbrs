<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitParamedicDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitParamedicDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitParamedic" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitParamedic"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
        </td>
        <td class="entry">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" MaxLength="10"
                            AutoPostBack="true" OnTextChanged="txtParamedicID_TextChanged" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblParamedicName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                ControlToValidate="txtParamedicID" SetFocusOnError="True" ValidationGroup="ServiceUnitParamedic"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="Label1" runat="server" Text="Default Room"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox Width="300px" ID="cbRoomDefault" runat="server">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsUsingQue" runat="server" Text="Using Que Slot" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsAcceptBPJS" runat="server" Text="Accept BPJS" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry">
            <asp:CheckBox ID="chkIsAcceptNonBPJS" runat="server" Text="Accept Non BPJS" />
        </td>
        <td width="20px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitParamedic"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitParamedic" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
