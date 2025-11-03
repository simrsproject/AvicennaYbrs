<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NosocomialDesktop.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialDesktop" %>

<%@ Register TagPrefix="cc" TagName="NosocomialCtl" Src="~/Module/RADT/EmrIp/EmrIpCommon/MainContent/Nosocomial/NosocomialCtl.ascx" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <%=JavascriptOpenPrintPreview()%>

        <script type="text/javascript" language="javascript">

            function tbarNosocomial_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                var tabStrip = $find("<%=nosocomialCtl.TabStripClientID%>");
                var montype = tabStrip.get_selectedTab().get_value();
                var monno = 0;
                switch (montype) {
                case "infus":
                    monno = $find("<%= nosocomialCtl.InfusMonitoringNoClientID %>").get_value();
                    break;
                case "ngt":
                    monno = $find("<%= nosocomialCtl.NgtMonitoringNoClientID %>").get_value();
                    break;
                case "catheter":
                    monno = $find("<%= nosocomialCtl.CatheterMonitoringNoClientID %>").get_value();
                    break;
                case "surgery":
                    monno = $find("<%= nosocomialCtl.SurgeryMonitoringNoClientID %>").get_value();
                    break;
                case "ett":
                    monno = $find("<%= nosocomialCtl.EttMonitoringNoClientID %>").get_value();
                    break;
                case "bedrest":
                    monno = $find("<%= nosocomialCtl.BedRestMonitoringNoClientID %>").get_value();
                    break;
                }


                if (val === 'refresh') {
                    var grd = $find('<%=nosocomialCtl.InfusMonitoringGridClientID %>').get_masterTableView();
                    switch (montype) {
                    case "ngt":
                        grd = $find("<%= nosocomialCtl.NgtMonitoringGridClientID %>").get_masterTableView();
                        break;
                    case "catheter":
                        grd = $find("<%= nosocomialCtl.CatheterMonitoringGridClientID %>").get_masterTableView();
                        break;
                    case "surgery":
                        grd = $find("<%= nosocomialCtl.SurgeryMonitoringGridClientID %>").get_masterTableView();
                        break;
                    case "ett":
                        grd = $find("<%= nosocomialCtl.EttMonitoringGridClientID %>").get_masterTableView();
                        break;
                        case "bedrest":
                        grd = $find("<%= nosocomialCtl.BedRestMonitoringGridClientID %>").get_masterTableView();
                        break;
                    }

                    grd.rebind();
                } else if (val === 'addheader') {
                    entryNosocomial("new", montype, 0);
                } else if (val === 'editheader') {
                    if (monno === '' || monno === '0') {
                        alert("No nosocomial monitoring " + montype + " to edit");
                        return;
                    }
                    entryNosocomial("edit", montype, monno);
                } else if (val === 'adddetail') {
                    entryNosocomialDetail("new", montype, monno, 0);
                } else if (val === 'history') {
                    switch (montype) {
                    case "infus":
                        grdid = "<%=nosocomialCtl.InfusMonitoringGridClientID %>";
                        break;
                    case "ngt":
                        grdid = "<%=nosocomialCtl.NgtMonitoringGridClientID %>";
                        break;
                    case "catheter":
                        grdid = "<%=nosocomialCtl.CatheterMonitoringGridClientID %>";
                        break;
                    case "surgery":
                        grdid = "<%=nosocomialCtl.SurgeryMonitoringGridClientID %>";
                        break;
                    case "ett":
                        grdid = "<%=nosocomialCtl.EttMonitoringGridClientID %>";
                        break;
                    case "bedrest":
                        grdid = "<%=nosocomialCtl.BedRestMonitoringGridClientID %>";
                        break;
                    }
                    var url =
                        '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialHist.aspx?regno=<%=RegistrationNo %>&patid=<%= PatientID %>&montype=' + montype + '&ccm=rebind&cet=' +grdid;
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';
                    openWinEntry(url, 1280, 780);
                } else if (val === 'print') {
                    var obj = {};
                    obj.registrationNo = "<%=RegistrationNo%>";
                    obj.monitoringNo = monno;

                    openPrintPreview("PopulatePrintParameterNosocomial", obj);
                }
            }

            function entryNosocomial(mod, montype, monno) {
                var grdid = "";
                var url = "";
                switch (montype) {
                case "infus":
                    url = "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialInfusEntry.aspx";
                    grdid = "<%=nosocomialCtl.InfusMonitoringGridClientID %>";
                    break;
                case "ngt":
                    url = "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialNgtEntry.aspx";
                    grdid = "<%=nosocomialCtl.NgtMonitoringGridClientID %>";
                    break;
                case "catheter":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialCatheterEntry.aspx";
                    grdid = "<%=nosocomialCtl.CatheterMonitoringGridClientID %>";
                    break;
                case "surgery":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialSurgeryEntry.aspx";
                    grdid = "<%=nosocomialCtl.SurgeryMonitoringGridClientID %>";
                    break;
                case "ett":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialEttEntry.aspx";
                    grdid = "<%=nosocomialCtl.EttMonitoringGridClientID %>";
                    break;
                case "bedrest":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialBedRestEntry.aspx";
                    grdid = "<%=nosocomialCtl.BedRestMonitoringGridClientID %>";
                    break;
                }

                url = url +
                    '?mod=' +
                    mod +
                    '&patid=<%= PatientID %>&regno=<%=RegistrationNo %>&monno=' +
                    monno +
                    '&ccm=rebind&cet=' +
                    grdid;
                openWinEntry(url, 600, 450);
            }


            function entryNosocomialDetail(mod, montype, seqno) {
                var url = "";
                var grdid = "";
                var monno = 0;
                var height = 750;
                switch (montype) {
                    case "infus":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialInfusDetailEntry.aspx";
                    monno = $find("<%= nosocomialCtl.InfusMonitoringNoClientID %>").get_value();
                    grdid = "<%=nosocomialCtl.InfusMonitoringGridClientID %>";
                    break;
                case "ngt":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialNgtDetailEntry.aspx";
                    monno = $find("<%= nosocomialCtl.NgtMonitoringNoClientID %>").get_value();
                    grdid = "<%=nosocomialCtl.NgtMonitoringGridClientID %>";
                    break;
                case "catheter":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialCatheterDetailEntry.aspx";
                    monno = $find("<%= nosocomialCtl.CatheterMonitoringNoClientID %>").get_value();
                    grdid = "<%=nosocomialCtl.CatheterMonitoringGridClientID %>";
                    break;
                case "surgery":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialSurgeryDetailEntry.aspx";
                    monno = $find("<%= nosocomialCtl.SurgeryMonitoringNoClientID %>").get_value();
                    grdid = "<%=nosocomialCtl.SurgeryMonitoringGridClientID %>";
                    break;
                case "ett":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialEttDetailEntry.aspx";
                    monno = $find("<%= nosocomialCtl.EttMonitoringNoClientID %>").get_value();
                    grdid = "<%=nosocomialCtl.EttMonitoringGridClientID %>";
                    break;
                case "bedrest":
                    url =
                        "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialBedRestDetailEntry.aspx";
                    monno = $find("<%= nosocomialCtl.BedRestMonitoringNoClientID %>").get_value();
                    grdid = "<%=nosocomialCtl.BedRestMonitoringGridClientID %>";
                    height = 400;
                    break;
                }

                if (monno === '' || monno === '0') {
                    alert("Please create " + montype + " monitoring installation first");
                    return;
                }

                url = url +
                    '?mod=' +
                    mod +
                    '&patid=<%= PatientID %>&regno=<%=RegistrationNo %>&monno=' +
                    monno +
                    '&seqno=' +
                    seqno +
                    '&ccm=rebind&cet=' +
                    grdid;

                openWinEntry(url, 600, height);
            }

            function openWinEntry(url, width, height) {
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWindow(url, width, height);
            }

            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y<0)
                    oWnd.moveTo(pos.x, 0);
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
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) ;

                // set height to the whole RadGrid control
                var grid = $find("<%= nosocomialCtl.InfusMonitoringGridClientID %>");
                grid.get_element().style.height = height - 266 + "px";
                grid.repaint();

                var grid2 = $find("<%= nosocomialCtl.CatheterMonitoringGridClientID %>");
                grid2.get_element().style.height = height - 240 + "px";
                grid2.repaint();

                var grid3 = $find("<%= nosocomialCtl.NgtMonitoringGridClientID %>");
                grid3.get_element().style.height = height - 266 + "px";
                grid3.repaint();

                var grid4 = $find("<%= nosocomialCtl.SurgeryMonitoringGridClientID %>");
                grid4.get_element().style.height = height - 184 + "px";
                grid4.repaint();

                var grid5 = $find("<%= nosocomialCtl.EttMonitoringGridClientID %>");
                grid5.get_element().style.height = height - 240 + "px";
                grid5.repaint();

                var grid6 = $find("<%= nosocomialCtl.BedRestMonitoringGridClientID %>");
                grid6.get_element().style.height = height - 184 + "px";
                grid6.repaint();
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
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
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
            <telerik:RadToolBarButton  runat="server" Text="Print" Value="print" ImageUrl="~/Images/Toolbar/print16.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
        </Items>
    </telerik:RadToolBar>
    <cc:NosocomialCtl runat="server" ID="nosocomialCtl" />

</asp:Content>
