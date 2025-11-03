<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="PrescriptionOrderHandlingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionOrderHandlingList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoEditUrl(pno, regno) {
                PauseAutoRefresh();
                var url = 'PrescriptionSalesDetail.aspx?md=edit&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=" + "&unit=" + '<%= Request.QueryString["unit"] %>' + "&loc=" + '<%= Request.QueryString["loc"] %>';
                window.location.href = url;
            }

            function gotoEdit2Url(pno, regno, soape) {
                if (soape == 'False') {
                    alert("SOAPE required.");
                } else {
                    PauseAutoRefresh();
                    var url = 'PrescriptionSalesDetail.aspx?md=edit&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=" + "&unit=" + '<%= Request.QueryString["unit"] %>' + "&loc=" + '<%= Request.QueryString["loc"] %>';
                    window.location.href = url;
                }
            }

            function gotoViewUrl(pno, regno) {
                PauseAutoRefresh();
                var url = 'PrescriptionSalesDetail.aspx?md=view&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=" + "&unit=" + '<%= Request.QueryString["unit"] %>' + "&loc=" + '<%= Request.QueryString["loc"] %>';
                window.location.href = url;
            }

            function gotoAddUrl(regno) {
                PauseAutoRefresh();
                var url = 'PrescriptionSalesDetail.aspx?md=new&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=" + "&unit=" + '<%= Request.QueryString["unit"] %>' + "&loc=" + '<%= Request.QueryString["loc"] %>';
                window.location.href = url;
            }

            function viewHistory(patientID) {
                PauseAutoRefresh();
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('PrescriptionHistoryDialog.aspx?pid=' + patientID + "&rt=" + '<%= Request.QueryString["rt"] %>');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function viewOutstandingDelivery(regNo, prescNo) {
                PauseAutoRefresh();
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('PrescriptionSalesDelivery.aspx?regno=' + regNo + "&pno=" + prescNo + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function openRpt() {
                PauseAutoRefresh();
                var oWnd = $find('<%=winPrint.ClientID%>');
                oWnd.SetUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
                return;
            }

            function tabStrip_OnClientTabSelected(sender, eventArgs) {
                var tab = eventArgs.get_tab();
                if (tab.get_text().toLowerCase().includes("order"))
                    ContinueAutoRefresh();
                else
                    PauseAutoRefresh();
            }

<%--            function RefreshOrderList() {
                PauseAutoRefresh
                document.getElementById('<%= btnRefreshOrder.ClientID %>').click();
            }--%>

            // AutoRefresh function
            var _autoRefreshTimer = null;
            function AutoRefresh(val) {
                clearTimeout(_autoRefreshTimer);
                if (val == 1) {
                    document.getElementById('<%= btnRefreshOrder.ClientID %>').click();
                }
            }

            function PauseAutoRefresh() {
                clearTimeout(_autoRefreshTimer);
            }

            function ContinueAutoRefresh() {
                clearTimeout(_autoRefreshTimer); // Non Activekan AutoRefresh
                if (document.getElementById("chkAutoRefreshOnOff").checked == true) {
                    var interval = <%=Temiang.Avicenna.Common.AppSession.Parameter.IntervalRefreshPrescriptionOrderList%>;
                    if (interval == "" || interval < 5) // Jika diisi detik tapi <5detik
                        interval = 5;

                    if (interval > 999) // Jika diisi miliseconds
                        interval = interval / 1000;

                    if (interval < 5) // Jika kurang <3detik pakai default 5detik saja
                        interval = 5;

                    _autoRefreshTimer = setTimeout('AutoRefresh(1)', interval * 1000);
                }
            }

            function StartUpAutoRefresh() {
                document.getElementById("chkAutoRefreshOnOff").checked = true;
                ContinueAutoRefresh();
                Sys.Application.remove_load(StartUpAutoRefresh);
            }

            var AjaxManager_OnResponseEnd = function (sender, eventArgs) {
                document.getElementById("lblLastRefresh").innerHTML = new Date().format("HH:mm:ss");
                ContinueAutoRefresh();
            }

            function toggleAutoRefresh(el) {
                //element.checked = !element.checked; //Test tidak boleh berubah checked nya
                if (el.checked == false) {
                    PauseAutoRefresh()
                }
                else {
                    ContinueAutoRefresh();
                }
            }

            Sys.Application.add_load(StartUpAutoRefresh);
        </script>

    </telerik:RadCodeBlock>
    <div style="display: none;">
        <asp:Button runat="server" ID="btnRefreshOrder" OnClick="btnRefreshOrder_Click" />
        <asp:HiddenField ID="hdnIsAutoRefreshToggleOn" runat="server" Value="1" />
    </div>

    <%--<asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick" Enabled="false"></asp:Timer>--%>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server" OnClientClose="ContinueAutoRefresh"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close" OnClientClose="ContinueAutoRefresh"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="ajxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnRefreshOrder">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterOrderDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterOrderNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID=" btnFilterOrderSRFloor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionCategory">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionCreatedBy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionSRFloor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDispensary">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDeliveryStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterKioskQueueNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdOrder">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdOrder" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBarcode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtBarcode" />
                    <telerik:AjaxUpdatedControl ControlID="lblBarcodeMsg" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true" OnClientTabSelected="tabStrip_OnClientTabSelected"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Order List" PageViewID="pgOrder" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Prescription List" PageViewID="pgList">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOrder" runat="server" Selected="true">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrderDate" runat="server" Text="Order Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px" />
                                </td>
                                <td >
                                    <asp:ImageButton ID="btnFilterOrderDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label4" runat="server" Text="Prescription No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterOrderNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                    <td style="width: 40%;" valign="top">
                        <table width="100%">
                            <tr runat="server" id="trPrescriptionCategory">
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionCategory" runat="server" Text="Prescription Category"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboSRPrescriptionCategory" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterPrescriptionCategory" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr runat="server" id="trPrescriptionCreatedBy">
                                <td class="label">
                                    <asp:Label ID="Label3" runat="server" Text="Created By"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboPrescriptionCreatedBy" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterPrescriptionCreatedBy" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr runat="server" id="trOrderSRFloor">
                                <td class="label">
                                    <asp:Label ID="lblOrderSRFloor" runat="server" Text="Floor"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboOrderSRFloor" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterOrderSRFloor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="bottom">
                        <table width="100%">
                            <tr>
                                <td style="text-align: right">Auto Refresh
                                </td>
                                <td style="width: 60px;">:&nbsp;
                                    <label class="switch">
                                        <input id="chkAutoRefreshOnOff" type="checkbox" name="chkAutoRefreshOnOff" checked="checked" onchange="toggleAutoRefresh(this)" />
                                        <span class="slider round"></span>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">Last Refresh
                                </td>
                                <td>:&nbsp;<label id="lblLastRefresh"></label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdOrder" runat="server" OnNeedDataSource="grdOrder_NeedDataSource"
                AllowSorting="true" ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Dispensary "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="edit1" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApproval").Equals(true) ? string.Empty :
                                        string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="edit2" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApproval").Equals(true) ? string.Empty :
                                        string.Format("<a href=\"#\" onclick=\"gotoEdit2Url('{0}','{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "Soape"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="NeedValidationByCasemix" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsGuarantorBpjsCasemix").Equals(false) ? string.Empty
                                                        : (DataBinder.Eval(Container.DataItem, "IsNeedValidationByCasemix").Equals(false) ? string.Format("<img src=\"../../../../Images/Toolbar/post_green_16.png\" border=\"0\" title=\"\" /></a>") :
                                        string.Format("<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" title=\"Some item(s) need validation by Casemix\" /></a>")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <%--<telerik:GridTemplateColumn UniqueName="NeedValidationByCasemix" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsNeedValidationByCasemix").Equals(false) ? string.Format("<img src=\"../../../../Images/Toolbar/post_green_16.png\" border=\"0\" title=\"\" /></a>") :
                                        string.Format("<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" title=\"Some item(s) need validation by Casemix\" /></a>")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PrescriptionDate"
                            HeaderText="Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="PrescriptionCategory" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" HeaderText="C"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSRPrescriptionCategory" ToolTip='<%#string.Format("{0}", DataBinder.Eval(Container.DataItem, "PrescriptionCategoryName").ToString()) %>'
                                    runat="server" Width="20px" ReadOnly="true" BackColor='<%# GetColorCategory(DataBinder.Eval(Container.DataItem,"PrescriptionCategoryBackColor")) %>'></asp:TextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="130px" HeaderText="Prescription No"
                            UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PrescriptionNo")%>&nbsp;
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCito")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Cito\" title=\"Cito\" />" : string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="160px" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>&nbsp;
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDebtAvailable")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Debt\" title=\"Debt\" />" : string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="Service Unit" HeaderStyle-Width="150px"
                            UniqueName="FromServiceUnit" SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="CreatedDateTime" HeaderText="Create Date/Time"
                            UniqueName="CreatedDateTime" SortExpression="CreatedDateTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgList" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptioDate" runat="server" Text="Prescription Date"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="100px" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnFilterPrescriptionDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr runat="server" id="trPrescriptionSRFloor">
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionSRFloor" runat="server" Text="Floor"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboPrescriptionSRFloor" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionSRFloor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblPrescriptionNo" runat="server" Text="Prescription No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPrescriptionNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                            <tr runat="server" id="trKioskQueueNo">
                                <td class="label">
                                    <asp:Label ID="lblKioskQueueNo" runat="server" Text="Queue No"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtKioskQueueNo" runat="server" Width="300px" MaxLength="20" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterKioskQueueNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblDispensary" runat="server" Text="Dispensary"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboDispensaryID" Width="300px" AllowCustomText="true"
                                        Filter="Contains">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionDispensary" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="trPrescriptionStatus">
                                <td class="label">
                                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboStatus" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterPrescriptionStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr runat="server" id="tr1">
                                <td class="label">
                                    <asp:Label ID="Label1" runat="server" Text="Delivery Status"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadComboBox runat="server" ID="cboDeliveryStatus" Width="300px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="" Text="" />
                                            <telerik:RadComboBoxItem Value="1" Text="Oustanding" />
                                            <telerik:RadComboBoxItem Value="2" Text="Complete" />
                                            <telerik:RadComboBoxItem Value="3" Text="Delivered" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:ImageButton ID="btnFilterDeliveryStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                        OnClick="btnFilter_Click" ToolTip="Search" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="Label2" runat="server" Text="Barcode"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rbBarcodeMode" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Search" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Set Complete" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Set Delivered" Value="4"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <telerik:RadTextBox ID="txtBarcode" runat="server" Width="150px" MaxLength="50" OnTextChanged="txtBarcode_TextChanged" AutoPostBack="true">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <label id="lblBarcodeMsg" runat="server" style="color: red;"></label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdPrescription" runat="server" OnNeedDataSource="grdPrescription_NeedDataSource"
                OnDetailTableDataBind="grdPrescription_DetailTableDataBind" AllowSorting="true"
                OnItemCommand="grdPrescription_ItemCommand" ShowStatusBar="true" AllowPaging="true"
                PageSize="15">
                <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Dispensary "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "OrderNo"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Delivery" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsHasPendingDelivery").Equals(false) ? string.Empty :
                                    string.Format("<a href=\"#\" onclick=\"viewOutstandingDelivery('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/consumption.png\" border=\"0\" title=\"Prescription Delivery\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "PrescriptionNo")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="PrescriptionDate"
                            HeaderText="Presc. Date" UniqueName="PrescriptionDate" SortExpression="PrescriptionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PrescriptionNo" HeaderText="Prescription No"
                            UniqueName="PrescriptionNo" SortExpression="PrescriptionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="OrderNo" HeaderText="Order No"
                            UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="MedicalNo" HeaderText="Medical No"
                            UniqueName="MedicalNo" SortExpression="MedicalNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                            UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="FromServiceUnit" HeaderText="Service Unit" HeaderStyle-Width="150px"
                            UniqueName="FromServiceUnit" SortExpression="FromServiceUnit" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Proceed"
                            UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="65px" DataField="IsApproval" HeaderText="Approved"
                            UniqueName="IsApproval" SortExpression="IsApproval" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPaid" HeaderText="Paid"
                            UniqueName="IsPaid" SortExpression="IsPaid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsProceedByPharmacist"
                            HeaderText="Proceed" UniqueName="IsProceedByPharmacist" SortExpression="IsProceedByPharmacist"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPrinted" HeaderText="Printed"
                            UniqueName="IsPrinted" SortExpression="IsPrinted" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="KioskQueueNo" HeaderText="Queue No" HeaderStyle-Width="70px"
                            UniqueName="KioskQueueNo" SortExpression="KioskQueueNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <div <%#((bool)DataBinder.Eval(Container.DataItem, "IsVoid"))==false ?"style=\"display:block\"":"style=\"display:none\""%>>
                                    <asp:LinkButton ID="lbStatus" runat="server" CommandArgument='<%#string.Format("{0}|{1}|{2}", DataBinder.Eval(Container.DataItem, "PrescriptionNo"), (((int)DataBinder.Eval(Container.DataItem, "Status")) == 3 ? "" : ((int)DataBinder.Eval(Container.DataItem, "Status")) == 0 ? "inprogress" : ((int)DataBinder.Eval(Container.DataItem, "Status")) == 2 ? "deliver":"complete"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>'
                                        CommandName="setStatus" ToolTip='<%#string.Format("Status: {0}", ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "Delivered" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "Completed" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "In Progress" : "Outstanding")))) %>'
                                        OnClientClick='<%#string.Format("{0}", (int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "alert(\"Prescription has been delivered\"); return false;":((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "return confirm(\"Are you sure want to set as delivered?\");" :(int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "return confirm(\"Are you sure want to set as completed?\");":"return confirm(\"Are you sure want to set as in progress?\");")) %>'>
                                    <%#string.Format("<img style=\"border: 0px; text-align:center; vertical-align: middle;\" src=\"../../../../Images/Toolbar/{0} \" />",
                                        ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "post16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "post_green_16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "post_yellow_16.png" : "Scheduler16.png"))))%>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <a href="#" onclick="viewHistory('<%# DataBinder.Eval(Container.DataItem, "PatientID") %>'); return false;">
                                    <img src="../../../../Images/Toolbar/details16.png" border="0" alt="Prescription List" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintStickerPasien" HeaderStyle-Width="35px"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintStickerPasien" runat="server" CommandName="PrintPatientSticker"
                                    ToolTip='Patient Sticker' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'>
                                    <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="Detail" DataKeyNames="SequenceNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Seq. No" UniqueName="SequenceNo"
                                    HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsCompound" HeaderText="C"
                                    UniqueName="IsCompound" SortExpression="IsCompound" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="ParentNo" HeaderText="Header" UniqueName="ParentNo"
                                    HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderStyle-Width="35px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblR" runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsRFlag")) ? @"R/" : "" %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Item Name" UniqueName="TemplateItemName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemName" runat="server" Text='<%# GetItemName(DataBinder.Eval(Container.DataItem, "IsRFlag"), DataBinder.Eval(Container.DataItem, "ItemName")) %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Numero" UniqueName="TemplateItemName3" HeaderStyle-Width="60px"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "PrescriptionQty") %><br />
                                        (<%# DataBinder.Eval(Container.DataItem, "ResultQty") %>)
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Unit" UniqueName="TemplateItemName4" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCompound")) ? DataBinder.Eval(Container.DataItem, "EmbalaceLabel") : DataBinder.Eval(Container.DataItem, "SRItemUnit")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ConsumeQty" HeaderText="Dosing"
                                    UniqueName="ConsumeQty" SortExpression="ConsumeQty" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRDosageUnit" HeaderText="Unit"
                                    UniqueName="SRDosageUnit" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="SRConsumeMethodName" HeaderText="Consume Method"
                                    UniqueName="SRConsumeMethodName" SortExpression="SRConsumeMethodName" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RecipeAmount" HeaderText="Recipe"
                                    UniqueName="RecipeAmount" SortExpression="RecipeAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                                    UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LineAmount" HeaderText="Total"
                                    UniqueName="LineAmount" SortExpression="LineAmount" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
