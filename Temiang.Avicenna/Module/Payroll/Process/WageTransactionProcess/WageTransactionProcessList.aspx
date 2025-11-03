<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="WageTransactionProcessList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Process.WageTransactionProcessList"
    Title="Untitled Page" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">

            function Reprocessing(periodId, msg) {
                if (confirm('Are you sure to ' + msg + ' this wage transaction?'))
                    __doPostBack("<%= grdList.UniqueID %>", "reprocessing|" + periodId + "|" + msg);
            }
	        
	        function Closing(periodId) {
		        if (confirm('Are you sure to close this wage transaction?'))
                    __doPostBack("<%= grdList.UniqueID %>", "closing|" + periodId);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSearchPayrollPeriod">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
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
                                <asp:Label ID="lblSearchPayrollPeriod" runat="server" Text="Payroll Period"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboPayrollPeriodID" runat="server" Width="304px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="False" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPayrollPeriodID_ItemDataBound"
                                    OnItemsRequested="cboPayrollPeriodID_ItemsRequested">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodCode")%>
                                        &nbsp;-&nbsp;
                                        <%# DataBinder.Eval(Container.DataItem, "PayrollPeriodName")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Note : Show max 12 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20px">
                                <asp:ImageButton ID="btnSearchPayrollPeriod" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="True" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="PayrollPeriodID" ClientDataKeyNames="PayrollPeriodID"
            GroupLoadMode="Client">
            <Columns>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# (this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) ? string.Empty
                            : string.Format("<a href=\"#\" onclick=\"Reprocessing('{0}','{1}'); return false;\"><b>{2}</b></a>",
                              DataBinder.Eval(Container.DataItem, "PayrollPeriodID"),
                              DataBinder.Eval(Container.DataItem, "IsProcessed").Equals(true) ? "Reprocessing" : "Process",                        
                              DataBinder.Eval(Container.DataItem, "IsProcessed").Equals(true) ? "<img src=\"../../../../Images/Toolbar/refresh16.png\" border=\"0\" title=\"Reprocessing\" />" : "<img src=\"../../../../Images/Toolbar/process16.png\" border=\"0\" title=\"Process\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PayrollPeriodID" HeaderText="Payroll Period ID"
                    UniqueName="PayrollPeriodID" SortExpression="PayrollPeriodID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn DataField="PayrollPeriodName" HeaderText="Payroll Period"
                    UniqueName="PayrollPeriodName" SortExpression="PayrollPeriodName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaySequentName" HeaderText="Pay Sequent"
                    UniqueName="PaySequentName" SortExpression="PaySequentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartDate" HeaderText="Start Date"
                    UniqueName="StartDate" SortExpression="StartDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="EndDate" HeaderText="End Date"
                    UniqueName="EndDate" SortExpression="EndDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PayDate" HeaderText="Pay Date"
                    UniqueName="PayDate" SortExpression="PayDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsProcessed" HeaderText="Processed"
                    UniqueName="IsProcessed" SortExpression="IsProcessed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# (this.IsUserApproveAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsClosed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsProcessed").Equals(false) ? string.Empty
                                : string.Format("<a href=\"#\" onclick=\"Closing('{0}'); return false;\"><b>{1}</b></a>",
                                   DataBinder.Eval(Container.DataItem, "PayrollPeriodID"), "<img src=\"../../../../Images/Toolbar/close.png\" border=\"0\" title=\"Closing\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
    </telerik:RadAjaxPanel>
</asp:Content>
