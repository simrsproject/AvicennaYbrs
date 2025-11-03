<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="ScoringList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.Scoring.v2.ScoringList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            function gotoAddUrl(pid, eid, pd) {
                var url = 'ScoringDetail.aspx?md=new&id=&pid=' + pid + '&eid=' + eid + '&pd=' + pd + '&type=' + '<%= Request.QueryString["type"] %>';
                window.location.href = url;
            }
            function gotoAddUrl2(pid, eid, pd) {
                var url = 'ScoringDetail2.aspx?md=new&id=&pid=' + pid + '&eid=' + eid + '&pd=' + pd + '&type=' + '<%= Request.QueryString["type"] %>';
                window.location.href = url;
            }

            function gotoViewUrl(id, pid, eid, pd) {
                var url = 'ScoringDetail.aspx?md=view&id=' + id + '&pid=' + pid + '&eid=' + eid + '&pd=' + pd + '&type=' + '<%= Request.QueryString["type"] %>';
                window.location.href = url;
            }
            function gotoViewUrl2(id, pid, eid, pd) {
                var url = 'ScoringDetail2.aspx?md=view&id=' + id + '&pid=' + pid + '&eid=' + eid + '&pd=' + pd + '&type=' + '<%= Request.QueryString["type"] %>';
                window.location.href = url;
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterSubjectName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdListScoresheet" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEvaluatorName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdListScoresheet" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParticipantName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdListScoresheet" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPeriodYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdListScoresheet" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListScoresheet" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">Job Holder Name</td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboEmployeeID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboEmployeeID_ItemDataBound"
                                    OnItemsRequested="cboEmployeeID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>)</b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterSubjectName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Evaluator Name</td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboEvaluatorName" runat="server" Width="304px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboEmployeeID_ItemDataBound"
                                    OnItemsRequested="cboEmployeeID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "EmployeeNumber") %>&nbsp;-&nbsp;<b>(<%# DataBinder.Eval(Container.DataItem, "EmployeeName")%>)</b>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterEvaluatorName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">Participant Name</td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtParticipantName" Width="300px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterParticipantName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">Period Year</td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadTextBox runat="server" ID="txtPeriodYear" Width="80px" />
                                        </td>
                                        <td style="width: 2px"></td>
                                        <td class="label" style="width: 50px">Quarter</td>
                                        <td style="width: 2px"></td>
                                        <td>
                                            <telerik:RadComboBox ID="cboSRQuarterPeriod" runat="server" Width="156px" AllowCustomText="true"
                                                Filter="Contains" />
                                        </td>
                                    </tr>
                                </table>

                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterPeriodYear" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding" PageViewID="pgvOutstanding" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Scoresheet" PageViewID="pgvScoresheet">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvOutstanding" runat="server">
            <telerik:RadGrid ID="grdListOutstanding" runat="server" OnNeedDataSource="grdListOutstanding_NeedDataSource" GridLines="None"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
                <MasterTableView DataKeyNames="ParticipantItemID, EvaluatorID" ClientDataKeyNames="ParticipantItemID, EvaluatorID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="gotoAddUrl" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ParticipantItemID"), DataBinder.Eval(Container.DataItem, "EvaluatorID"), DataBinder.Eval(Container.DataItem, "ParticipantID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="gotoAddUrl2" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoAddUrl2('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ParticipantItemID"), DataBinder.Eval(Container.DataItem, "EvaluatorID"), DataBinder.Eval(Container.DataItem, "ParticipantID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParticipantName" HeaderText="Participant Name"
                            UniqueName="ParticipantName" SortExpression="ParticipantName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PeriodYear" HeaderText="Period Year" UniqueName="PeriodYear" SortExpression="PeriodYear">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="QuarterPeriod" HeaderText="Quarter"
                            UniqueName="QuarterPeriod" SortExpression="QuarterPeriod" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Job Holder Name" UniqueName="SubjectName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SubjectName") %><br />
                                (<%# DataBinder.Eval(Container.DataItem, "SubjectNumber") %>)
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="OrganizationUnitName" HeaderText="Service Unit" UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" UniqueName="PositionName" SortExpression="PositionName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Evaluator Name" UniqueName="EvaluatorName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EvaluatorName") %><br />
                                (<%# DataBinder.Eval(Container.DataItem, "EvaluatorNumber") %>)
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="EvaluatorType" HeaderText="Evaluator Type" UniqueName="EvaluatorType" SortExpression="EvaluatorType">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvScoresheet" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">Status</td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboStatus" Width="300px" />
                                    <td width="20px">
                                        <asp:ImageButton ID="btnFilterStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                    <td />
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdListScoresheet" runat="server" OnNeedDataSource="grdListScoresheet_NeedDataSource" GridLines="None"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
                <MasterTableView DataKeyNames="ScoresheetID, ParticipantItemID, EvaluatorID" ClientDataKeyNames="ScoresheetID, ParticipantItemID, EvaluatorID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="gotoViewUrl" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}', '{2}', '{3}'); return false;\"><img src=\"../../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ScoresheetID"), DataBinder.Eval(Container.DataItem, "ParticipantItemID"), DataBinder.Eval(Container.DataItem, "EvaluatorID"), DataBinder.Eval(Container.DataItem, "ParticipantID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="gotoViewUrl2" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl2('{0}','{1}', '{2}', '{3}'); return false;\"><img src=\"../../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ScoresheetID"), DataBinder.Eval(Container.DataItem, "ParticipantItemID"), DataBinder.Eval(Container.DataItem, "EvaluatorID"), DataBinder.Eval(Container.DataItem, "ParticipantID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ParticipantName" HeaderText="Participant Name"
                            UniqueName="ParticipantName" SortExpression="ParticipantName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="PeriodYear" HeaderText="Period Year" UniqueName="PeriodYear" SortExpression="PeriodYear">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="QuarterPeriod" HeaderText="Quarter"
                            UniqueName="QuarterPeriod" SortExpression="QuarterPeriod" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn DataField="ScoringDate" HeaderText="Scoring Date" UniqueName="ScoringDate"
                            SortExpression="ScoringDate">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Job Holder Name" UniqueName="SubjectName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "SubjectName") %><br />
                                (<%# DataBinder.Eval(Container.DataItem, "SubjectNumber") %>)
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="OrganizationUnitName" HeaderText="Service Unit" UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" UniqueName="PositionName" SortExpression="PositionName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" HeaderText="Evaluator Name" UniqueName="EvaluatorName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "EvaluatorName") %><br />
                                (<%# DataBinder.Eval(Container.DataItem, "EvaluatorNumber") %>)
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="EvaluatorType" HeaderText="Evaluator Type" UniqueName="EvaluatorType" SortExpression="EvaluatorType">
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn DataField="ApprovedDateTime" HeaderText="Approved Date" UniqueName="ApprovedDateTime"
                            SortExpression="ApprovedDateTime">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridNumericColumn DataField="TotalScore" HeaderText="Total Score" UniqueName="TotalScore"
                            SortExpression="TotalScore">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridNumericColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
