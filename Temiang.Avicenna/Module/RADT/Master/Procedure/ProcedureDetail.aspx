<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    Codebehind="ProcedureDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.ProcedureDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblProcedureID" runat="server" Text="Procedure ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProcedureID" runat="server" Width="100px" MaxLength="10" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvProcedureID" runat="server" ErrorMessage="Procedure ID required."
                    ValidationGroup="entry" ControlToValidate="txtProcedureID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblProcedureName" runat="server" Text="Procedure Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProcedureName" runat="server" Width="300px" MaxLength="250" TextMode="MultiLine" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvProcedureName" runat="server" ErrorMessage="Procedure Name required."
                    ValidationGroup="entry" ControlToValidate="txtProcedureName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry" colspan="3">
                <div style="display: flex; align-items: center; gap: 8px; flex-wrap: wrap;">
                    <asp:CheckBox ID="chkIsIM" runat="server" Text="IM"
                        ToolTip="IM: Tidak bisa digunakan di INACBG." />
                    <asp:CheckBox ID="chkIsValidCode" runat="server" Text="ValidCode"
                        ToolTip="ValidCode: Bisa dipilih." />
                    <asp:CheckBox ID="chkIsAsterisk" runat="server" Text="Asterisk"
                        ToolTip="Asterisk: Bisa dipilih digunakan sebagai header." />
                    <asp:CheckBox ID="chkIsAccpdx" runat="server" Text="Accpdx"
                        ToolTip="Accpdx: Boleh jadi diagnosis primer." />
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="grdProcedureSynonym" runat="server" OnNeedDataSource="grdProcedureSynonym_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdProcedureSynonym_DeleteCommand" OnUpdateCommand="grdProcedureSynonym_UpdateCommand"
                    OnInsertCommand="grdProcedureSynonym_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ProcedureID, SequenceNo">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                <HeaderStyle Width="35px" />
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="SequenceNo" HeaderText="Sequence No"
                                UniqueName="SequenceNo" SortExpression="SequenceNo" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="120px" />
                            <telerik:GridBoundColumn DataField="SynonymText" HeaderText="Synonym"
                                UniqueName="SynonymText" SortExpression="SynonymText" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left"/>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                                ButtonType="ImageButton" ConfirmText="Delete this row?">
                                <HeaderStyle Width="30px" />
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                            </telerik:GridButtonColumn>
                            <telerik:GridTemplateColumn>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings UserControlName="ProcedureSynonymDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="ProcedureSynonymEditCommand">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Resizing AllowColumnResize="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
