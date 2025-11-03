<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="ScoringList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.ScoringList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            function gotoAddUrl(pid, eid) {
                var url = 'ScoringDetail.aspx?md=new&id=&pid=' + pid + '&eid=' + eid + '&type=' + '<%= Request.QueryString["type"] %>';
                window.location.href = url;
            }

            function gotoEditUrl(id, pid, eid) {
                var url = 'ScoringDetail.aspx?md=edit&id=' + id + '&pid=' + pid + '&eid=' + eid + '&type=' + '<%= Request.QueryString["type"] %>';
                window.location.href = url;
            }

            function gotoViewUrl(id, pid, eid) {
                var url = 'ScoringDetail.aspx?md=view&id=' + id + '&pid=' + pid + '&eid=' + eid + '&type=' + '<%= Request.QueryString["type"] %>';
                window.location.href = url;
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterSubjectName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterEvaluatorName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPeriodYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkIsApproved">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Subject Name</td>
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
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">Period Year</td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtPeriodYear" Width="100px" />
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterPeriodYear" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkIsApproved" Text="Approved" AutoPostBack="true" OnCheckedChanged="chkIsApproved_CheckedChanged" /></td>
                            <td width="20px" />
                            <td />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
        <MasterTableView DataKeyNames="ParticipantItemID, ScoresheetID" ClientDataKeyNames="ParticipantItemID, ScoresheetID" GroupLoadMode="Client">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) ? string.Empty : DataBinder.Eval(Container.DataItem, "IsNewRecord").Equals(true) ? 
                                string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ParticipantItemID"), DataBinder.Eval(Container.DataItem, "EvaluatorID")) :
                                string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ScoresheetID"), DataBinder.Eval(Container.DataItem, "ParticipantItemID"), DataBinder.Eval(Container.DataItem, "EvaluatorID")) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="PeriodYear" HeaderText="Period Year" UniqueName="PeriodYear" SortExpression="PeriodYear">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="ScoringDate" HeaderText="Scoring Date" UniqueName="ScoringDate"
                    SortExpression="ScoringDate">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="SubjectName" HeaderText="Subject Name" UniqueName="SubjectName" SortExpression="SubjectName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EvaluatorName" HeaderText="Evaluator Name" UniqueName="EvaluatorName" SortExpression="EvaluatorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
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
                <telerik:GridNumericColumn DataField="ScoreValue" HeaderText="Score" UniqueName="ScoreValue"
                    SortExpression="ScoreValue">
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
</asp:Content>
