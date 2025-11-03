<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ItemLaboratoryList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ItemLaboratoryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function gotoEditUrl(id, ParentItemID) {
            var url = 'ItemLaboratoryDetail.aspx?md=edit&id=' + id + '&parentid=' + ParentItemID;
            window.location.href = url;
        }
    </script>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" OnItemCommand="grdList_ItemCommand">
        <MasterTableView DataKeyNames="ItemID">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                    UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Item Name" UniqueName="ItemName"
                    SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ItemGroupName" HeaderText="Group" UniqueName="ItemGroupName"
                    SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ReportRLID" HeaderText="Report RL" UniqueName="ReportRLID"
                    SortExpression="ReportRLID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAdminCalculation"
                    HeaderText="Admin Calculation" UniqueName="IsAdminCalculation" SortExpression="IsAdminCalculation"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowVariable"
                    HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowCito" HeaderText="Cito"
                    UniqueName="IsAllowCito" SortExpression="IsAllowCito" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowDiscount"
                    HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAssetUtilization"
                    HeaderText="Asset" UniqueName="IsAssetUtilization" SortExpression="IsAssetUtilization"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="ParentItemID,DetailItemID,DisplaySequence"
                    AutoGenerateColumns="false" GroupLoadMode="Client">
                    <SortExpressions>
                        <telerik:GridSortExpression FieldName="DisplaySequence" SortOrder="Ascending" />
                    </SortExpressions>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "DetailItemID"), DataBinder.Eval(Container.DataItem, "ParentItemID"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnOrder" HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnOrder" CommandName="order" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParentItemID") + "|" + DataBinder.Eval(Container.DataItem, "DetailItemID")%>'>
                                    <img alt="" src="../../../../Images/Toolbar/arrowup_blue16.png" border="0" title="Order" />    
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DisplaySequence" HeaderText="Sequence"
                            UniqueName="DisplaySequence" SortExpression="DisplaySequence" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Detail Item ID"
                            UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="DetailItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                            SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView Name="detail2" DataKeyNames="ParentItemID,DetailItemID,DisplaySequence"
                            AutoGenerateColumns="false" GroupLoadMode="Client">
                            <SortExpressions>
                                <telerik:GridSortExpression FieldName="DisplaySequence" SortOrder="Ascending" />
                            </SortExpressions>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                                    <ItemTemplate>
                                        <%# string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "DetailItemID"), DataBinder.Eval(Container.DataItem, "ParentItemID"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumnOrder" HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnOrder" CommandName="order" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParentItemID") + "|" + DataBinder.Eval(Container.DataItem, "DetailItemID")%>'>
                                    <img alt="" src="../../../../Images/Toolbar/arrowup_blue16.png" border="0" title="Order" />    
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DisplaySequence" HeaderText="Sequence"
                                    UniqueName="DisplaySequence" SortExpression="DisplaySequence" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Detail Item ID"
                                    UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridBoundColumn DataField="DetailItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                                    SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            </Columns>
                            <DetailTables>
                                <telerik:GridTableView Name="detail3" DataKeyNames="ParentItemID,DetailItemID,DisplaySequence"
                                    AutoGenerateColumns="false" GroupLoadMode="Client">
                                    <SortExpressions>
                                        <telerik:GridSortExpression FieldName="DisplaySequence" SortOrder="Ascending" />
                                    </SortExpressions>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                                            <ItemTemplate>
                                                <%# string.Format("<a href=\"#\" onclick=\"gotoEditUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/edit16.png\" border=\"0\" title=\"Edit\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "DetailItemID"), DataBinder.Eval(Container.DataItem, "ParentItemID"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumnOrder" HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtnOrder" CommandName="order" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ParentItemID") + "|" + DataBinder.Eval(Container.DataItem, "DetailItemID")%>'>
                                    <img alt="" src="../../../../Images/Toolbar/arrowup_blue16.png" border="0" title="Order" />    
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DisplaySequence" HeaderText="Sequence"
                                            UniqueName="DisplaySequence" SortExpression="DisplaySequence" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DetailItemID" HeaderText="Detail Item ID"
                                            UniqueName="DetailItemID" SortExpression="DetailItemID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                        <telerik:GridBoundColumn DataField="DetailItemName" HeaderText="Item Name" UniqueName="DetailItemName"
                                            SortExpression="DetailItemName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                        </telerik:GridTableView>
                    </DetailTables>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
