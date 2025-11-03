<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    Codebehind="RemunList.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.ParamedicFee.RemunList"
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
                switch (val) {
                    case "newBpjs": {
                        NavigateToDetailBpjs('new', '');
                        break;
                    }
                    case "newNonBpjs": {
                        NavigateToDetailNonBpjs('new', '');
                        break;
                    }
                }
            }
            function NavigateToDetailBpjs(command, remunNo) {
                var url = "RemunDetail.aspx?md=" + command + "&RemunNo=" + remunNo + "";
                window.location.href = url;
            }
            function NavigateToDetailNonBpjs(command, remunNo) {
                var url = "RemunDetailNonBPJS.aspx?md=" + command + "&RemunNo=" + remunNo + "";
                window.location.href = url;
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRemunNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
            <telerik:RadToolBarButton runat="server" Text="New BPJS" Value="newBpjs" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="New Non BPJS" Value="newNonBpjs" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTransactionNo" runat="server" Text="Remun No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRemunNo" runat="server" Width="300px" MaxLength="20" />
                        </td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="btnFilterRemunNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnFilter_Click" ToolTip="Search" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboYear" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
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
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Remun No"
                    UniqueName="RemunNo" SortExpression="RemunNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Left" >
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"NavigateToDetail{2}('{0}', '{1}'); return false;\">{1}</a>",
                            "view", 
                            DataBinder.Eval(Container.DataItem, "RemunNo"), 
                            ((bool)DataBinder.Eval(Container.DataItem, "IsBPJS")) ? "Bpjs":"NonBpjs")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="PeriodYear"
                    HeaderText="Year" UniqueName="PeriodYear" SortExpression="PeriodYear"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="PeriodMonth"
                    HeaderText="Month" UniqueName="PeriodMonth" SortExpression="PeriodMonth"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                
                <telerik:GridTemplateColumn HeaderStyle-Width="80px" HeaderText="Type" UniqueName="IsBPJS" SortExpression="IsBPJS"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                    <ItemTemplate>
                        <%# string.Format("{0}", ((bool)DataBinder.Eval(Container.DataItem, "IsBPJS")) ? "BPJS":"Non BPJS")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridDateTimeColumn DataField="Notes"
                    HeaderText="Notes" UniqueName="Notes" SortExpression="Notes"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
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
