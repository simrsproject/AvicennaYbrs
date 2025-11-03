<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="NursingCareStandardViewImportedAssessment.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EmrIp.MainContent.NursingCare.NursingCareStandardViewImportedAssessment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdListAssessment" runat="server" AutoGenerateColumns="false"
        GridLines="None" OnNeedDataSource="grdListAssessment_NeedDataSource"
        OnItemDataBound="grdListAssessment_ItemDataBound" OnDataBound="grdListAssessment_DataBound">
        <MasterTableView ClientDataKeyNames="QuestionID" DataKeyNames="QuestionID">
            <Columns>
                <telerik:GridBoundColumn DataField="QuestionTextEdited" HeaderText="Assessment" HeaderStyle-Width="25%"
                    SortExpression="QuestionTextEdited" UniqueName="QuestionTextEdited">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SRAnswerType" HeaderText="SRAnswerType"
                    SortExpression="SRAnswerType" UniqueName="SRAnswerType" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AnswerPrefix" HeaderText="AnswerPrefix"
                    SortExpression="AnswerPrefix" UniqueName="AnswerPrefix" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AnswerSuffix" HeaderText="AnswerSuffix"
                    SortExpression="AnswerSuffix" UniqueName="AnswerSuffix" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Assessment" UniqueName="QuestionTextEdited" Visible="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtQuestionText" runat="server" Text='<%#Eval("QuestionTextEdited")%>'
                            Width="300px" MaxLength="255" AutoPostBack="true" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn HeaderText="Answer" UniqueName="AnswerObj">
                    <HeaderStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                
                <telerik:GridTemplateColumn HeaderText="Answer" UniqueName="AnswerText" Visible="false">
                    <HeaderStyle HorizontalAlign="Left"/>
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtAnswerText" runat="server" Text='<%#Eval("AnswerText")%>'
                            Width="200px" MaxLength="500" AutoPostBack="true" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="LastUpdateTransDTBy" HeaderText="Last Update By"
                    SortExpression="LastUpdateTransDTBy" UniqueName="LastUpdateTransDTBy">
                    <HeaderStyle HorizontalAlign="Left" Width="100" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastUpdateTransDTDateTime" HeaderText="Date"
                    DataFormatString="{0:MM/dd/yyyy HH:mm}" DataType="System.DateTime" 
                    SortExpression="LastUpdateTransDTDateTime" UniqueName="LastUpdateTransDTDateTime">
                    <HeaderStyle HorizontalAlign="Left" Width="120" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
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
