<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="MedicationStatusPerPatient.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicationStatus.MedicationStatusPerPatient" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/MedicationStatus/MedicationStatusCtl.ascx" TagPrefix="uc1" TagName="MedicationStatusCtl" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%=JavascriptOpenPrintPreview() %>
    <script type="text/javascript">
        function printPreview() {
            enablePrint(0);
            setTimeout(function () { enablePrint(1); }, 5000);

            var obj = {};
            obj.programID = '<%=Temiang.Avicenna.Common.AppSession.Parameter.ProgramIdPrintUddEtiquette%>';
            obj.registrationNo = '<%=RegistrationNo%>';
            openPrintPreview("PopulatePrintParameter", obj);
        }
        function enablePrint(isEnable) {
            var lnkPrintPreview = $find("<%= lnkPrintPreview.ClientID %>");
            if (isEnable === 1)
                lnkPrintPreview.set_enabled(true);
            else
                lnkPrintPreview.set_enabled(false);
        }
        function openPrescriptionHist() {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PrescriptionHist.aspx?patid=<%= PatientID %>&regno=<%=RegistrationNo %>&fregno=&ccm=rebind&cet=<%=medicationStatusCtl.GridMedicationStatus.ClientID %>';
            openWinEntryMaxHeight(url, 1000);
        }

        function openPatientSign() {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationStatusPatientSign.aspx?patid=<%= PatientID %>&regno=<%=RegistrationNo %>&fregno=&ccm=rebind&cet=<%=medicationStatusCtl.GridMedicationStatus.ClientID %>';
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
            oWnd = radopen(url, 'winDialog');
            oWnd.setSize(width, height);
            oWnd.center();

            // Cek position
            var pos = oWnd.getWindowBounds();
            if (pos.y < 0)
                oWnd.moveTo(pos.x, 0);
        }

        function radWindowManager_ClientClose(oWnd, args) {
            //get the transferred arguments from MasterDialogEntry
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
                    default:
                        break;
                }
            }

        }

    </script>

    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterSuId">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="loadingPnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnFilterDrugSource">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="loadingPnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnFilterStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="loadingPnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btnFilterStartDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="loadingPnl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="loadingPnl" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <cc:CollapsePanel ID="cpInfo" runat="server" Title="Patient Info">
        <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" IsShowVitalSign="false" />
    </cc:CollapsePanel>
    <cc:CollapsePanel ID="cpFilter" runat="server" Title="Filter & Menu">

        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="500px" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">Dispensary
                            </td>
                            <td class="entry2Column">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AutoPostBack="false">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterSuId" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Drug Source</td>
                            <td class="entry2Column">
                                <telerik:RadRadioButtonList ID="optDrugSource" runat="server" Width="100%" AutoPostBack="false">
                                    <Items>
                                        <telerik:ButtonListItem Text="ALL" Value="" Selected="true" />
                                        <telerik:ButtonListItem Text="From Patient" Value="P" />
                                        <telerik:ButtonListItem Text="From Hospital" Value="H" />
                                    </Items>
                                </telerik:RadRadioButtonList>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterDrugSource" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="500px" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label"></td>
                            <td class="entry2Column">
                                <asp:CheckBox ID="chkIsIncludeFinished" runat="server" Text="Show Finished" />
                                <asp:CheckBox ID="chkIsIncludeStopped" runat="server" Text="Show Stopped" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">Start Date
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker runat="server" ID="txtStartDate"></telerik:RadDatePicker>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterStartDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10px;">&nbsp;</td>
                <td width="110px" style="vertical-align: central">
                    <telerik:RadLinkButton runat="server" ID="lnkPrintPresHist" Text="Prescription History" Width="100px" Height="80px" Icon-Url="~/Images/Toolbar/views16.png" OnClientClicked="openPrescriptionHist"></telerik:RadLinkButton>
                </td>
                <td width="110px" style="vertical-align: central">
                    <telerik:RadLinkButton runat="server" ID="lnkPrintPreview" Text="Print UDD Etiquette" Width="100px" Height="80px" Icon-Url="~/Images/Toolbar/print_preview16.png" OnClientClicked="printPreview"></telerik:RadLinkButton>
                </td>
                <td width="110px" style="vertical-align: central">
                    <telerik:RadLinkButton runat="server" ID="lnkPatientSign" Text="Patient Sign" Width="100px" Height="80px" Icon-Url="~/Images/Toolbar/print_preview16.png" OnClientClicked="openPatientSign"></telerik:RadLinkButton>
                </td>
                <td></td>
            </tr>
        </table>
    </cc:CollapsePanel>

    <uc1:MedicationStatusCtl runat="server" ID="medicationStatusCtl" />
</asp:Content>
