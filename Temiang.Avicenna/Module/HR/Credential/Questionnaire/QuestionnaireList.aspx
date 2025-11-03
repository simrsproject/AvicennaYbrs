<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="QuestionnaireList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Credential.Questionnaire.QuestionnaireList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="QuestionnaireID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="QuestionnaireCode" HeaderText="Code"
                    UniqueName="QuestionnaireCode" SortExpression="QuestionnaireCode" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="QuestionnaireName" HeaderText="Form Name"
                    UniqueName="QuestionnaireName" SortExpression="QuestionnaireName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProfessionGroupName" HeaderText="Profession Group"
                    UniqueName="ProfessionGroupName" SortExpression="ProfessionGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClinicalWorkAreaName" HeaderText="Work Area"
                    UniqueName="ClinicalWorkAreaName" SortExpression="ClinicalWorkAreaName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClinicalAuthorityLevelName" HeaderText="Clinical Authority Level / Qualification"
                    UniqueName="ClinicalAuthorityLevelName" SortExpression="ClinicalAuthorityLevelName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

