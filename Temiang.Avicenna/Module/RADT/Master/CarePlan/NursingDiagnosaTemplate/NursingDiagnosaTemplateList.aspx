<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="NursingDiagnosaTemplateList.aspx.cs" Inherits="Temiang.Avicenna.Module.NursingCare.Master.NursingDiagnosaTemplateList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TemplateID">
            <Columns>
                <telerik:GridBoundColumn DataField="TemplateName" HeaderText="Template Name"
                    UniqueName="TemplateName" SortExpression="TemplateName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="130" />
                <telerik:GridBoundColumn DataField="TemplateText" HeaderText="Template Text"
                    UniqueName="TemplateText" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
