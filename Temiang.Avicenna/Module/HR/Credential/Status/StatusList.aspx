<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="StatusList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Status.StatusList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            function openReportsDocument(pid) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Document/DocumentHist.aspx?pid=' + pid + "&note=&type=cst&role=&pg=<%= Request.QueryString["pg"] %>";
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

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchScheduleDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchProfessionGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchEmployeeName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransactionDate" runat="server" Text="Application Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromTransactionDate" runat="server" Width="100px" />
                                        </td>
                                        <td>to &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToTransactionDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblScheduleDate" runat="server" Text="Schedule Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtFromScheduleDate" runat="server" Width="100px" />
                                        </td>
                                        <td>to &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtToScheduleDate" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchScheduleDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRProfessionGroup" runat="server" Text="Profession Group"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRProfessionGroup" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnSearchProfessionGroup" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPersonID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboPersonID_ItemDataBound"
                                    OnItemsRequested="cboPersonID_ItemsRequested">
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
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchEmployeeName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" Height="600px"
        AutoGenerateColumns="False" GridLines="None" OnItemCommand="grdList_OnItemCommand" OnItemDataBound="grdList_OnItemDataBound"
        AllowSorting="true">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="TransactionNo" ShowHeader="True" HierarchyDefaultExpanded="true">
            <NestedViewTemplate>
                <div style="padding-left: 20px; width: 98%">
                    <telerik:RadTabStrip ID="tabsCredentialProcess" runat="server" MultiPageID="mpCredentialProcess" ShowBaseLine="true"
                        Align="Left" PerTabScrolling="True" Width="100%"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Status" PageViewID="pgTimeStamp"
                                Selected="True" />
                            <telerik:RadTab runat="server" Text="Information" PageViewID="pgInfo" />
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="mpCredentialProcess" runat="server" SelectedIndex="0" ScrollBars="Auto" Width="100%"
                        CssClass="multiPage">
                        <telerik:RadPageView ID="pgTimeStamp" runat="server">
                            <telerik:RadGrid ID="grdListTimeStamp" runat="server"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="TransactionNo" ShowHeader="True">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="Disposition" HeaderText="Disposition">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsDisposition").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "DispositionDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Disposition\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsDisposition").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "DispositionBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="DocumentChecking" HeaderText="Document Checking">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsDocumentChecking").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "DocumentCheckingDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Document Checking\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsDocumentChecking").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "DocumentCheckingBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Scheduling" HeaderText="Scheduling">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsScheduling").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "SchedulingDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Scheduling\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsScheduling").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "SchedulingBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Invitation" HeaderText="Invitation">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsInvitation").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "InvitationDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Invitation\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsInvitation").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "InvitationBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CompetencyAssessment" HeaderText="Competency Assessment / Observation Instrument">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCompetencyAssessment").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "CompetencyAssessmentDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Competency Assessment / Observation Instrument\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCompetencyAssessment").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "CompetencyAssessmentBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Recommendation" HeaderText="Recommendation">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "RecommendationDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Recommendation\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "RecommendationBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="IsRecommendation1" HeaderText="Approval By Sub Committee">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation1").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "Recommendation1DateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Approval By Sub Committee\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation1").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "Recommendation1By")) : string.Empty)%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation1").Equals(true) ? string.Format("Result: <b>{0}</b>", DataBinder.Eval(Container.DataItem, "RecomendationResultBySubcommitte")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="IsRecommendation2" HeaderText="Approval By Committee">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation2").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "Recommendation2DateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Approval By Committee\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation2").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "Recommendation2By")) : string.Empty)%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation2").Equals(true) ? string.Format("Result: <b>{0}</b>", DataBinder.Eval(Container.DataItem, "RecomendationResultByCommitte")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="IsRecommendation3" HeaderText="Approval By Director">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation3").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "Recommendation3DateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/silang.jpg\" border=\"0\" title=\"Approval By Director\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation3").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "Recommendation3By")) : string.Empty)%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendation3").Equals(true) ? string.Format("Status: <b>{0}</b>", DataBinder.Eval(Container.DataItem, "ClinicalAppoinmentStatus")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="False">
                                    <Selecting AllowRowSelect="False" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pgInfo" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%; vertical-align: top">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblDispositionDate" runat="server" Text="Disposition Date"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtDispositionDate" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("DispositionDate")%>' />
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblDispositionNo" runat="server" Text="Disposition No"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtDispositionNo" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("DispositionNo")%>' />
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblDocumentStatus" runat="server" Text="Documents Status"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtDocumentStatus" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("DocumentStatus")%>' />
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%; vertical-align: top">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblScheduleDate" runat="server" Text="Schedule Date"></asp:Label>
                                                </td>
                                                <td class="entry" colspan="3">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtScheduleDate" runat="server" Width="200px" ReadOnly="true" Text='<%#Eval("ScheduleDate")%>' />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td class="label" style="width: 85px">
                                                                <asp:Label ID="Label3" runat="server" Text="Time"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtScheduleTimeFrom" runat="server" Width="50px" ReadOnly="true" Text='<%#Eval("ScheduleTimeFrom")%>' />
                                                            </td>
                                                            <td>&nbsp;To&nbsp;</td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtScheduleTimeTo" runat="server" Width="50px" ReadOnly="true" Text='<%#Eval("ScheduleTimeTo")%>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblRecommendationResultDate" runat="server" Text="Rec. By Sub Committee Date"></asp:Label>
                                                </td>
                                                <td class="entry">
                                                    <telerik:RadTextBox ID="txtRecommendationResultDate" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("RecommendationResultDate")%>' />
                                                </td>
                                                <td style="width: 20px"></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <asp:Label ID="lblConclusionDate" runat="server" Text="Rec. By Committee Date"></asp:Label>
                                                </td>
                                                <td class="entry" colspan="3">
                                                    <telerik:RadTextBox ID="txtConclusionDate" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("ConclusionDate")%>' />
                                                </td>
                                                <td style="width: 20px"></td>
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
                                                <asp:Label ID="Label1" runat="server" Text="CREDENTIAL SUB COMMITTEE"></asp:Label></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSRRecommendationResult" runat="server" Text="Result"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtRecommendationResultName" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("RecomendationResultBySubcommitte")%>' />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblRecommendationResultNotes" runat="server" Text="Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry" colspan="3">
                                                        <telerik:RadTextBox ID="txtRecommendationResultNotes" runat="server" Width="95%" TextMode="MultiLine" Height="80px" ReadOnly="true" Text='<%#Eval("RecommendationResultNotes")%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td width="50%" valign="top">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label4" runat="server" Text="COMMITTEE (MEDIC / NURSING / OTHER)"></asp:Label></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSRConclusionResult" runat="server" Text="Result"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtConclusionResultName" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("RecomendationResultByCommitte")%>' />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblConclusionNotes" runat="server" Text="Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry" colspan="3">
                                                        <telerik:RadTextBox ID="txtConclusionNotes" runat="server" Width="95%" TextMode="MultiLine" Height="80px" ReadOnly="true" Text='<%#Eval("ConclusionNotes")%>' />
                                                    </td>
                                                </tr>

                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="100%" valign="top">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label2" runat="server" Text="CLINICAL APPOINTMENT (DIRECTOR)"></asp:Label></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblClinicalAppoinmentNo" runat="server" Text="Clinical Appoinment No"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtClinicalAppoinmentNo" runat="server" Width="300px" MaxLength="50" ReadOnly="true" Text='<%#Eval("ClinicalAppoinmentNo")%>' />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblClinicalAppoinmentDateOfIssue" runat="server" Text="Date Of Issue"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtClinicalAppoinmentDateOfIssue" runat="server" Width="100px" ReadOnly="true" Text='<%#Eval("ClinicalAppoinmentDateOfIssue")%>' />

                                                                </td>
                                                                <td>&nbsp;Valid To&nbsp;&nbsp;</td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtClinicalAppoinmentValidTo" runat="server" Width="100px" ReadOnly="true" Text='<%#Eval("ClinicalAppoinmentValidTo")%>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSRClinicalAppoinmentStatus" runat="server" Text="Status"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtClinicalAppoinmentStatus" runat="server" Width="300px" ReadOnly="true" Text='<%#Eval("ClinicalAppoinmentStatus")%>' />
                                                    </td>
                                                    <td width="20"></td>
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblClinicalAppoinmentNotes" runat="server" Text="Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry" colspan="3">
                                                        <telerik:RadTextBox ID="txtClinicalAppoinmentNotes" runat="server" Width="43%" TextMode="MultiLine" Height="80px" ReadOnly="true" Text='<%#Eval("ClinicalAppoinmentNotes")%>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </NestedViewTemplate>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Application No"
                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TransactionDate" HeaderText="Date"
                    UniqueName="TransactionDate" SortExpression="TransactionDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ScheduleDate" HeaderText="Schedule Date"
                    UniqueName="ScheduleDate" SortExpression="ScheduleDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="ScheduleTimeText" HeaderText="Schedule Time"
                    UniqueName="ScheduleTimeText" SortExpression="ScheduleTimeText" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeNumber" HeaderText="Employee No"
                    UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="250px" DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                    SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ProfessionGroupName" HeaderText="Profession Group"
                    UniqueName="ProfessionGroupName" SortExpression="ProfessionGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ClinicalWorkAreaName" HeaderText="Work Area"
                    UniqueName="ClinicalWorkAreaName" SortExpression="ClinicalWorkAreaName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ClinicalAuthorityLevelName" HeaderText="Clinical Authority Level"
                    UniqueName="ClinicalAuthorityLevelName" SortExpression="ClinicalAuthorityLevelName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="center" UniqueName="DocumentUpload"
                    ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openReportsDocument('{0}'); return false;\"><img src=\"../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document\" /></a>", DataBinder.Eval(Container.DataItem, "TransactionNo")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
