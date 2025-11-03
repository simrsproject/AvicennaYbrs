<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="QuestionerList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Appraisal.QuestionerList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="QuestionerID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="QuestionerNo" HeaderText="Questionnaire No"
                    UniqueName="QuestionerNo" SortExpression="QuestionerNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="QuestionerName" HeaderText="Questionnaire Name"
                    UniqueName="QuestionerName" SortExpression="QuestionerName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="AppraisalTypeName" HeaderText="Appraisal Type"
                    UniqueName="AppraisalTypeName" SortExpression="AppraisalTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PeriodYear" HeaderText="Period Year" UniqueName="PeriodYear"
                    SortExpression="PeriodYear" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LoadScore" HeaderText="Load (%)" UniqueName="LoadScore"
                    SortExpression="LoadScore" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes"
                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsScoringRecapitulation" HeaderText="Scoring Recapitulation"
                    UniqueName="IsScoringRecapitulation" SortExpression="IsScoringRecapitulation" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
