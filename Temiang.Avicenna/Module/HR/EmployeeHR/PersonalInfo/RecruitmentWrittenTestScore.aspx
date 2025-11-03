<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="RecruitmentWrittenTestScore.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.EmployeeHR.RecruitmentWrittenTestScore"
    Title="Recruitment Written Test Score" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="Temiang.Avicenna.BusinessObject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdPersonalRecruitmentTest">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdPersonalRecruitmentTest" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="grdPersonalRecruitmentTest" runat="server" OnNeedDataSource="grdPersonalRecruitmentTest_NeedDataSource"
                    OnInsertCommand="grdPersonalRecruitmentTest_InsertCommand" OnUpdateCommand="grdPersonalRecruitmentTest_UpdateCommand"
                    AutoGenerateColumns="False" GridLines="None">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="PersonalRecruitmentTestID, EvaluatorID, PositionID, Score">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn HeaderText="Evaluator" UniqueName="TemplateItemName">
                                <ItemTemplate>
                                    <asp:Label ID="lblEvaluatorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EvaluatorName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position"
                                UniqueName="PositionName" SortExpression="PositionName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="Score" HeaderText="Score"
                                UniqueName="Score" SortExpression="Score" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                        <EditFormSettings UserControlName="RecruitmentWrittenTestScoreDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ServiceUnitItemServiceCompDetailEditCommand">
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
        </tr>
    </table>
</asp:Content>

