<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="TherapyList.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.TherapyList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="TherapyID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TherapyID" HeaderText="Therapy ID"
                    UniqueName="TherapyID" SortExpression="TherapyID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TherapyName" HeaderText="Therapy Name" UniqueName="TherapyName"
                    SortExpression="TherapyName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="TherapyGroupName" HeaderText="Group"
                    UniqueName="TherapyGroupName" SortExpression="TherapyGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
