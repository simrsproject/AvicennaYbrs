<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DrugObservationHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.DrugObservationHist" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <style>
            #counselingLine {
                font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
                border-collapse: collapse;
                width: 100%;
            }

                #counselingLine td, #counselingLine th {
                    border: 1px solid #a9a9a9;
                    padding: 4px;
                }

                #counselingLine tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

                #counselingLine tr:hover {
                    background-color: #ddd;
                }

                #counselingLine th {
                    padding-top: 6px;
                    padding-bottom: 6px;
                    text-align: center;
                    background-color: #4CAF50;
                    color: white;
                }
        </style>
        <script type="text/javascript" language="javascript">

            function tbarMain_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case 'refresh':
                        var grd = $find('<%=grdList.ClientID %>').get_masterTableView();
                        grd.rebind();
                        break;
                    case 'add':
                        entryDetail("new", 0);
                        break
                    case 'print':
<%--                        var obj = {};
                        obj.registrationNo = "<%=RegistrationNo%>";
                        obj.monitoringNo = monno;

                        openPrintPreview("PopulatePrintParameterNosocomial", obj);--%>
                        break;
                    case 'close':
                        Close();
                        break;
                }

            }

            function entryDetail(mod, seqNo) {
                var grdid = "";
                grdid = "<%=grdList.ClientID %>";
                var url = "DrugObservationEntry.aspx?mod=" +
                    mod +
                    '&patid=<%= PatientID %>&regno=<%=RegistrationNo %>&sn=' +
                    seqNo +
                    '&ccm=rebind&cet=' +
                    grdid;

                openWindowMaxScreen(url);
            }

            function openWindowMaxScreen(url) {
                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }

            function radWindowManager_ClientClose(oWnd, args) {
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
            function openPrintPreview(method, parameter) {
                $.ajax({
                    url: "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EhrWebService.asmx/" + method,
                    data: JSON.stringify(parameter), //ur data to be sent to server
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    success: function (data) {
                        var url = '<%= Helper.UrlRoot() %>/Module/Reports/ReportViewer.aspx';
                        var oWnd = radopen(url, 'winDialog');
                        oWnd.maximize();
                    },
                    error: function (x, y, z) {
                        alert(x.responseText + "  " + x.status);
                    }
                });

            }
            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                // set height to the whole RadGrid control
                var grid = $find("<%= grdList.ClientID %>");
                grid.get_element().style.height = height - 266 + "px";
                grid.repaint();


            }
            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tbarMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadToolBar ID="tbarMain" runat="server" Width="100%" EnableEmbeddedScripts="false" OnClientButtonClicking="tbarMain_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Add" Value="add" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print" Value="print" ImageUrl="~/Images/Toolbar/print16.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
            <telerik:RadToolBarButton runat="server" Text="Close" Value="close"
                ImageUrl="~/Images/Toolbar/close16.png" />
        </Items>
    </telerik:RadToolBar>
    <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" />
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDeleteCommand="grdList_DeleteCommand" OnItemDataBound="grdList_ItemDataBound"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" BorderStyle="None"
        AllowSorting="true">
        <MasterTableView DataKeyNames="DrugObsNo,IsDeleted">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"javascript:entryDetail('view', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/views16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "DrugObsNo"),Helper.UrlRoot())%>
                        <br />
                        <br />
                        <%# (!AppSession.UserLogin.UserID.Equals(DataBinder.Eval(Container.DataItem, "CreatedByUserID")) || (DataBinder.Eval(Container.DataItem, "IsDeleted") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsDeleted").Equals(true))  ? string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" />",Helper.UrlRoot()) : "")%>
                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                            Visible='<%#!(!AppSession.UserLogin.UserID.Equals(DataBinder.Eval(Container.DataItem, "CreatedByUserID")) || (DataBinder.Eval(Container.DataItem, "IsDeleted") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsDeleted").Equals(true)) ) %>'
                            OnClientClick="javascript: if (!confirm('Delete this record, are you sure ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="<%#Helper.UrlRoot()%>/Images/Toolbar/row_delete16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="DrugObsNo" HeaderText="No" UniqueName="DrugObsNo" SortExpression="DrugObsNo" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="DrugObsDateTime" HeaderText="Time">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DrugObsDateTime")).ToString(AppConstant.DisplayFormat.DateShortMonth) %><br />
                        <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DrugObsDateTime")).ToString(AppConstant.DisplayFormat.HourMin) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="Drps" HeaderText="Drug Related Problems Criteria" HeaderStyle-Width="400px">
                    <ItemTemplate>
                        <%#DrpsHtml(Container) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="DrugItem" HeaderText="Drug Item" HeaderStyle-Width="450px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#DrugItemHtml(Container) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="DrugInteractionRisk" HeaderText="" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <fieldset style="width:400px;">
                            <legend>Drug Interaction Risk</legend>
                            <%#Eval("DrugInteractionRisk") %>
                        </fieldset>
                        <br />
                        <fieldset style="width:400px;">
                            <legend>Recommendation</legend>
                            <%#Eval("Recommendation") %>
                        </fieldset>
                        <br />
                        <fieldset style="width:400px;">
                            <legend>Create By</legend>
                            <%# DataBinder.Eval(Container.DataItem, "CreatedByUserName") %><br />
                            Lic:&nbsp;<%# DataBinder.Eval(Container.DataItem, "LicenseNo") %>
                        </fieldset>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
