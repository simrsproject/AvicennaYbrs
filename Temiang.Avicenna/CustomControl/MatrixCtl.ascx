<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MatrixCtl.ascx.cs" Inherits="Temiang.Avicenna.CustomControl.MatrixCtl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadScriptBlock runat="server" ID="scriptBlock">

    <script type="text/javascript">
        function grdSource_OnRowDropping(sender, args) {
            if (sender.get_id() == "<%=grdSource.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=grdSelected.ClientID %>', node)) {
                    args.set_cancel(true);
                }
            }
        }

        function grdSelected_OnRowDropping(sender, args) {
            if (sender.get_id() == "<%=grdSelected.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=grdSource.ClientID %>', node) && !isChildOf('<%=grdSelected.ClientID %>', node)) {
                    args.set_cancel(true);
                }
            }
        }

        function isChildOf(parentId, element) {
            while (element) {
                if (element.id && element.id.indexOf(parentId) > -1) {
                    return true;
                }
                element = element.parentNode;
            }
            return false;
        }
    </script>

</telerik:RadScriptBlock>
<table width="100%">
    <tr>
        <td valign="top" style="width: 45%">
            <div style="width: 100%">
                <asp:Image ID="imgLeft" ImageUrl="~/Images/boundleft.gif" runat="server" />
                <asp:Label ID="lbl1" runat="server" Text="Avialable Record"></asp:Label>
            </div>
            <telerik:RadGrid ID="grdSource" Width="100%" runat="server" AutoGenerateColumns="false"
                AllowMultiRowSelection="true" AllowSorting="true" AllowPaging="True" PageSize="20" OnNeedDataSource="grdSource_NeedDataSource"
                OnRowDrop="grdSource_RowDrop" AllowFilteringByColumn="true">
                <MasterTableView>
                    <Columns>
                        <telerik:GridClientSelectColumn HeaderStyle-Width="40px" UniqueName="ClientSelectColumn1" />
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" HeaderText="ID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <ClientSettings AllowRowsDragDrop="True">
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="true" />
                    <ClientEvents OnRowDropping="grdSource_OnRowDropping" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="400px" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
        <td style="width: 10%">
            <center>
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnMoveRightAll" Text=">>" Width="35px" OnClick="btnMoveRightAll_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnMoveRight" Text=">" Width="35px" OnClick="btnMoveRight_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnMoveLeft" Text="<" Width="35px" OnClick="btnMoveLeft_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnMoveLeftAll" Text="<<" Width="35px" OnClick="btnMoveLeftAll_Click" />
                        </td>
                    </tr>
                </table>
            </center>
        </td>
        <td valign="top" style="width: 45%">
            <div style="width: 100%">
                <asp:Image ID="Image3" ImageUrl="~/Images/boundleft.gif" runat="server" />
                <asp:Label ID="Label1" runat="server" Text="Selected Record"></asp:Label>
            </div>
            <telerik:RadGrid ID="grdSelected" Width="100%" runat="server" AutoGenerateColumns="false"
                AllowMultiRowSelection="true" AllowSorting="false" OnNeedDataSource="grdSelected_NeedDataSource"
                OnRowDrop="grdSelected_RowDrop" AllowFilteringByColumn="true">
                <MasterTableView>
                    <Columns>
                        <telerik:GridClientSelectColumn HeaderStyle-Width="40px" UniqueName="ClientSelectColumn2" />
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" HeaderText="ID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
                <ClientSettings AllowRowsDragDrop="True" AllowColumnsReorder="false">
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="true" />
                    <ClientEvents OnRowDropping="grdSelected_OnRowDropping" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="400px" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
