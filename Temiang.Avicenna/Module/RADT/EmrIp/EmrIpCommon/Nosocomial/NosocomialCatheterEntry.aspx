<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialCatheterEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialCatheterEntry" %>

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
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Installation Date required."
                    ValidationGroup="entry" ControlToValidate="txtInstallationDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator></td>
            <td></td>
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
                <asp:Label ID="Label5" runat="server" Text="Type of Catheter"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTypeOfCatheter" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Antibiotic Consumption"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAutoCompleteBox ID="acbAntibiotic" runat="server" Width="304px" DropDownHeight="150">
                </telerik:RadAutoCompleteBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Other Drugs Consumption"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadAutoCompleteBox ID="acbOtherDrug" runat="server" Width="304px" DropDownHeight="150">
                </telerik:RadAutoCompleteBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="Problem"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProblem" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr style="display: none;">
            <td class="label">
                <asp:Label ID="Label8" runat="server" Text="Monitoring"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMonitoring" runat="server" Width="304px" MaxLength="200" />
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
