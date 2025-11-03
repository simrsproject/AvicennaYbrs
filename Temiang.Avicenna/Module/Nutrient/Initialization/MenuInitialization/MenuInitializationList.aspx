<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="MenuInitializationList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.MenuInitializationList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(ext) {
                var url = "MenuInitializationDetail.aspx?md=new&ext=" + ext;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="StartingDate">
            <Columns>
                <telerik:GridDateTimeColumn HeaderStyle-Width="100px" DataField="StartingDate" HeaderText="Starting Date"
                    UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="VersionID" HeaderText="Version ID"
                    UniqueName="VersionID" SortExpression="VersionID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="VersionName" HeaderText="Version"
                    UniqueName="VersionName" SortExpression="VersionName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="SeqNo" HeaderText="Seq No" UniqueName="SeqNo"
                    SortExpression="SeqNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
