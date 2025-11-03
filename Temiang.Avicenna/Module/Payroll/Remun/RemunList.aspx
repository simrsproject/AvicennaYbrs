<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    Codebehind="RemunList.aspx.cs" Inherits="Temiang.Avicenna.Module.Payroll.Remun.RemunList"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function onClientTabSelected(sender, eventArgs) {
                var tabIndex = eventArgs.get_tab().get_index();
                switch (tabIndex) {
                    case 0:
                        __doPostBack("<%= grdList.UniqueID %>", "rebind");
                        break;
                }
            }
            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                NavigateToDetail(val,'');
            }
            function NavigateToDetail(command, RemunID) {
                var url = "RemunDetail.aspx?md=" + command + "&RemunID=" + RemunID + "";
                window.location.href = url;
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterMonth">
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
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New" Value="new" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtYear" Width="300px"></telerik:RadTextBox>
                        </td>
                        <td style="text-align: left;">
                            <asp:ImageButton ID="btnFilterYear" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboMonth" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterMonth" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="RemunID">
            <Columns>
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Period"
                    UniqueName="RemunID" SortExpression="RemunID" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" >
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"NavigateToDetail('{0}', '{1}'); return false;\">{2} - {3}</a>",
                            "view", DataBinder.Eval(Container.DataItem, "RemunID"), DataBinder.Eval(Container.DataItem, "PeriodYear").ToString(), 
                            DataBinder.Eval(Container.DataItem, "PeriodMonth").ToString())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="FundAllocPosition" HeaderText="Fund Position Allocation"
                    UniqueName="FundAllocPosition" SortExpression="FundAllocPosition" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="FundAllocInsetif" HeaderText="Fund Incentive Allocation"
                    UniqueName="FundAllocInsetif" SortExpression="FundAllocInsetif" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="KursPosition" HeaderText="Position Rate"
                    UniqueName="KursPosition" SortExpression="KursPosition" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="KursInsentif" HeaderText="Incentive Rate"
                    UniqueName="KursInsentif" SortExpression="KursInsentif" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>

                <telerik:GridCheckBoxColumn  HeaderStyle-Width="60px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left">

                </telerik:GridCheckBoxColumn>
            </Columns>
        </MasterTableView>
        <filtermenu>
        </filtermenu>
        <clientsettings enablerowhoverstyle="true">
            <resizing allowcolumnresize="True" />
            <selecting allowrowselect="True" />
        </clientsettings>
    </telerik:RadGrid>
</asp:Content>
