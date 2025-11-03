<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialEttEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialEttEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnEditRegistrationNo"/>
    <asp:HiddenField runat="server" ID="hdnEditMonitoringNo"/>

    <table width="100%">
        <tr>
            <td class="label">Date 
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtInstallationDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Installation Date required."
                    ValidationGroup="entry" ControlToValidate="txtInstallationDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label1" runat="server" Text="Installation Room"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboRoomID" Width="304px" EmptyMessage="Select a Room"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                    <WebServiceSettings Method="ServiceRooms" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                        <div>
                            <ul class="details">
                                <li class="bold"><span>#= Text # </span></li>
                                <li class="smaller"><span>#= Attributes.ServiceUnitName # </span></li>
                            </ul>
                        </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="ETT Type"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSREttType" Width="304px">
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label5" runat="server" Text="ETT No"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTubeNo" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="ETT Position"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLocation" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label4" runat="server" Text="Installation By"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInstallationByName" runat="server" Width="304px" ReadOnly="True" />
            </td>
            <td width="20px">
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label8" runat="server" Text="Preparation"></asp:Label>
            </td>
            <td class="entry">
                <table width="100%">
                    <tr>
                        <td style="width: 50%">
                            <table width="100%">
                                <tr>
                                    <td style="width: 100px;">- Body Weight</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtBodyWeight" Width="50px">
                                            <NumberFormat DecimalDigits="2" PositivePattern="n kg" />
                                        </telerik:RadNumericTextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>- Ventilator Mode</td>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtVentilatorMode" MaxLength="200" Width="80px" /></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>- Tidal Vol</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtTidalVolume" NumberFormat-DecimalDigits="0" Width="50px"></telerik:RadNumericTextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>- Resp Rate</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtRespirationRate" NumberFormat-DecimalDigits="0" Width="50px"></telerik:RadNumericTextBox></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%">
                            <table width="100%">
                                <tr>
                                    <td>- FiO2</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtFiO2" NumberFormat-DecimalDigits="0" Width="50px"></telerik:RadNumericTextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>- PEEP</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtPeep" NumberFormat-DecimalDigits="0" Width="50px"></telerik:RadNumericTextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>- Peak Flow</td>
                                    <td>
                                        <telerik:RadNumericTextBox runat="server" ID="txtPeakFlow" NumberFormat-DecimalDigits="0" Width="50px"></telerik:RadNumericTextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>- Sensitivity</td>
                                    <td>
                                        <telerik:RadTextBox runat="server" ID="txtSensitivity" MaxLength="200" Width="80px" /></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>

            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Reason"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProblem" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>


    </table>

    <fieldset>
        <legend>Release Info</legend>
        <table width="100%">
            <tr>
                <td class="label">
                    <asp:Label ID="Label3" runat="server" Text="Release Date"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtReleaseDateTime" runat="server" Width="304px" MaxLength="200" ReadOnly="True" />
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label9" runat="server" Text="Release By"></asp:Label>
                </td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtReleaseByName" runat="server" Width="304px" MaxLength="200" ReadOnly="True" />
                </td>
                <td width="20px"></td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
