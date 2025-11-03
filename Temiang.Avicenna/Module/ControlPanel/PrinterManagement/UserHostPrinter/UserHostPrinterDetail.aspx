<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="UserHostPrinterDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.ControlPanel.PrinterManagement.UserHostPrinterDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblUserHostName" runat="server" Text="User Host Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtUserHostName" runat="server" Width="300px" MaxLength="15" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvUserHostName" runat="server" ErrorMessage="User Host Name required."
                    ValidationGroup="entry" ControlToValidate="txtUserHostName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDescription" runat="server" Width="300px" MaxLength="200" />
            </td>
            <td width="20px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPrinterID" runat="server" Text="Default Printer"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPrinterID" runat="server" Width="300px" EnableLoadOnDemand="true"
                    MarkFirstMatch="true" HighlightTemplatedItems="true" AutoPostBack="false" OnItemDataBound="cboPrinterID_ItemDataBound"
                    OnItemsRequested="cboPrinterID_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PrinterID")%>
                        &nbsp;-&nbsp;
                        <%# DataBinder.Eval(Container.DataItem, "PrinterName")%>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 10 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <%--<asp:RequiredFieldValidator ID="rfvPrinterID" runat="server" ErrorMessage="Printer ID required."
                    ValidationGroup="entry" ControlToValidate="cboPrinterID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>--%>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdUserHostPrinterOther" runat="server" OnNeedDataSource="grdUserHostPrinterOther_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdUserHostPrinterOther_UpdateCommand"
        OnDeleteCommand="grdUserHostPrinterOther_DeleteCommand" OnInsertCommand="grdUserHostPrinterOther_InsertCommand">
        <HeaderContextMenu>
            
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="UserHostName, ProgramID">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="300px" DataField="ProgramName" HeaderText="Report Name"
                    UniqueName="ProgramName" SortExpression="ProgramName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="PrinterName" HeaderText="Printer Name"
                    UniqueName="PrinterName" SortExpression="PrinterName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings UserControlName="UserHostPrinterOtherDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="UserHostPrinterOtherEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
            
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="true">
            <Resizing AllowColumnResize="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
