<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExamOrderHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MainContent.ExamOrderHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<script type="text/javascript">
    function viewResult(url, status, type, unit) {
        if (type == 'eps') {
            if (status == '1') {
                var links = url.split(';');
                for (i = 0; i < links.length; i++) {
                    window.open(links[i]);
                }
            }
            else alert('Examination results is not available yet. Contact ' + unit);
        }
        else {
            if (status == '1') {
                var links = url.split(';');
                for (i = 0; i < links.length; i++) {
                    window.open(links[i]);
                }
            }
            else alert('Examination results is not available yet. Contact ' + unit);
        }
    }

    function showLabPdfFile(trno) {
        url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=lab&trno=" + trno;
        openWinEntryMaxWindow(url);
    }

    function showLabPaPdfFile(trno) {
        url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=labpa&trno=" + trno;
        openWinEntryMaxWindow(url);
    }


    function showRadPdfFile(accno, trno, seqno) {
        url = "<%= Helper.UrlRoot() %>/Module/Reports/PdfUrlViewer.aspx?mode=rad&accno=" + accno + "&trno=" + trno + "&seqno=" + seqno;
        openWinEntryMaxWindow(url);
    }

    function printPreviewRadiologyResult(transactionNo, sequenceNo) {
        var obj = {};
        obj.transactionNo = transactionNo;
        obj.sequenceNo = sequenceNo;

        // Method ada di EmrWebService.asmx.cs
        openPrintPreview("PopulatePrintRadiologyResult", obj);
    }
    function printPreviewExamOrderOtherResult(transactionNo, sequenceNo) {
        var obj = {};
        obj.transactionNo = transactionNo;
        obj.sequenceNo = sequenceNo;

        // Method ada di EmrWebService.asmx.cs
        openPrintPreview("PopulatePrintExamOrderOtherResult", obj);
    }
    function printPreviewPaResult(resultNo, reportId) {
        var obj = {};
        obj.resultNo = resultNo;
        obj.reportId = reportId;

        // Method ada di EmrWebService.asmx.cs
        openPrintPreview("PopulatePrintPaResult", obj);
    }

    function openUrlInNewWindow(url) {
        var oWnd = radopen(url, 'winDialog');
        oWnd.maximize();
        setTimeout(function () { oWnd.close(); }, 2000);
    }

    function MarkCriticalAsReadAll(step, trno) {
        alert(trno);
    }
    function MarkCriticalAsRead(step, trno, item) {
        alert(trno);
    }
    function openSpecimenCRDetail(id, regno) {
        var oWnd = radopen('../../Charges/ServiceUnit/ServiceUnitTransaction/SpecimenCollectItem.aspx?type=<%= Request.QueryString["type"]%>&id=' + id + '&reg=' + regno + '&sc=0', 'SpecimenCRDetail');
        oWnd.setSize(1000, 600);
        oWnd.show();
    }
</script>
<style>
    .apprImg {
        object-fit: contain;
        width: 45px;
        height: 30px;
    }
</style>
<telerik:RadAjaxManagerProxy ID="ajaxManagerProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdLaboratory">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdLaboratory" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdRadiology">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdRadiology" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdPathology">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdPathology" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdPathology2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdPathology2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grdExamOrderOther">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdExamOrderOther" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="Timer1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdLaboratory" />
                <telerik:AjaxUpdatedControl ControlID="grdRadiology" />
                <telerik:AjaxUpdatedControl ControlID="grdPathology" />
                <telerik:AjaxUpdatedControl ControlID="grdPathology2" />
                <telerik:AjaxUpdatedControl ControlID="grdExamOrderOther" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="60000" Enabled="false" />
<telerik:RadTabStrip ID="mainRadTabStrip" runat="server" MultiPageID="mainRadMultiPage" ShowBaseLine="true" Skin="BlackMetroTouch"
    Align="Left" PerTabScrolling="True"
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Value="Laboratory" Text="Laboratory" PageViewID="pgLaboratory" Selected="True" />
        <telerik:RadTab runat="server" Value="Radiology" Text="Radiology and Imaging" PageViewID="pgRadiology" />
        <telerik:RadTab runat="server" Value="Pathology" Text="Pathology Anatomy" PageViewID="pgPathology" />
        <telerik:RadTab runat="server" Value="Pathology2" Text="Pathology Anatomy*" PageViewID="pgPathology2" />
        <telerik:RadTab runat="server" Value="Other" Text="Other" PageViewID="pgOther" />
    </Tabs>
</telerik:RadTabStrip>
<div style="font-weight: bold; color: red;"></div>
<telerik:RadMultiPage ID="mainRadMultiPage" runat="server" SelectedIndex="0" ScrollBars="Auto"
    CssClass="multiPage">

    <telerik:RadPageView ID="pgLaboratory" runat="server" Width="100%">
        <telerik:RadGrid ID="grdLaboratory" runat="server" OnNeedDataSource="grdLaboratory_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" Height="560px"
            OnItemCommand="grdJobOrder_ItemCommand" OnItemDataBound="grdLaboratory_OnItemDataBound">
            <MasterTableView DataKeyNames="TransactionNo,ResultValue" CommandItemDisplay="Top">
                <CommandItemTemplate>
                    <div>
                        <div class="l">
                            <%# (!IsUserAccessExamOrder(RegistrationNo, IsPostBack) ? string.Format("<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"{0}/Images/Toolbar/new16_d.png\" />&nbsp;Add Laboratory Test Order</a>",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryLaboratory('new', ''); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Laboratory Test Order</a>",Helper.UrlRoot()))%>
                        </div>

                        <div class="r">
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="RebindLab" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Refresh</asp:LinkButton>&nbsp;
                        </div>
                        <div class="r">
                            <asp:LinkButton ID="lbLabHist" runat="server" ImageUrl="~/Images/Toolbar/refresh16.png" OnClientClick="openExamOrderlabHist();return false;"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Show History</asp:LinkButton>&nbsp;
                        </div>
                    </div>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>

                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <%# (Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate")))
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) 
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdApproved"))
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdVoid"))) ? 
                                                    string.Format("<a href=\"#\" onclick=\"javascript:entryLaboratory('view', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/views16.png\"  alt=\"view\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())
                                                    : string.Format("<a href=\"#\" onclick=\"javascript:entryLaboratory('edit', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())%>
                            <div style="height: 10px">&nbsp;</div>
                            <%--                            <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                Visible='<%#!(Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate"))) 
                                                              || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) 
                                                              || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdApproved"))
                                                              || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdVoid"))) %>'
                                OnClientClick="javascript: if (!confirm('Delete this record, are you sure ?')) return false;">
                                                    <img style="border: 0px; vertical-align: middle;" src="<%=Helper.UrlRoot()%>/Images/Toolbar/row_delete16.png" />
                                <div style="height:10px" >&nbsp;</div>
                            </asp:LinkButton>--%>
                            <%# string.Format("<a href=\"#\" onclick=\"openSpecimenCRDetail('{0}','{1}'); return false;\"><img src=\"{2}/Images/Toolbar/details16.png\" border=\"0\" title=\"Specimen and Collect Method\" /></a>",
                                                  DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "RegistrationNo"),Helper.UrlRoot())%>
                            <div style="height: 10px">&nbsp;</div>
                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" ToolTip='Print Job Order Notes'
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                                                    <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/print16.png" border="0" />
                            </asp:LinkButton>
                            <div style="height: 10px">&nbsp;</div>
                            <a href='#' onclick="showLabPdfFile('<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>');return false;">
                                <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/imp_exp_pdf16.png" border="0" /></a>
                            <div style="height: 10px">&nbsp;</div>
                            <asp:LinkButton ID="lbMarkAsRead" runat="server" CommandName="MarkAsRead" ToolTip='Mark As Read' CssClass="blink"
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>' Visible='<%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ResultReadByPhysicianID").ToString())%>'>
                                <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/mail16.png" border="0" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TransactionDate" HeaderText="Date" HeaderStyle-Width="70px">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%--                            <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate").ToStringDefaultEmpty()).ToString(AppConstant.DisplayFormat.DateCultureInfo.DateTimeFormat.ShortDatePattern) %><br />--%>
                            <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate").ToStringDefaultEmpty()).ToString(AppConstant.DisplayFormat.DateTime) %><br />
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
                            <%#LaboratoryResultNote(DataBinder.Eval(Container.DataItem, "TransactionNo").ToString())%>
                            <telerik:RadGrid ID="grdLaboratoryResult" runat="server" AutoGenerateColumns="False" GridLines="None" OnItemCommand="grdJobOrder_ItemCommand">
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
                                        <telerik:GridTemplateColumn UniqueName="templateCriticalNurseMarking" HeaderText="" HeaderStyle-Width="20px">
                                            <HeaderTemplate>
                                                <%# string.Format("<img src=\"{0}/Images/Toolbar/Interventions32.png\" style=\"width:16px;height:16px;\" alt=\"Read By Nurse\" />", Helper.UrlRoot()) %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbMarkAsReadByNurse" runat="server" CommandName="MarkAsReadByNurse"
                                                    ToolTip='Mark critical value as reported to physician by nurse'
                                                    CommandArgument='<%# string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "OrderLabNo"), DataBinder.Eval(Container.DataItem, "LabOrderCode"))%>'
                                                    Visible='<%# (bool)DataBinder.Eval(Container.DataItem, "IsCritical") %>'
                                                    Enabled='<%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ReportedByNurseID").ToString()) && AppSession.UserLogin.SRUserType.Equals(AppUser.UserType.Nurse)) %>'
                                                    CssClass='<%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ReportedByNurseID").ToString()) && AppSession.UserLogin.SRUserType.Equals(AppUser.UserType.Nurse)) ? "blink":"blenk" %>'>
                                                    <img src='<%# string.Format("{0}/Images/Toolbar/{1}",Helper.UrlRoot(), (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ReportedByNurseID").ToString()) ? "post16_d.png":"post_green_16.png")) %>' />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="templateCriticalPhyMarking" HeaderText="" HeaderStyle-Width="20px">
                                            <HeaderTemplate>
                                                <%# string.Format("<img src=\"{0}/Images/Toolbar/dokter16.png\" alt=\"Read By Physician\" />", Helper.UrlRoot()) %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbMarkAsReadByPhy" runat="server" CommandName="MarkAsReadByPhy"
                                                    ToolTip='Mark critical value as read by physician'
                                                    CommandArgument='<%# string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "OrderLabNo"), DataBinder.Eval(Container.DataItem, "LabOrderCode"))%>'
                                                    Visible='<%# (bool)DataBinder.Eval(Container.DataItem, "IsCritical") %>'
                                                    Enabled='<%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ReadByPhysicianID").ToString()) && (AppSession.UserLogin.SRUserType.Equals(AppUser.UserType.Doctor) || AppSession.UserLogin.SRUserType.Equals(AppUser.UserType.Nurse))) %>'
                                                    CssClass='<%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ReadByPhysicianID").ToString()) && (AppSession.UserLogin.SRUserType.Equals(AppUser.UserType.Doctor) || AppSession.UserLogin.SRUserType.Equals(AppUser.UserType.Nurse))) ? "blink":"blenk" %>'>
                                                    <img src='<%# string.Format("{0}/Images/Toolbar/{1}",Helper.UrlRoot(), (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ReadByPhysicianID").ToString()) ? "post16_d.png":"post_green_16.png")) %>' />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="templateCriticalLabMarking" HeaderText="" HeaderStyle-Width="20px">
                                            <HeaderTemplate>
                                                <%# string.Format("<img src=\"{0}/Images/Toolbar/ImmediateAction32.png\" style=\"width:16px;height:16px;\" alt=\"Mark as Complete\" />", Helper.UrlRoot()) %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbMarkAsComplete" runat="server" CommandName="MarkAsComplete"
                                                    ToolTip='Mark critical value as completely reported'
                                                    CommandArgument='<%# string.Format("{0}|{1}", DataBinder.Eval(Container.DataItem, "OrderLabNo"), DataBinder.Eval(Container.DataItem, "LabOrderCode"))%>'
                                                    Visible='<%# (bool)DataBinder.Eval(Container.DataItem, "IsCritical") %>'
                                                    Enabled='<%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "CompletelyReportedByUserID").ToString()) && IsLaboratoryOfficer) %>'
                                                    CssClass='<%# (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "CompletelyReportedByUserID").ToString()) && IsLaboratoryOfficer) ? "blink":"blenk" %>'>
                                                    <img src='<%# string.Format("{0}/Images/Toolbar/{1}",Helper.UrlRoot(), (string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "CompletelyReportedByUserID").ToString()) ? "post16_d.png":"post_green_16.png")) %>' />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
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

    </telerik:RadPageView>

    <telerik:RadPageView ID="pgRadiology" runat="server" Width="100%">
        <telerik:RadGrid ID="grdRadiology" runat="server" OnNeedDataSource="grdRadiology_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" Height="560px" Width="100%"
            OnItemCommand="grdJobOrder_ItemCommand" OnItemDataBound="grdRadiology_OnItemDataBound">
            <MasterTableView DataKeyNames="TransactionNo" CommandItemDisplay="Top">
                <CommandItemTemplate>
                    <div>
                        <div class="l">
                            <%# (!IsUserAccessExamOrder(RegistrationNo, IsPostBack) ? string.Format("<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"{0}/Images/Toolbar/new16_d.png\" />&nbsp;Add Radiology Order</a>",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryRadiology('new', ''); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Radiology Order</a>",Helper.UrlRoot()))%>
                        </div>
                        <div class="r" style="color: yellow;">
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="RebindRad" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Refresh</asp:LinkButton>&nbsp;
                        </div>
                    </div>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>

                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <%# (Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate"))) 
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) 
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdApproved"))
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdVoid"))) ? 
                                                    string.Format("<a href=\"#\" onclick=\"javascript:entryRadiology('view', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/views16.png\"  alt=\"view\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())
                                                    : string.Format("<a href=\"#\" onclick=\"javascript:entryRadiology('edit', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())%>
                            <div style="height: 10px">&nbsp;</div>                            
                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" ToolTip='Print Job Order Notes'
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                                                    <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/print16.png" border="0" />
                            </asp:LinkButton>
                            <a href='#' onclick="showRadPdfFile('<%#DataBinder.Eval(Container.DataItem, "ResultValue")%>', '<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>', '<%#DataBinder.Eval(Container.DataItem, "SequenceNo")%>');return false;">
                                <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/imp_exp_pdf16.png" border="0" /></a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TransactionDate" HeaderText="Date" HeaderStyle-Width="70px">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate").ToStringDefaultEmpty()).ToString(AppConstant.DisplayFormat.DateTime) %><br />
                            <br />
                            <%#DataBinder.Eval(Container.DataItem, "IsHdApproved").ToBoolean()?"<img src=\""+ Helper.UrlRoot() +"/Images/ApprovedStampRed.png\" class=\"apprImg\" />":String.Empty%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="250px">
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
                    <telerik:GridTemplateColumn UniqueName="INTIWID" HeaderText="RIS/PACS" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%# string.Format("<a href=\"#\" title=\"Expertise\" onclick=\"viewResult('{0}{1}','{2}','eps', '{3}'); return false;\"><img src=\"../../../Images/eps.png\" border=\"0\" /></a>",DataBinder.Eval(Container.DataItem, "EpsUrlLocation"), string.Empty, DataBinder.Eval(Container.DataItem, "IsResultAvailable"), DataBinder.Eval(Container.DataItem, "ToServiceUnitName")) %>
                            <%# string.Format("<a href=\"#\" title=\"DICOM\" onclick=\"viewResult('{0}{1}','{2}','dicom', '{3}'); return false;\"><img src=\"../../../Images/dicom.png\" border=\"0\" /></a>",DataBinder.Eval(Container.DataItem, "DcomUrlLocation"), string.Empty, DataBinder.Eval(Container.DataItem, "IsResultAvailable"), DataBinder.Eval(Container.DataItem, "ToServiceUnitName")) %>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:gridtemplatecolumn uniquename="ELVA" headertext="RIS/PACS" headerstyle-width="200px" headerstyle-horizontalalign="Center">
                        <itemtemplate>
                            <%# string.Format("<a href=\"#\" title=\"view image\" onclick=\"viewResult('{0}{1}','{2}','dicom', '{3}'); return false;\"><img src=\"../../../Images/pacspreview.jpg\" border=\"0\" style=\"width: 80px; height: 80px;\" /></a>",DataBinder.Eval(Container.DataItem, "DcomUrlLocation"), string.Empty, DataBinder.Eval(Container.DataItem, "IsResultAvailable"), DataBinder.Eval(Container.DataItem, "ToServiceUnitName")) %>
                        </itemtemplate>
                        <itemstyle verticalalign="Middle" horizontalalign="Center" />
                    </telerik:gridtemplatecolumn>
                    <telerik:GridTemplateColumn UniqueName="RadResult" HeaderText="Result">
                        <ItemTemplate>
                            <div>
                                <%# DataBinder.Eval(Container.DataItem, "ResultValue") %>
                            </div>
                            <telerik:RadGrid ID="grdRadiologyResult" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnItemDataBound="grdRadiologyResult_OnItemDataBound" ShowHeader="False" Width="100%">
                                <MasterTableView DataKeyNames="TransactionNo,SequenceNo">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="LabOrderSummary"
                                            HeaderText="Exam Name" HeaderStyle-Width="350px">
                                            <ItemTemplate>
                                                <div style="font-weight: bold">
                                                    •&nbsp;<%#DataBinder.Eval(Container.DataItem, "ItemName")%>&nbsp;
                                                    <%#string.Format("<a href=\"#\" onclick=\"javascript:printPreviewRadiologyResult('{0}', '{1}'); return false;\"><img src=\"{2}/Images/Toolbar/print16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),Helper.UrlRoot()) %>
                                                </div>
                                                <%#DataBinder.Eval(Container.DataItem, "TestResult")%>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-Width="700px">
                                            <ItemTemplate>

                                                <telerik:RadListView ID="lvItemDocumentImage" runat="server" RenderMode="Lightweight" Width="100%" PageSize="10" AllowCustomPaging="True"
                                                    ItemPlaceholderID="ImageContainer">
                                                    <LayoutTemplate>
                                                        <div style="height: 180px; overflow: auto;">
                                                            <asp:PlaceHolder ID="ImageContainer" runat="server"></asp:PlaceHolder>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <div style="width: 250px; text-align: center;">
                                                            <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="Zoom"
                                                                OnClientClick='<%#string.Format("javascript:ZoomViewImage(\"{0}\",\"{1}\",{2});return false;",DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                                                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                                                    Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("DocumentImage") == DBNull.Value? new System.Byte[0]: Eval("DocumentImage") %>'></telerik:RadBinaryImage>
                                                            </asp:LinkButton>
                                                            <br />
                                                            <%#DataBinder.Eval(Container.DataItem, "DocumentName")%><br />
                                                            <%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.Date))%>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:RadListView>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        </telerik:GridTemplateColumn>
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
    </telerik:RadPageView>

    <telerik:RadPageView ID="pgPathology" runat="server" Width="100%">
        <telerik:RadGrid ID="grdPathology" runat="server" OnNeedDataSource="grdPathology_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" Height="560px" Width="100%"
            OnItemCommand="grdJobOrder_ItemCommand" OnItemDataBound="grdPathology_OnItemDataBound">
            <MasterTableView DataKeyNames="TransactionNo" CommandItemDisplay="Top">
                <CommandItemTemplate>
                    <div>
                        <div class="l">
                            <%# (!IsUserAccessExamOrder(RegistrationNo, IsPostBack) ? string.Format("<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"{0}/Images/Toolbar/new16_d.png\" />&nbsp;Add Pathology Anatomy Order</a>",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryPa('new', ''); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Pathology Anatomy Order</a>",Helper.UrlRoot()))%>
                        </div>
                        <div class="r" style="color: yellow;">
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="RebindPat" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Refresh</asp:LinkButton>&nbsp;
                        </div>
                    </div>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>

                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <%# (Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate"))) 
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) 
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdApproved"))
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdVoid"))) ? 
                                                    string.Format("<a href=\"#\" onclick=\"javascript:entryPa('view', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/views16.png\"  alt=\"view\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())
                                                    : string.Format("<a href=\"#\" onclick=\"javascript:entryPa('edit', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())%>
                            <div style="height: 10px">&nbsp;</div>
                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" ToolTip='Print Job Order Notes'
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                                                    <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/print16.png" border="0" />
                            </asp:LinkButton>
                            <div style="height: 10px">&nbsp;</div>
                            <a href='#' onclick="showLabPaPdfFile('<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>');return false;">
                                <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/imp_exp_pdf16.png" border="0" /></a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="TransactionDate" HeaderText="Date" HeaderStyle-Width="70px">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate").ToStringDefaultEmpty()).ToString(AppConstant.DisplayFormat.DateTime) %><br />
                            <br />
                            <%#DataBinder.Eval(Container.DataItem, "IsHdApproved").ToBoolean()?"<img src=\""+ Helper.UrlRoot() +"/Images/ApprovedStampRed.png\" class=\"apprImg\" />":String.Empty%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="250px">
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
                                <div style="height: 4px;"></div>
                                <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="Zoom"
                                    OnClientClick='<%#string.Format("javascript:ZoomViewImage(\"{0}\",\"{1}\",{2});return false;",DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),"1")%>'>
                                    <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                        Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("DocumentImage") == DBNull.Value? new System.Byte[0]: Eval("DocumentImage") %>'></telerik:RadBinaryImage>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="PatResult" HeaderText="Result">
                        <ItemTemplate>
                            <%#PathologyAnatomyResult((string) DataBinder.Eval(Container.DataItem, "TransactionNo"))%>
                            <div style="height: 4px;"></div>
                            <fieldset style="width: 100%">
                                <legend>Result Image</legend>

                                <telerik:RadListView ID="lvItemDocumentImage" runat="server" RenderMode="Lightweight" Width="100%" PageSize="10" AllowCustomPaging="True"
                                    ItemPlaceholderID="ImageContainer">
                                    <LayoutTemplate>
                                        <div style="height: 180px; overflow: auto;">
                                            <asp:PlaceHolder ID="ImageContainer" runat="server"></asp:PlaceHolder>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <div style="width: 250px; text-align: center;">
                                            <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="Zoom"
                                                OnClientClick='<%#string.Format("javascript:ZoomViewImage(\"{0}\",\"{1}\",{2});return false;",DataBinder.Eval(Container.DataItem, "ResultNo"),"PAT",DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                                                <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                                    Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("DocumentImage") == DBNull.Value? new System.Byte[0]: Eval("DocumentImage") %>'></telerik:RadBinaryImage>
                                            </asp:LinkButton>
                                            <br />
                                            <%#DataBinder.Eval(Container.DataItem, "DocumentName")%><br />
                                            <%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.Date))%>
                                        </div>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </fieldset>
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
    </telerik:RadPageView>

    <telerik:RadPageView ID="pgPathology2" runat="server" Width="100%">
        <telerik:RadGrid ID="grdPathology2" runat="server" OnNeedDataSource="grdPathology2_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" Height="560px" Width="100%"
            OnItemCommand="grdJobOrder_ItemCommand" OnItemDataBound="grdPathology2_OnItemDataBound">
            <MasterTableView DataKeyNames="TransactionNo" CommandItemDisplay="Top">
                <CommandItemTemplate>
                    <div>
                        <div class="l">
                            <%# (!IsUserAccessExamOrder(RegistrationNo, IsPostBack) ? string.Format("<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"{0}/Images/Toolbar/new16_d.png\" />&nbsp;Add Order</a>",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryPa('new', ''); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Pathology Anatomy Order</a>",Helper.UrlRoot()))%>
                        </div>
                        <div class="r" style="color: yellow;">
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="RebindPat2" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Refresh</asp:LinkButton>&nbsp;
                        </div>
                    </div>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <%# (DataBinder.Eval(Container.DataItem, "FromServiceUnitName").Equals((DataBinder.Eval(Container.DataItem, "ToServiceUnitName"))))? string.Empty: ( (Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate")) )
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) 
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdApproved"))
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdVoid"))) ? 
                                                    string.Format("<a href=\"#\" onclick=\"javascript:entryExamOrderOther('view', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/views16.png\"  alt=\"view\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())
                                                    : string.Format("<a href=\"#\" onclick=\"javascript:entryExamOrderOther('edit', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot()))%>
                            <div style="height: 10px">&nbsp;</div>
                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" ToolTip='Print Job Order Notes'
                                Visible='<%#!(DataBinder.Eval(Container.DataItem, "FromServiceUnitName").Equals((DataBinder.Eval(Container.DataItem, "ToServiceUnitName"))))%>'
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                                                    <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/print16.png" border="0" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TransactionDate" HeaderText="Date" HeaderStyle-Width="70px">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate").ToStringDefaultEmpty()).ToString(AppConstant.DisplayFormat.DateTime) %><br />
                            <br />
                            <%#DataBinder.Eval(Container.DataItem, "IsHdApproved").ToBoolean()?"<img src=\""+ Helper.UrlRoot() +"/Images/ApprovedStampRed.png\" class=\"apprImg\" />":String.Empty%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="250px">
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
                    <telerik:GridTemplateColumn UniqueName="Pathology2Result" HeaderText="Result">
                        <ItemTemplate>
                            <telerik:RadGrid ID="grdPathology2Result" runat="server" AutoGenerateColumns="False" GridLines="None"
                                ShowHeader="False" Width="100%">
                                <MasterTableView DataKeyNames="TransactionNo,SequenceNo" NoMasterRecordsText="">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="LabOrderSummary"
                                            HeaderText="Exam Name" HeaderStyle-Width="350px">
                                            <ItemTemplate>
                                                <div style="font-weight: bold">
                                                    •&nbsp;<%#DataBinder.Eval(Container.DataItem, "ItemName")%>&nbsp;
                                                    <%#string.Format("<a href=\"#\" onclick=\"javascript:printPreviewRadiologyResult('{0}', '{1}'); return false;\"><img src=\"{2}/Images/Toolbar/print16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),Helper.UrlRoot()) %>
                                                </div>
                                                <%#DataBinder.Eval(Container.DataItem, "TestResult")%>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        </telerik:GridTemplateColumn>
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
    </telerik:RadPageView>

    <telerik:RadPageView ID="pgOther" runat="server" Width="100%">
        <telerik:RadGrid ID="grdExamOrderOther" runat="server" OnNeedDataSource="grdExamOrderOther_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" Height="560px" Width="100%"
            OnItemCommand="grdJobOrder_ItemCommand" OnItemDataBound="grdExamOrderOther_OnItemDataBound">
            <MasterTableView DataKeyNames="TransactionNo" CommandItemDisplay="Top">
                <CommandItemTemplate>
                    <div>
                        <div class="l">
                            <%# (!IsUserAccessExamOrder(RegistrationNo, IsPostBack) ? string.Format("<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"{0}/Images/Toolbar/new16_d.png\" />&nbsp;Add Order</a>",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryExamOrderOther('new', ''); return false;\"><img src=\"{0}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Order</a>",Helper.UrlRoot()))%>
                        </div>
                        <div class="r" style="color: yellow;">
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="RebindOth" ImageUrl="~/Images/Toolbar/refresh16.png"><img src="<%=Helper.UrlRoot()%>/Images/Toolbar/refresh16.png" />&nbsp;Refresh</asp:LinkButton>&nbsp;
                        </div>
                    </div>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <%# (DataBinder.Eval(Container.DataItem, "FromServiceUnitName").Equals((DataBinder.Eval(Container.DataItem, "ToServiceUnitName"))))? string.Empty: ( (Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate")) )
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) 
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdApproved"))
                                                 || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdVoid"))) ? 
                                                    string.Format("<a href=\"#\" onclick=\"javascript:entryExamOrderOther('view', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/views16.png\"  alt=\"view\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot())
                                                    : string.Format("<a href=\"#\" onclick=\"javascript:entryExamOrderOther('edit', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),Helper.UrlRoot()))%>
                            <div style="height: 10px">&nbsp;</div>
                            <%--                            <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                Visible='<%#!(Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate")))
                                                              || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBillProceed")) 
                                                              || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdApproved"))
                                                              || Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsHdVoid"))) %>'
                                OnClientClick="javascript: if (!confirm('Delete this record, are you sure ?')) return false;">
                                                    <img style="border: 0px; vertical-align: middle;" src="<%=Helper.UrlRoot()%>/Images/Toolbar/row_delete16.png" />
                            </asp:LinkButton>
                            <hr />--%>
                            <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="Print" ToolTip='Print Job Order Notes'
                                Visible='<%#!(DataBinder.Eval(Container.DataItem, "FromServiceUnitName").Equals((DataBinder.Eval(Container.DataItem, "ToServiceUnitName"))))%>'
                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransactionNo")%>'>
                                                    <img src="<%=Helper.UrlRoot()%>/Images/Toolbar/print16.png" border="0" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TransactionDate" HeaderText="Date" HeaderStyle-Width="70px">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "TransactionDate").ToStringDefaultEmpty()).ToString(AppConstant.DisplayFormat.DateTime) %><br />
                            <br />
                            <%#DataBinder.Eval(Container.DataItem, "IsHdApproved").ToBoolean()?"<img src=\""+ Helper.UrlRoot() +"/Images/ApprovedStampRed.png\" class=\"apprImg\" />":String.Empty%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="250px">
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
                    <telerik:GridTemplateColumn UniqueName="TestResult" HeaderText="Result">
                        <ItemTemplate>
                            <telerik:RadGrid ID="grdExamOrderOtherResult" runat="server" AutoGenerateColumns="False" GridLines="None"
                                ShowHeader="False" Width="100%">
                                <MasterTableView DataKeyNames="TransactionNo,SequenceNo" NoMasterRecordsText="">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="LabOrderSummary"
                                            HeaderText="Exam Name" HeaderStyle-Width="350px">
                                            <ItemTemplate>
                                                <div style="font-weight: bold">
                                                    •&nbsp;<%#DataBinder.Eval(Container.DataItem, "ItemName")%>&nbsp;
                                                    <%#string.Format("<a href=\"#\" onclick=\"javascript:printPreviewExamOrderOtherResult('{0}', '{1}'); return false;\"><img src=\"{2}/Images/Toolbar/print16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),Helper.UrlRoot()) %>
                                                </div>
                                                <%#DataBinder.Eval(Container.DataItem, "TestResult")%>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        </telerik:GridTemplateColumn>
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
    </telerik:RadPageView>
</telerik:RadMultiPage>
