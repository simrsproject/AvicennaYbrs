<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="RlReport3_14.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.RlReport3_14" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onProcess() {
                __doPostBack("<%= grdRlReport3_14.UniqueID %>", "process");
            }
            function onPrint() {
                __doPostBack("<%= grdRlReport3_14.UniqueID %>", "print");
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
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/print16.png" />
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
        <telerik:RadGrid ID="grdRlReport3_14" runat="server" OnNeedDataSource="grdRlReport3_14_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None">
            <HeaderContextMenu>
            </HeaderContextMenu>
            <MasterTableView CommandItemDisplay="None" DataKeyNames="RlMasterReportItemID">
                <CommandItemTemplate>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbPickList" runat="server" OnClientClick="javascript:onProcess();return false;">
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/process16.png" />
                        &nbsp;<asp:Label runat="server" ID="lblPicList" Text="Process Data"></asp:Label>
                    </asp:LinkButton>
                </CommandItemTemplate>
                <CommandItemStyle Height="29px" />
                <Columns>
                    <telerik:GridBoundColumn DataField="RlMasterReportItemID" HeaderText="ID" UniqueName="RlMasterReportItemID"
                        SortExpression="RlMasterReportItemID" Visible="False">
                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RlMasterReportItemCode" HeaderText="No" UniqueName="RlMasterReportItemCode"
                        SortExpression="RlMasterReportItemCode">
                        <HeaderStyle HorizontalAlign="Left" Width="70px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RlMasterReportItemName" HeaderText="Jenis Spesialisasi"
                        UniqueName="RlMasterReportItemName" SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Rujukan diterima dari Puskesmas"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtRujukanPuskesmas" runat="server" Width="35px" DbValue='<%#Eval("RujukanPuskesmas")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Rujukan diterima dari Faskes Lain"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtRujukanFasKesLain" runat="server" Width="35px"
                                DbValue='<%#Eval("RujukanFasKesLain")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Rujukan diterima dari RS Lain"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtRujukanRsLain" runat="server" Width="35px" DbValue='<%#Eval("RujukanRsLain")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Rujukan dikembalikan ke Puskesmas Asal"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDirujukKePuskesmasAsal" runat="server" Width="35px"
                                DbValue='<%#Eval("DirujukKePuskesmasAsal")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Rujukan dikembalikan ke Faskes Asal"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDirujukKeFasKesAsal" runat="server" Width="35px"
                                DbValue='<%#Eval("DirujukKeFasKesAsal")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Rujukan dikembalikan ke RS Asal"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDirujukKeRsAsal" runat="server" Width="35px" DbValue='<%#Eval("DirujukKeRsAsal")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Dirujuk Pasien Rujukan"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDirujukPasienRujukan" runat="server" Width="35px"
                                DbValue='<%#Eval("DirujukPasienRujukan")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Dirujuk Pasien Datang Sendiri"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDirujukPasienDtgSendiri" runat="server" Width="35px"
                                DbValue='<%#Eval("DirujukPasienDtgSendiri")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="70px" HeaderText="Dirujuk diterima Kembali"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDirujukDiterimaKembali" runat="server" Width="35px"
                                DbValue='<%#Eval("DirujukDiterimaKembali")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="True">
                <Resizing AllowColumnResize="True" />
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
