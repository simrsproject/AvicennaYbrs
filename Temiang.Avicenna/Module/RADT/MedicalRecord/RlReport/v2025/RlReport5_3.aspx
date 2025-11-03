<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="RlReport5_3.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.v2025.RlReport5_3" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onProcess() {
                __doPostBack("<%= grdRlReport5_3V2025.UniqueID %>", "process");
            }
            function onPrint() {
                __doPostBack("<%= grdRlReport5_3V2025.UniqueID %>", "print");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxLoadingPanel ID="ajxLoadingPanel" runat="server" Transparency="30">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px; margin-top: 75px;" />
    </telerik:RadAjaxLoadingPanel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblRlMasterReportID" runat="server" Text="RL Master Report"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRlMasterReportID" runat="server" Width="20px" Visible="False" />
                <telerik:RadTextBox ID="txtRlMasterReportNo" runat="server" Width="100px" ReadOnly="True" />
                <telerik:RadTextBox ID="txtRlMasterReportName" runat="server" Width="193px" ReadOnly="True" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvRlMasterReportID" runat="server" ErrorMessage="RL Master Report ID required."
                    ValidationGroup="entry" ControlToValidate="txtRlMasterReportID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <asp:Panel runat="server" ID="pnlPrint">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbPrint" runat="server" OnClientClick="javascript:onPrint();return false;">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/print16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblPrint" Text="Print Report" Font-Bold="True"></asp:Label>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblRlReportNo" runat="server" Text="Report No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtRlTxReportNo" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvRlReportNo" runat="server" ErrorMessage="Report No required."
                    ValidationGroup="entry" ControlToValidate="txtRlTxReportNo" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox ID="cboPeriodMonthStart" runat="server" Width="104px" />
                &nbsp;to&nbsp;
                <telerik:RadComboBox ID="cboPeriodMonthEnd" runat="server" Width="104px" />
            </td>
            <td colspan="2" />
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" MaxLength="4" />
            </td>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year required."
                    ValidationGroup="entry" ControlToValidate="txtPeriodYear" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="ajxPanel" runat="server" Width="100%" LoadingPanelID="ajxLoadingPanel">
        <telerik:RadGrid ID="grdRlReport5_3V2025" runat="server" OnNeedDataSource="grdRlReport5_3V2025_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdRlReport5_3V2025_UpdateCommand"
            OnDeleteCommand="grdRlReport5_3V2025_DeleteCommand" OnInsertCommand="grdRlReport5_3V2025_InsertCommand">
            <HeaderContextMenu>
            </HeaderContextMenu>
            <MasterTableView CommandItemDisplay="None" DataKeyNames="DiagnosaID">
                <CommandItemTemplate>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbInsert" runat="server" CommandName="InitInsert" Visible='<%# !grdRlReport5_3V2025.MasterTableView.IsItemInserted %>'>
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/insert16.png" />
                        &nbsp;<asp:Label runat="server" ID="lblAddRow" Text="Add new record"></asp:Label>
                    </asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbPickList" runat="server" Visible='<%# !grdRlReport5_3V2025.MasterTableView.IsItemInserted %>'
                        OnClientClick="javascript:onProcess();return false;">
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/process16.png" />
                        &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Process Data"></asp:Label>
                    </asp:LinkButton>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton">
                        <HeaderStyle Width="35px" />
                        <ItemStyle CssClass="MyImageButton" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DiagnosaID" HeaderText="Kode ICD X"
                        UniqueName="DiagnosaID" SortExpression="DiagnosaID" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridBoundColumn DataField="DiagnoseName" HeaderText="Deskripsi" UniqueName="DiagnoseName"
                        SortExpression="DiagnoseName" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="KasusBaruL" HeaderText="Kasus Baru Lk"
                        UniqueName="KasusBaruL" SortExpression="KasusBaruL" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="KasusBaruP" HeaderText="Kasus Baru Pr"
                        UniqueName="KasusBaruP" SortExpression="KasusBaruP" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="JumlahKasusBaru"
                        HeaderText="Jumlah Kasus Baru" UniqueName="JumlahKasusBaru" SortExpression="JumlahKasusBaru"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                     <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="KunjunganL" HeaderText="Kunjungan Lk"
                        UniqueName="KunjunganL" SortExpression="KunjunganL" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="KunjunganP" HeaderText="Kunjungan Pr"
                        UniqueName="KunjunganP" SortExpression="KunjunganP" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="JumlahKunjungan"
                        HeaderText="Jumlah Kunjungan" UniqueName="JumlahKunjungan" SortExpression="JumlahKunjungan"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridButtonColumn UniqueName="DeleteColumn" Text="Delete" CommandName="Delete"
                        ButtonType="ImageButton" ConfirmText="Delete this row?">
                        <HeaderStyle Width="35px" />
                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                    </telerik:GridButtonColumn>
                </Columns>
                <EditFormSettings UserControlName="RlReport5_3Detail.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="RlReport5_3DetailCommand">
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
    </telerik:RadAjaxPanel>
</asp:Content>
