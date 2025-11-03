<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="SafetyCultureIncidentReportsDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEHRS.SafetyCultureIncidentReportsDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/PHR/PhrCtl.ascx" TagPrefix="uc1" TagName="PhrCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            function openReportsDocument(note) {
                var pid = $find("<%= txtTransactionNo.ClientID %>");
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/KEHRS/Document/DocumentHist.aspx?pid=' + pid.get_value() + "&note=" + note;
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
                            <asp:Label ID="lblTransactioNo" runat="server" Text="No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="160px" ReadOnly="true" />
                        </td>
                        <td width="20" />
                        <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="No required."
                            ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                            Width="100%">
                            <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                        </asp:RequiredFieldValidator>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReportDate" runat="server" Text="Report Time" />
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtReportDate" runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <telerik:RadMaskedTextBox ID="txtReportTime" runat="server" Mask="<00..23>:<00..59>"
                                            PromptChar="_" RoundNumericRanges="false" Width="50px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvReportDate" runat="server" ErrorMessage="Report Time required."
                                ValidationGroup="entry" ControlToValidate="txtReportDate" SetFocusOnError="True"
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
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReportDescription" runat="server" Text="Report Description" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReportDescription" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvReportDescription" runat="server" ErrorMessage="Report Description required."
                                ValidationGroup="entry" ControlToValidate="txtReportDescription" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr runat="server" id="trDocumentUpload">
                        <td colspan="4">
                            <a href="#" onclick="javascript:openReportsDocument(''); return false;">
                                <img src="../../../../Images/doc_upload48.png" border="0" alt="New" /><br />
                                Document Upload</a>
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
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Incident Report" PageViewID="pgReport"
                Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Chronology" PageViewID="pgChronology">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Verification" PageViewID="pgVerification">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Conclusion & Recommendation" PageViewID="pgConclusionRecomendation">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgReport" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="34%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblVictim" runat="server" Text="VICTIM"></asp:Label></legend>
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
                                        <asp:Label ID="lblSRProfessionType" runat="server" Text="Profession Type"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSRProfessionType" runat="server" Width="300px" AllowCustomText="true"
                                            Filter="Contains" Enabled="false" />
                                    </td>
                                    <td width="20" />
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblOrganizationUnitID" runat="server" Text="Department"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboOrganizationUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20" />
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSubOrganizationUnitID" runat="server" Text="Division"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSubOrganizationUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20" />
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblSubDivisonID" runat="server" Text="Sub Division"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboSubDivisonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20" />
                                    <td />
                                </tr>
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblServiceUnit" runat="server" Text="Section / Service Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="300px" EnableLoadOnDemand="true"
                                            MarkFirstMatch="False" HighlightTemplatedItems="true" OnItemDataBound="cboOrganizationUnitID_ItemDataBound" Enabled="false">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td width="20" />
                                    <td />
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td width="33%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="lblSubject" runat="server" Text="SUBJECT"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="grdSubject" runat="server" OnNeedDataSource="grdSubject_NeedDataSource"
                                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdSubject_UpdateCommand"
                                            OnDeleteCommand="grdSubject_DeleteCommand" OnInsertCommand="grdSubject_InsertCommand">
                                            <HeaderContextMenu>
                                            </HeaderContextMenu>
                                            <MasterTableView CommandItemDisplay="None" DataKeyNames="SubjectPersonID">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="SubjectPersonID" HeaderText="Person ID"
                                                        UniqueName="SubjectPersonID" SortExpression="SubjectPersonID" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" Visible="False" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="SubjectName" HeaderText="Subject Name"
                                                        UniqueName="SubjectName" SortExpression="SubjectName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridBoundColumn DataField="SubjectProfessionTypeName" HeaderText="Profession Type"
                                                        UniqueName="SubjectProfessionTypeName" SortExpression="SubjectProfessionTypeName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridBoundColumn DataField="SubjectOrganizationName" HeaderText="Department"
                                                        UniqueName="SubjectOrganizationName" SortExpression="SubjectOrganizationName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridBoundColumn DataField="SubjectSubOrganizationName" HeaderText="Division"
                                                        UniqueName="SubjectSubOrganizationName" SortExpression="SubjectSubOrganizationName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                    <telerik:GridBoundColumn DataField="SubjectSubDivisonName" HeaderText="Sub Division"
                                                        UniqueName="SubjectSubDivisonName" SortExpression="SubjectSubDivisonName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                    <telerik:GridBoundColumn DataField="SubjectServiceUnitName" HeaderText="Section / Service Unit"
                                                        UniqueName="SubjectServiceUnitName" SortExpression="SubjectServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsMainSubject" HeaderText="Main Subject"
                                                        UniqueName="IsMainSubject" SortExpression="IsMainSubject" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="SafetyCultureIncidentReportsSubjectItem.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="SafetyCultureIncidentReportsSubjectEditCommand">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td width="33%" valign="top">
                        <fieldset>
                            <legend>
                                <asp:Label ID="Label1" runat="server" Text="WITNESS"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="grdWitness" runat="server" OnNeedDataSource="grdWitness_NeedDataSource"
                                            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdWitness_UpdateCommand"
                                            OnDeleteCommand="grdWitness_DeleteCommand" OnInsertCommand="grdWitness_InsertCommand">
                                            <HeaderContextMenu>
                                            </HeaderContextMenu>
                                            <MasterTableView CommandItemDisplay="None" DataKeyNames="WitnessPersonID">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle CssClass="MyImageButton" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="WitnessPersonID" HeaderText="Person ID"
                                                        UniqueName="WitnessPersonID" SortExpression="WitnessPersonID" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" Visible="False" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="WitnessName" HeaderText="Witness Name"
                                                        UniqueName="WitnessName" SortExpression="WitnessName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridBoundColumn DataField="WitnessProfessionTypeName" HeaderText="Profession Type"
                                                        UniqueName="WitnessProfessionTypeName" SortExpression="WitnessProfessionTypeName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridBoundColumn DataField="WitnessOrganizationName" HeaderText="Department"
                                                        UniqueName="WitnessOrganizationName" SortExpression="WitnessOrganizationName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridBoundColumn DataField="WitnessSubOrganizationName" HeaderText="Division"
                                                        UniqueName="WitnessSubOrganizationName" SortExpression="WitnessSubOrganizationName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                    <telerik:GridBoundColumn DataField="WitnessSubDivisonName" HeaderText="Sub Division"
                                                        UniqueName="WitnessSubDivisonName" SortExpression="WitnessSubDivisonName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" Visible="false" />
                                                    <telerik:GridBoundColumn DataField="WitnessServiceUnitName" HeaderText="Section / Service Unit"
                                                        UniqueName="WitnessServiceUnitName" SortExpression="WitnessServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" />
                                                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings UserControlName="SafetyCultureIncidentReportsWitnessItem.ascx" EditFormType="WebUserControl">
                                                    <EditColumn UniqueName="SafetyCultureIncidentReportsWitnessEditCommand">
                                                    </EditColumn>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlSafetyCultureIncidentReportsQuestion" runat="server" />
        </telerik:RadPageView>

        <telerik:RadPageView ID="pgChronology" runat="server">
            <telerik:RadGrid ID="grdChronology" runat="server" OnNeedDataSource="grdChronology_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdChronology_UpdateCommand"
                OnDeleteCommand="grdChronology_DeleteCommand" OnInsertCommand="grdChronology_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ChronologyDateTime" HeaderText="Date / Time"
                            UniqueName="ChronologyDateTime" SortExpression="ChronologyDateTime" DataType="System.DateTime"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="105px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ChronologyDescription" HeaderText="Chronology"
                            UniqueName="ChronologyDescription" SortExpression="ChronologyDescription" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Subjects" HeaderText="Subjects"
                            UniqueName="Subjects" SortExpression="Subjects" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="SafetyCultureIncidentReportsChronologyItem.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="SafetyCultureIncidentReportsChronologyEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>

        <telerik:RadPageView ID="pgVerification" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblSRIncidentReportStatus" runat="server" Text="Report Status" />
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboSRIncidentReportStatus" runat="server" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20">
                                    <asp:RequiredFieldValidator ID="rfvSRIncidentReportStatus" runat="server" ErrorMessage="Report Status required."
                                        ValidationGroup="entry" ControlToValidate="cboSRIncidentReportStatus" SetFocusOnError="True"
                                        Width="100%">
                                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td />
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblResume" runat="server" Text="Resume" />
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtResume" runat="server" Width="300px" TextMode="MultiLine" Height="50px" />
                                </td>
                                <td width="20" />
                                <td />
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdParticipant" runat="server" OnNeedDataSource="grdParticipant_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdParticipant_UpdateCommand"
                OnDeleteCommand="grdParticipant_DeleteCommand" OnInsertCommand="grdParticipant_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ParticipantPersonID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ParticipantPersonID" HeaderText="Person ID"
                            UniqueName="ParticipantPersonID" SortExpression="ParticipantPersonID" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" Visible="False" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ParticipantName" HeaderText="Participant Name"
                            UniqueName="ParticipantName" SortExpression="ParticipantName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ParticipantStatusName" HeaderText="Status"
                            UniqueName="ParticipantStatusName" SortExpression="ParticipantStatusName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="SafetyCultureIncidentReportsParticipantItem.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="SafetyCultureIncidentReportsParticipantEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>

        </telerik:RadPageView>

        <telerik:RadPageView ID="pgConclusionRecomendation" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <telerik:RadTabStrip ID="tabStrip1" runat="server" MultiPageID="multiPage1" ShowBaseLine="true"
                            Orientation="HorizontalTop">
                            <Tabs>
                                <telerik:RadTab runat="server" Text="Meeting" PageViewID="pgMeeting"
                                    Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Conclusion" PageViewID="pgConclusion">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Recommendation" PageViewID="pgRecommendation">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="multiPage1" runat="server" BorderStyle="Solid" BorderColor="Gray">
                            <telerik:RadPageView ID="pgMeeting" runat="server" Selected="true">
                                <telerik:RadGrid ID="grdMeeting" runat="server" OnNeedDataSource="grdMeeting_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdMeeting_UpdateCommand"
                                    OnDeleteCommand="grdMeeting_DeleteCommand" OnInsertCommand="grdMeeting_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="MeetingDateTime" HeaderText="Date / Time"
                                                UniqueName="MeetingDateTime" SortExpression="MeetingDateTime" DataType="System.DateTime"
                                                DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                <HeaderStyle HorizontalAlign="Center" Width="105px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MeetingSummary" HeaderText="Meeting Summary"
                                                UniqueName="MeetingSummary" SortExpression="MeetingSummary" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Participants" HeaderText="Participants"
                                                UniqueName="Participants" SortExpression="Participants" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="SafetyCultureIncidentReportsMeetingItem.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="SafetyCultureIncidentReportsMeetingEditCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>

                            <telerik:RadPageView ID="pgConclusion" runat="server">
                                <telerik:RadGrid ID="grdConslusion" runat="server" OnNeedDataSource="grdConslusion_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdConslusion_UpdateCommand"
                                    OnDeleteCommand="grdConslusion_DeleteCommand" OnInsertCommand="grdConslusion_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80" DataField="SequenceNo" HeaderText="No"
                                                UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Conclusion" HeaderText="Conclusion"
                                                UniqueName="Conclusion" SortExpression="Conclusion" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="SafetyCultureIncidentReportsConslusionItem.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="SafetyCultureIncidentReportsConslusionEditCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>

                            <telerik:RadPageView ID="pgRecommendation" runat="server">
                                <telerik:RadGrid ID="grdRecommendation" runat="server" OnNeedDataSource="grdRecommendation_NeedDataSource"
                                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRecommendation_UpdateCommand"
                                    OnDeleteCommand="grdRecommendation_DeleteCommand" OnInsertCommand="grdRecommendation_InsertCommand">
                                    <HeaderContextMenu>
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" DataKeyNames="SequenceNo">
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle CssClass="MyImageButton" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="80" DataField="SequenceNo" HeaderText="No"
                                                UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Recommendation" HeaderText="Recommendation"
                                                UniqueName="Recommendation" SortExpression="Recommendation" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings UserControlName="SafetyCultureIncidentReportsRecommendationItem.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="SafetyCultureIncidentReportsRecommendationEditCommand">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </td>
                </tr>
            </table>

        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
