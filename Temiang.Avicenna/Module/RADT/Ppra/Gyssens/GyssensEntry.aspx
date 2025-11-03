<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="GyssensEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Ppra.GyssensEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="cdBlock">
        <script type="text/javascript" language="javascript">
            var _height = 0;
            var _width = 0;
            function onPickerClose(sender, eventArgs) {

                // Restore window size
                var curWnd = GetRadWindow();
                if (_width > 0) {
                    curWnd.setSize(_width, _height);
                    curWnd.center();
                }

                // Get retval
                var arg = eventArgs.get_argument();
                if (arg) {
                    var tgt = $find(arg.eventTarget);
                    var prevVal = tgt.get_value();
                    if (prevVal != "")
                        tgt.set_value(prevVal + '\r' + unescape(arg.retval));
                    else
                        tgt.set_value(unescape(arg.retval));
                }
            }

            function onItemFlowClick(val) {
                var masterTable = $find("<%= grdList.ClientID %>").get_masterTableView();
                masterTable.fireCommand("setvalue", val);
            }

            function tbarInfo_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
<%--                openWindow("Common/GyssensCommonInfo.aspx?regno=<%= RegistrationNo %>&itemid=<%= ItemID %>&infotype=" + val, 500, 500)--%>
            }

            function openRasproFormView(patid, regno, rseqno) {
                openWindowMaxScreen("<%= Helper.UrlRoot() %>/Module/RADT/Ppra/RasproFormView.aspx?patid=" + patid + "&regno=" + regno + "&rseqno=" + rseqno)
            }
            function openWindowMaxScreen(url) {
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
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Maximize, Close,Move" Modal="True" ShowOnTopWhenMaximized="true">
    </telerik:RadWindow>

    <telerik:RadAjaxManagerProxy ID="ajxProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <cc:CollapsePanel ID="clpPnl" runat="server" Title="Patient Information">
        <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" />
    </cc:CollapsePanel>
    <fieldset>
        <asp:HiddenField runat="server" ID="hdnGyssensSeqNo" />
        <table width="100%">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <table width="100%">
                        <tr>
                            <td class="label">Drug Name</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtItemName" Width="100%" ReadOnly="true" Font-Bold="true" Font-Size="Large">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Zat Active</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtZaztActiveName" Width="100%" ReadOnly="true" Font-Bold="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">Consume Method</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtConsumeMethod" Width="100%" ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <fieldset>
                                    <legend>Match RASPRO Form</legend>
                                    <telerik:RadGrid ID="grdGyssensRasproForm" runat="server"
                                        AutoGenerateColumns="False" OnNeedDataSource="grdGyssensRasproForm_NeedDataSource">
                                        <MasterTableView DataKeyNames="SeqNo" ShowHeader="True">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                    <ItemTemplate>
                                                        <%# string.Format("<a href=\"#\" onclick=\"javascript:openRasproFormView('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Raspro Form\" title=\"Raspro Form\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "SeqNo"))%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="RasproName" HeaderText="Raspro Form" UniqueName="RasproName" HeaderStyle-Width="250px"
                                                    SortExpression="RasproName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
                                                    SortExpression="RasprajaReason" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="False">
                                            <Selecting AllowRowSelect="False" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </fieldset>

                                <fieldset>
                                    <legend>Other RASPRO Form</legend>
                                    <telerik:RadGrid ID="grdGyssensRasproFormOther" runat="server"
                                        AutoGenerateColumns="False" OnNeedDataSource="grdGyssensRasproFormOther_NeedDataSource">
                                        <MasterTableView DataKeyNames="SeqNo" ShowHeader="True">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                                    <ItemTemplate>
                                                        <%# string.Format("<a href=\"#\" onclick=\"javascript:openRasproFormView('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"Raspro Form\" title=\"Raspro Form\" /></a>",
                                            DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "SeqNo"))%>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="RasproName" HeaderText="Raspro Form" UniqueName="RasproName" HeaderStyle-Width="250px"
                                                    SortExpression="RasproName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridDateTimeColumn DataField="RasproDateTime" HeaderText="Date" UniqueName="RasproDateTime" HeaderStyle-Width="150px"
                                                    SortExpression="RasproDateTime" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Advice By" UniqueName="ParamedicName" HeaderStyle-Width="150px"
                                                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="FocusInfection" HeaderText="Focus Infection" UniqueName="FocusInfection" HeaderStyle-Width="300px"
                                                    SortExpression="FocusInfection" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="Action" HeaderText="Action" UniqueName="Action" HeaderStyle-Width="250px"
                                                    SortExpression="Action" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn DataField="RasprajaReason" HeaderText="Raspraja Reason" UniqueName="RasprajaReason" HeaderStyle-Width="250px"
                                                    SortExpression="RasprajaReason" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="False">
                                            <Selecting AllowRowSelect="False" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </fieldset>
                            </td>
                        </tr>
                    </table>

                </td>
                <td style="width: 50%; vertical-align: top;">
                    <cc:CollapsePanel ID="clpSoap" runat="server" Title="First SOAP">
                        <asp:Literal runat="server" ID="litSoapInfo"></asp:Literal>
                    </cc:CollapsePanel>
                </td>
            </tr>
        </table>
    </fieldset>
    <telerik:RadToolBar ID="tbarInfo" runat="server" Width="100%" EnableEmbeddedScripts="false" OnClientButtonClicking="tbarInfo_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Laboratory Result" Value="lab"
                ImageUrl="~/Images/Toolbar/ordering16.png" />
        </Items>
    </telerik:RadToolBar>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnItemCommand="grdList_ItemCommand" OnItemDataBound="grdList_ItemDataBound"
        GridLines="None" AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="RasproLineID">
            <Columns>
                <telerik:GridCheckBoxColumn DataField="IsEntryVisible" HeaderText="" UniqueName="IsEntryVisible" Visible="false">
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="SeqNo" HeaderText="No" UniqueName="SeqNo">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Spesification" HeaderText="Observation" UniqueName="Spesification">
                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>

                <telerik:GridTemplateColumn UniqueName="Flow" HeaderText="">
                    <HeaderStyle Width="140px" />
                    <HeaderTemplate>
                        <table width="130px">
                            <tr>
                                <td style="width: 60px; align-content: center">Flow
                                </td>
                                <td style="align-content: center">Category
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="130px">
                            <tr>
                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes")? string.Empty:
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "YesAction"))? "style=\"width: 60px;background-color: #4CAF50;color:white;\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "YesAction").ToString())? "style=\"width: 60px;background-color: red;color:white;\"" :
                                            "style=\"width: 60px;background-color: #4CAF50;color:white;\""%>>
                                    <input type="radio" id="yes"
                                        name="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>"
                                        value="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_yes"
                                        <%# false.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible"))? "disabled":string.Empty %>
                                        <%#DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes")?"checked":string.Empty%>
                                        <%# true.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible")) 
                                                && !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes") ? "onclick=\"onItemFlowClick('"+DataBinder.Eval(Container.DataItem, "RasproLineID")+"_yes')\"" : string.Empty %> />
                                    <label for="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_yes">Ya</label></td>

                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("yes")? "style=\"text-align:center\"":
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "YesAction"))? "style=\"background-color: #4CAF50;color:white;font-weight:bold;text-align:center\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "YesAction").ToString())? "style=\"background-color: red;color:white;font-weight:bold;text-align:center\"" :
                                            "style=\"background-color: #4CAF50;color:white; text-align: center;\""%>>
                                    <%#DataBinder.Eval(Container.DataItem, "YesAction")%>
                                </td>
                            </tr>
                            <tr>
                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")? string.Empty:
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "NoAction"))? "style=\"width: 60px;background-color: #4CAF50;color:white;\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "NoAction").ToString())? "style=\"width: 60px;background-color: red;color:white;\"" :
                                            "style=\"width: 60px;background-color: #4CAF50;color:white;\""%>>
                                    <input type="radio" id="no"
                                        name="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>"
                                        value="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_no"
                                        <%# false.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible"))? "disabled":string.Empty %>
                                        <%#DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")?"checked":string.Empty%>
                                        <%# true.Equals((bool)DataBinder.Eval(Container.DataItem, "IsEntryVisible")) && !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")? "onclick=\"onItemFlowClick('"+DataBinder.Eval(Container.DataItem, "RasproLineID")+"_no')\"" : string.Empty%> />
                                    <label for="<%#DataBinder.Eval(Container.DataItem, "RasproLineID")%>_no">Tidak</label></td>

                                <td <%# !DataBinder.Eval(Container.DataItem, "SelectedValue").Equals("no")? "style=\"text-align:center\"":
                                        "0".Equals(DataBinder.Eval(Container.DataItem, "NoAction"))? "style=\"background-color: #4CAF50;color:white;font-weight:bold;text-align:center\"" :
                                            !string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "NoAction").ToString())? "style=\"background-color: red;color:white;font-weight:bold;text-align:center\"" :
                                            "style=\"background-color: #4CAF50;color:white; text-align: center;\""%>>
                                    <%#DataBinder.Eval(Container.DataItem, "NoAction")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="Action" HeaderText="Parameter Information">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "ParameterInfo")%>
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
