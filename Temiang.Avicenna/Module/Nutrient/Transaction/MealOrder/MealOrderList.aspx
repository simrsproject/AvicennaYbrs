<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="MealOrderList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.MealOrderList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
            function gotoViewUrl(id, regno, unitid) {
                var url = 'MealOrderDetail.aspx?md=view&id=' + id + '&regno=' + regno + '&unitid=' + unitid + '&add=<%= IsAdditional %>';
                window.location.href = url;
            }

            function OnClientButtonClicking(sender, args) {
                var val = args.get_item().get_value();

                switch (val) {
                    case "process":
                        __doPostBack("<%= grdList.UniqueID %>", 'process');
                        break;
                    case "printo":
                        __doPostBack("<%= grdList.UniqueID %>", 'printo');
                        break;
                    case "prints":
                        __doPostBack("<%= grdList.UniqueID %>", 'prints');
                        break;
                    case "approved":
                        __doPostBack("<%= grdList.UniqueID %>", 'approved');
                        break;
                    case "unapproved":
                        __doPostBack("<%= grdList.UniqueID %>", 'unapproved');
                        break;
                    case "void":
                        __doPostBack("<%= grdList.UniqueID %>", 'void');
                        break;
                }
            }

            function rowVoid(txno) {
                if (confirm('Are you sure to void meal order for selected patient?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'cancel|' + txno);
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnFilterServiceUnit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                    <telerik:AjaxUpdatedControl ControlID="pnlInfo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process" Value="process" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png"/>
            <telerik:RadToolBarButton runat="server" Text="Print Optional Menu" Value="printo"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print Standard Menu" Value="prints"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Approved" Value="approved" ImageUrl="~/Images/Toolbar/post16.png"
                HoveredImageUrl="~/Images/Toolbar/post16_h.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Un-Approved" Value="unapproved" ImageUrl="~/Images/Toolbar/cancel16.png"
                HoveredImageUrl="~/Images/Toolbar/cancel16_h.png" DisabledImageUrl="~/Images/Toolbar/cancel16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Void" Value="void" ImageUrl="~/Images/Toolbar/delete16.png"
                HoveredImageUrl="~/Images/Toolbar/delete16_h.png" DisabledImageUrl="~/Images/Toolbar/delete16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <asp:Panel ID="pnlInfo" runat="server" Visible="false" BackColor="#FFFFC0" Font-Size="Small"
        BorderColor="#FFC080" BorderStyle="Solid">
        <table width="100%">
            <tr>
                <td width="10px" valign="top">
                    <asp:Image ID="Image6" ImageUrl="~/Images/boundleft.gif" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc:CollapsePanel ID="CollapsePanel1" runat="server" Title="Search Filter">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="300px" AllowCustomText="true"
                                    Filter="Contains" AutoPostBack="True" OnSelectedIndexChanged="cboServiceUnitID_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterServiceUnit" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" MaxLength="20" />
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterRegistration" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOrderDate" runat="server" Text="Order To Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px" />
                            </td>
                            <td style="text-align: left;"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" MaxLength="150" />
                            </td>
                            <td style="text-align: left;">
                                <asp:ImageButton ID="btnFilterName" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="False" PageSize="15"
        AllowSorting="true" OnItemCommand="grdList_ItemCommand" OnItemDataBound="grdList_ItemDataBound"
        OnDetailTableDataBind="grdList_DetailTableDataBind">
        <MasterTableView Name="master" DataKeyNames="RegistrationNo, OrderNo" ClientDataKeyNames="RegistrationNo, OrderNo"
            GroupLoadMode="Client">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                    <SelectFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" HeaderText="Service Unit "></telerik:GridGroupByField>
                    </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="ServiceUnitName" SortOrder="Ascending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="new">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnNewOrder" runat="server" CommandName="neworder" ToolTip='New Order'
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RegistrationNo") %>'>
                            <img src="../../../../Images/Toolbar/new16.png" border="0" />
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsViewVisible").Equals(false) ? string.Empty :
                                                        string.Format("<a href=\"#\" onclick=\"gotoViewUrl('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/Toolbar/views16.png\" border=\"0\" title=\"View\" /></a>",
                                DataBinder.Eval(Container.DataItem, "OrderNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID")))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                    SortExpression="RegistrationDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No"
                    UniqueName="RegistrationNo" SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="40px" DataField="SalutationName" HeaderText=""
                    UniqueName="SalutationName" SortExpression="SalutationName" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Patient Name" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblPatientName" runat="server" Text='<%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName")) %>' /><br />
                        <i>Age : </i>
                        <asp:Label ID="lblAge" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Age") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    HeaderStyle-Width="80px" SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                    HeaderStyle-Width="80px" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class" UniqueName="ClassName"
                    HeaderStyle-Width="80px" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="OrderNo" HeaderText="Order No" UniqueName="OrderNo"
                    SortExpression="OrderNo" Visible="False">
                    <HeaderStyle HorizontalAlign="Center" Width="135px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DietName" HeaderText="Diet" UniqueName="DietName"
                    SortExpression="DietName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="MenuName" HeaderText="Menu" UniqueName="MenuName"
                    SortExpression="MenuName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsFastingMorning"
                    HeaderText="Fast. Morning" UniqueName="IsFastingMorning" SortExpression="IsFastingMorning"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsFastingDay" HeaderText="Fast. Day"
                    UniqueName="IsFastingDay" SortExpression="IsFastingDay" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsFastingNight" HeaderText="Fast. Night"
                    UniqueName="IsFastingNight" SortExpression="IsFastingNight" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="40px" DataField="IsAdditional" HeaderText="Add"
                    UniqueName="IsAdditional" SortExpression="IsAdditional" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsApproved" HeaderText="Approved"
                    UniqueName="IsApproved" SortExpression="IsApproved" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn UniqueName="view" HeaderText="" Groupable="false">
                    <ItemTemplate>
                        <%# (this.IsUserVoidAble.Equals(false) || DataBinder.Eval(Container.DataItem, "IsApproved").Equals(true) || DataBinder.Eval(Container.DataItem, "MenuName").Equals(string.Empty) ? string.Empty :
                            string.Format("<a href=\"#\" onclick=\"rowVoid('{0}'); return false;\">{1}</a>",
                            DataBinder.Eval(Container.DataItem, "OrderNo"), "<img src=\"../../../../Images/Toolbar/delete16.png\" border=\"0\" title=\"Void\" />"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ServiceUnitID" HeaderText="ServiceUnitID" UniqueName="ServiceUnitID"
                    SortExpression="ServiceUnitID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridBoundColumn DataField="BedID" HeaderText="BedID" UniqueName="BedID"
                    SortExpression="BedID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridBoundColumn DataField="ClassID" HeaderText="ClassID" UniqueName="ClassID"
                    SortExpression="ClassID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridBoundColumn DataField="ServiceUnitOrderID" HeaderText="ServiceUnitOrderID"
                    UniqueName="ServiceUnitOrderID" SortExpression="ServiceUnitOrderID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" Visible="False" />
                <telerik:GridBoundColumn DataField="BedOrderID" HeaderText="BedOrderID" UniqueName="BedOrderID"
                    SortExpression="BedOrderID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
                <telerik:GridBoundColumn DataField="ClassOrderID" HeaderText="ClassOrderID" UniqueName="ClassOrderID"
                    SortExpression="ClassOrderID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                    Visible="False" />
            </Columns>
            <DetailTables>
                <telerik:GridTableView DataKeyNames="OrderNo" Name="grdDetail" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="10">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="OrderNo" HeaderText="Order No"
                            UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="ServiceUnitName" HeaderText="Service Unit"
                            UniqueName="ServiceUnitName" SortExpression="ServiceUnitName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="RoomName" HeaderText="Room"
                            UniqueName="RoomName" SortExpression="RoomName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="BedID" HeaderText="Bed No"
                            UniqueName="BedID" SortExpression="BedID" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ClassName" HeaderText="Class"
                            UniqueName="ClassName" SortExpression="ClassName" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" />
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="fw_ajxLoadingPanel"
        EnableEmbeddedScripts="false" HorizontalAlign="NotSet">
    </telerik:RadAjaxPanel>
</asp:Content>
