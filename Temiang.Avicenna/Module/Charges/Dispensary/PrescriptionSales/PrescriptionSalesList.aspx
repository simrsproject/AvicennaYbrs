<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="PrescriptionSalesList.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.PrescriptionSalesList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoEditUrl(pno, regno, direct, otc) {
                if (otc == 'True') {
                    var url = 'PrescriptionSalesDetail.aspx?md=edit&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&mode=otc&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                    window.location.href = url;
                }
                else if (direct == 'True') {
                    var url = 'PrescriptionSalesDetail.aspx?md=edit&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&mode=direct&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                    window.location.href = url;
                }
                else {
                    var url = 'PrescriptionSalesDetail.aspx?md=edit&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                    window.location.href = url;
                }
            }

            function gotoViewUrl(pno, regno, direct, otc) {

                if (otc == 'True') {
                    var url = 'PrescriptionSalesDetail.aspx?md=view&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&mode=otc&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                    window.location.href = url;
                }
                else if (direct == 'True') {
                    var url = 'PrescriptionSalesDetail.aspx?md=view&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&mode=direct&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                    window.location.href = url;
                }
                else {
                    var url = 'PrescriptionSalesDetail.aspx?md=view&pno=' + pno + '&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                    window.location.href = url;
                }
            }

            function gotoAddUrl(regno) {
                var url = 'PrescriptionSalesDetail.aspx?md=new&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                window.location.href = url;
            }

            function gotoAdd2Url(regno, soape) {
                if (soape == 'False') {
                    alert('SOAPE required.');
                } else {
                    var url = 'PrescriptionSalesDetail.aspx?md=new&regno=' + regno + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>' + "&ono=";
                    window.location.href = url;
                }

            }

            function viewHistory(patientID) {
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('PrescriptionHistoryDialog.aspx?pid=' + patientID + "&rt=" + '<%= Request.QueryString["rt"] %>');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function viewOutstandingDelivery(regNo, prescNo) {
                var oWnd = $find("<%= winHistory.ClientID %>");
                oWnd.setUrl('PrescriptionSalesDelivery.aspx?regno=' + regNo + "&pno=" + prescNo + "&type=" + '<%= Request.QueryString["type"] %>' + "&rt=" + '<%= Request.QueryString["rt"] %>');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "direct":
                        var url = 'PrescriptionSalesDetail.aspx?md=new&regno=&type=' + '<%= Request.QueryString["type"] %>' + '&mode=direct&rt=' + '<%= Request.QueryString["rt"] %>' + '&ono=';
                        window.location.href = url;
                        break;
                    case "otc":
                        var url = 'PrescriptionSalesDetail.aspx?md=new&regno=&type=' + '<%= Request.QueryString["type"] %>' + '&mode=otc&rt=' + '<%= Request.QueryString["rt"] %>' + '&ono=';
                        window.location.href = url;
                        break;
                }
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

            function barcodeChanged(sender, eventArgs) {
               <%-- var oWnd = $find("<%= winDocsOption.ClientID %>");
                console.log(sender);
                oWnd.setUrl('');
                //oWnd.setUrl('<%=Page.ResolveUrl("~/")%>WebService/KioskQueue.asmx/PrescriptionGetOneByKeyword?Keyword=' + sender.get_value());
                oWnd.set_title('Prescription Status');
                oWnd.show();

                eventArgs.set_cancel(true);--%>
            }

            function openRpt() {
                var oWnd = $find('<%=winPrint.ClientID%>');
                oWnd.SetUrl('<%=Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx")%>');
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
                return;
            }

            function openWinApolKlaimDash() {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("../../../RADT/Bpjs/ApotekOnline/ApotekOnlineDashboard.aspx");
                oWnd.show();
                oWnd.setSize($(window).width() * 0.95, $(window).height() * 0.95);
                oWnd.center();
            }

            function openWinRef() {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("../../../RADT/Bpjs/ApotekOnline/ApotekOnlineReference.aspx");
                oWnd.show();
                oWnd.setSize($(window).width() * 0.95, $(window).height() * 0.95);
                oWnd.center();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" />
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="550px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winHistory">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="400px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" Title="Document Checklist"
        ID="winDocsOption">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winDialog" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterParamedic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterOrderDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistrationType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                    <telerik:AjaxUpdatedControl ControlID="lblRegistrationCount" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionSRFloor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionDispensary">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterPrescriptionStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterDeliveryStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterKioskQueueNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdRegistration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdPrescription">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtBarcode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPrescription" />
                    <telerik:AjaxUpdatedControl ControlID="txtRegistrationNo" />
                    <telerik:AjaxUpdatedControl ControlID="txtBarcode" />
                    <telerik:AjaxUpdatedControl ControlID="lblBarcodeMsg" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Direct Prescription" Value="direct"
                ImageUrl="~/Images/Toolbar/insert16.png" />
            <telerik:RadToolBarButton runat="server" Text="OTC" Value="otc" ImageUrl="~/Images/Toolbar/insert16_d.png"
                HoveredImageUrl="~/Images/Toolbar/insert16.png" />
        </Items>
    </telerik:RadToolBar>
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
                <td style="vertical-align: central; width: 100px">
                    
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Registration List" PageViewID="pgOrder" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Prescription List" PageViewID="pgList">
            </telerik:RadTab>
            <telerik:RadTab runat="server" ID="TabPrescApol" Text="Prescription Apol List" PageViewID="pgDataApol" Visible="false">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgOrder" runat="server" Selected="true">
            <asp:Panel ID="pnlRegDate" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%" valign="top">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="110px" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnFilterRegistrationDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trRegistrationType">
                                    <td class="label">
                                        <asp:Label ID="lblRegistrationType" runat="server" Text="RegistrationType"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadComboBox ID="cboRegistrationType" Width="300px" runat="server" EnableLoadOnDemand="true"
                                            HighlightTemplatedItems="true" OnItemDataBound="cboRegistrationType_ItemDataBound"
                                            OnItemsRequested="cboRegistrationType_ItemsRequested">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ItemName")%> </b>
                                            </ItemTemplate>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnFilterRegistrationType" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%" valign="top">
                            <table width="100%">
                                
                            </table>
                        </td>
                        <td style="vertical-align: central; width: 100px">
                            <fieldset style="width: 50px">
                                <legend>Count</legend>
                                <asp:Label ID="lblRegistrationCount" runat="server" Text="" Font-Size="20px"></asp:Label>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlOrderDate" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%">
                            <table width="100%">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblOrderDate" runat="server" Text="Order Date"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="110px" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnFilterOrderDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                            OnClick="btnFilter_Click" ToolTip="Search" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <telerik:RadGrid ID="grdRegistration" runat="server" OnNeedDataSource="grdRegistration_NeedDataSource" OnItemCommand="grdRegistration_ItemCommand"
                OnDetailTableDataBind="grdRegistration_DetailTableDataBind" AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15">
                <MasterTableView DataKeyNames="RegistrationNo, IsCheckinConfirmed" AutoGenerateColumns="false"
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
                        <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsCheckinConfirmed").Equals(false) ? string.Empty : 
                                string.Format("<a href=\"#\" onclick=\"gotoAddUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "RegistrationNo"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="New2" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (this.IsUserAddAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsCheckinConfirmed").Equals(false) ? string.Empty : 
                                string.Format("<a href=\"#\" onclick=\"gotoAdd2Url('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"New\" /></a>",
                                                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "Soape")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Delivery" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (DataBinder.Eval(Container.DataItem, "IsHasPendingDelivery").Equals(false) ? string.Empty :
                                    string.Format("<a href=\"#\" onclick=\"viewOutstandingDelivery('{0}', '{1}'); return false;\"><img src=\"../../../../Images/Toolbar/consumption.png\" border=\"0\" title=\"Prescription Delivery\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "RegistrationNo"), string.Empty))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                            SortExpression="RegistrationDate">
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
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
                        <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="Sex" HeaderText="Gender"
                            UniqueName="Sex" SortExpression="Sex" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                            SortExpression="GuarantorName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                            SortExpression="RoomName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                            HeaderStyle-Width="80px" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <a href="#" onclick="viewHistory('<%# DataBinder.Eval(Container.DataItem, "PatientID") %>'); return false;">
                                    <img src="../../../../Images/Toolbar/details16.png" border="0" title="Prescription List" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
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
                        <telerik:GridTableView Name="History" DataKeyNames="PrescriptionNo" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="edit" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# (this.IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApproval").Equals(true) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsCheckinConfirmed").Equals(false) ? string.Empty :
                                        string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "IsDirect"), DataBinder.Eval(Container.DataItem, "IsOtc"))) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "IsCheckinConfirmed").Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}','{3}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "IsDirect"), DataBinder.Eval(Container.DataItem, "IsOtc")) %>
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
                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Dispensary Name"
                                    UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                    SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Proceed"
                                    UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproval" HeaderText="Approved"
                                    UniqueName="IsApproval" SortExpression="IsApproval" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
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
                                    <telerik:RadDatePicker ID="txtPrescriptionDate" runat="server" Width="110px" />
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
                                <td><label id="lblBarcodeMsg" runat="server" style="color:red;"></label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdPrescription" runat="server" OnNeedDataSource="grdPrescription_NeedDataSource"
                OnDetailTableDataBind="grdPrescription_DetailTableDataBind" AllowSorting="true"
                ShowStatusBar="true" AllowPaging="true" PageSize="15" OnItemCommand="grdPrescription_ItemCommand">
                <MasterTableView DataKeyNames="PrescriptionNo" AutoGenerateColumns="false" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" HeaderText="Dispensary "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="DispensaryName" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "IsCheckinConfirmed").Equals(false) ? string.Empty : string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}','{3}','{4}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                    DataBinder.Eval(Container.DataItem, "PrescriptionNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "OrderNo"), DataBinder.Eval(Container.DataItem, "IsDirect"), DataBinder.Eval(Container.DataItem, "IsOtc"))%>
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
                        <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                            SortExpression="PatientName">
                            <ItemTemplate>
                                <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" HeaderStyle-Width="150px"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                            SortExpression="ParamedicID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Notes" UniqueName="Note" SortExpression="Note"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsBillProceed" HeaderText="Proceed"
                            UniqueName="IsBillProceed" SortExpression="IsBillProceed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="False" />
                        <telerik:GridBoundColumn DataField="ApprovalDateTime" HeaderText="Approval Date"
                            UniqueName="ApprovalDateTime" SortExpression="ApprovalDateTime" DataType="System.DateTime"
                            DataFormatString="{0:MM/dd/yyyy HH:mm}">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproval" HeaderText="Approved"
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
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsDirect" HeaderText="Direct"
                            UniqueName="IsDirect" SortExpression="IsDirect" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="KioskQueueNo" HeaderText="Queue No" HeaderStyle-Width="70px"
                            UniqueName="KioskQueueNo" SortExpression="KioskQueueNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridTemplateColumn UniqueName="fStatus" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbStatus" runat="server" CommandArgument='<%#string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "PrescriptionNo"),((int)DataBinder.Eval(Container.DataItem, "Status")) == 3 ? "" : (((int)DataBinder.Eval(Container.DataItem, "Status")) == 2 ? "deliver":"complete"))%>'
                                    CommandName="setStatus" ToolTip='<%#string.Format("Status: {0}", ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "Delivered" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "Complete" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "In Progress" : "Outstanding")))) %>'
                                    OnClientClick='<%#string.Format("{0}", (int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "alert(\"Prescription has been delivered\"); return false;":((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "return confirm(\"Are you sure want to set as delivered?\");" : "return confirm(\"Are you sure want to set as complete?\");")) %>'>
                                    <%#string.Format("<img style=\"border: 0px; text-align:center; vertical-align: middle;\" src=\"../../../../Images/Toolbar/{0} \" />",
                                        ((int)DataBinder.Eval(Container.DataItem, "Status") == 3 ? "post16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 2 ? "post_green_16.png" : ((int)DataBinder.Eval(Container.DataItem, "Status") == 1 ? "post_yellow_16.png" : "post16_d.png"))))%>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="History" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <a href="#" onclick="viewHistory('<%# DataBinder.Eval(Container.DataItem, "PatientID") %>'); return false;">
                                    <img src="../../../../Images/Toolbar/details16.png" border="0" title="Prescription List" /></a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Profile" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblEdit" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%>'
                                    CommandName="Profile" ToolTip="Profile">
                                    <img style="border: 0px; text-align:center; vertical-align: middle;" src="../../../../Images/Toolbar/print16.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle HorizontalAlign="Center" />
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
                                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="DosageQty" HeaderText="Dosing"
                                    UniqueName="DosageQty" SortExpression="DosageQty" HeaderStyle-HorizontalAlign="Center"
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
        <telerik:RadPageView ID="pgDataApol" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">Kode PPK</td>
                                <td class="entry">
                                    <telerik:RadTextBox ID="txtPPK" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Jenis Obat </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboJnsObt" runat="server" Width="304px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="Semua" Selected="true" />
                                            <telerik:RadComboBoxItem Value="1" Text="Obat PRB" />
                                            <telerik:RadComboBoxItem Value="2" Text="Obat Kronis Blm Stabil" />
                                            <telerik:RadComboBoxItem Value="3" Text="Obat Kemoterapi" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr runat="server" id="trBpjsApol">
                                <td class="label">
                                    <asp:Label ID="bpjaspol" runat="server" Text="Etc."></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnOpenDialog" runat="server" Text="📊" OnClientClick="openWinApolKlaimDash(); return false;" />
                                    <asp:Button ID="btnOpenRef" runat="server" Text="🛠" OnClientClick="openWinRef(); return false;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%" valign="top">
                        <table width="100%">
                            <tr>
                                <td class="label">Jenis Tanggal </td>
                                <td class="entry">
                                    <telerik:RadComboBox ID="cboTgl" runat="server" Width="304px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="TGLPELSJP" Text="Tanggal Pelayanan" />
                                            <telerik:RadComboBoxItem Value="TGLRSP" Text="Tanggal Resep" Selected="true" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">Tanggal </td>
                                <td class="entry">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadDatePicker ID="txtTglAwal" runat="server" Width="100px" />
                                            </td>
                                            <td>&nbsp;-&nbsp;</td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtTglAkhir" runat="server" Width="100px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnDaftarResepApol" runat="server" Text="List Data Apol" OnClick="btnDaftarResepApol_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdListApol" runat="server" OnNeedDataSource="grdListApol_NeedDataSource"
                AutoGenerateColumns="False" ShowGroupPanel="false" AllowPaging="True" PageSize="15"
                AllowSorting="True" GridLines="None" OnDeleteCommand="grdListApol_DeleteCommand">
                <MasterTableView DataKeyNames="NORESEP, NOSEP_KUNJUNGAN, NOKARTU" ClientDataKeyNames="NORESEP, NOSEP_KUNJUNGAN, NOKARTU">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="NORESEP" HeaderText="NO RESEP"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="NORESEP" SortExpression="NORESEP"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="NOAPOTIK" HeaderText="NO APOTIK"
                            HeaderStyle-HorizontalAlign="Center" UniqueName="NOAPOTIK" SortExpression="NOAPOTIK"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="NOSEP_KUNJUNGAN" HeaderText="NO SEP" UniqueName="NOSEP_KUNJUNGAN"
                            SortExpression="NOSEP_KUNJUNGAN" />
                        <telerik:GridBoundColumn DataField="NOKARTU" HeaderText="NO KARTU"
                            UniqueName="NOKARTU" SortExpression="NOKARTU" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="PesertaNama" HeaderText="NAMA" UniqueName="PesertaNama"
                            SortExpression="PesertaNama" />
                        <telerik:GridDateTimeColumn DataField="TGLENTRY" HeaderText="TGL ENTRY" UniqueName="TGLENTRY"
                            SortExpression="TGLENTRY" />
                        <telerik:GridDateTimeColumn DataField="TGLRESEP" HeaderText="TGL RESEP" UniqueName="TGLRESEP"
                            SortExpression="TGLRESEP" />
                        <telerik:GridDateTimeColumn DataField="TGLPELRSP" HeaderText="TGL PELAYANAN" UniqueName="TGLPELRSP"
                            SortExpression="TGLPELRSP" />
                        <telerik:GridBoundColumn DataField="BYTAGRSP" HeaderText="BY TAGIHAN" UniqueName="BYTAGRSP"
                            SortExpression="BYTAGRSP" />
                        <telerik:GridBoundColumn DataField="BYVERRSP" HeaderText="BY VERIF" UniqueName="BYVERRSP"
                            SortExpression="BYVERRSP" />
                        <telerik:GridBoundColumn DataField="KDJNSOBAT" HeaderText="JENIS" UniqueName="KDJNSOBAT"
                            SortExpression="KDJNSOBAT" />
                        <telerik:GridBoundColumn DataField="FASKESASAL" HeaderText="FASKES ASAL" UniqueName="FASKESASAL"
                            SortExpression="FASKESASAL" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
