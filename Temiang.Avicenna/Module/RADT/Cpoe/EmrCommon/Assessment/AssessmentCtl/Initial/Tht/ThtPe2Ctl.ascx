<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThtPe2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ThtPe2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>


<table style="width: 100%">
    <tr>
        <td style="width: 50%" valign="top">
            <uc1:GcsCtl runat="server" ID="gcsCtl" />
            <fieldset>
                <legend><b>EARS</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="labelcaption">Regio</td>
                        <td class="labelcaption">Right</td>
                        <td class="labelcaption">Left</td>
                    </tr>
                    <tr>
                        <td class="label">
                            <div style="font-weight: bold;">Outer ear:</div>
                            <div style="padding-left: 10px;">• Auricle</div>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadTextBox ID="txtRDaun" runat="server" Width="100%" />
                        </td>
                        <td class="entry2Column">
                            <telerik:RadTextBox ID="txtLDaun" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	External Acoustic Meatus (auditory canal)</td>
                        <td>
                            <telerik:RadTextBox ID="txtRLiang" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLLiang" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	Discharge</td>
                        <td>
                            <telerik:RadTextBox ID="txtRDischarge" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLDischarge" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	Tympanic Membrane (eardrum)</td>
                        <td>
                            <telerik:RadTextBox ID="txtRTympani" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLTympani" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Tumor</td>
                        <td>
                            <telerik:RadTextBox ID="txtRTumor" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLTumor" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Mastoid</td>
                        <td>
                            <telerik:RadTextBox ID="txtRMastoid" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLMastoid" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Pre Aurikula</td>
                        <td>
                            <telerik:RadTextBox ID="txtRPreAurikula" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLPreAurikula" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Post Aurikula</td>
                        <td>
                            <telerik:RadTextBox ID="txtRPostAurikula" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLPostAurikula" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Hearing Tests</td>
                        <td>
                            <telerik:RadTextBox ID="txtRPendengaran" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLPendengaran" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Audiometri</td>
                        <td>
                            <telerik:RadTextBox ID="txtRAudiometri" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLAudiometri" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">OAE</td>
                        <td>
                            <telerik:RadTextBox ID="txtROae" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLOae" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Balance Tests</td>
                        <td>
                            <telerik:RadTextBox ID="txtRKeseimbangan" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLKeseimbangan" runat="server" Width="100%" />
                        </td>

                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend><b>NOSE</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="labelcaption">Regio</td>
                        <td class="labelcaption">Right</td>
                        <td class="labelcaption">Left</td>

                    </tr>
                    <tr>
                        <td class="label">Tests Nose</td>
                        <td class="entry2Column">
                            <telerik:RadTextBox ID="txtRTestHidung" runat="server" Width="100%" />
                        </td>
                        <td class="entry2Column">
                            <telerik:RadTextBox ID="txtLTestHidung" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">External Nose</td>
                        <td>
                            <telerik:RadTextBox ID="txtRHidungLuar" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLHidungLuar" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Kavum Nasi</td>
                        <td>
                            <telerik:RadTextBox ID="txtRKavum" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLKavum" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Septum</td>
                        <td>
                            <telerik:RadTextBox ID="txtRSeptum" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLSeptum" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Discharge</td>
                        <td>
                            <telerik:RadTextBox ID="txtRNoseDischarge" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLNoseDischarge" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Mukosa</td>
                        <td>
                            <telerik:RadTextBox ID="txtRMukosa" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLMukosa" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Tumor</td>
                        <td>
                            <telerik:RadTextBox ID="txtRNoseTumor" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLNoseTumor" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Konka</td>
                        <td>
                            <telerik:RadTextBox ID="txtRKonka" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLKonka" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Sinus Paranasal</td>
                        <td>
                            <telerik:RadTextBox ID="txtRSinus" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLSinus" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Koana</td>
                        <td>
                            <telerik:RadTextBox ID="txtRKoana" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLKoana" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Naso Endoskopi</td>
                        <td>
                            <telerik:RadTextBox ID="txtRNaso" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLNaso" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">
                            <div style="font-weight: bold;">Internal Nose:</div>
                            <div style="padding-left: 10px;">•	Rhinoscopi Anterior</div>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtRAnterior" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLAnterior" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	Rhinoscopi Posterior</td>
                        <td>
                            <telerik:RadTextBox ID="txtRPosterior" runat="server" Width="100%" />
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLPosterior" runat="server" Width="100%" />
                        </td>

                    </tr>

                </table>
            </fieldset>

            <fieldset>
                <legend><b>LARINX</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="label">Epiglotis</td>
                        <td class="entry" style="width: 400px">
                            <telerik:RadTextBox ID="txtEpiglotis" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Plika Vokalis</td>
                        <td class="entry" style="width: 400px">
                            <telerik:RadTextBox ID="txtPVokal" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Plika Ventrikulosis</td>
                        <td class="entry" style="width: 400px">
                            <telerik:RadTextBox ID="txtPVentri" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Aritenoid</td>
                        <td class="entry" style="width: 400px">
                            <telerik:RadTextBox ID="txtAritenoid" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Rimaglotis</td>
                        <td class="entry" style="width: 400px">
                            <telerik:RadTextBox ID="txtRimaglotis" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Endoskopi</td>
                        <td class="entry" style="width: 400px">
                            <telerik:RadTextBox ID="txtEndoskopi" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend><b>THROAT</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="label">Oral cavity</td>
                        <td class="entry" style="width: 400px">
                            <telerik:RadTextBox ID="txtRRonggaMulut" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Teeth</td>
                        <td>
                            <telerik:RadTextBox ID="txtRGigi" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Faring</td>
                        <td>
                            <telerik:RadTextBox ID="txtRFaring" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td class="label">Laring</td>
                        <td>
                            <telerik:RadTextBox ID="txtRLaring" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Dypneu</td>
                        <td>
                            <telerik:RadTextBox ID="txtDypneu" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Sianosis</td>
                        <td>
                            <telerik:RadTextBox ID="txtSianosis" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Mucosa</td>
                        <td>
                            <telerik:RadTextBox ID="txtMucosa" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Stridor</td>
                        <td>
                            <telerik:RadTextBox ID="txtStridor" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Sound</td>
                        <td>
                            <telerik:RadTextBox ID="txtSound" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="label">Tonsils</td>
                        <td>
                            <telerik:RadTextBox ID="txtRTonsil" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
            </fieldset>

            <table style="width: 100%">
                <tr>
                    <td class="label">Head-Neck</td>
                    <td class="entry">
                        <telerik:RadTextBox ID="txtKepalaLeher" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Trakea</td>
                    <td>
                        <telerik:RadTextBox ID="txtTrakea" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Neck lymph glands</td>
                    <td>
                        <telerik:RadTextBox ID="txtNeckLymph" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Esofagus</td>
                    <td>
                        <telerik:RadTextBox ID="txtEsofagus" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Bronkus</td>
                    <td>
                        <telerik:RadTextBox ID="txtBronkus" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
                <tr>
                    <td class="label">Other Exam Notes</td>
                    <td>
                        <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                            TextMode="MultiLine" Resize="Vertical" />
                    </td>

                </tr>
            </table>
        </td>
    </tr>
</table>

