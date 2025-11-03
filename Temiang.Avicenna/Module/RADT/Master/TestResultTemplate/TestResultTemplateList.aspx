<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="TestResultTemplateList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.TestResultTemplateList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TestResultTemplateID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="65px" DataField="TestResultTemplateID"
                    HeaderText="Template ID" UniqueName="TestResultTemplateID" SortExpression="TestResultTemplateID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ParamedicName" HeaderText="Physician"
                    UniqueName="ParamedicName" SortExpression="ParamedicName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item"
                    UniqueName="ItemName" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TestResultTemplateName"
                    HeaderText="Template Name" UniqueName="TestResultTemplateName" SortExpression="TestResultTemplateName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TestResult" HeaderText="Result" UniqueName="TestResult"
                    SortExpression="TestResult" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="False" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
