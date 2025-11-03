<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="ProcedureList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ProcedureList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ProcedureID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ProcedureID" HeaderText="ID"
                    UniqueName="ProcedureID" SortExpression="ProcedureID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProcedureName" HeaderText="Procedure Name"
                    UniqueName="ProcedureName" SortExpression="ProcedureName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
