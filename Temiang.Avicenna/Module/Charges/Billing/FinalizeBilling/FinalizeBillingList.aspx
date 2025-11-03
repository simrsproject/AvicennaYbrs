<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="FinalizeBillingList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.FinalizeBillingList" %>

<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../../../../JavaScript/jQuery.js"></script>

    <telerik:RadCodeBlock runat="server">

        <script type="text/javascript">
            function openWinProcess(patientId, regNo, regType) {
                if ("<%=AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPaymentCheckBeforePatientTrans).ToString()%>" == "True") {
                    openBillingWithCheckPayment(patientId, regNo, regType);
                } else {
                    continueBilling(regNo, regType);
                }
            }
            function openBillingWithCheckPayment(patientId, regNo, regType) {
                $.ajax({
                    type: "POST",
                    url: "../../../../WebService/BillingChargeService.asmx/RemainingAmountConfirm",
                    data: "{'patientId':'" + patientId + "','beforeRegNo':'" + regNo + "'}", // if ur method take parameters
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        var result = response.d;
                        if (result != '') {
                            alert('This patient has remain amount: ' + result);
                        }
                        continueBilling(regNo, regType);
                    },
                    dataType: "json",
                    failure: function (response) {
                        var result = response.d;
                        alert(result);
                    }
                });
            }

            function continueBilling(regNo, regType) {
                location.replace('FinalizeBillingVerification.aspx?regNo=' + regNo + '&regType=' + regType + '&md=new&from=1');
            }

            function gotoViewUrl(regNo, regType) {
                location.replace('FinalizeBillingVerification.aspx?regNo=' + regNo + '&regType=' + regType + '&md=view&from=1');
            }

            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openWinQuestionFormCheckList(regNo) {
                var oWnd = $find("<%= winDocsOption.ClientID %>");
                var lblToBeUpdate = "noti2_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/Registration/RegistrationDocumentCheckList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.set_title('Document Checklist');
                oWnd.show();
            }

            function OnClientClose(oWnd, args) {
                __doPostBack("<%= grdRegisterOpenList.UniqueID %>", "rebind");
            }

            function ProcessClosed(regNo) {
                __doPostBack("<%= grdRegisterOpenList.UniqueID %>", "processClosed|" + regNo);
            }

            function openWinTransfer(mode, recId, regNo) {
                var oWnd = window.$find("<%= winRegInfo.ClientID %>");
                oWnd.setUrl("../../ServiceUnit/ServiceUnitTransaction/ReferToOtherServiceUnit.aspx?md=" + mode + "&id=" + recId + "&reg=" + regNo + "&rt=OPR&trans=1" + '&type=<%= Page.Request.QueryString["type"] %>');
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
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDischargePlanDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterReadyToDischarge">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegisterOpenList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegisterOpenList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
        ID="winDocsOption">
    </telerik:RadWindow>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align: top; width: 40%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtOrderDate1" runat="server" Width="100px" />
                                        </td>
                                        <td>&nbsp;-&nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtOrderDate2" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDischargePlanDate" runat="server" Text="Discharge Plan Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtDischargePlanDate" runat="server" Width="100px" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterDischargePlanDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
                                <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboRegistrationType" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top; width: 40%">
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
                            <td width="20">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboGuarantorID" runat="server" Width="300px" HighlightTemplatedItems="True"
                                    MarkFirstMatch="False" EnableLoadOnDemand="true" NoWrap="False" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                    OnItemsRequested="cboGuarantorID_ItemsRequested">
                                    <FooterTemplate>
                                        Note : Show max 30 result
                                    </FooterTemplate>
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top; width: 20%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Ready To Discharge"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:CheckBox ID="chkReadyToDischarge" runat="server" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterReadyToDischarge" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdRegisterOpenList" runat="server" AutoGenerateColumns="false"
        OnNeedDataSource="grdRegisterOpenList_NeedDataSource" OnItemDataBound="grdRegisterOpenList_ItemDataBound"
        AllowPaging="True" PageSize="15">
        <MasterTableView DataKeyNames="RegistrationNo" GroupLoadMode="client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="process" HeaderText="">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"New\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SRRegistrationType")))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="process" HeaderText="">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) || DataBinder.Eval(Container.DataItem, "IsMergeBilling").Equals(true) ? string.Empty
                                                        : string.Format("<a href=\"#\" onclick=\"openWinProcess('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SRRegistrationType")))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="view" Visible="False">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"View\" /></a>",
                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "SRRegistrationType")) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
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
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
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
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                    SortExpression="BedID">
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ChargeClassID2" HeaderText="Cover Class / Charge Class" UniqueName="ChargeClassID2"
                    SortExpression="ChargeClassID2">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DischargePlanDate" HeaderStyle-Width="90px" DataFormatString="{0:dd/MM/yyyy}"
                    UniqueName="DischargePlanDate" HeaderText="Discharge Plan Date" SortExpression="DischargePlanDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="DischargeDate" HeaderStyle-Width="90px" DataFormatString="{0:dd/MM/yyyy}"
                    UniqueName="DischargeDate" HeaderText="Discharge Date" SortExpression="DischargeDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn DataField="IsHoldTransactionEntry" HeaderText="Locked"
                    UniqueName="IsHoldTransactionEntry" SortExpression="IsHoldTransactionEntry" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="" ItemStyle-HorizontalAlign="Center" UniqueName="templateARReceipt">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsArReceipt").Equals(false) ? string.Empty : 
                                string.Format("<a href=\"#\">{0}</a>","<img src=\"../../../../Images/RpY.png\" border=\"0\" title=\"Payment or A/R Receipt\" />")
                                )%>
                    </ItemTemplate>
                    <HeaderStyle Width="32px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="" ItemStyle-HorizontalAlign="Center" UniqueName="templateProcessClosed">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsAllowClosed").Equals(false) ? string.Empty : 
                                string.Format("<a href=\"#\" onclick=\"ProcessClosed('{0}'); return false;\"><b>{1}</b></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"), "<img src=\"../../../../Images/Toolbar/lock16.png\" border=\"0\" title=\"Closed\" />")
                                )%>
                    </ItemTemplate>
                    <HeaderStyle Width="32px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="32px">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" title=\"Form Check List\" class=\"noti2_Container\" onclick=\"openWinQuestionFormCheckList('{0}'); return false;\"><span id=\"noti2_{0}\" class=\"noti_bubble\">{1}</span></a>",
                                                                           DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                           DataBinder.Eval(Container.DataItem, "DocumentCheckListCountRemains")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    HeaderText="Plafond Progress" HeaderStyle-Width="80px" UniqueName="PlafondProgress">
                    <ItemTemplate>
                        <%--                        <%#PlafondProgress(DataBinder.Eval(Container.DataItem, "RegistrationNo").ToString())%>--%>
                        <%# string.Format("<div id=\"plafond{0}\"></div>",Eval("RegistrationNo").ToString().Replace("/","_")) %>
                        <script type="text/javascript">
                            <%# UpdateStatScript("plafond",Eval("RegistrationNo"),"","","","","")%>
                        </script>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassSeq1" UniqueName="ClassSeq1" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassSeq2" UniqueName="ClassSeq2" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="DefaultClassID" UniqueName="DefaultClassID" Visible="False" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" HeaderText="Ref" UniqueName="openWinTransfer">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) ? string.Empty :
                                string.Format("<a href=\"#\" onclick=\"openWinTransfer('new', '{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/transactions16.png\" border=\"0\" title=\"Refer To Other Unit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="RealizationStatus">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsOrderRealization").Equals(true) ? 
                                                        string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Approved\" /></a>")
                                                        : string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" title=\"\" /></a>"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
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
