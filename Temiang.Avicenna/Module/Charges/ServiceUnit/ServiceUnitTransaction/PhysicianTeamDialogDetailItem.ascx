<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhysicianTeamDialogDetailItem.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Charges.PhysicianTeamDialogDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPhysicianTeamDetailItem" runat="server" ValidationGroup="PhysicianTeamDetailItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PhysicianTeamDetailItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPhysicianID" runat="server" Text="Physician"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboPhysicianID" Width="304px" EnableLoadOnDemand="True"
                            HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboPhysicianID_ItemDataBound"
                            OnItemsRequested="cboPhysicianID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 15 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvPhysicianID" runat="server" ErrorMessage="Physician required."
                            ValidationGroup="PhysicianTeamDetailItem" ControlToValidate="cboPhysicianID"
                            SetFocusOnError="True" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPeriod" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" Enabled="False">
                        </telerik:RadDatePicker>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine"
                            Height="50px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PhysicianTeamDetailItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PhysicianTeamDetailItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
