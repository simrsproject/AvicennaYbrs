<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="EmrList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.EmrList" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            var refreshTimeout = "1";
            var isAutoRefresh = <%= (AppSession.Parameter.IsAutoRefreshEhrList).ToString().ToLower() %>;
            var suspendAutoReload = false;
            //var refreshTimer = setTimeout('ReloadList()', parseInt(refreshTimeout) * 60 * 1000);

//            function ReloadList() {
//                if (!isAutoRefresh) return;
//
//                if (!suspendAutoReload) {
//                    var masterTable = $find("<%= grdList.ClientID %>").get_masterTableView();
            //                    masterTable.rebind();
            //                }
            //                //reset the timeout
            //                refreshTimer = setTimeout('ReloadList()',OPt
            //                    parseInt(refreshTimeout) * 60 * 1000);
            //            }           

            function ReloadList() {
                if (!isAutoRefresh) return;
                if (!suspendAutoReload) {
                    var masterTable = $find("<%= grdList.ClientID %>").get_masterTableView();
                    masterTable.rebind();
                }
            }

            $(document).ready(function () {
                //setInterval(ReloadList, parseInt(refreshTimeout) * 60 * 1000);
            });

            function gotoAddUrl(rtype, regno, parid, unit, room, astp, patid, fregno, isdod) {
                if (isdod == "false") {
                    var url = 'EmrDetail.aspx?rt=' + rtype + '&regno=' + regno + '&fregno=' + fregno + '&parid=' + parid + '&unit=' + unit + '&room=' + room + '&patid=' + patid;
                    if (rtype === '<%=AppConstant.RegistrationType.InPatient%>') {
                        // InPatient EMR
                        url = '../EmrIp/EmrIpDetail.aspx?rt=' + rtype + '&regno=' + regno + '&fregno=' + fregno + '&parid=' + parid + '&unit=' + unit + '&room=' + room + '&patid=' + patid;
                    }
                    window.location.href = url;
                }
                else {
                    if (confirm('Are you sure to take over this patient?')) {
                        //__doPostBack("<%= grdList.UniqueID %>", 'takeover|' + regno);

                        var obj = {};
                        obj.registrationNo = regno;
                        $.ajax({
                            url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrWebService.asmx/TakeOverRegistration',
                            data: JSON.stringify(obj), //ur data to be sent to server
                            contentType: "application/json; charset=utf-8",
                            type: "POST",
                            dataType: "json",
                            success: function (data) {
                                var abortMsg = decodeURI(data.d);
                                if (abortMsg == "")
                                    gotoAddUrl(rtype, regno, parid, unit, room, astp, patid, fregno, "false");
                                else
                                    radalert(abortMsg, 400, 120, "Take over Registration failed: " + abortMsg);
                            },
                            error: function (x, y, z) {
                                alert(x.responseText + "  " + x.status);
                            }
                        });
                    }
                }
            }
            function openCovidDialog(regNo) {
                stopAutofresh();
                var oWnd = $find("<%= winDialog2.ClientID %>");
                oWnd.SetUrl("CovidStatusDialog.aspx?regno=" + regNo);
                oWnd.show();
            }
            function openPatientRiskDialog(regNo) {
                stopAutofresh();
                var oWnd = $find("<%= winDialog2.ClientID %>");
                oWnd.SetUrl("PatientRiskDialog.aspx?regno=" + regNo);
                oWnd.show();
            }
            function openPatientRiskColorDialog(regNo, divId) {
                stopAutofresh();
                var argument = "riskcol|" + regNo;
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("PatientRiskColorDialog.aspx?regno=" + regNo+'&ccm=updstat&cea='+argument+'&cet='+divId);
                oWnd.show();
            }

            function openVitalSignChartEws(patid, regno, fregno, date, ewsType) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartEws.aspx?patid=' + patid + '&regno=' + regno + '&fregno=' + fregno + '&ewstype=' + ewsType + '&date=' + date;
                openWindowMaxScreen(url);
            }
            function openMedicationHist(patid, regno, fregno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid=' + patid + '&regno=' + regno + '&fregno=' + fregno;
                openWindowMaxScreen(url);
            }
            function openPendingPrescription(regno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PendingPrescription.aspx?regno=' + regno;
                openWinEntryMaxHeightWindow(url, 1000);
            }
            function openWinEntryMaxHeightWindow(url, width) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                openWindow(url, width, height - 40);
            }
            function openPrintDialog(regNo) {
                var oWnd = $find("<%= winPrintLbl.ClientID %>");
                oWnd.SetUrl("PrintDialog.aspx?regno=" + regNo);
                oWnd.show();
            }
            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.print != null) {
                    var oWnd = $find("<%= winPrint.ClientID %>");
                    oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                    oWnd.show();
                }
            }
            function rowConfirmed(regNo) {
                if (confirm('Are you sure to confirmed for selected patient?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'confirmed|' + regNo);
                }
            }
            function rowFinished(regNo) {
                if (confirm('Are you sure to finished (Antrian Online) for selected patient?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'finished|' + regNo);
                }
            }
            function openWinRegistrationInfo(regNo) {
                var lblToBeUpdate = "noti_" + regNo;
                var url = '<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>';
                openWindow(url, 900, 500);
            }

            function onEditPhysician(type, regNo, unitId) {
                var oWnd = window.$find("<%= winEditPhysician.ClientID %>");
                oWnd.setUrl("../../RADT/Registration/EditPhysicianDialog.aspx?rt=" + type + "&regNo=" + regNo + "&unitID=" + unitId);
                oWnd.show();
            }
            function openMedicationReceiveOpt(regNo, patid) {
                stopAutofresh();
                var url =
                    '<%=Page.ResolveUrl("~/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveOpt.aspx")%>';

                url = url + "?regNo=" + regNo + "&patid=" + patid;

                var oWnd = $find("<%= winMedicationReceiveOpt.ClientID %>");
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();
            }
            function winMedicationReceiveOpt_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    //if (oWnd.argument.url.includes("wintype=max"))
                    //    openWindowMaxScreen(oWnd.argument.url);
                    //else
                    openWinEntryMaxWindow(oWnd.argument.url);
                }
                else
                    startAutofresh();
            }
            function openWindow(url, width, height) {
                stopAutofresh();

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();
                oWnd.show();
            }

            function openWinEntryMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                openWindow(url, width - 40, height - 40);
            }

            function openWindowMaxScreen(url) {
                stopAutofresh();

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }
            function winDialog_ClientClose(oWnd, args) {
                startAutofresh();

                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
                var arg = args.get_argument();
                if (arg != null) {
                    switch (arg.callbackMethod) {
                        case "submit":
                            __doPostBack(arg.eventTarget, arg.eventArgument);
                            break;
                        case "rebind":
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }
                            break;
                        case "click":
                            $("#" + arg.eventTarget).click();
                            break;
                        case "redirect":
                            window.location = arg.url;
                            break;
                        case "updstat":
                        {
                            // Baru untuk riskcol
                            var vals = arg.eventArgument.split('|');
                            UpdateStateEmrList(vals[0], arg.eventTarget, vals[1], '', '', '', '', '');
                            break;
                        }
                        default:
                            break;
                    }
                }
            }

            function startAutofresh() {
                suspendAutoReload = false;

                //reset the timeout
                //refreshTimer = setTimeout('ReloadList()',
                //    parseInt(refreshTimeout) * 60 * 1000);
            }
            function stopAutofresh() {
                suspendAutoReload = true;
            }
            function rebindOnClientClose(oWnd, args) {
                __doPostBack("<%= grdList.UniqueID %>", "rebind");
            }
            function winCloseClinic_ClientClose(oWnd, args) {
                startAutofresh();
            }
            function openWinCloseClinic() {
                stopAutofresh();
                var oWnd = window.$find("<%= winCloseClinic.ClientID %>");
                oWnd.setUrl("../../RADT/CloseClinic/CloseClinicDialog.aspx");
                oWnd.show();
            }

            function cboRegistrationType_OnClientSelectedIndexChanged() {
                var obj = {};
                var combo = window.$find("<%= cboRegistrationType.ClientID %>");
                obj.registrationType = combo.get_selectedItem().get_value();

                obj.userID = '<%=AppSession.UserLogin.UserID%>';
                obj.userType = '<%=AppSession.UserLogin.SRUserType%>';
                $.ajax({
                    url: "EmrWebService.asmx/ServiceUnitList",
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {

                        var retVal = decodeURI(data.d);
                        if (retVal.length > 0) {
                            var list = retVal.split('|');
                            var cbo = window.$find("<%= cboServiceUnitID.ClientID %>");
                            cbo.clearItems();
                            cbo.clearSelection();

                            cbo.trackChanges();
                            var larr = list.length - 1;
                            for (var i = 0; i < larr; i++) {
                                var item = new Telerik.Web.UI.RadComboBoxItem();

                                var arr = list[i].split('_');
                                item.set_text(arr[1]);
                                item.set_value(arr[0]);
                                cbo.get_items().add(item);
                            }
                            cbo.commitChanges();

                        } else {

                            var combo = $find("<%= cboServiceUnitID.ClientID %>");
                            combo.clearItems();
                        }

                    },
                    error: function (x, y, z) {
                        alert(x.responseText + "  " + x.status);
                    }
                });
            }


            function UpdateStateEmrList(statType, divCtl, regNo, fromRegNo, regType, patID, dob, parID) {
                var obj = {};
                obj.statType = statType;
                obj.regNo = regNo;
                obj.fromRegNo = fromRegNo;
                obj.regType = regType;
                obj.patID = patID;
                obj.dob = dob;
                obj.parID = parID;
                $.ajax({
                    url: 'EmrWebService.asmx/UpdateStateEmrList',
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        // update div
                        document.getElementById(divCtl).innerHTML = data.d;

                    },
                    error: function (x, y, z) {
                        //alert(x.responseText + "  " + x.status);
                    }
                });
            }


        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="ajxmProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSmf">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterInculde">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterConfirmedAttendence">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterIncludeOpr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
<%--            <telerik:AjaxSetting AjaxControlID="btnFilterIncludeNotInBed">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="btnFilterExamOrder">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedicTeam">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="ajaxLoadingPanel" runat="server" Skin="Default" OnClientShowing="stopAutofresh" OnClientHiding="startAutofresh">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow ID="winMedicationReceiveOpt" Width="400px" Height="450px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="winMedicationReceiveOpt_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="winDialog_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winDialog2" Width="600px" Height="300px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="rebindOnClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrintLbl" Animation="None" Width="600px" Height="300px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="True"
        Modal="true" OnClientClose="onClientClose" Title="Print Label">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" Title="Edit Physician" ID="winEditPhysician" OnClientClose="rebindOnClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
        Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
        Modal="true" Title="Close Clinic" ID="winCloseClinic" OnClientClose="winCloseClinic_ClientClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnIsClinicalPathwayActive" />
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="520px" valign="top">
                    <fieldset>
                        <legend>General</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EmptyMessage="Select a Paramedic"
                                        EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                                        <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left;">
                                    <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trSmfFilter">
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="SMF"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox runat="server" ID="cboSmf" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterSmf" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRegType" runat="server" Text="Registration"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadDropDownList runat="server" ID="cboRegistrationType" Width="300px" OnClientSelectedIndexChanged="cboRegistrationType_OnClientSelectedIndexChanged"></telerik:RadDropDownList>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterRegType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                                </td>
                                <td style="text-align: left;">
                                    <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Paramedic Team Status</td>
                                <td>
                                    <telerik:RadComboBox runat="server" ID="cboParamedicTeam" Width="200px">
                                    </telerik:RadComboBox>
                                    <asp:CheckBox ID="chkIsIncludeClosed" runat="server" Text="Include Closed" />
                                </td>
                                <td style="text-align: left;">
                                    <asp:ImageButton ID="btnFilterParamedicTeam" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td style="vertical-align: top; width: 500px;">
                    <fieldset style="width: 500px;" runat="server" id="pnlFilterInPatient">
                        <legend>For Inpatient</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">Include</td>
                                <td class="entry300">

                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkIsIncludeNotInBed" runat="server" Text="Discharged Patients" /></td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:CheckBox ID="chkIprIsSoapInputted" runat="server" Text="SOAP Inputted" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnFilterIncludeNotInBed" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="width: 500px;">
                        <legend>For Non Inpatient</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;/&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtFromRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                            <td>&nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="txtToRegistrationTime" runat="server" Mask="<00..23>:<00..59>"
                                                    PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblConfirmedAttendance" runat="server" Text="Confirmed Attendance Status"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <telerik:RadComboBox ID="cboConfirmedAttendanceStatus" runat="server" Width="250px">
                                    </telerik:RadComboBox>
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnFilterConfirmedAttendence" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Include</td>
                                <td class="entry300">
                                    <asp:CheckBox ID="chkIsAllSoap" runat="server" Text="SOAP Inputted" /></td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnFilterIncludeOpr" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="width: 500px;">
                        <legend>For Exam Order Patient</legend>
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Exam Order Date"></asp:Label>
                                </td>
                                <td class="entry300">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtExamOrderDateFrom" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;-&nbsp;
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtExamOrderDateTo" runat="server" Width="100px" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="20px">
                                    <asp:ImageButton ID="btnFilterExamOrder" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>

                        </table>
                    </fieldset>
                </td>
                <td style="width: 20px;"></td>
                <td style="vertical-align: central; width: 100px">
                    <fieldset style="width: 50px">
                        <legend>Count</legend>
                        <asp:Label ID="lblRegistrationCount" runat="server" Text="" Font-Size="20px"></asp:Label>
                    </fieldset>
                </td>
                <td style="vertical-align: central;">
                    <fieldset style="width: 110px;">
                        <legend>Close Clinic</legend>
                        <asp:ImageButton ID="btnCloseClinic" runat="server" ImageUrl="~/Images/doctor_with_closed_sign.png"
                            OnClientClick="javascript:openWinCloseClinic();return false;" />
                    </fieldset>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo" ShowHeadersWhenNoRecords="true"
            GroupLoadMode="Client">
            <GroupHeaderTemplate>
                <%#string.Format("Service Unit: <b>{0}</b><br/>&nbsp;&nbsp;Reg To: <b>{1}</b>" , Eval("Group"), Eval("ParamedicName") )%>
            </GroupHeaderTemplate>
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="Group" HeaderText="Service Unit " />
                        <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Reg To Physician " />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="Group" SortOrder="Ascending" />
                        <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsDoctorOnDuty").Equals("false") ? 
                                string.Format("<a href=\"#\" onclick=\"javascript:gotoAddUrl('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"New\" /></a>",
                                DataBinder.Eval(Container.DataItem, "SRRegistrationType"), DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                    DataBinder.Eval(Container.DataItem, "ParamedicID"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"),
                                        DataBinder.Eval(Container.DataItem, "RoomID"),
                                        string.Empty, DataBinder.Eval(Container.DataItem, "PatientID"), 
                                        DataBinder.Eval(Container.DataItem, "FromRegistrationNo"),
                                        DataBinder.Eval(Container.DataItem, "IsDoctorOnDuty")) :
                                string.Format("<a href=\"#\" onclick=\"javascript:gotoAddUrl('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'); return false;\"><img src=\"../../../Images/doctor16.png\" border=\"0\" title=\"Doctor required\" /></a>",
                                DataBinder.Eval(Container.DataItem, "SRRegistrationType"), DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                    DataBinder.Eval(Container.DataItem, "ParamedicID"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"),
                                        DataBinder.Eval(Container.DataItem, "RoomID"),
                                        string.Empty, DataBinder.Eval(Container.DataItem, "PatientID"), 
                                        DataBinder.Eval(Container.DataItem, "FromRegistrationNo"),
                                        DataBinder.Eval(Container.DataItem, "IsDoctorOnDuty")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="RegistrationDate" HeaderText="Reg. Date">
                    <ItemTemplate>
                        <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(AppConstant.DisplayFormat.DateShortMonth) %><br />
                        <%#DataBinder.Eval(Container.DataItem, "RegistrationTime") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="RegistrationQue" HeaderText="Que"
                    UniqueName="RegistrationQue" SortExpression="RegistrationQue" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />--%>
                 <telerik:GridTemplateColumn UniqueName="RegistrationQue" HeaderText="Que" SortExpression="RegistrationQue" Visible="True">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}",DataBinder.Eval(Container.DataItem, "RegistrationQue"), DataBinder.Eval(Container.DataItem, "FormattedNo"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn HeaderStyle-Width="40px" UniqueName="ExternalQueNo" HeaderText="Que" SortExpression="ExternalQueNo">
                    <ItemTemplate>
                        <%#String.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "ExternalQueNo").ToString()) ? string.Format("{0} {1}",DataBinder.Eval(Container.DataItem, "RegistrationQue"), DataBinder.Eval(Container.DataItem, "FormattedNo")) : string.Format("{0}",DataBinder.Eval(Container.DataItem, "ExternalQueNo")) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="RegistrationNo" HeaderText="MRN / Reg No">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "MedicalNo")  %><br />
                        <%#DataBinder.Eval(Container.DataItem, "RegistrationNo") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="IsVipMember" HeaderText="">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsVipMember")) ? "<img src=\"../../../Images/Animated/vipmember16.gif\" border=\"0\" alt=\"VIP\" title=\"VIP\" />" : string.Empty%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="ParamedicID" HeaderText="ParamedicID"
                    UniqueName="ParamedicID" SortExpression="ParamedicID" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName" HeaderStyle-Width="225px">
                    <ItemTemplate>
                        <span style="font-size: 12pt;"><%# string.Format("<a href=\"#\" onclick=\"javascript:gotoAddUrl('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{10}'); return false;\">{9} {8}</a>",
                                DataBinder.Eval(Container.DataItem, "SRRegistrationType"), DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                DataBinder.Eval(Container.DataItem, "ParamedicID"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"),
                                DataBinder.Eval(Container.DataItem, "RoomID"),
                                string.Empty, DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "FromRegistrationNo"),DataBinder.Eval(Container.DataItem, "PatientName"),
                                DataBinder.Eval(Container.DataItem, "SalutationName"),
                                DataBinder.Eval(Container.DataItem, "IsDoctorOnDuty"))%>
                        </span>
                         <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsAlive")) ? string.Empty : "<img src=\"../../../Images/Rip16.png\" border=\"0\" alt=\"Decease\" title=\"Decease\" />" %>
                        <br />
                            <%# 
                            DataBinder.Eval(Container.DataItem, "IsAlive").Equals(false) ? string.Format("{0}Y {1}M {2}D", 
                            Helper.GetAgeInYear(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date), Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DeceasedDateTime") == DBNull.Value ? DateTime.Now.Date : DataBinder.Eval(Container.DataItem, "DeceasedDateTime"))), 
                            Helper.GetAgeInMonth(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date), Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DeceasedDateTime") == DBNull.Value ? DateTime.Now.Date : DataBinder.Eval(Container.DataItem, "DeceasedDateTime"))), 
                            Helper.GetAgeInDay(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date), Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DeceasedDateTime") == DBNull.Value ? DateTime.Now.Date : DataBinder.Eval(Container.DataItem, "DeceasedDateTime")))) : string.Format("{0}Y {1}M {2}D", 
                            Helper.GetAgeInYear(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), 
                            Helper.GetAgeInMonth(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), 
                            Helper.GetAgeInDay(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)))
                            %>
                        <br />
                        <%#DataBinder.Eval(Container.DataItem, "GuarantorName") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gdr" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="32px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Group" HeaderText="Service Unit" UniqueName="Group" Visible="False"
                    SortExpression="Group">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="RoomName" HeaderText="Room / Bed" SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemTemplate>
                        R: &nbsp; <%#DataBinder.Eval(Container.DataItem, "RoomName")  %><br />
                        B: &nbsp;<%#DataBinder.Eval(Container.DataItem, "BedID") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ReferFrom" HeaderText="Refer" SortExpression="ReferFrom">
                    <ItemTemplate>
                        <%#String.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "ReferFrom").ToString())? string.Empty:string.Format("From: {0}",DataBinder.Eval(Container.DataItem, "ReferFrom"))  %><br />
                        <%#String.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "ReferTo").ToString())? string.Empty:string.Format("To: {0}",DataBinder.Eval(Container.DataItem, "ReferTo"))  %><br />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="TRG" HeaderStyle-Width="25px" HeaderStyle-Font-Size="XX-Small">
                    <ItemTemplate>
                        <div style="width: 25px; font-size: xx-small; background-color: <%# ColorOfTriase(DataBinder.Eval(Container.DataItem,"SRTriage")) %>; color: gray;">TRG</div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="|" HeaderStyle-Width="15px" HeaderStyle-Font-Size="XX-Small" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div style="width: 5px; font-size: xx-small; color: gray;">|</div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="SOAP" HeaderStyle-Width="35px" HeaderStyle-Font-Size="XX-Small" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--                        <%#  SoapEntryStatuslHtml(Container) %>--%>
                        <%# string.Format("<div id=\"soap{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("soap",Eval("RegistrationNo"),Eval("FromRegistrationNo"),Eval("SRRegistrationType"),Eval("PatientID"),Eval("DateOfBirth"),Eval("ParamedicID"))%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="|" HeaderStyle-Width="15px" HeaderStyle-Font-Size="XX-Small" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <div style="width: 5px; font-size: xx-small; color: gray;">|</div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="CONF" HeaderStyle-Width="35px" HeaderStyle-Font-Size="XX-Small" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "IsConfirmedAttendance").Equals(true) && !DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals("IPR") ? 
                                                          string.Format("<img src='{0}/Images/Toolbar/post_yellow_16.png'/>", Helper.UrlRoot()): string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="7" ItemStyle-Font-Size="7" ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <table style="width: 100px;">
                            <tr>
                                <td style="width: 20px;">TRG</td>
                                <td>|</td>
                                <td style="width: 20px;">SOAP</td>
                                <td>|</td>
                                <td style="width: 20px;">CONF</td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 100px;">
                            <tr>
                                <td>
                                    <div style="width: 20px; background-color: <%# ColorOfTriase(DataBinder.Eval(Container.DataItem,"SRTriage")) %>; color: gray;">TRG</div>
                                </td>
                                <td>|</td>
                                <td style="width: 20px;"><%#  SoapEntryStatuslHtml(Container) %></td>
                                <td>|</td>
                                <td style="width: 20px;"><%#DataBinder.Eval(Container.DataItem, "IsConfirmedAttendance").Equals(true) && !DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals("IPR") ? 
                                                          string.Format("<img src='{0}/Images/Toolbar/post_yellow_16.png'/>", Helper.UrlRoot()): string.Empty%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Menu">
                    <HeaderStyle HorizontalAlign="Center" Width="170px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <ItemTemplate>
                        <table width="170px">
                            <tr>
                                <td style="width: 50px">
                                    <%# DataBinder.Eval(Container.DataItem, "IsConfirmedAttendance").Equals(true) || DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals("IPR") ? "<img src=\"../../../Images/Toolbar/post16_d.png\" border=\"0\" alt=\"Confirmed\" title=\"\" />" :
                                        string.Format("<a href=\"#\" onclick=\"rowConfirmed('{0}'); return false;\"><img src=\"../../../Images/Toolbar/post16.png\" border=\"0\" alt=\"Confirmed\" title=\"Confirmed Attendance\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>&nbsp;
                                             <%# DataBinder.Eval(Container.DataItem, "IsFinishedAttendance").Equals(true) || DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals("IPR") || DataBinder.Eval(Container.DataItem, "IsConfirmedAttendance").Equals(false) ? "<img src=\"../../../Images/Toolbar/post16_d.png\" border=\"0\" alt=\"Finished\" title=\"\" />" :
                                        string.Format("<a href=\"#\" onclick=\"rowFinished('{0}'); return false;\"><img src=\"../../../Images/Toolbar/post_green_16.png\" border=\"0\" alt=\"Finished\" title=\"Finished Attendance\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>



                                    <%--                                    <%# DataBinder.Eval(Container.DataItem, "IsConfirmedAttendance").Equals(true)   || DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals("OPR") ? 
                                        string.Format("<a href=\"#\" onclick=\"rowFinished('{0}'); return false;\"><img src=\"../../../Images/Toolbar/post_green_16.png\" border=\"0\" alt=\"Finished (Antrian Online)\" title=\"Finished Attendance\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo")) :
                                        "<img src=\"../../../Images/Toolbar/post16_d.png\" border=\"0\" alt=\"Finished\" title=\"\" />"%>--%>
                                </td>
                                <td style="width: 20px"><%# RegistrationNoteCount(Container)%>
                                </td>
                                <td style="width: 20px"><%# string.Format("<a href=\"#\" onclick=\"javascript:openMedicationReceiveOpt('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/drugs16.png\" border=\"0\" alt=\"Confirmed\" title=\"Medication Menu\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "PatientID"))%></td>
                                <td style="width: 20px"><%# !DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals(AppConstant.RegistrationType.InPatient)? string.Empty: string.Format("<a href=\"#\" onclick=\"openMedicationHist('{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/ordering16.png\" border=\"0\" alt=\"MedHist\" title=\"Medication History\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"), 
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                            DataBinder.Eval(Container.DataItem, "FromRegistrationNo"))%></td>
                                <td style="width: 20px"><%#string.Format("<a href=\"#\" onclick=\"openPrintDialog('{0}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print\" /></a>",
                                                                   DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                                </td>
                                <td style="width: 20px"><%# ((DataBinder.Eval(Container.DataItem, "IsParamedicNotNull").Equals(true) ? string.Empty : string.Format("<a href=\"#\" onclick=\"onEditPhysician('{0}', '{1}', '{2}'); return false;\">{3}</a>",
                                            Request.QueryString["rt"], DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"),
                                            "<img src=\"../../../Images/Toolbar/dokter.png\" border=\"0\" alt=\"Edit Physician\" title=\"Edit Physician\" />")))%>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName4" HeaderText="Covid" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td align="center">&nbsp;<%# string.Format("<a href=\"#\" onclick=\"openCovidDialog('{0}'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                            string.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "SRCovidStatus").ToString()) ? "<img src=\"../../../Images/pandemic.png\" border=\"0\" alt=\"Covid-19 Status\" title=\"Covid-19 Status\" width=\"24px\" height=\"24px\" />" : 
                                            "<img src=\"../../../Images/covid.jpg\" border=\"0\" alt=\"Covid-19 Status\" title=\"Covid-19 Status\" />")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Risk1" HeaderText="Risk" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openPatientRiskDialog('{0}'); return false;\">{1}</a>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                            string.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "SRPatientRiskStatus").ToString()) ? "<img src=\"../../../Images/healthrisk.png\" border=\"0\" alt=\"Patient Risk Status\" title=\"Patient Risk Status\" width=\"24px\" height=\"24px\" />" : 
                                            (DataBinder.Eval(Container.DataItem, "SRPatientRiskStatus").Equals("X") ? "<img src=\"../../../Images/norisk.png\" border=\"0\" alt=\"Patient Risk Status\" title=\"Patient Risk Status\" />" : 
                                            (DataBinder.Eval(Container.DataItem, "SRPatientRiskStatus").Equals("0") ? "<img src=\"../../../Images/lowrisk.png\" border=\"0\" alt=\"Patient Risk Status\" title=\"Patient Risk Status\" />" : 
                                            (DataBinder.Eval(Container.DataItem, "SRPatientRiskStatus").Equals("1") ? "<img src=\"../../../Images/mediumrisk.png\" border=\"0\" alt=\"Patient Risk Status\" title=\"Patient Risk Status\" />": "<img src=\"../../../Images/highrisk.png\" border=\"0\" alt=\"Patient Risk Status\" title=\"Patient Risk Status\" />"))))%>
                        <div style="height: 4px;"></div>
                        <%# string.Format("<div id=\"riskcol{2}\"><a href=\"#\" onclick=\"openPatientRiskColorDialog('{0}','riskcol{2}'); return false;\">{1}</a></div>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                            (string.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "SRPatientRiskColor").ToString()) || (DataBinder.Eval(Container.DataItem, "SRPatientRiskColor").Equals("0"))) ? "<div style=\"background-color: gray; width: 38px; height: 18px;\"></div>" : 
                                            "<div style=\"background-color: " + GetRiskColor(DataBinder.Eval(Container.DataItem, "SRPatientRiskColor")) + "; width: 38px; height: 18px;\"></div>",Eval("RegistrationNo").ToString().Replace("/","_"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ReferFromRegistrationType" Display="False" />
                <telerik:GridTemplateColumn UniqueName="EWS" HeaderText="EWS" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--                        <%#  EwsScoreLevelHtml(Container) %>--%>
                        <%# string.Format("<div id=\"ews{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("ews",Eval("RegistrationNo"),Eval("FromRegistrationNo"),Eval("SRRegistrationType"),Eval("PatientID"),Eval("DateOfBirth"),Eval("ParamedicID"))%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Presc" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <%--                        <%# PrescriptionProgress(DataBinder.Eval(Container.DataItem, "SRRegistrationType").ToString(), DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>--%>

                        <%# string.Format("<div id=\"presc{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("presc",Eval("RegistrationNo"),Eval("FromRegistrationNo"),Eval("SRRegistrationType"),Eval("PatientID"),Eval("DateOfBirth"),Eval("ParamedicID"))%>
                        </script>

                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Lab" HeaderStyle-Width="45px">
                    <ItemTemplate>
                        <%--                        <%# ExamOrderLabProgress(DataBinder.Eval(Container.DataItem, "SRRegistrationType").ToString(), DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>--%>
                        <%# string.Format("<div id=\"lab{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("lab",Eval("RegistrationNo"),Eval("FromRegistrationNo"),Eval("SRRegistrationType"),Eval("PatientID"),Eval("DateOfBirth"),Eval("ParamedicID"))%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Rad" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%--                        <%# ExamOrderRadProgress(DataBinder.Eval(Container.DataItem, "SRRegistrationType").ToString(), DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>--%>
                        <%# string.Format("<div id=\"rad{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("rad",Eval("RegistrationNo"),Eval("FromRegistrationNo"),Eval("SRRegistrationType"),Eval("PatientID"),Eval("DateOfBirth"),Eval("ParamedicID"))%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center" UniqueName="ClinicalPathway"
                    HeaderText="CP" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--                        <%# RegistrationPathwayStatuslHtml(Container) %>--%>
                        <%# string.Format("<div id=\"pathway{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("pathway",Eval("RegistrationNo"),Eval("FromRegistrationNo"),Eval("SRRegistrationType"),Eval("PatientID"),Eval("DateOfBirth"),Eval("ParamedicID"))%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center" UniqueName="PlafondProgress"
                    HeaderText="Plafond Progress" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <%--                        <%#PlafondProgress(DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>--%>
                        <%# string.Format("<div id=\"plafond{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("plafond",Eval("RegistrationNo"),Eval("FromRegistrationNo"),Eval("SRRegistrationType"),Eval("PatientID"),Eval("DateOfBirth"),Eval("ParamedicID"))%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="RowSource" UniqueName="RowSource"
                    SortExpression="RowSource">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "RowSource").ToString() %>
                        <%# DataBinder.Eval(Container.DataItem, "SRBedStatus").ToString() %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
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
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
