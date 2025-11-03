<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="ZatActiveDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Inventory.Master.ZatActiveDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblZatActiveID" runat="server" Text="Zat Active ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtZatActiveID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvZatActiveID" runat="server" ErrorMessage="Zat Active ID required."
                    ValidationGroup="entry" ControlToValidate="txtZatActiveID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblZatActiveName" runat="server" Text="Zat Active Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtZatActiveName" runat="server" Width="300px" MaxLength="100" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvZatActiveName" runat="server" ErrorMessage="Zat Active Name required."
                    ValidationGroup="entry" ControlToValidate="txtZatActiveName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblSRZatActiveGroup" runat="server" Text="Group"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRZatActiveGroup" runat="server" Width="300px" AllowCustomText="true"
                    Filter="Contains" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvSRZatActiveGroup" runat="server" ErrorMessage="Zat Active Group required."
                    ValidationGroup="entry" ControlToValidate="cboSRZatActiveGroup" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Defined Daily Dosage (DDD) Oral"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtDddOral" runat="server" Width="50px" NumberFormat-DecimalDigits="2" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Defined Daily Dosage (DDD) Parenteral"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtDddParenteral" runat="server" Width="50px" NumberFormat-DecimalDigits="2" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry">
                <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdZatActiveInteraction" Width="98%" runat="server" OnNeedDataSource="grdZatActiveInteraction_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdZatActiveInteraction_UpdateCommand"
        OnDeleteCommand="grdZatActiveInteraction_DeleteCommand" OnInsertCommand="grdZatActiveInteraction_InsertCommand"
        AllowMultiRowEdit="false">
        <MasterTableView DataKeyNames="InteractionZatActiveID" CommandItemDisplay="Top">
            <CommandItemTemplate>
                <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdZatActiveInteraction.MasterTableView.IsItemInserted %>'>
                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                    &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new Interaction"></asp:Label>
                </asp:LinkButton>
            </CommandItemTemplate>
            <CommandItemStyle Height="29px" />
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" Visible="true">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="InteractionZatActiveID" HeaderText="ID" UniqueName="InteractionZatActiveID"
                    HeaderStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="InteractionZatActiveName" HeaderText="Interaction With" UniqueName="ZatActiveName"
                    HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn HeaderText="Interaction Description" UniqueName="TemplateItemName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Interaction")%>'
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                    ButtonType="ImageButton" ConfirmText="Delete this row?">
                    <HeaderStyle Width="30px" />
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <telerik:GridTemplateColumn></telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="ZatActiveInteractionDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="ZatActiveInteractionEditCommand">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="false">
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="false" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
