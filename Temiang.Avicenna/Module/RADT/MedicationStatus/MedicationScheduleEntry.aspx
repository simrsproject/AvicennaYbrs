<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="MedicationScheduleEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Medication.MedicationScheduleEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate" Display="None"></asp:CustomValidator>
    <div style="font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; font-size: 20px; padding-top: 6px; padding-bottom: 6px; padding-left: 6px; background-color: #4CAF50; color: white;">
        <asp:Label runat="server" ID="lblItemDescription"/>
    </div>
    <fieldset>
        <legend>Patient Info</legend>
        <table width="100%">
            <tr>
                <td class="label">Patient Name</td>
                <td class="entry">
                    <asp:Label runat="server" ID="lblPatientName" Font-Size="12px" Font-Bold="True"/>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Service Unit</td>
                <td class="entry">
                    <asp:Label runat="server" ID="lblServiceUnitName"/>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Room / Bed</td>
                <td class="entry">
                    <asp:Label runat="server" ID="lblRoomAndBed"/>
                </td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Medication</legend>
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label runat="server" ID="lblScheduleTime" Text="Schedule Time"/>
                     
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 100px">
                                <telerik:RadDatePicker ID="txtScheduleDate" runat="server" Width="100px" Enabled="False" />
                            </td>
                            <td style="width: 93px">
                                <telerik:RadTimePicker ID="txtScheduleTime" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" runat="server" Width="93px" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Qty</td>
                <td class="entry">
                    <table width="100%">
                        <tr>
                            <td style="width: 60px">
                                <telerik:RadNumericTextBox ID="txtQty" runat="server" Width="100%" />
                            </td>
                            <td style="width: 60px">
                                <telerik:RadTextBox ID="txtItemUnit" runat="server" Width="100%" Enabled="False" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="20px"></td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
