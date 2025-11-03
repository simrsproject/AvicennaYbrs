<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurgicalHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MainContent.SurgicalHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadScriptBlock ID="scrbPhr" runat="server">
    <script type="text/javascript">
        function entryPhrFromBookingNo(md, id, regno, fid, unit, bookingno, grdId) {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regno + '&unit=' + unit + '&fid=' + fid + '&menu=su' + '&refno=' + bookingno + '&ccm=rebind&cet=<%=grdEpisodeProcedure.ClientID%>';
            window.openWinEntryMaxWindow(url);
        }

        function printPreviewBooking(bookingno) {
            var obj = {};
            obj.bookingNo = bookingno;
            openPrintPreview("PopulatePrintServiceUnitBooking", obj);
        }

        function printPreviewOperatingNotes(bookingno, seqNo) {
            var obj = {};
            obj.bookingNo = bookingno;
            obj.seqNo = seqNo;
            openPrintPreview("PopulatePrintOperatingNotes", obj);
        }

        function printPreviewAnesthesistNotes(bookingno) {
            var obj = {};
            obj.bookingNo = bookingno;
            openPrintPreview("PopulatePrintAnesthesistNotes", obj);
        }

        function entrySurgicalDocument(surgno, regno, patid) {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/PatientDocument/PatientDocumentHist.aspx?patid=' + patid + '&regno=' + regno + '&surgno=' + surgno;
            window.openWinEntryMaxWindow(url);
        }
    </script>
</telerik:RadScriptBlock>
<telerik:RadAjaxManagerProxy ID="ajmProxy" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="grdEpisodeProcedure">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grdEpisodeProcedure" LoadingPanelID="fw_ajxLoadingPanel" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadGrid ID="grdEpisodeProcedure" runat="server" OnNeedDataSource="grdEpisodeProcedure_NeedDataSource" Height="560px"
    AutoGenerateColumns="False" GridLines="None"
    OnDeleteCommand="grdEpisodeProcedure_DeleteCommand" OnItemCommand="grdEpisodeProcedure_OnItemCommand" OnItemDataBound="grdEpisodeProcedure_OnItemDataBound"
    AllowSorting="true">
    <MasterTableView CommandItemDisplay="Top" DataKeyNames="RegistrationNo,SequenceNo,BookingNo" ShowHeader="True" HierarchyDefaultExpanded="true">
        <CommandItemTemplate>
            <div>
                <div class="l">
                    <%# (!IsUserEntrySurgical("add") ? string.Format("<a style='pointer-events: none;cursor: default;color: gray;'><img src=\"{0}/Images/Toolbar/new16_d.png\" />&nbsp;Add Direct Surgical</a>",Helper.UrlRoot()) : string.Format("<a href=\"#\" onclick=\"javascript:entryEpisodeProcedure('new', '{0}', '', ''); return false;\"><img src=\"{1}/Images/Toolbar/new16.png\"  alt=\"New\" />&nbsp;Add Direct Surgical</a>",RegistrationNo,Helper.UrlRoot()))%>
                </div>
                <div class="r" style="color: yellow;">
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="refresh"><img src="../../../Images/Toolbar/refresh16.png" />&nbsp;Refresh&nbsp;</asp:LinkButton>
                </div>
            </div>
        </CommandItemTemplate>
        <CommandItemStyle Height="29px" />
        <NestedViewTemplate>
            <div style="padding-left: 20px; width: 98%">
                <telerik:RadTabStrip ID="tabsEpisodeProcedure" runat="server" MultiPageID="mpEpisodeProcedure" ShowBaseLine="true"
                    Align="Left" PerTabScrolling="True" Width="100%"
                    SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Procedure & Operating Notes" PageViewID="pgProcedure"
                            Selected="True" />
                        <telerik:RadTab runat="server" Text="Anesthetist Notes" PageViewID="pgNotes" />
                        <telerik:RadTab runat="server" Text="Document" PageViewID="pgDocument" />
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="mpEpisodeProcedure" runat="server" SelectedIndex="0" ScrollBars="Auto" Width="100%"
                    CssClass="multiPage">
                    <telerik:RadPageView ID="pgProcedure" runat="server">
                        <telerik:RadGrid ID="grdEpisodeProcedureDetail" runat="server"
                            AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdEpisodeProcedure_DeleteCommand">
                            <MasterTableView DataKeyNames="BookingNo, SequenceNo, OpNotesSeqNo" ShowHeader="True">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%# IsUserEditAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsEditable").Equals(false)? string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" />",Helper.UrlRoot()) : 
                                                    string.Format("<a href=\"#\" onclick=\"javascript:entryEpisodeProcedure('edit', '{0}', '{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/edit16.png\"  /></a>",DataBinder.Eval(Container.DataItem, "RegistrationNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),DataBinder.Eval(Container.DataItem, "BookingNo"), Helper.UrlRoot())%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="colPrintOperatingNotes" HeaderText="" HeaderStyle-Width="35px">
                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <%# string.Format("<a href=\"#\" onclick=\"javascript:printPreviewOperatingNotes('{0}', '{1}'); return false;\"><img src=\"{2}/Images/Toolbar/print16.png\"  /></a>",DataBinder.Eval(Container.DataItem, "BookingNo"), DataBinder.Eval(Container.DataItem, "OpNotesSeqNo"),Helper.UrlRoot())%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                                        SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        AllowSorting="false" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Regio" HeaderText="Regio" UniqueName="Regio"
                                        SortExpression="Regio" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        AllowSorting="false" />
                                    <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                                        SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        AllowSorting="false" HeaderStyle-Width="250px" />
                                    <telerik:GridBoundColumn DataField="OperatingNotes" HeaderText="Operating Notes" UniqueName="OperatingNotes"
                                        SortExpression="OperatingNotes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        AllowSorting="false" />
                                    <telerik:GridBoundColumn DataField="PostSurgeryInstructions" HeaderText="Post Surgery Instructions" UniqueName="PostSurgeryInstructions"
                                        SortExpression="PostSurgeryInstructions" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                        AllowSorting="false" />
                                    <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                                        <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete" Visible="<%#IsUserEditAble.Equals(true)%>"
                                                OnClientClick="javascript: if (!confirm('Delete this record, are you sure ?')) return false;">
                                                    <img style="border: 0px; vertical-align: middle;" src="../../../Images/Toolbar/row_delete16.png" />
                                            </asp:LinkButton>

                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        <br />
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgNotes" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%; vertical-align: top">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 95%; vertical-align: top;">
                                                <b>- Anesthetist Notes -</b>
                                            </td>
                                            <td>
                                                <div class="l">
                                                    <%# string.Format("<a href=\"#\" onclick=\"javascript:printPreviewAnesthesistNotes('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/print16.png\"  /></a>", DataBinder.Eval(Container.DataItem, "BookingNo"), Helper.UrlRoot())%>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: top" colspan="2">
                                                <telerik:RadTextBox ID="txtAnestesyNotes" runat="server" Text='<%#Eval("AnestesyNotes")%>' Width="100%" Height="150px"
                                                    TextMode="MultiLine" ReadOnly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <b>- Post Surgery Instructions -</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: top" colspan="2">
                                                <telerik:RadTextBox ID="txtAnestPostOp" runat="server" Text='<%#Eval("AnestPostSurgeryInstructions")%>' Width="100%" Height="150px"
                                                    TextMode="MultiLine" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 50%; vertical-align: top">
                                    <telerik:RadGrid ID="grdEpisodeProcedureDetailAns" runat="server" Height="150px"
                                        AutoGenerateColumns="False" GridLines="None">
                                        <MasterTableView DataKeyNames="RegistrationNo, SequenceNo" ShowHeader="True">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ProcedureID" HeaderText="Code" UniqueName="ProcedureID"
                                                    SortExpression="ProcedureID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    AllowSorting="false" />
                                                <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name" UniqueName="ProcedureName"
                                                    SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    AllowSorting="false" />
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="False">
                                            <Selecting AllowRowSelect="False" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pgDocument" runat="server">
                        <telerik:RadGrid ID="grdForm" runat="server"
                            AutoGenerateColumns="False" GridLines="None">
                            <MasterTableView DataKeyNames="QuestionFormID" ShowHeader="True">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                                        <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <%# BookingFormLink(Container) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="" HeaderStyle-Width="30px">
                                        <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <%#Eval("TransactionNo")==DBNull.Value || string.IsNullOrEmpty(Eval("TransactionNo").ToString() )? string.Empty:string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm( '{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="RmNO" HeaderText="Form ID" UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-Width="80px">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="QuestionFormName" HeaderText="Form Name" UniqueName="QuestionFormName" SortExpression="QuestionFormName" HeaderStyle-Width="200px">
                                        <ItemTemplate>
                                            <%# Eval("QuestionFormName") %>&nbsp;&nbsp;&nbsp;<%# AddBookingFormLink(Container) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="Document No" HeaderStyle-Width="150px">
                                        <ItemStyle VerticalAlign="Middle"></ItemStyle>
                                        <ItemTemplate>
                                            <%# BookingFormViewLink(Container, "trno") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CreateByUserName" HeaderText="Created By" UniqueName="CreateByUserName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="CreateDateTime" HeaderText="Created Time" UniqueName="CreateDateTime"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ApprovedByUserName" HeaderText="Verified By" UniqueName="ApprovedByUserName"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn DataField="ApprovedDateTime" HeaderText="Verified Time" UniqueName="ApprovedDateTime"
                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />

                                    <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Selecting AllowRowSelect="False" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </NestedViewTemplate>
        <Columns>
            <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                <ItemStyle VerticalAlign="Top"></ItemStyle>
                <ItemTemplate>
                    <%# (!IsUserEntrySurgical("") 
                         || DataBinder.Eval(Container.DataItem, "BookingNo").ToString() == string.Empty 
                         || DataBinder.Eval(Container.DataItem, "IsApproved").Equals(false) 
                         || DataBinder.Eval(Container.DataItem, "IsNew").Equals(false) ? 
                            string.Format("<img src=\"{0}/Images/Toolbar/new16_d.png\" />",Helper.UrlRoot()) : 
                            string.Format("<a href=\"#\" onclick=\"javascript:entryEpisodeProcedure('new', '{0}','','{1}'); return false;\"><img src=\"{2}/Images/Toolbar/new16.png\"  title=\"Create New Operating Notes\" /></a>",DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "BookingNo"),Helper.UrlRoot()))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                AllowSorting="false" />
            <telerik:GridBoundColumn HeaderStyle-Width="30px" DataField="SequenceNo" HeaderText="Seq" UniqueName="SequenceNo"
                SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                AllowSorting="false" Visible="False" />
            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BookingNo" HeaderText="Booking No" UniqueName="BookingNo"
                SortExpression="BookingNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                AllowSorting="false" />
            <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcedureDate" HeaderText="Date"
                UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcedureTime" HeaderText="Time"
                UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-HorizontalAlign="Center" />
            <telerik:GridTemplateColumn DataField="Group" UniqueName="UnitRoom" HeaderText="Service Unit - Room" HeaderStyle-Width="200px" Visible="True">
                <ItemTemplate>
                    <%# string.Format("{0} - {1}",Eval("ServiceUnitName"),Eval("RoomName")) %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="Diagnose" HeaderText="Diagnose" UniqueName="Diagnose" HeaderStyle-Width="200px"
                SortExpression="Diagnose" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                AllowSorting="false" />
            <telerik:GridBoundColumn DataField="PostDiagnosis" HeaderText="Post Diagnose" UniqueName="PostDiagnosis"
                SortExpression="PostDiagnosis" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                AllowSorting="false" />
            <telerik:GridTemplateColumn UniqueName="colSurgicalDocument" HeaderText="" HeaderStyle-Width="35px">
                <ItemStyle VerticalAlign="Top"></ItemStyle>
                <ItemTemplate>
                    <%# string.Format("<a href=\"#\" onclick=\"javascript:entrySurgicalDocument('{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/doc_upload16.png\"  /></a>",DataBinder.Eval(Container.DataItem, "BookingNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "PatientID"), Helper.UrlRoot())%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="colPrint" HeaderText="" HeaderStyle-Width="35px">
                <ItemStyle VerticalAlign="Top"></ItemStyle>
                <ItemTemplate>
                    <%# string.Format("<a href=\"#\" onclick=\"javascript:printPreviewBooking('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/print16.png\"  /></a>",DataBinder.Eval(Container.DataItem, "BookingNo"), Helper.UrlRoot())%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
    </ClientSettings>
</telerik:RadGrid>
