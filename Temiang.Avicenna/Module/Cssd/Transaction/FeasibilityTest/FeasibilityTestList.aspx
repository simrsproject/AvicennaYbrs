<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="FeasibilityTestList.aspx.cs" Inherits="Temiang.Avicenna.Module.Cssd.Transaction.FeasibilityTestList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function viewItemDetail(rno, seqNo, itemId, itemName, srItemUnit, qty, type) {
                var oWnd = $find("<%= winDetailItem.ClientID %>");

                oWnd.setUrl('../SterileItemsReceived/SterileItemsReceivedDetailItemInfo.aspx?itemid=' + itemId + '&itemname=' + itemName + '&unit=' + srItemUnit + '&qty=' + qty + '&rno=' + rno + '&seq=' + seqNo + '&type=' + type + '&from=fts');

                oWnd.set_width(document.body.offsetWidth);
                oWnd.show();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="600px" Height="500px" Behavior="Move, Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" ID="winDetailItem">
    </telerik:RadWindow>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind" AllowPaging="true" PageSize="15"
        AutoGenerateColumns="false">
        <MasterTableView DataKeyNames="FeasibilityTestNo" GroupLoadMode="Client">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="FeasibilityTestNo" HeaderText="Transaction No"
                    UniqueName="FeasibilityTestNo" SortExpression="FeasibilityTestNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="FeasibilityTestDate" HeaderText="Date"
                    UniqueName="FeasibilityTestDate" SortExpression="FeasibilityTestDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="FeasibilityTestTime" HeaderText="Time"
                    UniqueName="FeasibilityTestTime" SortExpression="FeasibilityTestTime" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="Notes" HeaderText="Notes"
                    UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="FeasibilityTestNo, FeasibilityTestSeqNo" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="15">
                    <ColumnGroups>
                        <telerik:GridColumnGroup HeaderText="Package Item Info" Name="PackageItemInfo" HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="listDetailView" HeaderText="" Groupable="false">
                            <ItemTemplate>
                                <%# (string.Format("<a href=\"#\" onclick=\"viewItemDetail('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', 'info'); return false;\">{6}</a>",
                           DataBinder.Eval(Container.DataItem, "ReceivedNo"), DataBinder.Eval(Container.DataItem, "ReceivedSeqNo"), DataBinder.Eval(Container.DataItem, "ItemID"), DataBinder.Eval(Container.DataItem, "ItemName"),
                           DataBinder.Eval(Container.DataItem, "CssdItemUnit"), DataBinder.Eval(Container.DataItem, "Qty"), "<img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"View Item Detail\" />"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="70px" DataField="FeasibilityTestSeqNo" HeaderText="Seq No"
                            UniqueName="FeasibilityTestSeqNo" SortExpression="FeasibilityTestSeqNo" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ReceivedNo" HeaderText="Received No"
                            UniqueName="ReceivedNo" SortExpression="ReceivedNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemNo" HeaderText="Item #"
                            UniqueName="ItemNo" SortExpression="ItemNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" Visible="false" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="Item ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="80px" DataField="Qty" HeaderText="Qty"
                            UniqueName="Qty" SortExpression="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="CssdItemUnit" HeaderText="Unit"
                            UniqueName="CssdItemUnit" SortExpression="CssdItemUnit" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />

                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsFeasibilityTestPassed" HeaderText="Passed"
                            UniqueName="IsFeasibilityTestPassed" SortExpression="IsFeasibilityTestPassed" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBrokenInstrument" HeaderText="Broken Instrument"
                            UniqueName="IsBrokenInstrument" SortExpression="IsBrokenInstrument" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyReplacements" HeaderText="Qty Replacements"
                            UniqueName="QtyReplacements" SortExpression="QtyReplacements" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" />
                        <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsBrokenInstrumentDetail" HeaderText="Broken Instrument"
                            UniqueName="IsBrokenInstrumentDetail" SortExpression="IsBrokenInstrumentDetail" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" ColumnGroupName="PackageItemInfo" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyReplacementsDetail" HeaderText="Qty Replacements"
                            UniqueName="QtyReplacementsDetail" SortExpression="QtyReplacementsDetail" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:n2}" ColumnGroupName="PackageItemInfo" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
