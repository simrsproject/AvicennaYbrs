<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTrainingDetailItemCompDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingDetailItemCompDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeTrainingItem" runat="server" ValidationGroup="EmployeeTrainingDetailItemComp" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeTrainingDetailItemComp"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="lblComponentID" Text="Component Name" />
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox runat="server" ID="cboComponentID" Width="300px" />
                    </td>
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPrice" runat="server" Text="Price"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="100px" MaxLength="10"
                            MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeTrainingDetailItemComp"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeTrainingDetailItemComp" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <table width="100%">
            </table>
        </td>
    </tr>
</table>
