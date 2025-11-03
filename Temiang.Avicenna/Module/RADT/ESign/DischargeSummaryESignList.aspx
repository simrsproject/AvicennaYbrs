<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="DischargeSummaryESignList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.ESign.DischargeSummaryESignList" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <script type="text/javascript" language="javascript">
            function tbMenu_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "process":
                        if (confirm('Are you sure to process the selected registration?'))
                            ESignLogin();
                        break;
                }
            }

            function CloseStatus(registrationNo) {
                if (confirm("Close this pending PCare data?"))
                    __doPostBack("<%= grdList.UniqueID %>", "closestatus|" + registrationNo);
            }

            function ESignLogin() {
                var oWnd = $find("<%= winEsignLogin.ClientID %>");
                oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/Reports/ESign/ESignLogin.aspx");
                oWnd.show();

                return false;
            }
            function winEsignLogin_OnClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.esignkey != null) {
                    __doPostBack("<%= grdList.UniqueID %>", "esign_" + oWnd.argument.esignkey);
                }
            }
            function cboRegistrationType_OnClientSelectedIndexChanged() {
                var obj = {};
                var combo = window.$find("<%= cboRegistrationType.ClientID %>");
                obj.registrationType = combo.get_selectedItem().get_value();

                obj.userID = '<%=AppSession.UserLogin.UserID%>';
                obj.userType = '<%=AppSession.UserLogin.SRUserType%>';
                $.ajax({
                    url: "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrWebService.asmx/ServiceUnitList",
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


            function entryResumeMedis(regno, fregno, parid) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                if ('<%=AppSession.Parameter.HealthcareInitial%>' === 'RSMMP')
                    url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/RSMMP/ResumeMedisRichTextInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                else
                    url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisRichTextInPatientEntry.aspx?mod=view&regno=' + regno + '&fregno=' + fregno + '&patid=<%= PatientID %>&parid=' + parid;
                openWinEntryMaxHeight(url, 1200);
            }
            function openESignHist(regno) {
                var url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=esign&programid=<%= DischargeSummaryReportID %>&regno=" + regno
                openWinEntryMaxHeight(url, 1200);
            }

            function openWinEntryMaxHeight(url, width) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);


                if (!(url.includes("&rt=") || url.includes("?rt=")))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                openWindow(url, width, height - 40);
            }

            function openWindow(url, width, height) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
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
            <telerik:AjaxSetting AjaxControlID="btnFilterIncludeNotInBed">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="ajaxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
    <telerik:RadAjaxLoadingPanel ID="ajaxLoadingPanel" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindow ID="winEsignLogin" Animation="None" Width="400px" Height="350px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="True"
        Modal="true" OnClientClose="winEsignLogin_OnClientClose" Title="ESign Login">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winDialog" Animation="None" Width="400px" Height="350px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="True"
        Modal="true">
    </telerik:RadWindow>
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
                    <fieldset style="width: 500px;">
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
                <td></td>
            </tr>
        </table>
    </cc:CollapsePanel>

    <telerik:RadToolBar ID="tbMenu" runat="server" Width="100%" OnClientButtonClicking="tbMenu_OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process Selected Reg." Value="process" ImageUrl="~/Images/Toolbar/refresh16.png"
                HoveredImageUrl="~/Images/Toolbar/refresh16_h.png" DisabledImageUrl="~/Images/Toolbar/refresh16_d.png" />
        </Items>
    </telerik:RadToolBar>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true" OnItemCommand="grdList_ItemCommand">
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
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="40px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "IsESigned") %>'></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridTemplateColumn UniqueName="RegistrationDate" HeaderText="Reg. Date">
                    <ItemTemplate>
                        <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(AppConstant.DisplayFormat.DateShortMonth) %><br />
                        <%#DataBinder.Eval(Container.DataItem, "RegistrationTime") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="RegistrationQue" HeaderText="Que"
                    UniqueName="RegistrationQue" SortExpression="RegistrationQue" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderStyle-Width="40px" UniqueName="ExternalQueNo" HeaderText="Que" SortExpression="ExternalQueNo">
                    <ItemTemplate>
                        <%#String.IsNullOrWhiteSpace(DataBinder.Eval(Container.DataItem, "ExternalQueNo").ToString()) ? string.Format("{0}",DataBinder.Eval(Container.DataItem, "RegistrationQue")) : string.Format("{0}",DataBinder.Eval(Container.DataItem, "ExternalQueNo")) %>
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
                        <span style="font-size: 12pt;">
                            <%# string.Format("<a href=\"#\" onclick=\"entryResumeMedis('{0}','{1}','{2}'); return false;\">{3} {4}</a>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                            DataBinder.Eval(Container.DataItem, "FromRegistrationNo"),
                                            DataBinder.Eval(Container.DataItem, "ParamedicID"),
                                            DataBinder.Eval(Container.DataItem, "SalutationName"),
                                DataBinder.Eval(Container.DataItem, "PatientName"))%>
                        </span>
                        <br />
                        <%# string.Format("{0}Y {1}M {2}D", Helper.GetAgeInYear (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), Helper.GetAgeInMonth(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), Helper.GetAgeInDay(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date))) %>
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
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="RoomName" HeaderText="Room / Bed" SortExpression="RoomName">
                    <ItemTemplate>
                        R: &nbsp; <%#DataBinder.Eval(Container.DataItem, "RoomName")  %><br />
                        B: &nbsp;<%#DataBinder.Eval(Container.DataItem, "BedID") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Menu" HeaderText=" ">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"entryResumeMedis('{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"MedHist\" title=\"Discharge Summary Entry\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                            DataBinder.Eval(Container.DataItem, "FromRegistrationNo"),
                                            DataBinder.Eval(Container.DataItem, "ParamedicID"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="32px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Menu2" HeaderText=" ">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MdsLastUpdateByUserID") == null ? 
                                string.Empty: 
                                string.Format("<a href=\"#\" onclick=\"javascript:openESignHist('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Gyssens\" title=\"ESigned PDF\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="32px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Mds" HeaderText="Medical Discharge Summary">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "MdsLastUpdateByUserID")!=null ? string.Format("{0} by {1}",DataBinder.Eval(Container.DataItem, "MdsDischargeDate"),DataBinder.Eval(Container.DataItem, "MdsLastUpdateByUserID")):string.Empty %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SignedFilePath" HeaderText="Signed File Path" UniqueName="SignedFilePath"
                    SortExpression="SignedFilePath">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="MdsErrMsg" HeaderText="ESign Log Error">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "ErrorMessageShort")  %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn HeaderText="RowSource" UniqueName="RowSource"
                    SortExpression="RowSource">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
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
</asp:Content>

