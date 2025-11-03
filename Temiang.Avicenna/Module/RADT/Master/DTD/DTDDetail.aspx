<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="DtdDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Master.DtdDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDtdNo" runat="server" Text="DTD No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDtdNo" runat="server" Width="100px" MaxLength="10">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDtdNo" runat="server" ErrorMessage="DTD No required."
                    ValidationGroup="entry" ControlToValidate="txtDtdNo" SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDtdName" runat="server" Text="DTD Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDtdName" runat="server" Width="300px" MaxLength="500">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDtdName" runat="server" ErrorMessage="DTD Name required."
                    ValidationGroup="entry" ControlToValidate="txtDtdName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDtdLabel" runat="server" Text="DTD Label"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDtdLabel" runat="server" Width="300px" TextMode="MultiLine"
                    MaxLength="500">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDtdLabel" runat="server" ErrorMessage="DTD Label required."
                    ValidationGroup="entry" ControlToValidate="txtDtdLabel" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Chronic Disease"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboSRChronicDisease" Width="300px" runat="server" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" OnItemDataBound="cboSRChronicDisease_ItemDataBound"
                    OnItemsRequested="cboSRChronicDisease_ItemsRequested">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemName")%>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label" style="height: 22px" />
            <td class="entry" style="height: 22px">
                <asp:CheckBox ID="chkIsActive" Text="Active" runat="server" />
            </td>
            <td width="20" style="height: 22px">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdDiagnose" runat="server" OnNeedDataSource="grdDiagnose_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDiagnose_UpdateCommand"
                    OnDeleteCommand="grdDiagnose_DeleteCommand" OnInsertCommand="grdDiagnose_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="DiagnoseID">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="30px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="120px" DataField="DiagnoseID" HeaderText="Diagnose ID"
                                UniqueName="DiagnoseID" SortExpression="DiagnoseID" />
                            <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Diagnose Name" UniqueName="DiagnoseName"
                                SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="130px" DataField="IsChronicDisease"
                                HeaderText="Chronic Disease" UniqueName="IsChronicDisease" SortExpression="IsChronicDisease"
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsDisease" HeaderText="Disease"
                                UniqueName="IsDisease" SortExpression="IsDisease" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridCheckBoxColumn HeaderStyle-Width="80px" DataField="IsActive" HeaderText="Active"
                                UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" />
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <EditFormSettings UserControlName="DiagnoseDetail.ascx" EditFormType="WebUserControl">
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
            </td>
        </tr>
    </table>
</asp:Content>
