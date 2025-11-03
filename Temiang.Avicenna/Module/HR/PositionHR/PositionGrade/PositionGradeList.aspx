<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="PositionGradeList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.PositionInformation.PositionGradeList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="PositionGradeID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="PositionGradeID"
                    HeaderText="Position Grade ID" UniqueName="PositionGradeID" SortExpression="PositionGradeID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PositionGradeCode"
                    HeaderText="Code" UniqueName="PositionGradeCode" SortExpression="PositionGradeCode"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="PositionGradeName" HeaderText="Position Grade Name"
                    UniqueName="PositionGradeName" SortExpression="PositionGradeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="Interval" HeaderText="Interval"
                    UniqueName="Interval" SortExpression="Interval" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Ranking" HeaderText="Ranking"
                    UniqueName="Ranking" SortExpression="Ranking" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn DataField="RankName" HeaderText="Rank Name" UniqueName="RankName"
                    SortExpression="RankName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="EmploymentTypeName" HeaderText="Employment Type" UniqueName="EmploymentTypeName"
                    SortExpression="EmploymentTypeName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
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
