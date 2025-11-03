<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RlMasterReportItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.v2025.RlMasterReportItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRlMasterReportItem" runat="server" ValidationGroup="RlMasterReportItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RlMasterReportItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblRlMasterReportItemID" runat="server" Text="RL Master Report Item ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtRlMasterReportItemID" runat="server" Width="300px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRlMasterReportItemNo" runat="server" Text="Item No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRlMasterReportItemNo" runat="server" Width="300px" MaxLength="3" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRlMasterReportItemNo" runat="server" ErrorMessage="Item No required."
                            ControlToValidate="txtRlMasterReportItemNo" SetFocusOnError="True" ValidationGroup="RlMasterReportItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRlMasterReportItemCode" runat="server" Text="Report Code"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRlMasterReportItemCode" runat="server" Width="300px" MaxLength="50" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRlMasterReportItemCode" runat="server" ErrorMessage="Report Code required."
                            ControlToValidate="txtRlMasterReportItemCode" SetFocusOnError="True" ValidationGroup="RlMasterReportItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblRlMasterReportItemName" runat="server" Text="Report Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRlMasterReportItemName" runat="server" Width="300px" MaxLength="300" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvRlMasterReportItemName" runat="server" ErrorMessage="Report Name required."
                            ControlToValidate="txtRlMasterReportItemName" SetFocusOnError="True" ValidationGroup="RlMasterReportItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRParamedicRL1" runat="server" Text="SMF"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRParamedicRL1" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParameterValue" runat="server" Text="Parameter Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtParameterValue" runat="server" Width="300px" MaxLength="4000"
                            TextMode="MultiLine" Height="50px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RlMasterReportItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RlMasterReportItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%">
</table>
