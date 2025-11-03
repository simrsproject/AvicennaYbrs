<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParamedicFeeItemGuarantorDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicFeeItemGuarantorDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumParamedicFeeItemGuarantor" runat="server" ValidationGroup="ParamedicFeeItemGuarantor" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="ParamedicFeeItemGuarantor"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" style="vertical-align: top">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" MarkFirstMatch="False" OnItemDataBound="cboGuarantorID_ItemDataBound"
                            OnItemsRequested="cboGuarantorID_ItemsRequested">
                            <FooterTemplate>
                                Note : Show max 10 result
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvGuarantorID" runat="server" ErrorMessage="Guarantor required."
                            ControlToValidate="cboGuarantorID" SetFocusOnError="True" ValidationGroup="ParamedicFeeItemGuarantor"
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
                            ControlToValidate="txtParamedicFeeAmount" ValidationGroup="ParamedicFeeItemGuarantor"
                            SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDeductionAmount" runat="server" ErrorMessage="Deduction Amount required."
                            ControlToValidate="txtDeductionFeeAmount" ValidationGroup="ParamedicFeeItemGuarantor"
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
                            ControlToValidate="txtParamedicFeeAmountReferral" ValidationGroup="ParamedicFeeItemGuarantor"
                            SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDeductionAmountReferral" runat="server" ErrorMessage="Deduction Amount Referral required."
                            ControlToValidate="txtDeductionFeeAmountReferral" ValidationGroup="ParamedicFeeItemGuarantor"
                            SetFocusOnError="True" Width="100%">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="ParamedicFeeItemGuarantor"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="ParamedicFeeItemGuarantor" Visible='<%# DataItem is GridInsertionObject %>'>
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
