<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="SanitationMaintenanceActivityProcessList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Management.SanitationMaintenanceActivityProcessList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "generate":
                        if (confirm('Are you sure to generate Sanitation Maintenance Activity for the selected schedule?'))
                            __doPostBack("<%= grdList.UniqueID %>", 'generate');
                        break;
                }
            }

            function OnClientButtonClicking2(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "approved":
                        if (confirm('Are you sure want to process for the selected data?'))
                            __doPostBack("<%= grdList.UniqueID %>", 'approved');
                        break;
                }
            }

            function rowApproved(pmNo) {
                if (confirm('Are you sure to approval for the selected data?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'approved|' + pmNo);
                }
            }

            function rowVoid(pmNo) {
                if (confirm('Are you sure to void for the selected data?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'void|' + pmNo);
                }
            }

            function rowPrint(pmNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'print|' + pmNo);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListTaskList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListTaskList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSRWorkTradeItem">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListTaskList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListTaskList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTargetDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListTaskList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="grdListTaskList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                    <telerik:AjaxUpdatedControl ControlID="lblInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdListTaskList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdListTaskList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDate" runat="server" Text="Period"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="cboPeriodMonth" runat="server" Width="104px" />
                                        </td>
                                        <td>&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" MaxLength="4" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnitID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRWorkTradeItem" runat="server" Text="Work Trade Item"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboSRWorkTradeItem" runat="server" Width="300px"
                                    AllowCustomText="true" Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterSRWorkTradeItem" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Schedule" PageViewID="pgSchedule" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Task List" PageViewID="pgTaskList">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgSchedule" runat="server" Selected="true">
            <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
                <Items>
                    <telerik:RadToolBarButton runat="server" Text="Generate Sanitation Maintenance Activity" Value="generate"
                        ImageUrl="~/Images/Toolbar/process16.png" HoveredImageUrl="~/Images/Toolbar/process16_h.png"
                        DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
                </Items>
            </telerik:RadToolBar>
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
                EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
                <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AllowPaging="False" AllowSorting="true"
                    ShowStatusBar="true">
                    <MasterTableView DataKeyNames="SRWorkTradeItem,ServiceUnitID,ScheduleDate" ClientDataKeyNames="SRWorkTradeItem,ServiceUnitID,ScheduleDate"
                        AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                                        runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="detailChkbox" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ScheduleDate" HeaderText="Scheduled Date" UniqueName="ScheduleDate"
                                SortExpression="ScheduleDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SRWorkTradeItem" HeaderText="SRWorkTradeItem" UniqueName="SRWorkTradeItem"
                                SortExpression="SRWorkTradeItem" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                Visible="False" />
                            <telerik:GridBoundColumn DataField="WorkTradeItemName" HeaderText="Work Trade Item" UniqueName="WorkTradeItemName"
                                SortExpression="WorkTradeItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="ServiceUnitID" HeaderText="ServiceUnitID" UniqueName="ServiceUnitID"
                                SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                Visible="False" />
                            <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgTaskList" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTransactionDate" runat="server" Text="Transcation Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterTransactionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterPm_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblTargetDate" runat="server" Text="Target Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtTargetDate" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterTargetDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilterPm_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking2">
                <Items>
                    <telerik:RadToolBarButton runat="server" Text="Approval Process" Value="approved"
                        ImageUrl="~/Images/Toolbar/post16.png" HoveredImageUrl="~/Images/Toolbar/post16_h.png"
                        DisabledImageUrl="~/Images/Toolbar/post16_d.png" />
                </Items>
            </telerik:RadToolBar>
            <telerik:RadGrid ID="grdListTaskList" runat="server" OnNeedDataSource="grdListTaskList_NeedDataSource"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="TransactionNo">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedStateApproved" AutoPostBack="True"
                                    runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="detailChkbox" runat="server" Enabled='<%# (bool)DataBinder.Eval(Container.DataItem, "IsEnabled") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Transaction No" UniqueName="TransactionNo"
                            SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="TransactionDate" HeaderText="Date" UniqueName="TransactionDate"
                            SortExpression="TransactionDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WorkTradeItemName" HeaderText="Work Trade Item" UniqueName="WorkTradeItemName"
                            SortExpression="WorkTradeItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="TargetDate" HeaderText="Target Date" UniqueName="TargetDate"
                            SortExpression="TargetDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="IsApprovedCheckBoxTemplate" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <span>Approved</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="approvedChkbox" runat="server" Checked='<%# (bool)DataBinder.Eval(Container.DataItem, "IsApproved") %>'
                                    Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="IsVoidCheckBoxTemplate" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <span>Void</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="voidChkbox" runat="server" Checked='<%# (bool)DataBinder.Eval(Container.DataItem, "IsVoid") %>'
                                    Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <%#(DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true)) ? string.Empty
                                     : string.Format("<a href=\"#\" onclick=\"rowVoid('{0}'); return false;\"><img src=\"../../../../Images/cancel16.png\" border=\"0\" alt=\"Void\" title=\"Void\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "IsApproved").Equals(false) ? string.Empty
                                     : string.Format("<a href=\"#\" onclick=\"rowPrint('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print\" /></a>",
                                                                            DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
