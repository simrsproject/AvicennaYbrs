<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="LocationTemplateList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.LocationTemplateList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TemplateNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TemplateNo" HeaderText="Template No"
                    UniqueName="TemplateNo" SortExpression="TemplateNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TemplateName" HeaderText="Template Name" UniqueName="TemplateName"
                    SortExpression="TemplateName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="LocationID" HeaderText="LocationID"
                    UniqueName="LocationID" SortExpression="LocationID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn DataField="LocationName" HeaderText="LocationName"
                    UniqueName="LocationName" SortExpression="LocationName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
