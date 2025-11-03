<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="EmployeeWorkingInfoDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.EmployeeWorkingInfoDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            function openPersonalDocument(dCode, rId, note) {
                var pid = $find("<%= txtPersonID.ClientID %>");
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/EmployeeHR/PersonalDocument/PersonalDocumentHist.aspx?pageId=ewi&pid=' + pid.get_value() + "&dc=" + dCode + "&rid=" + rId + "&note=" + note;
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
            function openTrainingEvaluation(id) {
                var pid = $find("<%= txtPersonID.ClientID %>");
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/TrainingHR/EmployeeTrainingEvaluation/EmployeeTrainingEvaluationDetail.aspx?md=view&id=' + id + "&pageId=ewi&pid=" + pid.get_value();
                window.location.href = url;
            }
            function openCredentialingDocument(tno, note) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Document/DocumentHist.aspx?pid=' + tno + "&note=" + note + "&type=ewi&role=adm";
                openWinMaxWindow(url);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winEntry" Width="600px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnPageId" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table width="150px">
                    <tr>
                        <td style="vertical-align: top">
                            <fieldset id="FieldSet1" style="width: 135px; min-height: 180px;">
                                <legend>Photo</legend>
                                <asp:Image runat="server" ID="imgPhoto" Width="135px" Height="180px" />
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr runat="server" id="trDocumentUpload">
                        <td>
                            <a href="#" onclick="javascript:openPersonalDocument('01','0',''); return false;">
                                <img src="../../../../Images/doc_upload48.png" border="0" alt="New" /><br />
                                Document Upload</a>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPersonID" runat="server" Text="Person ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPersonID" runat="server" Width="300px" AutoPostBack="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPersonID" runat="server" ErrorMessage="Person ID required."
                                ValidationGroup="entry" ControlToValidate="txtPersonID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
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
                            <asp:Label ID="Label2" runat="server" Text="Date Of Birth / Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtBirthDate" runat="server" Width="100px" DateInput-ReadOnly="true"
                                            DatePopupButton-Enabled="false" />
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtAge" runat="server" Width="199px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeStatus" runat="server" Text="Employee Status"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeStatus" runat="server" Width="300px" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSREmployeeStatus_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREmployeeStatus" runat="server" ErrorMessage="Employee Status required."
                                ValidationGroup="entry" ControlToValidate="cboSREmployeeStatus" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trResign" visible="false">
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Resign / Retired Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRResignReason" runat="server" Width="300px" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trSREmployeeType">
                        <td class="label">
                            <asp:Label ID="lblSREmployeeType" runat="server" Text="Employee Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSREmployeeType" runat="server" ErrorMessage="Employee Type required."
                                ValidationGroup="entry" ControlToValidate="cboSREmployeeType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRProfessionType" runat="server" Text="Profession Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRProfessionType" runat="server" Width="300px" AllowCustomText="true" Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblManagerID" runat="server" Text="Manager"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboManagerID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboManagerID_ItemDataBound"
                                OnItemsRequested="cboManagerID_ItemsRequested">
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupervisorID" runat="server" Text="Supervisor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSupervisorID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboSupervisorID_ItemDataBound"
                                OnItemsRequested="cboSupervisorID_ItemsRequested">
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPreceptorId" runat="server" Text="Preceptor"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPreceptorId" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPreceptorId_ItemDataBound"
                                OnItemsRequested="cboPreceptorId_ItemsRequested">
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
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr runat="server" id="trEmployeeRegistrationNo">
                        <td class="label">
                            <asp:Label ID="lblEmployeeRegistrationNo" runat="server" Text="Employee Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeRegistrationNo" runat="server" Width="300px" MaxLength="50" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlAttendanceSheet">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAbsenceCardNo" runat="server" Text="Absence Card No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAbsenceCardNo" runat="server" Width="300px" MaxLength="40" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSREmployeeShiftType" runat="server" Text="Shift Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSREmployeeShiftType" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSREmployeeScheduleType" runat="server" Text="Schedule Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSREmployeeScheduleType" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblJoinDate" runat="server" Text="Join - (Est.) Resign Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtJoinDate" runat="server" Width="100px" />
                                    </td>
                                    <td style="width: 10px">
                                        <asp:Label ID="lblResignDate" runat="server" Text=" - "></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtResignDate" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvJoinDate" runat="server" ErrorMessage="Join Date required."
                                ValidationGroup="entry" ControlToValidate="txtJoinDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREducationLevel" runat="server" Text="Education Level"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSREducationLevel" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrganizationName" runat="server" Text="Organization Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrganizationName" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionTitle" runat="server" Text="Position Title"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionTitle" runat="server" Width="300px" ReadOnly="true" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlCoorporateGrade">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" Text="Coorporate Grade Level / Value"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtCoorporateGradeLevel" runat="server" Width="148px" ReadOnly="true" /></td>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtCoorporateGradeValue" runat="server" Width="148px" ReadOnly="true" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade / Year"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox ID="txtPositionGradeID" runat="server" Width="200px" ReadOnly="true" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtGradeYearString" runat="server" Width="96px" ReadOnly="true" />
                                            <telerik:RadNumericTextBox ID="txtGradeYear" runat="server" Width="96px" ReadOnly="true" NumberFormat-DecimalDigits="0" Visible="false" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
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
                                <asp:Label ID="Label4" runat="server" Text="Service Year (Permanent)"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtServiceYearPermanent" runat="server" Width="100px" ReadOnly="true" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtServiceYearPermanentText" runat="server" Width="196px" ReadOnly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblNote" runat="server" Text="Note"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNote" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trKWI" visible="False">
                            <td class="label">
                                <asp:Label ID="lblKWI" runat="server" Text="KWI"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkKWI" Text="KWI" />
                            </td>
                            <td width="20px"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Employment Period" PageViewID="pgvEmploymentPeriod"
                Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Education Level" PageViewID="pgvEducationLevel">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Organization Unit" PageViewID="pgvOrganizationUnit">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Position" PageViewID="pgvPosition">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Position Grade" PageViewID="pgvPositionGrade">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Employee Grade" PageViewID="pgvGrade" Visible="false">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Achievement" PageViewID="pgvAchievement">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Disciplinary" PageViewID="pgvDisciplinary">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Training" PageViewID="pgvTraining">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Orientation" PageViewID="pgvOrientation">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Post Education" PageViewID="pgvEducation">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Appraisal" PageViewID="pgvAppraisal">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Clinical Privilege" PageViewID="pgvClinicalPrivilege">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Miscellaneous Benefit" PageViewID="pgvMiscellaneousBenefit">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Language Proficiency" PageViewID="pgvLanguageProficiency">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="RL4" PageViewID="pgvRL4">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvEmploymentPeriod" runat="server">
            <telerik:RadGrid ID="grdEmployeeEmploymentPeriod" runat="server" OnNeedDataSource="grdEmployeeEmploymentPeriod_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeEmploymentPeriod_UpdateCommand"
                OnDeleteCommand="grdEmployeeEmploymentPeriod_DeleteCommand" OnInsertCommand="grdEmployeeEmploymentPeriod_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeEmploymentPeriodID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeEmploymentPeriodID"
                            HeaderText="Employee Employment Period ID" UniqueName="EmployeeEmploymentPeriodID"
                            SortExpression="EmployeeEmploymentPeriodID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SREmploymentType" HeaderText="Employment Type"
                            UniqueName="SREmploymentType" SortExpression="SREmploymentType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="EmploymentTypeName" HeaderText="Employment Type"
                            UniqueName="EmploymentTypeName" SortExpression="EmploymentTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmployeeNumber" HeaderText="Applicant/ Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EmploymentCategoryName" HeaderText="Employment Category"
                            UniqueName="EmploymentCategoryName" SortExpression="EmploymentCategoryName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RecruitmentPlanName" HeaderText="Recruitment Plan"
                            UniqueName="RecruitmentPlanName" SortExpression="RecruitmentPlanName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Notes" UniqueName="Note" SortExpression="Note"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeEmploymentPeriodDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeEmploymentPeriodEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvEducationLevel" runat="server">
            <telerik:RadGrid ID="grdEmployeeEducationLevel" runat="server" OnNeedDataSource="grdEmployeeEducationLevel_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeEducationLevel_UpdateCommand"
                OnDeleteCommand="grdEmployeeEducationLevel_DeleteCommand" OnInsertCommand="grdEmployeeEducationLevel_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeEducationLevelID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeEducationLevelID"
                            HeaderText="EmployeeEducationLevelID" UniqueName="EmployeeEducationLevelID"
                            SortExpression="EmployeeEducationLevelID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SREducationLevel" HeaderText="SREducationLevel"
                            UniqueName="SREducationLevel" SortExpression="SREducationLevel" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn DataField="EducationLevelName" HeaderText="Education Level"
                            UniqueName="EducationLevelName" SortExpression="EducationLevelName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EducationGroupName" HeaderText="Education Group"
                            UniqueName="EducationGroupName" SortExpression="EducationGroupName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TypeOfLaborName" HeaderText="Type Of Labor"
                            UniqueName="TypeOfLaborName" SortExpression="TypeOfLaborName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeEducationLevelDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeEducationLevelEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvOrganizationUnit" runat="server">
            <telerik:RadGrid ID="grdEmployeeOrganization" runat="server" OnNeedDataSource="grdEmployeeOrganization_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeOrganization_UpdateCommand"
                OnDeleteCommand="grdEmployeeOrganization_DeleteCommand" OnInsertCommand="grdEmployeeOrganization_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeOrganizationID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeOrganizationID"
                            HeaderText="Employee Organization ID" UniqueName="EmployeeOrganizationID" SortExpression="EmployeeOrganizationID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="OrganizationID" HeaderText="Organization ID"
                            UniqueName="OrganizationID" SortExpression="OrganizationID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn DataField="OrganizationLevelTypeName" HeaderText="Level Type"
                            UniqueName="OrganizationLevelTypeName" SortExpression="OrganizationLevelTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="OrganizationUnitName" HeaderText="Department"
                            UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SubOrganizationUnitName" HeaderText="Division"
                            UniqueName="SubOrganizationUnitName" SortExpression="SubOrganizationUnitName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="SubDivisonName" HeaderText="Sub Divison"
                            UniqueName="SubDivisonName" SortExpression="SubDivisonName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Section / Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "21", DataBinder.Eval(Container.DataItem, "EmployeeOrganizationID"), DataBinder.Eval(Container.DataItem, "OrganizationUnitName") + " - " + DataBinder.Eval(Container.DataItem, "ServiceUnitName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeOrganizationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeOrganizationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvPosition" runat="server">
            <telerik:RadGrid ID="grdEmployeePosition" runat="server" OnNeedDataSource="grdEmployeePosition_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeePosition_UpdateCommand"
                OnDeleteCommand="grdEmployeePosition_DeleteCommand" OnInsertCommand="grdEmployeePosition_InsertCommand" OnItemCommand="grdEmployeePosition_ItemCommand" >
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeePositionID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeePositionID"
                            HeaderText="Employee Position ID" UniqueName="EmployeePositionID" SortExpression="EmployeePositionID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PositionID" HeaderText="Position ID"
                            UniqueName="PositionID" SortExpression="PositionID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position Name"
                            UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PositionDescription" HeaderText="Description"
                            UniqueName="PositionDescription" SortExpression="PositionDescription" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsPrimaryPosition"
                            HeaderText="Primary Position" UniqueName="IsPrimaryPosition" SortExpression="IsPrimaryPosition"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoorporateGradeLevel" HeaderText="Coorporate Grade Level"
                            UniqueName="CoorporateGradeLevel" SortExpression="CoorporateGradeLevel" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CoorporateGradeValue" HeaderText="Coorporate Grade Value"
                            UniqueName="CoorporateGradeValue" SortExpression="CoorporateGradeValue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />

                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="AssignmentNo" HeaderText="Assignment No"
                            UniqueName="AssignmentNo" SortExpression="AssignmentNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResignmentNo" HeaderText="Re-Asignment No"
                            UniqueName="ResignmentNo" SortExpression="ResignmentNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                         <telerik:GridTemplateColumn UniqueName="PrintJobDescription" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintGuarantorReceipt" runat="server" CommandName="PrintJobDescription"
                                    ToolTip='Print Job Description'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EmployeePositionID") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "22", DataBinder.Eval(Container.DataItem, "EmployeePositionID"), DataBinder.Eval(Container.DataItem, "PositionName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeePositionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeePositionEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvPositionGrade" runat="server">
            <telerik:RadGrid ID="grdPositionGrade" runat="server" OnNeedDataSource="grdPositionGrade_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionGrade_UpdateCommand"
                OnDeleteCommand="grdPositionGrade_DeleteCommand" OnInsertCommand="grdPositionGrade_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeePositionGradeID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeePositionGradeID"
                            HeaderText="ID" UniqueName="EmployeePositionGradeID" SortExpression="EmployeePositionGradeID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridBoundColumn DataField="EducationLevelName" HeaderText="Education Level"
                            UniqueName="EducationLevelName" SortExpression="EducationLevelName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DecreeTypeName" HeaderText="Decree Type"
                            UniqueName="DecreeTypeName" SortExpression="DecreeTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PositionGradeName" HeaderText="Position Grade"
                            UniqueName="PositionGradeName" SortExpression="PositionGradeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="GradeYear" HeaderText="Grade Year"
                            UniqueName="GradeYear" SortExpression="GradeYear" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SalaryScaleName" HeaderText="Salary Scale"
                            UniqueName="SalaryScaleName" SortExpression="SalaryScaleName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position Name" UniqueName="PositionName"
                            SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DecreeNo" HeaderText="Decree No" UniqueName="DecreeNo"
                            SortExpression="DecreeNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="70px" DataField="NextProposalDate"
                            HeaderText="Next Proposal" UniqueName="NextProposalDate" SortExpression="NextProposalDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="NextDecreeTypeName"
                            HeaderText="Next Decree Type" UniqueName="NextDecreeTypeName" SortExpression="NextDecreeTypeName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="NextPositionGradeName" HeaderText="Next Position Grade"
                            UniqueName="NextPositionGradeName" SortExpression="NextPositionGradeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NextGradeYear" HeaderText="Next Grade Year"
                            UniqueName="NextGradeYear" SortExpression="NextGradeYear" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />

                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="NextSalaryScaleName" HeaderText="Next Salary Scale"
                            UniqueName="NextSalaryScaleName" SortExpression="NextSalaryScaleName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="NextPositionName" HeaderText="Next Position Name"
                            UniqueName="NextPositionName" SortExpression="NextPositionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="Dp3Name" HeaderText="DP3" UniqueName="Dp3Name"
                            SortExpression="Dp3Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "23", DataBinder.Eval(Container.DataItem, "EmployeePositionGradeID"),  DataBinder.Eval(Container.DataItem, "DecreeTypeName") + " - " + DataBinder.Eval(Container.DataItem, "PositionGradeName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeePositionGradeDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeePositionGradeEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvGrade" runat="server">
            <telerik:RadGrid ID="grdEmployeeGrade" runat="server" OnNeedDataSource="grdEmployeeGrade_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeGrade_UpdateCommand"
                OnDeleteCommand="grdEmployeeGrade_DeleteCommand" OnInsertCommand="grdEmployeeGrade_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeGradeID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeGradeID" HeaderText="Employee Grade ID"
                            UniqueName="EmployeeGradeID" SortExpression="EmployeeGradeID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeGradeMasterID"
                            HeaderText="Employee Grade Master ID" UniqueName="EmployeeGradeMasterID" SortExpression="EmployeeGradeMasterID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="EmployeeGradeName"
                            HeaderText="Employee Grade Name" UniqueName="EmployeeGradeName" SortExpression="EmployeeGradeName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SalaryTableNumber"
                            HeaderText="Salary Table No" UniqueName="SalaryTableNumber" SortExpression="SalaryTableNumber"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeGradeDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeGradeEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAchievement" runat="server">
            <telerik:RadGrid ID="grdEmployeeAchievement" runat="server" OnNeedDataSource="grdEmployeeAchievement_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeAchievement_UpdateCommand"
                OnDeleteCommand="grdEmployeeAchievement_DeleteCommand" OnInsertCommand="grdEmployeeAchievement_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeAchievementID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeAchievementID"
                            HeaderText="Employee Achievement ID" UniqueName="EmployeeAchievementID" SortExpression="EmployeeAchievementID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="AwardID" HeaderText="Award ID"
                            UniqueName="AwardID" SortExpression="AwardID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridNumericColumn DataField="AwardName" HeaderText="Award Name"
                            UniqueName="AwardName" SortExpression="AwardName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="AwardDate" HeaderText="Award Date"
                            UniqueName="AwardDate" SortExpression="AwardDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Achievement" HeaderText="Achievement"
                            UniqueName="Achievement" SortExpression="Achievement" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="FinancialValue" HeaderText="Financial Value"
                            UniqueName="FinancialValue" SortExpression="FinancialValue" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Note" HeaderText="Note"
                            UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeAchievementDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeAchievementEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvDisciplinary" runat="server">
            <telerik:RadGrid ID="grdEmployeeDisciplinary" runat="server" OnNeedDataSource="grdEmployeeDisciplinary_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeDisciplinary_UpdateCommand"
                OnDeleteCommand="grdEmployeeDisciplinary_DeleteCommand" OnInsertCommand="grdEmployeeDisciplinary_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeDisciplinaryID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeDisciplinaryID"
                            HeaderText="Employee Disciplinary ID" UniqueName="EmployeeDisciplinaryID" SortExpression="EmployeeDisciplinaryID"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRWarningLevel" HeaderText="Warning Level"
                            UniqueName="SRWarningLevel" SortExpression="SRWarningLevel" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="WarningLevelName" HeaderText="Warning Level"
                            UniqueName="WarningLevelName" SortExpression="WarningLevelName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="IncidentDate" HeaderText="Incident Date"
                            UniqueName="IncidentDate" SortExpression="IncidentDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="DateIssue" HeaderText="Date Issue"
                            UniqueName="DateIssue" SortExpression="DateIssue" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="Violation" HeaderText="Violation"
                            UniqueName="Violation" SortExpression="Violation" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="EffectViolation" HeaderText="Effect Violation"
                            UniqueName="EffectViolation" SortExpression="EffectViolation" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRViolationDegree" HeaderText="Violation Degree"
                            UniqueName="SRViolationDegree" SortExpression="SRViolationDegree" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ViolationDegreeName"
                            HeaderText="Violation Degree" UniqueName="ViolationDegreeName" SortExpression="ViolationDegreeName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRViolationType" HeaderText="Violation Type"
                            UniqueName="SRViolationType" SortExpression="SRViolationType" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ViolationTypeName"
                            HeaderText="Violation Type" UniqueName="ViolationTypeName" SortExpression="ViolationTypeName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Notes"
                            UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="90px" DataField="EffectiveDate" HeaderText="Effective Date"
                            UniqueName="EffectiveDate" SortExpression="EffectiveDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidUntil" HeaderText="Valid Until"
                            UniqueName="ValidUntil" SortExpression="ValidUntil" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "24", DataBinder.Eval(Container.DataItem, "EmployeeDisciplinaryID"), DataBinder.Eval(Container.DataItem, "WarningLevelName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeDisciplinaryDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeDisciplinaryEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvTraining" runat="server">
            <telerik:RadGrid ID="grdEmployeeTrainingHistory" runat="server" OnNeedDataSource="grdEmployeeTrainingHistory_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeTrainingHistory_UpdateCommand"
                OnDeleteCommand="grdEmployeeTrainingHistory_DeleteCommand" OnInsertCommand="grdEmployeeTrainingHistory_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeTrainingHistoryID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeTrainingHistoryID"
                            HeaderText="Employee Training History ID" UniqueName="EmployeeTrainingHistoryID"
                            SortExpression="EmployeeTrainingHistoryID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="EventName" HeaderText="Training Name"
                            UniqueName="EventName" SortExpression="EventName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ActivityTypeName" HeaderText="Training Type"
                            UniqueName="ActivityTypeName" SortExpression="ActivityTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ActivitySubTypeName" HeaderText="Sub Type"
                            UniqueName="ActivitySubTypeName" SortExpression="ActivitySubTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TrainingLocation" HeaderText="Training Location"
                            UniqueName="TrainingLocation" SortExpression="TrainingLocation" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TrainingInstitution"
                            HeaderText="Training Organizer" UniqueName="TrainingInstitution" SortExpression="TrainingInstitution"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="SREmployeeTrainingDateSeparator" HeaderText=""
                            UniqueName="SREmployeeTrainingDateSeparator" SortExpression="SREmployeeTrainingDateSeparator" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="TotalHour" HeaderText="Total Hour"
                            UniqueName="TotalHour" SortExpression="TotalHour" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="CreditPoint" HeaderText="Credit Point"
                            UniqueName="CreditPoint" SortExpression="CreditPoint" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PlanningCosts" HeaderText="Planning Costs"
                            UniqueName="PlanningCosts" SortExpression="PlanningCosts" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Fee" HeaderText="Fee"
                            UniqueName="Fee" SortExpression="Fee" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SponsorFee" HeaderText="Sponsor Fee"
                            UniqueName="SponsorFee" SortExpression="SponsorFee" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsInHouseTraining" HeaderText="In-House"
                            UniqueName="IsInHouseTraining" SortExpression="IsInHouseTraining" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsScheduledTraining" HeaderText="Scheduled"
                            UniqueName="IsScheduledTraining" SortExpression="IsScheduledTraining" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsAttending" HeaderText="Attend"
                            UniqueName="IsAttending" SortExpression="IsAttending" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Notes"
                            UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="TrainingEvaluation"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openTrainingEvaluation('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Training Evaluation\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "EmployeeTrainingHistoryID")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "25", DataBinder.Eval(Container.DataItem, "EmployeeTrainingHistoryID"), DataBinder.Eval(Container.DataItem, "EventName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeTrainingHistoryDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeTrainingHistoryEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvOrientation" runat="server">
            <table width="100%">
                <tr>
                    <td style="width= 50%; vertical-align=top">
                        <table>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFilterOrientationType" runat="server" Text="Orientation Type"></asp:Label>
                                </td>
                                <td class="entry">
                                    <asp:RadioButtonList ID="rblFilterOrientationType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="true">All</asp:ListItem>
                                        <asp:ListItem>General</asp:ListItem>
                                        <asp:ListItem>Particular</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnFilterOrientationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterOrientationType_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width= 50%; vertical-align=top"></td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdEmployeeOrientation" runat="server" OnNeedDataSource="grdEmployeeOrientation_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeOrientation_UpdateCommand"
                OnDeleteCommand="grdEmployeeOrientation_DeleteCommand" OnInsertCommand="grdEmployeeOrientation_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeOrientationID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeOrientationID"
                            HeaderText="ID" UniqueName="EmployeeOrientationID"
                            SortExpression="EmployeeOrientationID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsGeneral" HeaderText="General Orientation"
                            UniqueName="IsGeneral" SortExpression="IsGeneral" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="150px" DataField="IsParticularOrientation" HeaderText="Particular Orientation"
                            UniqueName="IsParticularOrientation" SortExpression="IsParticularOrientation" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DurationHour" HeaderText="Duration (Hour)"
                            UniqueName="DurationHour" SortExpression="DurationHour" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n0}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="120px" DataField="DurationMinutes" HeaderText="Duration (Minutes)"
                            UniqueName="DurationMinutes" SortExpression="DurationMinutes" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n0}" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "27", DataBinder.Eval(Container.DataItem, "EmployeeOrientationID"), "Orientation") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeOrientationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeOrientationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvEducation" runat="server">
            <telerik:RadGrid ID="grdEmployeeEducation" runat="server" OnNeedDataSource="grdEmployeeEducation_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeEducation_UpdateCommand"
                OnDeleteCommand="grdEmployeeEducation_DeleteCommand" OnInsertCommand="grdEmployeeEducation_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeEducationID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeEducationID"
                            HeaderText="ID" UniqueName="EmployeeEducationID"
                            SortExpression="EmployeeEducationID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EducationStatusName" HeaderText="Status"
                            UniqueName="EducationStatusName" SortExpression="EducationStatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EducationFinancingSourcesName" HeaderText="Financing Sources"
                            UniqueName="EducationFinancingSourcesName" SortExpression="EducationFinancingSourcesName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsTuitionAssistance" HeaderText="Tuition Assistance"
                            UniqueName="IsTuitionAssistance" SortExpression="IsTuitionAssistance" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="InstitutionName"
                            HeaderText="Institution Name" UniqueName="InstitutionName" SortExpression="InstitutionName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="StudyProgram"
                            HeaderText="Study Program" UniqueName="StudyProgram" SortExpression="StudyProgram"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="StartYearPeriod"
                            HeaderText="Start Year" UniqueName="StartYearPeriod" SortExpression="StartYearPeriod"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="EndYearPeriod"
                            HeaderText="End Year" UniqueName="EndYearPeriod" SortExpression="EndYearPeriod"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="StudyPeriodStatusName" HeaderText="Study Period Status"
                            UniqueName="StudyPeriodStatusName" SortExpression="StudyPeriodStatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsCommitmentToWork" HeaderText="Employment / Service Bond"
                            UniqueName="IsCommitmentToWork" SortExpression="IsCommitmentToWork" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DurationOfService" HeaderText="Duration Of Service"
                            UniqueName="DurationOfService" SortExpression="DurationOfService" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartServiceDate" HeaderText="Start Service Date"
                            UniqueName="StartServiceDate" SortExpression="StartServiceDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndServiceDate" HeaderText="End Service Date"
                            UniqueName="EndServiceDate" SortExpression="EndServiceDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeEducationDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeEducationEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAppraisal" runat="server">
            <telerik:RadTabStrip ID="tabDetailAppraisal" runat="server" MultiPageID="mpgDetailAppraisal" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Questionnaire" PageViewID="pgvAppraisalQuestionnaire"
                        Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Performance Appraisal" PageViewID="pgvPerformanceAppraisal">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetailAppraisal" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvAppraisalQuestionnaire" runat="server">
                    <telerik:RadGrid ID="grdAppraisalQuestion" runat="server" OnNeedDataSource="grdAppraisalQuestion_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAppraisalQuestion_UpdateCommand"
                        OnDeleteCommand="grdAppraisalQuestion_DeleteCommand" OnInsertCommand="grdAppraisalQuestion_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeAppraisalQuestionerID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeAppraisalQuestionerID"
                                    HeaderText="ID" UniqueName="EmployeeAppraisalQuestionerID"
                                    SortExpression="EmployeeAppraisalQuestionerID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="QuestionerNo" HeaderText="Questionnaire No"
                                    UniqueName="QuestionerNo" SortExpression="QuestionerNo" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="QuestionerName" HeaderText="Questionnaire Name"
                                    UniqueName="QuestionerName" SortExpression="QuestionerName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="EmployeeAppraisalQuestionDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EmployeeAppraisalQuestionEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvPerformanceAppraisal" runat="server">
                    <telerik:RadGrid ID="grdPerformanceAppraisal" runat="server" OnNeedDataSource="grdPerformanceAppraisal_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPerformanceAppraisal_UpdateCommand"
                        OnDeleteCommand="grdPerformanceAppraisal_DeleteCommand" OnInsertCommand="grdPerformanceAppraisal_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PerformanceAppraisalID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PerformanceAppraisalID"
                                    HeaderText="ID" UniqueName="PerformanceAppraisalID"
                                    SortExpression="PerformanceAppraisalID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="YearPeriod" HeaderText="Year"
                                    UniqueName="YearPeriod" SortExpression="YearPeriod" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="QuarterPeriodName" HeaderText="Quarter"
                                    UniqueName="QuarterPeriodName" SortExpression="QuarterPeriodName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Score" HeaderText="Score"
                                    UniqueName="Score" SortExpression="Score" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ScoreText" HeaderText="Score Text"
                                    UniqueName="ScoreText" SortExpression="ScoreText" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="EmployeePerformanceAppraisalDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="EmployeePerformanceAppraisalEditCommand">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvClinicalPrivilege" runat="server">
            <telerik:RadGrid ID="grdEmployeeClinicalPrivilege" runat="server" OnNeedDataSource="grdEmployeeClinicalPrivilege_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeClinicalPrivilege_UpdateCommand"
                OnDeleteCommand="grdEmployeeClinicalPrivilege_DeleteCommand" OnInsertCommand="grdEmployeeClinicalPrivilege_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeClinicalPrivilegeID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeClinicalPrivilegeID"
                            HeaderText="ID" UniqueName="EmployeeClinicalPrivilegeID"
                            SortExpression="EmployeeClinicalPrivilegeID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ProfessionGroupName"
                            HeaderText="Profession Group" UniqueName="ProfessionGroupName" SortExpression="ProfessionGroupName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ClinicalWorkAreaName"
                            HeaderText="Clinical Work Area" UniqueName="ClinicalWorkAreaName" SortExpression="ClinicalWorkAreaName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClinicalAuthorityLevelName"
                            HeaderText="Clinical Authority Level" UniqueName="ClinicalAuthorityLevelName" SortExpression="ClinicalAuthorityLevelName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DecreeNo"
                            HeaderText="Decree No" UniqueName="DecreeNo" SortExpression="DecreeNo"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Note" HeaderText="Note"
                            UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "TransactionNo").Equals("") ? "" : string.Format("<a href=\"#\" onclick=\"openCredentialingDocument('{0}', 'cal'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Clinical Assignment Letter\" /></a>",
                                                                                DataBinder.Eval(Container.DataItem, "TransactionNo"))) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeClinicalPrivilegeDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeClinicalPrivilegeEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvMiscellaneousBenefit" runat="server">
            <telerik:RadGrid ID="grdEmployeeMiscellaneousBenefit" runat="server" OnNeedDataSource="grdEmployeeMiscellaneousBenefit_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeMiscellaneousBenefit_UpdateCommand"
                OnDeleteCommand="grdEmployeeMiscellaneousBenefit_DeleteCommand" OnInsertCommand="grdEmployeeMiscellaneousBenefit_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeMiscellaneousBenefitID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="EmployeeMiscellaneousBenefitID"
                            HeaderText="Employee Miscellaneous Benefit ID" UniqueName="EmployeeMiscellaneousBenefitID"
                            SortExpression="EmployeeMiscellaneousBenefitID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRMiscellaneousBenefit"
                            HeaderText="Miscellaneous Benefit" UniqueName="SRMiscellaneousBenefit" SortExpression="SRMiscellaneousBenefit"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="MiscellaneousBenefitName"
                            HeaderText="Miscellaneous Benefit" UniqueName="SRMiscellaneousBenefit" SortExpression="SRMiscellaneousBenefit"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Note"
                            UniqueName="Note" SortExpression="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeMiscellaneousBenefitDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeMiscellaneousBenefitEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvLanguageProficiency" runat="server">
            <telerik:RadGrid ID="grdEmployeeLanguageProficiency" runat="server" OnNeedDataSource="grdEmployeeLanguageProficiency_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeLanguageProficiency_UpdateCommand"
                OnDeleteCommand="grdEmployeeLanguageProficiency_DeleteCommand" OnInsertCommand="grdEmployeeLanguageProficiency_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeLanguageProficiencyID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeLanguageProficiencyID"
                            HeaderText="Employee Language Proficiency ID" UniqueName="EmployeeLanguageProficiencyID"
                            SortExpression="EmployeeLanguageProficiencyID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EvaluationDate" HeaderText="Evaluation Date"
                            UniqueName="EvaluationDate" SortExpression="EvaluationDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LanguageName" HeaderText="Language"
                            UniqueName="LanguageName" SortExpression="LanguageName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ConversationName" HeaderText="Conversation"
                            UniqueName="ConversationName" SortExpression="ConversationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TranslationName" HeaderText="Translation"
                            UniqueName="TranslationName" SortExpression="TranslationName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openPersonalDocument('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document Upload\" /></a>",
                                                                                        "26", DataBinder.Eval(Container.DataItem, "EmployeeLanguageProficiencyID"), DataBinder.Eval(Container.DataItem, "LanguageName")) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeLanguageProficiencyDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeLanguageProficiencyEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRL4" runat="server">
            <telerik:RadGrid ID="grdRL4" runat="server" OnNeedDataSource="grdRL4_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRL4_UpdateCommand"
                OnDeleteCommand="grdRL4_DeleteCommand" OnInsertCommand="grdRL4_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="EmployeeRL4ID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeRL4ID"
                            HeaderText="ID" UniqueName="EmployeeRL4ID"
                            SortExpression="EmployeeRL4ID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RL4StatusName" HeaderText="Labor Status"
                            UniqueName="RL4StatusName" SortExpression="RL4StatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="RL4TypeName" HeaderText="RL4 Type"
                            UniqueName="RL4TypeName" SortExpression="RL4TypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="RL4ProfessionTypeName" HeaderText="Profession Type"
                            UniqueName="RL4ProfessionTypeName" SortExpression="RL4ProfessionTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="RL4EducationLevelName" HeaderText="Education Level"
                            UniqueName="RL4EducationLevelName" SortExpression="RL4EducationLevelName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RL4EducationMajorName" HeaderText="Major"
                            UniqueName="RL4EducationMajorName" SortExpression="RL4EducationMajorName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidFrom" HeaderText="Valid From"
                            UniqueName="ValidFrom" SortExpression="ValidFrom" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ValidTo" HeaderText="Valid To"
                            UniqueName="ValidTo" SortExpression="ValidTo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                            UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                            HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeRL4Detail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeRL4DetailEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
