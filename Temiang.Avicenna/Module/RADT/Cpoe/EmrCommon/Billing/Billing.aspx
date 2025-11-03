<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="Billing.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.Billing" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function openWinEntryNoTitleBarMaximize(url) {
                var oWnd;
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                oWnd = radopen(url, 'winNoTitlebar');
                oWnd.maximize();
            }
            function entryServiceUnitTrans(mod, trno, smf) {
                var url = '<%= Helper.UrlRoot() %>/Module/Charges/ServiceUnit/ServiceUnitTransaction/ServiceUnitTransactionDetail.aspx?emr=1&md=' + mod + '&type=tr&resp=0&disch=0&id=' + trno + '&smf=<%= RegistrationCurrent.SmfID %>&regno=<%= RegistrationNo %>&pid=<%= ParamedicID %>&cid=<%= ServiceUnitID %>&roomid=<%= RoomID %>&verif=0&ccm=rebind&cet=<%=grdServiceUnitTrans.ClientID %>';
                openWinEntryNoTitleBarMaximize(url);
            }
            function tbarServiceUnitTrans_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                if (val === 'refresh') {
                    var grd = $find('<%=grdServiceUnitTrans.ClientID %>').get_masterTableView();
                    grd.rebind();
                } else if (val === 'add') {
                    entryServiceUnitTrans('new', '');
                    args.set_cancel(true);
                } else if (val === 'close') {
                    Close();
                    return false;
                }
            }

            function radWindowManager_ClientClose(oWnd, args) {
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

            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                // set height to the whole RadGrid control
                var grid = $find("<%= grdServiceUnitTrans.ClientID %>");
                grid.get_element().style.height = height - 50 + "px";
                grid.repaint();
            }
            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winNoTitlebar" runat="server" Behaviors="None" VisibleTitlebar="False"
                               ShowContentDuringLoad="false" Modal="True">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadToolBar ID="tbarServiceUnitTrans" runat="server" Width="100%" EnableEmbeddedScripts="false"
        OnClientButtonClicking="tbarServiceUnitTrans_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Add" Value="add" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
            <telerik:RadToolBarButton runat="server" Text="Close" Value="close" ImageUrl="~/Images/Toolbar/close16.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadGrid ID="grdServiceUnitTrans" runat="server" OnNeedDataSource="grdServiceUnitTrans_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None"  Height="560px" Width="100%" BorderStyle="None"
        OnItemCommand="grdServiceUnitTrans_ItemCommand">
        <MasterTableView DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%# (!IsUserEditAble || Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate"))) || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) ? "<img src=\"../../../../../Images/Toolbar/edit16_d.png\"  alt=\"Edit\" />" : string.Format("<a href=\"#\" onclick=\"javascript:entryServiceUnitTrans('edit', '{0}'); return false;\"><img src=\"../../../../../Images/Toolbar/edit16.png\"  alt=\"Edit\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="Tr. No" HeaderStyle-Width="110px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"javascript:entryServiceUnitTrans('view', '{0}'); return false;\">{0}</a>",DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="TransactionNo" UniqueName="TransactionNo" HeaderText="Transaction No"
                    HeaderStyle-Width="120px" ItemStyle-VerticalAlign="Top" Display="False" />
                <telerik:GridDateTimeColumn DataField="TransactionDate" UniqueName="TransactionDate"
                    HeaderText="Date" HeaderStyle-Width="80px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridBoundColumn DataField="ServiceUnitID" UniqueName="ServiceUnitID"
                    HeaderText="Service Unit ID" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top" Display="False" />

                <telerik:GridBoundColumn DataField="ServiceUnitName" UniqueName="ServiceUnitName"
                    HeaderText="Service Unit" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "ServiceUnitTransSummary")%>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="UserName" UniqueName="UserName"
                                         HeaderText="Created By" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridCheckBoxColumn DataField="IsApprove" UniqueName="IsApprove" HeaderText="Appr"
                    HeaderStyle-Width="50px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridTemplateColumn/>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
