<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="MedicationReconEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.MedicationReconEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <link rel="stylesheet" href="<%=Helper.UrlRoot()%>/App_Themes/Default/SmallSwitch.css">
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
        <script type="text/javascript">
            function UpdateAlertIconOnParent(objectName, iCount) {
                if (objectName == null || objectName == undefined || objectName == 'none') {
                    // do nothing
                } else {
                    var obj = GetRadWindow().BrowserWindow.document.getElementById(objectName);
                    obj.innerHTML = iCount;
                    if (iCount > 0) {
                        // set bubble visible true
                        obj.style.visibility = 'visible';
                    } else {
                        // set bubble visible false
                        obj.style.visibility = 'hidden';
                    }
                }
            }

            function entryReconStatus(mrecno, fgid) {
                var fgClientId = "";
                if (fgid == "<%=grdMedicationReceive.ID%>")
                    fgClientId = "<%=grdMedicationReceive.ClientID%>";
                else
                    fgClientId = "<%=grdStoppedMed.ClientID%>"

                var url = "MedicationReconStat.aspx?prgid=<%= ProgramID %>&mrecno=" + mrecno + "&fgid=" + fgid + "&rectype=<%=Request.QueryString["rectype"]%>&ccm=rebind&cet=" + fgClientId;
                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(600, 600);
                oWnd.center();

            }
            var _isModeMedicationReceiveFromPatient = false;
            function openMedicationReceiveFromPatient() {
                _isModeMedicationReceiveFromPatient = true;
                var url = "<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationReceiveFromPatientEntry.aspx" +
                    "?mod=view&prgid=<%= ProgramID %>&patid=<%=PatientID%>&regno=<%=RegistrationNo%>&fregno=<%=FromRegistrationNo%>";
                openWinEntryMaxHeight(url, 1450);
            }

            function openEducation() {
                // PatientEducationDetail.aspx OBSOLETE (Handono 231107)
<%--                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/PatientEducation/PatientEducationDetail.aspx?mod=view&edutype=FAR&regno=<%= RegistrationNo %>&patid=<%= PatientID %>&parid=<%= ParamedicID %>';
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(1000, height - 40);
                oWnd.center();--%>

                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/PatientEducation/PatientEducationHist.aspx?regno=<%= RegistrationNo %>&patid=<%= PatientID %>&parid=<%= ParamedicID %>';
                openWinEntryMaxHeight(url, 1450);
            }

            function openWinEntryMaxHeight(url, width) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var oWnd = $find("<%= winEntry.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
                oWnd.setSize(width, height - 40);
                oWnd.center();
            }

            function refreshList() {
                var masterTable = $find("<%=grdMedicationReceive.ClientID %>").get_masterTableView();
                masterTable.rebind();
            }

            function winEntry_ClientClose(oWnd, args) {
                if (_isModeMedicationReceiveFromPatient) {
                    _isModeMedicationReceiveFromPatient = false;
                    __doPostBack("<%=ToolBarMenuData.ClientID %>", "refresh");
                }
                else {
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

            }

            function ShowSignEntry(mod, signFor) {
                var imgId = "";
                var txtId = "";

                switch (signFor) {
                    case "FAR":
                        imgId = "<%=rbiSign.ClientID %>";
                        txtId = "<%=hdnSign.ClientID %>";
                        break;
                    case "PAR":
                        imgId = "<%=rbiParSign.ClientID %>";
                        txtId = "<%=hdnParSign.ClientID %>";
                        break;
                    case "PAT":
                        imgId = "<%=rbiPatSign.ClientID %>";
                        txtId = "<%=hdnPatSign.ClientID %>";
                        break;

                }

                // Tampilkan tandatangan
                //var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=' + mod + '&imgId=' + imgId + '&txtId=' + txtId;

                var oWnd = $find("<%= winSign.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();

                //    Sys.Application.remove_load(ShowSignEntry);
            }
<%--            function winSign_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    if (arg.image) //non-empty string, true, 42, Infinity, [], ...
                    {
                        var obj = {};
                        obj.registrationNo = "<%= RegistrationNo %>";
                        obj.reconSeqNo = document.getElementById("<%= hdnReconSeqNo.ClientID %>").value;
                        obj.signImage = arg.image;
                        $.ajax({
                            url: '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrWebService.asmx/SaveReconImageSign',
                            data: JSON.stringify(obj), //ur data to be sent to server
                            contentType: "application/json; charset=utf-8",
                            type: "POST",
                            dataType: "json",
                            success: function (data) {
                                var msgFailed = decodeURI(data.d);
                                if (msgFailed != "OK")
                                    radalert(msgFailed, 400, 120, "Confirmation");
                            },
                            error: function (x, y, z) {
                                radalert(x.responseText + "  " + x.status, 400, 120, "Error");
                            }
                        });
                    }
                }
            }--%>

            function winSign_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);

                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winEntry" Width="400px" Height="450px" runat="server" VisibleStatusbar="false"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True" OnClientClose="winEntry_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Move, Close,Maximize,Resize"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winSign_ClientClose"
        ID="winSign" />
    <asp:HiddenField runat="server" ID="hdnReconSeqNo" />
    <table>
        <tr>
            <td valign="top">
                <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" />
            </td>
            <td rowspan="2" valign="top">

                <fieldset style="width: 300px">
                    <legend>Farmacy Sign</legend>
                    <telerik:RadBinaryImage ID="rbiSign" runat="server"
                        Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                    <asp:Button runat="server" ID="btnSign" Text="Sign" Width="300px" OnClientClick="javascript:ShowSignEntry('edit','FAR');return false;" />
                    <asp:HiddenField runat="server" ID="hdnSign" />
                </fieldset>

                <div style="height: 5px;"></div>
                <fieldset runat="server" id="divParSign" style="width: 300px">
                    <legend>Paramedic Sign</legend>
                    <telerik:RadBinaryImage ID="rbiParSign" runat="server"
                        Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                    <asp:Button runat="server" ID="btnParSign" Text="Sign" Width="300px" OnClientClick="javascript:ShowSignEntry('edit','PAR');return false;" />
                    <asp:HiddenField runat="server" ID="hdnParSign" />
                </fieldset>
                <div style="height: 5px;"></div>
                <fieldset runat="server" id="divPatSign" style="width: 300px">
                    <legend>Patient Sign</legend>
                    <telerik:RadBinaryImage ID="rbiPatSign" runat="server"
                        Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                    <asp:Button runat="server" ID="btnPatSign" Text="Sign" Width="300px" OnClientClick="javascript:ShowSignEntry('edit','PAT');return false;" />
                    <asp:HiddenField runat="server" ID="hdnPatSign" />
                </fieldset>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table>
                    <tr>
                        <td valign="top">
                            <fieldset style="width: 450px">
                                <legend>Recon Status</legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">Date</td>
                                        <td class="entry">
                                            <asp:Label runat="server" ID="lblCreateDateTime"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="label">By</td>
                                        <td class="entry">
                                            <asp:Label runat="server" ID="lblCreateByUserName"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="label">Body Weight</td>
                                        <td class="entry">
                                            <asp:Label runat="server" ID="lblBodyWeight"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="label">Status</td>
                                        <td class="entry">
                                            <asp:CheckBox runat="server" ID="chkIsFinish" Text="Finish" /></td>
                                    </tr>
                                </table>
                            </fieldset>

                        </td>
                        <td valign="top">
                            <fieldset runat="server" id="patientTransferSelection" style="width: 450px">
                                <legend>Patient Transfer</legend>
                                <table width="100%">
                                    <tr>
                                        <td class="label">From</td>
                                        <td class="entry">
                                            <telerik:RadMultiColumnComboBox runat="server" ID="cboPatientTransfer"
                                                DropDownWidth="600px" Height="400px" Width="300px"
                                                DataTextField="FromServiceUnitName" DataValueField="TransferNo" Filter="none"
                                                Placeholder="Select from the dropdown" AutoPostBack="true" OnSelectedIndexChanged="cboPatientTransfer_SelectedIndexChanged">
                                                <ColumnsCollection>
                                                    <telerik:MultiColumnComboBoxColumn Field="TransferNo" Title="No" Width="100px" />
                                                    <telerik:MultiColumnComboBoxColumn Field="TransferDate" Title="Date" Width="170px" />
                                                    <telerik:MultiColumnComboBoxColumn Field="FromServiceUnitName" Title="From" Width="200px" />
                                                    <telerik:MultiColumnComboBoxColumn Field="ToServiceUnitName" Title="To" Width="200px" />
                                                </ColumnsCollection>

                                            </telerik:RadMultiColumnComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">To</td>
                                        <td class="entry300">
                                            <asp:Label runat="server" ID="lblToServiceUnit"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">Transfer Date</td>
                                        <td class="entry300">
                                            <asp:Label runat="server" ID="lblTransferDate"></asp:Label></td>
                                    </tr>
                                </table>
                                <div style="height: 5px;"></div>
                            </fieldset>
                        </td>
                        <td></td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>




    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage"
        Align="Left"
        SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Value="ContinuedMed" Text="Not Stopped Therapy" PageViewID="pgContinuedMed"
                Selected="True" />
            <telerik:RadTab runat="server" Value="StoppedMed" Text="Stopped Therapy" PageViewID="pgStoppedMed" />
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" ScrollBars="Auto"
        CssClass="multiPage">

        <telerik:RadPageView ID="pgContinuedMed" runat="server">


            <telerik:RadGrid ID="grdMedicationReceive" runat="server" OnNeedDataSource="grdMedicationReceive_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None"
                OnItemCommand="grdMedicationReceive_ItemCommand" OnItemDataBound="grdMedicationReceive_OnItemDataBound">
                <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid,ReconStatus" AllowPaging="False">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="MenuRecon" HeaderStyle-Width="50px" HeaderText="">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "IsMenuReconVisible").Equals(true) ?
                        string.Format("&nbsp;<a href=\"#\" onclick=\"javascript:entryReconStatus('{1}','{2}'); return false;\"><img src=\"{0}/Images/Toolbar/edit16.png\"  alt=\"stop\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),grdMedicationReceive.ID):string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="250px" HeaderText="Item">
                            <ItemTemplate>
                                <table id="tableStyle01">
                                    <tr>
                                        <th>
                                            <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>
                                        </td>
                                    </tr>
                                </table>
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
                        <%--<telerik:GridBoundColumn DataField="RefTransactionNo" UniqueName="RefTransactionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" />--%>
                        <telerik:GridBoundColumn DataField="LastPrescriptionNo" UniqueName="LastPrescriptionNo" HeaderText="Prescription No" HeaderStyle-Width="100px" />

                        <telerik:GridBoundColumn DataField="ReconStatusName" UniqueName="ReconStatusName" HeaderText="Reconciliation Status" HeaderStyle-Width="200px" />
                        <telerik:GridBoundColumn DataField="NewConsumeMethodName" UniqueName="NewConsumeMethodName" HeaderText="New Consume Method" HeaderStyle-Width="200px" />

                        <telerik:GridTemplateColumn UniqueName="MenuApprove" HeaderStyle-Width="150px" HeaderText="Confirm Status" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblApprove" runat="server" CommandName="Approve" OnClientClick="return confirm('Approve this Drug Recon Admision ?')" ToolTip="Approve this Drug Recon Admision"
                                    Visible='<%# "ADM".Equals(Request.QueryString["rectype"].ToUpper()) 
                                && DataBinder.Eval(Container.DataItem, "ReconStatus")!=DBNull.Value 
                                && false.Equals(DataBinder.Eval(Container.DataItem, "IsVoid")??false) 
                                && DataBinder.Eval(Container.DataItem, "IsApprove")==DBNull.Value%>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/post16.png" />
                                </asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lblUnApprove" runat="server" CommandName="UnApprove" OnClientClick="return confirm('Unapprove this Drug Recon Admision ?')" ToolTip="UnApprove this Drug Recon Admision"
                            Visible='<%# "ADM".Equals(Request.QueryString["rectype"].ToUpper()) 
                                && DataBinder.Eval(Container.DataItem, "ReconStatus")!=DBNull.Value 
                                && false.Equals(DataBinder.Eval(Container.DataItem, "IsVoid")??false) 
                                && DataBinder.Eval(Container.DataItem, "IsApprove")==DBNull.Value %>'>
                        <img style="border: 0px; vertical-align: middle;" src="<%= Helper.UrlRoot() %>/Images/Toolbar/cancel16.png" />
                        </asp:LinkButton>
                                <div>
                                    <%# DataBinder.Eval(Container.DataItem, "ApproveDateTime")==DBNull.Value?string.Empty:Convert.ToDateTime(Eval("ApproveDateTime")).ToString(AppConstant.DisplayFormat.DateHourMinute) %><br />
                                    <%# DataBinder.Eval(Container.DataItem, "IsApprove")==DBNull.Value?string.Empty: (true.Equals(Eval("IsApprove"))? "Approved":"<div style=\"color:red;\">Unapproved</div>" ) %>
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgStoppedMed" runat="server">
            <telerik:RadGrid ID="grdStoppedMed" runat="server" OnNeedDataSource="grdMedicationReceive_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None"
                OnItemCommand="grdMedicationReceive_ItemCommand" OnItemDataBound="grdMedicationReceive_OnItemDataBound">
                <MasterTableView DataKeyNames="MedicationReceiveNo,IsVoid,ReconStatus" AllowPaging="False">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="MenuRecon" HeaderStyle-Width="50px" HeaderText="">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "IsMenuReconVisible").Equals(true) ?
                        string.Format("&nbsp;<a href=\"#\" onclick=\"javascript:entryReconStatus('{1}','{2}'); return false;\"><img src=\"{0}/Images/Toolbar/edit16.png\"  alt=\"stop\" /></a>",Helper.UrlRoot(),DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),grdStoppedMed.ID):string.Empty%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="250px" HeaderText="Item">
                            <ItemTemplate>
                                <table id="tableStyle01">
                                    <tr>
                                        <th>
                                            <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>
                                        </td>
                                    </tr>
                                </table>
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

                        <telerik:GridBoundColumn DataField="ReconStatusName" UniqueName="ReconStatusName" HeaderText="Reconciliation Status" HeaderStyle-Width="200px" />
                        <telerik:GridBoundColumn DataField="NewConsumeMethodName" UniqueName="NewConsumeMethodName" HeaderText="New Consume Method" HeaderStyle-Width="200px" />

                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="False">
                    <Selecting AllowRowSelect="False" />
                </ClientSettings>
            </telerik:RadGrid>


        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
