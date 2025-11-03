<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicFeeByTeamDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicFeeByTeamDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ParamedicFeeByTeam" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ParamedicFeeByTeam"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParamedicMember" runat="server" Text="Member"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboParamedicMember" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvParamedicMember" runat="server" ErrorMessage="Member required."
                            ControlToValidate="cboParamedicMember" SetFocusOnError="True" ValidationGroup="ParamedicFeeByTeam"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFeePercentage" runat="server" Text="Percentage"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtFeePercentage" runat="server" Width="300px"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rvfFeePercentage" runat="server" ErrorMessage="Fee Percentage required."
                            ControlToValidate="txtFeePercentage" SetFocusOnError="True" ValidationGroup="ParamedicFeeByTeam"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ParamedicFeeByTeam"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ParamedicFeeByTeam" Visible='<%# DataItem is GridInsertionObject %>'>
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