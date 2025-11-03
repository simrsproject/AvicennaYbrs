<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ParamedicScheduleDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function openRadWindow() {
            var cboYear = $find("<%= cboPeriodYear.ClientID %>");
            var year = cboYear.get_text();
            var unitId = $find("<%= cboServiceUnitID.ClientID %>");
            var parId = $find("<%= cboParamedicID.ClientID %>");
            var oWnd = $find("<%= winSchedule.ClientID %>");
            oWnd.setUrl("OperationalTimeSelect.aspx?year=" + year + "&unitId=" + unitId._value + "&parId=" + parId._value);
            oWnd.show();
        }

        function onWinScheduleClientClose(oWnd) {
            //Jika apply di click
            var arg = oWnd.argument;
            if (arg) {
                var ajxPanel = $find("<%= AjaxPanel.ClientID %>");
                ajxPanel.ajaxRequest('cldSchedule');
            }
            oWnd = null;
        }

        function openGlobalRadWindow() {
            var cboYear = $find("<%= cboPeriodYear.ClientID %>");
            var unitId = $find("<%= cboServiceUnitID.ClientID %>");
            var parId = $find("<%= cboParamedicID.ClientID %>");
            var year = cboYear.get_text();
            var oWnd = $find("<%= winGlobalSchedule.ClientID %>");
            oWnd.setUrl("GlobalScheduleSelect.aspx?year=" + year + "&unitId=" + unitId._value + "&parId=" + parId._value);
            oWnd.show();
        }

        function onClientClose(oWnd) {
            //Jika apply di click
            var arg = oWnd.argument;
            if (arg) {
                var ajxPanel = $find("<%= AjaxPanel.ClientID %>");
                ajxPanel.ajaxRequest('rebind');
            }
            oWnd = null;
        }

    </script>

    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="330px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Change Schedule"
        OnClientClose="onWinScheduleClientClose" ID="winSchedule" ReloadOnShow="true">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="900px" Height="500px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Change Schedule"
        OnClientClose="onClientClose" ID="winGlobalSchedule" ReloadOnShow="true">
    </telerik:RadWindow>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPeriodYear" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPeriodYear" ValidationGroup="entry" runat="server" Width="80px"
                                AutoPostBack="true" OnSelectedIndexChanged="cboPeriodYear_SelectedIndexChanged" />
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvPeriodYear" runat="server" ErrorMessage="Year"
                                ValidationGroup="entry" ControlToValidate="cboPeriodYear" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                AutoPostBack="true" HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound"
                                OnItemsRequested="cboServiceUnitID_ItemsRequested" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvServiceUnitID" runat="server" ErrorMessage="Service Unit required."
                                ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                AutoPostBack="true" HighlightTemplatedItems="true" OnItemDataBound="cboParamedicID_ItemDataBound"
                                OnItemsRequested="cboParamedicID_ItemsRequested" OnSelectedIndexChanged="cboParamedicID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "ParamedicName") %>
                                    </b>
                                    <br />
                                    Physician ID :
                                    <%# DataBinder.Eval(Container.DataItem, "ParamedicID")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 10 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="50">
                            <asp:RequiredFieldValidator ID="rfvParamedicID" runat="server" ErrorMessage="Physician required."
                                ValidationGroup="entry" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvParamedicID" runat="server" ErrorMessage="Physician schedule in this year has registered"
                                ValidationGroup="entry" ControlToValidate="cboParamedicID" SetFocusOnError="True"
                                Width="20px" OnServerValidate="cvParamedicID_ServerValidate">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:CustomValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblExamDuration" runat="server" Text="Exam Duration (Minutes)"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtExamDuration" runat="server" Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvExamDuration" runat="server" ErrorMessage="Exam Duration required."
                                ValidationGroup="entry" ControlToValidate="txtExamDuration" SetFocusOnError="True"
                                Width="20px">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td class="entry">
                            <asp:Button ID="btnChangeSchedule" runat="server" Enabled="false" Text="Change Schedule"
                                OnClientClick="openRadWindow();return false;" />
                            <asp:Button ID="btnGlobalSchedule" runat="server" Enabled="false" Text="Setting From Schedule Template"
                                OnClientClick="openGlobalRadWindow();return false;" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
                    Orientation="HorizontalTop">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="SIMRS" PageViewID="pvAddress" Selected="True" />
                        <telerik:RadTab runat="server" Text="HFIS" PageViewID="pvFeeItemMtx" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
                    <telerik:RadPageView ID="pvAddress" runat="server" Selected="true">
                        <telerik:RadCalendar ID="cldSchedule" runat="server" OnDayRender="CustomizeDay" Height="100px"
                            Width="150px" Enabled="false" EnableMultiSelect="False" EnableViewState="False"
                            UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" MultiViewColumns="6"
                            MultiViewRows="2" EnableMonthYearFastNavigation="False" EnableNavigation="False"
                            ShowOtherMonthsDays="False" ShowRowHeaders="False" ShowDayCellToolTips="false">
                        </telerik:RadCalendar>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pvFeeItemMtx" runat="server">
                        <table width="100%">
                            <tr>
                                <td class="label">Senin</td>
                                <td class="label">Selasa</td>
                                <td class="label">Rabu</td>
                                <td class="label">Kamis</td>
                                <td class="label">Jumat</td>
                                <td class="label">Sabtu</td>
                                <td class="label">Minggu</td>
                                <td rowspan="2">
                                    <asp:Button runat="server" ID="btnUpdateHFIS" OnClick="btnUpdateHFIS_Click" Text="Update" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="cboSenin" runat="server" Width="100%" />
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSelasa" runat="server" Width="100%" />
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboRabu" runat="server" Width="100%" />
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboKamis" runat="server" Width="100%" />
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboJumat" runat="server" Width="100%" />
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboSabtu" runat="server" Width="100%" />
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cboMinggu" runat="server" Width="100%" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
            <td valign="top" align="center">
                <table>
                    <asp:Panel ID="pnlQuota" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="lblQuotaLimit" runat="server" Text="- Quota Limit -" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width: 5px"></td>
                            <td class="label" style="width: 10px">
                                <asp:Label ID="lblQuota" runat="server" Text="Direct"></asp:Label>
                            </td>
                            <td style="width: 5px"></td>
                            <td class="label" style="width: 10px">
                                <asp:Label ID="lblQuotaOnline" runat="server" Text="Online"></asp:Label>
                            </td>
                            <td class="entry"></td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblQuotaGeneral" runat="server" Text="General"></asp:Label>
                            </td>
                            <td style="width: 5px"></td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtQuota" runat="server" Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                            </td>
                            <td style="width: 5px"></td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtQuotaOnline" runat="server" Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                            </td>
                            <td class="entry"></td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblQuotaBpjs" runat="server" Text="BPJS"></asp:Label>
                            </td>
                            <td style="width: 5px"></td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtQuotaBpjs" runat="server" Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                            </td>
                            <td style="width: 5px"></td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtQuotaBpjsOnline" runat="server" Width="100px" NumberFormat-DecimalDigits="0" MinValue="0" />
                            </td>
                            <td class="entry"></td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
