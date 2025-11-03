<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTrainingExternalTrainerDetail.ascx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingExternalTrainerDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeTrainingExternalTrainer" runat="server" ValidationGroup="EmployeeTrainingExternalTrainer" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeTrainingExternalTrainer"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table>
                <tr style="display: none">
                    <td class="label" />
                    <td class="entry">
                        <telerik:RadTextBox ID="txtExternalTrainerSeqNo" runat="server" Width="300px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="Label1" runat="server" Text="Trainer Name"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtExternalTrainerName" runat="server" Width="300px" MaxLength="255" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblPositionAs" runat="server" Text="Position As"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPositionAs" runat="server" Width="300px" MaxLength="255"/>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500"/>
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeTrainingExternalTrainer"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeTrainingExternalTrainer" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table>
                
            </table>
        </td>
    </tr>
</table>