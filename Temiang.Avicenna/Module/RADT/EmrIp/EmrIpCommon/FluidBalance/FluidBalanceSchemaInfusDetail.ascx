<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FluidBalanceSchemaInfusDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Emr.FluidBalanceSchemaInfusDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsum" runat="server" ValidationGroup="SchemaInfus" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="SchemaInfus"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnSchemaInfusNo" />
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lbl2" runat="server" Text="Schema Infus"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtSchemaInfusName" runat="server" Width="300px" MaxLength="250"  />
        </td>
        <td width="20">
            <asp:RequiredFieldValidator ID="rfvInteraction" runat="server" ErrorMessage="Schema Infus required."
                ValidationGroup="SchemaInfus" ControlToValidate="txtSchemaInfusName" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
        <tr>
        <td class="label">
            <asp:Label ID="Label1" runat="server" Text="Volume (CC)"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtQtyVolume" runat="server" Width="100px" NumberFormat-DecimalDigits="2"  />
        </td>
        <td width="20">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Volume (CC) required."
                ValidationGroup="SchemaInfus" ControlToValidate="txtQtyVolume" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
            <tr>
        <td class="label">
            <asp:Label ID="Label2" runat="server" Text="In per Hour (CC)"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtQtyPerHour" runat="server" Width="100px" NumberFormat-DecimalDigits="2"  />
        </td>
        <td width="20">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="In per Hour (CC) required."
                ValidationGroup="SchemaInfus" ControlToValidate="txtQtyPerHour" SetFocusOnError="True"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="InteractionZatActive"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="SchemaInfus" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
