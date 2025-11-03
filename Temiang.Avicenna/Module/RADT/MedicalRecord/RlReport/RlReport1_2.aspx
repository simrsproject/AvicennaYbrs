<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RlReport1_2.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.RlReport1_2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server"
        ShowContentDuringLoad="false" Behavior="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxLoadingPanel ID="ajxLoadingPanel" runat="server" Transparency="30">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px; margin-top: 75px;" />
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="ajxPanel" runat="server" Width="100%" LoadingPanelID="ajxLoadingPanel">
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
                <td width="20px">
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
                                    <asp:LinkButton ID="lbPrint" runat="server" OnClick="lbtnPrint_Click">
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
                <td width="20px">
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
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ErrorMessage="Period required."
                        ValidationGroup="entry" ControlToValidate="cboPeriodMonthStart" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td />
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                </td>
                <td class="entry">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txtPeriodYear" runat="server" Width="100px" MaxLength="4" />&nbsp;
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnProcess" Text="Process" OnClick="btnProcess_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="Year required."
                        ValidationGroup="entry" ControlToValidate="txtPeriodYear" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td />
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td colspan="2" class="labelcaption">
                    <asp:Label ID="Label8" runat="server" Text="Indikator Pelayanan" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBor" runat="server" Text="BOR"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBor" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBor" runat="server" ErrorMessage="BOR required."
                                    ValidationGroup="entry" ControlToValidate="txtBor" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLos" runat="server" Text="LOS"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtLos" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvLos" runat="server" ErrorMessage="LOS required."
                                    ValidationGroup="entry" ControlToValidate="txtLos" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBto" runat="server" Text="BTO"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBto" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBto" runat="server" ErrorMessage="BTO required."
                                    ValidationGroup="entry" ControlToValidate="txtBto" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblToi" runat="server" Text="TOI"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtToi" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvToi" runat="server" ErrorMessage="TOI required."
                                    ValidationGroup="entry" ControlToValidate="txtToi" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image9" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%">
                    <table width="100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblNdr" runat="server" Text="NDR"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtNdr" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNdr" runat="server" ErrorMessage="NDR required."
                                    ValidationGroup="entry" ControlToValidate="txtNdr" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image10" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblGdr" runat="server" Text="GDR"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtGdr" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvGdr" runat="server" ErrorMessage="GDR required."
                                    ValidationGroup="entry" ControlToValidate="txtGdr" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image11" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRataKunj" runat="server" Text="Rata-Rata Kunjungan"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtRataKunj" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvRataKunj" runat="server" ErrorMessage="Rata-Rata Kunjungan required."
                                    ValidationGroup="entry" ControlToValidate="txtRataKunj" SetFocusOnError="True"
                                    Width="100%">
                                    <asp:Image ID="Image12" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRata2" runat="server" Text="Rata-Rata"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtRata2" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvRata2" runat="server" ErrorMessage="Rata-Rata required."
                                    ValidationGroup="entry" ControlToValidate="txtRata2" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image13" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
