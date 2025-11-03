<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="InterventionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.Intervention.v2.InterventionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr style="display: none">
                        <td class="label">
                            <asp:Label ID="lblScoresheetID" runat="server" Text="ScoresheetID"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtScoresheetID" runat="server" Width="100px" ReadOnly="true" />
                            <telerik:RadNumericTextBox ID="txtReferenceID" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">Scoring Date</td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtScoringDate" runat="server" Width="100px" />
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
                        <td class="label">Period Year / Type</td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="95px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 5px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtEvaluatorTypeName" runat="server" Width="200px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Evaluator Name</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEvaluatorName" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Employee No</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEvaluatorEmployeeNo" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Organization Unit</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEvaluatorOrganizationUnitName" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">Position</td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEvaluatorPositionName" runat="server" Width="300px" ReadOnly="true" /></td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" Height="560px" AutoGenerateColumns="False" GridLines="None" OnItemDataBound="grdList_OnItemDataBound">
        <MasterTableView DataKeyNames="ScoresheetItemID, QuestionerItemID, QuestionerID, QuestionName" CommandItemDisplay="None" ShowHeader="True" HierarchyDefaultExpanded="true">
            <NestedViewTemplate>
                <div style="padding-left: 20px; width: 98%">
                    <telerik:RadTabStrip ID="tabsQuestioner" runat="server" MultiPageID="mpQuestioner" ShowBaseLine="true"
                        Align="Left" PerTabScrolling="True" Width="100%"
                        SelectedIndex="0">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Guidelines" PageViewID="pgvQuestioner"
                                Selected="True" />
                            <telerik:RadTab runat="server" Text="Type Of Rating" PageViewID="pgvRating" />
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="mpQuestioner" runat="server" SelectedIndex="0" ScrollBars="Auto" Width="100%"
                        CssClass="multiPage">
                        <telerik:RadPageView ID="pgvQuestioner" runat="server">
                            <telerik:RadGrid ID="grdQuestionerList" runat="server"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="QuestionerItemID" ShowHeader="True">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="QuestionCode" HeaderText="Code"
                                            UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="QuestionName" HeaderText="Question Name"
                                            UniqueName="QuestionName" SortExpression="QuestionName" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Description"
                                            UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="false">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                            <br />
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pgvRating" runat="server">
                            <telerik:RadGrid ID="grdRatingList" runat="server"
                                AutoGenerateColumns="False" GridLines="None">
                                <MasterTableView DataKeyNames="RatingID" ShowHeader="True">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="RatingCode" HeaderText="Code"
                                            UniqueName="RatingCode" SortExpression="RatingCode" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="RatingName" HeaderText="Type Of Rating" UniqueName="RatingName"
                                            SortExpression="RatingName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="RatingValue" HeaderText="Rating Value"
                                            UniqueName="RatingValue" SortExpression="RatingValue" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                                        <telerik:GridTemplateColumn />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="false">
                                    <Resizing AllowColumnResize="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                            <br />
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </NestedViewTemplate>
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="QuestionerName" HeaderText="Questioner "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="QuestionerNo" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="QuestionCode" HeaderText="Code"
                    UniqueName="QuestionCode" SortExpression="QuestionCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="QuestionName" HeaderText="Question Name"
                    UniqueName="QuestionName" SortExpression="QuestionName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="QuestionNotes" HeaderText="Description"
                    UniqueName="QuestionNotes" SortExpression="QuestionNotes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Score" HeaderText="Score"
                    UniqueName="Score" SortExpression="Score" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridBoundColumn DataField="InterventionNotes" HeaderText="Intervention Notes"
                    UniqueName="InterventionNotes" SortExpression="InterventionNotes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="InterventionScore" HeaderText="Intervention Score"
                    UniqueName="InterventionScore" SortExpression="InterventionScore" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridTemplateColumn HeaderStyle-Width="350px" HeaderText="Intervention Notes" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtInterventionNotes" runat="server" Width="325px" Text='<%#Eval("InterventionNotes")%>' TextMode="MultiLine" MaxLength="4000" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="100px" HeaderText="Intervention Score" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <ItemTemplate>
                        <telerik:RadNumericTextBox ID="txtInterventionScore" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "InterventionScore")) %>'
                            MinValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "MinValue")) %>'
                            MaxValue='<%# System.Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "MaxValue")) %>'
                            Width="90px">
                        </telerik:RadNumericTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="TotalScore" HeaderText="Total Score"
                    UniqueName="TotalScore" SortExpression="TotalScore" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HeaderStyle-Font-Bold="true" ItemStyle-Font-Bold="true" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
