<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="RecapitulationList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.Recapitulation.RecapitulationList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">

            function gotoViewUrl(pid) {
                var url = 'RecapitulationDetail.aspx?md=view&pid=' + pid + "&role=" + '<%= Request.QueryString["role"] %>';
                window.location.href = url;
            }

            function goPrint(pid) {
                __doPostBack("<%= grdList.UniqueID %>", "print|" + pid);
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
            <telerik:AjaxSetting AjaxControlID="btnFilterStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParticipantName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPeriodYear">
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
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
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
                            <td class="label">Status</td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="true">All</asp:ListItem>
                                    <asp:ListItem>New</asp:ListItem>
                                    <asp:ListItem>View</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnFilterStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" GridLines="None"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
        <MasterTableView DataKeyNames="ParticipantItemID" ClientDataKeyNames="ParticipantItemID" GroupLoadMode="Client">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "IsScoringRecapitulation").Equals(false) ? string.Format("<a href=\"#\" onclick=\"goPrint('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16_h.png\" border=\"0\" title=\"Print\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ParticipantItemID")) : (DataBinder.Eval(Container.DataItem, "IsNewRecord").Equals(true) ? 
                                string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ParticipantItemID")) :
                                string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                DataBinder.Eval(Container.DataItem, "ParticipantItemID"))) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ParticipantName" HeaderText="Participant Name" UniqueName="ParticipantName" SortExpression="ParticipantName">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PeriodYear" HeaderText="Period Year" UniqueName="PeriodYear" SortExpression="PeriodYear">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes" SortExpression="Notes">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="300px" HeaderText="Job Holder Name" UniqueName="SubjectName"
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
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsClosed" HeaderText="Approved"
                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
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
