<%@  Language="C#" MasterPageFile="~/MasterPage/MasterBasePage.Master" AutoEventWireup="true"
    CodeBehind="CardexMonitoringList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.CardexMonitoring.CardexMonitoringList" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
        <style>
            .noti_homepres {
                position: relative; /*border:1px solid blue;*/ /* This is just to show you where the container ends */
                cursor: pointer;
                background: transparent url('<%= Helper.UrlRoot() %>/Images/Toolbar/edit16.png') no-repeat 2px 50%;
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
        <script src="../../../JavaScript/OpenWindowMax.js"></script>
        <script type="text/javascript">
            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%= Helper.UrlRoot() %>/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate);
                oWnd.show();
            }

            function openCardexMonitoring(patid, regno, fregno, mc) {
                var url = 'CardexMonitoringDashboard.aspx?patid=' + patid + '&regno=' + regno + '&fregno=' + fregno + '&mc=' + mc;
                var oWnd = $find("<%= winCardexMon.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();

                //openWindowMax(url, "");
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

    <telerik:RadWindow ID="winCardexMon" runat="server" VisibleStatusbar="false" InitialBehaviors="Maximize"
        ShowContentDuringLoad="false" Behaviors="Maximize, Close,Move" Modal="True" ShowOnTopWhenMaximized="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />


    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">

            <tr>
                <td width="500px" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                            </td>
                            <td>
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

                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegType" runat="server" Text="Registration"></asp:Label>
                            </td>
                            <td>
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
                            <td>
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
                            <td>
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
                            <td>
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
                                <asp:Label ID="Label1" runat="server" Text="Bed No"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtBedID" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterBedID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Main Physician"></asp:Label>
                            </td>
                            <td>
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
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit " FormatString="{0}"></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
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
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed" UniqueName="BedID"
                    SortExpression="BedID">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ReferralGroupName" HeaderText="Referral Group" UniqueName="ReferralGroupName"
                    SortExpression="ReferralGroupName">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Main Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
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
                                <td colspan="5">Cardex Monitoring</td>
                            </tr>
                            <tr>
                                <td style="width: 22px;">C3</td>
                                <td>|</td>
                                <td style="width: 22px;">NEO</td>
                                <td>|</td>
                                <td style="width: 22px;">CTCU</td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 22px;"><%#CardexMonMenu(Container,"mc3") %></td>
                                <td>&nbsp;</td>
                                <td style="width: 22px;"><%#CardexMonMenu(Container,"mc3n") %></td>
                                <td>&nbsp;</td>
                                <td style="width: 22px;"><%# CardexMonMenu(Container,"mcctcu")%></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="SpcaceRight" />

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
