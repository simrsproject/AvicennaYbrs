<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NutrientPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.NutrientPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/MedHistCtl.ascx" TagPrefix="uc1" TagName="MedHistCtl" %>

<style>
    br
    {
        content: " ";
        display: block;
        margin: 2px;
    }
</style>
<fieldset style="width: 49%;">
    <legend>Changes in Body Weight</legend>
    BW usually:<telerik:RadTextBox ID="txtWeightUsually" runat="server" Width="50px" />&nbsp;kg. BW current:&nbsp;<telerik:RadTextBox ID="txtWeightCurrent" runat="server" Width="50px" />&nbsp;kg. BL:&nbsp;<telerik:RadTextBox ID="txtBodyLegth" runat="server" Width="50px" />&nbsp;cm. BMI:&nbsp;<telerik:RadTextBox ID="txtBMI" runat="server" Width="50px" />&nbsp;kg/m2.
    <br />
    (Time:&nbsp;
    <telerik:RadTextBox ID="txtTime" runat="server" Width="50px" />&nbsp;
    <telerik:RadDropDownList runat="server" ID="cboTimeType">
        <Items>
            <telerik:DropDownListItem Text="Week" Value="W" />
            <telerik:DropDownListItem Text="Month" Value="M" />
            <telerik:DropDownListItem Text="Year" Value="Y" />
        </Items>
    </telerik:RadDropDownList>
    <br />
    <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="optVisitType" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="No changes" Value="01" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Changes since 6 months ago" Value="02"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td style="vertical-align: bottom;">
                <telerik:RadTextBox ID="txtWeightChangeInSixMonth" runat="server" Width="50px" />&nbsp;kg =&nbsp;<telerik:RadTextBox ID="txtPercentChangeInSixMonth" runat="server" Width="50px" />&nbsp;%
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 20px;">
                <asp:RadioButtonList ID="optChangeInSixMonth" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="0- <5% (no change in the size of the shirt / pants)" Value="01" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="5- 10% (changes in the size of the shirt / pants)" Value="02"></asp:ListItem>
                    <asp:ListItem Text="> 10% (shirt / pants very loose)" Value="03"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset style="width: 49%;">
    <legend>Changes in food intake</legend>
    <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="optFoodIntake" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="No changes" Value="01" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Changes in the type of diet / food form" Value="02"></asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:RadioButtonList ID="optChangeFoodIntake" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Solid food suboptimal" Value="01" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Full liquid diet" Value="02"></asp:ListItem>
                    <asp:ListItem Text="Liquid food hipokalori" Value="03"></asp:ListItem>
                    <asp:ListItem Text="Starvation, unable to eat" Value="04"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>

</fieldset>

<fieldset style="width: 49%;">
    <legend>Gastrointestinal Changes</legend>
    <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="optGastrointestinal" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="No changes" Value="01" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Changes since 6 months ago" Value="02"></asp:ListItem>
                </asp:RadioButtonList></td>
            <td rowspan="2" style="vertical-align: bottom;">
                <table>
                    <tr>
                        <td>Frequency</td>
                        <td style="width: 20px"></td>
                        <td>Duration (< 2mg /> 2mg)</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtFreqNausea" runat="server" Width="50px" /></td>
                        <td></td>
                        <td>
                            <telerik:RadTextBox ID="txtDurationNausea" runat="server" Width="50px" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtFreqGag" runat="server" Width="50px" /></td>
                        <td></td>
                        <td>
                            <telerik:RadTextBox ID="txtDurationGag" runat="server" Width="50px" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtFreqDiarrhea" runat="server" Width="50px" /></td>
                        <td></td>
                        <td>
                            <telerik:RadTextBox ID="txtDurationDiarrhea" runat="server" Width="50px" /></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadTextBox ID="txtFreqAnorexia" runat="server" Width="50px" /></td>
                        <td></td>
                        <td>
                            <telerik:RadTextBox ID="txtDurationAnorexia" runat="server" Width="50px" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 20px;">
                <asp:RadioButtonList ID="optGastrointestinalChange" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Nausea" Value="01" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Gag" Value="02"></asp:ListItem>
                    <asp:ListItem Text="Diarrhea" Value="03"></asp:ListItem>
                    <asp:ListItem Text="Anorexia" Value="03"></asp:ListItem>
                </asp:RadioButtonList>
            </td>

        </tr>
    </table>
</fieldset>

<fieldset style="width: 49%;">
    <legend>Changes in functional capacity</legend>
    <asp:RadioButtonList ID="optCapacityFuncChange" runat="server" RepeatDirection="Vertical">
        <asp:ListItem Text="No changes" Value="01" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Normal activity / ambulatory but with difficulty" Value="02"></asp:ListItem>
        <asp:ListItem Text="Bed rest / wheelchair" Value="03"></asp:ListItem>
    </asp:RadioButtonList>
</fieldset>

<fieldset style="width: 49%;">
    <legend>Illness and relationship with nutritional needs</legend>
    <table>
        <tr>
            <td>Medical diagnostics.</td>
            <td>
                <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2">Relations with metabolic needs.</td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 20px;">
                <asp:RadioButtonList ID="optMetabolic" runat="server" RepeatDirection="Vertical">
                    <asp:ListItem Text="Nothing" Value="01" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Low to moderate" Value="02"></asp:ListItem>
                    <asp:ListItem Text="High" Value="03"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>

</fieldset>

<fieldset style="width: 49%;">
    <legend>PHYSICAL EXAMINATION</legend>
    <table width="100%">
        <tr>
            <td class="label">Lost subcutaneous fat (triceps, chest)</td>
            <td>
                <telerik:RadTextBox ID="txtLostFat" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Lost muscle mass (quadriceps, deltoids)</td>
            <td>
                <telerik:RadTextBox ID="txtLostMuscle" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Ankle oedema</td>
            <td>
                <telerik:RadTextBox ID="txtAnkle" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Anasarca oedema</td>
            <td>
                <telerik:RadTextBox ID="txtAnasarca" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Ascites</td>
            <td>
                <telerik:RadTextBox ID="txtAscites" runat="server" Width="100%" />
            </td>
        </tr>
    </table>
    <uc1:GcsCtl runat="server" ID="gcsCtl" />
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
    </table>
</fieldset>
<fieldset style="width: 49%;">
    <legend>Ancillary Examination</legend>
    <telerik:RadTextBox ID="txtAncillaryExam" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" />
</fieldset>

<fieldset style="width: 49%;">
    <legend>ASSESSMENT SGA</legend>
    Category:
                <asp:RadioButtonList ID="optSga" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="A: Nutrition Good / Normal (Risk Malnutrition)" Value="A" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="B: Malnutrition moderate / mild" Value="B"></asp:ListItem>
                    <asp:ListItem Text="C: Severe malnutrition" Value="C"></asp:ListItem>
                </asp:RadioButtonList>
</fieldset>

<fieldset style="width: 49%;">
    <legend>PLANNING</legend>
    <telerik:RadTextBox ID="txtPlanning" runat="server" Width="100%" Height="80px"
        TextMode="MultiLine" />

</fieldset>


