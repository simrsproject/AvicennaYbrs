<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="AbRestrictionDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Master.AbRestrictionDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">AB Restriction ID
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAbRestrictionID" runat="server" Width="100px" MaxLength="10">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfID" runat="server" ErrorMessage="ID required."
                    ValidationGroup="entry" ControlToValidate="txtAbRestrictionID" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">AB Restriction Name
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAbRestrictionName" runat="server" Width="300px" MaxLength="500">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="Name required."
                    ValidationGroup="entry" ControlToValidate="txtAbRestrictionName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="AB Restriction Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRAbRestrictionType" Width="300px" runat="server">
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvType" runat="server" ErrorMessage="Restriction Type required."
                    ValidationGroup="entry" ControlToValidate="cboSRAbRestrictionType" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label>
            </td>
            <td class="entry">
                <asp:CheckBox runat="server" ID="chkIsNotRestricted" Text="Antibiotic not restricted" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
    </table>
    <telerik:RadTabStrip ID="tabDetail" runat="server" MultiPageID="mpgDetail" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab runat="server" Text="Zat Active Restriction" PageViewID="pgvAbRestrictionItem"
                Selected="true" />
            <telerik:RadTab runat="server" Text="Zat Active Suggestion Label" PageViewID="pgvAbRestrictionSuggestion" />

        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="mpgDetail" runat="server" SelectedIndex="0" BorderStyle="Solid"
        BorderColor="gray">
        <telerik:RadPageView ID="pgvAbRestrictionItem" runat="server">
            <telerik:RadGrid ID="grdAbRestrictionItem" runat="server" OnNeedDataSource="grdAbRestrictionItem_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAbRestrictionItem_UpdateCommand"
                OnDeleteCommand="grdAbRestrictionItem_DeleteCommand" OnInsertCommand="grdAbRestrictionItem_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="AbLevel,ZatActiveID" AllowSorting="false">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn HeaderText="Stratification" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# ( Eval("AbLevel") == null ?string.Empty: "I;II;III".Split(';')[Convert.ToInt32( Eval("AbLevel"))-1] )%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="ZatActiveID" HeaderText="ID"
                            UniqueName="ZatActiveID" SortExpression="ZatActiveID" />
                        <telerik:GridBoundColumn HeaderStyle-Width="320px" DataField="ZatActiveName" HeaderText="Zat Active" UniqueName="ZatActiveName"
                            SortExpression="ZatActiveName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                            SortExpression="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="AbRestrictionItemDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
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
        </telerik:RadPageView>
        <telerik:RadPageView ID="pgvAbRestrictionSuggestion" runat="server">
            <telerik:RadGrid ID="grdAbRestrictionSuggestion" runat="server" OnNeedDataSource="grdAbRestrictionSuggestion_NeedDataSource"
                AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdAbRestrictionSuggestion_UpdateCommand"
                OnDeleteCommand="grdAbRestrictionItem_DeleteCommand" OnInsertCommand="grdAbRestrictionSuggestion_InsertCommand">
                <HeaderContextMenu>
                </HeaderContextMenu>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="AbLevel" AllowSorting="false">
                    <Columns>
                        <telerik:GridEditCommandColumn ButtonType="ImageButton">
                            <HeaderStyle Width="30px" />
                            <ItemStyle CssClass="MyImageButton" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridTemplateColumn HeaderText="Stratification" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <%# ( Eval("AbLevel") == null ?string.Empty: "I;II;III".Split(';')[Convert.ToInt32( Eval("AbLevel"))-1] )%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Suggestion Label" HeaderStyle-Width="700px" >
                            <ItemTemplate>
                                <%# Eval("SuggestionNote") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn></telerik:GridTemplateColumn>

                        <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                            ButtonType="ImageButton" ConfirmText="Delete this row?">
                            <HeaderStyle Width="30px" />
                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <EditFormSettings UserControlName="AbRestrictionSuggestionDetail.ascx" EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
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
        </telerik:RadPageView>

    </telerik:RadMultiPage>
</asp:Content>
