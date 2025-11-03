<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="CompletenessAnalysisDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.CompletenessAnalysisDialog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <%=JavascriptOpenPrintPreview()%>
        <script type="text/javascript" language="javascript">
            function printPreviewQuestionForm(tno, regNo, formId) {
                var obj = {};
                obj.transactionNo = tno;
                obj.registrationNo = regNo;
                obj.questionFormID = formId;
                openPrintPreview("PopulatePrintQuestionForm", obj);
            }

            function printPreviewBooking(bookingno) {
                var obj = {};
                obj.bookingNo = bookingno;
                openPrintPreview("PopulatePrintServiceUnitBooking", obj);
            }

            function printPreviewAnesthesistNotes(bookingno) {
                var obj = {};
                obj.bookingNo = bookingno;
                openPrintPreview("PopulatePrintAnesthesistNotes", obj);
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
        ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
    </telerik:RadWindow>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" ShowBaseLine="true"
        Orientation="HorizontalTop">
        <Tabs>
            <telerik:RadTab runat="server" Text="PatientHealthRecord" PageViewID="pgPHR" Visible="false"/>
            <telerik:RadTab runat="server" Text="Surgical & Anesthetist" PageViewID="pgSurgicalAnesthesi" Visible="false"/>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server">
        <telerik:RadPageView ID="pgPHR" runat="server" Selected="true">
            <telerik:RadGrid ID="grdPHR" runat="server" OnNeedDataSource="grdPHR_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
                <MasterTableView DataKeyNames="TransactionNo, RegistrationNo, QuestionFormID" CommandItemDisplay="None" ShowHeader="True">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px" AllowFiltering="False">
                            <ItemStyle VerticalAlign="Middle"></ItemStyle>
                            <ItemTemplate>
                                <%#string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/print16.png\" border=\"0\" title=\"Print\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TransactionNo" HeaderText="Transaction No"
                            UniqueName="TransactionNo" SortExpression="TransactionNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No"
                            UniqueName="RegistrationNo" SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionFormID" HeaderText="Form ID"
                            UniqueName="QuestionFormID" SortExpression="QuestionFormID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="QuestionFormName" HeaderText="Form Name"
                            UniqueName="QuestionFormName" SortExpression="QuestionFormName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ReferenceNo" HeaderText="Ref No" UniqueName="ReferenceNo"
                            SortExpression="ReferenceNo" AllowFiltering="False">
                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="RecordDateTime" HeaderText="Create Date" UniqueName="RecordDateTime"
                            SortExpression="RecordDateTime" AllowFiltering="False">
                            <HeaderStyle HorizontalAlign="Center" Width="110px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="CreatedByUserName" HeaderText="Create By" UniqueName="CreatedByUserName"
                            SortExpression="CreatedByUserName">
                            <HeaderStyle HorizontalAlign="Center" Width="150px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName">
                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="false">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="False" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView runat="server" ID="pgSurgicalAnesthesi">
            <telerik:RadGrid ID="grdEpisodeProcedure" runat="server" OnNeedDataSource="grdEpisodeProcedure_NeedDataSource" Height="560px" AutoGenerateColumns="False" GridLines="None" AllowSorting="true">
            <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo,BookingNo">
                <Columns>
                    <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                        SortExpression="RegistrationNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                        AllowSorting="false" />
                    <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="BookingNo" HeaderText="Booking No" UniqueName="BookingNo"
                        SortExpression="BookingNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                        AllowSorting="false" />
                    <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ProcedureDate" HeaderText="Date"
                        UniqueName="ProcedureDate" SortExpression="ProcedureDate" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ProcedureTime" HeaderText="Time"
                        UniqueName="ProcedureTime" SortExpression="ProcedureTime" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn DataField="Group" UniqueName="UnitRoom" HeaderText="Service Unit - Room" HeaderStyle-Width="230px">
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
                    <telerik:GridTemplateColumn UniqueName="colPrintAnesthesi" HeaderText="" HeaderStyle-Width="35px">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%# string.Format("<a href=\"#\" onclick=\"javascript:printPreviewAnesthesistNotes('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/print16.png\" title=\"Print Anesthetist\" /></a>", DataBinder.Eval(Container.DataItem, "BookingNo"), Helper.UrlRoot())%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="colPrintSurgical" HeaderText="" HeaderStyle-Width="35px">
                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <%# string.Format("<a href=\"#\" onclick=\"javascript:printPreviewBooking('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/print16.png\" title=\"Print Surgical\" /></a>",DataBinder.Eval(Container.DataItem, "BookingNo"), Helper.UrlRoot())%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="False">
                <Selecting AllowRowSelect="False" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
