<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="Prmrj.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.Prmrj" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="radCodeBlock" runat="server">
        <script type="text/javascript" language="javascript">
            function showResumeMedis(regno, patientID, isRichTextMode) {
                var url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisInPatientEntry.aspx?mod=view&editable=false&regno=' + regno + '&fregno=&patid=' + patientID + '&parid=';
                if (isRichTextMode=== true)
                    url = '<%= Helper.UrlRoot() %>/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ResumeMedisRichTextInPatientEntry.aspx?mod=view&editable=false&regno=' + regno + '&fregno=&patid=' + patientID + '&parid=';

                openWindow(url, 1000, 600);
            }
            function openWindow(url, width, height) {
                var oWnd;
                oWnd = radopen(url, 'winDialog');
                oWnd.setSize(width, height);
                oWnd.center();

                // Cek position
                var pos = oWnd.getWindowBounds();
                if (pos.y < 0)
                    oWnd.moveTo(pos.x, 0);
            }
        </script>
    </telerik:RadCodeBlock>
        <fieldset>
        <legend>This patient has been diagnosed with Chronic Disease</legend>
        <asp:Label runat="server" ID="lblChronicDisease" BackColor="Black" ForeColor="Yellow" Font-Size="Large"  Width="100%"></asp:Label>
    </fieldset>
    <div style="height:4px;"></div>
    <telerik:RadWindowManager ID="radWindowManager" runat="server" Style="z-index: 7001"
        Modal="true" VisibleStatusbar="false" DestroyOnClose="false" Behavior="Close,Move"
        ReloadOnShow="True" ShowContentDuringLoad="false">
        <Windows>
            <telerik:RadWindow ID="winDialog" Width="900px" Height="600px" runat="server"
                ShowContentDuringLoad="false" Behaviors="Close,Move" Modal="True">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadGrid ID="grdRegistrationInfoMedic" runat="server" EnableViewState="False" Height="620px"
        OnNeedDataSource="grdRegistrationInfoMedic_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None">
        <MasterTableView DataKeyNames="RegistrationInfoMedicID">
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridBoundColumn DataField="IsNewPatient" UniqueName="IsNewPatient" Display="False"></telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DateTimeInfo" UniqueName="DateTimeInfo" Display="False"></telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Registration" HeaderStyle-Width="250px">
                    <ItemStyle VerticalAlign="Top" />
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "RegInfo")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="" HeaderStyle-Width="450px">
                    <ItemStyle VerticalAlign="Top" />
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "Notes")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Follow Up" HeaderStyle-Width="450px">
                    <ItemStyle VerticalAlign="Top" />
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FollowUp")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
