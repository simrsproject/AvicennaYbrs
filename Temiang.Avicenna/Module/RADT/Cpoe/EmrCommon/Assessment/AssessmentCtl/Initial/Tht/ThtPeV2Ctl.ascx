<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThtPeV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ThtPeV2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>


<table style="width: 100%">
    <tr>
        <td style="width: 50%" valign="top">
            <uc1:GcsCtl runat="server" ID="gcsCtl" />
            <fieldset>
                <legend><b>EARS</b></legend>
                <table style="width: 100%">
                    <%--                    <tr>
                        <td class="labelcaption">Regio</td>
                        <td class="labelcaption">Right</td>
                        <td class="labelcaption">Left</td>
                    </tr>--%>
                    <tr>
                        <td class="label">
                            <div style="font-weight: bold;">Outer ear:</div>
                            <div style="padding-left: 10px;">• Auricle</div>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="optDaun" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtDaun" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	External Acoustic Meatus (auditory canal)</td>
                        <td>
                            <asp:RadioButtonList ID="optLiang" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtLiang" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	Discharge</td>
                        <td>
                            <asp:RadioButtonList ID="optDischargeEARS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtDischargeEARS" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	Serumen</td>
                        <td>
                            <asp:RadioButtonList ID="optSerumen" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSerumen" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label" style="padding-left: 20px;">•	Tympanic Membrane (eardrum)</td>
                        <td>
                            <asp:RadioButtonList ID="optTympani" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTympani" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Tumor</td>
                        <td>
                            <asp:RadioButtonList ID="optTumorEARS" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTumorEARS" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Mastoid</td>
                        <td>
                            <asp:RadioButtonList ID="optMastoid" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtMastoid" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Pre Aurikula</td>
                        <td>
                            <asp:RadioButtonList ID="OptPreAurikula" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPreAurikula" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Post Aurikula</td>
                        <td>
                            <asp:RadioButtonList ID="optPostAurikula" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPostAurikula" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Hearing Tests</td>
                        <td>
                            <asp:RadioButtonList ID="optHearingTests" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtHearingTests" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Audiometri</td>
                        <td>
                            <asp:RadioButtonList ID="optAudiometri" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtAudiometri" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">OAE</td>
                        <td>
                            <asp:RadioButtonList ID="optOAE" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtOAE" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Balance Tests</td>
                        <td>
                            <asp:RadioButtonList ID="OptBalanceTests" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtBalanceTests" runat="server" Width="100%" />

                        </td>

                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend><b>NOSE</b></legend>
                <table style="width: 100%">
                    <%--                    <tr>
                        <td class="labelcaption">Regio</td>
                        <td class="labelcaption">Right</td>
                        <td class="labelcaption">Left</td>

                    </tr>--%>
                    <%--                    <tr>
                        <td class="label">Tests Nose</td>
                        <td class="entry2Column">
                            <telerik:RadTextBox ID="txtRTestHidung" runat="server" Width="100%" />
                        </td>
                        <td class="entry2Column">
                            <telerik:RadTextBox ID="txtLTestHidung" runat="server" Width="100%" />
                        </td>

                    </tr>--%>
                    <tr>
                        <td class="label">External Nose</td>
                        <td>
                            <asp:RadioButtonList ID="optExternalNose" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtExternalNose" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Kavum Nasi</td>
                        <td>
                            <asp:RadioButtonList ID="optKavumNasi" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtKavumNasi" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Septum</td>
                        <td>
                            <asp:RadioButtonList ID="optSeptum" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSeptum" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Discharge</td>
                        <td>
                            <asp:RadioButtonList ID="optDischargeNOSE" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtDischargeNOSE" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Mukosa</td>
                        <td>
                            <asp:RadioButtonList ID="optMukosa" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtMukosa" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Tumor</td>
                        <td>
                            <asp:RadioButtonList ID="optTumorNOSE" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTumorNOSE" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Konka</td>
                        <td>
                            <asp:RadioButtonList ID="optKonka" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtKonka" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Sinus Paranasal</td>
                        <td>
                            <asp:RadioButtonList ID="optSinus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSinus" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Koana</td>
                        <td>
                            <asp:RadioButtonList ID="optKoana" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtKoana" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <%--                    <tr>
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

                    </tr>--%>
                </table>
            </fieldset>

            <fieldset>
                <legend><b>LARINX</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="label">Epiglotis</td>
                        <td>
                            <asp:RadioButtonList ID="optEpiglotis" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtEpiglotis" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Plika Vokalis</td>
                        <td>
                            <asp:RadioButtonList ID="optPVokalis" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPVokalis" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Plika Ventrikulosis</td>
                        <td>
                            <asp:RadioButtonList ID="optPVentrikulosis" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPVentrikulosis" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Aritenoid</td>
                        <td>
                            <asp:RadioButtonList ID="optAritenoid" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtAritenoid" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Rimaglotis</td>
                        <td>
                            <asp:RadioButtonList ID="optRimaglotis" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtRimaglotis" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Endoskopi</td>
                        <td>
                            <asp:RadioButtonList ID="optEndoskopi" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtEndoskopi" runat="server" Width="100%" />

                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend><b>FARINGS</b></legend>
                <table style="width: 100%">
                    <tr>
                        <td class="label">Oral cavity</td>
                        <td>
                            <asp:RadioButtonList ID="optOcavity" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtOcavity" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Tonsils</td>
                        <td>
                            <asp:RadioButtonList ID="optTonsils" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtTonsils" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Faring</td>
                        <td>
                            <asp:RadioButtonList ID="optFaring" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtFaring" runat="server" Width="100%" />

                        </td>

                    </tr>
                    <tr>
                        <td class="label">Mucosa</td>
                        <td>
                            <asp:RadioButtonList ID="optMucosa" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtMucosa" runat="server" Width="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td class="label">Sound</td>
                        <td>
                            <asp:RadioButtonList ID="optSound" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSound" runat="server" Width="100%" />

                        </td>
                    </tr>
                </table>
            </fieldset>

            <table style="width: 100%">
                <tr>
                    <td class="label">Facilo</td>
                    <td>
                        <asp:RadioButtonList ID="optHNeck" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                            <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtHNeck" runat="server" Width="100%" />

                    </td>

                </tr>
                <tr>
                    <td class="label">Trakea</td>
                    <td>
                        <asp:RadioButtonList ID="optTrakea" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                            <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtTrakea" runat="server" Width="100%" />

                    </td>

                </tr>
                <tr>
                    <td class="label">Neck</td>
                    <td>
                        <asp:RadioButtonList ID="optNlglands" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                            <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtNlglands" runat="server" Width="100%" />

                    </td>

                </tr>
                <tr>
                    <td class="label">Esofagus</td>
                    <td>
                        <asp:RadioButtonList ID="optEsofagus" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                            <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEsofagus" runat="server" Width="100%" />

                    </td>

                </tr>
                <tr>
                    <td class="label">Bronkus</td>
                    <td>
                        <asp:RadioButtonList ID="optBronkus" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                            <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtBronkus" runat="server" Width="100%" />

                    </td>

                </tr>
<%--                <tr>
                    <td class="label">Other Exam Notes</td>
                    <td>
                        <asp:RadioButtonList ID="optOENotes" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                            <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtOENotes" runat="server" Width="100%" />

                    </td>

                </tr>--%>
            </table>
        </td>
    </tr>
</table>

