<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientLetterHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PatientLetterHist" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Telerik.Web.UI.Skins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%=JavascriptOpenPrintPreview()%>

    <telerik:RadWindow ID="winPhr" Width="1000px" Height="600px" runat="server" OnClientClose="radWindowManager_ClientClose"
        ShowContentDuringLoad="false" Behaviors="None" Modal="True" VisibleTitlebar="False" VisibleStatusbar="False">
    </telerik:RadWindow>

    <telerik:RadToolBar ID="tbarPhr" runat="server" Width="100%" EnableEmbeddedScripts="false"
        OnClientButtonClicking="tbarPhr_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarDropDown ID="tbiAdd" runat="server" Text="Add" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton ID="tbiRefresh" runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
            <telerik:RadToolBarButton runat="server" Text="Close" Value="close" ImageUrl="~/Images/Toolbar/close16.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadGrid ID="grdPhr" runat="server" OnNeedDataSource="grdPhr_NeedDataSource" AllowSorting="true"
        EnableLinqExpressions="false">
        <MasterTableView DataKeyNames="QuestionFormID" ClientDataKeyNames="QuestionFormID"
            AllowPaging="False" AutoGenerateColumns="False">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="editPhr" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# PatientLetterEditLink(Container)%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px">
                    <ItemStyle VerticalAlign="Middle"></ItemStyle>
                    <ItemTemplate>
                        <%#string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm( '{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/print16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"), Helper.UrlRoot())%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Document No" UniqueName="TransactionNo">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"entryPatientLetter('view', '{0}','{1}','{2}'); return false;\">{0}</a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                    SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="130px" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="UserName" HeaderText="Create By" UniqueName="UserName"
                    SortExpression="UserName">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Form"
                    UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                    <HeaderStyle HorizontalAlign="Left" Width="350px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="RecordDate" HeaderText="Record" UniqueName="RecordDate"
                    SortExpression="RecordDate">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>

                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="False" UseStaticHeaders="False" />
        </ClientSettings>
    </telerik:RadGrid>

    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">

            function printPreviewQuestionForm(tno, regNo, formId) {
                var obj = {};
                obj.transactionNo = tno;
                obj.registrationNo = regNo;
                obj.questionFormID = formId;
                openPrintPreview("PopulatePrintQuestionForm", obj);
            }

            function entryPatientLetter(md, id, regno, fid) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&isletter=1&id=' + id + '&regno=' + regno + '&unit=&fid=' + fid + '&menu=su' + '&bookingno=&ccm=rebind&cet=<%=grdPhr.ClientID %>';
                openWinEntryMaxWindow(url, "<%= winPhr.ClientID %>");
            }

            function openWinEntryMaxWindow(url, windowid) {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                var width =
                    (window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth);

                if (!(url.includes("&rt=") || url.includes("?rt=")))	
                    url = url + '&rt=<%= Request.QueryString["rt"] %>';

                var oWnd = window.$find(windowid);
                oWnd.setUrl(url);
                oWnd.setSize(width-60, height-60);
                oWnd.show();
                oWnd.center();
            }


            function tbarPhr_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                if (val === 'refresh') {
                    var grdPhr = $find('<%=grdPhr.ClientID %>').get_masterTableView();
                    grdPhr.rebind();
                } else if (val === 'close') {
                    Close();
                    return false;
                } else {
                    var fid = val.split('_')[1];
                    entryPatientLetter('new', '', '<%= RegistrationNo %>', fid, '');
                }
        }


        function radWindowManager_ClientClose(oWnd, args) {
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
    </telerik:RadCodeBlock>

</asp:Content>
