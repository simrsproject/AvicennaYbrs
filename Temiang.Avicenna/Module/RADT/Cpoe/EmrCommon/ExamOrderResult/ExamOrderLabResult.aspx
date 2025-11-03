<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="ExamOrderLabResult.aspx.cs" Inherits="Temiang.Avicenna.Module.Emr.ExamOrderLabResult" %>

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
            if (pos.y<0)
                oWnd.moveTo(pos.x, 0);
        }
        function openExamOrderLabResultChart(id, nm) {
            var url = 'ExamOrderLabResultChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&frid=' + id + '&frnm=' + nm;
            var oWnd = $find("<%= winDialog.ClientID %>");
            oWnd.setUrl(url);
            oWnd.show();
            oWnd.center();

            // Cek position
            var pos = oWnd.getWindowBounds();
            if (pos.y<0)
                oWnd.moveTo(pos.x, 0);
        }

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
    <table width="980px">
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
    </table>
    <telerik:RadGrid ID="grdLabHist" runat="server" AutoGenerateColumns="False" GridLines="None"
        Height="486px" OnNeedDataSource="grdLabHist_NeedDataSource">
        <MasterTableView DataKeyNames="OrderLabNo" GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="OrderLabTglOrder" HeaderText="Order Date" />
                        <telerik:GridGroupByField FieldName="OrderLabNo" HeaderText="Order No" />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="OrderLabTglOrder" SortOrder="Descending" />
                        <telerik:GridGroupByField FieldName="OrderLabNo" SortOrder="Descending" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="TEST_GROUP" HeaderText="GROUP" />
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="TEST_GROUP" SortOrder="None" />
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn DataField="LabOrderSummary" UniqueName="LabOrderSummary"
                    HeaderText="Exam Name">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "LabOrderSummary")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Result" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "Result")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="StandarValue" UniqueName="StandarValue" HeaderText="Standard Value"
                    HeaderStyle-Width="150px" />
                <telerik:GridBoundColumn DataField="LabOrderCode" UniqueName="LabOrderCode" HeaderText="Code" Display="False" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="100px"
                    ItemStyle-VerticalAlign="Top" HeaderText="Chart">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"javascript:openExamOrderLabResultChart('{0}','{1}'); return false;\"><img src=\"../../../../../Images/Toolbar/barchart.bmp\"  alt=\"View\" /></a>", Eval("LabOrderCode"), Eval("LabOrderSummary"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
