<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicOtherTypeDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicOtherTypeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ParamedicOtherType" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ParamedicOtherType"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRParamedicType" runat="server" Text="Physician Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRParamedicType" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRParamedicType" runat="server" ErrorMessage="Physician Type required."
                            ControlToValidate="cboSRParamedicType" SetFocusOnError="True" ValidationGroup="ParamedicOtherType"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ParamedicOtherType"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ParamedicOtherType" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                
            </table>
        </td>
    </tr>
</table>
