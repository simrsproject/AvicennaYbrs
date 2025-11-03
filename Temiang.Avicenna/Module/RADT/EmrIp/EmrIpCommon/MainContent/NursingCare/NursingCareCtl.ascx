<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NursingCareCtl.ascx.cs" 
    Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<telerik:RadCodeBlock ID="radCodeBlock" runat="server">
    <style type="text/css">
        /*Override default button*/
        .onoffswitchns {
            width: 110px;
        }
        .onoffswitchns-switch {
            right: 92px;
        }
        .nursing:before {
            content: "Nursing ON";
        }
        .nursing:after {
            content: "Nursing OFF";
        }
        .midwifery:before {
            content: "Midwifery ON";
        }
        .midwifery:after {
            content: "Midwifery OFF";
        }
        .nutrition:before {
            content: "Nutrition ON";
        }
        .nutrition:after {
            content: "Nutrition OFF";
        }

        .linicnoc{
            margin-left:-27px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function openWinViewNSAssessment(id) {
            //alert(TmpParentID);
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardViewImportedAssessment.aspx?assid=" + id + "");
            oWnd.Show();
            oWnd.Maximize();
        }

        function nSwin_OnClientClose(oWnd, args) {
            //alert("rebind grid");
            if (oWnd.argument == undefined || oWnd.argument == null) {
            } else {
                if (oWnd.argument.result == undefined || oWnd.argument.result == null) {
                } else if (oWnd.argument.result == 'OK') {
                    //alert(oWnd.argument.result);
                    //__doPostBack("<%= gridD.UniqueID %>", "rebind");
                    
                    $find("<%= gridD.ClientID %>").get_masterTableView().rebind();
                }
            }
            oWnd = null;
            //alert("post back");
        }

        function NSRowDblClick(sender, eventArgs) {
            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
        }

        function tbarNursingCare_OnClientButtonClicking(sender, args) {
            var comandName = args.get_item().get_commandName();
            switch (comandName) {
                case "addDiagnose":
                    {
                        var regNo = "<%= RegistrationNo %>";
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardAddDiagnose.aspx?regno=" + regNo + "");
                        oWnd.Show();
                        oWnd.Maximize();
                        break;
                    }
                case "addDiagnoseCustom":
                    {
                        var regNo = "<%= RegistrationNo %>";
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardAddDiagnoseCustom.aspx?regno=" + regNo + "&diagTypes=01");
                        oWnd.Show();
                        oWnd.Maximize();
                        break;
                    }
                case "addDiagnoseMidwifeCustom":
                    {
                        var regNo = "<%= RegistrationNo %>";
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardAddDiagnoseMidwifeCustomWithEdit.aspx?regno=" + regNo + "&diagTypes=02");
                        oWnd.Show();
                        oWnd.Maximize();
                        break;
                    }
                case "addDiagnoseNutritionCustom":
                    {
                        var regNo = "<%= RegistrationNo %>";
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardAddDiagnoseCustom.aspx?regno=" + regNo + "&diagTypes=03");
                        oWnd.Show();
                        oWnd.Maximize();
                        break;
                    }
                case "editDiagnose":
                    {
                        var selectedTypes = GetDiagType();

                        var regNo = "<%= RegistrationNo %>";
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardEditDiagnose.aspx?regno=" + regNo + "&diagTypes=" + selectedTypes);
                        oWnd.Show();
                        oWnd.Maximize();
                        break;
                    }
                case "refresh":
                    {
                        //$find("<%= gridFormPengkajian.ClientID %>").get_masterTableView().rebind();
                        //$find("<%= gridD.ClientID %>").get_masterTableView().rebind();
                        break;
                    }
                case "print": {
                    //var val = args.get_item().get_value();
                    //alert(val);
                    break;
                }
            }
        }

        function NOC(id, name) {
            //alert(id); alert(name);
            var regNo = "<%= RegistrationNo %>";
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardNOC.aspx?regno=" + regNo + "&idL10=" + id + "&name=" + name + "");
            oWnd.Show();
            oWnd.Maximize();
        }

        function NIC(id, name) {
            //alert(id); alert(name);
            var regNo = "<%= RegistrationNo %>";
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardNIC.aspx?regno=" + regNo + "&idL10=" + id + "&name=" + name + "");
            oWnd.Show();
            oWnd.Maximize();
        }

        function Evaluation(id, name, diagtype, readonly) {
            var regNo = "<%= RegistrationNo %>";
            var oWnd = $find("<%= winDialog.ClientID %>");
            if (diagtype == "03") {
                oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardEvaluationADIME.aspx?regno=" + regNo + "&idL10=" + id + "&name=" + name + "&readonly=" + readonly);
            } else {
                oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardEvaluation.aspx?regno=" + regNo + "&idL10=" + id + "&name=" + name + "&readonly=" + readonly);
            }
            oWnd.Show();
            oWnd.Maximize();
        }

        function NsDiagTypeChanged() {
            var toolbar1 = $find("<%= tbarNursingCare.ClientID %>");
            var btnTRefresh = toolbar1.findItemByText("Refresh");
            btnTRefresh.click();
        }

        function GetDiagType() {
            var ns01 = $(<%= chkNsType01.ClientID %>).is(":checked") ? "01;":"";
            //alert(ns01);
            var ns02 = $(<%= chkNsType02.ClientID %>).is(":checked") ? "02;" : "";
            //alert(ns02);
            var ns03 = $(<%= chkNsType03.ClientID %>).is(":checked") ? "03;" : "";
            //alert(ns03);
            return ns01 + ns02 + ns03;
        }
    </script>
</telerik:RadCodeBlock>

<telerik:RadAjaxLoadingPanel ID="ralp_ajaxLoadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="gridFormPengkajian">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gridFormPengkajian" LoadingPanelID="ralp_ajaxLoadingPanel" />
                <telerik:AjaxUpdatedControl ControlID="gridD" LoadingPanelID="ralp_ajaxLoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="gridD">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gridD" LoadingPanelID="ralp_ajaxLoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbarNursingCare">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbarNursingCare" />
                <telerik:AjaxUpdatedControl ControlID="gridFormPengkajian" LoadingPanelID="ralp_ajaxLoadingPanel" />
                <telerik:AjaxUpdatedControl ControlID="gridD" LoadingPanelID="ralp_ajaxLoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
    Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
    ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
    <Windows>
        <telerik:RadWindow ID="winPrintPreview" Animation="None" Width="680px" Height="200px" runat="server"
            ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
            Modal="true">
        </telerik:RadWindow>
        <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
            ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="nSwin_OnClientClose">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<telerik:RadToolBar ID="tbarNursingCare" runat="server" EnableEmbeddedScripts="false" Width="100%" 
    OnClientButtonClicking="tbarNursingCare_OnClientButtonClicking" OnButtonClick="tbarNursingCare_ButtonClick">
    <CollapseAnimation Duration="200" Type="OutQuint" />
    <Items>
        <telerik:RadToolBarDropDown runat="server" Text="Diagnosis" ImageUrl="~/Images/Toolbar/new16.png"
            HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" >
            <Buttons>
                <telerik:RadToolBarButton runat="server" Text="Add based on assessment" Value="addDiagnose" CommandName="addDiagnose"
                    ImageUrl="~/Images/Toolbar/new16_h.png" />
                <telerik:RadToolBarButton runat="server" Text="Add custom (nursing)" Value="addDiagnoseCustom" CommandName="addDiagnoseCustom"
                    ImageUrl="~/Images/Toolbar/new16_h.png" Visible="false" />
                <telerik:RadToolBarButton runat="server" Text="Add custom (midwifery)" Value="addDiagnoseMidwifeCustom" CommandName="addDiagnoseMidwifeCustom"
                    ImageUrl="~/Images/Toolbar/new16_h.png" Visible="false" />
                <telerik:RadToolBarButton runat="server" Text="Add custom (nutrition)" Value="addDiagnoseNutritionCustom" CommandName="addDiagnoseNutritionCustom"
                    ImageUrl="~/Images/Toolbar/new16_h.png" Visible="false" />
                <telerik:RadToolBarButton runat="server" Text="Edit" Value="editDiagnose" CommandName="editDiagnose"
                    ImageUrl="~/Images/Toolbar/edit16_h.png" />
            </Buttons>
        </telerik:RadToolBarDropDown>
        <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" CommandName="refresh"
            ImageUrl="~/Images/Toolbar/refresh16.png" />
        <telerik:RadToolBarDropDown runat="server" Text="Print" ImageUrl="~/Images/Toolbar/print16.png"
            HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" >
            <Buttons>
                <telerik:RadToolBarButton runat="server" Text="Diagnosis And Planning (Nursing)" Value="SLP.10.0002" CommandName="print"
                    ImageUrl="~/Images/Toolbar/print16_h.png" />
                <telerik:RadToolBarButton runat="server" Text="Diagnosis And Planning (Midwifery)" Value="SLP.10.0002b" CommandName="print"
                    ImageUrl="~/Images/Toolbar/print16_h.png" />
                <telerik:RadToolBarButton runat="server" Text="Implementation" Value="SLP.10.0003" CommandName="print"
                    ImageUrl="~/Images/Toolbar/print16_h.png" />
                <telerik:RadToolBarButton runat="server" Text="Evaluation" Value="SLP.10.0004" CommandName="print"
                    ImageUrl="~/Images/Toolbar/print16_h.png" />
            </Buttons>
        </telerik:RadToolBarDropDown>
    </Items>
</telerik:RadToolBar>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td valign="top" style="width: 20%">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Diagnosis Type</legend>
                            <table>
                                <tr>
                                    <td>
                                        <div class="onoffswitch onoffswitchns" id="divSelected" runat="server" >
                                            <input type='checkbox' name='chkNsType01' class='onoffswitch-checkbox' 
                                            id='chkNsType01' runat='server' onchange="javascript:NsDiagTypeChanged();" />
                                            <label id="lblSwitch01" runat="server" class="onoffswitch-label">
                                                <span class="onoffswitch-inner nursing"></span>
                                                <span class="onoffswitch-switch onoffswitchns-switch"></span>
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="onoffswitch onoffswitchns" id="div2" runat="server" >
                                            <input type='checkbox' name='chkNsType02' class='onoffswitch-checkbox' 
                                            id='chkNsType02' runat='server' onchange="javascript:NsDiagTypeChanged();" />
                                            <label id="lblSwitch02" runat="server" class="onoffswitch-label">
                                                <span class="onoffswitch-inner midwifery"></span>
                                                <span class="onoffswitch-switch onoffswitchns-switch"></span>
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="onoffswitch onoffswitchns" id="div1" runat="server" >
                                            <input type='checkbox' name='chkNsType03' class='onoffswitch-checkbox' 
                                            id='chkNsType03' runat='server' onchange="javascript:NsDiagTypeChanged();" />
                                            <label id="lblSwitch03" runat="server" class="onoffswitch-label">
                                                <span class="onoffswitch-inner nutrition"></span>
                                                <span class="onoffswitch-switch onoffswitchns-switch"></span>
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="gridFormPengkajian" runat="server" 
                            OnNeedDataSource="gridFormPengkajian_NeedDataSource"
                            OnItemCommand="gridFormPengkajian_ItemCommand"
                            GridLines="None" AutoGenerateColumns="false" 
                            AllowSorting="true" Width="100%" Height="100%">
                            <HeaderContextMenu>
                            </HeaderContextMenu>
                            <MasterTableView DataKeyNames="TransactionNo" ClientDataKeyNames="TransactionNo">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Assessment List" UniqueName="TransactionNo">
                                        <HeaderStyle HorizontalAlign="Left"/>
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td>No.</td>
                                                    <td>
                                                        <asp:LinkButton ID="lbImport" runat="server" 
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>' 
                                                            ForeColor="Red" 
                                                            CommandName="ImportPHR"
                                                            OnClientClick="return confirm('Are you sure you want to import?');"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>'
                                                            ></asp:LinkButton>
                                                    </td>
                                                    <td align="right">
                                                        <span><%# DataBinder.Eval(Container.DataItem, "RecordTime").ToString()%></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Form</td>
                                                    <td colspan="2">
                                                        <span><%# DataBinder.Eval(Container.DataItem, "QuestionFormName").ToString()%></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Status</td>
                                                    <td>
                                                        <span><%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Id").ToString()) ? "" : (string.Format("<a href='javascript:void(0);' onclick='javascript:openWinViewNSAssessment({0});'>&nbsp;Imported</a>", DataBinder.Eval(Container.DataItem, "Id").ToString()))%></span>
                                                                                
                                                    </td>
                                                    <td align="right">
                                                        <asp:LinkButton ID="lbd" runat="server" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() %>' 
                                                        ForeColor="Red" 
                                                        CommandName="DeletePHR"
                                                        OnClientClick="return confirm('Are you sure you want to delete?');"
                                                        Visible='<%# IsUserEditAble && !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Id").ToString()) %>'>
                                                            <img src="../../../Images/Toolbar/delete16.png" border="0" title="Delete" />
                                                        </asp:LinkButton>&nbsp;
<%--                                                        <asp:LinkButton ID="lb" runat="server" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>' 
                                                        ForeColor="Red" 
                                                        CommandName="PrintPHR"
                                                        >
                                                            <img src="../../../Images/Toolbar/print16.png" border="0" title="Print" />
                                                        </asp:LinkButton>--%>
                                                        <%#Eval("TransactionNo")==DBNull.Value || string.IsNullOrEmpty(Eval("TransactionNo").ToString() )? string.Empty:string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm( '{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>

                                                    </td>

                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <FilterMenu>
                            </FilterMenu>
                            <ClientSettings>
                                <Scrolling ScrollHeight="500px" UseStaticHeaders="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <telerik:RadGrid ID="gridD" runat="server" 
                OnNeedDataSource="gridD_NeedDataSource"
                OnItemCommand="gridD_ItemCommand"
                OnItemDataBound="gridD_ItemDataBound"
                GridLines="None" AutoGenerateColumns="false" 
                AllowSorting="true" AllowFilteringByColumn="false" FilterType="CheckList"
                OnFilterCheckListItemsRequested="gridD_FilterCheckListItemsRequested">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="NursingDiagnosaID,ID" ClientDataKeyNames="NursingDiagnosaID,ID" >
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="No" UniqueName="Priority" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="30px"
                            AllowFiltering="false">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <span><%# DataBinder.Eval(Container.DataItem, "Priority").ToString()%></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Date" UniqueName="ExecuteDateTime" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="80px"
                            AllowFiltering="false">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <span><%# ((DateTime)DataBinder.Eval(Container.DataItem, "ExecuteDateTime")).ToString("dd/MM/yyyy HH:mm")%></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Diagnosis, Outcomes / Immediate Action and Interventions" UniqueName="NursingDiagnosaNameDisplay"
                            AllowFiltering="false" FilterCheckListEnableLoadOnDemand="true">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="5" border="0" style="width:100%;">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label id="lblNDName" runat="server"><strong><%# string.Format("<span title='{1}'>{2}{0}</span>",DataBinder.Eval(Container.DataItem, "NursingDiagnosaNameDisplay").ToString(),DataBinder.Eval(Container.DataItem, "F1").ToString(),string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "F1").ToString()) ? "":"*") %></strong></asp:Label>
                                            <asp:LinkButton ID="lbd" runat="server" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>' 
                                            ForeColor="Red" 
                                            CommandName="DeleteD"
                                            OnClientClick="return confirm('Are you sure you want to delete?');"
                                            Visible='<%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "NOC").ToString()) && string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "NIC").ToString())) %>'>
                                                <img src="../../../Images/Toolbar/delete16.png" border="0" title="Delete" />
                                            </asp:LinkButton>
                                        </td>
                                        <td rowspan="2" style='width:33px;vertical-align:top;<%= IsUserEditAble?"":"display:none;" %>'>
                                            <table cellpadding="0" cellspacing="5" border="0" >
                                                <tr>
                                                    <td>
                                                        <a href="javascript:void(0);"
                                                        style='<%# ((DataBinder.Eval(Container.DataItem, "SRNursingCarePlanning").ToString() == "01") || (DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "03")) ? "display:none":"" %>'
                                                        onclick='<%# string.Format("javascript:NOC({0},\"{1}\");",DataBinder.Eval(Container.DataItem, "ID").ToString(), Helper.StripHTML(DataBinder.Eval(Container.DataItem, "NursingDiagnosaNameDisplay").ToString()).Replace("\r","").Replace("'","&#39;").Replace("\"","&#39;&#39;")) %>' >
                                                            <%# string.Format("<img src='../../../Images/Toolbar/{0}.png' border='0' title='{1}' />", 
                                                                (DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "02") ? "ImmediateAction32": "Outcomes32",
                                                                (DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "02") ? AppSession.Parameter.NsOutcome02: AppSession.Parameter.NsOutcome) %>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:void(0);"
                                                        style='<%# (DataBinder.Eval(Container.DataItem, "SRNursingCarePlanning").ToString() != "") ? "display:none":"" %>'
                                                        onclick='<%# string.Format("javascript:NIC({0},\"{1}\");",DataBinder.Eval(Container.DataItem, "ID").ToString(), Helper.StripHTML(DataBinder.Eval(Container.DataItem, "NursingDiagnosaNameDisplay").ToString()).Replace("\r","").Replace("'","&#39;").Replace("\"","&#39;&#39;")) %>' >
                                                            <%# string.Format("<img src='../../../Images/Toolbar/{0}.png' border='0' title='{1}' />", 
                                                                (DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "01") ? "Interventions32":"Interventions32",
                                                                (DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "01") ? AppSession.Parameter.NsIntervention:((DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "03") ? "Interventions":"Planning / Interventions")) %>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:void(0);"
                                                        style='<%# (DataBinder.Eval(Container.DataItem, "SRNursingCarePlanning").ToString() == "01") ? "display:none":"" %>'
                                                        onclick='<%# string.Format("javascript:Evaluation({0},\"{1}\",\"{2}\",0);",
                                                            DataBinder.Eval(Container.DataItem, "ID").ToString(), 
                                                            Helper.StripHTML(DataBinder.Eval(Container.DataItem, "NursingDiagnosaNameDisplay").ToString()).Replace("\r","").Replace("'","&#39;").Replace("\"","&#39;&#39;"),
                                                            DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString()) %>' >
                                                            <%# string.Format("<img src='../../../Images/Toolbar/{0}.png' border='0' title='{1}' />", 
                                                                (DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "01") ? "Evaluation32":"Evaluation32",
                                                                (DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString() == "01") ? "Evaluation":"Evaluation") %>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label id="Label1" runat="server"><%# DataBinder.Eval(Container.DataItem, "EtiologyActual").ToString()%></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label id="Label5" runat="server"><%# DataBinder.Eval(Container.DataItem, "Noc").ToString()%></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label id="Label4" runat="server"><%# DataBinder.Eval(Container.DataItem, "Nic").ToString()%></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Evaluation Period /<br />Status" UniqueName="EvalPeriod" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="80px"
                            AllowFiltering="false">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <span><%#System.Convert.ToDouble(Eval("EvalPeriod"))%> X <%#System.Convert.ToDouble(Eval("PeriodConversionInHour"))%> hour(s)</span>
                                <br />
                                <span><%# DataBinder.Eval(Container.DataItem, "SRNursingCarePlanningName").ToString().ToUpper() != "STOP" ? 
                                DataBinder.Eval(Container.DataItem, "SRNursingCarePlanningName").ToString().ToUpper():(string.Format("<a href='javascript:void(0);' onclick=\"Evaluation('{0}', '{1}', '{2}', 1)\">Stop</a>",
                                DataBinder.Eval(Container.DataItem, "ID").ToString(), 
                                DataBinder.Eval(Container.DataItem, "NursingDiagnosaNameDisplay").ToString().Replace("\r","").Replace("'","&#39;").Replace("\"","&#39;&#39;"),
                                DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString())) %></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Status" UniqueName="SRNursingCarePlanningName"  ItemStyle-VerticalAlign="Top" Visible="false">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <span><%# DataBinder.Eval(Container.DataItem, "SRNursingCarePlanningName").ToString().ToUpper() != "STOP" ? 
                                DataBinder.Eval(Container.DataItem, "SRNursingCarePlanningName").ToString().ToUpper():(string.Format("<a href='javascript:void(0);' onclick=\"Evaluation('{0}', '{1}', '{2}', 1)\">Stop</a>",
                                DataBinder.Eval(Container.DataItem, "ID").ToString(), 
                                DataBinder.Eval(Container.DataItem, "NursingDiagnosaNameDisplay").ToString().Replace("\r","").Replace("'","&#39;").Replace("\"","&#39;&#39;"),
                                DataBinder.Eval(Container.DataItem, "SRNsDiagnosaType").ToString())) %></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings>
                    <Scrolling AllowScroll="true" ScrollHeight="500px" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>