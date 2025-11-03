<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="EmployeeMedicalHistory.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.EmployeeMedicalHistory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "list":
                        location.replace('EmployeeMedicalHistoryList.aspx');
                        break;
                }
            }
            function RowSelected(sender, args) {
                __doPostBack("<%=grdDiagnose.UniqueID%>", "rebind:" + args.getDataKeyValue("RegistrationNo"));
            }

            function openWinResultLab() {
                var oWnd = $find("<%= winInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                oWnd.setUrl('LaboratoryResultTest.aspx?regNo=' + regNo.get_value());
                oWnd.show();
                oWnd.maximize();
            }
            function openWinResultRad() {
                var oWnd = $find("<%= winInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                oWnd.setUrl('RadiologyResultTest.aspx?regNo=' + regNo.get_value());
                oWnd.show();
                oWnd.maximize();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="600px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true"
        ID="winInfo">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDiagnose">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationDateTime" />
                    <telerik:AjaxUpdatedControl ControlID="txtDischargeDateTimeInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtParamedicName" />
                    <telerik:AjaxUpdatedControl ControlID="txtServiceUnitName" />
                    <telerik:AjaxUpdatedControl ControlID="txtRoomName" />
                    <telerik:AjaxUpdatedControl ControlID="txtBedID" />
                    <telerik:AjaxUpdatedControl ControlID="txtGuarantorName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationDateTime" />
                    <telerik:AjaxUpdatedControl ControlID="txtDischargeDateTimeInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtParamedicName" />
                    <telerik:AjaxUpdatedControl ControlID="txtServiceUnitName" />
                    <telerik:AjaxUpdatedControl ControlID="txtRoomName" />
                    <telerik:AjaxUpdatedControl ControlID="txtBedID" />
                    <telerik:AjaxUpdatedControl ControlID="txtGuarantorName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationDateTime" />
                    <telerik:AjaxUpdatedControl ControlID="txtDischargeDateTimeInfo" />
                    <telerik:AjaxUpdatedControl ControlID="txtParamedicName" />
                    <telerik:AjaxUpdatedControl ControlID="txtServiceUnitName" />
                    <telerik:AjaxUpdatedControl ControlID="txtRoomName" />
                    <telerik:AjaxUpdatedControl ControlID="txtBedID" />
                    <telerik:AjaxUpdatedControl ControlID="txtGuarantorName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rblTestResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnResultLab" />
                    <telerik:AjaxUpdatedControl ControlID="btnResultRad" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="List" Value="list" ImageUrl="~/Images/Toolbar/details16.png"
                HoveredImageUrl="~/Images/Toolbar/details16_h.png" DisabledImageUrl="~/Images/Toolbar/details16_d.png" />
            <telerik:RadToolBarButton IsSeparator="True" runat="server" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="vertical-align: top">
                                <table width="150px">
                                    <tr>
                                        <td style="vertical-align: top">
                                            <fieldset id="FieldSet1" style="width: 100px; min-height: 100px;">
                                                <legend>Photo</legend>
                                                <asp:Image runat="server" ID="imgPhoto" Width="100px" Height="100px" />
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeNumber" runat="server" Text="Employee No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBirthDate" runat="server" Text="City / Date Of Birth"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 190px">
                                                        <telerik:RadTextBox ID="txtPlaceBirth" runat="server" Width="180px" ReadOnly="true" />
                                                    </td>
                                                    <td style="width: 10px">/
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="100px" Enabled="false" MinDate="01/01/1900"
                                                            MaxDate="12/31/2999" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRGenderType" runat="server" Text="Gender"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                                <asp:ListItem Value="M" Text="Male" />
                                                <asp:ListItem Value="F" Text="Female" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="True" />
                                            <telerik:RadTextBox ID="txtPatientID" runat="server" Visible="false" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSREmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtSREmployeeStatus" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblJoinDate" runat="server" Text="Join - (Est.) Resign Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtJoinDate" runat="server" Width="100px" Enabled="false" MinDate="01/01/1900"
                                                            MaxDate="12/31/2999"/>
                                                    </td>
                                                    <td style="width: 10px">
                                                        <asp:Label ID="lblResignDate" runat="server" Text=" - "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtResignDate" runat="server" Width="100px" Enabled="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblOrganizationName" runat="server" Text="Organization Unit"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtOrganizationName" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="Label3" runat="server" Text="Service Year"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txtServiceYear" runat="server" Width="100px" ReadOnly="true" />
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtServiceYearText" runat="server" Width="196px" ReadOnly="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtSREmploymentType" runat="server" Width="300px" ReadOnly="true" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="25%" valign="top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Reg. Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDateFrom" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDateTo" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left;">
                            <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationType" runat="server" Text="Reg. Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSRRegistrationType" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="grdList" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnNeedDataSource="grdList_NeedDataSource">
                    <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo">
                        <Columns>
                            <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" SortExpression="RegistrationNo"
                                HeaderText="Registration No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" />
                            <telerik:GridDateTimeColumn DataField="RegistrationDateTime" HeaderText="Reg. Date"
                                UniqueName="RegistrationDateTime" SortExpression="RegistrationDateTime">
                                <HeaderStyle HorizontalAlign="Center" Width="85px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="ServiceUnitName" UniqueName="ServiceUnitName" SortExpression="ServiceUnitName"
                                HeaderText="Service Unit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnRowSelected="RowSelected" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Width="3px" />
            </td>
            <td width="85%" valign="top">
                <table>
                    <tr>
                        <td style="width: 50%; vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationDateTime" runat="server" Text="Reg. Date / Disch. Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="txtRegistrationDateTime" runat="server" AutoPostBackControl="None"
                                                        Enabled="False">
                                                        <DateInput ID="DateInput3" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                                        </DateInput>
                                                        <TimeView ID="TimeView3" runat="server" TimeFormat="HH:mm">
                                                        </TimeView>
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                                <td style="width: 10px">to&nbsp;
                                                </td>
                                                <td>
                                                    <telerik:RadDateTimePicker ID="txtDischargeDateTimeInfo" runat="server" AutoPostBackControl="None"
                                                        Enabled="False">
                                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm" DateFormat="dd/MM/yyyy HH:mm">
                                                        </DateInput>
                                                        <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm">
                                                        </TimeView>
                                                    </telerik:RadDateTimePicker>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtGuarantorName" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                    <td width="20"></td>
                                    <td></td>
                                </tr>

                            </table>
                        </td>
                        <td style="width: 50%; vertical-align: top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="true" />
                                    </td>
                                    <td width="20"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRoomName" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td width="20"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBedID" runat="server" Text="Bed No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtBedID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td width="20"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblTestResult" runat="server" Text="Test Result"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="rblTestResult" runat="server" RepeatDirection="Horizontal" OnTextChanged="rblTestResult_OnTextChanged" AutoPostBack="True">
                                                        <asp:ListItem Selected="true">Laboratory</asp:ListItem>
                                                        <asp:ListItem>Radiology and Imaging</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td style="width: 70px"></td>
                                                <td>
                                                    <asp:ImageButton ID="btnResultLab" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                        CausesValidation="False" OnClientClick="openWinResultLab();return false;"
                                                        ToolTip="Laboratorium Result" />
                                                    <asp:ImageButton ID="btnResultRad" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                        CausesValidation="False" OnClientClick="openWinResultRad();return false;"
                                                        ToolTip="Radiologi and Imaging Result" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="grdDiagnose" runat="server" AutoGenerateColumns="False" GridLines="Both" ShowFooter="True">
                    <MasterTableView DataKeyNames="RegistrationNo, SequenceNo" ClientDataKeyNames="RegistrationNo, SequenceNo">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="DiagnoseID" HeaderText="Code"
                                UniqueName="DiagnoseID" SortExpression="DiagnoseID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="DiagnoseName" HeaderText="Diagnosis Name" UniqueName="DiagnoseName"
                                SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DiagnoseType" HeaderText="Diagnosis Type" UniqueName="DiagnoseType"
                                SortExpression="DiagnoseType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                AllowSorting="false" />
                            <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAcuteDisease" HeaderText="Acute"
                                UniqueName="IsAcuteDisease" SortExpression="IsAcuteDisease" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsChronicDisease"
                                HeaderText="Chronic" UniqueName="IsChronicDisease" SortExpression="IsChronicDisease"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsOldCase" HeaderText="Old Case"
                                UniqueName="IsOldCase" SortExpression="IsOldCase" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsConfirmed" HeaderText="Conf."
                                UniqueName="IsConfirmed" SortExpression="IsConfirmed" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
