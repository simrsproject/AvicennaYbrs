<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NutritionCareCtl.ascx.cs" 
    Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NutritionCare.NutritionCareCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>


<telerik:RadCodeBlock ID="radCodeBlock" runat="server">
    <script language="javascript" type="text/javascript">
        function openWinViewAssessment(id) {
            //alert(TmpParentID);
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NutritionCare/NutritionCareStandardViewImportedAssessment.aspx?assid=" + id + "");
            oWnd.Show();
            oWnd.Maximize();
        }

        function nS_OnClientClose(oWnd, args) {
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

    function RowDblClick(sender, eventArgs) {
        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    }

    function tbarNutritionCare_OnClientButtonClicking(sender, args) {
        var comandName = args.get_item().get_commandName();
        switch (comandName) {
            case "addDiagnose":
                {
                    var regNo = "<%= RegistrationNo %>";
                    var oWnd = $find("<%= winDialog.ClientID %>");
                    oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NutritionCare/NutritionCareStandardAddDiagnose.aspx?regno=" + regNo + "");
                    oWnd.Show();
                    oWnd.Maximize();
                    break;
                }
            case "addDiagnoseCustom":
                {
                    var regNo = "<%= RegistrationNo %>";
                        var oWnd = $find("<%= winDialog.ClientID %>");
                    oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NutritionCare/NutritionCareStandardAddDiagnoseCustom.aspx?regno=" + regNo + "");
                        oWnd.Show();
                        oWnd.Maximize();
                        break;
                    }
                case "editDiagnose":
                    {
                        var regNo = "<%= RegistrationNo %>";
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NutritionCare/NutritionCareStandardEditDiagnose.aspx?regno=" + regNo + "");
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

        function NICNU(id, name) {
            var regNo = "<%= RegistrationNo %>";
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NutritionCare/NutritionCareStandardNIC.aspx?regno=" + regNo + "&idL10=" + id + "&name=" + name + "");
            oWnd.Show();
            oWnd.Maximize();
        }

        function EvaluationNU(id, name) {
            var regNo = "<%= RegistrationNo %>";
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.SetUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NutritionCare/NutritionCareStandardEvaluation.aspx?regno=" + regNo + "&idL10=" + id + "&name=" + name + "");
            oWnd.Show();
            oWnd.Maximize();
        }
    </script>
</telerik:RadCodeBlock>

<telerik:RadAjaxLoadingPanel ID="ralp_ajaxLoadingPanel" runat="server"></telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="gridFormPengkajian">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gridFormPengkajian" LoadingPanelID="ralp_ajaxLoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="gridD">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="gridD" LoadingPanelID="ralp_ajaxLoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="tbarNutritionCare">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="tbarNutritionCare" />
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
            ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="nS_OnClientClose">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<telerik:RadToolBar ID="tbarNutritionCare" runat="server" Width="100%" EnableEmbeddedScripts="false"
    OnClientButtonClicking="tbarNutritionCare_OnClientButtonClicking" OnButtonClick="tbarNutritionCare_ButtonClick">
    <CollapseAnimation Duration="200" Type="OutQuint" />
    <Items>
        <telerik:RadToolBarDropDown runat="server" Text="Diagnose" ImageUrl="~/Images/Toolbar/new16.png"
            HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" >
            <Buttons>
                <telerik:RadToolBarButton runat="server" Text="Add (based on assessment)" Value="addDiagnose" CommandName="addDiagnose"
                    ImageUrl="~/Images/Toolbar/new16_h.png" visible="False" />
                <telerik:RadToolBarButton runat="server" Text="Add (custom)" Value="addDiagnoseCustom" CommandName="addDiagnoseCustom"
                    ImageUrl="~/Images/Toolbar/new16_h.png" />
                <telerik:RadToolBarButton runat="server" Text="Edit" Value="editDiagnose" CommandName="editDiagnose"
                    ImageUrl="~/Images/Toolbar/edit16_h.png" Visible="false" />
            </Buttons>
        </telerik:RadToolBarDropDown>
        <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh" CommandName="refresh"
            ImageUrl="~/Images/Toolbar/refresh16.png" />
        <telerik:RadToolBarDropDown runat="server" Text="Print" ImageUrl="~/Images/Toolbar/print16.png"
            HoveredImageUrl="~/Images/Toolbar/print16_h.png" DisabledImageUrl="~/Images/Toolbar/print16_d.png" Visible="false" >
            <Buttons>
                <telerik:RadToolBarButton runat="server" Text="Diagnosa And Planning" Value="SLP.10.0002" CommandName="print"
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
        <td valign="top" style="width: 20%; display: none" >
            <table width="100%" cellpadding="0" cellspacing="0">
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
                                                    <td colspan="2">
                                                        <asp:LinkButton ID="lbImport" runat="server" 
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>' 
                                                            ForeColor="Red" 
                                                            CommandName="ImportPHR"
                                                            OnClientClick="return confirm('Are you sure you want to import?');"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>'
                                                            ></asp:LinkButton>
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
                                                        <span><%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Id").ToString()) ? "" : (string.Format("<a href='javascript:void(0);' onclick='javascript:openWinViewAssessment({0});'>&nbsp;Imported</a>", DataBinder.Eval(Container.DataItem, "Id").ToString()))%></span>
                                                                                
                                                    </td>
                                                    <td align="right">
                                                        <asp:LinkButton ID="lbd" runat="server" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() %>' 
                                                        ForeColor="Red" 
                                                        CommandName="DeletePHR"
                                                        OnClientClick="return confirm('Are you sure you want to delete?');"
                                                        Visible='<%# !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Id").ToString()) %>'>
                                                            <img src="../../../Images/Toolbar/delete16.png" border="0" title="Delete" />
                                                        </asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lb" runat="server" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>' 
                                                        ForeColor="Red" 
                                                        CommandName="PrintPHR"
                                                        >
                                                            <img src="../../../Images/Toolbar/print16.png" border="0" title="Print" />
                                                        </asp:LinkButton>
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
                AllowSorting="true" Width="100%">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView DataKeyNames="TerminologyID,ID" ClientDataKeyNames="TerminologyID,ID" >
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Date" UniqueName="CreateDateTime" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="100px">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <span><%# ((DateTime)DataBinder.Eval(Container.DataItem, "CreateDateTime")).ToString("dd/MM/yyyy")%></span>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Diagnose and Etiology" UniqueName="TerminologyNameDisplay">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label id="lblNDName" runat="server"><%# DataBinder.Eval(Container.DataItem, "TerminologyNameDisplay").ToString()%></asp:Label>
                                            <asp:LinkButton ID="lbd" runat="server" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID").ToString() %>' 
                                            ForeColor="Red" 
                                            CommandName="DeleteD"
                                            OnClientClick="return confirm('Are you sure you want to delete?');"
                                            Visible='<%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ME").ToString()) %>'>
                                                <img src="../../../Images/Toolbar/delete16.png" border="0" title="Delete" />
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label id="Label4" runat="server"><%# DataBinder.Eval(Container.DataItem, "Nic").ToString()%></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="" UniqueName="Nic" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="110px">
                            <HeaderStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <a href="javascript:void(0);"
                                onclick='<%# string.Format("javascript:NICNU({0},\"{1}\");",DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "TerminologyNameDisplay").ToString().Replace("\r","").Replace("'","&#39;")) %>' >
                                    Intervention
                                </a>
                                <a href="javascript:void(0);"
                                onclick='<%# string.Format("javascript:EvaluationNU({0},\"{1}\");",DataBinder.Eval(Container.DataItem, "ID").ToString(),DataBinder.Eval(Container.DataItem, "TerminologyNameDisplay").ToString().Replace("\r","").Replace("'","&#39;")) %>' >
                                    Evaluation
                                </a>
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