<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfusMonitoringCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.InfusMonitoringCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<telerik:RadAjaxPanel runat="server">
    <telerik:RadTextBox runat="server" ID="txtRegistrationNo" Display="False"/>
    <telerik:RadTextBox runat="server" ID="txtMonitoringNo" Display="False"/>
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
                        <td class="label">Chateter Type / Set Infus </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td><telerik:RadTextBox runat="server" ID="txtChateterType" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td>&nbsp;</td>
                                    <td><telerik:RadTextBox runat="server" ID="txtSetInfus" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td>&nbsp;</td>
                                    <td><asp:CheckBox runat="server" ID="chkIsSetBlood" Text="Set Blood" /></td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Infus Location</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtLocation" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
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
                        <td class="label">Fluid Type</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtTypeOfInfus" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
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
                    <tr>
                        <td class="label">Problem</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtProblem" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
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

    <telerik:RadGrid ID="grdInfusMonitoring" runat="server" OnNeedDataSource="grdInfusMonitoring_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdInfusMonitoring_DeleteCommand" Height="560px"
        OnItemCommand="grdInfusMonitoring_ItemCommand" OnItemDataBound="grdInfusMonitoring_ItemDataBound">
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
                <telerik:GridBoundColumn DataField="SRIVCatheterName" UniqueName="SRIVCatheter" HeaderText="Chateter Type" ColumnGroupName="Replacement"
                    HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="SRInfusSetName" UniqueName="SRInfusSet" HeaderText="Set Infus" ColumnGroupName="Replacement"
                    HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsSetBlood" UniqueName="IsSetBlood" HeaderText="Set Blood" ColumnGroupName="Replacement"
                    HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="LiquidType" UniqueName="LiquidType" HeaderText="Liquid Type" ColumnGroupName="Replacement" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="MedicineAndLiquid" UniqueName="MedicineAndLiquid" HeaderText="Medicine" ColumnGroupName="Replacement" HeaderStyle-Width="100px" />

                <telerik:GridBoundColumn DataField="SRInfusLocationName" UniqueName="SRInfusLocation" HeaderText="Location"
                    HeaderStyle-Width="120px" Visible="False" />
                <telerik:GridBoundColumn DataField="InfusLocation" UniqueName="InfusLocation" HeaderText="Location"
                    HeaderStyle-Width="200px" />
                <telerik:GridCheckBoxColumn DataField="IsTempAbove38" UniqueName="IsTempAbove38" HeaderText="T>38 /<=35C" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsApneu" UniqueName="IsApneu" HeaderText="Apneu/ Asw" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsRedness" UniqueName="IsRedness" HeaderText="Redness" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />

                <telerik:GridCheckBoxColumn DataField="IsPain" UniqueName="IsPain" HeaderText="Pain" ColumnGroupName="Nosocomial" HeaderStyle-Width="50px" />
                <telerik:GridCheckBoxColumn DataField="IsFeelingHot" UniqueName="IsFeelingHot" HeaderText="Hot" ColumnGroupName="Nosocomial" HeaderStyle-Width="50px"  Visible="False" />
                <telerik:GridCheckBoxColumn DataField="IsSwollen" UniqueName="IsSwollen" HeaderText="Swollen" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />

                <telerik:GridCheckBoxColumn DataField="IsVeinHarden" UniqueName="IsVeinHarden" HeaderText="Vein Harden" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsPus" UniqueName="IsPus" HeaderText="Pus" ColumnGroupName="Nosocomial" HeaderStyle-Width="50px" />
                <telerik:GridCheckBoxColumn DataField="IsDirty" UniqueName="IsDirty" HeaderText="Dirty" ColumnGroupName="Nosocomial" HeaderStyle-Width="50px" Visible="False" />
                <telerik:GridCheckBoxColumn DataField="IsKanulaCulture" UniqueName="IsKanulaCulture" HeaderText="Kanula Culture" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsShivers" UniqueName="IsSwollen" HeaderText="Shivers" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                                <telerik:GridCheckBoxColumn DataField="IsInfected" UniqueName="IsInfected" HeaderText="Infected" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="MonitoringByName" UniqueName="MonitoringByName" HeaderText="PPA" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="MedicineAndLiquid" UniqueName="MedicineAndLiquid" HeaderText="Medicine / Liquid" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="MedicationMethod" UniqueName="MedicationMethod" HeaderText="Medication Method" HeaderStyle-Width="100px" />
                <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" HeaderText="Notes" HeaderStyle-Width="200px" />
                <telerik:GridDateTimeColumn DataField="ReleaseDateTime" UniqueName="ReleaseDateTime" HeaderText="Release"
                    HeaderStyle-Width="120px" />
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
