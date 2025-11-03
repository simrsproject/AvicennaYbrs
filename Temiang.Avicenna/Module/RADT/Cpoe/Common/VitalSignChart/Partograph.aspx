<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="Partograph.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Cpoe.Partograph" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartCtl.ascx" TagPrefix="uc1" TagName="VitalSignChartCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


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
                height: 30px;
                width: 2.17%;
            }

            .tblgraph tr:nth-child(even) {
                background-color: #f2f2f2;
                height: 30px;
            }

            .tblgraph th {
                border: 1px solid #a9a9a9;
                padding-top: 2px;
                padding-bottom: 2px;
                text-align: center;
                background-color: #F0EFEF;
                color: black;
                height: 20px;
                width: 2.17%;
            }


        .ctgBg01 {
            background-image: url('data:image/png;base64,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')
        }

        .ctgBg02 {
            background-image: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACsAAAAsCAIAAABzOtYvAAAB8ElEQVRYhcWYwdGrIBRGLzNZxbVpAFvQJrSMUIjWEruAIkwHoQG3buC+BQ5BdJA/Ko+lOHO+73pyF4G+73HrtG1LCMmyTAixvlVKPZ9PAHg8Hu/3e/3CNE11XQNAURRSyvUL4ziWZQkAEMZzzte3WmuDz/N8GIYj+KqqNhLstmeMnYUfx9FPED/8U/CIuEjQdV0Ar7U+t715+E0Q054QcmL7RQLbPoF6Ln5OkFg97xbSq+cnCA//ePuqqgJ4KSWEh3+FevZ8Ph9KKVzXPoyXUlJKt7fyKertDt/g67r2E1j85cMHaJpmmqZFgjTquXh0d2IC9db4b4KU6rn4OUFK9Tw8IkIC9aSURVFs4hERIttTSo//8NZ4NFs5/be3ZxiGUPumaS769haf5zkk2Hoh/OZWjml/RD0XzxjzE6RszxhTSi0SpFHP4rXW6G7lePXKsjw+fKWUeQjx7XeHb/Dh4RNCXPycILF6ZvjfBBa/u/UCw/+Tet4t7C7dSPVi8F77OcEp6v3WHhGFEP9BPXs451mWQXr1XDwhZGMrX60eIgohDL5tWz/BuUs33L7rOvT+P0igHuf8fr9b/CJBMvVc/DdBPP64ei5+TpBYPe8Kzhr+D+3nBOnV8xMcx8eotx6+OX3fn9P+h+Ej4uv1ut1u/wCe7JjcrP0KmgAAAABJRU5ErkJggg==')
        }

        .ctgBg03 {
            background-color: gray;
        }

        /* tooltip */
        .tooltip {
            position: relative;
            display: inline-block;
            margin: auto;
            width: 60%;
            padding: 5px;
        }

            .tooltip .tooltiptext {
                visibility: hidden;
                width: 120px;
                background-color: #ff6600;
                color: #fff;
                text-align: left;
                border-radius: 6px;
                padding: 5px 5px;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -60px;
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

        <script type="text/jscript">
            function PrintChartArea() {
                var elem = '<%=pnlChartArea.ClientID %>';
                var mywindow = window.open('', 'PRINT', 'height=400,width=600');

                mywindow.document.write('<html><head><title>' + document.title + '</title>');
                mywindow.document.write('</head><body >');
                mywindow.document.write('<h1>' + document.title + '</h1>');
                mywindow.document.write(document.getElementById(elem).innerHTML);
                mywindow.document.write('</body></html>');

                mywindow.document.close(); // necessary for IE >= 10
                mywindow.focus(); // necessary for IE >= 10*/

                mywindow.print();
                mywindow.close();

                return true;
            }

            function entryQuestionRespond(md, trNo, regno, fid, unit) {
                var urlEntry = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/IntegratedNote/QuestionRespondEntry.aspx?md=' + md + '&trNo=' + trNo + '&regno=' + regno + '&patid=<%= PatientID %>&unit=' + unit + '&tid=' + fid + '&ccm=click&cet=<%=btnRefresh.ClientID %>';

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(urlEntry);
                oWnd.show();

            }
            function winDialog_ClientClose(oWnd, args) {
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

            function applyDivEntryHeightMax() {
                var height =
                    ((window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) - 90) + "px";
                document.getElementById("<%= divEntry.ClientID %>").style.maxHeight = height;
            }
            window.onload = function () {
                applyDivEntryHeightMax();
            }
            window.onresize = function () {
                applyDivEntryHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyDivEntryHeightMax();
            });
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="800px" Height="600px" runat="server" OnClientClose="winDialog_ClientClose"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" VisibleStatusbar="false">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnPrevDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartArea" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtFromDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartArea" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNextDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartArea" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnStartFromRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartArea" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnLastMonitoring">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartArea" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtFromDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnRefresh">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlChartArea" LoadingPanelID="fw_ajxLoadingPanel" />
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
                            <td class="label" style="width: 40px">Gender</td>
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
            <td>
                <asp:Button runat="server" ID="btnPrint" Text="Print" OnClientClick="PrintChartArea();return false;" /></td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="pnlChartArea">
        <fieldset>
            <asp:Literal runat="server" ID="litMenu"></asp:Literal>
        </fieldset>
        <div id="divEntry" runat="server" style="width: 100%; overflow-y: scroll;">

            <fieldset>
                <legend>
                    <strong>Fetal Hearth Rate</strong>
                </legend>
                <telerik:RadHtmlChart runat="server" ID="chtFetalHearthRate" Width="100%" Height="300" Transitions="true" PlotArea-YAxis-LabelsAppearance-TextStyle-Padding="0">
                    <Appearance>
                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                    </Appearance>
                    <ChartTitle Text="">
                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top" Visible="false">
                        </Appearance>
                    </ChartTitle>
                    <Legend>
                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                        </Appearance>
                        <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="None" Type="Log"
                            Reversed="false" Justified="true">
                            <Items>
                            </Items>
                            <LabelsAppearance DataFormatString="{0:HH:mm}" RotationAngle="0" Skip="0">
                            </LabelsAppearance>
                            <TitleAppearance Position="Center" RotationAngle="0" Text="">
                            </TitleAppearance>
                            <MajorGridLines Visible="true" />
                            <MinorGridLines Visible="false" />
                        </XAxis>
                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                            MaxValue="185" MinorTickSize="1" MinorTickType="None" MinValue="80" Reversed="false"
                            Step="10">
                            <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1" Position="Start">
                            </LabelsAppearance>
                            <TitleAppearance Position="Center" RotationAngle="0" Text="Fetal\nHearth Rate">
                            </TitleAppearance>
                            <MinorGridLines Visible="false" />
                        </YAxis>
                        <Series>
                            <telerik:LineSeries Name="FetalHR" MissingValues="Interpolate" VisibleInLegend="false">
                                <Appearance>
                                    <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                </Appearance>
                                <LabelsAppearance DataFormatString="{0}" Position="Above">
                                </LabelsAppearance>
                                <LineAppearance Width="1" />
                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                    BorderWidth="2"></MarkersAppearance>
                                <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                <SeriesItems>
                                </SeriesItems>
                            </telerik:LineSeries>

                        </Series>
                    </PlotArea>
                </telerik:RadHtmlChart>

            </fieldset>
            <br />
            <fieldset>
                <legend>
                    <strong>Liquor and Moulding</strong>
                </legend>
                <asp:Literal runat="server" ID="litLiquorMoulding"></asp:Literal>
            </fieldset>
            <br />
            <fieldset>
                <legend>
                    <strong>Cervix Status</strong>
                </legend>

                <telerik:RadHtmlChart runat="server" ID="chtCervix" Width="100%" Height="300" Transitions="true" PlotArea-YAxis-LabelsAppearance-TextStyle-Padding="2">
                    <Appearance>
                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                    </Appearance>
                    <ChartTitle Text="">
                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top" Visible="false">
                        </Appearance>
                    </ChartTitle>
                    <Legend>
                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                        </Appearance>
                        <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="None" Type="Log"
                            Reversed="false" Justified="true">
                            <Items>
                            </Items>
                            <LabelsAppearance DataFormatString="{0:HH:mm}" RotationAngle="0" Skip="0">
                            </LabelsAppearance>
                            <TitleAppearance Position="Center" RotationAngle="0" Text="">
                            </TitleAppearance>
                            <MajorGridLines Visible="true" />
                            <MinorGridLines Visible="false" />
                        </XAxis>
                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                            MaxValue="10.5" MinorTickSize="1" MinorTickType="None" MinValue="0" Reversed="false"
                            Step="1">
                            <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1" Position="Start">
                            </LabelsAppearance>
                            <TitleAppearance Position="Center" RotationAngle="0" Text="Cervix (Plot X)\nDescent of Head (Plot O)">
                            </TitleAppearance>
                            <MinorGridLines Visible="false" />
                        </YAxis>
                        <Series>
                            <telerik:LineSeries Name="Head" MissingValues="Interpolate" VisibleInLegend="false">
                                <Appearance>
                                    <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                </Appearance>
                                <LabelsAppearance DataFormatString="{0}" Position="Above">
                                </LabelsAppearance>
                                <LineAppearance Width="1" />
                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                    BorderWidth="2"></MarkersAppearance>
                                <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                <SeriesItems>
                                </SeriesItems>
                            </telerik:LineSeries>
                            <telerik:LineSeries Name="Cervix" MissingValues="Interpolate" VisibleInLegend="false">
                                <Appearance>
                                    <FillStyle BackgroundColor="#2d6b99"></FillStyle>
                                </Appearance>
                                <LabelsAppearance DataFormatString="{0}" Position="Above">
                                </LabelsAppearance>
                                <LineAppearance Width="1" />
                                <MarkersAppearance MarkersType="Cross" BackgroundColor="#2d6b99" Size="8" BorderColor="#2d6b99"
                                    BorderWidth="2"></MarkersAppearance>
                                <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                <SeriesItems>
                                </SeriesItems>
                            </telerik:LineSeries>

                            <telerik:LineSeries Name="Alert" MissingValues="Interpolate" VisibleInLegend="true">
                                <Appearance>
                                    <FillStyle BackgroundColor="Orange"></FillStyle>
                                </Appearance>
                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                <LineAppearance Width="3" DashType="Dot" />
                                <MarkersAppearance Visible="false"></MarkersAppearance>
                                <TooltipsAppearance Visible="false"></TooltipsAppearance>
                                <SeriesItems>
                                    <telerik:CategorySeriesItem Y="4"></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem Y="10"></telerik:CategorySeriesItem>
                                </SeriesItems>
                            </telerik:LineSeries>

                            <telerik:LineSeries Name="Action" MissingValues="Interpolate" VisibleInLegend="true">
                                <Appearance>
                                    <FillStyle BackgroundColor="Red"></FillStyle>
                                </Appearance>
                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                <LineAppearance Width="3" DashType="Dot" />
                                <MarkersAppearance Visible="false"></MarkersAppearance>
                                <TooltipsAppearance Visible="false"></TooltipsAppearance>
                                <SeriesItems>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem Y="4"></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem></telerik:CategorySeriesItem>
                                    <telerik:CategorySeriesItem Y="10"></telerik:CategorySeriesItem>
                                </SeriesItems>
                            </telerik:LineSeries>
                        </Series>
                    </PlotArea>
                </telerik:RadHtmlChart>

                <asp:Literal runat="server" ID="litCervix"></asp:Literal>
            </fieldset>
            <br />
            <fieldset>
                <legend>
                    <strong>Contraction per 10 mins</strong>
                </legend>
                <asp:Literal runat="server" ID="litContraction"></asp:Literal>
            </fieldset>

            <br />
            <fieldset>
                <legend>
                    <strong>Oxytocin U/L</strong>
                </legend>
                <asp:Literal runat="server" ID="litOxytocin"></asp:Literal>
            </fieldset>
            <fieldset>
                <legend>
                    <strong>Drugs Given And IV Fluids</strong>
                </legend>
                <asp:Literal runat="server" ID="litDrugAndFluid"></asp:Literal>
                <telerik:RadHtmlChart runat="server" ID="chtPulseBp" Width="100%" Height="300" Transitions="true" PlotArea-YAxis-LabelsAppearance-TextStyle-Padding="0">
                    <Appearance>
                        <FillStyle BackgroundColor="Transparent"></FillStyle>
                    </Appearance>
                    <ChartTitle Text="">
                        <Appearance Align="Center" BackgroundColor="Transparent" Position="Top" Visible="false">
                        </Appearance>
                    </ChartTitle>
                    <Legend>
                        <Appearance BackgroundColor="Transparent" Position="Bottom">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                        </Appearance>
                        <XAxis AxisCrossingValue="0" Color="black" MajorTickType="Outside" MinorTickType="None" Type="Log"
                            Reversed="false" Justified="true">
                            <Items>
                            </Items>
                            <LabelsAppearance DataFormatString="{0:HH:mm}" RotationAngle="0" Skip="0">
                            </LabelsAppearance>
                            <TitleAppearance Position="Center" RotationAngle="0" Text="">
                            </TitleAppearance>
                            <MajorGridLines Visible="true" />
                            <MinorGridLines Visible="false" />
                        </XAxis>
                        <YAxis AxisCrossingValue="0" Color="black" MajorTickSize="1" MajorTickType="Outside"
                            MaxValue="190" MinorTickSize="1" MinorTickType="None" MinValue="60" Reversed="false"
                            Step="10">
                            <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1" Position="Start">
                            </LabelsAppearance>
                            <TitleAppearance Position="Center" RotationAngle="0" Text="Pulse\nBlood Preasure <--->">
                            </TitleAppearance>
                            <MinorGridLines Visible="false" />
                        </YAxis>
                        <Series>
                            <telerik:LineSeries Name="Pulse" MissingValues="Interpolate" VisibleInLegend="false">
                                <Appearance>
                                    <FillStyle BackgroundColor="#5ab7de"></FillStyle>
                                </Appearance>
                                <LabelsAppearance DataFormatString="{0}" Position="Above">
                                </LabelsAppearance>
                                <LineAppearance Width="1" />
                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="8" BorderColor="#5ab7de"
                                    BorderWidth="2"></MarkersAppearance>
                                <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                <SeriesItems>
                                </SeriesItems>
                            </telerik:LineSeries>
                            <telerik:LineSeries Name="BPS" MissingValues="Interpolate" VisibleInLegend="false">
                                <Appearance>
                                    <FillStyle BackgroundColor="Brown"></FillStyle>
                                </Appearance>
                                <LabelsAppearance DataFormatString="{0}" Position="Above">
                                </LabelsAppearance>
                                <LineAppearance Width="1" />
                                <MarkersAppearance MarkersType="Square" BackgroundColor="White" Size="8" BorderColor="Brown"
                                    BorderWidth="2"></MarkersAppearance>
                                <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                <SeriesItems>
                                </SeriesItems>
                            </telerik:LineSeries>
                            <telerik:LineSeries Name="BPD" MissingValues="Interpolate" VisibleInLegend="false">
                                <Appearance>
                                    <FillStyle BackgroundColor="DarkGreen"></FillStyle>
                                </Appearance>
                                <LabelsAppearance DataFormatString="{0}" Position="Above">
                                </LabelsAppearance>
                                <LineAppearance Width="1" />
                                <MarkersAppearance MarkersType="Triangle" BackgroundColor="White" Size="8" BorderColor="DarkGreen"
                                    BorderWidth="2"></MarkersAppearance>
                                <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                <SeriesItems>
                                </SeriesItems>
                            </telerik:LineSeries>
                            <%--                            <telerik:RangeColumnSeries Name="BP" VisibleInLegend="false">
                                <Appearance>
                                    <FillStyle BackgroundColor="#2d6b99"></FillStyle>
                                </Appearance>
                                <SeriesItems>
                                </SeriesItems>
                                <LabelsAppearance Visible="true">
                                    <FromLabelsAppearance>
                                        <ClientTemplate>#=value.from#</ClientTemplate>
                                    </FromLabelsAppearance>
                                    <ToLabelsAppearance>
                                        <ClientTemplate>#=value.to#</ClientTemplate>
                                    </ToLabelsAppearance>
                                </LabelsAppearance>
                                <TooltipsAppearance Color="White">
                                    <ClientTemplate>#= value.to #/#= value.from #</ClientTemplate>
                                </TooltipsAppearance>
                            </telerik:RangeColumnSeries>--%>
                        </Series>
                    </PlotArea>
                </telerik:RadHtmlChart>

            </fieldset>
            <br />
            <fieldset>
                <legend>
                    <strong>Temperature</strong>
                </legend>
                <asp:Literal runat="server" ID="litSuhu"></asp:Literal>
            </fieldset>
            <br />
            <fieldset>
                <legend>
                    <strong>Urine</strong>
                </legend>
                <asp:Literal runat="server" ID="litUrine"></asp:Literal>
            </fieldset>

            <table>
                <tr>
                    <td colspan="2"></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
