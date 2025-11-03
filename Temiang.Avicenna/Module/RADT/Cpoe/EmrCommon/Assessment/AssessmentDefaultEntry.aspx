<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="AssessmentDefaultEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentDefaultEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/EpisodeDiagnoseCtl.ascx" TagPrefix="uc1" TagName="EpisodeDiagnoseCtl" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function openVitalSignChart(vitalSignID) {

            var url = '../../Common/VitalSignChart.aspx?patid=<%= PatientID %>&regno=<%= RegistrationNo %>&vid=' + vitalSignID;
            openWinEntryMaxWindow(url);
        }

        function entryLocalistStatus(mod, rimid, bodyId, parid, unit) {
            var url = '<%=Helper.UrlRoot()%>/Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx?mod=' + mod + '&parid=' + parid + '&patid=<%= PatientID %>&rimid=' + rimid + '&regno=<%= RegistrationNo %>&unit=' + unit + '&bodyId=' + bodyId + '&ccm=rebind&cet=<%=lvLocalistStatus.ClientID %>';
            window.openWinEntryMaxWindow(url);
        }

        function openWinEntryMaxWindow(url) {
            var height =
                (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

            var width =
                (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

            if (!(url.includes("&rt=") || url.includes("?rt=")))	
                url = url + '&rt=<%= Request.QueryString["rt"] %>';

            openWindow(url, width - 40, height - 40);
        }

        function openWindow(url, width, height) {
            var oWnd = $find('<%=winDialog.ClientID %>');
            oWnd.setUrl(url);
            oWnd.setSize(width, height);
            oWnd.show();
            oWnd.center();

            // Cek position
            var pos = oWnd.getWindowBounds();
            if (pos.y < 0)
                oWnd.moveTo(pos.x, 0);
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

    </script>
    <telerik:RadWindow ID="winDialog" Width="700px" Height="300px" runat="server" Modal="True" Behaviors="None" VisibleTitlebar="False" VisibleStatusbar="False" OnClientClose="winDialog_ClientClose">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="hdnServiceUnitID" />
    <asp:HiddenField runat="server" ID="hdnRegistrationInfoMedicID" />

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 55%">
                <table width="100%">
                    <tr>
                        <td class="label">Physican
                        </td>
                        <td >
                            <table width="100%">
                                <tr>

                                    <td>
                                        <telerik:RadTextBox runat="server" Width="99%" ID="txtSoapParamedicName" ReadOnly="True" />
                                    </td>
                                    <td style="width: 100px">
                                        <telerik:RadDatePicker ID="txtDateSOAP" runat="server" Width="100px" />
                                    </td>
                                    <td style="width: 93px">
                                        <telerik:RadTimePicker ID="txtTimeSOAP" runat="server" Width="93px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>

                    <tr>
                        <td class="label">Subjective
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSubjective" runat="server" Width="99%" TextMode="MultiLine" Height="40px" Resize="Vertical" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Objective
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtObjective" runat="server" Width="99%" TextMode="MultiLine" Height="100px" Resize="Vertical" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Assessment / Diagnosis
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtAssesment" runat="server" Width="99%" TextMode="MultiLine" Height="40px" Resize="Vertical" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">ICD 10
                        </td>
                        <td>
                            <uc1:EpisodeDiagnoseCtl runat="server" id="epDiagCtl" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Planning
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPlanning" runat="server" Width="99%" TextMode="MultiLine" Height="100px" Resize="Vertical" />
                        </td>
                    </tr>
                    <tr runat="server" id="trPpaInstruction">
                        <td class="label">PPA Instruction
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPpaInstruction" runat="server" Width="99%" TextMode="MultiLine" Height="60px" Resize="Vertical" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Notes
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtAttendingNotes" runat="server" Width="99%" TextMode="MultiLine" Height="40px" Resize="Vertical" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Chronic Disease</td>
                        <td>
                            <asp:Label runat="server" ID="lblChronicDisease" BackColor="Yellow" Width="100%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <asp:CheckBox ID="chkIsInformConcern" runat="server" Text="Inform Concern" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
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
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <fieldset>
                    <legend><b>VITAL SIGN</b></legend>
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
                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Answer" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <span>
                                            <%#DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted")%></span>
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
                <asp:Panel ID="pnlReferAnswer" runat="server">
                </asp:Panel>
            </td>
        </tr>

    </table>
</asp:Content>
