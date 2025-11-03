<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ClosingList.aspx.cs" Inherits="Temiang.Avicenna.Module.AssetManagement.Process.ClosingList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" AllowPaging="true">
        <MasterTableView DataKeyNames="PostingId">
            <Columns>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsClosed" HeaderText="Closed" 
                    UniqueName="IsClosed" AllowSorting="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn ItemStyle-Wrap="false" HeaderStyle-Width="120px" DataField="Periode" HeaderText="Periode" 
                    UniqueName="Periode" AllowSorting="false" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="120px" DataField="DateEdited" HeaderText="Date Edited" 
                    UniqueName="DateEdited" AllowSorting="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="EditedBy" AllowSorting="false" 
                    HeaderText="Edited By" UniqueName="EditedBy" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
