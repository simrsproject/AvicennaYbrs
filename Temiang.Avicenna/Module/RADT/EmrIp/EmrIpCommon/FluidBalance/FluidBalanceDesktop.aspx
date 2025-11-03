<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="FluidBalanceDesktop.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.FluidBalanceDesktop" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="FluidBalanceCtl.ascx" TagPrefix="uc1" TagName="FluidBalanceCtl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function tbarFluidBalance_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                var regno = $find("<%= fluidBalanceCtl.RegistrationNoClientID %>").get_value();
                if (regno === "")
                    regno = "<%=RegistrationNo%>";
                var seqno = $find("<%= fluidBalanceCtl.SequencegNoClientID %>").get_value();

                switch (val) {
                case 'refresh':
                    var grd = $find('<%=fluidBalanceCtl.GridFluidBalanceClientID %>').get_masterTableView();
                    grd.rebind();
                    break;
                case 'addheader':
                    // Add Header ikut Registrasi saat dipilih di page List nya
                    entryMonitoring("new", "<%=RegistrationNo%>", 0);
                    break;
                case 'editheader':
                    if (seqno === '' || seqno === '0') {
                        alert("Please select Registered Schema Infus first");
                        return false;
                    }
                    entryMonitoring("edit", regno, seqno);
                    break;
                    case 'adddetail':
                        if (seqno === '' || seqno === '0') {
                            alert("Please select or create New Schema Infus first");
                            return false;
                        }
                    entryMonitoringDetail("new", regno, seqno, 0);
                    break;
                case 'history':
                    openHistory(regno, seqno);
                    break;
                case 'print':
                    var obj = {};
                    obj.registrationNo = regno;
                    obj.sequenceNo = seqno;
                    obj.userName = "<%=AppSession.UserLogin.UserName%>";
                    openPrintPreview("PopulatePrintParameterFluidBalance", obj);
                    break;
                case 'close':
                    Close();
                    break;
                }
                return false;
            }

            function entryMonitoring(mod, regno, seqno) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/FluidBalance/FluidBalanceEntry.aspx?mod=' +
                        mod +
                        '&patid=<%= PatientID %>&regno=' +
                        regno +
                        '&seqno=' +
                        seqno +
                        '&ccm=rebind&cet=<%=fluidBalanceCtl.GridFluidBalanceClientID %>';
                openWinEntry(url, 600, 450,"winHeader");
            }

            function entryMonitoringDetail(mod, regno, seqno, dseqno) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/FluidBalance/FluidBalanceDetailEntry.aspx?mod=' +
                        mod +
                        '&patid=<%= PatientID %>&regno=' +
                        regno +
                        '&seqno=' +
                        seqno +
                        '&dseqno=' +
                        dseqno +
                        '&ccm=rebind&cet=<%=fluidBalanceCtl.GridFluidBalanceClientID %>';
                openWinEntry(url, 600, 450,"winDetail");
            }

            function openWinEntryMaxWindow(url, windowID) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                openWinEntry(url, width - 40, height - 40, windowID);
            }

            function openWinEntry(url, width, height, windowID) {
                if (!(url.includes("&rt=") || url.includes("?rt=")))	
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                var oWnd = radopen(url, windowID);
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y<0)
                    oWnd.moveTo(pos.x, 0);
            }

            function openHistory(regno, seqno) {
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/FluidBalance/FluidBalanceHist.aspx?regno='+regno+'&seqno='+seqno+'&patid=<%= PatientID %>&ccm=rebind&cet=<%=fluidBalanceCtl.GridFluidBalanceClientID %>';
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWinEntryMaxWindow(url,'winHeader');
            }

            function winHeader_ClientClose(oWnd, args) {
                //get the transferred arguments from History Page
                var arg = args.get_argument();
                if (arg != null) {

                    if (arg.callbackMethod === 'rebind') {
                        $find("<%= fluidBalanceCtl.RegistrationNoClientID %>").set_value(arg.editRegNo);
                        $find("<%= fluidBalanceCtl.SequencegNoClientID %>").set_value(arg.editSeqNo);
                        var masterTable = $find(arg.eventTarget).get_masterTableView();
                        masterTable.rebind();
                    }
                }

            }
            function winDetail_ClientClose(oWnd, args) {
                //get the transferred arguments from History Page
                var arg = args.get_argument();
                if (arg != null) {

                    if (arg.callbackMethod === 'rebind') {
                        var masterTable = $find(arg.eventTarget).get_masterTableView();
                        masterTable.rebind();
                    }
                }
            }

            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                // set height to the whole RadGrid control
                var grid = $find("<%= fluidBalanceCtl.GridFluidBalanceClientID %>");
                grid.get_element().style.height = height - 110 + "px";
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
            });
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdHeader">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="fluidBalanceCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="tbarFluidBalance">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdHeader" />
                    <telerik:AjaxUpdatedControl ControlID="fluidBalanceCtl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
                              Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
                              ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winDetail" Width="900px" Height="600px" runat="server"
                               ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="winDetail_ClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winHeader" Width="900px" Height="600px" runat="server"
                               Behaviors="Close,Move" Modal="True" OnClientClose="winHeader_ClientClose">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadToolBar ID="tbarFluidBalance" runat="server" Width="100%" EnableEmbeddedScripts="false"
        OnClientButtonClicking="tbarFluidBalance_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New Schema Infus" Value="addheader" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Add In / Out" Value="adddetail" ImageUrl="~/Images/Toolbar/insert16.png"
                HoveredImageUrl="~/Images/Toolbar/insert16_h.png" DisabledImageUrl="~/Images/Toolbar/insert16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="History" Value="history"
                ImageUrl="~/Images/Toolbar/print_preview16.png" />
            <telerik:RadToolBarButton runat="server" Text="Edit Schema Infus" Value="editheader" ImageUrl="~/Images/Toolbar/edit16.png"
                                      HoveredImageUrl="~/Images/Toolbar/edit16_h.png" DisabledImageUrl="~/Images/Toolbar/edit16_d.png" />

            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
            <telerik:RadToolBarButton runat="server" Text="Close" Value="close" ImageUrl="~/Images/Toolbar/close16.png" />
        </Items>
    </telerik:RadToolBar>
    <uc1:FluidBalanceCtl runat="server" ID="fluidBalanceCtl" GridHeight="490" />
</asp:Content>
