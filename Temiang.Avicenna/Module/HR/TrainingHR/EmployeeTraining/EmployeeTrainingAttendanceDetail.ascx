<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTrainingAttendanceDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingAttendanceDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:ValidationSummary ID="vsumEmployeeTrainingHistory" runat="server" ValidationGroup="EmployeeTrainingHistory" />
<asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="EmployeeTrainingHistory"
    ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="50%" valign="top">
            <table>
                <tr style="display: none">
                    <td class="label" />
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtEmployeeTrainingHistoryID" runat="server" Width="300px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr style="display: none">
                    <td class="label" />
                    <td class="entry">
                        <telerik:RadNumericTextBox ID="txtTrainingID" runat="server" Width="300px" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label">Employee Name
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboPersonID" runat="server" Width="304px" OnItemsRequested="cboPersonID_ItemsRequested"
                            AllowCustomText="true" EnableLoadOnDemand="true" OnItemDataBound="cboPersonID_ItemDataBound" />
                    </td>
                    <td width="20px">
                        <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                            ControlToValidate="cboPersonID" SetFocusOnError="True" ValidationGroup="EmployeeTrainingHistory"
                            Width="100%">
                            <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSREmployeeTrainingRole" runat="server" Text="Role"></asp:Label>
                    </td>
                    <td class="entry">
                        <telerik:RadComboBox ID="cboSREmployeeTrainingRole" runat="server" Width="300px" AllowCustomText="true"
                            Filter="Contains" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td class="label" />
                    <td class="entry">
                        <asp:CheckBox ID="chkIsAttending" runat="server" Text="Attend" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
                <tr>
                    <td align="right" colspan="2" style="height: 26px">
                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" ValidationGroup="EmployeeTrainingHistory"
                            Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                        <asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert"
                            ValidationGroup="EmployeeTrainingHistory" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                        &nbsp;
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                            CommandName="Cancel"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%" valign="top">
            <table>
                <tr>
                    <td class="label">Note
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" TextMode="MultiLine" />
                    </td>
                    <td width="20px" />
                    <td />
                </tr>
            </table>
        </td>
    </tr>
</table>
