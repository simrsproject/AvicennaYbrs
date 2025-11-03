<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlPlanCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.ResumeMedis.ControlPlanCtl" %>
<fieldset style="width: 98%">
    <legend><strong>NEXT CONTROL PLAN</strong></legend>
    <table style="width: 100%">
        <tr>
            <th style="width: 30px" rowspan="2">No</th>
            <th style="width: 130px" rowspan="2">Date Hour</th>
            <th style="width: 300px" rowspan="2">Service Unit</th>
            <th style="width: 300px" rowspan="2">Physician Name</th>
            <th style="width: 230px" rowspan="2">Specialty</th>
            <th style="width: 170px" colspan="3">Appointment</th>
            <th></th>
        </tr>
        <tr>
            <th style="width: 120px">No</th>
            <th style="width: 60px">Time</th>
            <th style="width: 60px">Seq No</th>
            <th></th>
        </tr>
        <tr>
            <td>1</td>
            <td>
                <telerik:RadDateTimePicker runat="server" ID="txtControlPlanDateTime01"></telerik:RadDateTimePicker>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboServiceUnitID01" Width="100%" EmptyMessage="Select ServiceUnit"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                    <WebServiceSettings Method="ServiceUnitFoRegistration" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboParamedicName01" Width="100%" EmptyMessage="Select Paramedics"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false" OnClientItemsRequesting="OnClientItemsRequesting1">
                    <WebServiceSettings Method="ServiceUnitParamedics" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                            <div onclick="SetSpecialtyValue('txtSpecialtyName01','#= Attributes.SpecialtyName #')">
                                <ul class="details">
                                    <li class="bold"><span>#= Text # </span></li>
                                    <li class="small"><span>#= Attributes.SpecialtyName #</span></li>
                                </ul>
                            </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtSpecialtyName01" ClientIDMode="Static" Width="100%"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentNo01" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentTime01" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentQue01" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>

            <td></td>
        </tr>
        <tr>
            <td>2</td>
            <td>
                <telerik:RadDateTimePicker runat="server" ID="txtControlPlanDateTime02"></telerik:RadDateTimePicker>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboServiceUnitID02" Width="100%" EmptyMessage="Select ServiceUnit"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                    <WebServiceSettings Method="ServiceUnitFoRegistration" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboParamedicName02" Width="100%" EmptyMessage="Select Paramedics"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false" OnClientItemsRequesting="OnClientItemsRequesting2">
                    <WebServiceSettings Method="ServiceUnitParamedics" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                            <div onclick="SetSpecialtyValue('txtSpecialtyName02','#= Attributes.SpecialtyName #')">
                                <ul class="details">
                                    <li class="bold"><span>#= Text # </span></li>
                                    <li class="small"><span>#= Attributes.SpecialtyName #</span></li>
                                </ul>
                            </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtSpecialtyName02" ClientIDMode="Static" Width="100%"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentNo02" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentTime02" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentQue02" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>

            <td></td>
        </tr>
        <tr>
            <td>3</td>
            <td>
                <telerik:RadDateTimePicker runat="server" ID="txtControlPlanDateTime03"></telerik:RadDateTimePicker>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboServiceUnitID03" Width="100%" EmptyMessage="Select ServiceUnit"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                    <WebServiceSettings Method="ServiceUnitFoRegistration" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboParamedicName03" Width="100%" EmptyMessage="Select Paramedics"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false" OnClientItemsRequesting="OnClientItemsRequesting3">
                    <WebServiceSettings Method="ServiceUnitParamedics" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                            <div onclick="SetSpecialtyValue('txtSpecialtyName03','#= Attributes.SpecialtyName #')">
                                <ul class="details">
                                    <li class="bold"><span>#= Text # </span></li>
                                    <li class="small"><span>#= Attributes.SpecialtyName #</span></li>
                                </ul>
                            </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtSpecialtyName03" ClientIDMode="Static" Width="100%"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentNo03" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentTime03" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentQue03" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>

            <td></td>
        </tr>
        <tr>
            <td>4</td>
            <td>
                <telerik:RadDateTimePicker runat="server" ID="txtControlPlanDateTime04"></telerik:RadDateTimePicker>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboServiceUnitID04" Width="100%" EmptyMessage="Select ServiceUnit"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                    <WebServiceSettings Method="ServiceUnitFoRegistration" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboParamedicName04" Width="100%" EmptyMessage="Select Paramedics"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false" OnClientItemsRequesting="OnClientItemsRequesting4">
                    <WebServiceSettings Method="ServiceUnitParamedics" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                            <div onclick="SetSpecialtyValue('txtSpecialtyName04','#= Attributes.SpecialtyName #')">
                                <ul class="details">
                                    <li class="bold"><span>#= Text # </span></li>
                                    <li class="small"><span>#= Attributes.SpecialtyName #</span></li>
                                </ul>
                            </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtSpecialtyName04" ClientIDMode="Static" Width="100%"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentNo04" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentTime04" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentQue04" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>

            <td></td>
        </tr>
        <tr>
            <td>5</td>
            <td>
                <telerik:RadDateTimePicker runat="server" ID="txtControlPlanDateTime05"></telerik:RadDateTimePicker>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboServiceUnitID05" Width="100%" EmptyMessage="Select ServiceUnit"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                    <WebServiceSettings Method="ServiceUnitFoRegistration" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboParamedicName05" Width="100%" EmptyMessage="Select Paramedics"
                    EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false" OnClientItemsRequesting="OnClientItemsRequesting5">
                    <WebServiceSettings Method="ServiceUnitParamedics" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                            <div onclick="SetSpecialtyValue('txtSpecialtyName05','#= Attributes.SpecialtyName #')">
                                <ul class="details">
                                    <li class="bold"><span>#= Text # </span></li>
                                    <li class="small"><span>#= Attributes.SpecialtyName #</span></li>
                                </ul>
                            </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtSpecialtyName05" ClientIDMode="Static" Width="100%"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentNo05" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentTime05" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtAppointmentQue05" Width="100%" ReadOnly="true"></telerik:RadTextBox></td>

            <td></td>
        </tr>
    </table>
</fieldset>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function SetSpecialtyValue(id, val) {
            var txt = $find(id);
            txt.set_value(val);
        }
        function OnClientItemsRequesting1(sender, eventArgs) {
            var unitCombo = $find("<%= cboServiceUnitID01.ClientID %>");
            var unitText = unitCombo.get_value();
            var context = eventArgs.get_context();
            context["serviceUnitID"] = unitText;
        }
        function OnClientItemsRequesting2(sender, eventArgs) {
            var unitCombo = $find("<%= cboServiceUnitID02.ClientID %>");
            var unitText = unitCombo.get_value();
            var context = eventArgs.get_context();
            context["serviceUnitID"] = unitText;
        }
        function OnClientItemsRequesting3(sender, eventArgs) {
            var unitCombo = $find("<%= cboServiceUnitID03.ClientID %>");
            var unitText = unitCombo.get_value();
            var context = eventArgs.get_context();
            context["serviceUnitID"] = unitText;
        }
        function OnClientItemsRequesting4(sender, eventArgs) {
            var unitCombo = $find("<%= cboServiceUnitID04.ClientID %>");
            var unitText = unitCombo.get_value();
            var context = eventArgs.get_context();
            context["serviceUnitID"] = unitText;
        }
        function OnClientItemsRequesting5(sender, eventArgs) {
            var unitCombo = $find("<%= cboServiceUnitID05.ClientID %>");
            var unitText = unitCombo.get_value();
            var context = eventArgs.get_context();
            context["serviceUnitID"] = unitText;
        }
    </script>
</telerik:RadCodeBlock>
