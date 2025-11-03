<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="ExposureFactorDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Charges.ExposureFactorDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManagerProxy runat="server" ID="RadAjaxManagerProxy1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="grdDetail">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grdDetail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblItemID" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry" colspan="3">
                            <telerik:RadTextBox ID="txtItemID" runat="server" Width="100px" MaxLength="10"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblItemName" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 100%; vertical-align: top">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblCaption" runat="server" Text="EXPOSURE FACTOR" Font-Bold="True" Font-Size="9"></asp:Label>
                    </legend>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption" colspan="2">
                                            <asp:Label ID="lblKv" runat="server" Text="kV"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblKvF" runat="server" Text="f"></asp:Label>
                                        </td>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblKvC" runat="server" Text="c"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption" colspan="2">
                                            <asp:Label ID="lblMa" runat="server" Text="mA"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblMaF" runat="server" Text="f"></asp:Label>
                                        </td>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblMaC" runat="server" Text="c"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption" colspan="2">
                                            <asp:Label ID="lblS" runat="server" Text="s"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblSF" runat="server" Text="f"></asp:Label>
                                        </td>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblSC" runat="server" Text="c"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption" colspan="2">
                                            <asp:Label ID="lblMas" runat="server" Text="mAs"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblMasF" runat="server" Text="f"></asp:Label>
                                        </td>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblMasC" runat="server" Text="c"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblFfd" runat="server" Text="FFD"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblFfdCm" runat="server" Text="(cm)"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblScreeningTime" runat="server" Text="Screening Time"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblScreeningTimeS" runat="server" Text="(in seconds)"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblCineTime" runat="server" Text="Cine Time"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelcaption">
                                            <asp:Label ID="lblCineTimeS" runat="server" Text="(in seconds)"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtKvF" runat="server" Width="50px" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtKvC" runat="server" Width="50px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtMaF" runat="server" Width="50px" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtMaC" runat="server" Width="50px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtSF" runat="server" Width="50px" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtSC" runat="server" Width="50px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtMasF" runat="server" Width="50px" />
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtMasC" runat="server" Width="50px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtFfd" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtScreeningTime" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 8%">
                                <table width="100%">
                                    <tr>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtCineTime" runat="server" Width="100px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="255" TextMode="MultiLine" />
                            </td>
                            <td width="20"></td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
