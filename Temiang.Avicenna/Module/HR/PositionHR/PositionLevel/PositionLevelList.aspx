<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PositionLevelList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionLevelList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="PositionLevelID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionLevelID"
                    HeaderText="Position Level ID" UniqueName="PositionLevelID" SortExpression="PositionLevelID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PositionLevelCode"
                    HeaderText="Code" UniqueName="PositionLevelCode" SortExpression="PositionLevelCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PositionLevelName"
                    HeaderText="Position Level Name" UniqueName="PositionLevelName" SortExpression="PositionLevelName"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Ranking" HeaderText="Ranking"
                    UniqueName="Ranking" SortExpression="Ranking" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" />
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
