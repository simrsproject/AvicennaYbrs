<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeLoanItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.Payroll.Transaction.EmployeeLoanItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeLoanItem" runat="server" ValidationGroup="EmployeeLoanItem" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeLoanItem"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblEmployeeLoanDetailID" runat="server" Text="Employee Loan Detail ID"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeLoanDetailID" runat="server" Width="300px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblInstallmentNumber" runat="server" Text="Installment Number"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtInstallmentNumber" runat="server" Width="100px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblPlanDate" runat="server" Text="Plan Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtPlanDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        Payment Period
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboStartPaymentID" runat="server" Width="304px" AllowCustomText="true"
                            EnableLoadOnDemand="true" DataTextField="PayrollPeriodName" DataValueField="PayrollPeriodID"
                            OnItemsRequested="cboStartPaymentID_ItemsRequested" OnItemDataBound="cboStartPaymentID_DataBound">
                            <FooterTemplate>
                                Note : Show max 12 items
                            </FooterTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPaymentPeriod" runat="server" ErrorMessage="Payroll Period required."
                            ControlToValidate="cboStartPaymentID" SetFocusOnError="True" ValidationGroup="EmployeeLoanItem"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPlanAmount" runat="server" Text="Plan Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPlanAmount" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPlanAmount" runat="server" ErrorMessage="Plan Amount required."
                            ControlToValidate="txtPlanAmount" SetFocusOnError="True" ValidationGroup="EmployeeLoanItem"
                            Width="100%">
                            <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblMainPayment" runat="server" Text="Main Payment"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtMainPayment" runat="server" Width="100px" Value="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblInterestPayment" runat="server" Text="Interest Payment"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtInterestPayment" runat="server" Width="100px" Value="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeLoanItem"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeLoanItem" Visible='<%# DataItem is GridInsertionObject %>'>
                        </asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <table>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblActualDate" runat="server" Text="Actual Date"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtActualDate" runat="server" Width="100px" />
                    </td>
                    <td width="20px">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblActualAmount" runat="server" Text="Actual Amount"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtActualAmount" runat="server" Width="100px" Value="0" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label">
                    </td>
                    <td class="entry">
                        <asp:CheckBox ID="chkIsPaid" runat="server" Text="Is Paid" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%">
</table>
