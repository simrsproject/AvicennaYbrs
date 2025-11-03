<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="JobOrderRealisationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.JobOrderRealisationList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function openWinProcess(regNo, joNo) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl("JobOrderRealisationDetail.aspx?regNo=" + regNo + "&joNo=" + joNo + '&disch=<%= Page.Request.QueryString["disch"] %>');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function onClientClose(oWnd, args) {
                if (oWnd.argument && oWnd.argument.command == 'rebind') {
                    __doPostBack("<%= grdList.UniqueID %>", 'rebind');
                }
            }

            function gotoHealthRecordUrl(md, id, regno, fid, regType) {
                var url = '';
                if (fid == 'SUMMARY')
                    url = '../ServiceUnitTransaction/PatientMedicalSummaryDetail.aspx?md=' + md + '&regno=' + regno + '&fid=' + fid;
                else
                    url = '../ServiceUnitTransaction/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regno + '&fid=' + fid + '&menu=jo';
                window.location.href = url;
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

            function openWinRegistrationInfo(regNo) {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var lblToBeUpdate = "noti_" + regNo;

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openWinCons(regNo, transNo, seqNo, itemID, serviceUnitID) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                var pageId = document.getElementById('<%= hdnPageId.ClientID %>').value;
                oWnd.setUrl('../ServiceUnitTransaction/ItemConsumptionPackage.aspx?trans=' + transNo + '&seq=' + seqNo + '&item=' + itemID + '&unit=' + serviceUnitID + '&reg=' + regNo + '&pageId=' + pageId + '&md=view');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }

            function openWinFilm(joNo, itemID, seqNo) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl("ExposureFactorDialog.aspx?mode=view&joNo=" + joNo + "&itemID=" + itemID + "&seqNo=" + seqNo);
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
            function openSpecimenCRDetail(id, regno) {
                var oWnd = $find("<%= winProcess.ClientID %>");
                oWnd.setUrl('../ServiceUnitTransaction/SpecimenCollectItem.aspx?type=<%= Request.QueryString["type"]%>&id=' + id + '&reg=' + regno + '&sc=1');
                oWnd.setSize(1000, 600);
                oWnd.show();   
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="820px" Height="600px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winProcess"
        OnClientClose="onClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrintLbl" Animation="None" Width="600px" Height="300px"
        runat="server" Behavior="Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="True" Modal="true" OnClientClose="onClientCloseLabelPrint" Title="Print Label">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <asp:HiddenField runat="server" ID="hdnPageId" />
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadWindowManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterTransactionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterToServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterGuarantor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Timer1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkStatusRejected">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rblFilterDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterBy">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                    <telerik:AjaxUpdatedControl ControlID="grdList2" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxPanel ID="radAjaxPanel" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFilterDateBy" runat="server" Text="Filter By"></asp:Label>
                            </td>
                            <td class="entry">
                                <asp:RadioButtonList ID="rblFilterDate" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rblFilterDate_OnSelectedIndexChanged">
                                    <asp:ListItem Value="OD" Text="Order Date" Selected="True" />
                                    <asp:ListItem Value="ED" Text="Execution Date" />
                                </asp:RadioButtonList>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterBy" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>
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
                                <asp:Label ID="lblTransactionNo" runat="server" Text="Job Order No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterTransactionNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
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

                    </table>
                </td>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblToServiceUnitOD" runat="server" Text="Service Unit Order"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboToServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td width="20">
                                <asp:ImageButton ID="btnFilterToServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
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
                                <asp:Label ID="lblGuarantorID" runat="server" Text="Guarantor"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboGuarantorID" Width="300px" EnableLoadOnDemand="true"
                                    MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboGuarantorID_ItemDataBound"
                                    OnItemsRequested="cboGuarantorID_ItemsRequested">
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
                        <tr runat="server" id="trCasemix">
                            <td class="label"></td>
                            <td class="entry">
                                <asp:CheckBox runat="server" ID="chkStatusRejected" Text="Status Rejected" AutoPostBack="true" OnCheckedChanged="chkStatusRejected_CheckedChanged" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="60000" Enabled="True" />
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Order List" PageViewID="pgOutstanding"
                Selected="True" />
            <telerik:RadTab runat="server" Text="Realization List" PageViewID="pgRealization" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOutstanding" runat="server" Selected="true">
            <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
                OnItemCommand="grdList_ItemCommand" OnItemCreated="grdList_ItemCreated" OnPreRender="grdList_PreRender" OnItemDataBound="grdList_ItemDataBound"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                <MasterTableView Name="master" DataKeyNames="RegistrationNo, TransactionNo" ClientDataKeyNames="RegistrationNo, TransactionNo"
                    GroupLoadMode="Server" HierarchyLoadMode="ServerOnDemand">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Group" HeaderText="Job Order Date " FormatString="{0:dd-MMM-yyyy}"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Group" SortOrder="Descending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <a href="#" onclick="openWinProcess('<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>', '<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>'); return false;">
                                    <img src="../../../../Images/Toolbar/edit16.png" border="0" alt="Edit" /></a>
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
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
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Order"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="135px" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>&nbsp;
                       
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCitoAvailable")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Cito\" title=\"Cito\" />" : string.Empty%>
                                <%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "SpecimenStatus").ToString()) ? (DataBinder.Eval(Container.DataItem, "SpecimenStatus").ToString() == "2" ?"<img src=\"../../../../Images/test_tubes_full16.png\" border=\"0\" alt=\"Specimens received\" title=\"Specimens received\" />" : (DataBinder.Eval(Container.DataItem, "SpecimenStatus").ToString() == "1" ? "<img src=\"../../../../Images/test_tubes_halffull16.png\" border=\"0\" alt=\"Specimens are partially received\" title=\"Specimens are partially received\" />":"<img src=\"../../../../Images/test_tubes_empty16.png\" border=\"0\" alt=\"Specimens have not been received\" title=\"Specimens have not been received\" />")) : string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="TransactionTime" HeaderText="Time"
                            UniqueName="TransactionTime" SortExpression="TransactionTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExecDate" HeaderText="Execution Date"
                            UniqueName="ExecDate" SortExpression="ExecDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
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
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ClusterName" HeaderText="Service Unit" UniqueName="ClusterName"
                            SortExpression="ClusterName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BedID" HeaderText="Bed No"
                            UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                         <telerik:GridTemplateColumn UniqueName="viewSpecimenCRDetail" HeaderText="">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName").ToString().ToLower().Contains("lab") ?
                                string.Format("<a href=\"#\" onclick=\"openSpecimenCRDetail('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Specimen and Collect Method\" /></a>",
                                DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo")) : string.Empty %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintLabel1" HeaderStyle-Width="30px"
                            ItemStyle-HorizontalAlign="center" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintLabel1" runat="server" CommandName="PrintLabel1"
                                    ToolTip='Print Label 1' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintJobOrderNotes" HeaderStyle-Width="30px"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintJobOrderNotes" runat="server" CommandName="PrintJoNotes"
                                    ToolTip='Print Job Order Notes' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Print" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" ToolTip='Print Order Receipt'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openLabelPrint('{0}','1'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print Label\" /></a>",
                                                                    DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" Visible="False" />
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
        <telerik:RadPageView ID="pgRealization" runat="server">
            <telerik:RadGrid ID="grdList2" runat="server" OnNeedDataSource="grdList2_NeedDataSource"
                OnItemCommand="grdList2_ItemCommand" OnItemDataBound="grdList2_ItemDataBound" OnDetailTableDataBind="grdList2_DetailTableDataBind"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" >
                <MasterTableView Name="master" DataKeyNames="RegistrationNo, TransactionNo" ClientDataKeyNames="RegistrationNo, TransactionNo"
                    GroupLoadMode="Server" HierarchyLoadMode="ServerOnDemand">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="Group" HeaderText="Job Order Date " FormatString="{0:dd-MMM-yyyy}"></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="Group" SortOrder="Descending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit Order"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="135px" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>&nbsp;
                       
                                <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsCitoAvailable")) ? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Cito\" title=\"Cito\" />" : string.Empty%>
                                <%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "SpecimenStatus").ToString()) ? (DataBinder.Eval(Container.DataItem, "SpecimenStatus").ToString() == "2" ?"<img src=\"../../../../Images/test_tubes_full16.png\" border=\"0\" alt=\"Specimens received\" title=\"Specimens received\" />" : (DataBinder.Eval(Container.DataItem, "SpecimenStatus").ToString() == "1" ? "<img src=\"../../../../Images/test_tubes_halffull16.png\" border=\"0\" alt=\"Specimens are partially received\" title=\"Specimens are partially received\" />":"<img src=\"../../../../Images/test_tubes_empty16.png\" border=\"0\" alt=\"Specimens have not been received\" title=\"Specimens have not been received\" />")) : string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="TransactionTime" HeaderText="Time"
                            UniqueName="TransactionTime" SortExpression="TransactionTime" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ExecDate" HeaderText="Execution Date"
                            UniqueName="ExecDate" SortExpression="ExecDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
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
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ClusterName" HeaderText="Service Unit" UniqueName="ClusterName"
                            SortExpression="ClusterName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="BedID" HeaderText="Bed No"
                            UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="PaymentNo" HeaderText="Payment No"
                            UniqueName="PaymentNo" SortExpression="PaymentNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="PrintTransactionReceipt" HeaderStyle-Width="30px"
                            ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintTransactionReceipt" runat="server" CommandName="PrintTransactionReceipt"
                                    ToolTip='Transaction Receipt' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="PrintLabel1" HeaderStyle-Width="30px"
                            ItemStyle-HorizontalAlign="center" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnPrintLabel1" runat="server" CommandName="PrintLabel1"
                                    ToolTip='Print Label 1' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openLabelPrint('{0}','1'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"Print\" title=\"Print Label\" /></a>",
                                                                    DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" title=\"Note\" class=\"noti_Container\" onclick=\"openWinRegistrationInfo('{0}'); return false;\"><span id=\"noti_{0}\" class=\"noti_bubble\">{1}</span></a>", 
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "NoteCount")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" Visible="False" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail" DataKeyNames="TransactionNo" AutoGenerateColumns="false">
                            <Columns>
                            <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" SortExpression="SequenceNo"
                                Visible="false" />
                            <telerik:GridTemplateColumn UniqueName="ItemName" HeaderText="Item Name">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                                    <br />
                                    <span style="color: orange"><%# DataBinder.Eval(Container.DataItem, "Notes")%></span>&nbsp
                                    <i><span style="color: green"><%# DataBinder.Eval(Container.DataItem, "SpecimenTypeName")%></span></i>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ParamedicCollectionName" HeaderText="Physician"
                                UniqueName="ParamedicCollectionName" SortExpression="ParamedicCollectionName"
                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ChargeQuantity" HeaderText="Qty"
                                UniqueName="ChargeQuantity" SortExpression="ChargeQuantity" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" />
                            <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="SRItemUnit" HeaderText="Unit"
                                UniqueName="SRItemUnit" SortExpression="SRItemUnit" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="DiscountAmount" HeaderText="Discount"
                                UniqueName="DiscountAmount" SortExpression="DiscountAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CitoAmount" HeaderText="Cito"
                                UniqueName="CitoAmount" SortExpression="CitoAmount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                            <telerik:GridCalculatedColumn HeaderStyle-Width="100px" HeaderText="Total" UniqueName="Total"
                                DataType="System.Double" DataFields="ChargeQuantity,Price,DiscountAmount,CitoAmount"
                                SortExpression="Total" Expression="{0} * (({1} - {2}) + {3})" FooterStyle-HorizontalAlign="Right"
                                Aggregate="Sum" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:n2}" />
                            <telerik:GridTemplateColumn UniqueName="cons" HeaderText="" Groupable="false" Visible="false">
                                <ItemTemplate>
                                    <%# (DataBinder.Eval(Container.DataItem, "IsVoid").Equals(false) ? 
                            string.Format("<a href=\"#\" onclick=\"openWinCons('{0}','{1}','{2}','{3}','{4}'); return false;\"><img src=\"../../../../Images/Toolbar/consumption.png\" border=\"0\" title=\"Item Consumption\" /></a>", DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "SequenceNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "refToTransCharges_ToServiceUnitID")) : string.Empty)%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="" UniqueName="ExposureFactor" SortExpression="ExposureFactor"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <HeaderStyle Width="50px" />
                                <ItemTemplate>
                                    <%# string.Format("<a href=\"#\" onclick=\"openWinFilm('{0}', '{1}', '{2}'); return false;\"><img src=\"../../../../Images/radfilm.png\" border=\"0\" title=\"Exposure Factor\" /></a>",
                                                                                        DataBinder.Eval(Container.DataItem, "TransactionNo"), DataBinder.Eval(Container.DataItem, "ItemID"),
                                                                                                                                                                        DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsCito" HeaderText="Cito"
                                UniqueName="IsCito" SortExpression="IsCito" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
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

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false">
    </telerik:RadAjaxPanel>
</asp:Content>
