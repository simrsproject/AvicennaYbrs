<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="RlReport3_16.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.v2025.RlReport3_16" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onProcess() {
                __doPostBack("<%= grdRlReport3_16.UniqueID %>", "process");
            }
            function onPrint() {
                __doPostBack("<%= grdRlReport3_16.UniqueID %>", "print");
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
        <telerik:RadGrid ID="grdRlReport3_16" runat="server" OnNeedDataSource="grdRlReport3_16_NeedDataSource"
            AutoGenerateColumns="False" GridLines="None">
            <HeaderContextMenu>
            </HeaderContextMenu>
            <MasterTableView CommandItemDisplay="None" DataKeyNames="RlMasterReportItemID">
                <CommandItemTemplate>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lbPickList" runat="server" OnClientClick="javascript:onProcess();return false;">
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../../Images/Toolbar/process16.png" />
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
                    <telerik:GridBoundColumn DataField="RlMasterReportItemName" HeaderText="Metoda" UniqueName="RlMasterReportItemName"
                        SortExpression="ItemName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Pelayanan KB Pasca Persalinan" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtPKBPascaPersalinan" runat="server" Width="35px" DbValue='<%#Eval("PKBPascaPersalinan")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Pelayanan KB Pasca Keguguran"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtPKBPascaKeguguran" runat="server" Width="35px"
                                DbValue='<%#Eval("PKBPascaKeguguran")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Pelayanan KB Interval"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtPKBInterval" runat="server" Width="35px"
                                DbValue='<%#Eval("PKBInterval")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Pelayanan KB Total"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtPKBTotal" runat="server" Width="35px"
                                DbValue='<%#Eval("PKBTotal")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Komplikasi KB"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtKomplikasiKB" runat="server" Width="35px"
                                DbValue='<%#Eval("KomplikasiKB")%>' NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Kegagalan KB"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtKegagalanKB" runat="server" Width="35px" DbValue='<%#Eval("KegagalanKB")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Efek Samping"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtEfekSamping" runat="server" Width="35px" DbValue='<%#Eval("EfekSamping")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="60px" HeaderText="Drop Out"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtDropOut" runat="server" Width="35px" DbValue='<%#Eval("DropOut")%>'
                                NumberFormat-DecimalDigits="0" />
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
