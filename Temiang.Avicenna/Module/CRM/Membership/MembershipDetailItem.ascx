<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MembershipDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.CRM.MembershipDetailItem" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPatientMembershipDetail" runat="server" ValidationGroup="PatientMembershipDetail" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PatientMembershipDetail"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<asp:HiddenField runat="server" ID="hdnTotalAmount" />
<asp:HiddenField runat="server" ID="hdnReedeemAmount" />
<asp:HiddenField runat="server" ID="hdnBalanceAmount" />
<asp:HiddenField runat="server" ID="hdnRewardPoint" />
<asp:HiddenField runat="server" ID="hdnRewardPointRefferal" />
<asp:HiddenField runat="server" ID="hdnClaimedPoint" />
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="30%" valign="top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblStartDate" runat="server" Text="Active Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="120px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="Active Date required."
                            ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="PatientMembershipDetail"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblEndDate" runat="server" Text="Valid Thru"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="120px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="Valid Thru required."
                            ControlToValidate="txtEndDate" SetFocusOnError="True" ValidationGroup="PatientMembershipDetail"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td class="entry" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PatientMembershipDetail"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PatientMembershipDetail" Visible='<%# DataItem is GridInsertionObject %>'>
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
        </td>
    </tr>
</table>