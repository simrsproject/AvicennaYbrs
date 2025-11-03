<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="LiquidFoodDietSettingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.LiquidFoodDietSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openGender(time) {
                var dietId = $find("<%= txtDietID.ClientID %>");
                var oWnd = $find("<%= winOpen.ClientID %>");

                oWnd.setUrl('LiquidFoodDietTimeGenderSettingDetail.aspx?dietId=' + dietId.get_value() + "&time=" + time);
                oWnd.set_title('Gender Setting');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winOpen" Animation="None" Width="800px" Height="500px" runat="server"
        Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDietID" runat="server" Text="Diet ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDietID" runat="server" Width="100px" MaxLength="10" ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDietID" runat="server" ErrorMessage="ID required."
                    ValidationGroup="entry" ControlToValidate="txtDietID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDietName" runat="server" Text="Diet Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDietName" runat="server" Width="300px" ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDietName" runat="server" ErrorMessage="Diet Name required."
                    ValidationGroup="entry" ControlToValidate="txtDietName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblFoodID" runat="server" Text="Formula (Adults)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboFoodID" Width="300px" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound" OnItemsRequested="cboFoodID_ItemsRequested"
                    ValidationGroup="other">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "FoodName")%>
                            &nbsp;(<%# DataBinder.Eval(Container.DataItem, "FoodID")%>) </b>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvFoodID" runat="server" ErrorMessage="Formula (Adults) required."
                    ValidationGroup="entry" ControlToValidate="cboFoodID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblChildrenFoodID" runat="server" Text="Formula (Children)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboChildrenFoodID" Width="300px" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" OnItemDataBound="cboFoodID_ItemDataBound" OnItemsRequested="cboFoodID_ItemsRequested"
                    ValidationGroup="other">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "FoodName")%>
                            &nbsp;(<%# DataBinder.Eval(Container.DataItem, "FoodID")%>) </b>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 20 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvChildrenFoodID" runat="server" ErrorMessage="Formula (Children) required."
                    ValidationGroup="entry" ControlToValidate="cboChildrenFoodID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdItem_UpdateCommand"
        OnInsertCommand="grdItem_InsertCommand" OnDeleteCommand="grdItem_DeleteCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="Time">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="Time" HeaderText="Time"
                    UniqueName="Time" SortExpression="Time" />
                <telerik:GridBoundColumn DataField="FoodName" HeaderText="Formula (Adults)" UniqueName="FoodName"
                    SortExpression="FoodName" />
                <telerik:GridBoundColumn DataField="ChildrenFoodName" HeaderText="Formula (Children)"
                    UniqueName="ChildrenFoodName" SortExpression="ChildrenFoodName" />
                <telerik:GridTemplateColumn UniqueName="process">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openGender('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Open Per Gender\" /></a>",
                                                                                                        DataBinder.Eval(Container.DataItem, "Time"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="LiquidFoodDietSettingItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="LiquidFoodDietSettingEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings>
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
