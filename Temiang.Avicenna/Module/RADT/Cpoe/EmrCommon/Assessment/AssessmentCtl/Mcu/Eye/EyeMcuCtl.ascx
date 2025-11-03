<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EyeMcuCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EyeMcuCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>

<fieldset style="width: 98%;">
    <legend>EXAMINATION</legend>
    <table style="width: 100%">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="label" colspan="4">Spectacles : </td>
                    </tr>
                    <tr>
                        <td class="label">OD</td>
                        <td>
                            <telerik:RadTextBox ID="txtOd" runat="server" Width="100%" />
                        </td>
                        <td class="label">OS</td>
                        <td>
                            <telerik:RadTextBox ID="txtOs" runat="server" Width="100%" /></td>
                    </tr>
                    <tr>
                        <td class="label" colspan="4">Presbyopia : </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <telerik:RadTextBox ID="txtPresbyopia" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="divcaption">Status Lokalis</div>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td class="labelcaption" colspan="3">Right Eye</td>
                    </tr>
                    <tr>
                        <td class="label">•	Visus</td>
                        <td>
                            <telerik:RadTextBox ID="txtRVisus" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Correction</td>
                        <td>
                            <telerik:RadTextBox ID="txtRCorrection" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Additions</td>
                        <td>
                            <telerik:RadTextBox ID="txtRAdditions" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" colspan="3"><b>Eyeball Movement</b></td>
                    </tr>
                    <tr>
                        <td class="label">•	Position</td>
                        <td>
                            <telerik:RadTextBox ID="txtRPosition" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Palpebra</td>
                        <td>
                            <telerik:RadTextBox ID="txtRPalpebra" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Conjunctiva</td>
                        <td>
                            <telerik:RadTextBox ID="txtRConjunctiva" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Cornea</td>
                        <td>
                            <telerik:RadTextBox ID="txtRCornea" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	COA</td>
                        <td>
                            <telerik:RadTextBox ID="txtRCoa" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Pupil</td>
                        <td>
                            <telerik:RadTextBox ID="txtRPupil" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Iris</td>
                        <td>
                            <telerik:RadTextBox ID="txtRIris" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Lens</td>
                        <td>
                            <telerik:RadTextBox ID="txtRLens" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Vitreous</td>
                        <td>
                            <telerik:RadTextBox ID="txtRVitreous" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Fundus</td>
                        <td>
                            <telerik:RadTextBox ID="txtRFundus" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	TIO</td>
                        <td>
                            <telerik:RadTextBox ID="txtRTio" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Campus</td>
                        <td>
                            <telerik:RadTextBox ID="txtRCampus" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                </table>

            </td>
            <td style="width: 50%">
                <table style="width: 100%">
                    <tr>
                        <td class="labelcaption" colspan="3">Left Eye</td>
                    </tr>
                    <tr>
                        <td class="label">•	Visus</td>
                        <td>
                            <telerik:RadTextBox ID="txtLVisus" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Correction</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCorrection" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Additions</td>
                        <td>
                            <telerik:RadTextBox ID="txtLAdditions" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label" colspan="3"><b>Eyeball Movement</b></td>
                    </tr>
                    <tr>
                        <td class="label">•	Position</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPosition" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Palpebra</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPalpebra" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Conjunctiva</td>
                        <td>
                            <telerik:RadTextBox ID="txtLConjunctiva" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Cornea</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCornea" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	COA</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCoa" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Pupil</td>
                        <td>
                            <telerik:RadTextBox ID="txtLPupil" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Iris</td>
                        <td>
                            <telerik:RadTextBox ID="txtLIris" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Lens</td>
                        <td>
                            <telerik:RadTextBox ID="txtLLens" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Vitreous</td>
                        <td>
                            <telerik:RadTextBox ID="txtLVitreous" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Fundus</td>
                        <td>
                            <telerik:RadTextBox ID="txtLFundus" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	TIO</td>
                        <td>
                            <telerik:RadTextBox ID="txtLTio" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">•	Campus</td>
                        <td>
                            <telerik:RadTextBox ID="txtLCampus" runat="server" Width="100%" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="width: 50%">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <div class="divcaption">Ishihara Test</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="label" valign="top">Result</td>
                        <td style="width: 220px" valign="top">
                            <asp:RadioButtonList ID="optIshihara" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="Red Green Deficiency" Value="RGD"></asp:ListItem>
                                <asp:ListItem Text="Total Color Blindness" Value="TCB"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>

    </table>
</fieldset>
