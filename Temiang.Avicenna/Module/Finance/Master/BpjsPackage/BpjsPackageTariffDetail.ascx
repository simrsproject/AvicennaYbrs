<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BpjsPackageTariffDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.BpjsPackageTariffDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumBpjsPackageTariff" runat="server" ValidationGroup="BpjsPackageTariff" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="BpjsPackageTariff"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">
            <asp:Label ID="lblStartingDate" runat="server" Text="Starting Date"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadDatePicker ID="txtStartingDate" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvStartingDate" runat="server" ErrorMessage="Starting Date required."
                ControlToValidate="txtStartingDate" SetFocusOnError="True" ValidationGroup="BpjsPackageTariff"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
        </td>
        <td class="entry" valign="middle">
            <telerik:RadComboBox ID="cboClassID" runat="server" Width="304px" EnableLoadOnDemand="True"
                HighlightTemplatedItems="True" MarkFirstMatch="True">
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class required."
                ControlToValidate="cboClassID" SetFocusOnError="True" ValidationGroup="BpjsPackageTariff"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="label">
            <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="Price required."
                ControlToValidate="txtPrice" SetFocusOnError="True" ValidationGroup="BpjsPackageTariff"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td class="entry" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="BpjsPackageTariff"
                Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="BpjsPackageTariff" Visible='<%# DataItem is GridInsertionObject %>'>
            </asp:Button>
            &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
        </td>
        <td width="20">
        </td>
        <td>
        </td>
    </tr>
</table>