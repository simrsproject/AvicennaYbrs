<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicFeeGuarantorCategoryItemListDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicFeeGuarantorCategoryItemListDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumParamedicFeeItem" runat="server" ValidationGroup="ParamedicFeeItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ParamedicFeeItem"
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
                        <telerik:RadComboBox ID="cboItemID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboItemID_ItemDataBound"
                            OnItemsRequested="cboItemID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 10 result
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                            ControlToValidate="cboItemID" SetFocusOnError="True" ValidationGroup="ParamedicFeeItem"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblInfoFee" runat="server" Text="Fee"></asp:Label>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblInfoDeduction" runat="server" Text="Deduction"></asp:Label>
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
                    </td>
                    <td class="entry">
                        <table width="100%">
                            <tr>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsParamedicFeeUsingPercentage" runat="server" Text="Fee Using Percentage" />
                                </td>
                                <td class="entry">
                                    <asp:CheckBox ID="chkIsDeductionFeeUsePercentage" Text="Deduction Using Percentage"
                                        runat="server" />
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
                    <td class="label">
                        <asp:Label ID="lblFeeAmount" runat="server" Text="Amount for Patient Direct"></asp:Label>
                    </td>
                    <td class="entry">
                        <table width="100%">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtParamedicFeeAmount" runat="server" Width="100px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtDeductionFeeAmount" runat="server" Width="100px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFeeAmount" runat="server" ErrorMessage="Fee Amount required."
                            ControlToValidate="txtParamedicFeeAmount" ValidationGroup="ParamedicFeeItem"
                            SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDeductionAmount" runat="server" ErrorMessage="Deduction Amount required."
                            ControlToValidate="txtDeductionFeeAmount" ValidationGroup="ParamedicFeeItem"
                            SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFeeAmountReferral" runat="server" Text="Amount for Patient Referral"></asp:Label>
                    </td>
                    <td class="entry">
                        <table width="100%">
                            <tr>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtParamedicFeeAmountReferral" runat="server" Width="100px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtDeductionFeeAmountReferral" runat="server" Width="100px">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvFeeAmountReferral" runat="server" ErrorMessage="Fee Amount Referral required."
                            ControlToValidate="txtParamedicFeeAmountReferral" ValidationGroup="ParamedicFeeItem"
                            SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDeductionAmountReferral" runat="server" ErrorMessage="Deduction Amount Referral required."
                            ControlToValidate="txtDeductionFeeAmountReferral" ValidationGroup="ParamedicFeeItem"
                            SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ParamedicFeeItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ParamedicFeeItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>