<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="DiagnosisDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Master.DiagnosisDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblDiagnoseID" runat="server" Text="Diagnose ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDiagnoseID" runat="server" Width="100px" MaxLength="20">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDiagnoseID" runat="server" ErrorMessage="Diagnose ID required."
                    ValidationGroup="entry" ControlToValidate="txtDiagnoseID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDiagnoseName" runat="server" Text="Diagnose Name"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtDiagnoseName" runat="server" Width="300px" TextMode="MultiLine" MaxLength="500">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDiagnoseName" runat="server" ErrorMessage="Diagnose Name required."
                    ValidationGroup="entry" ControlToValidate="txtDiagnoseName" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr id="trSynonym" runat="server">
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Synonym"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtSynonym" runat="server" Width="300px" TextMode="MultiLine" MaxLength="200">
                </telerik:RadTextBox>
            </td>
            <td width="20"></td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblDtdLabel" runat="server" Text="DTD"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboDtdNo" Width="300px" AutoPostBack="False"
                    EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                    OnItemDataBound="cboDtdNo_ItemDataBound" OnItemsRequested="cboDtdNo_ItemsRequested">
                    <ItemTemplate>
                        <b>
                            <%# DataBinder.Eval(Container.DataItem, "DtdName")%>
                            &nbsp;(<%# DataBinder.Eval(Container.DataItem, "DtdNo")%>) </b>
                    </ItemTemplate>
                    <FooterTemplate>
                        Note : Show max 15 items
                    </FooterTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvDtd" runat="server" ErrorMessage="DTD required."
                    ValidationGroup="entry" ControlToValidate="cboDtdNo" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label"></td>
            <td class="entry" colspan="3">
                <div style="display: flex; align-items: center; gap: 8px; flex-wrap: wrap;">
                    <asp:CheckBox ID="chkIsChronicDisease" runat="server" Text="Chronic Disease" />
                    <asp:CheckBox ID="chkIsDisease" runat="server" Text="Disease" />
                    <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" />
                </div>
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
    <table width="100%" id="tableSynonym" runat="server">
        <tr>
            <td>
                <telerik:RadGrid ID="grdDiagnoseSynonym" runat="server" OnNeedDataSource="grdDiagnoseSynonym_NeedDataSource"
                    AutoGenerateColumns="False" GridLines="None" OnDeleteCommand="grdDiagnoseSynonym_DeleteCommand" OnUpdateCommand="grdDiagnoseSynonym_UpdateCommand"
                    OnInsertCommand="grdDiagnoseSynonym_InsertCommand">
                    <HeaderContextMenu>
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="DiagnoseID, SequenceNo">
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
                        <EditFormSettings UserControlName="DiagnoseSynonymDetail.ascx" EditFormType="WebUserControl">
                            <EditColumn UniqueName="DiagnoseSynonymEditCommand">
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
