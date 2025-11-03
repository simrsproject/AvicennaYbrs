<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveUsedEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Medication.MedicationReceiveUsedEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="entry"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate" Display="None"></asp:CustomValidator>
    <div style="font-family: 'Trebuchet MS', Arial, Helvetica, sans-serif; font-size: 20px; padding-top: 6px; padding-bottom: 6px; padding-left: 6px; background-color: #4CAF50; color: white;">
        <asp:Label runat="server" ID="lblItemDescription" />
    </div>
    <fieldset>
        <legend>Patient Info</legend>
        <table width="100%">
            <tr>
                <td class="label">Patient Name</td>
                <td class="entry">
                    <asp:Label runat="server" ID="lblPatientName" Font-Size="12px" Font-Bold="True" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Service Unit</td>
                <td class="entry">
                    <asp:Label runat="server" ID="lblServiceUnitName" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Room / Bed</td>
                <td class="entry">
                    <asp:Label runat="server" ID="lblRoomAndBed" />
                </td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Medication</legend>
        <table width="100%">
            <tr>
                <td class="label">Schedule Time
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
            <tr runat="server" id="trIsNotConsume">
                <td class="label"></td>
                <td class="entry">
                    <asp:CheckBox ID="chkIsNotConsume" runat="server" Text="Medicine not consumed by patients" />
                </td>
                <td width="20"></td>
                <td></td>
            </tr>
            <tr runat="server" id="trReSchedule">
                <td class="label"></td>
                <td class="entry">
                    <asp:CheckBox ID="chkIsReSchedule" runat="server" Text="Reschedule Medication" />
                </td>
                <td width="20"></td>
                <td></td>
            </tr>
            <tr runat="server" id="trVoidSchedule">
                <td class="label"></td>
                <td class="entry">
                    <asp:CheckBox ID="chkIsVoidSchedule" runat="server" Text="Void Schedule Medication" />
                </td>
                <td width="20"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Setup Time</td>
                <td>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadTimePicker ID="txtSetupTime" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" runat="server" Width="93px" />
                            </td>
                            <td></td>
                            <td>By</td>
                            <td>
                                <telerik:RadTextBox ID="txtSetupBy" runat="server" Width="200px" Enabled="False" /></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Handovers Time</td>
                <td>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadTimePicker ID="txtHandoversTime" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" runat="server" Width="93px" />
                            </td>
                            <td></td>
                            <td>By</td>
                            <td>
                                <telerik:RadTextBox ID="txtHandoversBy" runat="server" Width="200px" Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label"></td>
                <td>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 112px"></td>
                            <td></td>
                            <td>To</td>
                            <td>
                                <telerik:RadTextBox ID="txtHandoversTo" runat="server" Width="200px" Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Verification Time</td>
                <td>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadTimePicker ID="txtVerificationTime" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" runat="server" Width="93px" />
                            </td>
                            <td></td>
                            <td>By</td>
                            <td>
                                <telerik:RadTextBox ID="txtVerificationBy" runat="server" Width="200px" Enabled="False" /></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Realized Time</td>
                <td>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadTimePicker ID="txtRealizedTime" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" runat="server" Width="93px" />
                            </td>
                            <td></td>
                            <td>By</td>
                            <td>
                                <telerik:RadTextBox ID="txtRealizedBy" runat="server" Width="200px" Enabled="False" /></td>
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
                            <td class="label" style="width: 100px">&nbsp;Realization Bal</td>
                            <td style="width: 40px">
                                <telerik:RadNumericTextBox ID="txtBalanceRealQty" runat="server" Width="100%" Enabled="False" />
                            </td>
                            <td class="label" style="width: 100px">&nbsp;Setup Bal</td>
                            <td style="width: 40px">
                                <telerik:RadNumericTextBox ID="txtBalanceQty" runat="server" Width="100%" Enabled="False" />
                            </td>

                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="20px"></td>
            </tr>
            <tr runat="server" id="trReason">
                <td class="label">Reason
                </td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboSRMedicationReason" runat="server" Width="100%" />
                </td>
                <td width="20"></td>
                <td></td>
            </tr>
            <tr runat="server" id="trHandovers">
                <td class="label">Handovers Receive By
                </td>
                <td class="entry">
                    <telerik:RadComboBox runat="server" ID="cboHandoversToUserID" Width="100%" EmptyMessage="Select a Receiver"
                        EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                        <WebServiceSettings Method="Users" Path="~/WebService/ComboBoxDataService.asmx" />
                    </telerik:RadComboBox>
                </td>
                <td width="20"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Note</td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtNote" runat="server" Width="100%" TextMode="MultiLine" Height="60px" />
                </td>
            </tr>

        </table>
    </fieldset>
</asp:Content>
