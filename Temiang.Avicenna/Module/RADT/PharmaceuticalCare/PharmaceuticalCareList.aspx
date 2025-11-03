<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="PharmaceuticalCareList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.PharmaceuticalCareList" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <style>
            .noti_homepres {
                position: relative; /*border:1px solid blue;*/ /* This is just to show you where the container ends */
                cursor: pointer;
                background: transparent url('<%= Helper.UrlRoot() %>/Images/Toolbar/home_green16.png') no-repeat 2px 50%;
                padding: 0 0 0 18px;
            }

            .noti_recon_adm {
                position: relative;
                cursor: pointer;
                background: transparent url('<%= Helper.UrlRoot() %>/Images/Toolbar/drugs16.png') no-repeat 2px 50%;
                padding: 0 0 0 18px;
            }

            .noti_recon_trf {
                position: relative;
                cursor: pointer;
                background: yellow url('<%= Helper.UrlRoot() %>/Images/Toolbar/drugs16.png') no-repeat 2px 50%;
                padding: 0 0 0 18px;
            }

            .noti_recon_dcg {
                position: relative;
                cursor: pointer;
                background: greenyellow url('<%= Helper.UrlRoot() %>/Images/Toolbar/drugs16.png') no-repeat 2px 50%;
                padding: 0 0 0 18px;
            }
        </style>
        <script type="text/javascript">
            function openVitalSignChartEws(patid, regno, fregno, date) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/Common/VitalSignChart/VitalSignChartEws.aspx?patid=' + patid + '&regno=' + regno + '&fregno=' + fregno + '&date=' + date;
                openWindowMaxScreen(url);
            }
            function openMedicationHist(patid, regno, fregno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?prgid=<%= ProgramID %>&patid=' + patid + '&regno=' + regno + '&fregno=' + fregno;
                openWindowMaxScreen(url);
            }

            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%= Helper.UrlRoot() %>/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate);
                oWnd.show();
            }
            function openVisit(patid, regNo) {
                openWindowMaxScreen("Visit/RegistrationVisitHist.aspx?prgid=<%= ProgramID %>&patid=" + patid + "&regNo=" + regNo)
            }
            function openCounseling(patid, regNo) {
                openWindowMaxScreen("Counseling/RegistrationCounselingHist.aspx?prgid=<%= ProgramID %>&patid=" + patid + "&regNo=" + regNo)
            }
            function openPto(patid, regNo) {
                openWindowMaxScreen("Pto/RegistrationPtoHist.aspx?prgid=<%= ProgramID %>&patid=" + patid + "&regNo=" + regNo)
            }

<%--           function openReconciliation(rectype, patid, regno, fregno) {
                var idupd = "noti_" + rectype + "_" + regno;
                var url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveReconciliaton.aspx?mod=view&prgid=<%= ProgramID %>&patid=" + patid + "&regno=" + regno + "&fregno=" + fregno + "&rectype=" + rectype + "&idupd=" + idupd;
                openWindowMaxScreen(url);
            }--%>

            function openReconciliation(rectype, patid, regno, fregno) {
                var idupd = "noti_" + rectype + "_" + regno;
                var url = "MedicationRecon/MedicationReconEntry.aspx?mod=view&prgid=<%= ProgramID %>&patid=" + patid + "&regno=" + regno + "&fregno=" + fregno + "&rectype=" + rectype + "&idupd=" + idupd;
                openWindowMaxScreen(url);
            }

            function openMedicationConsume(stat, patid, regno, fregno) {
                var url = "<%=Helper.UrlRoot()%>/Module/RADT/MedicationStatus/MedicationStatusPerPatient.aspx?wintype=max&stat=" + stat + "&prgid=<%= ProgramID %>&patid=" + patid + "&regno=" + regno + "&fregno=" + fregno;
                openWindowMaxScreen(url);
            }

            function openHomePrescription(patid, regno, fregno) {
                var idupd = "noti_hp_" + regno;
                var url = "<%=Helper.UrlRoot()%>/Module/RADT/PharmaceuticalCare/HomePrescription/HomePrescriptionEntry.aspx?mod=view&prgid=<%= ProgramID %>&patid=" + patid + "&regno=" + regno + "&fregno=" + fregno + "&idupd=" + idupd;
                openWindowMaxScreen(url);
            }

            function openDrugObservation(patid, regNo) {
                openWindowMaxScreen("DrugObservation/DrugObservationHist.aspx?prgid=<%= ProgramID %>&patid=" + patid + "&regNo=" + regNo)
            }
            function openEso(patid, regNo) {
                openWindowMaxScreen("Eso/EsoHist.aspx?prgid=<%= ProgramID %>&patid=" + patid + "&regNo=" + regNo)
            }
            function openMedicationReceiveOpt(regNo, patid) {
                var url =
                    '<%=Page.ResolveUrl("~/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveOpt.aspx")%>';

                url = url + "?prgid=<%= ProgramID %>&regNo=" + regNo + "&patid=" + patid;

                var oWnd = $find("<%= winMedicationReceiveOpt.ClientID %>");
                oWnd.setUrl(url);
                oWnd.center();
                oWnd.show();

            }


            function winMedicationReceiveOpt_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    openWindowMaxScreen(oWnd.argument.url);
                }
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
                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }

            function openWindow(url, width, height) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();
                oWnd.show();
            }

            function cboRegistrationType_OnClientSelectedIndexChanged() {
                var obj = {};
                var combo = window.$find("<%= cboRegistrationType.ClientID %>");
                obj.registrationType = combo.get_selectedItem().get_value();

                obj.userID = '<%=AppSession.UserLogin.UserID%>';
                obj.userType = '<%=AppSession.UserLogin.SRUserType%>';
                $.ajax({
                    url: "<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrWebService.asmx/ServiceUnitList",
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {

                        var retVal = decodeURI(data.d);
                        if (retVal.length > 1) {
                            var list = retVal.split('|');
                            var cbo = window.$find("<%= cboServiceUnitID.ClientID %>");
                            cbo.clearItems();
                            cbo.clearSelection();

                            cbo.trackChanges();
                            var larr = list.length - 2;
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


        </script>

    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="loadPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="loadPanel" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>

    <telerik:RadWindow ID="winMedicationReceiveOpt" Width="400px" Height="450px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="winMedicationReceiveOpt_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Maximize, Close,Move" Modal="True" ShowOnTopWhenMaximized="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>

    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="500px" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegType" runat="server" Text="Registration"></asp:Label>
                            </td>
                            <td >
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
                            <td >
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
                            <td >
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
                            <td >
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50px"></td>
                <td width="500px" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Main Physician"></asp:Label>
                            </td>
                            <td >
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" EmptyMessage="Select a Paramedic"
                                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                                    <WebServiceSettings Method="Paramedics" Path="~/WebService/ComboBoxDataService.asmx" />
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterParamedic" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>

                        <tr>
                            <td class="label">Include</td>
                            <td>
                                <asp:CheckBox ID="chkIsClosed" runat="server" Text="Closed" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkIsIncludeNotInBed" runat="server" Text="Discharged Patients (Inpatient)" />
                            </td>
                            <td style="text-align: left;"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                            </td>
                            <td >
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
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>

                    </table>
                </td>
                <td></td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true">
        <MasterTableView DataKeyNames="RegistrationNo" ClientDataKeyNames="RegistrationNo"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="Group" HeaderText="Service Unit " FormatString="{0}"></telerik:GridGroupByField>
                        <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Main Physician " FormatString="{0}"></telerik:GridGroupByField>

                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="Group" SortOrder="Ascending"></telerik:GridGroupByField>
                        <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending"></telerik:GridGroupByField>

                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="RegistrationDate" HeaderText="Reg. Date">
                    <ItemTemplate>
                        <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(AppConstant.DisplayFormat.DateShortMonth) %><br />
                        <%#DataBinder.Eval(Container.DataItem, "RegistrationTime") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
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
                <telerik:GridTemplateColumn HeaderStyle-Width="240px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <span style="font-size: 12pt;"><%# string.Format("{0} {1}",
                                DataBinder.Eval(Container.DataItem, "SalutationName"),
                                DataBinder.Eval(Container.DataItem, "PatientName")
                                )%>
                        </span>&nbsp;<%# RegistrationNoteCount(Container)%>
                        <br />
                        <%# string.Format("{0}Y {1}M {2}D", Helper.GetAgeInYear (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), Helper.GetAgeInMonth(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date)), Helper.GetAgeInDay(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth") ?? DateTime.Now.Date))) %>
                        <br />
                        <%#DataBinder.Eval(Container.DataItem, "GuarantorName") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReferralGroupName" HeaderText="Referral Group" UniqueName="ReferralGroupName"
                    SortExpression="ReferralGroupName">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="RoomName" HeaderText="Room / Bed" SortExpression="RoomName">
                    <ItemTemplate>
                        R: &nbsp; <%#DataBinder.Eval(Container.DataItem, "RoomName")  %><br />
                        B: &nbsp;<%#DataBinder.Eval(Container.DataItem, "BedID") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
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
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="7" ItemStyle-Font-Size="7" ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <table style="width: 50px;">
                            <tr>
                                <td style="width: 20px;">TRG</td>
                                <td>|</td>
                                <td style="width: 20px;">SOAP</td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 50px;">
                            <tr>
                                <td>
                                    <div style="width: 20px; background-color: <%# ColorOfTriase(DataBinder.Eval(Container.DataItem,"SRTriage")) %>; color: gray;">&nbsp;</div>
                                </td>
                                <td>|</td>
                                <td style="width: 20px;"><%# SoapEntryStatuslHtml(Container) %></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-Font-Size="7" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="5">Drug Reconciliation</td>
                            </tr>
                            <tr>
                                <td style="width: 22px;">ADM</td>
                                <td>|</td>
                                <td style="width: 22px;">TRF</td>
                                <td>|</td>
                                <td style="width: 22px;">DIS</td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 22px;"><%#DrugRecon(Container,"adm")%></td>
                                <td>&nbsp;</td>
                                <td style="width: 22px;"><%#DrugRecon(Container,"trf") %></td>
                                <td>&nbsp;</td>
                                <td style="width: 22px;"><%# DrugRecon(Container,"dcg")%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-Font-Size="7" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="7">Drug Consume</td>
                            </tr>
                            <tr>
                                <td style="width: 20px;">SET</td>
                                <td>|</td>
                                <td style="width: 20px;">HOV</td>
                                <td>|</td>
                                <td style="width: 20px;">VER</td>
                                <td>|</td>
                                <td style="width: 20px;">REA</td>
                                <td>|</td>
                                <td style="width: 20px;">HIS</td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 20px;"><%# MedicationConsume(Container, "S")%></td>
                                <td>&nbsp;</td>
                                <td style="width: 20px;"><%# MedicationConsume(Container, "H")%></td>
                                <td>&nbsp;</td>
                                <td style="width: 20px;"><%# MedicationConsume(Container, "V")%></td>
                                <td>&nbsp;</td>
                                <td style="width: 20px;"><%# MedicationConsume(Container, "R")%></td>
                                <td>&nbsp;</td>
                                <td style="width: 20px;"><%# !DataBinder.Eval(Container.DataItem, "SRRegistrationType").Equals(AppConstant.RegistrationType.InPatient)? string.Empty: string.Format("<a href=\"#\" onclick=\"openMedicationHist('{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/history16.png\" border=\"0\" alt=\"MedHist\" title=\"Service Unit Kardex\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"), 
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                            DataBinder.Eval(Container.DataItem, "FromRegistrationNo"))%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>


                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td style="width: 20px;">&nbsp;<%# string.Format("<a href=\"#\" onclick=\"javascript:openMedicationReceiveOpt('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/drugs16.png\" border=\"0\" alt=\"\" title=\"Medication Menu\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "PatientID"))%></td>
                                <td style="width: 20px;">&nbsp;<%# string.Format("<a href=\"#\" onclick=\"javascript:openVisit('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/paste16.png\" border=\"0\" alt=\"\" title=\"Visit Notes\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"))%></td>
                                <td style="width: 20px;">&nbsp;<%# string.Format("<a href=\"#\" onclick=\"javascript:openCounseling('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/treeview_add16.png\" border=\"0\" alt=\"\" title=\"Counseling\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"))%></td>
                                <td style="width: 20px;">&nbsp;<%# string.Format("<a href=\"#\" onclick=\"javascript:openPto('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/monitor16.png\" border=\"0\" alt=\"\" title=\"Drug Therapy Monitoring\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"))%></td>
                                <td style="width: 20px;">&nbsp;<%# string.Format("<a href=\"#\" onclick=\"javascript:openDrugObservation('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/drugs16.png\" border=\"0\" alt=\"\" title=\"Inpatient Pharmacy Observation\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"))%></td>
                                <td style="width: 20px;">&nbsp;<%# string.Format("<a href=\"#\" onclick=\"javascript:openEso('{0}','{1}'); return false;\"><img src=\"../../../Images/Toolbar/drugs16.png\" border=\"0\" alt=\"\" title=\"Drug side effects\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"))%></td>

                                <td style="width: 20px;">&nbsp;<%# HomePrescription(Container)%>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
