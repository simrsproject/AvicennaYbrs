<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="PositionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: top">
                <table>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPositionID" runat="server" Text="Position ID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtPositionID" runat="server" Width="300px" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionID" runat="server" ErrorMessage="Position ID required."
                                ValidationGroup="entry" ControlToValidate="txtPositionID" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionCode" runat="server" Text="Position Code"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionCode" runat="server" Width="300px" MaxLength="10" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionCode" runat="server" ErrorMessage="Position Code required."
                                ValidationGroup="entry" ControlToValidate="txtPositionCode" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPositionName" runat="server" Text="Position Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionName" runat="server" Width="300px" MaxLength="200" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvPositionName" runat="server" ErrorMessage="Position Name required."
                                ValidationGroup="entry" ControlToValidate="txtPositionName" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSummary" runat="server" Text="Summary"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtSummary" runat="server" Width="300px" Height="100px" MaxLength="4000"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <table>
                    <tr runat="server" id="trPositionGradeID">
                        <td class="label">
                            <asp:Label ID="lblPositionGradeID" runat="server" Text="Position Grade Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPositionGradeID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionGradeID_ItemDataBound"
                                OnItemsRequested="cboPositionGradeID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "PositionGradeCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PositionGradeName")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Note : Show max 20 items
                                </FooterTemplate>
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblPositionLevelID" runat="server" Text="Position Level"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboPositionLevelID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPositionLevelID_ItemDataBound"
                                OnItemsRequested="cboPositionLevelID_ItemsRequested">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "PositionLevelCode")%>
                                    &nbsp;-&nbsp;
                                    <%# DataBinder.Eval(Container.DataItem, "PositionLevelName")%>
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
                        <td class="label">
                            <asp:Label ID="lblValidFrom" runat="server" Text="Valid From"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidFrom" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidFrom" runat="server" ErrorMessage="Valid From required."
                                ValidationGroup="entry" ControlToValidate="txtValidFrom" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblValidTo" runat="server" Text="Valid To"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtValidTo" runat="server" Width="100px" MinDate="01/01/1900"
                                MaxDate="12/31/2999" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvValidTo" runat="server" ErrorMessage="Valid To required."
                                ValidationGroup="entry" ControlToValidate="txtValidTo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <telerik:RadTabStrip ID="tabHeader" runat="server" MultiPageID="mpgHeader" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Job Description" PageViewID="pgvJobDesc" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Qualification" PageViewID="pgvQualification">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgHeader" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvJobDesc" runat="server">
            <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="Duty" PageViewID="pgvPositionDuty" Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Goal" PageViewID="pgvPositionGoal">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Ranking" PageViewID="pgvPositionRanking">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Responsibility" PageViewID="pgvPositionResponsibility">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Authority" PageViewID="pgvPositionAuthority">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Work Result" PageViewID="pgvPositionWorkResult">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Functional Area" PageViewID="pgvFunctionalArea">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Position Benchmark" PageViewID="pgvPositionBenchmark">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvPositionDuty" runat="server">
                    <telerik:RadGrid ID="grdPositionDuty" runat="server" OnNeedDataSource="grdPositionDuty_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionDuty_UpdateCommand"
                        OnDeleteCommand="grdPositionDuty_DeleteCommand" OnInsertCommand="grdPositionDuty_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionDutyID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionDutyID" HeaderText="Position Duty ID"
                                    UniqueName="PositionDutyID" SortExpression="PositionDutyID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionID" HeaderText="Position ID"
                                    UniqueName="PositionID" SortExpression="PositionID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn DataField="DutyName" HeaderText="Duty Name"
                                    UniqueName="DutyName" SortExpression="DutyName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionDutyDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionDutyEditCommand">
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

                <telerik:RadPageView ID="pgvPositionGoal" runat="server">
                    <telerik:RadGrid ID="grdPositionGoal" runat="server" OnNeedDataSource="grdPositionGoal_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionGoal_UpdateCommand"
                        OnDeleteCommand="grdPositionGoal_DeleteCommand" OnInsertCommand="grdPositionGoal_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionGoalID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionGoalID" HeaderText="ID"
                                    UniqueName="PositionGoalID" SortExpression="PositionGoalID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionID" HeaderText="Position ID"
                                    UniqueName="PositionID" SortExpression="PositionID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="GoalName" HeaderText="Goal Name"
                                    UniqueName="GoalName" SortExpression="GoalName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionGoalDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionGoalEditCommand">
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

                <telerik:RadPageView ID="pgvPositionRanking" runat="server">
                    <telerik:RadGrid ID="grdPositionRanking" runat="server" OnNeedDataSource="grdPositionRanking_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionRanking_UpdateCommand"
                        OnDeleteCommand="grdPositionRanking_DeleteCommand" OnInsertCommand="grdPositionRanking_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionRankingID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionRankingID" HeaderText="ID"
                                    UniqueName="PositionRankingID" SortExpression="PositionRankingID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionID" HeaderText="Position ID"
                                    UniqueName="PositionID" SortExpression="PositionID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="RankingName" HeaderText="Ranking Name"
                                    UniqueName="RankingName" SortExpression="RankingName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionRankingDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionRankingEditCommand">
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

                <telerik:RadPageView ID="pgvPositionResponsibility" runat="server">
                    <telerik:RadGrid ID="grdPositionResponsibility" runat="server" OnNeedDataSource="grdPositionResponsibility_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionResponsibility_UpdateCommand"
                        OnDeleteCommand="grdPositionResponsibility_DeleteCommand" OnInsertCommand="grdPositionResponsibility_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionID, PositionResponsibilityID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="ResponsibilityName"
                                    HeaderText="Responsibility Name" UniqueName="ResponsibilityName" SortExpression="ResponsibilityName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionResponsibilityDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionResponsibilityEditCommand">
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

                <telerik:RadPageView ID="pgvPositionAuthority" runat="server">
                    <telerik:RadGrid ID="grdPositionAuthority" runat="server" OnNeedDataSource="grdPositionAuthority_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionAuthority_UpdateCommand"
                        OnDeleteCommand="grdPositionAuthority_DeleteCommand" OnInsertCommand="grdPositionAuthority_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionID, PositionAuthorityID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="AuthorityName"
                                    HeaderText="Authority Name" UniqueName="AuthorityName" SortExpression="AuthorityName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionAuthorityDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionAuthorityEditCommand">
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

                <telerik:RadPageView ID="pgvPositionWorkResult" runat="server">
                    <telerik:RadGrid ID="grdPositionWorkResult" runat="server" OnNeedDataSource="grdPositionWorkResult_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionWorkResult_UpdateCommand"
                        OnDeleteCommand="grdPositionWorkResult_DeleteCommand" OnInsertCommand="grdPositionWorkResult_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionID, PositionWorkResultID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="WorkResultName"
                                    HeaderText="Work Result Name" UniqueName="WorkResultName" SortExpression="WorkResultName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionWorkResultDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionWorkResultEditCommand">
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

                <telerik:RadPageView ID="pgvFunctionalArea" runat="server">
                    <telerik:RadGrid ID="grdPositionFunctionalArea" runat="server" OnNeedDataSource="grdPositionFunctionalArea_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionFunctionalArea_UpdateCommand"
                        OnDeleteCommand="grdPositionFunctionalArea_DeleteCommand" OnInsertCommand="grdPositionFunctionalArea_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionFunctionalAreaID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PositionFunctionalAreaID"
                                    HeaderText="Position Functional Area ID" UniqueName="PositionFunctionalAreaID"
                                    SortExpression="PositionFunctionalAreaID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn DataField="PositionFunctionalAreaName" HeaderText="Position Functional Area"
                                    UniqueName="PositionFunctionalAreaName" SortExpression="PositionFunctionalAreaName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionFunctionalAreaDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionFunctionalAreaEditCommand">
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

                <telerik:RadPageView ID="pgvPositionBenchmark" runat="server">
                    <telerik:RadGrid ID="grdPositionBenchmark" runat="server" OnNeedDataSource="grdPositionBenchmark_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionBenchmark_UpdateCommand"
                        OnDeleteCommand="grdPositionBenchmark_DeleteCommand" OnInsertCommand="grdPositionBenchmark_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionBenchmarkID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionBenchmarkID" HeaderText="ID"
                                    UniqueName="PositionBenchmarkID" SortExpression="PositionBenchmarkID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionID" HeaderText="Position ID"
                                    UniqueName="PositionID" SortExpression="PositionID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="BenchmarkName" HeaderText="Benchmark Name"
                                    UniqueName="BenchmarkName" SortExpression="BenchmarkName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description"
                                    SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="PositionBenchmarkDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionBenchmarkEditCommand">
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
        </telerik:RadPageView>

        <telerik:RadPageView ID="pgvQualification" runat="server">
            <telerik:RadTabStrip ID="tabDetail2" runat="server" MultiPageID="mpgDetail2" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab runat="server" Text="General" PageViewID="pgvGeneral" Selected="true">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Physical" PageViewID="pgvPhysical">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Psychological" PageViewID="pgvPsychological">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Education" PageViewID="pgvEducation">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="License" PageViewID="pgvLicense">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Work Experience" PageViewID="pgvWorkExperience">
                    </telerik:RadTab>
                    <telerik:RadTab runat="server" Text="Employment at this Company" PageViewID="pgvEmploymentAtThisCompany">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="mpgDetail2" runat="server" SelectedIndex="0" BorderStyle="Solid"
                BorderColor="gray">
                <telerik:RadPageView ID="pgvGeneral" runat="server">
                    <table width="100%">
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txtGeneralQualification" runat="server" Width="100%" Height="200px" MaxLength="4000"
                                    TextMode="MultiLine" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="pgvPhysical" runat="server">
                    <telerik:RadGrid ID="grdPositionPhysical" runat="server" OnNeedDataSource="grdPositionPhysical_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionPhysical_UpdateCommand"
                        OnDeleteCommand="grdPositionPhysical_DeleteCommand" OnInsertCommand="grdPositionPhysical_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionPhysicalID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionPhysicalID"
                                    HeaderText="Position Physical ID" UniqueName="PositionPhysicalID" SortExpression="PositionPhysicalID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="False" />
                                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SRPhysicalCharacteristic"
                                    HeaderText="Physical Characteristic Code" UniqueName="SRPhysicalCharacteristic"
                                    SortExpression="SRPhysicalCharacteristic" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                                <telerik:GridBoundColumn DataField="PhysicalCharacteristicName" HeaderText="Physical Characteristic"
                                    UniqueName="PhysicalCharacteristicName" SortExpression="PhysicalCharacteristicName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SROperandType" HeaderText="Operand Type Code"
                                    UniqueName="SROperandType" SortExpression="SROperandType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OperandTypeName" HeaderText="Operand Type"
                                    UniqueName="OperandTypeName" SortExpression="OperandTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PhysicalValue" HeaderText="Physical Value"
                                    UniqueName="PhysicalValue" SortExpression="PhysicalValue" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SRMeasurementCode" HeaderText="Measurement Code"
                                    UniqueName="SRMeasurementCode" SortExpression="SRMeasurementCode" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="MeasurementName" HeaderText="Measurement Code"
                                    UniqueName="MeasurementName" SortExpression="MeasurementName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="../PositionQualification/PositionPhysicalDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionPhysicalEditCommand">
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
                <telerik:RadPageView ID="pgvPsychological" runat="server">
                    <telerik:RadGrid ID="grdPositionPsychological" runat="server" OnNeedDataSource="grdPositionPsychological_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionPsychological_UpdateCommand"
                        OnDeleteCommand="grdPositionPsychological_DeleteCommand" OnInsertCommand="grdPositionPsychological_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionPsychologicalID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionPsychologicalID"
                                    HeaderText="Position Psychological ID" UniqueName="PositionPsychologicalID" SortExpression="PositionPsychologicalID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRPsychological" HeaderText="Psychological"
                                    UniqueName="SRPsychological" SortExpression="SRPsychological" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn DataField="PsychologicalName" HeaderText="Psychological Name"
                                    UniqueName="PsychologicalName" SortExpression="PsychologicalName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SROperandType" HeaderText="Operand Type Code"
                                    UniqueName="SROperandType" SortExpression="SROperandType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="OperandTypeName" HeaderText="Operand Type" UniqueName="OperandTypeName"
                                    SortExpression="OperandTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="PsychologicalValue"
                                    HeaderText="Psychological Value" UniqueName="PsychologicalValue" SortExpression="PsychologicalValue"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="../PositionQualification/PositionPsychologicalDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionPsychologicalEditCommand">
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
                <telerik:RadPageView ID="pgvEducation" runat="server">
                    <telerik:RadGrid ID="grdPositionEducation" runat="server" OnNeedDataSource="grdPositionEducation_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionEducation_UpdateCommand"
                        OnDeleteCommand="grdPositionEducation_DeleteCommand" OnInsertCommand="grdPositionEducation_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionEducationID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionEducationID"
                                    HeaderText="Position Education ID" UniqueName="PositionEducationID" SortExpression="PositionEducationID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                                    UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn DataField="HRRequirementName" HeaderText="Requirement Name"
                                    UniqueName="HRRequirementName" SortExpression="HRRequirementName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SREducationLevel" HeaderText="Education Level"
                                    UniqueName="SREducationLevel" SortExpression="SREducationLevel" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="EducationLevelName"
                                    HeaderText="Education Level" UniqueName="EducationLevelName" SortExpression="EducationLevelName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SREducationField" HeaderText="Education Field"
                                    UniqueName="SREducationField" SortExpression="SREducationField" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="EducationFieldName"
                                    HeaderText="Education Field" UniqueName="EducationFieldName" SortExpression="EducationFieldName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="EducationNotes" HeaderText="Education Notes"
                                    UniqueName="EducationNotes" SortExpression="EducationNotes" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="../PositionQualification/PositionEducationDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionEducationEditCommand">
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
                <telerik:RadPageView ID="pgvLicense" runat="server">
                    <telerik:RadGrid ID="grdPositionLicense" runat="server" OnNeedDataSource="grdPositionLicense_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionLicense_UpdateCommand"
                        OnDeleteCommand="grdPositionLicense_DeleteCommand" OnInsertCommand="grdPositionLicense_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionLicenseID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionLicenseID"
                                    HeaderText="Position License ID" UniqueName="PositionLicenseID" SortExpression="PositionLicenseID"
                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                                    UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="HRRequirementName"
                                    HeaderText="Requirement Name" UniqueName="HRRequirementName" SortExpression="HRRequirementName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRLicenseType" HeaderText="License Type"
                                    UniqueName="SRLicenseType" SortExpression="SRLicenseType" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="LicenseTypeName" HeaderText="License Type"
                                    UniqueName="LicenseTypeName" SortExpression="LicenseTypeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="LicenseNotes" HeaderText="License Notes" UniqueName="LicenseNotes"
                                    SortExpression="LicenseNotes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="../PositionQualification/PositionLicenseDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionLicenseEditCommand">
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
                <telerik:RadPageView ID="pgvWorkExperience" runat="server">
                    <telerik:RadGrid ID="grdPositionWorkExperience" runat="server" OnNeedDataSource="grdPositionWorkExperience_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionWorkExperience_UpdateCommand"
                        OnDeleteCommand="grdPositionWorkExperience_DeleteCommand" OnInsertCommand="grdPositionWorkExperience_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionWorkExperienceID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionWorkExperienceID"
                                    HeaderText="Position Work Experience ID" UniqueName="PositionWorkExperienceID"
                                    SortExpression="PositionWorkExperienceID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                                    UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="HRRequirementName"
                                    HeaderText="Requirement Name" UniqueName="HRRequirementName" SortExpression="HRRequirementName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SRLineBusiness" HeaderText="Line Business"
                                    UniqueName="SRLineBusiness" SortExpression="SRLineBusiness" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LineBusinessName" HeaderText="Line Business Name"
                                    UniqueName="LineBusinessName" SortExpression="LineBusinessName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="YearExperience" HeaderText="Year Experience"
                                    UniqueName="YearExperience" SortExpression="YearExperience" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridBoundColumn DataField="WorkExperienceNotes" HeaderText="Work Experience Notes"
                                    UniqueName="WorkExperienceNotes" SortExpression="WorkExperienceNotes" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="../PositionQualification/PositionWorkExperienceDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionWorkExperienceEditCommand">
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
                <telerik:RadPageView ID="pgvEmploymentAtThisCompany" runat="server">
                    <telerik:RadGrid ID="grdPositionEmploymentCompany" runat="server" OnNeedDataSource="grdPositionEmploymentCompany_NeedDataSource"
                        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdPositionEmploymentCompany_UpdateCommand"
                        OnDeleteCommand="grdPositionEmploymentCompany_DeleteCommand" OnInsertCommand="grdPositionEmploymentCompany_InsertCommand">
                        <HeaderContextMenu>
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="PositionEmploymentCompanyID">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionEmploymentCompanyID"
                                    HeaderText="Position Employment Company ID" UniqueName="PositionEmploymentCompanyID"
                                    SortExpression="PositionEmploymentCompanyID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="10px" DataField="SRRequirement" HeaderText="Requirement"
                                    UniqueName="SRRequirement" SortExpression="SRRequirement" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="180px" DataField="HRRequirementName"
                                    HeaderText="Requirement Name" UniqueName="HRRequirementName" SortExpression="HRRequirementName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="YearOfService" HeaderText="Year Of Service"
                                    UniqueName="YearOfService" SortExpression="YearOfService" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Right" />
                                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="PositionGradeID" HeaderText="Position Grade ID"
                                    UniqueName="PositionGradeID" SortExpression="PositionGradeID" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                                <telerik:GridBoundColumn DataField="PositionGradeName" HeaderText="Position Grade Name"
                                    UniqueName="PositionGradeName" SortExpression="PositionGradeName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="LastUpdateByUserID"
                                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <EditFormSettings UserControlName="../PositionQualification/PositionEmploymentCompanyDetail.ascx" EditFormType="WebUserControl">
                                <EditColumn UniqueName="PositionEmploymentCompanyEditCommand">
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
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
