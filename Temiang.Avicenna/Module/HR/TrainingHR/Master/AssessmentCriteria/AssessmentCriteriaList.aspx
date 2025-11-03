<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="AssessmentCriteriaList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.TrainingHR.Master.AssessmentCriteria.AssessmentCriteriaList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="AssessmentCriteriaID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="AssessmentCriteriaID" HeaderText="ID"
                    UniqueName="AssessmentCriteriaID" SortExpression="AssessmentCriteriaID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="AssessmentCriteriaName" HeaderText="Assessment Criteria" UniqueName="AssessmentCriteriaName"
                    SortExpression="AssessmentCriteriaName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MinValue"
                    HeaderText="Min Value" UniqueName="MinValue" SortExpression="MinValue"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="MaxValue"
                    HeaderText="Max Value" UniqueName="MaxValue" SortExpression="MaxValue"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                </telerik:GridNumericColumn>
                <telerik:GridBoundColumn DataField="Recommendation" HeaderText="Recommendation" UniqueName="Recommendation"
                    SortExpression="Recommendation" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>