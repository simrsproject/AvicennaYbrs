<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="CredentialingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.CredentialingDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/PHR/PhrCtl.ascx" TagPrefix="uc1" TagName="PhrCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            function openReportsDocument(note) {
                var pid = $find("<%= txtTransactionNo.ClientID %>");
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Document/DocumentHist.aspx?pid=' + pid.get_value() + "&note=" + note + "&type=<%= Request.QueryString["type"] %>&role=<%= Request.QueryString["role"] %>&pg=";
                openWinMaxWindow(url);
            }
            function openWinMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                openWindow(url, width - 40, height - 40);
            }
            function openWindow(url, width, height) {
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
                oWnd.show();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactioNo" runat="server" Text="Transaction No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="160px" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionDate" runat="server" Text="Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTransactionDate" runat="server" ErrorMessage="Date required."
                                ValidationGroup="entry" ControlToValidate="txtTransactionDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblQuestionForm" runat="server" Text="Form Name" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtFormName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20"></td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr runat="server" id="trDocumentUpload">
                        <td colspan="4">
                            <a href="#" onclick="javascript:openReportsDocument(''); return false;">
                                <img src="../../../../Images/doc_upload48.png" border="0" alt="New" /><br /></a>
                            <asp:Label runat="server" ID="lblDocumentUpload" Text="Document Upload" ForeColor="Blue" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                            <asp:CheckBox ID="chkIsVoid" Text="Void" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblIdentity" runat="server" Text="IDENTITY"></asp:Label></legend>
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                    OnItemsRequested="cboPersonID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboPersonID_SelectedIndexChanged">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Employee Name required."
                                    ValidationGroup="entry" ControlToValidate="cboPersonID" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image25" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee No" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPlaceDOB" runat="server" Text="Place / Date Of Birth" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" ReadOnly="true" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSREmploymentType" runat="server" Text="Employment Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSREmploymentType" runat="server" Width="300px" Enabled="false" />
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblREmploymentPermanentDate" runat="server" Text="Employment Permanent Date" />
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtREmploymentPermanentDate" runat="server" Width="100px" Enabled="false" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td width="50%" valign="top">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblEmploymentData" runat="server" Text="EMPLOYMENT DATA"></asp:Label></legend>
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnitID" runat="server" Text="Section / Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                    OnItemsRequested="cboServiceUnitID_ItemsRequested" Enabled="false">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPositionID" runat="server" Text="Position"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPositionID" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboPositionID_ItemDataBound"
                                    OnItemsRequested="cboPositionID_ItemsRequested" Enabled="false">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRProfessionGroup" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRProfessionGroup_SelectedIndexChanged" />
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvSRProfessionGroup" runat="server" ErrorMessage="Profession Group required."
                                    ValidationGroup="entry" ControlToValidate="cboSRProfessionGroup" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRClinicalWorkArea" runat="server" Text="Work Area"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRClinicalWorkArea" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboSRClinicalWorkArea_ItemDataBound"
                                    OnItemsRequested="cboSRClinicalWorkArea_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboSRClinicalWorkArea_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvSRClinicalWorkArea" runat="server" ErrorMessage="Work Area required."
                                    ValidationGroup="entry" ControlToValidate="cboSRClinicalWorkArea" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRClinicalAuthorityLevel" runat="server" Text="Clinical Authority Level"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRClinicalAuthorityLevel" runat="server" Width="300px" EnableLoadOnDemand="True"
                                    HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboSRClinicalAuthorityLevel_ItemDataBound"
                                    OnItemsRequested="cboSRClinicalAuthorityLevel_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboSRClinicalAuthorityLevel_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:RequiredFieldValidator ID="rfvSRClinicalAuthorityLevel" runat="server" ErrorMessage="Clinical Authority Level required."
                                    ValidationGroup="entry" ControlToValidate="cboSRClinicalAuthorityLevel" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td />
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Application" PageViewID="pgApplication"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Clinical Privilege" PageViewID="pgClinicalPrivilege">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Credentialing Team" PageViewID="pgCredentialingTeam">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Recommendation & Conclusion" PageViewID="pgRecommendation">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Recommendation Letter" PageViewID="pgRecommendationLetter">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Clinical Assignment Letter" PageViewID="pgAssignmentLetter">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgApplication" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="100%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblEducationData" runat="server" Text="EDUCATION & WORK EXPERIENCE DATA"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td width="50%" valign="top">
                                        <table>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadComboBox ID="cboSREducationLevel" runat="server" Width="300px" AllowCustomText="true"
                                                        Filter="Contains" />
                                                </td>
                                                <td width="20" />
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblInstitutionName" runat="server" Text="Institution"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtInstitutionName" runat="server" Width="300px" MaxLength="255" />
                                                </td>
                                                <td width="20" />
                                                <td />
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblDiplomaNumber" runat="server" Text="Diploma No"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtDiplomaNumber" runat="server" Width="150px" MaxLength="50" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td class="label">
                                                                <asp:Label ID="lblDiplomaDate" runat="server" Text="Date"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtDiplomaDate" runat="server" Width="100px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20" />
                                                <td />
                                            </tr>
                                            <tr style="display: none">
                                                <td class="label">
                                                    <asp:Label ID="lblCompetencyCertificateNo" runat="server" Text="Competency Certificate No"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtCompetencyCertificateNo" runat="server" Width="150px" MaxLength="50" />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td class="label">
                                                                <asp:Label ID="lblCompetencyCertificateDateOfIssue" runat="server" Text="Date Of Issue"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="txtCompetencyCertificateDateOfIssue" runat="server" Width="100px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20" />
                                                <td />
                                            </tr>

                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <telerik:RadGrid ID="grdWorkExperience" runat="server" OnNeedDataSource="grdWorkExperience_NeedDataSource"
                                                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdWorkExperience_UpdateCommand"
                                                        OnDeleteCommand="grdWorkExperience_DeleteCommand" OnInsertCommand="grdWorkExperience_InsertCommand">
                                                        <HeaderContextMenu>
                                                        </HeaderContextMenu>
                                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="WorkExperienceNo">
                                                            <Columns>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle CssClass="MyImageButton" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="WorkExperienceNo" HeaderText="ID"
                                                                    UniqueName="WorkExperienceNo" SortExpression="WorkExperienceNo" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                                <telerik:GridBoundColumn DataField="InstitutionName" HeaderText="Institution"
                                                                    UniqueName="InstitutionName" SortExpression="InstitutionName" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="StartPeriod" HeaderText="Start Period"
                                                                    UniqueName="StartPeriod" SortExpression="StartPeriod" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EndPeriod" HeaderText="End Period"
                                                                    UniqueName="EndPeriod" SortExpression="EndPeriod" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="PositionName" HeaderText="Position"
                                                                    UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-HorizontalAlign="Left" />
                                                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                                    <HeaderStyle Width="30px" />
                                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                            <EditFormSettings UserControlName="CredentialingWorkExperienceItem.ascx" EditFormType="WebUserControl">
                                                                <EditColumn UniqueName="CredentialingWorkExperienceItemEditCommand">
                                                                </EditColumn>
                                                            </EditFormSettings>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </td>
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
                <asp:Panel runat="server" ID="pnlCompetencyAssessmentEvaluator">
                    <tr>
                        <td width="100%" valign="top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblCompetencyAssessmentEvaluator" runat="server" Text="COMPETENCY ASSESMENT EVALUATOR"></asp:Label></legend>
                                <table width="100%">
                                    <tr>
                                        <td width="50%" valign="top">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <telerik:RadGrid ID="grdCompetencyAssessmentEvaluator" runat="server" OnNeedDataSource="grdCompetencyAssessmentEvaluator_NeedDataSource"
                                                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdCompetencyAssessmentEvaluator_UpdateCommand"
                                                            OnDeleteCommand="grdCompetencyAssessmentEvaluator_DeleteCommand" OnInsertCommand="grdCompetencyAssessmentEvaluator_InsertCommand">
                                                            <HeaderContextMenu>
                                                            </HeaderContextMenu>
                                                            <MasterTableView CommandItemDisplay="None" DataKeyNames="EvaluatorID">
                                                                <Columns>
                                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                                        <HeaderStyle Width="30px" />
                                                                        <ItemStyle CssClass="MyImageButton" />
                                                                    </telerik:GridEditCommandColumn>
                                                                    <telerik:GridBoundColumn DataField="EvaluatorName" HeaderText="Evaluator Name"
                                                                        UniqueName="EvaluatorName" SortExpression="EvaluatorName" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRCompetencyAssessmentEvalRole" HeaderText="ID"
                                                                        UniqueName="SRCompetencyAssessmentEvalRole" SortExpression="SRCompetencyAssessmentEvalRole" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CompetencyAssessmentEvalRoleName" HeaderText="Role"
                                                                        UniqueName="CompetencyAssessmentEvalRoleName" SortExpression="CompetencyAssessmentEvalRoleName" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                                        <HeaderStyle Width="30px" />
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                    </telerik:GridButtonColumn>
                                                                </Columns>
                                                                <EditFormSettings UserControlName="CredentialingCompetencyAssessmentEvaluatorItem.ascx" EditFormType="WebUserControl">
                                                                    <EditColumn UniqueName="CredentialingCompetencyAssessmentEvaluatorItemEditCommand">
                                                                    </EditColumn>
                                                                </EditFormSettings>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="50%" valign="top">
                                            <table width="100%">
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <table width="100%" cellpadding="0" cellspacing="0">
                <asp:Panel runat="server" ID="pnlCredentialingProposed">
                    <tr>
                        <td width="50%" valign="top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblCredentialingProposesd" runat="server" Text="CREDENTIALING PROPOSED"></asp:Label></legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblCredentialApplicationDate" runat="server" Text="Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtCredentialApplicationDate" runat="server" Width="100px" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvCredentialApplicationDate" runat="server" ErrorMessage="Credentialing Proposed Date required."
                                                ValidationGroup="entry" ControlToValidate="txtCredentialApplicationDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRCredentialingStatus" runat="server" Text="Credentialing Status"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRCredentialingStatus" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvSRCredentialingStatus" runat="server" ErrorMessage="Credentialing Status required."
                                                ValidationGroup="entry" ControlToValidate="cboSRCredentialingStatus" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRRecredentialReason" runat="server" Text="Re-Credential Reason"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRRecredentialReason" runat="server" Width="300px" AllowCustomText="true"
                                                Filter="Contains" />
                                        </td>
                                        <td width="20" />
                                        <td />
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl" runat="server" Text="- DOCUMENT -" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="grdDocument" runat="server" OnNeedDataSource="grdDocument_NeedDataSource"
                                                AutoGenerateColumns="False" GridLines="None">
                                                <HeaderContextMenu>
                                                </HeaderContextMenu>
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="DocumentItemID">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                                                    Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsSelect"
                                                            UniqueName="IsSelect" SortExpression="IsSelect" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DocumentItemID" HeaderText="ID"
                                                            UniqueName="DocumentItemID" SortExpression="DocumentItemID" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="DocumentName" HeaderText="Document"
                                                            UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="350px" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" UniqueName="txtNotes">
                                                            <ItemTemplate>
                                                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="325px" Text='<%#Eval("Notes")%>' MaxLength="4000"
                                                                    Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Verified" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" UniqueName="chkIsVerified">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkIsVerified" runat="server" Width="50px" Checked='<%#Eval("IsVerified")%>'
                                                                    Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVerified" HeaderText="Verified"
                                                            UniqueName="IsVerified" SortExpression="IsVerified" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCPD" runat="server" Text="- CPD -" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="grdCPD" runat="server" OnNeedDataSource="grdCPD_NeedDataSource"
                                                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdCPD_UpdateCommand"
                                                OnDeleteCommand="grdCPD_DeleteCommand" OnInsertCommand="grdCPD_InsertCommand">
                                                <HeaderContextMenu>
                                                </HeaderContextMenu>
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="CpdNo">
                                                    <Columns>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle CssClass="MyImageButton" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CpdNo" HeaderText="ID"
                                                            UniqueName="CpdNo" SortExpression="CpdNo" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="CpdName" HeaderText="CPD Name"
                                                            UniqueName="CpdName" SortExpression="CpdName" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridBoundColumn DataField="InstitutionName" HeaderText="Institution"
                                                            UniqueName="InstitutionName" SortExpression="InstitutionName" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridBoundColumn DataField="TimeAndHours" HeaderText="Time & Number of Hours"
                                                            UniqueName="TimeAndHours" SortExpression="TimeAndHours" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridNumericColumn HeaderStyle-Width="70px" DataField="Skp" HeaderText="SKP"
                                                            UniqueName="Skp" SortExpression="Skp" HeaderStyle-HorizontalAlign="Right"
                                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                                        <telerik:GridBoundColumn DataField="AchievedCompetence" HeaderText="Achieved Competence"
                                                            UniqueName="AchievedCompetence" SortExpression="AchievedCompetence" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridBoundColumn DataField="PhysicalEvidence" HeaderText="Physical Evidence"
                                                            UniqueName="PhysicalEvidence" SortExpression="PhysicalEvidence" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <EditFormSettings UserControlName="CredentialingCpdItem.ascx" EditFormType="WebUserControl">
                                                        <EditColumn UniqueName="CredentialingCpdItemEditCommand">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLicense" runat="server" Text="- LICENSE -" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="grdLicense" runat="server" OnNeedDataSource="grdLicense_NeedDataSource"
                                                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdLicense_UpdateCommand"
                                                OnDeleteCommand="grdLicense_DeleteCommand" OnInsertCommand="grdLicense_InsertCommand">
                                                <HeaderContextMenu>
                                                </HeaderContextMenu>
                                                <MasterTableView CommandItemDisplay="None" DataKeyNames="SRLicenseType">
                                                    <Columns>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle CssClass="MyImageButton" />
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRLicenseType" HeaderText="ID"
                                                            UniqueName="SRLicenseType" SortExpression="SRLicenseType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="LicenseTypeName" HeaderText="License Type"
                                                            UniqueName="LicenseTypeName" SortExpression="LicenseTypeName" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridBoundColumn DataField="LicenseNo" HeaderText="License No"
                                                            UniqueName="LicenseNo" SortExpression="LicenseNo" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-HorizontalAlign="Left" />
                                                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="DateOfIssue" HeaderText="Date Of Issue"
                                                            UniqueName="DateOfIssue" SortExpression="DateOfIssue" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidUntil" HeaderText="Valid Until"
                                                            UniqueName="ValidUntil" SortExpression="ValidUntil" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                            <HeaderStyle Width="30px" />
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <EditFormSettings UserControlName="CredentialingLicenseItem.ascx" EditFormType="WebUserControl">
                                                        <EditColumn UniqueName="CredentialingLicenseItemEditCommand">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td width="50%" valign="top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblPrerequisites" runat="server" Text="INDIVIDUAL CREDENTIALING INFORMATION"></asp:Label></legend>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlCredentialingQuestion" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgClinicalPrivilege" runat="server">
            <telerik:RadTabStrip ID="tabStrip2" runat="server" MultiPageID="multiPage2" ShowBaseLine="true"
                Orientation="HorizontalTop">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Questionnaire" PageViewID="pgQuestionnaire"
                        Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Guidelines" PageViewID="pgGuidance">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="multiPage2" runat="server" BorderStyle="Solid" BorderColor="Gray">
                <telerik:RadPageView ID="pgQuestionnaire" runat="server" Selected="true">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 33%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblQuestionnaireID" runat="server" Text="Form Name" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboQuestionnaireID" runat="server" Width="100%" EnableLoadOnDemand="True"
                                                HighlightTemplatedItems="True" MarkFirstMatch="True" OnItemDataBound="cboQuestionnaireID_ItemDataBound"
                                                OnItemsRequested="cboQuestionnaireID_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboQuestionnaireID_SelectedIndexChanged">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "QuestionnaireName")%>
                                            &nbsp;[
                                            <%# DataBinder.Eval(Container.DataItem, "QuestionnaireCode")%>]
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvQuestionnaireID" runat="server" ErrorMessage="Questionnaire Form Name required."
                                                ValidationGroup="entry" ControlToValidate="cboQuestionnaireID" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%; vertical-align: top">
                                <table width="100%">
                                    <tr runat="server" id="trCompetencyAssessmentVerificationDate">
                                        <td class="label">
                                            <asp:Label ID="lblCompetencyAssessmentVerificationDate" runat="server" Text="Competency Asst. (#1) Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtCompetencyAssessmentVerificationDate" runat="server" Width="100px" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvCompetencyAssessmentVerificationDate" runat="server" ErrorMessage="Competency Asst. (#1) Date required."
                                                ValidationGroup="entry" ControlToValidate="txtCompetencyAssessmentVerificationDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                    <tr runat="server" id="trCompetencyAssessmentVerificationDate2">
                                        <td class="label">
                                            <asp:Label ID="lblCompetencyAssessmentVerificationDate2" runat="server" Text="Competency Asst. (#2) Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtCompetencyAssessmentVerificationDate2" runat="server" Width="100px" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvCompetencyAssessmentVerificationDate2" runat="server" ErrorMessage="Competency Asst. (#2) Date required."
                                                ValidationGroup="entry" ControlToValidate="txtCompetencyAssessmentVerificationDate2" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image17" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 33%; vertical-align: top">
                                <table width="100%">
                                    <tr runat="server" id="trCredentialingDate">
                                        <td class="label">
                                            <asp:Label ID="lblCredentialingDate" runat="server" Text="Credentialing Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtCredentialingDate" runat="server" Width="100px" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvCredentialingDate" runat="server" ErrorMessage="Credentialing Date required."
                                                ValidationGroup="entry" ControlToValidate="txtCredentialingDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                    <tr runat="server" id="trCertificateVerification">
                                        <td class="label">
                                            <asp:Label ID="lblCertificateVerification" runat="server" Text="Certificate Verification"></asp:Label></td>
                                        <td class="entry">
                                            <asp:RadioButtonList ID="rbtIsCertificateVerification" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem Value="1" Text="Yes" />
                                                <asp:ListItem Value="0" Text="No" />
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvIsCertificateVerification" runat="server" ErrorMessage="Certificate Verification required."
                                                ValidationGroup="entry" ControlToValidate="rbtIsCertificateVerification" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdSheet" runat="server" OnNeedDataSource="grdSheet_NeedDataSource"
                        OnItemDataBound="grdSheet_ItemDataBound" OnItemCreated="grdSheet_ItemCreated" AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="QuestionnaireItemID" CommandItemDisplay="None" ShowHeader="True">
                            <ColumnGroups>
                                <telerik:GridColumnGroup HeaderText="Collaboration" Name="Collaboration" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                            </ColumnGroups>
                            <ColumnGroups>
                                <telerik:GridColumnGroup HeaderText="Current Ability" Name="CurrentAbility" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionnaireItemID" HeaderText="ID"
                                    UniqueName="QuestionnaireItemID" SortExpression="QuestionnaireItemID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionCode" HeaderText="Code"
                                    UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="QuestionNo" HeaderText="No."
                                    UniqueName="QuestionNo" SortExpression="QuestionNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Question Name" UniqueName="lblQuestionName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestionName" runat="server" Text='<%# GetQuestionName(DataBinder.Eval(Container.DataItem, "SRCredentialQuestionLevel"), DataBinder.Eval(Container.DataItem, "QuestionName")) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsIndependent" HeaderText="Independent"
                                    UniqueName="IsIndependent" SortExpression="IsIndependent" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsDelegation" HeaderText="Delegation"
                                    UniqueName="IsDelegation" SortExpression="IsDelegation" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Collaboration" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsMandate" HeaderText="Mandate"
                                    UniqueName="IsMandate" SortExpression="IsMandate" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Collaboration" />

                                <telerik:GridTemplateColumn UniqueName="cboSRCurrentAbility" HeaderText="Self Assessment">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRCurrentAbility" runat="server" Width="80px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="SRCurrentAbility_ItemDataBound"
                                            OnItemsRequested="cboSRCurrentAbility_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRCurrentAbility" HeaderText="Self Assessment"
                                    UniqueName="SRCurrentAbility" SortExpression="SRCurrentAbility" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="CurrentAbility" HeaderText="Self Assessment"
                                    UniqueName="CurrentAbility" SortExpression="CurrentAbility" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />

                                <telerik:GridTemplateColumn HeaderStyle-Width="200px" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" UniqueName="txtSelfAssessmentNotes">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtSelfAssessmentNotes" runat="server" Width="185px" Text='<%#Eval("SelfAssessmentNotes")%>' MaxLength="200"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SelfAssessmentNotes" HeaderText="Notes"
                                    UniqueName="SelfAssessmentNotes" SortExpression="SelfAssessmentNotes" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" />

                                <telerik:GridTemplateColumn UniqueName="cboSRCurrentAbilitySupervisor" HeaderText="Eval #1 Assessment">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRCurrentAbilitySupervisor" runat="server" Width="80px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="SRCurrentAbility_ItemDataBound"
                                            OnItemsRequested="cboSRCurrentAbility_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRCurrentAbilitySupervisor" HeaderText="Eval #1 Assessment"
                                    UniqueName="SRCurrentAbilitySupervisor" SortExpression="SRCurrentAbilitySupervisor" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="CurrentAbilitySupervisor" HeaderText="Eval #1 Assessment"
                                    UniqueName="CurrentAbilitySupervisor" SortExpression="CurrentAbilitySupervisor" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />

                                <telerik:GridTemplateColumn UniqueName="cboSRCurrentAbilitySupervisor2" HeaderText="Eval #2 Assessment">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRCurrentAbilitySupervisor2" runat="server" Width="80px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="SRCurrentAbility_ItemDataBound"
                                            OnItemsRequested="cboSRCurrentAbility_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRCurrentAbilitySupervisor2" HeaderText="Eval #2 Assessment"
                                    UniqueName="SRCurrentAbilitySupervisor2" SortExpression="SRCurrentAbilitySupervisor2" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="CurrentAbilitySupervisor2" HeaderText="Eval #2 Assessment"
                                    UniqueName="CurrentAbilitySupervisor2" SortExpression="CurrentAbilitySupervisor2" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />

                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="CurrentAbility" HeaderText="Self Assessment"
                                    UniqueName="CurrentAbilityX" SortExpression="CurrentAbility" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="CurrentAbility" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="SelfAssessmentNotes" HeaderText="Notes"
                                    UniqueName="SelfAssessmentNotesX" SortExpression="SelfAssessmentNotes" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" ColumnGroupName="CurrentAbility" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="CurrentAbilitySupervisor" HeaderText="Eval #1 Assessment"
                                    UniqueName="CurrentAbilitySupervisorX" SortExpression="CurrentAbilitySupervisor" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="CurrentAbility" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="CurrentAbilitySupervisor2" HeaderText="Eval #2 Assessment"
                                    UniqueName="CurrentAbilitySupervisor2X" SortExpression="CurrentAbilitySupervisor2" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="CurrentAbility" />

                                <telerik:GridTemplateColumn UniqueName="cboSRReview" HeaderText="Review">
                                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRReview" runat="server" Width="80px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRReview_ItemDataBound"
                                            OnItemsRequested="cboSRReview_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRReview" HeaderText="Review"
                                    UniqueName="SRReview" SortExpression="SRReview" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="Review" HeaderText="Review"
                                    UniqueName="Review" SortExpression="Review" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />

                                <telerik:GridTemplateColumn UniqueName="cboSRRecomendation" HeaderText="Recommendation">
                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRRecomendation" runat="server" Width="80px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRRecomendation_ItemDataBound"
                                            OnItemsRequested="cboSRRecomendation_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRRecomendation" HeaderText="Recommendation"
                                    UniqueName="SRRecomendation" SortExpression="SRRecomendation" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="Recomendation" HeaderText="Recommendation"
                                    UniqueName="Recomendation" SortExpression="Recomendation" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />

                                <telerik:GridTemplateColumn UniqueName="cboSRConclusion" HeaderText="Conclusion">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRConclusion" runat="server" Width="110px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRConclusion_ItemDataBound"
                                            OnItemsRequested="cboSRConclusion_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRConclusion" HeaderText="Conclusion"
                                    UniqueName="SRConclusion" SortExpression="SRConclusion" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="Conclusion" HeaderText="Conclusion"
                                    UniqueName="Conclusion" SortExpression="Conclusion" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />

                                <telerik:GridTemplateColumn HeaderStyle-Width="200px" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" UniqueName="txtNotes">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="185px" Text='<%#Eval("Notes")%>' MaxLength="200"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Notes" HeaderText="Notes"
                                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />

                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IsDetail" HeaderText="IsDetail"
                                    UniqueName="IsDetail" SortExpression="IsDetail" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridTemplateColumn />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="False" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgGuidance" runat="server">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%" valign="top">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="lblInfo1" runat="server" Text="INFO 1" Font-Bold="true"></asp:Label></legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtInfo1" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td width="50%" valign="top">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="lblInfo2" runat="server" Text="INFO 2" Font-Bold="true"></asp:Label></legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtInfo2" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%" valign="top">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="lblInfo3" runat="server" Text="INFO 3" Font-Bold="true"></asp:Label></legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtInfo3" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td width="50%" valign="top">
                                <fieldset>
                                    <legend>
                                        <asp:Label ID="lblInfo4" runat="server" Text="INFO 4" Font-Bold="true"></asp:Label></legend>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <telerik:RadTextBox ID="txtInfo4" runat="server" Width="100%" MaxLength="4000" TextMode="MultiLine" Height="200" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgCredentialingTeam" runat="server">
            <telerik:RadGrid ID="grdCredentialTeam" runat="server" OnNeedDataSource="grdCredentialTeam_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdCredentialTeam_UpdateCommand"
                OnDeleteCommand="grdCredentialTeam_DeleteCommand" OnInsertCommand="grdCredentialTeam_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonID" HeaderText="Person ID"
                            UniqueName="PersonID" SortExpression="PersonID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="TeamMemberName" HeaderText="Team Member"
                            UniqueName="TeamMemberName" SortExpression="TeamMemberName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="PositionName" HeaderText="Position"
                            UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="CredentialingTeamPositionName" HeaderText="Team Status"
                            UniqueName="CredentialingTeamPositionName" SortExpression="CredentialingTeamPositionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="AreasOfExpertise" HeaderText="Areas Of Expertise"
                            UniqueName="AreasOfExpertise" SortExpression="AreasOfExpertise" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="CredentialingTeam.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CredentialingTeamEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRecommendation" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="grdRecommendationResult" runat="server" OnNeedDataSource="grdRecommendationResult_NeedDataSource"
                                        AutoGenerateColumns="False" GridLines="None">
                                        <HeaderContextMenu>
                                        </HeaderContextMenu>
                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="SRRecommendationResult">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="chkIsSelect" Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>"
                                                            Checked='<%#DataBinder.Eval(Container.DataItem, "IsSelect") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRRecommendationResult" HeaderText="ID"
                                                    UniqueName="SRRecommendationResult" SortExpression="SRRecommendationResult" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                <telerik:GridBoundColumn DataField="RecommendationResultName" HeaderText="Recommendation Result"
                                                    UniqueName="RecommendationResultName" SortExpression="RecommendationResultName" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridTemplateColumn HeaderStyle-Width="350px" HeaderText="Notes" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" UniqueName="txtNotes">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txtNotes" runat="server" Width="325px" Text='<%#Eval("Notes")%>' MaxLength="100"
                                                            Enabled="<%#DataModeCurrent != Temiang.Avicenna.Common.AppEnum.DataMode.Read %>" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="Perform"></asp:Label></td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rbtIsPerform" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="1" Text="Yes" />
                                        <asp:ListItem Value="0" Text="No" />
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvIsPerform" runat="server" ErrorMessage="Perform required."
                                        ValidationGroup="entry" ControlToValidate="rbtIsPerform" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRecommendationNotes" runat="server" Text="Notes"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtRecommendationNotes" runat="server" Width="100%" TextMode="MultiLine" Height="50px" />
                                </td>
                                <td width="20" />
                                <td />
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top"></td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgRecommendationLetter" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRecommendationLetterDate" runat="server" Text="Recommendation Letter Date" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtRecommendationLetterDate" runat="server" Width="100px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvRecommendationLetterDate" runat="server" ErrorMessage="Recommendation Letter Date required."
                                        ValidationGroup="entry" ControlToValidate="txtRecommendationLetterDate" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRecommendationLetterNo" runat="server" Text="Recommendation Letter No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtRecommendationLetterNo" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvRecommendationLetterNo" runat="server" ErrorMessage="Recommendation Letter required."
                                        ValidationGroup="entry" ControlToValidate="txtRecommendationLetterNo" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image19" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" AutoPostBack="true"
                                                    OnSelectedDateChanged="txtValidFrom_SelectedDateChanged" />
                                            </td>
                                            <td>&nbsp;To&nbsp;&nbsp;</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                                        ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image20" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                                        ValidationGroup="entry" ControlToValidate="txtValidTo" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image21" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top"></td>
                </tr>
            </table>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgAssignmentLetter" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblClinicalAssignmentLetterDate" runat="server" Text="Assignment Letter Date" />
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtClinicalAssignmentLetterDate" runat="server" Width="100px" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvClinicalAssignmentLetterDate" runat="server" ErrorMessage="Assignment Letter Date required."
                                        ValidationGroup="entry" ControlToValidate="txtClinicalAssignmentLetterDate" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image14" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDecreeNo" runat="server" Text="Decree No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtDecreeNo" runat="server" Width="300px" MaxLength="50" />
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvDecreeNo" runat="server" ErrorMessage="DecreeNo required."
                                        ValidationGroup="entry" ControlToValidate="txtDecreeNo" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image15" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblValidFrom2" runat="server" Text="Valid From"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtValidFrom2" runat="server" Width="100px" Enabled="false" />
                                            </td>
                                            <td>&nbsp;To&nbsp;&nbsp;</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtValidTo2" runat="server" Width="100px" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvValidFrom2" runat="server" ErrorMessage="Valid From required."
                                        ValidationGroup="entry" ControlToValidate="txtValidFrom2" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvValidTo2" runat="server" ErrorMessage="Valid To required."
                                        ValidationGroup="entry" ControlToValidate="txtValidTo2" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image16" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table>
                            <tr runat="server" id="trClinicalAssigmnetLetterUpload">
                                <td>
                                    <a href="#" onclick="javascript:openReportsDocument('cal'); return false;">
                                        <img src="../../../../Images/doc_upload48.png" border="0" alt="New" /><br />
                                    </a>
                                    <asp:Label runat="server" ID="lblClinicalAssigmnetLetterUpload" Text="Upload" ForeColor="Blue" Font-Size="Small"></asp:Label>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
