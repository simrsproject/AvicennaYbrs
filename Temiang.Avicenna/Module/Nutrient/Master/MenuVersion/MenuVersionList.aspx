<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="MenuVersionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.MenuVersionList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(ext) {
                var url = "MenuVersionDetail.aspx?md=new&ext=" + ext;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="VersionID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VersionID" HeaderText="Version ID"
                    UniqueName="VersionID" SortExpression="VersionID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="VersionName" HeaderText="Version Name"
                    UniqueName="VersionName" SortExpression="VersionName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Cycle"
                    HeaderText="Cycle" UniqueName="Cycle" SortExpression="Cycle"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="120px" DataField="IsPlusOneRule" HeaderText="Using +1 Rule"
                    UniqueName="IsPlusOneRule" SortExpression="IsPlusOneRule" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>