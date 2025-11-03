<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NeurologyNonGcsPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.NeurologyNonGcsPeCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

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
            <td class="label">Color Blind</td>
            <td>
                <asp:RadioButtonList ID="optColorBlind" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtColorBlind" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Visus</td>
            <td colspan="2">
                <telerik:RadTextBox ID="txtVisus" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Ear Nose Throat</td>
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
            <td class="label">Heart</td>
            <td>
                <asp:RadioButtonList ID="optJantung" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtJantung" runat="server" Width="100%" />
            </td>


        </tr>
        <tr>
            <td class="label">Lungs</td>
            <td>
                <asp:RadioButtonList ID="optParu" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtParu" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Abdomen</td>
            <td>
                <asp:RadioButtonList ID="optAbdomen" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAbdomen" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Hepar</td>
            <td>
                <asp:RadioButtonList ID="optHepar" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtHepar" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Lien</td>
            <td>
                <asp:RadioButtonList ID="optLien" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtLien" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Reflex Fisiologis</td>
            <td>
                <asp:RadioButtonList ID="optReflexFis" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtReflexFis" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="label">Reflex Patologis</td>
            <td>
                <asp:RadioButtonList ID="optReflexPat" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtReflexPat" runat="server" Width="100%" />
            </td>
        </tr>
    <tr>
        <td class="label">Tumor</td>
        <td>
            <asp:RadioButtonList ID="optTumor" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Exist" Value="Y" ></asp:ListItem>
                <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
        </td>
    </tr>
    <tr>
        <td class="label">Hernia</td>
        <td>
            <asp:RadioButtonList ID="optHernia" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Exist" Value="Y" ></asp:ListItem>
                <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
        </td>
    </tr>
    <tr>
        <td class="label">Hemorrhoids</td>
        <td>
            <asp:RadioButtonList ID="optHemorrhoids" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Exist" Value="Y" ></asp:ListItem>
                <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
        </td>
    </tr>
        <tr>
            <td class="label">Auskulatasi</td>
            <td>
                <asp:RadioButtonList ID="optAuskulatasi" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtAuskulatasi" runat="server" Width="100%" />
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
            <td class="label">Other Notes
            </td>
            <td class="entry" colspan="2">
                <telerik:RadTextBox ID="txtOtherNotes" runat="server" Width="100%" Height="80px"
                    TextMode="MultiLine" Resize="Vertical" />
            </td>

        </tr>
    </table>

<fieldset>
    <legend>Funduscopy</legend>
    <table style="width: 100%;">
        <tr>
            <td class="label">Papiledema
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optPapiledema" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>Stimulus Sign Meningeal</legend>
    <table style="width: 100%;">
        <tr>
            <td class="label">Nuchal Rigidity
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optNuchalRigidity" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
        <tr>
            <td class="label">Kernig
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optKernig" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
        <tr>
            <td class="label">Lasgque
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="optLasgque" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            <td></td>
        </tr>
    </table>
</fieldset>

<table style="width: 100%;">
    <tr>
        <td class="label">Nervus Cranialis</td>
        <td>
            <telerik:RadDropDownList ID="ddlCranialis" runat="server" Width="250px" />
        </td>
    </tr>
</table>

<fieldset>
    <legend>Motoric Status</legend>
    <table style="width: 100%;">
        <tr>
            <td></td>
            <td>Left</td>
            <td></td>
            <td>Right</td>
        </tr>
        <tr>
            <td class="label">Extermintas Superior
            </td>
            <td>
                <asp:RadioButtonList ID="optExtermintasSuperior" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
            <td>
                <asp:RadioButtonList ID="optExtermintasSuperiorR" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="label">Extermintas Interior
            </td>
            <td>
                <asp:RadioButtonList ID="optExtermintasInterior" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td></td>
            <td>
                <asp:RadioButtonList ID="optExtermintasInteriorR" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
    </table>
</fieldset>

<table style="width: 100%;">
    <tr>
        <td></td>
        <td>Left</td>
        <td></td>
        <td>Right</td>
    </tr>
    <tr>
        <td class="label">Pupil
        </td>
        <td><telerik:RadTextBox ID="txtPupilLeft" runat="server" Width="100%" /></td>
        <td></td>
        <td><telerik:RadTextBox ID="txtPupilRight" runat="server" Width="100%" /></td>
    </tr>
    <tr>
        <td class="label">Pupil Reflex
        </td>
        <td><telerik:RadTextBox ID="txtPupilReflexLeft" runat="server" Width="100%" /></td>
        <td></td>
        <td><telerik:RadTextBox ID="txtPupilReflexRight" runat="server" Width="100%" /></td>
    </tr>
</table>

<table style="width: 100%;">
    <tr>
        <td class="label">Refleks Fisiologis</td>
        <td>
            <telerik:RadTextBox ID="txtRefleksFisiologis" runat="server" Width="100%" />
        </td>
    </tr>
    <tr>
        <td class="label">Refleks Patologis</td>
        <td>
            <telerik:RadTextBox ID="txtRefleksPatologis" runat="server" Width="100%" />
        </td>
    </tr>
    <tr>
        <td class="label">Status Otonom
        </td>
        <td>
            <telerik:RadTextBox ID="txtStatusOtonom" runat="server" Width="100%" />
        </td>
    </tr>

</table>
<table style="width: 100%;">
    <tr>
        <td class="label">Physical Exam
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>
    </tr>
    <tr>
        <td class="label">Neurologis
        </td>
        <td class="entry">
            <telerik:RadTextBox ID="txtNeurologis" runat="server" Width="100%" TextMode="MultiLine" Height="100px" Resize="Vertical" />
        </td>
    </tr>

</table>
