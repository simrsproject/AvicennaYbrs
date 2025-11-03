<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationReconStat.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.MedicationReconStat" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function HideChangeConsume() {
            document.getElementById("divChangeConsume").style.display = "none";
            Sys.Application.remove_load(HideChangeConsume);
        }

        <%=optReconStatus.SelectedValue != "CC"?"Sys.Application.add_load(HideChangeConsume);":string.Empty %>

    </script>
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
                        <DateInput ID="dtInput" runat="server"
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
                    <telerik:RadRadioButtonList runat="server" ID="optReconStatus" AutoPostBack="False" >
                        <ClientEvents OnSelectedIndexChanged="selectedIndexChanged" />
                    </telerik:RadRadioButtonList>
                    
                <script type="text/javascript">
                    (function(global, undefined) {
                        function selectedIndexChanged(list, args) {
                            var newVal = args.get_newSelectedIndex();
                            
                            var dcc = document.getElementById("divChangeConsume");
                            if (newVal === 1) {
                                dcc.style.display = "block";
                            } else {
                                dcc.style.display = "none";
                            }
                        }
                        global.selectedIndexChanged = selectedIndexChanged;
                    })(window);
                </script>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvReconStatus" runat="server"
                        ControlToValidate="optReconStatus" ForeColor="Red"
                        ErrorMessage="" />
                </td>
            </tr>
        </table>

    </fieldset>

    <fieldset id="divChangeConsume">
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
                                <telerik:RadNumericTextBox ID="txtConsumeQty" runat="server" Width="90px" />
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
