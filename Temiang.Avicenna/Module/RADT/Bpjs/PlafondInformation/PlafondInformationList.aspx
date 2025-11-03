<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true" CodeBehind="PlafondInformationList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Bpjs.PlafondInformationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function openWinEditPlafond(regno) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.SetUrl("PlafondInformationDialog.aspx?regNo=" + regno);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument == 'rebind') {
                    __doPostBack("<%= grdRegistrationList.UniqueID %>", "rebind");
                    oWnd.argument = 'undefined';
                }
            }

            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function UpdateState(statType, divCtl, regNo, fromRegNo, regType, patID, dob, parID) {
                var obj = {};
                obj.statType = statType;
                obj.regNo = regNo;
                obj.fromRegNo = fromRegNo;
                obj.regType = regType;
                obj.patID = patID;
                obj.dob = dob;
                obj.parID = parID;
                $.ajax({
                    url: '<%=Page.ResolveUrl("~/Module/RADT/Cpoe/EmrWebService.asmx/UpdateStateEmrList")%>',
                    data: JSON.stringify(obj), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        // update div
                        document.getElementById(divCtl).innerHTML = data.d;

                    },
                    error: function (x, y, z) {
                        //alert(x.responseText + "  " + x.status);
                    }
                });
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDischargeDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedicID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterIncludeDischargedPatients">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnInitialDiagnosis">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistrationList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboServiceUnitID" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterOverPlafond">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnPatientClass">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistrationList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="600px" Height="400px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow ID="winProcess" Animation="None" Width="1000px" Height="600px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true" OnClientClose="onClientClose">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRegistrationDate1" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtRegistrationDate2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDischarge" runat="server" Text="Discharge Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDischargeDate1" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDischargeDate2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDischargeDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trGuarantorID">
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound" OnItemsRequested="cboGuarantorID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInitialDiagnosis" runat="server" Text="Initial Diagnosis"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtInitialDiagnosis" runat="server" Width="300px" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnInitialDiagnosis" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientClass" runat="server" Text="Patient Class"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblPatientClass" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="" Text="All" Selected="True" />
                                    <asp:ListItem Value="+" Text="Upgrade" />
                                    <asp:ListItem Value="-" Text="Downgrade" />
                                </asp:RadioButtonList>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnPatientClass" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Registration Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRRegistrationType" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRRegistrationType_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" EnableLoadOnDemand="true"
                                    HighlightTemplatedItems="true" OnItemDataBound="cboServiceUnitID_ItemDataBound" OnItemsRequested="cboServiceUnitID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 20 items
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblParamedicID" runat="server" Text="Physician"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboParamedicID" Width="300px" AllowCustomText="true" Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterParamedicID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox ID="chkIncludeDischargedPatients" runat="server" Text="Include Discharged Patients" />
                                <asp:CheckBox ID="chkIncludeClosedPatients" runat="server" Text="Include Closed Patients" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterIncludeDischargedPatients" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="trPrescription23Days">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox ID="chkPrescription23Days" runat="server" Text="23 Days Prescription" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOverPlafond" runat="server" Text="Over Plafond With Range"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtOverPlafondRange1" runat="server" Width="50px" />&nbsp;%
                                        </td>
                                        <td>&nbsp;&nbsp;-&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtOverPlafondRange2" runat="server" Width="50px" />&nbsp;%
                                        </td>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblOverPlafondRangeMsg" runat="server" Text="* Value 0 will be ignored" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterOverPlafond" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr runat="server" id="tr1">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox ID="chkPlafondNotSet" runat="server" Text="Plafond Not Set" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdRegistrationList" runat="server" AutoGenerateColumns="false"
        OnNeedDataSource="grdRegistrationList_NeedDataSource" OnItemDataBound="grdRegistrationList_ItemDataBound"
        AllowPaging="True" PageSize="15">
        <MasterTableView DataKeyNames="RegistrationNo" GroupLoadMode="client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ParamedicName" HeaderText="Physician "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ParamedicName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="editPlafond">
                    <ItemTemplate>
                        <%# (this.IsUserEditAble.Equals(false) ? string.Empty
                                                        : string.Format("<a href=\"#\" onclick=\"openWinEditPlafond('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                    SortExpression="RegistrationDate">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="160px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" DataField="ChargeClassName" HeaderText="Charge Class" UniqueName="ChargeClassName"
                    SortExpression="ChargeClassName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                    SortExpression="BedID" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LoS" HeaderText="LoS" UniqueName="LoS"
                    SortExpression="LoS" DataFormatString="{0:n0}">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InitialDiagnose" HeaderText="Initial Diagnosis" UniqueName="InitialDiagnose"
                    SortExpression="InitialDiagnose">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PackageName" HeaderText="INA-CBG Grouper" UniqueName="PackageName"
                    SortExpression="PackageName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <%--                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Total Transaction" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#TransctionProgress(DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Plafond & Billing" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%#Plafond(DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Plafond Progress" HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <%#PlafondProgress(DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>

                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Total Transaction" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# string.Format("<div id=\"billamt{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("billamt",Eval("RegistrationNo"),"","","","","")%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Right"
                    HeaderText="Plafond" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# string.Format("<div id=\"plafamt{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("plafamt",Eval("RegistrationNo"),"","","","","")%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Plafond Progress" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# string.Format("<div id=\"plafond{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("plafond",Eval("RegistrationNo"),"","","","","")%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsClosed" HeaderText="Closed"
                    UniqueName="IsClosed" SortExpression="IsClosed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="32px">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassSeq1" UniqueName="ClassSeq1" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassSeq2" UniqueName="ClassSeq2" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="DefaultClassID" UniqueName="DefaultClassID" Visible="False" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="True">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
