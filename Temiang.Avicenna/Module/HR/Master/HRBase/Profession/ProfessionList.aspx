<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="ProfessionList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.HRBase.ProfessionList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID"
                    HeaderText="Code" UniqueName="ItemID" SortExpression="ItemID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName"
                    HeaderText="Profession Type" UniqueName="ItemName" SortExpression="ItemName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProfessionGroupName"
                    HeaderText="Profession Group" UniqueName="ProfessionGroupName" SortExpression="ProfessionGroupName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="150px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
