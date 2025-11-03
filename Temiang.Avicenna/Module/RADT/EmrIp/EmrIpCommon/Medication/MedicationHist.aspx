<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationHist" %>

<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHistCtl.ascx" TagPrefix="cc" TagName="MedicationHistCtl" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #medused {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #medused td, #medused th {
                border: 1px solid #a9a9a9;
                padding: 4px;
            }

            #medused tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #medused tr:hover {
                background-color: #ddd;
            }

            #medused th {
                padding-top: 6px;
                padding-bottom: 6px;
                text-align: center;
                background-color: #4CAF50;
                color: white;
            }

        .tooltip {
            position: relative;
            display: inline-block;
            border-bottom: 1px dotted black;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 100px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -40px;
                opacity: 0;
                transition: opacity 0.3s;
            }

                .tooltip .tooltiptext::after {
                    content: "";
                    position: absolute;
                    top: 100%;
                    left: 50%;
                    margin-left: -5px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: #555 transparent transparent transparent;
                }

            .tooltip:hover .tooltiptext {
                visibility: visible;
                opacity: 1;
            }
    </style>
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function openWinEntry(url, width, height) {
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWindow(url, width, height);
            }
            function openMedStatPatSign(signid) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationStatusPatientSign.aspx?md=view&patid=<%= PatientID %>&regno=<%=RegistrationNo %>&signid='+signid;
                openWinEntry(url, 1000, 550);
            }
            function entryMedicationScheduleSetup(medrecno, scdate, scno, timeSchedule) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationScheduleEntry.aspx?mod=new&prgid=<%= ProgramID %>&medrecno=' + medrecno + '&scdate=' + scdate + '&scno=' + scno + '&time=' + timeSchedule + '&ccm=rebind&cet=<%=medicationHistCtl.ClientID %>';
                openWinEntry(url, 600, 550);
                    }
            function entryMedicationChangeConsumeMethod(medrecno, conmtd, patientID) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationChangeConsumeMethod.aspx?mod=new&patid=' + patientID + '&medrecno=' + medrecno + '&conmtd=' + conmtd + '&stat=<%=Request.QueryString["stat"]%>&ccm=rebind&cet=<%= medicationHistCtl.GridClientID %>';
                openWinEntry(url, 600, 550);
            }
            function entryMedicationReceiveEdit(medrecno, patientID) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationReceiveEdit.aspx?mod=edit&patid=' + patientID + '&medrecno=' + medrecno + '&stat=<%=Request.QueryString["stat"]%>&ccm=rebind&cet=<%= medicationHistCtl.GridClientID %>';
                openWinEntry(url, 600, 550);
            }
            function entryMedicationStatusConfirm(mrecno, medsttp) {

                var url = "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationStatusConfirm.aspx?mrecno=" + mrecno + "&medsttp=" + medsttp + "&ccm=click&cet=<%=btnRefresh.ClientID %>";
                            openWinEntry(url, 400, 420);
                        }
            function showPrescription(prescno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/Prescription.aspx?prescno=' + prescno;
                openWinEntry(url, 600, 550);
            }
            function openRasproFormView(patid, regno, rseqno) {
                openWindowMaxScreen("<%= Helper.UrlRoot() %>/Module/RADT/Ppra/RasproFormView.aspx?patid=" + patid + "&regno=" + regno + "&rseqno=" + rseqno)
            }
            function openWindowMaxScreen(url) {
                var oWnd = radopen(url, 'winDialog');
                oWnd.maximize();
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
                        case "click":
                            $("#" + arg.eventTarget).click();
                            break;
                        case "redirect":
                            window.location = arg.url;
                            break;
                        default:
                            break;
                    }
                }

            }

            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) -
                    document.getElementById('tblHeader').offsetHeight - 36;
                var grid;

                // set height to the whole RadGrid control
                grid = $find("<%= medicationHistCtl.GridClientID %>");
                grid.get_element().style.height = height + "px";
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
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRefresh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStartFromRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStartFromCurrentDay">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStartFromLast1Day">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStartFromLast3Day">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationHist" />
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


    <table id="tblHeader" style="width: 100%">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label" style="width: 40px">Date</td>
                        <td style="width: 110px">
                            <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="100px"></telerik:RadDatePicker>
                        </td>

                        <td style="width: 80px">
                            <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td class="label" style="width: 40px">Start From</td>
                        <td style="width: 120px">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnStartFromRegistration" Text="Registration Date" OnClick="btnStartFromRegistration_Click" Width="120px" /></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnStartFromCurrentDay" Text="Current Day" OnClick="btnStartFromCurrentDay_Click" Width="120px" /></td>
                                </tr>
                            </table>



                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" style="width: 40px">Display Only</td>
                        <td colspan="2">
                            <asp:CheckBox runat="server" ID="chkIsOnlyUsed" Text="Used Medicine" Checked="True" /><br />
                            <asp:CheckBox runat="server" ID="chkIsAntibiotic" Text="Antibiotic" Checked="False" />
                        </td>
                        <td class="label" style="width: 40px"></td>
                        <td style="width: 120px">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnStartFromLast1Day" Text="Last 1 Day" OnClick="btnStartFromLast1Day_Click" Width="120px" /></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnStartFromLast3Day" Text="Last 3 Day" OnClick="btnStartFromLast3Day_Click" Width="120px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <fieldset width="100%">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 40px">Name</td>
                            <td style="width: 200px">:&nbsp;<asp:Label runat="server" ID="lblPatientName"></asp:Label>
                            </td>
                            <td class="label" style="width: 40px">Sex</td>
                            <td style="width: 10px">:&nbsp;<asp:Label runat="server" ID="lblSex"></asp:Label>
                            </td>
                            <td class="label" style="width: 40px">DOB</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblBirthDate"></asp:Label>
                            </td>
                            <td class="label" style="width: 40px">Age</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblAge"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <cc:MedicationHistCtl runat="server" ID="medicationHistCtl" IsConsumeMethodChangeAble="False" />
</asp:Content>
