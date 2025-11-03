<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BedDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.BedDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumBed" runat="server" ValidationGroup="Bed" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Bed" ErrorMessage=""
    OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnRegistrationNo" />
<asp:HiddenField runat="server" ID="hdnSRBedStatus" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table style="width:100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="10" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvBedID" runat="server" ErrorMessage="Bed No required."
                            ControlToValidate="txtBedID" SetFocusOnError="True" ValidationGroup="Bed" Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboClassID" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvClassID" runat="server" ErrorMessage="Class required."
                            ControlToValidate="cboClassID" SetFocusOnError="True" ValidationGroup="Bed" Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDefaultChargeClassID" runat="server" Text="Tariff Class Default"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboDefaultChargeClassID" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvDefaultChargeClassID" runat="server" ErrorMessage="Tariff Class Default required."
                            ControlToValidate="cboDefaultChargeClassID" SetFocusOnError="True" ValidationGroup="Bed" Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsTemporary" runat="server" Text="Temporary" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsNeedConfirmation" runat="server" Text="Need Confirmation" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsSharedTo3rdParty" runat="server" Text="Shared To 3rd Party Apps" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="Bed"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="Bed" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Height="100px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>

</table>
