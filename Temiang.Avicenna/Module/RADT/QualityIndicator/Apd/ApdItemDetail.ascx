<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApdItemDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.ApdItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumApdSurveyItem" runat="server" ValidationGroup="ApdSurveyItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ApdSurveyItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%">
    <tr>
        <td style="width: 50%; vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRApdType" runat="server" Text="APD Type"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboSRApdType" Width="300px" AllowCustomText="true"
                            Filter="Contains">
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvSRApdType" runat="server" ErrorMessage="APD Type required."
                            ControlToValidate="cboSRApdType" SetFocusOnError="True" ValidationGroup="ApdSurveyItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblIndication" runat="server" Text="Indication"></asp:Label></td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rbtIndication" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Text="Correct" />
                            <asp:ListItem Value="0" Text="Incorrect" />
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvIndication" runat="server" ErrorMessage="Indication required."
                            ControlToValidate="rbtIndication" SetFocusOnError="True" ValidationGroup="ApdSurveyItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblUsageTime" runat="server" Text="Usage Time"></asp:Label></td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rbtUsageTime" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Text="Appropriate" />
                            <asp:ListItem Value="0" Text="Inappropriate" />
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvUsageTime" runat="server" ErrorMessage="Usage Time required."
                            ControlToValidate="rbtUsageTime" SetFocusOnError="True" ValidationGroup="ApdSurveyItem"
                            Width="100%">
                            <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblUsageTechnique" runat="server" Text="Usage Technique"></asp:Label></td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rbtUsageTechnique" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Text="Correct" />
                            <asp:ListItem Value="0" Text="Incorrect" />
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvUsageTechnique" runat="server" ErrorMessage="Usage Technique required."
                            ControlToValidate="rbtUsageTechnique" SetFocusOnError="True" ValidationGroup="ApdSurveyItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReleaseTechnique" runat="server" Text="Release Technique"></asp:Label></td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rbtReleaseTechnique" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Text="Correct" />
                            <asp:ListItem Value="0" Text="Incorrect" />
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvReleaseTechnique" runat="server" ErrorMessage="Release Technique required."
                            ControlToValidate="rbtReleaseTechnique" SetFocusOnError="True" ValidationGroup="ApdSurveyItem"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ApdSurveyItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ApdSurveyItem" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
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
