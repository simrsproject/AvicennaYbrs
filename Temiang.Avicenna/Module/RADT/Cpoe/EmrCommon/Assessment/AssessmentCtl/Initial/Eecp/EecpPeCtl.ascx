<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EecpPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.EecpPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/MedHistCtl.ascx" TagPrefix="uc1" TagName="MedHistCtl" %>

<style>
    .singleborder {
    border-collapse: collapse;
}
.singleborder th, .singleborder td {
    border: 1px solid gray;
}
</style>
<table width="100%">
    <tr>
        <td style="width: 50%">
            <fieldset>
                <legend>Checking type</legend>
                 <table width="100%" class="singleborder" >
                    <tr>
                        <td class="labelcaption" style="width: 200px">Indication</td>
                        <td class="labelcaption" style="width: 100px">Checklist</td>
                        
                    </tr>
                    <tr>
                        <td>EKG </td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkEkg" /></td>
                    </tr>
                    <tr>
                        <td>Echocardiography (EF = <telerik:RadNumericTextBox ID="txtEchocardiographyEF" Type="Percent" runat="server" Width="30px%"></telerik:RadNumericTextBox>%)</td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkEchocardiography" /></td>
                    </tr>
                    <tr>
                        <td>Lower extremity duplex</td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkDuplex" /></td>
                    </tr>
                    <tr>
                        <td>USG Abdomen</td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkUsgAbdomen" /></td>
                    </tr>
                    <tr>
                        <td>Treadmill Test*</td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkTreadmill" /></td>
                    </tr>
                    <tr>
                        <td>Angiografi*</td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkAngiografi" /></td>
                    </tr>
                    <tr>
                        <td>CT-Scan*</td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkCTScan" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">Information : *: Optional</td>
                        </tr>
                </table>
            </fieldset>
        </td>
        <td style="width: 50%; vertical-align: top;">
            <fieldset>
                <legend>EECP Indication</legend>
                <table width="100%" class="singleborder">
                    <tr>
                        <td class="labelcaption" style="width: 200px">Indication</td>
                        <td class="labelcaption" style="width: 100px">Checklist</td>
                    </tr>
                    <tr>
                        <td>Refractory angina </td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkRefractory" /></td>
                    </tr>
                    <tr>
                        <td>Chronic heart failure</td>
                        <td align="center">
                            <asp:CheckBox runat="server" ID="chkHeartFailure" /></td>
                    </tr>

                    <tr>
                        <td colspan="2">Etc&nbsp;
                            <telerik:RadTextBox ID="txtEtcIndication" runat="server" Width="100%"></telerik:RadTextBox></td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>

</table>



<uc1:MedHistCtl runat="server" ID="medHistCtl" />


<fieldset style="width: 49%;">
    <legend>EECP Contraindications</legend>
    <table class="singleborder">
        <tr>
            <td class="labelcaption" style="width: 250px">Condition</td>
            <td class="labelcaption" style="width: 50px">Exist</td>
            <td class="labelcaption" style="width: 50px">Nothing</td>
        </tr>
        <tr>
            <td>Severe aortic regurgitation</td>
            <td>
                <asp:RadioButton runat="server" ID="chkRegurgitation" GroupName="Regurgitation" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoRegurgitation" GroupName="Regurgitation" />
            </td>
        </tr>
        <tr>
            <td>Aortic aneurysms (> 5 cm)</td>
            <td>
                <asp:RadioButton runat="server" ID="chkAortic" GroupName="Aortic" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoAortic" GroupName="Aortic" />
            </td>
        </tr>
        <tr>
            <td>Severe hypertension (≥180 / 110 mmHg)</td>
            <td>
                <asp:RadioButton runat="server" ID="chkHypertension" GroupName="Hypertension" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoHypertension" GroupName="Hypertension" />
            </td>
        </tr>
        <tr>
            <td>Thromboflebitis / DVT ext down</td>
            <td>
                <asp:RadioButton runat="server" ID="chkThromboflebitis" GroupName="Thromboflebitis" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoThromboflebitis" GroupName="Thromboflebitis" />
            </td>
        </tr>
        <tr>
            <td>Peripheral artery disease (PAD)</td>
            <td>
                <asp:RadioButton runat="server" ID="chkPeripheral" GroupName="Peripheral" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoPeripheral" GroupName="Peripheral" />
            </td>
        </tr>
        <tr>
            <td>Severe arrhythmia (AF, extrasistole)</td>
            <td>
                <asp:RadioButton runat="server" ID="chkArrhythmia" GroupName="Arrhythmia" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoArrhythmia" GroupName="Arrhythmia" />
            </td>
        </tr>
        <tr>
            <td>Hemorrhagic diathesis, hemophilia</td>
            <td>
                <asp:RadioButton runat="server" ID="chkHemorrhagic" GroupName="Hemorrhagic" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoHemorrhagic" GroupName="Hemorrhagic" />
            </td>
        </tr>
        <tr>
            <td>Pregnancy</td>
            <td>
                <asp:RadioButton runat="server" ID="chkPregnancy" GroupName="Pregnancy" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoPregnancy" GroupName="Pregnancy" />
            </td>
        </tr>
        <tr>
            <td>Abdominal tumors</td>
            <td>
                <asp:RadioButton runat="server" ID="chkAbdominal" GroupName="Abdominal" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="chkNoAbdominal" GroupName="Abdominal" />
            </td>
        </tr>
    </table>
</fieldset>

<fieldset style="width: 49%;">
    <legend>The drugs that are currently consumed</legend>
    <telerik:RadTextBox ID="txtDrugCurrentConsumed" runat="server" Width="100%" TextMode="MultiLine" Resize="Vertical" />
</fieldset>
