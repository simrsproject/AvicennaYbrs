<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProcedureSynonymDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ProcedureSynonymDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ProcedureSynonym" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ProcedureSynonym"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSynonym" runat="server" Text="Synonym"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSynonym" runat="server" Width="300px"/>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rvfProcedureSynonym" runat="server" ErrorMessage="Synonym required."
                            ControlToValidate="txtSynonym" SetFocusOnError="True" ValidationGroup="ProcedureSynonym"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ProcedureSynonym"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ProcedureSynonym" Visible='<%# DataItem is GridInsertionObject %>'>
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