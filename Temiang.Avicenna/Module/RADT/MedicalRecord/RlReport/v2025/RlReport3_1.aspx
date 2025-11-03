<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true"
    CodeBehind="RlReport3_1.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.v2025.RlReport3_1" %>

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
                <td width="20px">
                    <asp:RequiredFieldValidator ID="rfvRlReportNo" runat="server" ErrorMessage="Report No required."
                        ValidationGroup="entry" ControlToValidate="txtRlTxReportNo" SetFocusOnError="True"
                        Width="100%">
                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
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
                                <asp:Label ID="lblBorNonIntensif" runat="server" Text="BOR NON INTENSIF"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBorNonIntensif" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBorNonIntensif" runat="server" ErrorMessage="BOR NON INTENSIF required."
                                    ValidationGroup="entry" ControlToValidate="txtBorNonIntensif" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBorICU" runat="server" Text="BOR ICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBorICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBorICU" runat="server" ErrorMessage="BOR ICU required."
                                    ValidationGroup="entry" ControlToValidate="txtBorICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBorICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBorNICU" runat="server" Text="BOR NICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBorNICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBorNICU" runat="server" ErrorMessage="BOR NICU required."
                                    ValidationGroup="entry" ControlToValidate="txtBorNICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBorNICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBorPICU" runat="server" Text="BOR PICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBorPICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBorPICU" runat="server" ErrorMessage="BOR PICU required."
                                    ValidationGroup="entry" ControlToValidate="txtBorPICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBorPICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBorIntensifLainnya" runat="server" Text="BOR INTENSIF LAINNYA"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBorIntensifLainnya" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBorIntensifLainnya" runat="server" ErrorMessage="BOR INTENSIF LAINNYA required."
                                    ValidationGroup="entry" ControlToValidate="txtBorIntensifLainnya" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBorIntensifLainnya" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLosNonIntensif" runat="server" Text="LOS NON INTENSIF"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtLosNonIntensif" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvLosNonIntensif" runat="server" ErrorMessage="LOS NON INTENSIF required."
                                    ValidationGroup="entry" ControlToValidate="txtLosNonIntensif" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageLosNonIntensif" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLosICU" runat="server" Text="LOS ICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtLosICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvLosICU" runat="server" ErrorMessage="LOS ICU required."
                                    ValidationGroup="entry" ControlToValidate="txtLosICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageLosICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLosNICU" runat="server" Text="LOS NICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtLosNICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvLosNICU" runat="server" ErrorMessage="LOS NICU required."
                                    ValidationGroup="entry" ControlToValidate="txtLosNICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageLosNICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLosPICU" runat="server" Text="LOS PICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtLosPICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvLosPICU" runat="server" ErrorMessage="LOS PICU required."
                                    ValidationGroup="entry" ControlToValidate="txtLosPICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageLosPICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLosIntensifLainnya" runat="server" Text="LOS INTENSIF LAINNYA"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtLosIntensifLainnya" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvLosIntensifLainnya" runat="server" ErrorMessage="LOS INTENSIF LAINNYA required."
                                    ValidationGroup="entry" ControlToValidate="txtLosIntensifLainnya" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageLosIntensifLainnya" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBtoNonIntensif" runat="server" Text="BTO NON INTENSIF"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBtoNonIntensif" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBtoNonIntensif" runat="server" ErrorMessage="BTO NON INTENSIF required."
                                    ValidationGroup="entry" ControlToValidate="txtBtoNonIntensif" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBtoNonIntensif" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBtoICU" runat="server" Text="BTO ICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBtoICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBtoICU" runat="server" ErrorMessage="BTO ICU required."
                                    ValidationGroup="entry" ControlToValidate="txtBtoICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBtoICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBtoNICU" runat="server" Text="BTO NICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBtoNICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBtoNICU" runat="server" ErrorMessage="BTO NICU required."
                                    ValidationGroup="entry" ControlToValidate="txtBtoNICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBtoNICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBtoPICU" runat="server" Text="BTO PICU"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBtoPICU" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBtoPICU" runat="server" ErrorMessage="BTO PICU required."
                                    ValidationGroup="entry" ControlToValidate="txtBtoPICU" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBtoPICU" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBtoIntensifLainnya" runat="server" Text="BTO INTENSIF LAINNYA"></asp:Label>
                            </td>
                            <td class="entry">
                                <telerik:RadNumericTextBox ID="txtBtoIntensifLainnya" runat="server" Width="100px" MinValue="0" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBtoIntensifLainnya" runat="server" ErrorMessage="BTO INTENSIF LAINNYA required."
                                    ValidationGroup="entry" ControlToValidate="txtBtoIntensifLainnya" SetFocusOnError="True" Width="100%">
                                    <asp:Image ID="ImageBtoIntensifLainnya" runat="server" SkinID="rfvImage" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    </td>
                    <td style="width: 50%">
                        <table width="100%">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblToiNonIntensif" runat="server" Text="TOI NON INTENSIF"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtToiNonIntensif" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvToiNonIntensif" runat="server" ErrorMessage="TOI NON INTENSIF required."
                                        ValidationGroup="entry" ControlToValidate="txtToiNonIntensif" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageToiNonIntensif" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblToiICU" runat="server" Text="TOI ICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtToiICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvToiICU" runat="server" ErrorMessage="TOI ICU required."
                                        ValidationGroup="entry" ControlToValidate="txtToiICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageToiICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblToiNICU" runat="server" Text="TOI NICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtToiNICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvToiNICU" runat="server" ErrorMessage="TOI NICU required."
                                        ValidationGroup="entry" ControlToValidate="txtToiNICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageToiNICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblToiPICU" runat="server" Text="TOI PICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtToiPICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvToiPICU" runat="server" ErrorMessage="TOI PICU required."
                                        ValidationGroup="entry" ControlToValidate="txtToiPICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageToiPICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblToiIntensifLainnya" runat="server" Text="TOI INTENSIF LAINNYA"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtToiIntensifLainnya" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvToiIntensifLainnya" runat="server" ErrorMessage="TOI INTENSIF LAINNYA required."
                                        ValidationGroup="entry" ControlToValidate="txtToiIntensifLainnya" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageToiIntensifLainnya" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNdrNonIntensif" runat="server" Text="NDR NON INTENSIF"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNdrNonIntensif" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvNdrNonIntensif" runat="server" ErrorMessage="NDR NON INTENSIF required."
                                        ValidationGroup="entry" ControlToValidate="txtNdrNonIntensif" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageNdrNonIntensif" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNdrICU" runat="server" Text="NDR ICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNdrICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvNdrICU" runat="server" ErrorMessage="NDR ICU required."
                                        ValidationGroup="entry" ControlToValidate="txtNdrICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageNdrICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNdrNICU" runat="server" Text="NDR NICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNdrNICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvNdrNICU" runat="server" ErrorMessage="NDR NICU required."
                                        ValidationGroup="entry" ControlToValidate="txtNdrNICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageNdrNICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNdrPICU" runat="server" Text="NDR PICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNdrPICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvNdrPICU" runat="server" ErrorMessage="NDR PICU required."
                                        ValidationGroup="entry" ControlToValidate="txtNdrPICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageNdrPICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblNdrIntensifLainnya" runat="server" Text="NDR INTENSIF LAINNYA"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtNdrIntensifLainnya" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvNdrIntensifLainnya" runat="server" ErrorMessage="NDR INTENSIF LAINNYA required."
                                        ValidationGroup="entry" ControlToValidate="txtNdrIntensifLainnya" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageNdrIntensifLainnya" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGdrNonIntensif" runat="server" Text="GDR NON INTENSIF"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtGdrNonIntensif" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvGdrNonIntensif" runat="server" ErrorMessage="GDR NON INTENSIF required."
                                        ValidationGroup="entry" ControlToValidate="txtGdrNonIntensif" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageGdrNonIntensif" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGdrICU" runat="server" Text="GDR ICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtGdrICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvGdrICU" runat="server" ErrorMessage="GDR ICU required."
                                        ValidationGroup="entry" ControlToValidate="txtGdrICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageGdrICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGdrNICU" runat="server" Text="GDR NICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtGdrNICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvGdrNICU" runat="server" ErrorMessage="GDR NICU required."
                                        ValidationGroup="entry" ControlToValidate="txtGdrNICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageGdrNICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGdrPICU" runat="server" Text="GDR PICU"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtGdrPICU" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvGdrPICU" runat="server" ErrorMessage="GDR PICU required."
                                        ValidationGroup="entry" ControlToValidate="txtGdrPICU" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageGdrPICU" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblGdrIntensifLainnya" runat="server" Text="GDR INTENSIF LAINNYA"></asp:Label>
                                </td>
                                <td class="entry">
                                    <telerik:RadNumericTextBox ID="txtGdrIntensifLainnya" runat="server" Width="100px" MinValue="0" />
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvGdrIntensifLainnya" runat="server" ErrorMessage="GDR INTENSIF LAINNYA required."
                                        ValidationGroup="entry" ControlToValidate="txtGdrIntensifLainnya" SetFocusOnError="True" Width="100%">
                                        <asp:Image ID="ImageGdrIntensifLainnya" runat="server" SkinID="rfvImage" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
</asp:Content>
