<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EyePeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EyePeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>

<div style="width: 100%; padding: 0 0 0 0;">
    <uc1:GcsCtl runat="server" ID="gcsCtl" />
</div>
<fieldset style="width: 98%;">
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
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRVisus" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Visus</td>
                        <td>
                            <telerik:RadTextBox ID="txtLVisus" runat="server" Width="100%" /></td>

                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRRefractio" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Refractio</td>
                        <td>
                            <telerik:RadTextBox ID="txtLRefractio" runat="server" Width="100%" /></td>

                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRTension" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Tension culi</td>
                        <td>
                            <telerik:RadTextBox ID="txtLTension" runat="server" Width="100%" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCorrection" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Correction</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCorrection" runat="server" Width="100%" /></td>

                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRGlasses" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Reading glasses</td>
                        <td>
                            <telerik:RadTextBox ID="txtLGlasses" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtROcular" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Intra-ocular pressure</td>
                        <td>
                            <telerik:RadTextBox ID="txtLOcular" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRAnterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Segmen anterior</td>
                        <td>
                            <telerik:RadTextBox ID="txtLAnterior" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRPosterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Segmen Posterior</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPosterior" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtREyeBallPosition" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Eye Ball Position</td>
                        <td>
                            <telerik:RadTextBox ID="txtLEyeBallPosition" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtREyeBallMovement" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Eye Ball Movement</td>
                        <td>
                            <telerik:RadTextBox ID="txtLEyeBallMovement" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRConfrontation" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Confrontation</td>
                        <td>
                            <telerik:RadTextBox ID="txtLConfrontation" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtROrbitalBone" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Orbital Bone</td>
                        <td>
                            <telerik:RadTextBox ID="txtLOrbitalBone" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRPalpebra" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Palpebrae</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPalpebra" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRConjungtivaTars" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Conjungtiva Tars</td>
                        <td>
                            <telerik:RadTextBox ID="txtLConjungtivaTars" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRConjungtivaBulbi" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Conjungtiva Bulbi</td>
                        <td>
                            <telerik:RadTextBox ID="txtLConjungtivaBulbi" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRSclera" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Sclera</td>
                        <td>
                            <telerik:RadTextBox ID="txtLSclera" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRLimbCornea" runat="server" Width="100%" />
                        </td>
                        <td class="labelcaption">Limb Cornea</td>
                        <td>
                            <telerik:RadTextBox ID="txtLLimbCornea" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCornea" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Cornea</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCornea" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCameraOculiAnterior" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Camera Oculi Anterior</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCameraOculiAnterior" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRIris" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Iris</td>
                        <td>
                            <telerik:RadTextBox ID="txtLIris" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRPupil" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Pupil</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPupil" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRLens" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Lens</td>
                        <td>
                            <telerik:RadTextBox ID="txtLLens" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRFundus" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Fundus</td>
                        <td>
                            <telerik:RadTextBox ID="txtLFundus" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtRCorpusVitreum" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Corpus Vitreum</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCorpusVitreum" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtROther" runat="server" Width="100%" /></td>
                        <td class="labelcaption">Other</td>
                        <td>
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
    <tr id="trNutriSkrinning" runat="server" visible="false">
        <td class="label">Nutrition Skrinning</td>
        <td colspan="2">
            <asp:RadioButtonList ID="optNutritionSkrinning" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                <asp:ListItem Text="Malnutrition" Value="Malnutrition"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="label" valign="top">Notes</td>

        <td colspan="2">
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
</table>
