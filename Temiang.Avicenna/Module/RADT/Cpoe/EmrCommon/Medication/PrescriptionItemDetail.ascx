<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrescriptionItemDetail.ascx.cs"
    Inherits="Temiang.Avicenna.Module.RADT.Emr.PrescriptionItemDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .RightAligned {
        text-align: right;
    }
</style>

<fieldset style="background-color: lavender; width: 90%;">
    <legend>Item Entry</legend>
    <asp:ValidationSummary ID="vsumTransPrescriptionItem" runat="server" ValidationGroup="Item" DisplayMode="BulletList" ShowSummary="true" ShowMessageBox="true" />
    <asp:CustomValidator ID="customValidator" runat="server" ValidationGroup="Item"
        ErrorMessage="" OnServerValidate="customValidator_ServerValidate">&nbsp;</asp:CustomValidator>
    <asp:HiddenField runat="server" ID="hdnQty23Days" />
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
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
                                OnClientItemsRequesting="cboItemID_ClientItemsRequesting" OnClientFocus="showDropDown"
                                OnSelectedIndexChanged="cboItemID_SelectedIndexChanged">
                                <WebServiceSettings Method="PrescriptionItemSelection" Path="~/WebService/ComboBoxDataService.asmx" />
                                <ClientItemTemplate>
                                 <div>
                                    <ul class="details" style="#= Attributes.Style #">
                                        <li class="bold"><span>#= Text #</span></li>
                                        <li class="small" style="display:#= _stockStyleDisplay #"><span>#= Attributes.GenericFlag # Stock: #= Attributes.Balance # #= Attributes.SRItemUnit #</span></li>
                                        <%--<li class="small" style="display:#= (_stockStyleDisplay==='none' ? 'block' : 'none') #" ><span>#= Attributes.GenericFlag #</span></li>--%>
                                        <li class="smaller"><span>Substance:#= Attributes.ZatActive #  </span></li>
                                        <li class="smaller"><i><span style="color: orangered">#= Attributes.FornasRestrictionNotes #</span></i></li>
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
                                <ClientEvents OnBlur="ResetLineAmount"></ClientEvents>
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
                                <ClientEvents OnBlur="ResetLineAmount"></ClientEvents>
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
                            <telerik:RadComboBox runat="server" ID="cboDosageUnit" Width="79px" OnClientSelectedIndexChanged="ResetLineAmount" />
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
            </td>
            <td>
                <asp:Panel ID="pnlInfoLastBuy" runat="server" Visible="false" Width="98%">
                    <fieldset style="padding: 0">
                        <legend>Previous Transaction Info</legend>
                        <table>
                            <tr>
                                <td class="header" style="text-align: center;">Item Name
                                </td>
                                <td class="header" style="text-align: center;">Qty
                                </td>
                                <td class="header" style="text-align: center;">Unit
                                </td>
                                <td class="header" style="text-align: center;">Date(d/m/y)
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblPrevItemName" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblPrevItemQty" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblPrevItemSRUnit" runat="server"></asp:Label>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblPrevDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
    </table>


    <table>
        <tr>
            <td class="labelcaption" colspan="2" style="font-style: italic">Consume Method
            </td>
            <td class="labelcaption" colspan="4" style="font-style: italic">Dosing
            </td>
            <td class="labelcaption" colspan="2" style="font-style: italic">Cons. Time
            </td>
            <td class="labelcaption" style="font-style: italic">Start at
            </td>
            <td class="labelcaption" style="font-style: italic">Iter
            </td>
            <td class="labelcaption" style="font-style: italic">Notes
            </td>
            <td class="labelcaption" style="font-style: italic; display: <%=DisplayPrice %>">Amount
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadComboBox ID="cboConsumeMethod" runat="server" Width="150px" AllowCustomText="true" OnClientFocus="showDropDownNoKeypress"
                    Filter="Contains" OnClientSelectedIndexChanged="cboConsumeMethod_ClientSelectedIndexChanged" />
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
                <%--                <telerik:RadComboBox ID="cboAcPcDc" runat="server" Width="120px" AllowCustomText="true" OnClientFocus="showDropDown"
                    Filter="Contains">
                </telerik:RadComboBox>--%>
                <telerik:RadComboBox ID="cboMedicationConsume" runat="server" Width="120px" EmptyMessage="Select a Item"
                    EnableLoadOnDemand="true" ShowMoreResultsBox="true" EnableVirtualScrolling="true"
                    OnClientItemsRequesting="cboMedicationConsume_ClientItemsRequesting" OnClientFocus="showDropDown">
                    <WebServiceSettings Method="StandardReference" Path="~/WebService/ComboBoxDataService.asmx" />
                </telerik:RadComboBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvAcPcDc" runat="server" ErrorMessage="Please specify a Consume Time"
                    ValidationGroup="Item" ControlToValidate="cboMedicationConsume" SetFocusOnError="True"
                    Width="20px">
                    <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
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
                <telerik:RadTextBox ID="txtIter" runat="server" Width="50px" MaxLength="500" />
            </td>
            <td>
                <telerik:RadTextBox runat="server" ID="txtNotes" Width="185px" MaxLength="500" />
            </td>
            <td style="display: <%= DisplayPrice %>">
                <telerik:RadNumericTextBox runat="server" ID="txtLineAmount" Width="90px" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblFornasRestrictionNotes" runat="server" Text="" Font-Italic="true" ForeColor="OrangeRed"></asp:Label>
            </td>
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
