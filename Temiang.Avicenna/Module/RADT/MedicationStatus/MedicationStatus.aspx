<%@  Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master" AutoEventWireup="true"
    CodeBehind="MedicationStatus.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Medication.MedicationStatus" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <style type="text/css">
            .RadGrid tr.rgOdd {
                background-color: #f2f2f2;
                border: 1px solid #a9a9a9;
            }

            .RadGrid tr.rgEven {
                background-color: white;
                border: 1px solid #a9a9a9;
            }

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
                    font-size: smaller;
                    padding-top: 6px;
                    padding-bottom: 6px;
                    background-color: #4CAF50;
                    color: white;
                }
        </style>
        <script type="text/javascript" language="javascript">
            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function openWinEntry(url, width, height) {
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                openWindow(url, width, height);
            }

            function entryMedicationReceiveUsed(mod, medrecno, seqno, timeSchedule, patientID, dayNo, isAdditional) {
                var url = 'MedicationReceiveUsedEntry.aspx?mod=' + mod + '&patid=' + patientID + '&medrecno=' + medrecno + '&seqno=' + seqno + '&time=' + timeSchedule+'&dayno='+dayNo+'&isAdditional='+isAdditional + '&stat=<%=Request.QueryString["stat"]%>&ccm=rebind&cet=<%=grdMedicationStatus.ClientID %>';
                openWinEntry(url, 600, 550);
            }
            function entryMedicationChangeConsumeMethod(medrecno,conmtd, patientID) {
                var url = 'MedicationChangeConsumeMethod.aspx?mod=new&patid=' + patientID + '&medrecno=' + medrecno +'&conmtd='+conmtd+ '&stat=<%=Request.QueryString["stat"]%>&ccm=rebind&cet=<%=grdMedicationStatus.ClientID %>';
                openWinEntry(url, 600, 550);
            }            
            function entryMedicationReceiveEdit(medrecno, patientID) {
                var url = 'MedicationReceiveEdit.aspx?mod=edit&patid=' + patientID + '&medrecno=' + medrecno + '&stat=<%=Request.QueryString["stat"]%>&ccm=rebind&cet=<%=grdMedicationStatus.ClientID %>';
                openWinEntry(url, 600, 550);
            }
            function openMedicationHist(patid,regno,fregno) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid='+patid+'&regno='+regno+'&fregno='+fregno;
                openWindow(url, 1000, 600);
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
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRoomID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>            
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="btnFilterStartDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>            
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="btnFilterStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>            
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="304px" AllowCustomText="true"
                                    Filter="Contains">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Room"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboRoomID" Width="304px" MarkFirstMatch="true" OnItemDataBound="cboRoomID_ItemDataBound" OnItemsRequested="cboRoomID_ItemsRequested"
                                    EnableLoadOnDemand="true" NoWrap="True">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRoomID" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Medical No / Registration No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtMedRegNo" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterMedRegNo" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                 OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" style="vertical-align: top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label"></td>
                            <td class="entry2Column">
                                <asp:CheckBox ID="chkIsIncludeFinished" runat="server" Text="Show Finished" />
                                <asp:CheckBox ID="chkIsIncludeStopped" runat="server" Text="Show Stopped" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterStatus" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                 OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Start Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker runat="server" id="txtStartDate" ></telerik:RadDatePicker>
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterStartDate" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                                 OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>

    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdMedicationStatus">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationStatus" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>


    <telerik:RadGrid ID="grdMedicationStatus" runat="server" OnNeedDataSource="grdMedicationStatus_NeedDataSource" OnDetailTableDataBind="grdMedicationStatus_DetailTableDataBind"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdMedicationStatus_DeleteCommand" 
        OnItemCommand="grdMedicationStatus_ItemCommand" OnItemDataBound="grdMedicationStatus_ItemDataBound">
        <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid,IsOddBackground,IsAntibiotic,IsContinue" PageSize="7">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Med" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openMedicationHist('{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/ordering16.png\" border=\"0\" alt=\"History\" title=\"Print\" /></a>",
                                DataBinder.Eval(Container.DataItem, "PatientID"), 
                                DataBinder.Eval(Container.DataItem, "RegistrationNo"), 
                                DataBinder.Eval(Container.DataItem, "FromRegistrationNo"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitName"
                    SortExpression="ServiceUnitName">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" Width="110px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="PatientName" HeaderStyle-Width="200px" HeaderText="Patient Name">
                    <ItemTemplate>

                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SRSalutationName"),DataBinder.Eval(Container.DataItem, "PatientName")).Trim()%>
                        <br />
                        Dob: <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateOfBirth")).ToString(AppConstant.DisplayFormat.Date)%> Age: (<%# DataBinder.Eval(Container.DataItem, "Age")%>)
                        <br />
                        Bed: <%# DataBinder.Eval(Container.DataItem, "BedID")%> 
                        <br />
                        Reg: <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%> 
                        <br />
                        MN:<a onclick="javascript:location.replace('MedicationStatus.aspx?stat=<%# Request.QueryString["stat"]%>&mn=<%# DataBinder.Eval(Container.DataItem, "MedicalNo")%>')" href="#"> <%# DataBinder.Eval(Container.DataItem, "MedicalNo")%> </a>

                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="ItemDescription" HeaderStyle-Width="350px" HeaderText="Item">
                    <ItemTemplate>
                        <table id="tableStyle01">
                            <tr>
                                <th>Presc #: <%# DataBinder.Eval(Container.DataItem, "RefTransactionNo")%>
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <%# MedicationChangeConsumeMethodHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),DataBinder.Eval(Container.DataItem, "PatientID"),DataBinder.Eval(Container.DataItem, "BalanceQty")) %>
                                    <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%><br />
                                    <%# DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName")==DBNull.Value?string.Empty:String.Format("{0}<br,>",DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName"))%>
                        Start: <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "StartDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute)%> &nbsp;
                        <%# MedicationEditHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "PatientID")) %>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RefTransactionNo" UniqueName="RefTransactionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" Visible="False" />
                <telerik:GridTemplateColumn UniqueName="Day0" HeaderStyle-Width="200px" HeaderText="This Day Medication" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# MedicationScheduleHtml(Container, 0)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Day1" HeaderStyle-Width="200px" HeaderText="This Day + 1" HeaderStyle-HorizontalAlign="Center"> 
                    <ItemTemplate>
                        <%# MedicationScheduleHtml(Container, 1)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Day2" HeaderStyle-Width="160px" HeaderText="This Day + 2" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# MedicationScheduleHtml(Container, 2)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="ReceiveDateTime" UniqueName="ReceiveDateTime" HeaderText="Receive Time"
                    HeaderStyle-Width="110px" />
                <telerik:GridNumericColumn DataField="ReceiveQty" UniqueName="ReceiveQty" HeaderText="Rec. Qty" HeaderStyle-Width="60px"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Bal. Qty" HeaderStyle-Width="60px"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridNumericColumn DataField="BalanceRealQty" UniqueName="BalanceRealQty" HeaderText="Bal Real" HeaderStyle-Width="60px"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="60px" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" Visible="False">
                    <ItemTemplate>
                        <%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true)) ? "<img src=\"../../../Images/Toolbar/edit16_d.png\" />" : string.Format("<a href=\"#\" onclick=\"javascript:entryMedicationReceive('edit', '{0}'); return false;\"><img src=\"../../../Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "MedicationReceiveNo")))%>
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
                        <%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))  ? "<img src=\"../../../Images/Toolbar/row_delete16_d.png\" />" : "")%>
                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                            Visible='<%#!(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")) < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) %>'
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
                                <%# MedicationUsedEditHtml(DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),DataBinder.Eval(Container.DataItem, "ScheduleDateTime"),DataBinder.Eval(Container.DataItem, "VerificationDateTime"),DataBinder.Eval(Container.DataItem, "RealizedDateTime")) %><br />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                                    Visible='<%# DataBinder.Eval(Container.DataItem, "VerificationDateTime")==DBNull.Value || true.Equals(DataBinder.Eval(Container.DataItem, "IsVoidSchedule"))%>'
                                    OnClientClick="javascript: if (!confirm('Delete this Medication Setup ?')) return false;">
                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn DataField="ScheduleDateTime" HeaderText="Schedule" UniqueName="ScheduleDateTime" HeaderStyle-Width="120px" />
                        <telerik:GridDateTimeColumn DataField="SetupDateTime" HeaderText="Setup" UniqueName="SetupDateTime" HeaderStyle-Width="120px" />
                        <telerik:GridBoundColumn DataField="SetupByUserName" UniqueName="SetupByUserName" HeaderText="Setup By" HeaderStyle-Width="120px" />
                        
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="120px" HeaderText="Verification">
                            <ItemTemplate>
                                <%# MedicationScheduleGridDetailHtml(Container,"V") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="VerificationByUserName" UniqueName="VerificationByUserName" HeaderText="Verification By" HeaderStyle-Width="120px" />
                        
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="120px" HeaderText="Realized">
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
</asp:Content>
