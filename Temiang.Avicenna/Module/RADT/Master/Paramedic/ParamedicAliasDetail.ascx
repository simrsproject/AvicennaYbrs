<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicAliasDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicAliasDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumServiceRoom" runat="server" ValidationGroup="ServiceUnitAutoBillItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ServiceUnitAutoBillItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td class="label">Bridging Type
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboBridgingType" runat="server" Width="300px" AutoPostBack="true"
                OnSelectedIndexChanged="cboBridgingType_SelectedIndexChanged" />
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
    <asp:Panel ID="pnlBpjs" runat="server">
        <tr>
            <td class="label">Spesialistic
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSpecialistic" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSpecialistic_SelectedIndexChanged" />
            </td>
            <td width="20px" />
            <td />
        </tr>
        <tr>
            <td class="label">Duty Type
            </td>
            <td class="entry">
                <asp:RadioButtonList ID="rblJenis" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                    OnSelectedIndexChanged="rblJenis_SelectedIndexChanged">
                    <asp:ListItem Text="Inpatient" Value="1" Selected="True" />
                    <asp:ListItem Text="Outpatient" Value="2" Enabled="false" />
                </asp:RadioButtonList>
            </td>
            <td width="20px" />
            <td />
        </tr>
    </asp:Panel>
    <tr>
        <td class="label">Bridging ID
        </td>
        <td class="entry">
            <telerik:RadComboBox ID="cboServiceUnitAliasID" runat="server" Width="300px" AllowCustomText="true"
                Filter="Contains" />
            <telerik:RadComboBox ID="cboSsBridgingID" runat="server" Width="300px" DataValueField="ID" DataTextField="Name" AllowCustomText="true"
                Filter="Contains" Visible="false">
                <ItemTemplate>
                    <div>
                        <%# Eval("Name") %><br />
                        DOB:&nbsp;<%# Eval("DOB") %><br />
                        ADDR:&nbsp;<%# Eval("Address") %><br />
                    </div>
                </ItemTemplate>
            </telerik:RadComboBox>
        </td>
        <td width="20px">
            <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Bridging ID required."
                ControlToValidate="cboServiceUnitAliasID" SetFocusOnError="True" ValidationGroup="ServiceUnitAutoBillItem"
                Width="100%">
                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
            </asp:RequiredFieldValidator>
        </td>
        <td />
    </tr>
    <tr>
        <td class="label">Bridging Name
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtServiceUnitAliasName" runat="server" Width="300px" />
        </td>
        <td width="20px" />
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
