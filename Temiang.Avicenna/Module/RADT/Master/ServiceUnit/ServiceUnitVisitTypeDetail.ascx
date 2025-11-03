<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitVisitTypeDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ServiceUnitVisitTypeDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitVisitType" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitVisitType"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblVisitTypeID" runat="server" Text="Visit Type ID"></asp:Label>
        </td>
        <td class="entry">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtVisitTypeID" runat="server" Width="100px" MaxLength="10"
                            AutoPostBack="true" OnTextChanged="txtVisitTypeID_TextChanged" />
                    </td>
                    <td>
                        <asp:Label ID="lblVisitTypeName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvVisitTypeID" runat="server" ErrorMessage="Visit Type ID required."
                ControlToValidate="txtVisitTypeID" SetFocusOnError="True" ValidationGroup="ServiceUnitVisitType"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblVisitDuration" runat="server" Text="Visit Duration"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtVisitDuration" runat="server" Width="300px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvVisitDuration" runat="server" ErrorMessage="Visit Duration required."
                ControlToValidate="txtVisitDuration" SetFocusOnError="True" ValidationGroup="ServiceUnitVisitType"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitVisitType"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitVisitType" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
