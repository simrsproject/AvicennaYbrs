<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="RegistrationCounselingEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.RegistrationCounselingEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="500px">
        <tr>
            <td class="label">No 
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCounselingNo" runat="server" Width="50px" Enabled="false" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Date 
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtCounselingDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Counseling Time required."
                    ValidationGroup="entry" ControlToValidate="txtCounselingDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Counseling Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCounselingNotes" TextMode="MultiLine" runat="server" Width="304px" Height="70px" MaxLength="300" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Counseling Note required."
                    ValidationGroup="entry" ControlToValidate="txtCounselingNotes" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdRegistrationCounselingLine" Width="99%" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
        AllowMultiRowSelection="True"
        OnNeedDataSource="grdRegistrationCounselingLine_NeedDataSource" OnItemDataBound="grdRegistrationCounselingLine_ItemDataBound">
        <MasterTableView DataKeyNames="ItemID,ReferenceID" ShowHeader="true" ShowHeadersWhenNoRecords="false" Width="100%">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="IsSelectedEdit" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkIsSelected" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                <telerik:GridTemplateColumn HeaderText="Counseling Item" UniqueName="ItemName" HeaderStyle-Width="250px">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "ReferenceID") == null || string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "ReferenceID").ToString() ))? DataBinder.Eval(Container.DataItem, "ItemName") : string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}", DataBinder.Eval(Container.DataItem, "ItemName")) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridBoundColumn DataField="Notes" UniqueName="Notes" HeaderText="Notes" />
                <telerik:GridTemplateColumn HeaderText="Notes" UniqueName="NotesEdit">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtNotes" runat="server"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
            <Resizing AllowColumnResize="False" />
            <Scrolling UseStaticHeaders="True" ScrollHeight=""></Scrolling>
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
