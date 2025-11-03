<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EttMonitoringCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.EttMonitoringCtl" %>
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

                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtInstallationDate" Width="140px" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td class="label" style="width: 90px;">Installation By</td>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtInstallationBy" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                                </tr>
                            </table>
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
                        <td class="label">ETT Type</td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtSREttTypeName" Width="100px" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td class="label" style="width: 50px;">ETT No</td>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtTubeNo" Width="40px" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td class="label" style="width: 70px;">ETT Position</td>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtLocation" Width="100px" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td></td>
                                </tr>
                            </table>

                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Reason
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtProblem" runat="server" Width="100%" MaxLength="200" />
                        </td>
                        <td width="20px"></td>
                    </tr>
                    <tr>
                        <td class="label">Release</td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtReleaseDate" Width="140px" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td class="label" style="width: 70px;">Release By</td>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtReleaseBy" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                                </tr>
                            </table>

                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Preparation"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 100px;">- Body Weight</td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txtBodyWeight" Width="100px">
                                                        <NumberFormat DecimalDigits="2" PositivePattern="n kg" />
                                                    </telerik:RadNumericTextBox></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>- Ventilator Mode</td>
                                                <td>
                                                    <telerik:RadTextBox runat="server" ID="txtVentilatorMode" MaxLength="200" Width="100px" /></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>- Tidal Volume</td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txtTidalVolume" Width="100px">
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>- Respiration Rate</td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txtRespirationRate" NumberFormat-DecimalDigits="0" Width="100px"></telerik:RadNumericTextBox></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 100px;">- FiO2</td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txtFiO2" NumberFormat-DecimalDigits="0" Width="100px"></telerik:RadNumericTextBox></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>- PEEP</td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txtPeep" NumberFormat-DecimalDigits="0" Width="100px"></telerik:RadNumericTextBox></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>- Peak Flow</td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txtPeakFlow" NumberFormat-DecimalDigits="0" Width="100px"></telerik:RadNumericTextBox></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>- Sensitivity</td>
                                                <td>
                                                    <telerik:RadTextBox runat="server" ID="txtSensitivity" MaxLength="200" Width="100px" /></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>

                        </td>
                        <td></td>
                    </tr>

                </table>

            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdEttMonitoring" runat="server" OnNeedDataSource="grdEttMonitoring_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdEttMonitoring_DeleteCommand" Height="560px"
        OnItemCommand="grdEttMonitoring_ItemCommand" OnItemDataBound="grdEttMonitoring_ItemDataBound">
        <MasterTableView DataKeyNames="SequenceNo,IsDeleted">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Replacement" Name="Replacement" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridColumnGroup HeaderText="Changed" Name="Changed" HeaderStyle-HorizontalAlign="Center" />
                <telerik:GridColumnGroup HeaderText="HAIs Risk" Name="Nosocomial" HeaderStyle-HorizontalAlign="Center" />
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
                <telerik:GridCheckBoxColumn DataField="IsElbowConnectorRepl" UniqueName="IsElbowConnectorRepl" HeaderText="Elbow Con." HeaderStyle-Width="60px" ColumnGroupName="Replacement" />
                <telerik:GridCheckBoxColumn DataField="IsHumidificationRepl" UniqueName="IsHumidificationRepl" HeaderText="Humidi- fication" HeaderStyle-Width="60px" ColumnGroupName="Replacement" />
                <telerik:GridCheckBoxColumn DataField="IsGuedeleRepl" UniqueName="IsGuedeleRepl" HeaderText="Guedele" HeaderStyle-Width="60px" ColumnGroupName="Replacement" />
                <telerik:GridCheckBoxColumn DataField="IsTidalVolChange" UniqueName="IsTidalVolChange" HeaderText="Tidal Vol" HeaderStyle-Width="60px" ColumnGroupName="Changed" />
                <telerik:GridCheckBoxColumn DataField="IsRrChange" UniqueName="IsRrChange" HeaderText="RR" HeaderStyle-Width="60px" ColumnGroupName="Changed" />
                <telerik:GridCheckBoxColumn DataField="IsModeVentChange" UniqueName="IsModeVentChange" HeaderText="Mode Vent" HeaderStyle-Width="60px" ColumnGroupName="Changed" />

                <telerik:GridBoundColumn DataField="SREttTypeName" UniqueName="SREttTypeName" HeaderText="Replacement" HeaderStyle-Width="100px" Display="False" />

                <telerik:GridCheckBoxColumn DataField="IsTempAbove38" UniqueName="IsTempAbove38" HeaderText="Temp >38 C" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsBradikardi" UniqueName="IsBradikardi" HeaderText="Brad/ Tach" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsDispenea" UniqueName="IsDispenea" HeaderText="Dispen/ Tach" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsSpO2LessThan94" UniqueName="IsSpO2LessThan94" HeaderText="SpO2 <94" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsWetRonchi" UniqueName="IsWetRonchi" HeaderText="Wet Ronchi" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" Display="False" />
                <telerik:GridCheckBoxColumn DataField="IsSputum" UniqueName="IsSputum" HeaderText="New / changes in sputum" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridBoundColumn DataField="SputumColor" UniqueName="SputumColor" HeaderText="Sputum Color" HeaderStyle-Width="100px" ColumnGroupName="Nosocomial" />
                <telerik:GridCheckBoxColumn DataField="IsCough" UniqueName="IsCough" HeaderText="Cough that worsens" ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsLeukositosis" UniqueName="IsLeukositosis" HeaderText="Leukopeni/ Leukositosis" ColumnGroupName="Nosocomial" HeaderStyle-Width="80px" />
                <telerik:GridBoundColumn DataField="Thorax" UniqueName="Thorax" HeaderText="Thorax" HeaderStyle-Width="100px" ColumnGroupName="Nosocomial" />
                <telerik:GridCheckBoxColumn DataField="IsVapDiagnose" UniqueName="IsVapDiagnose" HeaderText="VAP Diag." ColumnGroupName="Nosocomial" HeaderStyle-Width="60px" />
                <telerik:GridCheckBoxColumn DataField="IsDipsnoe" UniqueName="IsDipsnoe" HeaderText="Dips/Tach" ColumnGroupName="Nosocomial" HeaderStyle-Width="100px" Display="False" />
                <telerik:GridCheckBoxColumn DataField="IsDesaturasi" UniqueName="IsDesaturasi" HeaderText="Desaturation O2 (PaO2/FiO2 <= 240)" ColumnGroupName="Nosocomial" HeaderStyle-Width="100px" Display="False" />
                <telerik:GridCheckBoxColumn DataField="IsCulture" UniqueName="IsCulture" HeaderText="Culture" HeaderStyle-Width="80px" Display="False" />
                <telerik:GridCheckBoxColumn DataField="IsRadiology" UniqueName="IsRadiology" HeaderText="Radiology" HeaderStyle-Width="80px" Display="False" />

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
