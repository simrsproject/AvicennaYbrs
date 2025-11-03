<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="LiquidFoodSettingDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Initialization.LiquidFoodSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openTime(itemId) {
                var stdId = $find("<%= txtStandardReferenceID.ClientID %>");
                var oWnd = $find("<%= winOpen.ClientID %>");

                oWnd.setUrl('LiquidFoodTimeSettingDetail.aspx?stdId=' + stdId.get_value() + "&itemId=" + itemId);
                oWnd.set_title('Time Setting');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winOpen" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblStandardReferenceID" runat="server" Text="ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStandardReferenceID" runat="server" Width="100px" MaxLength="10"
                    ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvStandardReferenceID" runat="server" ErrorMessage="ID required."
                    ValidationGroup="entry" ControlToValidate="txtStandardReferenceID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblStandardReferenceName" runat="server" Text="Description"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStandardReferenceName" runat="server" Width="300px" ReadOnly="True" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvStandardReferenceName" runat="server" ErrorMessage="Description required."
                    ValidationGroup="entry" ControlToValidate="txtStandardReferenceName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
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
        <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="ItemID" HeaderText="Code"
                    UniqueName="ItemID" SortExpression="ItemID" />
                <telerik:GridBoundColumn DataField="ItemName" HeaderText="Name" UniqueName="ItemName"
                    SortExpression="ItemName" />
                <telerik:GridBoundColumn DataField="ReferenceID" HeaderText="Formula ID (Adults)" UniqueName="ReferenceID"
                    SortExpression="ReferenceID" /> 
                <telerik:GridBoundColumn DataField="Note" HeaderText="Formula ID (Children)" UniqueName="Note"
                    SortExpression="Note" />        
                <telerik:GridTemplateColumn UniqueName="process">
                    <ItemTemplate>
                        <%# string.Format("<a href=\"#\" onclick=\"openTime('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/new16.png\" border=\"0\" title=\"Open Per Time\" /></a>",
                                                                                                        DataBinder.Eval(Container.DataItem, "ItemID"))%>
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
            <EditFormSettings UserControlName="LiquidFoodSettingItemDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="LiquidFoodSettingEditCommand">
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
