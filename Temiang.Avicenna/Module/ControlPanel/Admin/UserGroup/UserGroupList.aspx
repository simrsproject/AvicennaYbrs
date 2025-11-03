<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    Codebehind="UserGroupList.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.Admin.UserGroupList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            <!--
    function RowDblClick(sender, args)
    {
        var id=args.getDataKeyValue("UserGroupID");
        window.location.href="UserGroupDetail.aspx?id=" + id;
    }
    -->
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="UserGroupID" ClientDataKeyNames="UserGroupID" >
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="UserGroupID" HeaderText="Group ID"
                    UniqueName="UserGroupID" SortExpression="UserGroupID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="UserGroupName" HeaderText="Group Name"
                    UniqueName="UserGroupName" SortExpression="UserGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="100px" DataField="IsEditAble" HeaderText="Editable" UniqueName="IsEditAble"
                    SortExpression="IsEditAble" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="False" />
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <ClientEvents OnRowDblClick="RowDblClick" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
