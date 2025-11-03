<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveReconciliatonStat.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveReconciliatonStat" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <legend>Reconsiliation Admision Status</legend>
        <table width="100%">
            <tr>
                <td class="label">Reconciliation Date</td>
                <td>
                    <telerik:RadDateTimePicker ID="txtStatusDateTime" runat="server" Width="170px">
                        <DateInput ID="DateInput1" runat="server"
                            DisplayDateFormat="dd/MM/yyyy HH:mm"
                            DateFormat="dd/MM/yyyy HH:mm">
                        </DateInput>
                    </telerik:RadDateTimePicker>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Reconciliaton Status</td>
                <td>
                    <telerik:RadRadioButtonList runat="server" ID="optReconStatus" AutoPostBack="false">
                    </telerik:RadRadioButtonList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvReconStatus" runat="server"
                        ControlToValidate="optReconStatus" ForeColor="Red"
                        ErrorMessage="" />
                </td>
            </tr>
        </table>

    </fieldset>

    <fieldset>
        <legend>Change to</legend>
        <table width="100%">

            <tr>
                <td class="label">Consume Method</td>
                <td class="entry">
                    <telerik:RadComboBox runat="server" ID="cboConsumeMethod" Width="100%" EmptyMessage="Select Unit Consume"
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
                            <td style="width: 100px">
                                <telerik:RadTextBox ID="txtConsumeQty" runat="server" Width="90px" CssClass="RightAligned" />
                            </td>
                            <td style="width: 93px">
                                <telerik:RadTextBox ID="txtItemUnit2" runat="server" Width="100px" Enabled="False" />
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
                    <telerik:RadComboBox ID="cboSRMedicationConsume" runat="server" Width="100%" />
                </td>
                <td width="20px"></td>
            </tr>
        </table>
    </fieldset>


</asp:Content>
