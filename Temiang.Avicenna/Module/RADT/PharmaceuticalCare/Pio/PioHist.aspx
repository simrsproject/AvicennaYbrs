<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="PioHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.PioHist" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript">
            function tbarMain_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case 'refresh':
                        var grd = $find('<%=grdList.ClientID %>').get_masterTableView();
                        grd.rebind();
                        break;
                    case 'add':
                        entryPio("new", 0);
                        break
                    case 'print':
                        break;
                }

            }

            function entryPio(mod, pioNo) {
                var grdid = "";
                grdid = "<%=grdList.ClientID %>";
                var url = "PioEntry.aspx?mod=" +
                    mod +
                    '&pn=' +
                    pioNo +
                    '&ccm=rebind&cet=' +
                    grdid;
                openWinEntryMaxHeight(url, 1000);
            }

            function openWinEntryMaxHeight(url, width) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);


                if (!(url.includes("&rt=") || url.includes("?rt=")))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                openWindow(url, width, height - 40);
            }
            function openWindow(url, width, height) {
                var oWnd;
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();
                oWnd.show();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }

            function winEntry_ClientClose(oWnd, args) {
                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {

                    if (arg.callbackMethod === 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {

                        if (arg.callbackMethod === 'rebind') {
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }

                        }
                    }
                }
            }

        </script>

    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="ajmProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btn4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="loadPanel" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindow ID="winEntry" Width="400px" Height="450px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="winEntry_ClientClose">
    </telerik:RadWindow>

    <telerik:RadToolBar ID="tbarMain" runat="server" Width="100%" OnClientButtonClicking="tbarMain_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Add" Value="add" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print" Value="print" ImageUrl="~/Images/Toolbar/print16.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
        </Items>
    </telerik:RadToolBar>

    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDate" runat="server" Width="100px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btn1" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Question
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtQuestion" runat="server" Width="300px" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btn2" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">Category
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRPioCategory" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btn3" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Source
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRPioSource" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="btn4" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>

            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="PioNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"javascript:entryPio('view', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/views16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "PioNo"),Helper.UrlRoot())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="PioDateTime" HeaderText="Date" SortExpression="PioDateTime">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "PioDateTime")).ToString(AppConstant.DisplayFormat.DateShortMonth) %><br />
                        <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "PioDateTime")).ToString(AppConstant.DisplayFormat.HourMin) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="QuestionerName" HeaderText="Questioner" SortExpression="QuestionerName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "QuestionerName") %><br />
                        <span style="font-size: smaller;"><%# DataBinder.Eval(Container.DataItem, "OccupationName") %></span>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Question" HeaderText="Question" UniqueName="Question" SortExpression="Question">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />

                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Information" HeaderText="Information" UniqueName="Information" SortExpression="Information">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Category" HeaderText="Category" HeaderStyle-Width="200px">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />

                    <ItemTemplate>
                        <%#CategoryLineHtml(Container) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Source" HeaderText="Source" HeaderStyle-Width="200px">
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemTemplate>
                        <%#SourceLineHtml(Container) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="CreatedByUserName" HeaderText="By">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreatedByUserName") %><br />
                        Lic:&nbsp;<%# DataBinder.Eval(Container.DataItem, "LicenseNo") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
