<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="NursingAssessmentQuestionList.aspx.cs" Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingAssessmentQuestionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnItemDataBound="grdList_ItemDataBound">
        <MasterTableView DataKeyNames="QuestionID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="QuestionID" HeaderText="ID"
                    UniqueName="QuestionID" SortExpression="QuestionID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="QuestionText" HeaderText="Question Text" UniqueName="QuestionText"
                    SortExpression="QuestionText" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" /> 
                <telerik:GridBoundColumn DataField="NursingDisplayAs" HeaderText="Display As" UniqueName="NursingDisplayAs"
                    SortExpression="NursingDisplayAs" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />  
                <telerik:GridBoundColumn DataField="SRAnswerType" HeaderText="Type" HeaderStyle-Width="50px" UniqueName="SRAnswerType"
                    SortExpression="SRAnswerType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="equivQuestionID" HeaderText="Equivalent ID"
                    UniqueName="equivQuestionID" SortExpression="equivQuestionID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="equivQuestionText" HeaderText="Equivalent Question" UniqueName="equivQuestionText"
                    SortExpression="equivQuestionText" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />    
                <telerik:GridBoundColumn DataField="equivSRAnswerType" HeaderText="Type" HeaderStyle-Width="50px" UniqueName="equivSRAnswerType"
                    SortExpression="equivSRAnswerType" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
