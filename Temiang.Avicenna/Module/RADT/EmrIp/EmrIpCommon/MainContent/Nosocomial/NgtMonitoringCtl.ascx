<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NgtMonitoringCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NgtMonitoringCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <telerik:RadTextBox runat="server" ID="txtRegistrationNo" Display="False" />
    <telerik:RadTextBox runat="server" ID="txtMonitoringNo" Display="False" />

    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%">
                <table style="width: 100%">
                    <tr>
                        <td class="label">Installation Date</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtInstallationDate" Width="100%" ReadOnly="True"></telerik:RadTextBox>

                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">Installation Room</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtRoomName" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Location</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtLocation" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">NGT No</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtNgtNo" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Installation By</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtInstallationBy" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>

                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%">

                    <tr>
                        <td class="label">Antibiotic</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtAntibiotic" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Other Drugs</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtOtherDrugs" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">Monitoring</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtMonitoring" Width="100%" TextMode="MultiLine" Height="40px" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Release</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtReleaseDate" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Release By</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtReleaseBy" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdNgtMonitoring" runat="server" OnNeedDataSource="grdNgtMonitoring_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdNgtMonitoring_DeleteCommand" Height="560px"
        OnItemCommand="grdNgtMonitoring_ItemCommand" OnItemDataBound="grdNgtMonitoring_ItemDataBound">
        <MasterTableView DataKeyNames="SequenceNo,IsDeleted">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="HAIs Risk" Name="Nosocomial" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%#  ScriptMonitoringEdit(Container,"infus")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="MonitoringDateTime" UniqueName="MonitoringDateTime" HeaderText="Date"
                    HeaderStyle-Width="120px" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="40px" HeaderText="Day" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#  (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "MonitoringDateTime")).Date - InstallationDate.Date).TotalDays.ToInt()%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="SequenceNo" UniqueName="SequenceNo" HeaderText="No"
                    HeaderStyle-Width="60px" Visible="False" />
                <telerik:GridBoundColumn DataField="Replacement" UniqueName="ChangeDescription" HeaderText="Replacement"
                    HeaderStyle-Width="200px" />
                <telerik:GridCheckBoxColumn DataField="IsTempAbove38" UniqueName="IsTempAbove38" HeaderText="Temp >38 C" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsPus" UniqueName="IsPus" HeaderText="Pus (Nose / Throat)" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsPain" UniqueName="IsPain" HeaderText="Sinus Local Pain" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsHeadache" UniqueName="IsHeadache" HeaderText="Headache" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsNoseClogged" UniqueName="IsNoseClogged" HeaderText="Nose Clogged" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsPainSwallow" UniqueName="IsPainSwallow" HeaderText="Pain Swallow" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsPharingRedness" UniqueName="IsPharingRedness" HeaderText="Pharing Redness" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsCough" UniqueName="IsCough" HeaderText="Cough" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsTransillumination" UniqueName="IsTransillumination" HeaderText="Transillumination" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsCulture" UniqueName="IsCulture" HeaderText="Culture" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />

                <telerik:GridBoundColumn DataField="MonitoringByName" UniqueName="MonitoringByName" HeaderText="PPA" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="Note" UniqueName="Note" HeaderText="Note" HeaderStyle-Width="200px" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <%# (!IsMonitoringDeleteable(Container)  
                            ? string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" />",Helper.UrlRoot()) : "")%>
                        <asp:LinkButton ID="lblDelete" runat="server" CommandName="Delete" ToolTip="Delete"
                            Visible='<%#IsMonitoringDeleteable(Container) %>'
                            OnClientClick="javascript: if (!confirm('Delete this record, are you sure ?')) return false;">
                        <img style="border: 0px; vertical-align: middle;" src="<%#Helper.UrlRoot()%>/Images/Toolbar/row_delete16.png" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>

        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</telerik:RadAjaxPanel>
