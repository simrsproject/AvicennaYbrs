<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="DrugItemPicker.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.DrugItemPicker" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function grdSelection_OnRowDropping(sender, args) {
            if (sender.get_id() == "<%=grdSelection.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=grdSelected.ClientID %>', node)) {
                    args.set_cancel(true);
                }
            }
        }

        function grdSelected_OnRowDropping(sender, args) {
            if (sender.get_id() == "<%=grdSelected.ClientID %>") {
                var node = args.get_destinationHtmlElement();
                if (!isChildOf('<%=grdSelection.ClientID %>', node)) {
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
    <table>
        <tr>
            <td>
                <div style="width: 100%">
                    <asp:Image ID="imgLeft" ImageUrl="~/Images/boundleft.gif" runat="server" />
                    <asp:Label ID="lbl1" runat="server" Text="Note: Drag row item from unselected box to selected item box or from selected box to
                unselected item box"></asp:Label>
                </div>
                <telerik:RadGrid ID="grdSelection" Height="600px" runat="server" AutoGenerateColumns="false"
                    AllowMultiRowSelection="true" AllowSorting="true" OnNeedDataSource="grdSelection_NeedDataSource"
                    OnRowDrop="grdSelection_RowDrop">
                    <MasterTableView DataKeyNames="MedicationReceiveNo">
                        <Columns>
                            <telerik:GridClientSelectColumn HeaderStyle-Width="40px" UniqueName="ClientSelectColumn1" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="MedicationReceiveNo" HeaderText="ID"
                                UniqueName="MedicationReceiveNo" SortExpression="MedicationReceiveNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn DataField="ItemDescription" HeaderText="Unselected Item"
                                UniqueName="ItemDescription" SortExpression="ItemDescription" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ConsumeMethod" HeaderText="Consume Method"
                                UniqueName="ConsumeMethod" SortExpression="ConsumeMethod" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings AllowRowsDragDrop="True">
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="true" />
                        <ClientEvents OnRowDropping="grdSelection_OnRowDropping" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
            <td></td>
            <td>
                <div style="width: 100%">
                    <asp:Image ID="Image3" ImageUrl="~/Images/boundleft.gif" runat="server" />
                    <asp:Label ID="Label1" runat="server" Text="Selected Item"></asp:Label>
                </div>
                <telerik:RadGrid ID="grdSelected" Height="600px" runat="server" AutoGenerateColumns="false"
                    AllowMultiRowSelection="true" AllowSorting="true" OnNeedDataSource="grdSelected_NeedDataSource"
                    OnRowDrop="grdSelected_RowDrop">
                    <MasterTableView DataKeyNames="MedicationReceiveNo">
                        <Columns>
                            <telerik:GridClientSelectColumn HeaderStyle-Width="40px" UniqueName="ClientSelectColumn2" />
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="MedicationReceiveNo" HeaderText="ID"
                                UniqueName="MedicationReceiveNo" SortExpression="MedicationReceiveNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn DataField="ItemDescription" HeaderText="Selected Item"
                                UniqueName="ItemDescription" SortExpression="ItemDescription" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="ConsumeMethod" HeaderText="Consume Method"
                                UniqueName="ConsumeMethod" SortExpression="ConsumeMethod" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings AllowRowsDragDrop="True">
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="true" />
                        <ClientEvents OnRowDropping="grdSelected_OnRowDropping" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>

            </td>
        </tr>
    </table>
</asp:Content>
