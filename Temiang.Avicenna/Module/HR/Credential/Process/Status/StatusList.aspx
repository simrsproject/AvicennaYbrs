<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="StatusList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Process.Status.StatusList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">
            function openReportsDocument(pid) {
                var url = '<%= Temiang.Avicenna.Common.Helper.UrlRoot() %>/Module/HR/Credential/Document/DocumentHist.aspx?pid=' + pid + "&note=&type=cst1&role=&pg=<%= Request.QueryString["pg"] %>";
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
                                <asp:Label ID="lblTransactionDate" runat="server" Text="Transaction Date"></asp:Label>
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
                                        <telerik:GridTemplateColumn UniqueName="SelfAssessment" HeaderText="Competency Assessment Application">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "ApprovedDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../../Images/silang.jpg\" border=\"0\" title=\"Competency Assessment Application\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "ApprovedBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CompetencyAssessmentProcess" HeaderText="Competency Assessment Process">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCompletelyVerified").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "LastVerifiedDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../../Images/silang.jpg\" border=\"0\" title=\"Competency Assessment Process\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCompletelyVerified").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "LastLastVerifiedBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CredentialApplication" HeaderText="Credentialing Application">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCredentialApplication").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "LastCredentialApplicationDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../../Images/silang.jpg\" border=\"0\" title=\"Credential / Re-Credential Application\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCredentialApplication").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "LastCredentialApplicationBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="CredentialProcess" HeaderText="Credentialing Process">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCredentialing").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "LastCredentialingDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../../Images/silang.jpg\" border=\"0\" title=\"Credential / Re-Credential Process\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsCredentialing").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "LastCredentialingBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="RecommendationLetter" HeaderText="Recommendation Letter">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendationLetter").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "LastRecommendationLetterDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../../Images/silang.jpg\" border=\"0\" title=\"Recommendation Letter\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsRecommendationLetter").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "LastRecommendationLetterBy")) : string.Empty)%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="190px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="ClinicalAssignmentLetter" HeaderText="Clinical Assignment Letter">
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "IsClinicalAssignmentLetter").Equals(true) ? string.Format("{0}", DataBinder.Eval(Container.DataItem, "LastClinicalAssignmentLetterDateTimes"))
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../../Images/silang.jpg\" border=\"0\" title=\"Clinical Assignment Letter\" /></a>"))%>
                                                <br />
                                                <%# (DataBinder.Eval(Container.DataItem, "IsClinicalAssignmentLetter").Equals(true) ? string.Format("By: {0}", DataBinder.Eval(Container.DataItem, "LastClinicalAssignmentLetterBy")) : string.Empty)%>
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
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="100%" valign="top">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label2" runat="server" Text="CLINICAL ASSIGNMENT LETTER"></asp:Label></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblClinicalAssignmentLetterDate" runat="server" Text="Assignment Letter Date" />
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtClinicalAssignmentLetterDate" runat="server" Width="100px" ReadOnly="true" Text='<%#Eval("ClinicalAssignmentLetterDate")%>' />
                                                    </td>
                                                    <td width="20">
                                                    </td>
                                                    <td />
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblDecreeNo" runat="server" Text="Decree No"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtDecreeNo" runat="server" Width="300px" MaxLength="50" ReadOnly="true" Text='<%#Eval("DecreeNo")%>' />
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
                                                                    <telerik:RadTextBox ID="txtValidFrom" runat="server" Width="100px" ReadOnly="true" Text='<%#Eval("ValidFrom")%>' />

                                                                </td>
                                                                <td>&nbsp;Valid To&nbsp;&nbsp;</td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtValidTo" runat="server" Width="100px" ReadOnly="true" Text='<%#Eval("ValidTo")%>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20"></td>
                                                    <td />
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
                        <%# string.Format("<a href=\"#\" onclick=\"openReportsDocument('{0}'); return false;\"><img src=\"../../../../../Images/doc_upload16.png\" border=\"0\" title=\"Document\" /></a>", DataBinder.Eval(Container.DataItem, "TransactionNo")) %>
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
