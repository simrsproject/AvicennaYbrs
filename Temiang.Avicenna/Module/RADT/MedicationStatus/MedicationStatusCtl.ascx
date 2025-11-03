<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedicationStatusCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicationStatus.MedicationStatusCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadCodeBlock ID="radCodeBlock" runat="server">
    <style type="text/css">
        #tableStyle01 {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #tableStyle01 td, #tableStyle01 th {
                border: 1px solid #a9a9a9;
                padding: 4px;
            }

            #tableStyle01 th {
                text-align: left;
                /*font-size: smaller;*/
                padding-top: 6px;
                padding-bottom: 6px;
                background-color: #4CAF50;
                color: white;
            }
    </style>
    <script type="text/javascript" language="javascript">
        function ms_openWindow(url, width, height) {
            var oWnd;
            oWnd = radopen(url, 'ms_winDialog');
            oWnd.setSize(width, height);
            oWnd.center();
        }
        function ms_openWinEntry(url, width, height) {
            url = url + '&rt=<%= Request.QueryString["rt"] %>';
            ms_openWindow(url, width, height);
        }
        function entryMedicationReceiveUsed(mod, medrecno, seqno, timeSchedule, patientID, dayNo, isAdditional) {
            entryMedicationReceiveUsed(mod, medrecno, seqno, timeSchedule, patientID, dayNo, isAdditional, 0)
        }
        function entryMedicationReceiveUsed(mod, medrecno, seqno, timeSchedule, patientID, dayNo, isAdditional, scno) {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationReceiveUsedEntry.aspx?mod=' + mod + '&prgid=<%= ProgramID %>&patid=' + patientID + '&medrecno=' + medrecno + '&seqno=' + seqno + '&time=' + timeSchedule + '&dayno=' + dayNo + '&isAdditional=' + isAdditional + '&scno=' + scno + '&stat=<%=MedicationStep%>&ccm=rebind&cet=<%=grdMedicationStatus.ClientID %>';
            ms_openWinEntry(url, 600, 550);
        }
        function entryMedicationChangeConsumeMethod(medrecno, conmtd, patientID) {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationChangeConsumeMethod.aspx?mod=new&prgid=<%= ProgramID %>&patid=' + patientID + '&medrecno=' + medrecno + '&conmtd=' + conmtd + '&stat=<%=MedicationStep%>&ccm=rebind&cet=<%=grdMedicationStatus.ClientID %>';
            ms_openWinEntry(url, 600, 550);
        }
        function entryMedicationReceiveEdit(medrecno, patientID) {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationReceiveEdit.aspx?mod=edit&prgid=<%= ProgramID %>&patid=' + patientID + '&medrecno=' + medrecno + '&stat=<%=MedicationStep%>&ccm=rebind&cet=<%=grdMedicationStatus.ClientID %>';
            ms_openWinEntry(url, 600, 550);
        }
        function openRasproFormView(patid, regno, rseqno) {
            var url = "<%= Helper.UrlRoot() %>/Module/RADT/Ppra/RasproFormView.aspx?patid=" + patid + "&regno=" + regno + "&rseqno=" + rseqno;
            var height =
                (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

            ms_openWinEntry(url, 1200, height - 40);

        }

        function entryMedicationScheduleSetup(medrecno, scdate, scno, timeSchedule) {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/MedicationStatus/MedicationScheduleEntry.aspx?mod=new&prgid=<%= ProgramID %>&medrecno=' + medrecno + '&scdate=' + scdate + '&scno=' + scno + '&time=' + timeSchedule + '&ccm=rebind&cet=<%=grdMedicationStatus.ClientID %>';
            ms_openWinEntry(url, 600, 550);
        }

        function deleteMedicationReceiveUsed(medrecno, seqno) {
            if (confirm("Cancel this Medication <%= MedicationStep == "S"? "Setup" : MedicationStep == "V"? "Verification" : "Realization"%>?"))
                __doPostBack("<%= grdMedicationStatus.ClientID%>", "del|<%= MedicationStep%>|" + medrecno + "|" + seqno);
        }
        function ms_radWindowManager_ClientClose(oWnd, args) {
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

<telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdMedicationStatus">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="loadingPnl" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadAjaxLoadingPanel ID="loadingPnl" runat="server" Skin="Default">
</telerik:RadAjaxLoadingPanel>
<telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
    Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
    ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="ms_radWindowManager_ClientClose">
    <Windows>
        <telerik:RadWindow ID="ms_winDialog" Width="900px" Height="600px" runat="server"
            ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<telerik:RadGrid ID="grdMedicationStatus" runat="server" OnDetailTableDataBind="grdMedicationStatus_DetailTableDataBind"
    AutoGenerateColumns="False" GridLines="None"
    OnItemCommand="grdMedicationStatus_ItemCommand" OnItemDataBound="grdMedicationStatus_ItemDataBound">
    <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid,IsAntibiotic,IsContinue">
        <Columns>
            <telerik:GridTemplateColumn UniqueName="ItemDescription" HeaderStyle-Width="350px" HeaderText="Item">
                <ItemTemplate>
                    <table id="tableStyle01">
                        <tr>
                            <th>
                                <%# Temiang.Avicenna.BusinessObject.MedicationReceive.PrescriptionItemDescription(DataBinder.Eval( Container.DataItem, "RefTransactionNo"), DataBinder.Eval( Container.DataItem, "RefSequenceNo"), DataBinder.Eval( Container.DataItem, "ItemDescription"), false, DataBinder.Eval( Container.DataItem, "SRMedicationRoute"))%>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <%# MedicationChangeConsumeMethodHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "BalanceQty"), DataBinder.Eval(Container.DataItem, "IsAntibiotic")) %>
                                <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQtyInString")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%><br />
                                <%# DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName")==DBNull.Value?string.Empty:String.Format("{0}<br,>",DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName"))%>
                                <%--(<%# MedicationScheduleSetupHtml(Container)%>)<br />--%>
                        Start: <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "StartDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute)%> &nbsp;
                                
                                <%# MedicationEditHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "PatientID")) %>

                                <%# true.Equals("IsAntibiotic") && !0.Equals(Eval("RasproSeqNo"))? string.Format("&nbsp;&nbsp;<a href=\"#\" onclick=\"javascript:openRasproFormView('{0}','{1}','{2}'); return false;\"><img src=\"{4}/Images/Toolbar/views16.png\" border=\"0\" alt=\"Raspro Form\" title=\"Raspro Form\" />&nbsp;{3}</a>",
                                            Eval("PatientID"),Eval("RegistrationNo"),Eval("RasproSeqNo"),Eval("SRRaspro"),Helper.UrlRoot()):string.Empty %>

                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="RefTransactionNo" UniqueName="RefTransactionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" Visible="False" />
            <telerik:GridTemplateColumn UniqueName="Day0" HeaderStyle-Width="250px" HeaderText="This Day Medication" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%--<%# MedicationScheduleHtml(Container, 0)%>--%>
                    <%# DataBinder.Eval(Container.DataItem, "ScheduleDay0")%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="Day1" HeaderStyle-Width="250px" HeaderText="This Day + 1" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%--<%# MedicationScheduleHtml(Container, 1)%>--%>
                    <%# DataBinder.Eval(Container.DataItem, "ScheduleDay1")%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="Day2" HeaderStyle-Width="250px" HeaderText="This Day + 2" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%--<%# MedicationScheduleHtml(Container, 2)%>--%>
                    <%# DataBinder.Eval(Container.DataItem, "ScheduleDay2")%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridDateTimeColumn DataField="ReceiveDateTime" UniqueName="ReceiveDateTime" HeaderText="Receive Time"
                HeaderStyle-Width="110px" />
            <telerik:GridNumericColumn DataField="ReceiveQty" UniqueName="ReceiveQty" HeaderText="Rec. Qty" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Bal. Qty" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridNumericColumn DataField="BalanceRealQty" UniqueName="BalanceRealQty" HeaderText="Bal Real" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
            <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="60px" />
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" Visible="False">
                <ItemTemplate>
                    <%# ( DataBinder.Eval(Container.DataItem, "ReceiveDateTime") == DBNull.Value || Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true)) ? "<img src=\"../../../Images/Toolbar/edit16_d.png\" />" : string.Format("<a href=\"#\" onclick=\"javascript:entryMedicationReceive('edit', '{0}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "MedicationReceiveNo")))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridDateTimeColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                SortExpression="RegistrationDate" Visible="False">
                <HeaderStyle HorizontalAlign="Center" Width="75px" />
                <ItemStyle HorizontalAlign="Center" />
            </telerik:GridDateTimeColumn>
            <telerik:GridBoundColumn DataField="PatientID" HeaderText="PatientID" UniqueName="PatientID" Visible="False"></telerik:GridBoundColumn>
            <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" Visible="False">
                <ItemTemplate>
                    <%# (DataBinder.Eval(Container.DataItem, "ReceiveDateTime")==DBNull.Value || Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))  ? "<img src=\"../../../Images/Toolbar/row_delete16_d.png\" />" : "")%>
                    <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                        Visible='<%# !(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")==DBNull.Value || Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")) < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) %>'
                        OnClientClick="javascript: if (!confirm('Void this Medication, are you sure ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" />
                    </asp:LinkButton>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
        </Columns>
        <DetailTables>
            <telerik:GridTableView DataKeyNames="MedicationReceiveNo, SequenceNo" Name="grdMedicationStatusUsed" Width="100%"
                AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="200px" HeaderText="">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        <ItemTemplate>
                            <%# MedicationUsedEditHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),DataBinder.Eval(Container.DataItem, "ScheduleDateTime"),DataBinder.Eval(Container.DataItem, "HandoversDateTime"),DataBinder.Eval(Container.DataItem, "VerificationDateTime"),DataBinder.Eval(Container.DataItem, "RealizedDateTime")) %><br />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                        <ItemTemplate>
                            <%# (MedicationStep == "S" && (Eval("VerificationByUserName")==null || string.IsNullOrWhiteSpace(Eval("VerificationByUserName").ToString()))) 
                                    || (MedicationStep == "V" && Eval("VerificationByUserName")!=null && !string.IsNullOrWhiteSpace(Eval("VerificationByUserName").ToString()) && (Eval("RealizedByUserName")==null || string.IsNullOrWhiteSpace(Eval("RealizedByUserName").ToString()) ))
                                    || (MedicationStep == "R" && Eval("RealizedByUserName") != null && !string.IsNullOrWhiteSpace(Eval("RealizedByUserName").ToString())) 
                                    ? string.Format("<a  href=\"#\" onclick=\"javascript:deleteMedicationReceiveUsed('{0}','{1}'); return false;\"><img style='vertical-align: text-bottom;' src=\"../../../Images/Toolbar/delete16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "SequenceNo")): string.Empty %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridDateTimeColumn DataField="ScheduleDateTime" HeaderText="Schedule" UniqueName="ScheduleDateTime" HeaderStyle-Width="120px" />
                    <telerik:GridDateTimeColumn DataField="SetupDateTime" HeaderText="Setup" UniqueName="SetupDateTime" HeaderStyle-Width="120px" />
                    <telerik:GridBoundColumn DataField="SetupByUserName" UniqueName="SetupByUserName" HeaderText="Setup By" HeaderStyle-Width="120px" />

                    <telerik:GridTemplateColumn UniqueName="Handovers" HeaderStyle-Width="120px" HeaderText="Handovers">
                        <ItemTemplate>
                            <%# MedicationScheduleGridDetailHtml(Container,"H") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="Handovers" HeaderStyle-Width="120px" HeaderText="From -> To">
                        <ItemTemplate>
                            <%# string.Format("{0} -> {1}", Eval("HandoversByUserName"), Eval("HandoversToUserName")) %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn UniqueName="Verification" HeaderStyle-Width="120px" HeaderText="Verification">
                        <ItemTemplate>
                            <%# MedicationScheduleGridDetailHtml(Container,"V") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="VerificationByUserName" UniqueName="VerificationByUserName" HeaderText="Verification By" HeaderStyle-Width="120px" />

                    <telerik:GridTemplateColumn UniqueName="Realized" HeaderStyle-Width="120px" HeaderText="Realized">
                        <ItemTemplate>
                            <%# MedicationScheduleGridDetailHtml(Container,"R") %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="RealizedByUserName" UniqueName="RealizedByUserName" HeaderText="Realized By" HeaderStyle-Width="120px" />

                    <telerik:GridNumericColumn DataField="Qty" UniqueName="Qty" HeaderText="Qty" DecimalDigits="2" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                    <telerik:GridCheckBoxColumn DataField="IsNotConsume" UniqueName="IsNotConsume" HeaderText="Not Consume" HeaderStyle-Width="70px" />
                    <telerik:GridCheckBoxColumn DataField="IsReSchedule" UniqueName="IsReSchedule" HeaderText="Reschedule" HeaderStyle-Width="70px" />
                    <telerik:GridCheckBoxColumn DataField="IsVoidSchedule" UniqueName="IsVoidSchedule" HeaderText="Void Schedule" HeaderStyle-Width="70px" />
                    <telerik:GridCheckBoxColumn DataField="IsAdditionalSchedule" UniqueName="IsAdditionalSchedule" HeaderText="Extra" HeaderStyle-Width="70px" />
                    <telerik:GridBoundColumn DataField="Note" UniqueName="Note" HeaderText="Note" />

                    <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                </Columns>
            </telerik:GridTableView>
        </DetailTables>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling FrozenColumnsCount="1"></Scrolling>
    </ClientSettings>
</telerik:RadGrid>
