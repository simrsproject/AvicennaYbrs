<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="EmployeeTrainingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.EmployeeTrainingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">

            function openItemComp(pid,type) {
                var oWnd = $find("<%= winItemDialog.ClientID %>");
                var tId = $find("<%= txtTrainingID.ClientID %>");

                oWnd.SetUrl("EmployeeTrainingDetailItemComp.aspx?tId=" + tId.get_value() + "&pId=" + pid + '&type=' + type);
                oWnd.Show();
                //oWnd.Maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winItemDialog" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="33%" valign="top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">Training ID
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTrainingID" runat="server" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr runat="server" id="trReferenceID">
                        <td class="label">
                            <asp:Label ID="lbTrainingProposalID" runat="server" Text="Proposal ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboReferenceID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboReferenceID_ItemDataBound"
                                OnItemsRequested="cboReferenceID_ItemsRequested" AutoPostBack="True" OnSelectedIndexChanged="cboReferenceID_SelectedIndexChanged">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeTrainingName")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "TrainingLocation")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Training Name
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTrainingName" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvTrainingName" runat="server" ErrorMessage="Training Name required."
                                ValidationGroup="entry" ControlToValidate="txtTrainingName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRActivityType" runat="server" Text="Training Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRActivityType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" AutoPostBack="true" OnSelectedIndexChanged="cboSRActivityType_SelectedIndexChanged" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvSRActivityType" runat="server" ErrorMessage="Training Type required."
                                ValidationGroup="entry" ControlToValidate="cboSRActivityType" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRActivitySubType" runat="server" Text="Training Sub Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRActivitySubType" runat="server" Width="300px" EnableLoadOnDemand="true"
                                HighlightTemplatedItems="true" MarkFirstMatch="true" OnItemDataBound="cboSRActivitySubType_ItemDataBound"
                                OnItemsRequested="cboSRActivitySubType_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Training Location
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTrainingLocation" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Training Organizer
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtTrainingOrganizer" runat="server" Width="300px" MaxLength="255" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Training Date
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateStart" runat="server" Width="100px" AutoPostBack="true" OnSelectedDateChanged="txtDateStart_SelectedDateChanged" />
                                    </td>
                                    <td>&nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDateEnd" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSREmployeeTrainingDateSeparator" runat="server" Text="With Date Separator"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeTrainingDateSeparator" runat="server" Width="75px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Training Time
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTimePicker ID="txtTimeStart" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTimePicker ID="txtTimeEnd" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Total Hour
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalHour" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDurationHour" runat="server" Width="100px" MinValue="0" NumberFormat-DecimalDigits="0" />
                                    </td>
                                    <td>&nbsp;Hour&nbsp;</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtDurationMinutes" runat="server" Width="100px" MinValue="0" NumberFormat-DecimalDigits="0" />
                                    </td>
                                    <td>&nbsp;Minutes</td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Credit Point
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtCreditPoint" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr runat="server" id="trTrainingPoint">
                        <td class="label">
                            <asp:Label ID="lblSREmployeeTrainingPointType" runat="server" Text="Training Point"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSREmployeeTrainingPointType" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="33%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlanningCosts" runat="server" Text="Planning Costs"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPlanningCosts" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRTrainingFinancingSources" runat="server" Text="Financing Sources"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRTrainingFinancingSources" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Total Fee Amount
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTotalFeeAmount" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Sponsor Fee Amount
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtSponsorFee" runat="server" Width="100px" Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Target Attendance
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTargetAttendance" runat="server" Width="100px"
                                Value="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>

                    <tr>
                        <td class="label">Notes
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" Height="80px" TextMode="MultiLine" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox ID="chkIsInHouseTraining" runat="server" Text="In-House" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox ID="chkIsScheduledTraining" runat="server" Text="Scheduled" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr style="display: none">
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked="true" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
            <td width="34%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCertificateValidityPeriod" runat="server" Text="Certificate Validity Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtCertificateValidityPeriod" runat="server" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label" />
                        <td class="entry">
                            <asp:CheckBox ID="chkIsCommitmentToWork" runat="server" Text="Commitment To Work" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblLengthOfService" runat="server" Text="Service Year"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLengthOfService" runat="server" Width="100px" Value="0" NumberFormat-DecimalDigits="0" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceDate" runat="server" Text="Service Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtStartServiceDate" runat="server" Width="100px" />
                                    </td>
                                    <td>&nbsp;-&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtEndServiceDate" runat="server" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblEvaluationDate" runat="server" Text="Evaluation Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtEvaluationDate" runat="server" Width="100px" />
                        </td>
                        <td width="20px" />
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Participant" PageViewID="pgvParticipant" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="External Trainer" PageViewID="pgvExternalTrainer">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvParticipant" runat="server">
            <telerik:RadGrid ID="grdEmployeeTrainingHistory" runat="server" OnNeedDataSource="grdEmployeeTrainingHistory_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdEmployeeTrainingHistory_UpdateCommand"
                OnDeleteCommand="grdEmployeeTrainingHistory_DeleteCommand" OnInsertCommand="grdEmployeeTrainingHistory_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="PersonID">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn UniqueName="listDetailEdit">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openItemComp('{0}', 'edit'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" alt=\"Edit Item Detail\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "PersonID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="listDetailView">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"openItemComp('{0}', 'view'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" alt=\"View Item Detail\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "PersonID"))%>
                            </ItemTemplate>
                            <HeaderStyle Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="EmployeeTrainingHistoryID" UniqueName="EmployeeTrainingHistoryID"
                            SortExpression="EmployeeTrainingHistoryID" Visible="false" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PersonID" UniqueName="PersonID"
                            SortExpression="PersonID" Visible="false" />
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EmployeeTrainingRoleName" HeaderText="Role" UniqueName="EmployeeTrainingRoleName"
                            SortExpression="EmployeeTrainingRoleName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAttending" HeaderText="Attend"
                            UniqueName="IsAttending" SortExpression="IsAttending" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Note" UniqueName="Note" SortExpression="Note"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeTrainingAttendanceDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeTrainingAttendanceEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvExternalTrainer" runat="server">
            <telerik:RadGrid ID="grdExternalTrainer" runat="server" OnNeedDataSource="grdExternalTrainer_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdExternalTrainer_UpdateCommand"
                OnDeleteCommand="grdExternalTrainer_DeleteCommand" OnInsertCommand="grdExternalTrainer_InsertCommand">
                <HeaderContextMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ExternalTrainerSeqNo">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridBoundColumn DataField="ExternalTrainerName" HeaderText="Trainer Name" UniqueName="ExternalTrainerName"
                            SortExpression="ExternalTrainerName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PositionAs" HeaderText="Position As" UniqueName="PositionAs"
                            SortExpression="PositionAs" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes" SortExpression="Notes"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="EmployeeTrainingExternalTrainerDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EmployeeTrainingExternalTrainerEditCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                    <CollapseAnimation Duration="200" Type="OutQuint" />
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>


</asp:Content>
