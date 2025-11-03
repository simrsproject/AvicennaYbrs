<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EyePeV2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EyePeV2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>

<%--<div style="width: 100%; padding: 0 0 0 0;">
    <uc1:GcsCtl runat="server" ID="gcsCtl" />
</div>--%>
<fieldset style="width: 80%;">
    <legend>&nbsp;Localist Status&nbsp;</legend>
    <table style="width: 100%">

        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="labelcaption" style="width: 40%">Right Eye</td>
                        <td class="labelcaption" style="width: 20%">&nbsp;</td>
                        <td class="labelcaption" style="width: 40%">Left Eye</td>

                    </tr>
                    <%--                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRVisus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRVisus" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Visus</td>
                        <td>
                            <asp:RadioButtonList ID="optLVisus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLVisus" runat="server" Width="100%" /></td>

                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRRefractio" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRRefractio" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Refractio</td>
                        <td>
                            <asp:RadioButtonList ID="optLRefractio" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLRefractio" runat="server" Width="100%" /></td>

                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRTension" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRTension" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Tension culi</td>
                        <td>
                            <asp:RadioButtonList ID="optLTension" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLTension" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRCorrection" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRCorrection" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Correction</td>
                        <td>
                            <asp:RadioButtonList ID="optLCorrection" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLCorrection" runat="server" Width="100%" /></td>

                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRGlasses" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRGlasses" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Reading glasses</td>
                        <td>
                            <asp:RadioButtonList ID="optLGlasses" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLGlasses" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optROcular" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtROcular" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Intra-ocular pressure</td>
                        <td>
                            <asp:RadioButtonList ID="optLOcular" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLOcular" runat="server" Width="100%" /></td>
                    </tr>--%>
                    <%--                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRAnterior" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRAnterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Segmen anterior</td>
                        <td>
                            <asp:RadioButtonList ID="optLAnterior" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLAnterior" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRPosterior" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRPosterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Segmen Posterior</td>
                        <td>
                            <asp:RadioButtonList ID="optLPosterior" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLPosterior" runat="server" Width="100%" /></td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optREyeBallPosition" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtREyeBallPosition" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Eye Ball Position</td>
                        <td>
                            <asp:RadioButtonList ID="optLEyeBallPosition" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLEyeBallPosition" runat="server" Width="100%" /></td>
                    </tr>
                    <%--                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optREyeBallMovement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtREyeBallMovement" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Eye Ball Movement</td>
                        <td>
                            <asp:RadioButtonList ID="optLEyeBallMovement" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLEyeBallMovement" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRConfrontation" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRConfrontation" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Confrontation</td>
                        <td>
                            <asp:RadioButtonList ID="optLConfrontation" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLConfrontation" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optROrbitalBone" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtROrbitalBone" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Orbital Bone</td>
                        <td>
                            <asp:RadioButtonList ID="optLOrbitalBone" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLOrbitalBone" runat="server" Width="100%" /></td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRPalpebra" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRPalpebra" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Palpebrae</td>
                        <td>
                            <asp:RadioButtonList ID="optLPalpebra" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLPalpebra" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRConjungtivaTars" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRConjungtivaTars" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Conjungtiva Tars</td>
                        <td>
                            <asp:RadioButtonList ID="optLConjungtivaTars" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLConjungtivaTars" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRConjungtivaBulbi" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRConjungtivaBulbi" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Conjungtiva Bulbi</td>
                        <td>
                            <asp:RadioButtonList ID="optLConjungtivaBulbi" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLConjungtivaBulbi" runat="server" Width="100%" /></td>
                    </tr>
                    <%--                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRSclera" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRSclera" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Sclera</td>
                        <td>
                            <asp:RadioButtonList ID="optLSclera" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLSclera" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRLimbCornea" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRLimbCornea" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Limb Cornea</td>
                        <td>
                            <asp:RadioButtonList ID="optLLimbCornea" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="false"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLLimbCornea" runat="server" Width="100%" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRCornea" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRCornea" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Cornea</td>
                        <td>
                            <asp:RadioButtonList ID="optLCornea" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLCornea" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRCameraOculiAnterior" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRCameraOculiAnterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Camera Oculi Anterior</td>
                        <td>
                            <asp:RadioButtonList ID="optLCameraOculiAnterior" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLCameraOculiAnterior" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRIris" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRIris" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Iris</td>
                        <td>
                            <asp:RadioButtonList ID="optLIris" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLIris" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRPupil" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRPupil" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Pupil</td>
                        <td>
                            <asp:RadioButtonList ID="optLPupil" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLPupil" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRLens" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRLens" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Lens</td>
                        <td>
                            <asp:RadioButtonList ID="optLLens" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLLens" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRCorpusVitreum" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRCorpusVitreum" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Corpus Vitreum</td>
                        <td>
                            <asp:RadioButtonList ID="optLCorpusVitreum" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLCorpusVitreum" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optRFundus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtRFundus" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Fundus</td>
                        <td>
                            <asp:RadioButtonList ID="optLFundus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLFundus" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optROther" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtROther" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Other</td>
                        <td>
                            <asp:RadioButtonList ID="optLOther" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                            </asp:RadioButtonList>
                            <telerik:RadTextBox ID="txtLOther" runat="server" Width="100%" /></td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</fieldset>
<fieldset style="width: 98%;">
    <legend>&nbsp; Ishihara Test&nbsp;</legend>
    <table width="100%">
        <tr>
            <td class="label" valign="top">Result</td>
            <td style="width: 280px" valign="top">
                <asp:RadioButtonList ID="optIshihara" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No Test" Value="X"></asp:ListItem>
                    <asp:ListItem Text="No Color Blind" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Color Blind :" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtIshihara" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" />
            </td>
        </tr>
    </table>
</fieldset>
<table width="100%">
    <tr>
        <td class="label" valign="top">Notes</td>

        <td colspan="2">
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
</table>
