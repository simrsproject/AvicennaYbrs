<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KidPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.KidPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/PhysicalExamMethodCtl.ascx" TagPrefix="uc1" TagName="PhysicalExamMethodCtl" %>

    <uc1:GcsCtl runat="server" ID="gcsCtl" />
    <table style="width: 100%">
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
            <td></td>
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

            <td></td>

        </tr>
        <tr>
            <td class="label">ENT (THT)</td>
            <td>
                <asp:RadioButtonList ID="optTht" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtTht" runat="server" Width="100%" />
            </td>

            <td></td>

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
            <td></td>


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
            <td></td>


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
            <td></td>

        </tr>
        <tr>
            <td class="label">Heart</td>
            <td>
                <asp:RadioButtonList ID="optJantung" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry"></td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Rhythm</td>
            <td>
                <asp:RadioButtonList ID="optJantungIrama" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Regular" Value="R" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Irregular" Value="I"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry"><telerik:RadTextBox ID="txtJantungIrama" runat="server" Width="100%" /></td>
            <td></td>
        </tr>
         <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Heart Sounds</td>
             <td>
                <asp:RadioButtonList ID="optJantungBunyi" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
               <telerik:RadTextBox ID="txtJantungBunyi" runat="server" Width="100%" />
            </td>
             <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Inspection</td>
            <td  colspan="2">
                <telerik:RadTextBox ID="txtJantungInspeksi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Palpation</td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtJantungPalpasi" runat="server" Width="100%" />
            </td>
            <td></td>

        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Percussion</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtJantungPerkusi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Auscultation</td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtJantungAuskulatasi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Other</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtJantung" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Lungs</td>
            <td>
                <asp:RadioButtonList ID="optParu" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry"></td>
            <td></td>

        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Movement</td>
            <td>
                <asp:RadioButtonList ID="optParuPergerakan" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Asymmetry" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtParuPergerakan" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Percussion</td>
            <td>
                <asp:RadioButtonList ID="optParuPerkusi" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtParuPerkusi" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Respiratory</td>
            <td>
                <asp:RadioButtonList ID="optParuPernapasan" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtParuPernapasan" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Ronchi</td>
            <td>
                <asp:RadioButtonList ID="optParuRonchi" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Wheezing</td>
            <td>
                <asp:RadioButtonList ID="optParuWheezing" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Inspection</td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtParuInspeksi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Palpation</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtParuPalpasi" runat="server" Width="100%" />
            </td>
            <td></td>

        </tr>
        
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Auscultation</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtParuAuskulatasi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        

        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Other</td>
            <td  colspan="2">
                <telerik:RadTextBox ID="txtParu" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Abdomen</td>
            <td>
                <asp:RadioButtonList ID="optAbdomen" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry"></td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Abnormalities</td>
            <td>
                <asp:RadioButtonList ID="optAbdomenKelainan" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtAbdomenKelainan" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Lump</td>
            <td>
                <asp:RadioButtonList ID="optAbdomenBenjolan" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtAbdomenBenjolan" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Tenderness</td>
            <td>
                <asp:RadioButtonList ID="optAbdomenNyeriTekan" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtAbdomenNyeriTekan" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Hernia</td>
            <td>
                <asp:RadioButtonList ID="optAbdomenHernia" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtAbdomenHernia" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Bowel Sound</td>
            <td>
                <asp:RadioButtonList ID="optAbdomenBisingUsus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtAbdomenBisingUsus" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Distention</td>
            <td>
                <asp:RadioButtonList ID="optAbdomenDistensi" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="AY"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtAbdomenDistensi" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Inspection</td>
            <td  colspan="2">
                <telerik:RadTextBox ID="txtAbdomenInspeksi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Palpation</td>
            <td  colspan="2">
                <telerik:RadTextBox ID="txtAbdomenPalpasi" runat="server" Width="100%" />
            </td>
            <td></td>

        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Percussion</td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtAbdomenPerkusi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Auscultation</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtAbdomenAuskulatasi" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Other</td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtAbdomen" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Spine & Limb</td>
            <td>
                <asp:RadioButtonList ID="optSpineLimb" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtSpineLimb" runat="server" Width="100%" />
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
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Penis</td>
            <td>
                <asp:RadioButtonList ID="optGenitaliaPenis" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtGenitaliaPenis" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Testis</td>
            <td class="entry">
                <asp:RadioButtonList ID="optGenitaliaTestis" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtGenitaliaTestis" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Labia Minor</td>
            <td>
                <asp:RadioButtonList ID="optGenitaliaLabiaMinor" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtGenitaliaLabiaMinor" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Anus</td>
            <td>
                <asp:RadioButtonList ID="optGenitaliaAnus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtGenitaliaAnus" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Ekstremitas</td>
            <td>
                <asp:RadioButtonList ID="optEkstremitas" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtEkstremitas" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Edema</td>
            <td>
                <asp:RadioButtonList ID="optEkstremitasEdema" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEkstremitasEdema" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- CRT</td>
            <td>
                <asp:RadioButtonList ID="optEkstremitasCrt" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtEkstremitasCrt" runat="server" Width="100%" />
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
            <td></td>
        </tr>
        <tr>
            <td class="label">Neurology Status :</td>
            <td></td>
            <td class="entry"></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- GRM exist</td>
            <td>
                <asp:RadioButtonList ID="optGrm" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="Y" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry"></td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Reflex Fisiologis</td>
            <td>
                <asp:RadioButtonList ID="optReflexFisiologis" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtReflexFisiologis" runat="server" Width="100%" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Reflex Patologis exist</td>
            <td>
                <asp:RadioButtonList ID="optReflexPatologis" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="No" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry"></td>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;- Motoric</td>
            <td>
                <asp:RadioButtonList ID="optMotoric" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtMotoric" runat="server" Width="100%" />
            </td>
            <td></td>
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
        <tr>
            <td class="label">Other
            </td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtOther" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="label">Notes
            </td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>
            <td></td>
        </tr>
    </table>
