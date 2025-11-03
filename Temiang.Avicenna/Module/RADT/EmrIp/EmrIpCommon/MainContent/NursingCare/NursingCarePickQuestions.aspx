<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCarePickQuestions.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCarePickQuestions" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdListAssessment" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="grdListAssessment_NeedDataSource"
        OnItemDataBound="grdListAssessment_ItemDataBound" OnDataBound="grdListAssessment_DataBound">
        <MasterTableView ClientDataKeyNames="QuestionID" DataKeyNames="QuestionID">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="DS" UniqueName="ChkBoxDS" Visible="true">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDS" runat="server" ToolTip='<%#Eval("QuestionID")%>'>
                        </asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="DO" UniqueName="ChkBoxDO" Visible="true">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDO" runat="server" ToolTip='<%#Eval("QuestionID")%>'>
                        </asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="QuestionText" HeaderText="Question Text" HeaderStyle-Width="25%"
                    SortExpression="QuestionText" UniqueName="QuestionText">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SRAnswerType" HeaderText="SRAnswerType"
                    SortExpression="SRAnswerType" UniqueName="SRAnswerType" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AnswerPrefix" HeaderText="Prefix"
                    SortExpression="AnswerPrefix" UniqueName="AnswerPrefix" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AnswerSuffix" HeaderText="Suffix"
                    SortExpression="AnswerSuffix" UniqueName="AnswerSuffix" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuestionAnswerText" HeaderText="QuestionAnswerText"
                    SortExpression="QuestionAnswerText" UniqueName="QuestionAnswerText" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuestionAnswerText" HeaderText="QuestionAnswerText"
                    SortExpression="QuestionAnswerText" UniqueName="QuestionAnswerText" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AnswerDecimalDigit" HeaderText="AnswerDecimalDigit"
                    SortExpression="AnswerDecimalDigit" UniqueName="AnswerDecimalDigit" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QuestionAnswerNum" HeaderText="QuestionAnswerNum"
                    SortExpression="QuestionAnswerNum" UniqueName="QuestionAnswerNum" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Answer" UniqueName="AnswerObj">
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn HeaderText="Answer" UniqueName="QuestionAnswerText" Visible="false">
                    <HeaderStyle HorizontalAlign="Left"/>
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtAnswerText" runat="server" Text='<%#Eval("QuestionAnswerText")%>'
                            Width="200px" MaxLength="500" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
