<%@ Page Title="Emergency Information & Discharge Detail" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="DetailRegistrationAndDischargeDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Emergency.DetailRegistrationAndDischargeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cboSRReferralGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReferralID" />
                    <telerik:AjaxUpdatedControl ControlID="txtReferralName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboReferralID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboSRReferralGroup" />
                    <telerik:AjaxUpdatedControl ControlID="txtReferralName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRVisitReason">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReasonForTreatmentID" />
                    <telerik:AjaxUpdatedControl ControlID="cboReasonsForTreatmentDescID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboReasonForTreatmentID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboReasonsForTreatmentDescID" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRPatientInType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rfvSRPatientInCondition" />
                    <telerik:AjaxUpdatedControl ControlID="rfvSRERCaseType" />
                    <telerik:AjaxUpdatedControl ControlID="rfvSRTriage" />
                    <telerik:AjaxUpdatedControl ControlID="rfvcSRVisitReason" />
                    <telerik:AjaxUpdatedControl ControlID="rfvReasonForTreatmentID" />
                    <telerik:AjaxUpdatedControl ControlID="rfvFirstResponDate" />
                    <telerik:AjaxUpdatedControl ControlID="rfvPhysicianResponDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadAjaxPanel ID="radAjaxPanel" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function gotoHealthRecordUrl(md, id, fid) {
                var url = '';
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                if (fid == 'SUMMARY')
                    url = '../../../Charges/ServiceUnit/ServiceUnitTransaction/PatientMedicalSummaryDetail.aspx?md=' + md + '&regno=' + regNo.get_value() + '&fid=' + fid;
                else
                    url = '../../../Charges/ServiceUnit/ServiceUnitTransaction/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regNo.get_value() + '&fid=' + fid + '&menu=su';
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationTime" runat="server" Width="50px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="true" />
                            <telerik:RadTextBox ID="txtPatientID" runat="server" Visible="False" />
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Visible="False" />
                        </td>
                        <td style="width: 20px"></td>
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
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpagDetail">
        <Tabs>
            <telerik:RadTab runat="server" Text="Emergency Information Detail" Selected="True"
                PageViewID="pgEmrInfo">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Registration & Discharge Detail" PageViewID="pgDischargeInfo">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Patient Health Record & Document" PageViewID="pgPatHealthRecord" Visible="false">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpagDetail" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgEmrInfo" runat="server">
            <table width="100%">
                <tr>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPatientInType" runat="server" Text="Patient In Type"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRPatientInType" runat="server" Width="300px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSRPatientInType_SelectedIndexChanged" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRPatientInType" runat="server" ErrorMessage="Patient In Type required."
                                        ValidationGroup="entry" ControlToValidate="cboSRPatientInType" SetFocusOnError="True">
                                        <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRPatientInCondition" runat="server" Text="Patient Initial Condition"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRPatientInCondition" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRPatientInCondition" runat="server" ErrorMessage="Patient Initial Condition required."
                                        ValidationGroup="entry" ControlToValidate="cboSRPatientInCondition" SetFocusOnError="True">
                                        <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRERCaseType" runat="server" Text="Case Type"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRERCaseType" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRERCaseType" runat="server" ErrorMessage="Case Type required."
                                        ValidationGroup="entry" ControlToValidate="cboSRERCaseType" SetFocusOnError="True">
                                        <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRTriage" runat="server" Text="Triage"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRTriage" runat="server" Width="300px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRTriage" runat="server" ErrorMessage="Triage required."
                                        ValidationGroup="entry" ControlToValidate="cboSRTriage" SetFocusOnError="True">
                                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRVisitReason" runat="server" Text="Visit Reason"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRVisitReason" runat="server" Width="300px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSRVisitReason_SelectedIndexChanged" AllowCustomText="true"
                                        Filter="Contains" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvcSRVisitReason" runat="server" ErrorMessage="Visit Reason required."
                                        ValidationGroup="entry" ControlToValidate="cboSRVisitReason" SetFocusOnError="True">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReasonForTreatment" runat="server" Text="Reason For Treatment"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboReasonForTreatmentID" runat="server" Width="300px" OnSelectedIndexChanged="cboReasonForTreatmentID_SelectedIndexChanged"
                                        AutoPostBack="True" AllowCustomText="true" Filter="Contains" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvReasonForTreatmentID" runat="server" ErrorMessage="Reason For Treatment required."
                                        ValidationGroup="entry" ControlToValidate="cboReasonForTreatmentID" SetFocusOnError="True">
                                        <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReasonsForTreatmentDescID" runat="server" Text="Reason For Treatment Description"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboReasonsForTreatmentDescID" runat="server" Width="300px"
                                        AllowCustomText="true" Filter="Contains" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCauseOfAccident" runat="server" Text="Cause Of Accident / Type Or Location Of The Bite"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtCauseOfAccident" runat="server" Width="300px" MaxLength="250"
                                        TextMode="MultiLine" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRCrashSite" runat="server" Text="Crash Site"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRCrashSite" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCrashSiteDetail" runat="server" Text="Crash Site Detail"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtCrashSiteDetail" runat="server" Width="300px" MaxLength="200"
                                        TextMode="MultiLine" />
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
                                    <asp:Label ID="lblFirstResponDateTime" runat="server" Text="First Respon Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtFirstResponDate" runat="server" Width="110px" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtFirstResponTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px">
                                    <asp:RequiredFieldValidator ID="rfvFirstResponDate" runat="server" ErrorMessage="First Respon Date required."
                                        ValidationGroup="entry" ControlToValidate="txtFirstResponDate" SetFocusOnError="True">
                                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPhysicianResponDateTime" runat="server" Text="Physician Respon Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtPhysicianResponDate" runat="server" Width="110px" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtPhysicianResponTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px">
                                    <asp:RequiredFieldValidator ID="rfvPhysicianResponDate" runat="server" ErrorMessage="Physician Respon Date required."
                                        ValidationGroup="entry" ControlToValidate="txtPhysicianResponDate" SetFocusOnError="True">
                                        <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblEmrDiagnoseID" runat="server" Text="Diagnose"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtEmrDiagnoseID" runat="server" Width="300px" MaxLength="200"
                                        TextMode="MultiLine" />
                                    <telerik:RadComboBox ID="cboEmrDiagnoseID" runat="server" Width="244px" HighlightTemplatedItems="True"
                                        MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboEmrDiagnoseID_ItemDataBound"
                                        OnItemsRequested="cboEmrDiagnoseID_ItemsRequested">
                                        <FooterTemplate>
                                            Note : Show max 20 result
                                        </FooterTemplate>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry2Column">
                                    <asp:CheckBox ID="chkIsOldCase" runat="server" Text="Old Case" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry2Column">
                                    <asp:CheckBox ID="chkIsDHF" runat="server" Text="DHF" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry2Column">
                                    <asp:CheckBox ID="chkIsEKG" runat="server" Text="EKG" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label"></td>
                                <td class="entry2Column">
                                    <asp:CheckBox ID="chkIsObservation" runat="server" Text="Observation" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">COVID-19 Status
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboCovidStatus" runat="server" Width="304px">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 60px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdTreatmentForAnimalBites" runat="server" OnNeedDataSource="grdTreatmentForAnimalBites_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" AllowPaging="true" PageSize="10"
                OnDeleteCommand="grdTreatmentForAnimalBites_DeleteCommand" OnInsertCommand="grdTreatmentForAnimalBites_InsertCommand"
                AllowSorting="False">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SRTreatmentForAnimalBites"
                    GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn DataField="SRTreatmentForAnimalBites" HeaderText="ID" UniqueName="SRTreatmentForAnimalBites"
                            SortExpression="SRTreatmentForAnimalBites">
                            <HeaderStyle HorizontalAlign="Left" Width="130px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TreatmentForAnimalBitesName" HeaderText="Therapy"
                            UniqueName="TreatmentForAnimalBitesName" SortExpression="TreatmentForAnimalBitesName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Void" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Void this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="TreatmentForAnimalBitesItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="TreatmentForAnimalBitesItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgDischargeInfo" runat="server">
            <table width="100%">
                <tr>
                    <td width="50%" style="vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRReferralGroup" runat="server" Text="Referral Group"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboSRReferralGroup" runat="server" Width="300px" AutoPostBack="True"
                                        OnSelectedIndexChanged="cboSRReferralGroup_SelectedIndexChanged" />
                                </td>
                                <td style="width: 60px">
                                    <asp:RequiredFieldValidator ID="rfvSRReferralGroup" runat="server" ErrorMessage="Referral Group required."
                                        ValidationGroup="entry" ControlToValidate="cboSRReferralGroup" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReferralID" runat="server" Text="Refer From"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadComboBox ID="cboReferralID" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="cboReferralID_SelectedIndexChanged" HighlightTemplatedItems="True"
                                        MarkFirstMatch="false" EnableLoadOnDemand="true" NoWrap="True" OnItemDataBound="cboReferralID_ItemDataBound"
                                        OnItemsRequested="cboReferralID_ItemsRequested">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 60px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReferralName" runat="server" Text="Description"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtReferralName" runat="server" Width="300px" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width: 60px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDischargeDate" runat="server" Text="Discharge Date / Time"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellspacing="=0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtDischargeTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 20px">
                                    <asp:RequiredFieldValidator ID="rfvDischargeDate" runat="server" ErrorMessage="Discharge Date required."
                                        ValidationGroup="entry" ControlToValidate="txtDischargeDate" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRDischargeMethod" runat="server" Text="Discharge Method"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDischargeMethod" runat="server" Width="300px" />
                                </td>
                                <td style="width: 20px">
                                    <asp:RequiredFieldValidator ID="rfvSRDischargeMethod" runat="server" ErrorMessage="Discharge Method required."
                                        ValidationGroup="entry" ControlToValidate="cboSRDischargeMethod" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblReferTo" runat="server" Text="Refer To"></asp:Label>
                                </td>
                                <td class="entry2Column">
                                    <telerik:RadTextBox ID="txtReferTo" runat="server" Width="300px" MaxLength="250" />
                                </td>
                                <td width="20"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRDischargeCondition" runat="server" Text="Discharge Condition"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRDischargeCondition" runat="server" Width="304" />
                                </td>
                                <td style="width: 20px">
                                    <asp:RequiredFieldValidator ID="rfvSRDischargeCondition" runat="server" ErrorMessage="Discharge Condition required."
                                        ValidationGroup="entry" ControlToValidate="cboSRDischargeCondition" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDeathCertificateNo" runat="server" Text="Death Certificate No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDeathCertificateNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDischargeMedicalNotes" runat="server" Text="Discharge Medical Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDischargeMedicalNotes" runat="server" Width="300px" MaxLength="4000"
                                        TextMode="MultiLine" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDischargeNotes" runat="server" Text="Discharge Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDischargeNotes" runat="server" Width="300px" MaxLength="4000"
                                        TextMode="MultiLine" />
                                </td>
                                <td style="width: 20px"></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgPatHealthRecord" runat="server">
            <telerik:RadTabStrip runat="server" ID="tabDetail2" MultiPageID="mpagDetail2">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Patient Health Record" PageViewID="pvPHR" Selected="True" />
                    <telerik:RadTab runat="server" Text="Patient Health Record History" PageViewID="pvPHR2" />
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="mpagDetail2" BorderStyle="Solid" SelectedIndex="0"
                BorderColor="Gray">
                <telerik:RadPageView runat="server" ID="pvPHR">
                    <telerik:RadGrid ID="grdPHR" runat="server" OnNeedDataSource="grdPHR_NeedDataSource"
                        AllowSorting="true" EnableLinqExpressions="false">
                        <MasterTableView DataKeyNames="QuestionFormID" ClientDataKeyNames="QuestionFormID"
                            AllowPaging="True" AutoGenerateColumns="False" GroupLoadMode="Client" HierarchyLoadMode="ServerOnDemand">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="Edit">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"gotoHealthRecordUrl('new', '', '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" /></a>", Eval("QuestionFormID"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form"
                                    UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="RecordDate" HeaderText="Record Date" UniqueName="RecordDate"
                                    SortExpression="RecordDate">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Examiner" UniqueName="EmployeeName"
                                    SortExpression="EmployeeName">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsMCUForm" HeaderText="MCU Form" UniqueName="IsMCUForm">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsComplete" HeaderText="Complete"
                                    UniqueName="IsComplete" SortExpression="IsComplete" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="pvPHR2">
                    <telerik:RadGrid ID="grdPHR2" runat="server" OnNeedDataSource="grdPHR2_NeedDataSource"
                        AllowSorting="true" EnableLinqExpressions="false" OnItemCommand="grdPHR_ItemCommand">
                        <MasterTableView DataKeyNames="QuestionFormID" ClientDataKeyNames="QuestionFormID"
                            AllowPaging="True" AutoGenerateColumns="False" GroupLoadMode="Client" HierarchyLoadMode="ServerOnDemand">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="Edit">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"gotoHealthRecordUrl('edit', '{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("QuestionFormID"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="PrintPHR" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnPrintPHR" runat="server" CommandName='<%# DataBinder.Eval(Container.DataItem, "formID") %>'
                                            ToolTip='Print Form' CommandArgument='<%#string.Format("print|{0}_{1}_{2}", DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "QuestionFormID"),DataBinder.Eval(Container.DataItem, "TransactionNo")) %>'>
                                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn DataField="TransactionNo" HeaderText="Document No" UniqueName="TransactionNo"
                                    SortExpression="TransactionNo">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician Name" UniqueName="ParamedicName"
                                    SortExpression="ParamedicName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Name"
                                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form"
                                    UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="RecordDate" HeaderText="Record Date" UniqueName="RecordDate"
                                    SortExpression="RecordDate">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="ReferenceNo" HeaderText="Reference No" UniqueName="ReferenceNo"
                                    SortExpression="ReferenceNo">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Examiner" UniqueName="EmployeeName"
                                    SortExpression="EmployeeName" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="IsMCUForm" HeaderText="MCU Form" UniqueName="IsMCUForm">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsComplete" HeaderText="Complete"
                                    UniqueName="IsComplete" SortExpression="IsComplete" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="center"
                                    ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="linkDelete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure?'))return false;"
                                            CommandArgument='<%#string.Format("delete|{0}_{1}_{2}", DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "QuestionFormID"),DataBinder.Eval(Container.DataItem, "TransactionNo")) %>'>
                                                    <img src="../../../../Images/Toolbar/row_delete16.png" border="0" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
