<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master"
    AutoEventWireup="true" CodeBehind="AtePatientsControlDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.AtePatientsControlDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="197px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image7" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPatientName" runat="server" Width="275px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUnitRoomBed" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                            <telerik:RadTextBox ID="txtUnitID" runat="server" Visible="False" />
                            <telerik:RadTextBox ID="txtBedID" runat="server" Visible="False" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            <asp:Label ID="lblClassName" runat="server" CssClass="labeldescription" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysicianName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td class="labelcaption">
                            <asp:Label ID="Label2" runat="server" Text="Patient Eating Control" Font-Bold="True"
                                ForeColor="DarkBlue"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblOrderNo" runat="server" Text="Order No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtOrderNo" runat="server" Width="300px" MaxLength="20" ReadOnly="true" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblControlDate" runat="server" Text="Control Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtControlDate" runat="server" Width="100px" Enabled="False">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMealSet" runat="server" Text="Meal Set"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMealSet" runat="server" Width="300px" ReadOnly="True"/>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="500" Height="43px"
                                TextMode="MultiLine" />
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%" valign="top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblCarbohydrate" runat="server" Text="Carbohydrate"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboCarbohydrate" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvCarbohydrate" runat="server" ErrorMessage="Carbohydrate required."
                                ValidationGroup="entry" ControlToValidate="cboCarbohydrate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVegetableSideDish" runat="server" Text="Vegetable Side Dish"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboVegetableSideDish" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvVegetableSideDish" runat="server" ErrorMessage="Vegetable Side Dish required."
                                ValidationGroup="entry" ControlToValidate="cboVegetableSideDish" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAnimalSideDish" runat="server" Text="Animal Side Dish"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboAnimalSideDish" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvAnimalSideDish" runat="server" ErrorMessage="Animal Side Dish required."
                                ValidationGroup="entry" ControlToValidate="cboAnimalSideDish" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblVegetable" runat="server" Text="Vegetable"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboVegetable" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvVegetable" runat="server" ErrorMessage="Vegetable required."
                                ValidationGroup="entry" ControlToValidate="cboVegetable" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFruit" runat="server" Text="Fruit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboFruit" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvFruit" runat="server" ErrorMessage="Fruit required."
                                ValidationGroup="entry" ControlToValidate="cboFruit" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image5" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBeverage" runat="server" Text="Beverage"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboBeverage" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvBeverage" runat="server" ErrorMessage="Beverage required."
                                ValidationGroup="entry" ControlToValidate="cboBeverage" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblReason" runat="server" Text="Reason"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox runat="server" ID="cboSREatingPatientStatusReason" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
