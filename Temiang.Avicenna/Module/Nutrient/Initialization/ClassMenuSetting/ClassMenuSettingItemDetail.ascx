<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClassMenuSettingItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.ClassMenuSettingItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumClassMealSetMenuSetting" runat="server" ValidationGroup="ClassMealSetMenuSetting" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ClassMealSetMenuSetting"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblSRMealSet" runat="server" Text="Meal Set"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboSRMealSet" Width="300px">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvSRMealSet" runat="server" ErrorMessage="Meal Set required."
                ControlToValidate="cboSRMealSet" SetFocusOnError="True" ValidationGroup="ClassMealSetMenuSetting"
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
            <asp:CheckBox ID="chkIsOptinal" Text="Optional" runat="server" />
        </td>
        <td width="20" style="height: 22px">
        </td>
        <td style="height: 22px">
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ClassMealSetMenuSetting"
                CausesValidation="true" Visible='<%# !(DataItem is GridInsertionObject) %>'>
            </asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ClassMealSetMenuSetting" CausesValidation="true" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
