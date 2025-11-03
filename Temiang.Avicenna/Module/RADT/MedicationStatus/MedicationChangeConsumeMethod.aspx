<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="MedicationChangeConsumeMethod.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Medication.MedicationChangeConsumeMethod" %>

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
            <td class="label">Realized Time</td>
            <td>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadTimePicker ID="txtRealizedTime" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" runat="server" Width="93px" Enabled="False" />
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
            <td class="label">Balance Qty (Real)</td>
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
        <legend>Change to</legend>
        <table width="100%">

            <tr>
                <td class="label">Consume Method</td>
                <td class="entry">
                    <telerik:RadComboBox runat="server" ID="cboNewConsumeMethod" Width="100%" EmptyMessage="Select Consume Method"
                        EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                        <WebServiceSettings Method="ConsumeMethods" Path="~/WebService/ComboBoxDataService.asmx" />
                        <ClientItemTemplate>
                        <div>
                            <ul class="details">
                                <li class="bold"><span>#= Text # </span></li>
                                <li class="small"><span>Time: #= Attributes.TimeSequence #</span></li>
                            </ul>
                        </div>
                        </ClientItemTemplate>
                    </telerik:RadComboBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Consume Qty</td>
                <td class="entry">
                    <table width="100%">
                        <tr>
                            <td style="width: 60px">
                                <telerik:RadTextBox ID="txtNewConsumeQty" runat="server" Width="60px" CssClass="RightAligned" />
                            </td>
                            <td>
                                <telerik:RadComboBox runat="server" ID="cboNewSRConsumeUnit" Width="100%" EmptyMessage="Select Unit Consume">
                                </telerik:RadComboBox>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Medication Consume</td>
                <td class="entry">
                    <telerik:RadComboBox ID="cboNewSRMedicationConsume" runat="server" Width="100%" />
                </td>
                <td width="20px"></td>
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
