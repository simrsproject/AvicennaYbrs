<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RadiologyResultTest.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.MedicalHistory.RadiologyResultTest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function ZoomViewImage(trno, seqno, no) {
            var url = "<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/EmrCommon/ExamOrderResult/ExamOrderImageZoomView.aspx?trno=" + trno + "&seqno=" + seqno + "&imgno=" + no;
            openWinEntryMaximize(url);
        }
        function openWinEntryMaximize(url) {
            var oWnd;
            url = url + '&rt=<%= Request.QueryString["rt"] %>';
            oWnd = radopen(url, 'winDialog');
            oWnd.maximize();
        }
    </script>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Maximize,Close,Move" Modal="True">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="100%" style="vertical-align: top">
                <telerik:RadGrid ID="grdRadiology" runat="server" OnNeedDataSource="grdRadiology_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" Height="760px" Width="100%" OnItemDataBound="grdRadiology_OnItemDataBound">
                    <MasterTableView DataKeyNames="TransactionNo" CommandItemDisplay="None">
                        <Columns>
                            <telerik:GridDateTimeColumn DataField="TransactionDate" UniqueName="TransactionDate"
                                HeaderText="Date" HeaderStyle-Width="80px" ItemStyle-VerticalAlign="Top" />
                            <telerik:GridTemplateColumn UniqueName="OrderBy" HeaderText="Order" HeaderStyle-Width="250px">
                                <ItemTemplate>
                                    Reg No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                    <br />
                                    Tx No:&nbsp;<%#DataBinder.Eval(Container.DataItem, "TransactionNo")%>
                                    <br />
                                    From:&nbsp;<b><%#DataBinder.Eval(Container.DataItem, "FromServiceUnitName")%></b>
                                    <br />
                                    By:&nbsp;<i> <%#DataBinder.Eval(Container.DataItem, "PhysicianSenders")%></i>
                                    <br />
                                    Order Item:
                            <br />
                                    <div style="padding-left: 10px;">
                                        <%#DataBinder.Eval(Container.DataItem, "JobOrderSummary")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="RadResult" HeaderText="Result">
                                <ItemTemplate>
                                    <telerik:RadGrid ID="grdRadiologyResult" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        OnItemDataBound="grdRadiologyResult_OnItemDataBound" ShowHeader="False" Width="100%">
                                        <MasterTableView DataKeyNames="TransactionNo,SequenceNo">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="LabOrderSummary"
                                                    HeaderText="Exam Name" HeaderStyle-Width="350px">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "TestResult")%>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="" HeaderStyle-Width="700px">
                                                    <ItemTemplate>

                                                        <telerik:RadListView ID="lvItemDocumentImage" runat="server" RenderMode="Lightweight" Width="100%" PageSize="10" AllowCustomPaging="True"
                                                            ItemPlaceholderID="ImageContainer">
                                                            <LayoutTemplate>
                                                                <div style="height: 180px; overflow: auto;">
                                                                    <asp:PlaceHolder ID="ImageContainer" runat="server"></asp:PlaceHolder>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <div style="width: 250px; text-align: center;">
                                                                    <asp:LinkButton ID="lbtnDocumentImage" runat="server" ToolTip="Zoom"
                                                                        OnClientClick='<%#string.Format("javascript:ZoomViewImage(\"{0}\",\"{1}\",{2});return false;",DataBinder.Eval(Container.DataItem, "TransactionNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"),DataBinder.Eval(Container.DataItem, "ImageNo"))%>'>
                                                                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server"
                                                                            Width="125px" Height="125px" ResizeMode="Fit" DataValue='<%# Eval("DocumentImage") == DBNull.Value? new System.Byte[0]: Eval("DocumentImage") %>'></telerik:RadBinaryImage>
                                                                    </asp:LinkButton>
                                                                    <br />
                                                                    <%#DataBinder.Eval(Container.DataItem, "DocumentName")%>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:RadListView>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="False">
                                            <Selecting AllowRowSelect="False" />
                                            <Scrolling UseStaticHeaders="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top"></ItemStyle>
                            </telerik:GridTemplateColumn>
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
</asp:Content>
