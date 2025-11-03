<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RehabilitationPe2Ctl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.RehabilitationPe2Ctl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>

<fieldset style="width: 49%;">
    <legend>PHYSICAL EXAMINATION</legend>
    <table width="100%">
        <tr>
            <td class="label">General Condition</td>
            <td>
                <telerik:RadComboBox runat="server" ID="cboGeneralCondition"></telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="label">Neuromuskuloskeletal</td>
            <td>
                <telerik:RadTextBox ID="txtNeuromuskuloskeletal" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="label">Respiratory</td>
            <td>
                <telerik:RadTextBox ID="txtrespiratory" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="label">Functional Test</td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkPhysicalActivity" runat="server" Text="Physical activity" /><br />
                            <asp:CheckBox ID="chkSwallowing" runat="server" Text="Swallowing function" /><br />
                            <asp:CheckBox ID="chkGait" runat="server" Text="Gait" /><br />
                            <asp:CheckBox ID="chkCardiorespiratory" runat="server" Text="Cardiorespiratory" /><br />
                            <asp:CheckBox ID="chkDefecation" runat="server" Text="Defecation" /><br />
                            <asp:CheckBox ID="chkMicturition" runat="server" Text="Micturition" /><br />
                            <asp:CheckBox ID="chkNoble" runat="server" Text="Noble function" /><br />
                            <asp:CheckBox ID="chkExecution" runat="server" Text="Execution function" /><br />
                        </td>
                        <td style="vertical-align: top">
                            <asp:CheckBox ID="chkSensory" runat="server" Text="Sensory function" /><br />
                            <asp:CheckBox ID="chkCommunication" runat="server" Text="Communication function" /><br />
                            <asp:CheckBox ID="chkBalance" runat="server" Text="Balance" /><br />
                            <asp:CheckBox ID="chkPosture" runat="server" Text="Posture control" /><br />
                            <asp:CheckBox ID="chkMuscle" runat="server" Text="Muscle strength" /><br />
                            <asp:CheckBox ID="chkJoint" runat="server" Text="Joint flexibility" /><br />
                            <asp:CheckBox ID="chkLocomotor" runat="server" Text="Locomotor" /><br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</fieldset>

<fieldset style="width: 49%;">
    <legend>SPECIAL EXAMINATION</legend>
    <table>
        <tr>
            <td class="labelcaption">No</td>
            <td class="labelcaption">Special Assessment</td>
            <td class="labelcaption">Tools</td>
            <td colspan="2" class="labelcaption">Result</td>
        </tr>
        <tr>
            <td>1</td>
            <td>Activities of daily life</td>
            <td>FIM</td>
            <td>
                <asp:RadioButtonList ID="optFim" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtFimDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Barthel Index</td>
            <td>
                <asp:RadioButtonList ID="optBarthelIndex" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtBarthelIndexDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>2</td>
            <td>Swallowing function</td>
            <td>Disphagya screening tools</td>
            <td>
                <asp:RadioButtonList ID="optDisphagya" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtDisphagyaDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>3</td>
            <td>Cognitive function</td>
            <td>MMSE</td>
            <td>
                <asp:RadioButtonList ID="optMmse" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtMmseDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>4</td>
            <td>Mocalna</td>
            <td></td>
            <td>
                <asp:TextBox runat="server" ID="txtMocalnaDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>5</td>
            <td>Communication function</td>
            <td>Receptive Language</td>
            <td>
                <asp:RadioButtonList ID="optReceptiveLanguage" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtReceptiveLanguageDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Expressive Language</td>
            <td>
                <asp:RadioButtonList ID="optExpressiveLanguage" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtExpressiveLanguageDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Speak Word and Sentence</td>
            <td>
                <asp:RadioButtonList ID="optSpeakWordandSentence" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtSpeakWordandSentenceDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Articulation</td>
            <td>
                <asp:RadioButtonList ID="optArticulation" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtArticulationDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Orientation</td>
            <td>
                <asp:RadioButtonList ID="optOrientation" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtOrientationDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Recall</td>
            <td>
                <asp:RadioButtonList ID="optRecall" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtRecallDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>6</td>
            <td>Balance and control functions</td>
            <td>Berg Balance Scale</td>
            <td>
                <asp:RadioButtonList ID="optBergBalance" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtBergBalanceDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td>7</td>
            <td>Flexibility function</td>
            <td>Schober Test</td>
            <td>
                <asp:RadioButtonList ID="optSchober" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtSchoberDesc"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>Goniometer</td>
            <td>
                <asp:RadioButtonList ID="optGoniometer" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtGoniometerDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>8</td>
            <td>Locomotor function</td>
            <td>Time up and go test</td>
            <td colspan="2">
                <asp:RadioButtonList ID="optTimeUpGoTest" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="No risk" Value="NR" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Low risk" Value="LR"></asp:ListItem>
                    <asp:ListItem Text="High risk" Value="HR"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
        </tr>

        <tr>
            <td>9</td>
            <td>Painful</td>
            <td>Numeric Rating Scale</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtNrs"></asp:TextBox></td>
        </tr>

        <tr>
            <td>10</td>
            <td>Gait</td>
            <td></td>
            <td>
                <asp:RadioButtonList ID="optGait" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Adequate" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Inadequate" Value="I"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td>
                <asp:TextBox runat="server" ID="txtGaitDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>11</td>
            <td>Muscle Strength</td>
            <td></td>
            <td>
                <asp:TextBox runat="server" ID="txtMuscleStrengthDesc"></asp:TextBox></td>
        </tr>

        <tr>
            <td>12</td>
            <td>Others</td>
            <td></td>
            <td>
                <asp:TextBox runat="server" ID="txtOthersDesc"></asp:TextBox></td>
        </tr>
    </table>

</fieldset>

<fieldset style="width: 49%;">
    <legend>Summary</legend>
    <telerik:RadTextBox ID="txtSummary" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" />

</fieldset>

<fieldset style="width: 49%;">
    <legend>Evaluation</legend>
    <asp:RadioButtonList ID="optEvaluation" runat="server" RepeatDirection="Vertical">
        <asp:ListItem Text="Back to the sender doctor (rehabilitation is completed)" Value="Back" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Continued rehabilitation (treatment protocol as recommended)" Value="Continued"></asp:ListItem>
    </asp:RadioButtonList>
    <table width="100%">
        <tr>
            <td class="label">Reason</td>
            <td>
                <telerik:RadTextBox ID="txtEvaluationReason" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" />
            </td>
        </tr>
    </table>
</fieldset>


