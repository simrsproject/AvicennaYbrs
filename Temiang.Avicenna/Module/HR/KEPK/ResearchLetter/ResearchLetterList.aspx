<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ResearchLetterList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.KEPK.ResearchLetterList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="LetterID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="LetterID"
                    HeaderText="ID" UniqueName="LetterID" SortExpression="LetterID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResearcherName" HeaderText="Researcher Name"
                    UniqueName="ResearcherName" SortExpression="ResearcherName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LetterNo" HeaderText="Letter No"
                    UniqueName="LetterNo" SortExpression="LetterNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LetterDate" HeaderText="Date"
                    UniqueName="LetterDate" SortExpression="LetterDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject"
                    UniqueName="Subject" SortExpression="Subject" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResearchDecisionName" HeaderText="Decision" UniqueName="ResearchDecisionName"
                    SortExpression="ResearchDecisionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResearchInstitutionName" HeaderText="Institution" UniqueName="ResearchInstitutionName"
                    SortExpression="ResearchInstitutionName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResearchFacultyName" HeaderText="Faculty" UniqueName="ResearchFacultyName"
                    SortExpression="ResearchFacultyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResearchMajorsName" HeaderText="Majors" UniqueName="ResearchMajorsName"
                    SortExpression="ResearchMajorsName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="EducationDegreeName" HeaderText="Degree" UniqueName="EducationDegreeName"
                    SortExpression="EducationDegreeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsUpload" HeaderText="Upload"
                    UniqueName="IsUpload" SortExpression="IsUpload" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="ReviewTime"
                    HeaderText="Review Time (Days)" UniqueName="ReviewTime" SortExpression="ReviewTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ResearchReviewerName" HeaderText="Reviewer Name" UniqueName="ResearchReviewerName"
                    SortExpression="ResearchReviewerName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>