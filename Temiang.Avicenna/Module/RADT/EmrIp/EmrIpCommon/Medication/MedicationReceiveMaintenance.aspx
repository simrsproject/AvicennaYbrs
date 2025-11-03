<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveMaintenance.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveMaintenance" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">

        <script type="text/javascript" language="javascript">

            function entryStopReason(mrecno) {
                var url = "MedicationStopConfirm.aspx?mrecno=" + mrecno + "&ccm=rebind&cet=<%=grdMedicationReceive.ClientID %>";
                openWindow(url, 400, 420);
            }

            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }

            function radWindowManager_ClientClose(oWnd, args) {
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

    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdMedicationReceive">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationReceive" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="grdMedicationReceive" runat="server" OnNeedDataSource="grdMedicationReceive_NeedDataSource" OnDetailTableDataBind="grdMedicationReceive_DetailTableDataBind"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdMedicationReceive_DeleteCommand" Height="560px"
        OnItemCommand="grdMedicationReceive_ItemCommand" OnItemDataBound="grdMedicationReceive_OnItemDataBound">
        <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblContinue" runat="server" CommandName="Continue" ToolTip="Continue"
                            Visible='<%#!((DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) && (DataBinder.Eval(Container.DataItem, "IsContinue") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsContinue").Equals(false)) %>'
                            OnClientClick="javascript: if (!confirm('Continue this Medication, are you sure ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/lock16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" HeaderText="">
                    <ItemTemplate>
                        <%# !((DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) && (DataBinder.Eval(Container.DataItem, "IsContinue") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsContinue").Equals(true))? 
                        string.Format("<a href=\"#\" onclick=\"javascript:entryStopReason('{1}'); return false;\"><img src=\"{0}/Images/Toolbar/unlock16.png\"  alt=\"stop\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "MedicationReceiveNo")):""%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="IsAdmissionAppropriate" UniqueName="IsAdmissionAppropriate" HeaderText="Appropriate"
                    HeaderStyle-Width="70px" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="250px" HeaderText="Item">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%><br />
                        <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%><br />
                        <%# DataBinder.Eval(Container.DataItem, "SRMedicationConsumeName")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="ReceiveDateTime" UniqueName="ReceiveDateTime" HeaderText="Receive Time"
                    HeaderStyle-Width="110px" />
                <telerik:GridDateTimeColumn DataField="LastConsumeDateTime" UniqueName="LastConsumeDateTime" HeaderText="Last Consume"
                    HeaderStyle-Width="110px" />
                <telerik:GridDateTimeColumn DataField="ExpireDate" UniqueName="ExpireDate" HeaderText="Expire"
                    HeaderStyle-Width="110px" />
                <telerik:GridBoundColumn DataField="Condition" UniqueName="Condition" HeaderText="Condition" HeaderStyle-Width="100px" />
                <telerik:GridNumericColumn DataField="ReceiveQty" UniqueName="ReceiveQty" HeaderText="Rec. Qty" HeaderStyle-Width="60px" />
                <telerik:GridNumericColumn DataField="BalanceQty" UniqueName="BalanceQty" HeaderText="Bal. Qty" HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="RefTransactionNo" UniqueName="RefTransactionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px" Visible="False">
                    <ItemTemplate>
                        <%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true)) ? string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" />",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryMedicationReceive('edit', '{0}'); return false;\"><img src=\"{1}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),Helper.UrlRoot()))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")).Date < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))  ? string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" />",Helper.UrlRoot()) : "")%>
                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                            Visible='<%#!(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ReceiveDateTime")) < DateTime.Now.Date || (DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) %>'
                            OnClientClick="javascript: if (!confirm('Void this Medication, are you sure ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/row_delete16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="grdMedicationReceiveUsed" Width="100%"
                    AutoGenerateColumns="false" ShowFooter="true" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridDateTimeColumn DataField="ScheduleDateTime" HeaderText="Schedule" UniqueName="ScheduleDateTime" HeaderStyle-Width="120px" />
                        <telerik:GridDateTimeColumn DataField="SetupDateTime" HeaderText="Setup" UniqueName="SetupDateTime" HeaderStyle-Width="120px" />
                        <telerik:GridBoundColumn DataField="SetupByUserName" UniqueName="SetupByUserName" HeaderText="Setup By" HeaderStyle-Width="120px" />

                        <telerik:GridDateTimeColumn DataField="VerificationDateTime" HeaderText="Verification" UniqueName="VerificationDateTime" HeaderStyle-Width="120px" />
                        <telerik:GridBoundColumn DataField="VerificationByUserName" UniqueName="VerificationByUserName" HeaderText="Verification By" HeaderStyle-Width="120px" />

                        <telerik:GridDateTimeColumn DataField="RealizedDateTime" HeaderText="Realized" UniqueName="RealizedDateTime" HeaderStyle-Width="120px" />
                        <telerik:GridBoundColumn DataField="RealizedByUserName" UniqueName="RealizedByUserName" HeaderText="Realized By" HeaderStyle-Width="120px" />

                        <telerik:GridNumericColumn DataField="Qty" UniqueName="Qty" HeaderText="Qty" DecimalDigits="2" HeaderStyle-Width="60px" />
                        <telerik:GridBoundColumn DataField="Note" UniqueName="Note" HeaderText="Note" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
