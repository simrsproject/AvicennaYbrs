<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="SanitationActivityResultTemplateList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Master.SanitationActivityResultTemplateList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="SanitationActivityResultID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="65px" DataField="SanitationActivityResultID"
                    HeaderText="ID" UniqueName="SanitationActivityResultID" SortExpression="SanitationActivityResultID"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="WorkTradeItemName" HeaderText="Work Trade Item"
                    UniqueName="WorkTradeItemName" SortExpression="WorkTradeItemName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ResultTemplateName"
                    HeaderText="Template Name" UniqueName="ResultTemplateName" SortExpression="ResultTemplateName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
