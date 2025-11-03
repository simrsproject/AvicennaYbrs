<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="ResumeMedisInPatientEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ResumeMedisInPatientEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ControlPlanCtl.ascx" TagPrefix="uc1" TagName="ControlPlanCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EpisodeDiagnoseCtl.ascx" TagPrefix="uc1" TagName="EpisodeDiagnoseCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisProcedureCtl.ascx" TagPrefix="uc1" TagName="ResumeMedisProcedureCtl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdDiagnose">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDiagnose" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboProcedureID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtProcedureName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetHistoryOfPresentIllness">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtHistoryOfPresentIllness" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetFinalDiag">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="epDiagCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="epDiagCtl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="epDiagCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetResumeMedisProcedure">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="resumeMedisProcedureCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="resumeMedisProcedureCtl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="resumeMedisProcedureCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetPastMedicalHistory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPastMedicalHistory" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="lbtnResetPhysicalExamination">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPhysicalExamination" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="lbtnResetAncillaryExamination">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtAncillaryExamination" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetMedicalProcedures">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtMedicalProcedures" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="lbtnResetPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <table width="100%">
        <tr>
            <td style="width: 50%">
                <table style="width: 100%">
                    <tr>
                        <td class="label">Registration Date 
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationDate" runat="server" Width="100px" Enabled="False" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Date Plan
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtDischargeTime" runat="server" Width="93px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvDischargeDate" runat="server" ErrorMessage="Discharge Date required."
                                ValidationGroup="entry" ControlToValidate="txtDischargeDate" SetFocusOnError="True">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Present Status
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRDischargeCondition" runat="server" Width="304px" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRDischargeCondition" runat="server" ErrorMessage="Present Status required."
                                ValidationGroup="entry" ControlToValidate="cboSRDischargeCondition" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Discharge Reason
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="304px" />
                        </td>
                        <td style="width: 20px">
                            <asp:RequiredFieldValidator ID="rfvSRDischargeMethod" runat="server" ErrorMessage="Discharge Reason required."
                                ValidationGroup="entry" ControlToValidate="cboSRDischargeMethod" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td class="label">Treating Physician
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboTreatingPhysician" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">(or type here)
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTreatingPhysicianName" runat="server" Width="300px" MaxLength="250" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Unit Intended
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cboSRUnitIntended" runat="server" Width="304px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>

    <table width="100%">
        <tr>
            <td class="label">Indication for Admission<br />
                Indikasi dirawat
            </td>
            <td>
                <telerik:RadTextBox ID="txtTreatmentIndications" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Chief Complaint<br />
                Keluhan Utama
            </td>
            <td>
                <telerik:RadTextBox ID="txChiefComplaint" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label">History of Present Illness<br />
                Anamnesis Perjalanan Penyakit
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetHistoryOfPresentIllness" OnClick="lbtnResetHistoryOfPresentIllness_OnClick" OnClientClick="if (!confirm('Reset this History of Present Illness')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtHistoryOfPresentIllness" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Comurbidity<br />
                Riwayat Penyakit Dahulu
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetPastMedicalHistory" OnClick="lbtnResetPastMedicalHistory_OnClick" OnClientClick="if (!confirm('Reset this Comurbidity')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPastMedicalHistory" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Prognosis
            </td>
            <td>
                <telerik:RadTextBox ID="txtPrognosis" runat="server" TextMode="MultiLine" Width="100%" Height="80px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Physical Examination<br />
                Pemeriksaan Fisik
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetPhysicalExamination" OnClick="lbtnResetPhysicalExamination_OnClick" OnClientClick="if (!confirm('Reset this Physical Examination')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPhysicalExamination" runat="server" TextMode="MultiLine" Width="100%" Height="60px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Ancillary Examination(Lab, X-rays, etc)<br />
                Pemeriksaan Penunjang(Lab, Ro, dll)
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetAncillaryExamination" OnClick="lbtnResetAncillaryExamination_OnClick" OnClientClick="if (!confirm('Reset this Ancillary Examination')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtAncillaryExamination" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="vertical-align: top">Final Diagnosis
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetFinalDiag" OnClick="lbtnResetFinalDiag_OnClick" OnClientClick="if (!confirm('Delete Final Diagnose and import from Work Diagnose?')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton></td>
            <td>
                <uc1:EpisodeDiagnoseCtl runat="server" ID="finalDiagCtl" />
            </td>
        </tr>
        <tr>
            <td class="label">Medical Procedures<br />
                Tindakan selama di Rumah Sakit
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetMedicalProcedures" OnClick="lbtnResetMedicalProcedures_OnClick" OnClientClick="if (!confirm('Reset this Medical Procedures')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtMedicalProcedures" runat="server" TextMode="MultiLine" Width="100%" Height="60px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">ICD 9 CM
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetResumeMedisProcedure" OnClick="lbtnResetResumeMedisProcedure_OnClick" OnClientClick="if (!confirm('Reset ICD 9 CM?')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <uc1:ResumeMedisProcedureCtl runat="server" ID="resumeMedisProcedureCtl" />
            </td>
        </tr>
        <tr>
            <td class="label"></td>
            <td>
                <telerik:RadTextBox ID="txtProcedureName" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label">Medications<br />
                Obat yang diberikan
                
                <br />
                <asp:LinkButton runat="server" ID="lbtnResetPrescription" OnClick="lbtnResetPrescription_OnClick" OnClientClick="if (!confirm('Reset this Medications')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                </asp:LinkButton>
            </td>
            <td>
                <telerik:RadTextBox ID="txtPrescription" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>

        <tr>
            <td class="label">Instruction & Follow Up<br />
                Intruksi dan Tindak Lanjut
            </td>
            <td>
                <telerik:RadTextBox ID="txtSuggestionFollowUp" runat="server" TextMode="MultiLine" Width="100%" Height="60px" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Home Prescription<br />
                Terapi Pulang
            </td>
            <td>
                <telerik:RadGrid ID="grdPrescription" runat="server" OnNeedDataSource="grdPrescription_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None"
                    OnItemCommand="grdPrescription_ItemCommand" OnItemDataBound="grdPrescription_OnItemDataBound" AllowPaging="false">
                    <MasterTableView DataKeyNames="MedicationReceiveNo,IsBroughtHome">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lblBroughtAll" runat="server" CommandName="BroughtHomeAll" ToolTip="Set All as Home Prescription" OnClientClick="if (!confirm('Set All as Home Prescription')) return false;">
                                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16.png" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lblNotBroughtAll" runat="server" CommandName="NotBroughtHomeAll" ToolTip="Set All as not Home Prescription" OnClientClick="if (!confirm('Set All as not Home Prescription')) return false;">
                                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16_d.png" />
                                    </asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblBroughtHome" runat="server" CommandName="BroughtHome" ToolTip="Set as Home Prescription"
                                        Visible='<%#  false.Equals(DataBinder.Eval(Container.DataItem, "IsBroughtHome")) || DataBinder.Eval(Container.DataItem, "IsBroughtHome")==DBNull.Value %>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16_d.png" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lblNotBroughtHome" runat="server" CommandName="NotBroughtHome" ToolTip="Set as not Home Prescription"
                                        Visible='<%#  true.Equals(DataBinder.Eval(Container.DataItem, "IsBroughtHome")) %>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16.png" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridCheckBoxColumn DataField="IsDischargeAppropriate" UniqueName="IsDischargeAppropriate" HeaderText="Discharge Appropriate" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="70px">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </telerik:GridCheckBoxColumn>

                            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="350px" HeaderText="Item">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%><br />
                                    <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Qty" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="80px" />
                            <telerik:GridBoundColumn DataField="RefTransactionNo" UniqueName="RefTransactionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" />
                            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="False">
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td></td>
        </tr>
    </table>
    <br />
    <asp:Panel runat="server" ID="pnlRefer" Width="100%">
        <fieldset style="width: 98%">
            <legend>EXTERNAL PATIENT REFERRAL / RUJUKAN PASIEN EKSTERNAL</legend>
            <table width="100%">
                <tr>
                    <td class="label">Refer To<br />
                        Dirujuk Ke
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboReferralID" runat="server" Width="304px" EmptyMessage="Select a ICD 9"
                            EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                            <WebServiceSettings Method="ReferralTo" Path="~/WebService/ComboBoxDataService.asmx" />
                            <ClientItemTemplate>
                                <div>
                                    <ul class="details">
                                        <li class="bold"><span>#= Text # </span></li>
                                        <li class="small"><span>Phone: #= Attributes.PhoneNo # </span></li>
                                        <li class="small"><span>Address: #= Attributes.Address # </span></li>
                                    </ul>
                                </div>
                            </ClientItemTemplate>
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Reason for Referral<br />
                        Alasan Dirujuk
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cboSRReferReason" runat="server" Width="304px" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Remark & Other Information<br />
                        Keterangan Lain</td>
                    <td>
                        <telerik:RadTextBox ID="txtOtherInformation" runat="server" TextMode="MultiLine" Width="100%" Height="100px" Resize="Vertical" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Referral Agreed By<br />
                        Nama Penerima Persetujuan via Telp</td>
                    <td>
                        <telerik:RadTextBox ID="txtReferralAgreedBy" runat="server" Width="304px" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Time<br />
                        Jam
                    </td>
                    <td>
                        <telerik:RadDateTimePicker ID="txtReferralAgreedTime" runat="server" Width="170px" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
        <uc1:ControlPlanCtl runat="server" ID="controlPlanCtl" />
    </asp:Panel>
</asp:Content>
