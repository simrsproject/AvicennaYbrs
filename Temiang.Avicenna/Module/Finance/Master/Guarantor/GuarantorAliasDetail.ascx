<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuarantorAliasDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.GuarantorAliasDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">Bridging Type
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboBridgingType" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboBridgingType_SelectedIndexChanged" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvBridgingType" runat="server" ErrorMessage="Bridging Type required."
                ControlToValidate="cboBridgingType" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr runat="server" id="trCombo">
        <td class="label">Bridging ID</td>
        <td class="entry">
            <telerik:RadComboBox ID="cboServiceUnitAliasID" runat="server" Width="300px" AllowCustomText="true"
                Filter="Contains" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr runat="server" id="trText1">
        <td class="label">Bridging ID
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtBridgingCode" runat="server" Width="300px" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr runat="server" id="trText2">
        <td class="label">Bridging Code
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtBridgingInitial" runat="server" Width="300px" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td class="label">Coverage Value (%)
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox runat="server" ID="txtCoverageValue" Width="100px" Type="Percent"
                MinValue="0" MaxValue="100" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvCoverageValue" runat="server" ErrorMessage="Coverage Percentage Value required."
                ControlToValidate="txtCoverageValue" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">Margin Value (%)
        </td>
        <td class="entry">
            <telerik:RadNumericTextBox runat="server" ID="txtMarginValue" Width="100px" Type="Percent"
                MinValue="0" MaxValue="100" />
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvMarginValue" runat="server" ErrorMessage="Margin Value required."
                ControlToValidate="txtMarginValue" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label" />
        <td class="entry">
            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
        </td>
        <td width="20px" />
        <td />
    </tr>
    <tr>
        <td align="right" colspan="2" style="height: 26px">
            <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ServiceUnitAutoBillItem"
                Visible='<%# !(DataItem is GridInsertionObject) %>' />
            <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                ValidationGroup="ServiceUnitAutoBillItem" Visible='<%# DataItem is GridInsertionObject %>' />
            &nbsp;
           
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel" />
        </td>
    </tr>
</table>
