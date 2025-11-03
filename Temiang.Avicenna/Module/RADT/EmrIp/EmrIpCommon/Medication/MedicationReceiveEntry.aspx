<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="vsumTransPrescriptionItem" runat="server" ValidationGroup="Entry" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Entry"
                         ErrorMessage="" OnServerValidate="customValidator_ServerValidate"></asp:CustomValidator>
    <table width="600px">
        <tr>
            <td class="label">Date</td>
            <td>
                <telerik:RadDatePicker ID="txtReceiveDateTime" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Item</td>
            <td class="entry">
                <telerik:RadComboBox ID="cboItemID" runat="server" Width="100%" EmptyMessage="Select a Item"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true"
                    OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                    <WebServiceSettings Method="ItemProductMedics" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                        <div>
                            <ul class="details">
                                <li class="bold"><span>#= Text # </span></li>
                                <li class="small"><span>#= Attributes.GenericFlag #</span></li>
                                <li class="smaller"><span>Substance:#= Attributes.ZatActive #  </span></li>
                            </ul>
                        </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px"></td>
        </tr>
        <tr>
            <td class="label">Item Description</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtItemDescription" runat="server" Width="100%" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Item Description required."
                                            ValidationGroup="Entry" ControlToValidate="txtItemDescription"
                                            SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Item Condition</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtCondition" runat="server" Width="100%" />
            </td>
            <td width="20px">

            </td>
        </tr>
        <tr>
            <td class="label">Last Consumption</td>
            <td>
                <telerik:RadDatePicker ID="txtLastConsumeDateTime" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Receive Qty</td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtReceiveQty"  runat="server" Width="90px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Receive Qty required."
                                            ValidationGroup="Entry" ControlToValidate="txtReceiveQty"
                                            SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Expire Date</td>
            <td>
                <telerik:RadDatePicker ID="txtExpireDate" runat="server" Width="100px" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Consume Qty</td>
            <td class="entry">
                <telerik:RadTextBox ID="txtConsumeQty" runat="server" Width="90px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Consume Qty required."
                                            ValidationGroup="Entry" ControlToValidate="txtConsumeQty"
                                            SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Receive / Consume Unit</td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboSRItemUnit" Width="100%" EmptyMessage="Select Unit Consume"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                    <WebServiceSettings Method="ItemUnits" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Unit required."
                                            ValidationGroup="Entry" ControlToValidate="cboSRItemUnit"
                                            SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Consume Method</td>
            <td class="entry">
                <telerik:RadComboBox runat="server" ID="cboConsumeMethod" Width="100%" EmptyMessage="Select Unit Consume"
                                     EnableLoadOnDemand="True" ShowMoreResultsBox="true" EnableVirtualScrolling="false">
                    <WebServiceSettings Method="ConsumeMethods" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                        <div>
                            <ul class="details">
                                <li class="bold"><span>#= Text # </span></li>
                                <li class="small"><span>Time: #= Attributes.TimeSequence #</span></li>
                            </ul>
                        </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvConsumeMethod" runat="server" ErrorMessage="Consume Method required."
                                            ValidationGroup="Entry" ControlToValidate="cboConsumeMethod"
                                            SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
       </table>
</asp:Content>
