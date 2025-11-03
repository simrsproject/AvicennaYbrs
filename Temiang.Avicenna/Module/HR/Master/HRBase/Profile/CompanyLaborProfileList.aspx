<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="CompanyLaborProfileList.aspx.cs" Inherits="Temiang.Avicenna.Module.HR.Master.CompanyLaborProfileList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="CompanyLaborProfileID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="CompanyLaborProfileID"
                    HeaderText="Company Labor Profile ID" UniqueName="CompanyLaborProfileID" SortExpression="CompanyLaborProfileID"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="CompanyLaborProfileCode"
                    HeaderText="Code" UniqueName="CompanyLaborProfileCode"
                    SortExpression="CompanyLaborProfileCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="CompanyLaborProfileName"
                    HeaderText="Company Labor Profile Name" UniqueName="CompanyLaborProfileName"
                    SortExpression="CompanyLaborProfileName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="LastUpdateDateTime"
                    HeaderText="Last Update" UniqueName="LastUpdateDateTime" SortExpression="LastUpdateDateTime"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="LastUpdateByUserID"
                    HeaderText="Update By" UniqueName="LastUpdateByUserID" SortExpression="LastUpdateByUserID"
                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
