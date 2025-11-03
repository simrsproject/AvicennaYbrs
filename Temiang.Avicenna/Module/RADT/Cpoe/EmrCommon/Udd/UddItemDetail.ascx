<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UddItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Emr.UddItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .RightAligned {
        text-align: right;
    }
</style>

<fieldset style="background-color: lavender; width: 850px;">
    <legend>Item Entry</legend>
    <asp:ValidationSummary ID="vsumTransPrescriptionItem" runat="server" ValidationGroup="Item" DisplayMode="BulletList" ShowSummary="true" ShowMessageBox="true" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Item"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
    <table id="tblItemEntry">
        <tr>
            <td class="labelcaption" style="font-style: italic">C</td>
            <td class="labelcaption" style="font-style: italic;">
                <asp:Label runat="server" ID="lblHeader" Text="Header" /></td>
            <td class="labelcaption" colspan="2" style="font-style: italic">Item</td>
            <td class="labelcaption" colspan="3" style="font-style: italic">Numero</td>
            <td class="labelcaption" colspan="4" style="font-style: italic;">
                <asp:Label runat="server" ID="lblFormula" Text="Formula" /></td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <telerik:RadCheckBox runat="server" ID="chkIsCompound" AutoPostBack="true" OnCheckedChanged="chkIsCompound_CheckedChanged" Value="true">
                </telerik:RadCheckBox>
            </td>

            <td>
                <telerik:RadComboBox ID="cboParentNo" runat="server" Width="200px" AutoPostBack="true"
                    Filter="Contains" AllowCustomText="true" OnSelectedIndexChanged="cboParentNo_SelectedIndexChanged" />
            </td>

            <td>
                <%--fungsi cboItemID_ClientItemsRequesting ada di parent page--%>
                <telerik:RadComboBox ID="cboItemID" runat="server" Width="250px" EmptyMessage="Select a Item"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true" AutoPostBack="true"
                    OnClientFocus="showDropDown"
                    OnClientItemsRequesting="cboItemID_ClientItemsRequesting"
                    OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                    <WebServiceSettings Method="PrescriptionItemSelection" Path="~/WebService/ComboBoxDataService.asmx" />
                    <ClientItemTemplate>
                     <div>
                        <ul class="details" style="#= Attributes.Style #">
                            <li class="bold"><span>#= Text #</span></li>
                            <li class="small" style="display:#= _stockStyleDisplay #"><span>#= Attributes.GenericFlag #, S: #= Attributes.Balance # #= Attributes.SRItemUnit #</span></li>
                            <li class="smaller"><span>Substance:#= Attributes.ZatActive #  </span></li>
                        </ul>
                    </div>
                    </ClientItemTemplate>
                </telerik:RadComboBox>
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvItemID" runat="server" ErrorMessage="Item required."
                    ValidationGroup="Item" ControlToValidate="cboItemID" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>

            </td>

            <td>
                <telerik:RadTextBox ID="txtQty" runat="server" Width="50px" CssClass="RightAligned" SelectionOnFocus="SelectAll">
                </telerik:RadTextBox>
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="valDispenseQty" runat="server" ErrorMessage="Please specify a digit for Dispense Qty"
                    ValidationGroup="Item" ControlToValidate="txtQty" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadTextBox ID="txtItemUnit" runat="server" Width="50px" ReadOnly="True" />
                <%--fungsi cboEmbalace_ClientSelectedIndexChanged ada di parent page--%>
                <telerik:RadComboBox runat="server" ID="cboEmbalace" Width="79px"
                    OnClientSelectedIndexChanged="cboEmbalace_ClientSelectedIndexChanged" />
            </td>

            <td>
                <telerik:RadTextBox ID="txtDosage" runat="server" Width="50px" SelectionOnFocus="SelectAll">
                </telerik:RadTextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="valFormulaQty" runat="server" ErrorMessage="Please specify a digit for Formula Qty"
                    ValidationGroup="Item" ControlToValidate="txtDosage" SetFocusOnError="True" Enabled="False"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>

            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboDosageUnit" Width="79px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="valFormulaDosageUnit" runat="server" ErrorMessage="Please specify a Formula Dosage Unit"
                    ValidationGroup="Item" ControlToValidate="cboDosageUnit" SetFocusOnError="True" Enabled="False"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
    </table>



    <table>
        <tr>
            <td class="labelcaption" colspan="2" style="font-style: italic">Consume Method
            </td>
            <td class="labelcaption" colspan="4" style="font-style: italic">Dosing
            </td>
            <td class="labelcaption" style="font-style: italic">Cons. Time
            </td>
            <td class="labelcaption" style="font-style: italic">Start at
            </td>
            <td class="labelcaption" style="font-style: italic">Notes
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadComboBox ID="cboConsumeMethod" runat="server" Width="150px" AllowCustomText="true"
                    Filter="Contains" OnClientSelectedIndexChanged="cboConsumeMethod_ClientSelectedIndexChanged" OnClientFocus="showDropDownNoKeypress" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvConsumeMethod" runat="server" ErrorMessage="Please specify a Consume Method"
                    ValidationGroup="Item" ControlToValidate="cboConsumeMethod" 
                    SetFocusOnError="True" Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadTextBox ID="txtConsumeQty" runat="server" Width="40px" CssClass="RightAligned" />
            </td>
            <td width="20px">
                <asp:RequiredFieldValidator ID="rfvConsume" runat="server" ErrorMessage="Please specify a Dosing Qty"
                    ValidationGroup="Item" ControlToValidate="txtConsumeQty" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboConsumeUnit" Width="100px"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                    OnClientItemsRequesting="cboConsumeUnit_ClientItemsRequesting" OnClientFocus="showDropDown">
                    <WebServiceSettings Method="ConsumeUnits" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvConsumeUnit" runat="server" ErrorMessage="Please specify a Dosing Unit"
                    ValidationGroup="Item" ControlToValidate="cboConsumeUnit" SetFocusOnError="True"
                    Width="20px">
                    <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>

            <td>
                <%--                <telerik:RadComboBox ID="cboAcPcDc" runat="server" Width="120px">
                </telerik:RadComboBox>--%>
                <telerik:RadComboBox ID="cboMedicationConsume" runat="server" Width="120px" EmptyMessage="Select a Item"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                    OnClientItemsRequesting="cboMedicationConsume_ClientItemsRequesting" OnClientFocus="showDropDown">
                    <WebServiceSettings Method="StandardReference" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>

            <td>
                <telerik:RadDateTimePicker ID="txtStartDateTime" runat="server" Width="155px">
                    <TimeView TimeFormat="HH:mm" runat="server">
                    </TimeView>
                    <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                    </DateInput>
                </telerik:RadDateTimePicker>
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtNotes" Width="185px" MaxLength="500" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="10">
                <asp:Button ID="btnUpdate" Text="Update Item" runat="server" CommandName="Update" ValidationGroup="Item"
                    Visible='<%# !(DataItem is GridInsertionObject) %>'></asp:Button>
                <asp:Button ID="btnInsert" Text="Add Item" runat="server" CommandName="PerformInsert"
                    ValidationGroup="Item" Visible='<%# DataItem is GridInsertionObject %>'></asp:Button>
                &nbsp;
            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                CommandName="Cancel"></asp:Button>
            </td>
        </tr>
    </table>



</fieldset>
<br />
