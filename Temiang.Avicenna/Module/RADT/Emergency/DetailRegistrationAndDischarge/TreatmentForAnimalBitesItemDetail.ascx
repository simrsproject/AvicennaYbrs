<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreatmentForAnimalBitesItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emergency.TreatmentForAnimalBitesItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRegistrationItemRule" runat="server" ValidationGroup="TreatmentForAnimalBitesItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="TreatmentForAnimalBitesItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRTreatmentForAnimalBites" runat="server" Text="Therapy"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRTreatmentForAnimalBites" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvSRTreatmentForAnimalBites" runat="server" ErrorMessage="Therapy required."
                            ValidationGroup="TreatmentForAnimalBitesItem" ControlToValidate="cboSRTreatmentForAnimalBites" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image21" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="TreatmentForAnimalBitesItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="TreatmentForAnimalBitesItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%" runat="server">
                
            </table>
        </td>
    </tr>
</table>