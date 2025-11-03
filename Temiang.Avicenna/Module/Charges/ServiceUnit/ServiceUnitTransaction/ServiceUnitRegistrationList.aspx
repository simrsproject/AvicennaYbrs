<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="ServiceUnitRegistrationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ServiceUnitRegistrationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoEditUrl(id, type, regno, cid, pid, roomid) {
                var url = 'ServiceUnitTransactionDetail.aspx?md=edit&id=' + id + '&type=' + type + '&regno=' + regno + '&pid=' + pid + '&cid=' + cid + '&roomid=' + roomid + '&resp=<%= Page.Request.QueryString["resp"] %>' + '&disch=<%= Page.Request.QueryString["disch"] %>' + '&verif=0';
                window.location.href = url;
            }

            function gotoViewUrl(id, type, regno, cid, pid, roomid) {
                var url = 'ServiceUnitTransactionDetail.aspx?md=view&id=' + id + '&type=' + type + '&regno=' + regno + '&pid=' + pid + '&cid=' + cid + '&roomid=' + roomid + '&resp=<%= Page.Request.QueryString["resp"] %>' + '&disch=<%= Page.Request.QueryString["disch"] %>' + '&verif=0';
                window.location.href = url;
            }

            function gotoPrintLblUrl(TransactionNo, SequenceNo) {
                //alert("empty");
                __doPostBack("<%= grdList.UniqueID %>", 'printlbl|' + TransactionNo + '|' + SequenceNo);
            }

            function gotoAddUrl(type, regno, pid, cid, roomid) {
                var url = 'ServiceUnitTransactionDetail.aspx?md=new&type=' + type + '&regno=' + regno + '&pid=' + pid + '&cid=' + cid + '&roomid=' + roomid + '&resp=<%= Page.Request.QueryString["resp"] %>' + '&disch=<%= Page.Request.QueryString["disch"] %>' + '&verif=0';
                window.location.href = url;
            }

            function openPatientPrintOpt(patientID) {
                var oWnd = window.$find("<%= winPrintOpt.ClientID %>");
                oWnd.setUrl('ServiceUnitBookingDialog.aspx?id=' + window.selectedAppointment.get_id());
                oWnd.show();
            }

            function openWinQuestionForm(regno, sid) {
                var oWnd = window.$find("<%= winQuestionForm.ClientID %>");
                oWnd.setUrl("QuestionFormSelection.aspx?regno=" + regno + "&sid=" + sid + "&rwndrnd=" + Math.random());
                oWnd.show();
            }

            function openWinTransfer(mode, recId, regNo) {
                var oWnd = window.$find("<%= winReg.ClientID %>");
                oWnd.setUrl("ReferToOtherServiceUnit.aspx?md=" + mode + "&id=" + recId + "&reg=" + regNo + "&rt=OPR&trans=1" + '&type=<%= Page.Request.QueryString["type"] %>');
                oWnd.show();
            }

            function openWinPrescOrder(mode, regNo) {
                var oWnd = window.$find("<%= winPrescOrder.ClientID %>");
                oWnd.setUrl("PrescriptionOrderDialog.aspx?md=" + mode + "&reg=" + regNo);
                oWnd.show();
            }

            function openWinVisiteRealization(patid, regNo) {
                var url = 'SubForm/VisiteRealizationEntry.aspx?md=edit&type=<%= Request.QueryString["type"]%>&regno=' + regNo + '&patid=' + patid;
                openWindowMaximize(url);
            }

            function openWinVisite(trno, itemid) {
                var oWnd = window.$find("<%= winReg.ClientID %>");
                oWnd.setUrl('SubForm/VisiteEntry.aspx?md=edit&type=<%= Request.QueryString["type"]%>&trno=' + trno + '&itemid=' + itemid);
                oWnd.show();
            }

            function onVoidRegistration(regNo) {
                var oWnd = window.$find("<%= winVoid.ClientID %>");
                oWnd.setUrl("../../../RADT/Registration/VoidRegistrationDialog.aspx?regNo=" + regNo + '&disch=<%= Page.Request.QueryString["disch"] %>');
                oWnd.show();
            }

            function openWinPhysicinTeam(type, regno) {
                var url = '../../../RADT/InPatient/PhysicianTeam/PhysicianTeamDetail.aspx?md=edit&regno=' + regno + '&fr=2&type=' + type + '&resp=<%= Page.Request.QueryString["resp"] %>' + '&disch=<%= Page.Request.QueryString["disch"] %>';
                window.location.href = url;
            }

            function openWinPhysicinTeamOp(regno) {
                var oWnd = window.$find("<%= winPrescOrder.ClientID %>");
                oWnd.setUrl("PhysicianTeamDialog.aspx?reg=" + regno);
                oWnd.show();
            }

            function openItemTxDetail(regno) {
                var url = 'ItemTransactionList.aspx?type=<%= Request.QueryString["type"]%>&reg=' + regno;
                openWindowMaximize(url);
            }
            
            function gotoEditPhyisicianSendersUrl(trn) {
                var oWnd = window.$find("<%= winEditPhysicianSender.ClientID %>");
                oWnd.setUrl("EditPhysicianSendersDialog.aspx?trn=" + trn);
                oWnd.show();
            }

            function openLabelPrint(transNo, type) {
                var oWnd = $find("<%= winPrintLbl.ClientID %>");
                oWnd.SetUrl("../FilmConsumption/LabelPrint.aspx?tno=" + transNo + "&type=" + type + "&init=rsch");
                oWnd.show();
            }

            function onClientCloseLabelPrint(oWnd, args) {
                if (oWnd.argument && oWnd.argument.print != null) {
                    var oWnd = $find("<%= winPrint.ClientID %>");
                    oWnd.setUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                    oWnd.show();
                }
            }

            function OnClientClose(oWnd, args) {
                __doPostBack("<%= grdList.UniqueID %>", "rebind");
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

            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function openWindowMaximize(url) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openRpt() {
                var oWnd = $find('<%=winPrint.ClientID%>');
                oWnd.SetUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
                return;
            }
            function openWinBookingOt(regno, roomid) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("../ServiceUnitBooking/ServiceUnitBookingDialog.aspx?id=&regno=" + regno + "&roomid=" + roomid);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function onClientClickUpdateRadNo(regno) {
                var diagno = prompt('Are you sure to change Radiology No for this patient?\nNew Radiology No :');
                if (diagno == null || diagno == '') {
                    args.set_cancel(true);
                }
                else {
                    __doPostBack("<%= grdList.UniqueID %>", 'updateRadNo!' + regno + '!' + diagno);
                }
            }
            function showAcceptMedicalFile(regno, unitid) {
                if (confirm('Are you sure to receive medical file?')) {
                    __doPostBack("<%= grdList.UniqueID %>", "receive|" + regno + "|" + unitid);
                }
            }
            function gotoEditPhysician(regNo, unitID) {
                radopen("../../../RADT/Registration/EditPhysicianDialog.aspx?rt=tr&regNo=" + regNo + "&unitID=" + unitID, "winReg");
            }
            function openSpecimenCRDetail(id, regno) {
                var oWnd = radopen('SpecimenCollectItem.aspx?type=<%= Request.QueryString["type"]%>&id=' + id + '&reg=' + regno + '&sc=0');
                oWnd.setSize(1000, 600);
                oWnd.show(); 
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBarcodeEntry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="txtBarcodeEntry" />
                    <telerik:AjaxUpdatedControl ControlID="fw_PanelInfo" />
                    <telerik:AjaxUpdatedControl ControlID="fw_LabelInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" ShowContentDuringLoad="false"
                Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winNoTitlebar" runat="server" Behaviors="None" VisibleTitlebar="False"
                ShowContentDuringLoad="false" Modal="True">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
                runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
                Modal="true" />
            <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
                ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
                Modal="true">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="400px" Height="300px" Behavior="Close, Move"
                ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winPrintOpt"
                Title="Select report then click Ok button">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="500px"
                Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
                Modal="true" Title="Question Form" ID="winQuestionForm">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
                Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
                Modal="true" ID="winReg" OnClientClose="OnClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
                Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
                Modal="true" Title="Prescription Order Form" ID="winPrescOrder" OnClientClose="OnClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="500px"
                Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
                Modal="true" Title="Edit Physician Senders" ID="winEditPhysicianSender">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="1000px" Height="600px"
                Behavior="Close, Move" ShowContentDuringLoad="False" VisibleStatusbar="False"
                Modal="true" Title="Void Transfer" ID="winVoid" OnClientClose="OnClientClose">
            </telerik:RadWindow>
            <telerik:RadWindow ID="winPrintLbl" Animation="None" Width="600px" Height="300px"
                runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
                ReloadOnShow="True" Modal="true" OnClientClose="onClientCloseLabelPrint" Title="Print Label">
            </telerik:RadWindow>
            <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
                ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
                ID="winDocsOption">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:Panel ID="fw_PanelInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image1" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="fw_LabelInfo" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
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
                        <tr id="trFilterDate" runat="server">
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Registration Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
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
                        <tr>
                            <td class="label">Guarantor
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterGuarantor" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Barcode Scan Entry (MRN)"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtBarcodeEntry" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtBarcodeEntry_OnTextChanged" />
                            </td>
                            <td style="text-align: left"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind" OnItemCommand="grdList_ItemCommand" OnItemDataBound="grdList_ItemDataBound">
        <MasterTableView Name="master" DataKeyNames="RegistrationNo, ServiceUnitID" ClientDataKeyNames="RegistrationNo, ServiceUnitID"
            GroupLoadMode="Client">
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
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans2" HeaderText="">
                    <ItemTemplate>
                        <%# (this.IsUserAddAble.Equals(false) ? string.Empty : (DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) && this.IsPowerUser.Equals(true) ? string.Format("<a href=\"#\" onclick=\"gotoEditPhysician('{0}', '{1}'); return false;\"><img src=\"../../../../Images/doctor16.png\" border=\"0\" title=\"Doctor required\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID")) : 
                                (DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) && this.IsPowerUser.Equals(false) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/doctor16.png\" border=\"0\" title=\"Doctor required, please contact your supervisor or related unit\" /></a>") : 
                                (DataBinder.Eval(Container.DataItem, "IsHoldTransactionEntry").Equals(true) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/Toolbar/lock16_d.png\" border=\"0\" title=\"Lock\" /></a>") : 
                                (DataBinder.Eval(Container.DataItem, "IsValidAssessment").Equals(false) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/Toolbar/new16_d.png\" border=\"0\" title=\"Diagnosis required\" /></a>") : 
                                (DataBinder.Eval(Container.DataItem, "IsValidMedicalFileReceived").Equals(false) ? string.Format("<a href=\"#\" return false;\"><img src=\"../../../../Images/Toolbar/new16_d.png\" border=\"0\" title=\"Medical files have not been received\" /></a>") : 
                                string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}','{1}','{2}','{3}', '{4}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>", 
                                    Request.QueryString["type"], DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ParamedicID"), DataBinder.Eval(Container.DataItem, "ServiceUnitID"), DataBinder.Eval(Container.DataItem, "RoomID")))))))
                                                        )%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="PHR" HeaderText="PHR" Visible="false">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openWinQuestionForm('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" /></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID")) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                    SortExpression="RegistrationDate">
                    <HeaderStyle HorizontalAlign="Center" Width="75px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                 <telerik:GridTemplateColumn UniqueName="QueNo" HeaderText="Que" Visible="True">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "QueNo"), DataBinder.Eval(Container.DataItem, "FormattedNo"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

<%--             <telerik:GridBoundColumn DataField="QueNo" HeaderText="Que" UniqueName="Que"
                    SortExpression="QueNo">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>--%>
                <%--<telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="145px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridTemplateColumn HeaderStyle-Width="145px" UniqueName="RegistrationNo" HeaderText="Registration No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                        <br /><i><span style="color: red"><%# DataBinder.Eval(Container.DataItem, "FromRegistrationNo")%></span></i>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Group" HeaderText="Service Unit" UniqueName="Group"
                    SortExpression="Group">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Room" UniqueName="RoomName" SortExpression="RoomName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "RoomName")%>&nbsp;
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="110px" HeaderText="Bed No" UniqueName="BedID">
                    <ItemTemplate>
                        <asp:Label ID="lblBedID" runat="server" Text='<%#Eval("BedID")%>' />
                        <%# (DataBinder.Eval(Container.DataItem, "BedIsReady").Equals(false) ? string.Empty : string.Format("<img src=\"../../../../Images/infogreen16.png\" border=\"0\" title=\"Bed is available\" />"))%>
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsSurgeryRoom")) ? string.Format("<a href=\"#\" onclick=\"openWinBookingOt('{0}', '{1}'); return false;\"><img src=\"../../../../Images/or20.png\" border=\"0\" title=\"Surgery Detail\" /></a>",
                                                            DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "RoomID")) : string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsConsul" HeaderText="Consul"
                    UniqueName="IsConsul" SortExpression="IsConsul" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                <telerik:GridTemplateColumn UniqueName="viewDetailTx" HeaderText="">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openItemTxDetail('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"List Detail Transaction\" /></a>",
                                                             DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="Vist" Visible="false" UniqueName="openWinVisiteRealization">
                    <ItemTemplate>
                        <%# (Request.QueryString["type"] == "tr" && Request.QueryString["disch"] == "0") ?
                                                        string.Format("<a href=\"#\" onclick=\"openWinVisiteRealization('{0}','{1}'); return false;\"><img src=\"../../../../Images/todolist16.png\" border=\"0\" title=\"Visite Order Realization\" /></a>",
                                                             DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"))
                            : string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" Visible="false" UniqueName="openPatientPrintOpt">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openPatientPrintOpt('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print\" /></a>", DataBinder.Eval(Container.DataItem, "PatientID"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" HeaderText="Ref" UniqueName="openWinTransfer">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) ? string.Empty :
                                                        string.Format("<a href=\"#\" onclick=\"openWinTransfer('new', '{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/transactions16.png\" border=\"0\" title=\"Refer To Other Unit\" /></a>",
                                DataBinder.Eval(Container.DataItem, "PatientID"), DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="Cons" UniqueName="onVoidRegistration">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsConsul").Equals(false) ? string.Empty :
                                    string.Format("<a href=\"#\" onclick=\"onVoidRegistration('{0}'); return false;\">{1}</a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                                                DataBinder.Eval(Container.DataItem, "IsConsul").Equals(false) ? string.Empty : "<img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Void Consul\" />"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="Pres" UniqueName="openWinPrescOrder">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsInpatient").Equals(false) || DataBinder.Eval(Container.DataItem, "IsNewVisible").Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"openWinPrescOrder('new', '{0}'); return false;\"><img src=\"../../../../Images/order16.png\" border=\"0\" title=\"Prescription Order\" /></a>",
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="" UniqueName="openWinPhysicinTeamOp">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsInpatient").Equals(false) ? string.Format("<a href=\"#\" onclick=\"openWinPhysicinTeamOp('{0}'); return false;\"><img src=\"../../../../Images/doctors.png\" border=\"0\" title=\"Substitute Doctor\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "RegistrationNo")) : string.Format("<a href=\"#\" onclick=\"openWinPhysicinTeam('{0}', '{1}'); return false;\"><img src=\"../../../../Images/doctors.png\" border=\"0\" title=\"Physician Team\" /></a>",
                                                            Request.QueryString["type"], DataBinder.Eval(Container.DataItem, "RegistrationNo")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="" UniqueName="openWinPhysicinTeam">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsInpatient").Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"openWinPhysicinTeam('{0}', '{1}'); return false;\"><img src=\"../../../../Images/doctors.png\" border=\"0\" title=\"Physician Team\" /></a>",
                                                            Request.QueryString["type"], DataBinder.Eval(Container.DataItem, "RegistrationNo")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="showAcceptMedicalFile" HeaderText="">
                    <ItemTemplate>
                        <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "LastPositionUserID").ToString()) ?
                            string.Format("<a href=\"#\" onclick=\"showAcceptMedicalFile('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" title=\"Medical File Received\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID")) : 
                            "<a href=\"#\"><img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" title=\"Medical File Received\" /></a>" %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" UniqueName="openWinRegistrationInfo">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" UniqueName="openWinQuestionFormCheckList">
                    <ItemTemplate>
                        <%# (string.Format("<a href=\"#\" title=\"Form Check List\" class=\"noti2_Container\" onclick=\"openWinQuestionFormCheckList('{0}'); return false;\"><span id=\"noti2_{0}\" class=\"noti_bubble\">{1}</span></a>",
                                                                           DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                           DataBinder.Eval(Container.DataItem, "DocumentCheckListCountRemains")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="PrintMotherChildWristband" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnMotherChildWristband" runat="server" CommandName="MotherChildWristband"
                            ToolTip='Patient Wristbands (Mother & Child)' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="PrintPatientLabelRSUI" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnLbl" runat="server" CommandName="PatientLabel" ToolTip='Patient Diet Label'
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="PrintPatientStickerRssmcb" HeaderStyle-Width="35px"
                    ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnLblRSSMCB" runat="server" CommandName="PatientStickerRssmcb"
                            ToolTip='Patient Sticker' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="PrintLabelMCURssmcb" HeaderStyle-Width="35px"
                    ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnPrintMcu" runat="server" CommandName="PrintLabelMCU" ToolTip='Print Label MCU'
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Triage" HeaderStyle-Width="50px" HeaderText="SOAP"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTriage" runat="server" Width="20px" BackColor='<%# GetColorOfTriase(DataBinder.Eval(Container.DataItem,"SRTriage")) %>'></asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="LinkPhr" HeaderStyle-Width="50px" HeaderText="Form"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"ServiceUnitPatientList.aspx?noreg={0}\"><img src=\"../../../../Images/doc.png\" border=\"0\" width=\"20\" height=\"20\" title=\"Quistionnare\" /></a>",Eval("RegistrationNo"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassSeq1" UniqueName="ClassSeq1" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassSeq2" UniqueName="ClassSeq2" Visible="False" />
                <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />
                <telerik:GridBoundColumn DataField="DefaultClassID" UniqueName="DefaultClassID" Visible="False" />
                <telerik:GridTemplateColumn UniqueName="RadNo" HeaderText="Rad #">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"onClientClickUpdateRadNo('{0}'); return false;\"><img src=\"../../../../Images/radfilm.png\" border=\"0\" title=\"Edit Radiology No\" /></a>",
                                                             DataBinder.Eval(Container.DataItem, "RegistrationNo"))%>
                        <asp:Label ID="lblRadNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiagnosticNo") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="TransactionNo,SequenceNo" AutoGenerateColumns="false"
                    GroupLoadMode="Client">
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
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# ((this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true)) ? string.Empty :
                                                                        string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}','{2}', '{3}', '{4}', '{5}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                                                                                                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"), Request.QueryString["type"], DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                                                                                                                                                                            DataBinder.Eval(Container.DataItem, "FromServiceUnitID"), DataBinder.Eval(Container.DataItem, "ParamedicID"), DataBinder.Eval(Container.DataItem, "RoomID")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}', '{3}', '{4}', '{5}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                                                DataBinder.Eval(Container.DataItem, "TransactionNo"), Request.QueryString["type"], DataBinder.Eval(Container.DataItem, "RegistrationNo"),
                                                                                                                                                    DataBinder.Eval(Container.DataItem, "FromServiceUnitID"), DataBinder.Eval(Container.DataItem, "ParamedicID"), DataBinder.Eval(Container.DataItem, "RoomID"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="print" HeaderText="" Groupable="false" Visible="false">
                            <ItemTemplate>
                                <%# ((bool)DataBinder.Eval(Container.DataItem, "IsOrder")) ? string.Format("<a href=\"#\" onclick=\"gotoPrintLblUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print Label Order Realization\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo")) : "" %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ReferenceNo" HeaderText="Reference No"
                            UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="75px" DataField="TransactionDate"
                            HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                            SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                            Visible="false" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item" UniqueName="ItemName"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Physician"
                            UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                            UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="CorrectionQty" HeaderText="Qty Corr."
                            UniqueName="CorrectionQty" SortExpression="CorrectionQty" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                            DataType="System.Double" DataFields="ChargeQuantity,Price,DiscountAmount,CitoAmount,CorrectionQty"
                            SortExpression="Total" Expression="((({0}+{4}) * {1}) - {2}) + {3}" FooterStyle-HorizontalAlign="Right"
                            Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAutoBillTransaction"
                            HeaderText="Auto Bill" UniqueName="IsAutoBillTransaction" SortExpression="IsAutoBillTransaction"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                            HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IntermBillNo" HeaderText="IntermBill No"
                            UniqueName="IntermBillNo" SortExpression="IntermBillNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="65px" DataField="IsCorrection" HeaderText="Correction"
                            UniqueName="IsCorrection" SortExpression="IsCorrection" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Process"
                            UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                            UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsVoid" HeaderText="Void"
                            UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPackage" HeaderText="Package"
                            UniqueName="IsPackage" SortExpression="IsPackage" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="viewSpecimenCRDetail" HeaderText="">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName").ToString().ToLower().Contains("lab") ?
                                string.Format("<a href=\"#\" onclick=\"openSpecimenCRDetail('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Specimen and Collect Method\" /></a>",
                                DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo")) : string.Empty %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsOrder").Equals(false) ? string.Empty :
                                      string.Format("<a href=\"#\" onclick=\"gotoEditPhyisicianSendersUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/dokter.png\" border=\"0\" title=\"Edit Physician Senders\" /></a>",
                                       DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsOrder").Equals(false) ? string.Empty :
                                                                          string.Format("<a href=\"#\" onclick=\"openLabelPrint('{0}', '2'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print Label Diagnostic\" title=\"Print Label Diagnostic\" /></a>",
                                                                                                                DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                        <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID"
                            Visible="False" />
                        <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" HeaderText="Vist" Visible="false">
                            <ItemTemplate>
                                <%# (Request.QueryString["type"] == "tr" && Request.QueryString["disch"] == "0") 
                                    && Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ChargeQuantity")) > 1
                                    && Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsApproved")) == true 
                                    && DataBinder.Eval(Container.DataItem, "SRItemType").Equals(Temiang.Avicenna.BusinessObject.Reference.ItemType.Service) ? 
                                    string.Format("<a href=\"#\" onclick=\"openWinVisite('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Visite Order\" /></a>",
                                                             DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "ItemID"))
                                    : string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detailPackage" DataKeyNames="TransactionNo" AutoGenerateColumns="false">
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="Group" HeaderText="Service Unit "></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="Group" SortOrder="None"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# ((this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "HasAccess").Equals(false)) ? string.Empty :
                                                                                string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}','{2}', '{3}', '{4}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                                                                                                                                                                                DataBinder.Eval(Container.DataItem, "TransactionNo"), Request.QueryString["type"], DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                                                                                                                                                                                DataBinder.Eval(Container.DataItem, "FromServiceUnitID"), DataBinder.Eval(Container.DataItem, "ParamedicID")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# (DataBinder.Eval(Container.DataItem, "HasAccess").Equals(false)) ? string.Empty : string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}', '{3}', '{4}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"), Request.QueryString["type"], DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                                                                                                        DataBinder.Eval(Container.DataItem, "FromServiceUnitID"), DataBinder.Eval(Container.DataItem, "ParamedicID"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="print" HeaderText="" Groupable="false" Visible="false">
                                    <ItemTemplate>
                                        <%# ((bool)DataBinder.Eval(Container.DataItem, "IsOrder")) ? string.Format("<a href=\"#\" onclick=\"gotoPrintLblUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print Label Order Realization\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo")) : "" %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="TransactionNo" HeaderText="Transaction No"
                                    UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ReferenceNo" HeaderText="Reference No"
                                    UniqueName="ReferenceNo" SortExpression="ReferenceNo" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="75px" DataField="TransactionDate"
                                    HeaderText="Date" UniqueName="TransactionDate" SortExpression="TransactionDate"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                    SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    Visible="false" />
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item" UniqueName="ItemName"
                                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="ParamedicCollectionName" HeaderText="Physician"
                                    UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                                    UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                                    DataType="System.Double" DataFields="ChargeQuantity,Price,DiscountAmount,CitoAmount"
                                    SortExpression="Total" Expression="(({0} * {1}) - {2}) + {3}" FooterStyle-HorizontalAlign="Right"
                                    Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n2}" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsAutoBillTransaction"
                                    HeaderText="Auto Bill" UniqueName="IsAutoBillTransaction" SortExpression="IsAutoBillTransaction"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="65px" DataField="IsCorrection" HeaderText="Correction"
                                    UniqueName="IsCorrection" SortExpression="IsCorrection" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Process"
                                    UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsApproved" HeaderText="Approved"
                                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsPackage" HeaderText="Package"
                                    UniqueName="IsPackage" SortExpression="IsPackage" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# (DataBinder.Eval(Container.DataItem, "IsOrder").Equals(false) ? string.Empty :
                                              string.Format("<a href=\"#\" onclick=\"gotoEditPhyisicianSendersUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/dokter.png\" border=\"0\" title=\"Edit Physician Senders\" /></a>",
                                               DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <%# (DataBinder.Eval(Container.DataItem, "IsOrder").Equals(false) ? string.Empty :
                                              string.Format("<a href=\"#\" onclick=\"openLabelPrint('{0}', '2'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print Label Diagnostic\" title=\"Print Label Diagnostic\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "TransactionNo")))%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ChargeClassID" UniqueName="ChargeClassID" Visible="False" />
                                <telerik:GridBoundColumn DataField="CoverageClassID" UniqueName="CoverageClassID"
                                    Visible="False" />
                                <telerik:GridBoundColumn DataField="ClassID" UniqueName="ClassID" Visible="False" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
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
</asp:Content>
