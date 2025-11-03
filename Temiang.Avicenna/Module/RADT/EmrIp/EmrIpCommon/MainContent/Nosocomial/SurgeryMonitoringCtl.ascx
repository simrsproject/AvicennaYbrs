<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurgeryMonitoringCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.SurgeryMonitoringCtl" %>
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
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Operating Room Booking No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtReferenceNo" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Surgery Date</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtSurgeryDate" Width="100%" ReadOnly="True"></telerik:RadTextBox>
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Surgery By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSurgeryByName" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Wound Classification"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtWoundClassification" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="ASA"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAsaScore" runat="server" Width="100%" ReadOnly="True" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Surgery Location</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtLocation" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
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
                </table>

            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdSurgeryMonitoring" runat="server" OnNeedDataSource="grdSurgeryMonitoring_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdSurgeryMonitoring_DeleteCommand" Height="560px"
        OnItemCommand="grdSurgeryMonitoring_ItemCommand" OnItemDataBound="grdSurgeryMonitoring_ItemDataBound">
        <MasterTableView DataKeyNames="SequenceNo,IsDeleted">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Replacement" Name="Replacement" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
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
                <telerik:GridBoundColumn DataField="InjuryCondition" UniqueName="InjuryCondition" HeaderText="Injury Condition" ColumnGroupName="Replacement"
                    HeaderStyle-Width="150px" />
                <telerik:GridBoundColumn DataField="SRExudateCharacterName" UniqueName="SRExudateCharacter" HeaderText="Exudate Character" ColumnGroupName="Replacement"
                    HeaderStyle-Width="100px" />
                <telerik:GridCheckBoxColumn DataField="IsAfDrain" UniqueName="IsAfDrain" HeaderText="Af Drain" ColumnGroupName="Replacement"
                    HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsAfSuture" UniqueName="IsAfSuture" HeaderText="Af Suture" ColumnGroupName="Replacement"
                    HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsRedness" UniqueName="IsRedness" HeaderText="Redness" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsSwollen" UniqueName="IsSwollen" HeaderText="Swollen" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsPain" UniqueName="IsPain" HeaderText="Suppressed Pain" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsFeelingHot" UniqueName="IsFeelingHot" HeaderText="Feeling Hot (local)" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsTempAbove38" UniqueName="IsTempAbove38" HeaderText="Fever >38 C" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsPus" UniqueName="IsPus" HeaderText="Pus" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsCulture" UniqueName="IsCulture" HeaderText="Culture" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsIdoDiagnose" UniqueName="IsIdoDiagnose" HeaderText="IDO Diag." ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsGlukosa" UniqueName="IsGlukosa" HeaderText="Abnormally Glucose" ColumnGroupName="Nosocomial" HeaderStyle-Width="90px" />


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
