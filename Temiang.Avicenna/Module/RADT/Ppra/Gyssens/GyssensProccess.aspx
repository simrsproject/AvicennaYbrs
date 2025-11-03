<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="GyssensProccess.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Ppra.GyssensProccess" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Telerik.Web.UI.Skins" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.Emr" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock" runat="server">
        <style>
            .AutoHeightGridClass .rgDataDiv {
                height: auto !important;
            }
        </style>

        <script type="text/javascript">

            function openGyssensEntry(patid, regno, seqno) {
                openWindowMaxScreen("GyssensEntry.aspx?md=edit&patid=" + patid + "&regno=" + regno + "&seqno=" + seqno)
            }
            function openRasproFormView(patid, regno, rseqno) {
                openWindowMaxScreen("../RasproFormView.aspx?patid=" + patid + "&regno=" + regno + "&rseqno=" + rseqno)
            }
            function openWinEntryMaxWindow(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                openWindow(url, width - 40, height - 40);
            }
            function openWindowMaxScreen(url) {
                if (url.includes("&rt=") || url.includes("?rt="))
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.maximize();
            }

            function openWindow(url, width, height) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.center();
                oWnd.show();
            }

            function openMedicationHist(patid, regno, fregno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid=' + patid + '&regno=' + regno + '&fregno=' + fregno;
                openWindowMaxScreen(url);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Maximize, Close,Move" Modal="True" ShowOnTopWhenMaximized="true">
    </telerik:RadWindow>

            <telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdGyssens">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdGyssens" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            </AjaxSettings>
            </telerik:RadAjaxManagerProxy>

    <cc:CollapsePanel ID="clpPnl" runat="server" Title="Patient Information">
        <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" />
    </cc:CollapsePanel>
    <telerik:RadTabStrip ID="tabsPpra" runat="server" MultiPageID="mpPpra" ShowBaseLine="true"
        Align="Left" PerTabScrolling="True" Width="100%"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Gyssens Observation" PageViewID="pgGyssens"
                Selected="True" />
<%--            <telerik:RadTab runat="server" Text="Raspro Form History" PageViewID="pgRasproForm" Visible="false" />--%>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpPpra" runat="server" SelectedIndex="0" ScrollBars="Auto" Width="100%"
        CssClass="multiPage">
        <telerik:RadPageView ID="pgGyssens" runat="server">
            <telerik:RadGrid ID="grdGyssens" runat="server"
                AutoGenerateColumns="False" GridLines="None" OnNeedDataSource="grdGyssens_NeedDataSource" 
                OnItemCommand="grdGyssens_ItemCommand" OnItemDataBound="grdGyssens_OnItemDataBound">
                <MasterTableView DataKeyNames="SeqNo" ShowHeader="True" CommandItemDisplay="Top">
                    <CommandItemTemplate>
                        <div>
                            <div style="float:left;">
                                <asp:LinkButton ID="lnkReproccess" runat="server" CommandName="reproccess" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Reprocess</asp:LinkButton>&nbsp;
                            </div>

                            <divstyle="float:right;">
                                <asp:LinkButton ID="lnkRefresh" runat="server" CommandName="refresh" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Refresh</asp:LinkButton>&nbsp;
                            </div>

                        </div>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />

                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Gyssens" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"javascript:openGyssensEntry('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" alt=\"Confirmed\" title=\"Gyssens Observation Form\" /></a>",
                                            Eval("PatientID"),Eval("RegistrationNo"),Eval("SeqNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ItemID" HeaderText="Item ID" UniqueName="ItemID" HeaderStyle-Width="100px"
                            SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName" HeaderStyle-Width="250px"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ZatActiveName" HeaderText="Zat Active Name" UniqueName="ZatActiveName" HeaderStyle-Width="250px"
                            SortExpression="ZatActiveName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderText="Consume Method" UniqueName="ConsumeMethodName" HeaderStyle-Width="250px"
                            SortExpression="ConsumeMethodName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#string.Format("{0} @{1} {2}", Eval("SRConsumeMethodName"), Eval("ConsumeQty"), Eval("SRConsumeUnit"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="PrescriptionDateStart" HeaderText="From Date" UniqueName="PrescriptionDateStart" HeaderStyle-Width="120px"
                            SortExpression="PrescriptionDateStart" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn DataField="PrescriptionDateEnd" HeaderText="To Date" UniqueName="PrescriptionDateEnd" HeaderStyle-Width="120px"
                            SortExpression="PrescriptionDateEnd" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="GyssensCreateByUserID" HeaderText="Create By" UniqueName="GyssensCreateByUserID" HeaderStyle-Width="100px"
                            SortExpression="GyssensCreateByUserID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="GyssensCreateDateTime" HeaderText="Create Date" UniqueName="GyssensCreateDateTime"
                            SortExpression="GyssensCreateDateTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                    </Columns>
                    <NestedViewTemplate>
                        <fieldset style="padding-left: 20px; padding-bottom: 10px;">
                            <legend>RASPRO FORM</legend>

                            <telerik:RadGrid ID="grdGyssensRasproForm" runat="server"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="SeqNo" ShowHeader="True">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                            <ItemTemplate>
                                                <%# string.Format("<a href=\"#\" onclick=\"javascript:openRasproFormView('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Raspro Form\" title=\"Raspro Form\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "SeqNo"))%>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Raspro Form Type" UniqueName="ItemName" HeaderStyle-Width="250px"
                                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridDateTimeColumn DataField="RasproDateTime" HeaderText="Date" UniqueName="RasproDateTime" HeaderStyle-Width="150px"
                                            SortExpression="RasproDateTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Advice By" UniqueName="ParamedicName" HeaderStyle-Width="150px"
                                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridDateTimeColumn DataField="StartDateTime" HeaderText="Start" UniqueName="StartDateTime" HeaderStyle-Width="120px"
                                            SortExpression="StartDateTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridDateTimeColumn DataField="StopDateTime" HeaderText="Stop" UniqueName="StopDateTime" HeaderStyle-Width="120px"
                                            SortExpression="StopDateTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="FocusInfection" HeaderText="Focus Infection" UniqueName="FocusInfection" HeaderStyle-Width="300px"
                                            SortExpression="FocusInfection" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="Action" HeaderText="Action" UniqueName="Action" HeaderStyle-Width="250px"
                                            SortExpression="Action" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="RasprajaReason" HeaderText="Raspraja Reason" UniqueName="RasprajaReason" HeaderStyle-Width="250px"
                                            SortExpression="Action" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="False">
                                    <Selecting AllowRowSelect="False" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </fieldset>
                    </NestedViewTemplate>
                </MasterTableView>

                <ClientSettings EnableRowHoverStyle="False">
                    <Selecting AllowRowSelect="False" />
                </ClientSettings>
            </telerik:RadGrid>

        </telerik:RadPageView>
<%--        <telerik:RadPageView ID="pgRasproForm" runat="server">
            <telerik:RadGrid ID="grdRasproForm" runat="server" OnNeedDataSource="grdRasproForm_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None">
                <MasterTableView DataKeyNames="SeqNo" ShowHeader="True">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Left" Width="70px" />
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"javascript:openRasproFormView('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Raspro Form\" title=\"Raspro Form\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "SeqNo"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ItemName" HeaderText="Raspro Form Type" UniqueName="ItemName" HeaderStyle-Width="250px"
                            SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridDateTimeColumn DataField="RasproDateTime" HeaderText="Date" UniqueName="RasproDateTime" HeaderStyle-Width="150px"
                            SortExpression="RasproDateTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridBoundColumn DataField="FocusInfection" HeaderText="Focus Infection" UniqueName="FocusInfection" HeaderStyle-Width="300px"
                            SortExpression="FocusInfection" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Action" HeaderText="Action" UniqueName="Action" HeaderStyle-Width="250px"
                            SortExpression="Action" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Advice By" UniqueName="ParamedicName" HeaderStyle-Width="150px"
                            SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="False">
                    <Selecting AllowRowSelect="False" />
                </ClientSettings>
            </telerik:RadGrid>
            <br />

        </telerik:RadPageView>--%>
    </telerik:RadMultiPage>


</asp:Content>
