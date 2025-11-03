<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true" CodeBehind="FoodList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Master.FoodList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "FoodDetail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="FoodID" ClientDataKeyNames="FoodID"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="FoodType" HeaderText="Type "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="SRFoodGroup2" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="FoodGroup1" HeaderText="Group "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="FoodGroup1" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="FoodID" HeaderText="Food ID"
                    UniqueName="FoodID" SortExpression="FoodID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="FoodName" HeaderText="Food Name"
                    UniqueName="FoodName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Weight"
                    HeaderText="Weight" UniqueName="Weight" SortExpression="Weight"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemUnit" HeaderText="Unit"
                    UniqueName="ItemUnit" SortExpression="ItemUnit" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="QtyPortion"
                    HeaderText="Qty Portion" UniqueName="QtyPortion" SortExpression="Weight"
                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsPackage" HeaderText="Package"
                    UniqueName="IsPackage" SortExpression="IsPackage" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="FoodDetailID" Name="grdDetail" AutoGenerateColumns="False"
                    ShowFooter="true" AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="80px" DataField="FoodDetailID" HeaderText="ID"
                            UniqueName="FoodDetailID" SortExpression="FoodDetailID" />
                        <telerik:GridBoundColumn DataField="FoodDetailName" HeaderText="Food Detail Name" UniqueName="FoodDetailName"
                            SortExpression="FoodDetailName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
