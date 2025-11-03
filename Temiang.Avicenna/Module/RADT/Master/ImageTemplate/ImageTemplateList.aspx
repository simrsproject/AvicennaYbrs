<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ImageTemplateList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ImageTemplateList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ImageTemplateID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ImageTemplateID" HeaderText="ID"
                    UniqueName="ImageTemplateID" SortExpression="ImageTemplateID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ImageTemplateName" HeaderText="Name"
                    UniqueName="ImageTemplateName" SortExpression="ImageTemplateName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description"
                    UniqueName="ImageTemplateName" SortExpression="Description" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Image Template">
                    <ItemTemplate>
                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" AlternateText="" DataValue='<%# DataBinder.Eval(Container.DataItem,"Image") == DBNull.Value? new System.Byte[0]: DataBinder.Eval(Container.DataItem,"Image") %>'
                            Width="100"
                            Height="100"
                            ResizeMode="Fill"></telerik:RadBinaryImage>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
