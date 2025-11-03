<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="EmployeeTrainingEvaluationList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingEvaluationList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoViewUrl(mode, id) {
                var url = 'EmployeeTrainingEvaluationDetail.aspx?md=' + mode + '&id=' + id;
                window.location.href = url;
            }
            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
                switch (tabIndex) {
                    case 0:
                        __doPostBack("<%= grdList.UniqueID %>", "rebind");
                        break;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchPeriod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchEmployee">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListOutstanding">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListOutstanding" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPeriod" runat="server" Text="Evaluation Period"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDateStart" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDateEnd" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchPeriod" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPersonID" runat="server" Text="Employee Name"></asp:Label>
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
                                <asp:ImageButton ID="btnSearchEmployee" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop" OnClientTabSelected="onClientTabSelected">
        <Tabs>
            <telerik:RadTab runat="server" Text="Outstanding List" PageViewID="pgOutstanding"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Employee Training Evaluation" PageViewID="pgEmployeeTraining" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <telerik:RadGrid ID="grdListOutstanding" runat="server" OnNeedDataSource="grdListOutstanding_NeedDataSource" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="EmployeeTrainingHistoryID">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('edit', '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "EmployeeTrainingHistoryID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EvaluationDate" HeaderText="Evaluation Date (Est.)"
                            UniqueName="EvaluationDate" SortExpression="EvaluationDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="OrganizationUnitName" HeaderText="Department"
                            UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Division" HeaderText="Division"
                            UniqueName="Division" SortExpression="Division" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Section / Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="PositionName" HeaderText="Position" UniqueName="PositionName"
                            SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="SREmployeeTrainingDateSeparator" HeaderText=""
                            UniqueName="SREmployeeTrainingDateSeparator" SortExpression="SREmployeeTrainingDateSeparator" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="TrainingLocation" HeaderText="Training Location"
                            UniqueName="TrainingLocation" SortExpression="TrainingLocation" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EventName" HeaderText="Training Name"
                            UniqueName="EventName" SortExpression="EventName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ActivityTypeName" HeaderText="Training Type"
                            UniqueName="ActivityTypeName" SortExpression="ActivityTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TrainingInstitution"
                            HeaderText="Training Organizer" UniqueName="TrainingInstitution" SortExpression="TrainingInstitution"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgEmployeeTraining" runat="server">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="EmployeeTrainingHistoryID">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="View" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('view', '{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "EmployeeTrainingHistoryID")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EvaluationDate" HeaderText="Evaluation Date"
                            UniqueName="EvaluationDate" SortExpression="EvaluationDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="130px" DataField="EmployeeNumber" HeaderText="Employee No"
                            UniqueName="EmployeeNumber" SortExpression="EmployeeNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="OrganizationUnitName" HeaderText="Department"
                            UniqueName="OrganizationUnitName" SortExpression="OrganizationUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Division" HeaderText="Division"
                            UniqueName="Division" SortExpression="Division" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ServiceUnitName" HeaderText="Section / Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="PositionName" HeaderText="Position" UniqueName="PositionName"
                            SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                            UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="20px" DataField="SREmployeeTrainingDateSeparator" HeaderText=""
                            UniqueName="SREmployeeTrainingDateSeparator" SortExpression="SREmployeeTrainingDateSeparator" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                            UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="TrainingLocation" HeaderText="Training Location"
                            UniqueName="TrainingLocation" SortExpression="TrainingLocation" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="EventName" HeaderText="Training Name"
                            UniqueName="EventName" SortExpression="EventName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ActivityTypeName" HeaderText="Training Type"
                            UniqueName="ActivityTypeName" SortExpression="ActivityTypeName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TrainingInstitution"
                            HeaderText="Training Organizer" UniqueName="TrainingInstitution" SortExpression="TrainingInstitution"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
