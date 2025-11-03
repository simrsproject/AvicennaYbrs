<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="NumberOfBedList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.NumberOfBedList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoViewUrl(sdate) {
                var url = 'NumberOfBedDetail.aspx?md=view&sdate=' + sdate;
                window.location.href = url;
            }

            function gotoViewDetailUrl(sdate, sclass) {
                var url = 'NumberOfBedDetail.aspx?md=view&sdate=' + sdate + '&sclass=' + sclass;
                window.location.href = url;
            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();
                switch (val) {
                    case "new":
                        __doPostBack("<%= grdList.UniqueID %>", 'new');
                        break;
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="New" Value="new" ImageUrl="~/Images/Toolbar/new16.png"
                HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="true" PageSize="15"
        AllowSorting="true" OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView DataKeyNames="StartingDate" ClientDataKeyNames="StartingDate" GroupLoadMode="Client">
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                                                                                                    DataBinder.Eval(Container.DataItem, "StartingDate"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="StartingDate" HeaderText="Starting Date" UniqueName="StartingDate"
                    SortExpression="StartingDate" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="105px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn />
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="detail" DataKeyNames="ClassID" AutoGenerateColumns="false"
                                       GroupLoadMode="Client">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TemplateColumnTrans" HeaderText="">
                            <ItemTemplate>
                                <%# string.Format("<a href=\"#\" onclick=\"gotoViewDetailUrl('{0}','{1}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                        DataBinder.Eval(Container.DataItem, "StartingDate"), DataBinder.Eval(Container.DataItem, "ClassID"))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="35px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ClassID" HeaderText="Class ID"
                                                 UniqueName="ClassID" SortExpression="ClassID" HeaderStyle-HorizontalAlign="Left"
                                                 ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class Name"
                                                 UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                                                 ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn />
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
