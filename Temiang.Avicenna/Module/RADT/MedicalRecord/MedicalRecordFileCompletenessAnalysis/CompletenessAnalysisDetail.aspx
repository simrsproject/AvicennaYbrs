<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="CompletenessAnalysisDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.MedicalRecordFileCompletenessAnalysis.CompletenessAnalysisDetail" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/PHR/PhrCtl.ascx" TagPrefix="uc1" TagName="PhrCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
    <%=JavascriptOpenPrintPreview()%>
        <script type="text/javascript" language="javascript">
            function onSubmitToUnit(oWnd, args) {
                if (confirm('Are you sure to submit this note?')) {
                    __doPostBack("<%= grdItem.UniqueID %>", "submit");
                }
            }

            function openWinDetail(id) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("CompletenessAnalysisHistoryDetailList.aspx?id=" + id);
                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
            }

            var height = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

            function onClientButtonClicking(regNo, docType, progId, qfid, rimid, careNo, haisType) {
                switch (docType) {
                    case "Assessment":
                        printPreviewIntegratedNotes(rimid);
                        break;
                    case "PHR":
                        openWinDialog(qfid, "phr");
                        break;
                    case "Surgical":
                        openWinDialog(qfid, "sur");
                        break;
                    case "Report":
                    case "Education":
                    case "UDDRecon":
                        printPreview(progId, regNo);
                        break;
                    case "Careplan":
                        printPreviewCarePlan(progId, regNo, careNo);
                        break;
                    case "Hais":
                        switch (haisType) {
                            case "01": // Infus
                                entryNosocomial("infus");
                                break;
                            case "02": // InfusCentral
                                entryNosocomial("infuscentral");
                                break;
                            case "03": // Catheter
                                entryNosocomial("catheter");
                                break;
                            case "04": // Ngt
                                entryNosocomial("ngt");
                                break;
                            case "05": // Surgery
                                entryNosocomial("surgery");
                                break;
                            case "06": // Ett
                                entryNosocomial("ett");
                                break;
                            case "07": // BedRest
                                entryNosocomial("bedrest");
                                break;
                            case "08": // Hap
                                entryNosocomial("hap");
                                break;
                        }
                        break;
                    case "EWS":
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartEws.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>&rt=IPR");
                        oWnd.setSize(document.body.offsetWidth - 40, height - 40);
                        oWnd.show();
                        break;
                    case "Partograph":
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart/Partograph.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>");
                        oWnd.setSize(document.body.offsetWidth - 40, height - 40);
                        oWnd.show();
                        break;
                    case "Kardex":
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/Medication/MedicationHist.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>&rt=IPR&mod=view");
                        oWnd.setSize(document.body.offsetWidth - 40, height - 40);
                        oWnd.show();
                        break;
                    case "Fluid":
                        var oWnd = $find("<%= winDialog.ClientID %>");
                        oWnd.setUrl("<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/FluidBalance/FluidBalanceDesktop.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&fregno=<%=ReferFromRegistrationNo %>&rt=IPR&mod=view");
                        oWnd.setSize(document.body.offsetWidth - 40, height - 40);
                        oWnd.show();
                        break;
                    default:
                        alert("document files has not been set");
                        break;
                }
            }

            function printPreviewIntegratedNotes(rimid) {
                var obj = {};
                obj.parameterValue = rimid;
                openPrintPreview("PopulatePrintParameterIntegratedNotes", obj);
            }

            function openWinDialog(questionFormId, view) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.SetUrl("CompletenessAnalysisDialog.aspx?&regno=<%= RegistrationNo %>&view=" + view + "&qfid=" + questionFormId);
                oWnd.setSize(document.body.offsetWidth - 40, height - 40);
                oWnd.show();
            }

            function printPreview(programId, regNo) {
                var obj = {};
                obj.programID = programId;
                obj.registrationNo = regNo;
                openPrintPreview("PopulatePrintParameter", obj);
            }

            function openWinReport(programId) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("<%=Helper.UrlRoot()%>/Module/Reports/ReportOption.aspx?id=" + programId + "&tp=rpt");
                oWnd.show();
            }

            function printPreviewCarePlan(programId, regNo, careNo) {
                var obj = {};
                obj.programID = programId;
                obj.registrationNo = regNo;
                obj.transactionNo = careNo;
                openPrintPreview("PopulatePrintParameterCarePlan", obj);
            }

            function entryNosocomial(montype) {
                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl("<%=Helper.UrlRoot()%>/Module/RADT/EmrIp/EmrIpCommon/Nosocomial/NosocomialMain.aspx?regno=<%= RegistrationNo %>&patid=<%= PatientID %>&montype=" + montype + "&rt=IPR&mod=view");
                oWnd.setSize(document.body.offsetWidth - 40, height - 40);
                oWnd.show();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="500px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" VisibleStatusbar="False" Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 90%">
                                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100%" ReadOnly="True" />
                                    </td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 8%">
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="100%" ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblGender" runat="server" Text="Date of Birth / Age"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 78%">
                                        <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="100%" MaxLength="20" ReadOnly="true" />
                                    </td>
                                    <td style="width: 2%"></td>
                                    <td style="width: 20%">
                                        <telerik:RadTextBox ID="txtAge" runat="server" Width="100%" ReadOnly="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAddress" runat="server" Width="100%" ReadOnly="True"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="Analisys Information"></asp:Label></legend>
                    <table width="100%">
                        <tr>
                            <td width="50%" valign="top">
                                <table>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTransactionDate" runat="server" Text="Analisys Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtTransactionDate" runat="server" Width="100px" Enabled="false" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDepartmentID" runat="server" Text="Department"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDepartmentID" runat="server" Width="100px" ReadOnly="True" Visible="false" />
                                            <telerik:RadTextBox ID="txtDepartmentName" runat="server" Width="100%" ReadOnly="True" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblSRFilesAnalysis" runat="server" Text="Files Analysis"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboSRFilesAnalysis" runat="server" Width="100%" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSRFilesAnalysis_ItemDataBound"
                                                OnItemsRequested="cboSRFilesAnalysis_ItemsRequested" AutoPostBack="true" OnSelectedIndexChanged="cboSRFilesAnalysis_SelectedIndexChanged">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "FilesAnalysisName")%>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvSRFilesAnalysis" runat="server" ErrorMessage="Files Analysis required."
                                                ValidationGroup="entry" ControlToValidate="cboSRFilesAnalysis" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblParamedicID" runat="server" Text="Physician Name"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="100%" EnableLoadOnDemand="true"
                                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboParamedicID_ItemDataBound"
                                                OnItemsRequested="cboParamedicID_ItemsRequested" Enabled="false">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "ParamedicName")%>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Note : Show max 10 items
                                                </FooterTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="label"></td>
                                        <td>
                                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblLastSubmitDate" runat="server" Text="Last Submit Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtLastSubmitDate" runat="server" Width="100px" Enabled="false" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblLastReturnDate" runat="server" Text="Last Return Date"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadDatePicker ID="txtLastReturnDate" runat="server" Width="100px" Enabled="false" />
                                        </td>
                                        <td width="20px"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="Document Checklist" PageViewID="pgItem" Selected="True">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="History" PageViewID="pgHistory">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" BorderStyle="Solid" BorderColor="Gray">
        <telerik:RadPageView ID="pgItem" runat="server" Selected="true">
            <table width="100%">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table width="100%">
                            <asp:Panel runat="server" ID="pnlSubmitToUnit">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblNotesToUnit" runat="server" Text="Notes to Unit"></asp:Label>
                                    </td>
                                    <td class="entry">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNotesToUnit" runat="server" Width="300px">
                                                    </telerik:RadTextBox>
                                                    &nbsp;
                                                <asp:LinkButton ID="lbtnSubmitToUnit" runat="server"
                                                    OnClientClick="javascript:onSubmitToUnit();return false;">
                                                    <img style="border: 0px; vertical-align: middle;" alt="Submit" src="../../../../Images/arrowright16.png" />
                                                    &nbsp;<asp:Label runat="server" ID="lblSubmit" Text="Submit"></asp:Label>
                                                </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSubmitSuccess" runat="server" Text="** Submit Successfully" ForeColor="Red" Font-Italic="true"></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                        <table></table>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
                <MasterTableView DataKeyNames="DocumentFilesID" CommandItemDisplay="None" ShowHeader="True">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DocumentFilesID" HeaderText="ID"
                            UniqueName="DocumentFilesID" SortExpression="DocumentFilesID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="DocumentNumber" HeaderText="Document Number"
                            UniqueName="DocumentNumber" SortExpression="DocumentNumber" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="480px" DataField="DocumentName" HeaderText="Document Name"
                            UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsComplete" HeaderText="Complete"
                            UniqueName="IsComplete" SortExpression="IsComplete" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsNotApplicable" HeaderText="N/A"
                            UniqueName="IsNotApplicable" SortExpression="IsNotApplicable" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="Notes" HeaderText="Notes To Unit"
                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridTemplateColumn UniqueName="chkIsComplete" HeaderText="Complete">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsComplete") %>'
                                    ID="chkIsComplete" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="chkIsNotApplicable" HeaderText="N/A">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsNotApplicable") %>'
                                    ID="chkIsNotApplicable" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Notes To Unit" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" UniqueName="txtNotes">
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="485px" Text='<%#Eval("Notes")%>' MaxLength="1000" TextMode="MultiLine" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="View" HeaderText="">
                            <ItemTemplate>
                                <%# Eval("SRDocumentFileType").Equals("Assessment") || Eval("SRDocumentFileType").Equals("Report") || Eval("SRDocumentFileType").Equals("Education") || Eval("SRDocumentFileType").Equals("UDDRecon") || Eval("SRDocumentFileType").Equals("Careplan") ?
                                    string.Format("<a href=\"#\" onclick=\"onClientButtonClicking('{0}','{1}','{2}','{3}','{4}','{5}','{6}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print\" /></a>", RegistrationNo, Eval("SRDocumentFileType"), Eval("ProgramID"), Eval("QuestionFormID"),
                                    GetRegistrationInfoMedic(RegistrationNo, Eval("SRAssessmentType").ToString()), GetNursingCareNo(RegistrationNo), Eval("SRHaisMonitoring")) : 
                                    string.Format("<a href=\"#\" onclick=\"onClientButtonClicking('{0}','{1}','{2}','{3}','{4}','{5}','{6}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>", RegistrationNo, Eval("SRDocumentFileType"), Eval("ProgramID"), Eval("QuestionFormID"),
                                    GetRegistrationInfoMedic(RegistrationNo, Eval("SRAssessmentType").ToString()), GetNursingCareNo(RegistrationNo), Eval("SRHaisMonitoring"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="False" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgHistory" runat="server">
            <telerik:RadGrid ID="grdHistory" runat="server" OnNeedDataSource="grdHistory_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdHistory_UpdateCommand"
                OnDeleteCommand="grdHistory_DeleteCommand" OnInsertCommand="grdHistory_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="TxId">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Submit" Name="Submit" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup HeaderText="Return" Name="Return" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="SubmitDate" HeaderText="Date"
                            UniqueName="SubmitDate" SortExpression="SubmitDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Submit" />
                        <telerik:GridBoundColumn HeaderStyle-Width="550px" DataField="SubmitNotes" HeaderText="Notes"
                            UniqueName="SubmitNotes" SortExpression="SubmitNotes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Submit" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="SubmitBy" HeaderText="By"
                            UniqueName="SubmitBy" SortExpression="SubmitBy" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Submit" />

                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ReturnDate" HeaderText="Date"
                            UniqueName="ReturnDate" SortExpression="ReturnDate" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="Return" />
                        <telerik:GridBoundColumn HeaderStyle-Width="550px" DataField="ReturnNotes" HeaderText="Notes"
                            UniqueName="ReturnNotes" SortExpression="ReturnNotes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Return" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ReturnBy" HeaderText="By"
                            UniqueName="ReturnBy" SortExpression="ReturnBy" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" ColumnGroupName="Return" />
                        <telerik:GridTemplateColumn UniqueName="New" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"openWinDetail('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Detail\" /></a>", 
                                    DataBinder.Eval(Container.DataItem, "TxId"))) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="CompletenessAnalysisHistoryItem.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="CompletenessAnalysisHistoryItemEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>

