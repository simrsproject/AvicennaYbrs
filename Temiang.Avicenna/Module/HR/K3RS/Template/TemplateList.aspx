<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.K3RS.TemplateList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TemplateID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="65px" DataField="TemplateID"
                    HeaderText="ID" UniqueName="TemplateID" SortExpression="TemplateID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="TemplateName"
                    HeaderText="Template Name" UniqueName="TemplateName" SortExpression="TemplateName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>