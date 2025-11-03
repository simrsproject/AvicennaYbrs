<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="QuestionnaireList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.CPA.QuestionnaireList" %>
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
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinValue" HeaderText="Minimum Score" UniqueName="MinValue" SortExpression="MinValue"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}"/>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MaxValue" HeaderText="Maximum Score" UniqueName="MaxValue"
                    SortExpression="MaxValue" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}"/>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>