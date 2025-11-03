<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientIncidentComponentTypeItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentComponentTypeItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumIncidentCompType" runat="server" ValidationGroup="IncidentCompType" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="IncidentCompType"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRIncidentType" runat="server" Text="Incident Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRIncidentType" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" OnItemsRequested="cboSRIncidentType_ItemsRequested"
                            OnItemDataBound="cboSRIncidentType_ItemDataBound" OnSelectedIndexChanged="cboSRIncidentType_SelectedIndexChanged"
                            AutoPostBack="True">
                            <FooterTemplate>
                                Note : Show max 20 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvSRIncidentType" runat="server" ErrorMessage="Incident Type required."
                            ValidationGroup="IncidentCompType" ControlToValidate="cboSRIncidentType" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblComponentID" runat="server" Text="Component"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboComponentID" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" OnSelectedIndexChanged="cboComponentID_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvComponentID" runat="server" ErrorMessage="Component required."
                            ValidationGroup="IncidentCompType" ControlToValidate="cboComponentID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubComponentID" runat="server" Text="Sub Component"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSubComponentID" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" OnSelectedIndexChanged="cboSubComponentID_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvSubComponentID" runat="server" ErrorMessage="Sub Component required."
                            ValidationGroup="IncidentCompType" ControlToValidate="cboSubComponentID" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSubComponentName" runat="server" Text="Sub Component Description"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtSubComponentName" runat="server" Width="300px" MaxLength="250"
                            TextMode="MultiLine" Enabled="False" />
                        <asp:CheckBox ID="chkIsAllowEdit" Text="Allow Edit" runat="server" Visible="false" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblModus" runat="server" Text="Modus"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtModus" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="IncidentCompType"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="IncidentCompType" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
