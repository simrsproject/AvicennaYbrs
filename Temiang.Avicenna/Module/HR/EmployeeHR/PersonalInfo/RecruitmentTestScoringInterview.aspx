<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RecruitmentTestScoringInterview.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.RecruitmentTestScoringInterview" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEvaluator">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluator" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdInterview">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdInterview" />
                    <telerik:AjaxUpdatedControl ControlID="txtSumAverageScore" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:HiddenField runat="server" ID="hdnSRRecruitmentTest" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblApplicantNo" runat="server" Text="Applicant No" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeNumber" runat="server" Width="100%" ReadOnly="true" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblApplicantName" runat="server" Text="Applicant Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtEmployeeName" runat="server" Width="100%" ReadOnly="true" />
                        </td>
                        <td style="text-align: left"></td>
                    </tr>
                </table>
            </td>
            <td>
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTestDate" runat="server" Text="Test Date" />
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtTestDate" runat="server" Width="100px" Enabled="false" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRecruitmentTestName" runat="server" Text="Recruitment Test"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRecruitmentTestName" runat="server" Width="100%" ReadOnly="true" />
                        </td>
                        <td style="text-align: left"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <telerik:RadGrid ID="grdEvaluator" runat="server" OnNeedDataSource="grdEvaluator_NeedDataSource"
                    OnInsertCommand="grdEvaluator_InsertCommand" OnUpdateCommand="grdEvaluator_UpdateCommand" OnDeleteCommand="grdEvaluator_DeleteCommand"
                    AutoGenerateColumns="False" GridLines="None">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="EvaluatorID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="EvaluatorName" HeaderText="Evaluator"
                                UniqueName="EvaluatorName" SortExpression="EvaluatorName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position"
                                UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="Score" HeaderText="Score"
                                UniqueName="Score" SortExpression="Score" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="RecruitmentTestScoringEvaluatorDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="RecruitmentTestScoringEvaluatorEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td style="width: 50%; vertical-align: top">
                <asp:Panel ID="pnlInterview" runat="server">
                    <telerik:RadGrid ID="grdInterview" runat="server" OnNeedDataSource="grdInterview_NeedDataSource"
                        OnItemDataBound="grdInterview_ItemDataBound" OnItemCreated="grdInterview_ItemCreated" AutoGenerateColumns="False" GridLines="None">
                        <MasterTableView DataKeyNames="QuestionID" CommandItemDisplay="None" ShowHeader="True">
                            <ColumnGroups>
                                <telerik:GridColumnGroup HeaderText="Scoring" Name="Scoring" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridColumnGroup>
                            </ColumnGroups>
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="QuestionID" HeaderText="ID"
                                    UniqueName="QuestionID" SortExpression="QuestionID" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="QuestionName" HeaderText="Question Name"
                                    UniqueName="QuestionName" SortExpression="QuestionName" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="#1" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Scoring">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtScore1" runat="server" Width="50px" DbValue='<%#Eval("Score1")%>'
                                            NumberFormat-DecimalDigits="2" Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'
                                            OnTextChanged="txtScore_TextChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="#2" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Scoring">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtScore2" runat="server" Width="50px" DbValue='<%#Eval("Score2")%>'
                                            NumberFormat-DecimalDigits="2" Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'
                                            OnTextChanged="txtScore_TextChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="#3" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Scoring">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtScore3" runat="server" Width="50px" DbValue='<%#Eval("Score3")%>'
                                            NumberFormat-DecimalDigits="2" Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'
                                            OnTextChanged="txtScore_TextChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="#4" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Scoring">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtScore4" runat="server" Width="50px" DbValue='<%#Eval("Score4")%>'
                                            NumberFormat-DecimalDigits="2" Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'
                                            OnTextChanged="txtScore_TextChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="#5" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Scoring">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtScore5" runat="server" Width="50px" DbValue='<%#Eval("Score5")%>'
                                            NumberFormat-DecimalDigits="2" Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'
                                            OnTextChanged="txtScore_TextChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="#6" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ColumnGroupName="Scoring">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtScore6" runat="server" Width="50px" DbValue='<%#Eval("Score6")%>'
                                            NumberFormat-DecimalDigits="2" Enabled='<%# System.Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDetail")) %>'
                                            OnTextChanged="txtScore_TextChanged" AutoPostBack="true" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderStyle-Width="85px" HeaderText="Average Score" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" UniqueName="txtAverageScore">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtAverageScore" runat="server" Width="77px" Enabled="false" DbValue='<%#Eval("AverageScore")%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AverageScore" HeaderText="Average"
                                    UniqueName="AverageScore" SortExpression="AverageScore" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" Visible="false" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="IsDetail" HeaderText="IsDetail"
                                    UniqueName="IsDetail" SortExpression="IsDetail" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridTemplateColumn HeaderStyle-Width="5px" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="false">
                            <Resizing AllowColumnResize="True" />
                            <Selecting AllowRowSelect="False" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTotal" runat="server" Text="Total" />
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtSumAverageScore" runat="server" Width="100px" ReadOnly="true">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                    </table>
                    <hr />
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblAdvantages" runat="server" Text="Advantages" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtAdvantages" runat="server" Width="100%" Height="65px" TextMode="MultiLine" MaxLength="4000" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDeficiency" runat="server" Text="Deficiency" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDeficiency" runat="server" Width="100%" Height="65px" TextMode="MultiLine" MaxLength="4000" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSuggestion" runat="server" Text="Suggestion" />
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtSuggestion" runat="server" Width="100%" Height="65px" TextMode="MultiLine" MaxLength="4000" />
                            </td>
                            <td width="20px"></td>
                            <td />
                        </tr>
                    </table>
                </asp:Panel>
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSRRecruitmentTestConclusion" runat="server" Text="Conclusion" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRRecruitmentTestConclusion" runat="server" Width="100%" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                </table>
            </td>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
