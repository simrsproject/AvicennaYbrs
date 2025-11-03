<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="InterventionList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.InterventionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15" AllowSorting="true">
        <MasterTableView DataKeyNames="ParticipantItemID, ScoresheetID" ClientDataKeyNames="ParticipantItemID, ScoresheetID" GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn DataField="PeriodYear" HeaderText="Period Year" UniqueName="PeriodYear" SortExpression="PeriodYear">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="ScoringDate" HeaderText="Scoring Date" UniqueName="ScoringDate"
                    SortExpression="ScoringDate">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="SubjectName" HeaderText="Subject Name" UniqueName="SubjectName" SortExpression="SubjectName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EvaluatorName" HeaderText="Evaluator Name" UniqueName="EvaluatorName" SortExpression="EvaluatorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EvaluatorType" HeaderText="Evaluator Type" UniqueName="EvaluatorType" SortExpression="EvaluatorType">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn DataField="ApprovedDateTime" HeaderText="Approved Date" UniqueName="ApprovedDateTime"
                    SortExpression="ApprovedDateTime">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridDateTimeColumn>
                <telerik:GridNumericColumn DataField="ScoreValue" HeaderText="Score" UniqueName="ScoreValue"
                    SortExpression="ScoreValue">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridNumericColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
