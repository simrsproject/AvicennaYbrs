<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="VitalSignChartEws.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.VitalSignChartEws" %>

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
                width: 3.84%
            }

            /*            .tblgraph tr:nth-child(even) {
                background-color: #f2f2f2;
                height: 20px
            }*/

            .tblgraph tr {
                height: 20px
            }


            .tblgraph th {
                border: 1px solid #a9a9a9;
                padding-top: 6px;
                padding-bottom: 6px;
                text-align: center;
                /*                background-color: #4CAF50;
                color: white;*/
                background-color: #F0EFEF;
                color: black;
                width: 3.84%;
                height:18px;
            }


    </style>
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript">
            function entryQuestionRespond(md, trNo, regno, fid, unit) {
                var urlEntry = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/IntegratedNote/QuestionRespondEntry.aspx?md=' + md + '&trNo=' + trNo + '&regno=' + regno + '&patid=<%= PatientID %>&unit=' + unit + '&tid=' + fid + '&ccm=click&cet=<%=btnRefresh.ClientID %>';

                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(urlEntry);
                oWnd.show();

            }
            function winEntry_ClientClose(oWnd, args) {
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
                        case "click":
                            $("#" + arg.eventTarget).click();
                            break;
                    }
                }

            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winEntry" Width="800px" Height="600px" runat="server" OnClientClose="winEntry_ClientClose"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" VisibleStatusbar="false">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnPrevDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartAndScore" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtFromDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartAndScore" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNextDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartAndScore" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStartFromRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartAndScore" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnLastMonitoring">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartAndScore" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRefresh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartAndScore" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <div style="display: none">
        <asp:Button runat="server" ID="btnRefresh" OnClick="btnRefresh_Click" />
    </div>
    <table width="100%">
        <tr>
            <td style="width: 450px">
                <table width="100%">
                    <tr>
                        <td class="label" style="width: 40px">Date</td>
                        <td style="width: 20px">
                            <asp:ImageButton ID="btnPrevDate" runat="server" ImageUrl="~/Images/Toolbar/arrowleft_blue16.png"
                                OnClick="btnPrevDate_Click" ToolTip="Prev Date" />
                        </td>
                        <td style="width: 110px">
                            <telerik:RadDatePicker runat="server" ID="txtFromDate" Width="110px" AutoPostBack="true" OnSelectedDateChanged="txtFromDate_SelectedDateChanged"></telerik:RadDatePicker>
                        </td>
                        <td style="width: 20px">
                            <asp:ImageButton ID="btnNextDate" runat="server" ImageUrl="~/Images/Toolbar/arrowright_blue16.png"
                                OnClick="btnNextDate_Click" ToolTip="Next Date" />
                        </td>

                        <td style="width: 80px">
                            <asp:Button runat="server" ID="btnStartFromRegistration" Text="Reg. Date" OnClick="btnStartFromRegistration_Click" />
                        </td>
                        <td style="width: 100px">
                            <asp:Button runat="server" ID="btnLastMonitoring" Text="Last Monitoring" OnClick="btnLastMonitoring_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td>
                <fieldset width="100%">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 40px">Name</td>
                            <td>:&nbsp;<asp:Label runat="server" ID="lblPatientName" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">MRN</td>
                            <td style="width: 80px">:&nbsp;<asp:Label runat="server" ID="lblMedicalNo" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="label" style="width: 40px">Reg No</td>
                            <td style="width: 140px">:&nbsp;<asp:Label runat="server" ID="lblRegistrationNo" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Sex</td>
                            <td style="width: 20px">:&nbsp;<asp:Label runat="server" ID="lblSex" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">DOB</td>
                            <td style="width: 90px">:&nbsp;<asp:Label runat="server" ID="lblBirthDate" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="label" style="width: 30px">Age</td>
                            <td style="width: 100px">:&nbsp;<asp:Label runat="server" ID="lblAge" Font-Bold="true"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="pnlChartAndScore">
        <fieldset>
            <legend>Score Total</legend>
            <asp:Literal runat="server" ID="litEwsTotal"></asp:Literal>
        </fieldset>
        <asp:Panel runat="server" ID="pnlChart" Width="100%"></asp:Panel>
    </asp:Panel>
</asp:Content>
