<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ExamOrderLabHist.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.ExamOrderLabHist" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<%@ Import Namespace="Temiang.Avicenna.Module.RADT.Emr.MainContent" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript">
            function openWindow(url, width, height) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }
<%--        function openExamOrderLabResultChart(id, nm) {
            var url = 'ExamOrderLabResultChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&frid=' + id + '&frnm=' + nm;
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.setUrl(url);
            oWnd.show();
            oWnd.center();

            // Cek position
            var pos = oWnd.getWindowBounds();
            if (pos.y<0)
                oWnd.moveTo(pos.x, 0);
        }--%>

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="550px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Close" VisibleStatusbar="False" VisibleTitlebar="False" Modal="True">
    </telerik:RadWindow>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnQueryLabResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLabHist" LoadingPanelID="fw_ajxLoadingPanel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdLabHist">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdLabHist" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
<%--    <table width="980px">
        <tr>
            <td class="label">Name
            </td>
            <td style="width: 200px">
                <telerik:RadTextBox ID="txtExamName" runat="server" Width="100%" />
            </td>
            <td width="20px">
                <asp:ImageButton ID="btnQueryLabResult" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                    OnClick="btnQueryLabResult_Click" ToolTip="Search" />
            </td>
            <td></td>
        </tr>
    </table>--%>
    <telerik:RadGrid ID="grdLaboratory" runat="server" OnNeedDataSource="grdLaboratory_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" Height="560px"
        OnItemDataBound="grdLaboratory_OnItemDataBound">
        <MasterTableView DataKeyNames="TransactionNo,ResultValue" CommandItemDisplay="None" >
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TransactionDate" HeaderText="Date" HeaderStyle-Width="70px">
                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                    <ItemTemplate>
                        <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate").ToStringDefaultEmpty()).ToString(AppConstant.DisplayFormat.DateCultureInfo.DateTimeFormat.ShortDatePattern) %><br />
                        <br />
                        <%#DataBinder.Eval(Container.DataItem, "IsHdApproved").ToBoolean()?"<img src=\""+ Helper.UrlRoot() +"/Images/ApprovedStampRed.png\" class=\"apprImg\" />":String.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        Reg No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                        <br />
                        Tx No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>
                        <br />
                        From:&nbsp;<b><%#DataBinder.Eval(Container.DataItem, "FromServiceUnitName")%></b>
                        <br />
                        By:&nbsp;<i> <%#DataBinder.Eval(Container.DataItem, "PhysicianSenders")%></i>
                        <br />
                        Order Item:
                            <br />
                        <div style="padding-left: 10px;">
                            <%#DataBinder.Eval(Container.DataItem, "JobOrderSummary")%>
                        </div>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="LaboratoryResult" HeaderText="Result">
                    <ItemTemplate>
                        <%#ExamOrderHistCtl.LaboratoryResultNote(DataBinder.Eval(Container.DataItem, "TransactionNo").ToString())%>
                        <telerik:RadGrid ID="grdLaboratoryResult" runat="server" AutoGenerateColumns="False" GridLines="None">
                            <MasterTableView DataKeyNames="OrderLabNo,LabOrderCode,IsCritical" GroupLoadMode="Client">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldName="TestGroup" HeaderText="Group" />
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="TestGroup" SortOrder="None" />
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px"
                                        ItemStyle-VerticalAlign="Top" HeaderText="Chart">
                                        <ItemTemplate>
                                            <%# false.Equals( Eval("IsFraction").ToBoolean()) ? string.Empty : string.Format("<a href=\"#\" onclick=\"javascript:openExamOrderLabResultChart('{0}','{1}'); return false;\"><img src=\"{2}/Images/Toolbar/barchart.bmp\"  alt=\"View\" /></a>", Eval("LabOrderCode"), Eval("LabOrderSummary"),Helper.UrlRoot())%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="LabOrderSummary" UniqueName="LabOrderSummary"
                                        HeaderText="Exam Name" HeaderStyle-Width="250px">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn DataField="ResultDatetime" UniqueName="ResultDatetime" HeaderText="Result Date" HeaderStyle-Width="120px" />
                                    <telerik:GridBoundColumn DataField="Flag" UniqueName="Flag" HeaderText="Flag" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Result")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                                        HeaderStyle-Width="150px" />
                                    <telerik:GridBoundColumn DataField="ResultComment" UniqueName="ResultComment" HeaderText="Result Comment" />

                                    <telerik:GridBoundColumn DataField="LabOrderCode" UniqueName="LabOrderCode" HeaderText="Code" Display="False" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                                <Scrolling UseStaticHeaders="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
