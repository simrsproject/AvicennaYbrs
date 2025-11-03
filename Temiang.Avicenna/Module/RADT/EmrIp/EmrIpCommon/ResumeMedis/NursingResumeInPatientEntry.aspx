<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="NursingResumeInPatientEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.NursingResumeInPatientEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Module/RADT/EmrIp/EmrIpCommon/ResumeMedis/ControlPlanCtl.ascx" TagPrefix="uc1" TagName="ControlPlanCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>Entry by Nurse</legend>
        <table width="100%">
            <tr>
                <td class="label">Discharge Condition</td>
                <td class="entry">
                    <table width="100%">
                        <tr>
                            <td>Temp</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtTemp" Width="60px"></telerik:RadTextBox></td>
                            <td>C&nbsp;&nbsp;&nbsp;HeartRate</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtPulse" Width="60px"></telerik:RadTextBox></td>
                            <td>X/mnt&nbsp;&nbsp;&nbsp;Respiratory</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtRespiratory" Width="60px"></telerik:RadTextBox></td>
                            <td>X/mnt&nbsp;&nbsp;&nbsp;Blood Pressure</td>
                            <td>
                                <telerik:RadTextBox runat="server" ID="txtBloodPress" Width="60px"></telerik:RadTextBox></td>
                            <td>mm/Hg</td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label" rowspan="4">Diet / Nutrition</td>
                <td class="entry">
                    <telerik:RadListBox ID="lboxDiet" runat="server" CheckBoxes="True" BorderColor="white">
                        <Items>
                            <telerik:RadListBoxItem Text="Oral" Value="ORL" />
                            <telerik:RadListBoxItem Text="NGT" Value="NGT" />
                            <telerik:RadListBoxItem Text="Special Diet" Value="SDT" />
                        </Items>
                    </telerik:RadListBox>
                </td>
                <td></td>

            </tr>
            <tr>
                <td class="entry" style="padding-left: 30px;">
                    <telerik:RadTextBox ID="txtSpecialDietNote" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="entry">
                    <telerik:RadListBox ID="lboxDiet2" runat="server" CheckBoxes="true" BorderColor="white">
                        <Items>
                            <telerik:RadListBoxItem Text="Liquid Limit" Value="LQL" />
                        </Items>
                    </telerik:RadListBox>
                </td>
            </tr>
            <tr>
                <td class="entry" style="padding-left: 30px;">
                    <telerik:RadTextBox ID="txtDietLiquidLimitNote" runat="server" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="label">Defecate</td>
                <td class="entry">
                    <asp:RadioButtonList ID="optDefecateType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Normal" Value="NML" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Ileostomy / colostomy" Value="ILT"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="label">Urinate</td>
                <td class="entry">
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="optUrinateType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Normal" Value="NML" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Catheter, last installation date" Value="CAT"></asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td>
                                <telerik:RadDatePicker ID="txtCatheterLastDate" runat="server" Width="120px" />
                            </td>
                            <td></td>
                        </tr>
                    </table>

                </td>
            </tr>

        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend>Special for Obstetric Patient</legend>
        <table width="100%">
            <tr>
                <td class="label">Uterine Contractions</td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="optUterineType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="No" Value="NO" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Good" Value="GUD"></asp:ListItem>
                                    <asp:ListItem Text="Uterine Fundal, Height:" Value="GUD"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtUterineHeight" runat="server" Width="100px" NumberFormat-DecimalDigits="0" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Vulva</td>
                <td>
                    <asp:RadioButtonList ID="optVulvaType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Clean" Value="CLN" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Dirty" Value="DIRT"></asp:ListItem>
                        <asp:ListItem Text="Swollen" Value="SWO"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Perineal Wound</td>
                <td>
                    <asp:RadioButtonList ID="optPerinealWoundType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Dry" Value="DRY" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Wet" Value="WET"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Lochea</td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="optLocheaType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Many" Value="MNY" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Little" Value="LIT"></asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td>&nbsp;&nbsp;&nbsp;Color</td>
                            <td>
                                <telerik:RadTextBox ID="txtLocheaColor" runat="server" Width="150px" /></td>
                            <td>&nbsp;Smell</td>
                            <td>
                                <telerik:RadTextBox ID="txtLocheaSmell" runat="server" Width="200px" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>

            <tr>
                <td class="label">Operation Wound</td>
                <td>
                    <asp:RadioButtonList ID="optOperationWoundType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Clean" Value="CLN" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Dry" Value="DRY"></asp:ListItem>
                        <asp:ListItem Text="There is fluid from wound" Value="FLD"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Transfer & Mobilization</td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="optTransferMobilizationType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Independent" Value="IND" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Partially Assisted, Explain:" Value="PAS"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtPartiallyAssisted" runat="server" Width="400px" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Assist Tools</td>
                <td>
                    <asp:RadioButtonList ID="optAssistToolType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Stick" Value="STK" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Wheelchair" Value="WCR"></asp:ListItem>
                        <asp:ListItem Text="Full Assisted" Value="FAS"></asp:ListItem>
                        <asp:ListItem Text="Trolley" Value="TRO"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="OTH"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td></td>

            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend>Education that has been given</legend>
        <telerik:RadListBox ID="lboxEducation" runat="server" CheckBoxes="true" BorderColor="white">
            <Items>
                <telerik:RadListBoxItem Text="Disease and treatment" Value="001" />
                <telerik:RadListBoxItem Text="Care at home" Value="002" />
                <telerik:RadListBoxItem Text="Overcoming pain" Value="003" />
                <telerik:RadListBoxItem Text="Mother and baby care" Value="004" />
                <telerik:RadListBoxItem Text="Environmental preparation and facilities for home care" Value="005" />
                <telerik:RadListBoxItem Text="Injury cure " Value="006" />
                <telerik:RadListBoxItem Text="Family Planning Advice" Value="007" />
                <telerik:RadListBoxItem Text="Other:" Value="999" />
            </Items>
        </telerik:RadListBox>
        <br />
        <telerik:RadTextBox ID="txtEducationOthers" runat="server" Width="300px" />
    </fieldset>
    <br />
    <fieldset>
        <legend>Nursing Diagnosis During Treatment</legend>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 400px;">
                    <table>
                        <tr>
                            <td class="label" style="width: 20px;">1</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTreatmentDiag01" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label" style="width: 20px;">2</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTreatmentDiag02" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 400px;">
                    <table>
                        <tr>
                            <td class="label" style="width: 20px;">3</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTreatmentDiag03" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label" style="width: 20px;">4</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtTreatmentDiag04" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
        </table>

    </fieldset>
    <br />
    <fieldset>
        <legend>Nursing Diagnosis After Discharge</legend>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 400px;">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 20px;">1</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDischargeDiag01" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label" style="width: 20px;">2</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDischargeDiag02" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 400px;">
                    <table width="100%">
                        <tr>
                            <td class="label" style="width: 20px;">3</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDischargeDiag03" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label" style="width: 20px;">4</td>
                            <td class="entry">
                                <telerik:RadTextBox ID="txtDischargeDiag04" runat="server" Width="100%" /></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td></td>

            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend>Pain Management</legend>
        <table width="100%">
            <tr>
                <td class="label">Drugs taken / Anti pain</td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtDrugsTaken" runat="server" Width="100%" TextMode="MultiLine" /></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">Possible side effects</td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtPossibleEffect" runat="server" Width="100%" TextMode="MultiLine" /></td>
                <td></td>
            </tr>
            <tr>
                <td class="label">If the pain is getting heavy immediately go to the hospital</td>
                <td class="entry">
                    <telerik:RadTextBox ID="txtHospitalRefer" runat="server" Width="100%" TextMode="MultiLine" /></td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset>
        <legend>Goods and Inspection Result are Left to the Family</legend>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 400px" valign="top">
                    <table width="100%">
                        <tr>
                            <td class="label">1. Lab Results</td>
                            <td style="width: 50px;">
                                <telerik:RadNumericTextBox ID="txtLabResultSheet" runat="server" Width="50px" NumberFormat-DecimalDigits="0" /></td>
                            <td>Sheet</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">2. X-rays</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtXRaysSheet" runat="server" Width="50px" NumberFormat-DecimalDigits="0" /></td>
                            <td>Sheet</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">&nbsp;&nbsp;&nbsp;&nbsp;CT Scan</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtCTScanSheet" runat="server" Width="50px" NumberFormat-DecimalDigits="0" /></td>
                            <td>Sheet</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">&nbsp;&nbsp;&nbsp;&nbsp;MRI/MRA</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtMriMraSheet" runat="server" Width="50px" NumberFormat-DecimalDigits="0" /></td>
                            <td>Sheet</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">3. USG Results</td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtUsgResultSheet" runat="server" Width="50px" NumberFormat-DecimalDigits="0" /></td>
                            <td>Sheet</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">4. Certificate of illness</td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="optCertIllnes" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Exist" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 400px">
                    <table width="100%">
                        <tr>
                            <td class="label">5. Insurance Letter</td>
                            <td>
                                <asp:RadioButtonList ID="optInsLetter" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Exist" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">6. Patient Discharge Summary</td>
                            <td>
                                <asp:RadioButtonList ID="optDischSummaryLetter" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Exist" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">7. Baby Book</td>
                            <td>
                                <asp:RadioButtonList ID="optBabyBook" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Exist" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">8. Baby blood type card</td>
                            <td>
                                <asp:RadioButtonList ID="optBabyBloodType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Exist" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">9. Certificate of birth</td>
                            <td>
                                <asp:RadioButtonList ID="optCertBirth" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Exist" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">10. Babies are handed over by</td>
                            <td>
                                <telerik:RadTextBox ID="txtHandedBy" runat="server" Width="100%" />

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">11. Other</td>
                            <td>
                                <telerik:RadTextBox ID="txtOtherLetter" runat="server" Width="100%" />

                            </td>
                            <td></td>
                        </tr>

                    </table>
                </td>
                <td></td>
            </tr>
        </table>

    </fieldset>
    <br />
    <uc1:ControlPlanCtl runat="server" ID="controlPlanCtl" />
</asp:Content>
