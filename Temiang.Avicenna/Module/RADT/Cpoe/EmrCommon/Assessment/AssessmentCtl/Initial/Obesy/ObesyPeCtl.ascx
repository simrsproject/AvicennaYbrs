<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObesyPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.ObesyPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/QuestionCtl.ascx" TagPrefix="uc1" TagName="QuestionCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/PhysicalExamMethodCtl.ascx" TagPrefix="uc1" TagName="PhysicalExamMethodCtl" %>

<fieldset style="width: 49%;">
    <legend>Obesytas History</legend>
    <uc1:QuestionCtl runat="server" ID="qstObesytas" IsUseQuestionGroupCaption="False" />
</fieldset>
<fieldset style="width: 49%;">
    <legend>Habit</legend>
    <uc1:QuestionCtl runat="server" ID="qstHabit" IsUseQuestionGroupCaption="False" />
</fieldset>
<fieldset style="width: 49%;">
    <legend>PAR-Q Test</legend>
    <uc1:QuestionCtl runat="server" ID="qstParqTest"  IsUseQuestionGroupCaption="False"/>
</fieldset>
<fieldset style="width: 49%;">
    <legend>Nutrition Analisys</legend>
    <uc1:QuestionCtl runat="server" ID="qstNutrition" IsUseQuestionGroupCaption="False"/>
</fieldset>
<fieldset style="width: 49%;">
    <legend>PHYSICAL EXAMINATION</legend>
    <uc1:GcsCtl runat="server" ID="gcsCtl" />

    <table width="100%">
        <tr>
            <td class="label">Head</td>
            <td style="width: 150px">
                <asp:RadioButtonList ID="optKepala" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtKepala" runat="server" Width="100%" />
            </td>

        </tr>
        <tr>
            <td class="label">Eyes</td>
            <td>
                <asp:RadioButtonList ID="optMata" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMata" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">ENT</td>
            <td>
                <asp:RadioButtonList ID="optTht" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTht" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Mouth</td>
            <td>
                <asp:RadioButtonList ID="optMulut" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMulut" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Neck</td>
            <td>
                <asp:RadioButtonList ID="optLeher" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLeher" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Thorax</td>
            <td>
                <asp:RadioButtonList ID="optThorax" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtThorax" runat="server" Width="100%" />
            </td>


        </tr>

        <tr>
            <td colspan="3">
                <uc1:PhysicalExamMethodCtl runat="server" ID="pemHeart" Caption="Heart" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <uc1:PhysicalExamMethodCtl runat="server" ID="pemLung" Caption="Lung" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <uc1:PhysicalExamMethodCtl runat="server" ID="pemAbdomen" Caption="Abdomen" />
            </td>
        </tr>
        <tr>
            <td class="label">Genitalia & Anus</td>
            <td>
                <asp:RadioButtonList ID="optGenitaliaAndAnus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtGenitaliaAndAnus" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Extremity</td>
            <td>
                <asp:RadioButtonList ID="optEkstremitas" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEkstremitas" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Skin</td>
            <td>
                <asp:RadioButtonList ID="optKulit" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtKulit" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Notes
            </td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtNotes" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>

        </tr>
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
    <legend>Fitness Status</legend>
    <uc1:QuestionCtl runat="server" ID="qstFitness" IsUseQuestionGroupCaption="False"/>
</fieldset>

<fieldset style="width: 49%;">
    <legend>Mentalist / Psychiatric Status</legend>
    <uc1:QuestionCtl runat="server" ID="qstMentalist" IsUseQuestionGroupCaption="False"/>
</fieldset>
<fieldset style="width: 49%;">
    <legend>Assessment Psychology</legend>
    <telerik:RadTextBox ID="txtAssessPsychology" runat="server" Width="100%" Height="40px"
        TextMode="MultiLine" Resize="Vertical" />
</fieldset>