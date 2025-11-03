<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogList.Master" AutoEventWireup="true"
    CodeBehind="ParamedicConsultHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.ParamedicConsultHist" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphList" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <%=JavascriptOpenPrintPreview()%>
        <script type="text/javascript" language="javascript">
            function printPreviewQuestionForm(tno, regNo, formId) {
                var obj = {};
                obj.transactionNo = tno;
                obj.registrationNo = regNo;
                obj.questionFormID = formId;
                window.openPrintPreview("PopulatePrintQuestionForm", obj);
            }

            function openAnswerEntry(par) { 
                var url = "ParamedicConsultAnswerEntry.aspx?"+ par + "&pgID=<%=PageID %>&ccm=rebind&cet=<%=grdConsultHist.ClientID %>";
               openWind(url, 1000,null);
            }

            function entryPhrFromConsult(md, trno, regno, fid, refno, grdId) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&isletter=0&trno=' + trno + '&regno=' + regno + '&unit=&fid=' + fid + '&menu=su&refno=' + refno + '&ccm=rebind&cet='+grdId;

                openWind(url, 1000, null);
            }

            function openWind(url, width, height) {
                if (height==null)
                 height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight)- 40;

                if (width==null)
                 width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth)- 40;

                window.openWindow(url, "<%= radWinAnswer.ClientID %>", width, height);
            }

            function radWinAnswer_ClientClose(oWnd, args) {
                window.RestoreSizeCurrentWindow();
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

                                if (arg.enableadd !==  null ) {
                                    // Set Enable terblokir ajax jadi diset manual disini
                                    var toolBar = $find("<%=ToolBarMenu.ClientID %>");
                                    var button = toolBar.findItemByText("New");
                                    if (arg.enableadd === 'True')
                                        button.enable();
                                    else 
                                        button.disable();
                                }
                            }
                        }
                    }
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="radWinAnswer" Width="1000px" Height="600px" runat="server" OnClientClose="radWinAnswer_ClientClose"
        ShowContentDuringLoad="false" Behaviors="None" Modal="True" VisibleTitlebar="True" VisibleStatusbar="False">
    </telerik:RadWindow>
    <telerik:RadGrid ID="grdConsultHist" runat="server" OnNeedDataSource="grdConsultHist_NeedDataSource" OnItemDataBound="grdConsultHist_OnItemDataBound"
        AutoGenerateColumns="False" GridLines="None">
        <MasterTableView DataKeyNames="ConsultReferNo,ToParamedicID,IsPhysiotherapy" ShowHeader="True" HierarchyDefaultExpanded="true">
            <NestedViewSettings>
                <ParentTableRelation>
                    <telerik:GridRelationFields DetailKeyField="ConsultReferNo" MasterKeyField="ConsultReferNo" />
                    <telerik:GridRelationFields DetailKeyField="ToParamedicID" MasterKeyField="ToParamedicID" />
                </ParentTableRelation>
            </NestedViewSettings>
            <NestedViewTemplate>
                <div style="padding-left: 20px; padding-bottom: 10px; width: 98%">
                    <telerik:RadTabStrip ID="tabsNoteForm" runat="server" MultiPageID="mpNoteForm" ShowBaseLine="true"
                        Align="Left" PerTabScrolling="True" Width="100%"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Notes and Result" PageViewID="pgNotes"
                                Selected="True" />
                            <telerik:RadTab runat="server" Text="Physiotherapy Result & Form" PageViewID="pgPhysiotherapyForm" />
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="mpNoteForm" runat="server" SelectedIndex="0" ScrollBars="Auto" Width="100%"
                        CssClass="multiPage">

                        <telerik:RadPageView ID="pgNotes" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 33%" valign="top">
                                        <fieldset style="width: 90%">
                                            <legend>Examination</legend>
                                            <telerik:RadTextBox ID="txtActionExamTreatment" runat="server" Text='<%#Eval("ActionExamTreatment")%>' TextMode="MultiLine"
                                                Height="50px" Width="100%" ReadOnly="true" Resize="Vertical" />
                                        </fieldset>

                                    </td>
                                    <td style="width: 20%" valign="top">
                                        <fieldset style="width: 90%">
                                            <legend>Consultation Notes</legend>
                                            <telerik:RadTextBox ID="txtNotes" runat="server" Text='<%#Eval("Notes")%>' Width="100%" Height="50px"
                                                TextMode="MultiLine" ReadOnly="true" Resize="Vertical" />
                                        </fieldset>
                                    </td>
                                    <td valign="top">
                                        <fieldset style="width: 90%" >
                                            <legend>Consultation Result&nbsp;<%#AnswerLink(Container)%></legend>
                                            <telerik:RadTextBox ID="txtAnswer" runat="server" Text='<%#Eval("Answer")%>' Width="100%" Height="50px"
                                                TextMode="MultiLine" ReadOnly="true" Resize="Vertical" />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>

                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pgPhysiotherapyForm" runat="server">
                            <telerik:RadGrid ID="grdPhysiotherapyForm" runat="server" OnDetailTableDataBind="grdPhysiotherapyForm_OnDetailTableDataBind" OnNeedDataSource="grdPhysiotherapyForm_OnNeedDataSource"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="ConsultReferNo, QuestionFormID, ToParamedicID" ShowHeader="True" HierarchyDefaultExpanded="true">
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup HeaderText="Last Created Form" Name="LastForm" HeaderStyle-HorizontalAlign="Center">
                                        </telerik:GridColumnGroup>
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="40px">
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                            <ItemTemplate>
                                                <%# PhysiotherapyFormNewLink(Container) %>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="QuestionFormName" HeaderText="Document" UniqueName="DocumentName"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PhrCreatedByUserID" HeaderText="Created By" UniqueName="PhrCreatedByUserID"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="LastForm" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="PhrRegistrationNo" HeaderText="RegistrationNo" UniqueName="PhrRegistrationNo"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="LastForm" />
                                        <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="PhrCreatedDateTime" HeaderText="Created Time" UniqueName="PhrCreatedDateTime"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="LastForm" />
                                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                                    </Columns>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="TransactionNo" Name="grdPhrHist" Width="100%" GridLines="None" >
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="40px">
                                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# PhysiotherapyFormViewLink(Container) %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Document No" UniqueName="TransactionNo"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="CreatedByUserID" HeaderText="Created By" UniqueName="CreatedByUserID"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="CreatedDateTime" HeaderText="Created Time" UniqueName="CreatedDateTime"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="False">
                                    <Selecting AllowRowSelect="False" />
                                    <Scrolling AllowScroll="False" UseStaticHeaders="False" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </NestedViewTemplate>
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Consult / Refer To" Name="ConsultTo" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Consult / Refer From" Name="ConsultFrom" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%#string.Format("<a style=\"cursor:pointer\" onclick=\"openWindowEntry('mod=view&patid={0}&crno={5}&regno={1}&parid={2}&addable={3}')\"><img src=\"{4}/Images/Toolbar/views16.png\" border=\"0\" alt=\"\"/></a>",PatientID,RegistrationNo,ParamedicID, IsUserAddAble,Helper.UrlRoot(),Eval("ConsultReferNo")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="RegistrationNo" HeaderText="RegistrationNo" UniqueName="RegistrationNo"
                                         SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="ConsultFrom" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="130px" DataField="ConsultDateTime" HeaderText="Date"
                    UniqueName="ConsultDateTime" SortExpression="ConsultDateTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="ConsultFrom" />
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="ParamedicName" HeaderText="From Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="ConsultFrom" />
                <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="ToServiceUnitName" HeaderText="Service Unit" UniqueName="ToServiceUnitName"
                    SortExpression="ToServiceUnitName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="ConsultTo" />
                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="ToParamedicName" HeaderText="Physician" UniqueName="ToParamedicName"
                    SortExpression="ToParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="ConsultTo" />
                <telerik:GridBoundColumn HeaderStyle-Width="140px" DataField="ToRegistrationNo" HeaderText="Refer RegistrationNo" UniqueName="ToRegistrationNo"
                    SortExpression="ToRegistrationNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ColumnGroupName="ConsultTo" />
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
