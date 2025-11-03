<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveEdit.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Medication.MedicationReceiveEdit" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate" Display="None"></asp:CustomValidator>
    <table width="100%">
        <tr>
            <td class="label">Service Unit</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="100%" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="label">Room</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRoomName" runat="server" Width="100%" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="label">Bed</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtBed" runat="server" Width="100%" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="label">Patient Name</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="100%" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td class="label">Item Description</td>
            <td style="font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; padding-top: 6px; padding-bottom: 6px; background-color: #4CAF50; color: white;">
                <asp:Label runat="server" ID="lblItemDescription" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">Balance Qty</td>
            <td class="entry">
                <table width="100%">
                    <tr>
                        <td style="width: 100px">
                            <telerik:RadNumericTextBox ID="txtBalanceRealQty" runat="server" Enabled="False" />
                        </td>
                        <td style="width: 93px">
                            <telerik:RadTextBox ID="txtItemUnit" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Consume Method</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtConsumeMethod" runat="server" Width="100%" Enabled="False" />
            </td>
        </tr>
    </table>

    <fieldset>
        <legend>Edit</legend>
        <table width="100%">
            <tr>
                <td class="label">Start Time</td>
                <td>
                    <telerik:RadDateTimePicker ID="txtStartDateTime" runat="server" Width="170px" />
                </td>
                <td></td>
            </tr>
                    <tr>
            <td class="label">Receive Qty</td>
            <td class="entry">
                <table width="100%">
                    <tr>
                        <td style="width: 100px">
                            <telerik:RadNumericTextBox ID="txtReceiveQty" runat="server" />
                        </td>
                        <td style="width: 93px">
                            <telerik:RadTextBox ID="txtItemUnit2" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="20px"></td>
        </tr>
            <tr>
                <td class="label">Route</td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboSRMedicationRoute" runat="server" Width="100%" />
                </td>
                <td width="20px"></td>
            </tr>
            <tr>
                <td class="label">Consume Time</td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboSRMedicationConsume" runat="server" Width="100%" />
                </td>
                <td width="20px"></td>
            </tr>
        </table>
    </fieldset>

</asp:Content>
