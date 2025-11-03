<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReferralDetailItem.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.Referralv2.ReferralDetailItem" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumReferral" runat="server" ValidationGroup="Referral" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Referral"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<%@ Register Src="~/CustomControl/AddressCtl.ascx" TagName="AddressCtl" TagPrefix="uc1" %>
<%@ Register Src="~/PCareCommon/LookUp/PCareReferenceCtl.ascx" TagPrefix="uc2" TagName="PCareReferenceCtl" %>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReferralID" runat="server" Text="Referral ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReferralID" runat="server" Width="100px" MaxLength="10" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvReferralID" runat="server" ErrorMessage="Referral ID required."
                            ControlToValidate="txtReferralID" SetFocusOnError="True" ValidationGroup="Referral"
                            Width="100%">
                            <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr runat="server" id="trPcare">
                    <td colspan="4">
                        <table width="100%" cellpadding="0" cellspacing="0">

                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="PCare Code"></asp:Label>
                                </td>
                                <td class="entry">

                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="100px">
                                                <telerik:RadTextBox ID="txtPCareItemID" runat="server" Width="100px" ShowButton="true"
                                                    ClientEvents-OnButtonClick="openWinPCareLookUp" OnTextChanged="txtPCareItemID_TextChanged"
                                                    AutoPostBack="true" />

                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPCareItemName" runat="server" CssClass="labeldescription" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblReferralName" runat="server" Text="Referral Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtReferralName" runat="server" Width="300px" MaxLength="100" TextMode="MultiLine" />
                    </td>
                    <td width="20">
                        <asp:RequiredFieldValidator ID="rfvReferralName" runat="server" ErrorMessage="Referral Name required."
                            ControlToValidate="txtReferralName" SetFocusOnError="True" ValidationGroup="Referral"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblShortName" runat="server" Text="Short Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtShortName" runat="server" Width="100px" MaxLength="35" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblDepartmentName" runat="server" Text="Department Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtDepartmentName" runat="server" Width="300px" MaxLength="100" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            AutoPostBack="false" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                            OnItemsRequested="cboParamedicID_ItemsRequested">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ParamedicName") %>
                            </ItemTemplate>
                            <FooterTemplate>
                                Note : Show max 10 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblTaxRegistrationNo" runat="server" Text="Tax Registration No"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtTaxRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr style="display: none">
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsPKP" runat="server" Text="PKP" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsRefferalFrom" runat="server" Text="Referral From" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsRefferalTo" runat="server" Text="Referral To" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label"></td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                    </td>
                    <td width="20"></td>
                    <td></td>
                </tr>

            </table>
        </td>
    </tr>
</table>
<uc1:AddressCtl ID="AddressCtl1" runat="server" />
<table>
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="Referral"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="Referral" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
