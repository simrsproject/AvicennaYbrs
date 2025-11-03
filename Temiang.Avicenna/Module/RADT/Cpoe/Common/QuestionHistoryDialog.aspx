<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="QuestionHistoryDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.EpisodeAndHistory.RSCH.QuestionHistoryDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdQuestion" runat="server" AutoGenerateColumns="False"
        GridLines="None" OnNeedDataSource="grdQuestion_NeedDataSource">
        <MasterTableView DataKeyNames="TransactionNo">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName1" HeaderText="Question">
                    <ItemTemplate>
                        <span><%#DataBinder.Eval(Container.DataItem, "QuestionText")%></span>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderText="Answer">
                    <ItemTemplate>
                        <span><%#DataBinder.Eval(Container.DataItem, "QuestionAnswerFormatted")%></span>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RecordDate" UniqueName="RecordDate" />
                <telerik:GridBoundColumn DataField="QuestionFormName" UniqueName="QuestionFormName" />
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true" AllowGroupExpandCollapse="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
