<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="RecruitmentTestScoring.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.RecruitmentTestScoring" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy ID="ajaxMgrProxy" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdEvaluator">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdEvaluator" />
                    <telerik:AjaxUpdatedControl ControlID="txtTestResult" />
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
                                ItemStyle-HorizontalAlign="Left" />
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
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblTestResult" runat="server" Text="Test Result" />
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtTestResult" runat="server" Width="100px" MaxLength="10" MaxValue="99999999" MinValue="0" NumberFormat-DecimalDigits="2" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes" />
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" MaxLength="1000" TextMode="MultiLine" />
                        </td>
                        <td width="20px"></td>
                        <td />
                    </tr>
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
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    
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
