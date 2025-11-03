<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="CardexMonitoring.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.CardexMonitoring" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tblgraph {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .tblgraph td {
                border: 1px solid #a9a9a9;
                padding: 0px;
                text-align: center;
                width: 4.167%
            }

            .tblgraph tr {
                height: 20px;
            }

                .tblgraph tr:nth-child(even) {
                    background-color: #f2f2f2;
                    height: 20px;
                }

                .tblgraph tr:hover {
                    background-color: #ddd;
                    height: 20px;
                }

            .tblgraph th {
                border: 1px solid #a9a9a9;
                padding-top: 6px;
                padding-bottom: 6px;
                text-align: center;
                background-color: #4CAF50;
                color: white;
                height: 20px;
            }
    </style>
    <telerik:RadScriptBlock ID="scrbPhr" runat="server">

        <script type="text/javascript">
            function entryPhr(md, id, regno, fid, unit) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regno + '&patid=<%= PatientID %>&unit=' + unit + '&fid=' + fid + '&iusig=1&menu=su&refno=&ccm=submit';
                window.openWinEntryMaxWindow(url);
            }

            function radWindowManager_ClientClose(oWnd, args) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable
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
                        case "redirect":
                            window.location = arg.url;
                            break;
                        default:
                            break;
                    }

                }
            }

            function openWinEntryMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (!(url.includes("&rt=") || url.includes("?rt=")))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWindow(url, width - 40, height - 40);
            }


            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, "winDialog");
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }
        </script>

    </telerik:RadScriptBlock>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move,Maximize" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table width="100%">
        <tr>
            <td style="width: 450px">
                <table width="100%">
                    <tr>
                        <td class="label" style="width: 40px">Date</td>
                        <td style="width: 110px">
                            <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="100px"></telerik:RadDatePicker>
                        </td>
                        <td style="width: 40px">
                            <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                OnClick="btnRefresh_Click" ToolTip="Search" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button runat="server" ID="btnStartFromRegistration" Text="Registration Date" OnClick="btnStartFromRegistration_Click" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button runat="server" ID="btnLastVitalSign" Text="Last Vital Sign Date" OnClick="btnLastVitalSign_Click" />
                        </td>
                    </tr>
                    <td></td>
                </table>
            </td>
            <td>
                <fieldset width="100%">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 40px;">Name</td>
                            <td>:&nbsp;<asp:Label runat="server" ID="lblPatientName"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Sex</td>
                            <td style="width: 20px">:&nbsp;<asp:Label runat="server" ID="lblSex"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">DOB</td>
                            <td style="width: 90px">:&nbsp;<asp:Label runat="server" ID="lblBirthDate"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Age</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblAge"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <fieldset>
        <legend>Vital Sign Graph</legend>
        <telerik:RadHtmlChart runat="server" ID="chtVitalSign" Width="98%" Height="480" Transitions="true">
            <PlotArea>
                <Series>
                </Series>
                <XAxis MinValue="0" Type="Auto" MaxValue="24">
                    <MinorGridLines Visible="false"></MinorGridLines>
                    <MajorGridLines Visible="true"></MajorGridLines>
                    <LabelsAppearance Step="1"></LabelsAppearance>
                </XAxis>

            </PlotArea>
            <Legend>
                <Appearance Position="Top"></Appearance>
            </Legend>

        </telerik:RadHtmlChart>
    </fieldset>

    <%=CardexMonitoringHtml() %>
</asp:Content>
