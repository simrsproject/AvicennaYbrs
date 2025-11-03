<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="ItemTariffRequest2List.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Tariff.ItemTariffRequest2List" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(type) {
                var url = "ItemTariffRequest2Detail.aspx?md=new&type=" + type;
                window.location.href = url;
            }
            function onClientClose(oWnd) {
                if (oWnd.argument && oWnd.argument.command == 'ok') {
                    var prefix = (oWnd.argument.initial == "Yes") ? '../' : '';
                    var url = prefix + '<%= UrlPageDetail %>?md=view&id=' + oWnd.argument.trno;
                    window.location = url;
                }
            }
            function openWinImport() {
                var oWnd = $find("<%= winOpen.ClientID %>");
                oWnd.setUrl("ItemTariffRequestImport.aspx?type=<%= Request.QueryString["type"] %>");
                oWnd.show();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow runat="server" Animation="None" Width="500px" Height="350px" Behavior="Close, Move"
        ShowContentDuringLoad="False" VisibleStatusbar="False" Modal="true" AutoSize="false"
        ReloadOnShow="true" OnClientClose="onClientClose" ID="winOpen">
    </telerik:RadWindow>
    
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="TariffRequestNo">
            <Columns>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="TariffRequestNo" HeaderText="Request No"
                    UniqueName="TariffRequestNo" SortExpression="TariffRequestNo" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="TariffRequestDate"
                    HeaderText="Request Date" UniqueName="TariffRequestDate" SortExpression="TariffRequestDate"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="TariffTypeName" HeaderText="Tariff Type"
                    UniqueName="TariffTypeName" SortExpression="TariffTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ItemTypeName" HeaderText="Item Type"
                    UniqueName="ItemTypeName" SortExpression="ItemTypeName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="350px" DataField="ItemGroupName" HeaderText="Item Group"
                    UniqueName="ItemGroupName" SortExpression="ItemGroupName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="StartingDate" HeaderText="Starting Date"
                    UniqueName="StartingDate" SortExpression="StartingDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsImport" HeaderText="Import"
                    UniqueName="IsImport" SortExpression="IsImport" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" /> 
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridDateTimeColumn HeaderStyle-Width="80px" DataField="ApprovedDate" HeaderText="Approved Date"
                    UniqueName="ApprovedDate" SortExpression="ApprovedDate" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="TariffRequestNo,ClassID,ItemID" Name="grdItem"
                    AutoGenerateColumns="False">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ItemID" HeaderText="ID"
                            UniqueName="ItemID" SortExpression="ItemID" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ItemName" HeaderText="Item Name"
                            UniqueName="ItemName" SortExpression="ItemName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClassName" HeaderText="Class"
                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                            UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                        <telerik:GridTemplateColumn />
                    </Columns>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="TariffComponentID" Name="grdItemTariffRequestItemComp"
                            Width="500px" AutoGenerateColumns="false">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="TariffComponentName"
                                    HeaderText="Component Name" UniqueName="TariffComponentName" SortExpression="TariffComponentName"
                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Price" HeaderText="Price"
                                    UniqueName="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowDiscount"
                                    HeaderText="Discount" UniqueName="IsAllowDiscount" SortExpression="IsAllowDiscount"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsAllowVariable"
                                    HeaderText="Variable" UniqueName="IsAllowVariable" SortExpression="IsAllowVariable"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
