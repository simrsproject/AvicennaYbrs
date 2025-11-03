<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="PhrList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PhrList" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .viewWrap {
            padding: 5px;
            background-color: gray;
        }

        .childMenu {
            padding: 5px 5px;
            background-color: rgb(54, 72, 91);
            height: 17px;
        }
    </style>


    <telerik:RadCodeBlock runat="server" ID="codeBlock">
        <%=JavascriptOpenPrintPreview()%>
        <script type="text/javascript">
            function tbarPhr_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                if (val.includes('refresh')) {
                    var grdPhr = $find(val.split('_')[1]).get_masterTableView();
                    grdPhr.rebind();
                } else {
                    var vals = val.split('^');
                    entryPhr('new', '', vals[1], vals[2], vals[3], vals[4], vals[5]);
                }
            }

            function entryPhr(md, id, fid, suId, patId, regNo, grdPhrClientId) {
                // Data for grid rebind
                document.getElementById("<%=hdnPatientID.ClientID%>").value = patId;
                document.getElementById("<%=hdnRegistrationNo.ClientID%>").value = regNo;
                document.getElementById("<%=hdnServiceUnitID.ClientID%>").value = suId;

                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regNo + '&patid=' + patId + '&unit=' + suId + '&fid=' + fid + '&menu=su&prgid=<%=ProgramID%>&refno=&ccm=rebind&cet=' + grdPhrClientId;
                window.openWinEntryMaximize(url);
            }

            function printPreviewQuestionForm(tno, regNo, formId) {
                var obj = {};
                obj.transactionNo = tno;
                obj.registrationNo = regNo;
                obj.questionFormID = formId;
                openPrintPreview("PopulatePrintQuestionForm", obj);
            }

            function openWinEntryMaximize(url) {
                var oWnd;
                if (!(url.includes("&rt=") || url.includes("?rt=")))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                oWnd = radopen(url, 'winDialog');
                oWnd.maximize();
            }

            function radWindowManager_ClientClose(oWnd, args) {
                oWnd.setUrl("about:blank"); // Sets url to blank for release variable

                //get the transferred arguments from MasterDialogEntry
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.callbackMethod === 'submit') {
                        __doPostBack(arg.eventTarget, arg.eventArgument);
                    } else {
                        if (arg.callbackMethod === 'rebind') {
                            var ctl = $find(arg.eventTarget);
                            if (typeof ctl.rebind == 'function') {
                                ctl.rebind();
                            } else {
                                var masterTable = $find(arg.eventTarget).get_masterTableView();
                                masterTable.rebind();
                            }
                        }
                    }
                }

            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManagerProxy runat="server" ID="ajxProxy">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPatient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchAddress">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchDateOfBirth">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearchPhoneNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:HiddenField runat="server" ID="hdnServiceUnitID" />
    <asp:HiddenField runat="server" ID="hdnRegistrationNo" />
    <asp:HiddenField runat="server" ID="hdnPatientID" />
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientSearch" runat="server" Text="Patient Name / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox runat="server" ID="txtPatientSearch" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchPatient" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth"></asp:Label>
                            </td>
                            <td class="entry">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="100px">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchDateOfBirth" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPhoneNo" runat="server" Width="100px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchPhoneNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAddress" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td width="30px">
                                <asp:ImageButton ID="btnSearchAddress" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnSearchPatient_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" OnNeedDataSource ="grdList_NeedDataSource"
        AllowSorting="true" OnItemCreated="grdList_ItemCreated" OnItemCommand="grdList_OnItemCommand">
        <MasterTableView DataKeyNames="PatientID"
            GroupLoadMode="Server" HierarchyLoadMode="ServerOnDemand">
            <NestedViewTemplate>
                <asp:Panel runat="server" ID="pnlRegistration" CssClass="viewWrap" Visible="false">
                    <telerik:RadGrid ID="grdRegistration" runat="server"
                        AutoGenerateColumns="False" GridLines="None" OnItemCreated="grdRegistration_OnItemCreated" OnItemCommand="grdRegistration_OnItemCommand">
                        <MasterTableView DataKeyNames="PatientID,RegistrationNo,ServiceUnitID" ShowHeader="True">
                            <Columns>
                                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-Width="140px">
                                    <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="RegistrationDate" HeaderText="Reg. Date">
                                    <ItemTemplate>
                                        <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "RegistrationDate")).ToString(AppConstant.DisplayFormat.DateShortMonth) %><br />
                                        <%#DataBinder.Eval(Container.DataItem, "RegistrationTime") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="ParamedicName"
                                    UniqueName="ParamedicName" SortExpression="ParamedicName">
                                    <HeaderStyle HorizontalAlign="Left" Width="140px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                                    SortExpression="ServiceUnitName">
                                    <HeaderStyle HorizontalAlign="Left" Width="140px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="RoomName" HeaderText="Room / Bed" SortExpression="RoomName">
                                    <ItemTemplate>
                                        R: &nbsp; <%#DataBinder.Eval(Container.DataItem, "RoomName")  %><br />
                                        B: &nbsp;<%#DataBinder.Eval(Container.DataItem, "BedID") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                            </Columns>
                            <NestedViewTemplate>
                                <asp:Panel runat="server" ID="pnlPhr" CssClass="viewWrap" Visible="false">
                                    <telerik:RadTabStrip runat="server" ID="tabStrip" MultiPageID="mpg" SelectedIndex="0">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="Health Record" PageViewID="pvPhr" />
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage runat="server" ID="mpg" SelectedIndex="0" RenderSelectedPageOnly="false"
                                        BorderColor="Black" BorderStyle="Solid">
                                        <telerik:RadPageView runat="server" ID="pvPhr">
                                            <asp:Literal runat="server" ID="litPhrMessage"></asp:Literal>
                                            <telerik:RadToolBar ID="tbarPhr" runat="server" Width="100%" EnableEmbeddedScripts="true"
                                                OnClientButtonClicking="tbarPhr_OnClientButtonClicking">
                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                <Items>
                                                    <telerik:RadToolBarDropDown ID="tbiAdd" runat="server" Text="Add" ImageUrl="~/Images/Toolbar/new16.png"
                                                        HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
                                                    <telerik:RadToolBarButton ID="tbiRefresh" runat="server" Text="Refresh" Value="refresh"
                                                        ImageUrl="~/Images/Toolbar/refresh16.png" Visible="False" />
                                                </Items>
                                            </telerik:RadToolBar>
                                            <telerik:RadGrid ID="grdPhr" runat="server" AllowSorting="true" OnItemCommand="grdPhr_OnItemCommand"
                                                EnableLinqExpressions="false">
                                                <MasterTableView DataKeyNames="ServiceUnitID,PatientID,RegistrationNo,QuestionFormID" AllowMultiColumnSorting="true" AllowFilteringByColumn="False" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="editPhr" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center" AllowFiltering="False">
                                                            <ItemTemplate>
                                                                <%# Eval("UrlEdit")%>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%#string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm( '{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="Document No" HeaderStyle-Width="120px">
                                                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# Eval("UrlView") %>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="RmNO" HeaderText="Form ID" UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-Width="80px">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="QuestionFormName" HeaderText="Form Name" UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                                                            <ItemTemplate>
                                                                <%# Eval("QuestionFormName") %>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Ref No" UniqueName="ReferenceNo"
                                                            SortExpression="ReferenceNo" AllowFiltering="False">
                                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridDateTimeColumn DataField="RecordDateTime" HeaderText="Create Date" UniqueName="RecordDateTime"
                                                            SortExpression="RecordDateTime" AllowFiltering="False">
                                                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridBoundColumn DataField="CreatedByUserName" HeaderText="Create By" UniqueName="CreatedByUserName"
                                                            SortExpression="CreatedByUserName">
                                                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                                                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                                                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridBoundColumn>

                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="False">
                                                    <Selecting AllowRowSelect="False" />
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />

                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </asp:Panel>


                            </NestedViewTemplate>
                        </MasterTableView>

                        <ClientSettings EnableRowHoverStyle="False">
                            <Selecting AllowRowSelect="False" />
                        </ClientSettings>
                    </telerik:RadGrid>

                </asp:Panel>


            </NestedViewTemplate>
            <Columns>

                <telerik:GridBoundColumn DataField="Salutation" HeaderText="Salutation" UniqueName="Salutation"
                    SortExpression="Salutation">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OldMedicalNo" HeaderText="Old Medical No" UniqueName="OldMedicalNo"
                    SortExpression="OldMedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DateOfBirth" HeaderText="DOB" UniqueName="DateOfBirth"
                    SortExpression="DateOfBirth">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" UniqueName="Address"
                    SortExpression="Address">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Phone No" UniqueName="PhoneNo" SortExpression="PhoneNo">
                    <ItemTemplate>
                        <%# Eval("MobilePhoneNo") %>&nbsp;<%# Eval("PhoneNo") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
