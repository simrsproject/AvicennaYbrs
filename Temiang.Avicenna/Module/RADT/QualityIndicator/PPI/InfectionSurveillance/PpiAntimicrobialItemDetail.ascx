<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PpiAntimicrobialItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiAntimicrobialItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumPpiAntimicrobial" runat="server" ValidationGroup="PpiAntimicrobial" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="PpiAntimicrobial"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRTherapyGroup" runat="server" Text="Therapy Group"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRTherapyGroup" runat="server" Width="304px" AllowCustomText="true"
                            Filter="Contains" OnSelectedIndexChanged="cboSRTherapyGroup_SelectedIndexChanged"
                            AutoPostBack="True" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvSRTherapyGroup" runat="server" ErrorMessage="Therapy Group required."
                            ValidationGroup="PpiAntimicrobial" ControlToValidate="cboSRTherapyGroup" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTherapyID" runat="server" Text="Therapy"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboTherapyID" runat="server" Width="304px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDosage" runat="server" Text="Dosage"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDosage" runat="server" Width="100px" MaxLength="10"
                                        MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                                </td>
                                <td style="width: 5px"></td>
                                <td>
                                    <telerik:RadComboBox ID="cboSRDosageUnit" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvDosage" runat="server" ErrorMessage="Dosage required."
                            ControlToValidate="txtDosage" SetFocusOnError="True" ValidationGroup="PpiAntimicrobial"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvSRDosageUnit" runat="server" ErrorMessage="Dosage Unit required."
                            ControlToValidate="cboSRDosageUnit" SetFocusOnError="True" ValidationGroup="PpiAntimicrobial"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="txtStartDate" runat="server" Width="100px" />
                                </td>
                                <td style="width: 20px">
                                    to
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtEndDate" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRAntimicrobialApplicationTiming" runat="server" Text="Timing"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSRAntimicrobialApplicationTiming" runat="server" Width="304px"
                            AllowCustomText="true" Filter="Contains" />
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="PpiAntimicrobial"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="PpiAntimicrobial" Visible='<%# DataItem is GridInsertionObject %>'>
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
