<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="PatientIncidentInvestigationDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.QualityIndicator.PatientIncidentInvestigationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td colspan="4">
                            &nbsp;<b>Patient Identity</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientIncident" runat="server" Text="Incident No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientIncidentNo" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvPatientIncident" runat="server" ErrorMessage="Incident No required."
                                ValidationGroup="entry" ControlToValidate="txtPatientIncidentNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="RegistrationNo"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboRegistrationNo" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemsRequested="cboRegistrationNo_ItemsRequested"
                                OnItemDataBound="cboRegistrationNo_ItemDataBound" Enabled="False">
                                <ItemTemplate>
                                    <b>
                                        <%# DataBinder.Eval(Container.DataItem, "RegistrationNo")%>
                                    </b>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "PatientName")%>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalNo") %>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PatientID") %>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "ServiceUnitName")%>
                                    <br />
                                    <%# DataBinder.Eval(Container.DataItem, "BedID")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 5 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Nama Patient"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="100"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Medical Record"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" MaxLength="50"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInitialName" runat="server" Text="Initial Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInitialName" runat="server" Width="300px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Age / Sex"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtAge" runat="server" Width="100px" MaxLength="100" ReadOnly="True" />
                            <telerik:RadTextBox ID="txtSex" runat="server" Width="50px" MaxLength="100" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" Enabled="False" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Service Unit / Bed"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboRoomID" runat="server" Width="200px" Enabled="False" />
                            <telerik:RadComboBox ID="cboBedID" runat="server" Width="100px" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td>
                            &nbsp;<b>Incident Information</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentLocation" runat="server" Text="Incident Location"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentLocation" runat="server" Width="300px" MaxLength="100"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Incident Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtIncidentDate" runat="server" Width="100px" Enabled="False">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtIncidentTime" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Reporting Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtReportingDate" runat="server" Width="100px" Enabled="false">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtReportingTime" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Investigation Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnitRelatedUnitID" runat="server" Width="300px"
                                Enabled="False" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%" border="0">
                    <tr>
                        <td colspan="4">
                            &nbsp;<b>Incident Detail Information</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentName" runat="server" Text="Incident"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentName" runat="server" Width="100%" MaxLength="200"
                                TextMode="MultiLine" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblChronology" runat="server" Text="Chronology"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtChronology" runat="server" Width="100%" MaxLength="500"
                                TextMode="MultiLine" Height="100px" ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsApproved" Text="Approved" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td colspan="4">
                            &nbsp;<b>Incident Investigation</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentChronologyCauses" runat="server" Text="Chronology Of The Cause"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentChronologyCauses" runat="server" Width="100%" MaxLength="5000"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblIncidentDirectCause" runat="server" Text="Direct Cause"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentDirectCause" runat="server" Width="100%" MaxLength="200"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvIncidentDirectCause" runat="server" ErrorMessage="Direct Cause required."
                                ValidationGroup="entry" ControlToValidate="txtIncidentDirectCause" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblIncidentUnderlyingCauses" runat="server" Text="Underlying Causes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtIncidentUnderlyingCauses" runat="server" Width="100%"
                                MaxLength="500" TextMode="MultiLine" Height="100px" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblInvestigation" runat="server" Text="Investigated By"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtInvestigationBy" runat="server" Width="300px" MaxLength="100"
                                ReadOnly="True" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Investigation Date Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtInvestigationDate" runat="server" Width="100px">
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtInvestigationTime" runat="server" TimeView-Interval="00:30"
                                            TimeView-TimeFormat="HH:mm" DateInput-DateFormat="HH:mm" DateInput-DisplayDateFormat="HH:mm"
                                            TimeView-Columns="4" TimeView-StartTime="07:00" TimeView-EndTime="22:00" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="rfvInvestigationDateTime" runat="server" ErrorMessage="Investigation date required."
                                ValidationGroup="entry" ControlToValidate="txtInvestigationDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabStrip" runat="server" MultiPageID="multiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Underlying Causes" PageViewID="pgvUnderlyingCauses"
                Selected="true" />
            <telerik:RadTab runat="server" Text="Recomendation" PageViewID="pgvInvestigation" />
            <telerik:RadTab runat="server" Text="Investigation Units" PageViewID="pgvRelatedUnit" />
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="multiPage" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvUnderlyingCauses" runat="server">
            <telerik:RadGrid ID="grdUnderlyingCauses" runat="server" OnNeedDataSource="grdUnderlyingCauses_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdUnderlyingCauses_UpdateCommand"
                OnDeleteCommand="grdUnderlyingCauses_DeleteCommand" OnInsertCommand="grdUnderlyingCauses_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="FactorID,FactorItemID,ComponentID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="FactorName" HeaderText="Factor" UniqueName="FactorName"
                            SortExpression="IncidentType">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FactorItemName" HeaderText="Factor Item" UniqueName="FactorItemName"
                            SortExpression="FactorItemName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Component" HeaderText="Component" UniqueName="Component"
                            SortExpression="Component">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ComponentName" HeaderText="Description" UniqueName="ComponentName"
                            SortExpression="ComponentName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PatientIncidentUnderlyingCausesItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="pgvUnderlyingCausesEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvInvestigation" runat="server">
            <telerik:RadGrid ID="grdInvestigation" runat="server" OnNeedDataSource="grdInvestigation_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdInvestigation_UpdateCommand"
                OnDeleteCommand="grdInvestigation_DeleteCommand" OnInsertCommand="grdInvestigation_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="SeqNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SeqNo" HeaderText="SeqNo"
                            UniqueName="SeqNo" SortExpression="SeqNo" Visible="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Recomendation" HeaderText="Recomendation" UniqueName="Recomendation"
                            SortExpression="Recomendation">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="RecomendationDateTime"
                            HeaderText="Recomendation Date" UniqueName="RecomendationDateTime" SortExpression="RecomendationDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PersonInCharge" HeaderText="Person In Charge"
                            UniqueName="PersonInCharge" SortExpression="PersonInCharge">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Implementation" HeaderText="Implementation" UniqueName="Implementation"
                            SortExpression="Implementation">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="ImplementationDateTime"
                            HeaderText="Implementation Date" UniqueName="ImplementationDateTime" SortExpression="ImplementationDateTime"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="PatientIncidentInvestigationItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="grdInvestigartionEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRelatedUnit" runat="server">
            <telerik:RadGrid ID="grdRelatedUnit" runat="server" OnNeedDataSource="grdRelatedUnit_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" >
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ServiceUnitID">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ServiceUnitID" HeaderText="ID"
                            UniqueName="ServiceUnitID" SortExpression="ServiceUnitID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceUnitName" HeaderText="Service Unit" UniqueName="ServiceUnitID"
                            SortExpression="ServiceUnitID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="True">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
