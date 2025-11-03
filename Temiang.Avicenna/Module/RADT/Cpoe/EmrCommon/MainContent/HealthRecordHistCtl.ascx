<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HealthRecordHistCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MainContent.HealthRecordHistCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<telerik:RadScriptBlock ID="scrbPhr" runat="server">
    <script type="text/javascript">

        function tbarPhr_OnClientButtonClicking(sender, args) {
            var val = args.get_item().get_value();
            if (val === 'refresh') {
                var grdPhr = $find('<%=grdPhr.ClientID %>').get_masterTableView();
                grdPhr.rebind();
            } else {
                var fid = val.split('_')[1];
                entryPhr('new', '','<%= RegistrationNo %>', fid, '<%= ServiceUnitID %>');
            }
        }
        function entryPhr(md, id, regno, fid, unit) {
            var url = '<%= Helper.UrlRoot() %>/Module/RADT/Cpoe/Common/Phr/PatientHealthRecordDetail.aspx?md=' + md + '&id=' + id + '&regno=' + regno + '&patid=<%= PatientID %>&unit=' + unit + '&fid=' + fid + '&menu=su' + '&refno=&ccm=rebind&cet=<%=grdPhr.ClientID %>';
            window.openWinEntryMaxWindow(url);
        }

        function cboPhrMenuAdd_ClientItemsRequesting(sender, eventArgs) {
            var context = eventArgs.get_context();

            context["ServiceUnitID"] = "<%=ServiceUnitID %>";
            context["RegistrationNo"] = "<%=RegistrationNo %>";
            context["IsFirstRegInServiceUnit"] = "<%=IsFirstRegistrationInServiceUnit.ToString().ToLower() %>";
            context["UserType"] = "<%=AppSession.UserLogin.SRUserType%>";
        }

        function onPhrMenuAddClick(fid) {
            entryPhr('new', '','<%= RegistrationNo %>', fid, '<%= ServiceUnitID %>');
        }

    </script>
</telerik:RadScriptBlock>

<telerik:RadToolBar ID="tbarPhr" runat="server" Width="100%" EnableEmbeddedScripts="false"
    OnClientButtonClicking="tbarPhr_OnClientButtonClicking">
    <CollapseAnimation Duration="200" Type="OutQuint" />
    <Items>
        <telerik:RadToolBarButton>
            <ItemTemplate>
                <div style="padding-left: 20px">
                    <telerik:RadComboBox ID="cboPhrMenuAdd" runat="server" Width="500px" EmptyMessage="Search here for New Health Record"
                        OnClientItemsRequesting="cboPhrMenuAdd_ClientItemsRequesting"
                        EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                        <WebServiceSettings Method="PhrMenuAdd" Path="~/Module/RADT/Cpoe/EmrWebService.asmx" />
                        <ClientItemTemplate>
                    <div onclick="onPhrMenuAddClick('#= Value #')" style="cursor: pointer;">
                        #= Text #
                    </div>
                        </ClientItemTemplate>
                    </telerik:RadComboBox>
                </div>
            </ItemTemplate>
        </telerik:RadToolBarButton>
        <telerik:RadToolBarButton ID="tbiRefresh" runat="server" Text="Refresh" Value="refresh"
            ImageUrl="~/Images/Toolbar/refresh16.png" />
    </Items>
</telerik:RadToolBar>
<telerik:RadGrid ID="grdPhr" runat="server" OnNeedDataSource="grdPhr_NeedDataSource" AllowSorting="true" Height="560px"
    EnableLinqExpressions="false">
    <MasterTableView DataKeyNames="QuestionFormID" ClientDataKeyNames="QuestionFormID" AllowMultiColumnSorting="true" AllowFilteringByColumn="True" AutoGenerateColumns="False">
        <Columns>
            <telerik:GridTemplateColumn UniqueName="editPhr" HeaderStyle-Width="35px" ItemStyle-HorizontalAlign="center" AllowFiltering="False">
                <ItemTemplate>
                    <%# PhrEditLink(Container)%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="colMenu" HeaderText="" HeaderStyle-Width="30px" AllowFiltering="False">
                <ItemStyle VerticalAlign="Middle"></ItemStyle>
                <ItemTemplate>
                    <%#string.Format("<a href=\"#\" onclick=\"printPreviewQuestionForm( '{0}','{1}','{2}'); return false;\"><img src=\"../../../Images/Toolbar/print16.png\" border=\"0\" /></a>", Eval("TransactionNo"), Eval("RegistrationNo"), Eval("QuestionFormID"))%>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn UniqueName="TransactionNo" HeaderText="Document No" HeaderStyle-Width="120px">
                <ItemStyle VerticalAlign="Middle"></ItemStyle>
                <ItemTemplate>
                    <%# PhrViewLink(Container) %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="RmNO" HeaderText="Form ID" UniqueName="RmNO" SortExpression="RmNO" HeaderStyle-Width="80px">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn DataField="QuestionFormName" HeaderText="Form Name" UniqueName="QuestionFormName" SortExpression="QuestionFormName">
                <ItemTemplate>
                    <%# Eval("QuestionFormName") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
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

        </Columns>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="False">
        <Selecting AllowRowSelect="False" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" />

    </ClientSettings>
</telerik:RadGrid>