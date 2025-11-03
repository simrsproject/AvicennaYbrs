<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemLaboratoryProfile.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ItemLaboratoryProfile" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblCaptionName" runat="server" Text="" />
        </td>
        <td class="entry">
            <telerik:RadComboBox runat="server" ID="cboCaptionName" Width="300px" EnableLoadOnDemand="True"
                MarkFirstMatch="False" OnItemDataBound="cboCaptionName_ItemDataBound"
                OnItemsRequested="cboCaptionName_ItemsRequested" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage=""
                ControlToValidate="cboCaptionName" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
    </tr>
</table>
