<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PpiProcedureSurveillanceDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PpiProcedureSurveillanceDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;<b>I. Medical Record</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBookingNo" runat="server" Text="Registration No / Booking No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="147px" ReadOnly="True" />
                                        <telerik:RadTextBox ID="txtBookingNo" runat="server" Width="147px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRealizationDateTimeFrom" runat="server" Text="Date & Time Started"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="100px">
                                                    <telerik:RadDatePicker ID="txtRealizationDateFrom" runat="server" Width="100px" Enabled="False" />
                                                </td>
                                                <td width="50px">
                                                    <telerik:RadTimePicker ID="txtRealizationTimeFrom" runat="server" TimeView-Interval="00:30"
                                                        TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                        TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Enabled="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRealizationDateTimeTo" runat="server" Text="Date & Time Finished"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="100px">
                                                    <telerik:RadDatePicker ID="txtRealizationDateTo" runat="server" Width="100px" Enabled="False" />
                                                </td>
                                                <td width="50px">
                                                    <telerik:RadTimePicker ID="txtRealizationTimeTo" runat="server" TimeView-Interval="00:30"
                                                        TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                                        TimeView-Columns="4" TimeView-StartTime="00:00" TimeView-EndTime="23:59" Enabled="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSRPatientInType" runat="server" Text="Patient In Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSRPatientInType" runat="server" Width="304px" Enabled="False" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblMedicalNo" runat="server" Text="Medical Record No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblInitialDiagnose" runat="server" Text="Initial Diagnose"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtInitialDiagnose" runat="server" Width="300px" ReadOnly="True"
                                            TextMode="MultiLine" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblFinalDiagnose" runat="server" Text="Final Diagnose"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtFinalDiagnose" runat="server" Width="300px" ReadOnly="True"
                                            TextMode="MultiLine" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;<b>II. Patient Demographic</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px" Enabled="False" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAge" runat="server" Text="Age / Sex"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtAge" runat="server" Width="100px" ReadOnly="True" />
                                        <telerik:RadTextBox ID="txtSex" runat="server" Width="50px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px" ReadOnly="True"
                                            TextMode="MultiLine" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="300px" ReadOnly="True" />
                                    </td>
                                    <td width="20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="III. Patient Risk Factors" PageViewID="pgvPatientRiskFactors"
                Selected="true" />
            <telerik:RadTab runat="server" Text="IV. The Surgery Room Attendant" PageViewID="pgvSurgeryAttendent" />
            <telerik:RadTab runat="server" Text="V. Risk Category" PageViewID="pgvRiksCategory" />
            <telerik:RadTab runat="server" Text="VI. Use Of Antibiotics" PageViewID="pgvUseOfAntibiotics" />
            <telerik:RadTab runat="server" Text="VII. Ancillary Services" PageViewID="pgvAncillaryServices" />
            <telerik:RadTab runat="server" Text="VIII. Cultures" PageViewID="pgvCultures" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvPatientRiskFactors" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPatientRiskFactors" runat="server" Text="Patient Risk Factors"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsRiskFactorAge" runat="server" Text="Age" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsRiskFactorNutrient" runat="server" Text="Nutrient" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsRiskFactorObesity" runat="server" Text="Obesity" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblComorbidity" runat="server" Text="Comorbidity"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsDiabetes" runat="server" Text="Diabetes" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsHypertension" runat="server" Text="Hypertension" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsHiv" runat="server" Text="HIV" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsHbv" runat="server" Text="HBV" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsHcv" runat="server" Text="HCV" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvSurgeryAttendent" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSurgeon1" runat="server" Text="Surgeon #1"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtSurgeon1" runat="server" Width="300px" ReadOnly="True" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSurgeon2" runat="server" Text="Surgeon #2"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtSurgeon2" runat="server" Width="300px" ReadOnly="True" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAssistant1" runat="server" Text="Assistant #1"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAssistant1" runat="server" Width="300px" ReadOnly="True" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAssistant2" runat="server" Text="Assistant #2"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtAssistant2" runat="server" Width="300px" ReadOnly="True" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRProcedureClassification" runat="server" Text="Procedure Classification"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRProcedureClassification" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRTypesOfSurgery" runat="server" Text="Types Of Surgery"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRTypesOfSurgery" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRiksCategory" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRRiskCategory" runat="server" Text="Risk Category"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRRiskCategory" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRWoundClassification" runat="server" Text="Wound Classification"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRWoundClassification" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRAsaScore" runat="server" Text="Asa Score"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRAsaScore" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRTTime" runat="server" Text="T. Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRTTime" runat="server" Width="304px" />
                                </td>
                                <td width="20">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvUseOfAntibiotics" runat="server">
            <telerik:RadGrid ID="grdUseOfAntibiotict" runat="server" OnNeedDataSource="grdUseOfAntibiotict_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdUseOfAntibiotict_UpdateCommand"
                OnDeleteCommand="grdUseOfAntibiotict_DeleteCommand" OnInsertCommand="grdUseOfAntibiotict_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="ItemID, StartDate">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="ItemName" UniqueName="ItemName"
                            SortExpression="ItemName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PpiProcedureSurveillanceUseOfAntibiotictDetail.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdUseOfAntibiotictEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAncillaryServices" runat="server">
            <telerik:RadTabStrip ID="tabStrip1" runat="server" MultiPageID="multiPage1">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Radiology" PageViewID="pgvRad" Selected="true" />
                    <telerik:RadTab runat="server" Text="Laboratory" PageViewID="pgvLab" />
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="multiPage1" runat="server" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvRad" runat="server" Selected="True">
                    <telerik:RadGrid ID="grdRadTestResult" runat="server" OnNeedDataSource="grdRadTestResult_NeedDataSource"
                        AutoGenerateColumns="False" >
                        <MasterTableView DataKeyNames="TransactionNo, SequenceNo">
                            <Columns>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TransactionNo" HeaderText="Transaction No"
                                    UniqueName="TransactionNo" />
                                <telerik:GridNumericColumn HeaderStyle-Width="50px" DataField="SequenceNo" HeaderText="Seq #"
                                    UniqueName="SequenceNo" />
                                <telerik:GridNumericColumn HeaderStyle-Width="250px" DataField="ItemName" HeaderText="Item"
                                    UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParamedicName" HeaderText="Physician"
                                    UniqueName="ParamedicName" SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TestResultDateTime"
                                    HeaderText="Result Date" UniqueName="TestResultDateTime" SortExpression="TestResultDateTime"
                                    DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="TestResult" HeaderText="Test Result"
                                    UniqueName="TestResult" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvLab" runat="server">
                    <telerik:RadGrid ID="grdLabTestResult" runat="server" OnNeedDataSource="grdLabTestResult_NeedDataSource"
                        AutoGenerateColumns="False" OnDetailTableDataBind="grdLabTestResult_DetailTableDataBind">
                        <MasterTableView DataKeyNames="TransactionNo">
                            <Columns>
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate"
                                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TransactionNo" HeaderText="Transaction No"
                                    UniqueName="TransactionNo" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                                    SortExpression="Notes" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsValidated" HeaderText="Validated"
                                    UniqueName="Validated" SortExpression="IsValidated" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="ValidatedByUserID" HeaderText="Validated By"
                                    UniqueName="ValidatedByUserID" SortExpression="ValidatedByUserID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="ValidatedDateTime"
                                    HeaderText="Validated Date" UniqueName="ValidatedDateTime" SortExpression="ValidatedDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <DetailTables>
                                <telerik:GridTableView Name="grdLabTestResultDetail" DataKeyNames="TransactionNo, SequenceNo, ItemID"
                                    AutoGenerateColumns="false">
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldName="ItemGroupName" HeaderText="Group Name "></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="ItemGroupName" SortOrder="Ascending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                                            Visible="false" />
                                        <telerik:GridBoundColumn DataField="ItemID" UniqueName="ItemID" SortExpression="ItemID"
                                            Visible="false" />
                                        <telerik:GridBoundColumn DataField="TestName" UniqueName="TestName" SortExpression="TestName" HeaderText="Item Name" />
                                        <telerik:GridBoundColumn DataField="NormalValueMin" UniqueName="NormalValueMin" SortExpression="NormalValueMin"
                                            HeaderText="Normal Value Min" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn DataField="NormalValueMax" UniqueName="NormalValueMax" SortExpression="NormalValueMax"
                                            HeaderText="Normal Value Max" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn DataField="ResultValue" UniqueName="ResultValue" SortExpression="ResultValue"
                                            HeaderText="Result Value" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="ItemUnit" UniqueName="ItemUnit" SortExpression="ItemUnit"
                                            HeaderText="Unit" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" SortExpression="Notes"
                                            HeaderText="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Cito" UniqueName="IsCito"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsCito" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsCito") %>'
                                                    Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Duplo" UniqueName="IsDuplo" HeaderStyle-Width="60px"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsDuplo" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsDuplo") %>'
                                                    Enabled="False" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Description" UniqueName="IsDuplo" HeaderStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsDescription" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsDescriptionResult") %>'
                                                    Enabled="False" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                        </MasterTableView>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvCultures" runat="server">
            <table width="100%">
                <tr>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtCultures" runat="server" Width="95%" MaxLength="500" TextMode="MultiLine"
                            Height="100px" />
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
