<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NosocomialMain.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialMain" %>

<%@ Register TagPrefix="cc" TagName="NosocomialCtl" Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/NosocomialCtl.ascx" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <%=JavascriptOpenPrintPreview()%>

        <script type="text/javascript" language="javascript">

            function tbarNosocomial_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                var regno = $find("<%= nosocomialCtl.RegistrationNoClientID %>").get_value();
                if (regno === "")
                    regno = "<%=RegistrationNo%>";
                var monno = $find("<%= nosocomialCtl.MonitoringNoClientID %>").get_value();
                var montype = "<%=MonitoringType%>";

                switch (val) {
                    case 'refresh':
                        var grd = $find('<%=nosocomialCtl.GridClientID %>').get_masterTableView();
                        grd.rebind();
                        break;
                    case 'addheader':
                        // Add Header ikut Registrasi saat dipilih di page List nya
                        entryNosocomial("new", montype, "<%=RegistrationNo%>", 0);
                        break;
                    case 'editheader':
                        if (monno === '' || monno === '0') {
                            alert("No nosocomial monitoring to edit");
                            return false;
                        }
                        entryNosocomial("edit", montype, regno, monno);
                        break;
                    case 'adddetail':
                        entryNosocomialDetail("new", montype, regno, monno, 0);
                        break;
                    case 'history':
                        openHistory(montype, "<%=RegistrationNo%>", regno, monno);
                        break;
                    case 'print':
                        var obj = {};
                        obj.registrationNo = regno;
                        obj.monitoringNo = monno;
                        obj.userName = "<%=AppSession.UserLogin.UserName%>";
                        openPrintPreview("PopulatePrintParameterNosocomial", obj);
                        break;
                    case 'close':
                        Close();
                        break;
                }
                return false;
            }

            function entryNosocomial(mod, montype, regno, monno) {
                var url = "<%=nosocomialCtl.UrlHeaderEntry %>";
            var grdid = "<%=nosocomialCtl.GridClientID %>";

            if (url.indexOf("?") > -1)
                url = url + '&';
            else
                url = url + '?';

            url = url +
                'mod=' +
                mod +
                    '&patid=<%= PatientID %>&regno='+regno+'&monno=' +
                                monno +
                                '&ccm=rebind&cet=' +
                                grdid;

                openWinEntry(url, 600, 500,'winHeader');
            }


            function entryNosocomialDetail(mod, montype, regno, monno, seqno) {
                var url = "<%=nosocomialCtl.UrlDetailEntry %>";
                var grdid = "<%=nosocomialCtl.GridClientID %>";

                if (monno === '' || monno === '0') {
                    alert("Please create " + montype + " monitoring installation first");
                    return;
                }

                if (url.indexOf("?") > -1)
                    url = url + '&';
                else
                    url = url + '?';

                url = url +
                    'mod=' +
                    mod +
                    '&patid=<%= PatientID %>&regno='+regno+'&monno=' +
                    monno +
                    '&seqno=' +
                    seqno +
                    '&ccm=rebind&cet=' +
                    grdid;

                openWinEntry(url, 600, <%= nosocomialCtl.HeightWindowDetailEntry %>,'winDetail');
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

            function openHistory(montype,regno,regnoedt, monno) {
                var grdid = "<%=nosocomialCtl.GridClientID %>";
                var url =
                    '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialHist.aspx?regno='+regno+'&regnoedt='+regnoedt+'&monno='+monno+'&patid=<%= PatientID %>&montype=' + montype + '&ccm=rebind&cet=' +grdid;

                openWinEntryMaxWindow(url,"winHeader");
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

            function winHeader_ClientClose(oWnd, args) {
                //get the transferred arguments from History Page
                var arg = args.get_argument();
                if (arg != null) {

                    if (arg.callbackMethod === 'rebind') {
                        $find("<%= nosocomialCtl.RegistrationNoClientID %>").set_value(arg.editRegNo);
                        $find("<%= nosocomialCtl.MonitoringNoClientID %>").set_value(arg.editMonNo);
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
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) ;

                // set height to the whole RadGrid control
                var grid = $find("<%= nosocomialCtl.GridClientID %>");
                grid.get_element().style.height = height - 150 + "px";
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
            <telerik:AjaxSetting AjaxControlID="tbarMedication">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="medicationCtl" />
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

    <telerik:RadToolBar ID="tbarNosocomial" runat="server" Width="100%" EnableEmbeddedScripts="false" OnClientButtonClicking="tbarNosocomial_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New Installation" Value="addheader" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Edit Installation" Value="editheader" ImageUrl="~/Images/Toolbar/edit16.png"
                HoveredImageUrl="~/Images/Toolbar/edit16_h.png" DisabledImageUrl="~/Images/Toolbar/edit16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Add Day Monitoring" Value="adddetail" ImageUrl="~/Images/Toolbar/insert16.png"
                HoveredImageUrl="~/Images/Toolbar/insert16_h.png" DisabledImageUrl="~/Images/Toolbar/insert16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="History" Value="history" ImageUrl="~/Images/Toolbar/print_preview16.png" />
            <telerik:RadToolBarButton runat="server" Text="Print" Value="print" ImageUrl="~/Images/Toolbar/print16.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
            <telerik:RadToolBarButton runat="server" Text="Close" Value="close" ImageUrl="~/Images/Toolbar/close16.png" />
        </Items>
    </telerik:RadToolBar>
    <asp:Panel runat="server" ID="pnlNosocomial"></asp:Panel>
</asp:Content>
