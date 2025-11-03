<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RasproHeader.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.RasproHeader" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<asp:Label runat="server" ID="lblRasproNote" Text=""></asp:Label>
<table style="width: 100%">
    <tr>
        <td style="width: 30%; vertical-align: top;">
            <table style="width: 100%">
                <tr>
                    <td class="label">Registration No
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Medical No
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Name
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="300px" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Birth Date
                    </td>
                    <td class="entry">
                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Width="160px" DatePopupButton-Visible="False" MinDate="01/01/1900" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">DPJP
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtParamedicName" runat="server" Width="300px" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Service Unit
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtServiceUnitName" runat="server" Width="300px" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>

            </table>

        </td>
        <td style="width: 30%; vertical-align: top;">
            <table style="width: 100%">

                <tr>
                    <td class="label">Advise By
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAdviseByParamedicName" runat="server" Width="300px" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Focus Infection
                    </td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtAbRestrictionName" runat="server" Width="300px" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="label">Date and Time
                    </td>
                    <td class="entry">
                        <telerik:RadDateTimePicker ID="txtRasproDateTime" runat="server" Width="160px" DatePopupButton-Visible="False" TimePopupButton-Visible="False" ReadOnly="true" />
                    </td>
                    <td style="width: 20px"></td>
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top; background-color: lightyellow;">
            <fieldset>
                <legend><b>Information</b></legend>
                Pasien <b>Imunnorcompromised</b> adalah semua pasien dengan salah satu atau lebih kondisi berikut ini:<br />

                1. Neonatus Berat Badan Lahir Rendah (BBLR)<br />
                2. Neonatus dengan kelahiran prematur<br />
                3. Neonatus dengan multipatologi (banyak komorbid)<br />
                4. Pasien-pasien geriatri dengan multipatologi, >1 komorbid, termasuk dengan infeksi yang diderita<br />
                5. Pasien-pasien dengan HIV / AIDS<br />
                6. Pasien-pasien dengan malignancy (keganasan)<br />
                7. Pasien-pasien dengan febrile neutropenia<br />
                8. Pasien-pasien dengan penyakit kronis / infeksi kronis, sirosis hati dan gagal ginjal kronik<br />
                9. Pasien-pasien dengan autoimmune dan / atau penggunaan immunosupresan lama
                    
            </fieldset>
        </td>
    </tr>
</table>
<hr />
