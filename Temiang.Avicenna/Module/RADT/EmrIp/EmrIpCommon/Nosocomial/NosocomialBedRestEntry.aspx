<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NosocomialBedRestEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NosocomialBedRestEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnEditRegistrationNo"/>
    <asp:HiddenField runat="server" ID="hdnEditMonitoringNo"/>

    <telerik:RadAjaxPanel runat="server">
        <table width="100%">
            <tr>
                <td class="label">Start Monitoring Bed Rest
                </td>
                <td class="entry">
                    <telerik:RadDateTimePicker ID="txtInstallationDateTime" runat="server" Width="170px" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Bed Rest Date."
                                                ValidationGroup="entry" ControlToValidate="txtInstallationDateTime" SetFocusOnError="True"
                                                Width="100%">
                        <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                    </asp:RequiredFieldValidator></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Decubitus from</td>
                <td>
                    <asp:RadioButtonList ID="optDecubitusFrom" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="optDecubitusFrom_OnSelectedIndexChanged">
                        <asp:ListItem Text="None" Value="N" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Room" Value="R" ></asp:ListItem>
                        <asp:ListItem Text="Hospital" Value="H"></asp:ListItem>
                        <asp:ListItem Text="Home" Value="M"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td class="label"></td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtDecubitusFrom" runat="server" Width="304px" MaxLength="200" Visible="False" />
                    <telerik:RadComboBox runat="server" ID="cboServiceUnitID" Width="304px" EmptyMessage="Select a Service Unit Care"
                        EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                        <WebServiceSettings Method="ServiceUnitCares" Path="~/WebService/ComboBoxDataService.asmx" />
                    </telerik:RadComboBox>
                </td>
                <td width="20px"></td>
                <td></td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <table width="100%">
        <tr>
            <td class="label">Date found decubitus 
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtDecubitusDateTime" runat="server" Width="170px" />
            </td>
            <td>
            <td></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label6" runat="server" Text="Location of injury decubitus"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLocation" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label7" runat="server" Text="The state of injury when it comes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtProblem" runat="server" Width="304px" MaxLength="200" />
            </td>
            <td width="20px"></td>
        </tr>
    </table>
</asp:Content>
