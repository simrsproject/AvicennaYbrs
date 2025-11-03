<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="IntegratedNoteEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.IntegratedNoteEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/RegInMedDiagnoseCtl.ascx" TagPrefix="uc1" TagName="RegInMedDiagnoseCtl" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/IntegratedNote/EpisodeProcedureCtl.ascx" TagPrefix="uc1" TagName="EpisodeProcedureCtl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript">

            function openVitalSignChart(vitalSignID) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/VitalSignChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&vid=' + vitalSignID;
                openWinDialog(url);
            }

            function entryLocalistStatus(mod, rimid, bodyId, parid, unit) {
                var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx?mod=' + mod + '&parid=' + parid + '&patid=<%= PatientID %>&rimid=' + rimid + '&regno=<%= RegistrationNo %>&unit=' + unit + '&bodyId=' + bodyId + '&ccm=rebind&cet=<%=lvLocalistStatus.ClientID %>';
                openWinDialog(url);
            }

            function openWinDialog(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) - 40;

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth) - 40;

                var oWnd = $find("<%= winDialog.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.show();
            }

            function showRaspraja() {
                openWinDialog("<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/EmrCommon/Medication/Raspro/RasprajaEntry.aspx?mod=new&rsno=&patid=<%= PatientID %>&regno=<%= RegistrationNo %>&seqno=", "");
            }

            function winDialog_ClientClose(oWnd, args) {
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

            function openWinLookUpSO(url) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight) - 40;

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth) - 40;

                var oWnd = $find("<%= winLookUpSO.ClientID %>");
                oWnd.setUrl(url);
                oWnd.setSize(width, height);
                oWnd.show();
            }

            function openLookUpS() {
                var url = "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardPickImplementation.aspx?regno=<%= RegistrationNo %>&isS=true&isO=false";
                openWinLookUpSO(url);
            }
            function openLookUpO() {
                var url = "<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/MainContent/NursingCare/NursingCareStandardPickImplementation.aspx?regno=<%= RegistrationNo %>&isS=false&isO=true";
                openWinLookUpSO(url);
            }

            function winLookUpSO_OnClientClose(oWnd, args) {
                if (oWnd.argument == undefined || oWnd.argument == null) {
                } else {
                    if (oWnd.argument.result == undefined || oWnd.argument.result == null) {
                    } else if (oWnd.argument.result == 'OK') {
                        if (oWnd.argument.isS === "True")
                            $find('<%=txtInfo1.ClientID %>').set_value(decodeURI(oWnd.argument.dataDS));
                        if (oWnd.argument.isO === "True")
                            $find('<%=txtInfo2.ClientID %>').set_value(decodeURI(oWnd.argument.dataDO));
                    }
                }
            }
            function fixDangText(sender, eventArgs) {
                var s = sender.get_value();

                //s = s.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot');
                s = s.replace(/</g, '< ').replace(/>/g, '> ').replace(/  /g, ' ');
                sender.set_value(s);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server" Style="z-index: 7001" VisibleStatusbar="False" Behaviors="Maximize,Close,Move"
        ShowContentDuringLoad="false" Modal="True" OnClientClose="winDialog_ClientClose">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winLookUpSO" Width="900px" Height="600px" runat="server" Style="z-index: 7001" VisibleStatusbar="False" Behaviors="Maximize,Close,Move"
        ShowContentDuringLoad="false" Modal="True" OnClientClose="winLookUpSO_OnClientClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnServiceUnitID" />
    <asp:HiddenField runat="server" ID="hdnRegistrationInfoMedicID" />

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 600px" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">Time
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 100px">
                                        <telerik:RadDatePicker ID="txtDateSOAP" runat="server" Width="100px" AutoPostBack="True" OnSelectedDateChanged="txtDateSOAP_OnSelectedDateChanged" />
                                    </td>
                                    <td style="width: 93px">
                                        <telerik:RadTimePicker ID="txtTimeSOAP" runat="server" Width="93px" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInputType" runat="server" Text="Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboInputType" runat="server" AutoPostBack="true" Width="100%"
                                OnSelectedIndexChanged="cboInputType_OnSelectedIndexChanged" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicID" runat="server" Width="100%" />
                        </td>
                        <td width="20px"></td>
                    </tr>

                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInfo1" runat="server" Text="Info 1" />&nbsp;&nbsp;<telerik:RadCodeBlock ID="radCodeBlock1" runat="server"><%=LookUpSoLink("S") %></telerik:RadCodeBlock>
                            <asp:LinkButton runat="server" ID="lbtnResetHistoryOfPresentIllness" OnClick="lbtnResetHistoryOfPresentIllness_OnClick" OnClientClick="if (!confirm('Reset this History of Present Illness')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                            </asp:LinkButton>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo1" runat="server" Width="150%" Height="150px" MaxLength="4000"
                                TextMode="MultiLine" Resize="Vertical" ValidateRequestMode="Disabled" />
                        </td>


                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="row2">
                        <td class="label">
                            <asp:Label ID="lblInfo2" runat="server" Text="Info 2"></asp:Label>&nbsp;&nbsp;<telerik:RadCodeBlock ID="radCodeBlock2" runat="server"><%=LookUpSoLink("O") %></telerik:RadCodeBlock>
                            <asp:LinkButton runat="server" ID="LinkButton3" OnClick="lbtnResetObjective_OnClick" OnClientClick="if (!confirm('Reset this Objective')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                            </asp:LinkButton>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo2" runat="server" Width="150%" Height="150px" MaxLength="4000"
                                TextMode="MultiLine" Resize="Vertical" ValidateRequestMode="Disabled" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="row3">
                        <td class="label">
                            <asp:Label ID="lblInfo3" runat="server" Text="Info 3"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo3" runat="server" Width="150%" Height="150px" MaxLength="4000"
                                TextMode="MultiLine" Resize="Vertical" ValidateRequestMode="Disabled" Visible="false"/>
                        </td>
                        <td width="20px"></td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" width="130%">
                    <tr runat="server" id="trIcd10">
                        <td class="label" style="width: 30px;">I<br />
                            C<br />
                            D<br />
                            <br />
                            X
                        </td>
                        <td>
                            <uc1:RegInMedDiagnoseCtl runat="server" ID="workDiagCtl" />
                            <div style="height: 5px;">&nbsp;</div>
                        </td>
                    </tr>
                    <tr runat="server" id="trIcd9cm">
                        <td class="label" style="width: 30px;">I<br />
                            C<br />
                            D<br />
                            <br />
                            9<br />
                            C<br />
                            M
                        </td>
                        <td>
                            <uc1:EpisodeProcedureCtl runat="server" ID="epProcedureCtl" />
                        </td>
                    </tr>
                </table>

                <table width="100%">
                    <tr runat="server" id="row4">
                        <td class="label">
                            <asp:Label ID="lblInfo4" runat="server" Text="Info 4"></asp:Label>
                            <asp:LinkButton runat="server" ID="LinkButton2" OnClick="lbtnResetPlanning_OnClick" OnClientClick="if (!confirm('Reset this Planning')) return false;">
                    <img src="../../../../../Images/Toolbar/refresh16.png"/>
                            </asp:LinkButton>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo4" runat="server" Width="150%" Height="150px" MaxLength="4000"
                                TextMode="MultiLine" Resize="Vertical" ValidateRequestMode="Disabled" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="trPpaInstruction">
                        <td class="label">
                            <asp:Label ID="lblPpaInstruction" runat="server" Text="Ppa Instruction"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPpaInstruction" runat="server" Width="150%" Height="150px" TextMode="MultiLine" Resize="Vertical" ValidateRequestMode="Disabled" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr runat="server" id="row5">
                        <td class="label">
                            <asp:Label ID="lblInfo5" runat="server" Text="Info 5"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInfo5" runat="server" Width="150%" Height="150px" MaxLength="4000"
                                TextMode="MultiLine" Resize="Vertical" ValidateRequestMode="Disabled" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr id="trReceiveBy" runat="server">
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Receive By"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtReceiveBy" runat="server" Width="400px"
                                TextMode="SingleLine" />
                        </td>
                        <td width="25px">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset>
                                <legend>Current Day Prescription</legend>
                                <telerik:RadTextBox ID="txtPrescriptionCurrentDay" runat="server" Width="100%" Height="150px" TextMode="MultiLine" Resize="Vertical" ReadOnly="True" ValidateRequestMode="Disabled" />
                            </fieldset>
                        </td>
                    </tr>


                </table>
            </td>
            <td width="250px"></td>
            <td style="width: 600px;" valign="top">
                <fieldset>
                    <legend>VITAL SIGN</legend>
                    <telerik:RadGrid ID="grdVitalSign" runat="server" OnNeedDataSource="grdVitalSign_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="VitalSignID" ShowHeader="False">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Question">
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                            <%#DataBinder.Eval(Container.DataItem, "VitalSignName")%>
                                        </a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="80px"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="50px"></telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Answer" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <span>
                                            <%# string.Format("<div style='background-color: {0};width:100%;padding-left: 2px'>{1}</div>",DataBinder.Eval(Container.DataItem, "EwsLevelColor"),DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted"))%>
                                        </span>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="">
                                    <HeaderStyle Width="30px"></HeaderStyle>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" onclick='<%# string.Format("javascript:openVitalSignChart(\"{0}\")", DataBinder.Eval(Container.DataItem, "VitalSignID")) %>'>
                                            <img src='../../../../../Images/Toolbar/barchart.bmp' alt='chart' /></a>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Selecting AllowRowSelect="false" />
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
                <br />
                <telerik:RadAjaxPanel runat="server" id="curbPanel">
                <fieldset>
                    <legend>CURB 65 SCORE</legend>
                    <telerik:RadGrid ID="grdCurb" runat="server" OnNeedDataSource="grdCurb_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="VitalSignID" ShowHeader="False">
                            <Columns>
                                <telerik:GridBoundColumn DataField="VitalSignName" UniqueName="TemplateItemName1" HeaderText="Question" HeaderStyle-Width="100px"></telerik:GridBoundColumn>

                                <telerik:GridDateTimeColumn DataField="RecordDate" UniqueName="RecordDate" HeaderText="Date" HeaderStyle-Width="30px"></telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="RecordTime" UniqueName="RecordTime" HeaderText="Time" HeaderStyle-Width="20px"></telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="QuestionAnswerFormatted" HeaderText="Answer" UniqueName="QuestionAnswerFormatted"
                                HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Selecting AllowRowSelect="false" />
                            <Resizing AllowColumnResize="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
                <br />
                </telerik:RadAjaxPanel>
                <telerik:RadListView ID="lvLocalistStatus" runat="server" RenderMode="Lightweight"
                    ItemPlaceholderID="BodyImageContainer" OnNeedDataSource="lvLocalistStatus_NeedDataSource">
                    <LayoutTemplate>
                        <fieldset>
                            <legend>LOCALIST STATUS</legend>
                            <asp:PlaceHolder ID="BodyImageContainer" runat="server"></asp:PlaceHolder>
                        </fieldset>

                    </LayoutTemplate>
                    <ItemTemplate>
                        <fieldset style="height: 100px;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Edit"
                                            OnClientClick='<%# string.Format("entryLocalistStatus(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\");return false;", 
                                                        DataBinder.Eval(Container.DataItem, "EntryMode"),        
                                                        RegistrationInfoMedicID,DataBinder.Eval(Container.DataItem, "BodyID"),
                                                        ParamedicID,
                                                        ServiceUnitID)%>'>
                                            <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                                Width="90px" Height="90px" ResizeMode="Fit" DataValue='<%# Eval("BodyImage") == DBNull.Value? new System.Byte[0]: Eval("BodyImage") %>'></telerik:RadBinaryImage>
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="2"><%#DataBinder.Eval(Container.DataItem, "BodyName")%></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50px;">Add:</td>
                                                <td><%#string.Format("{0}",Eval("CreatedDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("CreatedDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                                            </tr>
                                            <tr>
                                                <td>Upd:</td>
                                                <td><%#string.Format("{0}",Eval("LastUpdateDateTime") == DBNull.Value? string.Empty:  Convert.ToDateTime(Eval("LastUpdateDateTime")).ToString(AppConstant.DisplayFormat.Date))%></td>
                                            </tr>
                                        </table>
                                    </td>

                                </tr>
                            </table>

                        </fieldset>
                        <br />
                    </ItemTemplate>
                </telerik:RadListView>

                <br />
                <fieldset>
                    <legend>&nbsp;&nbsp;Used Antibiotics&nbsp;&nbsp;<telerik:RadLinkButton runat="server" ID="lnkRaspraja" Text="Create RASPRAJA" Icon-Url="~/Images/Toolbar/ordering16.png" ToolTip="Formulir Raspro Antibiotik Berkepanjangan" OnClientClicked="showRaspraja"></telerik:RadLinkButton>
                        &nbsp;&nbsp;</legend>
                    <telerik:RadGrid ID="grdAntibioticItem" Width="100%" runat="server" OnNeedDataSource="grdAntibioticItem_NeedDataSource"
                        OnItemDataBound="grdAntibioticItem_ItemDataBound"
                        AutoGenerateColumns="False" GridLines="None"
                        ShowHeader="true" AllowMultiRowEdit="false">
                        <MasterTableView DataKeyNames="ItemID,ZatActiveID">
                            <CommandItemStyle Height="29px" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Product" UniqueName="ItemName"
                                    HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="ZatActiveName" HeaderText="Zat Active" UniqueName="ZatActiveName"
                                    HeaderStyle-Width="180px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn HeaderText="Consume Method" UniqueName="SRConsumeMethodName" HeaderStyle-Width="120px"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# string.Format("{0} @{1} {2}", DataBinder.Eval(Container.DataItem, "SRConsumeMethodName"),  DataBinder.Eval(Container.DataItem, "ConsumeQty"),   DataBinder.Eval(Container.DataItem, "SRConsumeUnit")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ConsumeDayNo" HeaderText="Day" UniqueName="ConsumeDayNo"
                                    HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Resizing AllowColumnResize="false" />
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </fieldset>
            </td>
            <td></td>
        </tr>
    </table>
    <asp:Panel ID="pnlReferAnswer" runat="server"></asp:Panel>

</asp:Content>
