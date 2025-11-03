<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HandHygieneItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.HandHygieneItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumHandHygieneItem" runat="server" ValidationGroup="HandHygieneItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="HandHygieneItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSROpportunity" runat="server" Text="Opportunity"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSROpportunity" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSROpportunity" runat="server" ErrorMessage="Opportunity required."
                            ControlToValidate="cboSROpportunity" SetFocusOnError="True" ValidationGroup="HandHygieneItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRHandWashType" runat="server" Text="Hand Wash Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRHandWashType" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRHandWashType" runat="server" ErrorMessage="Hand Wash Type required."
                            ControlToValidate="cboSRHandWashType" SetFocusOnError="True" ValidationGroup="HandHygieneItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsWearGloves" runat="server" Text="Wear Gloves" />
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td><asp:CheckBox ID="chkIsApply6Steps" runat="server" Text="Apply 6 Steps" /></td>
                                <td>&nbsp;&nbsp;&nbsp;</td>
                                <td class="label" style="width:50px"><asp:Label ID="lblSRApply6StepsResult" runat="server" Text="Result"></asp:Label></td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="cboSRApply6StepsResult" Width="137px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRHandHygieneNote" runat="server" Text="Note"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRHandHygieneNote" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRHandHygieneNote" runat="server" ErrorMessage="Note required."
                            ControlToValidate="cboSRHandHygieneNote" SetFocusOnError="True" ValidationGroup="HandHygieneItem"
                            Width="100%" Visible="false">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="HandHygieneItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="HandHygieneItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
