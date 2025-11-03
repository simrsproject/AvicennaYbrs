<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="PatientEducationHist.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.PatientEducationHist" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="cdBlock">
        <style>
            #educationLine {
                font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
                border-collapse: collapse;
                width: 100%;
            }

                #educationLine td, #educationLine th {
                    border: 1px solid #a9a9a9;
                    padding: 4px;
                }

                #educationLine tr:nth-child(even) {
                    background-color: #f2f2f2;
                }

                #educationLine tr:hover {
                    background-color: #ddd;
                }

                #educationLine th {
                    padding-top: 6px;
                    padding-bottom: 6px;
                    text-align: center;
                    background-color: #4CAF50;
                    color: white;
                }

            .ColumnSign {
                float: left;
                width: 50%;
            }
            /* Clear floats after the columns */
            .RowSign:after {
                content: "";
                display: table;
                clear: both;
            }
        </style>
        <%=JavascriptOpenPrintPreview()%>
        <script type="text/javascript" language="javascript">
            function printPreviewPatientEducation(regNo, seqNo) {
                var parVal = "RegistrationNo:" + regNo + ";SeqNo:" + seqNo;
                openGeneralPrintPreview("<%=AppConstant.Report.CetakanPasienEdukasi%>", parVal); // deklarasinya ada di JavascriptOpenPrintPreview

            }
            function openWinEntryNoTitleBarMaximize(url) {
                var oWnd;
                url = url + '&rt=<%= Request.QueryString["rt"] %>';
                oWnd = radopen(url, 'winNoTitlebar');
                oWnd.maximize();
            }
            function entryPatientEducation(mod,regno, sno) {
                if (regno === '')
                    regno = '<%= RegistrationNo %>';
                var url = 'PatientEducationDetail.aspx?md=' + mod + '&sno=' + sno + '&regno=' + regno + '&patid=<%= PatientID %>&ccm=rebind&cet=<%=grdPatientEducationHist.ClientID %>';;
                openWinEntryNoTitleBarMaximize(url);
            }
            function tbar_OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                if (val === 'refresh') {
                    var grd = $find('<%=grdPatientEducationHist.ClientID %>').get_masterTableView();
                    grd.rebind();
                } else if (val === 'add') {
                    entryPatientEducation('new', '');
                    args.set_cancel(true);
                } else if (val === 'close') {
                    Close();
                    return false;
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

            function applyGridHeightMax() {
                var height =
                    (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

                // set height to the whole RadGrid control
                var grid = $find("<%= grdPatientEducationHist.ClientID %>");
                grid.get_element().style.height = height - 50 + "px";
                grid.repaint();
            }
            window.onload = function () {
                applyGridHeightMax();
            }
            window.onresize = function () {
                applyGridHeightMax();
            }

            // After postback
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                applyGridHeightMax();
            });
        </script>
    </telerik:RadCodeBlock>


    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false" OnClientClose="radWindowManager_ClientClose">
        <Windows>
            <telerik:RadWindow ID="winNoTitlebar" runat="server" Behaviors="None"
                ShowContentDuringLoad="false" Modal="True">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadToolBar ID="tbar" runat="server" Width="100%" EnableEmbeddedScripts="false"
        OnClientButtonClicking="tbar_OnClientButtonClicking">
        <CollapseAnimation Duration="200" Type="OutQuint" />
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Add" Value="add" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Refresh" Value="refresh"
                ImageUrl="~/Images/Toolbar/refresh16.png" />
            <telerik:RadToolBarButton runat="server" Text="Close" Value="close" ImageUrl="~/Images/Toolbar/close16.png" />
        </Items>
    </telerik:RadToolBar>



    <telerik:RadGrid ID="grdPatientEducationHist" runat="server" OnNeedDataSource="grdPatientEducationHist_NeedDataSource"
        AutoGenerateColumns="False">
        <MasterTableView DataKeyNames="SeqNo, RegistrationNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#string.Format("<a style=\"cursor:pointer;\" onclick=\"entryPatientEducation('view', '{0}', '{1}')\"><img src=\"../../../../../Images/Toolbar/views16.png\" border=\"0\" alt=\"\"/></a>",Eval("RegistrationNo"), Eval("SeqNo"))%><br />
                        <hr />
                        <%#string.Format("<a style=\"cursor:pointer;\" onclick=\"printPreviewPatientEducation('{0}', '{1}')\"><img src=\"../../../../../Images/Toolbar/print16.png\" border=\"0\" alt=\"\"/></a>",Eval("RegistrationNo"), Eval("SeqNo"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" UniqueName="RegistrationNo" HeaderText="Registration No" HeaderStyle-Width="130px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridTemplateColumn UniqueName="SeqNo" HeaderText="No" HeaderStyle-Width="50px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%#string.Format("{0:00000}", DataBinder.Eval(Container.DataItem, "SeqNo")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="EducationDateTime" UniqueName="EducationDateTime" HeaderText="Date Time" HeaderStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridTemplateColumn UniqueName="Educator" HeaderText="Educator & Verificator" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "EducationByUserName")%>&nbsp;(<%# DataBinder.Eval(Container.DataItem, "SRUserTypeName")%>)<br />
                        <br />
                        <%# DataBinder.Eval(Container.DataItem, "VerifyByUserName")==DBNull.Value ? 
                                string.Empty: 
                                string.Format("<img src=\"../../../../../Images/checklist16.png\" border=\"0\" alt=\"\"/>&nbsp;Verified By:<br />{0}<br />{1}", DataBinder.Eval(Container.DataItem,"VerifyByUserName"),Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "VerifyDateTime")).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="Education" HeaderText="Education" HeaderStyle-Width="400px">
                    <ItemTemplate>
                        <%#PatientEducationLineHtml(Container) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SRPatientEducationProblemName" UniqueName="SRPatientEducationProblemName" HeaderText="Problem" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridBoundColumn DataField="SRPatientEducationMethodName" UniqueName="SRPatientEducationMethodName" HeaderText="Method" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top" />

                <telerik:GridTemplateColumn UniqueName="Recipient" HeaderText="Recipient" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "RecipientName")%><br />
                        (<%# DataBinder.Eval(Container.DataItem, "SRPatientEducationRecipientName")%>)
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SRPatientEducationEvaluationName" UniqueName="SRPatientEducationEvaluationName" HeaderText="Evaluation" HeaderStyle-Width="200px" ItemStyle-VerticalAlign="Top" />
                <telerik:GridBoundColumn DataField="Duration" UniqueName="Duration" HeaderText="Duration" HeaderStyle-Width="100px" ItemStyle-VerticalAlign="Top" />

                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
