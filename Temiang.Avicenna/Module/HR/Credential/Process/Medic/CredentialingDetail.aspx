<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="CredentialingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.Medic.CredentialingDetail" %>

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
                                <img src="../../../../../Images/doc_upload48.png" border="0" alt="New" /><br />
                            </a>
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
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRProfessionGroup_SelectedIndexChanged" Enabled="false" />
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
            <telerik:RadTab runat="server" Text="Notes" PageViewID="pgRecommendation">
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
                                <asp:Label ID="lblEducationData" runat="server" Text="EDUCATION & LICENSE DATA"></asp:Label></legend>
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
                                                            <EditFormSettings UserControlName="../CredentialingLicenseItem.ascx" EditFormType="WebUserControl">
                                                                <EditColumn UniqueName="CredentialingLicenseItemEditCommand">
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
                            </fieldset>
                        </td>
                        <td width="50%" valign="top"></td>
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
                            <td style="width: 34%; vertical-align: top">
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
                                    <tr runat="server" id="trVerifiedDateTime">
                                        <td class="label">
                                            <asp:Label ID="lblVerifiedDateTime" runat="server" Text="Appr. By Supervisor Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtVerifiedDateTime" runat="server" Width="100px" Enabled="false" />
                                        </td>
                                        <td width="20"></td>
                                        <td />
                                    </tr>
                                    <tr runat="server" id="trCredentialingDate">
                                        <td class="label">
                                            <asp:Label ID="lblCredentialingDate" runat="server" Text="Appr. By Sub Committee Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtCredentialingDate" runat="server" Width="100px" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvCredentialingDate" runat="server" ErrorMessage="Approval By Sub Committe required."
                                                ValidationGroup="entry" ControlToValidate="txtCredentialingDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%; vertical-align: top">
                                <table width="100%">
                                    <tr runat="server" id="trRecommendationLetterDate">
                                        <td class="label">
                                            <asp:Label ID="lblRecommendationLetterDate" runat="server" Text="Appr. By Committee Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtRecommendationLetterDate" runat="server" Width="100px" />
                                        </td>
                                        <td width="20">
                                            <asp:RequiredFieldValidator ID="rfvRecommendationLetterDate" runat="server" ErrorMessage="Approval By Committe Date required."
                                                ValidationGroup="entry" ControlToValidate="txtRecommendationLetterDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image18" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td />
                                    </tr>
                                    <tr runat="server" id="trVerifiedDateTime2">
                                        <td class="label">
                                            <asp:Label ID="lblVerifiedDateTime2" runat="server" Text="Appr. By Director Date" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtVerifiedDateTime2" runat="server" Width="100px" Enabled="false" />
                                        </td>
                                        <td width="20"></td>
                                        <td />
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="grdSheet" runat="server" OnNeedDataSource="grdSheet_NeedDataSource"
                        OnItemDataBound="grdSheet_ItemDataBound" OnItemCreated="grdSheet_ItemCreated" AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="QuestionnaireItemID" CommandItemDisplay="None" ShowHeader="True">
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

                                <telerik:GridTemplateColumn UniqueName="cboSRCurrentAbility" HeaderText="Self Assessment">
                                    <HeaderStyle Width="210px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRCurrentAbility" runat="server" Width="200px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="SRCurrentAbility_ItemDataBound"
                                            OnItemsRequested="cboSRCurrentAbility_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRCurrentAbility" HeaderText="Self Assessment"
                                    UniqueName="SRCurrentAbility" SortExpression="SRCurrentAbility" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="CurrentAbilityName" HeaderText="Self Assessment"
                                    UniqueName="CurrentAbility" SortExpression="CurrentAbility" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />

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

                                <telerik:GridTemplateColumn UniqueName="cboSRReview" HeaderText="Appr. By Supervisor">
                                    <HeaderStyle Width="210px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRReview" runat="server" Width="200px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRReview_ItemDataBound"
                                            OnItemsRequested="cboSRReview_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRReview" HeaderText="Appr. By Supervisor"
                                    UniqueName="SRReview" SortExpression="SRReview" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ReviewName" HeaderText="Appr. By Supervisor"
                                    UniqueName="Review" SortExpression="Review" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />

                                <telerik:GridTemplateColumn UniqueName="cboSRRecomendation" HeaderText="Appr. By Director">
                                    <HeaderStyle Width="210px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRRecomendation" runat="server" Width="200px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRRecomendation_ItemDataBound"
                                            OnItemsRequested="cboSRRecomendation_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="SRRecomendation" HeaderText="Appr. By Director"
                                    UniqueName="SRRecomendation" SortExpression="SRRecomendation" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="RecomendationName" HeaderText="Appr. By Director"
                                    UniqueName="Recomendation" SortExpression="Recomendation" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />

                                <telerik:GridTemplateColumn UniqueName="cboSRConclusion" HeaderText="Conclusion">
                                    <HeaderStyle Width="210px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cboSRConclusion" runat="server" Width="200px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRConclusion_ItemDataBound"
                                            OnItemsRequested="cboSRConclusion_ItemsRequested"
                                            Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRConclusion" HeaderText="Conclusion"
                                    UniqueName="SRConclusion" SortExpression="SRConclusion" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Conclusion" HeaderText="Conclusion"
                                    UniqueName="Conclusion" SortExpression="Conclusion" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />

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
        <telerik:RadPageView ID="pgRecommendation" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblNotesBySubCommittee" runat="server" Text="CREDENTIALING SUB COMMITTEE"></asp:Label></legend>
                            <table width="100%">

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
                        </fieldset>
                    </td>
                    <td width="50%" valign="top">
                        <asp:Panel runat="server" ID="pnlCommitteeNote">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblNotesByCommitte" runat="server" Text="MEDICAL COMMITTEE"></asp:Label></legend>
                                <table width="100%">

                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblRecommendationResultNotes" runat="server" Text="Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtRecommendationResultNotes" runat="server" Width="100%" TextMode="MultiLine" Height="50px" />
                                        </td>
                                        <td width="20" />
                                        <td />
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                    </td>
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
                    <td width="50%" valign="top">
                        <table>
                            <tr runat="server" id="trClinicalAssigmnetLetterUpload">
                                <td>
                                    <a href="#" onclick="javascript:openReportsDocument('cal'); return false;">
                                        <img src="../../../../../Images/doc_upload48.png" border="0" alt="New" /><br />
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
