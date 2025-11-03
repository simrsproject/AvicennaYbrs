<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="ServiceFeeSettingDetail.aspx.cs"
    Inherits="Temiang.Avicenna.Module.Finance.Master.ServiceFeeSettingDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 40%; vertical-align: top;">
                <table width="100%">
                    <tr style="display:none;">
                        <td class="label">
                            <asp:Label ID="Label14" runat="server" Text="Level" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadNumericTextBox ID="txtLevel" runat="server" Type="Number" NumberFormat-DecimalDigits="0" Width="100px" MinValue="1" />
                        </td>
                        <td width="20">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Level required."
                                ControlToValidate="txtLevel" ValidationGroup="entry" SetFocusOnError="True"
                                Width="100%">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="display:none;">
                        <td class="label">
                            <asp:Label ID="Label16" runat="server" Text="Paramedic Type" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboParamedicStatus" runat="server" Width="300px" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr style="display:none;">
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
                            <asp:Label ID="Label11" runat="server" Text="SMF" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboSmf" runat="server" Width="300px" AllowCustomText="true"
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
                            <asp:Label ID="Label17" runat="server" Text="Item Condition Rule Type" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemConditionRuleType" runat="server" Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td width="20"></td>

                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label18" runat="server" Text="Item Condition Rule" Width="100px"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadComboBox ID="cboItemConditionRule" runat="server" Width="300px">
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
                </table>
            </td>
            <td style="width: 60%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label10" runat="server" Text="Notes"></asp:Label>
                        </td>
                        <td class="entry">
                             <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="255" TextMode="MultiLine" Height="50px" ></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblFormula" runat="server" Text="Formula Direktur"></asp:Label>
                        </td>
                        <td class="entry">
                             <telerik:RadTextBox ID="txtFormulaDirektur" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine" Height="70px" ></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label8" runat="server" Text="Formula Struktural"></asp:Label>
                        </td>
                        <td class="entry">
                             <telerik:RadTextBox ID="txtFormulaStruktural" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine" Height="70px" ></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label19" runat="server" Text="Formula Medis"></asp:Label>
                        </td>
                        <td class="entry">
                             <telerik:RadTextBox ID="txtFormulaMedis" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine" Height="70px" ></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label20" runat="server" Text="Formula Unit"></asp:Label>
                        </td>
                        <td class="entry">
                             <telerik:RadTextBox ID="txtFormulaUnit" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine" Height="70px" ></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label21" runat="server" Text="Formula Pemerataan"></asp:Label>
                        </td>
                        <td class="entry">
                             <telerik:RadTextBox ID="txtFormulaPemerataan" runat="server" Width="100%" MaxLength="500" TextMode="MultiLine" Height="70px" ></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <th>Variable</th>
                                    <th>&nbsp;</th>
                                    <th>Description</th>
                                    <th>&nbsp;</th>
                                    <th>Type</th>
                                </tr>
                                <tr style="display:none;">
                                    <td>@prevLevel</td>
                                    <td>&nbsp;</td>
                                    <td>Nilai jasa dari perhitungan rumus level sebelumnya</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@totalPrevLevel</td>
                                    <td>&nbsp;</td>
                                    <td>Total nilai jasa dari perhitungan rumus level sebelumnya</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr>
                                    <td>@comp</td>
                                    <td>&nbsp;</td>
                                    <td>Tarif Component</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr>
                                    <td>@tarif</td>
                                    <td>&nbsp;</td>
                                    <td>Tarif</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr>
                                    <td>@plafon</td>
                                    <td>&nbsp;</td>
                                    <td>Plafon (diambil dari total guarantor receive / AR Uninvoiced)</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr>
                                    <td>@bill</td>
                                    <td>&nbsp;</td>
                                    <td>Total Billing</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr>
                                    <td>@patPayment</td>
                                    <td>&nbsp;</td>
                                    <td>Pembayaran pasien (cash, personal AR, discount)</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr>
                                    <td>@toIPR</td>
                                    <td>&nbsp;</td>
                                    <td>Transfer Ke Inpatient</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@isDelegation</td>
                                    <td>&nbsp;</td>
                                    <td>Apakah tarif delegasi atau bukan (berdasarkan master tarif, delegasi ke perawat)</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@name</td>
                                    <td>&nbsp;</td>
                                    <td>Nama tarif (string lower case)</td>
                                    <td>&nbsp;</td>
                                    <td>Text</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@isDpjpIPR</td>
                                    <td>&nbsp;</td>
                                    <td>Identifikasi DPJP Inpatient untuk transaksi non IPR</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@isDpjpByReg</td>
                                    <td>&nbsp;</td>
                                    <td>Identifikasi DPJP dilihat dari data Registrasi</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@isSurgeryCase</td>
                                    <td>&nbsp;</td>
                                    <td>Kasus Operasi atau bukan</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@surgeryCount</td>
                                    <td>&nbsp;</td>
                                    <td>Jumlah berapa kali operasi</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@isSurgeon</td>
                                    <td>&nbsp;</td>
                                    <td>Dokter Operator atau bukan [berdasarkan realisasi booking operasi]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@isDpjpEqualSurgeon</td>
                                    <td>&nbsp;</td>
                                    <td>Identifikasi apakah dokter DPJP (berdasarkan REG) sama dengan dokter Operator (berdasarkan realisasi operasi)</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@isAnesthetist</td>
                                    <td>&nbsp;</td>
                                    <td>Dokter Anestesi atau bukan [berdasarkan realisasi booking operasi]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@isParturition</td>
                                    <td>&nbsp;</td>
                                    <td>Kasus melahirkan atau bukan [berdasarkan flag Parturition saat registrasi]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@isNewBorn</td>
                                    <td>&nbsp;</td>
                                    <td>Bayi baru lahir atau bukan [berdasarkan flag New Born Infant saat registrasi]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@isHealthyByBirthRecord</td>
                                    <td>&nbsp;</td>
                                    <td>Bayi sehat atau bukan [berdasarkan patient birth record, jika tidak ada data maka default adalah true / dianggap bayi sehat]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@isCOB</td>
                                    <td>&nbsp;</td>
                                    <td>Lebih dari 1 penjamin bayar atau tidak [berdasarkan registrasi]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@hasConsulen</td>
                                    <td>&nbsp;</td>
                                    <td>Registrasi yang bersangkutan punya dokter konsulen atau tidak [berdasarkan physician team]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@hasRaber</td>
                                    <td>&nbsp;</td>
                                    <td>Registrasi yang bersangkutan punya dokter rawat bersama atau tidak [berdasarkan physician team]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@isPhyConsulen</td>
                                    <td>&nbsp;</td>
                                    <td>Dokter yang bersangkutan adalah dokter konsulen atau bukan [berdasarkan physician team]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>@isPhyRaber</td>
                                    <td>&nbsp;</td>
                                    <td>Dokter yang bersangkutan adalah dokter rawat bersama atau bukan [berdasarkan physician team]</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>@firstRegServiceUnitID</td>
                                    <td>&nbsp;</td>
                                    <td>Kode service unit saat pasien masuk perawatan [bisa dipakai untuk identifikasi bayi sehat jika bayi sehat atau bayi sakit ditempatkan di service unit tersendiri, cth: Contains('SU01,SU02,SU03',@firstRegServiceUnitID)]</td>
                                    <td>&nbsp;</td>
                                    <td>Text</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>TakeOneHighest(value) / TOH(value)</td>
                                    <td>&nbsp;</td>
                                    <td>Fungsi untuk identifikasi ambil 1 nilai tertinggi, yang lainnya diset jadi 0</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>TakeByItem(maxcount, value) / TBI(maxcount, value)</td>
                                    <td>&nbsp;</td>
                                    <td>Fungsi untuk menempatkan maksimal sebanyak maxcount per item, sorting yang paling besar berdasarkan value</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>GetBySmf(smfid)</td>
                                    <td>&nbsp;</td>
                                    <td>Fungsi untuk mengambil total fee berdasarkan smf, kosongkan smf untuk mengambil semua smf selain dokter yang bersangkutan</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>GetByServUnit(ServiceUnitID)</td>
                                    <td>&nbsp;</td>
                                    <td>Fungsi untuk mengambil total fee berdasarkan service unit</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>GetBySurgeon()</td>
                                    <td>&nbsp;</td>
                                    <td>Fungsi untuk mengambil total fee dokter operator, [identifikasi dokter operator berdasarkan realisasi booking operasi] </td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>GetByItemNameLike(ItemName)</td>
                                    <td>&nbsp;</td>
                                    <td>Fungsi untuk mengambil total fee berdasarkan item name</td>
                                    <td>&nbsp;</td>
                                    <td>Num</td>
                                </tr>
                                <tr>
                                    <td>Contains(Str1, Str2)</td>
                                    <td>&nbsp;</td>
                                    <td>Fungsi untuk mengecek apakah Str1 mengandung string Str2, return bool (true or false)</td>
                                    <td>&nbsp;</td>
                                    <td>Bool</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr style="display:none;">
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="3">
                                        <fieldset>
                                            <legend>Penting</legend>
                                            Jika ada fungsi TakeOneHighest atau TakeByItem dalam formula keseluruhan maka fungsi GetBy harus dibuat pada formula level berikutnya setelah level TakeOneHighest atau TakeByItem
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
