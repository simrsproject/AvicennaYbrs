<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveReconciliaton.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveReconciliaton" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <link rel="stylesheet" href="<%=Helper.UrlRoot()%>/App_Themes/Default/SmallSwitch.css">

        <script type="text/javascript" language="javascript">

            function entryReconStatus(mrecno) {
                var url = "MedicationReceiveReconciliatonStat.aspx?prgid=<%= ProgramID %>&mrecno=" + mrecno + "&rectype=<%=Request.QueryString["rectype"]%>&ccm=rebind&cet=<%=grdMedicationReceive.ClientID %>";
                openWindow(url, 600, 600);
            }

            function openMedicationReceiveFromPatient() {
                var url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveFromPatientEntry.aspx" +
                    "?mod=view&prgid=<%= ProgramID %>&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=FromRegistrationNo%>&ccm=rebind&cet=<%=grdMedicationReceive.ClientID %>";
                openWindow(url, 1300, 800);
            }

            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();
            }
            function refreshList() {
                var masterTable = $find("<%=grdMedicationReceive.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }
            function togglePrescribed(recno) {
                var masterTable = $find("<%= grdMedicationReceive.ClientID %>").get_masterTableView();
                masterTable.fireCommand("Prescribed", recno);
            }

            function togglePrescribedAll() {
                var hdnPrescribedAll = document.getElementById("<%= hdnPrescribedAll.ClientID %>");
                //if (!confirm('Set all prescribed to ' + (hdnPrescribedAll.value === "0" ? "Yes" : "No") + ' ?')) {
                //    return false;
                //}

                if (hdnPrescribedAll.value == "0")
                    hdnPrescribedAll.value = "1"
                else
                    hdnPrescribedAll.value = "0";


                var masterTable = $find("<%= grdMedicationReceive.ClientID %>").get_masterTableView();
                masterTable.fireCommand(hdnPrescribedAll.value === "1" ? "PrescribedAll" : "NotPrescribedAll");
            }
            function appropriateAll() {
                if (!confirm('Set all to appropriate ?')) return false;

                var masterTable = $find("<%= grdMedicationReceive.ClientID %>").get_masterTableView();
                masterTable.fireCommand("AppropriateAll");
            }
            function entryStopReason(mrecno) {
                var url = "MedicationStopConfirm.aspx?mrecno=" + mrecno + "&ccm=rebind&cet=<%=grdMedicationReceive.ClientID %>";
                openWindow(url, 400, 420);
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

    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdMedicationReceive">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdMedicationReceive" />
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

    <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" />
    <asp:HiddenField runat="server" ID="hdnPrescribedAll" Value="0" />

    <table>
        <tr>
            <td style="width: 400px">
                <fieldset>
                    <legend>Show Record Include</legend>
                    <telerik:RadCheckBox runat="server" ID="chkIncludeIsClosed" Text="Stoped" Checked="true" OnClientClicked="refreshList" AutoPostBack="False"></telerik:RadCheckBox>
                    &nbsp;&nbsp;
        <telerik:RadCheckBox runat="server" ID="chkIncludeEmptyBal" Text="Empty Balance" Checked="true" OnClientClicked="refreshList" AutoPostBack="False"></telerik:RadCheckBox>
                    &nbsp;&nbsp;
        <telerik:RadCheckBox runat="server" ID="chkIncludeReconciled" Text="Reconciled" Checked="true" OnClientClicked="refreshList" AutoPostBack="False"></telerik:RadCheckBox>

                </fieldset>

            </td>
            <td>
                <fieldset>
                    <legend>Menu</legend>
                    <telerik:RadLinkButton runat="server" ID="lnkDrugFromPatient" Text="Drug Item From Patient" Width="200px" Icon-Url="~/Images/Toolbar/ordering16.png" OnClientClicked="openMedicationReceiveFromPatient"></telerik:RadLinkButton>
                    <telerik:RadLinkButton runat="server" ID="lnkHasRecon" Text="Recon has been done" Width="200px" Icon-Url="~/Images/Toolbar/ordering16.png"></telerik:RadLinkButton>
                    <telerik:RadLinkButton runat="server" ID="lnkPrint" Text="Print" Width="70px" Icon-Url="~/Images/Toolbar/print16.png" OnClientClicked="openMedicationReceiveFromPatient"></telerik:RadLinkButton>
                </fieldset>
            </td>
        </tr>
    </table>

    <div style="height: 5px;"></div>
    <telerik:RadGrid ID="grdMedicationReceive" runat="server" OnNeedDataSource="grdMedicationReceive_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None"
        OnItemCommand="grdMedicationReceive_ItemCommand" OnItemDataBound="grdMedicationReceive_OnItemDataBound">
        <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid,ReconStatusID" AllowPaging="False">
            <Columns>
                <%--                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="60px">
                    <HeaderTemplate>
                        <center>
                            <span style="display: <%# DisplayMenuAppropriateAll() %>">
                                <input type="button" value="Appr All" onclick="appropriateAll()" /></span>
                        </center>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lblReconciliation" runat="server" CommandName="Appropriate" ToolTip="Appropriate Drug"
                            Visible='<%# DataBinder.Eval(Container.DataItem, "IsMenuVisible").Equals(true) &&  !((DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) && (DataBinder.Eval(Container.DataItem, "IsAppropriate") == DBNull.Value || DataBinder.Eval(Container.DataItem, "IsAppropriate").Equals(false)) %>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridCheckBoxColumn DataField="IsAppropriate" UniqueName="IsAppropriate" HeaderText="Appr"
                    HeaderStyle-Width="30px" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="80px" HeaderText="Prescribed">
                    <HeaderTemplate>
                        <center>
                            <div style="margin-bottom: 4px;">Prescribed</div>
                            <label class="switch">
                                <%# string.Format("<input id=\"chkOnOffAll\" type=\"checkbox\" onclick=\"togglePrescribedAll();\" name=\"chkOnOffAll\" {0}  />", hdnPrescribedAll.Value.Equals("1")?"checked=\"checked\"":string.Empty)%>
                                <span class="slider round"></span>
                            </label>
                        </center>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <center>
                            <label class="switch">
                                <%# string.Format("<input id=\"chkOnOff\" type=\"checkbox\" name=\"chkOnOff\" {0} onclick=\"togglePrescribed({1});\" />",DataBinder.Eval(Container.DataItem, "IsPrescribed").Equals(true)?"checked=\"checked\"":string.Empty,DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"))%>
                                <span class="slider round"></span>
                            </label>
                        </center>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="50px" HeaderText="">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "IsMenuReconVisible").Equals(true) ?
                        string.Format("&nbsp;<a href=\"#\" onclick=\"javascript:entryReconStatus('{1}'); return false;\"><img src=\"{0}/Images/Toolbar/edit16.png\"  alt=\"stop\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "MedicationReceiveNo")):string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="250px" HeaderText="Item">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%><br />
                        <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="IsClosed" UniqueName="IsClosed" HeaderText="Stop"
                    HeaderStyle-Width="30px" />
                <telerik:GridDateTimeColumn DataField="ReceiveDateTime" UniqueName="ReceiveDateTime" HeaderText="Receive Time"
                    HeaderStyle-Width="120px" />
                <telerik:GridDateTimeColumn DataField="LastConsumeDateTime" UniqueName="LastConsumeDateTime" HeaderText="Last Consume"
                    HeaderStyle-Width="120px" />
                <telerik:GridDateTimeColumn DataField="ExpireDate" UniqueName="ExpireDate" HeaderText="Expired"
                    HeaderStyle-Width="110px" />
                <telerik:GridBoundColumn DataField="Condition" UniqueName="Condition" HeaderText="Condition" HeaderStyle-Width="100px" />
                <telerik:GridNumericColumn DataField="ReceiveQty" UniqueName="ReceiveQty" HeaderText="Rec. Qty" HeaderStyle-Width="60px" />
                <telerik:GridNumericColumn DataField="BalanceRealQty" UniqueName="BalanceRealQty" HeaderText="Bal. Qty" HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="SRConsumeUnit" UniqueName="SRConsumeUnit" HeaderText="Unit" HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="RefTransactionNo" UniqueName="RefTransactionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" />
                <%--<telerik:GridBoundColumn DataField="MedicationReason" UniqueName="MedicationReason" HeaderText="Reason" HeaderStyle-Width="200px" />--%>

                <telerik:GridBoundColumn DataField="ReconStatus" UniqueName="ReconStatus" HeaderText="Reconciliation Status" HeaderStyle-Width="200px" />
                <telerik:GridBoundColumn DataField="NewConsumeMethodName" UniqueName="NewConsumeMethodName" HeaderText="New Consume Method" HeaderStyle-Width="200px" />

                <%--                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%# ((DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) || false.Equals(DataBinder.Eval(Container.DataItem, "IsMenuVisible").Equals(true) && DataBinder.Eval(Container.DataItem, "IsAppropriate") == DBNull.Value)?string.Empty:
                                string.Format("<a href=\"#\" onclick=\"javascript:entryNotAppropriateReason('{1}'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"stop\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"))%>

                        <%# DataBinder.Eval(Container.DataItem, "IsMenuVisible").Equals(true) && !((DataBinder.Eval(Container.DataItem, "IsVoid") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true))) && (DataBinder.Eval(Container.DataItem, "IsAppropriate") != DBNull.Value && DataBinder.Eval(Container.DataItem, "IsAppropriate").Equals(true))?
                        string.Format("&nbsp;<a href=\"#\" onclick=\"javascript:entryNotAppropriateReason('{1}'); return false;\"><img src=\"{0}/Images/Toolbar/cancel16.png\"  alt=\"stop\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "MedicationReceiveNo")):string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>


                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="150px" HeaderText="Confirm Status" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblApprove" runat="server" CommandName="Approve" OnClientClick="return confirm('Approve this Drug Recon Admision ?')" ToolTip="Approve this Drug Recon Admision"
                            Visible='<%# "adm".Equals(Request.QueryString["rectype"]) 
                                && DataBinder.Eval(Container.DataItem, "ReconStatusID")!=DBNull.Value 
                                && false.Equals(DataBinder.Eval(Container.DataItem, "IsVoid")??false) 
                                && DataBinder.Eval(Container.DataItem, "IsApproveAdm")==DBNull.Value%>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16.png" />
                        </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lblUnApprove" runat="server" CommandName="UnApprove" OnClientClick="return confirm('Unapprove this Drug Recon Admision ?')" ToolTip="UnApprove this Drug Recon Admision"
                            Visible='<%# "adm".Equals(Request.QueryString["rectype"]) 
                                && DataBinder.Eval(Container.DataItem, "ReconStatusID")!=DBNull.Value 
                                && false.Equals(DataBinder.Eval(Container.DataItem, "IsVoid")??false) 
                                && DataBinder.Eval(Container.DataItem, "IsApproveAdm")==DBNull.Value %>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/cancel16.png" />
                        </asp:LinkButton>
                        <div>
                            <%# DataBinder.Eval(Container.DataItem, "ApproveAdmDateTime")==DBNull.Value?string.Empty:Convert.ToDateTime(Eval("ApproveAdmDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute) %><br />
                            <%# DataBinder.Eval(Container.DataItem, "IsApproveAdm")==DBNull.Value?string.Empty: (true.Equals(Eval("IsApproveAdm"))? "Approved":"<div style=\"color:red;\">Unapproved</div>" ) %>
                        </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
