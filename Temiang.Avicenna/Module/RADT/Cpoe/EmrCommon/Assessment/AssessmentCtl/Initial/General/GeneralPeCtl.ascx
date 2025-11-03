<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralPeCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.GeneralPeCtl" %>
<%@ Register Src="~/Module/RADT/Cpoe/EmrCommon/Assessment/AssessmentCtl/GcsCtl.ascx" TagPrefix="uc1" TagName="GcsCtl" %>
<%@ Register TagPrefix="cc" Namespace="Temiang.Avicenna.CustomControl" Assembly="Temiang.Avicenna" %>

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
        <td>
            <asp:RadioButtonList ID="optVisus" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Normal" Value="N" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Abnormal" Value="A"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry">
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
                <asp:ListItem Text="Exist" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry"></td>
    </tr>
    <tr>
        <td class="label">Hernia</td>
        <td>
            <asp:RadioButtonList ID="optHernia" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Exist" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry"></td>
    </tr>
    <tr>
        <td class="label">Hemorrhoids</td>
        <td>
            <asp:RadioButtonList ID="optHemorrhoids" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Exist" Value="Y"></asp:ListItem>
                <asp:ListItem Text="No" Value="N" Selected="true"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="entry"></td>
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
        <td class="entry">
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
        <td class="label">Other Notes
        </td>
        <td class="entry" colspan="2">
            <telerik:RadTextBox ID="txtPhysicalExamNotes" runat="server" Width="100%" Height="80px"
                TextMode="MultiLine" Resize="Vertical" />
        </td>

    </tr>
</table>
