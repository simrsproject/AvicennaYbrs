<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ParamedicFeeByFee4ServiceSettingDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.ParamedicFeeByFee4ServiceSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label16" runat="server" Text="Paramedic Type" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicStatus" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="Paramedic" Width="100px"></asp:Label>
                            <asp:HiddenField ID="hfId" runat="server" />
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedic" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr style="display:none;">
                        <td class="label">
                            <asp:Label ID="Label11" runat="server" Text="Specialty" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSpecialty" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationType" runat="server" Text="Registration Type"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRRegistrationType" runat="server" Width="300px" AutoPostBack="True"
                                OnSelectedIndexChanged="cboSRRegistrationType_SelectedIndexChanged" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label12" runat="server" Text="Tariff Type" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTariffType" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label13" runat="server" Text="Class" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboClass" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label15" runat="server" Text="Guarantor Type" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboGuarantorType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>

                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Guarantor" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboGuarantor" runat="server" Width="300px"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboGuarantor_ItemDataBound"
                                OnItemsRequested="cboGuarantor_ItemsRequested">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>

                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboServiceUnit" runat="server" Width="300px"
                                OnItemDataBound="cboServiceUnit_ItemDataBound"
                                OnItemsRequested="cboServiceUnit_ItemsRequested" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label5" runat="server" Text="Item Group" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemGroup" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>

                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label9" runat="server" Text="Item"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItem" runat="server" Width="300px" AutoPostBack="true"
                                EnableLoadOnDemand="True" HighlightTemplatedItems="True" MarkFirstMatch="False"
                                OnItemDataBound="cboItem_ItemDataBound"
                                OnItemsRequested="cboItem_ItemsRequested"
                                OnSelectedIndexChanged="cboItem_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label4" runat="server" Text="Procedure" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSRProcedure" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains" />
                        </td>
                        <td width="20"></td>

                        <td></td>
                    </tr>

                    <tr>
                        <td class="label">
                            <asp:Label ID="Label6" runat="server" Text="Tarif Component"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboTariffComponent" runat="server" Width="300px" AllowCustomText="true"
                                Filter="Contains">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsUsingFormula" runat="server" Text="Use Formula" OnCheckedChanged="chkIsUsingFormula_CheckedChanged" AutoPostBack="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsFeeValueInPercent" runat="server" Text="Fee Value In Percent" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsFeeValueFromPlafon" runat="server" Text="Fee Value From Plafon" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                        </td>
                        <td class="entry">
                            <asp:CheckBox ID="chkIsFeeValueFromTariffPrice" runat="server" Text="Fee Value From Tariff Price" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label7" runat="server" Text="Fee Value"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtFeeValue" runat="server" Type="Number" NumberFormat-DecimalDigits="2"
                                Width="100px" MinValue="0" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Fee value required."
                                ControlToValidate="txtFeeValue" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="label">
                <asp:Label ID="Label17" runat="server" Text="Formula"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtFormula" runat="server" Width="300px" MaxLength="500" TextMode="MultiLine" Height="150px" ></telerik:RadTextBox>
            </td>
            <td><asp:Label ID="Label8" runat="server" Text="@comp = Tarif Component <br />@tarif = Tarif <br />@plafon = Plafon <br />@bill = Total Billing <br />@patPayment = Patient's Payment <br />@toIPR = Transfer To Inpatient (0 / 1) <br />@isDpjpIPR = DPJP Inpatient (0 / 1) <br />@isSurgeryCase = Kasus Operasi atau bukan (0 / 1) <br />TakeOneHighest() = Fungsi untuk identifikasi ambil 1 nilai tertinggi, yang lainnya diset jadi 0"></asp:Label></td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label10" runat="server" Text="Notes"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="255" TextMode="MultiLine" Height="100px" ></telerik:RadTextBox>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
