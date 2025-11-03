<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationItemRuleDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.Charges.RegistrationItemRuleDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumRegistrationItemRule" runat="server" ValidationGroup="RegistrationItemRule" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="RegistrationItemRule"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="304px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                            OnItemsRequested="cboItemID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 10 result
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="RegistrationItemRule"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rblInclude" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblInclude_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Include</asp:ListItem>
                            <asp:ListItem>Exclude</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:RadioButtonList ID="rblToGuarantor" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">To Guarantor</asp:ListItem>
                            <asp:ListItem>To Patient</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="20">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="RegistrationItemRule"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="RegistrationItemRule" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%" id="tblInclude" runat="server">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSRGuarantorRuleType" runat="server" Text="Rule Type Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboSRGuarantorRuleType" runat="server" Width="200px" />
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsValueInPercent" runat="server" Text="In Percent" />
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="label" style="width: 33%">
                                    <asp:Label ID="Label2" runat="server" Text="IPR/Default"></asp:Label>
                                </td>
                                <td class="label" style="width: 33%">
                                    <asp:Label ID="Label3" runat="server" Text="OPR"></asp:Label>
                                </td>
                                <td class="label" style="width: 33%">
                                    <asp:Label ID="Label4" runat="server" Text="EMR"></asp:Label>
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
                        <asp:Label ID="lblAmountValue" runat="server" Text="Amount Value"></asp:Label>
                    </td>
                    <td class="entry">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="entry" style="width: 33%">
                                    <telerik:RadNumericTextBox ID="txtAmountValue" runat="server" Width="100px" />
                                </td>
                                <td class="entry" style="width: 33%">
                                    <telerik:RadNumericTextBox ID="txtOutpatientAmountValue" runat="server" Width="100px" />
                                </td>
                                <td class="entry" style="width: 33%">
                                    <telerik:RadNumericTextBox ID="txtEmergencyAmountValue" runat="server" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvAmountValue" runat="server" ErrorMessage="Amount Value (IPR/Default) required."
                            ControlToValidate="txtAmountValue" SetFocusOnError="True" ValidationGroup="RegistrationItemRule"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvAmountValueOPR" runat="server" ErrorMessage="Amount Value (OPR) required."
                            ControlToValidate="txtOutpatientAmountValue" SetFocusOnError="True" ValidationGroup="RegistrationItemRule"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvAmountValueEMR" runat="server" ErrorMessage="Amount Value (EMR) required."
                            ControlToValidate="txtEmergencyAmountValue" SetFocusOnError="True" ValidationGroup="RegistrationItemRule"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>