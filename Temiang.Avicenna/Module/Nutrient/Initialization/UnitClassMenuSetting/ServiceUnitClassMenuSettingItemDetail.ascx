<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceUnitClassMenuSettingItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.ServiceUnitClassMenuSettingItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceUnitClassMenuSetting" runat="server" ValidationGroup="ServiceUnitClassMenuSetting" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitClassMenuSetting"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboClassID" Width="300px" ValidationGroup="other">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class required."
                ControlToValidate="cboClassID" SetFocusOnError="True" ValidationGroup="ServiceUnitClassMenuSetting"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
        </td>
        <td class="entry" style="height: 22px">
            <asp:CheckBox ID="chkIsOptinal" Text="Menu Optional" runat="server" />
        </td>
        <td width="20" style="height: 22px">
        </td>
        <td style="height: 22px">
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitClassMenuSetting"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitClassMenuSetting" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
