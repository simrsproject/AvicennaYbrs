<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterCustom.Master"
    AutoEventWireup="true" CodeBehind="DistributionPortionList.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.DistributionPortionList" %>

<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script type="text/javascript">
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
                }
            }

            function rowVoid(orderNo) {
                if (confirm('Are you sure to void for selected order?')) {
                    __doPostBack("<%= grdList.UniqueID %>", 'void|' + orderNo);
                }
            }

            function rowDistributed(orderNo) {
                __doPostBack("<%= grdList.UniqueID %>", 'distributed|' + orderNo);
            }

            function gotoMealOrderChangeUrl(id, regno, unitid) {
                var oms = $find("<%= cboSRMealSet.ClientID %>");
                var url = '../MealOrderChange/MealOrderChangeDetail.aspx?md=view&id=' + id + '&regno=' + regno + '&unitid=' + unitid + '&mealset=' + oms._value + '&type=dc&fdp=y';
                window.location.href = url;
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
            <telerik:AjaxSetting AjaxControlID="cboServiceUnitID">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterRegistration">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilterSRMealSet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboSRMealSet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="grdList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnClientButtonClicking="OnClientButtonClicking">
        <Items>
            <telerik:RadToolBarButton runat="server" Text="Process" Value="process" ImageUrl="~/Images/Toolbar/process16.png"
                HoveredImageUrl="~/Images/Toolbar/process16_h.png" DisabledImageUrl="~/Images/Toolbar/process16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print Optional Menu" Value="printo"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
            <telerik:RadToolBarButton runat="server" Text="Print Standard Menu" Value="prints"
                ImageUrl="~/Images/Toolbar/print16.png" HoveredImageUrl="~/Images/Toolbar/print16_h.png"
                DisabledImageUrl="~/Images/Toolbar/print16_d.png" />
        </Items>
    </telerik:RadToolBar>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
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
                                    OnClick="btnFilter_Click" ToolTip="Search" Visible="False" />
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
                <td width="50%" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblOrderDate" runat="server" Text="Date"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadDatePicker ID="txtOrderDate" runat="server" Width="100px" Enabled="true" />
                            </td>
                            <td style="text-align: left;"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSRMealSet" runat="server" Text="Set"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadComboBox runat="server" ID="cboSRMealSet" Width="300px" AutoPostBack="True"
                                    OnSelectedIndexChanged="cboSRMealSet_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                            <td style="text-align: left">
                                <asp:ImageButton ID="btnFilterSRMealSet" runat="server" ImageUrl="~/Images/Toolbar/search16.png"
                                    OnClick="btnFilter_Click" ToolTip="Search" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </cc:CollapsePanel>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource"
        GridLines="None" AutoGenerateColumns="false" AllowPaging="False" PageSize="15"
        AllowSorting="true" OnItemCommand="grdList_ItemCommand">
        <MasterTableView DataKeyNames="OrderNo" ClientDataKeyNames="OrderNo" GroupLoadMode="Client">
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
                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" HeaderStyle-Width="30px">
                    <HeaderTemplate>
                        <asp:CheckBox ID="headerChkbox" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True"
                            runat="server"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="detailChkbox" runat="server" Checked='<%#Eval("IsDistributed")%>'></asp:CheckBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%# ((DataBinder.Eval(Container.DataItem, "IsDistributed").Equals(true) || DataBinder.Eval(Container.DataItem, "IsCheckInvalid").Equals(true)|| DataBinder.Eval(Container.DataItem, "IsCheckAteCtl").Equals(true)) ? string.Empty :
                                    string.Format("<a href=\"#\" onclick=\"rowDistributed('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/process16.png\" border=\"0\" title=\"Distributed\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "OrderNo")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="Print">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnPrint" runat="server" CommandName="print" ToolTip='Print'
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderNo") %>'>
                            <img src="../../../../Images/Toolbar/print16.png" border="0" />
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="140px" HeaderText="OrderNo"
                    UniqueName="OrderNo" SortExpression="OrderNo" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "OrderNo")%>&nbsp;
                        <%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsDietChanged")) &&  DataBinder.Eval(Container.DataItem, "IsBeenProcessed").Equals(false)? "<img src=\"../../../../Images/Animated/warning16.gif\" border=\"0\" alt=\"Diet Changed\" title=\"Diet Changed\" />" : string.Empty%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RoomName" HeaderText="Room" UniqueName="RoomName"
                    SortExpression="RoomName">
                    <HeaderStyle HorizontalAlign="Left" Width="160px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                    HeaderStyle-Width="80px" SortExpression="BedID" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderText="Diet" UniqueName="TemplateItemDiet">
                    <ItemTemplate>
                        <asp:Label ID="lblDietName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DietName") %>' /><br />
                        <i>Complication : </i>
                        <asp:Label ID="lblDietComplicationName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DietComplicationName") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn DataField="DietName" HeaderText="Diet" UniqueName="DietName"
                    SortExpression="DietName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />--%>
                <telerik:GridBoundColumn DataField="MenuName" HeaderText="Menu" UniqueName="MenuName"
                    SortExpression="MenuName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsDistributed" HeaderText="Distributed"
                    UniqueName="IsDistributed" SortExpression="IsDistributed" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "IsDietChanged").Equals(false) || DataBinder.Eval(Container.DataItem, "IsDistributed").Equals(true) ? string.Empty :
                                     string.Format("<a href=\"#\" onclick=\"gotoMealOrderChangeUrl('{0}','{1}','{2}'); return false;\"><img src=\"../../../../Images/reload_refresh_arrow.png\" border=\"0\" title=\"Meal Order Change\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "OrderNo"), DataBinder.Eval(Container.DataItem, "RegistrationNo"), DataBinder.Eval(Container.DataItem, "ServiceUnitID")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="50px" DataField="IsVoid" HeaderText="Void"
                    UniqueName="IsVoid" SortExpression="IsVoid" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn Groupable="false" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <%# ((DataBinder.Eval(Container.DataItem, "IsDistributed").Equals(false) || DataBinder.Eval(Container.DataItem, "IsVoid").Equals(true) || DataBinder.Eval(Container.DataItem, "IsCheckInvalid").Equals(true)|| DataBinder.Eval(Container.DataItem, "IsCheckAteCtl").Equals(true)) ? string.Empty :
                                     string.Format("<a href=\"#\" onclick=\"rowVoid('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/row_delete16.png\" border=\"0\" title=\"Void\" /></a>",
                                    DataBinder.Eval(Container.DataItem, "OrderNo")))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
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
