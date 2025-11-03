<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BedRestMonitoringCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.BedRestMonitoringCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <telerik:RadTextBox runat="server" ID="txtRegistrationNo" Display="False"/>
    <telerik:RadTextBox runat="server" ID="txtMonitoringNo" Display="False"/>

    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%">
                <table style="width: 100%">
                    <tr>
                        <td class="label">Start Monitoring</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtInstallationDate" Width="100%" ReadOnly="True"></telerik:RadTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Decubitus from</td>
                        <td>
                            <asp:RadioButtonList ID="optDecubitusFrom" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="None" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Room" Value="R"></asp:ListItem>
                                <asp:ListItem Text="Hospital" Value="H"></asp:ListItem>
                                <asp:ListItem Text="Home" Value="M"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label"></td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtDecubitusFrom" Width="100%" ReadOnly="True"></telerik:RadTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Date found decubitus</td>
                        <td class="entry">
                            <telerik:RadTextBox runat="server" ID="txtDecubitusDate" Width="100%" ReadOnly="True"></telerik:RadTextBox>
                        </td>
                        <td></td>
                    </tr>

                </table>

            </td>
            <td style="width: 50%; vertical-align: top;">
                <table style="width: 100%">
                    <tr>
                        <td class="label">Location of injury decubitus</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtLocation" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">The state of injury when it comes</td>
                        <td>
                            <telerik:RadTextBox runat="server" ID="txtProblem" Width="100%" ReadOnly="True"></telerik:RadTextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Stop</td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 140px;">
                                        <telerik:RadTextBox runat="server" ID="txtReleaseDate" Width="140px" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td class="label" style="width: 70px;">Release By</td>
                                    <td style="width: 150px;">
                                        <telerik:RadTextBox runat="server" ID="txtReleaseBy" Width="150px" ReadOnly="True"></telerik:RadTextBox></td>
                                    <td></td>
                                </tr>
                            </table>

                        </td>
                        <td></td>
                    </tr>
                </table>

            </td>
        </tr>

    </table>

    <telerik:RadGrid ID="grdBedRestMonitoring" runat="server" OnNeedDataSource="grdBedRestMonitoring_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdBedRestMonitoring_DeleteCommand" Height="560px"
        OnItemCommand="grdBedRestMonitoring_ItemCommand" OnItemDataBound="grdBedRestMonitoring_ItemDataBound">
        <MasterTableView DataKeyNames="SequenceNo,IsDeleted">
            <ColumnGroups>
                <telerik:GridColumnGroup HeaderText="Skin Condition" Name="Skin" HeaderStyle-HorizontalAlign="Center">
                </telerik:GridColumnGroup>
                <telerik:GridColumnGroup HeaderText="Injury Condition" Name="Injury" HeaderStyle-HorizontalAlign="Center">
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

                <telerik:GridBoundColumn DataField="Mobilization" UniqueName="Mobilization" HeaderText="Mobilization" HeaderStyle-Width="150px" />
                <telerik:GridBoundColumn DataField="Fisiotherapi" UniqueName="Fisiotherapi" HeaderText="Fisiotherapi" HeaderStyle-Width="150px" />
                <telerik:GridCheckBoxColumn DataField="IsInjuryCare" UniqueName="IsInjuryCare" HeaderText="Injury Care" HeaderStyle-Width="60px"/>

                <telerik:GridBoundColumn DataField="SkinCondition" UniqueName="SkinCondition" HeaderText="Skin Condition" HeaderStyle-Width="150px"  ColumnGroupName="Skin" Visible="False"/>
                <telerik:GridCheckBoxColumn DataField="IsSkinRed" UniqueName="IsSkinRed" HeaderText="Red" HeaderStyle-Width="60px" ColumnGroupName="Skin"/>
                <telerik:GridCheckBoxColumn DataField="IsSkinComplete" UniqueName="IsSkinComplete" HeaderText="Complete" HeaderStyle-Width="60px" ColumnGroupName="Skin"/>
                <telerik:GridCheckBoxColumn DataField="IsSkinNoBlister" UniqueName="IsSkinNoBlister" HeaderText="No Blister" HeaderStyle-Width="60px" ColumnGroupName="Skin"/>
                <telerik:GridCheckBoxColumn DataField="IsSkinWarm" UniqueName="IsSkinWarm" HeaderText="Warm" HeaderStyle-Width="60px" ColumnGroupName="Skin"/>
                <telerik:GridCheckBoxColumn DataField="IsSkinHard" UniqueName="IsSkinHard" HeaderText="Hard" HeaderStyle-Width="60px" ColumnGroupName="Skin"/>
                <telerik:GridCheckBoxColumn DataField="IsSkinItchy" UniqueName="IsSkinItchy" HeaderText="Itchy/ Pain" HeaderStyle-Width="60px" ColumnGroupName="Skin"/>

                <telerik:GridBoundColumn DataField="InjuryCondition" UniqueName="InjuryCondition" HeaderText="Injury Condition" HeaderStyle-Width="150px" ColumnGroupName="Injury" Visible="False"/>
                <telerik:GridCheckBoxColumn DataField="IsInjuryBlister" UniqueName="IsInjuryBlister" HeaderText="Blister" HeaderStyle-Width="60px"  ColumnGroupName="Injury"/>
                <telerik:GridCheckBoxColumn DataField="IsInjuryOpen" UniqueName="IsInjuryOpen" HeaderText="Open" HeaderStyle-Width="60px"  ColumnGroupName="Injury"/>
                <telerik:GridCheckBoxColumn DataField="IsInjuryToFat" UniqueName="IsInjuryToFat" HeaderText="To Fat" HeaderStyle-Width="60px"  ColumnGroupName="Injury"/>
                <telerik:GridCheckBoxColumn DataField="IsInjuryNekrosis" UniqueName="IsInjuryNekrosis" HeaderText="Nekrotik" HeaderStyle-Width="60px"  ColumnGroupName="Injury"/>
                <telerik:GridCheckBoxColumn DataField="IsInjuryToBone" UniqueName="IsInjuryToBone" HeaderText="To Bone & Muscle" HeaderStyle-Width="80px"  ColumnGroupName="Injury"/>
                <telerik:GridCheckBoxColumn DataField="IsCulture" UniqueName="IsCulture" HeaderText="Culture" HeaderStyle-Width="60px"  />
                <telerik:GridCheckBoxColumn DataField="IsDxDekubitus" UniqueName="IsDxDekubitus" HeaderText="Dx Dekubitus" HeaderStyle-Width="80px"  />

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
