<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="BodyDiagramList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.BodyDiagramList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="BodyID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="BodyID" HeaderText="ID"
                    UniqueName="BodyID" SortExpression="BodyID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="BodyName" HeaderText="Body Name"
                    UniqueName="BodyName" SortExpression="BodyName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description"
                    UniqueName="BodyName" SortExpression="Description" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn UniqueName="TemplateItemName2" HeaderText="Body Image">
                    <ItemTemplate>
                        <telerik:RadBinaryImage ID="RadBinaryImage1" runat="server" AlternateText="" DataValue='<%# DataBinder.Eval(Container.DataItem,"BodyImage") == DBNull.Value? new System.Byte[0]: DataBinder.Eval(Container.DataItem,"BodyImage") %>'
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
