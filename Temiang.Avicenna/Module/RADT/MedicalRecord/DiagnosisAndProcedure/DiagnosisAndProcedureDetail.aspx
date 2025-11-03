<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="DiagnosisAndProcedureDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DiagnosisAndProcedureDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function openWinCaptureImage() {
                var oWnd;
                oWnd = radopen("<%=Helper.UrlRoot()%>/Module/RADT/Registration/PatientPhoto/CaptureImageForm.aspx",
                    "winCaptureImage");
                oWnd.setSize(740, 340);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }

        </script>
    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top;">
                <fieldset id="FieldSet1" style="min-height: 150px;">
                    <legend>Photo</legend>
                    <div>
                        <asp:Image runat="server" ID="imgPatientPhoto" Width="120px" Height="120px" />

                        <div style='float: right; padding: 4px'>
                            <a href='#' onclick='javascript:openWinCaptureImage(); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();'></a>
                        </div>

                        <div style="display: none;">
                            <asp:Button runat="server" Text=".." ID="btnCaptureImage" Width="10px" />
                        </div>
                    </div>
                </fieldset>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / MRN"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="193px" ReadOnly="true" />
                                    </td>
                                    <td>&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="242px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="24px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtAgeYear" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeMonth" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeDay" runat="server" ReadOnly="true" Width="40px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" TextMode="MultiLine" Height="45px"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; width: 50%">
                <table width="100%">
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
                            <telerik:RadTextBox ID="txtParamedicID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblParamedicName" runat="server"></asp:Label>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblRoomName" runat="server"></asp:Label>
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
                            <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtGuarantorID" runat="server" Width="100px" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblGuarantorName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" ShowBaseLine="true">
        <Tabs>
            <telerik:RadTab runat="server" Text="SOAP" PageViewID="pgSOAPE" Selected="True" />
            <telerik:RadTab runat="server" Text="Episode Diagnosis" PageViewID="pgDiagnose" />
            <telerik:RadTab runat="server" Text="Episode Procedure" PageViewID="pgProcedure" />
            <telerik:RadTab runat="server" Text="Episode Procedure (Diagnostic)" PageViewID="pgProcedureDiagnostic" />
            <telerik:RadTab runat="server" Text="Discharge" PageViewID="pgDischarge" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray"
        SelectedIndex="0">
        <telerik:RadPageView ID="pgSOAPE" runat="server">
            <telerik:RadGrid ID="grdEpisodeSOAPE" runat="server" OnNeedDataSource="grdEpisodeSOAPE_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowSorting="true">
                <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationInfoMedicID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn DataField="DateTimeInfo" HeaderText="Date / Time" UniqueName="DateTimeInfo"
                            SortExpression="DateTimeInfo" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="130px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Info1" HeaderText="Subjective" UniqueName="Info1"
                            SortExpression="Info1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Info2" HeaderText="Objective" UniqueName="Info2"
                            SortExpression="Info2" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Info3" HeaderText="Assessment / Diagnosis"
                            UniqueName="Info3" SortExpression="Info3" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Info4" HeaderText="Planning" UniqueName="Info4"
                            SortExpression="Info4" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDiagnose" runat="server">
            <telerik:RadGrid ID="grdEpisodeDiagnose" runat="server" OnNeedDataSource="grdEpisodeDiagnose_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEpisodeDiagnose_UpdateCommand"
                OnDeleteCommand="grdEpisodeDiagnose_DeleteCommand" OnInsertCommand="grdEpisodeDiagnose_InsertCommand"
                OnItemCreated="grdEpisodeDiagnose_ItemCreated" AllowSorting="true">
                <MasterTableView DataKeyNames="RegistrationNo, SequenceNo" CommandItemDisplay="Top">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="DiagnoseID" HeaderText="Code"
                            UniqueName="DiagnoseID" SortExpression="DiagnoseID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="DiagnoseName" HeaderText="Diagnosis Name" UniqueName="DiagnoseName"
                            SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DiagnoseType" HeaderText="Diagnosis Type" UniqueName="DiagnoseType"
                            SortExpression="DiagnoseType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
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
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="CreateDateTime" HeaderText="Create Date/Time"
                            UniqueName="CreateDateTime" SortExpression="CreateDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="CreateByUserName" HeaderText="Create By"
                            UniqueName="CreateByUserName" SortExpression="CreateByUserName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update Date/Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserName"
                            HeaderText="Last Update By" UniqueName="LastUpdateByUserName" SortExpression="LastUpdateByUserName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EpisodeDiagDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EpisodeDiagnoseEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgProcedure" runat="server">
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2" ShowBaseLine="true">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Procedure" PageViewID="pgProc" Selected="True" />
                    <telerik:RadTab runat="server" Text="Surgical History" PageViewID="pgHist" />
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage2" runat="server" BorderStyle="Solid" BorderColor="Gray"
                SelectedIndex="0">
                <telerik:RadPageView ID="pgProc" runat="server">
                    <telerik:RadGrid ID="grdEpisodeProcedure" runat="server" OnNeedDataSource="grdEpisodeProcedure_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEpisodeProcedure_UpdateCommand"
                        OnDeleteCommand="grdEpisodeProcedure_DeleteCommand" OnInsertCommand="grdEpisodeProcedure_InsertCommand"
                        AllowSorting="true" OnItemCreated="grdEpisodeProcedure_ItemCreated">
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo, SequenceNo">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ProcedureID" HeaderText="Code"
                                    UniqueName="ProcedureID" SortExpression="ProcedureID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                                    SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    AllowSorting="false" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcedureDate" HeaderText="Date"
                                    UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcedureTime" HeaderText="Time"
                                    UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    AllowSorting="false" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="CreateDateTime" HeaderText="Create Date/Time"
                                    UniqueName="CreateDateTime" SortExpression="CreateDateTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CreateByUserName" HeaderText="Create By"
                                    UniqueName="CreateByUserName" SortExpression="CreateByUserID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update Date/Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserName"
                                    HeaderText="Last Update By" UniqueName="LastUpdateByUserName" SortExpression="LastUpdateByUserName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="EpisodeProcDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EpisodeProcedureEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true" />
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgHist" runat="server">
                    <telerik:RadGrid ID="grdBookingNotes" runat="server" OnNeedDataSource="grdBookingNotes_NeedDataSource" Height="560px"
                        AutoGenerateColumns="False" GridLines="None" OnItemCommand="grdBookingNotes_OnItemCommand"
                        OnItemDataBound="grdBookingNotes_OnItemDataBound" AllowSorting="true">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="BookingNo" ShowHeader="True" HierarchyDefaultExpanded="true">
                            <NestedViewTemplate>
                                <div style="padding-left: 20px; width: 98%">
                                    <telerik:RadTabStrip ID="tabsEpisodeProcedure" runat="server" MultiPageID="mpEpisodeProcedure" ShowBaseLine="true"
                                        Align="Left" PerTabScrolling="True" Width="100%"
                                        SelectedIndex="0">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="Procedure & Operating Notes" PageViewID="pgOpNotes"
                                                Selected="True" />
                                            <telerik:RadTab runat="server" Text="Anesthetist Notes" PageViewID="pgAnesNotes" />
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="mpEpisodeProcedure" runat="server" SelectedIndex="0" ScrollBars="Auto" Width="100%"
                                        CssClass="multiPage">
                                        <telerik:RadPageView ID="pgOpNotes" runat="server">
                                            <telerik:RadGrid ID="grdEpisodeProcedureDetail" runat="server"
                                                AutoGenerateColumns="False" GridLines="None">
                                                <MasterTableView DataKeyNames="BookingNo, SequenceNo" ShowHeader="True">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            AllowSorting="false" />
                                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Regio" HeaderText="Regio" UniqueName="Regio"
                                                            SortExpression="Regio" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            AllowSorting="false" />
                                                        <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                                                            SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            AllowSorting="false" HeaderStyle-Width="250px" />
                                                        <telerik:GridBoundColumn DataField="OperatingNotes" HeaderText="Operating Notes" UniqueName="OperatingNotes"
                                                            SortExpression="OperatingNotes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                            AllowSorting="false" />
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="False">
                                                    <Selecting AllowRowSelect="False" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="pgAnesNotes" runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%; vertical-align: top">
                                                        <fieldset style="width: 95%">
                                                            <legend>
                                                                <asp:Label runat="server" ID="lblParamedicIDAnestesi" Text='<%#Eval("ParamedicAnestesiName")%>' /></legend>
                                                            <telerik:RadTextBox ID="txtAnestesyNotes" runat="server" Text='<%#Eval("AnestesyNotes")%>' Width="100%" Height="315px"
                                                                TextMode="MultiLine" ReadOnly="true" />
                                                        </fieldset>
                                                    </td>
                                                    <td style="width: 50%; vertical-align: top">
                                                        <telerik:RadGrid ID="grdEpisodeProcedureDetailAns" runat="server"
                                                            AutoGenerateColumns="False" GridLines="None">
                                                            <MasterTableView DataKeyNames="RegistrationNo, SequenceNo" ShowHeader="True">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ProcedureID" HeaderText="Code" UniqueName="ProcedureID"
                                                                        SortExpression="ProcedureID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        AllowSorting="false" />
                                                                    <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                                                                        SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        AllowSorting="false" />
                                                                </Columns>
                                                            </MasterTableView>
                                                            <ClientSettings EnableRowHoverStyle="False">
                                                                <Selecting AllowRowSelect="False" />
                                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </div>
                            </NestedViewTemplate>
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="160px" DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                                    SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    AllowSorting="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BookingNo" HeaderText="Booking No" UniqueName="BookingNo"
                                    SortExpression="BookingNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    AllowSorting="false" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcedureDate" HeaderText="Date"
                                    UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcedureTime" HeaderText="Time"
                                    UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="PostDiagnosis" HeaderText="Diagnosis" UniqueName="PostDiagnosis"
                                    SortExpression="PostDiagnosis" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    AllowSorting="false" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    AllowSorting="false" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="False">
                            <Selecting AllowRowSelect="False" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgProcedureDiagnostic" runat="server">
            <telerik:RadGrid ID="grdEpisodeProcedureDiagnostic" runat="server" OnNeedDataSource="grdEpisodeProcedureDiagnostic_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEpisodeProcedureDiagnostic_UpdateCommand"
                OnDeleteCommand="grdEpisodeProcedureDiagnostic_DeleteCommand" OnInsertCommand="grdEpisodeProcedureDiagnostic_InsertCommand"
                AllowSorting="true" OnItemCreated="grdEpisodeProcedureDiagnostic_ItemCreated">
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo, SequenceNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="ProcedureID" HeaderText="Code"
                            UniqueName="ProcedureID" SortExpression="ProcedureID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                            SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcedureDate" HeaderText="Date"
                            UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcedureTime" HeaderText="Time"
                            UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            AllowSorting="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="CreateDateTime" HeaderText="Create Date/Time"
                            UniqueName="CreateDateTime" SortExpression="CreateDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CreateByUserName" HeaderText="Create By"
                            UniqueName="CreateByUserName" SortExpression="CreateByUserID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update Date/Time" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserName"
                            HeaderText="Last Update By" UniqueName="LastUpdateByUserName" SortExpression="LastUpdateByUserName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EpisodeProcDiagnosticDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EpisodeProcedureDiagnosticEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" />
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDischarge" runat="server">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label" style="font-style: italic">Discharge Date / Time
                                </td>
                                <td>
                                    <telerik:RadDateTimePicker ID="txtDischargeDateTime" runat="server" AutoPostBackControl="None" Enabled="False">
                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd-MMM-yyyy HH:mm" DateFormat="dd-MMM-yyyy HH:mm">
                                        </DateInput>
                                        <TimeView ID="TimeView2" runat="server" TimeFormat="HH:mm">
                                        </TimeView>
                                    </telerik:RadDateTimePicker>
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" style="font-style: italic">Discharge Method
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="300px" Enabled="False" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" style="font-style: italic">Discharge Condition
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSRDischargeCondition" runat="server" Width="300px" Enabled="False" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" style="font-style: italic">Discharge Medical Notes
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtDischargeMedicalNotes" runat="server" Width="300px" TextMode="MultiLine"
                                        ReadOnly="True" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label" style="font-style: italic">Discharge Notes
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtDischargeNotes" runat="server" Width="300px" TextMode="MultiLine"
                                        ReadOnly="True" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
