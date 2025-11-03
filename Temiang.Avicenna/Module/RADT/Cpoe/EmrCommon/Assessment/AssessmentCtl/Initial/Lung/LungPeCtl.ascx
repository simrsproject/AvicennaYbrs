<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LungPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.LungPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>

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
            <td class="label">THT</td>
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
            <td class="labelcaption" colspan="3">Lungs</td>

        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Inspection</td>
            <td colspan="2"></td>

        </tr>
        <tr>
            <td class="label">Movement On Breathing</td>
            <td>
                <asp:RadioButtonList ID="optInspeksiGerakan" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInspeksiGerakan" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Between Ribs</td>
            <td>
                <asp:RadioButtonList ID="optInspeksiSelaIga" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtInspeksiSelaIga" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Palpasi</td>


        </tr>
        <tr>
            <td class="label">Fremitus Vote</td>
            <td>
                <asp:RadioButtonList ID="optPalpasiFremitus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPalpasiFremitus" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Tenderness</td>
            <td>
                <asp:RadioButtonList ID="optPalpasiNyeri" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Nothing" Value="T" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Exists" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPalpasiNyeri" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Crepitations</td>
            <td>
                <asp:RadioButtonList ID="optPalpasiKrepitasi" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Nothing" Value="T" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Exists" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtPalpasiKrepitasi" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Percussion</td>


        </tr>
        <tr>
            <td class="label"></td>
            <td colspan="2">
                <asp:RadioButtonList ID="optPerkusi" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Sonor" Value="Sonor"></asp:ListItem>
                    <asp:ListItem Text="Hypersonor" Value="Hypersonor"></asp:ListItem>
                    <asp:ListItem Text="Dim" Value="Dim"></asp:ListItem>
                    <asp:ListItem Text="Deaf" Value="Deaf"></asp:ListItem>
                </asp:RadioButtonList>
            </td>


        </tr>
        <tr>
            <td class="label"></td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtPerkusiLocation" runat="server" Width="100%" LabelWidth="10%" Label="Lokasi" />
            </td>


        </tr>
        <tr>
            <td class="labelcaption" colspan="3">Auscultation</td>

        </tr>
        <tr>
            <td class="label">Vesicular</td>
            <td colspan="2">
                <asp:RadioButtonList ID="optVesikular" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Sub Bronkial" Value="Sub Bronkial"></asp:ListItem>
                    <asp:ListItem Text="Bronkial" Value="Bronkial"></asp:ListItem>
                </asp:RadioButtonList>
            </td>


        </tr>
        <tr>
            <td class="label"></td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtVesikularLocation" runat="server" Width="100%" LabelWidth="10%" Label="Lokasi" />
            </td>


        </tr>

        <tr>
            <td class="label">Ronchi</td>
            <td colspan="2">
                <div class="l">
                    <asp:RadioButtonList ID="optRonchi" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Dry" Value="Dry"></asp:ListItem>
                        <asp:ListItem Text="Wet" Value="Wet"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="r">
                    <asp:RadioButtonList ID="optSedangKasar" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Medium" Value="Middle"></asp:ListItem>
                        <asp:ListItem Text="Rude" Value="Rude"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </td>


        </tr>

        <tr>
            <td class="label"></td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtRonchiLocation" runat="server" Width="100%" LabelWidth="10%" Label="Lokasi" />
            </td>


        </tr>

        <tr>
            <td class="label">Auscultation Wheezing</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtAuscultationWheezing" runat="server" Width="100%" />
            </td>


        </tr>
        


        <tr>
            <td class="label">Swipe noisy pleura</td>
            <td>
                <asp:RadioButtonList ID="optBising" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Nothing" Value="T" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Exists" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <telerik:RadTextBox ID="txtBising" runat="server" Width="100%" />
            </td>


        </tr>

        <tr>
            <td class="label">Heart</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtJantung" runat="server" Width="100%" />
            </td>

        </tr>

        <tr>
            <td class="label">Abdomen</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtAbdomen" runat="server" Width="100%" />
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
            <td class="label">Notes
            </td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>
        </tr>
    </table>
