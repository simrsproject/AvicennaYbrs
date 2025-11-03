<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ApprovalRangeDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Finance.Master.ApprovalRangeDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblApprovalRangeID" runat="server" Text="Approval Range ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtApprovalRangeID" runat="server" Width="100px" MaxLength="5" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvApprovalRangeID" runat="server" ErrorMessage="Approval Range ID required."
                    ValidationGroup="entry" ControlToValidate="txtApprovalRangeID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblApprovalRangeName" runat="server" Text="Amount From"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtAmountFrom" runat="server" Width="100px" MaxLength="10"
                    MaxValue="9999999999" MinValue="0" NumberFormat-DecimalDigits="0" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvAmountFrom" runat="server" ErrorMessage="Amount From required."
                    ValidationGroup="entry" ControlToValidate="txtAmountFrom" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
           </td>
            <td>
            </td>
        </tr>
                <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Transaction Code"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboTransactionCode" runat="server" Width="300px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Transaction Code required."
                    ValidationGroup="entry" ControlToValidate="cboTransactionCode" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
           </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Item Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRItemType" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cboSRItemType_SelectedIndexChanged"  />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Item Type required."
                    ValidationGroup="entry" ControlToValidate="cboSRItemType" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
           </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblItemGroupID" runat="server" Text="Item Group"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItemGroupID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboItemGroupID_ItemDataBound"
                    OnItemsRequested="cboItemGroupID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "ItemGroupName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1">
        <Tabs>
            <telerik:RadTab runat="server" Text="Approval User" Selected="True" PageViewID="pgApprovalRangeUser">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" BorderStyle="Solid" SelectedIndex="0"
        BorderColor="Gray">
        <telerik:RadPageView ID="pgApprovalRangeUser" runat="server">
            <telerik:RadGrid ID="grdApprovalRangeUser" AllowPaging="true" PageSize="20" runat="server"
                OnNeedDataSource="grdApprovalRangeUser_NeedDataSource" AutoGenerateColumns="False"
                GridLines="None" OnDeleteCommand="grdApprovalRangeUser_DeleteCommand" OnInsertCommand="grdApprovalRangeUser_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ApprovalRangeID, UserID, ApprovalLevel">
                    <Columns>
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="ApprovalLevel" HeaderText="Approval Level"
                            UniqueName="ApprovalLevel" SortExpression="ApprovalLevel" HeaderStyle-HorizontalAlign="Left"
                            ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="UserName" HeaderText="User Approval Name" UniqueName="UserName"
                            SortExpression="UserName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="ApprovalRangeUserDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="ApprovalRangeUserDetailCommand">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu>
                </FilterMenu>
                <ClientSettings EnableRowHoverStyle="true">
                    <Resizing AllowColumnResize="True" />
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
