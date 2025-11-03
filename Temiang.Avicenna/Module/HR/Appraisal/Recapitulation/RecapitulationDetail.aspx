<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="RecapitulationDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.Recapitulation.RecapitulationDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function OnCalculation() {
                __doPostBack("<%= grdListScores.UniqueID %>", "calculation");
            }
        </script>

    </telerik:RadCodeBlock>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblParticipantItemID" runat="server" Text="ParticipantItemID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtParticipantItemID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Period Year</td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtPeriodYear" Width="80px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 2px"></td>
                                    <td class="label" style="width: 50px">Quarter</td>
                                    <td style="width: 2px"></td>
                                    <td>
                                        <telerik:RadComboBox ID="cboSRQuarterPeriod" runat="server" Width="156px" AllowCustomText="true"
                                            Filter="Contains" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Job Holder Name</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Employee No</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeNo" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Organization Unit</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrganizationUnitName" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Position</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPositionName" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">Conclusion
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtTotal" runat="server" Width="200px" Height="100px" Font-Size="50px" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                    </td>
                                    <td style="width: 20px"></td>
                                    <td>
                                        <asp:Label ID="lblConclusion" runat="server" Font-Size="50px"></asp:Label>
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
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Scores" PageViewID="pgvScores" Selected="true">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Recapitulation" PageViewID="pgvRecap">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Appraiser Notes" PageViewID="pgvNotes">
            </telerik:RadTab>
            <telerik:RadTab runat="server" Text="Conclusion Matrix" PageViewID="pgvConclusion">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvScores" runat="server">
            <telerik:RadGrid ID="grdListScores" runat="server" OnNeedDataSource="grdListScores_NeedDataSource" OnItemDataBound="grdListScores_ItemDataBound" GridLines="None"
                AutoGenerateColumns="false" AllowSorting="true">
                <MasterTableView DataKeyNames="QuestionerItemID" GroupLoadMode="Client">
                    <CommandItemTemplate>
                        &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbCalculation" runat="server" OnClientClick="javascript:OnCalculation();return false;">
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/process16.png" />
                        &nbsp;<asp:Label runat="server" ID="lblCalculation" Text="Process Scoring"></asp:Label>
                    </asp:LinkButton>
                    </CommandItemTemplate>
                    <CommandItemStyle Height="29px" />
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="QuestionerName" HeaderText="Questionnaire "></telerik:GridGroupByField>
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="QuestionerNo" SortOrder="Ascending"></telerik:GridGroupByField>
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="QuestionerItemID" HeaderText="QuestionerItemID"
                            UniqueName="QuestionerItemID" SortExpression="QuestionerItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="QuestionCode" HeaderText="Code"
                            UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="QuestionGroupName" HeaderText="Group"
                            UniqueName="QuestionGroupName" SortExpression="QuestionGroupName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="350px" HeaderText="Question Name" UniqueName="TemplateItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQuestionName" runat="server" Text='<%# GetQuestionName(DataBinder.Eval(Container.DataItem, "QuestionGroupName"), DataBinder.Eval(Container.DataItem, "QuestionName")) %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SupervisorScoreX" HeaderText="Supervisor"
                            UniqueName="SupervisorScoreX" SortExpression="SupervisorScoreX" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PartnerScoreX" HeaderText="Partner"
                            UniqueName="PartnerScoreX" SortExpression="PartnerScoreX" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SubordinateScoreX" HeaderText="Subordinate"
                            UniqueName="SubordinateScoreX" SortExpression="SubordinateScoreX" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="SelfScoreX" HeaderText="Self"
                            UniqueName="SelfScoreX" SortExpression="SelfScoreX" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TotalScoreX" HeaderText="Total Score"
                            UniqueName="TotalScoreX" SortExpression="TotalScoreX" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AverageScoreX" HeaderText="Average"
                            UniqueName="AverageScoreX" SortExpression="AverageScoreX" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IsScoringEnabled" HeaderText="IsScoringEnabled"
                            UniqueName="IsScoringEnabled" SortExpression="IsScoringEnabled" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvRecap" runat="server">
            <telerik:RadGrid ID="grdListRecap" runat="server" OnNeedDataSource="grdListRecap_NeedDataSource" GridLines="None"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
                <MasterTableView DataKeyNames="QuestionerID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="QuestionerID" HeaderText="QuestionerID"
                            UniqueName="QuestionerID" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="QuestionerName" HeaderText="Questionnaire"
                            UniqueName="QuestionerName" SortExpression="QuestionerName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AverageScore" HeaderText="Total Score"
                            UniqueName="AverageScore" SortExpression="AverageScore" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="NumberOfDividers" HeaderText="Number Of Dividers"
                            UniqueName="NumberOfDividers" SortExpression="NumberOfDividers" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Score" HeaderText="Score"
                            UniqueName="Score" SortExpression="Score" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LoadScore" HeaderText="Load Score (%)"
                            UniqueName="LoadScore" SortExpression="LoadScore" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalScore" HeaderText="Average"
                            UniqueName="TotalScore" SortExpression="TotalScore" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvNotes" runat="server">
            <telerik:RadGrid ID="grdListNotes" runat="server" OnNeedDataSource="grdListNotes_NeedDataSource" GridLines="None"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
                <MasterTableView DataKeyNames="QuestionerID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="QuestionerID" HeaderText="QuestionerID"
                            UniqueName="QuestionerID" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="QuestionerName" HeaderText="Questionnaire"
                            UniqueName="QuestionerName" SortExpression="QuestionerName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="Capacity" HeaderText="Capacity"
                            UniqueName="Capacity" SortExpression="Capacity" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="NeedsToBeDeveloped" HeaderText="Needs To Be Developed"
                            UniqueName="NeedsToBeDeveloped" SortExpression="NeedsToBeDeveloped" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Intervention Notes" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtCapacity" runat="server" Width="475px" Text='<%#Eval("Capacity")%>' TextMode="MultiLine" MaxLength="4000" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="500px" HeaderText="Needs To Be Developed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtNeedsToBeDeveloped" runat="server" Width="475px" Text='<%#Eval("NeedsToBeDeveloped")%>' TextMode="MultiLine" MaxLength="4000" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvConclusion" runat="server">
            <telerik:RadGrid ID="grdListConclusion" runat="server" OnNeedDataSource="grdListConclusion_NeedDataSource" GridLines="None"
                AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
                <MasterTableView DataKeyNames="ConclusionID" GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="ConclusionID" HeaderText="ConclusionID"
                            UniqueName="ConclusionID" SortExpression="ConclusionID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ConclusionName" HeaderText="Conclusion"
                            UniqueName="ConclusionName" SortExpression="ConclusionName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinValue" HeaderText="Minimum Value" UniqueName="MinValue" SortExpression="MinValue"
                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MaxValue" HeaderText="Maximum Value" UniqueName="MaxValue"
                            SortExpression="MaxValue" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                        <telerik:GridTemplateColumn />
                    </Columns>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
